﻿CREATE OR ALTER PROC [dbo].[FI_SP_AltBeneficiario]	
	@CPF VARCHAR(11),
	@NOME VARCHAR(100),
	@ID bigint
AS
	UPDATE	[BENEFICIARIOS] 
	SET		CPF = @CPF, 
			NOME = @NOME 
	WHERE	ID = @ID
GO