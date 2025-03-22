using analyzer;
using System.Globalization;

public class CompilerVisitor : LanguageBaseVisitor<ValueWrapper>
{
    public ValueWrapper result { get; private set; } = new VoidValue();
    public ValueWrapper defaultValue = new VoidValue();
    public string output = "";
    public Environment currentEnvironment;

    public CompilerVisitor(){
        currentEnvironment = new Environment(null);
        Embeded.Generate(currentEnvironment);
    }

    //VisitValueWrapper
    public override ValueWrapper VisitInt(LanguageParser.IntContext context)
    {
        return new IntValue(int.Parse(context.INT().GetText()));
    }


    //Negate    
    public override ValueWrapper VisitNegate(LanguageParser.NegateContext context)
    {
        ValueWrapper value = Visit(context.expr());
        return value switch
        {
            IntValue i => new IntValue(-i.Value),
            FloatValue f => new FloatValue(-f.Value),
            _ => throw new SemanticError("Invalid operation", context.Start)
        };
    }

    //VisitMulDiv
public override ValueWrapper VisitMulDiv(LanguageParser.MulDivContext context)
{
    var left = Visit(context.expr(0)); // Evalúa el lado izquierdo
    var right = Visit(context.expr(1)); // Evalúa el lado derecho
    var op = context.op.Text;

    return (left, right, op) switch
    {
        (IntValue l, IntValue r, "*") => new IntValue(l.Value * r.Value),
        (IntValue l, IntValue r, "/") => r.Value != 0 
            ? new IntValue(l.Value / r.Value)
            : throw new SemanticError("Division by zero", context.Start),
        (IntValue l, IntValue r, "%") => r.Value != 0 
            ? new IntValue(l.Value % r.Value) // Módulo
            : throw new SemanticError("Division by zero", context.Start),
        (FloatValue l, FloatValue r, "*") => new FloatValue(l.Value * r.Value),
        (FloatValue l, FloatValue r, "/") => r.Value != 0 
            ? new FloatValue(l.Value / r.Value)
            : throw new SemanticError("Division by zero", context.Start),
        (IntValue l, FloatValue r, "*") => new FloatValue(l.Value * r.Value),
        (IntValue l, FloatValue r, "/") => r.Value != 0 
            ? new FloatValue(l.Value / r.Value)
            : throw new SemanticError("Division by zero", context.Start),
        (FloatValue l, IntValue r, "*") => new FloatValue(l.Value * r.Value),
        (FloatValue l, IntValue r, "/") => r.Value != 0 
            ? new FloatValue(l.Value / r.Value)
            : throw new SemanticError("Division by zero", context.Start),
        _ => throw new SemanticError($"Invalid operation '{op}' between {left.GetType().Name} and {right.GetType().Name}", context.Start)
    };
}

    //VisitAddSub
    public override ValueWrapper VisitAddSub(LanguageParser.AddSubContext context)
{
    var left = Visit(context.expr(0)); // Evalúa el lado izquierdo
    var right = Visit(context.expr(1)); // Evalúa el lado derecho
    var op = context.op.Text;

    return (left, right, op) switch
    {
        (IntValue l, IntValue r, "+") => new IntValue(l.Value + r.Value),
        (IntValue l, IntValue r, "-") => new IntValue(l.Value - r.Value),
        (FloatValue l, FloatValue r, "+") => new FloatValue(l.Value + r.Value),
        (FloatValue l, FloatValue r, "-") => new FloatValue(l.Value - r.Value),
        (IntValue l, FloatValue r, "+") => new FloatValue(l.Value + r.Value), // Promoción a float
        (IntValue l, FloatValue r, "-") => new FloatValue(l.Value - r.Value), // Promoción a float
        (FloatValue l, IntValue r, "+") => new FloatValue(l.Value + r.Value), // Promoción a float
        (FloatValue l, IntValue r, "-") => new FloatValue(l.Value - r.Value), // Promoción a float
        (StringValue l, StringValue r, "+") => new StringValue(l.Value + r.Value), // Concatenación de cadenas
        _ => throw new SemanticError($"Invalid operation '{op}' between {left.GetType().Name} and {right.GetType().Name}", context.Start)
    };
}

    //VisitProgram
    public override ValueWrapper VisitProgram(LanguageParser.ProgramContext context)
    {
        foreach (var dcl in context.dcl())
        {
            Visit(dcl);
        }

        if(currentEnvironment.Get("main", null) is FunctionValue main)
        {
            try
            {
                main.invocable.Invoke(new List<ValueWrapper>(), this);
            }
            catch (ReturnException e)
            {
                result = e.Value;
            }
        }
        else
        {
            throw new SemanticError("Main function not found", context.Start);
        }
        return defaultValue;
    }

    //VisitVarDcl
public override ValueWrapper VisitVarDcl(LanguageParser.VarDclContext context)
{
    string id = context.ID().GetText();
    ValueWrapper value;

    // Declaración implícita
    if (context.expr() != null && context.type() == null)
    {
        value = Visit(context.expr());

        // Verificar si es un SliceInit
        if (value is SliceValue sliceValue)
        {
            currentEnvironment.Declare(id, sliceValue, context.Start);
        }
        else
        {
            currentEnvironment.Declare(id, value, context.Start);
        }
    }
    // Declaración explícita con tipo
    else if (context.type() != null)
    {
        string type = context.type().GetText();

        // Declaración explícita con tipo y valor
        if (context.expr() != null)
        {
            value = Visit(context.expr());

            // Verificar que el tipo declarado coincida con el tipo del valor asignado
            if (!IsTypeCompatible(type, value))
            {
                throw new SemanticError($"Type mismatch: Cannot assign value of type '{value.GetType().Name}' to variable '{id}' of type '{type}'", context.Start);
            }
        }
        // Declaración explícita con tipo pero sin valor
        else
        {
            value = GetDefaultValue(type);
        }

        currentEnvironment.Declare(id, value, context.Start);
    }
    else
    {
        throw new SemanticError($"Invalid variable declaration for '{id}'", context.Start);
    }

    return defaultValue;
}

    public bool IsTypeCompatible(string type, ValueWrapper value)
{
    return type switch
    {
        "int" => value is IntValue,
        "float" => value is FloatValue || value is IntValue, // Permitir conversión implícita de int a float
        "string" => value is StringValue,
        "bool" => value is BoolValue,
        "rune" => value is RuneValue,
        _ => value is StructValue structValue && structValue.Struct.Name == type // Para structs anidados
    };
}

    public ValueWrapper GetDefaultValue(string type)
{
    return type switch
    {
        "int" => new IntValue(0),
        "float" => new FloatValue(0.0f),
        "string" => new StringValue(""),
        "bool" => new BoolValue(false),
        "rune" => new RuneValue(0),
        _ => currentEnvironment.Get(type, null) is StructValue structValue ? 
            new StructValue(structValue.Struct, 
                structValue.Struct.Fields.ToDictionary(
                    f => f.Key, 
                    f => GetDefaultValue(f.Value))) 
            : throw new SemanticError($"Unknown type '{type}'", null)
    };
}

    //VisitExprStmt
    public override ValueWrapper VisitExprStmt(LanguageParser.ExprStmtContext context)
    {
        return Visit(context.expr());
    }

    //VisitIdentifier
    public override ValueWrapper VisitIdentifier(LanguageParser.IdentifierContext context)
    {
        string id = context.ID().GetText();
        return currentEnvironment.Get(id, context.Start);
    }

    //VisitFloat
    public override ValueWrapper VisitFloat(LanguageParser.FloatContext context)
    {
        return new FloatValue(float.Parse(context.FLOAT().GetText(), CultureInfo.InvariantCulture));
    }

    //VisitBoolean
    public override ValueWrapper VisitBoolean(LanguageParser.BooleanContext context)
    {
        return new BoolValue(bool.Parse(context.BOOL().GetText()));
    }

    //VisitString
    public override ValueWrapper VisitString(LanguageParser.StringContext context)
    {
        string text = context.STRING().GetText();

        // Eliminar las comillas dobles
        if (text.StartsWith("\"") && text.EndsWith("\""))
        {
            text = text.Substring(1, text.Length - 2);
        }

        // Manejar caracteres escapados
        text = text.Replace("\\n", "\n").Replace("\\t", "\t").Replace("\\\"", "\"").Replace("\\\\", "\\");

        return new StringValue(text);
    }

    //VisitRune
public override ValueWrapper VisitRune(LanguageParser.RuneContext context)
{
    string runeText = context.RUNE().GetText();
    char runeChar;

    // Eliminar las comillas simples
    if (runeText.StartsWith("'") && runeText.EndsWith("'"))
    {
        runeText = runeText.Substring(1, runeText.Length - 2);
    }

    // Manejar caracteres escapados
    if (runeText.StartsWith("\\"))
    {
        runeChar = runeText switch
        {
            "\\n" => '\n',
            "\\t" => '\t',
            "\\'" => '\'',
            "\\\\" => '\\',
            _ => throw new SemanticError($"Invalid escape sequence: {runeText}", context.Start)
        };
    }
    else
    {
        // Caracter normal
        runeChar = runeText[0];
    }

    // Validar rango (0-255)
    if (runeChar > 255)
    {
        throw new SemanticError($"Rune value '{runeChar}' is out of range (0-255)", context.Start);
    }

    return new RuneValue((byte)runeChar);
}

    //VisitParens

    public override ValueWrapper VisitParens(LanguageParser.ParensContext context)
{
    // Evalúa la expresión dentro de los paréntesis
    return Visit(context.expr());
}

    //VisitRelational
    public override ValueWrapper VisitRelational(LanguageParser.RelationalContext context)
    {
        var left = Visit(context.expr(0));
        var right = Visit(context.expr(1));
        var op = context.op.Text;
        return (left, right, op) switch
        {
            (IntValue l, IntValue r, "<") => new BoolValue(l.Value < r.Value),
            (IntValue l, IntValue r, ">") => new BoolValue(l.Value > r.Value),
            (IntValue l, IntValue r, "<=") => new BoolValue(l.Value <= r.Value),
            (IntValue l, IntValue r, ">=") => new BoolValue(l.Value >= r.Value),
            (FloatValue l, FloatValue r, "<") => new BoolValue(l.Value < r.Value),
            (FloatValue l, FloatValue r, ">") => new BoolValue(l.Value > r.Value),
            (FloatValue l, FloatValue r, "<=") => new BoolValue(l.Value <= r.Value),
            (FloatValue l, FloatValue r, ">=") => new BoolValue(l.Value >= r.Value),
            (IntValue l, FloatValue r, "<") => new BoolValue(l.Value < r.Value),
            (IntValue l, FloatValue r, ">") => new BoolValue(l.Value > r.Value),
            (IntValue l, FloatValue r, "<=") => new BoolValue(l.Value <= r.Value),
            (IntValue l, FloatValue r, ">=") => new BoolValue(l.Value >= r.Value),
            (FloatValue l, IntValue r, "<") => new BoolValue(l.Value < r.Value),
            (FloatValue l, IntValue r, ">") => new BoolValue(l.Value > r.Value),
            (FloatValue l, IntValue r, "<=") => new BoolValue(l.Value <= r.Value),
            (FloatValue l, IntValue r, ">=") => new BoolValue(l.Value >= r.Value),
            (RuneValue l, RuneValue r, "<") => new BoolValue(l.Value < r.Value),
            (RuneValue l, RuneValue r, ">") => new BoolValue(l.Value > r.Value),
            (RuneValue l, RuneValue r, "<=") => new BoolValue(l.Value <= r.Value),
            (RuneValue l, RuneValue r, ">=") => new BoolValue(l.Value >= r.Value),
            _ => throw new SemanticError("Invalid operation", context.Start)
        };
    }

    //VisitEquality
    public override ValueWrapper VisitEquality(LanguageParser.EqualityContext context)
    {
        var left = Visit(context.expr(0));
        var right = Visit(context.expr(1));
        var op = context.op.Text;
        return (left, right, op) switch
        {
            (IntValue l, IntValue r, "==") => new BoolValue(l.Value == r.Value),
            (IntValue l, IntValue r, "!=") => new BoolValue(l.Value != r.Value),
            (FloatValue l, FloatValue r, "==") => new BoolValue(l.Value == r.Value),
            (IntValue l, FloatValue r, "==") => new BoolValue(l.Value == r.Value),
            (FloatValue l, IntValue r, "==") => new BoolValue(l.Value == r.Value),
            (IntValue l, FloatValue r, "!=") => new BoolValue(l.Value != r.Value),
            (FloatValue l, IntValue r, "!=") => new BoolValue(l.Value != r.Value),
            (FloatValue l, FloatValue r, "!=") => new BoolValue(l.Value != r.Value),
            (StringValue l, StringValue r, "==") => new BoolValue(l.Value == r.Value),
            (StringValue l, StringValue r, "!=") => new BoolValue(l.Value != r.Value),
            (BoolValue l, BoolValue r, "==") => new BoolValue(l.Value == r.Value),
            (BoolValue l, BoolValue r, "!=") => new BoolValue(l.Value != r.Value),
            (RuneValue l, RuneValue r, "==") => new BoolValue(l.Value == r.Value),
            (RuneValue l, RuneValue r, "!=") => new BoolValue(l.Value != r.Value),  
            _ => throw new SemanticError("Invalid operation", context.Start)
        };
    }

    //VisitAssign
    public override ValueWrapper VisitAssign(LanguageParser.AssignContext context)
{
    var asignee = context.expr(0);
    ValueWrapper value = Visit(context.expr(1));

    // Asignación a una variable
    if (asignee is LanguageParser.IdentifierContext idContext)
    {
        string id = idContext.ID().GetText();
        try
        {
            currentEnvironment.Assign(id, value, context.Start);
        }
        catch (SemanticError e)
        {
            throw new SemanticError($"Error assigning to variable '{id}': {e.Message}", context.Start);
        }
        return defaultValue;
    }
    // Asignación a una propiedad de una instancia
    else if (asignee is LanguageParser.CalleeContext calleeContext)
    {
        ValueWrapper callee = Visit(calleeContext.expr());
        for (int i = 0; i < calleeContext.call().Length; i++)
        {
            var action = calleeContext.call(i);
            if (i == calleeContext.call().Length - 1)
            {
                if (action is LanguageParser.GetContext propertyAccess)
                {
                    if (callee is InstanceValue instanceValue)
                    {
                        var instance = instanceValue.Instance;
                        var propertyName = propertyAccess.ID().GetText();

                        // Verificar el tipo de la propiedad antes de asignar
                        try
                        {
                            instance.Set(propertyName, value);
                        }
                        catch (SemanticError e)
                        {
                            throw new SemanticError($"Error assigning to property '{propertyName}': {e.Message}", context.Start);
                        }
                    }
                    else if (callee is StructValue structValue)
                    {
                        var fieldName = propertyAccess.ID().GetText();
                        Console.WriteLine($"DEBUG: VisitAssign - Attempting to access field '{fieldName}'");
                        Console.WriteLine($"DEBUG: VisitAssign - Current struct is {structValue.Struct.Name}");
                        
                        // Para accesos anidados, necesitamos navegar hasta el struct correcto
                        StructValue currentStruct = structValue;

                        // Procesar todos los accesos excepto el último
                        for (int j = 0; j < i; j++)
                        {
                            var intermediateAccess = calleeContext.call(j);
                            if (intermediateAccess is LanguageParser.GetContext getContext)
                            {
                                var intermediateField = getContext.ID().GetText();
                                Console.WriteLine($"DEBUG: VisitAssign - Navigating through field '{intermediateField}'");
                                
                                if (!currentStruct.Struct.HasField(intermediateField))
                                {
                                    throw new SemanticError($"Struct {currentStruct.Struct.Name} does not have field '{intermediateField}'", getContext.Start);
                                }

                                var nextValue = currentStruct.Values[intermediateField];
                                if (nextValue is not StructValue nextStruct)
                                {
                                    throw new SemanticError($"Field '{intermediateField}' is not a struct", getContext.Start);
                                }
                                
                                currentStruct = nextStruct;
                                Console.WriteLine($"DEBUG: VisitAssign - Updated current struct to {currentStruct.Struct.Name}");
                            }
                        }

                        // Realizar la asignación en el último campo
                        if (!currentStruct.Struct.HasField(fieldName))
                        {
                            throw new SemanticError($"Struct {currentStruct.Struct.Name} does not have field '{fieldName}'", propertyAccess.Start);
                        }

                        var fieldType = currentStruct.Struct.GetFieldType(fieldName);
                        if (!IsTypeCompatible(fieldType, value))
                        {
                            throw new SemanticError($"Cannot assign value of type '{value.GetType().Name}' to field of type '{fieldType}'", context.Start);
                        }

                        currentStruct.Values[fieldName] = value;
                        Console.WriteLine($"DEBUG: VisitAssign - Successfully assigned value to field '{fieldName}' in struct {currentStruct.Struct.Name}");
                        return defaultValue;
                    }
                    
                    else
                    {
                        throw new SemanticError("Invalid property access: Target is not an instance or struct", context.Start);
                    }
                }
                else
                {
                    throw new SemanticError("Invalid assignment: Unsupported action", context.Start);
                }
            }
        }
        return defaultValue;
    }
    else if (asignee is LanguageParser.MultiDimSliceAccessContext multiDimAccess)
        {
            var slice = Visit(multiDimAccess.expr(0));
            var rowIndex = Visit(multiDimAccess.expr(1));
            var colIndex = Visit(multiDimAccess.expr(2));

            if (slice is not SliceValue sliceValue)
            {
                throw new SemanticError("The target is not a multidimensional slice", context.Start);
            }

            if (rowIndex is not IntValue rowInt || colIndex is not IntValue colInt)
            {
                throw new SemanticError("The indices must be integers", context.Start);
            }

            // Verificar que el índice de la fila esté dentro del rango
            if (rowInt.Value < 0 || rowInt.Value >= sliceValue.Elements.Count)
            {
                throw new SemanticError("Row index out of bounds", context.Start);
            }

            var row = sliceValue.Elements[rowInt.Value];

            if (row is not SliceValue rowSlice)
            {
                throw new SemanticError("The row is not a slice", context.Start);
            }

            // Verificar que el índice de la columna esté dentro del rango
            if (colInt.Value < 0 || colInt.Value >= rowSlice.Elements.Count)
            {
                throw new SemanticError("Column index out of bounds", context.Start);
            }

            // Verificar que el tipo del valor sea compatible con el tipo del slice
            if (!IsTypeCompatible(rowSlice.Type, value))
            {
                throw new SemanticError($"Cannot assign value of type '{value.GetType().Name}' to slice of type '{rowSlice.Type}'", context.Start);
            }

            // Asignar el nuevo valor
            rowSlice.Elements[colInt.Value] = value;
            return defaultValue;
        }
        // Asignación a slice
                    else if (asignee is LanguageParser.SliceAccessContext sliceAccess)
                    {
                        var slice = Visit(sliceAccess.expr(0));
                        var index = Visit(sliceAccess.expr(1));

                        if (slice is not SliceValue sliceValue)
                        {
                            throw new SemanticError("Cannot index non-slice value", context.Start);
                        }

                        if (index is not IntValue indexValue)
                        {
                            throw new SemanticError("Slice index must be integer", context.Start);
                        }

                        // Verificar que el índice esté dentro de los límites
                        if (indexValue.Value < 0 || indexValue.Value >= sliceValue.Elements.Count)
                        {
                            throw new SemanticError("Index out of bounds", context.Start);
                        }

                        // Verificar que el tipo del valor a asignar sea compatible con el tipo del slice
                        if (!IsTypeCompatible(sliceValue.Type, value))
                        {
                            throw new SemanticError($"Cannot assign value of type {value.GetType().Name} to slice of type {sliceValue.Type}", context.Start);
                        }

                        // Actualizar el elemento en el slice
                        sliceValue.Elements[indexValue.Value] = value;
                        return value;
                    }
    else
    {
        throw new SemanticError("Invalid assignment target", context.Start);
    }
}

    //VisitBlockStmt
    public override ValueWrapper VisitBlockStmt(LanguageParser.BlockStmtContext context)
    {
        Environment previousEnvironment = currentEnvironment;
        currentEnvironment = new Environment(previousEnvironment);
        foreach (var stmt in context.dcl())
        {
            Visit(stmt);
        }
        currentEnvironment = previousEnvironment;
        return defaultValue;
    }

    //VisitIfStmt
    public override ValueWrapper VisitIfStmt(LanguageParser.IfStmtContext context)
{
    // Evaluar la condición del if (primera expresión)
    ValueWrapper condition = Visit(context.expr(0));
    if (condition is not BoolValue)
    {
        throw new SemanticError("Invalid condition", context.Start);
    }

    // Si la condición es verdadera, ejecutar el primer bloque
    if (((BoolValue)condition).Value)
    {
        Visit(context.stmt(0));
    }
    else
    {
        // Evaluar los bloques else if y else
        for (int i = 1; i < context.expr().Length; i++)
        {
            condition = Visit(context.expr(i));
            if (condition is not BoolValue)
            {
                throw new SemanticError("Invalid condition", context.Start);
            }

            if (((BoolValue)condition).Value)
            {
                Visit(context.stmt(i));
                return defaultValue;
            }
        }

        // Si hay un bloque else, ejecutarlo
        if (context.stmt().Length > context.expr().Length)
        {
            Visit(context.stmt(context.stmt().Length - 1));
        }
    }

    return defaultValue;
        }   

    //VisitWhileStmt
    public override ValueWrapper VisitWhileStmt(LanguageParser.WhileStmtContext context)
    {
        //similar al for pero sin el visitForBody
        ValueWrapper condition = Visit(context.expr());
        var lastEnvironment = currentEnvironment;

        if (condition is not BoolValue)
        {
            throw new SemanticError("Invalid condition", context.Start);
        }

        try
        {
            while (((BoolValue)condition).Value)
            {
                Visit(context.stmt());
                condition = Visit(context.expr());
                if (condition is not BoolValue)
                {
                    throw new SemanticError("Invalid condition", context.Start);
                }
            }
        }
        catch (BreakException)
        {
            currentEnvironment = lastEnvironment;
        }
        catch (ContinueException)
        {
            currentEnvironment = lastEnvironment;
            condition = Visit(context.expr());
            VisitWhileStmt(context);
        }

        return defaultValue;

    }

    //VisitForStmt
    public override ValueWrapper VisitForStmt(LanguageParser.ForStmtContext context)
    {
        Environment previousEnvironment = currentEnvironment;
        currentEnvironment = new Environment(currentEnvironment);

        Visit(context.forInit());
        VisitForBody(context);

        currentEnvironment = previousEnvironment;
        return defaultValue;
    }

    public void VisitForBody(LanguageParser.ForStmtContext context)
    {
        ValueWrapper condition = Visit(context.expr(0));
        var lastEnvironment = currentEnvironment;

        if (condition is not BoolValue)
        {
            throw new SemanticError("Invalid condition", context.Start);
        }

        try
        {
            while (condition is BoolValue boolCondition && boolCondition.Value)
            {
                Visit(context.stmt());
                Visit(context.expr(1));
                condition = Visit(context.expr(0));
            }
        }
        catch (BreakException)
        {
            currentEnvironment = lastEnvironment;
        }
        catch (ContinueException)
        {
            currentEnvironment = lastEnvironment;
            Visit(context.expr(1));
            VisitForBody(context);
        }
    }

    //VisitDoWhileStmt
    public override ValueWrapper VisitDoWhileStmt(LanguageParser.DoWhileStmtContext context)
    {
        ValueWrapper condition = Visit(context.expr());
        if (condition is not BoolValue)
        {
            throw new SemanticError("Invalid condition", context.Start);
        }
        do
        {
            Visit(context.stmt());
            condition = Visit(context.expr());
            if (condition is not BoolValue)
            {
                throw new SemanticError("Invalid condition", context.Start);
            }
        } while (((BoolValue)condition).Value);
        return defaultValue;
    }

    //VisitBreakStmt
    public override ValueWrapper VisitBreakStmt(LanguageParser.BreakStmtContext context)
    {
        // Verificar si estamos dentro de un switch o un bucle
        var node = context.Parent;
        while (node != null)
        {
            if (node is LanguageParser.SwitchStmtContext || 
                node is LanguageParser.ForStmtContext || 
                node is LanguageParser.WhileStmtContext ||
                node is LanguageParser.ForRangeStmtContext ||
                node is LanguageParser.ForConditionStmtContext)
            {
                throw new BreakException();
            }
            node = node.Parent;
        }
        
        throw new SemanticError("Break statement outside loop or switch", context.Start);
    }

    //VisitContinueStmt
    public override ValueWrapper VisitContinueStmt(LanguageParser.ContinueStmtContext context)
    {
        throw new ContinueException();
    }

    //VisitReturnStmt
    public override ValueWrapper VisitReturnStmt(LanguageParser.ReturnStmtContext context)
    {
        ValueWrapper value = this.defaultValue;

        if (context.expr() != null)
        {
            value = Visit(context.expr());
        }

        throw new ReturnException(value);
    }

    //VisitCallee
    public override ValueWrapper VisitCallee(LanguageParser.CalleeContext context)
    {
        Console.WriteLine("DEBUG: Entering VisitCallee");
        ValueWrapper callee = Visit(context.expr());
        Console.WriteLine($"DEBUG: Initial callee type is {callee.GetType().Name}");

        foreach (var action in context.call())
        {
            Console.WriteLine($"DEBUG: Processing action of type {action.GetType().Name}");
            if (action is LanguageParser.FuncCallContext funcCall)
            {
                if (callee is FunctionValue functionValue)
                {
                    callee = VisitCall(functionValue.invocable, funcCall.args());
                }
                else
                {
                    throw new SemanticError("Invalid function call", context.Start);
                }
            }
            else if (action is LanguageParser.GetContext propertyAccess)
            {
                var fieldName = propertyAccess.ID().GetText();
                Console.WriteLine($"DEBUG: Attempting to access field '{fieldName}'");
                
                if (callee is InstanceValue instanceValue)
                {
                    callee = instanceValue.Instance.Get(propertyAccess.ID().GetText(), propertyAccess.Start);
                }
                else if (callee is StructValue structValue)
                {
                    Console.WriteLine($"DEBUG: Callee is a StructValue named {structValue.Struct.Name}");
                    Console.WriteLine($"DEBUG: Available fields: {string.Join(", ", structValue.Struct.Fields.Keys)}");
                    
                    if (!structValue.Struct.HasField(fieldName))
                    {
                        Console.WriteLine($"DEBUG: Field '{fieldName}' not found in struct");
                        throw new SemanticError($"Struct {structValue.Struct.Name} does not have field '{fieldName}'", propertyAccess.Start);
                    }
                    // Obtener el valor del campo y usarlo como el nuevo callee
                    callee = structValue.Values[fieldName];
                    Console.WriteLine($"DEBUG: Updated callee type to {callee.GetType().Name}");
                }
                else
                {
                    throw new SemanticError($"Cannot access field '{fieldName}' on {callee.GetType().Name}", propertyAccess.Start);
                }
            }
        }
        return callee;
    }

    public ValueWrapper VisitCall(Invocable invocable, LanguageParser.ArgsContext context)
    {
        List<ValueWrapper> args = new List<ValueWrapper>();

        if(context!=null){
            foreach(var expr in context.expr())
            {
                args.Add(Visit(expr));
            }
        }

        //if(context!=null && args.Count != invocable.Arity())
        //{
          //  throw new SemanticError("Invalid number of arguments", context.Start);
        //}

        return invocable.Invoke(args, this);
    }

    //VisitFuncDcl
    public override ValueWrapper VisitFuncDcl(LanguageParser.FuncDclContext context)
    {
        var foreign = new ForeignFunction(currentEnvironment, context);
        currentEnvironment.Declare(context.ID().GetText(), new FunctionValue(foreign, context.ID().GetText()), context.Start);
        return defaultValue;
    }

    //VisitClassDcl
    

    //VisitNew
    public override ValueWrapper VisitNew(LanguageParser.NewContext context)
    {
        ValueWrapper classValue = currentEnvironment.Get(context.ID().GetText(), context.Start);

        if (classValue is not ClassValue)
        {
            throw new SemanticError("Invalid class", context.Start);
        }

        List<ValueWrapper> arguments = new List<ValueWrapper>();

        if(context.args() != null)
        {
            foreach (var arg in context.args().expr())
            {
                arguments.Add(Visit(arg));
            }
        }

        var instance = ((ClassValue)classValue).languageClass.Invoke(arguments, this);

        return instance;
    }


    //VisitAssignOp
    public override ValueWrapper VisitAssignOp(LanguageParser.AssignOpContext context)
{
    var asignee = context.expr(0); // Variable a la que se asignará el valor
    var value = Visit(context.expr(1)); // Valor a sumar o restar
    var op = context.op.Text; // Operador (+= o -=)

    if (asignee is LanguageParser.IdentifierContext idContext)
    {
        string id = idContext.ID().GetText();
        var variable = currentEnvironment.Get(id, context.Start);

        // Verificar el tipo de la variable y realizar la operación
        if (variable is IntValue intVar && value is IntValue intValue)
        {
            if (op == "+=")
            {
                variable = new IntValue(intVar.Value + intValue.Value);
            }
            else if (op == "-=")
            {
                variable = new IntValue(intVar.Value - intValue.Value);
            }
        }
        else if (variable is FloatValue floatVar && (value is FloatValue || value is IntValue))
            {
                float operand = value is FloatValue floatValue ? floatValue.Value : ((IntValue)value).Value;
                if (op == "+=")
                {
                    variable = new FloatValue(floatVar.Value + operand);
                }
                else if (op == "-=")
                {
                    variable = new FloatValue(floatVar.Value - operand);
                }
            }
        else if (variable is StringValue strVar && value is StringValue strValue && op == "+=")
        {
            variable = new StringValue(strVar.Value + strValue.Value); // Concatenación de cadenas
        }
        else
        {
            throw new SemanticError($"Invalid operation '{op}' between {variable.GetType().Name} and {value.GetType().Name}", context.Start);
        }

        // Actualizar el valor de la variable en el entorno
        currentEnvironment.Assign(id, variable, context.Start);
        return defaultValue;
    }
    else
    {
        throw new SemanticError("Invalid assignment target", context.Start);
    }
}

  //VisitLogical
    public override ValueWrapper VisitLogical(LanguageParser.LogicalContext context)
    {
        var left = Visit(context.expr(0));
        var right = Visit(context.expr(1));
        var op = context.op.Text;
        return (left, right, op) switch
        {
            (BoolValue l, BoolValue r, "&&") => new BoolValue(l.Value && r.Value),
            (BoolValue l, BoolValue r, "||") => new BoolValue(l.Value || r.Value),
            _ => throw new SemanticError("Invalid operation", context.Start)
        };
    }

    //VisitNot
    public override ValueWrapper VisitNot(LanguageParser.NotContext context)
    {
        var value = Visit(context.expr());
        if (value is BoolValue boolValue)
        {
            return new BoolValue(!boolValue.Value);
        }
        else
        {
            throw new SemanticError("Invalid operation", context.Start);
        }
    }

    //VisitPrint
    public override ValueWrapper VisitPrintStmt(LanguageParser.PrintStmtContext context)
    {
        var output = "";

        if (context.args() != null)
        {
            foreach (var arg in context.args().expr())
            {
                var value = Visit(arg);

                output += value switch
                {
                    IntValue i => i.Value.ToString(),
                    FloatValue f => f.Value.ToString(CultureInfo.InvariantCulture),
                    StringValue s => s.Value,
                    BoolValue b => b.Value.ToString(),
                    SliceValue slice => $"[{string.Join(", ", slice.Elements.Select(e => ConvertElementToString(e)))}]",
                    MultiDimSliceValue multiSlice => $"[{string.Join(", ", multiSlice.Elements.Select(row => 
                        $"[{string.Join(", ", row.Select(e => ConvertElementToString(e)))}]"))}]",
                    StructValue structValue => $"{{{string.Join(", ", structValue.Values.Select(kv => $"{kv.Key}: {ConvertElementToString(kv.Value)}"))}}}",    
                    VoidValue _ => "void",
                    _ => throw new SemanticError($"Unsupported type in fmt.Println: {value.GetType().Name}", context.Start)
                };

                output += " ";
            }
        }

        this.output += output.TrimEnd() + "\n";
        return defaultValue;
    }

    // Método auxiliar para convertir elementos del slice a cadenas
    private string ConvertElementToString(ValueWrapper element)
    {
        if(element == null || element is NullValue)
        {
            return "nil";
        }

        return element switch
        {
            IntValue i => i.Value.ToString(),
            FloatValue f => f.Value.ToString(CultureInfo.InvariantCulture),
            StringValue s => s.Value,
            BoolValue b => b.Value.ToString(),
            RuneValue r => $"'{(char)r.Value}'",
            _ => throw new SemanticError($"Unsupported type in slice: {element.GetType().Name}", null)
        };
    }

        //SwitchStmt CaseStatement DefaultCaseStmt

        public override ValueWrapper VisitSwitchStmt(LanguageParser.SwitchStmtContext context)
        {
            ValueWrapper switchValue = Visit(context.expr());
            bool caseMatched = false;

            foreach (var switchCase in context.switchCase())
            {
                if (switchCase.caseStmt() != null)
                {
                    var caseStmt = switchCase.caseStmt();
                    ValueWrapper caseValue = Visit(caseStmt.expr());
                    
                    if (switchValue.Equals(caseValue))
                    {
                        caseMatched = true;
                        try
                        {
                            foreach (var stmt in caseStmt.stmt())
                            {
                                Visit(stmt);
                            }
                        }
                        catch (BreakException)
                        {
                            return defaultValue; // Salir del switch cuando encontramos un break
                        }
                        break;
                    }
                }
                else if (switchCase.defaultStmt() != null && !caseMatched)
                {
                    var defaultStmt = switchCase.defaultStmt();
                    try
                    {
                        foreach (var stmt in defaultStmt.stmt())
                        {
                            Visit(stmt);
                        }
                    }
                    catch (BreakException)
                    {
                        return defaultValue; // Salir del switch cuando encontramos un break
                    }
                }
            }

            return defaultValue;
        }

    private bool EvaluateCase(ValueWrapper caseValue)
    {

        return true;
    }


    public override ValueWrapper VisitCaseStmt(LanguageParser.CaseStmtContext context)
    {
        var caseValue = Visit(context.expr());
        
        // Ejecutar todas las instrucciones del case
        foreach (var stmt in context.stmt())
        {
            Visit(stmt);
        }
        
        return defaultValue;
    }

    //VisitForConditionStmt
    public override ValueWrapper VisitForConditionStmt(LanguageParser.ForConditionStmtContext context)
    {
        Environment previousEnvironment = currentEnvironment;
        currentEnvironment = new Environment(currentEnvironment);

        try
        {
            while (true)  // Si no hay condición, es un bucle infinito
            {
                // Si hay una condición, evaluarla
                if (context.expr() != null)
                {
                    var condition = Visit(context.expr());
                    if (condition is not BoolValue boolCondition)
                    {
                        throw new SemanticError("For condition must be boolean", context.Start);
                    }
                    if (!boolCondition.Value)
                    {
                        break;
                    }
                }

                try
                {
                    foreach (var stmt in context.stmt())
                    {
                        Visit(stmt);
                    }
                }
                catch (BreakException)
                {
                    break;
                }
                catch (ContinueException)
                {
                    continue;
                }
            }
        }
        finally
        {
            currentEnvironment = previousEnvironment;
        }

        return defaultValue;
    }

    //VisitIncDec
    public override ValueWrapper VisitIncDec(LanguageParser.IncDecContext context)
{
    // Obtener el identificador desde el subárbol expr
    if (context.expr() is LanguageParser.IdentifierContext idContext)
    {
        string id = idContext.ID().GetText();
        var variable = currentEnvironment.Get(id, context.Start);

        // Verificar que la variable sea de tipo IntValue
        if (variable is IntValue intVar)
        {
            // Determinar si es incremento (++) o decremento (--)
            if (context.GetChild(1).GetText() == "++")
            {
                variable = new IntValue(intVar.Value + 1);
            }
            else if (context.GetChild(1).GetText() == "--")
            {
                variable = new IntValue(intVar.Value - 1);
            }

            // Actualizar el valor de la variable en el entorno
            currentEnvironment.Assign(id, variable, context.Start);
            return defaultValue;
        }
        else
        {
            throw new SemanticError("Invalid operation: Only integers can be incremented or decremented", context.Start);
        }
    }
    else
    {
        throw new SemanticError("Invalid increment/decrement target", context.Start);
    }
    }

    public override ValueWrapper VisitSliceInit(LanguageParser.SliceInitContext context)
    {
        var type = context.type().GetText(); // Obtener el tipo del slice
        var elements = new List<ValueWrapper>();

        // Evaluar cada expresión en la inicialización
        foreach (var expr in context.expr())
        {
            var value = Visit(expr);
            elements.Add(value);

            // Verificar que el tipo del elemento coincida con el tipo del slice
            if (!IsTypeCompatible(type, value))
            {
                throw new SemanticError($"All elements in the slice must be of the same type: expected '{type}', but found '{value.GetType().Name}'", context.Start);
            }
        }

        return new SliceValue(type, elements);
    }

    public override ValueWrapper VisitSliceAccess(LanguageParser.SliceAccessContext context)
    {
        var slice = Visit(context.expr(0)); // Obtener el slice o matriz
        var index = Visit(context.expr(1)); // Obtener el índice

        // Manejar acceso a matrices bidimensionales
        if (slice is MultiDimSliceValue matrixValue)
        {
            if (index is not IntValue rowIndex)
            {
                throw new SemanticError("Matrix row index must be an integer", context.Start);
            }

            if (rowIndex.Value < 0 || rowIndex.Value >= matrixValue.Elements.Count)
            {
                throw new SemanticError("Row index out of bounds", context.Start);
            }

            // Devolver la fila como un nuevo slice
            var row = matrixValue.Elements[rowIndex.Value];
            return new SliceValue(matrixValue.Type, row);
        }
        // Manejar acceso a slices normales
        else if (slice is SliceValue sliceValue)
        {
            if (index is not IntValue intIndex)
            {
                throw new SemanticError("The index must be an integer", context.Start);
            }

            if (intIndex.Value < 0 || intIndex.Value >= sliceValue.Elements.Count)
            {
                throw new SemanticError("Index out of bounds", context.Start);
            }

            return sliceValue.Elements[intIndex.Value];
        }
        else
        {
            throw new SemanticError("The variable is not a slice or matrix", context.Start);
        }
    }

    public override ValueWrapper VisitSliceRange(LanguageParser.SliceRangeContext context)
    {
        var slice = Visit(context.expr(0)); // Obtener el slice
        var startIndex = Visit(context.expr(1)); // Índice inicial
        var endIndex = Visit(context.expr(2)); // Índice final

        if (slice is not SliceValue sliceValue)
        {
            throw new SemanticError("The variable is not a slice", context.Start);
        }

        if (startIndex is not IntValue start || endIndex is not IntValue end)
        {
            throw new SemanticError("The indices must be integers", context.Start);
        }

        if (start.Value < 0 || end.Value > sliceValue.Elements.Count || start.Value > end.Value)
        {
            throw new SemanticError("Invalid slice range", context.Start);
        }

        var subSlice = sliceValue.Elements.GetRange(start.Value, end.Value - start.Value);
        return new SliceValue(sliceValue.Type, subSlice);
    }

    public override ValueWrapper VisitSliceIndexCall(LanguageParser.SliceIndexCallContext context)
    {
        // Obtener los argumentos de la función
        var slice = Visit(context.expr(0));
        var searchValue = Visit(context.expr(1));

        // Verificar que el primer argumento sea un slice
        if (slice is not SliceValue sliceValue)
        {
            throw new SemanticError("The first argument to slices.Index must be a slice", context.Start);
        }

        // Buscar el valor en el slice
        for (int i = 0; i < sliceValue.Elements.Count; i++)
        {
            if (sliceValue.Elements[i].Equals(searchValue))
            {
                return new IntValue(i); // Retorna el índice de la primera coincidencia
            }
        }

        return new IntValue(-1); // Retorna -1 si no se encuentra
    }

    public override ValueWrapper VisitStringsJoinCall(LanguageParser.StringsJoinCallContext context)
    {
        // Obtener los argumentos de la función
        var slice = Visit(context.expr(0));
        var separator = Visit(context.expr(1));

        // Verificar que el primer argumento sea un slice de cadenas
        if (slice is not SliceValue sliceValue || sliceValue.Type != "string")
        {
            throw new SemanticError("The first argument to strings.Join must be a slice of strings ([]string)", context.Start);
        }

        // Verificar que el segundo argumento sea una cadena
        if (separator is not StringValue separatorValue)
        {
            throw new SemanticError("The second argument to strings.Join must be a string", context.Start);
        }

        // Unir los elementos del slice con el separador
        var joinedString = string.Join(separatorValue.Value, sliceValue.Elements.Select(e =>
        {
            if (e is not StringValue stringValue)
            {
                throw new SemanticError("All elements in the slice must be strings", context.Start);
            }
            return stringValue.Value;
        }));

        return new StringValue(joinedString);
    }

    public override ValueWrapper VisitLenCall(LanguageParser.LenCallContext context)
    {
        // Obtener el argumento de la función
        var slice = Visit(context.expr());

        // Verificar que el argumento sea un slice
        if (slice is not SliceValue sliceValue)
        {
            throw new SemanticError("The argument to len must be a slice", context.Start);
        }

        // Retornar la cantidad de elementos en el slice
        return new IntValue(sliceValue.Elements.Count);
    }

    public override ValueWrapper VisitAppendCall(LanguageParser.AppendCallContext context)
    {
        // Obtener el slice base
        var baseSlice = Visit(context.expr(0));
        
        // Si es un slice normal
        if (baseSlice is SliceValue sliceValue)
        {
            List<ValueWrapper> elements = new List<ValueWrapper>(sliceValue.Elements);
            
            // Agregar cada elemento nuevo
            for (int i = 1; i < context.expr().Length; i++)
            {
                var newValue = Visit(context.expr(i));
                if (!IsTypeCompatible(sliceValue.Type, newValue))
                {
                    throw new SemanticError($"Cannot append value of type {newValue.GetType().Name} to slice of type {sliceValue.Type}", context.Start);
                }
                elements.Add(newValue);
            }
            
            return new SliceValue(sliceValue.Type, elements);
        }
        // Si es una matriz (slice bidimensional)
        else if (baseSlice is MultiDimSliceValue matrixValue)
        {
            var elements = new List<List<ValueWrapper>>(matrixValue.Elements);
            
            // Para cada nuevo elemento a agregar
            for (int i = 1; i < context.expr().Length; i++)
            {
                var newValue = Visit(context.expr(i));
                
                // Si es un slice, lo agregamos como nueva fila
                if (newValue is SliceValue newSlice)
                {
                    // Verificar que el tipo sea compatible
                    if (newSlice.Type != matrixValue.Type)
                    {
                        throw new SemanticError($"Cannot append slice of type {newSlice.Type} to matrix of type {matrixValue.Type}", context.Start);
                    }
                    elements.Add(new List<ValueWrapper>(newSlice.Elements));
                }
                else
                {
                    throw new SemanticError("Can only append slices to a matrix", context.Start);
                }
            }
            
            return new MultiDimSliceValue(matrixValue.Type, elements);
        }
        else
        {
            throw new SemanticError("The first argument to append must be a slice", context.Start);
        }
    }
            
        public override ValueWrapper VisitMultiDimSliceInit(LanguageParser.MultiDimSliceInitContext context)
        {
            Console.WriteLine("Entering MultiDimSliceInit");
            var type = context.type().GetText();
            var rows = new List<List<ValueWrapper>>();

            foreach (var rowExpr in context.matrixRow())
            {
                Console.WriteLine($"Processing row: {rowExpr.GetText()}");
                var currentRow = new List<ValueWrapper>();

                // Procesar cada elemento de la fila
                foreach (var element in rowExpr.expr())
                {
                    var value = Visit(element);
                    
                    // Verificar que el tipo del elemento coincida
                    if (!IsTypeCompatible(type, value))
                    {
                        throw new SemanticError($"Type mismatch in matrix: Expected {type}, got {value.GetType().Name}", element.Start);
                    }
                    
                    currentRow.Add(value);
                }
                
                
                
                rows.Add(currentRow);
            }

            if (rows.Count == 0)
            {
                throw new SemanticError("Matrix cannot be empty", context.Start);
            }

            Console.WriteLine($"Created matrix with {rows.Count} rows");
            return new MultiDimSliceValue(type, rows);
        }

    public override ValueWrapper VisitMultiDimSliceAccess(LanguageParser.MultiDimSliceAccessContext context)
    {
        var matrix = Visit(context.expr(0));
        var row = Visit(context.expr(1));
        var col = Visit(context.expr(2));

        if (matrix is not MultiDimSliceValue matrixValue)
        {
            throw new SemanticError("Expected a matrix", context.Start);
        }

        if (row is not IntValue rowIndex || col is not IntValue colIndex)
        {
            throw new SemanticError("Matrix indices must be integers", context.Start);
        }

        return matrixValue.GetValue(rowIndex.Value, colIndex.Value);
    }

    public override ValueWrapper VisitAtoiCall(LanguageParser.AtoiCallContext context)
    {
        var arg = Visit(context.expr());
        
        // Verificar que el argumento sea un string
        if (arg is not StringValue strValue)
        {
            throw new SemanticError("strconv.Atoi expects a string argument", context.Start);
        }

        // Remover las comillas del string
        var strNumber = strValue.Value.Trim('"');

        // Verificar que no sea un número decimal
        if (strNumber.Contains('.'))
        {
            throw new SemanticError("strconv.Atoi cannot convert decimal numbers", context.Start);
        }

        // Intentar convertir a entero
        if (int.TryParse(strNumber, out int result))
        {
            return new IntValue(result);
        }
        else
        {
            throw new SemanticError($"strconv.Atoi: invalid integer format in \"{strNumber}\"", context.Start);
        }
    }

    public override ValueWrapper VisitParseFloatCall(LanguageParser.ParseFloatCallContext context)
    {
        // Obtener el primer argumento que es la cadena a convertir
        var arg = Visit(context.expr());
        
        // Verificar que el argumento sea un string
        if (arg is not StringValue strValue)
        {
            throw new SemanticError("strconv.ParseFloat expects a string argument", context.Start);
        }

        // Remover las comillas del string
        var strNumber = strValue.Value.Trim('"');

        // Intentar convertir a float
        if (float.TryParse(strNumber, 
            NumberStyles.Float | NumberStyles.AllowDecimalPoint, 
            CultureInfo.InvariantCulture, 
            out float result))
        {
            return new FloatValue(result);
        }
        else
        {
            throw new SemanticError($"strconv.ParseFloat: invalid float format in \"{strNumber}\"", context.Start);
        }
    }

    public override ValueWrapper VisitTypeOfCall(LanguageParser.TypeOfCallContext context)
    {
        var value = Visit(context.expr());
        string type = value switch
        {
            IntValue => "int",
            FloatValue => "float64",
            BoolValue => "bool",
            StringValue => "string",
            SliceValue slice => $"[]" + slice.Type,
            MultiDimSliceValue matrix => $"[][]" + matrix.Type,
            VoidValue => "void",
            _ => value.GetType().Name.Replace("Value", "")
        };
        
        // Devolver el tipo como un string
        return new StringValue(type);
    }

    public override ValueWrapper VisitStructDcl(LanguageParser.StructDclContext context)
    {
        var structName = context.ID().GetText();
        var fields = new Dictionary<string, string>();

        // Verificar que tenga al menos un campo
        if (context.structBody().Length == 0)
        {
            throw new SemanticError("Struct must have at least one field", context.Start);
        }

        // Procesar cada campo
        foreach (var field in context.structBody())
        {
            var fieldName = field.ID().GetText();
            var fieldType = field.type().GetText();
            
            // Verificar que el campo no esté duplicado
            if (fields.ContainsKey(fieldName))
            {
                throw new SemanticError($"Duplicate field '{fieldName}' in struct", field.Start);
            }

            fields[fieldName] = fieldType;
        }

        // Crear y guardar el struct en el entorno global
        var languageStruct = new LanguageStruct(structName);
        foreach (var field in fields)
        {
            languageStruct.AddField(field.Key, field.Value);
        }
        
        // Crear un StructValue inicial con valores por defecto
        var defaultValues = new Dictionary<string, ValueWrapper>();
        foreach (var field in fields)
        {
            defaultValues[field.Key] = GetDefaultValue(field.Value);
        }
        
        // Agregar el struct al entorno como un StructValue
        currentEnvironment.Declare(structName, new StructValue(languageStruct, defaultValues), context.Start);

        return defaultValue;
    }

    public override ValueWrapper VisitStructInit(LanguageParser.StructInitContext context)
    {
        var structName = context.ID().GetText();
        var structDef = currentEnvironment.Get(structName, context.Start);

        if (structDef is not StructValue structValue)
        {
            throw new SemanticError($"'{structName}' is not a struct", context.Start);
        }

        var values = new Dictionary<string, ValueWrapper>();

        // Process each field initializer
        foreach (var field in context.structField())
        {
            var fieldName = field.ID().GetText();
            var fieldValue = Visit(field.expr());

            if (!structValue.Struct.HasField(fieldName))
            {
                throw new SemanticError($"Unknown field '{fieldName}' in struct '{structName}'", field.Start);
            }

            var fieldType = structValue.Struct.GetFieldType(fieldName);
            if (!IsTypeCompatible(fieldType, fieldValue))
            {
                throw new SemanticError($"Type mismatch for field '{fieldName}': expected {fieldType}", field.Start);
            }

            values[fieldName] = fieldValue;
        }

        // Check all fields are initialized or set defaults
        foreach (var field in structValue.Struct.Fields)
        {
            if (!values.ContainsKey(field.Key))
            {
                values[field.Key] = GetDefaultValue(field.Value);
            }
        }

        return new StructValue(structValue.Struct, values);
    }

    public override ValueWrapper VisitStructAccess(LanguageParser.StructAccessContext context)
    {
        Console.WriteLine($"DEBUG: Entering VisitStructAccess");
        var target = Visit(context.expr());
        Console.WriteLine($"DEBUG: Target type is {target.GetType().Name}");
        var fieldName = context.ID().GetText();
        Console.WriteLine($"DEBUG: Trying to access field '{fieldName}'");

        if (target is not StructValue structValue)
        {
            Console.WriteLine($"DEBUG: Target is not a StructValue, it's a {target.GetType().Name}");
            throw new SemanticError($"Cannot access field '{fieldName}' on non-struct value", context.Start);
        }

        Console.WriteLine($"DEBUG: Struct name is {structValue.Struct.Name}");
        Console.WriteLine($"DEBUG: Available fields: {string.Join(", ", structValue.Struct.Fields.Keys)}");

        if (!structValue.Struct.HasField(fieldName))
        {
            Console.WriteLine($"DEBUG: Field '{fieldName}' not found in struct");
            throw new SemanticError($"Struct does not have field '{fieldName}'", context.ID().Symbol);
        }

        var value = structValue.Values[fieldName];
        Console.WriteLine($"DEBUG: Successfully retrieved field value of type {value.GetType().Name}");
        return value;
    }

    public override ValueWrapper VisitForRangeStmt(LanguageParser.ForRangeStmtContext context)
    {
        Environment previousEnvironment = currentEnvironment;
        currentEnvironment = new Environment(currentEnvironment);

        try 
        {
            var collection = Visit(context.expr());

            if (collection is not SliceValue sliceValue)
            {
                throw new SemanticError("Can only iterate over slice types", context.Start);
            }

            var indexVar = context.ID(0).GetText();
            var valueVar = context.ID(1).GetText();

            // Declarar las variables una sola vez antes del bucle
            currentEnvironment.Declare(indexVar, new IntValue(0), context.Start);
            currentEnvironment.Declare(valueVar, sliceValue.Elements[0], context.Start);

            for (int i = 0; i < sliceValue.Elements.Count; i++)
            {
                // Actualizar valores en lugar de redeclarar
                currentEnvironment.Update(indexVar, new IntValue(i), context.Start);
                currentEnvironment.Update(valueVar, sliceValue.Elements[i], context.Start);

                try
                {
                    foreach (var stmt in context.stmt())
                    {
                        Visit(stmt);
                    }
                }
                catch (BreakException)
                {
                    break;
                }
                catch (ContinueException)
                {
                    continue;
                }
            }
        }
        finally
        {
            currentEnvironment = previousEnvironment;
        }

        return defaultValue;
    }

    public override ValueWrapper VisitNilExpr(LanguageParser.NilExprContext context)
    {
        return NullValue.Instance;
    }

}