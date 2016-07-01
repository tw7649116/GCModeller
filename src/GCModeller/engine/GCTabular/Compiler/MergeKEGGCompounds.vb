﻿Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.CommandLine
Imports Microsoft.VisualBasic.DocumentFormat.Csv.Extensions
Imports Microsoft.VisualBasic

Namespace Compiler.Components

    Public Class MergeKEGGCompounds

        Dim _ModelLoader As FileStream.IO.XmlresxLoader, KEGGCompounds As LANS.SystemsBiology.Assembly.KEGG.DBGET.bGetObject.Compound()
        Dim _EntryViews As EntryViews

        Sub New(Loader As FileStream.IO.XmlresxLoader, KEGGCompoundsCsv As String)
            Me._ModelLoader = Loader
            Me.KEGGCompounds = KEGGCompoundsCsv.LoadCsv(Of LANS.SystemsBiology.Assembly.KEGG.DBGET.bGetObject.Compound)(explicit:=False).ToArray
            Call Console.WriteLine("There is {0} metabolites define in the model", _ModelLoader.MetabolitesModel.Count)
            Me._EntryViews = New EntryViews(Loader.MetabolitesModel.Values.ToList)
        End Sub

        Public Sub InvokeMergeCompoundSpecies()
            Dim UpdateCounts As Integer, Insert As Integer

            For Each item In KEGGCompounds
                If item.GetDBLinkManager Is Nothing OrElse item.GetDBLinkManager.IsEmpty Then
                    Continue For
                End If
                Dim cpd As FileStream.Metabolite = Internal_getCompound(item)
                If cpd Is Nothing Then '没有则进行添加
                    Call _EntryViews.AddEntry(item)
                    Insert += 1
                Else
                    '存在，则尝试更新DBLink属性
                    Call _EntryViews.Update(cpd, item)
                    UpdateCounts += 1
                End If
            Next

            Call Console.WriteLine("Job done, {0} was updated dblinks and {1} was insert into the compiled model!", UpdateCounts, Insert)
        End Sub

        ''' <summary>
        ''' 会自动根据目标对象<paramref name="Compound"></paramref>的DBLink属性来从集合之中获取数据
        ''' </summary>
        ''' <param name="Compound"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function Internal_getCompound(Compound As LANS.SystemsBiology.Assembly.KEGG.DBGET.bGetObject.Compound) As FileStream.Metabolite
            Dim cpd = _EntryViews.GetByCheBIEntry(Compound.CHEBI)
            If Not cpd Is Nothing Then
                Return cpd
            End If
            cpd = _EntryViews.GetByKeggEntry(Compound.Entry)
            If Not cpd Is Nothing Then
                Return cpd
            End If
            cpd = _EntryViews.GetByPubChemEntry(Compound.PUBCHEM)
            Return cpd
        End Function
    End Class
End Namespace