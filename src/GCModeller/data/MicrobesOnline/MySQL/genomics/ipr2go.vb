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
''' DROP TABLE IF EXISTS `ipr2go`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `ipr2go` (
'''   `iprId` varchar(10) NOT NULL DEFAULT '',
'''   `goId` varchar(10) NOT NULL DEFAULT '',
'''   PRIMARY KEY (`iprId`,`goId`),
'''   KEY `iprId` (`iprId`),
'''   KEY `goId` (`goId`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("ipr2go")>
Public Class ipr2go: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("iprId"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "10")> Public Property iprId As String
    <DatabaseField("goId"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "10")> Public Property goId As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `ipr2go` (`iprId`, `goId`) VALUES ('{0}', '{1}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `ipr2go` (`iprId`, `goId`) VALUES ('{0}', '{1}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `ipr2go` WHERE `iprId`='{0}' and `goId`='{1}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `ipr2go` SET `iprId`='{0}', `goId`='{1}' WHERE `iprId`='{2}' and `goId`='{3}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, iprId, goId)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, iprId, goId)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, iprId, goId)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, iprId, goId, iprId, goId)
    End Function
#End Region
End Class


End Namespace
