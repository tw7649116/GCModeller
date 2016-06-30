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
''' DROP TABLE IF EXISTS `groups`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `groups` (
'''   `groupId` int(10) unsigned NOT NULL AUTO_INCREMENT,
'''   `name` varchar(32) NOT NULL DEFAULT '',
'''   `description` varchar(255) NOT NULL DEFAULT '',
'''   `adminUserId` int(10) unsigned NOT NULL DEFAULT '0',
'''   PRIMARY KEY (`groupId`),
'''   UNIQUE KEY `name` (`name`),
'''   KEY `adminUserId` (`adminUserId`)
''' ) ENGINE=MyISAM AUTO_INCREMENT=10296746 DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("groups")>
Public Class groups: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("groupId"), PrimaryKey, AutoIncrement, NotNull, DataType(MySqlDbType.Int64, "10")> Public Property groupId As Long
    <DatabaseField("name"), NotNull, DataType(MySqlDbType.VarChar, "32")> Public Property name As String
    <DatabaseField("description"), NotNull, DataType(MySqlDbType.VarChar, "255")> Public Property description As String
    <DatabaseField("adminUserId"), NotNull, DataType(MySqlDbType.Int64, "10")> Public Property adminUserId As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `groups` (`name`, `description`, `adminUserId`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `groups` (`name`, `description`, `adminUserId`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `groups` WHERE `groupId` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `groups` SET `groupId`='{0}', `name`='{1}', `description`='{2}', `adminUserId`='{3}' WHERE `groupId` = '{4}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, groupId)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, name, description, adminUserId)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, name, description, adminUserId)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, groupId, name, description, adminUserId, groupId)
    End Function
#End Region
End Class


End Namespace
