<?php
class Quest{
 
    // database connection and table name
    private $conn;
    private $table_name = "quest";
 
    // object properties
    public $id;
    public $title;
    public $description;
    public $topic;
    public $reward;
    public $level;
    public $npc;
 
    // constructor
    public function __construct($db){
        $this->conn = $db;
    }

    function getQuest() {
        // query to check if email exists
        $query = "SELECT *
                FROM " . $this->table_name . "
                WHERE questId = ?
                LIMIT 0,1";
     
        // prepare the query
        $stmt = $this->conn->prepare( $query );
     
        // sanitize
        $this->id=htmlspecialchars(strip_tags($this->id));
     
        // bind value
        $stmt->bindParam(1, $this->id);
     
        // execute the query
        $stmt->execute();
     
        // get number of rows
        $num = $stmt->rowCount();
     
        if($num>0){
            // get record details / values
            $row = $stmt->fetch(PDO::FETCH_ASSOC);
     
            // assign values to object properties
            $this->id = $row['questId'];
            $this->title = $row['title'];
            $this->description = $row['description'];
            $this->topic = $row['topic'];
            $this->reward = $row['reward'];
            $this->level = $row['level'];


            // put the npc name instead?
            $this->npc = $row['npc'];
     
            // return true
            return true;
        }
     
        // return false
        return false;
    }

}