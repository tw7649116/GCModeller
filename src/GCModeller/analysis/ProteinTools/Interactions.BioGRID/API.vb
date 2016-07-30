﻿#Region "Microsoft.VisualBasic::d4bde139c37f209f0277eef2388e3d31, ..\GCModeller\analysis\ProteinTools\Interactions.BioGRID\API.vb"

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

Imports Microsoft.VisualBasic

''' <summary>
''' 
''' </summary>
Public Module API

    Public Function AllIdentifierTypes(source As IEnumerable(Of IDENTIFIERS)) As String()
        Dim out As New Dictionary(Of String, String)

        For Each x As IDENTIFIERS In source
            If Not out.ContainsKey(x.IDENTIFIER_TYPE) Then
                Call out.Add(x.IDENTIFIER_TYPE, Nothing)
            End If
        Next

        Return out.Keys.ToArray
    End Function
End Module

