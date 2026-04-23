USE DynamicPaymentsDB;
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 't_data')
BEGIN
    CREATE TABLE t_data (
        data_id INT IDENTITY(1,1) PRIMARY KEY,
        a FLOAT NOT NULL,
        b FLOAT NOT NULL,
        c FLOAT NOT NULL,
        d FLOAT NOT NULL
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 't_results')
BEGIN
    CREATE TABLE t_results (
        result_id INT IDENTITY(1,1) PRIMARY KEY,
        data_id INT NOT NULL,
        calculation_result FLOAT NOT NULL,
        engine_name NVARCHAR(50) NOT NULL, -- SQL, CSharp, Python
        calculation_time_ms FLOAT NOT NULL,
        created_at DATETIME DEFAULT GETDATE(),
        CONSTRAINT FK_Results_Data FOREIGN KEY (data_id) REFERENCES t_data(data_id)
    );
END
GO