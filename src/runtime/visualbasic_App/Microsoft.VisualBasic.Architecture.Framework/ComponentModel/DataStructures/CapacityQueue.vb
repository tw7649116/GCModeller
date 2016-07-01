﻿#Region "bb1436407883ff1282b5d8c5945018b4, ..\Microsoft.VisualBasic.Architecture.Framework\ComponentModel\DataStructures\CapacityQueue.vb"

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

Imports Microsoft.VisualBasic.Serialization
Imports Microsoft.VisualBasic.Serialization.JSON

Namespace ComponentModel.Collection

    Public Class CapacityQueue(Of T) : Inherits Language.ClassObject
        Implements IEnumerable(Of T)

        ReadOnly _queue As Queue(Of T)

        Public ReadOnly Property Capacity As Integer

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="capacity">The initial number of elements that the System.Collections.Generic.Queue`1 can
        ''' contain.</param>
        Sub New(capacity As Integer)
            _queue = New Queue(Of T)(capacity)
        End Sub

        Public Function Enqueue(x As T) As T
            Dim o As T

            Call _queue.Enqueue(x)

            If _queue.Count = Capacity - 1 Then
                o = _queue.Dequeue()
            Else
                o = _queue.Peek
            End If

            Return o
        End Function

        Public Sub Clear()
            Call _queue.Clear()
        End Sub

        Public Overrides Function ToString() As String
            Return JsonContract.GetJson(Me.ToArray, GetType(T()))
        End Function

        Public Overloads Shared Operator +(q As CapacityQueue(Of T), x As T) As CapacityQueue(Of T)
            Call q.Enqueue(x)
            Return q
        End Operator

        Public Iterator Function GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
            For Each x As T In _queue
                Yield x
            Next
        End Function

        Private Iterator Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Yield GetEnumerator()
        End Function
    End Class
End Namespace
