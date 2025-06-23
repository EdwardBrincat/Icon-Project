using Icon_Automation_Libs.Http;

// ReSharper disable once CheckNamespace
namespace FluentlyHttpClient;

public static class FluentlyHttpExtensions
{
    /// <summary>
    /// Get data from <see cref="HttpRequestClientContext"/> such as headers.
    /// </summary>
    /// <returns>Returns request builder for chaining.</returns>
    public static FluentHttpRequestBuilder FromClientContext(this FluentHttpRequestBuilder builder, HttpRequestClientContext? context)
    {
        if (context?.Headers != null)
            builder.WithHeaders(context.Headers);

        if (context?.Items != null)
            foreach (var contextItem in context.Items)
                builder.WithItem(contextItem.Key, contextItem.Value);

        return builder;
    }
}
