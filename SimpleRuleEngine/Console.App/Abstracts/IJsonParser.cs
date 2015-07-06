using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.App.Abstracts
{
    public interface IJsonParser
    {
        Object queryObject(string jsonData, string jsonPathExpression);
        string query(string jsonData, string jsonPathExpression);
        List<string> queryArray(string jsonData, string jsonPathExpression);
    }
}
