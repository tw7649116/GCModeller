REM  Oracle.LinuxCompatibility.MySQL.CodeGenerator
REM  MYSQL Schema Mapper
REM      for Microsoft VisualBasic.NET 

REM  Dump @12/3/2015 8:22:12 PM


Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes

Namespace MySQL.glamm

''' <summary>
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `amrxnelement`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `amrxnelement` (
'''   `id` bigint(20) NOT NULL AUTO_INCREMENT,
'''   `type` varchar(255) NOT NULL,
'''   `xrefId` varchar(255) NOT NULL,
'''   `amRxn_id` bigint(20) NOT NULL,
'''   PRIMARY KEY (`id`),
'''   KEY `FK1F5C04406B73B23C` (`amRxn_id`)
''' ) ENGINE=MyISAM AUTO_INCREMENT=12988 DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("amrxnelement", Database:="glamm")>
Public Class amrxnelement: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("id"), PrimaryKey, AutoIncrement, NotNull, DataType(MySqlDbType.Int64, "20")> Public Property id As Long
    <DatabaseField("type"), NotNull, DataType(MySqlDbType.VarChar, "255")> Public Property type As String
    <DatabaseField("xrefId"), NotNull, DataType(MySqlDbType.VarChar, "255")> Public Property xrefId As String
    <DatabaseField("amRxn_id"), NotNull, DataType(MySqlDbType.Int64, "20")> Public Property amRxn_id As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `amrxnelement` (`type`, `xrefId`, `amRxn_id`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `amrxnelement` (`type`, `xrefId`, `amRxn_id`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `amrxnelement` WHERE `id` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `amrxnelement` SET `id`='{0}', `type`='{1}', `xrefId`='{2}', `amRxn_id`='{3}' WHERE `id` = '{4}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, id)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, type, xrefId, amRxn_id)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, type, xrefId, amRxn_id)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, id, type, xrefId, amRxn_id, id)
    End Function
#End Region
End Class


End Namespace
