﻿#Region "Microsoft.VisualBasic::bbd85a98a0f90a4fb63f76ff8e5951e9, ..\GCModeller\core\Bio.Assembly\SequenceModel\ISequenceModel.vb"

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

Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Scripting.MetaData

Namespace SequenceModel

    ''' <summary>
    ''' The biological sequence molecular model.(蛋白质序列，核酸序列都可以使用本对象来表示)
    ''' </summary>
    ''' <remarks></remarks>
    Public MustInherit Class ISequenceModel : Inherits ClassObject
        Implements I_PolymerSequenceModel

#Region "Object properties"

        ''' <summary>
        ''' Sequence data in a string type.(字符串类型的序列数据)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overridable Property SequenceData As String Implements I_PolymerSequenceModel.SequenceData

        ''' <summary>
        ''' This sequence is a protein type sequence?(判断这条序列是否为蛋白质序列)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IsProtSource As Boolean
            Get
                Return IsProteinSource(Me)
            End Get
        End Property

        ''' <summary>
        ''' The <see cref="SequenceData"/> string length.
        ''' </summary>
        ''' <returns></returns>
        Public Overridable ReadOnly Property Length As Integer
            Get
                Return Len(SequenceData)
            End Get
        End Property
#End Region

#Region "Constants"

        ''' <summary>
        ''' Enumerate all of the amino acid.(字符串常量枚举所有的氨基酸分子)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const AA_CHARS_ALL As String = "BDEFHIJKLMNOPQRSVWXYZ"
        ''' <summary>
        ''' Enumerate all of the nucleotides.(字符串常量枚举所有的核苷酸分子类型)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NA_CHARS_ALL As String = "ATGCU"
#End Region

#Region "Common shared functions"

        ''' <summary>
        ''' Get the composition vector for a sequence model using a specific composition description.
        ''' </summary>
        ''' <param name="SequenceModel"></param>
        ''' <param name="compositions">This always should be the constant string of <see cref="ISequenceModel.AA_CHARS_ALL">amino acid</see>
        ''' or <see cref="ISequenceModel.NA_CHARS_ALL">nucleotide</see>.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Get_CompositionVector(SequenceModel As I_PolymerSequenceModel, compositions As Char()) As Integer()
            Dim CompositionVector As Integer() = New Integer(compositions.Length - 1) {}
            Dim SequenceData As Char() = SequenceModel.SequenceData.ToUpper

            For i As Integer = 0 To compositions.Length - 1
                Dim ch As Char = compositions(i)
                Dim LQuery = (From c In SequenceData Where ch = c Select 1).Count
                CompositionVector(i) = LQuery
            Next

            Return CompositionVector
        End Function

        ''' <summary>
        ''' 目标序列数据是否为一条蛋白质序列
        ''' </summary>
        ''' <param name="SequenceData"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function IsProteinSource(SequenceData As I_PolymerSequenceModel) As Boolean
            Dim LQuery = (From c As Char
                          In SequenceData.SequenceData.ToUpper
                          Where c <> "N"c AndAlso AA_CHARS_ALL.IndexOf(c) > -1
                          Select 100).FirstOrDefault   '
            Return LQuery > 10
        End Function
#End Region
    End Class
End Namespace
