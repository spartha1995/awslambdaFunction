using AWSLambda1;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Function function = new Function();
            function.FunctionHandler();
        }
    }
}
