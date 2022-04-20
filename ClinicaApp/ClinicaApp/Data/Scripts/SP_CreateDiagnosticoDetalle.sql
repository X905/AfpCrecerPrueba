SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Daniel Castillo>
-- Create date: <20 de abril del 2022>
-- Description:	<Creacion de pacientes>
-- =============================================
CREATE PROCEDURE [dbo].[sp_CreateDiagnosticoDetalle]
	--parameters 
	
	@PruebaNombre VARCHAR(30),
	@Observacion VARCHAR (150),
	@IdDiagnostico INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	SET NOCOUNT ON;

    -- statement for procedure here
	INSERT INTO [dbo].[DiagnosticoDetalle] (PruebaNombre,Observacion,IdDiagnostico)
	VALUES(@PruebaNombre,@Observacion,@IdDiagnostico)
END
GO