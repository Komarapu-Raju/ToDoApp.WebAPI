CREATE TABLE [dbo].[ToDo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NOT NULL,
	[TargetDate] [datetime] NULL,
	[IsCompleted] [bit] NOT NULL,
	[CompletedOn] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedOn] [datetime] NULL
)