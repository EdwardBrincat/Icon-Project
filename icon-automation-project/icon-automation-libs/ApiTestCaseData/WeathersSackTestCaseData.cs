using Icon_Automation_Libs.Config;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.Collections;

namespace Icon_Automation_Libs.ApiTestCaseData;

public class WeathersSackTestCaseData : IEnumerable<ITestCaseData>
{
	private readonly List<TestCaseData> _data;

	public WeathersSackTestCaseData()
	{
		_data = new List<TestCaseData>();

		var config = ConfigFetcher.GetConfiguration();

        if (!config.Cities.Any()) return;        

        var testId = string.Empty;

        foreach (var city in config.Cities)
        {           

            testId = _data.Count + 1 <= 9 ? $"0{_data.Count + 1}" : $"{_data.Count + 1}";

            _data.Add(new TestCaseData(testId, city));
        }
    }

	public IEnumerator<ITestCaseData> GetEnumerator() => _data.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
