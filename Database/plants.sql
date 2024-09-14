DROP TABLE IF EXISTS plants;
CREATE TABLE "plants" (
	"class"	TEXT,
	"name"	TEXT,
	"water"	INTEGER,
	"place"	TEXT,
	"level"	TEXT,
	"cost" INTEGER,
	"value" INTEGER,
	"yield" INTEGER,
	PRIMARY KEY("class")
);

INSERT INTO plants (class, name, water, place, level, cost, value, yield)
VALUES ('Agave', 'Jahrhundertpflanze', 10, 'sunny', 'easy', 5, 3, 1),
	('Aglaonema', 'Kolbenpfaden', 7, 'cloudy', 'easy', 5, 3, 1),
	('Alocasia', 'Elefantenohr', 3, 'sunny', 'hard', 8, 6, 3),
	('Aloe Vera', 'Wuestenlilie', 10, 'sunny', 'easy', 5, 3, 1),
	('Anthurium', 'Flamingoblumen', 7, 'partly', 'medium', 7, 5, 2),
	('Polyscias', 'Fiederaralien', 3, 'partly', 'medium', 7, 5, 2),
	('Areca', 'Goldfruchtpalme', 3, 'partly', 'medium', 7, 5, 2),
	('Asparagus', 'Zier-Spargel', 7, 'partly', 'medium', 7, 5, 2),
	('Aspidistra', 'Schusterpalme', 7, 'partly', 'easy', 5, 3, 1),
	('Asplenium', 'Nestfarn', 3, 'partly', 'medium', 7, 5, 2),
	('Beaucarnea', 'Elefantenfuß', 10, 'sunny', 'easy', 5, 3, 1),
	('Begonia', 'Forellenbegonie', 3, 'partly', 'hard', 8, 6, 3),
	('Cactaceae', 'Kaktus', 10, 'sunny', 'easy', 5, 3, 1),
	('Caladium', 'Kaladie', 7, 'partly', 'hard', 8, 6, 3),
	('Calathea', 'Pfeilwurze', 3, 'cloudy', 'hard', 8, 6, 3),
	('Caryota', 'Fischschwanzpalme', 3, 'cloudy', 'easy', 5, 3, 1),
	('Ceropegia', 'Leuchterblume', 10, 'partly', 'easy', 5, 3, 1),
	('Chamaedorea', 'Bergpalme', 3, 'partly', 'medium', 7, 5, 2),
	('Chlorophytum', 'Grünlilie', 7, 'partly', 'easy', 5, 3, 1),
	('Cissus', 'Klimme', 7, 'partly', 'medium', 7, 5, 2),
	('Clusia', 'Balsamapfel', 7, 'partly', 'easy', 5, 3, 1),
	('Coffea Arabica', 'Kaffeepflanze', 7, 'partly', 'medium', 7, 5, 2),
	('Cordyline', 'Keulenlilie', 7, 'partly', 'medium', 7, 5, 2),
	('Crassula', 'Jadestrauch', 10, 'sunny', 'easy', 5, 3, 1),
	('Croton', 'Wunderstrauch', 3, 'sunny', 'medium', 7, 5, 2),
	('Dieffenbachia', 'Schweigrohr', 7, 'partly', 'medium', 7, 5, 2),
	('Dracaena', 'Drachenbaum', 10, 'cloudy', 'easy', 5, 3, 1),
	('Echeveria', '', 10, 'sunny', 'easy', 5, 3, 1),
	('Epiphyllum', 'Sägekaktus', 10, 'partly', 'easy', 5, 3, 1),
	('Fatsia', 'Zimmeraralie', 7, 'partly', 'medium', 7, 5, 2),
	('Ficus', 'Gummibaum', 3, 'sunny', 'easy', 5, 3, 1),
	('Hedera', 'Efeu', 3, 'sunny', 'medium', 7, 5, 2),
	('Heteropanax', '', 3, 'sunny', 'medium', 7, 5, 2),
	('Homalomena', '', 3, 'partly', 'easy', 5, 3, 1),
	('Howea Fosteriana', 'Kentia-Palme', 3, 'cloudy', 'easy', 5, 3, 1),
	('Licuala Grandis', 'Fächerpalme', 7, 'partly', 'hard', 8, 6, 3),
	('Maranta', 'Gebetspflanze', 3, 'partly', 'medium', 7, 5, 2),
	('Monstera', 'Fensterblatt', 7, 'cloudy', 'easy', 5, 3, 1),
	('Musa', 'Bananenpflanze', 3, 'sunny', 'medium', 7, 5, 2),
	('Nephrolepis', 'Schwertfarn', 3, 'partly', 'hard', 8, 6, 3),
	('Pachira', 'Glückskastanie', 10, 'partly', 'easy', 5, 3, 1),
	('Peperomia', 'Zwergpfeffer', 7, 'partly', 'medium', 7, 5, 2),
	('Phalaenopsis', 'Orchidee', 10, 'sunny', 'hard', 8, 6, 3),
	('Philodendron', 'Baumfreund', 7, 'cloudy', 'easy', 5, 3, 1),
	('Phlebodium', 'Tüpfelfarn', 3, 'cloudy', 'medium', 7, 5, 2),
	('Phoenix', 'Dattelpalme', 3, 'sunny', 'hard', 8, 6, 3),
	('Pilea', 'Pfannkuchenpflanze', 7, 'partly', 'medium', 7, 5, 2),
	('Platycerium', 'Geweihfarn', 7, 'partly', 'medium', 7, 5, 2),
	('Rhapis', 'Steckenpalme', 10, 'cloudy', 'easy', 5, 3, 1),
	('Rhipsalis', 'Korallenkaktus', 10, 'cloudy', 'medium', 7, 5, 2),
	('Sansevieria', 'Bogenhanf', 10, 'partly', 'easy', 5, 3, 1),
	('Schefflera', 'Strahlenaralie', 3, 'partly', 'easy', 5, 3, 1),
	('Scindapsus', 'Efeutute', 7, 'partly', 'medium', 7, 5, 2),
	('Spathiphyllum', 'Einblatt', 3, 'partly', 'easy', 5, 3, 1),
	('Strelitzia', 'Paradiesvogelblume', 3, 'sunny', 'easy', 5, 3, 1),
	('Syngonium', 'Purpurtute', 7, 'partly', 'medium', 7, 5, 2),
	('Tradescantia', 'Wanderjude', 7, 'partly', 'medium', 7, 5, 2),
	('Veitchia', '', 3, 'cloudy', 'easy', 5, 3, 1),
	('Yucca', 'Palmlilie', 10, 'sunny', 'easy', 5, 3, 1),
	('Zamioculcas', 'Glücksfeder', 10, 'cloudy', 'easy', 5, 3, 1);