﻿#Region "Microsoft.VisualBasic::e2d968b4944e92e237821adeda93baf7, ..\GCModeller\core\Bio.Assembly\Assembly\NCBI\Database\GenBank\TabularFormat\FeatureBriefs\PTT\RNT.vb"

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

Namespace Assembly.NCBI.GenBank.TabularFormat

    Public Class RNT : Inherits PTT

        Public Enum RNA_TYPES
            tRNA
            ribosomalRNA
        End Enum
    End Class
End Namespace
