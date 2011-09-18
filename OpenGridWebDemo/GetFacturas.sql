CREATE PROC [dbo].[GetFacturas]

(@RowIndex INT ,

@MaxRows INT)

AS

DECLARE @StartRow INT

DECLARE @EndRow INT

 

SET @StartRow = (@RowIndex+1) 

SET @EndRow = @StartRow + @MaxRows

 

SELECT * FROM (

SELECT     facturaid, fecha, ROW_NUMBER() OVER (ORDER BY facturaid) AS ROW

FROM         facturas) As FacturasNumeradas 

WHERE ROW BETWEEN @StartRow AND @EndRow
