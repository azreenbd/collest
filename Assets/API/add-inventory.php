<?php
/*
Category: Inventory
Description: 
This API is for user to add an item to their inventory.
Input is user id, item id and amount of item.
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
//include_once 'objects/user.php';
 
// get database connection
$database = new Database();
$db = $database->getConnection();
 
// instantiate product object
$inventory = new Inventory($db);
//$user = new User($db);
 
// get posted data
$data = json_decode(file_get_contents("php://input"));

// if empty, try getting from webform instead
if (!empty($data)) {
    $inventory->player = $data->userId;
    $inventory->item = $data->itemId;
    $inventory->amount = $data->amount;

    //$user->id = $data->userId;
}
elseif (!empty($_POST)) {
    $inventory->player = $_POST["userId"];
    $inventory->item = $_POST["itemId"];
    $inventory->amount = $_POST["amount"];

    //$user->id = $_POST["userId"];
}

// add the item to user
if ($inventory->add()) {
	// success
	http_response_code(200);
}
else {
	// fail
	http_response_code(401);
}

