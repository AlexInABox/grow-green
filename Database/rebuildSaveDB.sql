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
	"rotten" BOOLEAN
);
INSERT INTO listOfOwnedPlants (className, growProgress, growProgressTimestamp, waterLevel, waterLevelTimestamp, withered, rotten)
VALUES ('Agave', 0.55, 0, 0.55, 0, FALSE, FALSE),
	('Alocasia', 0.55, 0, 0.55, 0, FALSE, FALSE),
	('Calathea', 0.55, 0, 0.55, 0, FALSE, TRUE),
	('Fatsia', 0.55, 0, 0.55, 0, FALSE, FALSE),
	('Polyscias', 0.55, 0, 0.55, 0, FALSE, TRUE);