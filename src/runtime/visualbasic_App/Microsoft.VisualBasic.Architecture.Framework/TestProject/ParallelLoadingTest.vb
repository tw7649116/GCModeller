﻿#Region "Microsoft.VisualBasic::a464b129e7eaf4962acfb515d1e1e1a9, ..\visualbasic_App\Microsoft.VisualBasic.Architecture.Framework\TestProject\ParallelLoadingTest.vb"

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

<Serializable>
Public Class ParallelLoadingTest

    Public Property ddddd As String
    Public Property fffd As Date

    <Microsoft.VisualBasic.Parallel.ParallelLoading.LoadEntry>
    Public Shared Function Load(path As String) As ParallelLoadingTest()
        Call Threading.Thread.Sleep(10 * 1000)
        Return {New ParallelLoadingTest With {.ddddd = RandomDouble(), .fffd = Now}}
    End Function
End Class
