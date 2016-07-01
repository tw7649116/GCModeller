﻿Imports System.Data.SQLite.Linq.DataMapping.Interface.Reflector
Imports LANS.SystemsBiology.GCModeller.Workbench.DatabaseServices.Model_Repository
Imports Microsoft.VisualBasic

Namespace RegpreciseRegulations

    ''' <summary>
    ''' Annotation for the genome wide regulation network using regprecise database.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Regprecise : Inherits AnnotationTool

        ''' <summary>
        ''' The meta data of the regprecise regulators.(Regprecise数据库的元数据)
        ''' </summary>
        ''' <remarks></remarks>
        Dim _MetaDataTable As LANS.SystemsBiology.GCModeller.Workbench.DatabaseServices.Model_Repository.DbFileSystemObject.ProteinDescriptionModel()

        Sub New(LocalBlast As LANS.SystemsBiology.NCBI.Extensions.LocalBLAST.InteropService.InteropService)
            Call MyBase.New(LocalBlast, DB:=Settings.Initialize.RepositoryRoot & "/Regprecise/")

            Dim LQuery = RepositoryEngine.SQLiteEngine.Load(Of LANS.SystemsBiology.GCModeller.Workbench.DatabaseServices.Model_Repository.Regprecise)()
            Me._MetaDataTable = (From item In LQuery Select DirectCast(item, LANS.SystemsBiology.GCModeller.Workbench.DatabaseServices.Model_Repository.DbFileSystemObject.ProteinDescriptionModel)).ToArray
        End Sub

        Public Shared Function CreateDatabaseTable(DbFile As String, RepositoryRoot As String) As LANS.SystemsBiology.GCModeller.Workbench.DatabaseServices.Model_Repository.Regprecise()
            Dim Fasta = (From GroupedItem In (From item In LANS.SystemsBiology.DatabaseServices.Regprecise.FastaReaders.Regulator.LoadDocument(FastaFile:=LANS.SystemsBiology.SequenceModel.FASTA.FastaFile.Read(DbFile))
                                              Select item
                                              Group By item.SpeciesCode.ToLower Into Group).ToArray.AsParallel
                         Select SpCode = GroupedItem.ToLower,
                                RegpreciseData = GroupedItem.Group.ToArray,
                                FastaData = CType((From item In GroupedItem.Group
                                                   Select New LANS.SystemsBiology.SequenceModel.FASTA.FastaToken With
                                                          {
                                                              .Attributes = New String() {item.KEGG},
                                                              .SequenceData = item.SequenceData}).ToArray, LANS.SystemsBiology.SequenceModel.FASTA.FastaFile)).ToArray
            Call FileIO.FileSystem.CreateDirectory(RepositoryRoot)

            Dim FastaFile = CType((From item In Fasta Select item.FastaData.ToArray).ToArray.MatrixToVector, LANS.SystemsBiology.SequenceModel.FASTA.FastaFile)
            DbFile = LANS.SystemsBiology.GCModeller.Workbench.DatabaseServices.Model_Repository.Regprecise.DBPath(RepositoryRoot)
            Call FastaFile.Save(DbFile)  '路径都是一样的
            Dim MD5 As String = SecurityString.GetFileHashString(DbFile)

            Dim ChunkBuffer As List(Of LANS.SystemsBiology.GCModeller.Workbench.DatabaseServices.Model_Repository.Regprecise) =
                New List(Of LANS.SystemsBiology.GCModeller.Workbench.DatabaseServices.Model_Repository.Regprecise)

            For Each Entry In Fasta
                Dim LQuery = (From item In Entry.RegpreciseData Select LANS.SystemsBiology.GCModeller.Workbench.DatabaseServices.Model_Repository.Regprecise.CreateObject(item, MD5)).ToArray
                Call ChunkBuffer.AddRange(LQuery)
            Next

            Return ChunkBuffer.ToArray
        End Function

        ''' <summary>
        ''' Install the regprecise database into the GCModeller repository database.
        ''' </summary>
        ''' <param name="DbFile">The regulator protein fasta file path</param>
        ''' <param name="RepositoryRoot">The database directory root of the GCModeller repository</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Install(DbFile As String, RepositoryRoot As String) As LANS.SystemsBiology.GCModeller.Workbench.DatabaseServices.Model_Repository.Regprecise()
            Dim ChunkBuffer = CreateDatabaseTable(DbFile, RepositoryRoot)
            Call WriteDatabase(ChunkBuffer)
            Return ChunkBuffer
        End Function

        ''' <summary>
        ''' Update tje database information of the regprecise repository
        ''' </summary>
        ''' <param name="data">The data will be write into or updates the regprecise repository source.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function WriteDatabase(data As LANS.SystemsBiology.GCModeller.Workbench.DatabaseServices.Model_Repository.Regprecise()) As Boolean
            Dim Engine = RepositoryEngine.SQLiteEngine
            Dim TableSchema = New System.Data.SQLite.Linq.DataMapping.Interface.TableSchema(GetType(LANS.SystemsBiology.GCModeller.Workbench.DatabaseServices.Model_Repository.Regprecise))

            If Engine.ExistsTable(TableSchema.TableName) Then
                Call Engine.DeleteTable(TableSchema.TableName)
            End If
            Call Engine.CreateTableFor(Of LANS.SystemsBiology.GCModeller.Workbench.DatabaseServices.Model_Repository.Regprecise)()

            Dim p As Integer, i As Integer

            For Each Entry As LANS.SystemsBiology.GCModeller.Workbench.DatabaseServices.Model_Repository.Regprecise In data
                Entry.GUID = p.MoveNext()
                Call Engine.Insert(TableSchema, Entry)
                Call Console.Write("-")

                If i = 0.05 * data.Count Then
                    i = 0
                    Call Console.WriteLine(p / data.Count * 100 & "%")
                Else
                    i += 1
                End If
            Next

            Return True
        End Function

        Protected Overrides Function GetAnnotationSourceMeta() As DbFileSystemObject.ProteinDescriptionModel()
            Return _MetaDataTable
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="Fasta"></param>
        ''' <param name="Export">无用的一个参数，只是为了保持对基类的重载所需要</param>
        ''' <param name="Parallel"></param>
        ''' <param name="evalue"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function InvokeAnnotation(Fasta As String, Export As String, Optional Parallel As Boolean = True, Optional evalue As String = "1e-5") As Reports.GenomeAnnotations
            Dim Paralogs = BBH_Handle.Paralogs(Fasta, Nothing)
            Dim Qvs As String = Me.BBH_Handle.WorkDir & "/" & IO.Path.GetFileNameWithoutExtension(Fasta) & "__vs_regprecise.orthologous_bh.txt"
            Dim Svq As String = Me.BBH_Handle.WorkDir & "/regprecise__vs_" & IO.Path.GetFileNameWithoutExtension(Fasta) & ".orthologous_bh.txt"

            Call Me.BBH_Handle.LocalBLASTService.FormatDb(Me.FastaPaths.First.Value, Me.BBH_Handle.LocalBLASTService.MolTypeProtein).Start(WaitForExit:=True)
            Call Me.BBH_Handle.LocalBLASTService.FormatDb(Fasta, Me.BBH_Handle.LocalBLASTService.MolTypeProtein).Start(WaitForExit:=True)

            Dim QvsProcess = Me.BBH_Handle.LocalBLASTService.Blastp(Fasta, Me.FastaPaths.First.Value, Qvs, evalue)
            Dim QvsThread As Microsoft.VisualBasic.CommandLine.IORedirect.ProcessAyHandle = AddressOf QvsProcess.Start
            Dim QvsAyHandle = QvsThread.BeginInvoke(WaitForExit:=True, PushingData:=Nothing, DelegateAsyncState:=Nothing, DelegateCallback:=Nothing, _DISP_DEBUG_INFO:=False)

            Call Me.BBH_Handle.LocalBLASTService.Blastp(Me.FastaPaths.First.Value, Fasta, Svq, evalue).Start(WaitForExit:=True)
            Call QvsThread.EndInvoke(QvsAyHandle)

            Dim Orthologous = Regprecise.Orthologous(Qvs, Svq)
            Dim OrthologousDict = (From item In Orthologous
                                   Select spcode = item.HitName.Split(":"c).First,
                                          bbh = item Group By spcode Into Group).ToArray.ToDictionary(Function(item) item.spcode, Function(item) (From bh In item.Group Select bh.bbh).ToArray)

            Return AnnotationTools.Reports.GenomeAnnotations.CompileResult(OrthologousDict, Paralogs, Fasta, Me.InternalGetAnnotationSourceMeta)
        End Function

        ''' <summary>
        ''' Invoke the regprecise annotation from the overview cache data.
        ''' </summary>
        ''' <param name="Orthologous"></param>
        ''' <param name="Paralogs"></param>
        ''' <param name="ProteinsFasta"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overloads Function InvokeAnnotation(Orthologous As LANS.SystemsBiology.NCBI.Extensions.LocalBLAST.Application.BBH.BiDirectionalBesthit(),
                                                   Paralogs As LANS.SystemsBiology.NCBI.Extensions.LocalBLAST.Application.BBH.BestHit(), ProteinsFasta As String) _
            As Reports.GenomeAnnotations

            Dim OrthologousDict = (From item In Orthologous
                                   Select spcode = item.HitName.Split(":"c).First,
                                          bbh = item
                                   Group By spcode Into Group).ToArray.ToDictionary(Function(item) item.spcode, Function(item) (From bh In item.Group Select bh.bbh).ToArray)
            Return AnnotationTools.Reports.GenomeAnnotations.CompileResult(OrthologousDict, Paralogs, ProteinsFasta, InternalGetAnnotationSourceMeta)
        End Function

        ''' <summary>
        ''' 从blastp日志数据之中导出regprecise数据库的注释结果
        ''' </summary>
        ''' <param name="qvsPath"></param>
        ''' <param name="svqPath"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Orthologous(qvsPath As String, svqPath As String) As LANS.SystemsBiology.NCBI.Extensions.LocalBLAST.Application.BBH.BiDirectionalBesthit()
            Dim QueryLoading As Func(Of String, LANS.SystemsBiology.NCBI.Extensions.LocalBLAST.BLASTOutput.BlastPlus.v228) =
                AddressOf LANS.SystemsBiology.NCBI.Extensions.LocalBLAST.BLASTOutput.BlastPlus.Parser.TryParse
            Dim AyLoad = QueryLoading.BeginInvoke(qvsPath, Nothing, Nothing)
            Dim QvSBlastOutput As LANS.SystemsBiology.NCBI.Extensions.LocalBLAST.BLASTOutput.BlastPlus.v228,
                SvQBlastOutput As LANS.SystemsBiology.NCBI.Extensions.LocalBLAST.BLASTOutput.BlastPlus.v228 =
                LANS.SystemsBiology.NCBI.Extensions.LocalBLAST.BLASTOutput.BlastPlus.Parser.TryParse(svqPath)
            QvSBlastOutput = QueryLoading.EndInvoke(AyLoad)

            Dim Script = Microsoft.VisualBasic.Text.TextGrepScriptEngine.Compile("tokens ' ' first")
            Dim RegpreciseScript = Microsoft.VisualBasic.Text.TextGrepScriptEngine.Compile("tokens ' ' first;tokens '|' last")

            Call QvSBlastOutput.Grep(Script.Method, RegpreciseScript.Method)
            Call SvQBlastOutput.Grep(RegpreciseScript.Method, Script.Method)

            '导出所有的最佳比对
            Dim QvsBesthit = QvSBlastOutput.ExportAllBestHist
            Dim SvQBesthit = SvQBlastOutput.ExportBestHit

            Dim LQuery = (From bbbh In (From item In SvQBesthit Select spcode = item.QueryName.Split(":"c).First, bh = item Group By spcode Into Group).ToArray Select bbbh.spcode, bh = (From item In bbbh.Group Select item.bh).ToArray).ToArray.ToDictionary(Function(item) item.spcode, Function(item) item.bh)  '对subject进行按照物种分组
            Dim QvsBHData = (From bhhh In (From item In QvsBesthit Select bh = item, spcode = item.HitName.Split(CChar(":")).First Group By spcode Into Group).ToArray Select spcode = bhhh.spcode, bh = (From item In bhhh.Group Select item.bh).ToArray).ToArray

            '分别构建出bbh
            Dim BBH As NCBI.Extensions.LocalBLAST.Application.BBH.BiDirectionalBesthit() = (From species In QvsBHData.AsParallel
                                                                                            Let bbhData = InternalCreateBBH(species.spcode, Qvs:=species.bh, SvqDict:=LQuery)
                                                                                            Where Not bbhData.IsNullOrEmpty
                                                                                            Select bbhData).ToArray.MatrixToVector
            BBH = (From item In BBH Where item.Matched Select item).ToArray
            Return BBH
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="sp_code">The KEGG brief species code.</param>
        ''' <param name="Qvs"></param>
        ''' <param name="SvqDict"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared Function InternalCreateBBH(sp_code As String,
                                                 Qvs As LANS.SystemsBiology.NCBI.Extensions.LocalBLAST.Application.BBH.BestHit(),
                                                 SvqDict As Dictionary(Of String, LANS.SystemsBiology.NCBI.Extensions.LocalBLAST.Application.BBH.BestHit())) _
            As LANS.SystemsBiology.NCBI.Extensions.LocalBLAST.Application.BBH.BiDirectionalBesthit()

            If Not SvqDict.ContainsKey(sp_code) Then
                Call Console.WriteLine("[DEBUG] Could not found the species bbh data for ""{0}""", sp_code)
                Return Nothing
            End If

            Dim SvQBesthit = SvqDict(sp_code)
            Dim ChunkBuffer = LANS.SystemsBiology.NCBI.Extensions.LocalBLAST.Application.BBH.BBHParser.GetBBHTop(qvs:=Qvs, svq:=SvQBesthit)

            Return ChunkBuffer
        End Function
    End Class
End Namespace