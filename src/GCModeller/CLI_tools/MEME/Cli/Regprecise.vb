﻿#Region "Microsoft.VisualBasic::a119734f79399a2b2796fabd12b3754b, ..\GCModeller\CLI_tools\MEME\Cli\Regprecise.vb"

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
Imports MEME.Analysis
Imports MEME.GCModeller.FileSystem.FileSystem
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CommandLine
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Data.csv
Imports Microsoft.VisualBasic.Data.csv.Extensions
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Language.UnixBash
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Linq.Extensions
Imports SMRUCC.genomics
Imports SMRUCC.genomics.Analysis.ProteinTools.Family.FileSystem
Imports SMRUCC.genomics.Analysis.ProteinTools.Interactions
Imports SMRUCC.genomics.Analysis.RNA_Seq
Imports SMRUCC.genomics.Assembly.DOOR
Imports SMRUCC.genomics.Assembly.KEGG.DBGET
Imports SMRUCC.genomics.Assembly.MiST2
Imports SMRUCC.genomics.Assembly.NCBI
Imports SMRUCC.genomics.Assembly.NCBI.GenBank
Imports SMRUCC.genomics.Assembly.NCBI.GenBank.TabularFormat
Imports SMRUCC.genomics.Assembly.NCBI.GenBank.TabularFormat.ComponentModels
Imports SMRUCC.genomics.ComponentModel.Loci
Imports SMRUCC.genomics.ContextModel
Imports SMRUCC.genomics.Data
Imports SMRUCC.genomics.Data.Regprecise
Imports SMRUCC.genomics.Data.Regprecise.WebServices
Imports SMRUCC.genomics.Data.Xfam
Imports SMRUCC.genomics.Data.Xfam.Pfam.ProteinDomainArchitecture
Imports SMRUCC.genomics.Interops.NBCR
Imports SMRUCC.genomics.Interops.NBCR.MEME_Suite.Analysis
Imports SMRUCC.genomics.Interops.NBCR.MEME_Suite.Analysis.FootprintTraceAPI
Imports SMRUCC.genomics.Interops.NBCR.MEME_Suite.Analysis.GenomeMotifFootPrints
Imports SMRUCC.genomics.Interops.NBCR.MEME_Suite.Analysis.MotifScans
Imports SMRUCC.genomics.Interops.NBCR.MEME_Suite.ComponentModel
Imports SMRUCC.genomics.Interops.NBCR.MEME_Suite.DocumentFormat
Imports SMRUCC.genomics.Interops.NBCR.MEME_Suite.DocumentFormat.MEME.LDM
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.Application
Imports SMRUCC.genomics.SequenceModel
Imports SMRUCC.genomics.SequenceModel.FASTA

Partial Module CLI

    ''' <summary>
    ''' Cluster of co-regulated orthologous operons
    ''' </summary>
    ''' <param name="args"></param>
    ''' <returns></returns>
    ''' 
    <ExportAPI("/CORN", Usage:="/CORN /in <operons.csv> /mast <mastDIR> /PTT <genome.ptt> [/out <out.csv>]")>
    Public Function CORN(args As CommandLine) As Integer
        Dim [in] As String = args("/in")
        Dim mastDIR As String = args("/mast")
        Dim out As String = args.GetValue("/out", [in].TrimSuffix & "." & mastDIR.BaseName & ".Csv")
        Dim operons As IEnumerable(Of RegPreciseOperon) = [in].LoadCsv(Of RegPreciseOperon)
        Dim result As New List(Of RegPreciseOperon)
        Dim PTT As PTT = TabularFormat.PTT.Load(args - "/PTT")
        Dim hash As New Dictionary(Of String, XmlOutput.MAST.MAST)
        Dim source As New Dictionary(Of String, String)

        For Each xml As String In ls - l - r - wildcards("*.xml") <= GCModeller.FileSystem.RegPrecise.Motif_PWM
            Dim motifs As MotifSitelog = xml.LoadXml(Of MotifSitelog)
            Dim uid As String = $"{motifs.Taxonomy.Key} {motifs.Regulog.Key}".NormalizePathString.Replace(" ", "_")
            Call source.Add(uid, motifs.Regulog.Key)
        Next

        Dim oprHash = (From x As RegPreciseOperon
                       In operons
                       Let first As String = x.Operon.__firstLocus(PTT)
                       Let uid As String = $"{first}|{x.TF_trace}|{x.source}"
                       Select uid,
                           x
                       Group By uid Into Group) _
                      .ToDictionary(Function(x) x.uid,
                                    Function(x) x.Group.First.x)

        For Each mast As XmlOutput.MAST.MAST In From path As String
                                                In ls - l - r - wildcards("*.Xml") <= mastDIR
                                                Where path.FileLength > 0
                                                Select path.LoadXml(Of XmlOutput.MAST.MAST)
            If mast.Sequences Is Nothing Then
                Continue For
            End If
            If mast.Sequences.SequenceList.IsNullOrEmpty Then
                Continue For
            End If

            Dim regulog As String = mast.Motifs.name.GetBaseName

            If source.ContainsKey(regulog) Then
                regulog = source(regulog)

                For Each hit In mast.Sequences.SequenceList
                    If oprHash.ContainsKey(hit.title) Then
                        Dim x As RegPreciseOperon = oprHash(hit.title)
                        If String.Equals(x.source, regulog, StringComparison.Ordinal) Then
                            result += x
                        End If
                    End If
                Next
            Else
                Call VBDebugger.Warning(regulog & " data not found!")
            End If
        Next

        Return result.SaveTo(out).CLICode
    End Function

    ''' <summary>
    ''' 可以根据这个来设置meme的maxw参数，因为tomquery里面的相似度的结果是和长度相关的：coverage
    ''' </summary>
    ''' <param name="args"></param>
    ''' <returns></returns>
    <ExportAPI("/LDM.MaxW", Usage:="/LDM.MaxW [/in <sourceDIR>]")>
    Public Function LDMMaxLen(args As CommandLine) As Integer
        Dim source As String = args.GetValue("/in", GCModeller.FileSystem.GetMotifLDM)
        Dim LDM = (From x As String
                   In FileIO.FileSystem.GetFiles(source, FileIO.SearchOption.SearchTopLevelOnly, "*.xml")
                   Select x.LoadXml(Of AnnotationModel)(ThrowEx:=False)).ToArray
        Dim maxW As Integer = (From x In LDM Select x.Width).Max
        Call $"MaxW in the RegPrecise MAST_LDM is {maxW}bp!".__DEBUG_ECHO
        Return 0
    End Function

    <ExportAPI("/BBH.Select.Regulators", Info:="Select bbh result for the regulators in RegPrecise database from the regulon bbh data.",
               Usage:="/BBH.Select.Regulators /in <bbh.csv> /db <regprecise_downloads.DIR> [/out <out.csv>]")>
    Public Function SelectRegulatorsBBH(args As CommandLine) As Integer
        Dim inFile As String = args("/in")
        Dim DbDIR As String = args("/db")
        Dim out As String = args.GetValue("/out", inFile.TrimSuffix & ".Regulators.bbh.csv")
        Dim inBBH = inFile.LoadCsv(Of BBH.BiDirectionalBesthit)
        Dim dict = (From x As BBH.BiDirectionalBesthit
                    In inBBH
                    Where Not String.IsNullOrEmpty(x.HitName)
                    Select x
                    Group x By x.HitName Into Group) _
                        .ToDictionary(Function(x) x.HitName.Split(":"c).Last,
                                      Function(x) x.Group.ToArray)
        Dim RegPrecise = (From file As String
                          In FileIO.FileSystem.GetFiles(DbDIR, FileIO.SearchOption.SearchTopLevelOnly, "*.xml").AsParallel
                          Select file.LoadXml(Of Regprecise.BacteriaGenome)).ToArray
        Dim regulators As String() = RegPrecise.ToArray(Function(x) x.ListRegulators).MatrixToVector
        Dim regBBH = (From sId As String In regulators.AsParallel Where dict.ContainsKey(sId) Select dict(sId)).ToArray.MatrixToList
        Return regBBH.SaveTo(out)
    End Function

    <ExportAPI("Download.Regprecise", Info:="Download Regprecise database from Web API",
               Usage:="Download.Regprecise [/work ./ /save <saveXml>]")>
    Public Function DownloadRegprecise2(args As CommandLine) As Integer
        Dim WORK As String = args.GetValue("/work", App.CurrentDirectory & "/RegpreciseDownloads/")
        Dim Db As TranscriptionFactors = WebAPI.Download(WORK)
        Dim out As String = args.GetValue("/save", App.CurrentDirectory & "/Regprecise.Xml")

        Return Db.GetXml.SaveTo(out)
    End Function

    ''' <summary>
    ''' 下载数据库
    ''' </summary>
    ''' <param name="args"></param>
    ''' <returns></returns>
    <ExportAPI("wGet.Regprecise",
               Info:="Download Regprecise database from REST API",
               Usage:="wGet.Regprecise [/repository-export <dir.export, default: ./> /updates]")>
    Public Function DownloadRegprecise(args As CommandLine) As Integer
        Dim Updates As Boolean = args.GetBoolean("/updates")
        Dim Export As String = args.GetValue(Of String)("/repository-export", "./")
        Return SMRUCC.genomics.Data.WebServices.Regprecise.wGetDownload(Export, Updates).CLICode
    End Function

    <ExportAPI("Regprecise.Compile",
               Usage:="Regprecise.Compile [<repository>]",
               Info:="The repository parameter is a directory path which is the regprecise database root directory in the GCModeller directory, if you didn't know how to set this value, please leave it blank.")>
    Public Function CompileRegprecise(args As CommandLine) As Integer
        Dim repository As String = args.Tokens.Get(1)
        If String.IsNullOrEmpty(repository) Then
            Call Settings.Session.Initialize()
            repository = RegpreciseRoot
        Else
            If FileIO.FileSystem.DirectoryExists(repository & "/Regprecise/MEME/") Then  ' 给出的参数是GCModeller数据库的根目录，则自动跳转到Regprecise数据库的根目录
                repository = repository & "/Regprecise/"
            End If
        End If
        Dim regulations As Regulations = Compiler.Compile(repository) ' 对于少于6条的序列的处理是聚集到至少或者多余6条
        Call regulations.GetXml.SaveTo($"{repository}/MEME/regulations.xml")
        Return 0
    End Function

    ''' <summary>
    ''' 对单个的mast文档进行数据导出
    ''' </summary>
    ''' <param name="args"></param>
    ''' <returns></returns>
    <ExportAPI("mast.compile",
               Usage:="mast.compile /mast <mast.xml> /ptt <genome.ptt> [/no-meme /no-regInfo /p-value 1e-3 /mast-ldm <DIR default:=GCModeller/Regprecise/MEME/MAST_LDM> /atg-dist 250]")>
    Public Function CompileMast(args As CommandLine) As Integer
        Dim PTT As TabularFormat.PTT = TabularFormat.PTT.Load(args("/ptt"))
        Dim mast As XmlOutput.MAST.MAST =
            args.GetObject("/mast", AddressOf LoadXml(Of XmlOutput.MAST.MAST))
        Dim sites As MastSites()
        Dim pvalue As Double = args.GetValue("/p-value", 0.001)
        Dim atgDist As Integer = args.GetValue("/atg-dist", 250)

        If args.GetBoolean("/no-meme") Then
            sites = __mastNoMEME(mast)
        Else
            Dim pwmFa As String = RegpreciseRoot & "/MEME/pwm"
            Dim mastLDM As String = args.GetValue("/mast-ldm", MotifLDM)

            If args.GetBoolean("/no-reginfo") Then
                pwmFa = ""
            End If

            sites = MastSites.Compile(
                mast,
                mastLDM, ' mast annotation model.
                pwmFa)
        End If

        sites = (From site In sites Where site.pValue <= pvalue Select site).ToArray

        Dim table = (From site In sites.AsParallel
                     Where Not site Is Nothing
                     Select site,
                         genes = site.GetRelatedUpstream(PTT, atgDist))
        Call "Extract duplicated genes".__DEBUG_ECHO
        Dim LQuery = (From site In table.AsParallel Select __extract(site.site, site.genes)).ToArray.MatrixToList
        Return LQuery.SaveTo(args("/mast").TrimSuffix & ".csv").CLICode
    End Function


    ''' <summary>
    ''' 批量汇编mast结果，导出调控位点的信息
    ''' </summary>
    ''' <param name="args"></param>
    ''' <returns></returns>
    <ExportAPI("mast.compile.bulk",
               Info:="Genome wide step 1",
               Usage:="mast.compile.bulk /source <source_dir> [/ptt <genome.ptt> /atg-dist <500> /no-meme /no-regInfo /p-value 1e-3 /mast-ldm <DIR default:=GCModeller/Regprecise/MEME/MAST_LDM> /related.all]")>
    <ParameterInfo("/no-meme", True,
                   Description:="Specific that the mast site construction will without and meme pwm MAST_LDM model.")>
    Public Function CompileMastBuck(args As CommandLine) As Integer
        Call $"Start loading mast documents from source {args("/source")}".__DEBUG_ECHO

        Dim masts = FileIO.FileSystem.GetFiles(args("/source"), FileIO.SearchOption.SearchAllSubDirectories, "*.xml") _
            .ToArray(Function(xml) xml.LoadXml(Of XmlOutput.MAST.MAST)(ThrowEx:=False))
        masts = (From obj In masts Where Not obj.__isNothing Select obj).ToArray
        Call $"Start compile {masts.Length} mast documents...".__DEBUG_ECHO
        Dim sites As MastSites()  ' 导出扫描得到的位点
        If args.GetBoolean("/no-meme") Then
            sites = masts.ToArray(Function(mast) MastSites.Compile(mast), Parallel:=True).MatrixToVector
        Else
            Dim pwmFa As String = RegpreciseRoot & "/MEME/pwm"
            Dim mastLDM As String = args.GetValue("/mast-ldm", MotifLDM)

            If args.GetBoolean("/no-reginfo") Then
                pwmFa = ""
            End If

            sites = masts.ToArray(
                Function(mast) MastSites.Compile(
                mast,
                mastLDM,
                faDIR:=pwmFa),
                Parallel:=True).MatrixToVector
        End If

        Dim pvalue As Double = args.GetValue("/p-value", 0.001)  ' 过滤掉不需要的位点

        sites = (From x In sites Where x.pValue <= pvalue Select x).ToArray

        Call $"Start assign {sites.Count} sites its genomics context...".__DEBUG_ECHO
        Dim PTTFile As String = args("/ptt")

        If String.IsNullOrEmpty(PTTFile) Then
            Return sites.SaveTo(args("/source") & "/mastSites.Csv").CLICode
        End If

        Dim PTT As GenBank.TabularFormat.PTT = GenBank.TabularFormat.PTT.Load(PTTFile)  ' 得到基因组上下文的信息
        Dim ATGDist As Integer = args.GetValue(Of Integer)("/atg-dist", 500)
        Dim Trims As MastSites() = Nothing

        If Not args.GetBoolean("/related.all") Then  '上游相关的位点信息
            Dim LQuery = __upstreamRelated(ATGDist, PTT, sites, Trims, args.GetBoolean("/no-meme"))

            Call "Job done! Save outputs".__DEBUG_ECHO
            Call LQuery.SaveTo(args("/source") & "/sites.csv")
            Call Trims.SaveTo(args("/source") & "/sites-trim.csv").CLICode
        Else
            ' 所有的位点信息
            Dim allRelateds = __allRelated(ATGDist, PTT, sites)
            Call allRelateds.SaveTo(args("/source") & "/mastSites.AllRelated.csv")
        End If

        Return 0
    End Function

    Private Function __allRelated(atgDist As Integer, PTT As PTT, sites As MastSites()) As MastSites()
        Call "Gets all related gene context information...".__DEBUG_ECHO

        Dim allRelated = (From site As MastSites
                          In sites.AsParallel
                          Select site,
                              related = PTT.GetRelatedGenes(site.MappingLocation, True, atgDist)).ToArray
        Call "Extract duplicated genes".__DEBUG_ECHO
        Dim LQuery As MastSites() =
            LinqAPI.Exec(Of MastSites) <= From site
                                          In allRelated.AsParallel
                                          Select __extract(site.site, site.related.ToArray(Function(g) g.Gene))
        Return LQuery
    End Function

    Private Function __upstreamRelated(atgDist As Integer,
                                       PTT As GenBank.TabularFormat.PTT,
                                       sites As MastSites(),
                                       ByRef trimSites As MastSites(),
                                       noMEME As Boolean) As MastSites()
        Dim table = (From site As MastSites
                     In sites.AsParallel
                     Where Not site Is Nothing
                     Select site,
                         genes = site.GetRelatedUpstream(PTT, atgDist)).ToArray
        Call "Extract duplicated genes".__DEBUG_ECHO
        Dim LQuery = (From site In table.AsParallel Select __extract(site.site, site.genes)).ToArray.MatrixToList
        Dim trims = (From site As MastSites
                     In LQuery
                     Where Math.Abs(site.ATGDist) <= atgDist AndAlso  ' Not site.HasEmptyMappings AndAlso  这一个条件和no-meme有冲突
                         Not String.IsNullOrEmpty(site.Gene)
                     Select site).ToArray
        If Not noMEME Then  ' 假若还有调控数据的话，则还会进一步筛选
            trims = (From x In trims Where Not x.HasEmptyMappings Select x).ToArray
        End If
        trimSites = trims
        Return LQuery.ToArray
    End Function

    Private Function __mastNoMEME(mastXml As XmlOutput.MAST.MAST) As MastSites()
        Return MastSites.Compile(mastXml)
    End Function

    ''' <summary>
    ''' 从当前的基因组之中利用规则得到一些可能的新的Motif之后再使用那个得到的新Motif比对回基因组，发现潜在的调控的联系
    ''' 和<see cref="CompileMast"/>函数所不同的是meme的数据源不同，仅此而已
    ''' </summary>
    ''' <param name="args"></param>
    ''' <returns></returns>
    <ExportAPI("--mapped-Back", Usage:="--mapped-Back /meme <meme.text> /mast <mast.xml> /ptt <genome.ptt> [/out <out.csv> /offset <10> /atg-dist <250>]")>
    Public Function SiteMappedBack(args As CommandLine) As Integer
        Dim MEME As String = args("/meme")
        Dim mastXml As String = args("/mast")

        If Not String.Equals(MEME.ParentDirName, mastXml.ParentDirName, StringComparison.OrdinalIgnoreCase) Then
            Call $"[WARNING] MEME_source='{MEME.ParentDirName}' is not equals to mast_xml='{mastXml.ParentDirName}'...".__DEBUG_ECHO
        End If

        Dim Mast As XmlOutput.MAST.MAST = mastXml.LoadXml(Of XmlOutput.MAST.MAST)
        Dim MEMEMotifs = MEME_Suite.DocumentFormat.MEME.Text.Load(MEME).ToDictionary(Function(motif) motif.Id)
        Dim PTT = TabularFormat.PTT.Load(args("/ptt"))
        Dim mastHits = Mast.Sequences.SequenceList.First.Segments
        Dim offset As Integer = args.GetValue("/offset", 10)
        Dim atgDist As Integer = args.GetValue("/atg-dist", 250)

        ' 只需要将mast文档里面的位置取出来就可以了
        ' 由于只是扫描自己的基因组，所以mast文档里面只有一条序列的
        Dim sites As MastSites() = mastHits.ToArray(Function(seq) __compile(seq, MEMEMotifs, offset)).MatrixToVector
        Dim table = (From site As MastSites
                     In sites.AsParallel
                     Where Not site Is Nothing
                     Select site,
                         genes = site.GetRelatedUpstream(PTT, atgDist))
        Call "Extract duplicated genes".__DEBUG_ECHO
        Dim LQuery = (From site In table.AsParallel Select __extract(site.site, site.genes)).ToArray.MatrixToList
        Return LQuery.SaveTo(args("/mast").TrimSuffix & ".csv").CLICode
    End Function

    Private Function __compile(hit As XmlOutput.MAST.Segment, MEMEMotifs As Dictionary(Of String, Motif), offset As Integer) As MastSites()
        Dim sequence As String = hit.SegmentData.TrimVBCrLf
        Dim resultSet = hit.Hits.ToArray(Function(loci) __compile(loci, MEMEMotifs, sequence, offset, hit.start))
        Return resultSet
    End Function

    Private Function __compile(loci As XmlOutput.MAST.HitResult,
                               MEMEMotifs As Dictionary(Of String, Motif),
                               sequence As String,
                               offset As Integer,
                               start As Integer) As MastSites
        Dim motifId As String = Regex.Match(loci.motif, "\d+").Value
        If Not MEMEMotifs.ContainsKey(motifId) Then
            Return Nothing
        End If

        Dim MEMEMotif = MEMEMotifs(motifId)
        Dim source = (From site In MEMEMotif.Sites Where site.Name.Count("_"c) <= 1 Select site).ToArray

        If source.IsNullOrEmpty Then
            Return Nothing
        End If

        Dim length As Integer = Len(loci.match) + 2 * offset  '为了保证在进行分子生物学实验的时候能够得到完整的片段，在这里将位点的范围扩大了10个bp
        start = loci.pos - start
        start -= offset
        If start <= 0 Then
            start = 1
            'Call $"{hit.pos} - {start} is not enough".__DEBUG_ECHO
        End If

        sequence = Mid(sequence, start, length)

        Dim strand As String = loci.strand.GetBriefStrandCode
        Return New MastSites With {
            .Start = loci.pos,
            .match = loci.match,
            .pValue = loci.pvalue,
            .SequenceData = sequence,
            .StrandRaw = strand,
            .Family = $"{MEMEMotif.uid}::{ String.Join(";", source.ToArray(Function(ll) ll.Name))}"
        }
    End Function

    Private Function __extract(site As MastSites, genes As IEnumerable(Of TabularFormat.ComponentModels.GeneBrief)) As MastSites()
        If genes.IsNullOrEmpty Then
            Return {site}
        End If

        Dim setValue As New SetValue(Of MastSites)
        Dim LQuery As MastSites() =
            LinqAPI.Exec(Of MastSites) <= From gene As TabularFormat.ComponentModels.GeneBrief
                                          In genes
                                          Let loci = New MastSites(site)
                                          Let atgDist = loci.GetsATGDist(gene)
                                          Select setValue _
                                              .InvokeSetValue(loci, NameOf(loci.Gene), gene.Synonym) _
                                              .InvokeSet(NameOf(loci.ATGDist), atgDist).obj
        Return LQuery
    End Function

    <Extension> Private Function __isNothing(mast As XmlOutput.MAST.MAST) As Boolean
        Try
            Return Not mast.Sequences.SequenceList.Length > 0
        Catch ex As Exception
            Return True
        End Try
    End Function

    <ExportAPI("regulators.compile", Info:="Regprecise regulators data source compiler.")>
    Public Function RegulatorsCompile() As Integer
        Dim regulatorsRepository As String = RegpreciseRoot & "/Fasta/Regulators/"
        Dim regulators As FastaReaders.Regulator() =
            FileIO.FileSystem.GetFiles(regulatorsRepository,
                                       FileIO.SearchOption.SearchAllSubDirectories,
                                       "*.fasta").ToArray(Function(fasta) FastaReaders.Regulator.LoadDocument(FastaToken.Load(fasta)))
        Dim regprecise = FileIO.FileSystem.GetFiles(RegpreciseRoot & "/regulators/",
                                                    FileIO.SearchOption.SearchAllSubDirectories, "*.xml").ToArray(
                                                    Function(xml) xml.LoadXml(Of JSONLDM.regulator())).MatrixToList
        Dim regpreciseGroup = (From regulator In regprecise
                               Where Not regulator Is Nothing AndAlso
                                   Not String.IsNullOrEmpty(regulator.locusTag)
                               Select regulator
                               Group regulator By regulator.locusTag Into Group).ToArray
        Dim regpreciseRegulators = regpreciseGroup _
               .ToDictionary(Function(regulator) regulator.locusTag,
                             Function(regulator) regulator.Group.First)
        Dim table As Model_Repository.Regulator() = regulators.ToArray(
            Function(regulator) _
                New Model_Repository.Regulator With {
                    .Definition = regulator.Definition,
                    .Family = regulator.Family,
                    .KEGG = regulator.KEGG,
                    .Regulog = regulator.Regulog,
                    .SequenceData = regulator.SequenceData,
                    .Sites = regulator.Sites,
                    .Species = regulator.SpeciesCode,
                    .vimssId = regpreciseRegulators.TryGetValue(Of Integer)(regulator.KEGG.Split(":"c).Last, NameOf(Model_Repository.Regulator.vimssId))
               })
        Return table.SaveTo(RegpreciseRoot & "/MEME/regulators.csv").CLICode
    End Function

    ''' <summary>
    ''' 联系需要注释的蛋白质在Regprecise数据库之中的信息
    ''' </summary>
    ''' <param name="args"></param>
    ''' <returns></returns>
    <ExportAPI("regulators.bbh",
               Info:="Compiles for the regulators in the bacterial genome mapped on the regprecise database using bbh method.",
               Usage:="regulators.bbh /bbh <bbhDIR/bbh.index.Csv> [/save <save.csv> /direct /regulons /maps <genome.gb>]")>
    <ParameterInfo("/regulons", True,
                   Description:="The data source of the /bbh parameter is comes from the regulons bbh data.")>
    Public Function RegulatorsBBh(args As CommandLine) As Integer
        Dim KEGGFamilies = GCModeller.FileSystem.KEGGFamilies.LoadCsv(Of FastaReaders.Regulator) _
            .ToDictionary(Function(prot) prot.KEGG)
        Dim regprecise = (RegpreciseRoot & "/MEME/regulators.csv").LoadCsv(Of Model_Repository.Regulator).ToDictionary(Function(regulator) regulator.KEGG)
        Dim direct As Boolean = args.GetBoolean("/direct")
        Dim bbhs As List(Of BBH.BBHIndex)

        If direct Then
            bbhs = args("/bbh").LoadCsv(Of BBH.BBHIndex)
        Else
            If args.GetBoolean("/regulons") Then
                Dim regulons = (From file As String
                                In FileIO.FileSystem.GetFiles(args("/bbh"), FileIO.SearchOption.SearchTopLevelOnly, "*.xml").AsParallel
                                Let regulon As BacteriaGenome = file.LoadXml(Of BacteriaGenome)
                                Where Not regulon Is Nothing AndAlso
                                    Not regulon.Regulons Is Nothing AndAlso
                                    Not regulon.Regulons.Regulators.IsNullOrEmpty
                                Select regulon.Regulons.Regulators).MatrixAsIterator
                bbhs = regulons.ToArray(
                    Function(x) New BBH.BBHIndex With {
                        .HitName = x.LocusTag.Key,
                        .QueryName = x.LocusTag.Value}).ToList
            Else
                bbhs = FileIO.FileSystem.GetFiles(
                    args("/bbh"), FileIO.SearchOption.SearchTopLevelOnly, "*.csv").ToArray(
                        Function(csv) csv.LoadCsv(Of BBH.BBHIndex)).MatrixToList
            End If
        End If

        Dim bbhsPaired = (From pair As BBH.BBHIndex
                          In bbhs.AsParallel
                          Let regEntry As String = __getEntry(pair, direct)
                          Where pair.Matched AndAlso regprecise.ContainsKey(regEntry)
                          Select pair,
                              reg = regprecise(regEntry),
                              KEGGFamily = KEGGFamilies(regEntry)).ToList
        Dim result As bbhMappings() = bbhsPaired.ToArray(
            Function(regulator) New bbhMappings With {
                .definition = regulator.reg.Definition,
                .Family = regulator.KEGGFamily.KEGGFamily,
                .hit_name = regulator.pair.HitName,
                .Identities = regulator.pair.identities,
                .Positive = regulator.pair.Positive,
                .query_name = regulator.pair.QueryName,
                .vimssId = regulator.reg.vimssId
           })

        If direct Then
            For Each pair In result
                Call pair.hit_name.SwapWith(pair.query_name)
            Next
        End If

        If args("/maps").FileExists Then
            Dim gb As GBFF.File = GBFF.File.Load(args("/maps"))
            Dim maps As Dictionary(Of String, String) = gb.LocusMaps

            For Each x In result
                If maps.ContainsKey(x.hit_name) Then
                    x.hit_name = maps(x.hit_name)
                End If
            Next
        End If

        Dim save As String = args.GetValue("/save", args("/bbh").TrimSuffix & ".Regprecise.bbh_mappings.csv")
        Return result.SaveTo(save).CLICode
    End Function

    Private Function __getEntry(bbhPair As BBH.BBHIndex, direct As Boolean) As String
        If direct Then
            Return bbhPair.HitName
        Else
            Return bbhPair.QueryName
        End If
    End Function

    ''' <summary>
    ''' 从mast sites之中得到调控信息
    ''' </summary>
    ''' <param name="args"></param>
    ''' <returns></returns>
    <ExportAPI("--build.Regulations",
               Info:="Genome wide step 2",
               Usage:="--build.Regulations /bbh <regprecise.bbhMapped.csv> /mast <mastSites.csv> [/cutoff <0.6> /out <out.csv> /sp <spName> /DOOR <genome.opr> /DOOR.extract]")>
    <ParameterInfo("/DOOR.extract", True,
                   Description:="Extract the operon structure genes after assign the operon information.")>
    Public Function Build(args As CommandLine) As Integer
        Dim bbh = RegpreciseSummary.LoadRegpreciseBBH(args("/bbh"))
        Dim mastSites As IEnumerable(Of MastSites) = RegpreciseSummary.LoadMEME(args("/mast"))
        Dim cut As Double = args.GetValue("/cutoff", 0.6)
        Dim corrs As Correlation2 = Correlation2.LoadAuto(args("/sp"))
        Dim virtualFootprints = RegpreciseSummary.GenerateRegulations(bbh, mastSites, corrs, cut)
        Dim out As String = args.GetValue("/out", args("/mast").TrimSuffix & ".VirtualFootprints.csv")
        Dim opr As String = args("/DOOR")

        If opr.FileExists Then
            Dim DOOR As DOOR = DOOR_API.Load(opr)   ' 为被调控的基因联系操纵子的信息
            Call "Assign DOOR operon information...".__DEBUG_ECHO
            virtualFootprints = virtualFootprints.AssignDOOR(DOOR)

            If args.GetBoolean("/DOOR.extract") Then
                cut *= 0.85
                Call $"Expand DOOR operon information...".__DEBUG_ECHO
                virtualFootprints = virtualFootprints.ExpandDOOR(DOOR, corrs, cut)
            End If
        End If

        Call "DONE!".__DEBUG_ECHO

        Return virtualFootprints.SaveTo(out).CLICode
    End Function

    <ExportAPI("--build.Regulations.From.Motifs",
               Usage:="--build.Regulations.From.Motifs /bbh <regprecise.bbhMapped.csv> /motifs <motifSites.csv> [/cutoff <0.6> /sp <spName> /out <out.csv>]")>
    Public Function BuildFromMotifSites(args As CommandLine) As Integer
        Dim bbh = RegpreciseSummary.LoadRegpreciseBBH(args("/bbh"))
        Dim motifSites = args("/motifs").LoadCsv(Of MotifSite)
        Dim virtualFootprints = RegpreciseSummary.GenerateRegulations(bbh, motifSites, args("/sp"), args.GetValue("/cutoff", 0.6))
        Dim out As String = args("/out")
        Dim brief As Boolean = args.GetBoolean("/brief")

        If String.IsNullOrEmpty(out) Then
            out = args("/motifs").TrimSuffix & ".virtualFootprints.csv"
        End If

        Return virtualFootprints.SaveTo(out).CLICode
    End Function

    <ExportAPI("--TCS.Regulations",
               Usage:="--TCS.Regulations /TCS <DIR.TCS.csv> /modules <DIR.mod.xml> /regulations <virtualfootprint.csv>")>
    Public Function TCSRegulations(args As CommandLine) As Integer
        Dim TCS = FileIO.FileSystem.GetFiles(args("/TCS"), FileIO.SearchOption.SearchAllSubDirectories, "*.csv") _
            .ToArray(Function(file) _
                     file.LoadCsv(Of SwissTCS.CrossTalks)).MatrixToList
        Dim mods = FileIO.FileSystem.GetFiles(args("/modules"), FileIO.SearchOption.SearchAllSubDirectories, "*.xml") _
            .ToArray(Function(file) file.LoadCsv(Of bGetObject.Module))
        Dim ModsRegulation = args("/regulations").LoadCsv(Of PredictedRegulationFootprint)
        ModsRegulation = (From regulates As PredictedRegulationFootprint
                          In ModsRegulation
                          Where Not String.IsNullOrEmpty(regulates.Regulator)
                          Select regulates).ToList
        ' 所有的双组分系统的反应调控蛋白
        Dim RR As String() = TCS.ToArray(Function(cTk) cTk.Regulator).Distinct.ToArray
        '  Dim RRMods As Dictionary(Of String, String()) =      ' 调控的代谢途径
    End Function

    Public Function FoundModules(locusId As String, mods As IEnumerable(Of bGetObject.Module)) As String()
        Dim LQuery = (From modObj As bGetObject.Module
                      In mods
                      Where Array.IndexOf(modObj.GetPathwayGenes, locusId) > -1
                      Select modObj.BriteId).ToArray
        Return LQuery
    End Function

    <ExportAPI("--TCS.Module.Regulations",
               Usage:="--TCS.Module.Regulations /MiST2 <MiST2.xml> /footprint <footprints.csv> /Pathways <KEGG_Pathways.DIR>")>
    Public Function TCSRegulateModule(args As CommandLine) As Integer
        Dim MiST2 = args("/MiST2").LoadXml(Of MiST2)
        Dim footprints = args("/footprint").LoadCsv(Of GenomeMotifFootPrints.PredictedRegulationFootprint)
        Dim Pathways = FileIO.FileSystem.GetFiles(args("/pathways"),
                                                  FileIO.SearchOption.SearchAllSubDirectories,
                                                  "*.xml").ToArray(
                                                        Function(Xml) Xml.LoadXml(Of bGetObject.Pathway))
        Dim TCS As String() = MiST2.MajorModules.First.TwoComponent.get_HisKinase.Join(MiST2.MajorModules.First.TwoComponent.GetRR).Distinct.ToArray
        Dim LQuery = (From regu As PredictedRegulationFootprint
                      In footprints.AsParallel
                      Where Not String.IsNullOrEmpty(regu.Regulator) AndAlso
                          Array.IndexOf(TCS, regu.Regulator) > -1
                      Select regu).ToArray
        Dim Csv As New DocumentStream.File
        Dim pwyBrits = BriteHEntry.Pathway.LoadFromResource.ToDictionary(Function(x) x.EntryId)
        Dim pwyGroup = (From gr In (From pwy In Pathways
                                    Let brite = pwyBrits(pwy.BriteId)
                                    Select brite,
                                        pwy
                                    Group By brite.Class Into Group).ToArray
                        Select gr.Class,
                            cate = (From pwy In gr.Group
                                    Select pwy
                                    Group pwy By pwy.brite.Category Into Group).ToArray).ToArray

        Call Csv.Add({"Total", MiST2.MajorModules.First.TwoComponent.GetRR.Length})

        For Each pwy In pwyGroup
            For Each subPwy In pwy.cate
                Dim genes = subPwy.Group.ToArray(Function(x) x.pwy.GetPathwayGenes).MatrixToList.Distinct.ToArray
                Dim fLQuery = (From regu In LQuery Where Array.IndexOf(genes, regu.ORF) > -1 Select regu).ToArray
                Call Csv.Add(pwy.Class, subPwy.Category, CStr(fLQuery.Length))
            Next
        Next

        Return Csv.Save(args("/footprint").TrimSuffix & ".TCS_Pie.csv", Encoding:=System.Text.Encoding.ASCII)
    End Function

    ''' <summary>
    ''' Regprecise之中的家族注释好像有些是错误的，使用这个来从KEGG数据库之中推测家族
    ''' </summary>
    ''' <param name="args"></param>
    ''' <returns></returns>
    <ExportAPI("--Dump.KEGG.Family", Usage:="--Dump.KEGG.Family /in <in.fasta> [/out <out.csv>]")>
    <ParameterInfo("/in", False, Description:="The RegPrecise formated title fasta file.")>
    Public Function KEGGFamilyDump(args As CommandLine) As Integer
        Dim inFile As String = args("/in")
        Dim outFile As String = args.GetValue("/out", inFile.TrimSuffix & "_KEGG.csv")
        Dim inFasta = SequenceModel.FASTA.FastaFile.Read(inFile)
        Dim Regulators = FastaReaders.Regulator.LoadDocument(inFasta)
        Return Regulators.SaveTo(outFile).CLICode
    End Function

    <ExportAPI("/Build.FamilyDb", Usage:="/Build.FamilyDb /prot <RegPrecise.prot.fasta> /pfam <pfam-string.csv> [/out <out.Xml>]")>
    Public Function BuildFamilyDb(args As CommandLine) As Integer
        Dim prot As String = args("/prot")
        Dim pfam As String = args("/pfam")
        Dim out As String = args.GetValue("/out", prot.TrimSuffix & ".Xml")
        Dim inFasta = SequenceModel.FASTA.FastaFile.Read(prot)
        Dim Regulators As FastaReaders.Regulator() = FastaReaders.Regulator.LoadDocument(inFasta)
        Dim FamilyGroups = (From reg In (From x As FastaReaders.Regulator In Regulators
                                         Let fs As String() = x.KEGGFamily.Replace("\", "/").Split("/"c)
                                         Select (From s As String
                                                 In fs
                                                 Select Family = s.ToLower.Trim,
                                                     TF = x).ToArray).ToArray.MatrixToList
                            Select reg
                            Group reg By reg.Family Into Group).ToArray
        Dim pfamHash = (From x As Pfam.PfamString.PfamString
                        In pfam.LoadCsv(Of Pfam.PfamString.PfamString)
                        Where Not StringHelpers.IsNullOrEmpty(x.PfamString)
                        Select x
                        Group x By x.ProteinId Into Group) _
                             .ToDictionary(Function(x) x.ProteinId,
                                           Function(x) x.Group.First)
        Dim FamilyDb As New FamilyPfam With {
            .Build = Now.ToString,
            .Family = (From x In FamilyGroups
                       Let source As FastaReaders.Regulator() = x.Group.ToArray(Function(xx) xx.TF)
                       Where source.Length >= 3
                       Select fs = __buildFamily(x.Family, source, pfamHash)
                       Order By fs.Family Ascending).ToArray
        }
        Return FamilyDb.SaveAsXml(out).CLICode
    End Function

    Private Function __getFamily(l As String, source As FastaReaders.Regulator()) As String
        Dim all = (From x In source Select x.KEGGFamily.Replace("\", "/").Split("/"c)).MatrixToList.Distinct.ToArray
        Dim LQuery = (From x In all Where String.Equals(l, x, StringComparison.OrdinalIgnoreCase) Select x).FirstOrDefault
        If String.IsNullOrEmpty(LQuery) Then
            LQuery = l
        End If
        Return LQuery
    End Function

    Private Function __buildFamily(Family As String,
                                   source As FastaReaders.Regulator(),
                                   pfamHash As Dictionary(Of String, Pfam.PfamString.PfamString)) As Family
        Dim LQuery = (From x As FastaReaders.Regulator In source
                      Where pfamHash.ContainsKey(x.KEGG)
                      Let pfam = pfamHash(x.KEGG)
                      Select stringPfam = SMRUCC.genomics.Analysis.ProteinTools.Family.FileSystem.PfamString.CreateObject(pfam)).ToArray
        Dim Db As Family = SMRUCC.genomics.Analysis.ProteinTools.Family.FileSystem.Family.CreateObject(__getFamily(Family, source), LQuery)
        Return Db
    End Function
End Module
