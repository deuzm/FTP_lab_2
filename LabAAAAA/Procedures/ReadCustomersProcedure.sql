CREATE PROCEDURE ReadCustomersProcedure 
AS 
SELECT TOP 1000 [Customer].[CustomerID],
                [Customer].[StoreID],
                [Customer].[TerritoryID]
                FROM[AdventureWorks2019].[Sales].[Customer]
EXEC ReadCustomersProcedure