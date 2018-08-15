create Table KarteiArt IF NOT EXISTS( ID INTEGER PRIMARY KEY ASC, typus varchar not null);
create Table EventLabel IF NOT EXISTS(ID INTEGER PRIMARY KEY ASC, nameOf varchar not null);
create Table RepeatEvents IF NOT EXISTS(ID INTEGER PRIMARY KEY AUTOINCREMENT, LabelID INTEGER NOT NULL, NameOf varchar,serialized BLOB, DayCode varchar not null, FOREIGN KEY(LabelID) REFERENCES EventLabel(ID));
create Table UniqueEvents IF NOT EXISTS(ID INTEGER PRIMARY KEY AUTOINCREMENT, LabelID INTEGER NOT NULL, NameOf varchar, serialized BLOB, day date not null, FOREIGN KEY(LabelID) REFERENCES EventLabel(ID));
create Table ReplaceEvents IF NOT EXISTS(ID INTEGER PRIMARY KEY AUTOINCREMENT, RepeatEvent INTEGER NOT NULL, NameOf varchar, serialized BLOB, Date varchar not null, FOREIGN KEY(RepeatEvent) REFERENCES RepeatEvents(ID));
create Table KarteiCard IF NOT EXISTS (ID INTEGER PRIMARY KEY ASC, serialized blob, kindOF INTEGER, belongTo INTEGER, tagID int, FOREIGN KEY(kindOf) REFERENCES KarteiArt(ID), FOREIGN KEY (belongTo) REFERENCES EventLabel(ID), FOREIGN KEY (tagID) REFERENCES Tag(ID));
create Table Tag IF NOT EXISTS (ID INTEGER PRIMARY KEY ASC, nameOf varchar, kurz varchar, code blob)