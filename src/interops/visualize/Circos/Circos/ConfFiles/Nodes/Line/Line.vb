﻿#Region "Microsoft.VisualBasic::40514368c9aa8b3cc4c2d602f821d646, ..\interops\visualize\Circos\Circos\ConfFiles\Nodes\Line\Line.vb"

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

'Imports System.Text
'Imports Microsoft.VisualBasic.ComponentModel.Settings
'Imports Microsoft.VisualBasic
'Imports SMRUCC.genomics.Visualize.Circos.Configurations

'Namespace Configurations.Nodes.Plots.Lines

'    Public Class Line : Inherits TracksPlot

'        <Circos> Public Property color As String = "vdgrey"
'        <Circos> Public Property max_gap As String = "1u"

'        Public Property Backgrounds As List(Of Background)
'        Public Property Axes As List(Of Axis)

'        Public Sub New(Data As Karyotype.TrackDataDocument)
'            Call MyBase.New(Data)

'            Me.Axes = New List(Of Axis) From {New Axis}
'            Me.Backgrounds = New List(Of Background) From {New Background}
'        End Sub

'        <Circos> Public Overrides ReadOnly Property type As String
'            Get
'                Return "line"
'            End Get
'        End Property

'        Protected Overrides Function GeneratePlotsElementListChunk() As Dictionary(Of String, List(Of CircosDocument))
'            Dim Dict = MyBase.GeneratePlotsElementListChunk
'            If Dict.IsNullOrEmpty Then
'                Dict = New Dictionary(Of String, List(Of CircosDocument))
'            End If

'            If Not Me.Axes.IsNullOrEmpty Then Call Dict.Add("axes", (From item In Me.Axes Select DirectCast(item, CircosDocument)).ToList)
'            If Not Me.Backgrounds.IsNullOrEmpty Then Call Dict.Add("backgrounds", (From item In Me.Backgrounds Select DirectCast(item, CircosDocument)).ToList)

'            Return Dict
'        End Function

'        Protected Overrides Function GetProperties() As String()
'            Return SimpleConfig.GenerateConfigurations(Of Line)(Me)
'        End Function
'    End Class
'End Namespace
