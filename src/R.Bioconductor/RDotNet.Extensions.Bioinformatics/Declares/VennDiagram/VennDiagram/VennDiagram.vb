﻿#Region "Microsoft.VisualBasic::06655e0555bfb511800d4f82e2f013a3, ..\R.Bioconductor\RDotNet.Extensions.Bioinformatics\Declares\VennDiagram\VennDiagram\VennDiagram.vb"

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

Imports System.Drawing
Imports System.Text
Imports System.Xml.Serialization
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Linq.Extensions
Imports RDotNET.Extensions.VisualBasic
Imports RDotNET.Extensions.VisualBasic.SymbolBuilder
Imports RDotNET.Extensions.VisualBasic.SymbolBuilder.Abstract

Namespace VennDiagram.ModelAPI

    ''' <summary>
    ''' The data model of the venn diagram.(文氏图的数据模型)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class VennDiagram : Inherits IRScript

        Dim __plot As vennDiagramPlot

        ''' <summary>
        ''' The venn.diagram plot API in R language
        ''' </summary>
        ''' <returns></returns>
        Public Property plot As vennDiagramPlot
            Get
                If __plot Is Nothing Then
                    __plot = New vennDiagramPlot("input_data", "fill_color", "title", "output_image_file")
                End If
                Return __plot
            End Get
            Set(value As vennDiagramPlot)
                __plot = value
            End Set
        End Property

        ''' <summary>
        ''' The title of the diagram.
        ''' </summary>
        ''' <returns></returns>
        <XmlIgnore> Public Property Title As String
            Get
                Return plot.main
            End Get
            Set(value As String)
                plot.main = Rstring(value)
            End Set
        End Property

        ''' <summary>
        ''' vennDiagram tiff file saved path.(所生成的文氏图的保存文件名)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <XmlIgnore> Public Property saveTiff As String
            Get
                Return plot.filename
            End Get
            Set(value As String)
                plot.filename = value
            End Set
        End Property

        ''' <summary>
        ''' Partitions on the venn diagram
        ''' </summary>
        ''' <returns></returns>
        <XmlElement> Public Property partitions As Partition()
            Get
                Return __partitions.Values.ToArray
            End Get
            Set(value As Partition())
                If value Is Nothing Then
                    __partitions = New Dictionary(Of Partition)
                Else
                    __partitions = value.ToDictionary
                End If
            End Set
        End Property

        Dim __partitions As Dictionary(Of Partition)

        ''' <summary>
        ''' The partition names
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property categoryNames As String()
            Get
                Return partitions.ToArray(Function(x) x.DisplName)
            End Get
        End Property

        Sub New()
            Requires = {"VennDiagram"}
        End Sub

        Public Overrides Function ToString() As String
            Return Title
        End Function

        ''' <summary>
        ''' Assign random colors to the venn diagram partitions
        ''' </summary>
        Public Sub RandomColors()
            Dim colors As String() = RSystem.RColors.Shuffles

            For i As Integer = 0 To partitions.Length - 1
                partitions(i).Color = colors(i)
            Next
        End Sub

        ''' <summary>
        ''' Applying the diagram options
        ''' </summary>
        ''' <param name="venn"></param>
        ''' <param name="opts"></param>
        ''' <returns></returns>
        Public Shared Operator +(venn As VennDiagram, opts As IEnumerable(Of String())) As VennDiagram
            For Each opt As SeqValue(Of String()) In opts.SeqIterator
                Dim name As String = opt.obj.First
                Dim part As Partition = venn.__partitions.Find(name)

                If part Is Nothing Then
                    part = venn.partitions(opt.i)
                End If

                Call part.ApplyOptions(opt.obj)
            Next

            Return venn
        End Operator

        Const venn__plots_out As String = NameOf(venn__plots_out)

        ''' <summary>
        ''' Convert the data model as the R script for venn diagram drawing.(将本数据模型对象转换为R脚本)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Overrides Function __R_script() As String
            Dim R As ScriptBuilder = New ScriptBuilder(capacity:=5 * 1024)
            Dim dataList As New List(Of String) ' The list elements for the venn diagram partitions
            Dim color As New List(Of String) ' The partitions color name vector

            For i As Integer = 0 To partitions.Length - 1
                Dim x As Partition = partitions(i)
                Dim objName As String = x.Name.NormalizePathString.Replace(" ", "_")

                R += $"d{i} <- c({x.Vector})"
                color += x.Color
                dataList += $"{objName}=d{i}"

                If Not String.Equals(x.Name, objName) Then
                    Call $"{x.Name} => '{objName}'".__DEBUG_ECHO
                End If
            Next

            plot.categoryNames = c(partitions.ToArray(Function(x) x.DisplName))

            R += $"input_data <- list({dataList.JoinBy(",")})"
            R += $"fill_color <- {c(color.ToArray)}"

            ' Calling the venn.diagram R API
            R += venn__plots_out <= plot.Copy("input_data", "fill_color", plot.categoryNames)

            Return R.ToString
        End Function
    End Class
End Namespace
