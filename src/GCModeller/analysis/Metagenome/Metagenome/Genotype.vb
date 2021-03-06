﻿#Region "Microsoft.VisualBasic::8c1b9454e952c7607fc62f7c5ae86514, ..\GCModeller\analysis\Metagenome\Metagenome\Genotype.vb"

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
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.ComponentModel
Imports Microsoft.VisualBasic.Data.csv
Imports Microsoft.VisualBasic.Data.csv.DocumentStream
Imports Microsoft.VisualBasic.Data.csv.StorageProvider.Reflection
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Serialization.JSON

Public Module Genotype

    ReadOnly __all As KeyValuePair(Of Char, Char)() =
        Comb(Of Char).CreateCompleteObjectPairs({"A"c, "T"c, "G"c, "C"c}) _
                     .MatrixAsIterator _
                     .ToArray

    <Extension>
    Public Function ExpandLocis(source As IEnumerable(Of GenotypeDetails)) As DocumentStream.File
        Dim file As New DocumentStream.File
        Dim head As New RowObject From {"", "Genomes"}

        For Each pp As KeyValuePair(Of Char, Char) In __all
            Call head.Add($"locus.{pp.Key}{pp.Value}")
        Next

        Call file.AppendLine(head)

        For Each x As GenotypeDetails In source
            Dim row As New RowObject From {x.Population.Split(":"c).Last}

            If row.First = "ALL" Then
                Continue For
            End If

            row += CStr(x.Frequency.Sum(Function(f) f.Count))
            row += From pp As KeyValuePair(Of Char, Char)
                   In __all
                   Select CStr(x(pp.Key, pp.Value).Frequency)
            file += row
        Next

        Dim notZEROs As New List(Of String())

        For Each col In file.Columns
            Dim notZERO As String = LinqAPI.DefaultFirst(Of String) <=
 _
                From s As String
                In col.Skip(1)  ' 由于第一行是标题，所以肯定不是0值，跳过第一行
                Where Not String.IsNullOrEmpty(s) AndAlso
                    s <> "0" AndAlso
                    s <> "0.0"
                Select s

            If Not String.IsNullOrEmpty(notZERO) Then
                notZEROs += col
            End If
        Next

        Dim gns = notZEROs(1).Skip(1).ToArray(Function(x) CInt(x))
        Dim max As Integer = gns.Max

        For i As Integer = 0 To gns.Length - 1
            gns(i) = CInt(10 * gns(i) / max)
        Next

        notZEROs(1) = (notZEROs(1).First + gns.ToList(Function(x) x.ToString))
        file = notZEROs.JoinColumns

        Return file
    End Function

    <Extension>
    Public Function Statics(source As DocumentStream.File) As DocumentStream.File
        Dim out As New DocumentStream.File
        out.AppendRange(source.Select(Function(x) New RowObject(x.ToArray)))
        out.AppendLine()

        Dim row As New RowObject From {"types"}
        row.AddRange(source.First.Skip(1))

        Dim total As Integer() = New Integer(source.First.Skip(1).Count) {}

        For Each line In source.Skip(1)
            Dim ns As Integer() = line.Skip(1).ToArray(Function(s) CInt(Val(s)))

            For Each x In ns.SeqIterator
                total(x.i) += x.obj
            Next

            row = New RowObject From {line.First}
            row.AddRange(ns.ToArray(Function(n) n.ToString))
            out.AppendLine(row)
        Next

        out.AppendLine()
        row = New RowObject From {"total"}
        row.AddRange(total.ToArray(Function(x) x.ToString))
        out.AppendLine(row)
        out.AppendLine()

        Dim nn As New List(Of Integer())

        ' no
        For Each line In source.Skip(1)
            Dim ns As Integer() = line.Skip(1).ToArray(Function(s) CInt(Val(s)))

            row = New RowObject From {"no-" & line.First}
            ns = ns.SeqIterator.ToArray(Function(x) total(x.i) - x.obj)
            row.AddRange(ns.ToArray(Function(x) x.ToString))
            out.AppendLine(row)
            nn += ns
        Next

        out.AppendLine()

        For Each line In source.Skip(1).SeqIterator
            Dim ns As Integer() = line.obj.Skip(1).ToArray(Function(s) CInt(Val(s))) 'A/A
            Dim no As Integer() = nn(line.i)  ' no-A/A

            row = New RowObject From {line.obj.First}
            row.AddRange(ns.ToArray(Function(x) x.ToString))
            out.AppendLine(row)
            row = New RowObject From {"no-" & line.obj.First}
            row.AddRange(no.ToArray(Function(x) x.ToString))
            out.AppendLine(row)
            out.AppendLine()
        Next

        Return out
    End Function

    ''' <summary>
    ''' 进行数据视图的转换
    ''' </summary>
    ''' <param name="source"></param>
    ''' <returns></returns>
    <Extension>
    Public Function TransViews(source As IEnumerable(Of GenotypeDetails)) As DocumentStream.File
        Dim out As New DocumentStream.File
        Dim array As GenotypeDetails() = source.ToArray()
        Dim allTag As String() = array.ToArray(Function(x) x.Population.Split(":"c).Last)
        Dim all = Comb(Of Char).CreateCompleteObjectPairs({"A"c, "T"c, "G"c, "C"c}).MatrixAsIterator
        Dim head As New RowObject From {"types"}

        For Each tag As String In allTag
            head += tag
        Next

        out += head

        For Each tag As KeyValuePair(Of Char, Char) In all
            Dim row As New RowObject({$"{tag.Key}/{tag.Value}"})

            For Each sample In array
                Dim genotype = sample(tag.Key, tag.Value)

                row += $"{genotype.Count} ({genotype.Frequency * 100})"
            Next

            out += row
        Next

        Return out
    End Function

    ''' <summary>
    ''' Example: ``C: 0.844 (162)``
    ''' </summary>
    ''' <param name="raw"></param>
    ''' <returns></returns>
    <Extension>
    Public Function FrequencyParser(raw As String) As Frequency
        Dim base As Char = raw.First
        raw = Mid(raw, 3).Trim
        Dim Count As String = Regex.Match(raw, "\(\d+\)").Value.GetStackValue("(", ")")
        Dim f As Double = Val(raw)

        Return New Frequency With {
            .base = base,
            .Count = CInt(Count),
            .Frequency = f
        }
    End Function

    Const Frequency As String = "[ATGC]: \d\.\d+ \(\d+\)"

    Public Function Frequencies(field As String) As Frequency()
        Dim fs As String() = Regex.Matches(field, Frequency, RegexICSng).ToArray
        Return fs.ToArray(AddressOf FrequencyParser)
    End Function

    Const Genotype As String = "[ATGC]\|[ATGC]: \d\.\d+ \(\d+\)"

    Public Function Genotypes(field As String) As Frequency()
        Dim fs As String() = Regex.Matches(field, Genotype, RegexICSng).ToArray
        Dim out As New List(Of Frequency)

        For Each m As String In fs
            Dim g As Char = m.First
            m = Mid(m, 3).Trim
            Dim f As Frequency = FrequencyParser(m)
            f.type = g
            out += f
        Next

        Return out.ToArray
    End Function
End Module

Public Class GenotypeDetails

    Public Property Population As String

    <Column("Allele: frequency (count)")>
    Public Property AlleleFrequency As String
        Get
            Return String.Join("", Frequency.ToArray(Function(x) x.ToString))
        End Get
        Set(value As String)
            _Frequency = Genotype.Frequencies(value)
        End Set
    End Property

    <Column("Genotype: frequency (count)")>
    Public Property GenotypeFreqnency As String
        Get
            Return String.Join("", Genotypes.ToArray(Function(x) x.ToString))
        End Get
        Set(value As String)
            _Genotypes = Genotype.Genotypes(value)
        End Set
    End Property

    Default Public Overloads ReadOnly Property [GetGenotype](type As Char, base As Char) As Frequency
        Get
            For Each x In Genotypes
                If x.type = type AndAlso x.base = base Then
                    Return x
                End If
            Next

            Return New Frequency
        End Get
    End Property

    Public ReadOnly Property Frequency As Frequency()
    Public ReadOnly Property Genotypes As Frequency()

    Public Overrides Function ToString() As String
        Return Me.GetJson
    End Function
End Class

Public Class Frequency

    ''' <summary>
    ''' 分子
    ''' </summary>
    ''' <returns></returns>
    Public Property type As Char
    ''' <summary>
    ''' 分母
    ''' </summary>
    ''' <returns></returns>
    Public Property base As Char
    Public Property Frequency As Double
    Public Property Count As Integer

    Public Overrides Function ToString() As String
        If type = Nothing OrElse type = NIL Then
            Return $"{base}: {Frequency} ({Count})"
        Else
            Return $"{type}|{base}: {Frequency} ({Count})"
        End If
    End Function
End Class
