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
''' DROP TABLE IF EXISTS `lib_wikifilearchive`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `lib_wikifilearchive` (
'''   `fa_id` int(11) NOT NULL AUTO_INCREMENT,
'''   `fa_name` varbinary(255) NOT NULL DEFAULT '',
'''   `fa_archive_name` varbinary(255) DEFAULT '',
'''   `fa_storage_group` varbinary(16) DEFAULT NULL,
'''   `fa_storage_key` varbinary(64) DEFAULT '',
'''   `fa_deleted_user` int(11) DEFAULT NULL,
'''   `fa_deleted_timestamp` binary(14) DEFAULT '\0\0\0\0\0\0\0\0\0\0\0\0\0\0',
'''   `fa_deleted_reason` varbinary(767) DEFAULT '',
'''   `fa_size` int(10) unsigned DEFAULT '0',
'''   `fa_width` int(11) DEFAULT '0',
'''   `fa_height` int(11) DEFAULT '0',
'''   `fa_metadata` mediumblob,
'''   `fa_bits` int(11) DEFAULT '0',
'''   `fa_media_type` enum('UNKNOWN','BITMAP','DRAWING','AUDIO','VIDEO','MULTIMEDIA','OFFICE','TEXT','EXECUTABLE','ARCHIVE') DEFAULT NULL,
'''   `fa_major_mime` enum('unknown','application','audio','image','text','video','message','model','multipart','chemical') DEFAULT 'unknown',
'''   `fa_minor_mime` varbinary(100) DEFAULT 'unknown',
'''   `fa_description` varbinary(767) DEFAULT NULL,
'''   `fa_user` int(10) unsigned DEFAULT '0',
'''   `fa_user_text` varbinary(255) DEFAULT NULL,
'''   `fa_timestamp` binary(14) DEFAULT '\0\0\0\0\0\0\0\0\0\0\0\0\0\0',
'''   `fa_deleted` tinyint(3) unsigned NOT NULL DEFAULT '0',
'''   `fa_sha1` varbinary(32) NOT NULL DEFAULT '',
'''   PRIMARY KEY (`fa_id`),
'''   KEY `fa_name` (`fa_name`,`fa_timestamp`),
'''   KEY `fa_storage_group` (`fa_storage_group`,`fa_storage_key`),
'''   KEY `fa_deleted_timestamp` (`fa_deleted_timestamp`),
'''   KEY `fa_user_timestamp` (`fa_user_text`,`fa_timestamp`),
'''   KEY `fa_sha1` (`fa_sha1`(10))
''' ) ENGINE=InnoDB DEFAULT CHARSET=binary;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("lib_wikifilearchive", Database:="wiki")>
Public Class lib_wikifilearchive: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("fa_id"), PrimaryKey, AutoIncrement, NotNull, DataType(MySqlDbType.Int64, "11")> Public Property fa_id As Long
    <DatabaseField("fa_name"), NotNull, DataType(MySqlDbType.Blob)> Public Property fa_name As Byte()
    <DatabaseField("fa_archive_name"), DataType(MySqlDbType.Blob)> Public Property fa_archive_name As Byte()
    <DatabaseField("fa_storage_group"), DataType(MySqlDbType.Blob)> Public Property fa_storage_group As Byte()
    <DatabaseField("fa_storage_key"), DataType(MySqlDbType.Blob)> Public Property fa_storage_key As Byte()
    <DatabaseField("fa_deleted_user"), DataType(MySqlDbType.Int64, "11")> Public Property fa_deleted_user As Long
    <DatabaseField("fa_deleted_timestamp"), DataType(MySqlDbType.Blob)> Public Property fa_deleted_timestamp As Byte()
    <DatabaseField("fa_deleted_reason"), DataType(MySqlDbType.Blob)> Public Property fa_deleted_reason As Byte()
    <DatabaseField("fa_size"), DataType(MySqlDbType.Int64, "10")> Public Property fa_size As Long
    <DatabaseField("fa_width"), DataType(MySqlDbType.Int64, "11")> Public Property fa_width As Long
    <DatabaseField("fa_height"), DataType(MySqlDbType.Int64, "11")> Public Property fa_height As Long
    <DatabaseField("fa_metadata"), DataType(MySqlDbType.Blob)> Public Property fa_metadata As Byte()
    <DatabaseField("fa_bits"), DataType(MySqlDbType.Int64, "11")> Public Property fa_bits As Long
    <DatabaseField("fa_media_type"), DataType(MySqlDbType.Text)> Public Property fa_media_type As String
    <DatabaseField("fa_major_mime"), DataType(MySqlDbType.Text)> Public Property fa_major_mime As String
    <DatabaseField("fa_minor_mime"), DataType(MySqlDbType.Blob)> Public Property fa_minor_mime As Byte()
    <DatabaseField("fa_description"), DataType(MySqlDbType.Blob)> Public Property fa_description As Byte()
    <DatabaseField("fa_user"), DataType(MySqlDbType.Int64, "10")> Public Property fa_user As Long
    <DatabaseField("fa_user_text"), DataType(MySqlDbType.Blob)> Public Property fa_user_text As Byte()
    <DatabaseField("fa_timestamp"), DataType(MySqlDbType.Blob)> Public Property fa_timestamp As Byte()
    <DatabaseField("fa_deleted"), NotNull, DataType(MySqlDbType.Int64, "3")> Public Property fa_deleted As Long
    <DatabaseField("fa_sha1"), NotNull, DataType(MySqlDbType.Blob)> Public Property fa_sha1 As Byte()
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `lib_wikifilearchive` (`fa_name`, `fa_archive_name`, `fa_storage_group`, `fa_storage_key`, `fa_deleted_user`, `fa_deleted_timestamp`, `fa_deleted_reason`, `fa_size`, `fa_width`, `fa_height`, `fa_metadata`, `fa_bits`, `fa_media_type`, `fa_major_mime`, `fa_minor_mime`, `fa_description`, `fa_user`, `fa_user_text`, `fa_timestamp`, `fa_deleted`, `fa_sha1`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `lib_wikifilearchive` (`fa_name`, `fa_archive_name`, `fa_storage_group`, `fa_storage_key`, `fa_deleted_user`, `fa_deleted_timestamp`, `fa_deleted_reason`, `fa_size`, `fa_width`, `fa_height`, `fa_metadata`, `fa_bits`, `fa_media_type`, `fa_major_mime`, `fa_minor_mime`, `fa_description`, `fa_user`, `fa_user_text`, `fa_timestamp`, `fa_deleted`, `fa_sha1`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `lib_wikifilearchive` WHERE `fa_id` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `lib_wikifilearchive` SET `fa_id`='{0}', `fa_name`='{1}', `fa_archive_name`='{2}', `fa_storage_group`='{3}', `fa_storage_key`='{4}', `fa_deleted_user`='{5}', `fa_deleted_timestamp`='{6}', `fa_deleted_reason`='{7}', `fa_size`='{8}', `fa_width`='{9}', `fa_height`='{10}', `fa_metadata`='{11}', `fa_bits`='{12}', `fa_media_type`='{13}', `fa_major_mime`='{14}', `fa_minor_mime`='{15}', `fa_description`='{16}', `fa_user`='{17}', `fa_user_text`='{18}', `fa_timestamp`='{19}', `fa_deleted`='{20}', `fa_sha1`='{21}' WHERE `fa_id` = '{22}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, fa_id)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, fa_name, fa_archive_name, fa_storage_group, fa_storage_key, fa_deleted_user, fa_deleted_timestamp, fa_deleted_reason, fa_size, fa_width, fa_height, fa_metadata, fa_bits, fa_media_type, fa_major_mime, fa_minor_mime, fa_description, fa_user, fa_user_text, fa_timestamp, fa_deleted, fa_sha1)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, fa_name, fa_archive_name, fa_storage_group, fa_storage_key, fa_deleted_user, fa_deleted_timestamp, fa_deleted_reason, fa_size, fa_width, fa_height, fa_metadata, fa_bits, fa_media_type, fa_major_mime, fa_minor_mime, fa_description, fa_user, fa_user_text, fa_timestamp, fa_deleted, fa_sha1)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, fa_id, fa_name, fa_archive_name, fa_storage_group, fa_storage_key, fa_deleted_user, fa_deleted_timestamp, fa_deleted_reason, fa_size, fa_width, fa_height, fa_metadata, fa_bits, fa_media_type, fa_major_mime, fa_minor_mime, fa_description, fa_user, fa_user_text, fa_timestamp, fa_deleted, fa_sha1, fa_id)
    End Function
#End Region
End Class


End Namespace
