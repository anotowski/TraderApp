using AutoFixture;
using NSubstitute;

namespace TraderApp.UnitTests.Helpers;

public static class AutoFixtureHelpers
{
    public static T FreezeSubstitute<T>(this IFixture fixture) where T : class
    {
        var substitute = Substitute.For<T>();
        fixture.Inject(substitute);
        return substitute;
    }
}
