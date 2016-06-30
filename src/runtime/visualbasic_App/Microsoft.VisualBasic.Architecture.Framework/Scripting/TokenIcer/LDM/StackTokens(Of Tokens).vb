﻿Imports Microsoft.VisualBasic.Linq

Namespace Scripting.TokenIcer

    ''' <summary>
    ''' 进行栈树解析所必须要的一些基本元素
    ''' </summary>
    ''' <typeparam name="Tokens"></typeparam>
    Public Class StackTokens(Of Tokens)

        ''' <summary>
        ''' Pretend the root tokens as a true node
        ''' </summary>
        ''' <returns></returns>
        Public Property Pretend As Tokens
        ''' <summary>
        ''' 参数的分隔符，一般是逗号
        ''' </summary>
        ''' <returns></returns>
        Public Property ParamDeli As Tokens
        ''' <summary>
        ''' 向下一层堆栈符号，一般是左括号
        ''' </summary>
        ''' <returns></returns>
        Public Property LPair As Tokens
        ''' <summary>
        ''' 向上一层出栈符号，一般是右括号
        ''' </summary>
        ''' <returns></returns>
        Public Property RPair As Tokens
        Public Property WhiteSpace As Tokens

        ''' <summary>
        ''' Tokens equals?
        ''' </summary>
        ''' <param name="a"></param>
        ''' <param name="b"></param>
        ''' <returns></returns>
        Public Overloads Function Equals(a As Tokens, b As Tokens) As Boolean
            Dim flag As Boolean = __equals(a, b)
            Return flag
        End Function

        ReadOnly __equals As Func(Of Tokens, Tokens, Boolean)

        ''' <summary>
        ''' Tokens equals? 
        ''' </summary>
        ''' <param name="equals"></param>
        Sub New(equals As Func(Of Tokens, Tokens, Boolean))
            __equals = equals
        End Sub
    End Class
End Namespace