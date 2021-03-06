﻿#Region "Microsoft.VisualBasic::12492ca38c7992cf17b566589f47061f, ..\interops\RNA-Seq\RNA-seq.Data\Fastaq\FastaqFile.vb"

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
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.ComponentModel
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Parallel.Linq

Namespace Fastaq

    ''' <summary>
    ''' There is no standard file extension for a FASTQ file, but .fq and .fastq, are commonly used.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FastaqFile : Inherits ITextFile
        Implements IEnumerable(Of Fastaq)
        Implements IList(Of Fastaq)

        Dim _innerList As New List(Of Fastaq)

        ''' <summary>
        ''' Load the fastq data from a specific file handle.(从一个特定的文件句柄之中加载fastq文件的数据)
        ''' </summary>
        ''' <param name="Path">The file handle of the fastq data.</param>
        ''' <returns></returns>
        Public Shared Function Load(Path As String) As FastaqFile
            Dim sw As Stopwatch = Stopwatch.StartNew

            Call $"Start to load file data from handle *{Path.ToFileURL}".__DEBUG_ECHO

            Dim stream As New BufferedStream(Path)

            Call $"[Job Done!] {sw.ElapsedMilliseconds}ms...".__DEBUG_ECHO
            Call "Start to parsing the fastq format data...".__DEBUG_ECHO

            sw = Stopwatch.StartNew

            Dim sBufs As IEnumerable(Of String()) =
                TaskPartitions.SplitIterator(Of String)(stream.ReadAllLines, 4)
            Dim LQuery As Fastaq() =
                LinqAPI.Exec(Of Fastaq) <= From buf As String()
                                           In sBufs.AsParallel
                                           Select Fastaq.FastaqParser(buf)
            Dim FastaqFile As New FastaqFile With {
                ._innerList = LQuery.ToList,
                .FilePath = Path
            }
            Call $"[Job Done!] Fastq format data parsing in {sw.ElapsedMilliseconds}ms...".__DEBUG_ECHO

            Return FastaqFile
        End Function

#Region "Implements Generic.IEnumerable(Of Fastaq)"

        Public Iterator Function GetEnumerator() As IEnumerator(Of Fastaq) Implements IEnumerable(Of Fastaq).GetEnumerator
            For Each Fastaq As Fastaq In _innerList
                Yield Fastaq
            Next
        End Function

        Public Iterator Function GetEnumerator1() As IEnumerator Implements IEnumerable.GetEnumerator
            Yield GetEnumerator()
        End Function
#End Region

#Region "Implements Generic.IList(Of Fastaq)"

        Public Sub Add(item As Fastaq) Implements ICollection(Of Fastaq).Add

        End Sub

        Public Sub Clear() Implements ICollection(Of Fastaq).Clear

        End Sub

        Public Function Contains(item As Fastaq) As Boolean Implements ICollection(Of Fastaq).Contains
            Throw New NotImplementedException
        End Function

        Public Overloads Sub CopyTo(array() As Fastaq, arrayIndex As Integer) Implements ICollection(Of Fastaq).CopyTo

        End Sub

        Public ReadOnly Property NumOfReads As Integer Implements ICollection(Of Fastaq).Count
            Get
                Return _innerList.Count
            End Get
        End Property

        Public ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of Fastaq).IsReadOnly
            Get
                Return False
            End Get
        End Property

        Public Function Remove(item As Fastaq) As Boolean Implements ICollection(Of Fastaq).Remove
            Throw New NotImplementedException
        End Function

        Public Function IndexOf(item As Fastaq) As Integer Implements IList(Of Fastaq).IndexOf
            Throw New NotImplementedException
        End Function

        Public Sub Insert(index As Integer, item As Fastaq) Implements IList(Of Fastaq).Insert

        End Sub

        Default Public Property Item(index As Integer) As Fastaq Implements IList(Of Fastaq).Item
            Get
                Return Me._innerList(index)
            End Get
            Set(value As Fastaq)
                Me._innerList(index) = value
            End Set
        End Property

        Public Sub RemoveAt(index As Integer) Implements IList(Of Fastaq).RemoveAt

        End Sub
#End Region

        Public Overrides Function Save(Optional FilePath As String = "", Optional Encoding As Encoding = Nothing) As Boolean
            Throw New NotImplementedException
        End Function

        ''' <summary>
        ''' Convert fastaq data into a fasta data file.
        ''' </summary>
        ''' <returns></returns>
        Public Function ToFasta(Optional index As Boolean = False) As FASTA.FastaFile
            Dim sw As Stopwatch = Stopwatch.StartNew

            Call "Start to convert fastq to fastq...".__DEBUG_ECHO

            Dim __attrs As Func(Of Integer, Fastaq, String())

            If index Then
                __attrs = Function(i, fq) {$"lcl={i} ", fq.SEQ_ID.ToString}
            Else
                __attrs = Function(i, fq) {fq.SEQ_ID.ToString}
            End If

            Dim LQuery As FASTA.FastaToken() =
                LinqAPI.Exec(Of FASTA.FastaToken) <= From fq As SeqValue(Of Fastaq)
                                                     In Me.SeqIterator.AsParallel
                                                     Let read As Fastaq = fq.obj
                                                     Let attrs As String() = __trim(__attrs(fq.i, read))
                                                     Select fasta = New FASTA.FastaToken With {
                                                         .SequenceData = read.SequenceData,
                                                         .Attributes = attrs
                                                     }
                                                     Order By fasta.Attributes.First Ascending

            Call $"[Job Done!] {sw.ElapsedMilliseconds}ms...".__DEBUG_ECHO

            Return New FASTA.FastaFile(LQuery)
        End Function

        Private Shared Function __trim(attrs As String()) As String()
            Dim last As String = attrs.Last

            If last.First = "@"c Then
                attrs(attrs.Length - 1) = Mid(last, 2).Trim
            End If

            Return attrs
        End Function
    End Class
End Namespace
