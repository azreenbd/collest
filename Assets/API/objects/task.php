<?php
class Task{
 
    // database connection and table name
    private $conn;
    private $table_name = "task";
 
    // object properties
    public $id;
    public $questId;
    public $tasks;
 
    // constructor
    public function __construct($db){
        $this->conn = $db;
    }

    // get all tasks associated with the quest specified
    function getTasks() {
        // query to check if email exists
        $query = "SELECT *
                FROM " . $this->table_name . "
                WHERE questId = ?";
     
        // prepare the query
        $stmt = $this->conn->prepare( $query );
     
        // sanitize
        $this->questId=htmlspecialchars(strip_tags($this->questId));
     
        // bind given email value
        $stmt->bindParam(1, $this->questId);
     
        // execute the query
        $stmt->execute();
     
        // get number of rows
        $num = $stmt->rowCount();
     
        // if email exists, assign values to object properties for easy access and use for php sessions
        if($num>0){
            // get all result as multidimensional array
            $this->tasks = $stmt->fetchAll(PDO::FETCH_ASSOC);
     
            // success
            return true;
        }
     
        // fail
        return false;
    }

}