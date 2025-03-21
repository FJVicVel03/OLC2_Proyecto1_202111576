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

    private bool IsTypeCompatible(string type, ValueWrapper value)
{
    return type switch
    {
        "int" => value is IntValue,
        "float" => value is FloatValue || value is IntValue, // Permitir conversión implícita de int a float
        "string" => value is StringValue,
        "bool" => value is BoolValue,
        "rune" => value is RuneValue,
        _ => false
    };
}

    private ValueWrapper GetDefaultValue(string type)
{
    return type switch
    {
        "int" => new IntValue(0),
        "float" => new FloatValue(0.0f),
        "string" => new StringValue(""),
        "bool" => new BoolValue(false),
        "rune" => new RuneValue(0),
        _ => throw new SemanticError($"Unknown type '{type}'", null)
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
                    else
                    {
                        throw new SemanticError("Invalid property access: Target is not an instance", context.Start);
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
        throw new BreakException();
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
        ValueWrapper callee = Visit(context.expr());

        foreach (var action in context.call()){
            if(action is LanguageParser.FuncCallContext funcCall){
                
                if(callee is FunctionValue functionValue){
                    callee = VisitCall(functionValue.invocable, funcCall.args());
                }
                else
                {
                    throw new SemanticError("Invalid function call", context.Start);
                }

            }else if (action is LanguageParser.GetContext propertyAccess){
                if(callee is InstanceValue instanceValue){
                    callee = instanceValue.Instance.Get(propertyAccess.ID().GetText(), propertyAccess.Start); // Corregir error tipográfico
                }else{
                    throw new SemanticError("Invalid property access", context.Start);
                }
            }
        }
        return callee; // Agregar retorno de callee
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
    public override ValueWrapper VisitClassDcl(LanguageParser.ClassDclContext context){
        Dictionary<string, LanguageParser.VarDclContext> props = new Dictionary<string, LanguageParser.VarDclContext>();
        Dictionary<string, ForeignFunction> methods = new Dictionary<string, ForeignFunction>();

        foreach(var prop in context.classBody()){
            if(prop.varDcl() != null){
                var varDcl = prop.varDcl();
                props.Add(varDcl.ID().GetText(), varDcl);
            }else if(prop.funcDcl() != null){
                var funcDcl = prop.funcDcl();
                var foreignFunction = new ForeignFunction(currentEnvironment, funcDcl);
                methods.Add(funcDcl.ID().GetText(), foreignFunction);
            }
        }

        LanguageClass languageClass = new LanguageClass(context.ID().GetText(), props, methods);
        currentEnvironment.Declare(context.ID().GetText(), new ClassValue(languageClass), context.Start);

        return defaultValue;      
    }

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

        // Si hay argumentos, procesarlos
        if (context.args() != null)
        {
            foreach (var arg in context.args().expr())
            {
                var value = Visit(arg);

                // Convertir el valor a cadena según su tipo
                output += value switch
                {
                    IntValue i => i.Value.ToString(),
                    FloatValue f => f.Value.ToString(CultureInfo.InvariantCulture),
                    StringValue s => s.Value,
                    BoolValue b => b.Value.ToString(),
                    SliceValue slice => $"[{string.Join(", ", slice.Elements.Select(e => ConvertElementToString(e)))}]", // Manejar SliceValue                    RuneValue r => $"'{(char)r.Value}'",
                    VoidValue _ => "void",
                    _ => throw new SemanticError($"Unsupported type in fmt.Println: {value.GetType().Name}", context.Start)
                };

                output += " "; // Agregar un espacio entre los argumentos
            }
        }

        // Acumular el resultado en la propiedad `output` para el frontend
        this.output += output.TrimEnd() + "\n";

        return defaultValue;
    }

    // Método auxiliar para convertir elementos del slice a cadenas
    private string ConvertElementToString(ValueWrapper element)
    {
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

        // Iterar sobre los casos del switch
        foreach (var switchCase in context.switchCase())
        {
            if (switchCase.caseStmt() != null) // Caso específico
            {
                var caseStmt = switchCase.caseStmt();
                ValueWrapper caseValue = Visit(caseStmt.expr());
                if (switchValue.Equals(caseValue))
                {
                    caseMatched = true;
                    Visit(caseStmt.stmt());
                    break;
                }
            }
            else if (switchCase.defaultStmt() != null) // Caso por defecto
            {
                if (!caseMatched) // Solo ejecutar si no se ha encontrado un caso coincidente
                {
                    Visit(switchCase.defaultStmt().stmt());
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
        if (EvaluateCase(caseValue))
        {
            return Visit(context.stmt());
        }
        return defaultValue;
    }

    //VisitForConditionStmt
    public override ValueWrapper VisitForConditionStmt(LanguageParser.ForConditionStmtContext context)
    {
    // Evaluar la condición inicial
    ValueWrapper condition = Visit(context.expr());
    if (condition is not BoolValue)
    {
        throw new SemanticError("Invalid condition in for loop", context.Start);
    }

    // Ejecutar el cuerpo del bucle mientras la condición sea verdadera
    while (((BoolValue)condition).Value)
    {
        foreach (var stmt in context.stmt())
        {
            Visit(stmt);
        }

        // Reevaluar la condición
        condition = Visit(context.expr());
        if (condition is not BoolValue)
        {
            throw new SemanticError("Invalid condition in for loop", context.Start);
        }
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
        var slice = Visit(context.expr(0)); // Obtener el slice
        var index = Visit(context.expr(1)); // Obtener el índice

        if (slice is not SliceValue sliceValue)
        {
            throw new SemanticError("The variable is not a slice", context.Start);
        }

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
    
}