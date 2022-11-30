using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

public class StringMathParcer
{
    public string MathString;

    public StringMathParcer(string intputString)
    {
        MathString = intputString;
    }

    public int Result()
    {
        var mathOperation = Regex.Matches(MathString, @"\d+(\+\-)").FirstOrDefault().ToString();
        do
        {
            var operationSymbol = Regex.Match(mathOperation, @"(\+|\-)").ToString();
            switch (operationSymbol)
            {
                case "-":
                    MathString = MathString.Replace(mathOperation, Minus(mathOperation));
                    break;
                case "+":
                    MathString = MathString.Replace(mathOperation, Plus(mathOperation));
                    break;
            }
            mathOperation = Regex.Matches(MathString, @"\d+(\+\-)").FirstOrDefault().ToString();
        } while (!string.IsNullOrEmpty(mathOperation));

        return 1;
    }

    private string Minus(string mathOperation)
    {
        return Regex.Matches(mathOperation, @"\d+").Select(x => Int32.Parse(x.ToString())).Aggregate((x, y) => x - y).ToString();
    }

    private string Plus(string mathOperation)
    {
        return Regex.Matches(mathOperation, @"\d+").Select(x => Int32.Parse(x.ToString())).Aggregate((x, y) => x + y).ToString();
    }

}