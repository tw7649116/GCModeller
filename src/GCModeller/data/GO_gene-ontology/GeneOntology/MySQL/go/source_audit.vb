﻿#Region "Microsoft.VisualBasic::f04070b1eebf220cd6a9535d3a3c01c9, ..\GCModeller\data\GO_gene-ontology\GeneOntology\MySQL\go\source_audit.vb"

    ' Author:
    ' 
    '       asuka (amethyst.asuka@gcmodeller.org)
    '       xieguigang (xie.guigang@live.com)
    '       xie (genetics@smrucc.org)
    ' 
    ' Copyright (c) 2016 GPL3 Licensed
    ' 
    ' 
    ' GNU GENERAL PUBLIC LICENSE (GPL3)
    ' 
    ' This program is free software: you can redistribute it and/or modify
    ' it under the terms of the GNU General Public License as published by
    ' the Free Software Foundation, either version 3 of the License, or
    ' (at your option) any later version.
    ' 
    ' This program is distributed in the hope that it will be useful,
    ' but WITHOUT ANY WARRANTY; without even the implied warranty of
    ' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    ' GNU General Public License for more details.
    ' 
    ' You should have received a copy of the GNU General Public License
    ' along with this program. If not, see <http://www.gnu.org/licenses/>.

#End Region

REM  Oracle.LinuxCompatibility.MySQL.CodeGenerator
REM  MYSQL Schema Mapper
REM      for Microsoft VisualBasic.NET 1.0.0.0

REM  Dump @9/5/2016 7:59:33 AM


Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes

Namespace MySQL.Tables

''' <summary>
''' ```SQL
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `source_audit`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `source_audit` (
'''   `source_id` varchar(255) DEFAULT NULL,
'''   `source_fullpath` varchar(255) DEFAULT NULL,
'''   `source_path` varchar(255) DEFAULT NULL,
'''   `source_type` varchar(255) DEFAULT NULL,
'''   `source_md5` char(32) DEFAULT NULL,
'''   `source_parsetime` int(11) DEFAULT NULL,
'''   `source_mtime` int(11) DEFAULT NULL,
'''   KEY `fa1` (`source_path`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' ```
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("source_audit", Database:="go", SchemaSQL:="
CREATE TABLE `source_audit` (
  `source_id` varchar(255) DEFAULT NULL,
  `source_fullpath` varchar(255) DEFAULT NULL,
  `source_path` varchar(255) DEFAULT NULL,
  `source_type` varchar(255) DEFAULT NULL,
  `source_md5` char(32) DEFAULT NULL,
  `source_parsetime` int(11) DEFAULT NULL,
  `source_mtime` int(11) DEFAULT NULL,
  KEY `fa1` (`source_path`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;")>
Public Class source_audit: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("source_id"), DataType(MySqlDbType.VarChar, "255")> Public Property source_id As String
    <DatabaseField("source_fullpath"), DataType(MySqlDbType.VarChar, "255")> Public Property source_fullpath As String
    <DatabaseField("source_path"), PrimaryKey, DataType(MySqlDbType.VarChar, "255")> Public Property source_path As String
    <DatabaseField("source_type"), DataType(MySqlDbType.VarChar, "255")> Public Property source_type As String
    <DatabaseField("source_md5"), DataType(MySqlDbType.VarChar, "32")> Public Property source_md5 As String
    <DatabaseField("source_parsetime"), DataType(MySqlDbType.Int64, "11")> Public Property source_parsetime As Long
    <DatabaseField("source_mtime"), DataType(MySqlDbType.Int64, "11")> Public Property source_mtime As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `source_audit` (`source_id`, `source_fullpath`, `source_path`, `source_type`, `source_md5`, `source_parsetime`, `source_mtime`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `source_audit` (`source_id`, `source_fullpath`, `source_path`, `source_type`, `source_md5`, `source_parsetime`, `source_mtime`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `source_audit` WHERE `source_path` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `source_audit` SET `source_id`='{0}', `source_fullpath`='{1}', `source_path`='{2}', `source_type`='{3}', `source_md5`='{4}', `source_parsetime`='{5}', `source_mtime`='{6}' WHERE `source_path` = '{7}';</SQL>
#End Region
''' <summary>
''' ```SQL
''' DELETE FROM `source_audit` WHERE `source_path` = '{0}';
''' ```
''' </summary>
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, source_path)
    End Function
''' <summary>
''' ```SQL
''' INSERT INTO `source_audit` (`source_id`, `source_fullpath`, `source_path`, `source_type`, `source_md5`, `source_parsetime`, `source_mtime`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, source_id, source_fullpath, source_path, source_type, source_md5, source_parsetime, source_mtime)
    End Function
''' <summary>
''' ```SQL
''' REPLACE INTO `source_audit` (`source_id`, `source_fullpath`, `source_path`, `source_type`, `source_md5`, `source_parsetime`, `source_mtime`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, source_id, source_fullpath, source_path, source_type, source_md5, source_parsetime, source_mtime)
    End Function
''' <summary>
''' ```SQL
''' UPDATE `source_audit` SET `source_id`='{0}', `source_fullpath`='{1}', `source_path`='{2}', `source_type`='{3}', `source_md5`='{4}', `source_parsetime`='{5}', `source_mtime`='{6}' WHERE `source_path` = '{7}';
''' ```
''' </summary>
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, source_id, source_fullpath, source_path, source_type, source_md5, source_parsetime, source_mtime, source_path)
    End Function
#End Region
End Class


End Namespace

