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
''' DROP TABLE IF EXISTS `gene_product_ancestor`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `gene_product_ancestor` (
'''   `gene_product_id` int(11) NOT NULL DEFAULT '0',
'''   `ancestor_id` int(11) NOT NULL DEFAULT '0',
'''   KEY `gene_product_id` (`gene_product_id`),
'''   KEY `ancestor_id` (`ancestor_id`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("gene_product_ancestor")>
Public Class gene_product_ancestor: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("gene_product_id"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "11")> Public Property gene_product_id As Long
    <DatabaseField("ancestor_id"), NotNull, DataType(MySqlDbType.Int64, "11")> Public Property ancestor_id As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `gene_product_ancestor` (`gene_product_id`, `ancestor_id`) VALUES ('{0}', '{1}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `gene_product_ancestor` (`gene_product_id`, `ancestor_id`) VALUES ('{0}', '{1}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `gene_product_ancestor` WHERE `gene_product_id` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `gene_product_ancestor` SET `gene_product_id`='{0}', `ancestor_id`='{1}' WHERE `gene_product_id` = '{2}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, gene_product_id)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, gene_product_id, ancestor_id)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, gene_product_id, ancestor_id)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, gene_product_id, ancestor_id, gene_product_id)
    End Function
#End Region
End Class


End Namespace
