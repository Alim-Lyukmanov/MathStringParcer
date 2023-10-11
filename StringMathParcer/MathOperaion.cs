using System;
using System.Text.RegularExpressions;

public class MathOperation
{
	private double first;
	private double second;
	private string operation;


	public double First { get=>first; set=>first = value; }
	public double Second { get =>second; set=>second = value; }
	public string Operation { get => operation; set => operation = value; }

	public MathOperation(string strMathOperation)
	{
		first = double.Parse(Regex.Matches(strMathOperation, @"\d+")[0].ToString());
        second = double.Parse(Regex.Matches(strMathOperation, @"\d+")[1].ToString());
        operationSymbol = Regex.Match(strMathOperation, @"(\/|\*|\+|\-)").ToString()
    }

    private double Division()
    {
        return first/second;
    }

    private int Multiplication()
    {
        return first * second;
    }

    private int Difference()
    {
        return first-second;
    }

    private int Sum()
    {
        return first+second;
    }



}
