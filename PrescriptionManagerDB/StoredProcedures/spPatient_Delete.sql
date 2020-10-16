CREATE PROCEDURE [dbo].[spPatient_Delete]
	@id int

AS
BEGIN

	SET NOCOUNT ON

	DELETE FROM dbo.Patient WHERE Id = @id;

END
