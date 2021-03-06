﻿#Region "Microsoft.VisualBasic::e5809880ed9b39b24770a7bb12f8ef44, ..\visualbasic_App\Data_science\Mathematical\Plots\Scatter\Scatter.vb"

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
Imports System.Drawing.Drawing2D
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.Collection.Generic
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.ComponentModel.DataStructures.SlideWindow
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Imaging.Drawing2D.Vector.Shapes
Imports Microsoft.VisualBasic.Imaging.Drawing2D
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Mathematical.BasicR
Imports Microsoft.VisualBasic.Mathematical.diffEq
Imports Microsoft.VisualBasic.Mathematical.Plots
Imports Microsoft.VisualBasic.MIME.Markup.HTML.CSS
Imports Microsoft.VisualBasic.Serialization.JSON

Public Module Scatter

    ''' <summary>
    ''' 绘图函数
    ''' </summary>
    ''' <param name="c"></param>
    ''' <param name="size"></param>
    ''' <param name="margin"></param>
    ''' <param name="bg"></param>
    ''' <returns></returns>
    <Extension>
    Public Function Plot(c As IEnumerable(Of SerialData),
                         Optional size As Size = Nothing,
                         Optional margin As Size = Nothing,
                         Optional bg As String = "white",
                         Optional showGrid As Boolean = True,
                         Optional showLegend As Boolean = True,
                         Optional legendPosition As Point = Nothing,
                         Optional drawLine As Boolean = True,
                         Optional legendBorder As Border = Nothing,
                         Optional fill As Boolean = False,
                         Optional fillPie As Boolean = True) As Bitmap

        Return GraphicsPlots(
            size, margin, bg,
            Sub(ByRef g, grect)
                Dim array As SerialData() = c.ToArray
                Dim mapper As New Scaling(array)

                Call g.DrawAxis(size, margin, mapper, showGrid)

                For Each line As SerialData In mapper.ForEach(size, margin)
                    Dim pts = line.pts.SlideWindows(2)
                    Dim pen As New Pen(color:=line.color, width:=line.width) With {
                        .DashStyle = line.lineType
                    }
                    Dim br As New SolidBrush(line.color)
                    Dim d = line.PointSize
                    Dim r As Single = line.PointSize / 2
                    Dim bottom! = size.Height - margin.Height

                    For Each pt In pts
                        Dim a As PointData = pt.First
                        Dim b As PointData = pt.Last

                        If drawLine Then
                            Call g.DrawLine(pen, a.pt, b.pt)
                        End If
                        If fill Then
                            Dim path As New GraphicsPath
                            Dim ptbr As New PointF(b.pt.X, bottom)
                            Dim ptbl As New PointF(a.pt.X, bottom)

                            path.AddLine(a.pt, b.pt)
                            path.AddLine(b.pt, ptbr)
                            path.AddLine(ptbr, ptbl)
                            path.AddLine(ptbl, a.pt)
                            path.CloseFigure()

                            Call g.FillPath(br, path)
                        End If

                        If fillPie Then
                            Call g.FillPie(br, a.pt.X - r, a.pt.Y - r, d, d, 0, 360)
                            Call g.FillPie(br, b.pt.X - r, b.pt.Y - r, d, d, 0, 360)
                        End If
                    Next

                    If Not line.annotations.IsNullOrEmpty Then
                        Dim raw = array.Where(Function(s) s.title = line.title).First

                        For Each annotation As Annotation In line.annotations
                            Call annotation.Draw(g, mapper, raw, grect)
                        Next
                    End If

                    If showLegend Then
                        Dim legends As Legend() = LinqAPI.Exec(Of Legend) <=
 _
                            From x As SerialData
                            In array
                            Select New Legend With {
                                .color = x.color.RGBExpression,
                                .fontstyle = CSSFont.GetFontStyle(
                                    FontFace.MicrosoftYaHei,
                                    FontStyle.Regular,
                                    30),
                                .style = LegendStyles.Circle,
                                .title = x.title
                            }

                        If legendPosition.IsEmpty Then
                            legendPosition = New Point(CInt(size.Width * 0.8), margin.Height)
                        End If

                        Call g.DrawLegends(legendPosition, legends,,, legendBorder)
                    End If
                Next
            End Sub)
    End Function

    <Extension>
    Public Function Plot(ode As ODE, Optional size As Size = Nothing, Optional margin As Size = Nothing, Optional bg As String = "white") As Bitmap
        Return {ode.FromODE("cyan")}.Plot(size, margin, bg)
    End Function

    <Extension>
    Public Function Plot(ode As ODEsOut,
                         Optional size As Size = Nothing,
                         Optional margin As Size = Nothing,
                         Optional bg As String = "white",
                         Optional ptSize As Single = 30,
                         Optional width As Single = 5) As Bitmap
        Return ode.FromODEs(, ptSize, width).Plot(size, margin, bg)
    End Function

    Public Function Plot(x As Vector,
                         Optional size As Size = Nothing,
                         Optional margin As Size = Nothing,
                         Optional bg As String = "white",
                         Optional ptSize As Single = 15,
                         Optional width As Single = 5,
                         Optional drawLine As Boolean = False) As Bitmap
        Return {FromVector(x,,, ptSize, width)}.Plot(size, margin, bg, True, False, , drawLine)
    End Function

    Public Function FromVector(x As Vector,
                               Optional color As String = "black",
                               Optional dash As DashStyle = DashStyle.Dash,
                               Optional ptSize! = 30,
                               Optional width As Single = 5) As SerialData
        Return New SerialData With {
            .color = color.ToColor,
            .lineType = dash,
            .PointSize = ptSize,
            .title = "Vector Plot",
            .width = width,
            .pts = LinqAPI.Exec(Of PointData) <=
                From o As SeqValue(Of Double)
                In x.SeqIterator
                Select New PointData With {
                    .pt = New PointF(o.i, CSng(o.obj))
                }
                    }
    End Function

    <Extension>
    Public Function FromODE(ode As ODE, color As String,
                            Optional dash As DashStyle = DashStyle.Dash,
                            Optional ptSize As Integer = 30,
                            Optional width As Single = 5) As SerialData

        Return New SerialData With {
            .title = ode.df.ToString,
            .color = color.ToColor,
            .lineType = dash,
            .PointSize = ptSize,
            .width = width,
            .pts = LinqAPI.Exec(Of PointData) <=
                From x As SeqValue(Of Double)
                In ode.x.SeqIterator
                Select New PointData(CSng(x.obj), CSng(ode.y(x.i)))
        }
    End Function

    <Extension>
    Public Function FromODEs(odes As ODEsOut,
                             Optional colors As IEnumerable(Of String) = Nothing,
                             Optional ptSize! = 30,
                             Optional width As Single = 5) As SerialData()
        Dim c As Color() = If(
            colors.IsNullOrEmpty,
            ChartColors.Shuffles,
            colors.ToArray(AddressOf ToColor))
        Return LinqAPI.Exec(Of SerialData) <=
            From y As SeqValue(Of NamedValue(Of Double()))
            In odes.y.Values.SeqIterator
            Select New SerialData With {
                .color = c(y.i),
                .lineType = DashStyle.Solid,
                .PointSize = ptSize,
                .title = y.obj.Name,
                .width = width,
                .pts = odes.x.SeqIterator.ToArray(Function(x) New PointData(CSng(x.obj), CSng(y.obj.x(x.i))))
            }
    End Function
End Module
