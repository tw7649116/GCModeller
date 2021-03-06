﻿#Region "Microsoft.VisualBasic::35c475a9a6d83aead3d0204665758416, ..\GCModeller\core\Bio.Assembly\ContextModel\TFDensity.vb"

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
Imports System.Web.Script.Serialization
Imports System.Runtime.CompilerServices
Imports SMRUCC.genomics.ComponentModel
Imports SMRUCC.genomics.ComponentModel.Loci
Imports SMRUCC.genomics.ComponentModel.Loci.Abstract
Imports Microsoft.VisualBasic.ComponentModel.Collection.Generic
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Serialization
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Serialization.JSON

Namespace ContextModel

    ''' <summary>
    ''' Calculates the relative density of the TF on each gene on the genome.
    ''' </summary>
    Public Module TFDensity

        ''' <summary>
        ''' Although this function name means calculate the relative density of the TF on the genome for each gene, 
        ''' but you can also calculate any type of gene its density on the genome.
        ''' (虽然名称是调控因子的密度，但是也可以用作为其他类型的基因的密度的计算，
        ''' 这个函数是非顺式的，即只要在ATG前面的范围内或者TGA后面的范围内出现都算存在)
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="genome"></param>
        ''' <param name="TF"></param>
        ''' <returns></returns>
        <Extension>
        Public Function Density(Of T As IGeneBrief)(genome As IGenomicsContextProvider(Of T),
                                                    TF As IEnumerable(Of String),
                                                    Optional ranges As Integer = 10000,
                                                    Optional stranded As Boolean = False) As Density()

            Dim TFs As T() = TF.ToArray(AddressOf genome.GetByName)
            Dim getTF As Func(Of Strands, T()) = New __sourceHelper(Of T)(TFs, stranded).__gets
            Dim result As Density() = genome.__worker(getTF, AddressOf __getGenes(Of T), TFs.Length, ranges)
            Return result
        End Function

        <Extension>
        Private Function __getGenes(Of T As IGeneBrief)(g As T, TFs As T(), ranges As Integer) As T()
            Dim result As New List(Of T)

            If g.Location.Strand = Strands.Forward Then ' 上游是小于ATG，下游是大于TGA
                Dim ATG As Integer = g.Location.Left
                Dim TGA As Integer = g.Location.Right

                For Each loci In TFs
                    If Math.Abs(ATG - loci.Location.Right) <= ranges Then
                        result += loci
                    ElseIf Math.Abs(loci.Location.Left - TGA) <= ranges Then
                        result += loci
                    End If
                Next
            Else
                Dim ATG As Integer = g.Location.Right
                Dim TGA As Integer = g.Location.Left

                For Each loci In TFs
                    If Math.Abs(TGA - loci.Location.Right) <= ranges Then
                        result += loci
                    ElseIf Math.Abs(loci.Location.Left - ATG) <= ranges Then
                        result += loci
                    End If
                Next
            End If

            Return result
        End Function

        Private Structure __sourceHelper(Of T As IGeneBrief)
            Dim data As T()

            Sub New(source As T(), stranded As Boolean)
                data = source

                If stranded Then
                    forwards = (From gene As T In data Where gene.Location.Strand = Strands.Forward Select gene).ToArray
                    reversed = (From gene As T In data Where gene.Location.Strand = Strands.Reverse Select gene).ToArray
                    __gets = AddressOf __stranded
                Else
                    __gets = AddressOf __unstranded
                End If
            End Sub

            Dim forwards As T()
            Dim reversed As T()
            Dim __gets As Func(Of Strands, T())

            Private Function __stranded(strand As Strands) As T()
                Select Case strand
                    Case Strands.Forward
                        Return forwards
                    Case Strands.Reverse
                        Return reversed
                    Case Else
                        Return data
                End Select
            End Function

            Private Function __unstranded(strand As Strands) As T()
                Return data
            End Function
        End Structure

        <Extension> Private Function __worker(Of T As IGeneBrief)(
                                            genome As IGenomicsContextProvider(Of T),
                                             getTF As Func(Of Strands, T()),
                                        getRelated As Func(Of T, T(), Integer, T()),
                                          numTotal As Integer,
                                            ranges As Integer) As Density()

            Dim LQuery As Density() = LinqAPI.Exec(Of Density) <=
 _
                From gene As T
                In genome.AllFeatures
                Let sides As T() = getTF(gene.Location.Strand)
                Let related As T() = getRelated(gene, sides, ranges)
                Select New Density With {
                    .locus_tag = gene.Identifier,
                    .Abundance = related.Length / numTotal,
                    .Hits = related.ToArray(Function(g) g.Identifier),
                    .loci = gene.Location,
                    .product = gene.Product
                }

            Return LQuery
        End Function

        ''' <summary>
        ''' 顺式调控，只有TF出现在上游，并且二者链方向相同才算存在
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="genome">Bacteria genomic context provider.</param>
        ''' <param name="TF">The TF locus_tag list.</param>
        ''' <param name="ranges">
        ''' This value is set to 20000bp is more perfect works on the bacteria genome, probably...
        ''' </param>
        ''' <returns></returns>
        <Extension> Public Function DensityCis(Of T As IGeneBrief)(
                                             genome As IGenomicsContextProvider(Of T),
                                                 TF As IEnumerable(Of String),
                                    Optional ranges As Integer = 10000) As Density()

            Dim TFs As T() = TF.ToArray(AddressOf genome.GetByName)
            Dim getTF As Func(Of Strands, T()) = New __sourceHelper(Of T)(TFs, True).__gets
            Dim result As Density() =
                genome.__worker(getTF,
                                AddressOf __getCisGenes(Of T),
                                TFs.Length,
                                ranges)
            Return result
        End Function

        ''' <summary>
        ''' 查找当前的基因的上游符合距离范围内的TF目标
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="g"></param>
        ''' <param name="TFs"></param>
        ''' <param name="ranges"></param>
        ''' <returns></returns>
        <Extension>
        Private Function __getCisGenes(Of T As IGeneBrief)(g As T, TFs As T(), ranges As Integer) As T()
            Dim result As New List(Of T)

            If g.Location.Strand = Strands.Forward Then ' 上游是小于ATG，下游是大于TGA
                Dim ATG As Integer = g.Location.Left

                For Each loci In TFs
                    If ATG - loci.Location.Right <= ranges Then
                        result += loci
                    End If
                Next
            Else
                Dim ATG As Integer = g.Location.Right

                For Each loci In TFs
                    If loci.Location.Left - ATG <= ranges Then
                        result += loci
                    End If
                Next
            End If

            Return result
        End Function
    End Module

    ''' <summary>
    ''' Genomics context relative abundance
    ''' </summary>
    Public Class Density : Implements sIdEnumerable

        ''' <summary>
        ''' The gene locus_tag identifier
        ''' </summary>
        ''' <returns></returns>
        Public Property locus_tag As String Implements sIdEnumerable.Identifier
        Public Property loci As NucleotideLocation
        ''' <summary>
        ''' The specific features on the genome its relative abundance relative to this gene <see cref="locus_tag"/>
        ''' </summary>
        ''' <returns></returns>
        Public Property Abundance As Double
        Public Property Hits As String()
        ''' <summary>
        ''' Current gene object its function annotation.
        ''' </summary>
        ''' <returns></returns>
        Public Property product As String
        <ScriptIgnore> <XmlIgnore> Public Property location As String
            Get
                Return loci.ToString
            End Get
            Set(value As String)
                loci = LociAPI.TryParse(value)
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function
    End Class
End Namespace
