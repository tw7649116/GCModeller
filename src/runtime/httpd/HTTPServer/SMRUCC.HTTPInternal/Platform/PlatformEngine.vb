﻿#Region "Microsoft.VisualBasic::dbda62ed14726f6506b0a9e6cb6886d0, ..\httpd\HTTPServer\SMRUCC.HTTPInternal\Platform\PlatformEngine.vb"

' Author:
' 
'       asuka (amethyst.asuka@gcmodeller.org)
'       xieguigang (xie.guigang@live.com)
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

Imports System.IO
Imports System.Net.Sockets
Imports System.Reflection
Imports System.Text
Imports Microsoft.VisualBasic.Language
Imports SMRUCC.WebCloud.HTTPInternal.AppEngine
Imports SMRUCC.WebCloud.HTTPInternal.AppEngine.APIMethods.Arguments
Imports SMRUCC.WebCloud.HTTPInternal.AppEngine.POSTParser
Imports SMRUCC.WebCloud.HTTPInternal.Core
Imports SMRUCC.WebCloud.HTTPInternal.Platform.Plugins

Namespace Platform

    ''' <summary>
    ''' 服务基础类，REST API的开发需要引用当前的项目
    ''' </summary>
    Public Class PlatformEngine : Inherits HttpFileSystem

        Public ReadOnly Property AppManager As AppEngine.APPManager
        Public ReadOnly Property TaskPool As New TaskPool
        Public ReadOnly Property EnginePlugins As Plugins.PluginBase()

        ''' <summary>
        ''' Init engine.
        ''' </summary>
        ''' <param name="port"></param>
        ''' <param name="root">html wwwroot</param>
        ''' <param name="nullExists"></param>
        ''' <param name="appDll">Must have a Class object implements the type <see cref="WebApp"/></param>
        Sub New(root As String,
                Optional port As Integer = 80,
                Optional nullExists As Boolean = False,
                Optional appDll As String = "",
                Optional threads As Integer = -1,
                Optional cache As Boolean = False)

            Call MyBase.New(port, root, nullExists, threads:=threads, cache:=cache)
            Call __init(appDll)
        End Sub

        ''' <summary>
        ''' Scanning the dll file and then load the <see cref="WebApp"/> content.
        ''' </summary>
        ''' <param name="dll"></param>
        Private Sub __init(dll As String)
            _AppManager = New AppEngine.APPManager(Me)

            If dll.FileExists Then
                dll = FileIO.FileSystem.GetFileInfo(dll).FullName

                Call AppEngine.ExternalCall.ParseDll(dll, Me)
                Call __runDll(dll)
            Else
                Call AppEngine.ExternalCall.Scan(Me)
            End If

            Me._EnginePlugins = Plugins.ExternalCall.Scan(Me)
        End Sub

        ''' <summary>
        ''' Call sub main in the <see cref="WebApp"/> dll
        ''' </summary>
        ''' <param name="dll"></param>
        Private Sub __runDll(dll As String)
            Dim assm As Assembly = Assembly.LoadFile(dll)
            Dim types As Type() = assm.GetTypes
            Dim webApp As Type = LinqAPI.DefaultFirst(Of Type) <=
 _
                From type As Type
                In types
                Where String.Equals(type.Name, NameOf(AppEngine.WebApp), StringComparison.OrdinalIgnoreCase)
                Select type

            If webApp Is Nothing Then
                Return     ' 没有定义 Sub Main，则忽略掉这次调用
            End If

            Dim ms = webApp.GetMethods
            Dim main As MethodInfo = LinqAPI.DefaultFirst(Of MethodInfo) <=
 _
                From m As MethodInfo
                In ms
                Where String.Equals(m.Name, "Main", StringComparison.OrdinalIgnoreCase)
                Select m

            If main Is Nothing Then
                Return
            End If

            Dim params = main.GetParameters

            If params.IsNullOrEmpty Then
                Call main.Invoke(Nothing, Nothing)
            Else
                Dim args As Object() = {Me}
                Call main.Invoke(Nothing, args)
            End If
        End Sub

        Public Const contentType As String = "Content-Type"

        Public Overrides Sub handlePOSTRequest(p As HttpProcessor, inputData As MemoryStream)
            Dim request As New HttpPOSTRequest(p, inputData)
            Dim response As New HttpResponse(p.outputStream)
            Dim success As Boolean = AppManager.InvokePOST(request, response)

            Call __finally(request, success)
        End Sub

        ''' <summary>
        ''' GET
        ''' </summary>
        ''' <param name="p"></param>
        Protected Overrides Sub __handleREST(p As HttpProcessor)
            Dim request As New HttpRequest(p)
            Dim response As New HttpResponse(p.outputStream)
            Dim success As Boolean = AppManager.Invoke(request, response)
            Call __finally(request, success)
        End Sub

        Public Overrides Sub handleOtherMethod(p As HttpProcessor)
            MyBase.handleOtherMethod(p)
            Call __finally(New HttpRequest(p), False)
        End Sub

        Private Sub __finally(p As HttpRequest, success As Boolean)
            For Each plugin As PluginBase In EnginePlugins
                Call plugin.handleVisit(p, success)
            Next
        End Sub

        Protected Overrides Sub Dispose(disposing As Boolean)
            For Each plugin As Plugins.PluginBase In EnginePlugins
                Call plugin.Dispose()
            Next
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
