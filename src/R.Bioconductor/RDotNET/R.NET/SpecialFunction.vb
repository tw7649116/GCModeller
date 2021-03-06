﻿#Region "Microsoft.VisualBasic::5fd270fc00080abf7bf2bf819a626597, ..\R.Bioconductor\RDotNET\R.NET\SpecialFunction.vb"

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

Imports System.Collections.Generic

''' <summary>
''' A special function.
''' </summary>
Public Class SpecialFunction
	Inherits [Function]
	''' <summary>
	''' Creates a special function proxy.
	''' </summary>
	''' <param name="engine">The engine.</param>
	''' <param name="pointer">The pointer.</param>
	Protected Friend Sub New(engine As REngine, pointer As IntPtr)
		MyBase.New(engine, pointer)
	End Sub

	''' <summary>
	''' Invoke this special function, using an ordered list of unnamed arguments.
	''' </summary>
	''' <param name="args">The arguments of the function</param>
	''' <returns>The result of the evaluation</returns>
	Public Overrides Function Invoke(ParamArray args As SymbolicExpression()) As SymbolicExpression
		Return InvokeOrderedArguments(args)
	End Function

	' Invoke this special function, using a list of named arguments in the form of a dictionary
	''' <summary>
	''' NotSupportedException
	''' </summary>
	''' <param name="args">key-value pairs</param>
	''' <returns>Always throws an exception</returns>
	Public Overrides Function Invoke(args As IDictionary(Of String, SymbolicExpression)) As SymbolicExpression
		Throw New NotSupportedException()
	End Function
End Class
