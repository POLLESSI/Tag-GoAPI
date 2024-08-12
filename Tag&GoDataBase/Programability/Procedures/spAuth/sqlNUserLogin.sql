CREATE PROCEDURE [dbo].[sqlNUserLogin]
	@Email NVARCHAR(64),
	@Password NVARCHAR(64)
AS
BEGIN
	SET NOCOUNT OFF;

	DECLARE @Pwd BINARY(64), @SecurityStamp UNIQUEIDENTIFIER;

	SET @SecurityStamp = (SELECT SecurityStamp FROM [NUser] WHERE Email = @Email)
	SET @Pwd = dbo.fHasher(@Pwd, @SecurityStamp)

	IF EXISTS (SELECT TOP 1 * FROM [NUser] WHERE Email = @Email AND Pwd = @Pwd)
	BEGIN
		SELECT * INTO #TempNUser
		FROM [NUser]
		WHERE Email LIKE @Email
		ALTER TABLE #TempNUser
		DROP COLUMN Pwd, SecurityStamp
		SELECT * FROM #TempNUser
		DROP TABLE #TempNUser
	END

	RETURN 0

END
