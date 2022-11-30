using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

public class StringMathParcer
{
    public string MathString { get { return mathString; } set => mathString = value; }

    private string mathString;

    public StringMathParcer(string intputString)
    {
        mathString = intputString;
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

    private string Division(string mathOperation)
    {
        return Regex.Matches(mathOperation, @"\d+").Select(x => Double.Parse(x.ToString())).Aggregate((x, y) => x / y).ToString();
    }

    private string Multiplication(string mathOperation)
    {
        return Regex.Matches(mathOperation, @"\d+").Select(x => Double.Parse(x.ToString())).Aggregate((x, y) => x * y).ToString();
    }

    private string Difference(string mathOperation)
    {
        return Regex.Matches(mathOperation, @"\d+").Select(x => Double.Parse(x.ToString())).Aggregate((x, y) => x - y).ToString();
    }

    private string Sum(string mathOperation)
    {
        return Regex.Matches(mathOperation, @"\d+").Select(x => Double.Parse(x.ToString())).Aggregate((x, y) => x + y).ToString();
    }

    private string GetMathOperation()
    {
        string mathOperation;
        try
        {
            mathOperation = Regex.Matches(MathString, @"\d+(\*|\/|\+|\-)\d+").FirstOrDefault().ToString();
        }
        catch (Exception ex)
        {
            mathOperation = string.Empty;
        }
        return mathOperation;
    }

}