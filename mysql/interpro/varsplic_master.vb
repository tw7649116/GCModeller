REM  Oracle.LinuxCompatibility.MySQL.CodeGenerator
REM  Microsoft VisualBasic MYSQL




Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes

Namespace Interpro.Tables

''' <summary>
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `varsplic_master`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `varsplic_master` (
'''   `protein_ac` varchar(6) CHARACTER SET latin1 COLLATE latin1_bin DEFAULT NULL,
'''   `variant` int(3) DEFAULT NULL,
'''   `crc64` varchar(16) CHARACTER SET latin1 COLLATE latin1_bin DEFAULT NULL,
'''   `length` int(5) DEFAULT NULL
''' ) ENGINE=InnoDB DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("varsplic_master", Database:="interpro")>
Public Class varsplic_master: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("protein_ac"), DataType(MySqlDbType.VarChar, "6")> Public Property protein_ac As String
    <DatabaseField("variant"), DataType(MySqlDbType.Int64, "3")> Public Property [variant] As Long
    <DatabaseField("crc64"), DataType(MySqlDbType.VarChar, "16")> Public Property crc64 As String
    <DatabaseField("length"), DataType(MySqlDbType.Int64, "5")> Public Property length As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `varsplic_master` (`protein_ac`, `variant`, `crc64`, `length`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `varsplic_master` WHERE ;</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `varsplic_master` SET `protein_ac`='{0}', `variant`='{1}', `crc64`='{2}', `length`='{3}' WHERE ;</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Throw New NotImplementedException("Table key was not found, unable to generate ___DELETE_SQL_Invoke automatically, please edit this function manually!")
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, protein_ac, [variant], crc64, length)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Throw New NotImplementedException("Table key was not found, unable to generate ___UPDATE_SQL_Invoke automatically, please edit this function manually!")
    End Function
#End Region
End Class


End Namespace
