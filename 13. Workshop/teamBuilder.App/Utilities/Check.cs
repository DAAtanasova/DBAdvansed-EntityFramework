using System;
using System.Collections.Generic;
using System.Text;

namespace TeamBuilder.App.Utilities
{
    public static class Check
    {
        public static void CheckLenght(int expectedLenght, string[] array)
        {
            if(expectedLenght != array.Length)
            {
                throw new FormatException(Constants.ErrorMessages.InvalidArgumentsCount);
            }
        }

        public static void CheckCreateTeamCommandLength(int shortLength, int longLengt, string[] array)
        {
            if(array.Length != shortLength && array.Length != longLengt)
            {
                throw new FormatException(Constants.ErrorMessages.InvalidArgumentsCount);
            }
        }
    }
}
