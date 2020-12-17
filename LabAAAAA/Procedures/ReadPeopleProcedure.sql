CREATE PROCEDURE ReadPeopleProcedure 
AS 
SELECT [SalesPerson].[BusinessEntityID],
       [SalesPerson].[TerritoryID],
       [SalesPerson].[SalesQuota],
       [SalesPerson].[Bonus],
       [SalesPerson].[SalesLastYear]
        FROM [AdventureWorks2019].[Sales].[SalesPerson]
EXEC ReadPeopleProcedure