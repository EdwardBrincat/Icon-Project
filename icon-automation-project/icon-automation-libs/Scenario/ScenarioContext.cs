using Icon_Automation_Libs.Exceptions;
using System.Collections.Concurrent;

namespace Icon_Automation_Libs.Scenario;

public class ScenarioContext : IScenarioContext
{
    public ScenarioContext()
    {
        Current = new ConcurrentDictionary<string, object>();
    }

    public string ScenarioIdentifier { get; set; }
    public string FeatureContext { get; set; }
    public IDictionary<string, object> Current { get; }

    public T GetValue<T>(string key)
    {
        if (!Current.TryGetValue(key, out var value))
            throw new KeyNotFoundException($"Unable to find key: [{key}] within the current context. Make sure that the step required to add the key was invoked");

        return (T)value;
    }

    public T GetOrDefaultValue<T>(string key)
    {
        if (!Current.TryGetValue(key, out var value))
            return default;

        return (T)value;
    }

    public void AddValue(string key, object value)
    {
        if (Current.ContainsKey(key))
        {
            Current.TryGetValue(key, out var currentValue);
            throw new TestStepFailureException($"[TEST ERROR] Test attempted to add an existing key in the scenario context key: {key} | input type: {value.GetType().Name} | current type: {currentValue.GetType().Name}");
        }

        Current.Add(key, value);
    }

    public void AddOrUpdateValue(string key, object value)
    {
        if (Current.ContainsKey(key))
            Current.Remove(key);

        AddValue(key, value);
    }

    public void AddOrUpdateValue(string key, object value, bool isOverride)
    {
        if (isOverride)
        {
            AddOrUpdateValue(key, value);
            return;
        }

        AddValue(key, value);
    }
}
