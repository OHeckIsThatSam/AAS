CREATE TRIGGER balance_trigger ON dbo.BankAccount
AFTER UPDATE
AS IF(UPDATE(Balance))

DECLARE @Diffrence FLOAT = (SELECT Balance FROM dbo.BankAccount) - (SELECT Balance FROM deleted)
DECLARE @Type nvarchar;
DECLARE @AccountId int = (SELECT BankAccountID FROM deleted)

IF(@Diffrence > 0)
BEGIN
	SET @Type = 'Deposit'
END
ELSE
BEGIN
	SET @Type = 'Withdrawal'
END

BEGIN SET NOCOUNT ON
INSERT INTO Transactions (TransactionDate, Amount, Type, BankAccountID)
VALUES (GETDATE(), @Diffrence, @Type, @AccountId)
END
