REM  Oracle.LinuxCompatibility.MySQL.CodeGenerator
REM  MYSQL Schema Mapper
REM      for Microsoft VisualBasic.NET 

REM  Dump @12/3/2015 8:32:12 PM


Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes

Namespace MySQL.BioCyc

''' <summary>
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `enzrxn`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `enzrxn` (
'''   `enzrxnId` varchar(255) NOT NULL,
'''   `alterSubstrate` text,
'''   `name` varchar(255) DEFAULT NULL,
'''   `enzymeId` varchar(255) NOT NULL,
'''   `direction` varchar(255) DEFAULT NULL,
'''   PRIMARY KEY (`enzrxnId`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("enzrxn", Database:="biocyc")>
Public Class enzrxn: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("enzrxnId"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "255")> Public Property enzrxnId As String
    <DatabaseField("alterSubstrate"), DataType(MySqlDbType.Text)> Public Property alterSubstrate As String
    <DatabaseField("name"), DataType(MySqlDbType.VarChar, "255")> Public Property name As String
    <DatabaseField("enzymeId"), NotNull, DataType(MySqlDbType.VarChar, "255")> Public Property enzymeId As String
    <DatabaseField("direction"), DataType(MySqlDbType.VarChar, "255")> Public Property direction As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `enzrxn` (`enzrxnId`, `alterSubstrate`, `name`, `enzymeId`, `direction`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `enzrxn` (`enzrxnId`, `alterSubstrate`, `name`, `enzymeId`, `direction`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `enzrxn` WHERE `enzrxnId` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `enzrxn` SET `enzrxnId`='{0}', `alterSubstrate`='{1}', `name`='{2}', `enzymeId`='{3}', `direction`='{4}' WHERE `enzrxnId` = '{5}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, enzrxnId)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, enzrxnId, alterSubstrate, name, enzymeId, direction)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, enzrxnId, alterSubstrate, name, enzymeId, direction)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, enzrxnId, alterSubstrate, name, enzymeId, direction, enzrxnId)
    End Function
#End Region
End Class


End Namespace
