ALTER PROCEDURE [dbo].[Get_BTC_USDT_ClosedOrders_Top100]
AS
BEGIN

SELECT TOP 100 *
FROM [Exchange].[dbo].[BTC_USDT_ClosedOrders]
ORDER BY ClosedDate DESC

END
