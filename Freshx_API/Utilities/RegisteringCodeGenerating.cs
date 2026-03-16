using System.Security.Cryptography;
using System.Text;

namespace Freshx_API.Utilities
{
    public static class RegisteringCodeGenerating
    {
        private static readonly string LowercaseChars = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string UppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string NumberChars = "0123456789";
        private static readonly string SpecialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";

        public static string GenerateSecureCode(int length = 6)
        {
            if (length < 6)
                throw new ArgumentException("Length must be at least 6 characters", nameof(length));

            // Ensure we have at least one of each required character type
            var code = new StringBuilder();
            code.Append(GetRandomChar(LowercaseChars)); // Lowercase
            code.Append(GetRandomChar(UppercaseChars)); // Uppercase
            code.Append(GetRandomChar(NumberChars));    // Digit
            code.Append(GetRandomChar(SpecialChars));   // Special character

            // Fill the rest with random characters from all possible characters
            string allChars = LowercaseChars + UppercaseChars + NumberChars + SpecialChars;
            for (int i = code.Length; i < length; i++)
            {
                code.Append(GetRandomChar(allChars));
            }

            // Shuffle the string to make it more random
            return new string(code.ToString().ToCharArray().OrderBy(x => GetRandomInt()).ToArray());
        }

        private static char GetRandomChar(string chars)
        {
            return chars[GetRandomInt(0, chars.Length)];
        }

        private static int GetRandomInt(int minValue = 0, int maxValue = int.MaxValue)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] data = new byte[4];
                rng.GetBytes(data);
                int value = BitConverter.ToInt32(data, 0);

                // Convert negative numbers to positive
                value = Math.Abs(value);

                return minValue + (value % (maxValue - minValue));
            }
        } 
    }
}
