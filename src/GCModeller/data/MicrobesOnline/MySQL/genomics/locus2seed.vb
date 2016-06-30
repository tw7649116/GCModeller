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
''' DROP TABLE IF EXISTS `locus2seed`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `locus2seed` (
'''   `locusId` int(10) unsigned NOT NULL,
'''   `seedId` varchar(50) NOT NULL,
'''   `isIdentical` tinyint(1) NOT NULL DEFAULT '0',
'''   PRIMARY KEY (`locusId`),
'''   KEY `locusId_seedId_key` (`locusId`,`seedId`),
'''   KEY `seedId_locusId_key` (`seedId`,`locusId`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("locus2seed")>
Public Class locus2seed: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("locusId"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "10")> Public Property locusId As Long
    <DatabaseField("seedId"), NotNull, DataType(MySqlDbType.VarChar, "50")> Public Property seedId As String
    <DatabaseField("isIdentical"), NotNull, DataType(MySqlDbType.Int64, "1")> Public Property isIdentical As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `locus2seed` (`locusId`, `seedId`, `isIdentical`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `locus2seed` (`locusId`, `seedId`, `isIdentical`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `locus2seed` WHERE `locusId` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `locus2seed` SET `locusId`='{0}', `seedId`='{1}', `isIdentical`='{2}' WHERE `locusId` = '{3}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, locusId)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, locusId, seedId, isIdentical)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, locusId, seedId, isIdentical)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, locusId, seedId, isIdentical, locusId)
    End Function
#End Region
End Class


End Namespace
