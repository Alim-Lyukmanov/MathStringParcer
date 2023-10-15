using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;



public class StringMathParcer
{
    private List<object> mathList;

    private string[] operations = {"+","-","*","/"};

    public StringMathParcer(string intputString)
    {
        intputString = intputString.Replace(".", ",");
        mathList = ConvertStringToList(intputString);
    }

    

    public object Result()
    {
        object res;
        try
        {
            res = ProcessOperations();
        }
        catch (Exception e)
        {
            res = "Incorrect input";
        }
        return res;
    }

    private object ProcessOperations()
    {
        MathOperation mOp;
        object res = 0;
        while (mathList.Intersect(operations).Any())
        {
            var index = mathList.Where(x => operations.Contains(x)).Select(x => mathList.IndexOf(x)).FirstOrDefault();
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
                if (Regex.Matches(intputString[i + counter].ToString(), @"(\-|\+|\*|\/)").Count()>0|| Regex.Matches(intputString[i].ToString(), @"(\-|\+|\*|\/)").Count()>0)
                    break;
                sString += intputString[i+counter].ToString();
                counter++;
            }
            list.Add(sString);
            i += counter-1;
        }
        return list;
    }

}