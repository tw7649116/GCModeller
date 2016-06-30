REM  Oracle.LinuxCompatibility.MySQL.CodeGenerator
REM  MYSQL Schema Mapper
REM      for Microsoft VisualBasic.NET 

REM  Dump @12/3/2015 8:30:29 PM


Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes

Namespace MySQL.genomics

''' <summary>
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `groupusers`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `groupusers` (
'''   `groupId` int(10) unsigned NOT NULL DEFAULT '0',
'''   `userId` int(10) unsigned NOT NULL DEFAULT '0',
'''   `active` tinyint(1) unsigned NOT NULL DEFAULT '0',
'''   `time` int(10) unsigned NOT NULL DEFAULT '0',
'''   KEY `groupId` (`groupId`),
'''   KEY `userId` (`userId`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("groupusers")>
Public Class groupusers: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("groupId"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "10")> Public Property groupId As Long
    <DatabaseField("userId"), NotNull, DataType(MySqlDbType.Int64, "10")> Public Property userId As Long
    <DatabaseField("active"), NotNull, DataType(MySqlDbType.Int64, "1")> Public Property active As Long
    <DatabaseField("time"), NotNull, DataType(MySqlDbType.Int64, "10")> Public Property time As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `groupusers` (`groupId`, `userId`, `active`, `time`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `groupusers` (`groupId`, `userId`, `active`, `time`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `groupusers` WHERE `groupId` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `groupusers` SET `groupId`='{0}', `userId`='{1}', `active`='{2}', `time`='{3}' WHERE `groupId` = '{4}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, groupId)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, groupId, userId, active, time)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, groupId, userId, active, time)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, groupId, userId, active, time, groupId)
    End Function
#End Region
End Class


End Namespace
