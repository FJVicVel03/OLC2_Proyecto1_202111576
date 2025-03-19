using analyzer;

public class ForeignFunction : Invocable
{
    private Environment clousure;
    private LanguageParser.FuncDclContext context;
    private static bool mainExecuted = false; // Bandera para verificar si `main` fue ejecutada

    public ForeignFunction(Environment clousure, LanguageParser.FuncDclContext context)
    {
        this.clousure = clousure;
        this.context = context;
    }

    public int Arity()
    {
        return context.@params()?.ID().Length ?? 0; 
    }

    public ValueWrapper Invoke(List<ValueWrapper> args, CompilerVisitor visitor)
    {
        // Verificar si `main` ya fue ejecutada
        if (!mainExecuted && context.ID().GetText() != "main")
        {
            throw new SemanticError("Cannot execute functions before 'main' is executed", context.Start);
        }

        // Si es `main`, marcar como ejecutada
        if (context.ID().GetText() == "main")
        {
            mainExecuted = true;
        }

        var newEnv = new Environment(clousure);
        var beforeCallEnv = visitor.currentEnvironment;
        visitor.currentEnvironment = newEnv;

        if (context.@params() != null)
        {
            for (int i = 0; i < context.@params().ID().Length; i++)
            {
                newEnv.Declare(context.@params().ID(i).GetText(), args[i], null);
            }
        }
        try
        {
            foreach (var stmt in context.dcl())
            {
                visitor.Visit(stmt);
            }
        }
        catch (ReturnException e)
        {
            visitor.currentEnvironment = beforeCallEnv;
            return e.Value; 
        }
        finally
        {
            // Restaurar el entorno anterior
            visitor.currentEnvironment = beforeCallEnv;

            // Propagar la salida acumulada
            //visitor.output += visitor.output; // Asegurarse de que la salida se acumule correctamente
        }

        return visitor.defaultValue;
    }

    public ForeignFunction Bind(Instance instance){
        var hiddenEnv = new Environment(clousure);
        hiddenEnv.Declare("this", new InstanceValue(instance), null);
        return new ForeignFunction(hiddenEnv, context);
    }
}
