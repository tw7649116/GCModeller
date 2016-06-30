﻿Imports LANS.SystemsBiology.DatabaseServices.ComparativeGenomics.AnnotationTools.Reports
Imports LANS.SystemsBiology.NCBI.Extensions.LocalBLAST.Application.BBH

''' <summary>
''' 注释的流程为：
''' 以此比对数据库，通过BBH得到直系同源
''' </summary>
''' <remarks>
''' 对于BBH获取直系同源的基因的过程一般是这样的：
''' 当所比对的注释源较小的时候，可以直接使用BBH进行比对，之后再根据数据库之中的物种数据分组获取BBH结果即可，故而，对于较小的fasta数据库，注释工具的初始化参数new.db为fasta文件的路径
''' 当所比对的注释源很大的时候，进行BBH会非常慢，故而这个时候需要将数据库分割为不同的模块进行BBH操作，当BBH操作结束的时候，在合并为一个数据源按照数据库之中的物种信息进行分组获取BBH数据
''' </remarks>
Public MustInherit Class AnnotationTool

    Protected BBH_Handle As LANS.SystemsBiology.NCBI.Extensions.LocalBLAST.Application.BBH.BidirectionalBesthit_BLAST
    Protected FastaPaths As Dictionary(Of String, String)

    Sub New(BLASTHandle As LANS.SystemsBiology.NCBI.Extensions.LocalBLAST.InteropService.InteropService, DB As String)
        BBH_Handle = New LANS.SystemsBiology.NCBI.Extensions.LocalBLAST.Application.BBH.BidirectionalBesthit_BLAST(BLASTHandle, Settings.DataCache)
        FastaPaths = (From path As String
                      In FileIO.FileSystem.GetFiles(DB, FileIO.SearchOption.SearchAllSubDirectories, "*.fasta", "*.fsa")
                      Select KEY = IO.Path.GetFileNameWithoutExtension(path), path).ToArray.ToDictionary(Function(item) item.KEY, elementSelector:=Function(item) item.path)
    End Sub

    ''' <summary>
    ''' 获取所比对的蛋白质的信息的物种来源的信息
    ''' </summary>  
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected MustOverride Function GetAnnotationSourceMeta() As LANS.SystemsBiology.GCModeller.Workbench.DatabaseServices.Model_Repository.DbFileSystemObject.ProteinDescriptionModel()

    Protected Function InternalGetAnnotationSourceMeta() As MetaSource()
        Return CreateAnnotationSourceMeta(Me.GetAnnotationSourceMeta)
    End Function

    Public Shared Function CreateAnnotationSourceMeta(data As Generic.IEnumerable(Of LANS.SystemsBiology.GCModeller.Workbench.DatabaseServices.Model_Repository.DbFileSystemObject.ProteinDescriptionModel)) As MetaSource()
        Dim LQuery = (From item In data Select item Group By item.OrganismSpecies Into Group).ToArray
        Dim ChunkBuffer = (From item In LQuery Select New MetaSource With {.OrganismSpecies = item.OrganismSpecies, .AnnotationSource = item.Group.ToArray}).ToArray
        Return ChunkBuffer
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Fasta"></param>
    ''' <param name="Export"></param> 
    ''' <param name="Parallel">并行模型</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Function InvokeAnnotation(Fasta As String, Export As String, Optional Parallel As Boolean = True, Optional evalue As String = "1e-5") As GenomeAnnotations
        Dim Paralogs = BBH_Handle.Paralogs(Fasta, Nothing)

        If Parallel Then
            Return InvokeAnnotation_p(Fasta, Export, Paralogs)
        End If

        Dim Orthologs = New Dictionary(Of String, LANS.SystemsBiology.NCBI.Extensions.LocalBLAST.Application.BBH.BiDirectionalBesthit())

        For Each sp In FastaPaths
            Call Orthologs.Add(sp.Key, BBH_Handle.Peformance(Query:=Fasta, Subject:=sp.Value, HitsGrepMethod:=Nothing, QueryGrepMethod:=Nothing))
            Console.WriteLine("[DONE]   " & sp.Value.ToFileURL)
        Next

        Return GenomeAnnotations.CompileResult(Orthologs, Paralogs, Fasta, InternalGetAnnotationSourceMeta)
    End Function

    Public Function InvokeAnnotation_p(Fasta As String, Export As String, Paralogs As BestHit(), Optional evalue As String = "1e-5") As GenomeAnnotations
        Dim Orthologs = New Dictionary(Of String, BiDirectionalBesthit())
        Dim LQuery = (From sp As KeyValuePair(Of String, String) In FastaPaths.AsParallel
                      Let InternalBBHOperation = Function() As BiDirectionalBesthit()
                                                     Dim Reuslt = BBH_Handle.Peformance(Query:=Fasta, Subject:=sp.Value, HitsGrepMethod:=Nothing, QueryGrepMethod:=Nothing)
                                                     Console.WriteLine("[DONE]   " & sp.Value.ToFileURL)
                                                     Return Reuslt
                                                 End Function Select Species = sp.Key, BBH = InternalBBHOperation()).ToArray
        Return GenomeAnnotations.CompileResult(Orthologs, Paralogs, Fasta, InternalGetAnnotationSourceMeta)
    End Function

    ''' <summary>
    ''' 注释数据的物种来源信息，即按照物种分组之后得到的信息
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MetaSource
        Public Property OrganismSpecies As String
        Public Property AnnotationSource As LANS.SystemsBiology.GCModeller.Workbench.DatabaseServices.Model_Repository.DbFileSystemObject.ProteinDescriptionModel()
    End Class
End Class