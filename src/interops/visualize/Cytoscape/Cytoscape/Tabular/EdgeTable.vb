﻿#Region "Microsoft.VisualBasic::31451245648148be85b9f62b86243d11, ..\interops\visualize\Cytoscape\Cytoscape\Tabular\EdgeTable.vb"

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

Imports Microsoft.VisualBasic.Data.csv.StorageProvider.Reflection

Namespace Tables

    Public Class Edge
        Public Property SUID As Integer
        Public Property Confidence As Double
        Public Property EdgeBetweenness As Double
        <Column("interaction name")> Public Property Interaction As String
        <Column("shared interaction")> Public Property SharedInteraction As String
        <Column("shared name")> Public Property SharedName As String

        Public Overrides Function ToString() As String
            Return String.Format("[{0}]  {1}", SUID, SharedName)
        End Function
    End Class
End Namespace
