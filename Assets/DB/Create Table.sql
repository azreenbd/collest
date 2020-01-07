--Drop all table
ALTER TABLE Player
DROP CONSTRAINT player_fk;
ALTER TABLE Team
DROP CONSTRAINT team_fk;
ALTER TABLE Quest
DROP CONSTRAINT quest_fk;

DROP TABLE Player;
DROP TABLE Team;
DROP TABLE NPC;
DROP TABLE Quest;
DROP TABLE GroupQuest;
DROP TABLE Task;
DROP TABLE Item;
DROP TABLE Inventory;

--Create table
CREATE TABLE Player
(
	playerId int NOT NULL AUTO_INCREMENT,
	username varchar(80) NOT NULL UNIQUE,
	email varchar(255) NOT NULL UNIQUE,
	password varchar(255) NOT NULL,
	dateRegistered date NOT NULL,
	xp int DEFAULT 0 NOT NULL,
	groupId int,

	CONSTRAINT player_pk PRIMARY KEY (playerId)
);

CREATE TABLE Team
(
	groupId int NOT NULL AUTO_INCREMENT,
	name varchar(80) NOT NULL,
	creator int NOT NULL UNIQUE,
	point int DEFAULT 0 NOT NULL,

	CONSTRAINT team_pk PRIMARY KEY (groupId) 
);

ALTER TABLE Player 
ADD CONSTRAINT player_fk FOREIGN KEY (groupId) REFERENCES Team(groupId);
ALTER TABLE Team 
ADD CONSTRAINT team_fk FOREIGN KEY (creator) REFERENCES Player(playerId);

CREATE TABLE NPC
(
	npcId int NOT NULL AUTO_INCREMENT,
	name varchar(80) NOT NULL,

	CONSTRAINT npc_pk PRIMARY KEY (npcId)
);

CREATE TABLE Quest
(
	questId int NOT NULL AUTO_INCREMENT,
	description varchar(255) NOT NULL,
	topic varchar(80),
	reward int NOT NULL,
	level int NOT NULL,
	npc int NOT NULL,

	CONSTRAINT quest_pk PRIMARY KEY (questId),
	CONSTRAINT quest_fk FOREIGN KEY (npc) REFERENCES NPC(npcId)
);

CREATE TABLE GroupQuest
(
	groupQuestId int NOT NULL AUTO_INCREMENT,
	groupId int NOT NULL,
	questId int NOT NULL,
	progress int NOT NULL,
	isComplete boolean DEFAULT false NOT NULL,

	CONSTRAINT GroupQuest_pk PRIMARY KEY (groupQuestId),
	CONSTRAINT GroupQuest_fk1 FOREIGN KEY (groupId) REFERENCES Team(groupId),
	CONSTRAINT GroupQuest_fk2 FOREIGN KEY (questId) REFERENCES Quest(questId)
);

CREATE TABLE Task
(
	taskId int NOT NULL AUTO_INCREMENT,
	questId int NOT NULL,
	task varchar(255) NOT NULL,

	CONSTRAINT task_pk PRIMARY KEY (taskId),
	CONSTRAINT task_fk FOREIGN KEY (questId) REFERENCES Quest(questId)
);

CREATE TABLE Item
(
	itemId int NOT NULL AUTO_INCREMENT,
	name varchar(128) NOT NULL,
	price decimal(6,2) DEFAULT 0,

	CONSTRAINT item_pk PRIMARY KEY (itemId)
);

CREATE TABLE Inventory
(
	inventoryId int NOT NULL AUTO_INCREMENT,
	playerId int NOT NULL,
	itemId int NOT NULL,
	amount int NOT NULL,

	CONSTRAINT inventory_pk PRIMARY KEY (inventoryId),
	CONSTRAINT inventory_fk1 FOREIGN KEY (playerId) REFERENCES Player(playerId),
	CONSTRAINT inventory_fk2 FOREIGN KEY (itemId) REFERENCES Item(itemId)
);