﻿#Region "Microsoft.VisualBasic::124c8a43127bdcc7a8c29b9d7691fa0f, ..\interops\localblast\LocalBLAST\LocalBLAST\BlastOutput\Xml\BireHits.vb"

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

Imports System.Xml.Serialization
Imports Microsoft.VisualBasic.Extensions
Imports Microsoft.VisualBasic.Text

Namespace LocalBLAST.BLASTOutput.XmlFile.Hits

    Public Class Hit

        <XmlElement("Hit_num")> Public Property Num As String
        <XmlElement("Hit_id")> Public Property Id As String
        <XmlElement("Hit_def")> Public Property Def As String
        <XmlElement("Hit_accession")> Public Property Accession As String
        <XmlElement("Hit_len")> Public Property Len As String
        <XmlArray("Hit_hsps")> Public Property Hsps As Hsp()

        ''' <summary>
        ''' 高分区长度与Hit的总长度的比值
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend ReadOnly Property Coverage As Double
            Get
                Dim LQuery = From Hsp As Hsp In Me.Hsps Select Val(Hsp.HitTo) - Val(Hsp.HitFrom) '
                Return LQuery.Sum / Val(Len)
            End Get
        End Property

        Public ReadOnly Property QueryLength As Integer
            Get
                Return (From hsp In Hsps Select Val(hsp.QueryTo) - Val(hsp.QueryFrom)).Sum
            End Get
        End Property

        ''' <summary>
        ''' Hit序列比对上的区域长度，与Len属性不同的是，Len属性指的是Hit序列本身的长度
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property HitLength As Integer
            Get
                Return (From hsp In Hsps Select Val(hsp.HitTo) - Val(hsp.HitFrom)).Sum
            End Get
        End Property

        Protected Friend ReadOnly Property Identities As Double
            Get
                Dim LQuery As Double() = (From Hsp As Hsp In Me.Hsps Select Val(Hsp.Identity)).ToArray
                Return LQuery.Sum / LQuery.Length
            End Get
        End Property

        Protected Friend ReadOnly Property Gaps As Integer
            Get
                Dim LQuery = From Hsp As Hsp In Me.Hsps Select Val(Hsp.Gaps) '
                Return LQuery.Sum
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return String.Format("> {0}|{1}", Id, Def)
        End Function

        Public Function Grep(method As TextGrepMethod) As Integer
            If Not Id.IsNullOrEmpty Then
                Id = method(Id)
            Else
                Id = "Unknown"
            End If
            Return 0
        End Function
    End Class

    Public Class Hsp

        <XmlElement("Hsp_num")> Public Property Num As String
        <XmlElement("Hsp_bit-score")> Public Property BitScore As String
        <XmlElement("Hsp_score")> Public Property Score As String
        <XmlElement("Hsp_evalue")> Public Property Evalue As String
        <XmlElement("Hsp_query-from")> Public Property QueryFrom As String
        <XmlElement("Hsp_query-to")> Public Property QueryTo As String
        <XmlElement("Hsp_hit-from")> Public Property HitFrom As String
        <XmlElement("Hsp_hit-to")> Public Property HitTo As String
        <XmlElement("Hsp_query-frame")> Public Property QueryFrame As String
        <XmlElement("Hsp_hit-frame")> Public Property HitFrame As String
        <XmlElement("Hsp_identity")> Public Property Identity As String
        <XmlElement("Hsp_positive")> Public Property Positive As String
        <XmlElement("Hsp_gaps")> Public Property Gaps As String
        <XmlElement("Hsp_align-len")> Public Property AlignLen As String
        <XmlElement("Hsp_qseq")> Public Property Qseq As String
        <XmlElement("Hsp_hseq")> Public Property Hseq As String
        <XmlElement("Hsp_midline")> Public Property Midline As String

        Public Overrides Function ToString() As String
            Return Midline
        End Function
    End Class

    Public Class Statistics
        <XmlElement("Statistics_db-num")> Public Property DbNum As String
        <XmlElement("Statistics_db-len")> Public Property DbLen As String
        <XmlElement("Statistics_hsp-len")> Public Property HspLen As String
        <XmlElement("Statistics_eff-space")> Public Property EffSpace As String
        <XmlElement("Statistics_kappa")> Public Property Kappa As String
        <XmlElement("Statistics_lambda")> Public Property Lambda As String
        <XmlElement("Statistics_entropy")> Public Property Entropy As String

        Public Overrides Function ToString() As String
            Return String.Format("HspLen:={0}", HspLen)
        End Function
    End Class
End Namespace
