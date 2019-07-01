using System;
using System.Collections.Generic;

namespace call_routing
{
    class Program
    {
        static void Main(string[] args)
        {
            // For testing purposes
            // Initiating a new dict containing operators
            var operatorsList =
                new Dictionary<string, Dictionary<string, string>>
                {
                    ["Operator A"] = new Dictionary<string, string>
                    {
                        {"1", "0.9"},
                        {"268", "5.1"},
                        {"46", "0.17"},
                        {"4620", "0.0"},
                        {"468", "0.15"},
                        {"4631", "0.15"},
                        {"4673", "0.9"},
                        {"46732", "1.1"}
                    },
                    ["Operator B"] = new Dictionary<string, string>
                    {
                        {"1", "0.92"},
                        {"44", "0.5"},
                        {"46", "0.2"},
                        {"467", "1.0"},
                        {"48", "1.2"}
                    }
                };

            string cheapestOperator = CallRouter.FindCheapestOperator("4673212345", operatorsList);
            Console.WriteLine("Cheapest operator is " + cheapestOperator);
        }
    }
}