REM  Oracle.LinuxCompatibility.MySQL.CodeGenerator
REM  MYSQL Schema Mapper
REM      for Microsoft VisualBasic.NET 

REM  Dump @12/3/2015 7:34:48 PM


Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes

Namespace LocalMySQL

''' <summary>
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `orthology_modules`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `orthology_modules` (
'''   `entry_id` varchar(45) NOT NULL,
'''   `module` varchar(45) NOT NULL,
'''   `id` int(11) NOT NULL AUTO_INCREMENT,
'''   PRIMARY KEY (`module`,`entry_id`),
'''   UNIQUE KEY `id_UNIQUE` (`id`)
''' ) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("orthology_modules", Database:="jp_kegg2")>
Public Class orthology_modules: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("entry_id"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "45")> Public Property entry_id As String
    <DatabaseField("module"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "45")> Public Property [module] As String
    <DatabaseField("id"), AutoIncrement, NotNull, DataType(MySqlDbType.Int64, "11")> Public Property id As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `orthology_modules` (`entry_id`, `module`) VALUES ('{0}', '{1}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `orthology_modules` (`entry_id`, `module`) VALUES ('{0}', '{1}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `orthology_modules` WHERE `module`='{0}' and `entry_id`='{1}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `orthology_modules` SET `entry_id`='{0}', `module`='{1}', `id`='{2}' WHERE `module`='{3}' and `entry_id`='{4}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, [module], entry_id)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, entry_id, [module])
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, entry_id, [module])
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, entry_id, [module], id, [module], entry_id)
    End Function
#End Region
End Class


End Namespace
