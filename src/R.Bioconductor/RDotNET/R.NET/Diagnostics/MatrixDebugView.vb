﻿#Region "Microsoft.VisualBasic::cfa0de9f4a211f39b7e13e6fdb5083bc, ..\R.Bioconductor\RDotNET\R.NET\Diagnostics\MatrixDebugView.vb"

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

Imports System.Diagnostics

Namespace Diagnostics
	Friend Class MatrixDebugView(Of T)
		Private ReadOnly matrix As Matrix(Of T)

		Public Sub New(matrix As Matrix(Of T))
			Me.matrix = matrix
		End Sub

		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden)> _
		Public ReadOnly Property Value() As T(,)
			Get
				Dim array = New T(Me.matrix.RowCount - 1, Me.matrix.ColumnCount - 1) {}
				Me.matrix.CopyTo(array, Me.matrix.RowCount, Me.matrix.ColumnCount)
				Return array
			End Get
		End Property
	End Class
End Namespace
