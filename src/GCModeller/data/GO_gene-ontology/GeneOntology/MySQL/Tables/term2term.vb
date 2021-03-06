﻿#Region "Microsoft.VisualBasic::de83a240a054845871f13aa421e47318, ..\GCModeller\data\GO_gene-ontology\GeneOntology\MySQL\Tables\term2term.vb"

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
''' DROP TABLE IF EXISTS `term2term`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `term2term` (
'''   `id` int(11) NOT NULL AUTO_INCREMENT,
'''   `relationship_type_id` int(11) NOT NULL,
'''   `term1_id` int(11) NOT NULL,
'''   `term2_id` int(11) NOT NULL,
'''   `complete` int(11) NOT NULL DEFAULT '0',
'''   PRIMARY KEY (`id`),
'''   UNIQUE KEY `term1_id` (`term1_id`,`term2_id`,`relationship_type_id`),
'''   KEY `tt1` (`term1_id`),
'''   KEY `tt2` (`term2_id`),
'''   KEY `tt3` (`term1_id`,`term2_id`),
'''   KEY `tt4` (`relationship_type_id`)
''' ) ENGINE=MyISAM AUTO_INCREMENT=89342 DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' ```
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("term2term", Database:="go", SchemaSQL:="
CREATE TABLE `term2term` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `relationship_type_id` int(11) NOT NULL,
  `term1_id` int(11) NOT NULL,
  `term2_id` int(11) NOT NULL,
  `complete` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `term1_id` (`term1_id`,`term2_id`,`relationship_type_id`),
  KEY `tt1` (`term1_id`),
  KEY `tt2` (`term2_id`),
  KEY `tt3` (`term1_id`,`term2_id`),
  KEY `tt4` (`relationship_type_id`)
) ENGINE=MyISAM AUTO_INCREMENT=89342 DEFAULT CHARSET=latin1;")>
Public Class term2term: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("id"), PrimaryKey, AutoIncrement, NotNull, DataType(MySqlDbType.Int64, "11")> Public Property id As Long
    <DatabaseField("relationship_type_id"), NotNull, DataType(MySqlDbType.Int64, "11")> Public Property relationship_type_id As Long
    <DatabaseField("term1_id"), NotNull, DataType(MySqlDbType.Int64, "11")> Public Property term1_id As Long
    <DatabaseField("term2_id"), NotNull, DataType(MySqlDbType.Int64, "11")> Public Property term2_id As Long
    <DatabaseField("complete"), NotNull, DataType(MySqlDbType.Int64, "11")> Public Property complete As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `term2term` (`relationship_type_id`, `term1_id`, `term2_id`, `complete`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `term2term` (`relationship_type_id`, `term1_id`, `term2_id`, `complete`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `term2term` WHERE `id` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `term2term` SET `id`='{0}', `relationship_type_id`='{1}', `term1_id`='{2}', `term2_id`='{3}', `complete`='{4}' WHERE `id` = '{5}';</SQL>
#End Region
''' <summary>
''' ```SQL
''' DELETE FROM `term2term` WHERE `id` = '{0}';
''' ```
''' </summary>
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, id)
    End Function
''' <summary>
''' ```SQL
''' INSERT INTO `term2term` (`relationship_type_id`, `term1_id`, `term2_id`, `complete`) VALUES ('{0}', '{1}', '{2}', '{3}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, relationship_type_id, term1_id, term2_id, complete)
    End Function
''' <summary>
''' ```SQL
''' REPLACE INTO `term2term` (`relationship_type_id`, `term1_id`, `term2_id`, `complete`) VALUES ('{0}', '{1}', '{2}', '{3}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, relationship_type_id, term1_id, term2_id, complete)
    End Function
''' <summary>
''' ```SQL
''' UPDATE `term2term` SET `id`='{0}', `relationship_type_id`='{1}', `term1_id`='{2}', `term2_id`='{3}', `complete`='{4}' WHERE `id` = '{5}';
''' ```
''' </summary>
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, id, relationship_type_id, term1_id, term2_id, complete, id)
    End Function
#End Region
End Class


End Namespace

