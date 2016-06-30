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
''' DROP TABLE IF EXISTS `coginfo`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `coginfo` (
'''   `cogInfoId` int(10) unsigned NOT NULL DEFAULT '0',
'''   `funCode` varchar(5) NOT NULL DEFAULT '',
'''   `description` varchar(255) DEFAULT NULL,
'''   `geneName` varchar(20) DEFAULT NULL,
'''   `cddId` varchar(255) DEFAULT NULL,
'''   `length` int(10) unsigned DEFAULT NULL,
'''   PRIMARY KEY (`cogInfoId`),
'''   KEY `description` (`description`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("coginfo")>
Public Class coginfo: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("cogInfoId"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "10")> Public Property cogInfoId As Long
    <DatabaseField("funCode"), NotNull, DataType(MySqlDbType.VarChar, "5")> Public Property funCode As String
    <DatabaseField("description"), DataType(MySqlDbType.VarChar, "255")> Public Property description As String
    <DatabaseField("geneName"), DataType(MySqlDbType.VarChar, "20")> Public Property geneName As String
    <DatabaseField("cddId"), DataType(MySqlDbType.VarChar, "255")> Public Property cddId As String
    <DatabaseField("length"), DataType(MySqlDbType.Int64, "10")> Public Property length As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `coginfo` (`cogInfoId`, `funCode`, `description`, `geneName`, `cddId`, `length`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `coginfo` (`cogInfoId`, `funCode`, `description`, `geneName`, `cddId`, `length`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `coginfo` WHERE `cogInfoId` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `coginfo` SET `cogInfoId`='{0}', `funCode`='{1}', `description`='{2}', `geneName`='{3}', `cddId`='{4}', `length`='{5}' WHERE `cogInfoId` = '{6}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, cogInfoId)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, cogInfoId, funCode, description, geneName, cddId, length)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, cogInfoId, funCode, description, geneName, cddId, length)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, cogInfoId, funCode, description, geneName, cddId, length, cogInfoId)
    End Function
#End Region
End Class


End Namespace
