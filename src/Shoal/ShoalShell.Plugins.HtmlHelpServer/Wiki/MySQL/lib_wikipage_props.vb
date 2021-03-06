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
''' DROP TABLE IF EXISTS `lib_wikipage_props`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `lib_wikipage_props` (
'''   `pp_page` int(11) NOT NULL,
'''   `pp_propname` varbinary(60) NOT NULL,
'''   `pp_value` blob NOT NULL,
'''   `pp_sortkey` float DEFAULT NULL,
'''   UNIQUE KEY `pp_page_propname` (`pp_page`,`pp_propname`),
'''   UNIQUE KEY `pp_propname_page` (`pp_propname`,`pp_page`),
'''   UNIQUE KEY `pp_propname_sortkey_page` (`pp_propname`,`pp_sortkey`,`pp_page`)
''' ) ENGINE=InnoDB DEFAULT CHARSET=binary;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("lib_wikipage_props", Database:="wiki")>
Public Class lib_wikipage_props: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("pp_page"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "11")> Public Property pp_page As Long
    <DatabaseField("pp_propname"), PrimaryKey, NotNull, DataType(MySqlDbType.Blob)> Public Property pp_propname As Byte()
    <DatabaseField("pp_value"), NotNull, DataType(MySqlDbType.Blob)> Public Property pp_value As Byte()
    <DatabaseField("pp_sortkey"), DataType(MySqlDbType.Double)> Public Property pp_sortkey As Double
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `lib_wikipage_props` (`pp_page`, `pp_propname`, `pp_value`, `pp_sortkey`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `lib_wikipage_props` (`pp_page`, `pp_propname`, `pp_value`, `pp_sortkey`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `lib_wikipage_props` WHERE `pp_page`='{0}' and `pp_propname`='{1}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `lib_wikipage_props` SET `pp_page`='{0}', `pp_propname`='{1}', `pp_value`='{2}', `pp_sortkey`='{3}' WHERE `pp_page`='{4}' and `pp_propname`='{5}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, pp_page, pp_propname)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, pp_page, pp_propname, pp_value, pp_sortkey)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, pp_page, pp_propname, pp_value, pp_sortkey)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, pp_page, pp_propname, pp_value, pp_sortkey, pp_page, pp_propname)
    End Function
#End Region
End Class


End Namespace
