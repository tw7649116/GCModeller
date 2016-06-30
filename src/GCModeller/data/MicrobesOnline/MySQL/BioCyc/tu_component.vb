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
''' DROP TABLE IF EXISTS `tu_component`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `tu_component` (
'''   `tuId` varchar(100) NOT NULL,
'''   `cType` varchar(20) NOT NULL,
'''   `cId` varchar(100) NOT NULL,
'''   KEY `tuId` (`tuId`),
'''   KEY `cId` (`cId`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("tu_component", Database:="biocyc")>
Public Class tu_component: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("tuId"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "100")> Public Property tuId As String
    <DatabaseField("cType"), NotNull, DataType(MySqlDbType.VarChar, "20")> Public Property [cType] As String
    <DatabaseField("cId"), NotNull, DataType(MySqlDbType.VarChar, "100")> Public Property cId As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `tu_component` (`tuId`, `cType`, `cId`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `tu_component` (`tuId`, `cType`, `cId`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `tu_component` WHERE `tuId` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `tu_component` SET `tuId`='{0}', `cType`='{1}', `cId`='{2}' WHERE `tuId` = '{3}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, tuId)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, tuId, [cType], cId)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, tuId, [cType], cId)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, tuId, [cType], cId, tuId)
    End Function
#End Region
End Class


End Namespace
