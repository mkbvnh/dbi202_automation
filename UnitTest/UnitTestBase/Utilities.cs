using System.Collections.Generic;

namespace UnitTest.UnitTestBase
{
    public class Utilities
    {
        public static void SwapValues<T>(List<T> source, int index1, int index2)
        {
            var temp = source[index1];
            source[index1] = source[index2];
            source[index2] = temp;
        }
    }
}