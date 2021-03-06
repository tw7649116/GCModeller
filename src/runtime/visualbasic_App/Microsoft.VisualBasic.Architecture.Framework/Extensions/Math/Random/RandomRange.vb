﻿Imports Microsoft.VisualBasic.ComponentModel.Ranges
Imports Microsoft.VisualBasic.Linq

Namespace Mathematical

    ''' <summary>
    ''' 针对负数到正数的range随机数，小数位最多精确到1E-4
    ''' </summary>
    Public Module RandomRange

        ''' <summary>
        ''' 返回零表示比较小的常数
        ''' </summary>
        ''' <param name="x"></param>
        ''' <param name="INF"></param>
        ''' <returns></returns>
        Public Function Log(x#, Optional INF% = 5) As Single
            Dim p As Double = Math.Log10(Math.Abs(x))

            If p < -4 Then
                Return p
            End If
            If p > 5 Then
                Return p
            End If

            Return 0
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="from"></param>
        ''' <param name="[to]"></param>
        ''' <param name="INF"></param>
        ''' <param name="forceInit">
        ''' True的时候会通过牺牲性能来强制重新实例化随机数发生器来获取足够的随机
        ''' </param>
        ''' <returns></returns>
        Public Function GetRandom(from#, to#, Optional INF% = 5, Optional forceInit As Boolean = False) As INextRandomNumber
            Dim pf! = Log(from, INF), pt! = Log([to], INF)

            If from > 0 Then
                If [to] > 0 Then ' from 是正数，则to也必须是正数
                    If pf <> 0F Then  ' 如果from不是常数（极大数或者极小数），则整体当做极值数来看待
                        Return AddressOf New PreciseRandom(pf, CSng(Math.Log10([to]))).NextNumber
                    Else ' from 是常数  
                        If pt > 0 Then ' 同样的，当to也是极值数的时候，整体也将被当做极值数来看待
                            Return AddressOf New PreciseRandom(CSng(Math.Log10(from)), pt).NextNumber
                        Else
                            ' to 也是常数
                            Dim range As New DoubleRange(from, [to])

                            If forceInit Then
                                Return Function() New Random(Now.Millisecond).NextDouble(range)  ' 想要通过牺牲性能来强制获取足够的随机
                            Else
                                Dim rnd As New Random
                                Return Function() rnd.NextDouble(range)  ' 假若二者都是常数，则返回常数随机区间
                            End If
                        End If
                    End If
                Else
                    Throw New InvalidConstraintException(
                        $"Can not creates a range as min is positive but max is negative! (from:={from}, to:={[to]})")
                End If
            Else ' from是负数
                If [to] > 0 Then ' to 是正数
                    If pf <> 0F OrElse pt <> 0F Then  ' from是极值数，则整体当做极值数来看待

                        pf = Math.Log10(Math.Abs(from))
                        pt = Math.Log10(Math.Abs([to]))

                        Dim c!() = {0F, pf}
                        Dim rf As New PreciseRandom(c.Min, c.Max)
                        c = {0F, pt}
                        Dim rt As New PreciseRandom(c.Min, c.Max)
                        Dim ppf = Math.Abs(pf) / (Math.Abs(pf) + Math.Abs(pt))

                        If forceInit Then
                            Return Function()
                                       If New Random(Now.Millisecond).NextDouble < ppf Then
                                           Return -1 * rf.NextNumber
                                       Else
                                           Return rt.NextNumber
                                       End If
                                   End Function
                        Else
                            Dim rnd As New Random
                            Return Function()
                                       If rnd.NextDouble < ppf Then
                                           Return -1 * rf.NextNumber
                                       Else
                                           Return rt.NextNumber
                                       End If
                                   End Function
                        End If
                    Else
                        Dim range As New DoubleRange(from, [to])
                        If forceInit Then
                            Return Function() New Random(Now.Millisecond).NextDouble(range)
                        Else
                            Dim rnd As New Random
                            Return Function() rnd.NextDouble(range)
                        End If
                    End If
                Else  ' to 同样也是负数的情况
                    If pf <> 0F OrElse pt <> 0F Then ' 两个都是极值数

                        pf = Math.Log10(Math.Abs(from))
                        pt = Math.Log10(Math.Abs([to]))

                        Dim c = {pf, pt}
                        Dim rnd As New PreciseRandom(c.Min, c.Max)   ' 由于from要小于to
                        Return Function() -1 * rnd.NextNumber
                    Else  ' from 和 to 都是负实数
                        Dim range As New DoubleRange(from, [to])
                        If forceInit Then
                            Return Function() New Random(Now.Millisecond).NextDouble(range)
                        Else
                            Dim rnd As New Random
                            Return Function() rnd.NextDouble(range)
                        End If
                    End If
                End If
            End If
        End Function

        Public Function Testing(from#, to#) As Double()
            Dim rnd As INextRandomNumber = GetRandom(from, [to])
            Dim bufs As New List(Of Double)

            For Each i% In 1000%.Sequence
                bufs += rnd()
            Next

            Return bufs
        End Function
    End Module
End Namespace