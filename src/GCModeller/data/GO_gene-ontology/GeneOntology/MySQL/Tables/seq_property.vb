﻿#Region "Microsoft.VisualBasic::4541e9c0a04e4063b3b2806e8f58bb92, ..\GCModeller\data\GO_gene-ontology\GeneOntology\MySQL\Tables\seq_property.vb"

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
''' DROP TABLE IF EXISTS `seq_property`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `seq_property` (
'''   `id` int(11) NOT NULL AUTO_INCREMENT,
'''   `seq_id` int(11) NOT NULL,
'''   `property_key` varchar(64) NOT NULL,
'''   `property_val` varchar(255) NOT NULL,
'''   PRIMARY KEY (`id`),
'''   UNIQUE KEY `seq_id` (`seq_id`,`property_key`,`property_val`),
'''   KEY `seqp0` (`seq_id`),
'''   KEY `seqp1` (`property_key`),
'''   KEY `seqp2` (`property_val`)
''' ) ENGINE=MyISAM DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' ```
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("seq_property", Database:="go", SchemaSQL:="
CREATE TABLE `seq_property` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `seq_id` int(11) NOT NULL,
  `property_key` varchar(64) NOT NULL,
  `property_val` varchar(255) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `seq_id` (`seq_id`,`property_key`,`property_val`),
  KEY `seqp0` (`seq_id`),
  KEY `seqp1` (`property_key`),
  KEY `seqp2` (`property_val`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;")>
Public Class seq_property: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("id"), PrimaryKey, AutoIncrement, NotNull, DataType(MySqlDbType.Int64, "11")> Public Property id As Long
    <DatabaseField("seq_id"), NotNull, DataType(MySqlDbType.Int64, "11")> Public Property seq_id As Long
    <DatabaseField("property_key"), NotNull, DataType(MySqlDbType.VarChar, "64")> Public Property property_key As String
    <DatabaseField("property_val"), NotNull, DataType(MySqlDbType.VarChar, "255")> Public Property property_val As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `seq_property` (`seq_id`, `property_key`, `property_val`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `seq_property` (`seq_id`, `property_key`, `property_val`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `seq_property` WHERE `id` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `seq_property` SET `id`='{0}', `seq_id`='{1}', `property_key`='{2}', `property_val`='{3}' WHERE `id` = '{4}';</SQL>
#End Region
''' <summary>
''' ```SQL
''' DELETE FROM `seq_property` WHERE `id` = '{0}';
''' ```
''' </summary>
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, id)
    End Function
''' <summary>
''' ```SQL
''' INSERT INTO `seq_property` (`seq_id`, `property_key`, `property_val`) VALUES ('{0}', '{1}', '{2}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, seq_id, property_key, property_val)
    End Function
''' <summary>
''' ```SQL
''' REPLACE INTO `seq_property` (`seq_id`, `property_key`, `property_val`) VALUES ('{0}', '{1}', '{2}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, seq_id, property_key, property_val)
    End Function
''' <summary>
''' ```SQL
''' UPDATE `seq_property` SET `id`='{0}', `seq_id`='{1}', `property_key`='{2}', `property_val`='{3}' WHERE `id` = '{4}';
''' ```
''' </summary>
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, id, seq_id, property_key, property_val, id)
    End Function
#End Region
End Class


End Namespace

