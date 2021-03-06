'
' * Solution for CookieContainer bug in .NET 3.5
' *
' * Usage:
' * httpWebRequest.Headers[HttpRequestHeader.Cookie] = CookieParser.ParseSetCookie(httpWebResponse.Headers[HttpResponseHeader.SetCookie]);
' *
' * Created by LYF610400210
'


Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace AppEngine

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' https://github.com/LYF610400210/CookieParser
    ''' </remarks>
    Public Module CookieParser

#Region "Cookies解析（为的是解决 .Net 3.5 CookieContainer 的Bug）"

        ''' <summary>
        ''' 把服务器返回的Set-Cookie标头信息翻译成
        ''' <para>能放在Cookie标头上的信息</para>
        ''' </summary>
        ''' <param name="CookieStr">Set-Cookie标头信息</param>
        ''' <returns></returns>
        Public Function ParseSetCookie(CookieStr As String) As String
            If CookieStr.Contains("=") AndAlso CookieStr.Contains(";") Then
                '合法性验证
                Dim oneCookie As String() = CookieStr.Split(";"c)
                Dim returnmsg As String = ""
                Dim onename As String = ""
                For Each one As String In oneCookie
                    Dim ifThereisAComma As String = one
                    If ifThereisAComma.StartsWith(",") Then
                        '修复一个Bug：如果一个标头上只有一个Cookie，则解析出来的相应值之前会多一个逗号。
                        '截掉开头的逗号
                        ifThereisAComma = ifThereisAComma.Substring(1)
                    End If
                    onename = ParseOneNameAndValue(ifThereisAComma)
                    '判断是否有逗号
                    returnmsg &= onename
                Next

                Return returnmsg
            Else
                Return ""
            End If
        End Function

        ''' <summary>
        ''' 从数组中查找指定的值，并返回其Index
        ''' </summary>
        ''' <param name="Value">查找什么？</param>
        ''' <param name="Source">在哪个数组中找？</param>
        ''' <returns>Index或-1</returns>
        Public Function FindIndex(Value As String, Source As String()) As Integer
            For i As Integer = 0 To Source.Length - 1
                If Value = Source(i) Then
                    Return i
                End If
            Next
            Return -1
        End Function

        ''' <summary>
        ''' 判断一个Name=Value的值是不是一个真正的Cookies
        ''' <para>若是两个Cookies，则把两个Cookies用分号隔开；</para>
        ''' <para>不是，则返回输入的NameAndValue值。</para>
        ''' </summary>
        ''' <param name="NameAndValue">Cookies Name=Value</param>
        ''' <returns></returns>
        Public Function ParseOneNameAndValue(NameAndValue As String) As String
            If Not NameAndValue.Contains("=") Then
                Return ""
            End If

            Dim returnmsg As String = ""

            If NameAndValue.Contains(",") Then
                '有逗号
                For Each one As String In NameAndValue.Split(","c)
                    If one.Contains("=") AndAlso (Not NameAndValue.StartsWith(one)) Then
                        '逗号旁有等号且不是第一个，说明逗号是分隔两个Set-Cookie标头的
                        Dim indexArr As String() = NameAndValue.Split(","c)

                        Dim index As Integer = FindIndex(one, indexArr)
                        Dim one_s As String = ""

                        For i As Integer = index To indexArr.Length - 1
                            one_s += indexArr(i)
                        Next
                        '判断后面那一堆字符是不是一个Cookie
                        returnmsg &= ParseOneNameAndValue(one_s)
                    Else
                        '不是标头分隔
                        returnmsg &= isPathDomainOrDate(NameAndValue)
                    End If
                Next
            Else

                '没有逗号，那就是货真价实的一个Cookie。
                returnmsg &= isPathDomainOrDate(NameAndValue)
            End If

            Return returnmsg
        End Function

        ''' <summary>
        ''' 检测Name=Value是不是服务器Set-Cookie标头里的path、domain和过期日期
        ''' <para></para><para>有则返回空字符串，以去掉那些不能放在Cookie标头上的信息</para>
        ''' </summary>
        ''' <param name="input"></param>
        ''' <returns></returns>
        Public Function isPathDomainOrDate(input As String) As String
            input = input.Trim.ToLower

            If input.StartsWith("path=") OrElse
                input.StartsWith("domain=") OrElse
                input.StartsWith("expires=") OrElse
                input.StartsWith("max-age=") OrElse
                input.StartsWith("version=") OrElse
                input.StartsWith("httponly") Then

                '把Path、Domain和过期日期去掉
                Return ""
            Else
                Return input & ";"
            End If
        End Function
#End Region
    End Module
End Namespace
