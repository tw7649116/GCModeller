REM  Oracle.LinuxCompatibility.MySQL.CodeGenerator
REM  MYSQL Schema Mapper
REM      for Microsoft VisualBasic.NET 

REM  Dump @12/3/2015 8:32:12 PM


Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes

Namespace MySQL.BioCyc

''' <summary>
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `position`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `position` (
'''   `taxId` int(20) unsigned NOT NULL,
'''   `objectId` varchar(100) NOT NULL,
'''   `posLeft` int(50) unsigned DEFAULT NULL,
'''   `posRight` int(50) unsigned DEFAULT NULL,
'''   `strand` varchar(4) DEFAULT NULL,
'''   PRIMARY KEY (`objectId`),
'''   KEY `taxId` (`taxId`),
'''   KEY `posLeft` (`posLeft`),
'''   KEY `posRight` (`posRight`),
'''   KEY `strand` (`strand`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("position", Database:="biocyc")>
Public Class position: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("taxId"), NotNull, DataType(MySqlDbType.Int64, "20")> Public Property taxId As Long
    <DatabaseField("objectId"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "100")> Public Property objectId As String
    <DatabaseField("posLeft"), DataType(MySqlDbType.Int64, "50")> Public Property posLeft As Long
    <DatabaseField("posRight"), DataType(MySqlDbType.Int64, "50")> Public Property posRight As Long
    <DatabaseField("strand"), DataType(MySqlDbType.VarChar, "4")> Public Property strand As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `position` (`taxId`, `objectId`, `posLeft`, `posRight`, `strand`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `position` (`taxId`, `objectId`, `posLeft`, `posRight`, `strand`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `position` WHERE `objectId` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `position` SET `taxId`='{0}', `objectId`='{1}', `posLeft`='{2}', `posRight`='{3}', `strand`='{4}' WHERE `objectId` = '{5}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, objectId)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, taxId, objectId, posLeft, posRight, strand)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, taxId, objectId, posLeft, posRight, strand)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, taxId, objectId, posLeft, posRight, strand, objectId)
    End Function
#End Region
End Class


End Namespace
