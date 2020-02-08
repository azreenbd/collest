INSERT INTO item VALUES ('p0001', 'Water Bottle', 1.00);
INSERT INTO item VALUES ('p0002', 'Air Pump', 5.00);
INSERT INTO item VALUES ('p0003', 'Duct Tape', 0.50);
INSERT INTO item VALUES ('p0004', 'Paper', 0.10);
INSERT INTO item VALUES ('p0005', 'Plastic Bag', 0.20);
INSERT INTO item VALUES ('p0006', 'Drinking Glass', 0.50);
INSERT INTO item VALUES ('p0007', 'Vegetable Oil', 1.00);
INSERT INTO item VALUES ('p0008', 'Salt', 0.20);


INSERT INTO npc VALUES (1, 'John Smith');

--id, title, desc, topic, reward, level, npcId
INSERT INTO quest VALUES (1, 'Air Rocket', 'This is the learning objective', 'Physics', 100, 1, 1);
INSERT INTO quest VALUES (2, 'Lava Lamp', 'This is the learning objective', 'Chemistry', 100, 1, 1);

--questId, task, hint
INSERT INTO task (questId, task, hint, itemId, itemAmount) VALUES (1, 'Find 1 water bottle', 'Take a look at a recyling bin or shop.', 'p0001', 1);
INSERT INTO task (questId, task, hint, itemId, itemAmount) VALUES (1, 'Find 1 air pump', 'Scattered somewhere around the plaza.', 'p0002', 1);
INSERT INTO task (questId, task, hint, itemId, itemAmount) VALUES (1, 'Find 1 duct tape', 'Take a look at a recyling bin or shop.', 'p0003', 1);

INSERT INTO task (questId, task, hint, itemId, itemAmount) VALUES (2, 'Find 1 glass', 'Take a look at a recyling bin or shop.', 'p0006', 1);
INSERT INTO task (questId, task, hint, itemId, itemAmount) VALUES (2, 'Find 1 vegetable oil', 'Scattered somewhere around the plaza or at a shop.', 'p0007', 1);
INSERT INTO task (questId, task, hint, itemId, itemAmount) VALUES (2, 'Find 1 salt', 'Scattered somewhere around the plaza or at a shop.', 'p0008', 1);