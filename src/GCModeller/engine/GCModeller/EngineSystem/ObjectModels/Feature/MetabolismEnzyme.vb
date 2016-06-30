﻿Imports System.Xml.Serialization
Imports LANS.SystemsBiology.DatabaseServices.SabiorkKineticLaws.TabularDump
Imports LANS.SystemsBiology.GCModeller.ModellingEngine.EngineSystem.ObjectModels.Entity
Imports LANS.SystemsBiology.GCModeller.ModellingEngine.EngineSystem.ObjectModels.PoolMappings
Imports LANS.SystemsBiology.GCModeller.ModellingEngine.EngineSystem.Services.DataAcquisition.Services
Imports LANS.SystemsBiology.GCModeller.ModellingEngine.EngineSystem.Services.DataAcquisition.DataSerializer
Imports Microsoft.VisualBasic.IEnumerations

Namespace EngineSystem.ObjectModels.Feature

    ''' <summary>
    ''' 每一个酶促反应对象上面的酶分子对象的引用，一个<see cref="MetabolismEnzyme.EnzymeMetabolite">酶分子</see>对一个特定的<see cref="MetabolismEnzyme.EnzymeKineticLaw">代谢反应的催化作用</see>
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MetabolismEnzyme : Inherits MappingFeature(Of EnzymeClass)
        Implements IDataSourceEntity

        ''' <summary>
        ''' 本类型对象在初始化的时候会使用本属性中的<see cref="EnzymeCatalystKineticLaw.Enzyme"></see>
        ''' 和<see cref="EnzymeCatalystKineticLaw.Metabolite"></see>来进行两个属性的值的选择:
        ''' <see cref="MetabolismEnzyme.EnzymeMetabolite"></see>
        ''' <see cref="MetabolismEnzyme.CatalystMetabolite"></see>
        ''' </summary>
        ''' <remarks></remarks>
        <DumpNode> Protected Friend EnzymeKineticLaw As MathematicsModels.EnzymeKinetics.EnzymeCatalystKineticLaw
        ''' <summary>
        ''' 当前的这个酶分子对象的代谢底物类型
        ''' </summary>
        ''' <remarks></remarks>
        <DumpNode> Protected Friend EnzymeMetabolite As Compound
        ''' <summary>
        ''' 这个酶分子对象所催化的代谢底物
        ''' </summary>
        ''' <remarks></remarks>
        <DumpNode> Protected Friend CatalystMetabolite As Compound

        ''' <summary>
        ''' 本酶分子作为一种代谢底物在代谢组中的数量值大小
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <DumpNode> <XmlAttribute> Public ReadOnly Property Quantity As Double
            Get
                Return EnzymeMetabolite.Quantity
            End Get
        End Property

        Protected Friend _EnzymeActivity As Double

        Public Property ECNumber As String

        Public ReadOnly Property EnzymeActivity As Double
            Get
                Return _EnzymeActivity
            End Get
        End Property

        Public ReadOnly Property ReferenceTag As String
            Get
                Return String.Format("{0}.{1}", EnzymeKineticLaw.KineticRecord, EnzymeMetabolite.Identifier)
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return ReferenceTag
        End Function

        ''' <summary>
        ''' 从酶促反应处初始化目标对象
        ''' </summary>
        ''' <param name="RefBase">酶蛋白质复合物分子对象的唯一标识符</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function CreateObject(RefBase As String, KineticLaw As EnzymeCatalystKineticLaw, Metabolites As Compound()) As MetabolismEnzyme
            Dim MetabolismEnzyme As MetabolismEnzyme = New MetabolismEnzyme With {.Identifier = RefBase}
            Dim MetaboliteBase = Metabolites.GetItem(RefBase)
            MetabolismEnzyme.EnzymeMetabolite = MetaboliteBase
            MetabolismEnzyme.EnzymeKineticLaw = MathematicsModels.EnzymeKinetics.EnzymeCatalystKineticLaw.[New](KineticLaw)

            Return MetabolismEnzyme
        End Function

        Public ReadOnly Property DataSource As DataSource Implements IDataSourceEntity.DataSource
            Get
                Return New DataSource(Handle, _EnzymeActivity)
            End Get
        End Property

        Public ReadOnly Property SerialsHandle As HandleF Implements IDataSourceEntity.SerialsHandle
            Get
                Return New HandleF With {
                    .Handle = Handle,
                    .Identifier = ReferenceTag
                }
            End Get
        End Property

        Public Overrides ReadOnly Property TypeId As ObjectModel.TypeIds
            Get
                Return TypeIds.FeatureMetabolismEnzyme
            End Get
        End Property

        Public Overrides ReadOnly Property MappingHandler As PoolMappings.EnzymeClass
            Get
                Throw New NotImplementedException
            End Get
        End Property
    End Class
End Namespace