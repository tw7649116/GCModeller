REM  Oracle.LinuxCompatibility.MySQL.CodeGenerator
REM  MYSQL Schema Mapper
REM      for Microsoft VisualBasic.NET 

REM  Dump @12/3/2015 8:20:41 PM


Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes

Namespace Wiki.MySQL

''' <summary>
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `lib_wikipage_restrictions`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `lib_wikipage_restrictions` (
'''   `pr_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
'''   `pr_page` int(11) NOT NULL,
'''   `pr_type` varbinary(60) NOT NULL,
'''   `pr_level` varbinary(60) NOT NULL,
'''   `pr_cascade` tinyint(4) NOT NULL,
'''   `pr_user` int(11) DEFAULT NULL,
'''   `pr_expiry` varbinary(14) DEFAULT NULL,
'''   PRIMARY KEY (`pr_id`),
'''   UNIQUE KEY `pr_pagetype` (`pr_page`,`pr_type`),
'''   KEY `pr_typelevel` (`pr_type`,`pr_level`),
'''   KEY `pr_level` (`pr_level`),
'''   KEY `pr_cascade` (`pr_cascade`)
''' ) ENGINE=InnoDB DEFAULT CHARSET=binary;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("lib_wikipage_restrictions", Database:="wiki")>
Public Class lib_wikipage_restrictions: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("pr_id"), PrimaryKey, AutoIncrement, NotNull, DataType(MySqlDbType.Int64, "10")> Public Property pr_id As Long
    <DatabaseField("pr_page"), NotNull, DataType(MySqlDbType.Int64, "11")> Public Property pr_page As Long
    <DatabaseField("pr_type"), NotNull, DataType(MySqlDbType.Blob)> Public Property pr_type As Byte()
    <DatabaseField("pr_level"), NotNull, DataType(MySqlDbType.Blob)> Public Property pr_level As Byte()
    <DatabaseField("pr_cascade"), NotNull, DataType(MySqlDbType.Int64, "4")> Public Property pr_cascade As Long
    <DatabaseField("pr_user"), DataType(MySqlDbType.Int64, "11")> Public Property pr_user As Long
    <DatabaseField("pr_expiry"), DataType(MySqlDbType.Blob)> Public Property pr_expiry As Byte()
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `lib_wikipage_restrictions` (`pr_page`, `pr_type`, `pr_level`, `pr_cascade`, `pr_user`, `pr_expiry`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `lib_wikipage_restrictions` (`pr_page`, `pr_type`, `pr_level`, `pr_cascade`, `pr_user`, `pr_expiry`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `lib_wikipage_restrictions` WHERE `pr_id` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `lib_wikipage_restrictions` SET `pr_id`='{0}', `pr_page`='{1}', `pr_type`='{2}', `pr_level`='{3}', `pr_cascade`='{4}', `pr_user`='{5}', `pr_expiry`='{6}' WHERE `pr_id` = '{7}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, pr_id)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, pr_page, pr_type, pr_level, pr_cascade, pr_user, pr_expiry)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, pr_page, pr_type, pr_level, pr_cascade, pr_user, pr_expiry)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, pr_id, pr_page, pr_type, pr_level, pr_cascade, pr_user, pr_expiry, pr_id)
    End Function
#End Region
End Class


End Namespace
