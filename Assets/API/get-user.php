<?php
/*
Category: User
Description: 
This API is for retrieving public user info through username or user email.
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

// if empty, try getting from webform instead
if (!empty($data->username) || !empty($_POST["username"])) {
  if (!empty($data->username)) {
    $user->username = $data->username;
  }
  elseif (!empty($_POST["username"])) {
    $user->username = $_POST["username"];
  }
}
elseif (!empty($data->email) || !empty($_POST["email"])) {
  if (!empty($data->email)) {
    $user->email = $data->email;
  }
  elseif (!empty($_POST["email"])) {
    $user->email = $_POST["email"];
  }
}
 
// check if email exists and if password is correct
if($user->emailExists() || $user->usernameExists()) {

  $group->id = $user->group;

  if($group->setGroup()) {
    $userJson = array(
        "id" => $user->id,
        "username" => $user->username,
        "email" => $user->email,
        "date" => $user->date,
        "xp" => $user->xp,
        "coin" => $user->coin,
        "group" => array(
            "id" => $group->id,
            "name" => $group->name,
            "creator" => $group->creator,
            "point" => $group->point
          )
      );
  }
  else {
    $userJson = array(
        "id" => $user->id,
        "username" => $user->username,
        "email" => $user->email,
        "date" => $user->date,
        "xp" => $user->xp,
        "coin" => $user->coin,
        "group" => NULL
      );
  }

  // set response code
  http_response_code(200);

  echo json_encode($userJson);
}
 
// login failed
else {
 
    // set response code
    http_response_code(401);
 
    // tell the user login failed
    echo json_encode(array("message" => "Failed"));
}
?>