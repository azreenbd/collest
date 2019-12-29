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
        $this->username=htmlspecialchars(strip_tags($this->username));
        $this->email=htmlspecialchars(strip_tags($this->email));
        $this->password=htmlspecialchars(strip_tags($this->password));
     
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
     
    // emailExists() method will be here
}