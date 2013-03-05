<?php

header('Content-Type: text/xml');
echo '<?xml version="1.0" encoding="UTF-8" ?>';

$clientGUID = isset($HTTP_POST_VARS['guid']) ? trim($HTTP_POST_VARS['guid']) : "";
$clientName = isset($HTTP_POST_VARS['name']) ? trim($HTTP_POST_VARS['name']) : "";
$clientDesc = isset($HTTP_POST_VARS['desc']) ? trim($HTTP_POST_VARS['desc']) : "";
$clientLog = isset($HTTP_POST_VARS['log']) ? trim($HTTP_POST_VARS['log']) : "";

#mkdir("./errorlogs/$clientGUID/", 0777);
$data_file = "./errorlogs/$clientGUID/$clientName";

$file = fopen($data_file, 'w');
if (flock($file, LOCK_EX)) {
	fwrite($file, "$clientDesc\n\n--------\n\n$clientLog");
	flock($file, LOCK_UN);
}
fclose($file);

?>

<errorReport>
	<result>success</result>
</errorReport>