REM  Oracle.LinuxCompatibility.MySQL.CodeGenerator
REM  MYSQL Schema Mapper
REM      for Microsoft VisualBasic.NET 

REM  Dump @12/3/2015 8:20:41 PM


Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes

Namespace Wiki.MySQL

''' <summary>
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `lib_wikichange_tag`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `lib_wikichange_tag` (
'''   `ct_rc_id` int(11) DEFAULT NULL,
'''   `ct_log_id` int(11) DEFAULT NULL,
'''   `ct_rev_id` int(11) DEFAULT NULL,
'''   `ct_tag` varbinary(255) NOT NULL,
'''   `ct_params` blob,
'''   UNIQUE KEY `change_tag_rc_tag` (`ct_rc_id`,`ct_tag`),
'''   UNIQUE KEY `change_tag_log_tag` (`ct_log_id`,`ct_tag`),
'''   UNIQUE KEY `change_tag_rev_tag` (`ct_rev_id`,`ct_tag`),
'''   KEY `change_tag_tag_id` (`ct_tag`,`ct_rc_id`,`ct_rev_id`,`ct_log_id`)
''' ) ENGINE=InnoDB DEFAULT CHARSET=binary;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("lib_wikichange_tag", Database:="wiki")>
Public Class lib_wikichange_tag: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("ct_rc_id"), PrimaryKey, DataType(MySqlDbType.Int64, "11")> Public Property ct_rc_id As Long
    <DatabaseField("ct_log_id"), DataType(MySqlDbType.Int64, "11")> Public Property ct_log_id As Long
    <DatabaseField("ct_rev_id"), DataType(MySqlDbType.Int64, "11")> Public Property ct_rev_id As Long
    <DatabaseField("ct_tag"), PrimaryKey, NotNull, DataType(MySqlDbType.Blob)> Public Property ct_tag As Byte()
    <DatabaseField("ct_params"), DataType(MySqlDbType.Blob)> Public Property ct_params As Byte()
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `lib_wikichange_tag` (`ct_rc_id`, `ct_log_id`, `ct_rev_id`, `ct_tag`, `ct_params`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `lib_wikichange_tag` (`ct_rc_id`, `ct_log_id`, `ct_rev_id`, `ct_tag`, `ct_params`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `lib_wikichange_tag` WHERE `ct_rc_id`='{0}' and `ct_tag`='{1}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `lib_wikichange_tag` SET `ct_rc_id`='{0}', `ct_log_id`='{1}', `ct_rev_id`='{2}', `ct_tag`='{3}', `ct_params`='{4}' WHERE `ct_rc_id`='{5}' and `ct_tag`='{6}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, ct_rc_id, ct_tag)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, ct_rc_id, ct_log_id, ct_rev_id, ct_tag, ct_params)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, ct_rc_id, ct_log_id, ct_rev_id, ct_tag, ct_params)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, ct_rc_id, ct_log_id, ct_rev_id, ct_tag, ct_params, ct_rc_id, ct_tag)
    End Function
#End Region
End Class


End Namespace
