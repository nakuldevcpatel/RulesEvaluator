using Console.App.Abstracts;
using Console.App.Grammar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace Console.App.Implementation
{
    public class RuleSetCompiler : IRuleCompiler
    {
        protected IJsonParser jsonParser;
        public RuleSetCompiler(IJsonParser jsonParser)
        {
            this.jsonParser = jsonParser;
        }

        public IRuleSet GetCompiledRule(string rule)
        {
            AntlrInputStream input = new AntlrInputStream(rule);
            RuleSetLexer lexer = new RuleSetLexer(input);
            var tokens = new CommonTokenStream(lexer);
            RuleSetParser parser = new RuleSetParser(tokens);

            RuleSetTreeBuilder ruleSetTreeBuilder = new RuleSetTreeBuilder(jsonParser);

            parser.AddParseListener(ruleSetTreeBuilder);
            parser.ErrorHandler = new ExceptionThrowingErrorHandler();
            parser.rule_set();

            return ruleSetTreeBuilder.ruleSet;
        }

        public String GetParseTreeAsString(String rule)
        {
            AntlrInputStream input = new AntlrInputStream(rule);
            RuleSetLexer lexer = new RuleSetLexer(input);
            var tokens = new CommonTokenStream(lexer);
            RuleSetParser parser = new RuleSetParser(tokens);

            return parser.rule_set().ToStringTree(parser);
        }
    }
}
