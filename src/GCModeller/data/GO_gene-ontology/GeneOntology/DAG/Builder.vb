﻿#Region "Microsoft.VisualBasic::f9ddd6d6d4ab4283183e84ec452f530b, ..\GCModeller\data\GO_gene-ontology\GeneOntology\DAG\Builder.vb"

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

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Linq
Imports SMRUCC.genomics.Data.GeneOntology.OBO

Namespace DAG

    Public Module Builder

        <Extension>
        Public Function BuildTree(file As IEnumerable(Of OBO.Term)) As Dictionary(Of Term)
            Dim tree As New Dictionary(Of Term)

            For Each x As OBO.Term In file
                tree += New Term With {
                    .id = x.id,
                    .is_a = x.is_a.ToArray(Function(s) New is_a(s$)),
                    .relationship = x.relationship.ToArray(Function(s) New Relationship(s$)),
                    .synonym = x.synonym.ToArray(Function(s) New synonym(s$)),
                    .xref = x.xref.ToArray(AddressOf xrefParser),
                    .namespace = x.namespace
                }
            Next

            Return tree
        End Function

        Private Function xrefParser(s$) As NamedValue(Of String)
            Dim tokens$() = CommandLine.GetTokens(s$)
            Dim id = tokens(Scan0).Split(":"c)

            Return New NamedValue(Of String) With {
                .Name = id(Scan0),
                .x = id(1%),
                .Description = tokens.Get(1%)
            }
        End Function

        Public Function BuildTree(path$) As Dictionary(Of Term)
            Return GO_OBO.Open(path).BuildTree
        End Function
    End Module
End Namespace
