using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;



public class StringMathParcer
{
    private List<object> mathList;

    private string originalInput;

    private Dictionary<string, List<string>> operations = new Dictionary<string, List<string>> 
    { 
        { 
            "first", new List<string> { "*", "/" } 
        },
        {
            "second", new List<string>{"+", "-"}
        }
    };


    public StringMathParcer(string inputString)
    {
        originalInput = inputString.Replace(".", ",");
        mathList = ConvertStringToList(originalInput);
    }

    public object Result()
    {
        object res;
        try
        {
            if (!BraketsChecker())
                throw new Exception("Incorrect brakets");
            if (mathList.Count == 0)
                throw new Exception("Empty input");
            res = ProcessOperations();
        }
        catch (Exception ex)
        {
            res = ex.Message;
        }
        return res;
    }

    private object ProcessOperations()
    {
        MathOperation mOp;
        object res = 0;
        var searchingOperations = operations["first"];
        while (mathList.Intersect(operations.SelectMany(x=>x.Value)).Any())
        {
            if (!mathList.Intersect(operations["first"]).Any())
                searchingOperations = operations["second"];
            var index = mathList.Where(x => searchingOperations.Contains(x)).Select(x => mathList.IndexOf(x)).FirstOrDefault();
            if (index == 0)
            {
                res = mathList[index].ToString() + mathList[index + 1].ToString();
                mathList.RemoveRange(index, 2);
                mathList.Insert(index, res);
            }
            else
            {
                mOp = new MathOperation(mathList[index - 1], mathList[index], mathList[index + 1]);
                mathList.RemoveRange(index - 1, 3);
                res = mOp.Result();
                mathList.Insert(index - 1, mOp.Result());
            }
        }
        return res;
    }

    private List<object> ConvertStringToList(string intputString)
    {
        var list = new List<object>();
        for (int i = 0; i < intputString.Length;i++)
        {
            var sString = intputString[i].ToString();
            int counter = 1;
            while (i+counter < intputString.Length)
            {
                if (Regex.Matches(intputString[i + counter].ToString(), @"(\-|\+|\*|\/|\(|\))").Count()>0|| Regex.Matches(intputString[i].ToString(), @"(\-|\+|\*|\/|\(|\))").Count()>0)
                    break;
                sString += intputString[i+counter].ToString();
                counter++;
            }
            list.Add(sString);
            i += counter-1;
        }
        return list;
    }

    private bool BraketsChecker()
    {
        var stack = new Stack<string>();
        var brackets = Regex.Matches(originalInput, @"(\(|\))").Select(x => x.ToString());
        if (brackets.Count() == 0)
            return true;
        foreach (var m in brackets)
        {
            if (stack.Count == 0||m=="(")
            {
                stack.Push(m);
                continue;
            }
            var last = stack.Peek();
            if (last == "(" && m == ")")
            {
                stack.Pop();
            }
        }
        return stack.Count == 0;
    }

}