CREATE TRIGGER [salary_trigger] ON [dbo].[BankAccount]
	FOR UPDATE
	AS IF (UPDATE(Salary))
	BEGIN
		SET NOCOUNT ON
		DECLARE @AccountId int = (SELECT BankAccountID FROM deleted)
		DECLARE @Salary float = (SELECT Salary FROM BankAccount WHERE BankAccountID = @AccountId)
		IF (@Salary >= 30000)
		BEGIN
			UPDATE BankAccount
			SET Overdraft = (@Salary * 0.1)
			WHERE BankAccountID = @AccountId
		END
	END
