/************* CRIACAO TB PRODUTOS **************/
USE [loja]
GO

/****** Object:  Table [dbo].[produtos]    Script Date: 06/03/2018 23:25:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[produtos](
	[nome] [nvarchar](50) NOT NULL,
	[categoria] [nvarchar](20) NOT NULL,
	[preco] [float] NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO



/************* CRIACAO SP usp_find_produtos **************/
USE [loja]
GO

/****** Object:  StoredProcedure [dbo].[usp_find_produtos]    Script Date: 06/03/2018 23:26:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[usp_find_produtos]
	@id int
 AS   
    SET NOCOUNT ON;

	if @id = 0 
		select id, categoria, nome, preco from produtos;
	else
		select id, categoria, nome, preco from produtos where id = @id;
return

GO



/************* CRIACAO SP usp_ins_alt_produto **************/
USE [loja]
GO

/****** Object:  StoredProcedure [dbo].[usp_ins_alt_produto]    Script Date: 06/03/2018 23:40:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Batch submitted through debugger: SQLQuery7.sql|7|0|C:\Users\Rafael\AppData\Local\Temp\~vsC3F8.sql
CREATE PROCEDURE [dbo].[usp_ins_alt_produto]
    @id int,
	@nome char(50),
	@categoria char(20),
	@preco float
AS   
    SET NOCOUNT ON;

	/*DECLARE v_count INTEGER FOR
	select count(id) from produtos where nome*/

	PRINT 'Boolean_expression is true.'  

	if @nome = 'RAFAEL' 
		THROW 50091, 'Este é um nome reservado e por isso não pode ser cadastrado.', 1;

	if (select LEN(@nome)) > 20 
		THROW 50092, 'Limite de caracteres excedido para o campo Nome.', 1;

	if (select LEN(@categoria)) > 10 
		THROW 50093, 'Limite de caracteres excedido para o campo Categoria.', 1;
		
	DECLARE @count INT;

	if @id = 0
		begin			
			select @count = count(id) from produtos where nome = @nome;
			if @count > 0
				THROW 50094, 'Produto já cadastrado!', 1;

			INSERT INTO produtos
				(nome, categoria, preco)
			VALUES        
				(@nome,@categoria,@preco);
		end;
	else
		begin
			select @count = count(id) from produtos where nome = @nome and id <> @id;
			if @count > 0
				THROW 50094, 'Produto já cadastrado!', 1;

			UPDATE 
				produtos
			SET
				nome = @nome,
				categoria = @categoria,
				preco = @preco
			WHERE (id = @id);
		end;
GO





/************* CRIACAO SP usp_del_produto **************/
USE [loja]
GO

/****** Object:  StoredProcedure [dbo].[usp_del_produto]    Script Date: 06/03/2018 23:27:44 ******/
DROP PROCEDURE [dbo].[usp_del_produto]
GO

/****** Object:  StoredProcedure [dbo].[usp_del_produto]    Script Date: 06/03/2018 23:27:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_del_produto]
    @id int
AS   
    SET NOCOUNT ON;

	delete from produtos where id = @id;
GO


