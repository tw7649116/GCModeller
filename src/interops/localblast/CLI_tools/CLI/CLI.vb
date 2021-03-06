﻿#Region "Microsoft.VisualBasic::cfe046cd52c9da1f27fa68fb51bc0ec9, ..\interops\localblast\CLI_tools\CLI\CLI.vb"

    ' Author:
    ' 
    '       asuka (amethyst.asuka@gcmodeller.org)
    '       xieguigang (xie.guigang@live.com)
    '       xie (genetics@smrucc.org)
    ' 
    ' Copyright (c) 2016 GPL3 Licensed
    ' 
    ' 
    ' GNU GENERAL PUBLIC LICENSE (GPL3)
    ' 
    ' This program is free software: you can redistribute it and/or modify
    ' it under the terms of the GNU General Public License as published by
    ' the Free Software Foundation, either version 3 of the License, or
    ' (at your option) any later version.
    ' 
    ' This program is distributed in the hope that it will be useful,
    ' but WITHOUT ANY WARRANTY; without even the implied warranty of
    ' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    ' GNU General Public License for more details.
    ' 
    ' You should have received a copy of the GNU General Public License
    ' along with this program. If not, see <http://www.gnu.org/licenses/>.

#End Region

Imports System.Runtime.CompilerServices
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CommandLine
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Data.csv
Imports Microsoft.VisualBasic.Language.UnixBash
Imports Microsoft.VisualBasic.Linq.Extensions
Imports Microsoft.VisualBasic.Scripting.MetaData
Imports Microsoft.VisualBasic.Text
Imports SMRUCC.genomics.Assembly.NCBI.GenBank
Imports SMRUCC.genomics.Interops.NCBI.Extensions
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.Application
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.Application.BBH
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.BLASTOutput
Imports SMRUCC.genomics.SequenceModel
Imports SMRUCC.genomics.SequenceModel.FASTA
Imports Entry = System.Collections.Generic.KeyValuePair(Of
    SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.Application.BatchParallel.AlignEntry,
    SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.Application.BatchParallel.AlignEntry)

<PackageNamespace("NCBI.LocalBlast", Category:=APICategories.CLI_MAN,
                  Description:="Wrapper tools for the ncbi blast+ program and the blast output data analysis program.",
                  Publisher:="amethyst.asuka@gcmodeller.org")>
Module CLI

    <ExportAPI("/Bash.Venn", Usage:="/Bash.Venn /blast <blastDIR> /inDIR <fasta.DIR> /inRef <inRefAs.DIR> [/out <outDIR> /evalue <evalue:10>]")>
    Public Function BashShell(args As CommandLine) As Integer
        Dim blastDIR As String = args("/blast")
        Dim inDIR As String = args("/inDIR")
        Dim inRefAs As String = args("/inRef")
        Dim out As String = args.GetValue("/out", inDIR & "/blast_OUT/")
        Dim evalue As String = args.GetValue("/evalue", 10)
        Dim batch As String() = BatchParallel.BashShell.VennBatch(blastDIR, inDIR, inRefAs, out, evalue)
        Return BatchParallel.ScriptCallSave(batch, out).CLICode
    End Function

    <ExportAPI("--Xml2Excel", Usage:="--Xml2Excel /in <in.xml> [/out <out.csv>]")>
    Public Function XmlToExcel(args As CommandLine) As Integer
        Dim inXml As String = args("/in")
        Dim out As String = args.GetValue("/out", inXml.TrimSuffix & ".Csv")
        Dim blastOut = inXml.LoadXml(Of XmlFile.BlastOutput)
        Dim hits = blastOut.ExportOverview.GetExcelData
        Return hits.SaveTo(out).CLICode
    End Function

    <ExportAPI("--Xml2Excel.Batch", Usage:="--Xml2Excel.Batch /in <inDIR> [/out <outDIR> /Merge]")>
    Public Function XmlToExcelBatch(args As CommandLine) As Integer
        Dim inDIR As String = args("/in")
        Dim out As String = args.GetValue("/out", inDIR & ".Exports/")
        Dim Merge As Boolean = args.GetBoolean("/merge")
        Dim MergeList As New List(Of BestHit)

        For Each inXml As String In FileIO.FileSystem.GetFiles(inDIR, FileIO.SearchOption.SearchTopLevelOnly, "*.xml")
            Dim outCsv As String = out & "/" & IO.Path.GetFileNameWithoutExtension(inXml) & ".Csv"
            Dim blastOut = inXml.LoadXml(Of XmlFile.BlastOutput)
            Dim hits = blastOut.ExportOverview.GetExcelData
            Call hits.SaveTo(outCsv)
            Call MergeList.Add(hits)
        Next

        If Merge Then
            MergeList = (From x In MergeList Select x Order By x.evalue Ascending).ToList
            Call MergeList.SaveTo(out & "/" & FileIO.FileSystem.GetDirectoryInfo(inDIR).Name & ".Merge.Csv")
        End If

        Return 0
    End Function

    ''' <summary>
    '''
    ''' </summary>
    ''' <param name="entries"></param>
    ''' <param name="isAll">只导出最好的，还是导出全部匹配上的记录的？</param>
    ''' <param name="coverage"></param>
    ''' <param name="identities"></param>
    ''' <param name="singleQuery"></param>
    ''' <param name="outDIR"></param>
    ''' <returns></returns>
    <Extension>
    Private Function __exportBBH(entries As Entry(),
                                 isAll As Boolean,
                                 coverage As Double,
                                 identities As Double,
                                 singleQuery As String,
                                 outDIR As String) As Integer
        Dim Parser As __bbhParser = [If](Of __bbhParser)(isAll, AddressOf ParseAllbbhhits, AddressOf ParsebbhBesthit)  ' 导出方法
        Dim ParsingTask = (From entry As Entry
                           In entries.AsParallel
                           Let fileEntry As KeyValuePair(Of String, String) = __orderEntry(entry, singleQuery)
                           Select entry,
                               bbh = Parser(fileEntry.Key, fileEntry.Value, coverage, identities)).ToArray

        For Each xBBH In ParsingTask
            Dim path As String = $"{outDIR}/{xBBH.entry.Key.QueryName}_vs.{xBBH.entry.Key.HitName}.bbh.csv"
            Call xBBH.bbh.SaveTo(path)
        Next

        Dim Allbbh = (From hitPair As BiDirectionalBesthit
                      In ParsingTask.ToArray(Function(sp) sp.bbh).MatrixAsIterator.AsParallel
                      Where hitPair.Matched
                      Select hitPair).ToArray  ' 最后将所有的结果进行合并然后保存
        Dim inDIR As String = FileIO.FileSystem.GetParentPath(entries.First.Key.FilePath)

        singleQuery = If(String.IsNullOrEmpty(singleQuery),
            FileIO.FileSystem.GetDirectoryInfo(inDIR).Name,
            singleQuery)

        Return Allbbh.SaveTo($"{outDIR}/{singleQuery}.AllMatched.bbh.csv").CLICode
    End Function

    ''' <summary>
    ''' 从这里批量导出bbh数据
    ''' </summary>
    ''' <param name="args"></param>
    ''' <returns></returns>
    <ExportAPI("--bbh.export",
               Info:="Batch export bbh result data from a directory.",
               Usage:="--bbh.export /in <blast_out.DIR> [/all /out <out.DIR> /single-query <queryName> /coverage <0.5> /identities 0.15]")>
    <ParameterInfo("/all", True,
                   Description:="If this all Boolean value is specific, then the program will export all hits for the bbh not the top 1 best.")>
    Public Function ExportBBH(args As CommandLine) As Integer
        Dim inDIR As String = args("/in")
        Dim isAll As Boolean = args.GetBoolean("/all")
        Dim coverage As Double = args.GetValue("/coverage", 0.5)
        Dim identities As Double = args.GetValue("/identities", 0.15)
        Dim Entries = Analysis.BBHLogs.BuildBBHEntry(inDIR)  ' 得到bbh对
        Dim singleQuery As String = args("/single-query")
        Dim outDIR As String = args.GetValue("/out", inDIR & "/bbh/")

        Return Entries.__exportBBH(isAll, coverage, identities, singleQuery, outDIR)
    End Function

    Private Function __orderEntry(entry As Entry, singleQuery As String) As KeyValuePair(Of String, String)
        If String.IsNullOrEmpty(singleQuery) Then
            Return New KeyValuePair(Of String, String)(entry.Key.FilePath, entry.Value.FilePath)
        Else
            Dim query, subject As String

            If String.Equals(singleQuery, entry.Key.QueryName, StringComparison.OrdinalIgnoreCase) Then
                query = entry.Key.FilePath
                subject = entry.Value.FilePath
            Else
                query = entry.Value.FilePath
                subject = entry.Key.FilePath
            End If

            Return New KeyValuePair(Of String, String)(query, subject)
        End If
    End Function

    Private Delegate Function __bbhParser(query As String, subject As String, coverage As Double, identities As Double) As BiDirectionalBesthit()

    Private Function ParsebbhBesthit(queryFile As String,
                                     subjectFile As String,
                                     coverage As Double,
                                     identities As Double) As BiDirectionalBesthit()

        Dim query As BlastPlus.v228 = BlastPlus.Parser.TryParse(queryFile)
        If query Is Nothing Then
            Call $"Query file {queryFile.ToFileURL} is not valid!".__DEBUG_ECHO
            Return Nothing
        End If

        Dim subject As BlastPlus.v228 = BlastPlus.Parser.TryParse(subjectFile)
        If subject Is Nothing Then
            Call $"Subject file {subjectFile.ToFileURL} is not valid!".__DEBUG_ECHO
            Return Nothing
        End If

        Dim queryBesthits = query.ExportBestHit(coverage, identities)
        Dim subjectBesthits = subject.ExportBestHit(coverage, identities)
        Dim allBBH = BBHParser.GetBBHTop(subjectBesthits, queryBesthits)
        Return allBBH
    End Function

    Private Function ParseAllbbhhits(queryFile As String, subjectFile As String, coverage As Double, identities As Double) As BiDirectionalBesthit()
        Dim query = BlastPlus.Parser.TryParse(queryFile)
        If query Is Nothing Then
            Call $"Query file {queryFile.ToFileURL} is not valid!".__DEBUG_ECHO
            Return Nothing
        End If

        Dim subject = BlastPlus.Parser.TryParse(subjectFile)
        If subject Is Nothing Then
            Call $"Subject file {subjectFile.ToFileURL} is not valid!".__DEBUG_ECHO
            Return Nothing
        End If

        Dim queryBrite = (From protHit
                          In query.Queries
                          Let res As String = protHit.QueryName.Trim
                          Where Not String.IsNullOrEmpty(res)
                          Let ID As String = res.Split.First
                          Let desc As String = Mid(res, Len(ID) + 1).Trim
                          Select ID, desc
                          Group By ID Into Group) _
                              .ToDictionary(Function(prot) prot.ID,
                                            Function(prot) prot.Group.First.desc)

        Dim Grep As TextGrepMethod = TextGrepScriptEngine.Compile("tokens ' ' first").Method
        Call query.Grep(Grep, Grep)
        Call subject.Grep(Grep, Grep)

        Dim queryBesthits = query.ExportAllBestHist(coverage, identities)
        Dim subjectBesthits = subject.ExportAllBestHist(coverage, identities)
        Dim allBBH = BBHParser.GetDirreBhAll2(subjectBesthits, queryBesthits)
        allBBH = allBBH.ToArray(Function(prot) __assignAddition(prot, queryBrite))
        Return allBBH
    End Function

    Private Function __assignAddition(bbh As BiDirectionalBesthit, descri As Dictionary(Of String, String)) As BiDirectionalBesthit
        Dim ID As String = bbh.QueryName.Split.First
        bbh.QueryName = ID
        bbh.Description = descri(ID)
        Return bbh
    End Function

    Sub New()
        Call Settings.Session.Initialize()
        Call CollectionIO.SetHandle(AddressOf ISaveCsv)
    End Sub

    <ExportAPI("--blast.self", Usage:="--blast.self /query <query.fasta> [/blast <blast_HOME> /out <out.csv>]")>
    Public Function SelfBlast(args As CommandLine) As Integer
        Dim query As String = args("/query")
        Dim blast As String = args("/blast")

        If String.IsNullOrEmpty(blast) Then
            blast = Settings.SettingsFile.BlastBin
        End If

        Dim out As String = query.TrimSuffix & ".BlastSelf.txt"
        Dim localblast As New Programs.BLASTPlus(blast)

        Call localblast.FormatDb(query, localblast.MolTypeProtein).Start(WaitForExit:=True)
        Call localblast.Blastp(query, query, out, "1e-3").Start(WaitForExit:=True)

        Dim outLog As BlastPlus.v228 = BlastPlus.Parser.TryParse(out)
        Dim hits As BestHit() = outLog.ExportOverview.GetExcelData

        out = args.GetValue("/out", out.TrimSuffix & ".Csv")

        Return hits.SaveTo(out).CLICode
    End Function

    <ExportAPI("/export.prot", Usage:="/export.prot /gb <genome.gbk> [/out <out.fasta>]")>
    Public Function ExportProt(args As CommandLine) As Integer
        Dim gb As String = args("/gb")
        Dim out As String = args.GetValue("/out", gb.TrimSuffix & "_prot.fasta")
        Dim gbk As GBFF.File = GBFF.File.Load(gb)
        Dim prot As FASTA.FastaFile = gbk.ExportProteins_Short
        Return prot.Save(out).CLICode
    End Function

    <ExportAPI("/Copys", Usage:="/Copys /imports <DIR> [/out <outDIR>]")>
    Public Function Copys(args As CommandLine) As Integer
        Dim inDIR As String = args("/imports")
        Dim gbs = FileIO.FileSystem.GetFiles(inDIR, FileIO.SearchOption.SearchAllSubDirectories, "*.gbk", "*.gb") _
            .ToArray(Function(s) GBFF.File.LoadDatabase(s), Parallel:=True).MatrixAsIterator
        Dim out As String = args.GetValue("/out", inDIR & ".fasta/")

        For Each gb As GBFF.File In gbs
            Dim prot As FastaFile = gb.ExportProteins_Short
            Dim path As String = out & "/" & gb.Locus.AccessionID & ".fasta"
            Dim nulls = (From x In prot Where String.IsNullOrEmpty(x.SequenceData) Select x).ToArray
            prot = New FastaFile(From x As FASTA.FastaToken
                                 In prot.AsParallel
                                 Where Not String.IsNullOrEmpty(x.SequenceData)
                                 Select x)
            For Each x As FastaToken In nulls
                Call VBDebugger.Warning(x.Title & "  have not sequence data!")
            Next
            Call prot.Save(path)
        Next

        For Each fasta As String In ls - l - r - wildcards("*.fasta", "*.fsa", "*.faa", "*.fa") <= inDIR
            Dim fa As FastaFile = FastaFile.Read(fasta, True)
            fa = New FastaFile((From x As FastaToken
                                In fa
                                Let id As String = Regex.Match(x.Title, "\[gene=.+?\]").Value
                                Let title As String = If(String.IsNullOrEmpty(id), x.Attributes.First, id.GetStackValue("[", "]").Split("="c).Last)
                                Select New FastaToken({title}, x.SequenceData)).ToArray)
            Dim nulls = (From x In fa Where String.IsNullOrEmpty(x.SequenceData) Select x).ToArray
            fa = New FastaFile((From x In fa.AsParallel Where Not String.IsNullOrEmpty(x.SequenceData) Select x).ToArray)
            For Each x In nulls
                Call VBDebugger.Warning(x.Title & "  have not sequence data!")
            Next
            Try
                Call fa.Save(out & "/" & fasta.BaseName.Replace(" ", "_") & ".fasta", Encodings.ASCII)
            Catch ex As Exception
                ex = New Exception(fasta, ex)
                Call App.LogException(ex)
                Call ex.PrintException
            End Try
        Next

        Return 0
    End Function
End Module
