<?php
// 'user' object
class User{
 
    // database connection and table name
    private $conn;
    private $table_name = "player";
 
    // object properties
    /*
    public $id;
    public $firstname;
    public $lastname;
    public $email;
    public $password;
    */
    public $id;
    public $username;
    public $email;
    public $password;
    public $date;
    public $xp;
    public $group;
 
    // constructor
    public function __construct($db){
        $this->conn = $db;
    }
 
    // create new user record
    function create(){
     
        // insert query
        $query = "INSERT INTO " . $this->table_name . "
                SET
                    username = :username,
                    email = :email,
                    password = :password,
                    dateRegistered = :date";
     
        // prepare the query
        $stmt = $this->conn->prepare($query);
     
        // sanitize
        $this->username = htmlspecialchars(strip_tags($this->username));
        $this->email = htmlspecialchars(strip_tags($this->email));
        $this->password = htmlspecialchars(strip_tags($this->password));
     
        // bind the values
        $stmt->bindParam(':username', $this->username);
        $stmt->bindParam(':email', $this->email);
        $stmt->bindParam(':password', $this->password);
        $stmt->bindParam(':date', $this->date);
     
        // set date
        $current_date = date("Y-m-d");
        $stmt->bindParam(':date', $current_date);

        // hash the password before saving to database
        $password_hash = password_hash($this->password, PASSWORD_BCRYPT);
        $stmt->bindParam(':password', $password_hash);
     
        // execute the query, also check if query was successful
        if($stmt->execute()){
            return true;
        }
     
        return false;
    }
     
    // check if given email exist in the database
	function emailExists() {
	 
	    // query to check if email exists
	    $query = "SELECT *
	            FROM " . $this->table_name . "
	            WHERE email = ?
	            LIMIT 0,1";
	 
	    // prepare the query
	    $stmt = $this->conn->prepare( $query );
	 
	    // sanitize
	    $this->email=htmlspecialchars(strip_tags($this->email));
	 
	    // bind given email value
	    $stmt->bindParam(1, $this->email);
	 
	    // execute the query
	    $stmt->execute();
	 
	    // get number of rows
	    $num = $stmt->rowCount();
	 
	    // if email exists, assign values to object properties for easy access and use for php sessions
	    if($num>0){
	 
	        // get record details / values
	        $row = $stmt->fetch(PDO::FETCH_ASSOC);
	 
	        // assign values to object properties
	        $this->id = $row['playerId'];
	        $this->username = $row['username'];
	        $this->password = $row['password'];
            $this->date = $row['dateRegistered'];
            $this->xp = $row['xp'];
            $this->group = $row['groupId'];
	 
	        // return true because email exists in the database
	        return true;
	    }
	 
	    // return false if email does not exist in the database
	    return false;
	}

    // check if given username exist in the database
    function usernameExists() {
     
        // query to check if email exists
        $query = "SELECT *
                FROM " . $this->table_name . "
                WHERE username = ?
                LIMIT 0,1";
     
        // prepare the query
        $stmt = $this->conn->prepare( $query );
     
        // sanitize
        $this->username=htmlspecialchars(strip_tags($this->username));
     
        // bind given email value
        $stmt->bindParam(1, $this->username);
     
        // execute the query
        $stmt->execute();
     
        // get number of rows
        $num = $stmt->rowCount();
     
        // if email exists, assign values to object properties for easy access and use for php sessions
        if($num>0){
     
            // get record details / values
            $row = $stmt->fetch(PDO::FETCH_ASSOC);
     
            // assign values to object properties
            $this->id = $row['playerId'];
            $this->username = $row['username'];
            $this->password = $row['password'];
            $this->date = $row['dateRegistered'];
            $this->xp = $row['xp'];
            $this->group = $row['groupId'];
     
            // return true because email exists in the database
            return true;
        }
     
        // return false if email does not exist in the database
        return false;
    }

    function joinGroup() {
        // insert query
        $query = "UPDATE " . $this->table_name . " 
                SET groupId = :group 
                WHERE playerId = :id";
     
        // prepare the query
        $stmt = $this->conn->prepare($query);
     
        // sanitize
        $this->id = htmlspecialchars(strip_tags($this->id));
        $this->group = htmlspecialchars(strip_tags($this->group));
     
        // bind the values
        $stmt->bindParam(':id', $this->id);
        $stmt->bindParam(':group', $this->group);
     
        // execute the query, also check if query was successful
        if($stmt->execute()){
            return true;
        }
     
        return false;
    }

    function leaveGroup() {
        // insert query
        $query = "UPDATE " . $this->table_name . " 
                SET groupId = NULL 
                WHERE playerId = :id";
     
        // prepare the query
        $stmt = $this->conn->prepare($query);
     
        // sanitize
        $this->id = htmlspecialchars(strip_tags($this->id));
     
        // bind the values
        $stmt->bindParam(':id', $this->id);
     
        // execute the query, also check if query was successful
        if($stmt->execute()){
            return true;
        }
     
        return false;
    }

    function hasGroup() {
        $query = "SELECT groupId
                FROM " . $this->table_name . "
                WHERE playerId = ?
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
            
            // if group is not empty or null
            if(!empty($row['groupId'])) {
                $this->group = $row['groupId'];
                return true;
            }
            else {
                return false;
            }
        }
        return false;
    }
}