﻿CREATE TABLE [dbo].[NIcon]
(
	[NIcon_Id] INT IDENTITY,
	[NIconName] NVARCHAR(32) NOT NULL,
	[NIconDescription] NVARCHAR(128) NOT NULL,
	[NIconUrl] NVARCHAR(MAX) NOT NULL,
	[Active] BIT DEFAULT 1

	CONSTRAINT PK_NIcon PRIMARY KEY ([NIcon_Id])
)

GO

CREATE TRIGGER [dbo].[OnDeleteNIcon]
	ON [dbo].[NIcon]
	INSTEAD OF DELETE
	AS 
	BEGIN
		UPDATE NIcon SET Active = 0
		WHERE NIcon_Id = (SELECT NIcon_Id FROM deleted)
	END