﻿#Region "Microsoft.VisualBasic::647bf4f9d5aa32c1d9d5a2d4b219d8eb, ..\GCModeller\analysis\annoTools\DataMySql\Xfam\Rfam\Tables\clan_database_link.vb"

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
REM  Microsoft VisualBasic MYSQL

' SqlDump= 


' 

Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes

Namespace Xfam.Rfam.MySQL.Tables

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("clan_database_link")>
Public Class clan_database_link: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("clan_acc"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "7")> Public Property clan_acc As String
    <DatabaseField("db_id"), NotNull, DataType(MySqlDbType.Text)> Public Property db_id As String
    <DatabaseField("comment"), DataType(MySqlDbType.Text)> Public Property comment As String
    <DatabaseField("db_link"), NotNull, DataType(MySqlDbType.Text)> Public Property db_link As String
    <DatabaseField("other_params"), DataType(MySqlDbType.Text)> Public Property other_params As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `clan_database_link` (`clan_acc`, `db_id`, `comment`, `db_link`, `other_params`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `clan_database_link` (`clan_acc`, `db_id`, `comment`, `db_link`, `other_params`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `clan_database_link` WHERE `clan_acc` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `clan_database_link` SET `clan_acc`='{0}', `db_id`='{1}', `comment`='{2}', `db_link`='{3}', `other_params`='{4}' WHERE `clan_acc` = '{5}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, clan_acc)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, clan_acc, db_id, comment, db_link, other_params)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, clan_acc, db_id, comment, db_link, other_params)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, clan_acc, db_id, comment, db_link, other_params, clan_acc)
    End Function
#End Region
End Class


End Namespace
