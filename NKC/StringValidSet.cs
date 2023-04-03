using System;
using System.Collections.Generic;
using NKM;

namespace NKC
{
	// Token: 0x02000676 RID: 1654
	public static class StringValidSet
	{
		// Token: 0x060034DC RID: 13532 RVA: 0x0010B0F8 File Offset: 0x001092F8
		public static void Init()
		{
			for (int i = 0; i <= 64; i++)
			{
				StringValidSet.ignoreSet.Add(i);
			}
			for (int j = 91; j <= 96; j++)
			{
				StringValidSet.ignoreSet.Add(j);
			}
			for (int k = 123; k <= 127; k++)
			{
				StringValidSet.ignoreSet.Add(k);
			}
		}

		// Token: 0x060034DD RID: 13533 RVA: 0x0010B152 File Offset: 0x00109352
		public static bool Ignore(char ch)
		{
			return StringValidSet.ignoreSet.Contains((int)ch);
		}

		// Token: 0x060034DE RID: 13534 RVA: 0x0010B160 File Offset: 0x00109360
		public static bool Valid(char ch)
		{
			if (NKMContentsVersionManager.HasCountryTag(CountryTagType.KOR))
			{
				return StringValidSet.ValidEnglish(ch) || StringValidSet.ValidHangle(ch) || StringValidSet.ValidArabicNumerals(ch);
			}
			return !NKMContentsVersionManager.HasCountryTag(CountryTagType.CHN) || StringValidSet.ValidEnglish(ch) || StringValidSet.ValidChineseChar(ch) || StringValidSet.ValidArabicNumerals(ch);
		}

		// Token: 0x060034DF RID: 13535 RVA: 0x0010B1BE File Offset: 0x001093BE
		private static bool ValidChineseChar(char ch)
		{
			return ch >= '一' && ch <= '龥';
		}

		// Token: 0x060034E0 RID: 13536 RVA: 0x0010B1D5 File Offset: 0x001093D5
		private static bool ValidHangle(char ch)
		{
			return '가' <= ch && ch <= '힣';
		}

		// Token: 0x060034E1 RID: 13537 RVA: 0x0010B1EA File Offset: 0x001093EA
		private static bool ValidEnglish(char ch)
		{
			return ('A' <= ch && ch <= 'Z') || ('a' <= ch && ch <= 'z');
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x0010B205 File Offset: 0x00109405
		private static bool ValidArabicNumerals(char ch)
		{
			return '0' <= ch && ch <= '9';
		}

		// Token: 0x060034E3 RID: 13539 RVA: 0x0010B214 File Offset: 0x00109414
		public static bool CheckIgnoreSet(char ch)
		{
			return StringValidSet.Ignore(ch);
		}

		// Token: 0x040032E9 RID: 13033
		private static HashSet<int> ignoreSet = new HashSet<int>();
	}
}
