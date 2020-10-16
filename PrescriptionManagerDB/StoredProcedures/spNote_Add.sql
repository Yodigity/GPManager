CREATE PROCEDURE [dbo].[spNote_Add]
	@PatientId nvarchar(128),
	@Title nvarchar(50),
	@Text text,
	@AuthorId nvarchar(128)
AS
BEGIN
SET NOCOUNT ON

	INSERT INTO Note (PatientId, Title, [Text], AuthorId)
	VALUES (@PatientId, @Title, @Text, @AuthorId)

END

