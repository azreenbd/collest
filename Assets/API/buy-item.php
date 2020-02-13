<?php
/*
Category: Item
Description: 
This API is for buying item.
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
include_once 'objects/item.php';
include_once 'objects/user.php';
include_once 'objects/inventory.php';
 
// get database connection
$database = new Database();
$db = $database->getConnection();
 
// instantiate user object
$item = new Item($db);
$user = new User($db);
$inventory = new Inventory($db);
 
// get posted data
$data = json_decode(file_get_contents("php://input"));

// if empty, try getting from webform instead
if (!empty($data->itemId) && !empty($data->jwt)) {
	$item->id = $data->itemId;
	$jwt = $data->jwt;
}
elseif (!empty($_POST["itemId"]) && !empty($_POST["jwt"])) {
	$item->id = $_POST["itemId"];
	$jwt = $data->jwt;
}
else {
	$jwt = "";
}


if($jwt) {
 
    // if decode succeed, show user details
    try {
        // decode jwt
        $decoded = JWT::decode($jwt, $key, array('HS256'));

        $user->id = $decoded->data->id;

        // get item price and retrieve/refresh user information from db
        if($item->getItem() && $user->get()) {
        	// if user have sufficient coin
        	if($item->price <= $user->coin) {

        		$inventory->player = $user->id;
        		$inventory->item = $item->id;
        		$inventory->amount = 1;

        		if($user->deductCoin($item->price) && $inventory->add()) {
        			// SUCCESS DO
        			http_response_code(200);
        		}
        		else {
        			http_response_code(401);
            		echo json_encode(array("message" => "Access denied."));
        		}

        	}
        	else {
        		http_response_code(401);
            	echo json_encode(array("message" => "Insufficient funds."));
        	}
            
        }
        else {
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
