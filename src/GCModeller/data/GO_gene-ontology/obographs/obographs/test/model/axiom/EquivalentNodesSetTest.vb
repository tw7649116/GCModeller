﻿#Region "Microsoft.VisualBasic::fed5b9f51fa4c2317c301beda0d55a13, ..\GCModeller\data\GO_gene-ontology\obographs\obographs\test\model\axiom\EquivalentNodesSetTest.vb"

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

Imports System.Collections.Generic
Imports org.junit.Assert

Namespace org.geneontology.obographs.model.axiom




	Public Class EquivalentNodesSetTest

'JAVA TO VB CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		Public Overridable Sub test()
			Dim ens As org.geneontology.obographs.model.axiom.EquivalentNodesSet = build()
			assertEquals(2, ens.NodeIds.size())
		End Sub



		Public Shared Function build() As org.geneontology.obographs.model.axiom.EquivalentNodesSet
			Dim ids As String() = {"X:1", "X:2"}
			Dim nodeIds As java.util.Set(Of String) = New HashSet(Of String)(java.util.Arrays.asList(ids))
			Return (New org.geneontology.obographs.model.axiom.EquivalentNodesSet.Builder).nodeIds(nodeIds).representativeNodeId(ids(0)).build()

		End Function


	End Class

End Namespace
