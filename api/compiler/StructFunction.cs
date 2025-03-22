using System;
using System.Collections.Generic;
using analyzer;

public class StructFunction : Invocable
{
    private readonly LanguageStruct targetStruct;
    private readonly string parameterName;
    private readonly List<(string name, string type)> parameters;
    private readonly string returnType;
    private readonly LanguageParser.StmtContext body;
    private readonly Environment declarationEnv;

    public StructFunction(
        LanguageStruct targetStruct,
        string parameterName,
        List<(string name, string type)> parameters,
        string returnType,
        LanguageParser.StmtContext body,
        Environment declarationEnv)
    {
        this.targetStruct = targetStruct;
        this.parameterName = parameterName;
        this.parameters = parameters;
        this.returnType = returnType;
        this.body = body;
        this.declarationEnv = declarationEnv;
    }

    public int Arity()
    {
        return parameters.Count + 1;
    }

    public ValueWrapper Invoke(List<ValueWrapper> arguments, CompilerVisitor visitor)
    {
        if (arguments.Count != parameters.Count + 1)
        {
            throw new SemanticError($"Expected {parameters.Count + 1} arguments, got {arguments.Count}", null);
        }

        var functionEnv = new Environment(declarationEnv);
        
        // Bind struct instance
        var structInstance = arguments[0] as StructValue;
        if (structInstance == null || structInstance.Struct != targetStruct)
        {
            throw new SemanticError("Invalid struct instance", null);
        }
        functionEnv.Declare(parameterName, structInstance, null);

        // Bind other parameters
        for (int i = 0; i < parameters.Count; i++)
        {
            var (name, type) = parameters[i];
            var value = arguments[i + 1];
            if (!visitor.IsTypeCompatible(type, value))
            {
                throw new SemanticError($"Type mismatch for parameter {name}", null);
            }
            functionEnv.Declare(name, value, null);
        }

        var previousEnv = visitor.currentEnvironment;
        visitor.currentEnvironment = functionEnv;

        try
        {
            visitor.Visit(body);
            return visitor.defaultValue;
        }
        finally
        {
            visitor.currentEnvironment = previousEnv;
        }
    }
}