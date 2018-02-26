using ConsoleTableToProps.Models.ConfigModels;
using ConsoleTableToProps.Repositories;
using System;
using System.Linq;
using System.Text;

namespace ConsoleTableToProps.Services
{
    public class DataTypeMappingService
    {
        public void GenerateTxtPocoOutput(AppSettings settings)
        {
            try
            {
                if (string.IsNullOrEmpty(settings.ConnectionString) || string.IsNullOrEmpty(settings.DatabaseName) || string.IsNullOrEmpty(settings.TableName))
                {
                    Console.WriteLine("Parameters are empty !!!");
                }

                var dataRepository = new DataRepository(settings.ConnectionString);
                var columns = dataRepository.DbCallForSchema(settings.DatabaseName, settings.TableName).ToList();
                var resultString = new StringBuilder();
                foreach (var column in columns)
                {
                    resultString.Append(POCOLine(column.DATA_TYPE, column.COLUMN_NAME));
                    resultString.AppendLine();
                }

                dataRepository.OutputToTxtFile(resultString.ToString());
                Console.WriteLine("Txt File Created !!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                throw;
            }
        }

        private static string DBDataTypeMapping(string dBDataType)
        {
            switch (dBDataType)
            {
                case "varchar":
                    return "string";

                case "nvarchar":
                    return "string";

                case "uniqueidentifier":
                    return "Guid";

                case "real":
                    return "double";

                case "float":
                    return "double";

                case "int":
                    return "int";

                case "bit":
                    return "bool";

                case "date":
                    return "DateTime";

                case "time":
                    return "DateTime";

                case "datetime":
                    return "DateTime";

                case "money":
                    return "decimal";

                case "decimal":
                    return "decimal";

                default:
                    return "Check Data Type Mapping";
            }
        }

        private static string POCOLine(string dBDataType, string dBColumnType)
        {
            var relaventDataType = DBDataTypeMapping(dBDataType);
            return $"public {relaventDataType} {dBColumnType} {{ get; set; }}";
        }
    }
}