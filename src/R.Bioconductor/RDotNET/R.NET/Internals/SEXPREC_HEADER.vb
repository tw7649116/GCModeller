﻿#Region "Microsoft.VisualBasic::897d127bc91f99efe699af51a1921038, ..\R.Bioconductor\RDotNET\R.NET\Internals\SEXPREC_HEADER.vb"

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

Imports System.Runtime.InteropServices

Namespace Internals
	<StructLayout(LayoutKind.Sequential)> _
	Friend Structure SEXPREC_HEADER
		Public sxpinfo As sxpinfo
		Public attrib As IntPtr
		Public gengc_next_node As IntPtr
		Public gengc_prev_node As IntPtr
	End Structure
End Namespace
