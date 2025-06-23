namespace Icon_Automation_Libs.Scenario;

public interface IScenarioContext
{
    string ScenarioIdentifier { get; set; }
    string FeatureContext { get; set; }
    IDictionary<string, object> Current { get; }

    /// <summary>
    ///     Use to retrieve values from the scenario
    /// </summary>
    /// <typeparam name="T">expected type</typeparam>
    /// <param name="key">key</param>
    /// <returns>specified value</returns>
    T GetValue<T>(string key);

    /// <summary>
    ///     Use to return the default value of the type if key is not found
    /// </summary>
    /// <typeparam name="T">expected type</typeparam>
    /// <param name="key">key</param>
    /// <returns>specified value</returns>
    T GetOrDefaultValue<T>(string key);

    /// <summary>
    ///     Use to ensure a particular key was only called once
    /// </summary>
    /// <param name="key">key</param>
    /// <param name="value">object</param>
    void AddValue(string key, object value);

    /// <summary>
    ///     Use to update value of the set key. If it does not exist it adds a new value.
    /// </summary>
    /// <param name="key">key</param>
    /// <param name="value">object</param>
    void AddOrUpdateValue(string key, object value);
}
