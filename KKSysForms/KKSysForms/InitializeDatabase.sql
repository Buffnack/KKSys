create Table EventArt IF NOT EXISTS ( ID INTEGER PRIMARY KEY ASC, Discription varchar not null);
create Table KarteiArt IF NOT EXISTS( ID INTEGER PRIMARY KEY ASC, typus varchar not null);
create Table EventLabel IF NOT EXISTS(ID INTEGER PRIMARY KEY ASC, nameOf varchar not null);
create Table EventsTable IF NOT EXISTS(ID INTEGER PRIMARY KEY ASC, belongTo INTEGER, timeStart INTEGER, timeEnd INTEGER, nameOf varchar, dayCode Integer not null, FOREIGN KEY(belongTo) REFERENCES EventLabel(ID));
create Table KarteiCard IF NOT EXISTS (ID INTEGER PRIMARY KEY ASC, serialized blob, kindOF INTEGER, belongTo INTEGER, tagID int, FOREIGN KEY(kindOf) REFERENCES KarteiArt(ID), FOREIGN KEY (belongTo) REFERENCES EventLabel(ID), FOREIGN KEY (tagID) REFERENCES Tag(ID));
create Table Tag IF NOT EXISTS (ID INTEGER PRIMARY KEY ASC, nameOf varchar, kurz varchar, code blob)