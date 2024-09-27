DROP TABLE IF EXISTS "player";
CREATE TABLE "player" (
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
	PRIMARY KEY("className")
);
INSERT INTO listOfOwnedPlants (className, growProgress, growProgressTimestamp, waterLevel, waterLevelTimestamp, withered, rotten)
VALUES ('Agave', 0.55, 0, 0.55, 0, FALSE, FALSE);