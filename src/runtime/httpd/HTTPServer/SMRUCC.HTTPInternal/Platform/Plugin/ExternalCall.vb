﻿#Region "Microsoft.VisualBasic::346429dde894586b86da6920c0c2052f, ..\httpd\HTTPServer\SMRUCC.HTTPInternal\Platform\Plugin\ExternalCall.vb"

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

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic

Namespace Platform.Plugins

    Public Module ExternalCall

        Public Function Scan(platform As PlatformEngine) As PluginBase()
            Dim dllFiles As String() = FileIO.FileSystem.GetFiles(App.HOME, FileIO.SearchOption.SearchTopLevelOnly, "*.dll", "*.exe").ToArray
            Dim plugins As New List(Of PluginBase)

            For Each dll As String In dllFiles
                Try
                    Call plugins.Add(dll.__getPlugins(platform))
                Catch ex As Exception
                    ex = New Exception(dll, ex)  ' 可能不是.NET Assembly，则忽略掉错误记录下来然后继续下一个
                    Call App.LogException(ex)
                End Try
            Next

            Return plugins.ToArray
        End Function

        <Extension> Private Function __getPlugins(dll As String, platform As PlatformEngine) As PluginBase()
            Dim assm As Reflection.Assembly = Reflection.Assembly.LoadFile(dll)
            Dim types As Type() = (From typeDef As Type
                                   In assm.GetTypes
                                   Where typeDef.IsInheritsFrom(GetType(PluginBase)) AndAlso Not typeDef.IsAbstract
                                   Select typeDef).ToArray
            If types.Length = 0 Then
                Return New PluginBase() {}
            End If

            Dim plugins As PluginBase() =
                types.ToArray(Of PluginBase)(
                Function(typeDef As Type) DirectCast(Activator.CreateInstance(typeDef, {platform}), PluginBase))
            Return plugins
        End Function
    End Module
End Namespace

