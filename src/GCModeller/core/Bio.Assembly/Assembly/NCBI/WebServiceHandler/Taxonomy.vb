﻿#Region "Microsoft.VisualBasic::55a0a502b79f7fa513ff969982ee5601, ..\GCModeller\core\Bio.Assembly\Assembly\NCBI\WebServiceHandler\Taxonomy.vb"

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
Imports System.Xml.Serialization
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.ComponentModel
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Text.Xml.Models
Imports SMRUCC.genomics.SequenceModel.FASTA

Namespace Assembly.NCBI.Entrez

    ''' <summary>
    ''' ##### Automatically Getting The Ncbi Taxonomy Id From The Genbank Identifier
    ''' 
    ''' The question is whether, given a (long) list of Genbank identifiers, is possible to 
    ''' get the ncbi taxonomy identifier for each one. I know it may seem very easy, but I 
    ''' have not found any web service which makes this, and I wouldn't like to do this 
    ''' manually.
    ''' </summary>
    Public Module TaxonomyWebAPI

        Public Const API As String = "http://eutils.ncbi.nlm.nih.gov/entrez/eutils/efetch.fcgi?db=nuccore&id={0}&rettype={1}&retmode=xml"

        ''' <summary>
        ''' NCBI efetch can use an accession number instead of a gi. and the XML/Fasta returned by efetch contains the taxonomy-ID:
        ''' </summary>
        ''' <param name="gi"></param>
        ''' <returns></returns>
        ''' 
        <Extension>
        Public Function efetch(gi As String, Optional rettype As String = "fasta") As TSeqSet
            Dim url As String = API.sFormat(gi, rettype)
            Dim xml As String = url.GetRequest
            Dim seqs As TSeqSet = xml.LoadFromXml(Of TSeqSet)
            Return seqs
        End Function

        <Extension>
        Public Function efetch(gi As IEnumerable(Of String), Optional rettype As String = "fasta", Optional parts As Boolean = False) As TSeqSet
            If Not parts Then
                Return String.Join(",", gi.ToArray).efetch(rettype)
            Else
                Dim out As New List(Of TSeq)

                For Each buf In gi.SplitIterator(128)
                    out += String.Join(",", gi.ToArray).efetch(rettype).TSeq
                    Call Console.Write(".")
                Next

                Return New TSeqSet With {
                    .TSeq = out
                }
            End If
        End Function

        <Extension>
        Public Iterator Function efetch(source As IEnumerable(Of FastaToken),
                                        getId As Func(Of FastaToken, String),
                                        Optional bufSize As Integer = 128) As IEnumerable(Of TSeqSet)

            Dim tmp As New List(Of String)

            For Each nt In source
                Dim id As String = getId(nt)

                If Not String.IsNullOrEmpty(id) Then
                    tmp += id
                Else
                    Call nt.Title.Warning
                End If

                If tmp.Count >= bufSize Then
                    Yield tmp.efetch
                    Call tmp.Clear()
                Else

                End If
            Next

            If tmp.Count > 0 Then
                Yield tmp.efetch
            End If
        End Function

        <Extension>
        Public Function efetch(source As IEnumerable(Of FastaToken), Optional bufSize As Integer = 128) As IEnumerable(Of TSeqSet)
            Dim getId As Func(Of FastaToken, String) =
                Function(nt) nt.Title.Match("gi\|\d+", RegexICSng).Split("|"c).Last
            Return source.efetch(getId, bufSize)
        End Function
    End Module

    Public Class TSeqSet : Inherits ClassObject
        <XmlElement> Public Property TSeq As TSeq()
    End Class

    Public Class SeqBrief : Inherits ClassObject
        Public Property TSeq_gi As String
        Public Property TSeq_accver As String
        Public Property TSeq_taxid As String
        Public Property TSeq_orgname As String
        Public Property TSeq_defline As String
        Public Property TSeq_length As String
    End Class

    Public Class TSeq : Inherits SeqBrief
        Public Property TSeq_seqtype As StringValue
        Public Property TSeq_sequence As String
    End Class
End Namespace
