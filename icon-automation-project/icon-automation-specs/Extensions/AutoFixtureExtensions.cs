using AutoFixture.Dsl;

namespace AutoFixture;

public static class AutoFixtureExtensions
{
    public static IPostprocessComposer<T> BuildData<T>(this Fixture fixture) => fixture.Build<T>().OmitAutoProperties();
}
