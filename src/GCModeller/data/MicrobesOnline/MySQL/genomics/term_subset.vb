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
''' DROP TABLE IF EXISTS `term_subset`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `term_subset` (
'''   `term_id` int(11) NOT NULL DEFAULT '0',
'''   `subset_id` int(11) NOT NULL DEFAULT '0',
'''   KEY `tss1` (`term_id`),
'''   KEY `tss2` (`subset_id`),
'''   KEY `tss3` (`term_id`,`subset_id`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("term_subset")>
Public Class term_subset: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("term_id"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "11")> Public Property term_id As Long
    <DatabaseField("subset_id"), NotNull, DataType(MySqlDbType.Int64, "11")> Public Property subset_id As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `term_subset` (`term_id`, `subset_id`) VALUES ('{0}', '{1}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `term_subset` (`term_id`, `subset_id`) VALUES ('{0}', '{1}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `term_subset` WHERE `term_id` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `term_subset` SET `term_id`='{0}', `subset_id`='{1}' WHERE `term_id` = '{2}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, term_id)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, term_id, subset_id)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, term_id, subset_id)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, term_id, subset_id, term_id)
    End Function
#End Region
End Class


End Namespace
