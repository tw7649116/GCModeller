﻿#Region "Microsoft.VisualBasic::af2e581fa9a6f1053134c9facd31bc1e, ..\GCModeller\data\GO_gene-ontology\GeneOntology\MySQL\go\assoc_rel.vb"

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
''' DROP TABLE IF EXISTS `assoc_rel`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `assoc_rel` (
'''   `id` int(11) NOT NULL AUTO_INCREMENT,
'''   `from_id` int(11) NOT NULL,
'''   `to_id` int(11) NOT NULL,
'''   `relationship_type_id` int(11) NOT NULL,
'''   PRIMARY KEY (`id`),
'''   KEY `from_id` (`from_id`),
'''   KEY `to_id` (`to_id`),
'''   KEY `relationship_type_id` (`relationship_type_id`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' ```
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("assoc_rel", Database:="go", SchemaSQL:="
CREATE TABLE `assoc_rel` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `from_id` int(11) NOT NULL,
  `to_id` int(11) NOT NULL,
  `relationship_type_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `from_id` (`from_id`),
  KEY `to_id` (`to_id`),
  KEY `relationship_type_id` (`relationship_type_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;")>
Public Class assoc_rel: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("id"), PrimaryKey, AutoIncrement, NotNull, DataType(MySqlDbType.Int64, "11")> Public Property id As Long
    <DatabaseField("from_id"), NotNull, DataType(MySqlDbType.Int64, "11")> Public Property from_id As Long
    <DatabaseField("to_id"), NotNull, DataType(MySqlDbType.Int64, "11")> Public Property to_id As Long
    <DatabaseField("relationship_type_id"), NotNull, DataType(MySqlDbType.Int64, "11")> Public Property relationship_type_id As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `assoc_rel` (`from_id`, `to_id`, `relationship_type_id`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `assoc_rel` (`from_id`, `to_id`, `relationship_type_id`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `assoc_rel` WHERE `id` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `assoc_rel` SET `id`='{0}', `from_id`='{1}', `to_id`='{2}', `relationship_type_id`='{3}' WHERE `id` = '{4}';</SQL>
#End Region
''' <summary>
''' ```SQL
''' DELETE FROM `assoc_rel` WHERE `id` = '{0}';
''' ```
''' </summary>
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, id)
    End Function
''' <summary>
''' ```SQL
''' INSERT INTO `assoc_rel` (`from_id`, `to_id`, `relationship_type_id`) VALUES ('{0}', '{1}', '{2}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, from_id, to_id, relationship_type_id)
    End Function
''' <summary>
''' ```SQL
''' REPLACE INTO `assoc_rel` (`from_id`, `to_id`, `relationship_type_id`) VALUES ('{0}', '{1}', '{2}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, from_id, to_id, relationship_type_id)
    End Function
''' <summary>
''' ```SQL
''' UPDATE `assoc_rel` SET `id`='{0}', `from_id`='{1}', `to_id`='{2}', `relationship_type_id`='{3}' WHERE `id` = '{4}';
''' ```
''' </summary>
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, id, from_id, to_id, relationship_type_id, id)
    End Function
#End Region
End Class


End Namespace

