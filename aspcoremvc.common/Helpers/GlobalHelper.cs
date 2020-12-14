using Common.Enums;
using System;
using System.CodeDom.Compiler;
using System.Text.RegularExpressions;

namespace Common.Helpers
{
    public class GlobalHelper
    {
        public static string StringReplace(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            text = text.ToLower();

            text = Regex.Replace(text, "İ", "I");
            text = Regex.Replace(text, "ı", "i");
            text = Regex.Replace(text, "Ğ", "G");
            text = Regex.Replace(text, "ğ", "g");
            text = Regex.Replace(text, "Ö", "O");
            text = Regex.Replace(text, "ö", "o");
            text = Regex.Replace(text, "Ü", "U");
            text = Regex.Replace(text, "ü", "u");
            text = Regex.Replace(text, "Ş", "S");
            text = Regex.Replace(text, "ş", "s");
            text = Regex.Replace(text, "Ç", "C");
            text = Regex.Replace(text, "ç", "c");
            text = Regex.Replace(text, " ", "-");
            return text;
        }

        public static string GeneratePassword(int length = 8)
        {
            return Guid.NewGuid().ToString("d").Substring(1, length);
        }

        public static string MaskText(string text, MaskType type)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            var result = string.Empty;
            var textArray = text.Split(" ");
            var rd = new Random();
            if (type == MaskType.Text)
            {
                for (int i = 0; i < textArray.Length; i++)
                {

                    var t = textArray[i].Trim();
                    if (t.Length < 2) break;
                    var count = rd.Next(2, 3);
                    var mask = string.Empty;

                    //if (left)
                    //{
                        mask = t.Substring(count, t.Length - count);
                        textArray[i] = t.Replace(mask, "").PadRight(rd.Next(3, 8), '*');
                    //}
                    //else
                    //{
                    //    mask = t.Substring(t.Length - count, t.Length - (t.Length - count));
                    //    textArray[i] = t.Replace(mask, "").PadRight(rd.Next(3, 8), '*');
                    //}

                    result = string.Join(' ', textArray);
                }
                result = string.Join(' ', textArray);
            }
            else if (type == MaskType.Telephone)
            {
                var mask = text.Substring(text.Length - 4, text.Length - (text.Length - 4));
                result = text.Replace(mask, "").PadRight(rd.Next(1, 5), '*');
            }
            else if (type == MaskType.IdentityNumber)
            {
                var mask = text.Substring(2, (text.Length - 6));
                result = text.Replace(mask, "".PadRight(mask.Length, '*'));
            }
            else
            {
                result = text;
            }

            return result;
        }


    }
}
