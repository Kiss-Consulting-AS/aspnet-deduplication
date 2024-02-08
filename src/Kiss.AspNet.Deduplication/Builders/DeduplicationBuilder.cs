using System.Text.Json;

namespace Kiss.AspNet.Deduplication;

public interface IDeduplicationBuilder
{
    /// <summary>
    /// Gets a value indicating whether the SupportsAot feature is supported.
    /// </summary>
    /// <remarks>
    /// The SupportsAot property determines whether the deduplication setup supports Ahead-of-Time (AOT) compilation.
    /// AOT compilation is a technique where an application is compiled into executable code before it is run, rather than compiling it during runtime like Just-in-Time (JIT) compilation
    /// .
    /// If the SupportsAot property returns true, it means that the system supports AOT compilation. Otherwise, if it returns false, it means that the system does not support AOT compilation
    /// .
    /// The builder will use the SupportsAot property to determine whether to use AOT compilation or not
    /// </remarks>
    bool SupportsAot { get; }

    void Build();
}

/// <inheritdoc cref="IDeduplicationBuilder"/>
public class DeduplicationBuilder
    : IDeduplicationBuilder
{
    public bool SupportsAot => false;
    
    public void Build()
    {
        throwIfReflectionBasedSerializationDisabled();
    }

    private static void throwIfReflectionBasedSerializationDisabled()
    {
        if (!JsonSerializer.IsReflectionEnabledByDefault)
            throw new NotSupportedException("""
                                            The deduplication feature requires reflection-based serialization 
                                            to be enabled. Please enable reflection-based serialization by 
                                            setting the JsonSerializerIsReflectionEnabledByDefault setting in 
                                            your project file.
                                            https://devblogs.microsoft.com/dotnet/system-text-json-in-dotnet-8/
                                            """);
    }
}