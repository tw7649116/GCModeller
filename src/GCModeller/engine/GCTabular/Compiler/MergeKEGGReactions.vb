﻿Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.CommandLine
Imports Microsoft.VisualBasic.DocumentFormat.Csv.Extensions
Imports Microsoft.VisualBasic
Imports LANS.SystemsBiology.Assembly.KEGG.DBGET
Imports LANS.SystemsBiology.Assembly.MetaCyc.Schema

Namespace Compiler.Components

    Public Class MergeKEGGReactions
        Dim _ModelLoader As FileStream.IO.XmlresxLoader, _KEGGReaction As bGetObject.Reaction(), _KEGGCompounds As bGetObject.Compound()
        Dim _EuqationEquals As LANS.SystemsBiology.Assembly.MetaCyc.Schema.EquationEquals
        Dim _EntryViews As EntryViews
        Dim _Carmen As LANS.SystemsBiology.AnalysisTools.CARMEN.Reaction()

        Sub New(ModelLoader As FileStream.IO.XmlresxLoader, KEGGReactionsCsv As String, KEGGCompoundsCsv As String, CARMENCsv As String)
            Me._ModelLoader = ModelLoader
            Me._KEGGReaction = KEGGReactionsCsv.LoadCsv(Of LANS.SystemsBiology.Assembly.KEGG.DBGET.bGetObject.Reaction)(explicit:=False).ToArray
            Me._KEGGCompounds = KEGGCompoundsCsv.LoadCsv(Of LANS.SystemsBiology.Assembly.KEGG.DBGET.bGetObject.Compound)(explicit:=False).ToArray
            Me._EuqationEquals = New EquationEquals(MetaCycCompoundModels:=ModelLoader.MetabolitesModel, CompoundSpecies:=_KEGGCompounds)
            Me._EntryViews = New EntryViews(ModelLoader.MetabolitesModel.Values.ToList)
            Me._Carmen = (From item As LANS.SystemsBiology.AnalysisTools.CARMEN.Reaction
                          In CARMENCsv.LoadCsv(Of LANS.SystemsBiology.AnalysisTools.CARMEN.Reaction)(False).AsParallel
                          Where Not item.lstGene.IsNullOrEmpty
                          Select item).ToArray
        End Sub

        ''' <summary>
        ''' 本操作必须在生成了代谢反应列表之后，并且在完成了对KEGG。Compound 和Sabiork代谢物的添加
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InvokeMethods()
            Dim ReactionList = (From FluxObject In _ModelLoader.MetabolismModel.AsParallel Select FluxObject.CreateMetaCycReactionSchema).ToArray
            Dim InsertedList As List(Of LANS.SystemsBiology.Assembly.KEGG.DBGET.bGetObject.Reaction) = New List(Of LANS.SystemsBiology.Assembly.KEGG.DBGET.bGetObject.Reaction)
            Dim i As Integer = 0

            For Each KEGG_Reaction In _KEGGReaction
                Dim LQuery = (From item In ReactionList.AsParallel Where Me._EuqationEquals.Equals(Expression:=KEGG_Reaction.Equation, MetaCycEquation:=item, Explicit:=False) Select item).ToArray

                If LQuery.IsNullOrEmpty Then '不存在的话则添加
                    Call InsertedList.Add(KEGG_Reaction)
                Else '存在的话则添加KEGG.Reaction的标识号
                    For Each ReactionModel In LQuery
                        Call Console.WriteLine("[KEGG_REACTION] {0} --> [METACYC_REACTION] {1} ................... {2}%", KEGG_Reaction.Entry, ReactionModel.Identifier, 100 * i / _KEGGReaction.Count)
                        Call ReactionModel.GetDBLinks.AddEntry("KEGG.Reaction", KEGG_Reaction.Entry)
                    Next
                End If

                i += 1
            Next

            '对于不存在的过程对象，则需要满足条件才可以被插入到模型数据之中：
            '所有的底物都可以在模型数据中查找得到
            '对于酶促反应还必须要具备相应的酶分子

            Call Console.WriteLine("There is {0} kegg reaction was not matched in the metacyc database!", InsertedList.Count)

            '由于KEGG.Reaction的代谢底物具备有Kegg。Compounds属性，故而查找EntryView即可
            For Each KEGGReaction In InsertedList
                Dim EntryList = KEGGReaction.GetSubstrateCompounds
                Dim LQuery = (From Entry As String In EntryList Let KEGGCompound = _EntryViews.GetByKeggEntry(Entry) Where Not KEGGCompound Is Nothing Select KEGGCompound).ToArray
                If LQuery.Count = EntryList.Count Then '所有的需要的代谢底物都可以查找得到，则接下来判断相应的酶是否存在，这个过程是通过CARMEN的计算数据得来的
                    Dim Enzymes = GetEnzyme(_Carmen, KEGGReaction.Entry)
                    If Not Enzymes.IsNullOrEmpty Then
                        Dim NewModel As FileStream.MetabolismFlux = New FileStream.MetabolismFlux With {.Identifier = KEGGReaction.Entry, .UPPER_Bound = 100, .Enzymes = Enzymes, .CommonName = KEGGReaction.Comments}
                        NewModel.Equation = KEGGReaction.Equation
                        NewModel.LOWER_Bound = If(KEGGReaction.Reversible, -100, 0)
                        Call _ModelLoader.MetabolismModel.Add(NewModel)
                        Call Console.WriteLine("{0}:  {1} was inserted!", NewModel.Identifier, NewModel.Equation)
                    End If
                End If
            Next
        End Sub

        Private Shared Function GetEnzyme(CARMEN As LANS.SystemsBiology.AnalysisTools.CARMEN.Reaction(), KEGGReaction As String) As String()
            Dim LQuery = (From item In CARMEN Where String.Equals(item.rnId, KEGGReaction, StringComparison.OrdinalIgnoreCase) Select item).ToArray
            If LQuery.IsNullOrEmpty Then
                Return New String() {}
            Else
                Return LQuery.First.lstGene
            End If
        End Function
    End Class
End Namespace