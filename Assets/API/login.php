<?php
/*
Category: User
Description: 
This API is for user login. 
It will create a JSON web token for the user to use.
*/

// required headers
header("Access-Control-Allow-Origin: http://localhost/testapi/");
header("Content-Type: application/json; charset=UTF-8");
header("Access-Control-Allow-Methods: POST");
header("Access-Control-Max-Age: 3600");
header("Access-Control-Allow-Headers: Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");
 
// files needed to connect to database
include_once 'config/database.php';
include_once 'objects/user.php';
include_once 'objects/group.php';
 
// get database connection
$database = new Database();
$db = $database->getConnection();
 
// instantiate user object
$user = new User($db);
$group = new Group($db);
 
// get posted data
$data = json_decode(file_get_contents("php://input"));
$password;

// if empty, try getting from webform instead
if (!empty($data)) {
  $user->email = $data->email;
  $password = $data->password;
}
elseif (!empty($_POST)) {
  $user->email = $_POST["email"];
  $password = $_POST["password"];
}

// check if email exists
$email_exists = $user->emailExists();
 
// generate json web token
include_once 'config/core.php';
include_once 'libs/php-jwt-master/src/BeforeValidException.php';
include_once 'libs/php-jwt-master/src/ExpiredException.php';
include_once 'libs/php-jwt-master/src/SignatureInvalidException.php';
include_once 'libs/php-jwt-master/src/JWT.php';
use \Firebase\JWT\JWT;
 
// check if email exists and if password is correct
if($email_exists && password_verify($password, $user->password)) {

  $group->id = $user->group;

  if($group->setGroup()) {
    $token = array(
      "iss" => $iss,
      "aud" => $aud,
      "iat" => $iat,
      "nbf" => $nbf,
      "data" => array(
          "id" => $user->id,
          "username" => $user->username,
          "email" => $user->email,
          "date" => $user->date,
          "xp" => $user->xp,
          "group" => array(
              "id" => $group->id,
              "name" => $group->name,
              "creator" => $group->creator,
              "point" => $group->point
            )
       )
    );
  }
  else {
    $token = array(
      "iss" => $iss,
      "aud" => $aud,
      "iat" => $iat,
      "nbf" => $nbf,
      "data" => array(
          "id" => $user->id,
          "username" => $user->username,
          "email" => $user->email,
          "date" => $user->date,
          "xp" => $user->xp,
          "group" => NULL
        )
    );
  }

  // set response code
  http_response_code(200);

  // generate jwt
  $jwt = JWT::encode($token, $key);
  echo json_encode(
          array(
              "message" => "Success",
              "jwt" => $jwt
          )
      );
}
 
// login failed
else {
 
    // set response code
    http_response_code(401);
 
    // tell the user login failed
    echo json_encode(array("message" => "Failed"));
}
?>