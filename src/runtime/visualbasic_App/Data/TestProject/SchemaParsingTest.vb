﻿#Region "Microsoft.VisualBasic::a6a75db4183e05f2a50c54cd05c77036, ..\visualbasic_App\Data\TestProject\SchemaParsingTest.vb"

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

Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Data.csv.StorageProvider.Reflection
Imports TestProject.RTools

Public Class SchemaParsingTest

    Public Property a As CommandLine.CommandLine = "1 2 3 4 5"
    Public Property b As String = Guid.NewGuid.ToString
    Public Property c As Double = RandomDouble()
    Public Property d As Integer = RandomDouble()
    Public Property e As Long = Now.ToBinary
    Public Property f As Date
    Public Property g As String() = {"35345", "645646", RandomDouble()}
    Public Property h As Boolean
    <Column("abcc")> Public Property i As GZip.ArchiveAction
    Public Property KV As KeyValuePair(Of String, String) = New KeyValuePair(Of String, String)(RandomDouble, "1234")

    <Linq.Mapping.Column(Name:="alias.linq")> Public ReadOnly Property LINQAlias As Date = Now

    Public Property ffssdfsd As NamedValue(Of DESeq)

End Class
