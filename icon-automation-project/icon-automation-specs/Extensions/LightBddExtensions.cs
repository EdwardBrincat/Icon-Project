using Icon_Automation_Libs.DependencyInjection;
using Icon_Automation_Libs.Runner;
using Icon_Automation_Libs.Scenario;
using NUnit.Framework;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using static LightBDD.Framework.Scenarios.LightBddExtensions;

namespace LightBDD.Framework.Scenarios;

public static class LightBddExtensions
{
    public enum FilterContext
    {
        [EnumMember(Value = "Environment")]
        Environment
    }

    private static readonly RunnerContext Context = ServiceResolver.ResolveStatic<RunnerContext>();

    public static IScenarioRunner<TContext> AddIterativeSteps<TContext>(this IScenarioBuilder<TContext> builder,
        int iterations,
        params Expression<Action<TContext>>[] steps
    )
    {
        var integration = builder.Integrate();
        for (var i = 0; i < iterations; i++) integration.AddSteps(steps);

        return integration;
    }

    public static ICompositeStepBuilder<TContext> AddIterativeSteps<TContext>(this ICompositeStepBuilder<TContext> builder,
        int iterations,
        params Expression<Action<TContext>>[] steps
    )
    {
        if (iterations <= 0)
            throw new ArgumentNullException($"{nameof(iterations)} needs to be greater than zero");

        for (var i = 0; i < iterations; i++)
            builder.AddSteps(steps);

        return builder;
    }

    public static ICompositeStepBuilder<TContext> AddIterativeAsyncSteps<TContext>(this ICompositeStepBuilder<TContext> builder,
        uint iterations,
        params Expression<Func<TContext, Task>>[] steps
    )
    {
        if (iterations <= 0)
            throw new ArgumentNullException($"{nameof(iterations)} needs to be greater than zero");

        for (var i = 0; i < iterations; i++)
            builder.AddAsyncSteps(steps);

        return builder;
    }

    public static IScenarioRunner<TContext> AddIterativeAsyncSteps<TContext>(this IScenarioBuilder<TContext> builder,
        uint iterations,
        params Expression<Func<TContext, Task>>[] steps
    )
    {
        var integration = builder.Integrate();
        for (var i = 0; i < iterations; i++) integration.AddAsyncSteps(steps);

        return integration;
    }

    public static ICompositeStepBuilder<TContext> OnConditionSteps<TContext>(this ICompositeStepBuilder<TContext> builder,
        Func<bool> condition,
        params Expression<Action<TContext>>[] steps
    )
    {
        var execute = condition.Invoke();

        if (execute) builder.AddSteps(steps);

        return builder;
    }

    public static ICompositeStepBuilder<TContext> OnConditionAsyncSteps<TContext>(this ICompositeStepBuilder<TContext> builder,
        Func<bool> condition,
        params Expression<Func<TContext, Task>>[] steps
    )
    {
        var execute = condition.Invoke();

        if (execute) builder.AddAsyncSteps(steps);

        return builder;
    }

    public static IScenarioRunner<TContext> OnConditionSteps<TContext>(this IScenarioBuilder<TContext> builder,
        Func<bool> condition,
        params Expression<Action<TContext>>[] steps
    )
    {
        var integration = builder.Integrate();
        var execute = condition.Invoke();

        if (execute) integration.AddSteps(steps);

        return integration;
    }

    public static IScenarioRunner<TContext> OnConditionAsyncSteps<TContext>(this IScenarioBuilder<TContext> builder,
        Func<bool> condition,
        params Expression<Func<TContext, Task>>[] steps
    )
    {
        var integration = builder.Integrate();
        var execute = condition.Invoke();

        if (execute) integration.AddAsyncSteps(steps);

        return integration;
    }

    public static IScenarioRunner<TContext> AddFilteredSteps<TContext>(this IScenarioBuilder<TContext> builder,
        RunOnMode runOnMode,
        FilterContext runOnContext,
        string filter,
        params Expression<Action<TContext>>[] steps
    )
    {
        var filterArray = GetFilters(runOnContext, filter);
        var integration = builder.Integrate();
        var execute = IsExecutable(runOnMode, runOnContext, filterArray);

        if (execute) integration.AddSteps(steps);

        return integration;
    }

    public static ICompositeStepBuilder<TContext> AddFilteredSteps<TContext>(this ICompositeStepBuilder<TContext> builder,
        RunOnMode runOnMode,
        FilterContext runOnContext,
        string filter,
        params Expression<Action<TContext>>[] steps
    )
    {
        var filterArray = GetFilters(runOnContext, filter);
        var execute = IsExecutable(runOnMode, runOnContext, filterArray);

        if (execute) builder.AddSteps(steps);

        return builder;
    }

    public static IScenarioRunner<TContext> AddFilteredAsyncSteps<TContext>(this IScenarioBuilder<TContext> builder,
        Sc_Shared.Enums.RunOnMode runOnMode,
        FilterContext runOnContext,
        string filter,
        params Expression<Func<TContext, Task>>[] steps
    )
    {
        var filterArray = GetFilters(runOnContext, filter);
        var integration = builder.Integrate();
        var execute = IsExecutable(runOnMode, runOnContext, filterArray);

        if (execute) integration.AddAsyncSteps(steps);

        return integration;
    }

    public static ICompositeStepBuilder<TContext> AddFilteredAsyncSteps<TContext>(this ICompositeStepBuilder<TContext> builder,
        RunOnMode runOnMode,
        FilterContext runOnContext,
        string filter,
        params Expression<Func<TContext, Task>>[] steps
    )
    {
        var filterArray = GetFilters(runOnContext, filter);
        var execute = IsExecutable(runOnMode, runOnContext, filterArray);

        if (execute) builder.AddAsyncSteps(steps);

        return builder;
    }

    public static IScenarioRunner<TContext> AddFilteredSteps<TContext>(this IScenarioBuilder<TContext> builder,
        RunOnMode runOnMode,
        FilterContext runOnContext,
        string[] filters,
        params Expression<Action<TContext>>[] steps
    )
    {
        var integration = builder.Integrate();
        var filterArray = GetFilters(runOnContext, filters);
        var execute = IsExecutable(runOnMode, runOnContext, filterArray);


        if (execute) integration.AddSteps(steps);

        return integration;
    }

    public static ICompositeStepBuilder<TContext> AddFilteredSteps<TContext>(this ICompositeStepBuilder<TContext> builder,
        RunOnMode runOnMode,
        FilterContext runOnContext,
        string[] filters,
        params Expression<Action<TContext>>[] steps
    )
    {
        var filterArray = GetFilters(runOnContext, filters);
        var execute = IsExecutable(runOnMode, runOnContext, filterArray);

        if (execute) builder.AddSteps(steps);

        return builder;
    }

    public static IScenarioRunner<TContext> AddFilteredAsyncSteps<TContext>(this IScenarioBuilder<TContext> builder,
        RunOnMode runOnMode,
        FilterContext runOnContext,
        string[] filters,
        params Expression<Func<TContext, Task>>[] steps
    )
    {
        var integration = builder.Integrate();
        var filterArray = GetFilters(runOnContext, filters);
        var execute = IsExecutable(runOnMode, runOnContext, filterArray);

        if (execute) integration.AddAsyncSteps(steps);

        return integration;
    }

    public static ICompositeStepBuilder<TContext> AddFilteredAsyncSteps<TContext>(this ICompositeStepBuilder<TContext> builder,
        RunOnMode runOnMode,
        FilterContext runOnContext,
        string[] filters,
        params Expression<Func<TContext, Task>>[] steps
    )
    {
        var filterArray = GetFilters(runOnContext, filters);
        var execute = IsExecutable(runOnMode, runOnContext, filterArray);

        if (execute) builder.AddAsyncSteps(steps);

        return builder;
    }

    public static IScenarioRunner<TContext> AddFilteredSteps<TContext>(this IScenarioBuilder<TContext> builder,
        RunOnMode runOnMode,
        FilterContext[] runOnContext,
        string[] filters,
        params Expression<Action<TContext>>[] steps
    )
    {
        var integration = builder.Integrate();
        var contextMatrix = new Dictionary<FilterContext, bool>();

        for (var i = 0; i < filters.Length; i++)
            try
            {
                if (contextMatrix[runOnContext[i]]) continue;

                var execute = DeduceExecution(
                    runOnMode,
                    runOnContext[i],
                    filters[i]);
                contextMatrix[runOnContext[i]] = execute;
            }
            catch (KeyNotFoundException)
            {
                var execute = DeduceExecution(
                    runOnMode,
                    runOnContext[i],
                    filters[i]);
                contextMatrix.Add(runOnContext[i], execute);
            }

        if (contextMatrix.IsExecutable()) integration.AddSteps(steps);

        return integration;
    }

    public static ICompositeStepBuilder<TContext> AddFilteredSteps<TContext>(this ICompositeStepBuilder<TContext> builder,
        RunOnMode runOnMode,
        FilterContext[] runOnContext,
        string[] filters,
        params Expression<Action<TContext>>[] steps
    )
    {
        var contextMatrix = new Dictionary<FilterContext, bool>();

        for (var i = 0; i < filters.Length; i++)
            try
            {
                if (contextMatrix[runOnContext[i]]) continue;

                var execute = DeduceExecution(
                    runOnMode,
                    runOnContext[i],
                    filters[i]);
                contextMatrix[runOnContext[i]] = execute;
            }
            catch (KeyNotFoundException)
            {
                var execute = DeduceExecution(
                    runOnMode,
                    runOnContext[i],
                    filters[i]);
                contextMatrix.Add(runOnContext[i], execute);
            }

        if (contextMatrix.IsExecutable()) builder.AddSteps(steps);

        return builder;
    }

    public static IScenarioRunner<TContext> AddFilteredAsyncSteps<TContext>(this IScenarioBuilder<TContext> builder,
        RunOnMode runOnMode,
        FilterContext[] runOnContext,
        string[] filters,
        params Expression<Func<TContext, Task>>[] steps
    )
    {
        var integration = builder.Integrate();
        var contextMatrix = new Dictionary<FilterContext, bool>();

        for (var i = 0; i < filters.Length; i++)
            try
            {
                if (contextMatrix[runOnContext[i]]) continue;

                var execute = DeduceExecution(
                    runOnMode,
                    runOnContext[i],
                    filters[i]);
                contextMatrix[runOnContext[i]] = execute;
            }
            catch (KeyNotFoundException)
            {
                var execute = DeduceExecution(
                    runOnMode,
                    runOnContext[i],
                    filters[i]);
                contextMatrix.Add(runOnContext[i], execute);
            }

        if (contextMatrix.IsExecutable()) integration.AddAsyncSteps(steps);

        return integration;
    }

    public static ICompositeStepBuilder<TContext> AddFilteredAsyncSteps<TContext>(this ICompositeStepBuilder<TContext> builder,
        RunOnMode runOnMode,
        FilterContext[] runOnContext,
        string[] filters,
        params Expression<Func<TContext, Task>>[] steps
    )
    {
        var contextMatrix = new Dictionary<FilterContext, bool>();

        for (var i = 0; i < filters.Length; i++)
            try
            {
                if (contextMatrix[runOnContext[i]]) continue;

                var execute = DeduceExecution(
                    runOnMode,
                    runOnContext[i],
                    filters[i]);
                contextMatrix[runOnContext[i]] = execute;
            }
            catch (KeyNotFoundException)
            {
                var execute = DeduceExecution(
                    runOnMode,
                    runOnContext[i],
                    filters[i]);
                contextMatrix.Add(runOnContext[i], execute);
            }

        if (contextMatrix.IsExecutable()) builder.AddAsyncSteps(steps);

        return builder;
    }

    private static string[] GetFilters(FilterContext runOnContext, string filter)
        => GetFilters(runOnContext, new[] { filter });

    private static string[] GetFilters(FilterContext runOnContext, string[] filters)
        => filters;

    public static bool IsExecutable(RunOnMode runOnMode, FilterContext runOnContext, string filter)
        => IsExecutable(runOnMode, runOnContext, new[] { filter });

    public static bool IsExecutable(RunOnMode runOnMode, FilterContext runOnContext, string[] filter)
        => DeduceExecution(runOnMode, runOnContext, filter);

    private static bool DeduceExecution(RunOnMode runOnMode, FilterContext runOnContext, string filter)
        => DeduceExecution(runOnMode, runOnContext, new[] { filter });

    public static bool IsExecutable(RunOnMode runOnMode, FilterContext[] runOnContext, string[] filters)
    {
        var contextMatrix = new Dictionary<FilterContext, bool>();

        for (var i = 0; i < filters.Length; i++)
        {
            try
            {
                if (contextMatrix[runOnContext[i]]) continue;

                var execute = DeduceExecution(
                    runOnMode,
                    runOnContext[i],
                    filters[i]);
                contextMatrix[runOnContext[i]] = execute;
            }
            catch (KeyNotFoundException)
            {
                var execute = DeduceExecution(
                    runOnMode,
                    runOnContext[i],
                    filters[i]);
                contextMatrix.Add(runOnContext[i], execute);
            }
        }

        return runOnMode == RunOnMode.Include ? contextMatrix.IsExecutable() : !contextMatrix.IsExecutable();
    }

    private static bool DeduceExecution(RunOnMode runOnMode, FilterContext runOnContext, string[] filter)
    {
        switch (runOnContext)
        {
            case FilterContext.Environment:
                if ((runOnMode == RunOnMode.Include && filter.ToList().Contains(Context.Env)) ||
                    (runOnMode == RunOnMode.Exclude && !filter.ToList().Contains(Context.Env)))
                    return true;
                break;
            case FilterContext.Brand:
                if ((runOnMode == RunOnMode.Include && filter.ToList().Contains(Context.Brand)) ||
                    (runOnMode == RunOnMode.Exclude && !filter.ToList().Contains(Context.Brand)))
                    return true;
                break;
            case FilterContext.Orientation:
                if ((runOnMode == RunOnMode.Include && filter.ToList().Contains(Context.Orientation)) ||
                    (runOnMode == RunOnMode.Exclude && !filter.ToList().Contains(Context.Orientation)))
                    return true;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(runOnContext), runOnContext, null);
        }

        return false;
    }

    public static void IgnoreScenario(this StepExecution execution, string reason) => throw new IgnoreException(reason);

    public static IBddRunner<TContext> WithContext<TContext>(this IBddRunner runner, Func<ScenarioContextConfigure> func)
        where TContext : IHasScenarioContext
    {
        var context = ServiceResolver.ResolveStatic<TContext>();
        var contextConfigure = func.Invoke();
        contextConfigure.Set(context.ScenarioContext);
        var bddContext = runner.WithContext(context);

        return bddContext;
    }

    public static ICompositeStepBuilder<TContext> IgnoreSteps<TContext>(this ICompositeStepBuilder<TContext> builder,
        string reason,
        params Expression<Func<TContext, Task>>[] steps
    )
    {
        builder.AddSteps(bypass => Some_steps_within_this_scenario_be_ignored(reason, steps));
        return builder;
    }

    public static ICompositeStepBuilder<TContext> IgnoreAsyncSteps<TContext>(this ICompositeStepBuilder<TContext> builder,
        string reason,
        params Expression<Func<TContext, Task>>[] steps
    )
    {
        builder.AddSteps(bypass => Some_steps_within_this_scenario_be_ignored(reason, steps));
        return builder;
    }

    public static IScenarioRunner<TContext> IgnoreSteps<TContext>(this IScenarioBuilder<TContext> builder,
        string reason,
        params Expression<Action<TContext>>[] steps
    )
    {
        var integration = builder.Integrate();
        integration.AddSteps(ignore => Some_steps_within_this_scenario_be_ignored(reason, steps));

        return integration;
    }

    public static IScenarioRunner<TContext> IgnoreAsyncSteps<TContext>(this IScenarioBuilder<TContext> builder,
        string reason,
        params Expression<Func<TContext, Task>>[] steps
    )
    {
        var integration = builder.Integrate();
        integration.AddSteps(ignore => Some_steps_within_this_scenario_be_ignored(reason, steps));

        return integration;
    }

    private static void Some_steps_within_this_scenario_be_ignored<TContext>(
        string reason,
        params Expression<Action<TContext>>[] ignoreSteps
    )
    {
        var totalIgnoredSteps = ignoreSteps.GetLength(0);
        var stepNames = new StringBuilder();

        for (var i = 0; i < totalIgnoredSteps; i++) stepNames.Append($"{ignoreSteps.GetValue(i)} \n");
        StepExecution.Current.Comment($"Some steps within this scenario have been ignored due to: [{reason}] \n Ignored Steps: \n{stepNames}");
    }

    private static void Some_steps_within_this_scenario_be_ignored<TContext>(
        string reason,
        params Expression<Func<TContext, Task>>[] ignoreSteps
    )
    {
        var totalIgnoredSteps = ignoreSteps.GetLength(0);
        var stepNames = new StringBuilder();

        for (var i = 0; i < totalIgnoredSteps; i++) stepNames.Append($"{ignoreSteps.GetValue(i)} \n");
        StepExecution.Current.Comment($"Some steps within this scenario have been ignored due to: [{reason}]: \n Ignored Steps: \n{stepNames}");
    }
}

public static class DictionaryExtensions
{
    public static bool IsExecutable(this Dictionary<FilterContext, bool> dictionary)
    {
        var valuesAreTrue = false;

        foreach (var context in dictionary)
        {
            if (context.Value == false)
            {
                valuesAreTrue = false;
                break;
            }

            valuesAreTrue = true;
        }

        return valuesAreTrue;
    }
}