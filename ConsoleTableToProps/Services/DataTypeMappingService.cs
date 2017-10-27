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
                    System.Console.WriteLine("Parameters are empty !!!");
                }

                DataRepository dataRepository = new DataRepository(settings.ConnectionString);
                var columns = dataRepository.DbCallForSchema(settings.DatabaseName, settings.TableName).ToList();
                StringBuilder resultString = new StringBuilder();
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
                Console.WriteLine("Error: {0}", ex);
                throw;
            }
        }

        private string DBDataTypeMapping(string dBDataType)
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
                default:
                    return "Check Data Type Mapping";
            }
        }

        private string POCOLine(string dBDataType, string dBColumnType)
        {
            var relaventDataType = DBDataTypeMapping(dBDataType);
            return string.Format("public {0} {1} {{ get; set; }}", relaventDataType, dBColumnType);
        }
    }
}
