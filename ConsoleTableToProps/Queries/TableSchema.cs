namespace ConsoleTableToProps.Queries
{
    public class TableSchema
    {
        public const string columnNames = @"USE [{0}]
                                                SELECT COLUMN_NAME,DATA_TYPE
                                                FROM INFORMATION_SCHEMA.COLUMNS
                                                WHERE TABLE_NAME = @TableName AND TABLE_SCHEMA='dbo'";
    }
}
