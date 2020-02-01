<?php
/*
Category: GroupQuest
Description: 
This API is for user to start a quest. 
An input of a quest id and JSON web token is required.
This is to make sure only the logged in user has access.
*/

// required headers
header("Access-Control-Allow-Origin: http://localhost/testapi/");
header("Content-Type: application/json; charset=UTF-8");
header("Access-Control-Allow-Methods: POST");
header("Access-Control-Max-Age: 3600");
header("Access-Control-Allow-Headers: Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");

// required to decode jwt
include_once 'config/core.php';
include_once 'libs/php-jwt-master/src/BeforeValidException.php';
include_once 'libs/php-jwt-master/src/ExpiredException.php';
include_once 'libs/php-jwt-master/src/SignatureInvalidException.php';
include_once 'libs/php-jwt-master/src/JWT.php';
use \Firebase\JWT\JWT;
 
// files needed to connect to database
include_once 'config/database.php';
include_once 'objects/groupquest.php';
 
// get database connection
$database = new Database();
$db = $database->getConnection();
 
// instantiate product object
$groupQuest = new GroupQuest($db);
 
// get posted data
$data = json_decode(file_get_contents("php://input"));

// if empty, try getting from webform instead
if (!empty($data->questId) && !empty($data->jwt)) {
    // set product property values
    $groupQuest->questId = $data->questId;
    $jwt = $data->jwt;
}
elseif (!empty($_POST["questId"]) && !empty($_POST["jwt"])) {
    $groupQuest->questId = $_POST["questId"];
    $jwt = $_POST["jwt"];
}
else {
    $jwt = "";
}

if($jwt) {
 
    // if decode succeed, show user details
    try {
        // decode jwt
        $decoded = JWT::decode($jwt, $key, array('HS256'));

		$groupQuest->groupId = $decoded->data->group->id;

        if($groupQuest->add()) {
        	http_response_code(200);
        	echo json_encode(array("message" => "Quest started."));
        }
        else {
        	// set response code
	        http_response_code(401);
	     	echo json_encode(array("message" => "Access denied."));
        }
    }
    catch (Exception $e){ // if decode fails, it means jwt is invalid
     
        // set response code
        http_response_code(401);
     
        // tell the user access denied  & show error message
        echo json_encode(array(
            "message" => "Access denied.",
            "error" => $e->getMessage()
        ));
    }
}
else { // show error message if jwt is empty
 
    // set response code
    http_response_code(401);
 
    // tell the user access denied
    echo json_encode(array("message" => "Access denied."));
}
?>