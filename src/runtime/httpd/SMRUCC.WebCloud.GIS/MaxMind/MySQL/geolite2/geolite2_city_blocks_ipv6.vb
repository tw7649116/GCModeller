REM  Oracle.LinuxCompatibility.MySQL.CodeGenerator
REM  MYSQL Schema Mapper
REM      for Microsoft VisualBasic.NET 1.0.0.0

REM  Dump @9/4/2016 5:29:45 PM


Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes

Namespace MaxMind.geolite2

''' <summary>
''' ```SQL
''' 								\n
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `geolite2_city_blocks_ipv6`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `geolite2_city_blocks_ipv6` (
'''   `network` char(128) NOT NULL COMMENT 'This is the IPv4 or IPv6 network in CIDR format such as ''2.21.92.0/29'' or ''2001:4b0::/80''. We offer a utility to convert this column to start/end IPs or start/end integers. See the conversion utility section for details.',
'''   `geoname_id` int(11) DEFAULT NULL COMMENT 'A unique identifier for the network’s location as specified by GeoNames. This ID can be used to look up the location information in the Location file.',
'''   `registered_country_geoname_id` int(11) DEFAULT NULL COMMENT 'The registered country is the country in which the ISP has registered the network. This column contains a unique identifier for the network’s registered country as specified by GeoNames. This ID can be used to look up the location information in the Location file.',
'''   `represented_country_geoname_id` int(11) DEFAULT NULL COMMENT 'The represented country is the country which is represented by users of the IP\naddress. For instance, the country represented by an overseas military base. This column contains a unique identifier for the network’s registered country as specified by GeoNames. This ID can be used to look up the location information in the Location file.',
'''   `is_anonymous_proxy` int(11) DEFAULT NULL COMMENT 'A 1 if the network is an anonymous proxy, otherwise 0.',
'''   `is_satellite_provider` int(11) DEFAULT NULL COMMENT 'A 1 if the network is for a satellite provider that provides service to multiple countries, otherwise 0.',
'''   `postal_code` tinytext COMMENT 'The postal code associated with the IP address. These are available for some IP addresses in Australia, Canada, France, Germany, Italy, Spain, Switzerland, United Kingdom, and the US. We return the first 3 characters for Canadian postal codes. We return the the first 2-4 characters (outward code) for postal codes in the United Kingdom.',
'''   `latitude` double DEFAULT NULL COMMENT 'The latitude of the location associated with the network.',
'''   `longitude` double DEFAULT NULL COMMENT 'The longitude of the location associated with the network.',
'''   `accuracy_radius` double DEFAULT NULL,
'''   PRIMARY KEY (`network`),
'''   UNIQUE KEY `network_UNIQUE` (`network`)
''' ) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='								\n';
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' ```
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("geolite2_city_blocks_ipv6", Database:="maxmind_geolite2", SchemaSQL:="
CREATE TABLE `geolite2_city_blocks_ipv6` (
  `network` char(128) NOT NULL COMMENT 'This is the IPv4 or IPv6 network in CIDR format such as ''2.21.92.0/29'' or ''2001:4b0::/80''. We offer a utility to convert this column to start/end IPs or start/end integers. See the conversion utility section for details.',
  `geoname_id` int(11) DEFAULT NULL COMMENT 'A unique identifier for the network’s location as specified by GeoNames. This ID can be used to look up the location information in the Location file.',
  `registered_country_geoname_id` int(11) DEFAULT NULL COMMENT 'The registered country is the country in which the ISP has registered the network. This column contains a unique identifier for the network’s registered country as specified by GeoNames. This ID can be used to look up the location information in the Location file.',
  `represented_country_geoname_id` int(11) DEFAULT NULL COMMENT 'The represented country is the country which is represented by users of the IP\naddress. For instance, the country represented by an overseas military base. This column contains a unique identifier for the network’s registered country as specified by GeoNames. This ID can be used to look up the location information in the Location file.',
  `is_anonymous_proxy` int(11) DEFAULT NULL COMMENT 'A 1 if the network is an anonymous proxy, otherwise 0.',
  `is_satellite_provider` int(11) DEFAULT NULL COMMENT 'A 1 if the network is for a satellite provider that provides service to multiple countries, otherwise 0.',
  `postal_code` tinytext COMMENT 'The postal code associated with the IP address. These are available for some IP addresses in Australia, Canada, France, Germany, Italy, Spain, Switzerland, United Kingdom, and the US. We return the first 3 characters for Canadian postal codes. We return the the first 2-4 characters (outward code) for postal codes in the United Kingdom.',
  `latitude` double DEFAULT NULL COMMENT 'The latitude of the location associated with the network.',
  `longitude` double DEFAULT NULL COMMENT 'The longitude of the location associated with the network.',
  `accuracy_radius` double DEFAULT NULL,
  PRIMARY KEY (`network`),
  UNIQUE KEY `network_UNIQUE` (`network`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='								\n';")>
Public Class geolite2_city_blocks_ipv6: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
''' <summary>
''' This is the IPv4 or IPv6 network in CIDR format such as ''2.21.92.0/29'' or ''2001:4b0::/80''. We offer a utility to convert this column to start/end IPs or start/end integers. See the conversion utility section for details.
''' </summary>
''' <value></value>
''' <returns></returns>
''' <remarks></remarks>
    <DatabaseField("network"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "128")> Public Property network As String
''' <summary>
''' A unique identifier for the network’s location as specified by GeoNames. This ID can be used to look up the location information in the Location file.
''' </summary>
''' <value></value>
''' <returns></returns>
''' <remarks></remarks>
    <DatabaseField("geoname_id"), DataType(MySqlDbType.Int64, "11")> Public Property geoname_id As Long
''' <summary>
''' The registered country is the country in which the ISP has registered the network. This column contains a unique identifier for the network’s registered country as specified by GeoNames. This ID can be used to look up the location information in the Location file.
''' </summary>
''' <value></value>
''' <returns></returns>
''' <remarks></remarks>
    <DatabaseField("registered_country_geoname_id"), DataType(MySqlDbType.Int64, "11")> Public Property registered_country_geoname_id As Long
''' <summary>
''' The represented country is the country which is represented by users of the IP\naddress. For instance, the country represented by an overseas military base. This column contains a unique identifier for the network’s registered country as specified by GeoNames. This ID can be used to look up the location information in the Location file.
''' </summary>
''' <value></value>
''' <returns></returns>
''' <remarks></remarks>
    <DatabaseField("represented_country_geoname_id"), DataType(MySqlDbType.Int64, "11")> Public Property represented_country_geoname_id As Long
''' <summary>
''' A 1 if the network is an anonymous proxy, otherwise 0.
''' </summary>
''' <value></value>
''' <returns></returns>
''' <remarks></remarks>
    <DatabaseField("is_anonymous_proxy"), DataType(MySqlDbType.Int64, "11")> Public Property is_anonymous_proxy As Long
''' <summary>
''' A 1 if the network is for a satellite provider that provides service to multiple countries, otherwise 0.
''' </summary>
''' <value></value>
''' <returns></returns>
''' <remarks></remarks>
    <DatabaseField("is_satellite_provider"), DataType(MySqlDbType.Int64, "11")> Public Property is_satellite_provider As Long
''' <summary>
''' The postal code associated with the IP address. These are available for some IP addresses in Australia, Canada, France, Germany, Italy, Spain, Switzerland, United Kingdom, and the US. We return the first 3 characters for Canadian postal codes. We return the the first 2-4 characters (outward code) for postal codes in the United Kingdom.
''' </summary>
''' <value></value>
''' <returns></returns>
''' <remarks></remarks>
    <DatabaseField("postal_code"), DataType(MySqlDbType.Text)> Public Property postal_code As String
''' <summary>
''' The latitude of the location associated with the network.
''' </summary>
''' <value></value>
''' <returns></returns>
''' <remarks></remarks>
    <DatabaseField("latitude"), DataType(MySqlDbType.Double)> Public Property latitude As Double
''' <summary>
''' The longitude of the location associated with the network.
''' </summary>
''' <value></value>
''' <returns></returns>
''' <remarks></remarks>
    <DatabaseField("longitude"), DataType(MySqlDbType.Double)> Public Property longitude As Double
    <DatabaseField("accuracy_radius"), DataType(MySqlDbType.Double)> Public Property accuracy_radius As Double
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `geolite2_city_blocks_ipv6` (`network`, `geoname_id`, `registered_country_geoname_id`, `represented_country_geoname_id`, `is_anonymous_proxy`, `is_satellite_provider`, `postal_code`, `latitude`, `longitude`, `accuracy_radius`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `geolite2_city_blocks_ipv6` (`network`, `geoname_id`, `registered_country_geoname_id`, `represented_country_geoname_id`, `is_anonymous_proxy`, `is_satellite_provider`, `postal_code`, `latitude`, `longitude`, `accuracy_radius`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `geolite2_city_blocks_ipv6` WHERE `network` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `geolite2_city_blocks_ipv6` SET `network`='{0}', `geoname_id`='{1}', `registered_country_geoname_id`='{2}', `represented_country_geoname_id`='{3}', `is_anonymous_proxy`='{4}', `is_satellite_provider`='{5}', `postal_code`='{6}', `latitude`='{7}', `longitude`='{8}', `accuracy_radius`='{9}' WHERE `network` = '{10}';</SQL>
#End Region
''' <summary>
''' ```SQL
''' DELETE FROM `geolite2_city_blocks_ipv6` WHERE `network` = '{0}';
''' ```
''' </summary>
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, network)
    End Function
''' <summary>
''' ```SQL
''' INSERT INTO `geolite2_city_blocks_ipv6` (`network`, `geoname_id`, `registered_country_geoname_id`, `represented_country_geoname_id`, `is_anonymous_proxy`, `is_satellite_provider`, `postal_code`, `latitude`, `longitude`, `accuracy_radius`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, network, geoname_id, registered_country_geoname_id, represented_country_geoname_id, is_anonymous_proxy, is_satellite_provider, postal_code, latitude, longitude, accuracy_radius)
    End Function
''' <summary>
''' ```SQL
''' REPLACE INTO `geolite2_city_blocks_ipv6` (`network`, `geoname_id`, `registered_country_geoname_id`, `represented_country_geoname_id`, `is_anonymous_proxy`, `is_satellite_provider`, `postal_code`, `latitude`, `longitude`, `accuracy_radius`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, network, geoname_id, registered_country_geoname_id, represented_country_geoname_id, is_anonymous_proxy, is_satellite_provider, postal_code, latitude, longitude, accuracy_radius)
    End Function
''' <summary>
''' ```SQL
''' UPDATE `geolite2_city_blocks_ipv6` SET `network`='{0}', `geoname_id`='{1}', `registered_country_geoname_id`='{2}', `represented_country_geoname_id`='{3}', `is_anonymous_proxy`='{4}', `is_satellite_provider`='{5}', `postal_code`='{6}', `latitude`='{7}', `longitude`='{8}', `accuracy_radius`='{9}' WHERE `network` = '{10}';
''' ```
''' </summary>
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, network, geoname_id, registered_country_geoname_id, represented_country_geoname_id, is_anonymous_proxy, is_satellite_provider, postal_code, latitude, longitude, accuracy_radius, network)
    End Function
#End Region
End Class


End Namespace
