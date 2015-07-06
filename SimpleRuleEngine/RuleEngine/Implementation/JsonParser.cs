using RuleEngine.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleEngine.Implementation
{
    public class JsonParser : IJsonParser
    {

        public object queryObject(string jsonData, string jsonPathExpression)
        {
            var jsonObject = JObject.Parse(jsonData);
            JToken token = jsonObject.SelectToken(jsonPathExpression);
            return token;
        }

        public string query(string jsonData, string jsonPathExpression)
        {
            var token = queryObject(jsonData, jsonPathExpression);
            return token.ToString();
        }

        public List<string> queryArray(string jsonData, string jsonPathExpression)
        {
            var jObject = JObject.Parse(jsonData);
            var tokens = jObject.SelectTokens(jsonPathExpression);
            return tokens.Select(s => (string)s).ToList();

        }
    }
}
