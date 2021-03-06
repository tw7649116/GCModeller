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
''' DROP TABLE IF EXISTS `lib_wikiipblocks`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `lib_wikiipblocks` (
'''   `ipb_id` int(11) NOT NULL AUTO_INCREMENT,
'''   `ipb_address` tinyblob NOT NULL,
'''   `ipb_user` int(10) unsigned NOT NULL DEFAULT '0',
'''   `ipb_by` int(10) unsigned NOT NULL DEFAULT '0',
'''   `ipb_by_text` varbinary(255) NOT NULL DEFAULT '',
'''   `ipb_reason` varbinary(767) NOT NULL,
'''   `ipb_timestamp` binary(14) NOT NULL DEFAULT '\0\0\0\0\0\0\0\0\0\0\0\0\0\0',
'''   `ipb_auto` tinyint(1) NOT NULL DEFAULT '0',
'''   `ipb_anon_only` tinyint(1) NOT NULL DEFAULT '0',
'''   `ipb_create_account` tinyint(1) NOT NULL DEFAULT '1',
'''   `ipb_enable_autoblock` tinyint(1) NOT NULL DEFAULT '1',
'''   `ipb_expiry` varbinary(14) NOT NULL DEFAULT '',
'''   `ipb_range_start` tinyblob NOT NULL,
'''   `ipb_range_end` tinyblob NOT NULL,
'''   `ipb_deleted` tinyint(1) NOT NULL DEFAULT '0',
'''   `ipb_block_email` tinyint(1) NOT NULL DEFAULT '0',
'''   `ipb_allow_usertalk` tinyint(1) NOT NULL DEFAULT '0',
'''   `ipb_parent_block_id` int(11) DEFAULT NULL,
'''   PRIMARY KEY (`ipb_id`),
'''   UNIQUE KEY `ipb_address` (`ipb_address`(255),`ipb_user`,`ipb_auto`,`ipb_anon_only`),
'''   KEY `ipb_user` (`ipb_user`),
'''   KEY `ipb_range` (`ipb_range_start`(8),`ipb_range_end`(8)),
'''   KEY `ipb_timestamp` (`ipb_timestamp`),
'''   KEY `ipb_expiry` (`ipb_expiry`),
'''   KEY `ipb_parent_block_id` (`ipb_parent_block_id`)
''' ) ENGINE=InnoDB DEFAULT CHARSET=binary;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("lib_wikiipblocks", Database:="wiki")>
Public Class lib_wikiipblocks: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("ipb_id"), PrimaryKey, AutoIncrement, NotNull, DataType(MySqlDbType.Int64, "11")> Public Property ipb_id As Long
    <DatabaseField("ipb_address"), NotNull, DataType(MySqlDbType.Blob)> Public Property ipb_address As Byte()
    <DatabaseField("ipb_user"), NotNull, DataType(MySqlDbType.Int64, "10")> Public Property ipb_user As Long
    <DatabaseField("ipb_by"), NotNull, DataType(MySqlDbType.Int64, "10")> Public Property ipb_by As Long
    <DatabaseField("ipb_by_text"), NotNull, DataType(MySqlDbType.Blob)> Public Property ipb_by_text As Byte()
    <DatabaseField("ipb_reason"), NotNull, DataType(MySqlDbType.Blob)> Public Property ipb_reason As Byte()
    <DatabaseField("ipb_timestamp"), NotNull, DataType(MySqlDbType.Blob)> Public Property ipb_timestamp As Byte()
    <DatabaseField("ipb_auto"), NotNull, DataType(MySqlDbType.Int64, "1")> Public Property ipb_auto As Long
    <DatabaseField("ipb_anon_only"), NotNull, DataType(MySqlDbType.Int64, "1")> Public Property ipb_anon_only As Long
    <DatabaseField("ipb_create_account"), NotNull, DataType(MySqlDbType.Int64, "1")> Public Property ipb_create_account As Long
    <DatabaseField("ipb_enable_autoblock"), NotNull, DataType(MySqlDbType.Int64, "1")> Public Property ipb_enable_autoblock As Long
    <DatabaseField("ipb_expiry"), NotNull, DataType(MySqlDbType.Blob)> Public Property ipb_expiry As Byte()
    <DatabaseField("ipb_range_start"), NotNull, DataType(MySqlDbType.Blob)> Public Property ipb_range_start As Byte()
    <DatabaseField("ipb_range_end"), NotNull, DataType(MySqlDbType.Blob)> Public Property ipb_range_end As Byte()
    <DatabaseField("ipb_deleted"), NotNull, DataType(MySqlDbType.Int64, "1")> Public Property ipb_deleted As Long
    <DatabaseField("ipb_block_email"), NotNull, DataType(MySqlDbType.Int64, "1")> Public Property ipb_block_email As Long
    <DatabaseField("ipb_allow_usertalk"), NotNull, DataType(MySqlDbType.Int64, "1")> Public Property ipb_allow_usertalk As Long
    <DatabaseField("ipb_parent_block_id"), DataType(MySqlDbType.Int64, "11")> Public Property ipb_parent_block_id As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `lib_wikiipblocks` (`ipb_address`, `ipb_user`, `ipb_by`, `ipb_by_text`, `ipb_reason`, `ipb_timestamp`, `ipb_auto`, `ipb_anon_only`, `ipb_create_account`, `ipb_enable_autoblock`, `ipb_expiry`, `ipb_range_start`, `ipb_range_end`, `ipb_deleted`, `ipb_block_email`, `ipb_allow_usertalk`, `ipb_parent_block_id`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `lib_wikiipblocks` (`ipb_address`, `ipb_user`, `ipb_by`, `ipb_by_text`, `ipb_reason`, `ipb_timestamp`, `ipb_auto`, `ipb_anon_only`, `ipb_create_account`, `ipb_enable_autoblock`, `ipb_expiry`, `ipb_range_start`, `ipb_range_end`, `ipb_deleted`, `ipb_block_email`, `ipb_allow_usertalk`, `ipb_parent_block_id`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `lib_wikiipblocks` WHERE `ipb_id` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `lib_wikiipblocks` SET `ipb_id`='{0}', `ipb_address`='{1}', `ipb_user`='{2}', `ipb_by`='{3}', `ipb_by_text`='{4}', `ipb_reason`='{5}', `ipb_timestamp`='{6}', `ipb_auto`='{7}', `ipb_anon_only`='{8}', `ipb_create_account`='{9}', `ipb_enable_autoblock`='{10}', `ipb_expiry`='{11}', `ipb_range_start`='{12}', `ipb_range_end`='{13}', `ipb_deleted`='{14}', `ipb_block_email`='{15}', `ipb_allow_usertalk`='{16}', `ipb_parent_block_id`='{17}' WHERE `ipb_id` = '{18}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, ipb_id)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, ipb_address, ipb_user, ipb_by, ipb_by_text, ipb_reason, ipb_timestamp, ipb_auto, ipb_anon_only, ipb_create_account, ipb_enable_autoblock, ipb_expiry, ipb_range_start, ipb_range_end, ipb_deleted, ipb_block_email, ipb_allow_usertalk, ipb_parent_block_id)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, ipb_address, ipb_user, ipb_by, ipb_by_text, ipb_reason, ipb_timestamp, ipb_auto, ipb_anon_only, ipb_create_account, ipb_enable_autoblock, ipb_expiry, ipb_range_start, ipb_range_end, ipb_deleted, ipb_block_email, ipb_allow_usertalk, ipb_parent_block_id)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, ipb_id, ipb_address, ipb_user, ipb_by, ipb_by_text, ipb_reason, ipb_timestamp, ipb_auto, ipb_anon_only, ipb_create_account, ipb_enable_autoblock, ipb_expiry, ipb_range_start, ipb_range_end, ipb_deleted, ipb_block_email, ipb_allow_usertalk, ipb_parent_block_id, ipb_id)
    End Function
#End Region
End Class


End Namespace
