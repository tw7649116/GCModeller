﻿#Region "Microsoft.VisualBasic::3a69373563e38712bb3f48cc5db6e55f, ..\GCModeller\data\GO_gene-ontology\GeneOntology\MySQL\go\evidence_dbxref.vb"

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
''' DROP TABLE IF EXISTS `evidence_dbxref`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `evidence_dbxref` (
'''   `evidence_id` int(11) NOT NULL,
'''   `dbxref_id` int(11) NOT NULL,
'''   KEY `evx1` (`evidence_id`),
'''   KEY `evx2` (`dbxref_id`),
'''   KEY `evx3` (`evidence_id`,`dbxref_id`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' ```
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("evidence_dbxref", Database:="go", SchemaSQL:="
CREATE TABLE `evidence_dbxref` (
  `evidence_id` int(11) NOT NULL,
  `dbxref_id` int(11) NOT NULL,
  KEY `evx1` (`evidence_id`),
  KEY `evx2` (`dbxref_id`),
  KEY `evx3` (`evidence_id`,`dbxref_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;")>
Public Class evidence_dbxref: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("evidence_id"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "11")> Public Property evidence_id As Long
    <DatabaseField("dbxref_id"), NotNull, DataType(MySqlDbType.Int64, "11")> Public Property dbxref_id As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `evidence_dbxref` (`evidence_id`, `dbxref_id`) VALUES ('{0}', '{1}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `evidence_dbxref` (`evidence_id`, `dbxref_id`) VALUES ('{0}', '{1}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `evidence_dbxref` WHERE `evidence_id` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `evidence_dbxref` SET `evidence_id`='{0}', `dbxref_id`='{1}' WHERE `evidence_id` = '{2}';</SQL>
#End Region
''' <summary>
''' ```SQL
''' DELETE FROM `evidence_dbxref` WHERE `evidence_id` = '{0}';
''' ```
''' </summary>
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, evidence_id)
    End Function
''' <summary>
''' ```SQL
''' INSERT INTO `evidence_dbxref` (`evidence_id`, `dbxref_id`) VALUES ('{0}', '{1}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, evidence_id, dbxref_id)
    End Function
''' <summary>
''' ```SQL
''' REPLACE INTO `evidence_dbxref` (`evidence_id`, `dbxref_id`) VALUES ('{0}', '{1}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, evidence_id, dbxref_id)
    End Function
''' <summary>
''' ```SQL
''' UPDATE `evidence_dbxref` SET `evidence_id`='{0}', `dbxref_id`='{1}' WHERE `evidence_id` = '{2}';
''' ```
''' </summary>
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, evidence_id, dbxref_id, evidence_id)
    End Function
#End Region
End Class


End Namespace

