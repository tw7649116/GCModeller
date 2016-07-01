﻿Imports LANS.SystemsBiology
Imports LANS.SystemsBiology.Assembly.KEGG.DBGET
Imports LANS.SystemsBiology.Assembly.MetaCyc.File.FileSystem
Imports LANS.SystemsBiology.Assembly.MetaCyc.Schema

Public Class DifferTool

    Dim _MetaCyc As DatabaseLoadder
    Dim _KEGG_Compounds As bGetObject.Compound()
    Dim _KEGG_Reactions As bGetObject.Reaction()

    Sub New(MetaCyc As DatabaseLoadder, KEGGCompounds As bGetObject.Compound(), KEGGReactions As bGetObject.Reaction())
        Me._MetaCyc = MetaCyc
        Me._KEGG_Compounds = KEGGCompounds
        Me._KEGG_Reactions = KEGGReactions
    End Sub

    Public Sub Invoke()

    End Sub

    Private Shared Function MergeMetaCycCompounds() As EffectorMap()
        Throw New NotImplementedException
    End Function
End Class