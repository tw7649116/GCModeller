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
''' DROP TABLE IF EXISTS `term_property`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `term_property` (
'''   `term_id` int(11) NOT NULL DEFAULT '0',
'''   `property_key` varchar(64) NOT NULL DEFAULT '',
'''   `property_val` varchar(255) DEFAULT NULL,
'''   KEY `term_id` (`term_id`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("term_property")>
Public Class term_property: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("term_id"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "11")> Public Property term_id As Long
    <DatabaseField("property_key"), NotNull, DataType(MySqlDbType.VarChar, "64")> Public Property property_key As String
    <DatabaseField("property_val"), DataType(MySqlDbType.VarChar, "255")> Public Property property_val As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `term_property` (`term_id`, `property_key`, `property_val`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `term_property` (`term_id`, `property_key`, `property_val`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `term_property` WHERE `term_id` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `term_property` SET `term_id`='{0}', `property_key`='{1}', `property_val`='{2}' WHERE `term_id` = '{3}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, term_id)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, term_id, property_key, property_val)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, term_id, property_key, property_val)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, term_id, property_key, property_val, term_id)
    End Function
#End Region
End Class


End Namespace
