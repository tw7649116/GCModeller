﻿#Region "Microsoft.VisualBasic::954f74bd69183dc32433c386c963593c, ..\GCModeller\data\RegulonDatabase\Regtransbase\MySQL\obj_name_genomes.vb"

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
REM      for Microsoft VisualBasic.NET 

REM  Dump @12/3/2015 8:07:30 PM


Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes

Namespace Regtransbase.MySQL

''' <summary>
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `obj_name_genomes`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `obj_name_genomes` (
'''   `obj_guid` int(11) NOT NULL DEFAULT '0',
'''   `pkg_guid` int(11) NOT NULL DEFAULT '0',
'''   `art_guid` int(11) NOT NULL DEFAULT '0',
'''   `name` varchar(50) DEFAULT NULL,
'''   `genome_guid` int(11) DEFAULT NULL,
'''   `obj_type_id` int(11) NOT NULL DEFAULT '0',
'''   PRIMARY KEY (`obj_guid`),
'''   KEY `FK_obj_name_genomes-pkg_guid` (`pkg_guid`),
'''   KEY `FK_obj_name_genomes-art_guid` (`art_guid`),
'''   KEY `FK_obj_name_genomes-genome_guid` (`genome_guid`),
'''   CONSTRAINT `FK_obj_name_genomes-art_guid` FOREIGN KEY (`art_guid`) REFERENCES `articles` (`art_guid`),
'''   CONSTRAINT `FK_obj_name_genomes-genome_guid` FOREIGN KEY (`genome_guid`) REFERENCES `dict_genomes` (`genome_guid`),
'''   CONSTRAINT `FK_obj_name_genomes-pkg_guid` FOREIGN KEY (`pkg_guid`) REFERENCES `packages` (`pkg_guid`)
''' ) ENGINE=InnoDB DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("obj_name_genomes", Database:="dbregulation_update")>
Public Class obj_name_genomes: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("obj_guid"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "11")> Public Property obj_guid As Long
    <DatabaseField("pkg_guid"), NotNull, DataType(MySqlDbType.Int64, "11")> Public Property pkg_guid As Long
    <DatabaseField("art_guid"), NotNull, DataType(MySqlDbType.Int64, "11")> Public Property art_guid As Long
    <DatabaseField("name"), DataType(MySqlDbType.VarChar, "50")> Public Property name As String
    <DatabaseField("genome_guid"), DataType(MySqlDbType.Int64, "11")> Public Property genome_guid As Long
    <DatabaseField("obj_type_id"), NotNull, DataType(MySqlDbType.Int64, "11")> Public Property obj_type_id As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `obj_name_genomes` (`obj_guid`, `pkg_guid`, `art_guid`, `name`, `genome_guid`, `obj_type_id`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `obj_name_genomes` (`obj_guid`, `pkg_guid`, `art_guid`, `name`, `genome_guid`, `obj_type_id`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `obj_name_genomes` WHERE `obj_guid` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `obj_name_genomes` SET `obj_guid`='{0}', `pkg_guid`='{1}', `art_guid`='{2}', `name`='{3}', `genome_guid`='{4}', `obj_type_id`='{5}' WHERE `obj_guid` = '{6}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, obj_guid)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, obj_guid, pkg_guid, art_guid, name, genome_guid, obj_type_id)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, obj_guid, pkg_guid, art_guid, name, genome_guid, obj_type_id)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, obj_guid, pkg_guid, art_guid, name, genome_guid, obj_type_id, obj_guid)
    End Function
#End Region
End Class


End Namespace
