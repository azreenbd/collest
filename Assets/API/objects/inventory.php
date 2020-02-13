<?php
// 'user' object
class Inventory{
 
    // database connection and table name
    private $conn;
    private $table_name = "inventory";
 
    // object properties
    public $id;
    public $player;
    public $item;
    public $amount;
 
    // constructor
    public function __construct($db){
        $this->conn = $db;
    }

    function add() {
        // query
        $query = "SELECT * FROM " . $this->table_name . "
                WHERE playerId = :player 
                AND itemId = :item";

        // prepare the query
        $stmt = $this->conn->prepare($query);
     
        // sanitize
        $this->player = htmlspecialchars(strip_tags($this->player));
        $this->item = htmlspecialchars(strip_tags($this->item));
        $this->amount = htmlspecialchars(strip_tags($this->amount));
     
        // bind the values
        $stmt->bindParam(':player', $this->player);
        $stmt->bindParam(':item', $this->item);
        
        // execute query
        $stmt->execute();

        // get number of rows
        $num = $stmt->rowCount();
     
        // if user already have the item
        // add up the amount only
        if($num>0){
     
            // get record details / values
            $row = $stmt->fetch(PDO::FETCH_ASSOC);
            
            // amount currently in inventory
            $currentAmount = $row['amount'];
            // add up old amount with new item adding
            $this->amount += $currentAmount;

            // query
            $query = "UPDATE " . $this->table_name . "
                    SET amount = :amount
                    WHERE playerId = :player
                    AND itemId = :item";

            // prepare the query
            $stmt = $this->conn->prepare($query);
         
            // bind the values
            $stmt->bindParam(':player', $this->player);
            $stmt->bindParam(':item', $this->item);
            $stmt->bindParam(':amount', $this->amount);
         
            // execute the query, also check if query was successful
            if($stmt->execute()){
                return true;
            }
         
            return false;
        }
        // create new item in user inventory
        else {
            // insert query
            $query = "INSERT INTO " . $this->table_name . "
                    SET
                        playerId = :player,
                        itemId = :item,
                        amount = :amount";
         
            // prepare the query
            $stmt = $this->conn->prepare($query);
         
            // bind the values
            $stmt->bindParam(':player', $this->player);
            $stmt->bindParam(':item', $this->item);
            $stmt->bindParam(':amount', $this->amount);
         
            // execute the query, also check if query was successful
            if($stmt->execute()){
                return true;
            }
         
            return false;
        }
    }

    function getInventory() {
        //sanitize
        $this->player = htmlspecialchars(strip_tags($this->player));

        //query
        $query = "SELECT itemId, amount FROM " .$this->table_name. 
                " WHERE playerId = :player";

        // prepare the query
        $stmt = $this->conn->prepare( $query );

        $stmt->bindParam(':player', $this->player);

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
    }

    function getInventoryGroup($groupId) {
    	//sanitize
    	$groupId = htmlspecialchars(strip_tags($groupId));

    	//query
    	$query = "SELECT inventory.itemId, SUM(inventory.amount) AS 'amount' FROM inventory
		    	WHERE inventory.playerId IN 
		    	(SELECT playerId
				FROM player
				WHERE groupId = " . $groupId . ") 
				GROUP BY inventory.itemId";

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
    }

}