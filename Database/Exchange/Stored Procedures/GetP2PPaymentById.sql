SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetP2PPaymentById]
@id int
AS
BEGIN

SELECT * FROM [Exchange].[dbo].[P2PPayments] 
WHERE 
Id = @id

END
