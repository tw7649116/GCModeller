﻿#Region "Microsoft.VisualBasic::f4b84049e8c82714d855023b045d07ab, ..\GCModeller\data\GO_gene-ontology\GeneOntology\Files\Obo\base.vb"

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

Imports SMRUCC.genomics.foundation.OBO_Foundry

Namespace OBO

    Public MustInherit Class base

        ''' <summary>
        ''' Every term has a term name—e.g. mitochondrion, glucose transport, amino acid binding—and a unique zero-padded seven digit 
        ''' identifier (often called the term accession or term accession number) prefixed by GO:, e.g. GO:0005125 or GO:0060092. 
        ''' The numerical portion of the ID has no inherent meaning or relation to the position of the term in the ontologies. 
        ''' Ranges of GO IDs are assigned to individual ontology editors or editing groups, and can thus be used to trace who added the term.
        ''' </summary>
        ''' <returns></returns>
        <Field("id")> Public Property id As String
        <Field("name")> Public Property name As String
        ''' <summary>
        ''' Denotes which of the three sub-ontologies—cellular component, biological process or molecular function—the term belongs to.
        ''' </summary>
        ''' <returns></returns>
        <Field("namespace")> Public Property [namespace] As String
    End Class
End Namespace
