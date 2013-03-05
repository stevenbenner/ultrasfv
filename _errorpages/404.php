<?php
header("Status: 404 Not Found");

$data_file = '404.log';

$today = date("D M j Y G:i:s T");
$logLine = "$today|" . getenv("REQUEST_URI") . "|" . getenv("HTTP_REFERER") . "\r\n";

$file = fopen($data_file, 'a');
if (flock($file, LOCK_EX)) {
	fwrite($file, $logLine);
	flock($file, LOCK_UN);
}
fclose($file);

?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="en" xml:lang="en">
<!-- Copyright (c) 2008 Steven Benner. All Rights Reserved. -->
<head>
	<title>Page Not Found</title>
	<meta http-equiv="content-type" content="text/html; charset=utf-8" />
	<meta http-equiv="content-language" content="en" />
	<meta name="robots" content="noindex,follow" />
	<link rel="stylesheet" type="text/css" href="/css/screen.css" media="screen" />
	<link rel="stylesheet" type="text/css" href="/css/print.css" media="print" />
	<script src="/js/jsloader.js" type="text/javascript"></script>
	<style type="text/css">
		#goog-wm { }
		#goog-wm h3.closest-match { }
		#goog-wm h3.closest-match a { }
		#goog-wm h3.other-things { display: none; }
		#goog-wm ul li { }
		#goog-wm li.search-goog { }
	</style>
	<script type="text/javascript">
		var GOOG_FIXURL_LANG = 'en';
		var GOOG_FIXURL_SITE = 'http://www.ultrasfv.com/';
	</script>
</head>
<body>
	<!-- header -->
	<div id="header">
		<div class="wrap">
			<h1><a href="http://www.ultrasfv.com/"><span>UltraSFV</span></a></h1>
			<p>Ultra Simple File Verification</p>
		</div>
	</div>
	<!-- site navigation -->
	<div id="navigation">
		<div class="wrap">
			<ul>
				<li><a href="/" title="Home Page">Home</a></li>
				<li><a href="/about/" title="About UltraSFV">About</a>
						<ul id="submenu_about">
							<li><a href="/about/features.html" title="UltraSFV Features">Features</a></li>
							<li><a href="/about/easytouse.html" title="UltraSFV Is User Friendly">Easy To Use</a></li>
							<li><a href="/about/releasenotes.html" title="Release Notes">Release Notes</a></li>
						</ul>
					</li>
				<li><a href="/download/" title="Download UltraSFV">Download</a></li>
				<li><a href="/support/" title="Help &amp; Support">Support</a>
						<ul id="submenu_support">
							<li><a href="/support/usermanual/" title="UltraSFV User Manual">User Manual</a></li>
							<li><a href="/support/forums/" title="Message Forums">Message Forums</a></li>
							<li><a href="/support/faq.html" title="Frequently Asked Questions">FAQ</a></li>
							<li><a href="/support/bugreport.html" title="Report a Bug">Report a Bug</a></li>
						</ul>
					</li>
				<li><a href="/contact/" title="Contact Form">Contact</a></li>
			</ul>
		</div>
	</div>
	<!-- content -->
	<div id="content">
		<div class="wrap">
			<!-- main content -->
			<div id="content-main" class="nosup">
				<h2>Page Not Found</h2>
				<h3>The page you requested could not be found.</h3>
				<p>The page you are looking for might have been removed, had its name changed, or is temporarily unavailable.</p>
				<p><b>Please try the following:</b></p>
				<ul>
					<li>If you typed the page address in the Address bar, make sure that it is spelled correctly.</li>
					<li>Open the <a href="http://www.ultrasfv.com/" title="Home Page">UltraSFV.com</a> home page and look for links to the information you want.</li>
					<li>Use the navigation bar above to find the link you are looking for.</li>
					<li>Click the Back button to try another link.</li>
				</ul>
				<script type="text/javascript" src="http://linkhelp.clients.google.com/tbproxy/lh/wm/fixurl.js"></script>
				<p style="color: #999; font-size: 10px; line-height: 12px; text-align: right;">404 File Not Found<br />
					The requested URL <? echo(getenv("REQUEST_URI")); ?> was not found on this server<br />
					This error has been logged and will be reviewed by the webmaster</p>
			</div>
		</div>
	</div>
	<!-- footer -->
	<div id="footer">
		<div class="wrap">
			<div id="disclaimer">
				<p><a href="http://www.ultrasfv.com/">http://www.ultrasfv.com/</a></p>
				<p>This site is not intended for distribution to, or use by, any person or entity in any jurisdiction or country where such distribution or use would be contrary to local law or regulation.</p>
			</div>
			<div id="copyright">
				<p><a href="/acknowledgements.html">Acknowledgements</a> | <a href="/terms.html">Terms of Use</a> | <a href="/privacy.html">Privacy Policy</a></p>
				<p>&copy; 2008 Steven Benner. All rights reserved.</p>
			</div>
		</div>
	</div>
	<? include('../_includes/google-analytics.html') ?>
</body>
</html>