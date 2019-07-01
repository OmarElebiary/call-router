using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace call_routing
{
    public class CallRouter
    {
        // Function to receive the phone number and Dictionary containing the operators and operator rates per country code
        public static string FindCheapestOperator(string phoneNumber, Dictionary<string, Dictionary<string, string>> operatorsList)
        {
            // Check if the phoneNumber is null or empty or contains any non numeric chars
            if (string.IsNullOrEmpty(phoneNumber) || !Regex.IsMatch(phoneNumber, @"^\d+$")) return string.Empty;
            string currentCheapestOperator = string.Empty;
            float currentCallRate = float.MaxValue;

            foreach (KeyValuePair<string,Dictionary<string,string>> callOperator in operatorsList)
            {
                // Find all country codes that start with the same number instead of iterating over the whole country codes list
                var operatorRates = callOperator.Value.Where(s => s.Key.StartsWith(phoneNumber.ElementAt(0)));
                var keyValuePairs = operatorRates as KeyValuePair<string, string>[] ?? operatorRates.ToArray();
                if (!keyValuePairs.Any()) continue;

                // Finding the longest country code that matches the given number
                // I used this way because I don't know exactly the country code length in a given number
                int currentSubstringMax = FindAccurateKey(phoneNumber, keyValuePairs);
                if (!callOperator.Value.ContainsKey(phoneNumber.Substring(0, currentSubstringMax))) continue;
                float callRate = float.Parse(callOperator.Value[phoneNumber.Substring(0, currentSubstringMax)]);
                if (!(callRate <= currentCallRate)) continue;
                currentCallRate = callRate;
                currentCheapestOperator = callOperator.Key;
            }
            return currentCheapestOperator;
        }

        // This function is used to find the country code that is used in a given phone number
        private static int FindAccurateKey(string phoneNumber, IEnumerable<KeyValuePair<string, string>> operatorRates)
        {
            int count = 0, maxLen = 0;
            foreach (KeyValuePair<string,string> rate in operatorRates)
            {
                int lenA = phoneNumber.Length, lenB = rate.Key.Length;
                if (lenA == 0 || lenB == 0) return count;
                count = LongestCommonKey(lenA, lenB, 0, phoneNumber, rate.Key);
                if (count >= maxLen) maxLen = count;
            }

            return maxLen;
        }

        // A function used to find the longest common substring in the phone number which matches the country code in an operator list and return the length of the country code
        private static int LongestCommonKey(int lenA, int lenB, int count, string phoneNumber, string callKey)
        {
            if (lenA == 0 || lenB == 0) return count;
            if (phoneNumber[lenA - 1] == callKey[lenB - 1])
            {
                count = LongestCommonKey(lenA - 1, lenB - 1, count + 1, phoneNumber, callKey);
            }

            count = Math.Max(count,
                Math.Max(LongestCommonKey(lenA, lenB - 1, 0, phoneNumber, callKey),
                    LongestCommonKey(lenA - 1, lenB, 0, phoneNumber, callKey)));
            return count;
        }
    }
}