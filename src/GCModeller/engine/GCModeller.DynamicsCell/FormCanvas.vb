﻿#Region "Microsoft.VisualBasic::e95b77a88d5df10401dbe2b6bdf8b19a, ..\GCModeller\engine\GCModeller.DynamicsCell\FormCanvas.vb"

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

Imports System.ComponentModel
Imports Microsoft.VisualBasic.Data.visualize.Network.Canvas
Imports SMRUCC.genomics.Analysis.SSystem
Imports SMRUCC.genomics.Analysis.SSystem.Script

Public Class FormCanvas

    Dim WithEvents canvas As New Canvas
    Dim engine As Engine

    Private Sub FormCanvas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Controls.Add(canvas)
        canvas.Dock = DockStyle.Fill
        canvas.AutoRotate = False
        canvas.ShowLabel = True
        engine = New Engine(canvas)
    End Sub

    Private Sub LoadPLASScriptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadPLASScriptToolStripMenuItem.Click
        Using open As New OpenFileDialog With {.Filter = "Script text file(*.txt;*.plas)|*.txt;*.plas"}
            If open.ShowDialog = DialogResult.OK Then
                Dim model As Script.Model = Script.ScriptCompiler.Compile(open.FileName)
                engine.RunModel(model)
            End If
        End Using
    End Sub

    Private Sub LoadMetaCycSBMLToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadMetaCycSBMLToolStripMenuItem.Click
        Using open As New OpenFileDialog With {.Filter = "SBML(*.xml;*.sbml)|*.xml;*.sbml"}
            If open.ShowDialog = DialogResult.OK Then
                Dim model As Script.Model = SBML.Compile(open.FileName)
                engine.RunModel(model)
            End If
        End Using
    End Sub

    Private Sub FormCanvas_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        engine.dispose
        canvas.Dispose()
    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        canvas.ViewDistance = TrackBar1.Value
    End Sub
End Class

