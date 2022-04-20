SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Daniel Castillo>
-- Create date: <20 de abril del 2022>
-- Description:	<Creacion de pacientes>
-- =============================================
CREATE PROCEDURE [dbo].[sp_CreateCita]
	--parameters 
	@FechaCita DATETIME2,
	@IdPaciente INT,
	@IdDoctor INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	SET NOCOUNT ON;

    -- statement for procedure here
	INSERT INTO [dbo].[Citas] (FechaCita,IdPaciente,IdDoctor)
	VALUES(@FechaCita, @IdPaciente,@IdDoctor)
END
GO