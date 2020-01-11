<?php
// 'user' object
class Group{
 
    // database connection and table name
    private $conn;
    private $table_name = "team";
 
    // object properties
    public $id;
    public $name;
    public $creator;
    public $point;
 
    // constructor
    public function __construct($db){
        $this->conn = $db;
    }

    function setGroup() {
        // query to check if email exists
        $query = "SELECT *
                FROM " . $this->table_name . "
                WHERE groupId = ?
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
            $this->id = $row['groupId'];
            $this->name = $row['name'];
            $this->creator = $row['creator'];
            $this->point = $row['point'];
     
            // return true because email exists in the database
            return true;
        }
     
        // return false if email does not exist in the database
        return false;
    }

    function create(){
     
        // insert query
        $query = "INSERT INTO " . $this->table_name . "
                SET
                    name = :name,
                    creator = :creator";
     
        // prepare the query
        $stmt = $this->conn->prepare($query);
     
        // sanitize
        $this->name = htmlspecialchars(strip_tags($this->name));
        $this->creator = htmlspecialchars(strip_tags($this->creator));
     
        // bind the values
        $stmt->bindParam(':name', $this->name);
        $stmt->bindParam(':creator', $this->creator);
     
        // execute the query, also check if query was successful
        if($stmt->execute()){
            return true;
        }
     
        return false;
    }
    // ERROR, IF OTHER USER TRY DELETE GROUP HE DIDNT OWN, IT WILL DELETE THE SAID GROUP MEMBER, BUT NOT THE GROUP ITSELF!!
    function delete() {
        // insert query
        $query = "SELECT * FROM " . $this->table_name . " WHERE groupId = :id AND creator = :creator";
     
        // prepare the query
        $stmt = $this->conn->prepare($query);
     
        // sanitize
        $this->id = htmlspecialchars(strip_tags($this->id));
        $this->creator = htmlspecialchars(strip_tags($this->creator));
     
        // bind the values
        $stmt->bindParam(':id', $this->id);
        $stmt->bindParam(':creator', $this->creator);

        $stmt->execute();

        $num = $stmt->rowCount();
     
        if($num>0) {
            // insert query
            $query = "UPDATE player 
                    SET groupId = null
                    WHERE groupId = :id";
         
            // prepare the query
            $stmt = $this->conn->prepare($query);
         
            // sanitize
            $this->id = htmlspecialchars(strip_tags($this->id));
         
            // bind the values
            $stmt->bindParam(':id', $this->id);
         
            // execute the query, also check if query was successful
            if($stmt->execute()){
                // insert query
                $query = "DELETE FROM " . $this->table_name . " 
                        WHERE groupId = :id AND creator = :creator";
             
                // prepare the query
                $stmt = $this->conn->prepare($query);
             
                // sanitize
                $this->id = htmlspecialchars(strip_tags($this->id));
                $this->creator = htmlspecialchars(strip_tags($this->creator));
             
                // bind the values
                $stmt->bindParam(':id', $this->id);
                $stmt->bindParam(':creator', $this->creator);
             
                // execute the query, also check if query was successful
                if($stmt->execute()){
                    return true;
                }
            }
        }
        return false;
    }

    function join($userId) {
        // insert query
        $query = "UPDATE player
                SET groupId = :groupId
                WHERE playerId = :userId";
     
        // prepare the query
        $stmt = $this->conn->prepare($query);
     
        // sanitize
        $this->id = htmlspecialchars(strip_tags($this->id));
        $userId = htmlspecialchars(strip_tags($userId));
     
        // bind the values
        $stmt->bindParam(':groupId', $this->id);
        $stmt->bindParam(':userId', $userId);
     
        // execute the query, also check if query was successful
        if($stmt->execute()){
            return true;
        }
    
        return false;
    }

    function creatorExists() {
        // query to check if email exists
        $query = "SELECT *
                FROM " . $this->table_name . "
                WHERE creator = ?
                LIMIT 0,1";
     
        // prepare the query
        $stmt = $this->conn->prepare( $query );
     
        // sanitize
        $this->creator=htmlspecialchars(strip_tags($this->creator));
     
        // bind given email value
        $stmt->bindParam(1, $this->creator);
     
        // execute the query
        $stmt->execute();
     
        // get number of rows
        $num = $stmt->rowCount();
     
        if($num>0){
     
            // get record details / values
            $row = $stmt->fetch(PDO::FETCH_ASSOC);
     
            // assign values to object properties
            $this->id = $row['groupId'];
            $this->name = $row['name'];
            $this->point = $row['point'];
     
            return true;
        }
     
        return false;
    }

}
