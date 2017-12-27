USE KomunikatorDB

GO

IF OBJECT_ID('dbo.Komunikaty', 'U') IS NOT NULL 
DROP TABLE dbo.Komunikaty;
IF OBJECT_ID('dbo.Kontakty', 'U') IS NOT NULL 
DROP TABLE dbo.Kontakty;
IF OBJECT_ID('dbo.Uzytkownicy', 'U') IS NOT NULL 
DROP TABLE dbo.Uzytkownicy;



CREATE TABLE Uzytkownicy (
	login_uzytkownika VARCHAR(50) not null unique,
	haslo_uzytkownika varbinary(1024) not null,
	imie_uzytkownika TEXT not null,
	nazwisko_uzytkownika TEXT not null,
	email_uzytkownika VARCHAR(100) not null unique,
	status_uzytkownika TINYINT NOT NULL,
	sol_uzytkownika varbinary(1024),
	PRIMARY KEY(login_uzytkownika));


CREATE TABLE Kontakty (
	ID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	login_wlasciciela VARCHAR(50) REFERENCES  Uzytkownicy (login_uzytkownika),
	login_kontaktu VARCHAR(50) REFERENCES  Uzytkownicy (login_uzytkownika),
	data_dodania DATE not null,
	);

CREATE TABLE Komunikaty (
	ID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	nadawca VARCHAR(50) REFERENCES  Uzytkownicy (login_uzytkownika),
	odbiorca VARCHAR(50) REFERENCES  Uzytkownicy (login_uzytkownika),
	data_dodania DATETIME not null,
	komunikat TEXT,
	);