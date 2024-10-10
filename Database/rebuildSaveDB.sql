DROP TABLE IF EXISTS "playerStats";
CREATE TABLE "playerStats" (
	"username"	TEXT,
	"coins"	INTEGER,
	"characterId"	INTEGER,
	PRIMARY KEY("username")
);

DROP TABLE IF EXISTS "listOfOwnedPlants";
CREATE TABLE "listOfOwnedPlants" (
	"className"	TEXT,
	"growProgress"	FLOAT,
	"growProgressTimestamp"	INTEGER,
	"waterLevel" FLOAT,
	"waterLevelTimestamp" INTEGER,
	"withered" BOOLEAN,
	"rotten" BOOLEAN,
	"potName" TEXT,
	"spawnPoint" INTEGER
);

DROP TABLE IF EXISTS "listOfOwnedPots";
CREATE TABLE "listOfOwnedPots" (
	"potName"	TEXT
);