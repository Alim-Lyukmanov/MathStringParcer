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
        operation = Regex.Match(strMathOperation, @"(\/|\*|\+|\-)").ToString();
    }

    public MathOperation(object first, object operation, object second)
    {
        if (first == null || second == null || operation == null) 
            throw new ArgumentNullException(nameof(first));
        this.first = double.Parse(first.ToString());
        this.second = double.Parse(second.ToString());
        this.operation = operation.ToString();
    }

    public double Result()
    {
        double result = 0;
        switch (operation)
        {
            case "-":
                result = Difference();
                break;
            case "+":
                result = Sum();
                break;
            case "/":
                result = Division();
                break;
            case "*":
                result = Multiplication();
                break;
        }
        return result;
    }

    private double Division()
    {
        if (second == 0)
            throw new Exception("Can not divide by 0!!!");
        return first/second;
    }

    private double Multiplication()
    {
        return first * second;
    }

    private double Difference()
    {
        return first-second;
    }

    private double Sum()
    {
        return first+second;
    }

    ~MathOperation()
    { }

}
