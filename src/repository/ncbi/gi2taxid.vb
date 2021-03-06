REM  Oracle.LinuxCompatibility.MySQL.CodeGenerator
REM  MYSQL Schema Mapper
REM      for Microsoft VisualBasic.NET 1.0.0.0

REM  Dump @2016/10/4 20:02:15


Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes

Namespace mysql.NCBI

''' <summary>
''' ```SQL
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `gi2taxid`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `gi2taxid` (
'''   `gi` int(11) NOT NULL,
'''   `taxid` int(11) NOT NULL,
'''   PRIMARY KEY (`gi`,`taxid`)
''' ) ENGINE=InnoDB DEFAULT CHARSET=utf8;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' ```
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("gi2taxid", Database:="ncbi", SchemaSQL:="
CREATE TABLE `gi2taxid` (
  `gi` int(11) NOT NULL,
  `taxid` int(11) NOT NULL,
  PRIMARY KEY (`gi`,`taxid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;")>
Public Class gi2taxid: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("gi"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "11")> Public Property gi As Long
    <DatabaseField("taxid"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "11")> Public Property taxid As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `gi2taxid` (`gi`, `taxid`) VALUES ('{0}', '{1}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `gi2taxid` (`gi`, `taxid`) VALUES ('{0}', '{1}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `gi2taxid` WHERE `gi`='{0}' and `taxid`='{1}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `gi2taxid` SET `gi`='{0}', `taxid`='{1}' WHERE `gi`='{2}' and `taxid`='{3}';</SQL>
#End Region
''' <summary>
''' ```SQL
''' DELETE FROM `gi2taxid` WHERE `gi`='{0}' and `taxid`='{1}';
''' ```
''' </summary>
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, gi, taxid)
    End Function
''' <summary>
''' ```SQL
''' INSERT INTO `gi2taxid` (`gi`, `taxid`) VALUES ('{0}', '{1}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, gi, taxid)
    End Function
''' <summary>
''' ```SQL
''' REPLACE INTO `gi2taxid` (`gi`, `taxid`) VALUES ('{0}', '{1}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, gi, taxid)
    End Function
''' <summary>
''' ```SQL
''' UPDATE `gi2taxid` SET `gi`='{0}', `taxid`='{1}' WHERE `gi`='{2}' and `taxid`='{3}';
''' ```
''' </summary>
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, gi, taxid, gi, taxid)
    End Function
#End Region
End Class


End Namespace
