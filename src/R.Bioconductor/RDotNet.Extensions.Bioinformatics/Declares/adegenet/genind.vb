﻿Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.SchemaMaps
Imports Microsoft.VisualBasic.Language
Imports RDotNet.Extensions.VisualBasic
Imports RDotNet.Extensions.VisualBasic.API.base
Imports RDotNet.Extensions.VisualBasic.API.utils

Namespace adegenet

    ''' <summary>
    ''' ###### adegenet formal class (S4) for individual genotypes
    ''' 
    ''' The S4 class genind is used to store individual genotypes.
    ''' It contains several components described In the 'slots' section).
    ''' The summary Of a genind Object invisibly returns a list Of component. 
    ''' The Function .valid.genind Is For internal use. The Function genind 
    ''' creates a genind Object from a valid table Of alleles corresponding 
    ''' To the @tab slot. Note that As In other S4 classes, slots are accessed 
    ''' Using @ instead Of \$.
    ''' </summary>
    Public Class genind : Inherits ClassObject

        ''' <summary>
        ''' matrix integers containing genotypes data for individuals (in rows) for 
        ''' all alleles (in columns). 
        ''' The table differs depending on the @type slot:
        ''' 
        ''' + ``codom``: values are numbers of alleles, summing up to the individuals' ploidies.
        ''' + ``PA``: values are presence/absence of alleles.
        ''' 
        ''' In all cases, rows And columns are given generic names.
        ''' </summary>
        ''' <returns></returns>
        Public Property tab As Double()

        ''' <summary>
        ''' locus factor for the columns of tab
        ''' </summary>
        ''' <returns></returns>
        <Field("loc.fac")> Public Property locFac As Integer()
        ''' <summary>
        ''' integer vector giving the number of alleles per locus
        ''' </summary>
        ''' <returns></returns>
        <Field("loc.n.all")> Public Property locNAll As Integer()
        ''' <summary>
        ''' list having one component per locus, each containing a character vector of alleles names
        ''' </summary>
        ''' <returns></returns>
        <Field("all.names")> Public Property allNames As String()()
        ''' <summary>
        ''' an integer indicating the degree of ploidy of the genotypes. Beware: 2 is not an integer, but as.integer(2) is.
        ''' </summary>
        ''' <returns></returns>
        Public Property ploidy As Integer()
        ''' <summary>
        ''' a character string indicating the type of marker: ``codom`` stands for ``codominant`` 
        ''' (e.g. microstallites, allozymes); ``PA`` stands for ``presence/absence`` (e.g. AFLP).
        ''' </summary>
        ''' <returns></returns>
        Public Property type As String
        ''' <summary>
        ''' the matched call
        ''' </summary>
        ''' <returns></returns>
        Public Property [call] As String()
        ''' <summary>
        ''' (optional) data frame giving levels of population stratification for each individual
        ''' </summary>
        ''' <returns></returns>
        Public Property strata As String()
        ''' <summary>
        ''' (optional) a hierarchical formula defining the hierarchical levels in the @@strata slot.
        ''' </summary>
        ''' <returns></returns>
        Public Property hierarchy As String
        ''' <summary>
        ''' (optional) factor giving the population of each individual
        ''' </summary>
        ''' <returns></returns>
        Public Property pop As Integer()
        ''' <summary>
        ''' (optional) a list containing other information
        ''' </summary>
        ''' <returns></returns>
        Public Property other As Double()()

        ''' <summary>
        ''' ###### Microsatellites genotypes of 237 cats from 17 colonies of Nancy (France)
        ''' 
        ''' This data set gives the genotypes of 237 cats (Felis catus L.) for 9 microsatellites markers. 
        ''' The individuals are divided into 17 colonies whose spatial coordinates are also provided.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' nancycats is a genind object with spatial coordinates of the colonies as a supplementary components (@xy).
        ''' 
        ''' Dominique Pontier (UMR CNRS 5558, University Lyon1, France)
        ''' 
        ''' > Devillard, S.; Jombart, T. &amp; Pontier, D. Disentangling spatial and genetic structure of stray cat (Felis catus L.) colonies in urban habitat using: not all colonies are equal. submitted to Molecular Ecology
        ''' </remarks>
        Public Shared Function nancycats() As genind
            If require("adegenet") AndAlso data("nancycats") IsNot Nothing Then
                Dim obj As genind = "nancycats".ζ.S4Object(Of genind)
                Return obj
            Else
                Throw New Exception(RServer.ToString)
            End If
        End Function
    End Class
End Namespace