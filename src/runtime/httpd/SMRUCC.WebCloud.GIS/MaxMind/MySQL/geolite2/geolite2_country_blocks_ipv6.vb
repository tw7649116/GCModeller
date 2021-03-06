REM  Oracle.LinuxCompatibility.MySQL.CodeGenerator
REM  MYSQL Schema Mapper
REM      for Microsoft VisualBasic.NET 1.0.0.0

REM  Dump @9/4/2016 5:29:45 PM


Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes

Namespace MaxMind.geolite2

''' <summary>
''' ```SQL
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `geolite2_country_blocks_ipv6`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `geolite2_country_blocks_ipv6` (
'''   `network` varchar(128) NOT NULL,
'''   `geoname_id` varchar(45) DEFAULT NULL,
'''   `registered_country_geoname_id` varchar(45) DEFAULT NULL,
'''   `represented_country_geoname_id` varchar(45) DEFAULT NULL,
'''   `is_anonymous_proxy` varchar(45) DEFAULT NULL,
'''   `is_satellite_provider` varchar(45) DEFAULT NULL,
'''   PRIMARY KEY (`network`),
'''   UNIQUE KEY `network_UNIQUE` (`network`)
''' ) ENGINE=InnoDB DEFAULT CHARSET=utf8;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' ```
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("geolite2_country_blocks_ipv6", Database:="maxmind_geolite2", SchemaSQL:="
CREATE TABLE `geolite2_country_blocks_ipv6` (
  `network` varchar(128) NOT NULL,
  `geoname_id` varchar(45) DEFAULT NULL,
  `registered_country_geoname_id` varchar(45) DEFAULT NULL,
  `represented_country_geoname_id` varchar(45) DEFAULT NULL,
  `is_anonymous_proxy` varchar(45) DEFAULT NULL,
  `is_satellite_provider` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`network`),
  UNIQUE KEY `network_UNIQUE` (`network`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;")>
Public Class geolite2_country_blocks_ipv6: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("network"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "128")> Public Property network As String
    <DatabaseField("geoname_id"), DataType(MySqlDbType.VarChar, "45")> Public Property geoname_id As String
    <DatabaseField("registered_country_geoname_id"), DataType(MySqlDbType.VarChar, "45")> Public Property registered_country_geoname_id As String
    <DatabaseField("represented_country_geoname_id"), DataType(MySqlDbType.VarChar, "45")> Public Property represented_country_geoname_id As String
    <DatabaseField("is_anonymous_proxy"), DataType(MySqlDbType.VarChar, "45")> Public Property is_anonymous_proxy As String
    <DatabaseField("is_satellite_provider"), DataType(MySqlDbType.VarChar, "45")> Public Property is_satellite_provider As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `geolite2_country_blocks_ipv6` (`network`, `geoname_id`, `registered_country_geoname_id`, `represented_country_geoname_id`, `is_anonymous_proxy`, `is_satellite_provider`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `geolite2_country_blocks_ipv6` (`network`, `geoname_id`, `registered_country_geoname_id`, `represented_country_geoname_id`, `is_anonymous_proxy`, `is_satellite_provider`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `geolite2_country_blocks_ipv6` WHERE `network` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `geolite2_country_blocks_ipv6` SET `network`='{0}', `geoname_id`='{1}', `registered_country_geoname_id`='{2}', `represented_country_geoname_id`='{3}', `is_anonymous_proxy`='{4}', `is_satellite_provider`='{5}' WHERE `network` = '{6}';</SQL>
#End Region
''' <summary>
''' ```SQL
''' DELETE FROM `geolite2_country_blocks_ipv6` WHERE `network` = '{0}';
''' ```
''' </summary>
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, network)
    End Function
''' <summary>
''' ```SQL
''' INSERT INTO `geolite2_country_blocks_ipv6` (`network`, `geoname_id`, `registered_country_geoname_id`, `represented_country_geoname_id`, `is_anonymous_proxy`, `is_satellite_provider`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, network, geoname_id, registered_country_geoname_id, represented_country_geoname_id, is_anonymous_proxy, is_satellite_provider)
    End Function
''' <summary>
''' ```SQL
''' REPLACE INTO `geolite2_country_blocks_ipv6` (`network`, `geoname_id`, `registered_country_geoname_id`, `represented_country_geoname_id`, `is_anonymous_proxy`, `is_satellite_provider`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, network, geoname_id, registered_country_geoname_id, represented_country_geoname_id, is_anonymous_proxy, is_satellite_provider)
    End Function
''' <summary>
''' ```SQL
''' UPDATE `geolite2_country_blocks_ipv6` SET `network`='{0}', `geoname_id`='{1}', `registered_country_geoname_id`='{2}', `represented_country_geoname_id`='{3}', `is_anonymous_proxy`='{4}', `is_satellite_provider`='{5}' WHERE `network` = '{6}';
''' ```
''' </summary>
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, network, geoname_id, registered_country_geoname_id, represented_country_geoname_id, is_anonymous_proxy, is_satellite_provider, network)
    End Function
#End Region
End Class


End Namespace
