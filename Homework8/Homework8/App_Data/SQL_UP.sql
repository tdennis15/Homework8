--Homework 8 SQL up script, creation of tables and relationships

CREATE TABLE dbo.Artists
(
	ID			INT IDENTITY (1,1) NOT NULL,
	
	ArtistName		NVARCHAR(64) NOT NULL,
	DOB				NVARCHAR(64),
	BirthCity		NVARCHAR(64),
	BirthCountry	NVARCHAR(64),
	
	CONSTRAINT [PK_dbo.Artists] PRIMARY KEY CLUSTERED (ID ASC)
);

CREATE TABLE dbo.ArtWorks
(
	ID			INT IDENTITY (1,1) NOT NULL,
	Title		NVARCHAR(64) NOT NULL,
	ArtistID	INT  FOREIGN KEY REFERENCES dbo.Artists(ID),
	
	CONSTRAINT [PK_dbo.ArtWork] PRIMARY KEY CLUSTERED (ID ASC),
	Constraint [FK_dbo.Artist] Foreign Key (ArtistID)
		references dbo.Artists(ID)
		on delete Cascade
		on update cascade
);

CREATE TABLE dbo.Genre
(
	GenreID Int IDENTITY(1,1) NOT NULL,
	Genre	VARCHAR(24) NOT NULL,
	CONSTRAINT [PK_dbo.Genre] PRIMARY KEY CLUSTERED (GenreID ASC)
);

CREATE TABLE dbo.Classifications (
	CID				INT IDENTITY(1,1) NOT NULL,
	AWID			INT NOT NULL,
	GenreID			INT NOT NULL,

	CONSTRAINT[PK_dbo.class] PRIMARY KEY CLUSTERED (CID ASC),
	CONSTRAINT[FK_dbo.ArtWorks_Class] FOREIGN KEY (AWID)
		REFERENCES dbo.ARTWORKS (ID)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	CONSTRAINT[FK_dbo.GENRES_CLASSIFICATIONS] FOREIGN KEY (GenreID)
		REFERENCES dbo.Genre (GenreID)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

Insert INTO dbo.Artists (ArtistName, DOB, BirthCity, BirthCountry) Values
	('MC Escher', '06/17/1898','Leeuwarden', 'Netherlands'),
	('Leonardo Da Vinci','05/02/1519','Vinci','Italy'),
	('Hatip Mehmed Efendi','11/18/1680',null,null),
	('Salvador Dali','05/11/1904','Figures','Spain');

Insert INTO dbo.ArtWorks (Title, ArtistID) Values
	('Circle Limit III','1'),
	('Twon Tree','1'),
	('Mona Lisa','2'),
	('Vitruvian Man','2'),
	('Ebru','3'),
	('Honey Is Sweeter Than Blood','4');

Insert Into dbo.Genre(Genre) Values
	('Tesselation'),
	('Surrealism'),
	('Portrait'),
	('Renaissance');

Insert INTO dbo.Classifications(AWID, GenreID) Values
	('1','1'),
	('2','1'),
	('2','2'),
	('3','3'),
	('3','4'),
	('4','4'),
	('5','1'),
	('6','2');


	GO
