REM  Oracle.LinuxCompatibility.MySQL.CodeGenerator
REM  Microsoft VisualBasic MYSQL




Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes

Namespace Interpro.Tables

''' <summary>
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `cv_relation`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `cv_relation` (
'''   `code` char(2) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
'''   `abbrev` varchar(10) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
'''   `description` mediumtext CHARACTER SET latin1 COLLATE latin1_bin,
'''   `forward` varchar(30) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
'''   `reverse` varchar(30) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
'''   PRIMARY KEY (`code`),
'''   UNIQUE KEY `uq_relation$abbrev` (`abbrev`)
''' ) ENGINE=InnoDB DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("cv_relation", Database:="interpro")>
Public Class cv_relation: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("code"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "2")> Public Property code As String
    <DatabaseField("abbrev"), NotNull, DataType(MySqlDbType.VarChar, "10")> Public Property abbrev As String
    <DatabaseField("description"), DataType(MySqlDbType.Text)> Public Property description As String
    <DatabaseField("forward"), NotNull, DataType(MySqlDbType.VarChar, "30")> Public Property forward As String
    <DatabaseField("reverse"), NotNull, DataType(MySqlDbType.VarChar, "30")> Public Property reverse As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `cv_relation` (`code`, `abbrev`, `description`, `forward`, `reverse`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `cv_relation` WHERE `code` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `cv_relation` SET `code`='{0}', `abbrev`='{1}', `description`='{2}', `forward`='{3}', `reverse`='{4}' WHERE `code` = '{5}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, code)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, code, abbrev, description, forward, reverse)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, code, abbrev, description, forward, reverse, code)
    End Function
#End Region
End Class


End Namespace
