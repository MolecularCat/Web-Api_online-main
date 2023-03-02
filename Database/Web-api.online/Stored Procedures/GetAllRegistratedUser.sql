USE [web-api.online]
GO
/****** Object:  StoredProcedure [dbo].[GetAllRegistratedUser]    Script Date: 16.01.2022 13:22:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetAllRegistratedUser]

AS
BEGIN

SELECT AspNetUsers.Email, UsersInfo.FullName, UsersInfo.RegistrationDate 
FROM AspNetUsers 
LEFT JOIN UsersInfo
ON AspNetUsers.Id = UsersInfo.UserId
WHERE AspNetUsers.Id IS NOT NULL
AND UsersInfo.UserId IS NOT NULL
	
END