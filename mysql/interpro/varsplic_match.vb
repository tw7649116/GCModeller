REM  Oracle.LinuxCompatibility.MySQL.CodeGenerator
REM  Microsoft VisualBasic MYSQL




Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes

Namespace Interpro.Tables

''' <summary>
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `varsplic_match`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `varsplic_match` (
'''   `protein_ac` varchar(12) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
'''   `method_ac` varchar(25) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
'''   `pos_from` int(5) DEFAULT NULL,
'''   `pos_to` int(5) DEFAULT NULL,
'''   `status` char(1) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
'''   `dbcode` char(1) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
'''   `evidence` char(3) CHARACTER SET latin1 COLLATE latin1_bin DEFAULT NULL,
'''   `seq_date` datetime NOT NULL,
'''   `match_date` datetime NOT NULL,
'''   `score` double DEFAULT NULL,
'''   KEY `fk_varsplic_match$dbcode` (`dbcode`),
'''   KEY `fk_varsplic_match$evidence` (`evidence`),
'''   KEY `fk_varsplic_match$method` (`method_ac`),
'''   CONSTRAINT `fk_varsplic_match$dbcode` FOREIGN KEY (`dbcode`) REFERENCES `cv_database` (`dbcode`) ON DELETE NO ACTION ON UPDATE NO ACTION,
'''   CONSTRAINT `fk_varsplic_match$evidence` FOREIGN KEY (`evidence`) REFERENCES `cv_evidence` (`code`) ON DELETE NO ACTION ON UPDATE NO ACTION,
'''   CONSTRAINT `fk_varsplic_match$method` FOREIGN KEY (`method_ac`) REFERENCES `method` (`method_ac`) ON DELETE CASCADE ON UPDATE NO ACTION
''' ) ENGINE=InnoDB DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("varsplic_match", Database:="interpro")>
Public Class varsplic_match: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("protein_ac"), NotNull, DataType(MySqlDbType.VarChar, "12")> Public Property protein_ac As String
    <DatabaseField("method_ac"), NotNull, DataType(MySqlDbType.VarChar, "25")> Public Property method_ac As String
    <DatabaseField("pos_from"), DataType(MySqlDbType.Int64, "5")> Public Property pos_from As Long
    <DatabaseField("pos_to"), DataType(MySqlDbType.Int64, "5")> Public Property pos_to As Long
    <DatabaseField("status"), NotNull, DataType(MySqlDbType.VarChar, "1")> Public Property status As String
    <DatabaseField("dbcode"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "1")> Public Property dbcode As String
    <DatabaseField("evidence"), DataType(MySqlDbType.VarChar, "3")> Public Property evidence As String
    <DatabaseField("seq_date"), NotNull, DataType(MySqlDbType.DateTime)> Public Property seq_date As Date
    <DatabaseField("match_date"), NotNull, DataType(MySqlDbType.DateTime)> Public Property match_date As Date
    <DatabaseField("score"), DataType(MySqlDbType.Double)> Public Property score As Double
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `varsplic_match` (`protein_ac`, `method_ac`, `pos_from`, `pos_to`, `status`, `dbcode`, `evidence`, `seq_date`, `match_date`, `score`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `varsplic_match` WHERE `dbcode` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `varsplic_match` SET `protein_ac`='{0}', `method_ac`='{1}', `pos_from`='{2}', `pos_to`='{3}', `status`='{4}', `dbcode`='{5}', `evidence`='{6}', `seq_date`='{7}', `match_date`='{8}', `score`='{9}' WHERE `dbcode` = '{10}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, dbcode)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, protein_ac, method_ac, pos_from, pos_to, status, dbcode, evidence, DataType.ToMySqlDateTimeString(seq_date), DataType.ToMySqlDateTimeString(match_date), score)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, protein_ac, method_ac, pos_from, pos_to, status, dbcode, evidence, DataType.ToMySqlDateTimeString(seq_date), DataType.ToMySqlDateTimeString(match_date), score, dbcode)
    End Function
#End Region
End Class


End Namespace
