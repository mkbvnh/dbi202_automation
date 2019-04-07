namespace DBI_Grading.Utils
{
    internal static class StringUtils
    {
        internal static string GetNumbers(this string input)
        {
            while (input.Length > 0 && !char.IsDigit(input[input.Length - 1]))
                input = input.RemoveAt(input.Length - 1);
            var position = input.Length - 1;
            if (position == -1)
                return input;
            while (position != -1)
            {
                position--;
                if (position == -1) break;
                if (!char.IsNumber(input[position]))
                    break;
            }

            return position == -1 ? input : input.Remove(0, position + 1);
        }

        internal static string RemoveAt(this string s, int index)
        {
            return s.Remove(index, 1);
        }
    }
}