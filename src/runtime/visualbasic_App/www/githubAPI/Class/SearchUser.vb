﻿#Region "Microsoft.VisualBasic::aed9bd31aa3f725d14d835ef38d7e4ad, ..\visualbasic_App\www\githubAPI\Class\SearchUser.vb"

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

Namespace [Class]

    Public Class SearchResult(Of T As Class) : Inherits ClassObject
        Public Property total_count As Integer
        Public Property incomplete_results As Boolean
        Public Property items As T()
    End Class

    Public Class User : Inherits ClassObject
        Public Property score As Double
        Public Property login As String
        Public Property id As String
        Public Property avatar_url As String
        Public Property gravatar_id As String
        Public Property url As String
        Public Property html_url As String
        Public Property followers_url As String
        Public Property following_url As String
        Public Property gists_url As String
        Public Property starred_url As String
        Public Property subscriptions_url As String
        Public Property organizations_url As String
        Public Property repos_url As String
        Public Property events_url As String
        Public Property received_events_url As String
        Public Property type As String
        Public Property site_admin As String
        Public Property name As String
        Public Property company As String
        Public Property blog As String
        Public Property location As String
        Public Property email As String
        Public Property hireable As String
        Public Property bio As String
        Public Property public_repos As String
        Public Property public_gists As String
        Public Property followers As String
        Public Property following As String
        Public Property created_at As String
        Public Property updated_at As String
    End Class
End Namespace
