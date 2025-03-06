using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Antlr4.Runtime; // Agrega esta línea
using analyzer; // Asegúrate de que esta línea esté presente
using Antlr4.Runtime.Tree; // Para ParseTreeWalker

namespace api.Controllers
{
    [Route("[controller]")]
    public class Compile : Controller
    {
        private readonly ILogger<Compile> _logger;

        public Compile(ILogger<Compile> logger)
        {
            _logger = logger;
        }

        public class CompileRequest
        {
            [Required]
            public required string code { get; set; }
        }

        [HttpPost]
        public IActionResult Post([FromBody] CompileRequest request )
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new {error = "Invalid request"});
            }


            var inputStream = new AntlrInputStream(request.code);
            var lexer = new LanguageLexer(inputStream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new LanguageParser(tokens);

            var tree = parser.program();

            var visitor = new CompilerVisitor();
            visitor.Visit(tree);

            return Ok(new {result = visitor.output});
            //var walker = new ParseTreeWalker();
            //var listener = new CompilerListener();
            //walker.Walk(listener, tree);

            //return Ok(new {result =listener.GetResult()});
        }
    }
}