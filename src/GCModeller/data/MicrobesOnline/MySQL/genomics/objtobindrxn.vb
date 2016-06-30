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
''' DROP TABLE IF EXISTS `objtobindrxn`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `objtobindrxn` (
'''   `objectId` varchar(255) NOT NULL,
'''   `bindrxn` varchar(255) NOT NULL,
'''   UNIQUE KEY `combined` (`objectId`(250),`bindrxn`(250)),
'''   KEY `objectId` (`objectId`),
'''   KEY `bindrxn` (`bindrxn`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("objtobindrxn")>
Public Class objtobindrxn: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("objectId"), NotNull, DataType(MySqlDbType.VarChar, "255")> Public Property objectId As String
    <DatabaseField("bindrxn"), NotNull, DataType(MySqlDbType.VarChar, "255")> Public Property bindrxn As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `objtobindrxn` (`objectId`, `bindrxn`) VALUES ('{0}', '{1}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `objtobindrxn` (`objectId`, `bindrxn`) VALUES ('{0}', '{1}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `objtobindrxn` WHERE ;</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `objtobindrxn` SET `objectId`='{0}', `bindrxn`='{1}' WHERE ;</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Throw New NotImplementedException("Table key was Not found, unable To generate ___DELETE_SQL_Invoke automatically, please edit this Function manually!")
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, objectId, bindrxn)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, objectId, bindrxn)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Throw New NotImplementedException("Table key was Not found, unable To generate ___UPDATE_SQL_Invoke automatically, please edit this Function manually!")
    End Function
#End Region
End Class


End Namespace
