REM  Oracle.LinuxCompatibility.MySQL.CodeGenerator
REM  Microsoft VisualBasic MYSQL

' SqlDump= 


' 

Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes

Namespace iPfam.LocalMySQL

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("pdb_protein_atom_int")>
Public Class pdb_protein_atom_int: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("auto_res_int"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "20")> Public Property auto_res_int As Long
    <DatabaseField("pdb_id"), DataType(MySqlDbType.VarChar, "4")> Public Property pdb_id As String
    <DatabaseField("atom_a"), NotNull, DataType(MySqlDbType.Int64, "11")> Public Property atom_a As Long
    <DatabaseField("atom_b"), NotNull, DataType(MySqlDbType.Int64, "11")> Public Property atom_b As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `pdb_protein_atom_int` (`auto_res_int`, `pdb_id`, `atom_a`, `atom_b`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `pdb_protein_atom_int` (`auto_res_int`, `pdb_id`, `atom_a`, `atom_b`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `pdb_protein_atom_int` WHERE `auto_res_int` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `pdb_protein_atom_int` SET `auto_res_int`='{0}', `pdb_id`='{1}', `atom_a`='{2}', `atom_b`='{3}' WHERE `auto_res_int` = '{4}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, auto_res_int)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, auto_res_int, pdb_id, atom_a, atom_b)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, auto_res_int, pdb_id, atom_a, atom_b)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, auto_res_int, pdb_id, atom_a, atom_b, auto_res_int)
    End Function
#End Region
End Class


End Namespace
