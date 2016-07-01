﻿#Region "ecd0ffd91b41c2edf66248fea5772d55, ..\Microsoft.VisualBasic.Architecture.Framework\Tools\Network\Tcp\Persistent\Protocols\UserId.vb"

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

Namespace Net.Persistent.Application.Protocols

    Public Class UserId

        Public Property Remote As IPEndPoint
        Public Property uid As Long

        Public Function CreateApp(protocols As PushMessage) As USER
            Return New USER(Remote.IPAddress, Remote.Port, uid, protocols)
        End Function
    End Class
End Namespace
