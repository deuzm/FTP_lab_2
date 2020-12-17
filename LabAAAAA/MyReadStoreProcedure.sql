Use AdventureWorks2019
GO
CREATE PROCEDURE MyReadStoreProcedure 
AS 
SELECT [Store].[BusinessEntityID],
        [Store].[Name],
        [Store].[SalesPersonID]
        FROM[AdventureWorks2019].[Sales].[Store]
EXEC  MyReadStoreProcedure 