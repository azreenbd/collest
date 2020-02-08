<?php
/*
Category: Inventory
Description: 
This API is for retrieving the group inventory (taken from all members inventory).
Group Id need to be specified.
*/

// required headers
header("Access-Control-Allow-Origin: http://localhost/testapi/");
header("Content-Type: application/json; charset=UTF-8");
header("Access-Control-Allow-Methods: POST");
header("Access-Control-Max-Age: 3600");
header("Access-Control-Allow-Headers: Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");
 
// files needed to connect to database
include_once 'config/database.php';
include_once 'objects/inventory.php';
 
// get database connection
$database = new Database();
$db = $database->getConnection();
 
// instantiate user object
$inventory = new Inventory($db);
 
// get posted data
$data = json_decode(file_get_contents("php://input"));

$groupId;

// if empty, try getting from webform instead
if (!empty($data->groupId)) {
	$groupId = $data->groupId;
}
elseif (!empty($_POST["groupId"])) {
	$groupId = $_POST["groupId"];
}

if (!empty($groupId)) {

	$arr = $inventory->getInventoryGroup($groupId);

	if (!empty($arr)) {
		$json = [];
		$items = [];

		foreach ($arr as $i) {
			array_push($json, array(
				"itemId" => $i['itemId'],
				"amount" => $i['amount'],
			));
		}

		$items = array( "items" => $json );
		// set response code
		http_response_code(200);

		echo json_encode($items);
	}
	else {
		// error or fail
		http_response_code(401);
	}
} else {
	http_response_code(401);
}
