﻿#Region "Microsoft.VisualBasic::40cfbe0715822d2f4bba0e25353ba3bf, ..\visualbasic_App\Microsoft.VisualBasic.Architecture.Framework\Extensions\IO\IO.vb"

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

Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Text
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.FileIO
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Scripting.MetaData
Imports Microsoft.VisualBasic.Text

''' <summary>
''' IO函数拓展
''' </summary>
<PackageNamespace("IO")>
Public Module IOExtensions

    <Extension>
    Public Function ReadVector(path As String) As Double()
        Return IO.File.ReadAllLines(path).ToArray(Function(x) CDbl(x))
    End Function

    ''' <summary>
    ''' 打开本地文件指针，这是一个安全的函数，会自动创建不存在的文件夹
    ''' </summary>
    ''' <param name="path">文件的路径</param>
    ''' <param name="mode">文件指针的打开模式</param>
    ''' <returns></returns>
    <ExportAPI("Open.File")>
    <Extension>
    Public Function Open(path$, Optional mode As FileMode = FileMode.OpenOrCreate) As FileStream
        Call path.ParentPath.MkDIR
        Return IO.File.Open(path, mode)
    End Function

    <ExportAPI("Open.Reader")>
    <Extension>
    Public Function OpenReader(path As String, Optional encoding As Encoding = Nothing) As StreamReader
        encoding = If(encoding Is Nothing, Encoding.Default, encoding)
        Return New StreamReader(IO.File.Open(path, FileMode.OpenOrCreate), encoding)
    End Function

    ''' <summary>
    ''' <see cref="IO.File.ReadAllBytes"/>, if the file is not exists on the filesystem, then a empty array will be return.
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    <Extension>
    Public Function ReadBinary(path As String) As Byte()
        If Not path.FileExists Then
            Return {}
        End If
        Return IO.File.ReadAllBytes(path)
    End Function

    ''' <summary>
    ''' Write all object into a text file by using its <see cref="Object.ToString"/> method.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="data"></param>
    ''' <param name="saveTo"></param>
    ''' <param name="encoding"></param>
    ''' <returns></returns>
    <Extension> Public Function FlushAllLines(Of T)(data As IEnumerable(Of T), saveTo$, Optional encoding As Encodings = Encodings.Default) As Boolean
        Return data.FlushAllLines(saveTo, encoding.GetEncodings)
    End Function

    ''' <summary>
    ''' Save the binary data into the filesystem.(保存二进制数据包值文件系统)
    ''' </summary>
    ''' <param name="buf">The binary bytes data of the target package's data.(目标二进制数据)</param>
    ''' <param name="path">The saved file path of the target binary data chunk.(目标二进制数据包所要进行保存的文件名路径)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''
    <ExportAPI("FlushStream")>
    <Extension> Public Function FlushStream(buf As IEnumerable(Of Byte), <Parameter("Path.Save")> path As String) As Boolean
        Dim parentDIR As String = If(String.IsNullOrEmpty(path),
            FileIO.FileSystem.CurrentDirectory,
            FileIO.FileSystem.GetParentPath(path))

        Call FileIO.FileSystem.CreateDirectory(parentDIR)
        Call FileIO.FileSystem.WriteAllBytes(path, buf.ToArray, False)

        Return True
    End Function

    <ExportAPI("FlushStream")>
    <Extension> Public Function FlushStream(stream As Net.Protocols.ISerializable, savePath As String) As Boolean
        Dim rawStream As Byte() = stream.Serialize
        If rawStream Is Nothing Then
            rawStream = New Byte() {}
        End If
        Return rawStream.FlushStream(savePath)
    End Function
End Module
