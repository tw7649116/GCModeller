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
''' DROP TABLE IF EXISTS `promoter`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `promoter` (
'''   `taxId` int(20) unsigned NOT NULL,
'''   `promoterId` varchar(100) NOT NULL,
'''   `name` varchar(20) DEFAULT NULL,
'''   `evidence` varchar(100) DEFAULT NULL,
'''   PRIMARY KEY (`promoterId`),
'''   KEY `taxId` (`taxId`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("promoter")>
Public Class promoter: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("taxId"), NotNull, DataType(MySqlDbType.Int64, "20")> Public Property taxId As Long
    <DatabaseField("promoterId"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "100")> Public Property promoterId As String
    <DatabaseField("name"), DataType(MySqlDbType.VarChar, "20")> Public Property name As String
    <DatabaseField("evidence"), DataType(MySqlDbType.VarChar, "100")> Public Property evidence As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `promoter` (`taxId`, `promoterId`, `name`, `evidence`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `promoter` (`taxId`, `promoterId`, `name`, `evidence`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `promoter` WHERE `promoterId` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `promoter` SET `taxId`='{0}', `promoterId`='{1}', `name`='{2}', `evidence`='{3}' WHERE `promoterId` = '{4}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, promoterId)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, taxId, promoterId, name, evidence)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, taxId, promoterId, name, evidence)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, taxId, promoterId, name, evidence, promoterId)
    End Function
#End Region
End Class


End Namespace
