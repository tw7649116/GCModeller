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
''' DROP TABLE IF EXISTS `taxname`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `taxname` (
'''   `taxonomyId` int(20) DEFAULT NULL,
'''   `name` text NOT NULL,
'''   `uniqueName` text,
'''   `class` varchar(255) DEFAULT NULL,
'''   UNIQUE KEY `combined` (`taxonomyId`,`name`(200),`uniqueName`(100),`class`(100)),
'''   KEY `taxId` (`taxonomyId`),
'''   KEY `class` (`class`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("taxname")>
Public Class taxname: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("taxonomyId"), DataType(MySqlDbType.Int64, "20")> Public Property taxonomyId As Long
    <DatabaseField("name"), NotNull, DataType(MySqlDbType.Text)> Public Property name As String
    <DatabaseField("uniqueName"), DataType(MySqlDbType.Text)> Public Property uniqueName As String
    <DatabaseField("class"), DataType(MySqlDbType.VarChar, "255")> Public Property [class] As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `taxname` (`taxonomyId`, `name`, `uniqueName`, `class`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `taxname` (`taxonomyId`, `name`, `uniqueName`, `class`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `taxname` WHERE ;</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `taxname` SET `taxonomyId`='{0}', `name`='{1}', `uniqueName`='{2}', `class`='{3}' WHERE ;</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Throw New NotImplementedException("Table key was Not found, unable To generate ___DELETE_SQL_Invoke automatically, please edit this Function manually!")
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, taxonomyId, name, uniqueName, [class])
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, taxonomyId, name, uniqueName, [class])
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Throw New NotImplementedException("Table key was Not found, unable To generate ___UPDATE_SQL_Invoke automatically, please edit this Function manually!")
    End Function
#End Region
End Class


End Namespace
