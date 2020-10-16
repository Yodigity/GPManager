CREATE PROCEDURE [dbo].[spPatient_GetNotes]
	@id int 
	
AS
BEGIN

SET NOCOUNT ON	

	SELECT Id, PatientId, Title, [Text], AuthorId, CreatedDate 
	FROM dbo.Note
	WHERE PatientId = @id


END
