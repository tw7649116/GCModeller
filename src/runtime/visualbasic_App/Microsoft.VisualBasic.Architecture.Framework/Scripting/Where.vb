﻿#Region "Microsoft.VisualBasic::9397ce883be6708792aa475cf6f88fa6, ..\visualbasic_App\Microsoft.VisualBasic.Architecture.Framework\Scripting\Where.vb"

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

Imports System.Runtime.CompilerServices

Namespace Scripting

    Public Enum Logics
        [And]
        [Or]
        [AndAlso]
        [OrElse]
        [Not]
        [XOr]
    End Enum

    Public Module [Where]

        ''' <summary>
        ''' Not support <see cref="Logics.Not"/>
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="logic"></param>
        ''' <param name="tests"></param>
        ''' <returns></returns>
        Public Function BuildAll(Of T)(logic As Logics, ParamArray tests As Func(Of T, Boolean)()) As Func(Of T, Boolean)
            Select Case logic
                Case Logics.And
                    Return Function(x) x.__and(tests)
                Case Logics.AndAlso
                    Return Function(x) x.__andAlso(tests)
                Case Logics.Or
                    Return Function(x) x.__or(tests)
                Case Logics.OrElse
                    Return Function(x) x.__orElse(tests)
                Case Logics.XOr
                    Return Function(x) Not x.__orElse(tests)
                Case Else
                    Throw New NotSupportedException(logic.ToString)
            End Select
        End Function

        <Extension>
        Private Function __orElse(Of T)(x As T, tests As Func(Of T, Boolean)()) As Boolean
            For Each test As Func(Of T, Boolean) In tests
                If test(x) = True Then
                    Return True
                End If
            Next

            Return False
        End Function

        <Extension>
        Private Function __andAlso(Of T)(x As T, tests As Func(Of T, Boolean)()) As Boolean
            For Each test As Func(Of T, Boolean) In tests
                If test(x) = False Then
                    Return False
                End If
            Next

            Return True
        End Function

        ''' <summary>
        ''' <see cref="Logics.Or"/>, 所有的表达式都会被计算
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="x"></param>
        ''' <param name="tests"></param>
        ''' <returns></returns>
        <Extension>
        Private Function __or(Of T)(x As T, tests As Func(Of T, Boolean)()) As Boolean
            Dim [true] As Boolean = False

            For Each test As Func(Of T, Boolean) In tests
                If test(x) = True Then
                    [true] = True
                End If
            Next

            Return [true]
        End Function

        ''' <summary>
        ''' <see cref="Logics.And"/>, 所有的表达式都会被计算
        ''' </summary>
        ''' <param name="tests"></param>
        ''' <returns></returns>
        <Extension>
        Private Function __and(Of T)(x As T, tests As Func(Of T, Boolean)()) As Boolean
            Dim [false] As Boolean = False

            For Each test As Func(Of T, Boolean) In tests
                If test(x) = False Then
                    [false] = True
                End If
            Next

            If [false] Then
                Return False
            Else
                Return True
            End If
        End Function
    End Module
End Namespace
