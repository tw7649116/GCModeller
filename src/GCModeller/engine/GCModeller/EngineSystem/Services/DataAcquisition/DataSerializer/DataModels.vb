﻿Imports LANS.SystemsBiology.GCModeller.ModellingEngine.EngineSystem.Services.DataAcquisition.Services
Imports Microsoft.VisualBasic.ComponentModel
Imports Microsoft.VisualBasic.ComponentModel.Collection.Generic
Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes

Namespace EngineSystem.Services.DataAcquisition.DataSerializer

    ''' <summary>
    ''' (数据包中的一个数据点：表示为一个节点处的流量值或者该节点大小)
    ''' </summary>
    ''' <remarks></remarks>
    <TableName("<TABLE_NAME>")> Public Structure DataFlowF
        ''' <summary>
        ''' 插入一条记录所使用的SQL语句
        ''' </summary>
        ''' <remarks></remarks>
        Const INSERT_SQL As String = "INSERT INTO `%s` (`id`, `value`, `time`, `handle`) VALUES ('{0}', '{1}', '{2}', '{3}');"
        ''' <summary>
        ''' 创建存储所使用的数据表所使用的SQL语句
        ''' </summary>
        ''' <remarks></remarks>
        Const CREATE_TABLE_SQL As String = "CREATE TABLE `{0}` (`id` BIGINT NOT NULL AUTO_INCREMENT, `value` DOUBLE NOT NULL, `time` BIGINT NOT NULL, `handle` BIGINT NOT NULL, PRIMARY KEY (`id`), UNIQUE INDEX `id_UNIQUE` (`id` ASC));"
        ''' <summary>
        ''' 清空表中所有的数据所使用到的SQL语句
        ''' </summary>
        ''' <remarks></remarks>
        Const SQL_DROP_TABLE As String = "DROP TABLE `{0}`;"

        ''' <summary>
        ''' 当前记录在数据表中的Id编号
        ''' </summary>
        ''' <remarks></remarks>
        <DatabaseField("id")> Dim Id As Long
        ''' <summary>
        ''' 模拟计算所产生的值
        ''' </summary>
        ''' <remarks></remarks>
        <DatabaseField("value")> Dim Value As System.Double
        ''' <summary>
        ''' 当前的迭代次数
        ''' </summary>
        ''' <remarks></remarks>
        <DatabaseField("time")> Dim Time As Integer
        ''' <summary>
        ''' 目标对象唯一的句柄值
        ''' </summary>
        ''' <remarks></remarks>
        <DatabaseField("handle")> Dim Handle As Integer

        Public Overrides Function ToString() As String
            Return String.Format("[{0}] [{1}] - {2}", Id, Time, Value)
        End Function

        Public Shared Function CreateObject(DataElement As DataSource, Time As Integer) As DataFlowF
            Return New DataFlowF With {
                .Handle = DataElement.Address,
                .Value = DataElement.value,
                .Time = Time
            }
        End Function

        Public ReadOnly Property InsertSQL(Id As Long) As String
            Get
                Return String.Format(INSERT_SQL, CStr(Id), CStr(Value), CStr(Time), CStr(Handle))
            End Get
        End Property

        ''' <summary>
        ''' 将本记录中的数据转换为Csv数据表文件中的一行数据 [Id, Time, Handle, Value]
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ToCsvRow() As Microsoft.VisualBasic.DocumentFormat.Csv.DocumentStream.RowObject
            Return CType(Me, String())
        End Function

        Public Shared Widening Operator CType(e As DataFlowF) As String()
            Return {CStr(e.Id), CStr(e.Time), CStr(e.Handle), CStr(e.Value)}
        End Operator
    End Structure

    Public Structure HandleF
        Implements sIdEnumerable, IAddressHandle

        Const CREATE_TABLE_SQL As String = "CREATE TABLE `%s_handles` (`Handle` BIGINT NOT NULL, `unique_Id` LONGTEXT NOT NULL, PRIMARY KEY (`Handle`), UNIQUE INDEX `Handle_UNIQUE` (`Handle` ASC));"
        Const INSERT_INTO_SQL As String = "INSERT INTO `%s_handles` (`Handle`, `unique_Id`) VALUES ('{0}', '{1}');"
        Const SQL_DROP_TABLE As String = "DROP TABLE `{0}_handles`;"

        Public ReadOnly Property InsertSQL As String
            Get
                Return String.Format(INSERT_INTO_SQL, Handle, Identifier)
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return String.Format("[{0}] ---> {1}", Handle, Identifier)
        End Function

        Public Shared Function GenerateSQL([Object] As ModellingEngine.EngineSystem.ObjectModels.ObjectModel) As String
            Return String.Format(INSERT_INTO_SQL, [Object].Handle, [Object].Identifier)
        End Function

        Public Shared Function GetHandles(Collection As Generic.IEnumerable(Of EngineSystem.ObjectModels.ObjectModel)) As HandleF()
            Dim LQuery = From [Object] As EngineSystem.ObjectModels.ObjectModel
                         In Collection
                         Select New HandleF With {.Handle = [Object].Handle, .Identifier = [Object].Identifier} '
            Return LQuery.ToArray
        End Function

        Public Property Identifier As String Implements sIdEnumerable.Identifier
        Public Property Handle As Integer Implements IAddressHandle.Address

        Public Sub Dispose() Implements IDisposable.Dispose

        End Sub
    End Structure
End Namespace