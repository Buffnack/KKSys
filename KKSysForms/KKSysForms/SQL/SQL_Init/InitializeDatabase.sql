﻿create Table EventLabel (ID INTEGER PRIMARY KEY AUTOINCREMENT, nameOf varchar not null);
create Table RepeatEvents (ID INTEGER PRIMARY KEY AUTOINCREMENT, LabelID INTEGER NOT NULL, NameOf varchar,serialized BLOB, DayCode varchar not null, FOREIGN KEY(LabelID) REFERENCES EventLabel(ID));
create Table UniqueEvents (ID INTEGER PRIMARY KEY AUTOINCREMENT, LabelID INTEGER NOT NULL, NameOf varchar, serialized BLOB, day date not null, FOREIGN KEY(LabelID) REFERENCES EventLabel(ID));
create Table ReplaceEvents (ID INTEGER PRIMARY KEY AUTOINCREMENT, RepeatEvent INTEGER NOT NULL, NameOf varchar, serialized BLOB, Date varchar not null, FOREIGN KEY(RepeatEvent) REFERENCES RepeatEvents(ID));
create Table Thema(ID INTEGER PRIMARY KEY AUTOINCREMENT, nameOf varchar, belongsTo Integer not null, FOREIGN KEY (belongsTo) References EventLabel(ID));
insert into EventLabel(ID, nameOf) VALUES (0, 'None');
insert into Thema (ID, nameOF, belongsTo) VALUES (0, 'None', 0);
create Table Tags( ID INTEGER PRIMARY KEY AUTOINCREMENT, nameOf varchar);
insert into Tags(ID, nameOf) VALUES (0, '-');
create Table ContentCards(ID INTEGER PRIMARY KEY AUTOINCREMENT, ThemeID integer, Tag integer, serialized BLOB, FOREIGN KEY(ThemeID) REFERENCES Thema(ID), FOREIGN KEY (Tag) references Tags(ID));
insert into ContentCards(ID, ThemeID, Tag, serialized) VALUES (0,0,0,NULL);
create Table QACard(ID INTEGER PRIMARY KEY AUTOINCREMENT, ThemeID integer, Tag integer, serialized BLOB, FOREIGN KEY(ThemeID) references Thema(ID), FOREIGN KEY (Tag) references Tags(ID));