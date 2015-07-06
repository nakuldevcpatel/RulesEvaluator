using Console.App.Implementation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        private static readonly RuleSetCompiler rulesetCompiler = new RuleSetCompiler(new JsonParser());
        static void Main(string[] args)
        {
            var model = new SomeDomainModel()
            {
                Area = 10,
                Code = "BLR,KRA",
                Status = "approved"
            };
            var txnObj = new TxnObject()
            {
                OutletId = 1,
                TxnAmount = 10,
                Model = model
            };
            string jsonData = JsonConvert.SerializeObject(txnObj);
            string rule = "$.Model.Area equals $.TxnAmount";
            //string rule = "Area greater than 7 and $.Area less than 10\nStatus equals approved";
            //string rule = "TxnAmount greater than (5+1) and OutletId equals 1";
            
            //TODO:
            //string outcome = "$.Area = $.Area + $.Area * 0.02";
            
            bool expectedResult = true;
            System.Console.WriteLine("Data:\n{0}",jsonData);
            System.Console.WriteLine("---------");
            System.Console.WriteLine("Rule(s):\n{0}",rule);
            System.Console.WriteLine("---------");
            
            //System.Console.WriteLine(rulesetCompiler.GetParseTreeAsString(rule));
            var result = rulesetCompiler.GetCompiledRule(rule).isSatisfiedBy(jsonData);
            
            System.Console.WriteLine("Result: {0} - Expected: {1}",result,expectedResult);
            System.Console.ReadLine();

            
        }

    }
    class SomeDomainModel
    {
        public int Area;
        public string Code;
        public string Status;
    }
    class TxnObject
    {
        public int OutletId;
        public int TxnAmount;
        public SomeDomainModel Model;
    }
}
