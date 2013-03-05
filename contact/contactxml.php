<?php

$mailto = 'fukushuu@yahoo.com';
$subject = 'UltraSFV.com Web Contact';

$frmName = isset($HTTP_POST_VARS['name']) ? trim($HTTP_POST_VARS['name']) : '';
$frmOrg = isset($HTTP_POST_VARS['org']) ? trim($HTTP_POST_VARS['org']) : '';
$frmEmail = isset($HTTP_POST_VARS['email']) ? trim($HTTP_POST_VARS['email']) : '';
$frmMessage = isset($HTTP_POST_VARS['message']) ? trim($HTTP_POST_VARS['message']) : '';
$referrer = getenv('HTTP_REFERER');

header('Content-Type: text/xml');
echo '<?xml version="1.0" encoding="UTF-8" standalone="yes"?>';
echo "\n<resultset>\n";
echo "  <errors>\n";

$errors = false;

if ($frmName == '') {
  $errors = true;
  echo "    <error>You did not input a name.</error>\n";
}
if ($frmEmail == '') {
  $errors = true;
  echo "    <error>You did not input an email address.</error>\n";
} elseif (!preg_match("/([\w\.\-]+)(\@[\w\.\-]+)(\.[a-z]{2,4})+/i", $frmEmail)) {
  $errors = true;
  echo "    <error>The email address you entered was invalid.</error>\n";
}
if ($frmMessage == '') {
  $errors = true;
  echo "    <error>You did not input a message.</error>\n";
}
if (get_magic_quotes_gpc()) $frmMessage = stripslashes($frmMessage);

echo "  </errors>\n";

if ($errors == false) {
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
    'This was generated from an ajax form at: '.$referrer."\n";
  $mailit = mail($mailto, $subject, $message, $headers);

  if (@$mailit) {
    $posStatus = 'OK';
    $posConfirmation = 'Thank you, your email message has been sent.';
  } else {
    $posStatus = 'NOTOK';
    $posConfirmation = 'We\'re sorry, but there seems to be a problem with our email system. Please try again later.';
  }
} else {
  $posStatus = 'ERROR';
  $posConfirmation = 'The message you submitted had an error:';
}

echo '  <status>'.$posStatus."</status>\n";
echo '  <confirmation>'.$posConfirmation."</confirmation>\n";
echo '</resultset>';

function cleanPosUrl ($str) {
  $nStr = $str;
  $nStr = str_replace('__am__','&',$nStr);
  $nStr = str_replace('__pl__','+',$nStr);
  $nStr = str_replace('__eq__','=',$nStr);
  return stripslashes($nStr);
}

?>