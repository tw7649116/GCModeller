﻿#Region "Microsoft.VisualBasic::4e0ccbe00e29372ce695a7c15c16547b, ..\workbench\devenv\Methods.vb"

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

Module Methods

    Public Function SelectFolder() As String
        Using Folder As New System.Windows.Forms.FolderBrowserDialog
            If Folder.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                Return Folder.SelectedPath
            Else
                Return ""
            End If
        End Using
    End Function
End Module

