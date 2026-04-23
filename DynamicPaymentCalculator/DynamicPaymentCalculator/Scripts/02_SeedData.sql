USE DynamicPaymentsDB;
GO

DELETE FROM t_data;
DBCC CHECKIDENT ('t_data', RESEED, 0);
PRINT 'Starting data seeding (1,000,000 rows)...';

DECLARE @Counter INT = 0;
DECLARE @BatchSize INT = 100000; 

WHILE @Counter < 1000000
BEGIN
    INSERT INTO t_data (a, b, c, d)
    SELECT TOP (@BatchSize)
        RAND(CHECKSUM(NEWID())) * 100,
        RAND(CHECKSUM(NEWID())) * 100,
        RAND(CHECKSUM(NEWID())) * 100,
        RAND(CHECKSUM(NEWID())) * 100
    FROM sys.all_columns a 
    CROSS JOIN sys.all_columns b;

    SET @Counter = @Counter + @BatchSize;
    PRINT 'Progress: ' + CAST(@Counter AS VARCHAR) + ' rows inserted.';
END

PRINT 'Data seeding completed successfully.';
GO