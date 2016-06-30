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
''' DROP TABLE IF EXISTS `locus2go`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `locus2go` (
'''   `locusId` int(10) unsigned DEFAULT NULL,
'''   `goId` int(10) unsigned NOT NULL DEFAULT '0',
'''   `evidence` varchar(255) NOT NULL DEFAULT '',
'''   UNIQUE KEY `orf2go` (`locusId`,`goId`),
'''   KEY `orfId` (`locusId`),
'''   KEY `goId` (`goId`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("locus2go")>
Public Class locus2go: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("locusId"), PrimaryKey, DataType(MySqlDbType.Int64, "10")> Public Property locusId As Long
    <DatabaseField("goId"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "10")> Public Property goId As Long
    <DatabaseField("evidence"), NotNull, DataType(MySqlDbType.VarChar, "255")> Public Property evidence As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `locus2go` (`locusId`, `goId`, `evidence`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `locus2go` (`locusId`, `goId`, `evidence`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `locus2go` WHERE `locusId`='{0}' and `goId`='{1}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `locus2go` SET `locusId`='{0}', `goId`='{1}', `evidence`='{2}' WHERE `locusId`='{3}' and `goId`='{4}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, locusId, goId)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, locusId, goId, evidence)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, locusId, goId, evidence)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, locusId, goId, evidence, locusId, goId)
    End Function
#End Region
End Class


End Namespace
