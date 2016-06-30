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
''' DROP TABLE IF EXISTS `pathway`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `pathway` (
'''   `id` bigint(20) NOT NULL AUTO_INCREMENT,
'''   `mapId` varchar(255) NOT NULL,
'''   `title` varchar(255) NOT NULL,
'''   PRIMARY KEY (`id`)
''' ) ENGINE=MyISAM AUTO_INCREMENT=314 DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("pathway", Database:="glamm")>
Public Class pathway: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("id"), PrimaryKey, AutoIncrement, NotNull, DataType(MySqlDbType.Int64, "20")> Public Property id As Long
    <DatabaseField("mapId"), NotNull, DataType(MySqlDbType.VarChar, "255")> Public Property mapId As String
    <DatabaseField("title"), NotNull, DataType(MySqlDbType.VarChar, "255")> Public Property title As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `pathway` (`mapId`, `title`) VALUES ('{0}', '{1}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `pathway` (`mapId`, `title`) VALUES ('{0}', '{1}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `pathway` WHERE `id` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `pathway` SET `id`='{0}', `mapId`='{1}', `title`='{2}' WHERE `id` = '{3}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, id)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, mapId, title)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, mapId, title)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, id, mapId, title, id)
    End Function
#End Region
End Class


End Namespace
