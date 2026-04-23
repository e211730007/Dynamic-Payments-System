CREATE OR ALTER PROCEDURE [dbo].[sp_RunAllCalculations]
AS
BEGIN
    DECLARE @targil_id INT, @targil NVARCHAR(MAX), @tnai NVARCHAR(MAX), @targil_false NVARCHAR(MAX);
    DECLARE @sql NVARCHAR(MAX);

    DECLARE cur CURSOR FOR SELECT targil_id, targil, tnai, targil_false FROM t_targil;
    OPEN cur;
    FETCH NEXT FROM cur INTO @targil_id, @targil, @tnai, @targil_false;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- לוגיקת תנאי: אם עמודת tnai אינה ריקה, בנה שאילתת CASE WHEN
        IF @tnai IS NOT NULL AND @tnai <> ''
        BEGIN
            SET @sql = 'INSERT INTO t_results (data_id, targil_id, method, result) ' +
                       'SELECT data_id, ' + CAST(@targil_id AS NVARCHAR) + ', ''DB'', ' +
                       'CASE WHEN ' + @tnai + ' THEN ' + @targil + ' ELSE ' + @targil_false + ' END ' +
                       'FROM t_data';
        END
        ELSE
        BEGIN
            SET @sql = 'INSERT INTO t_results (data_id, targil_id, method, result) ' +
                       'SELECT data_id, ' + CAST(@targil_id AS NVARCHAR) + ', ''DB'', ' + @targil + 
                       ' FROM t_data';
        END

        EXEC sp_executesql @sql;
        FETCH NEXT FROM cur INTO @targil_id, @targil, @tnai, @targil_false;
    END

    CLOSE cur;
    DEALLOCATE cur;
END