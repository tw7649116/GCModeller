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
''' DROP TABLE IF EXISTS `glammentity2datasource`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `glammentity2datasource` (
'''   `guid` bigint(10) unsigned NOT NULL,
'''   `version` int(2) unsigned NOT NULL DEFAULT '1',
'''   `priority` tinyint(3) unsigned DEFAULT NULL,
'''   `created` date DEFAULT NULL,
'''   `entityGuid` bigint(10) unsigned NOT NULL,
'''   `dataSourceGuid` bigint(10) unsigned NOT NULL,
'''   PRIMARY KEY (`guid`),
'''   KEY `entityGuid` (`entityGuid`),
'''   KEY `dataSourceGuid` (`dataSourceGuid`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("glammentity2datasource", Database:="glamm")>
Public Class glammentity2datasource: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("guid"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "10")> Public Property guid As Long
    <DatabaseField("version"), NotNull, DataType(MySqlDbType.Int64, "2")> Public Property version As Long
    <DatabaseField("priority"), DataType(MySqlDbType.Int64, "3")> Public Property priority As Long
    <DatabaseField("created"), DataType(MySqlDbType.DateTime)> Public Property created As Date
    <DatabaseField("entityGuid"), NotNull, DataType(MySqlDbType.Int64, "10")> Public Property entityGuid As Long
    <DatabaseField("dataSourceGuid"), NotNull, DataType(MySqlDbType.Int64, "10")> Public Property dataSourceGuid As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `glammentity2datasource` (`guid`, `version`, `priority`, `created`, `entityGuid`, `dataSourceGuid`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `glammentity2datasource` (`guid`, `version`, `priority`, `created`, `entityGuid`, `dataSourceGuid`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `glammentity2datasource` WHERE `guid` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `glammentity2datasource` SET `guid`='{0}', `version`='{1}', `priority`='{2}', `created`='{3}', `entityGuid`='{4}', `dataSourceGuid`='{5}' WHERE `guid` = '{6}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, guid)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, guid, version, priority, DataType.ToMySqlDateTimeString(created), entityGuid, dataSourceGuid)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, guid, version, priority, DataType.ToMySqlDateTimeString(created), entityGuid, dataSourceGuid)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, guid, version, priority, DataType.ToMySqlDateTimeString(created), entityGuid, dataSourceGuid, guid)
    End Function
#End Region
End Class


End Namespace
