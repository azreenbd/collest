<?php
/*
Category: Item
Description: 
This API is for retrieving item.
If item Id is not specified, it will return all item.
*/

// required headers
header("Access-Control-Allow-Origin: http://localhost/testapi/");
header("Content-Type: application/json; charset=UTF-8");
header("Access-Control-Allow-Methods: POST");
header("Access-Control-Max-Age: 3600");
header("Access-Control-Allow-Headers: Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");
 
// files needed to connect to database
include_once 'config/database.php';
include_once 'objects/item.php';
 
// get database connection
$database = new Database();
$db = $database->getConnection();
 
// instantiate user object
$item = new Item($db);
 
// get posted data
$data = json_decode(file_get_contents("php://input"));

// if empty, try getting from webform instead
if (!empty($data->id)) {
	$item->id = $data->id;
}
elseif (!empty($_POST["id"])) {
	$item->id = $_POST["id"];
}

if (!empty($item->id)) {
	if ($item->getItem()) {
		$json = array(
			"id" => $item->id,
			"name" => $item->name,
			"price" => $item->price	
		);

		// success
		http_response_code(200);

		echo json_encode($json);
	}
	else {
		// error or fail
		http_response_code(401);
	}
} 
else {
	$arr = $item->read();

	if (!empty($arr)) {
		$json = [];

		foreach ($arr as $i) {
			array_push($json, array(
				"id" => $i['itemId'],
				"name" => $i['name'],
				"price" => $i['price'],
			));
		}

		// set response code
		http_response_code(200);

		echo json_encode($json);
	}
	else {
		// error or fail
		http_response_code(401);
	}
}
