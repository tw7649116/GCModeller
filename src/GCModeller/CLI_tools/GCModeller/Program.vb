﻿#Region "Microsoft.VisualBasic::9fd3752e7f397ccb68fd994e9a89aed2, ..\GCModeller\CLI_tools\GCModeller\Program.vb"

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

Imports Microsoft.VisualBasic.CommandLine
Imports Microsoft.VisualBasic.Data.csv

Module Program

    Sub New()
        Dim path$ = App.HOME & "/Templates/repository-query.csv"

        If Not path.FileExists Then
            Call {
                New QueryArgument With {
                    .Name = "Example output file name",
                    .Expression = "Test expression"
                }
            }.SaveTo(path)
        End If
    End Sub

    Public Function Main() As Integer
        Return New Interpreter(GetType(CLI)).Execute(App.CommandLine)
    End Function
End Module
