<?php

$mailto = 'fukushuu@yahoo.com';
$subject = 'UltraSFV.com Web Contact';

$frmName = isset($HTTP_POST_VARS['name']) ? trim($HTTP_POST_VARS['name']) : '';
$frmOrg = isset($HTTP_POST_VARS['org']) ? trim($HTTP_POST_VARS['org']) : '';
$frmEmail = isset($HTTP_POST_VARS['email']) ? trim($HTTP_POST_VARS['email']) : '';
$frmMessage = isset($HTTP_POST_VARS['message']) ? trim($HTTP_POST_VARS['message']) : '';
$referrer = getenv('HTTP_REFERER');

$errors = '';

if ($frmName == '') $errors .= "You did not input a name.<br />\n";
if ($frmEmail == '') {
  $errors .= "You did not input an email address.<br />\n";
} elseif (!preg_match("/([\w\.\-]+)(\@[\w\.\-]+)(\.[a-z]{2,4})+/i", $frmEmail)) {
  $errors .= "The email address you entered was invalid.<br />\n";
}
if ($frmMessage == '') $errors .= "You did not input a message.<br />\n";
if (get_magic_quotes_gpc()) $frmMessage = stripslashes($frmMessage);

?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head>
<title>Contact Form</title>
</head>
<body>
<?

if ($errors == '') {
  $headers =
    'From: "'.$frmName.'" <'.$frmEmail.">\r\n" .
    'To: '.$mailto."\r\n";
  $message =
    "UltraSFV.com Web Contact:\n\n" .
    'Name: '.$frmName."\n" .
    'Organization: '.$frmOrg."\n" .
    'Email: '.$frmEmail."\n\n" .
    "------------------------- MESSAGE -------------------------\n\n" .
    $frmMessage .
    "\n\n-----------------------------------------------------------\n" .
    'This was generated from a form at: '.$referrer."\n";
  $mailit = mail($mailto, $subject, $message, $headers);

  if (@$mailit) {

?>
<h1>Email Sent</h1>
<p>Thank you, your email message has been sent.</p>
<hr />
<address>UltraSFV.com</address>
<?

  } else {

?>
<h1>System Error</h1>
<p>We're sorry, but there seems to be a problem with our email system. Please try again later.</p>
<hr />
<address>UltraSFV.com</address>
<?

  }
} else {

?>
<h1>Submission Error</h1>
<p>The message you submitted generated the following error(s):</p>
<p><b><? echo($errors); ?></b></p>
<p>Please try again</p>
<hr />
<address>UltraSFV.com</address>
<?

}

?>
<? include('../_includes/google-analytics.html') ?>
</body>
</html>