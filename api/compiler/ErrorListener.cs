using System;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

public class SemanticError : Exception
{
    private string message;
    private IToken token;

    public SemanticError(string message, IToken token)
    {
        this.message = message;
        this.token = token;
    }

    public override string Message{
        get{
            return message + " en linea " + token.Line + " y columna " + token.Column;
        }
    }
}

public class LexicalErrorListener : BaseErrorListener, IAntlrErrorListener<int>
{
    public void SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    {
        Console.WriteLine($"Error léxico en línea {line}, columna {charPositionInLine}: {msg}");
        throw new ParseCanceledException($"Error léxico en línea {line}:{charPositionInLine} - {msg}");
    }
}

public class SyntaxErrorListener : BaseErrorListener
{
    public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    {
        Console.WriteLine($"Error de sintaxis en línea {line}, columna {charPositionInLine}: {msg}");
        throw new ParseCanceledException($"Error sintáctico en línea {line}:{charPositionInLine} - {msg}");
    }
}