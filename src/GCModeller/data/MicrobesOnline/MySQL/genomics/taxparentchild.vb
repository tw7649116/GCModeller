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
''' DROP TABLE IF EXISTS `taxparentchild`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `taxparentchild` (
'''   `parentId` int(10) NOT NULL DEFAULT '0',
'''   `childId` int(10) NOT NULL DEFAULT '0',
'''   `nDistance` int(10) unsigned NOT NULL DEFAULT '0',
'''   UNIQUE KEY `combined` (`parentId`,`childId`),
'''   KEY `parentId` (`parentId`),
'''   KEY `childId` (`childId`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("taxparentchild")>
Public Class taxparentchild: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("parentId"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "10")> Public Property parentId As Long
    <DatabaseField("childId"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "10")> Public Property childId As Long
    <DatabaseField("nDistance"), NotNull, DataType(MySqlDbType.Int64, "10")> Public Property nDistance As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `taxparentchild` (`parentId`, `childId`, `nDistance`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `taxparentchild` (`parentId`, `childId`, `nDistance`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `taxparentchild` WHERE `parentId`='{0}' and `childId`='{1}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `taxparentchild` SET `parentId`='{0}', `childId`='{1}', `nDistance`='{2}' WHERE `parentId`='{3}' and `childId`='{4}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, parentId, childId)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, parentId, childId, nDistance)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, parentId, childId, nDistance)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, parentId, childId, nDistance, parentId, childId)
    End Function
#End Region
End Class


End Namespace
