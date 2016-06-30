Imports System.Collections
Imports System.Text
Imports LANS.SystemsBiology.SequenceModel.NucleotideModels
Imports Microsoft.VisualBasic

Namespace SearchingModel

    ''' <summary>
    ''' Clustered Regularly Interspersed Short Palindromic Repeats
    ''' 
    ''' These loci consist of direct repeats of around 22-40 bp in length, in between which
    ''' are spacer sequences derived from foreign DNA. These spacer sequences are used to
    ''' recognize complementary invading DNA so that the CRISPR can target and subsequently 
    ''' remove the threat. It has been well documented that CRISPRs evolve rapidly in response 
    ''' to their environment, leading to a wide variation of spacer sequences.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CRISPR

        Dim __sequenceData As NucleicAcid
        Dim _Repeats As New List(Of Integer)

        Public Sub New(nt As NucleicAcid, positions As List(Of Integer), length As Integer)
            _Repeats = positions
            _RepeatLength = length
            __sequenceData = nt
        End Sub

        Sub New()
        End Sub

        Public Property RepeatLength As Integer

        ''' <summary>
        ''' �ظ�Ƭ�ε���ʼλ���б�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Repeats As Integer()
            Get
                Return _Repeats.ToArray
            End Get
        End Property

        Public Function RepeatSpacing(pos1 As Integer, pos2 As Integer) As Integer
            Return Math.Abs(RepeatAt(pos2) - RepeatAt(pos1))
        End Function

        Public Sub AddRepeatData(val As Integer)
            Call _Repeats.Add(New System.Nullable(Of Integer)(val))
        End Sub

        Public Sub InsertRepeatAt(val As Integer, pos As Integer)
            Call _Repeats.Insert(pos, val)
        End Sub

        Public Sub SetRepeatAt(val As Integer, pos As Integer)
            _Repeats(pos) = val
        End Sub

        Public Sub RemoveRepeat(val As Integer)
            Call _Repeats.Remove(val)
        End Sub

        Public Function RepeatAt(i As Integer) As Integer
            Return _Repeats(i)
        End Function

        Public ReadOnly Property StartLeft As Integer
            Get
                Return _Repeats(0)
            End Get
        End Property

        Public Function [End]() As Integer
            Dim lastRepeatBegin As Integer = _Repeats(_Repeats.Count - 1)
            Return lastRepeatBegin + _RepeatLength - 1
        End Function

        Public ReadOnly Property FirstRepeat As Integer
            Get
                Return _Repeats(0)
            End Get
        End Property

        Public ReadOnly Property LastRepeat As Integer
            Get
                Return _Repeats(_Repeats.Count - 1)
            End Get
        End Property

        Public ReadOnly Property NumberOfRepeats As Integer
            Get
                Return _Repeats.Count
            End Get
        End Property

        Public ReadOnly Property NumberOfSpacers As Integer
            Get
                Return NumberOfRepeats() - 1
            End Get
        End Property

        ''' <summary>
        ''' CRISPRλ����ظ�Ƭ��
        ''' </summary>
        ''' <param name="i"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function RepeatStringAt(i As Integer) As String
            Dim currRepeatStartIndex As Integer = _Repeats(i)
            Dim currRepeatEndIndex As Integer = currRepeatStartIndex + _RepeatLength - 1

            Return __sequenceData.ReadSegment(currRepeatStartIndex, currRepeatEndIndex + 1 - currRepeatStartIndex)
        End Function

        Public Function SpacingAt(i As Integer) As Integer
            Dim currRepeatEndIndex As Integer = _Repeats(i) + _RepeatLength - 1
            Dim nextRepeatStartIndex As Integer = _Repeats(i + 1)
            Dim currSpacerStartIndex As Integer = currRepeatEndIndex + 1

            Return currSpacerStartIndex
        End Function

        ''' <summary>
        ''' CRISPRλ��֮�е��ظ�Ƭ��֮��ļ�����У���Щ������ж���������Դ������
        ''' </summary>
        ''' <param name="i"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SpacerStringAt(i As Integer) As String
            Dim currRepeatEndIndex As Integer = _Repeats(i) + _RepeatLength - 1
            Dim nextRepeatStartIndex As Integer = _Repeats(i + 1)
            Dim currSpacerStartIndex As Integer = currRepeatEndIndex + 1
            Dim currSpacerEndIndex As Integer = nextRepeatStartIndex - 1

            Return __sequenceData.ReadSegment(currSpacerStartIndex, currSpacerEndIndex + 1 - currSpacerStartIndex)
        End Function

        Public Function AverageSpacerLength() As Integer
            Dim Sum As Integer = 0

            For i As Integer = 0 To NumberOfSpacers - 1
                Sum = Sum + SpacerStringAt(i).Length
            Next

            Return Sum \ NumberOfSpacers
        End Function

        Public Function AverageRepeatLength() As Integer
            Dim Sum As Integer = 0

            For i As Integer = 0 To NumberOfRepeats() - 1
                Sum = Sum + RepeatStringAt(i).Length
            Next

            Return Sum \ NumberOfRepeats()
        End Function

        Public Overloads Function ToString() As String
            Dim sBuilder As StringBuilder = New StringBuilder(1024)

            Call sBuilder.AppendLine("POSITION" & vbTab & "REPEAT" & vbTab & vbTab & vbTab & vbTab & "SPACER")
            Call sBuilder.Append("--------" & vbTab)
            Call sBuilder.Append(New String("-"c, Me.RepeatLength) & vbTab)
            Call sBuilder.AppendLine(New String("-"c, Me.AverageSpacerLength))

            'add 1 to each position, to offset programming languagues that begin at 0 rather than 1
            For i As Integer = 0 To NumberOfRepeats - 1

                Call sBuilder.Append((RepeatAt(i) + 1) & vbTab & vbTab & RepeatStringAt(i) & vbTab)

                ' print spacer
                ' because there are no spacers after the last repeat, we stop early (m < crisprIndexVector.size() - 1)
                If i < NumberOfSpacers() Then
                    Dim Spacer As String = SpacerStringAt(i)

                    Call sBuilder.Append(Spacer)
                    Call sBuilder.AppendLine(vbTab & "[ " & RepeatStringAt(i).Length & ", " & SpacerStringAt(i).Length & " ]")
                End If
            Next

            Call sBuilder.Append(vbLf & "--------" & vbTab)
            Call sBuilder.Append(New String("-"c, Me.RepeatLength) & vbTab)
            Call sBuilder.AppendLine(New String("-"c, Me.AverageSpacerLength))

            Return sBuilder.ToString
        End Function
    End Class
End Namespace