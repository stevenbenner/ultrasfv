<?php
header("Status: 403 Forbidden");
?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="en" xml:lang="en">
<!-- Copyright (c) 2008 Steven Benner. All Rights Reserved. -->
<head>
	<title>403 Forbidden</title>
	<meta http-equiv="content-type" content="text/html; charset=utf-8" />
	<meta http-equiv="content-language" content="en" />
	<meta name="robots" content="noindex,follow" />
</head>
<body>
	<div>
		<h1>403 Forbidden</h1>
		<p>You do not have permission to access <? echo(getenv("REQUEST_URI")); ?> on this server.</p>
	</div>
</body>
</html>