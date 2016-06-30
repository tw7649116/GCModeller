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
''' DROP TABLE IF EXISTS `description`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `description` (
'''   `descriptionId` int(10) unsigned NOT NULL AUTO_INCREMENT,
'''   `description` text,
'''   `source` varchar(255) DEFAULT NULL,
'''   `created` date DEFAULT NULL,
'''   `locusId` int(10) unsigned DEFAULT NULL,
'''   `version` int(2) unsigned NOT NULL DEFAULT '1',
'''   PRIMARY KEY (`descriptionId`),
'''   KEY `OrfId_version` (`locusId`,`version`),
'''   KEY `locusId` (`locusId`),
'''   KEY `description` (`description`(150)),
'''   FULLTEXT KEY `description2` (`description`)
''' ) ENGINE=MyISAM AUTO_INCREMENT=8014620 DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("description")>
Public Class description: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("descriptionId"), PrimaryKey, AutoIncrement, NotNull, DataType(MySqlDbType.Int64, "10")> Public Property descriptionId As Long
    <DatabaseField("description"), DataType(MySqlDbType.Text)> Public Property description As String
    <DatabaseField("source"), DataType(MySqlDbType.VarChar, "255")> Public Property source As String
    <DatabaseField("created"), DataType(MySqlDbType.DateTime)> Public Property created As Date
    <DatabaseField("locusId"), DataType(MySqlDbType.Int64, "10")> Public Property locusId As Long
    <DatabaseField("version"), NotNull, DataType(MySqlDbType.Int64, "2")> Public Property version As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `description` (`description`, `source`, `created`, `locusId`, `version`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `description` (`description`, `source`, `created`, `locusId`, `version`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `description` WHERE `descriptionId` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `description` SET `descriptionId`='{0}', `description`='{1}', `source`='{2}', `created`='{3}', `locusId`='{4}', `version`='{5}' WHERE `descriptionId` = '{6}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, descriptionId)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, description, source, DataType.ToMySqlDateTimeString(created), locusId, version)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, description, source, DataType.ToMySqlDateTimeString(created), locusId, version)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, descriptionId, description, source, DataType.ToMySqlDateTimeString(created), locusId, version, descriptionId)
    End Function
#End Region
End Class


End Namespace
