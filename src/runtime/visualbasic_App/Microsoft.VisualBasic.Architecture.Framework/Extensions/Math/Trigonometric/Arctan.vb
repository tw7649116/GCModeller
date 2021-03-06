﻿#Region "Microsoft.VisualBasic::6230e452588432b0bbca7cefc08f8b1a, ..\visualbasic_App\Microsoft.VisualBasic.Architecture.Framework\Extensions\Math\Trigonometric\Arctan.vb"

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

Namespace Mathematical

    Partial Module Trigonometric

        Const HalfPI# = Math.PI / 2
        Const n1# = 1.0R
        Const n05# = 0.5R

        ''' <summary>
        ''' 通过这个参数来控制计算精度，这个参数值越大，计算精度越高
        ''' </summary>
        Public AtanPrecise% = 500

        ''' <summary>
        ''' Taylor Atan
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns></returns>
        ''' <remarks>Atan测试没有问题</remarks>>
        Public Function Atn(x#) As Double
            If x# = 1.0 Then
                Return Math.PI / 4
            End If
            If Math.Sign(x) = Math.Sign(-1) Then
                Return -Atn(-x)
            End If
            If x > n1 Then
                Return HalfPI - Atn(1 / x)
            End If
            If x > n05 Then
                Return Atn(n1) + Atn((x - n1) / (1 + x))
            Else
                Dim xPow2 As Double = x * x
                Dim n__1 As Integer = AtanPrecise
                Dim y As Double = 1 / (2 * n__1 + 1)
                Dim i As Integer = AtanPrecise

                While i > 0
                    y = (1 / (2 * n__1 - 1)) - (xPow2 * y)
                    i -= 1
                    n__1 -= 1
                End While

                Return x * y
            End If
        End Function
    End Module
End Namespace
