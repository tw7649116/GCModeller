﻿#Region "Microsoft.VisualBasic::d53e62f8a316ae215ba10fba3739d984, ..\Bio.Assembly\Assembly\KEGG\Archives\Xml\CompilerAPI.vb"

    ' Author:
    ' 
    '       asuka (amethyst.asuka@gcmodeller.org)
    '       xieguigang (xie.guigang@live.com)
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
Imports System.Xml.Serialization
Imports SMRUCC.genomics.Assembly.KEGG.Archives.Xml.Nodes
Imports SMRUCC.genomics.Assembly.KEGG.DBGET
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.ComponentModel
Imports Microsoft.VisualBasic.Linq.Extensions
Imports Microsoft.VisualBasic.Scripting.MetaData

Namespace Assembly.KEGG.Archives.Xml

    <PackageNamespace("KEGG.XmlModel.Compiler")>
    Public Module CompilerAPI

        <ExportAPI("Compile")>
        Public Function Compile(KEGGPathways As String, KEGGModules As String, KEGGReactions As String, speciesCode As String) As XmlModel
            Dim Pathways As PwyBriteFunc() = CompilePathwayInfo(KEGGPathways)
            Dim Moduless = (From path As String In __getFiles(KEGGModules) Select path.LoadXml(Of bGetObject.Module)()).ToArray
            Dim Reactions = (From path As String In __getFiles(KEGGReactions) Let data = path.LoadXml(Of bGetObject.Reaction)()
                             Where Not data Is Nothing AndAlso Not String.IsNullOrEmpty(data.Entry)
                             Select data).ToArray
            Return Compile(Pathways, Moduless, Reactions, speciesCode)
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="Pathways"></param>
        ''' <param name="Modules"></param>
        ''' <param name="Reactions">数据库之中的所有的代谢反应数据</param>
        ''' <param name="speciesCode"></param>
        ''' <returns></returns>
        ''' 
        <ExportAPI("Compile")>
        Public Function Compile(Pathways As IEnumerable(Of PwyBriteFunc),
                                Modules As IEnumerable(Of bGetObject.Module),
                                Reactions As IEnumerable(Of bGetObject.Reaction),
                                speciesCode As String) As XmlModel
            Dim Model As XmlModel = New XmlModel With {
                .Pathways = Pathways.ToArray,
                .Modules = Modules.ToArray,
                .Metabolome = __getAllReactions(Pathways.ToArray(Function(x) x.Pathways).MatrixAsIterator, Reactions),
                .spCode = speciesCode
            }
            Model.EC_Mappings = EC_Mapping.Generate_ECMappings(Model)
            Return Model
        End Function

        <ExportAPI("Compile")>
        Public Function Compile(Pathways As bGetObject.Pathway(), Modules As bGetObject.Module(), Reactions As bGetObject.Reaction(), speciesCode As String) As XmlModel
            Dim pwyFuncs As PwyBriteFunc() = __compilePathwayInfo(Pathways)
            Return Compile(pwyFuncs, Modules, Reactions, speciesCode)
        End Function

        Private Function __getAllReactions(Modules As IEnumerable(Of bGetObject.Pathway), Reactions As IEnumerable(Of bGetObject.Reaction)) As bGetObject.Reaction()
            Dim source As bGetObject.Reaction() = Reactions.ToArray
            Dim parts = (From bMod As bGetObject.Pathway In Modules Select MapAPI.GetReactions(bMod, source)).MatrixAsIterator
            Dim allCompounds As String() = Modules.ToArray(
                Function(x As bGetObject.Pathway) x.Compound.ToArray(
                Function(cp) cp.Key)).MatrixAsIterator.Distinct.ToArray

            Dim allNoEnzyme = (From x As bGetObject.Reaction
                               In MapAPI.GetReactions(allCompounds, source)
                               Where StringHelpers.IsNullOrEmpty(x.ECNum)
                               Select x)
            Dim bufList As bGetObject.Reaction() = parts.Join(allNoEnzyme).Distinct.ToArray
            Return bufList
        End Function

        Private Function __getFiles(DIR As String) As IEnumerable(Of String)
            Return FileIO.FileSystem.GetFiles(DIR, FileIO.SearchOption.SearchAllSubDirectories, "*.xml")
        End Function

        <ExportAPI("Compile.Pathways")>
        Public Function CompilePathwayInfo(DIR As String) As PwyBriteFunc()
            Dim Pathways = (From path As String In __getFiles(DIR)
                            Select path.LoadXml(Of bGetObject.Pathway)()).ToArray
            Return __compilePathwayInfo(Pathways)
        End Function

        Private Function __compilePathwayInfo(Pathways As bGetObject.Pathway()) As PwyBriteFunc()
            Dim Classify = (From item In BriteHEntry.Pathway.LoadFromResource Select EntryId = item.EntryId, item Group By item.Class Into Group).ToArray
            Dim LQuery = (From [Class] In Classify
                          Let PathwayInfo = (From item In Pathways Where __isClassCat(item.EntryId, [Class].Group) Select item).ToArray
                          Let CategoryGroup = (From item In [Class].Group.ToArray Select EntryId = item.item.Category, item Group By item.item.Category Into Group).ToArray
                          Select [Class], CategoryGroup, PathwayInfo).ToArray
            Dim ChunkBuffer = (From item In LQuery
                               Let Category = ((From Category In item.CategoryGroup
                                                Let CategoryData = (From ci As bGetObject.Pathway
                                                                    In item.PathwayInfo
                                                                    Where __isCat(ci.EntryId, Category.Group)
                                                                    Select C = Category,
                                                                        dataItem = ci).ToArray
                                                Select CategoryData).ToArray).ToArray
                               Select item.Class, Category).ToArray
            Dim Data = (From item
                        In ChunkBuffer
                        Select (From Category In item.Category
                                Where Not Category.IsNullOrEmpty
                                Let CategoryValue As String = Category.First.C.Category
                                Select New PwyBriteFunc With {
                                    .Class = item.Class.Class,
                                    .Category = CategoryValue,
                                    .Pathways = (From cc In Category Select cc.dataItem).ToArray}).ToArray).MatrixToVector
            Return Data
        End Function

        Private Function __isCat(EntryId As String, data As IEnumerable(Of Object)) As Boolean
            Dim LQuery = (From item In data Where InStr(EntryId, item.item.EntryId) > 0 Select item).ToArray
            Return Not LQuery.IsNullOrEmpty
        End Function

        Private Function __isClassCat(EntryId As String, data As IEnumerable(Of Object)) As Boolean
            Dim LQuery = (From item In data Where InStr(EntryId, item.EntryId) > 0 Select item).ToArray
            Return Not LQuery.IsNullOrEmpty
        End Function
    End Module
End Namespace