﻿#Region "Microsoft.VisualBasic::19c2ca3f553b6ebfd29a0be99a20ba0c, ..\visualbasic_App\Microsoft.VisualBasic.Architecture.Framework\Extensions\Math\Trigonometric\Trigonometric2.vb"

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

Namespace Mathematical

    Partial Module Trigonometric

        ''' <summary>
        ''' Secant（正割） ``Sec(X) = 1 / Cos(X)`` 
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns></returns>
        Public Function Sec(x As Double) As Double
            Return 1 / Math.Cos(x)
        End Function

        ''' <summary>
        ''' Cosecant（余割） ``Cosec(X) = 1 / Sin(X)``
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns></returns>
        Public Function Cosec(x As Double) As Double
            Return 1 / Math.Sin(x)
        End Function

        ''' <summary>
        ''' Cotangent（余切） ``Cotan(X) = 1 / Tan(X)``
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns></returns>
        Public Function Cotan(x As Double) As Double
            Return 1 / Math.Tan(x)
        End Function

        ''' <summary>
        ''' Inverse Sine（反正弦）  ``Arcsin(X) = Atn(X / Sqr(-X * X + 1))``
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns></returns>
        Public Function Arcsin(x As Double) As Double
            Return Atn(x / Math.Sqrt(-x * x + 1))
        End Function

        ''' <summary>
        ''' Inverse Cosine（反余弦）  ``Arccos(X) = Atn(-X / Sqr(-X * X + 1)) + 2 * Atn(1)``
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns></returns>
        Public Function Arccos(x As Double) As Double
            Return Atn(-x / Math.Sqrt(-x * x + 1)) + 2 * Atn(1)
        End Function

        ''' <summary>
        ''' Inverse Secant（反正割）  ``Arcsec(X) = Atn(X / Sqr(X * X - 1)) + Sgn((X) - 1) * (2 * Atn(1))``
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns></returns>
        Public Function Arcsec(x As Double) As Double
            Return Atn(x / Math.Sqrt(x * x - 1)) + Math.Sign((x) - 1) * (2 * Atn(1))
        End Function

        ''' <summary>
        ''' Inverse Cosecant（反余割）  ``Arccosec(X) = Atn(X / Sqr(X * X - 1)) + (Sgn(X) - 1) * (2 * Atn(1))`` 
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns></returns>
        Public Function Arccosec(x As Double) As Double
            Return Atn(x / Math.Sqrt(x * x - 1)) + (Math.Sign(x) - 1) * (2 * Atn(1))
        End Function

        ''' <summary>
        ''' Inverse Cotangent（反余切）  ``Arccotan(X) = Atn(X) + 2 * Atn(1)``
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns></returns>
        Public Function Arccotan(x As Double) As Double
            Return Atn(x) + 2 * Atn(1)
        End Function

        ''' <summary>
        ''' Hyperbolic Sine（双曲正弦）  ``HSin(X) = (Exp(X) - Exp(-X)) / 2``  
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns></returns>
        Public Function HSin(x As Double) As Double
            Return (Math.Exp(x) - Math.Exp(-x)) / 2
        End Function

        ''' <summary>
        ''' Hyperbolic Cosine（双曲余弦）  ``HCos(X) = (Exp(X) + Exp(-X)) / 2`` 
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns></returns>
        Public Function HCos(x As Double) As Double
            Return (Math.Exp(x) + Math.Exp(-x)) / 2
        End Function

        ''' <summary>
        ''' Hyperbolic Tangent（双曲正切）  ``HTan(X) = (Exp(X) - Exp(-X)) / (Exp(X) + Exp(-X))`` 
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns></returns>
        Public Function HTan(x As Double) As Double
            Return (Math.Exp(x) - Math.Exp(-x)) / (Math.Exp(x) + Math.Exp(-x))
        End Function

        ''' <summary>
        ''' Hyperbolic Secant（双曲正割）  ``HSec(X) = 2 / (Exp(X) + Exp(-X))`` 
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns></returns>
        Public Function HSec(x As Double) As Double
            Return 2 / (Math.Exp(x) + Math.Exp(-x))
        End Function

        ''' <summary>
        ''' Hyperbolic Cosecant（双曲余割） ``HCosec(X) = 2 / (Exp(X) - Exp(-X))`` 
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns></returns>
        Public Function HCosec(x As Double) As Double
            Return 2 / (Math.Exp(x) - Math.Exp(-x))
        End Function

        ''' <summary>
        ''' Hyperbolic Cotangent（双曲余切） ``HCotan(X) = (Exp(X) + Exp(-X)) / (Exp(X) - Exp(-X))`` 
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns></returns>
        Public Function HCotan(x As Double) As Double
            Return (Math.Exp(x) + Math.Exp(-x)) / (Math.Exp(x) - Math.Exp(-x))
        End Function

        ''' <summary>
        ''' Inverse Hyperbolic Sine（反双曲正弦） ``HArcsin(X) = Log(X + Sqr(X * X + 1))`` 
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns></returns>
        Public Function HArcsin(x As Double) As Double
            Return Math.Log(x + Math.Sqrt(x * x + 1))
        End Function

        ''' <summary>
        ''' Inverse Hyperbolic Cosine（反双曲余弦） ``HArccos(X) = Log(X + Sqr(X * X - 1))`` 
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns></returns>
        Public Function HArccos(x As Double) As Double
            Return Math.Log(x + Math.Sqrt(x * x - 1))
        End Function

        ''' <summary>
        ''' Inverse Hyperbolic Tangent（反双曲正切） ``HArctan(X) = Log((1 + X) / (1 - X)) / 2`` 
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns></returns>
        Public Function Harctan(x As Double) As Double
            Return Math.Log((1 + x) / (1 - x)) / 2
        End Function

        ''' <summary>
        ''' Inverse Hyperbolic Secant（反双曲正割） ``HArcsec(X) = Log((Sqr(-X * X + 1) + 1) / X)`` 
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns></returns>
        Public Function HArcsec(x As Double) As Double
            Return Math.Log((Math.Sqrt(-x * x + 1) + 1) / x)
        End Function

        ''' <summary>
        ''' Inverse Hyperbolic Cosecant（反双曲余割） ``HArccosec(X) = Log((Sgn(X) * Sqr(X * X + 1) + 1) / X)`` 
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns></returns>
        Public Function HArccosec(x As Double) As Double
            Return Math.Log((Math.Sign(x) * Math.Sqrt(x * x + 1) + 1) / x)
        End Function

        ''' <summary>
        ''' Inverse Hyperbolic Cotangent（反双曲余切）  ``HArccotan(X) = Log((X + 1) / (X - 1)) / 2`` 
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns></returns>
        Public Function HArccotan(x As Double) As Double
            Return Math.Log((x + 1) / (x - 1)) / 2
        End Function
    End Module
End Namespace
