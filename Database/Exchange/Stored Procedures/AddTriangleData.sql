alter PROCEDURE [dbo].[AddTriangleData]
@pairs nvarchar(max),
@pair1price decimal(38,20),
@pair2price decimal(38,20),
@pair3price decimal(38,20),
@date datetime
AS
BEGIN

INSERT INTO [Exchange].[dbo].[TrianglesData] ([Date], Pairs, [1PairPrice], [2PairPrice], [3PairPrice])
VALUES (@date, @pairs, @pair1price, @pair2price, @pair3price)

END
