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
''' DROP TABLE IF EXISTS `gocount`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `gocount` (
'''   `goId` int(10) unsigned NOT NULL DEFAULT '0',
'''   `goCount` int(10) unsigned NOT NULL DEFAULT '0',
'''   `taxId` int(10) DEFAULT NULL,
'''   UNIQUE KEY `tax2go` (`taxId`,`goId`),
'''   KEY `taxId` (`taxId`),
'''   KEY `goId` (`goId`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("gocount")>
Public Class gocount: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("goId"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "10")> Public Property goId As Long
    <DatabaseField("goCount"), NotNull, DataType(MySqlDbType.Int64, "10")> Public Property goCount As Long
    <DatabaseField("taxId"), PrimaryKey, DataType(MySqlDbType.Int64, "10")> Public Property taxId As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `gocount` (`goId`, `goCount`, `taxId`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `gocount` (`goId`, `goCount`, `taxId`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `gocount` WHERE `taxId`='{0}' and `goId`='{1}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `gocount` SET `goId`='{0}', `goCount`='{1}', `taxId`='{2}' WHERE `taxId`='{3}' and `goId`='{4}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, taxId, goId)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, goId, goCount, taxId)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, goId, goCount, taxId)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, goId, goCount, taxId, taxId, goId)
    End Function
#End Region
End Class


End Namespace
