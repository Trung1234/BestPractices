
 CREATE OR ALTER PROCEDURE uspSumPrice(
 @sum DECIMAL OUTPUT) 	AS 
	BEGIN
		DECLARE  @list_price DECIMAL,@p_sum DECIMAL;
		SET @p_sum = 0;
		-- declare  a cursor to hold the result of a query 
		DECLARE cursor_product CURSOR
		FOR SELECT 
        list_price FROM  production.products;
		---  open the cursor
		OPEN cursor_product;
		--Then, fetch a row from the cursor into one or more variables--
		FETCH NEXT FROM cursor_product INTO 
    @list_price;
		PRINT @list_price;
	-- Iterate until FETCH statement was successful
	WHILE @@FETCH_STATUS = 0
		BEGIN
			SET @p_sum = @p_sum +   @list_price;
			FETCH NEXT FROM cursor_product INTO 
				@list_price;
		END;
	SET @sum = @p_sum;
	-- After that, close the cursor--
	CLOSE cursor_product;
	-- Finally, deallocate the cursor:--
	DEALLOCATE cursor_product;
	END;