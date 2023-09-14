// See https://aka.ms/new-console-template for more information


StringMathParcer p;
string operations;
do
{
    operations = Console.ReadLine();
    if (operations == null)
        break;
    p = new StringMathParcer(operations);
    Console.WriteLine(p.Result());
}while(operations != null);
Console.WriteLine("Обрабокта завершена");
Console.ReadKey();
