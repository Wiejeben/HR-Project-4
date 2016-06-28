BEGIN TRANSACTION;
CREATE TABLE `streets` (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`name`	TEXT NOT NULL,
	`district_id`	INTEGER NOT NULL,
	FOREIGN KEY(district_id) REFERENCES districts(id)
);
CREATE TABLE `districts` (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`name`	TEXT
);
CREATE TABLE `colors` (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`name`	TEXT
);
CREATE TABLE "brands" (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`name`	TEXT NOT NULL
);
CREATE TABLE "bikethefts" (
	`id`	TEXT NOT NULL,
	`date`	INTEGER NOT NULL,
	`brand_id`	INTEGER,
	`color_id`	INTEGER,
	`street_id`	INTEGER,
	PRIMARY KEY(id),
	FOREIGN KEY(street_id) REFERENCES streets(id),
	FOREIGN KEY(color_id) REFERENCES colors(id),
	FOREIGN KEY(brand_id) REFERENCES brands(id)
);
CREATE TABLE "bikecontainers" (
	`id`	TEXT NOT NULL,
	`lat`	REAL,
	`long`	REAL,
	`street_id`	INTEGER,
	PRIMARY KEY(id),
	FOREIGN KEY(street_id) REFERENCES streets(id)
);
COMMIT;
