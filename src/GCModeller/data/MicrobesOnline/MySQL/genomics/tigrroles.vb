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
''' DROP TABLE IF EXISTS `tigrroles`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `tigrroles` (
'''   `roleId` int(5) NOT NULL DEFAULT '0',
'''   `level` enum('main','sub1') NOT NULL DEFAULT 'main',
'''   `description` varchar(255) NOT NULL DEFAULT '',
'''   KEY `roleId` (`roleId`),
'''   KEY `description` (`description`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("tigrroles")>
Public Class tigrroles: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("roleId"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "5")> Public Property roleId As Long
    <DatabaseField("level"), NotNull, DataType(MySqlDbType.String)> Public Property level As String
    <DatabaseField("description"), NotNull, DataType(MySqlDbType.VarChar, "255")> Public Property description As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `tigrroles` (`roleId`, `level`, `description`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `tigrroles` (`roleId`, `level`, `description`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `tigrroles` WHERE `roleId` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `tigrroles` SET `roleId`='{0}', `level`='{1}', `description`='{2}' WHERE `roleId` = '{3}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, roleId)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, roleId, level, description)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, roleId, level, description)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, roleId, level, description, roleId)
    End Function
#End Region
End Class


End Namespace
