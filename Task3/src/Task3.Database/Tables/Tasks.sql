CREATE TABLE [dbo].[Tasks]
(
  [Id] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
  [Title] NVARCHAR(MAX),
  [Description] NVARCHAR(MAX),
  [IsCompleted] BIT,
  [CreatedAt] DATETIME
)
