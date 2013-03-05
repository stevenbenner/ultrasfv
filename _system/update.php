<?php

header('Content-Type: text/xml');
echo '<?xml version="1.0" encoding="UTF-8" ?>';

$data_file = 'update.log';

$clientVersion = isset($HTTP_GET_VARS['version']) ? trim($HTTP_GET_VARS['version']) : "";
$clientGUID = isset($HTTP_GET_VARS['guid']) ? trim($HTTP_GET_VARS['guid']) : "";

$today = date("D M j Y G:i:s T");
$logLine = "$today|$clientVersion|$clientGUID\n";

$file = fopen($data_file, 'a');
if (flock($file, LOCK_EX)) {
	fwrite($file, $logLine);
	flock($file, LOCK_UN);
}
fclose($file);

?>

<updateInfo>
	<fileName>UltraSFVPatch.dll</fileName>
	<fileSize>404 KB</fileSize>
	<crc>3F24BCFD</crc>
	<url>http://ultrasfv.com/UltraSFVPatch.dll</url>
	<version>1.0.1.123456</version>
	<releaseDate>7/20/2008 08:17:25 A.M.</releaseDate>
</updateInfo>