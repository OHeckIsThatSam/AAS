CREATE TRIGGER balance_trigger ON dbo.BankAccount
AFTER UPDATE
AS IF(UPDATE(Balance))
BEGIN
DECLARE @AccountId int = (SELECT BankAccountID FROM deleted)
DECLARE @Diffrence FLOAT = (SELECT Balance FROM dbo.BankAccount WHERE BankAccountID = @AccountId) - (SELECT Balance FROM deleted)
DECLARE @Type nvarchar(10);

IF(@Diffrence > 0)
BEGIN
	SET @Type = 'Deposit'
END
ELSE
BEGIN
	SET @Type = 'Withdrawal'
END

SET NOCOUNT ON
INSERT INTO Transactions (TransactionDate, Amount, Type, BankAccountID)
VALUES (GETDATE(), @Diffrence, @Type, @AccountId)
END
