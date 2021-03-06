﻿#Region "Microsoft.VisualBasic::db04af205fe655d5d9f5f2200e2683fe, ..\visualbasic_App\Data_science\Mathematical\Plots\Histogram.vb"

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
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Imaging.Drawing2D
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Mathematical.diffEq
Imports Microsoft.VisualBasic.Serialization.JSON

Public Module Histogram

    <Extension>
    Public Function Plot(data As HistogramGroup,
                         Optional size As Size = Nothing,
                         Optional margin As Size = Nothing,
                         Optional bg$ = "white",
                         Optional showGrid As Boolean = True,
                         Optional stacked As Boolean = False) As Bitmap

        Return GraphicsPlots(
            size, margin, bg,
            Sub(ByRef g, grect)
                Dim mapper As New Scaling(data, stacked)
                Dim n As Integer = If(
                    stacked,
                    data.Samples.Length,
                    data.Samples.Sum(Function(x) x.data.Length))
                Dim dx As Double =
                    (size.Width - 2 * margin.Width - 2 * margin.Width) / n
                Dim interval As Double = 2 * margin.Width / n
                Dim left As Single = margin.Width
                Dim sy As Func(Of Single, Single) =
                    mapper.YScaler(size, margin)
                Dim bottom = size.Height - margin.Height

                Call g.DrawAxis(size, margin, mapper, showGrid)

                For Each sample As SeqValue(Of HistogramSample) In data.Samples.SeqIterator
                    Dim x = left + interval

                    If stacked Then ' 改变Y
                        Dim right = x + dx
                        Dim top = sy(sample.obj.StackedSum)
                        Dim canvasHeight = size.Height - (margin.Height * 2)

                        For Each val As SeqValue(Of Double) In sample.obj.data.SeqIterator
                            Dim rect As Rectangle = Rectangle(top, x, right, bottom)

                            Call g.FillRectangle(New SolidBrush(data.Serials(val.i).x), rect)

                            top += ((val.obj - mapper.ymin) / mapper.dy) * canvasHeight
                        Next

                        x += dx
                    Else ' 改变X
                        For Each val As SeqValue(Of Double) In sample.obj.data.SeqIterator
                            Dim right = x + dx
                            Dim top = sy(val.obj)
                            Dim rect As Rectangle = Rectangle(top, x, right, size.Height - margin.Height)

                            Call g.DrawRectangle(Pens.Black, rect)
                            Call g.FillRectangle(
                                New SolidBrush(data.Serials(val.i).x),
                                Rectangle(top + 1,
                                          x + 1,
                                          right - 1,
                                          size.Height - margin.Height - 1))
                            x += dx
                        Next
                    End If

                    left = x
                Next
            End Sub)
    End Function

    Public Function Rectangle(top As Single, left As Single, right As Single, bottom As Single) As Rectangle
        Dim pt As New Point(left, top)
        Dim size As New Size(right - left, bottom - top)
        Return New Rectangle(pt, size)
    End Function

    <Extension>
    Public Function FromData(data As IEnumerable(Of Double)) As HistogramGroup
        Return New HistogramGroup With {
            .Serials = {
                New NamedValue(Of Color) With {
                    .Name = "",
                    .x = Color.Lime
                }
            },
            .Samples = LinqAPI.Exec(Of HistogramSample) <=
                From n
                In data.SeqIterator
                Select New HistogramSample With {
                    .data = {n.obj},
                    .Tag = n.i
                }
        }
    End Function

    Public Function FromODE(ParamArray odes As ODE()) As HistogramGroup
        Dim colors = Imaging.ChartColors.Shuffles
        Dim serials = LinqAPI.Exec(Of NamedValue(Of Color)) <=
 _
            From x As SeqValue(Of ODE)
            In odes.SeqIterator
            Select New NamedValue(Of Color) With {
                .Name = x.obj.df.ToString,
                .x = colors(x.i)
            }
        Dim samples = LinqAPI.Exec(Of HistogramSample) <=
 _
            From i As Integer
            In odes.First.y.Sequence
            Select New HistogramSample With {
                .Tag = i,
                .data = odes.ToArray(Function(x) x.y(i))
            }

        Return New HistogramGroup With {
            .Samples = samples,
            .Serials = serials
        }
    End Function
End Module