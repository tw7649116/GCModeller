﻿#Region "Microsoft.VisualBasic::2fea8a4f2605f927fe971f334885ceb7, ..\GCModeller\core\Bio.Assembly\Assembly\NCBI\Database\GenBank\TabularFormat\FeatureBriefs\GFF\FeatureParser.vb"

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

Imports System.Text.RegularExpressions
Imports SMRUCC.genomics.Assembly.NCBI.GenBank.TabularFormat.ComponentModels
Imports System.Text
Imports SMRUCC.genomics.ComponentModel.Loci
Imports Microsoft.VisualBasic.ComponentModel.DataStructures
Imports Microsoft.VisualBasic.Language

Namespace Assembly.NCBI.GenBank.TabularFormat

    Public Module FeatureParser

        ''' <summary>
        ''' 生成gff文件之中的一行的基因组特性位点的数据
        ''' </summary>
        ''' <returns></returns>
        Public Function ToString(x As Feature) As String
            Dim attrs As String() = (From Token As KeyValuePair(Of String, String)
                                     In x.attributes
                                     Select $"{Token.Key}={Token.Value.CliPath}").ToArray
            Dim attrsHash As String = String.Join(";", attrs)
            Dim tokens As String() = New String() {
                x.seqname, x.source, x.Feature, CStr(x.start), CStr(x.Ends), x.score, x.Strand.GetBriefCode, x.frame, attrsHash
            }
            Dim line As String = String.Join(vbTab, tokens)
            Return line
        End Function

        ''' <summary>
        ''' ```
        ''' Fields are: &lt;seqname> &lt;source> &lt;feature> &lt;start> &lt;end> &lt;score> &lt;strand> &lt;frame> [attributes] [comments]
        ''' ```
        ''' </summary>
        ''' <param name="s_Data"></param>
        ''' <param name="version">gff1, gff2, gff3之间的差异是由于本属性值的列的读取方式的差异而产生的</param>
        ''' <returns></returns>
        Public Function CreateObject(s_Data As String, version As Integer) As Feature
            Dim Tokens As String() = Strings.Split(s_Data, vbTab)
            Dim Feature As New Feature
            Dim p As New Pointer

            ' Fields are: <seqname> <source> <feature> <start> <end> <score> <strand> <frame> [attributes] [comments]

            With Feature
                .seqname = Tokens(++p)
                .source = Tokens(++p)
                .Feature = Tokens(++p)
                .Left = CLng(Val(Tokens(++p)))
                .Right = CLng(Val(Tokens(++p)))
                .score = Tokens(++p)
                .Strand = GetStrand(Tokens(++p))
                .frame = Tokens(++p)
            End With

            '在这里开始读取可选的列数据
            Dim attrValue As String = If(Tokens.Count > p, Tokens(++p), "")

            If Not String.IsNullOrEmpty(attrValue) Then
                Select Case version
                    Case 1
                    Case 2
                    Case 3 : Feature.attributes = CreateObjectGff3(attrValue)
                    Case Else
                        Call Console.WriteLine($"{NameOf(version)}={version} is currently not supported yet, ignored!")
                End Select
            End If

            Return Feature
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="s_Data"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' gi|66571684|gb|CP000050.1|	RefSeq	Coding gene	42	1370	.	+	.	name=dnaA;product="chromosome replication initiator DnaA"
        ''' </remarks>
        Private Function CreateObjectGff3(s_Data As String) As Dictionary(Of String, String)
            Dim Tokens As String() = attributeTokens(Line:=s_Data)
            Dim LQuery = (From Token As String In Tokens
                          Let p As Integer = InStr(Token, "=")
                          Let Name As String = Mid(Token, 1, p - 1),
                              Value As String = Mid(Token, p + 1)
                          Select Name, Value).ToArray
            Dim attrs = LQuery.ToDictionary(Function(obj) obj.Name.ToLower,   ' Key已经被转换为小写了
                                            Function(obj) If(Len(obj.Value) > 2 AndAlso
                                                            obj.Value.First = """"c AndAlso
                                                            obj.Value.Last = """"c, Mid(obj.Value, 2, Len(obj.Value) - 2), obj.Value))
            Return attrs
        End Function

        ''' <summary>
        ''' A regex expression string that use for split the line text.
        ''' </summary>
        ''' <remarks></remarks>
        Const SplitRegxExpression As String = "[" & vbTab & ";](?=(?:[^""]|""[^""]*"")*$)"

        ''' <summary>
        ''' Row parsing into column tokens
        ''' </summary>
        ''' <param name="Line"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function attributeTokens(Line As String) As String()
            If String.IsNullOrEmpty(Line) Then
                Return Nothing
            End If

            Dim Row = Regex.Split(Line, SplitRegxExpression)
            For i As Integer = 0 To Row.Length - 1
                If Not String.IsNullOrEmpty(Row(i)) Then
                    If Row(i).First = """"c AndAlso Row(i).Last = """"c Then
                        Row(i) = Mid(Row(i), 2, Len(Row(i)) - 2)
                    End If
                End If

            Next
            Return Row
        End Function
    End Module
End Namespace
