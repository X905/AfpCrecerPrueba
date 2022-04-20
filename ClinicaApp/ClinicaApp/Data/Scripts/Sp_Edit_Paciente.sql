SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Daniel Castillo>
-- Create date: <20 de abril del 2022>
-- Description:	<Creacion de pacientes>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Edit_Pacientes]
	--parameters 
	@Id INT,
	@Nombres VARCHAR(100),
	@Apellidos VARCHAR(100),
	@FechaNacimiento DATE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	SET NOCOUNT ON;

    -- statement for procedure here
	UPDATE [dbo].[Paciente]
	SET Nombres = @Nombres, Apellidos = @Apellidos, FechaNacimiento = @FechaNacimiento, FechaActualizacion = GETDATE()
	WHERE Id = @Id
END
GO
