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
''' DROP TABLE IF EXISTS `mog`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `mog` (
'''   `mogId` int(10) unsigned NOT NULL DEFAULT '0',
'''   `nComponents` int(10) unsigned NOT NULL DEFAULT '0',
'''   `nLoci` int(10) unsigned NOT NULL DEFAULT '0',
'''   `metric` float NOT NULL DEFAULT '0',
'''   PRIMARY KEY (`mogId`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("mog")>
Public Class mog: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("mogId"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "10")> Public Property mogId As Long
    <DatabaseField("nComponents"), NotNull, DataType(MySqlDbType.Int64, "10")> Public Property nComponents As Long
    <DatabaseField("nLoci"), NotNull, DataType(MySqlDbType.Int64, "10")> Public Property nLoci As Long
    <DatabaseField("metric"), NotNull, DataType(MySqlDbType.Double)> Public Property metric As Double
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `mog` (`mogId`, `nComponents`, `nLoci`, `metric`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `mog` (`mogId`, `nComponents`, `nLoci`, `metric`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `mog` WHERE `mogId` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `mog` SET `mogId`='{0}', `nComponents`='{1}', `nLoci`='{2}', `metric`='{3}' WHERE `mogId` = '{4}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, mogId)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, mogId, nComponents, nLoci, metric)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, mogId, nComponents, nLoci, metric)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, mogId, nComponents, nLoci, metric, mogId)
    End Function
#End Region
End Class


End Namespace
