using System.Collections.Generic;
using call_routing;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        // Dummy data for testing
        private static readonly Dictionary<string, Dictionary<string, string>> OperatorsList =
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

        [Test]
        public void TestingNumberWithKnownResult()
        {
            string cheapestOperator = CallRouter.FindCheapestOperator("4673212345", OperatorsList);
            Assert.AreEqual("Operator B", cheapestOperator);
        }

        [Test]
        public void TestingNumberWithKnownResult2()
        {
            string cheapestOperator = CallRouter.FindCheapestOperator("4603212345", OperatorsList);
            Assert.AreEqual("Operator A", cheapestOperator);
        }

        [Test]
        public void TestingInvalidNumber()
        {
            string cheapestOperator = CallRouter.FindCheapestOperator("054as4dsf23das", OperatorsList);
            Assert.AreEqual(string.Empty, cheapestOperator);
        }

        [Test]
        public void TestingNumberThatDoesntExistInAnyOperator()
        {
            string cheapestOperator = CallRouter.FindCheapestOperator("023456565748487", OperatorsList);
            Assert.AreEqual(string.Empty, cheapestOperator);
        }

        [Test]
        public void TestingNumberContainingChars()
        {
            string cheapestOperator = CallRouter.FindCheapestOperator("+12dfs23das", OperatorsList);
            Assert.AreEqual(string.Empty, cheapestOperator);
        }
    }
}