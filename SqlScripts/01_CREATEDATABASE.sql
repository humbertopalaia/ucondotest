USE UCONDOTEST

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  TABLE [dbo].[ChartAccount](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ParentAccountId] [int] NULL
	   CONSTRAINT FK_ParentAccount FOREIGN KEY (ParentAccountId)
        REFERENCES [dbo].[ChartAccount] (Id),
	[Code] varchar(max) NOT NULL,
	[LevelCode] int NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Type] [varchar](1) NOT NULL,
	[AcceptEntry] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL
) ON [PRIMARY]


GO

