<?php
// 'user' object
class Item{
 
    // database connection and table name
    private $conn;
    private $table_name = "item";
 
    // object properties
    public $id;
    public $name;
    public $price;
 
    // constructor
    public function __construct($db){
        $this->conn = $db;
    }

    function read() {
        // query to check if email exists
        $query = "SELECT * FROM " . $this->table_name;
     
        // prepare the query
        $stmt = $this->conn->prepare( $query );
     
        // execute the query
        $stmt->execute();
     
        // get number of rows
        $num = $stmt->rowCount();
     
        // if email exists, assign values to object properties for easy access and use for php sessions
        if($num>0){
     
            // get record details / values
            $rows = $stmt->fetchAll(PDO::FETCH_ASSOC);
            
            return $rows;
        }
     
        // return false if email does not exist in the database
        //return false;
    }

    function getItem() {
        // query to check if email exists
        $query = "SELECT *
                FROM " . $this->table_name . "
                WHERE itemId = ?
                LIMIT 0,1";
     
        // prepare the query
        $stmt = $this->conn->prepare( $query );
     
        // sanitize
        $this->id=htmlspecialchars(strip_tags($this->id));
     
        // bind given email value
        $stmt->bindParam(1, $this->id);
     
        // execute the query
        $stmt->execute();
     
        // get number of rows
        $num = $stmt->rowCount();
     
        // if email exists, assign values to object properties for easy access and use for php sessions
        if($num>0){
            // get record details / values
            $row = $stmt->fetch(PDO::FETCH_ASSOC);
     
            // assign values to object properties
            $this->id = $row['itemId'];
            $this->name = $row['name'];
            $this->price = $row['price'];
     
            // return true because email exists in the database
            return true;
        }
     
        // return false if email does not exist in the database
        return false;
    }
}