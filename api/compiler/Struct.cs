using System;
using System.Collections.Generic;
using analyzer;

public class LanguageStruct : Invocable
{
    public string Name { get; }
    public Dictionary<string, string> Fields { get; }

    public LanguageStruct(string name)
    {
        Name = name;
        Fields = new Dictionary<string, string>();
    }

    public void AddField(string name, string type)
    {
        Fields[name] = type;
    }

    public int Arity()
    {
        // El número de campos es el arity del struct
        return Fields.Count;
    }

    public ValueWrapper Invoke(List<ValueWrapper> args, CompilerVisitor visitor)
    {
        var values = new Dictionary<string, ValueWrapper>();
        int i = 0;

        // Inicializar cada campo con el valor correspondiente
        foreach (var field in Fields)
        {
            if (i < args.Count)
            {
                // Verificar que el tipo coincida
                if (!visitor.IsTypeCompatible(field.Value, args[i]))
                {
                    throw new SemanticError($"Type mismatch in field '{field.Key}': expected {field.Value}", null);
                }
                values[field.Key] = args[i];
            }
            else
            {
                // Si no hay valor, usar el valor por defecto del tipo
                values[field.Key] = visitor.GetDefaultValue(field.Value);
            }
            i++;
        }

        return new StructValue(this, values);
    }

    // Método auxiliar para verificar si un campo existe
    public bool HasField(string fieldName)
    {
        return Fields.ContainsKey(fieldName);
    }

    // Método auxiliar para obtener el tipo de un campo
    public string GetFieldType(string fieldName)
    {
        return Fields.TryGetValue(fieldName, out var type) ? type : throw new SemanticError($"Field '{fieldName}' not found in struct '{Name}'", null);
    }
}