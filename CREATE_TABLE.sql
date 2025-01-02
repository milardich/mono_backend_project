CREATE TABLE vehicle_make (
	id INT PRIMARY KEY NOT NULL,
	name TEXT NOT NULL,
	abrv CHAR(10)
);

CREATE TABLE vehicle_model (
	id INT PRIMARY KEY NOT NULL,
	make_id INT NOT NULL,
	name TEXT NOT NULL,
	abrv CHAR(10)
);