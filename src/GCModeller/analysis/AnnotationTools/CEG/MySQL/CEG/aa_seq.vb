REM  Oracle.LinuxCompatibility.MySQL.CodeGenerator
REM  MYSQL Schema Mapper
REM      for Microsoft VisualBasic.NET 

REM  Dump @12/3/2015 8:51:02 PM


Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes

Namespace CEG.MySQL

''' <summary>
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `aa_seq`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `aa_seq` (
'''   `gid` varchar(25) DEFAULT NULL,
'''   `aalength` varchar(8) DEFAULT NULL,
'''   `aaseq` longtext
''' ) ENGINE=MyISAM AUTO_INCREMENT=7687 DEFAULT CHARSET=gb2312;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("aa_seq", Database:="ceg")>
Public Class aa_seq: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("gid"), DataType(MySqlDbType.VarChar, "25")> Public Property gid As String
    <DatabaseField("aalength"), DataType(MySqlDbType.VarChar, "8")> Public Property aalength As String
    <DatabaseField("aaseq"), DataType(MySqlDbType.Text)> Public Property aaseq As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `aa_seq` (`gid`, `aalength`, `aaseq`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `aa_seq` (`gid`, `aalength`, `aaseq`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `aa_seq` WHERE ;</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `aa_seq` SET `gid`='{0}', `aalength`='{1}', `aaseq`='{2}' WHERE ;</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Throw New NotImplementedException("Table key was Not found, unable To generate ___DELETE_SQL_Invoke automatically, please edit this Function manually!")
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, gid, aalength, aaseq)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, gid, aalength, aaseq)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Throw New NotImplementedException("Table key was Not found, unable To generate ___UPDATE_SQL_Invoke automatically, please edit this Function manually!")
    End Function
#End Region
End Class


End Namespace
