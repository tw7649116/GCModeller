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
''' DROP TABLE IF EXISTS `domaininfo`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `domaininfo` (
'''   `domainDb` varchar(20) NOT NULL DEFAULT '',
'''   `domainId` varchar(20) NOT NULL DEFAULT '',
'''   `domainName` varchar(50) NOT NULL DEFAULT '',
'''   `iprId` varchar(10) DEFAULT NULL,
'''   `iprName` varchar(100) DEFAULT NULL,
'''   `domainLen` int(5) unsigned DEFAULT NULL,
'''   `fileName` varchar(100) DEFAULT NULL,
'''   PRIMARY KEY (`domainDb`,`domainId`),
'''   KEY `domainId` (`domainId`),
'''   KEY `iprId` (`iprId`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("domaininfo")>
Public Class domaininfo: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("domainDb"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "20")> Public Property domainDb As String
    <DatabaseField("domainId"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "20")> Public Property domainId As String
    <DatabaseField("domainName"), NotNull, DataType(MySqlDbType.VarChar, "50")> Public Property domainName As String
    <DatabaseField("iprId"), DataType(MySqlDbType.VarChar, "10")> Public Property iprId As String
    <DatabaseField("iprName"), DataType(MySqlDbType.VarChar, "100")> Public Property iprName As String
    <DatabaseField("domainLen"), DataType(MySqlDbType.Int64, "5")> Public Property domainLen As Long
    <DatabaseField("fileName"), DataType(MySqlDbType.VarChar, "100")> Public Property fileName As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `domaininfo` (`domainDb`, `domainId`, `domainName`, `iprId`, `iprName`, `domainLen`, `fileName`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `domaininfo` (`domainDb`, `domainId`, `domainName`, `iprId`, `iprName`, `domainLen`, `fileName`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `domaininfo` WHERE `domainDb`='{0}' and `domainId`='{1}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `domaininfo` SET `domainDb`='{0}', `domainId`='{1}', `domainName`='{2}', `iprId`='{3}', `iprName`='{4}', `domainLen`='{5}', `fileName`='{6}' WHERE `domainDb`='{7}' and `domainId`='{8}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, domainDb, domainId)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, domainDb, domainId, domainName, iprId, iprName, domainLen, fileName)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, domainDb, domainId, domainName, iprId, iprName, domainLen, fileName)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, domainDb, domainId, domainName, iprId, iprName, domainLen, fileName, domainDb, domainId)
    End Function
#End Region
End Class


End Namespace
