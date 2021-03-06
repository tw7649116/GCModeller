﻿#Region "Microsoft.VisualBasic::7f987c09a2e2400bf6e31382d6de9dc1, ..\interops\localblast\LocalBLAST\Analysis\Models\Hits.vb"

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

Imports System.Xml.Serialization
Imports Microsoft.VisualBasic.ComponentModel.Collection.Generic
Imports Microsoft.VisualBasic.Language

Namespace Analysis

    ''' <summary>
    ''' A collection of hits for the target query protein.
    ''' </summary>
    ''' <remarks>
    ''' 其实这个就是相当于一个KEGG里面的SSDB BBH结果文件
    ''' </remarks>
    Public Class HitCollection : Implements sIdEnumerable

        ''' <summary>
        ''' 按照物种编号取出数据构建一个新的bbh集合
        ''' </summary>
        ''' <param name="spTags"></param>
        ''' <returns></returns>
        Public Function Take(spTags As String()) As HitCollection
            Dim LQuery As Hit() =
                LinqAPI.Exec(Of Hit) <= From hitData As Hit
                                        In Hits
                                        Where Array.IndexOf(spTags, hitData.tag) > -1
                                        Select hitData

            Return New HitCollection With {
                .Hits = LQuery,
                .Description = Description,
                .QueryName = QueryName
            }
        End Function

        ''' <summary>
        ''' The locus tag of the query protein.(主键蛋白质名称)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <XmlAttribute> Public Property QueryName As String Implements sIdEnumerable.Identifier

        ''' <summary>
        ''' Query protein functional annotation.
        ''' </summary>
        ''' <returns></returns>
        Public Property Description As String

        ''' <summary>
        ''' Query hits protein.
        ''' </summary>
        ''' <returns></returns>
        <XmlElement> Public Property Hits As Hit()
            Get
                Return __hits
            End Get
            Set(value As Hit())
                __hits = value
                If __hits.IsNullOrEmpty Then
                    __hitsHash = New Dictionary(Of Hit)
                    __hits = New Hit() {}
                Else
                    __hitsHash = New Dictionary(Of Hit)(
                        (From x As Hit
                         In value
                         Select x
                         Group x By x.HitName Into Group) _
                              .ToDictionary(Function(x) x.HitName,
                                            Function(x) x.Group.First))
                End If
            End Set
        End Property

        Dim __hits As Hit()
        Dim __hitsHash As Dictionary(Of Hit)

        Public Overrides Function ToString() As String
            Return String.Format("{0}:   {1}", QueryName, Description)
        End Function

        ''' <summary>
        ''' Gets hits protein tag inform by hit protein locus_tag
        ''' </summary>
        ''' <param name="hitName"></param>
        ''' <returns></returns>
        Default Public ReadOnly Property Hit(hitName As String) As Hit
            Get
                If __hitsHash.ContainsKey(hitName) Then
                    Return __hitsHash(hitName)
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public Function GetHitByTagInfo(tag As String) As Hit
            Dim LQuery = From hit As Hit
                         In Hits
                         Where String.Equals(hit.tag, tag, StringComparison.OrdinalIgnoreCase)
                         Select hit
            Return LQuery.FirstOrDefault
        End Function

        ''' <summary>
        ''' 按照菌株排序
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Function __orderBySp() As HitCollection
            Me.Hits =
                LinqAPI.Exec(Of Hit) <= From hit As Hit
                                        In Me.Hits
                                        Select hit
                                        Order By hit.tag Ascending
            Return Me
        End Function
    End Class

    ''' <summary>
    ''' 和Query的一个比对结果
    ''' </summary>
    Public Class Hit : Implements sIdEnumerable

        ''' <summary>
        ''' <see cref="HitName"></see>所在的物种
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <XmlAttribute> Public Property tag As String
        ''' <summary>
        ''' 和query蛋白质比对上的
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <XmlAttribute> Public Property HitName As String Implements sIdEnumerable.Identifier
        <XmlAttribute> Public Property Identities As Double
        <XmlAttribute> Public Property Positive As Double

        Public Overrides Function ToString() As String
            Return $"[{tag}] {HitName},    Identities:= {Identities};   Positive:= {Positive};"
        End Function
    End Class
End Namespace
