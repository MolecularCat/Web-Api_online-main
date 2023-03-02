USE [web-api.online]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetAllRefferals]
AS
BEGIN

SELECT 
anu.Email,
anu.UserName,
ui.FullName,
ui.RegistrationDate,
ui.ReffererId

FROM AspNetUsers as anu
LEFT JOIN UsersInfo as ui
ON anu.Id = ui.UserId
WHERE ui.ReffererId IS NOT NULL

END