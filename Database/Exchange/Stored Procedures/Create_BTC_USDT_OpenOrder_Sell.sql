ALTER PROCEDURE [dbo].[Create_BTC_USDT_OpenOrder_Sell]
@userid nvarchar(450),
@price decimal(38,20),
@amount decimal(38,20),
@total decimal(38,20),
@new_identity bigint OUTPUT
AS
BEGIN

INSERT INTO [Exchange].[dbo].[BTC_USDT_OpenOrders_Sell] (Price, Amount, Total, CreateUserId)
VALUES (@price, @amount, @total, @userid)

set @new_identity = SCOPE_IDENTITY()

END
