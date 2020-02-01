<?php
// 'user' object
class GroupQuest{
 
    // database connection and table name
    private $conn;
    private $table_name = "groupquest";
 
    // object properties
    public $id;
    public $groupId;
    public $questId;
    public $isComplete;
 
    // constructor
    public function __construct($db){
        $this->conn = $db;
    }

    function add() {
        // insert query
        $query = "INSERT INTO " . $this->table_name . "
                SET
                    groupId = :groupId,
                    questId = :questId";
     
        // prepare the query
        $stmt = $this->conn->prepare($query);
     
        // sanitize
        $this->groupId = htmlspecialchars(strip_tags($this->groupId));
        $this->questId = htmlspecialchars(strip_tags($this->questId));
     
        // bind the values
        $stmt->bindParam(':groupId', $this->groupId);
        $stmt->bindParam(':questId', $this->questId);
     
        // execute the query, also check if query was successful
        if($stmt->execute()){
            return true;
        }
     
        return false;
    }

}