﻿#Region "Microsoft.VisualBasic::aa154ea02a9d81543276c4957e495a43, ..\GCModeller\core\Bio.Assembly\Assembly\KEGG\WebForm.vb"

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
Imports System.Text
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.ComponentModel
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.HtmlParser
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq.Extensions
Imports SMRUCC.genomics.Assembly.KEGG.DBGET

Namespace Assembly.KEGG.WebServices.InternalWebFormParsers

    ''' <summary>
    ''' KEGG 网页表格的数据解析方法，在Value之中可能会有重复的Key数据出现
    ''' </summary>
    ''' <remarks></remarks>
    Public Class WebForm : Implements IReadOnlyDictionary(Of String, String())
        Implements System.IDisposable

        ''' <summary>
        ''' Entry, {trim_formatted, non-process}
        ''' </summary>
        Dim _strData As SortedDictionary(Of String, NamedValue(Of String)())
        Dim _url As String

        Public ReadOnly Property WebPageTitle As String
        Public Property AllLinksWidget As AllLinksWidget

        Sub New(Url As String)
            Dim PageContent As String = Url.GET.Replace("&nbsp;", " ").Replace("&gt;", ">").Replace("&lt;", "<")
            Dim Tokens As String() = Regex.Split(PageContent, "<th class="".+?"" align="".+?""").Skip(1).ToArray
            Dim tmp As String() = LinqAPI.Exec(Of String) <=
 _
                From strValue As String
                In Tokens
                Let value As String = Regex.Match(strValue, "<nobr>.+?</nobr>.+", RegexOptions.Singleline).Value.Trim
                Where Not String.IsNullOrEmpty(value)
                Select value

            Me._WebPageTitle = PageContent.HTMLtitle
            Me._url = Url
            Me._strData = New SortedDictionary(Of String, NamedValue(Of String)())

            Dim fields = LinqAPI.Exec(Of NamedValue(Of String())) <=
 _
                From strValue As String
                In tmp
                Let Key As String = Regex.Match(strValue, "<nobr>.+?</nobr>").Value
                Let Value As String = RegexReplace(strValue.Replace(Key, ""), WebForm.HtmlFormatControl)
                Select New NamedValue(Of String()) With {
                    .Name = Key.GetValue,
                    .x = {Value.TrimA, strValue}
                }
            Dim allKeys As IEnumerable(Of String) = From item As NamedValue(Of String())
                                                    In fields
                                                    Let KeyValue As String = item.Name
                                                    Select KeyValue
                                                    Distinct
            For Each Key As String In allKeys
                Dim vals As NamedValue(Of String)() = LinqAPI.Exec(Of NamedValue(Of String)) <=
 _
                    From item As NamedValue(Of String())
                    In fields
                    Where String.Equals(Key, item.Name)
                    Select New NamedValue(Of String) With {
                        .Name = item.x(Scan0),
                        .x = item.x(1)
                    }

                Call _strData.Add(Key, vals)
            Next

            AllLinksWidget = InternalWebFormParsers.AllLinksWidget.InternalParser(PageContent)
            Call ParseRefList(PageContent)
        End Sub

        Private Sub ParseRefList(Page As String)
            Dim Tokens As String() = Strings.Split(Page, "<nobr>Reference</nobr></th>").Skip(1).ToArray
            Me.References = bGetObject.Reference.References(Tokens)
        End Sub

        ''' <summary>
        ''' Reference list of this biological object
        ''' </summary>
        ''' <returns></returns>
        Public Property References As bGetObject.Reference()

        Private Shared ReadOnly HtmlFormatControl As String() = New String() {"<td .+?>", "<div .+?>", "</th>|</div>|</td>|</tr>|<tr>|<tbody>|<div>|</tbody>|</table>|<nobr>|</nobr>", "<table .+?>"}

        Protected Friend Shared Function parseList(strValue As String, SplitRegx As String) As KeyValuePair()
            If String.IsNullOrEmpty(strValue) Then
                Return New KeyValuePair() {}
            End If

            Dim componentList As New List(Of KeyValuePair)
            Dim bufs As String() = LinqAPI.Exec(Of String) <=
 _
                From m As Match
                In Regex.Matches(strValue, SplitRegx)
                Select m.Value
                Distinct

            For i As Integer = 0 To bufs.Length - 2
                Dim p1 As Integer = InStr(strValue, bufs(i))
                Dim p2 As Integer = InStr(strValue, bufs(i + 1))
                Dim strTemp As String = Mid(strValue, p1, p2 - p1)

                Dim ComponentEntry As String = Regex.Match(strTemp, SplitRegx).Value
                Dim ComponentDescription As String = strTemp.Replace(ComponentEntry, "").Trim

                ComponentEntry = ComponentEntry.GetValue
                ComponentDescription = WebForm.RemoveHrefLink(ComponentDescription)

                componentList += New KeyValuePair With {
                    .Key = ComponentEntry,
                    .Value = ComponentDescription
                }
            Next

            Dim p As Integer = InStr(strValue, bufs.Last)
            strValue = Mid(strValue, p)
            Dim LastEntry As New KeyValuePair
            LastEntry.Key = Regex.Match(strValue, SplitRegx).Value
            LastEntry.Value = WebForm.RemoveHrefLink(strValue.Replace(LastEntry.Key, "").Trim)
            LastEntry.Key = LastEntry.Key.GetValue

            Call componentList.Add(LastEntry)

            Return componentList.ToArray
        End Function

        ''' <summary>
        ''' 将符合目标规则的字符串替换为空字符串
        ''' </summary>
        ''' <param name="strData"></param>
        ''' <param name="ExprCollection"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function RegexReplace(strData As String, ExprCollection As String()) As String
            For Each strItem As String In ExprCollection
                strData = Regex.Replace(strData, strItem, "")
            Next
            Return strData
        End Function

        ''' <summary>
        ''' 获取某一个字段的数据
        ''' </summary>
        ''' <param name="KeyWord">网页的表格之中的最左端的字段名</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetValue(KeyWord As String) As String()
            If _strData.ContainsKey(KeyWord) Then
                Return _strData.Item(KeyWord).ToArray(Function(x) x.x)
            Else
                Return New String() {""}
            End If
        End Function

        Public Function GetRaw(Keyword As String) As String()
            If _strData.ContainsKey(Keyword) Then
                Return _strData.Item(Keyword).ToArray(Function(x) x.x)
            Else
                Return New String() {""}
            End If
        End Function

        Public Overrides Function ToString() As String
            Return _url
        End Function

        Const HREF As String = "<a href="".+?"">.+?</a>"

        Public Shared Function RemoveHrefLink(strValue As String) As String
            If String.IsNullOrEmpty(strValue) Then
                Return ""
            End If

            Dim sBuilder As New StringBuilder(strValue)
            Dim Links = From m As Match
                        In Regex.Matches(strValue, HREF)
                        Select Original = m.Value,
                            Value = m.Value.GetValue

            For Each LinkItem In Links
                Call sBuilder.Replace(LinkItem.Original, LinkItem.Value)
            Next

            Return sBuilder.ToString
        End Function

#Region "Implements IReadOnlyDictionary(Of String, String)"

        Public Iterator Function GetEnumerator() As IEnumerator(Of KeyValuePair(Of String, String())) Implements IEnumerable(Of KeyValuePair(Of String, String())).GetEnumerator
            For Each Item As KeyValuePair(Of String, NamedValue(Of String)()) In Me._strData
                Yield New KeyValuePair(Of String, String())(Item.Key, Item.Value.ToArray(Function(obj) obj.x))
            Next
        End Function

        Public ReadOnly Property Count As Integer Implements IReadOnlyCollection(Of KeyValuePair(Of String, String())).Count
            Get
                Return _strData.Count
            End Get
        End Property

        Public Function ContainsKey(key As String) As Boolean Implements IReadOnlyDictionary(Of String, String()).ContainsKey
            Return _strData.ContainsKey(key)
        End Function

        Default Public ReadOnly Property Item(key As String) As String() Implements IReadOnlyDictionary(Of String, String()).Item
            Get
                Return GetValue(key)
            End Get
        End Property

        Public ReadOnly Property Keys As IEnumerable(Of String) Implements IReadOnlyDictionary(Of String, String()).Keys
            Get
                Return _strData.Keys.ToArray
            End Get
        End Property

        Public Function TryGetValue(key As String, ByRef value As String()) As Boolean Implements IReadOnlyDictionary(Of String, String()).TryGetValue
            Dim raw As NamedValue(Of String)() = Nothing
            Dim f As Boolean = _strData.TryGetValue(key, raw)
            value = raw.ToArray(Function(obj) obj.x)
            Return f
        End Function

        Public ReadOnly Property Values As IEnumerable(Of String()) Implements IReadOnlyDictionary(Of String, String()).Values
            Get
                Return _strData.Values.ToArray
            End Get
        End Property

        Public Iterator Function GetEnumerator1() As IEnumerator Implements IEnumerable.GetEnumerator
            Yield GetEnumerator()
        End Function

#End Region

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose( disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose( disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class
End Namespace
