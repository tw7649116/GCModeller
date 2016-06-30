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
''' DROP TABLE IF EXISTS `evidence`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `evidence` (
'''   `id` int(11) NOT NULL AUTO_INCREMENT,
'''   `code` varchar(8) NOT NULL DEFAULT '',
'''   `association_id` int(11) NOT NULL DEFAULT '0',
'''   `dbxref_id` int(11) NOT NULL DEFAULT '0',
'''   `seq_acc` varchar(255) DEFAULT NULL,
'''   PRIMARY KEY (`id`),
'''   UNIQUE KEY `association_id` (`association_id`,`dbxref_id`,`code`),
'''   UNIQUE KEY `ev0` (`id`),
'''   UNIQUE KEY `ev5` (`id`,`association_id`),
'''   UNIQUE KEY `ev6` (`id`,`code`,`association_id`),
'''   KEY `ev1` (`association_id`),
'''   KEY `ev2` (`code`),
'''   KEY `ev3` (`dbxref_id`),
'''   KEY `ev4` (`association_id`,`code`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("evidence")>
Public Class evidence: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("id"), PrimaryKey, AutoIncrement, NotNull, DataType(MySqlDbType.Int64, "11")> Public Property id As Long
    <DatabaseField("code"), NotNull, DataType(MySqlDbType.VarChar, "8")> Public Property code As String
    <DatabaseField("association_id"), NotNull, DataType(MySqlDbType.Int64, "11")> Public Property association_id As Long
    <DatabaseField("dbxref_id"), NotNull, DataType(MySqlDbType.Int64, "11")> Public Property dbxref_id As Long
    <DatabaseField("seq_acc"), DataType(MySqlDbType.VarChar, "255")> Public Property seq_acc As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `evidence` (`code`, `association_id`, `dbxref_id`, `seq_acc`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `evidence` (`code`, `association_id`, `dbxref_id`, `seq_acc`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `evidence` WHERE `id` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `evidence` SET `id`='{0}', `code`='{1}', `association_id`='{2}', `dbxref_id`='{3}', `seq_acc`='{4}' WHERE `id` = '{5}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, id)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, code, association_id, dbxref_id, seq_acc)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, code, association_id, dbxref_id, seq_acc)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, id, code, association_id, dbxref_id, seq_acc, id)
    End Function
#End Region
End Class


End Namespace
