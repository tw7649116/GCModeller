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
''' DROP TABLE IF EXISTS `lib_wikisearchindex`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `lib_wikisearchindex` (
'''   `si_page` int(10) unsigned NOT NULL,
'''   `si_title` varchar(255) NOT NULL DEFAULT '',
'''   `si_text` mediumtext NOT NULL,
'''   UNIQUE KEY `si_page` (`si_page`),
'''   FULLTEXT KEY `si_title` (`si_title`),
'''   FULLTEXT KEY `si_text` (`si_text`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=utf8;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("lib_wikisearchindex", Database:="wiki")>
Public Class lib_wikisearchindex: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("si_page"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "10")> Public Property si_page As Long
    <DatabaseField("si_title"), NotNull, DataType(MySqlDbType.VarChar, "255")> Public Property si_title As String
    <DatabaseField("si_text"), NotNull, DataType(MySqlDbType.Text)> Public Property si_text As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `lib_wikisearchindex` (`si_page`, `si_title`, `si_text`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `lib_wikisearchindex` (`si_page`, `si_title`, `si_text`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `lib_wikisearchindex` WHERE `si_page` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `lib_wikisearchindex` SET `si_page`='{0}', `si_title`='{1}', `si_text`='{2}' WHERE `si_page` = '{3}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, si_page)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, si_page, si_title, si_text)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, si_page, si_title, si_text)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, si_page, si_title, si_text, si_page)
    End Function
#End Region
End Class


End Namespace
