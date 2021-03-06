﻿#Region "Microsoft.VisualBasic::13f8c2c85ffbfa04c6be5f11cec8e4e8, ..\visualbasic_App\Microsoft.VisualBasic.Architecture.Framework\Extensions\Collection\Linq\SetValue.vb"

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

Imports System.Reflection
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Serialization.JSON

Namespace Linq

    Public Structure SetValuExtension(Of T)
        Public schema As SetValue(Of T)
        Public obj As T

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="name">Using NameOf</param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Function InvokeSet(name As String, value As Object) As SetValuExtension(Of T)
            Return schema.InvokeSetValue(obj, name, value)
        End Function

        Public Overrides Function ToString() As String
            Return obj.GetJson
        End Function

        Public Shared Narrowing Operator CType(x As SetValuExtension(Of T)) As T
            Return x.obj
        End Operator
    End Structure

    ''' <summary>
    ''' Set value linq expression helper
    ''' </summary>
    Public Class SetValue(Of T)
        Implements IEnumerable(Of PropertyInfo)

        ReadOnly __type As Type = GetType(T)
        ReadOnly __props As SortedDictionary(Of String, PropertyInfo)

        Sub New()
            __props = New SortedDictionary(Of String, PropertyInfo)(
                DataFramework.Schema(Of T)(
                PropertyAccess.Writeable))
        End Sub

        Public Overrides Function ToString() As String
            Return __type.ToString
        End Function

        Public Delegate Function IInvokeSetValue(x As T, value As Object) As T

        ''' <summary>
        ''' Public Delegate Function IInvokeSetValue(x As T, value As Object) As T
        ''' </summary>
        ''' <param name="name">Using NameOf</param>
        ''' <returns></returns>
        Public Function GetSet(name As String) As IInvokeSetValue
            Dim setValue As PropertyInfo = __props(name)
            Return Function(x, v)
                       Call setValue.SetValue(x, v)
                       Return x
                   End Function
        End Function

        ''' <summary>
        ''' <see cref="GetSet"/>
        ''' </summary>
        ''' <param name="setValue"></param>
        ''' <param name="name">Using NameOf</param>
        ''' <returns></returns>
        Public Shared Operator <=(setValue As SetValue(Of T), name As String) As IInvokeSetValue
            Return setValue.GetSet(name)
        End Operator

        Public Shared Operator >=(setValue As SetValue(Of T), name As String) As IInvokeSetValue
            Throw New NotSupportedException
        End Operator

        ''' <summary>
        ''' Assigning the value to the specific named property to the target object.
        ''' (将<paramref name="value"/>参数之中的值赋值给目标对象<paramref name="x"/>之中的指定的<paramref name="name"/>属性名称的属性，
        ''' 如果发生错误，则原有的对象<paramref name="x"/>不会被修改)
        ''' </summary>
        ''' <param name="x"></param>
        ''' <param name="name">Using NameOf.(可以使用NameOf得到需要进行修改的属性名称)</param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Function InvokeSetValue(x As T, name As String, value As Object) As SetValuExtension(Of T)
            If __props.ContainsKey(name) Then
                Dim setValue As PropertyInfo = __props(name)
                Call setValue.SetValue(x, value)
            Else
                Dim lstName As String = String.Join("; ", __props.Keys.ToArray)
                VBDebugger.Warning($"Could Not found the target parameter which is named {name} // {lstName}")
            End If

            Return New SetValuExtension(Of T) With {
                .schema = Me,
                .obj = x
            }
        End Function

        Public Function InvokeSetValue(x As T, value As NamedValue(Of Object)) As T
            Return InvokeSetValue(x, value.Name, value.x)
        End Function

        ''' <summary>
        ''' Assigning the value to the specific named property to the target object.
        ''' (将<paramref name="value"/>参数之中的值赋值给目标对象<paramref name="obj"/>之中的指定的<paramref name="name"/>属性名称的属性，
        ''' 如果发生错误，则原有的对象<paramref name="obj"/>不会被修改)
        ''' </summary>
        ''' <typeparam name="Tvalue"></typeparam>
        ''' <param name="obj"></param>
        ''' <param name="Name">可以使用NameOf得到需要进行修改的属性名称</param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Shared Function InvokeSet(Of Tvalue)(ByRef obj As T, Name As String, value As Tvalue) As T
            Dim type As Type = GetType(T)
            Dim lstProp As PropertyInfo() =
                type.GetProperties(BindingFlags.Public Or BindingFlags.Instance)
            Dim p As PropertyInfo =
                LinqAPI.DefaultFirst(Of PropertyInfo) <=
                    From pInfo As PropertyInfo
                    In lstProp
                    Where String.Equals(Name, pInfo.Name)
                    Select pInfo

            If Not p Is Nothing Then
                Call p.SetValue(obj, value, Nothing)
            Else
                Dim lstName As String =
                    String.Join("; ", LinqAPI.Exec(Of String) <= From pp As PropertyInfo
                                                                 In lstProp
                                                                 Select ss =
                                                                     pp.Name)
                VBDebugger.Warning($"Could Not found the target parameter which is named {Name} // {lstName}")
            End If

            Return obj
        End Function

        Public Iterator Function GetEnumerator() As IEnumerator(Of PropertyInfo) Implements IEnumerable(Of PropertyInfo).GetEnumerator
            For Each x In __props.Values
                Yield x
            Next
        End Function

        Private Iterator Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Yield GetEnumerator()
        End Function
    End Class
End Namespace
