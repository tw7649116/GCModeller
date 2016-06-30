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
''' DROP TABLE IF EXISTS `tu`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `tu` (
'''   `taxId` int(10) unsigned NOT NULL,
'''   `tuId` varchar(50) NOT NULL,
'''   `name` varchar(100) DEFAULT NULL,
'''   `evidence` varchar(255) DEFAULT NULL,
'''   PRIMARY KEY (`tuId`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("tu", Database:="biocyc")>
Public Class tu: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("taxId"), NotNull, DataType(MySqlDbType.Int64, "10")> Public Property taxId As Long
    <DatabaseField("tuId"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "50")> Public Property tuId As String
    <DatabaseField("name"), DataType(MySqlDbType.VarChar, "100")> Public Property name As String
    <DatabaseField("evidence"), DataType(MySqlDbType.VarChar, "255")> Public Property evidence As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `tu` (`taxId`, `tuId`, `name`, `evidence`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `tu` (`taxId`, `tuId`, `name`, `evidence`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `tu` WHERE `tuId` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `tu` SET `taxId`='{0}', `tuId`='{1}', `name`='{2}', `evidence`='{3}' WHERE `tuId` = '{4}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, tuId)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, taxId, tuId, name, evidence)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, taxId, tuId, name, evidence)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, taxId, tuId, name, evidence, tuId)
    End Function
#End Region
End Class


End Namespace
