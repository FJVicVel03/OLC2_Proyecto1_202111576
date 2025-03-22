public abstract record ValueWrapper;

public record IntValue(int Value) : ValueWrapper;

public record FloatValue(float Value) : ValueWrapper;

public record StringValue(string Value) : ValueWrapper;

public record BoolValue(bool Value) : ValueWrapper;

public record FunctionValue(Invocable invocable, string name) : ValueWrapper;

    
public record InstanceValue(Instance Instance) : ValueWrapper;

public record ClassValue(LanguageClass languageClass) : ValueWrapper;


public record StructValue(LanguageStruct Struct, Dictionary<string, ValueWrapper> Values) : ValueWrapper
{
    public StructValue Parent { get; set; }
    public string FieldName { get; set; }

    public void UpdateField(string fieldPath, ValueWrapper value)
    {
        var parts = fieldPath.Split('.');
        if (parts.Length == 1)
        {
            Values[fieldPath] = value;
        }
        else
        {
            var current = Values[parts[0]] as StructValue;
            for (int i = 1; i < parts.Length - 1; i++)
            {
                current = current.Values[parts[i]] as StructValue;
            }
            current.Values[parts[parts.Length - 1]] = value;
        }
    }
}

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

public record MultiSliceValue(string Type, List<List<ValueWrapper>> Elements) : ValueWrapper
{
    public override string ToString()
    {
        return "[" + string.Join(", ", Elements) + "]";
    }
    
    public int GetRowCount()
    {
        return Elements.Count;
    }

    public int GetColumnCount(int rowIndex)
    {
        if (rowIndex < 0 || rowIndex >= GetRowCount())
        {
            throw new ArgumentOutOfRangeException(nameof(rowIndex), "Row index out of bounds");
        }
        return Elements[rowIndex].Count;
    }
}

public record MultiDimSliceValue(string Type, List<List<ValueWrapper>> Elements) : ValueWrapper
{
    public ValueWrapper GetValue(int row, int col)
    {
        if (row < 0 || row >= Elements.Count)
        {
            throw new IndexOutOfRangeException("Row index out of bounds");
        }
        if (col < 0 || col >= Elements[row].Count)
        {
            throw new IndexOutOfRangeException("Column index out of bounds");
        }
        return Elements[row][col];
    }
}

public record VoidValue : ValueWrapper;



