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
''' DROP TABLE IF EXISTS `keggcompound`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `keggcompound` (
'''   `compound` varchar(7) NOT NULL DEFAULT '',
'''   `name` tinytext,
'''   `formula` tinytext,
'''   `mass` double DEFAULT NULL,
'''   PRIMARY KEY (`compound`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("keggcompound")>
Public Class keggcompound: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("compound"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "7")> Public Property compound As String
    <DatabaseField("name"), DataType(MySqlDbType.Text)> Public Property name As String
    <DatabaseField("formula"), DataType(MySqlDbType.Text)> Public Property formula As String
    <DatabaseField("mass"), DataType(MySqlDbType.Double)> Public Property mass As Double
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `keggcompound` (`compound`, `name`, `formula`, `mass`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `keggcompound` (`compound`, `name`, `formula`, `mass`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `keggcompound` WHERE `compound` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `keggcompound` SET `compound`='{0}', `name`='{1}', `formula`='{2}', `mass`='{3}' WHERE `compound` = '{4}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, compound)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, compound, name, formula, mass)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, compound, name, formula, mass)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, compound, name, formula, mass, compound)
    End Function
#End Region
End Class


End Namespace
