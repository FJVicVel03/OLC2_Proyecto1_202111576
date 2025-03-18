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
        var left = Visit(context.expr(0));
        var right = Visit(context.expr(1));
        var op = context.op.Text;
        return (left, right, op) switch
        {
            (IntValue l, IntValue r, "*") => new IntValue(l.Value * r.Value),
            (IntValue l, IntValue r, "/") => new IntValue(l.Value / r.Value),
            (IntValue l, FloatValue r, "*") => new FloatValue(l.Value * r.Value),
            (IntValue l, FloatValue r, "/") => new FloatValue(l.Value / r.Value),
            (FloatValue l, IntValue r, "*") => new FloatValue(l.Value * r.Value),
            (FloatValue l, IntValue r, "/") => new FloatValue(l.Value / r.Value),
            (FloatValue l, FloatValue r, "*") => new FloatValue(l.Value * r.Value),
            (FloatValue l, FloatValue r, "/") => new FloatValue(l.Value / r.Value),
            _ => throw new SemanticError("Invalid operation", context.Start)
        };
    }

    //VisitAddSub
    public override ValueWrapper VisitAddSub(LanguageParser.AddSubContext context)
    {
        var left = Visit(context.expr(0));
        var right = Visit(context.expr(1));
        var op = context.op.Text;
        return (left, right, op) switch
        {
            (IntValue l, IntValue r, "+") => new IntValue(l.Value + r.Value),
            (IntValue l, IntValue r, "-") => new IntValue(l.Value - r.Value),
            (IntValue l, FloatValue r, "+") => new FloatValue(l.Value + r.Value),
            (IntValue l, FloatValue r, "-") => new FloatValue(l.Value - r.Value),
            (FloatValue l, IntValue r, "+") => new FloatValue(l.Value + r.Value),
            (FloatValue l, IntValue r, "-") => new FloatValue(l.Value - r.Value),
            (FloatValue l, FloatValue r, "+") => new FloatValue(l.Value + r.Value),
            (FloatValue l, FloatValue r, "-") => new FloatValue(l.Value - r.Value),
            (StringValue l, StringValue r, "+") => new StringValue(l.Value + r.Value),
            (StringValue l, IntValue r, "+") => new StringValue(l.Value + r.Value.ToString()),
            (StringValue l, FloatValue r, "+") => new StringValue(l.Value + r.Value.ToString()),
            (IntValue l, StringValue r, "+") => new StringValue(l.Value.ToString() + r.Value),
            (FloatValue l, StringValue r, "+") => new StringValue(l.Value.ToString() + r.Value),
            _ => throw new SemanticError("Invalid operation", context.Start)
        };
    }

    //VisitProgram
    public override ValueWrapper VisitProgram(LanguageParser.ProgramContext context)
    {
        foreach (var dcl in context.dcl())
        {
            Visit(dcl);
        }
        return defaultValue;
    }

    //VisitVarDcl
    public override ValueWrapper VisitVarDcl(LanguageParser.VarDclContext context)
    {   
    string id = context.ID().GetText();
    var value = Visit(context.expr());
    currentEnvironment.Declare(id, value, context.Start);
    return defaultValue;
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
        return new StringValue(context.STRING().GetText());
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
            (FloatValue l, FloatValue r, "!=") => new BoolValue(l.Value != r.Value),
            (StringValue l, StringValue r, "==") => new BoolValue(l.Value == r.Value),
            (StringValue l, StringValue r, "!=") => new BoolValue(l.Value != r.Value),
            (BoolValue l, BoolValue r, "==") => new BoolValue(l.Value == r.Value),
            (BoolValue l, BoolValue r, "!=") => new BoolValue(l.Value != r.Value),
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
        ValueWrapper condition = Visit(context.expr());
        if (condition is not BoolValue)
        {
            throw new SemanticError("Invalid condition", context.Start);
        }
        if (((BoolValue)condition).Value)
        {
            Visit(context.stmt(0));
        }
        else if (context.stmt().Length > 1)
        {
            Visit(context.stmt(1));
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
}