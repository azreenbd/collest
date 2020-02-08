--Drop all table
ALTER TABLE player
DROP CONSTRAINT player_fk;
ALTER TABLE team
DROP CONSTRAINT team_fk;
ALTER TABLE quest
DROP CONSTRAINT quest_fk;

DROP TABLE player;
DROP TABLE team;
DROP TABLE npc;
DROP TABLE quest;
DROP TABLE groupquest;
DROP TABLE task;
DROP TABLE item;
DROP TABLE inventory;

--Create table
CREATE TABLE player
(
	playerId int NOT NULL AUTO_INCREMENT,
	username varchar(80) NOT NULL,
	email varchar(255) NOT NULL,
	password varchar(255) NOT NULL,
	dateRegistered date NOT NULL,
	xp int DEFAULT 0 NOT NULL,
	coin int DEFAULT 0 NOT NULL,
	groupId int,

	CONSTRAINT player_pk PRIMARY KEY (playerId)
);

CREATE TABLE team
(
	groupId int NOT NULL AUTO_INCREMENT,
	name varchar(80) NOT NULL,
	creator int NOT NULL,
	point int DEFAULT 0 NOT NULL,

	CONSTRAINT team_pk PRIMARY KEY (groupId) 
);

ALTER TABLE player 
ADD CONSTRAINT player_fk FOREIGN KEY (groupId) REFERENCES team(groupId) ON DELETE SET NULL;
ALTER TABLE team 
ADD CONSTRAINT team_fk FOREIGN KEY (creator) REFERENCES player(playerId) ON DELETE CASCADE;

CREATE TABLE npc
(
	npcId int NOT NULL AUTO_INCREMENT,
	name varchar(80) NOT NULL,

	CONSTRAINT npc_pk PRIMARY KEY (npcId)
);

CREATE TABLE quest
(
	questId int NOT NULL AUTO_INCREMENT,
	title varchar(255) NOT NULL,
	description varchar(255) NOT NULL,
	topic varchar(80),
	reward int NOT NULL,
	level int NOT NULL,
	npc int NOT NULL,

	CONSTRAINT quest_pk PRIMARY KEY (questId),
	CONSTRAINT quest_fk FOREIGN KEY (npc) REFERENCES npc(npcId) ON DELETE SET NULL
);

CREATE TABLE groupquest
(
	groupQuestId int NOT NULL AUTO_INCREMENT,
	groupId int NOT NULL,
	questId int NOT NULL,
	isComplete boolean DEFAULT false NOT NULL,

	CONSTRAINT GroupQuest_pk PRIMARY KEY (groupQuestId),
	CONSTRAINT GroupQuest_fk1 FOREIGN KEY (groupId) REFERENCES team(groupId) ON DELETE CASCADE,
	CONSTRAINT GroupQuest_fk2 FOREIGN KEY (questId) REFERENCES quest(questId) ON DELETE CASCADE
);

CREATE TABLE task
(
	taskId int NOT NULL AUTO_INCREMENT,
	questId int NOT NULL,
	task varchar(255) NOT NULL,
	hint varchar(255),
	itemId char(5) NOT NULL,
	itemAmount int DEFAULT 1 NOT NULL,

	CONSTRAINT task_pk PRIMARY KEY (taskId),
	CONSTRAINT task_fk1 FOREIGN KEY (questId) REFERENCES quest(questId) ON DELETE CASCADE,
	CONSTRAINT task_fk2 FOREIGN KEY (itemId) REFERENCES item(itemId) ON DELETE CASCADE
);

CREATE TABLE item
(
	itemId char(5) NOT NULL,
	name varchar(128) NOT NULL,
	price decimal(6,2) DEFAULT 0,

	CONSTRAINT item_pk PRIMARY KEY (itemId)
);

CREATE TABLE inventory
(
	inventoryId int NOT NULL AUTO_INCREMENT,
	playerId int NOT NULL,
	itemId char(5) NOT NULL,
	amount int NOT NULL,

	CONSTRAINT inventory_pk PRIMARY KEY (inventoryId),
	CONSTRAINT inventory_fk1 FOREIGN KEY (playerId) REFERENCES player(playerId) ON DELETE CASCADE,
	CONSTRAINT inventory_fk2 FOREIGN KEY (itemId) REFERENCES item(itemId) ON DELETE CASCADE
);