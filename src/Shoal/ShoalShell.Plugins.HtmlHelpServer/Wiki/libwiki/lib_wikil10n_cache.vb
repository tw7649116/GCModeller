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
''' DROP TABLE IF EXISTS `lib_wikil10n_cache`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `lib_wikil10n_cache` (
'''   `lc_lang` varbinary(32) NOT NULL,
'''   `lc_key` varbinary(255) NOT NULL,
'''   `lc_value` mediumblob NOT NULL,
'''   KEY `lc_lang_key` (`lc_lang`,`lc_key`)
''' ) ENGINE=InnoDB DEFAULT CHARSET=binary;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("lib_wikil10n_cache", Database:="wiki")>
Public Class lib_wikil10n_cache: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("lc_lang"), PrimaryKey, NotNull, DataType(MySqlDbType.Blob)> Public Property lc_lang As Byte()
    <DatabaseField("lc_key"), PrimaryKey, NotNull, DataType(MySqlDbType.Blob)> Public Property lc_key As Byte()
    <DatabaseField("lc_value"), NotNull, DataType(MySqlDbType.Blob)> Public Property lc_value As Byte()
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `lib_wikil10n_cache` (`lc_lang`, `lc_key`, `lc_value`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `lib_wikil10n_cache` (`lc_lang`, `lc_key`, `lc_value`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `lib_wikil10n_cache` WHERE `lc_lang`='{0}' and `lc_key`='{1}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `lib_wikil10n_cache` SET `lc_lang`='{0}', `lc_key`='{1}', `lc_value`='{2}' WHERE `lc_lang`='{3}' and `lc_key`='{4}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, lc_lang, lc_key)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, lc_lang, lc_key, lc_value)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, lc_lang, lc_key, lc_value)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, lc_lang, lc_key, lc_value, lc_lang, lc_key)
    End Function
#End Region
End Class


End Namespace
