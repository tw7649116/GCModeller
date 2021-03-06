﻿#Region "Microsoft.VisualBasic::6c192d4db665a2f346871c99f51eea81, ..\R.Bioconductor\RDotNET\R.NET\Pairlist.vb"

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

Imports RDotNet.Internals
Imports System.Collections
Imports System.Collections.Generic
Imports System.Runtime.InteropServices

''' <summary>
''' A pairlist.
''' </summary>
Public Class Pairlist
	Inherits SymbolicExpression
	Implements IEnumerable(Of Symbol)
	''' <summary>
	''' Creates a pairlist.
	''' </summary>
	''' <param name="engine">The engine</param>
	''' <param name="pointer">The pointer.</param>
	Protected Friend Sub New(engine As REngine, pointer As IntPtr)
		MyBase.New(engine, pointer)
	End Sub

	''' <summary>
	''' Gets the number of nodes.
	''' </summary>
	Public ReadOnly Property Count() As Integer
		Get
			Return Me.GetFunction(Of Rf_length)()(handle)
		End Get
	End Property

	#Region "IEnumerable<Symbol> Members"

	''' <summary>
	''' Gets an enumerator over this pairlist
	''' </summary>
	''' <returns>The enumerator</returns>
    Public Iterator Function GetEnumerator() As IEnumerator(Of Symbol) Implements IEnumerable(Of Symbol).GetEnumerator
        If Count <> 0 Then
            Dim sexp As SEXPREC = GetInternalStructure()
            While sexp.sxpinfo.type <> SymbolicExpressionType.Null
                Yield New Symbol(Engine, sexp.listsxp.tagval)
                sexp = CType(Marshal.PtrToStructure(sexp.listsxp.cdrval, GetType(SEXPREC)), SEXPREC)
            End While
        End If
    End Function

	Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
		Return GetEnumerator()
	End Function

	#End Region
End Class
