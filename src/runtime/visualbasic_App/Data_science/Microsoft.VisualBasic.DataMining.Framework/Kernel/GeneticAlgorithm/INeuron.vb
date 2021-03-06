﻿#Region "Microsoft.VisualBasic::b408509cf2332ab4172cad5fd961b300, ..\visualbasic_App\Data_science\Microsoft.VisualBasic.DataMining.Framework\Kernel\GeneticAlgorithm\INeuron.vb"

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

Namespace Kernel.GeneticAlgorithm
    ''' <summary>
    ''' This interface represents everything we need from a neuron in this
    ''' Genetic Algorithm sample. We need it to execute and give a result
    ''' for an input, we need it to be capable of reproducing and, for
    ''' performance reasons, we want it to say how complex it is. The
    ''' more complex cells lose points when analyzing how good it is the
    ''' result generated by them (if they didn't found the result... in that
    ''' case, they are the winners independently on how complex they are).
    ''' </summary>
    Public Interface INeuron
        ReadOnly Property Complexity() As Integer

        Function Execute(input As Integer) As System.Nullable(Of Integer)

        Function Reproduce() As INeuron
    End Interface
End Namespace
