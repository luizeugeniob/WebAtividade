﻿CREATE OR ALTER PROC [dbo].[FI_SP_ConsBeneficiarios]
	@IDCLIENTE BIGINT
AS
BEGIN	
	SELECT ID, CPF, NOME, IDCLIENTE FROM BENEFICIARIOS WITH(NOLOCK) WHERE IDCLIENTE = @IDCLIENTE		
END

GO