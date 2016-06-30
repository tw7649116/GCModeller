﻿Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml.Serialization
Imports LANS.SystemsBiology.DatabaseServices.Reactome.OwlDocument.Abstract
Imports Microsoft.VisualBasic.ComponentModel.Collection.Generic

Namespace OwlDocument.Nodes

    Public Class BiochemicalReaction : Inherits Node
        Public Property conversionDirection As String
        <XmlElement> Public Property participantStoichiometry As RDFresource()
        <XmlElement> Public Property left As RDFresource()
        <XmlElement> Public Property right As RDFresource()
        Public Property eCNumber As String
        <XmlElement> Public Property displayName As String()
    End Class

    Public Class SmallMolecule : Inherits Node
        Public Property displayName As String
        Public Property name As String
        Public Property cellularLocation As RDFresource
        Public Property entityReference As RDFresource
    End Class
    Public Class CellularLocationVocabulary : Inherits ResourceElement
        Public Property term As String
        <XmlElement> Public Property xref As RDFresource()
    End Class

    Public Class SmallMoleculeReference : Inherits ResourceElement
        <XmlElement> Public Property name As String()
        <XmlElement> Public Property xref As RDFresource()
    End Class

    Public Class Provenance : Inherits ResourceElement
        Public Property name As String
        Public Property comment As String
    End Class

    Public Class Catalysis : Inherits Node
        Public Property controller As RDFresource
        Public Property controlled As RDFresource
        Public Property controlType As String
    End Class
    Public Class Protein : Inherits Node
        Public Property displayName As String
        <XmlElement> Public Property name As String()
        Public Property cellularLocation As RDFresource
        Public Property entityReference As RDFresource
        <XmlElement> Public Property feature As RDFresource()

    End Class
    Public Class ProteinReference : Inherits Node
        Public Property organism As RDFresource
        <XmlElement> Public Property name As String()
    End Class
    Public Class BioSource : Inherits Node
        <XmlElement> Public Property name As String()
    End Class

    Public Class FragmentFeature : Inherits ResourceElement
        Public Property featureLocation As RDFresource
    End Class
    Public Class SequenceInterval : Inherits ResourceElement
        Public Property sequenceIntervalBegin As RDFresource
        Public Property sequenceIntervalEnd As RDFresource
    End Class
    Public Class SequenceSite : Inherits ResourceElement
        Public Property sequencePosition As String
        Public Property positionStatus As String
    End Class

    Public Class RelationshipTypeVocabulary : Inherits ResourceElement
        Public Property term As String
        <XmlElement> Public Property xref As RDFresource()
    End Class

    Public Class Complex : Inherits Node
        <XmlElement> Public Property displayName As String()
        Public Property cellularLocation As RDFresource
        <XmlElement> Public Property componentStoichiometry As RDFresource()
        <XmlElement> Public Property component As RDFresource()

    End Class

    Public Class Stoichiometry : Inherits ResourceElement
        Public Property stoichiometricCoefficient As String
        Public Property physicalEntity As RDFresource
    End Class

    Public Class PhysicalEntity : Inherits Node

        <XmlElement> Public Property memberPhysicalEntity As RDFresource()
        Public Property displayName As String()
        Public Property cellularLocation As RDFresource
    End Class
End Namespace