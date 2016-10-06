﻿Imports System.IO
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Data.IO.SearchEngine
Imports Microsoft.VisualBasic.Text
Imports SMRUCC.genomics.SequenceModel.FASTA

Public Module Extensions

    <Extension>
    Public Iterator Function FastaSearch(source As IEnumerable(Of FastaToken), query$) As IEnumerable(Of FastaToken)
        Dim expression As Expression = query.Build
        Dim type As New IObject(GetType(Text))

        For Each x As FastaToken In source
            If expression.Evaluate(
                type,
                New Text With {
                    .Text = x.Title
                }) Then

                Yield x
            End If
        Next
    End Function

    <Extension>
    Public Function BatchSearch(source As IEnumerable(Of FastaToken), arguments As IEnumerable(Of NamedValue(Of String)), out$) As Boolean
        Dim expressions As New Dictionary(Of Expression, StreamWriter)
        Dim def As New IObject(GetType(Text))

        For Each query In arguments
            Dim path$ = out & $"/{query.Name.NormalizePathString}.fasta"
            expressions.Add(query.x.Build, path.OpenWriter(Encodings.ASCII))
        Next

        For Each fa As FastaToken In source
            Dim title As New Text With {
                .Text = fa.Title
            }

            For Each query In expressions
                If query.Key.Evaluate(def, title) Then
                    Call query.Value.WriteLine(fa.GenerateDocument(120))
                End If
            Next
        Next

        For Each file In expressions.Values
            Call file.Flush()
            Call file.Close()
            Call file.Dispose()
        Next

        Return True
    End Function
End Module
