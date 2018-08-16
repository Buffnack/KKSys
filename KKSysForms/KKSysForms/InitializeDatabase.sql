create Table KarteiArt( ID INTEGER PRIMARY KEY ASC, typus varchar not null);
create Table EventLabel (ID INTEGER PRIMARY KEY AUTOINCREMENT, nameOf varchar not null);
create Table RepeatEvents (ID INTEGER PRIMARY KEY AUTOINCREMENT, LabelID INTEGER NOT NULL, NameOf varchar,serialized BLOB, DayCode varchar not null, FOREIGN KEY(LabelID) REFERENCES EventLabel(ID));
create Table UniqueEvents (ID INTEGER PRIMARY KEY AUTOINCREMENT, LabelID INTEGER NOT NULL, NameOf varchar, serialized BLOB, day date not null, FOREIGN KEY(LabelID) REFERENCES EventLabel(ID));
create Table ReplaceEvents (ID INTEGER PRIMARY KEY AUTOINCREMENT, RepeatEvent INTEGER NOT NULL, NameOf varchar, serialized BLOB, Date varchar not null, FOREIGN KEY(RepeatEvent) REFERENCES RepeatEvents(ID));
create Table KarteiCard (ID INTEGER PRIMARY KEY ASC, serialized blob, kindOF INTEGER, belongTo INTEGER, tagID int, FOREIGN KEY(kindOf) REFERENCES KarteiArt(ID), FOREIGN KEY (belongTo) REFERENCES EventLabel(ID), FOREIGN KEY (tagID) REFERENCES Tag(ID));
create Table Tag (ID INTEGER PRIMARY KEY ASC, nameOf varchar, kurz varchar, code blob)