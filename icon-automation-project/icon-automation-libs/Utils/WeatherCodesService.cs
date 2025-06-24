using CsvHelper;
using CsvHelper.Configuration;
using Icon_Automation_Libs.Contracts;
using System.Formats.Asn1;
using System.Globalization;

namespace Icon_Automation_Libs.Utils;

static public class WeatherCodesService
{
    public static List<WeatherCodes> LoadWeatherCodes()
    {
        try
        {
            using var reader = new StreamReader("AssertionData/Weather_Conditions.csv");
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                BadDataFound = null
            });

            return csv.GetRecords<WeatherCodes>().ToList();
        }
        catch(Exception ex) 
        {
            throw new Exception(ex.Message);
        }
    }
}
