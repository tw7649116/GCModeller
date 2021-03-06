REM  Oracle.LinuxCompatibility.MySQL.CodeGenerator
REM  Microsoft VisualBasic MYSQL




Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes

Namespace Interpro.Tables

''' <summary>
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `pfam_clan`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `pfam_clan` (
'''   `clan_id` varchar(15) CHARACTER SET latin1 COLLATE latin1_bin DEFAULT NULL,
'''   `method_ac` varchar(25) CHARACTER SET latin1 COLLATE latin1_bin DEFAULT NULL
''' ) ENGINE=InnoDB DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("pfam_clan", Database:="interpro")>
Public Class pfam_clan: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("clan_id"), DataType(MySqlDbType.VarChar, "15")> Public Property clan_id As String
    <DatabaseField("method_ac"), DataType(MySqlDbType.VarChar, "25")> Public Property method_ac As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `pfam_clan` (`clan_id`, `method_ac`) VALUES ('{0}', '{1}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `pfam_clan` WHERE ;</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `pfam_clan` SET `clan_id`='{0}', `method_ac`='{1}' WHERE ;</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Throw New NotImplementedException("Table key was not found, unable to generate ___DELETE_SQL_Invoke automatically, please edit this function manually!")
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, clan_id, method_ac)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Throw New NotImplementedException("Table key was not found, unable to generate ___UPDATE_SQL_Invoke automatically, please edit this function manually!")
    End Function
#End Region
End Class


End Namespace
