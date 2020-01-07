<?php
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
include_once 'objects/user.php';

// get database connection
$database = new Database();
$db = $database->getConnection();
 
// instantiate user object
$user = new User($db);
 
// get posted data
$data = json_decode(file_get_contents("php://input"));
 
// get jwt
// $jwt=isset($data->jwt) ? $data->jwt : "";

if(isset($data->jwt)) {
	$jwt = $data->jwt;
}
else if(isset($_POST["jwt"])) {
	$jwt = $_POST["jwt"];
}
else {
	$jwt = "";
}
 
// if jwt is not empty
if($jwt) {
 
    // if decode succeed, show user details
    try {
        // decode jwt
        $decoded = JWT::decode($jwt, $key, array('HS256'));

        $email = $decoded->data->email;

        $user->email = $email;

        if(!empty($email) && $user->emailExists()) {
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
                   "group" => $user->group
               )
            );
         
            // set response code
            http_response_code(200);
         
            // generate jwt
            $newJwt = JWT::encode($token, $key);
            echo json_encode(
                    array(
                        "message" => "Success",
                        "jwt" => $newJwt
                    )
                );
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