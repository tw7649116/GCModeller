﻿#Region "Microsoft.VisualBasic::31603529d5833ee57d6568e503fc395f, ..\GCModeller\core\Bio.Assembly\Assembly\NCBI\Database\GenBank\GBK\Keywords\ORIGIN.vb"

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

Imports System.Text
Imports System.Xml.Serialization
Imports SMRUCC.genomics.Assembly.NCBI.GenBank.GBFF.Keywords.FEATURES
Imports SMRUCC.genomics.SequenceModel
Imports SMRUCC.genomics.SequenceModel.FASTA
Imports SMRUCC.genomics.SequenceModel.NucleotideModels
Imports Microsoft.VisualBasic.Language

Namespace Assembly.NCBI.GenBank.GBFF.Keywords

    ''' <summary>
    ''' This GenBank keyword section stores the sequence data for this database.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ORIGIN : Inherits KeyWord
        Implements IAbstractFastaToken
        Implements IEnumerable(Of Char)

        ''' <summary>
        ''' The sequence data that stores in this GenBank database, which can be a genomics DNA sequence, protein sequence or RNA sequence.
        ''' (序列数据，类型可以包括基因组DNA序列，蛋白质序列或者RNA序列)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <XmlIgnore> Public Property SequenceData As String Implements I_PolymerSequenceModel.SequenceData
            Get
                Return __sequenceParser.OriginalSequence
            End Get
            Set(value As String)
                Try
                    __sequenceParser = New SegmentReader(value)
                Catch ex As Exception
                    __sequenceParser = New SegmentReader(NucleicAcid.CopyNT(value))
                    Call InvalidWarns.Warning
                End Try
            End Set
        End Property

        ''' <summary>
        ''' The origin nucleic acid sequence contains illegal character in the nt sequence, ignored as character N... 
        ''' for <see cref="SequenceData"/>
        ''' </summary>
        Const InvalidWarns As String = "The origin nucleic acid sequence contains illegal character in the nt sequence, ignored as character N..."

        Dim __sequenceParser As SegmentReader

        ''' <summary>
        ''' ``<see cref="SequenceData"/> -> index``
        ''' </summary>
        ''' <param name="index"></param>
        ''' <returns></returns>
        Default Public ReadOnly Property [Char](index As Long) As Char
            Get
                Return SequenceData(index)
            End Get
        End Property

        Public ReadOnly Property SequenceParser As SegmentReader
            Get
                Return __sequenceParser
            End Get
        End Property

        ''' <summary>
        ''' 基因组的大小
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Size As Long
            Get
                Return Len(__sequenceParser.OriginalSequence)
            End Get
        End Property

        ''' <summary> 
        ''' 获取该Feature位点的序列数据
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetFeatureSegment(Feature As Feature) As String
            Return __sequenceParser.TryParse(Feature.Location.ContiguousRegion).SequenceData
        End Function

        Public Overrides Function ToString() As String
            Return SequenceData
        End Function

        ''' <summary>
        ''' 是整条序列的GC偏移
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property GCSkew As Double
            Get
                Dim G As Integer = (From ch In Me.SequenceData Where ch = "G"c OrElse ch = "g"c Select 1).Count
                Dim C As Integer = (From ch In Me.SequenceData Where ch = "C"c OrElse ch = "c"c Select 1).Count
                Return (G + C) / (G - C)
            End Get
        End Property

        Public Shared Widening Operator CType(Data As String()) As ORIGIN
            Dim sBuilder As StringBuilder = New StringBuilder(2048)

            For Each line As String In Data
                sBuilder.Append(Mid$(line, 10))
            Next

            Dim trimChars As Char() =
                LinqAPI.Exec(Of Char) <= From b As Char In sBuilder.ToString
                                         Where b <> " "c
                                         Select b

            Return New ORIGIN With {
                .SequenceData = New String(trimChars)
            }
        End Operator

        Public Shared Narrowing Operator CType(ori As ORIGIN) As String
            Return ori.SequenceData
        End Operator

        Public Shared Narrowing Operator CType(obj As ORIGIN) As FastaToken
            Return obj.ToFasta
        End Operator

        ''' <summary>
        ''' Returns the whole genome sequence which was records in this GenBank database file.
        ''' (返回记录在本Genbank数据库文件之中的全基因组核酸序列)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ToFasta() As FastaToken
            Dim attrs As String() = {Title & " " & Len(SequenceData) & "bp"}
            Return New FastaToken(attrs, SequenceData)
        End Function

        Public ReadOnly Property Title As String Implements IAbstractFastaToken.Title
            Get
                Return MyBase.gbRaw.Definition.Value
            End Get
        End Property

        Public Property Attributes As String() Implements IAbstractFastaToken.Attributes

        Public Iterator Function GetEnumerator() As IEnumerator(Of Char) Implements IEnumerable(Of Char).GetEnumerator
            For Each ch As Char In SequenceData
                Yield ch
            Next
        End Function

        Public Iterator Function GetEnumerator1() As IEnumerator Implements IEnumerable.GetEnumerator
            Yield GetEnumerator()
        End Function
    End Class
End Namespace
