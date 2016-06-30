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
''' DROP TABLE IF EXISTS `dnabindsite`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `dnabindsite` (
'''   `taxId` int(20) unsigned NOT NULL,
'''   `bsId` varchar(100) NOT NULL,
'''   `comment` text,
'''   `name` varchar(255) DEFAULT NULL,
'''   `promoterID` varchar(255) DEFAULT NULL,
'''   `centerDist` float DEFAULT NULL,
'''   `evidence` varchar(255) DEFAULT NULL,
'''   PRIMARY KEY (`bsId`),
'''   KEY `taxId` (`taxId`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("dnabindsite")>
Public Class dnabindsite: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("taxId"), NotNull, DataType(MySqlDbType.Int64, "20")> Public Property taxId As Long
    <DatabaseField("bsId"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "100")> Public Property bsId As String
    <DatabaseField("comment"), DataType(MySqlDbType.Text)> Public Property comment As String
    <DatabaseField("name"), DataType(MySqlDbType.VarChar, "255")> Public Property name As String
    <DatabaseField("promoterID"), DataType(MySqlDbType.VarChar, "255")> Public Property promoterID As String
    <DatabaseField("centerDist"), DataType(MySqlDbType.Double)> Public Property centerDist As Double
    <DatabaseField("evidence"), DataType(MySqlDbType.VarChar, "255")> Public Property evidence As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `dnabindsite` (`taxId`, `bsId`, `comment`, `name`, `promoterID`, `centerDist`, `evidence`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `dnabindsite` (`taxId`, `bsId`, `comment`, `name`, `promoterID`, `centerDist`, `evidence`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `dnabindsite` WHERE `bsId` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `dnabindsite` SET `taxId`='{0}', `bsId`='{1}', `comment`='{2}', `name`='{3}', `promoterID`='{4}', `centerDist`='{5}', `evidence`='{6}' WHERE `bsId` = '{7}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, bsId)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, taxId, bsId, comment, name, promoterID, centerDist, evidence)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, taxId, bsId, comment, name, promoterID, centerDist, evidence)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, taxId, bsId, comment, name, promoterID, centerDist, evidence, bsId)
    End Function
#End Region
End Class


End Namespace
