using System;
using System.Collections.Generic;

namespace NLua.Extensions
{
	// Token: 0x02000079 RID: 121
	internal static class StringExtensions
	{
		// Token: 0x06000443 RID: 1091 RVA: 0x0001496F File Offset: 0x00012B6F
		public static IEnumerable<string> SplitWithEscape(this string input, char separator, char escapeCharacter)
		{
			int num = 0;
			int index = 0;
			while (index < input.Length)
			{
				index = input.IndexOf(separator, index);
				if (index == -1)
				{
					break;
				}
				if (input[index - 1] == escapeCharacter)
				{
					input = input.Remove(index - 1, 1);
				}
				else
				{
					yield return input.Substring(num, index - num);
					int num2 = index;
					index = num2 + 1;
					num = index;
				}
			}
			yield return input.Substring(num);
			yield break;
		}
	}
}
