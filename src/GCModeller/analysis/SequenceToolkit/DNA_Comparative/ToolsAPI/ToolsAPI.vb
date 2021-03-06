﻿#Region "Microsoft.VisualBasic::cf0a55725d566628b37300e52ab91b30, ..\GCModeller\analysis\SequenceToolkit\DNA_Comparative\ToolsAPI\ToolsAPI.vb"

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
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.ComponentModel
Imports Microsoft.VisualBasic.ComponentModel.DataStructures
Imports Microsoft.VisualBasic.ComponentModel.Ranges
Imports Microsoft.VisualBasic.Data.csv
Imports Microsoft.VisualBasic.Data.csv.DocumentStream
Imports Microsoft.VisualBasic.Data.csv.Extensions
Imports Microsoft.VisualBasic.Data.csv.StorageProvider.ComponentModels
Imports Microsoft.VisualBasic.Data.csv.StorageProvider.Reflection
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Scripting.MetaData
Imports Microsoft.VisualBasic.Serialization.JSON
Imports Microsoft.VisualBasic.Terminal.Utility
Imports Microsoft.VisualBasic.Text
Imports Microsoft.VisualBasic.Text.Similarity
Imports SMRUCC.genomics.Assembly.NCBI.GenBank
Imports SMRUCC.genomics.Assembly.NCBI.GenBank.CsvExports
Imports SMRUCC.genomics.Assembly.NCBI.GenBank.Extensions
Imports SMRUCC.genomics.Assembly.NCBI.GenBank.TabularFormat
Imports SMRUCC.genomics.Assembly.NCBI.GenBank.TabularFormat.ComponentModels
Imports SMRUCC.genomics.ComponentModel
Imports SMRUCC.genomics.ComponentModel.Loci
Imports SMRUCC.genomics.Interops.NCBI.Extensions.Analysis
Imports SMRUCC.genomics.SequenceModel
Imports SMRUCC.genomics.SequenceModel.FASTA
Imports SMRUCC.genomics.SequenceModel.NucleotideModels
Imports SMRUCC.genomics.SequenceModel.NucleotideModels.NucleicAcid

<[PackageNamespace]("ComparativeGenomics.Sigma-Difference",
                    Description:="Calculates the nucleotide sequence Delta similarity to measure how closed between the two sequence.",
                    Cites:="Karlin, S., et al. (1998). ""Comparative DNA analysis across diverse genomes."" Annu Rev Genet 32: 185-225.
	We review concepts and methods for comparative analysis of complete genomes including assessments of genomic compositional contrasts based on dinucleotide and tetranucleotide relative abundance values, identifications of rare and frequent oligonucleotides, evaluations and interpretations of codon biases in several large prokaryotic genomes, and characterizations of compositional asymmetry between the two DNA strands in certain bacterial genomes. The discussion also covers means for identifying alien (e.g. laterally transferred) genes and detecting potential specialization islands in bacterial genomes.

", Publisher:="xie.guigang@gcmodeller.org")>
<Cite(Title:="Comparative DNA analysis across diverse genomes",
      Journal:="Annu Rev Genet", PubMed:=9928479,
      Pages:="185-225",
      AuthorAddress:="Department of Mathematics, Stanford University, California 94305-2125, USA.",
      Abstract:="We review concepts and methods for comparative analysis of complete genomes including assessments of genomic compositional contrasts based on dinucleotide and tetranucleotide relative abundance values, identifications of rare and frequent oligonucleotides, evaluations and interpretations of codon biases in several large prokaryotic genomes, and characterizations of compositional asymmetry between the two DNA strands in certain bacterial genomes. 
The discussion also covers means for identifying alien (e.g. laterally transferred) genes and detecting potential specialization islands in bacterial genomes.",
      Authors:="Karlin, S.
Campbell, A. M.
Mrazek, J.",
      Keywords:="Animals
Bacteria/genetics
Base Composition
Base Sequence
Codon/genetics
DNA/chemistry/*genetics
DNA, Bacterial/genetics
Eukaryotic Cells
*Genome
Genome, Bacterial
Prokaryotic Cells
Species Specificity",
      DOI:="10.1146/annurev.genet.32.1.185", ISSN:="0066-4197 (Print)
0066-4197 (Linking)", Issue:="", Volume:=32, Year:=1998)>
Public Module ToolsAPI

    <ExportAPI("Read.Csv.Chromsome_Partitioning")>
    Public Function ReadPartitionalData(path As String) As ChromosomePartitioningEntry()
        Return path.LoadCsv(Of ChromosomePartitioningEntry)(False).ToArray
    End Function

    <ExportAPI("Simple.Partition.Create")>
    Public Function CreateSimplePartition(genbank As GBFF.File, data As IEnumerable(Of ChromosomePartitioningEntry)) As PartitioningData()
        Dim Reader As New SegmentReader(genbank.Origin.ToFasta)
        Dim dGroup = From x As ChromosomePartitioningEntry
                     In data
                     Select x
                     Group By x.PartitioningTag Into Group
        Dim Partitions = (From part
                          In dGroup
                          Let id As String() = part.Group.ToArray(Function(x) x.ORF)
                          Select part.PartitioningTag,
                              id).ToArray
        Dim ORFPartitions = From ORF As GeneBrief
                            In GbkffExportToPTT(genbank).GeneObjects
                            Let InternalGetPTag = (From p
                                                   In Partitions
                                                   Where Array.IndexOf(p.id, ORF.Synonym) > -1
                                                   Select p.PartitioningTag).FirstOrDefault
                            Select ORF,
                                 InternalGetPTag,
                                 SequenceData = Reader.TryParse(ORF.Location)
                            Group By InternalGetPTag Into Group
        Dim LQuery As PartitioningData() =
            LinqAPI.Exec(Of PartitioningData) <= From pInfo
                                                 In ORFPartitions
                                                 Let Loci = New IntRange((From ORF As GeneBrief
                                                                          In pInfo.Group.Select(Function(x) x.ORF)
                                                                          Let pt As NucleotideLocation = ORF.Location
                                                                          Select {pt.Left, pt.Right}).MatrixAsIterator)
                                                 Let St As Integer = Loci.Max
                                                 Let SP As Integer = Loci.Min
                                                 Let Sequence As String = Reader.GetSegmentSequence(SP, St)
                                                 Select New PartitioningData With {
                                                     .GenomeID = genbank.Accession.AccessionId,
                                                     .LociLeft = SP,
                                                     .LociRight = St,
                                                     .ORFList = (From ORF In pInfo.Group Select ORF.ORF.Synonym).ToArray,
                                                     .PartitioningTag = pInfo.InternalGetPTag,
                                                     .SequenceData = Sequence
                                                 }
        Return LQuery
    End Function

    ''' <summary>
    ''' 计算基因组之中的不同的功能分段之间的同质性
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <ExportAPI("Partition.Similarity.Calculates")>
    Public Function PartitionSimilarity(<Parameter("Partitioning.Data")> data As IEnumerable(Of PartitioningData)) As <FunctionReturns("")> DocumentStream.File
        Dim DF As DataFrame = DocumentStream.DataFrame.CreateObject(data.ToCsvDoc(False))
        Dim DataSource = DF.CreateDataSource
        Dim DeltaLQuery = (From i As Integer
                           In data.Sequence
                           Let pInfo As PartitioningData = data(i)
                           Let nt1 = New NucleotideModels.NucleicAcid(pInfo)
                           Select (From j As Integer
                                   In data.Sequence
                                   Let pInfo2 As PartitioningData = data(j)
                                   Let nt2 = New NucleotideModels.NucleicAcid(pInfo2)
                                   Let Delta As String = (1000 * DNA_Comparative.Sigma(nt1, nt2)).ToString
                                   Select Idx = i, j, Delta)).MatrixToList '为了保证顺序，这里也不可以使用并行化

        For Each Row In DeltaLQuery
            Call Row.GetJson.__DEBUG_ECHO
            Call DataSource(Row.Idx).SetAttributeValue(String.Format("Delta({0}, {1})", Row.Idx, Row.j), Row.Delta)
        Next

        Return DF
    End Function

    <ExportAPI("Partitions.Creates")>
    Public Function PartionDataCreates(<Parameter("Raw.Partitions")> PartitionRaw As DocumentStream.DataFrame,
                                       <Parameter("Column.Tag")> TagCol As String,
                                       <Parameter("Column.Start")> StartTag As String,
                                       <Parameter("Column.Stop")> StopTag As String,
                                       <Parameter("Nt.Source")> Nt As FastaToken) As PartitioningData()
        Dim Reader As New SegmentReader(Nt)
        Dim LQuery As PartitioningData() =
            LinqAPI.Exec(Of PartitioningData) <= From row As DynamicObjectLoader
                                                 In PartitionRaw.CreateDataSource
                                                 Let Tag As String = row(TagCol)
                                                 Select __getSequence(row, Tag, Reader, StartTag, StopTag, Nt)
        Return LQuery
    End Function

    Private Function __getSequence(row As DynamicObjectLoader,
                                   tag As String,
                                   Reader As SegmentReader,
                                   StartTag As String,
                                   <Parameter("Column.Stop")> StopTag As String,
                                   Nt As FastaToken) As PartitioningData
        Dim Left As String = row(StartTag).Replace(",", "")
        Dim Right As String = row(StopTag).Replace(",", "")
        Dim Join As Boolean = False

        If InStr(Left, "to", CompareMethod.Text) > 0 AndAlso InStr(Right, "to", CompareMethod.Text) > 0 Then
            Join = True
            Left = Left.Split.First
            Right = Right.Split.Last
        End If

        Dim Seq As String = If(Join, Reader.ReadJoinLocation(Val(Left), Val(Right)), Reader.TryParse(Val(Left), Val(Right) - Val(Left)))
        Return New PartitioningData With {
            .PartitioningTag = tag,
            .GenomeID = Nt.Title,
            .SequenceData = Seq,
            .LociLeft = Val(Left),
            .LociRight = Val(Right)
        }
    End Function

    ''' <summary>
    ''' 使用某一个标尺来计算基因组之中的序列片段的同质性的差异
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <param name="Sp">标尺片段的结束位置</param>
    ''' <param name="St">标尺片段的起始位置</param>
    <ExportAPI("Measure.Homogeneity")>
    Public Function MeasureHomogeneity(PartitionData As IEnumerable(Of PartitioningData),
                                       <Parameter("With.Rule")> Rule As FastaToken,
                                       St As Integer,
                                       Sp As Integer) As DocumentStream.DataFrame
        Dim Reader As New SegmentReader(Rule)
        Dim fa As New FastaToken With {
            .SequenceData = Reader.TryParse(St, Sp - St)
        }
        Dim RuleSegment As New NucleotideModels.NucleicAcid(fa)
        Dim Df = DocumentStream.DataFrame.CreateObject(PartitionData.ToCsvDoc(False))
        Dim i As Integer

        For Each Partition As DynamicObjectLoader In Df.CreateDataSource
            Dim Sequence = New NucleotideModels.NucleicAcid(PartitionData(i).ToFasta)
            Dim Delta As Double = DifferenceMeasurement.Sigma(Sequence, RuleSegment) * 1000
            Partition.SetAttributeValue(Rule.Title, Delta)
            i += 1
        Next

        Return Df
    End Function

    ''' <summary>
    ''' >Region1(1492-6218)
    ''' </summary>
    ''' <param name="Fasta"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    <ExportAPI("PartitioningData.From.Fasta")>
    Public Function PartitioningDataFromFasta(<Parameter("Path.Nt.Fasta")> Fasta As String) As PartitioningData()
        Dim FastaFile As FastaFile = FastaFile.Read(Fasta)
        Dim LQuery = (From FastaObject In FastaFile.AsParallel Select __regionMetaParser(FastaObject)).ToArray
        Return LQuery
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Fasta">>Region1(1492-6218)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function __regionMetaParser(Fasta As FastaToken) As PartitioningData
        Dim Loci As Integer() = (From m As Match
                                 In Regex.Matches(Regex.Match(Fasta.Title, "\(\d+[-]\d+\)").Value, "\d+")
                                 Select CInt(Val(m.Value))).ToArray
        Dim Left = Loci.First
        Dim Right = Loci.Last
        Dim pData As New PartitioningData With {
            .PartitioningTag = Fasta.Title,
            .LociLeft = Left,
            .LociRight = Right,
            .SequenceData = Fasta.SequenceData
        }
        Return pData
    End Function

    ''' <summary>
    ''' 获取默认的外标尺：基因组之中的dnaA-gyrB之间的序列
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    <ExportAPI("Get.Ref_Rule",
               Info:="Gets the segment betweens the dnaA and gyrB nucleotide sequence as the default reference rule for the homogeneity measuring.")>
    Public Function GetReferenceRule(Nt As FastaToken, PTT As PTT) As FastaToken
        Dim dnaA = MatchGene(PTT, "dnaA", {"chromosomal replication initiator protein DnaA", "chromosomal replication initiator"})
        Dim gyrB = MatchGene(PTT, "gyrB", {"DNA gyrase B subunit", "DNA gyrase, B subunit"})

        If (dnaA Is Nothing OrElse gyrB Is Nothing) Then
            Call $"Could not found gene dnaA or gyrB on {Nt.Title}".PrintException
            Return Nothing
        End If

        Dim Reader As New SegmentReader(Nt)
        Dim St As Integer = dnaA.Location.Left
        Dim Sp As Integer = gyrB.Location.Right

        If dnaA.Location.Strand = Strands.Reverse Then
            St = gyrB.Location.Left
            Sp = dnaA.Location.Right
        End If

        Dim RuleSegment As NucleotideModels.NucleicAcid

        Try
            RuleSegment = New NucleotideModels.NucleicAcid(New FastaToken With {.SequenceData = Reader.TryParse(St, Sp - St)})
            If RuleSegment.Length > 10 * 1000 Then
                Call $"Location exception on (""{Nt.Title}"") parsing segment.".PrintException
                Return Nothing
            End If
        Catch ex As Exception
            Call App.LogException(ex)
            Call ex.PrintException
            Return Nothing
        End Try

        Return New FastaToken With {
            .Attributes = New String() {"dnaA-gyrB", Nt.Title},
            .SequenceData = RuleSegment.SequenceData
        }
    End Function

    <ExportAPI("Measure.Homogeneity", Info:="Batch measuring the homogeneity property using a specific rule sequence between the dnaA and gyrB gene.")>
    Public Function MeasureHomogeneity(PartitionData As IEnumerable(Of PartitioningData),
                                       <Parameter("Dir.Rule.Source",
                                                  "The original GenBank dowunload data directory which should contains the *.ptt " &
                                                  "file for parsing the rule sequence between dnaA and gyrB gene and the *.fna file for parsing the " &
                                                  "genome nt fasta sequence.")>
                                       RuleSource As String) As DocumentStream.DataFrame

        Dim Df = DocumentStream.DataFrame.CreateObject(PartitionData.ToCsvDoc(False))
        Call Df.AppendLine({"GC%"})
        Dim Ddf = Df.CreateDataSource

        For Each Folder As String In FileIO.FileSystem.GetDirectories(RuleSource)
            Dim genes = From path
                        In Folder.LoadSourceEntryList({"*.ptt"}).AsParallel
                        Let Ptt As PTT = PTT.Load(path.Value),
                            ID = path.Key,
                            Name = path.Value.BaseName
                        Where Not Ptt Is Nothing
                        Let dnaA = MatchGene(Ptt, "dnaA", {"chromosomal replication initiator protein DnaA", "chromosomal replication initiator"}),
                            gyrB = MatchGene(Ptt, "gyrB", {"DNA gyrase B subunit", "DNA gyrase, B subunit"})
                        Where Not (dnaA Is Nothing OrElse gyrB Is Nothing)
                        Select dnaA,
                            gyrB,
                            ID,
                            Name

            For Each locus In genes
                Call locus.__DEBUG_ECHO

                Dim Rule As FastaToken = FastaToken.LoadNucleotideData(Folder & "/" & locus.ID & ".fna")

                If Rule Is Nothing Then
                    Continue For
                End If

                Dim Reader As New SegmentReader(Rule)
                Dim St As Integer = locus.dnaA.Location.Left
                Dim Sp As Integer = locus.gyrB.Location.Right

                If locus.dnaA.Location.Strand = Strands.Reverse Then
                    St = locus.gyrB.Location.Left
                    Sp = locus.dnaA.Location.Right
                End If

                Dim RuleSegment As NucleotideModels.NucleicAcid

                Try
                    Dim fa As New FastaToken With {
                        .SequenceData = Reader.TryParse(St, Sp - St)
                    }
                    RuleSegment = New NucleotideModels.NucleicAcid(fa)
                    If RuleSegment.Length > 10 * 1000 Then
                        Continue For
                    End If

                    Call Console.WriteLine(locus.dnaA.Gene & "  ---> " & locus.dnaA.Product)
                    Call Console.WriteLine(locus.gyrB.Gene & "  ---> " & locus.gyrB.Product)
                Catch ex As Exception
                    Call App.LogException(ex)
                    Continue For
                End Try

                For i As Integer = 0 To Ddf.Length - 2
                    Dim Partition As DynamicObjectLoader = Ddf(i)
                    Dim Sequence As New NucleotideModels.NucleicAcid(PartitionData(i).ToFasta)
                    Dim Delta As Double = DifferenceMeasurement.Sigma(Sequence, RuleSegment) * 1000
                    Call Partition.SetAttributeValue(locus.Name, Delta)
                Next

                Call Ddf.Last.SetAttributeValue(locus.Name, RuleSegment.GC)
            Next
        Next

        Return Df
    End Function

    Private Function __safeCreates(fa As FastaToken) As SegmentReader
        Try
            Dim Reader = New SegmentReader(fa, False)
            Return Reader
        Catch ex As Exception
            ex = New Exception(fa.Title, ex)
            Call ex.PrintException
            Return Nothing
        End Try
    End Function

    <ExportAPI("partition_data.create")>
    Public Function CreateChromesomePartitioningData(besthit As BestHit,
                                                     partitions As IEnumerable(Of ChromosomePartitioningEntry),
                                                     allCDSInfo As IEnumerable(Of GeneDumpInfo),
                                                     faDIR As String) As PartitioningData()

        Dim resource = faDIR.LoadSourceEntryList({}) '加载Fasta资源数据
        Dim FastaReader = (From entry As KeyValuePair(Of String, String)
                           In resource.AsParallel
                           Let fa = FastaToken.Load(entry.Value)
                           Select ID = entry.Key,
                               Reader = __safeCreates(fa)) _
                              .ToDictionary(Function(item) item.ID,
                                            Function(item) item.Reader)
        Dim pSource = (From part In partitions
                       Select part
                       Group part By part.PartitioningTag Into Group)
        Dim pData = (From nn In pSource.AsParallel
                     Let queryEntries As String() = (From item In nn.Group Select item.ORF).ToArray
                     Select nn.PartitioningTag,
                         besthits = (From item As HitCollection In besthit.hits
                                     Where Array.IndexOf(queryEntries, item.QueryName) > -1
                                     Select item).ToArray).ToArray
        Dim CDSInfo = (From g As GeneDumpInfo
                       In allCDSInfo
                       Where Not String.IsNullOrEmpty(g.LocusID)
                       Select g).ToDictionary(Function(obj) obj.LocusID)
        Dim CreatePartitionLQuery = (From item In pData.AsParallel
                                     Let Create = ToolsAPI.__group(item.besthits)
                                     Select item.PartitioningTag,
                                         Data = (From hit In Create Select GenomeID = hit.Key, ORF = (From h In hit.Value Select CDSInfo(h.HitName)).ToArray).ToList) _
                                         .ToDictionary(Function(item) item.PartitioningTag,
                                                       Function(item) item.Data) '根据参数partition之中的参照数据进行创建基因组分区数据的创建
        Dim LQuery = (From item In CreatePartitionLQuery
                      Select (From genome In item.Value
                              Let left As Integer = (From nn In genome.ORF Select nn.Left).Min
                              Let right As Integer = (From nn In genome.ORF Select nn.Right).Max
                              Let seq As String = __readSeq(left, right, FastaReader, genome.GenomeID)
                              Select New PartitioningData With {
                                  .GenomeID = genome.GenomeID,
                                  .LociLeft = left,
                                  .LociRight = right,
                                  .SequenceData = seq,
                                  .ORFList = (From nnnn In genome.ORF Select nnnn.LocusID).ToArray,
                                  .PartitioningTag = item.Key}).ToArray).MatrixToList

        Dim removed = CType((From item In LQuery.AsParallel Where String.Equals(CType(item.GenomeID, String), CType(besthit.sp, String), CType(StringComparison.OrdinalIgnoreCase, StringComparison)) Select item).ToArray, PartitioningData())
        For Each item In removed
            Call LQuery.Remove(item)
        Next

        For Each p In (From item In partitions Select item Group item By item.PartitioningTag Into Group).ToArray
            Dim left = (From nn In p.Group Let l = CDSInfo(nn.ORF).Left Select l).ToArray.Min
            Dim Right = (From nn In p.Group Let r = CDSInfo(nn.ORF).Right Select r).ToArray.Max
            Dim genomeID As String = besthit.sp

            Dim part As New PartitioningData With {
                .GenomeID = genomeID,
                .LociLeft = left,
                .LociRight = Right,
                .SequenceData = __readSequence(FastaReader, genomeID, left, Right),
                .ORFList = (From nn In p.Group Select nn.ORF).ToArray,
                .PartitioningTag = p.PartitioningTag
            }
            Call LQuery.Add(part)
        Next
        Return LQuery.ToArray
    End Function

    Private Function __readSeq(left As Integer, right As Integer, faReader As Dictionary(Of String, SegmentReader), genomeId As String) As String
        If Not faReader.ContainsKey(genomeId) Then
            Call $"The genome id ""{genomeId}"" is not exists in the fasta source...".__DEBUG_ECHO
            Return ""
        End If

        Dim l, r As Integer
        l = left
        r = right
        If l > r Then  '反向的，所以左边会大于右边
            Dim t = l
            l = r
            r = t
        End If

        Dim reader = faReader(genomeId)
        Dim seq As String = If(reader Is Nothing, "", reader.GetSegmentSequence(l, r))
        Return seq
    End Function

    Private Function __readSequence(fastaReader As Dictionary(Of String, SegmentReader),
                                    genomeId As String,
                                    left As Integer,
                                    right As Integer) As String
        If Not fastaReader.ContainsKey(genomeId) Then
            Call $"The genome id ""{genomeId}"" is not exists in the fasta source...".__DEBUG_ECHO
            Return ""
        End If

        Dim l, r As Integer
        l = left
        r = right

        If l > r Then  '反向的，所以左边会大于右边
            Dim t = l
            l = r
            r = t
        End If

        Dim reader = fastaReader(genomeId)
        Dim seq As String = If(reader Is Nothing, "", reader.GetSegmentSequence(l, r))
        Return seq
    End Function

    <ExportAPI("write.csv.genome_partition_data")>
    Public Function WritePartionalData(dat As IEnumerable(Of PartitioningData), saveto As String) As Boolean
        Return dat.SaveTo(saveto, False)
    End Function

    <ExportAPI("read.csv.genome_partition_data")>
    Public Function ReadPartitioningData(path As String) As PartitioningData()
        Return path.LoadCsv(Of PartitioningData)(False).ToArray
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    <ExportAPI("delta_diff.partitioning_query", Info:="Please notice that the query parameter is sensitive to the character case.")>
    Public Function PartitioningSigmaCompareWith(source As IEnumerable(Of PartitioningData), query As String, EXPORT As String, Optional winSize As Integer = 1000) As Boolean
        Using pb = New CBusyIndicator(_start:=True)
            Dim MAT = (From nn In (From item In source Select item Group By item.PartitioningTag Into Group).ToArray
                       Select nn.PartitioningTag,
                           dict = nn.Group.ToDictionary(Function(item) item.GenomeID)) _
                             .ToDictionary(Function(item) item.PartitioningTag,
                                           Function(item) item.dict)  '按照基因组分区进行分组
            Dim QuerySource = (From item In MAT Select item.Value(query)).ToArray
            Call $"Fasta data load done!, start to calculates the sigma differences in window_size {winSize / 1000}KB....".__DEBUG_ECHO

            Dim LQuery = (From queryItem In QuerySource.AsParallel
                          Where Not String.IsNullOrEmpty(queryItem.SequenceData) OrElse
                              queryItem.SequenceData.Length = 1
                          Select __query(queryItem, subject:=MAT(queryItem.PartitioningTag), windowsSize:=winSize, EXPORT:=EXPORT, ptag:=queryItem.PartitioningTag)).ToArray
            Return Not LQuery.IsNullOrEmpty
        End Using
    End Function

    Private Function __query(querySource As PartitioningData, subject As Dictionary(Of String, PartitioningData), windowsSize As Integer, EXPORT As String, ptag As String) As Boolean
        Dim Windows = New NucleotideModels.NucleicAcid(querySource.SequenceData).ToArray.CreateSlideWindows(windowsSize)
        Dim InternalCache As Cache() = (From Window In Windows
                                        Let cacheData = New NucleicAcid(Window.Elements)
                                        Select New Cache With {
                                            .Cache = cacheData,
                                            .SlideWindow = Window}).ToArray 'Internal create cache data.
        Call $"[INFO] query for the Sigma difference calculation in length of {querySource.SequenceData.Length / 1000}KB...".__DEBUG_ECHO

        Dim EmptySigma As SiteSigma() = New SiteSigma() {}

        Dim LQuery = (From SubjectData As KeyValuePair(Of String, PartitioningData) In subject
                      Let SubjectFasta As FastaToken = SubjectData.Value.ToFasta
                      Let procResult As KeyValuePair(Of String, SiteSigma()) =
                          __process(SubjectData, SubjectFasta, EmptySigma, InternalCache, querySource, EXPORT)
                      Where Not procResult.Value.IsNullOrEmpty
                      Select procResult).ToArray

        Dim FileName As String = String.Format("{0}/Compiled/{1}_{2}.csv", EXPORT, querySource.Title.NormalizePathString, ptag)
        Dim File = __compile(LQuery)  '只是得到了单个分区的数据

        Call Console.WriteLine("[JOB DONE]")
        Return File.Save(FileName, False)
    End Function

    Private Function __process(SubjectData As KeyValuePair(Of String, PartitioningData),
                               SubjectFasta As FastaToken,
                               EmptySigma As SiteSigma(),
                               InternalCache As Cache(),
                               querySource As PartitioningData,
                               EXPORT As String) As KeyValuePair(Of String, SiteSigma())
        Call $"Start the calculation threads ""{SubjectData.Key}""... ".__DEBUG_ECHO
        Dim sigma = If(String.IsNullOrEmpty(SubjectFasta.SequenceData) OrElse SubjectFasta.SequenceData.Length = 1,
            EmptySigma,
            __genomeSigmaDiff(InternalCache, SubjectFasta))
        Dim f As String = querySource.Title.Split(CChar("|")).First.NormalizePathString
        Dim g As String = SubjectFasta.Title.Split(CChar("|")).First.NormalizePathString
        Dim path As String = String.Format("{0}/{1}-{2}.csv", EXPORT, f, g)
        Call $"Calculation job done, trying to export data to filesystem {path}".__DEBUG_ECHO
        Call sigma.SaveTo(path, False)
        Return New KeyValuePair(Of String, SiteSigma())(g, sigma)
    End Function

    Private Function __group(besthits As HitCollection()) As KeyValuePair(Of String, Hit())()
        Dim gr = (From o In (From nn As HitCollection
                             In besthits
                             Select (From nnnnnnnn In nn.Hits Select nnnnnnnn).ToArray).MatrixToList
                  Select o
                  Group o By o.tag Into Group).ToArray
        Dim gd = (From iteddm In gr Select iteddm.tag, hits = (From fd In iteddm.Group.ToArray Where Not String.IsNullOrEmpty(fd.HitName) Select fd).ToArray).ToArray
        Dim LQuery = (From item In gd Select New KeyValuePair(Of String, Hit())(item.tag, item.hits)).ToArray
        Return LQuery
    End Function

    <ExportAPI("CAI")>
    Public Function CAI(ORF As FastaToken) As CAITable
        Return New CAITable(New RelativeCodonBiases(ORF))
    End Function

    <ExportAPI("write.xml.cai")>
    Public Function SaveCAI(dat As CAITable, saveXml As String) As Boolean
        Return dat.GetXml.SaveTo(saveXml)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="genes"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' SpeciesID, CAI, CUBIAS_LIST
    ''' src1
    ''' src2
    ''' src3
    ''' ......
    ''' </remarks>
    <ExportAPI("compile.cai")>
    Public Function CompileCABIAS(genes As String, Optional workTEMP As String = "./CAI_Xml") As DocumentStream.File
        Dim LQueryLoadFasta = (From path As String
                               In FileIO.FileSystem.GetFiles(genes, FileIO.SearchOption.SearchTopLevelOnly, "*.fasta", "*.fsa").AsParallel
                               Let FASTA = FastaFile.LoadNucleotideData(path, True)
                               Where Not FASTA.IsNullOrEmpty
                               Select ID = IO.Path.GetFileNameWithoutExtension(path),
                                   FASTA)
        Dim CAILQuery = (From item In LQueryLoadFasta.AsParallel
                         Let InternalId As String = item.ID
                         Select __compileCAIBIASCalculationThread(item.FASTA, workTEMP, InternalId)).MatrixToVector

        Dim Csv = __compileCAI(data:=CAILQuery)
        Return Csv
    End Function

    ''' <summary>
    ''' 按照功能分组数据<see cref="ChromosomePartitioningEntry"></see>进行比较基因组学分析报告文件的生成
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="CDSInfo"><see cref="SiteSigma.Site"></see>位置上面的基因的信息</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' 
    ''' Description QueryProtein PartionTAG genome1.delta genome1.similarity genome2.delta genome2.similarity
    ''' dsc1  a   1
    ''' dsc2  b   2
    ''' dsc3  c   3
    ''' 
    ''' </remarks>
    <ExportAPI("diff.create_report", Info:="The source parameter is the source directory of the Delta query export dirtectory.")>
    Public Function GenerateDeltaDiffReport(source As String,
                                            partitions As IEnumerable(Of ChromosomePartitioningEntry),
                                            CDSInfo As IEnumerable(Of GeneDumpInfo)) As DocumentStream.File

        Dim p = (From item In partitions Select item Group By item.PartitioningTag Into Group).ToArray  '分组，选择蛋白质
        Dim DeltaQuery = (From path As KeyValuePair(Of String, String)
                          In gbExportService.LoadGbkSource(source).AsParallel
                          Select ID = path.Key, dat = path.Value.LoadCsv(Of SiteSigma)(False).ToArray).ToArray
        Throw New NotImplementedException
    End Function

    Public Function __compileCAIBIASCalculationThread(gene_source As FastaFile, WorkTemp As String, InternalID As String) As KeyValuePair(Of String, CAITable)()
        Dim ResultList = New List(Of KeyValuePair(Of String, CAITable))

        For i As Integer = 0 To gene_source.Count - 1
            Dim Sequence As SMRUCC.genomics.SequenceModel.FASTA.FastaToken = gene_source(i)
            Dim Path As String = String.Format("({0}){1}", InternalID, Sequence.Attributes.First.NormalizePathString)
            Dim SeqID As String = Path
            Dim CAIData As CAITable

            Path = WorkTemp & "/" & Path & ".xml"

            If FileIO.FileSystem.FileExists(Path) Then
                CAIData = Path.LoadXml(Of CAITable)()
            Else
                CAIData = New CAITable(New RelativeCodonBiases(Sequence))
                Call CAIData.GetXml.SaveTo(Path)
            End If

            Call ResultList.Add(New KeyValuePair(Of String, CAITable)(SeqID, CAIData))
            Call Console.Write("{0}  ==>{1}%", SeqID, i / gene_source.Count * 100)
        Next

        Return ResultList.ToArray
    End Function

    Public Function CompileCAIBIASCalculationThread_p(gene_source As FastaFile, WorkTemp As String, InternalID As String) As KeyValuePair(Of String, CAITable)()
        Dim ResultList = (From Sequence As FastaToken
                          In gene_source.AsParallel
                          Let Path As String = String.Format("({0}){1}", InternalID, Sequence.Attributes.First.NormalizePathString)
                          Let SeqID As String = Path
                          Let CAIData As CAITable = __createTable(WorkTemp, Path, Sequence, SeqID)
                          Select New KeyValuePair(Of String, CAITable)(SeqID, CAIData)).ToArray
        Return ResultList.ToArray
    End Function

    Private Function __createTable(workTMP As String, path As String, Sequence As FastaToken, seqId As String) As CAITable
        Dim XMLPath = workTMP & "/" & path & ".xml"
        Dim da As CAITable

        If FileIO.FileSystem.FileExists(XMLPath) Then
            da = XMLPath.LoadXml(Of CAITable)()
        Else
            da = New CAITable(New RelativeCodonBiases(Sequence))
            Call da.GetXml.SaveTo(XMLPath)
        End If

        Call $"""{seqId}"" job done!!!".__DEBUG_ECHO

        Return da
    End Function

    <ExportAPI("Compile.CAI")>
    Public Function CompileCAIBIASCalculationThread(genes As FastaFile, Optional WorkTemp As String = "./CAI_Xml") As DocumentStream.File
        Dim CompiledData = CompileCAIBIASCalculationThread_p(genes, WorkTemp, InternalID:=IO.Path.GetFileNameWithoutExtension(genes.FilePath))
        Return __compileCAI(CompiledData)
    End Function

    Private Function __compileCAI(data As Generic.IEnumerable(Of KeyValuePair(Of String, CAITable))) As DocumentStream.File
        Dim CSV As DocumentStream.File = New DocumentStream.File
        Dim Head = New DocumentStream.RowObject From {"SpeciesID", "CAI"}

        Call CSV.Add(Head)

        For Each item In data.First.Value.BiasList
            Call Head.Add(item.Value.Key.ToString)
        Next

        For Each item In data
            Dim row As New DocumentStream.RowObject From {item.Key, item.Value.CAI}

            For i As Integer = 0 To item.Value.BiasList.Count - 1
                Call row.Add(item.Value.BiasList(i).Value.Value)
            Next

            Call CSV.Add(row)
        Next

        Return CSV
    End Function

    ''' <summary>
    ''' 并行版本的计算函数
    ''' </summary>
    ''' <param name="genome"></param>
    ''' <param name="windowsSize">默认为1kb的长度</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    <ExportAPI("genome.sigma_diff")>
    Public Function GenomeSigmaDifference_p(genome As FastaToken, compare As FastaToken, Optional windowsSize As Integer = 1000) As SiteSigma()
        Call Console.WriteLine("Start to create slide window for difference calculation.")
        Dim Windows = New NucleotideModels.NucleicAcid(genome).ToArray.CreateSlideWindows(windowsSize)
        Call Console.WriteLine("Creation job done!")
        Call Console.WriteLine("Start to create the sigma data collection...")

        Using pb As New CBusyIndicator(_start:=True)
            Dim LQuery = (From segment In Windows.AsParallel
                          Let x = New NucleotideModels.NucleicAcid(segment.Elements)
                          Let y = New NucleotideModels.NucleicAcid(compare)
                          Let Sigma = DifferenceMeasurement.Sigma(x, y)
                          Let p = New SiteSigma With {
                              .Site = __echo(segment, Windows.Count),
                              .Sigma = Sigma,
                              .Similarity = DifferenceMeasurement.SimilarDescription(Sigma)
                          }
                          Select p
                          Order By p.Site Ascending).ToArray
            Call Console.WriteLine("[JOB DONE!] Generating output document...")
            Return LQuery
        End Using
    End Function

    Private Function __echo(segment As SlideWindowHandle(Of DNA), numWins As Integer) As Integer
        Call $"{100 * segment.p / numWins}%".__DEBUG_ECHO
        Return segment.p
    End Function

    ''' <summary>
    ''' 应用于<see cref="BatchCalculation"></see>函数的非并行版本，请在上一层调用之中使用并行化
    ''' </summary>
    ''' <param name="cache">计算缓存</param>
    ''' <param name="compare"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function __genomeSigmaDiff(cache As Cache(), compare As FastaToken) As SiteSigma()
        Call "Creating compare cache..... ".__DEBUG_ECHO
        Dim CompareCache = New NucleicAcid(compare)
        Call "Compare cache creating job done!".__DEBUG_ECHO
        Dim LQuery = (From segment As Cache In cache
                      Let Sigma = DifferenceMeasurement.Sigma(segment.Cache, CompareCache)
                      Select New SiteSigma With {
                          .Site = segment.SlideWindow.p,
                          .Sigma = Sigma,
                          .Similarity = DifferenceMeasurement.SimilarDescription(Sigma)}).ToArray
        Return LQuery
    End Function

    Private Structure Cache
        Dim SlideWindow As SlideWindowHandle(Of DNA)
        Dim Cache As NucleicAcid
    End Structure

    ''' <summary>
    ''' 要求所有的文件都必须要为同一个基因组比对不同的基因组，不可以改动输出文件的文件名
    ''' </summary>
    ''' <param name="source"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <ExportAPI("rendering_merge.delta_source")>
    Public Function MergeDelta(source As String, query As IEnumerable(Of IGeneBrief), render_source As String, saveto As String, Optional samples As Integer = 1) As Boolean
        Dim LoadData = (From path As String
                        In FileIO.FileSystem.GetFiles(source, FileIO.SearchOption.SearchTopLevelOnly, "*.csv").AsParallel
                        Select New KeyValuePair(Of String, SiteSigma())(IO.Path.GetFileNameWithoutExtension(path).Split(CChar("_")).Last,
                             value:=path.LoadCsv(Of SiteSigma)(False).ToArray)).ToArray

        Return __mergeDelta(LoadData, query, render_source, saveto, samples)
    End Function

    <Extension> Private Function __samples(Of T)(data As T(), sample As Integer) As T()
        Dim list As New List(Of T)
        For i As Integer = 0 To data.Count - 1 Step sample
            Call list.Add(data(i))
        Next

        Return list.ToArray
    End Function

    Private Function __mergeDelta(LoadData As KeyValuePair(Of String, SiteSigma())(),
                                  Query As IEnumerable(Of IGeneBrief),
                                  render_source As String,
                                  saveto As String,
                                  Samples As Integer) As Boolean
        If Samples <= 0 Then
            Samples = 1
        End If

        LoadData = (From item In LoadData Select New KeyValuePair(Of String, SiteSigma())(item.Key, value:=item.Value.__samples(Samples))).ToArray

        Dim sitesData = (From site In LoadData.First.Value.AsParallel
                         Let lstName As String() = (From item As IGeneBrief
                                                    In Query.GetObjects(site.Site, direction:=Strands.Unknown)
                                                    Select item.Identifier).ToArray
                         Select New KeyValuePair(Of Integer, String())(site.Site, lstName)).ToArray
        '加载基因组双向BLAST同源片段染色数据
        Dim LoadCRendering = render_source.LoadXml(Of BestHit)() ' (From path As String In FileIO.FileSystem.GetFiles(render_source, FileIO.SearchOption.SearchTopLevelOnly, "*.xml").AsParallel
        '                      Select id = IO.Path.GetFileNameWithoutExtension(path),
        '                      data = path.LoadXml(Of SMRUCC.genomics.AnalysisTools.DataVisualization.VennDiagram.ShellScriptAPI.BestHit)()).ToArray
        '基因按照正向进行标识 ，当比对上去的时候，会进行delta染色，即基因号为相应的比对上的基因号，当没有比对上去的时候，基因号为空
        '   Dim LQuery = (From item In LoadData Let ptt = LoadPTT(item.Key) Let render = LoadCRendering(item.Key) Select ID = item.Key, renderData = InternalColorRender(item.Key, item.Value.ToArray, ptt, render.data, sitesData)).ToArray
        '合并数据，得到染色矩阵，并写入文件

        Dim CsvData As New DocumentStream.File
        Dim Head As New DocumentStream.RowObject From {"Site", "QUERY_ID"}
        For Each item In LoadData
            Call Head.Add("")
            Call Head.Add(item.Key)
            Call Head.Add("Similarity")
            Call Head.Add("GeneID")
        Next
        Call CsvData.Add(Head)

        For i As Integer = 0 To LoadData.First.Value.Count - 1 '都是使用同一个基因组进行比对，所以长度都是一样的
            Dim row As New DocumentStream.RowObject From {sitesData(i).Key}
            Call row.Add(String.Join("; ", sitesData(i).Value))

            For Each item In LoadData
                Dim rData = item.Value(i)
                Dim cols = (From id As String In sitesData(i).Value Select LoadCRendering(QueryName:=id, HitSpecies:=item.Key)).ToArray

                Call row.Add("")
                Call row.Add(rData.Sigma)
                Call row.Add(rData.Similarity.ToString)

                Call row.Add(String.Join("; ", cols))
            Next
            Call CsvData.Add(row)
        Next

        Return CsvData.Save(saveto, False)
    End Function

    ''' <summary>
    ''' 获取delta位点染色数据
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function __colorRender(UID As String,
                                   delta As SiteSigma(),
                                   PTT As PTT,
                                   render As BestHit,
                                   querySites As KeyValuePair(Of Integer, String())()) As SegmentRenderData()
        Dim LQuery = (From site In delta Let querySite = querySites(site.Site)
                      Select New SegmentRenderData With {
                          .Site = site.Site,
                          .Similarity = site.Similarity,
                          .Sigma = site.Sigma,
                          .QueryId = querySite.Value,
                          .SubjectId = (From id As String In querySite.Value Select render(QueryName:=id, HitSpecies:=UID))}).ToArray
        Return LQuery
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="query">Query基因组的fasta序列的文件路径</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <param name="sbjDIR">将要进行比较的基因组的fasta序列文件的存放文件夹</param>
    ''' 
    <ExportAPI("sigma_diff.query")>
    Public Function SigmaCompareWith(query As String, sbjDIR As String, EXPORT As String, Optional windowsSize As Integer = 1000) As Boolean
        Call ("Start to load subject fasta data from " & sbjDIR).__DEBUG_ECHO

        Using pb = New CBusyIndicator(_start:=True)
            Return __sigmaCompareWith(query, sbjDIR, EXPORT, windowsSize)
        End Using
    End Function

    Private Function __sigmaCompareWith(query As String, subject As String, EXPORT As String, windowsSize As Integer) As Boolean
        Dim FastaObjects = (From path As String
                            In FileIO.FileSystem.GetFiles(subject, FileIO.SearchOption.SearchTopLevelOnly, "*.fasta", "*.fsa").AsParallel
                            Select FastaToken.LoadNucleotideData(path)).ToArray

        Call $"Fasta data load done!, start to calculates the sigma differences in window_size {windowsSize / 1000}KB....".__DEBUG_ECHO
        Dim QueryFasta = FastaToken.LoadNucleotideData(query)
        Dim Windows = New NucleotideModels.NucleicAcid(QueryFasta).ToArray.CreateSlideWindows(windowsSize)
        Dim InternalCache = (From Window In Windows.AsParallel
                             Let cacheData = New NucleicAcid(Window.Elements)
                             Select New Cache With {
                                 .Cache = cacheData,
                                 .SlideWindow = Window}).ToArray 'Internal create cache data.
        Call Console.WriteLine("[INFO] query for the Sigma difference calculation in length of {0}KB...", QueryFasta.Length / 1000)

        Dim LQuery = (From SubjectFasta In FastaObjects.AsParallel Select __process(SubjectFasta, QueryFasta, EXPORT, InternalCache)).ToArray
        Dim FileName As String = String.Format("{0}/Compiled/{1}.csv", EXPORT, IO.Path.GetFileNameWithoutExtension(query))
        Dim File = __compile(LQuery)

        Call Console.WriteLine("[JOB DONE]")

        Return File.Save(FileName, False)
    End Function

    Private Function __process(subjectFasta As FastaToken, queryFasta As FastaToken, Export As String, internalCache As Cache()) As KeyValuePair(Of String, SiteSigma())
        Call Console.WriteLine("[DEBUG] Start the calculation threads ""{0}""... ", subjectFasta.Title)
        Dim sigma = __genomeSigmaDiff(internalCache, subjectFasta)
        Dim f As String = queryFasta.Title.Split(CChar("|")).First.NormalizePathString
        Dim g As String = subjectFasta.Title.Split(CChar("|")).First.NormalizePathString
        Dim path As String = String.Format("{0}/{1}-{2}.csv", Export, f, g)
        Call Console.WriteLine("[DEBUG] Calculation job done, trying to export data to filesystem " & path)
        Call sigma.SaveTo(path, False)
        Return New KeyValuePair(Of String, SiteSigma())(g, sigma)
    End Function

    Private Function __compile(LQuery As KeyValuePair(Of String, SiteSigma())()) As DocumentStream.File
        Dim File As DocumentStream.File = New DocumentStream.File
        Dim Head As New DocumentStream.RowObject     '为了保持一一对应关系，这里不能够再使用并行化

        Call Console.WriteLine("Compiling data....")
        Call Head.Add("Site")

        For Each Entry In LQuery
            Call Head.Add("")
            Call Head.Add("Sigma")
            Call Head.Add(Entry.Key & "->Similarity")
        Next

        Call File.Add(Head)

        For i As Integer = 0 To LQuery.First.Value.Count - 1
            Dim Row As New DocumentStream.RowObject
            Call Row.Add(i)

            For Each item In LQuery
                Dim Line = item.Value(i)
                Call Row.Add("")
                Call Row.Add(Line.Sigma)
                Call Row.Add(Line.Similarity)
            Next

            Call File.Add(Row)
        Next

        Return File
    End Function

    <ExportAPI("compile.delta_query")>
    Public Function Compile(source As String, saveCsv As String) As Boolean
        Dim Entry = gbExportService.LoadGbkSource(source)
        Dim LQuery = (From item In Entry.AsParallel Select New KeyValuePair(Of String, SiteSigma())(item.Key, item.Value.LoadCsv(Of SiteSigma)(False).ToArray)).ToArray
        Dim File = __compile(LQuery)
        Return File.Save(saveCsv, False)
    End Function

    ''' <summary>
    ''' 对<paramref name="source"></paramref>文件夹之中的所有序列对象进行两两比对，适用于小规模的计算
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="EXPORT"></param>
    ''' <param name="windowsSize"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    <ExportAPI("sigma_diff.batch", Info:="This function is suite for the large scales data calculation.")>
    Public Function BatchCalculation2(source As String, EXPORT As String, Optional windowsSize As Integer = 1000) As Boolean
        Call Console.WriteLine("[DEBUG] start to load fasta data from " & source)

        Using pb As New CBusyIndicator(_start:=True)
            Dim FastaObjects = (From path As String
                                In FileIO.FileSystem.GetFiles(source, FileIO.SearchOption.SearchTopLevelOnly, "*.fasta", "*.fsa").AsParallel
                                Select FastaToken.Load(path)).ToArray

            Call Console.WriteLine("[DEBUG] fasta data load done!, start to calculates the sigma differences in window_size {0}KB....", windowsSize / 1000)

            Dim MAT = Comb(Of FastaToken).CreateCompleteObjectPairs(FastaObjects)
            Dim ChunkBuffer = (From pairedList In MAT
                               Select pairedList.__calculates(windowsSize, EXPORT)).MatrixToList

            Call Console.WriteLine("All data calculation job done!, grouping data!")
            Dim grouped = (From item In ChunkBuffer Select item Group By item.Key Into Group).ToArray
            Call Console.WriteLine("Compiling data....")
            Dim ExportLQuery = (From item In grouped.AsParallel Select __compileSigma(item.Group.ToArray, EXPORT)).ToArray
            Call Console.WriteLine("[JOB DONE]")
        End Using

        Return True
    End Function

    <Extension> Private Function __calculates(pairedList As KeyValuePair(Of FastaToken, FastaToken)(),
                                              windowsSize As Integer,
                                              EXPORT As String) As KeyValuePair(Of String, String)()
        Dim InternalList As New List(Of KeyValuePair(Of String, String))

        For Each paired In pairedList
            Dim sigma = GenomeSigmaDifference_p(paired.Key, paired.Value, windowsSize)
            Call Console.WriteLine("[DEBUG] Calculation job done, trying to export data to filesystem " & EXPORT)
            Dim f = paired.Key.Title, g = paired.Value.Title
            Dim dat = New KeyValuePair(Of String, String)(f.Split(CChar("|")).First, g.Split(CChar("|")).First)
            Call sigma.SaveTo(String.Format("{0}/{1}-{2}.csv", EXPORT, dat.Key, dat.Value), False)
            Call InternalList.Add(dat)
        Next

        Return InternalList.ToArray
    End Function

    ''' <summary>
    ''' 对<paramref name="source"></paramref>文件夹之中的所有序列对象进行两两比对，适用于小规模的计算
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="EXPORT"></param>
    ''' <param name="windowsSize"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    <ExportAPI("sigma_diff.batch_small", Info:="This function is suite for the small scales data calculation.")>
    Public Function BatchCalculation(source As String, EXPORT As String, Optional windowsSize As Integer = 1000) As Boolean

        Call Console.WriteLine("[DEBUG] start to load fasta data from " & source)
        Dim pb As New CBusyIndicator(_start:=True)
        Dim FastaObjects = (From path As String In FileIO.FileSystem.GetFiles(source, FileIO.SearchOption.SearchTopLevelOnly, "*.fasta", "*.fsa").AsParallel Select SMRUCC.genomics.SequenceModel.FASTA.FastaToken.Load(path)).ToArray

        Call $"Fasta data load done!, start to calculates the sigma differences in window_size {windowsSize / 1000}KB....".__DEBUG_ECHO

        Dim MAT = Comb(Of FastaToken).CreateCompleteObjectPairs(FastaObjects)
        Dim ChunkBuffer = (From pairedList In MAT.AsParallel
                           Select __calculate(windowsSize, EXPORT, pairedList)).MatrixToList

        Call Console.WriteLine("All data calculation job done!, grouping data!")
        Dim grouped = (From item In ChunkBuffer Select item Group By item.Key Into Group).ToArray
        Call Console.WriteLine("Compiling data....")
        Dim ExportLQuery = (From item In grouped.AsParallel Select __compileSigma(item.Group.ToArray, EXPORT)).ToArray
        Call Console.WriteLine("[JOB DONE]")
        Call pb.Dispose()

        Return True
    End Function

    Private Function __calculate(windowsSize As Integer, EXPORT As String, paireds As KeyValuePair(Of FastaToken, FastaToken)()) As KeyValuePair(Of String, String)()
        Dim InternalList As New List(Of KeyValuePair(Of String, String))

        For Each paired In paireds
            Dim sigma = GenomeSigmaDifference_p(paired.Key, paired.Value, windowsSize)
            Call Console.WriteLine("[DEBUG] Calculation job done, trying to export data to filesystem " & EXPORT)
            Dim f = paired.Key.Title, g = paired.Value.Title
            Dim dat = New KeyValuePair(Of String, String)(f.Split(CChar("|")).First, g.Split(CChar("|")).First)
            Call sigma.SaveTo(String.Format("{0}/{1}-{2}.csv", EXPORT, dat.Key, dat.Value), False)
            Call InternalList.Add(dat)
        Next

        Return InternalList.ToArray
    End Function

    <ExportAPI("read.csv.site_delta")>
    Public Function SiteDataLoad(path As String) As SiteSigma()
        Return path.LoadCsv(Of SiteSigma)(False).ToArray
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="dat">
    ''' 每一个文件之中的行数都是一样的，因为都是以同一个菌株作为计算的参照
    ''' </param>
    ''' <param name="export"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function __compileSigma(dat As KeyValuePair(Of String, String)(), export As String) As Boolean
        Dim FileName As String = String.Format("{0}/Compiled/{1}.csv", export, dat.First.Key)
        Dim File As DocumentStream.File = New DocumentStream.File
        Dim Data = (From path In dat Select String.Format("{0}/{1}-{2}.csv", export, path.Key, path.Value).LoadCsv(Of SiteSigma)(False)).ToArray ' 为了保持一一对应关系，这里不能够再使用并行化
        Dim Head As New DocumentStream.RowObject

        Call Head.Add("Site")

        For Each Entry In dat
            Call Head.Add("")
            Call Head.Add("Sigma")
            Call Head.Add(Entry.Value & "->Similarity")
        Next

        Call File.Add(Head)

        For i As Integer = 0 To Data.First.Count - 1
            Dim Row As New DocumentStream.RowObject
            Call Row.Add(i)

            For Each item In Data
                Dim Line = item(i)
                Call Row.Add("")
                Call Row.Add(Line.Sigma)
                Call Row.Add(Line.Similarity)
            Next

            Call File.Add(Row)
        Next

        Return File.Save(FileName, False)
    End Function
End Module
