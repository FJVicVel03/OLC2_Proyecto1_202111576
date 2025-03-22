public class Environment
{
    private Dictionary<string, (ValueWrapper value, Type type)> variables = new Dictionary<string, (ValueWrapper, Type)>();
    public Environment? parent;

    public Environment(Environment? parent)
    {
        this.parent = parent;
    }

    public void Declare(string id, ValueWrapper value, Antlr4.Runtime.IToken? token)
    {
        if (variables.ContainsKey(id))
        {
            throw new SemanticError($"Variable '{id}' already declared", token);
        }
        variables[id] = (value, value.GetType());
    }

    public ValueWrapper Get(string id, Antlr4.Runtime.IToken token)
    {
        if (variables.ContainsKey(id))
        {
            return variables[id].value;
        }
        if (parent != null)
        {
            return parent.Get(id, token);
        }
        throw new SemanticError($"Variable '{id}' not found", token);
    }

    public void Assign(string id, ValueWrapper value, Antlr4.Runtime.IToken token)
    {
        if (variables.ContainsKey(id))
        {
            var (currentValue, type) = variables[id];
            if (value.GetType() != type)
            {
                throw new SemanticError($"Type mismatch: Cannot assign value of type '{value.GetType().Name}' to variable '{id}' of type '{type.Name}'", token);
            }
            variables[id] = (value, type);
            return;
        }
        if (parent != null)
        {
            parent.Assign(id, value, token);
            return;
        }
        throw new SemanticError($"Variable '{id}' not found", token);
    }

    public void Update(string id, ValueWrapper value, Antlr4.Runtime.IToken token)
    {
        if (variables.ContainsKey(id))
        {
            var (_, type) = variables[id];
            if (value.GetType() != type && !(type == typeof(FloatValue) && value is IntValue))
            {
                throw new SemanticError($"Type mismatch: Cannot assign value of type '{value.GetType().Name}' to variable '{id}' of type '{type.Name}'", token);
            }
            variables[id] = (value, type);
            return;
        }
        if (parent != null)
        {
            parent.Update(id, value, token);
            return;
        }
        throw new SemanticError($"Variable '{id}' not found", token);
    }
}