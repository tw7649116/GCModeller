﻿#Region "Microsoft.VisualBasic::afe604e5b15fc0a1ae6cccea5ae2afc8, ..\GCModeller\analysis\SequenceToolkit\SNP\SNPVcf.vb"

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

Imports Microsoft.VisualBasic.Data.csv
Imports Microsoft.VisualBasic.Data.csv.StorageProvider.Reflection
Imports Microsoft.VisualBasic.Serialization
Imports Microsoft.VisualBasic.Serialization.JSON

Public Class SNPVcf

    <Column("#CHROM")> Public Property CHROM As String
    Public Property POS As Integer
    Public Property ID As String
    Public Property REF As String
    Public Property ALT As String
    Public Property QUAL As String
    Public Property FILTER As String
    Public Property INFO As String
    Public Property FORMAT As String
    <Meta> Public Property Sequences As Dictionary(Of String, String)

    Public Overrides Function ToString() As String
        Return Me.GetJson
    End Function

    Public Shared Function Load(path As String) As SNPVcf()
        Dim bufs As IEnumerable(Of String) = path.ReadAllLines.Skip(3)
        Dim out As SNPVcf() = DataImports.ImportsData(bufs).AsDataSource(Of SNPVcf)
        Return out
    End Function
End Class
