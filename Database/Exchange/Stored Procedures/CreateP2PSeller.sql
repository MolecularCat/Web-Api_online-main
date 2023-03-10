USE [Exchange]
GO
/****** Object:  StoredProcedure [dbo].[CreateP2PSeller]    Script Date: 16.08.2022 23:56:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[CreateP2PSeller]
@userId nvarchar(450),
@price decimal(38,20),
@p2pFiatId int,
@limitFrom decimal(38,20),
@limitTo decimal(38,20),
@available decimal(38,20),
@p2pCryptId int,
@p2pTimeFrameId int
AS
BEGIN

INSERT INTO [Exchange].[dbo].[P2PSellers] (UserId, Price, P2PFiatId, LimitFrom, LimitTo, Available, P2PCryptId, P2PTimeFrameId)
VALUES (@userId, @price, @p2pFiatId, @limitFrom, @limitTo, @available,  @p2pCryptId, @p2pTimeFrameId)

END
