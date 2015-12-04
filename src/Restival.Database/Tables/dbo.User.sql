CREATE TABLE [dbo].[User]
(
[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_User_Id] DEFAULT (newsequentialid()),
[Username] [nvarchar] (32) COLLATE Latin1_General_CI_AS NOT NULL,
[Password] [nvarchar] (128) COLLATE Latin1_General_CI_AS NULL,
[Name] [varchar] (128) COLLATE Latin1_General_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[User] ADD CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
