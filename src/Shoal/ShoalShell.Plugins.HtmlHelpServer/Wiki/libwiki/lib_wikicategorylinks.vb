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
''' DROP TABLE IF EXISTS `lib_wikicategorylinks`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `lib_wikicategorylinks` (
'''   `cl_from` int(10) unsigned NOT NULL DEFAULT '0',
'''   `cl_to` varbinary(255) NOT NULL DEFAULT '',
'''   `cl_sortkey` varbinary(230) NOT NULL DEFAULT '',
'''   `cl_sortkey_prefix` varbinary(255) NOT NULL DEFAULT '',
'''   `cl_timestamp` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
'''   `cl_collation` varbinary(32) NOT NULL DEFAULT '',
'''   `cl_type` enum('page','subcat','file') NOT NULL DEFAULT 'page',
'''   UNIQUE KEY `cl_from` (`cl_from`,`cl_to`),
'''   KEY `cl_sortkey` (`cl_to`,`cl_type`,`cl_sortkey`,`cl_from`),
'''   KEY `cl_timestamp` (`cl_to`,`cl_timestamp`),
'''   KEY `cl_collation` (`cl_collation`)
''' ) ENGINE=InnoDB DEFAULT CHARSET=binary;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("lib_wikicategorylinks", Database:="wiki")>
Public Class lib_wikicategorylinks: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("cl_from"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "10")> Public Property cl_from As Long
    <DatabaseField("cl_to"), PrimaryKey, NotNull, DataType(MySqlDbType.Blob)> Public Property cl_to As Byte()
    <DatabaseField("cl_sortkey"), NotNull, DataType(MySqlDbType.Blob)> Public Property cl_sortkey As Byte()
    <DatabaseField("cl_sortkey_prefix"), NotNull, DataType(MySqlDbType.Blob)> Public Property cl_sortkey_prefix As Byte()
    <DatabaseField("cl_timestamp"), NotNull, DataType(MySqlDbType.DateTime)> Public Property cl_timestamp As Date
    <DatabaseField("cl_collation"), NotNull, DataType(MySqlDbType.Blob)> Public Property cl_collation As Byte()
    <DatabaseField("cl_type"), NotNull, DataType(MySqlDbType.String)> Public Property cl_type As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `lib_wikicategorylinks` (`cl_from`, `cl_to`, `cl_sortkey`, `cl_sortkey_prefix`, `cl_timestamp`, `cl_collation`, `cl_type`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `lib_wikicategorylinks` (`cl_from`, `cl_to`, `cl_sortkey`, `cl_sortkey_prefix`, `cl_timestamp`, `cl_collation`, `cl_type`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `lib_wikicategorylinks` WHERE `cl_from`='{0}' and `cl_to`='{1}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `lib_wikicategorylinks` SET `cl_from`='{0}', `cl_to`='{1}', `cl_sortkey`='{2}', `cl_sortkey_prefix`='{3}', `cl_timestamp`='{4}', `cl_collation`='{5}', `cl_type`='{6}' WHERE `cl_from`='{7}' and `cl_to`='{8}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, cl_from, cl_to)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, cl_from, cl_to, cl_sortkey, cl_sortkey_prefix, DataType.ToMySqlDateTimeString(cl_timestamp), cl_collation, cl_type)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, cl_from, cl_to, cl_sortkey, cl_sortkey_prefix, DataType.ToMySqlDateTimeString(cl_timestamp), cl_collation, cl_type)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, cl_from, cl_to, cl_sortkey, cl_sortkey_prefix, DataType.ToMySqlDateTimeString(cl_timestamp), cl_collation, cl_type, cl_from, cl_to)
    End Function
#End Region
End Class


End Namespace
