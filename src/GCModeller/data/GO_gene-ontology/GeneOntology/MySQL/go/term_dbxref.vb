﻿#Region "Microsoft.VisualBasic::18490b4b1705572cf81ea836b7545961, ..\GCModeller\data\GO_gene-ontology\GeneOntology\MySQL\go\term_dbxref.vb"

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
''' DROP TABLE IF EXISTS `term_dbxref`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `term_dbxref` (
'''   `term_id` int(11) NOT NULL,
'''   `dbxref_id` int(11) NOT NULL,
'''   `is_for_definition` int(11) NOT NULL DEFAULT '0',
'''   UNIQUE KEY `term_id` (`term_id`,`dbxref_id`,`is_for_definition`),
'''   KEY `tx0` (`term_id`),
'''   KEY `tx1` (`dbxref_id`),
'''   KEY `tx2` (`term_id`,`dbxref_id`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' ```
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("term_dbxref", Database:="go", SchemaSQL:="
CREATE TABLE `term_dbxref` (
  `term_id` int(11) NOT NULL,
  `dbxref_id` int(11) NOT NULL,
  `is_for_definition` int(11) NOT NULL DEFAULT '0',
  UNIQUE KEY `term_id` (`term_id`,`dbxref_id`,`is_for_definition`),
  KEY `tx0` (`term_id`),
  KEY `tx1` (`dbxref_id`),
  KEY `tx2` (`term_id`,`dbxref_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;")>
Public Class term_dbxref: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("term_id"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "11")> Public Property term_id As Long
    <DatabaseField("dbxref_id"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "11")> Public Property dbxref_id As Long
    <DatabaseField("is_for_definition"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "11")> Public Property is_for_definition As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `term_dbxref` (`term_id`, `dbxref_id`, `is_for_definition`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `term_dbxref` (`term_id`, `dbxref_id`, `is_for_definition`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `term_dbxref` WHERE `term_id`='{0}' and `dbxref_id`='{1}' and `is_for_definition`='{2}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `term_dbxref` SET `term_id`='{0}', `dbxref_id`='{1}', `is_for_definition`='{2}' WHERE `term_id`='{3}' and `dbxref_id`='{4}' and `is_for_definition`='{5}';</SQL>
#End Region
''' <summary>
''' ```SQL
''' DELETE FROM `term_dbxref` WHERE `term_id`='{0}' and `dbxref_id`='{1}' and `is_for_definition`='{2}';
''' ```
''' </summary>
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, term_id, dbxref_id, is_for_definition)
    End Function
''' <summary>
''' ```SQL
''' INSERT INTO `term_dbxref` (`term_id`, `dbxref_id`, `is_for_definition`) VALUES ('{0}', '{1}', '{2}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, term_id, dbxref_id, is_for_definition)
    End Function
''' <summary>
''' ```SQL
''' REPLACE INTO `term_dbxref` (`term_id`, `dbxref_id`, `is_for_definition`) VALUES ('{0}', '{1}', '{2}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, term_id, dbxref_id, is_for_definition)
    End Function
''' <summary>
''' ```SQL
''' UPDATE `term_dbxref` SET `term_id`='{0}', `dbxref_id`='{1}', `is_for_definition`='{2}' WHERE `term_id`='{3}' and `dbxref_id`='{4}' and `is_for_definition`='{5}';
''' ```
''' </summary>
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, term_id, dbxref_id, is_for_definition, term_id, dbxref_id, is_for_definition)
    End Function
#End Region
End Class


End Namespace

