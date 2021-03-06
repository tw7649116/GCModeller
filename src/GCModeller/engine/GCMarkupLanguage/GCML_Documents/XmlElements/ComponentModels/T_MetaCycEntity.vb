﻿#Region "Microsoft.VisualBasic::9eabc9b17a6cfba6c261533bb657dd40, ..\GCModeller\engine\GCMarkupLanguage\GCML_Documents\XmlElements\ComponentModels\T_MetaCycEntity.vb"

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
Imports SMRUCC.genomics.Assembly.MetaCyc.File.DataFiles
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.GCML_Documents.XmlElements
Imports Microsoft.VisualBasic.ComponentModel.Collection.Generic

Namespace GCML_Documents.ComponentModels

    Public MustInherit Class T_MetaCycEntity(Of T As Slots.Object)
        Implements sIdEnumerable

        <XmlIgnore> Friend BaseType As T

        ''' <summary>
        ''' Current object that define in the MetaCyc database.(当前所定义的MetaCyc数据库对象的唯一标识符)
        ''' </summary>
        ''' <remarks></remarks>
        <XmlAttribute("UniqueId")>
        Public Overridable Property Identifier As String Implements sIdEnumerable.Identifier

        Public Overrides Function ToString() As String
            Return Identifier
        End Function
    End Class

    ''' <summary>
    ''' 目标对象是一种生物过程
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface I_BiologicalProcess_EventHandle
        Function get_Regulators() As SignalTransductions.Regulator()
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="internal_GUID">这个可以是调控的motif位点对象，对于反应对象而言，这个参数似乎是没有用的</param>
        ''' <param name="Regulator"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function _add_Regulator(internal_GUID As String, Regulator As SignalTransductions.Regulator) As Boolean
    End Interface
End Namespace
