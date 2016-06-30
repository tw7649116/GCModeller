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
''' DROP TABLE IF EXISTS `orthologgroup`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `orthologgroup` (
'''   `treeId` int(10) unsigned NOT NULL DEFAULT '0',
'''   `ogId` int(10) unsigned NOT NULL DEFAULT '0',
'''   `parentOG` int(10) unsigned DEFAULT NULL,
'''   `isDuplication` tinyint(1) DEFAULT NULL,
'''   `nGenes` int(10) unsigned NOT NULL DEFAULT '0',
'''   `nGenomes` int(10) unsigned NOT NULL DEFAULT '0',
'''   `nNonUniqueGenomes` int(10) unsigned NOT NULL DEFAULT '0',
'''   `splitTaxId` int(10) DEFAULT NULL,
'''   PRIMARY KEY (`treeId`,`ogId`),
'''   KEY `treeId` (`treeId`,`ogId`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("orthologgroup")>
Public Class orthologgroup: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("treeId"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "10")> Public Property treeId As Long
    <DatabaseField("ogId"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "10")> Public Property ogId As Long
    <DatabaseField("parentOG"), DataType(MySqlDbType.Int64, "10")> Public Property parentOG As Long
    <DatabaseField("isDuplication"), DataType(MySqlDbType.Int64, "1")> Public Property isDuplication As Long
    <DatabaseField("nGenes"), NotNull, DataType(MySqlDbType.Int64, "10")> Public Property nGenes As Long
    <DatabaseField("nGenomes"), NotNull, DataType(MySqlDbType.Int64, "10")> Public Property nGenomes As Long
    <DatabaseField("nNonUniqueGenomes"), NotNull, DataType(MySqlDbType.Int64, "10")> Public Property nNonUniqueGenomes As Long
    <DatabaseField("splitTaxId"), DataType(MySqlDbType.Int64, "10")> Public Property splitTaxId As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `orthologgroup` (`treeId`, `ogId`, `parentOG`, `isDuplication`, `nGenes`, `nGenomes`, `nNonUniqueGenomes`, `splitTaxId`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `orthologgroup` (`treeId`, `ogId`, `parentOG`, `isDuplication`, `nGenes`, `nGenomes`, `nNonUniqueGenomes`, `splitTaxId`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `orthologgroup` WHERE `treeId`='{0}' and `ogId`='{1}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `orthologgroup` SET `treeId`='{0}', `ogId`='{1}', `parentOG`='{2}', `isDuplication`='{3}', `nGenes`='{4}', `nGenomes`='{5}', `nNonUniqueGenomes`='{6}', `splitTaxId`='{7}' WHERE `treeId`='{8}' and `ogId`='{9}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, treeId, ogId)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, treeId, ogId, parentOG, isDuplication, nGenes, nGenomes, nNonUniqueGenomes, splitTaxId)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, treeId, ogId, parentOG, isDuplication, nGenes, nGenomes, nNonUniqueGenomes, splitTaxId)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, treeId, ogId, parentOG, isDuplication, nGenes, nGenomes, nNonUniqueGenomes, splitTaxId, treeId, ogId)
    End Function
#End Region
End Class


End Namespace
