﻿#Region "Microsoft.VisualBasic::a1026e9d122bbbe377b9a90d6f7a47db, ..\GCModeller\visualize\PDB_molViews\Form_molView.vb"

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

Imports SMRUCC.genomics.Visualize.PDB_canvas

Public Class Form_molView

    Dim WithEvents Canvas As Canvas

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Canvas = New Canvas With {
            .Dock = DockStyle.Fill
        }
        Call Controls.Add(Canvas)
        Canvas.BringToFront()
#If DEBUG Then
        Canvas.LoadModel("G:\GCModeller\GCModeller\Data\pdb_Draw\XC_1184_pdb.txt")
#End If
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Using file As New OpenFileDialog
            If file.ShowDialog = DialogResult.OK Then
                Call Canvas.LoadModel(file.FileName)
            End If
        End Using
    End Sub

    Private Sub AutoRotationToolStripMenuItem_CheckedChanged(sender As Object, e As EventArgs) Handles AutoRotationToolStripMenuItem.CheckedChanged
        If Not Canvas Is Nothing Then
            Canvas.AutoRotate = AutoRotationToolStripMenuItem.Checked
        End If
    End Sub
End Class

