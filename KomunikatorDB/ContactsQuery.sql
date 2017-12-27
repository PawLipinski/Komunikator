select Uzytkownicy.imie_uzytkownika, Uzytkownicy.nazwisko_uzytkownika, status_uzytkownika
FROM (Kontakty INNER JOIN Uzytkownicy ON Uzytkownicy.login_uzytkownika=Kontakty.login_kontaktu)
Where Kontakty.login_wlasciciela like 'ppl'