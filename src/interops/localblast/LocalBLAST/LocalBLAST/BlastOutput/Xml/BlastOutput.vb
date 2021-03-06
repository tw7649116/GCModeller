﻿#Region "Microsoft.VisualBasic::635e3417ec3d6d25fddc92b9abe7876d, ..\interops\localblast\LocalBLAST\LocalBLAST\BlastOutput\Xml\BlastOutput.vb"

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
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq.Extensions
Imports Microsoft.VisualBasic.Text
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.Application.BBH
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.BLASTOutput.Views

Namespace LocalBLAST.BLASTOutput.XmlFile

    Public Class Parameters
        <XmlElement("Parameters_expect")> Public Property Expect As String
        <XmlElement("Parameters_sc-match")> Public Property ScMatch As String
        <XmlElement("Parameters_sc-mismatch")> Public Property ScMismatch As String
        <XmlElement("Parameters_gap-open")> Public Property GapOpen As String
        <XmlElement("Parameters_gap-extend")> Public Property GapExtend As String
        <XmlElement("Parameters_filter")> Public Property Filter As String
    End Class

    Public Class BlastOutput : Inherits LocalBLAST.BLASTOutput.IBlastOutput

        <XmlElement("BlastOutput_program")> Public Property Program As String
        <XmlElement("BlastOutput_version")> Public Property Version As String
        <XmlElement("BlastOutput_reference")> Public Property Reference As String
        <XmlElement("BlastOutput_db")> Public Overrides Property Database As String
            Get
                Return MyBase.Database
            End Get
            Set(value As String)
                MyBase.Database = value
            End Set
        End Property
        <XmlElement("BlastOutput_query-ID")> Public Property QueryId As String
        <XmlElement("BlastOutput_query-def")> Public Property QueryDef As String
        <XmlElement("BlastOutput_query-len")> Public Property QueryLen As String
        <XmlArray("BlastOutput_param")> Public Property Param As Parameters()
        <XmlArray("BlastOutput_iterations")> Public Property Iterations As Iteration()

        ''' <summary>
        ''' Trim for uid/locus_tag
        ''' </summary>
        ''' <param name="QueryGrepMethod"></param>
        ''' <param name="HitsGrepMethod"></param>
        ''' <returns></returns>
        Public Overrides Function Grep(QueryGrepMethod As TextGrepMethod, HitsGrepMethod As TextGrepMethod) As IBlastOutput
            If Not QueryGrepMethod Is Nothing Then
                For Each query In Iterations
                    Call query.GrepQuery(QueryGrepMethod)
                Next
            End If
            If Not HitsGrepMethod Is Nothing Then
                For Each query In Iterations
                    Call query.GrepHits(HitsGrepMethod)
                Next
            End If

            Return Me
        End Function

        Public Overrides Function CheckIntegrity(QuerySource As SequenceModel.FASTA.FastaFile) As Boolean
            Throw New NotImplementedException
        End Function

        Public Overrides Function ExportBestHit(Optional coverage As Double = 0.5, Optional identities_cutoff As Double = 0.15) As BestHit()
            Return LinqAPI.Exec(Of BestHit) <= From i As Iteration
                                               In Iterations
                                               Select i.BestHit(identities_cutoff)
        End Function

        Public Overrides Function Save(Optional FilePath As String = "", Optional encoding As Text.Encoding = Nothing) As Boolean
            Dim Xml As String = Me.GetXml

            If encoding Is Nothing Then
                encoding = System.Text.Encoding.UTF8
            End If

            Return Xml.SaveTo(getPath(FilePath), encoding:=encoding)
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="XmlPath">Xml文件的文件路径</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function LoadFromFile(XmlPath As String) As BlastOutput
            Dim BlastOutput As BlastOutput = XmlPath.LoadXml(Of BlastOutput)()
            BlastOutput.FilePath = XmlPath
            Return BlastOutput
        End Function

        Public Overrides Function ExportOverview() As Overview
            Return New Views.Overview With {
                .Queries = Iterations.ToArray(AddressOf __toQuery)
            }
        End Function

        Private Shared Function __toQuery(query As Iteration) As Views.Query
            Dim queryName As String = query.QueryId
            Dim hits = query.Hits.ToArray(Function(x) __toHit(x, query))
            Return New Views.Query With {
                .Id = queryName,
                .Hits = hits.MatrixToVector
            }
        End Function

        Private Shared Function __toHit(hit As XmlFile.Hits.Hit, query As Iteration) As BestHit()
            Dim list = hit.Hsps.ToArray(Function(hsp) New BestHit With {
                .HitName = hit.Id & "| " & hit.Def,
                .hit_length = hit.HitLength,
                .length_hit = hit.Len,
                .identities = hsp.Identity,
                .QueryName = query.QueryId & "| " & query.QueryDef,
                .query_length = query.QueryLen,
                .length_query = hsp.AlignLen,
                .evalue = hsp.Evalue,
                .length_hsp = hsp.AlignLen,
                .Positive = hsp.Positive,
                .Score = hsp.BitScore})
            Return list
        End Function

        Public Overrides Function ExportAllBestHist(Optional coverage As Double = 0.5, Optional identities_cutoff As Double = 0.15) As BestHit()
            Throw New NotImplementedException
        End Function
    End Class
End Namespace
