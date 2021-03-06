﻿#Region "Microsoft.VisualBasic::f1e638f9c6be63232d3a290f824f9c2e, ..\Settings\Shared\Settings.Configuration\Config\Programs\GCHOST.vb"

    ' Author:
    ' 
    '       asuka (amethyst.asuka@gcmodeller.org)
    '       xieguigang (xie.guigang@live.com)
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

Imports Microsoft.VisualBasic.ComponentModel.Settings

Namespace Settings.Programs

    Public Class GCHOST

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <ProfileItem(Name:=".net_sdk", Description:="")> Public Property SDK As String
        <ProfileItem(Name:="db_root")> Public Property DBRoot As String
        <ProfileItem(Name:="db_root_password")> Public Property DBRootPwd As String

    End Class
End Namespace


