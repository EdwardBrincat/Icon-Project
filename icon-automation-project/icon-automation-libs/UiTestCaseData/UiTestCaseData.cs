using Icon_Automation_Libs.Config;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.Collections;

namespace Icon_Automation_Libs.UiTestCaseData;

public class UiTestCaseData : IEnumerable<ITestCaseData>
{
	private readonly List<TestCaseData> _data;

	public UiTestCaseData()
	{
		_data = new List<TestCaseData>();

		var config = ConfigFetcher.GetConfiguration();

        if (!config.TestDataUi.Any()) return;

        _data.Add(new TestCaseData("01", config.TestDataUi["scenario_ui_01"]));
	}

	public IEnumerator<ITestCaseData> GetEnumerator() => _data.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
