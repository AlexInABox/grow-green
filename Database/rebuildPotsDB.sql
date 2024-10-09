DROP TABLE IF EXISTS pots;
CREATE TABLE "pots" (
	"potName"	TEXT,
	"cost"	INTEGER,
	PRIMARY KEY("potName")
);

INSERT INTO pots (potName, cost)
VALUES ("animal_giraffe", 8),
	("animal_tiger", 8),
	("animal_zebra", 8),
	("animal_chicken", 8),
	("minecraft_chicken", 10),
	("minecraft_creeper", 10),
	("minecraft_enderman", 10),
	("minecraft_pig", 10),
	("minecraft_sheep", 10),
	("minecraft_zombie", 10),
	("plain_black", 1),
	("plain_blue_dark", 1),
	("plain_blue_light", 1),
	("plain_brown", 1),
	("plain_green_dark", 1),
	("plain_green_light", 1),
	("plain_grey", 1),
	("plain_orange", 1),
	("plain_pink", 1),
	("plain_pink_light", 1),
	("plain_purple", 1),
	("plain_red", 1),
	("plain_white", 1),
	("plain_yellow", 1),
	("skin_chess", 6),
	("skin_germany", 5),
	("skin_gold", 10),
	("skin_rainbow", 3),
	("default", 0);