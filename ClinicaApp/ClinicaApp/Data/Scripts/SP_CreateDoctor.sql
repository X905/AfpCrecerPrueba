SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Daniel Castillo>
-- Create date: <20 de abril del 2022>
-- Description:	<Creacion de pacientes>
-- =============================================
CREATE PROCEDURE [dbo].[sp_CreateDoctor]
	--parameters 
	@Nombres VARCHAR(100),
	@Apellidos VARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	SET NOCOUNT ON;

    -- statement for procedure here
	INSERT INTO [dbo].[Doctor] (Nombres,Apellidos) 
	VALUES (@Nombres, @Apellidos)
	
END
GO