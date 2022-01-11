using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public static class StringExtensions
{

    public static string StripWhitespace(this string str)
    {
        return Regex.Replace(str, @"\s+", "");
    }

    public static string WrapText(this string str, int charCount)
    {
        string[] originalLines = str.Split(new string[] { " " }, StringSplitOptions.None);

        List<string> wrappedLines = new List<string>();

        StringBuilder actualLine = new StringBuilder();
        double actualWidth = 0;

        foreach (var item in originalLines)
        {
            actualWidth += item.Count() + 1;

            if (actualWidth > charCount)
            {
                wrappedLines.Add(actualLine.ToString());
                actualLine.Clear();
                actualWidth = item.Count() + 1;
            }
            actualLine.Append(item + " ");
        }

        if (actualLine.Length > 0)
        {
            wrappedLines.Add(actualLine.ToString());
        }

        return string.Join("\r\n", wrappedLines);
    }

    public static string FormatRawText(this string rawText, int charCount, char[] charsToSplitOn)
    {
        string[] words = rawText.Explode(charsToSplitOn);

        int curLineLength = 0;
        StringBuilder strBuilder = new StringBuilder();
        for (int i = 0; i < words.Length; i += 1)
        {
            string word = words[i];
            // If adding the new word to the current line would be too long,
            // then put it on a new line (and split it up if it's too long).
            if (curLineLength + word.Length > charCount)
            {
                // Only move down to a new line if we have text on the current line.
                // Avoids situation where wrapped whitespace causes emptylines in text.
                if (curLineLength > 0)
                {
                    strBuilder.Append(Environment.NewLine);
                    curLineLength = 0;
                }

                // If the current word is too long to fit on a line even on it's own then
                // split the word up.
                while (word.Length > charCount)
                {
                    strBuilder.Append(word.Substring(0, charCount - 1) + "-");
                    word = word.Substring(charCount - 1);

                    strBuilder.Append(Environment.NewLine);
                }

                // Remove leading whitespace from the word so the new line starts flush to the left.
                word = word.TrimStart();
            }
            strBuilder.Append(word);
            curLineLength += word.Length;
        }

        return strBuilder.ToString();
    }

    public static string[] Explode(this string str, char[] charsToSplitOn)
    {
        List<string> parts = new List<string>();
        int startIndex = 0;
        while (true)
        {
            int index = str.IndexOfAny(charsToSplitOn, startIndex);

            if (index == -1)
            {
                parts.Add(str.Substring(startIndex));
                return parts.ToArray();
            }

            string word = str.Substring(startIndex, index - startIndex);
            char nextChar = str.Substring(index, 1)[0];
            // Dashes and the likes should stick to the word occuring before it. Whitespace doesn't have to.
            if (char.IsWhiteSpace(nextChar))
            {
                parts.Add(word);
                parts.Add(nextChar.ToString());
            }
            else
            {
                parts.Add(word + nextChar);
            }

            startIndex = index + 1;
        }
    }

    public static bool Contains(this string source, string target, StringComparison comparison)
    {
        int index = source.IndexOf(target, comparison);
        return index >= 0;
    }
}