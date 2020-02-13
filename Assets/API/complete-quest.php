<?php
/*
Category: groupquest
Description: 
This API is for changing group quest status to complete.
*/

// required headers
header("Access-Control-Allow-Origin: http://localhost/testapi/");
header("Content-Type: application/json; charset=UTF-8");
header("Access-Control-Allow-Methods: POST");
header("Access-Control-Max-Age: 3600");
header("Access-Control-Allow-Headers: Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");
 
// files needed to connect to database
include_once 'config/database.php';
include_once 'objects/groupquest.php';
 
// get database connection
$database = new Database();
$db = $database->getConnection();
 
// instantiate object
$groupquest = new GroupQuest($db);
 
// get posted data
$data = json_decode(file_get_contents("php://input"));

// if empty, try getting from webform instead
if (!empty($data->groupId) && !empty($data->questId)) {
	$groupquest->groupId = $data->groupId;
	$groupquest->questId = $data->questId;
}
elseif (!empty($_POST["groupId"]) && !empty($_POST["questId"])) {
	$groupquest->groupId = $_POST["groupId"];
	$groupquest->questId = $_POST["questId"];
}

if($groupquest->isComplete()) {
	http_response_code(200);
}
else {
	http_response_code(401);
}

