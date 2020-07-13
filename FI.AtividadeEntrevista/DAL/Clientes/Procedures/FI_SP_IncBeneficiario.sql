﻿CREATE OR ALTER  PROC [dbo].[FI_SP_IncBeneficiario]	
	@CPF VARCHAR(11),
	@NOME VARCHAR(100),
	@IDCLIENTE bigint
AS
	INSERT INTO [dbo].[BENEFICIARIOS] ([CPF],[NOME],[IDCLIENTE])
	VALUES(@CPF,@NOME,@IDCLIENTE);

	SELECT CAST(SCOPE_IDENTITY() AS BIGINT)
GO