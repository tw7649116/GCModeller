Namespace Serialization.JSON.Formatter.Internals.Strategies
    Friend NotInheritable Class OpenSquareBracketStrategy
        Implements ICharacterStrategy
        Public Sub Execute(context As JsonFormatterStrategyContext) Implements ICharacterStrategy.Execute
            context.AppendCurrentChar()

            If context.IsProcessingString Then
                Return
            End If

            context.EnterArrayScope()
            context.BuildContextIndents()
        End Sub

        Public ReadOnly Property ForWhichCharacter() As Char Implements ICharacterStrategy.ForWhichCharacter
            Get
                Return "["c
            End Get
        End Property
    End Class
End Namespace