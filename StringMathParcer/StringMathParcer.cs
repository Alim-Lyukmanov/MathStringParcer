using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

public class StringMathParcer
{
    public string MathString { get { return mathString; } set => mathString = value; }

    private string mathString;

    public StringMathParcer(string intputString)
    {
        mathString = intputString.Replace(".", ",");
    }

    public double Result()
    {
        var mathOperation = GetMathOperation();
        ParceCurrentOperation(mathOperation); 
        mathOperation = GetMathOperation();
        if (!string.IsNullOrEmpty(mathOperation))
            Result();
        return Double.Parse(MathString);
    }

    private void ParceCurrentOperation(string mathOperation)
    {
        var operationSymbol = Regex.Match(mathOperation, @"(\/|\*|\+|\-)").ToString();
        var numbers = Regex.Matches(mathOperation, @"\d+(,\d+)?");
        switch (operationSymbol)
        {
            case "-":
                MathString = MathString.Replace(mathOperation, Difference(mathOperation));
                break;
            case "+":
                MathString = MathString.Replace(mathOperation, Sum(mathOperation));
                break;
            case "/":
                MathString = MathString.Replace(mathOperation, Division(mathOperation));
                break;
            case "*":
                MathString = MathString.Replace(mathOperation, Multiplication(mathOperation));
                break;

        }
    }

    private string Division(MatchCollection numbers)
    {
        return numbers.Select(x => Double.Parse(x.ToString())).Aggregate((x, y) => x / y).ToString();
    }

    private string Multiplication(MatchCollection numbers)
    {
        return numbers.Select(x => Double.Parse(x.ToString())).Aggregate((x, y) => x * y).ToString();
    }

    private string Difference(MatchCollection numbers)
    {
        return numbers.Select(x => Double.Parse(x.ToString())).Aggregate((x, y) => x - y).ToString();
    }

    private string Sum(MatchCollection numbers)
    {
        return numbers.Select(x => Double.Parse(x.ToString())).Aggregate((x, y) => x + y).ToString();
    }

    private string GetMathOperation()
    {
        string mathOperation;
        try
        {
            mathOperation = Regex.Match(MathString, @"\d+(,\d+)?(\*|\/|\+|\-)\d+(,\d+)?", RegexOptions.IgnoreCase).ToString();
        }
        catch (Exception ex)
        {
            mathOperation = string.Empty;
        }
        return mathOperation;
    }

}