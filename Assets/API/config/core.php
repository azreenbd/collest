<?php
// show error reporting
error_reporting(E_ALL);
 
// set your default time-zone
date_default_timezone_set('Asia/Kuala_Lumpur');
 
// variables used for jwt
$key = "example_key";
$iss = "http://example.org"; // issuer
$aud = "http://example.com"; //audience
$iat = 1356999524; // issued at
$nbf = 1357000000; // not before
?> 