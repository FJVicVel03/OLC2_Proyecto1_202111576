public abstract record ValueWrapper;

public record IntValue(int Value) : ValueWrapper;

public record FloatValue(float Value) : ValueWrapper;

public record StringValue(string Value) : ValueWrapper;

public record BoolValue(bool Value) : ValueWrapper;

public record FunctionValue(Invocable invocable, string name) : ValueWrapper;

    
public record InstanceValue(Instance Instance) : ValueWrapper;

public record ClassValue(LanguageClass languageClass) : ValueWrapper;

public record RuneValue(byte Value) : ValueWrapper
{
    public override global::System.String ToString()
    {
        return ((char)Value).ToString();
    }
}

public record SliceValue(string Type, List<ValueWrapper> Elements) : ValueWrapper
{
    public override string ToString()
    {
        return "[" + string.Join(", ", Elements) + "]";
    }
}

public record VoidValue : ValueWrapper;

