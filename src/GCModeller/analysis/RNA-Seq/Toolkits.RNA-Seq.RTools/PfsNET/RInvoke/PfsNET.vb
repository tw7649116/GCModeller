﻿Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.DocumentFormat.Csv.Extensions
Imports Microsoft.VisualBasic.DocumentFormat.Csv.StorageProvider.Reflection
Imports System.Text
Imports RDotNET
Imports Microsoft.VisualBasic.Scripting.MetaData
Imports RDotNET.Extensions.VisualBasic.RSystem
Imports RDotNET.Extensions.VisualBasic
Imports LANS.SystemsBiology.AnalysisTools.CellularNetwork.PFSNet
Imports RDotNET.Extensions

#Const DEBUG = True

Namespace PfsNET

    ''' <summary>
    ''' PFSNet computes signifiance of subnetworks generated through a process that selects genes in a pathway based on fuzzy scoring and a majority voting procedure.
    ''' (一个程序仅一个实例，是否是因为将Module修改为Class的原因所以导致了在64位服务器上面的Java初始化失败？？？？)
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    <PackageNamespace("PfsNET.R.Invoke", Description:="PFSNet computes signifiance of subnetworks generated through a process that selects genes in a pathway based on fuzzy scoring and a majority voting procedure.",
                        Cites:="Lim, K. and L. Wong (2014). ""Finding consistent disease subnetworks Using PFSNet."" Bioinformatics 30(2): 189-196.
<p> MOTIVATION: Microarray data analysis is often applied to characterize disease populations by identifying individual genes linked to the disease. In recent years, efforts have shifted to focus on sets of genes known to perform related biological functions (i.e. in the same pathways). Evaluating gene sets reduces the need to correct for false positives in multiple hypothesis testing. However, pathways are often large, and genes in the same pathway that do not contribute to the disease can cause a method to miss the pathway. In addition, large pathways may not give much insight to the cause of the disease. Moreover, when such a method is applied independently to two datasets of the same disease phenotypes, the two resulting lists of significant pathways often have low agreement. RESULTS: We present a powerful method, PFSNet, that identifies smaller parts of pathways (which we call subnetworks), and show that significant subnetworks (and the genes therein) discovered by PFSNet are up to 51% (64%) more consistent across independent datasets of the same disease phenotypes, even for datasets based on different platforms, than previously published methods. We further show that those methods which initially declared some large pathways to be insignificant would declare subnetworks detected by PFSNet in those large pathways to be significant, if they were given those subnetworks as input instead of the entire large pathways. AVAILABILITY: http://compbio.ddns.comp.nus.edu.sg:8080/pfsnet/

",
                        Publisher:="Kevin Lim and Limsoon Wong",
                        Url:="http://compbio.ddns.comp.nus.edu.sg:8080/pfsnet/")>
    Public Module PfsNETRInvoke

        ''' <summary>
        ''' 假若<paramref name="java_class"></paramref>参数为空，则初始化为非rJava的调用版本
        ''' </summary>
        ''' <param name="java_class"></param>
        ''' <param name="R_HOME"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' 
        <ExportAPI("Init()")>
        Public Function InitializeSession(<Parameter("Filter.java")> Optional java_class As String = "", Optional R_HOME As String = "") As Boolean
            If Not String.IsNullOrEmpty(R_HOME) Then
                Call TryInit(R_HOME)
            End If

            Call Library("igraph")
            Call New RScriptInvoke(Encoding.ASCII.GetString(My.Resources.onLoad)).PrintSTDOUT()

            If Not String.IsNullOrEmpty(java_class) Then
                Call Console.WriteLine("Try to start the Java VMs...")

                Dim JavaClassPath As String

                If FileIO.FileSystem.FileExists(java_class) Then
                    JavaClassPath = FileIO.FileSystem.GetFileInfo(java_class).FullName
                    Call Console.WriteLine("Java class file path found at location ""{0}""!", JavaClassPath)
                Else
                    Throw New Exception("Required java class file is not found!")
                End If

                Call Console.WriteLine("[DEBUG] call library('rJava')")
                Call New RScriptInvoke("library(rJava)").PrintSTDOUT()
                Call Console.WriteLine("[DEBUG] call .jinit()")
                Call New RScriptInvoke(".jinit()").PrintSTDOUT()
                Call Console.WriteLine("[DEBUG] call .jaddClassPath() for the user specific Java class file...")
                Call New RScriptInvoke(String.Format(".jaddClassPath(""{0}"")", JavaClassPath.Replace("\", "/"))).PrintSTDOUT()
                Call Console.WriteLine("[DEBUG] R create pfsnet function delegate.... ")
                Call New RScriptInvoke(Encoding.ASCII.GetString(My.Resources.pfsnet)).PrintSTDOUT()
            Else '调用非Java版本
                Call Console.WriteLine("[DEBUG] Java filter class is not assigned, using the non-java edition!")
                Call New RScriptInvoke(Encoding.ASCII.GetString(My.Resources.pfsnet_not_rJava)).PrintSTDOUT()
            End If

            Return True
        End Function

        <ExportAPI("Analysis.Invoke")>
        Public Function RInvoke(file1 As String, file2 As String, file3 As String,
                                Optional b As String = "0.5",
                                Optional t1 As String = "0.95",
                                Optional t2 As String = "0.85",
                                Optional n As String = "1000") As RDotNET.SymbolicExpression

            Call Console.WriteLine()

            file1 = FileIO.FileSystem.GetFileInfo(file1).FullName
            file2 = FileIO.FileSystem.GetFileInfo(file2).FullName
            file3 = FileIO.FileSystem.GetFileInfo(file3).FullName

            Dim Script = New PfsNETScript(b, t1, t2, n) With {.File1 = file1, .File2 = file2, .File3 = file3}
            Dim Expr = RServer.Evaluate(Script.RScript)

            Try
                Call Script.RScript.SaveTo("./PfsNET.txt")
            Catch ex As Exception

            End Try

            Return Expr
        End Function

        ''' <summary>
        ''' R脚本版本的PfsNET计算引擎
        ''' </summary>
        ''' <param name="file1"></param>
        ''' <param name="file2"></param>
        ''' <param name="file3"></param>
        ''' <param name="b"></param>
        ''' <param name="t1"></param>
        ''' <param name="t2"></param>
        ''' <param name="n"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' 
        <ExportAPI("Invoke")>
        Public Function Evaluate(file1 As String, file2 As String, file3 As String,
                                 Optional b As String = "0.5",
                                 Optional t1 As String = "0.95",
                                 Optional t2 As String = "0.85",
                                 Optional n As String = "1000") As PFSNetResultOut

            Dim STDOutput As String()

            Call Threading.Thread.Sleep(1000) '为了防止在进行pfsnet的批量计算的过程之中出现文件被当前进程占用的情况出现而导致的计算错误，需要在这里休眠1秒钟，使当前的进程可以完全的释放数据文件的文件句柄

            Call Console.WriteLine()

            file1 = FileIO.FileSystem.GetFileInfo(file1).FullName
            file2 = FileIO.FileSystem.GetFileInfo(file2).FullName
            file3 = FileIO.FileSystem.GetFileInfo(file3).FullName

            Dim Script = New PfsNETScript(b, t1, t2, n) With {.File1 = file1, .File2 = file2, .File3 = file3}

            STDOutput = RServer.WriteLine(Script)
            STDOutput = (From strLine As String In STDOutput Select strLine.Replace(vbCr, "").Replace(vbLf, "")).ToArray

            For Each Line As String In STDOutput
                Call Console.WriteLine(Line)
            Next

            Call Console.WriteLine()

            'Call FileIO.FileSystem.DeleteFile(file1, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
            'Call FileIO.FileSystem.DeleteFile(file2, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
            'Call FileIO.FileSystem.DeleteFile(file3, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
            Try
                Call IO.File.WriteAllLines(My.Application.Info.DirectoryPath & "/pfsnet_debug.log", STDOutput, encoding:=Encoding.ASCII)
            Catch ex As Exception

            End Try

            Dim Parsed = SubnetParser.TryParse(STDOutput)
            Dim Result = SubnetParser.GenerateDefaultStruct(Parsed, "")
            Result.STD_OUTPUT = STDOutput
            Return Result
        End Function
    End Module
End Namespace