using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.App.Implementation
{
    public class ExceptionThrowingErrorHandler : DefaultErrorStrategy
    {
        public override void Recover(Parser recognizer, RecognitionException e)
        {
            throw new Exception("Error!",e);
        }
        public override IToken RecoverInline(Parser recognizer)
        {
            throw new Exception("Argument Mismatch!", new InputMismatchException(recognizer));
        }
        public override void Sync(Parser recognizer) 
        {
        }

    }
}
