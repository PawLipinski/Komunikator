create database KomunikatorDB
USE KomunikatorDB

GO

IF OBJECT_ID('dbo.Uzytkownicy', 'U') IS NOT NULL 
DROP TABLE dbo.Uzytkownicy;


CREATE TABLE Uzytkownicy (
	ID int NOT NULL IDENTITY PRIMARY KEY,
	login_uzytkownika VARCHAR(50) not null unique,
	haslo_uzytkownika TEXT not null,
	imie_uzytkownika TEXT not null,
	nazwisko_uzytkownika TEXT not null,
	email_uzytkownika VARCHAR(100) not null unique,
	);
