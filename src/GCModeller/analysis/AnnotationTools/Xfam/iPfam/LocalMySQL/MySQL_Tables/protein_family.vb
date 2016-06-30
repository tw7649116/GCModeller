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
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("protein_family")>
Public Class protein_family: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("auto_prot_fam"), PrimaryKey, AutoIncrement, NotNull, DataType(MySqlDbType.Int64, "11")> Public Property auto_prot_fam As Long
    <DatabaseField("accession"), DataType(MySqlDbType.VarChar, "45")> Public Property accession As String
    <DatabaseField("identifier"), DataType(MySqlDbType.VarChar, "45")> Public Property identifier As String
    <DatabaseField("description"), DataType(MySqlDbType.Text)> Public Property description As String
    <DatabaseField("comment"), DataType(MySqlDbType.Text)> Public Property comment As String
    <DatabaseField("type"), DataType(MySqlDbType.String)> Public Property type As String
    <DatabaseField("source_db"), DataType(MySqlDbType.String)> Public Property source_db As String
    <DatabaseField("colour"), DataType(MySqlDbType.VarChar, "7")> Public Property colour As String
    <DatabaseField("number_fam_int"), DataType(MySqlDbType.Int64, "5")> Public Property number_fam_int As Long
    <DatabaseField("number_lig_int"), DataType(MySqlDbType.Int64, "5")> Public Property number_lig_int As Long
    <DatabaseField("number_pdbs"), DataType(MySqlDbType.Int64, "5")> Public Property number_pdbs As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `protein_family` (`accession`, `identifier`, `description`, `comment`, `type`, `source_db`, `colour`, `number_fam_int`, `number_lig_int`, `number_pdbs`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `protein_family` (`accession`, `identifier`, `description`, `comment`, `type`, `source_db`, `colour`, `number_fam_int`, `number_lig_int`, `number_pdbs`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `protein_family` WHERE `auto_prot_fam` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `protein_family` SET `auto_prot_fam`='{0}', `accession`='{1}', `identifier`='{2}', `description`='{3}', `comment`='{4}', `type`='{5}', `source_db`='{6}', `colour`='{7}', `number_fam_int`='{8}', `number_lig_int`='{9}', `number_pdbs`='{10}' WHERE `auto_prot_fam` = '{11}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, auto_prot_fam)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, accession, identifier, description, comment, type, source_db, colour, number_fam_int, number_lig_int, number_pdbs)
    End Function
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, accession, identifier, description, comment, type, source_db, colour, number_fam_int, number_lig_int, number_pdbs)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, auto_prot_fam, accession, identifier, description, comment, type, source_db, colour, number_fam_int, number_lig_int, number_pdbs, auto_prot_fam)
    End Function
#End Region
End Class


End Namespace
