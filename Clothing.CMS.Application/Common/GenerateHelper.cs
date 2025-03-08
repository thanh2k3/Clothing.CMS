namespace Clothing.CMS.Application.Common
{
    public static class GenerateHelper
    {
		public static string GenerateNextOrderCode(string lastCode)
		{
			// Tách phần chữ và số
			string letterPart = lastCode.Substring(0, 4);
			int numberPart = int.Parse(lastCode.Substring(4));

			// Tăng số, nếu quá 9999 thì reset về 0001 và tăng phần chữ
			numberPart++;
			if (numberPart > 9999)
			{
				numberPart = 1; // Reset về 0001
				letterPart = IncrementLetters(letterPart);
			}

			return $"{letterPart}{numberPart:D4}";
		}

		private static string IncrementLetters(string letters)
		{
			char[] charArray = letters.ToCharArray();

			for (int i = 3; i >= 0; i--) // Tăng chữ từ phải sang trái
			{
				if (charArray[i] < 'Z')
				{
					charArray[i]++;
					return new string(charArray);
				}
				charArray[i] = 'A';
			}

			return new string(charArray);
		}
	}
}
