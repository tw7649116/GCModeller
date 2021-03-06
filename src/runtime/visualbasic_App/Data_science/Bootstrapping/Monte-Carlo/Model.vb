﻿#Region "Microsoft.VisualBasic::074c8f697dfd2a0d4a95c4b1ad7aa72d, ..\visualbasic_App\Data_science\Bootstrapping\Monte-Carlo\Model.vb"

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

Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.ComponentModel.TagData
Imports Microsoft.VisualBasic.Emit
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Mathematical
Imports Microsoft.VisualBasic.Mathematical.diffEq

Namespace MonteCarlo

    Public MustInherit Class Model : Inherits ODEs

        ''' <summary>
        ''' 系统的初始值列表
        ''' </summary>
        ''' <returns></returns>
        Public MustOverride Function yinit() As NamedValue(Of INextRandomNumber)()
        ''' <summary>
        ''' 系统的状态列表，即方程里面的参数
        ''' </summary>
        ''' <returns></returns>
        Public MustOverride Function params() As NamedValue(Of INextRandomNumber)()
        Public MustOverride Function eigenvector() As Dictionary(Of String, Eigenvector)

        Protected NotOverridable Overrides Function y0() As var()
            Return {}
        End Function

        Public Function RunTest(estimates As Dictionary(Of String, Double), n%, a%, b%) As ODEsOut
            Dim model As Type = Me.GetType()
            Dim parms$() = ODEs.GetParameters(model).ToArray
            Dim vars$() = ODEs.GetVariables(model).ToArray

            For Each var$ In vars
                Me(var).value = estimates(var)
            Next

            For Each parm$ In parms
                Dim [set] As Action(Of Object, Double) =
                    Delegates.FieldSet(Of Double)(model, parm)
                Call [set](Me, estimates(parm))
            Next

            Return Me.Solve(n, a, b, incept:=True)
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="estimates"></param>
        ''' <param name="n%"></param>
        ''' <param name="a%"></param>
        ''' <param name="b%"></param>
        ''' <param name="modify">修改部分数据</param>
        ''' <returns></returns>
        Public Function RunTest(estimates As Dictionary(Of String, Double)(), n%, a%, b%, Optional modify As Dictionary(Of String, Double) = Nothing) As ODEsOut
            Dim parms As New Dictionary(Of String, Double)

            For Each var$ In estimates(Scan0%).Keys
                Dim dist = estimates.Select(Function(x) x(var$)).Distributes
                Dim most As DoubleTagged(Of Integer) =
                    LinqAPI.DefaultFirst(Of DoubleTagged(Of Integer)) <= From x As DoubleTagged(Of Integer)
                                                                         In dist.Values
                                                                         Select x
                                                                         Order By x.value Descending
                parms(var$) = most.Tag
            Next

            If Not modify.IsNullOrEmpty Then
                For Each var$ In modify.Keys
                    parms(var$) = modify(var$)
                Next
            End If

            Return RunTest(parms, n, a, b)
        End Function
    End Class
End Namespace
