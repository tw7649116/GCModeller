﻿#Region "Microsoft.VisualBasic::ff0af78f9298ab073a8a91aeab651f06, ..\GCModeller\core\Bio.Assembly\Assembly\KEGG\DBGET\Objects\bGetObject.vb"

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

Imports Microsoft.VisualBasic.Language

Namespace Assembly.KEGG.DBGET.bGetObject

    ''' <summary>
    ''' dbget-bin/www_bget
    ''' </summary>
    Public MustInherit Class bGetObject : Inherits ClassObject

        Public MustOverride ReadOnly Property Code As String

        Public Property Entry As String
        Public Property Name As String
        Public Property Definition As String

    End Class
End Namespace
