using System;
using System.Globalization;
using System.Text.RegularExpressions;
using NKM;

namespace NKC
{
	// Token: 0x02000678 RID: 1656
	public class NKCFilterManager
	{
		// Token: 0x060034F0 RID: 13552 RVA: 0x0010B4E8 File Offset: 0x001096E8
		public static bool LoadFromLua()
		{
			bool result = true;
			NKCFilterManager.LoadFromLua("LUA_BAD_CHAT_FILTER_TEMPLET", "BAD_CHAT_FILTER_TEMPLET", ref NKCFilterManager.cFilter);
			NKCFilterManager.LoadFromLua("LUA_BAD_GUILD_NAME_FILTER_TEMPLET", "BAD_GUILD_NAME_FILTER_TEMPLET", ref NKCFilterManager.cGuildnameFilter);
			return result;
		}

		// Token: 0x060034F1 RID: 13553 RVA: 0x0010B514 File Offset: 0x00109714
		private static void LoadFromLua(string scriptName, string tableName, ref CFilter targetFilter)
		{
			NKMLua nkmlua = new NKMLua();
			if (nkmlua.LoadCommonPath("AB_SCRIPT", scriptName, true) && nkmlua.OpenTable(tableName))
			{
				int num = 1;
				while (nkmlua.OpenTable(num))
				{
					if (!NKMContentsVersionManager.CheckContentsVersion(nkmlua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCFilterManager.cs", 369))
					{
						num++;
						nkmlua.CloseTable();
					}
					else
					{
						string text = "";
						nkmlua.GetData("WORD", ref text);
						if (!string.IsNullOrEmpty(text))
						{
							targetFilter.AddFilterString(text);
						}
						num++;
						nkmlua.CloseTable();
					}
				}
			}
		}

		// Token: 0x060034F2 RID: 13554 RVA: 0x0010B59A File Offset: 0x0010979A
		public static string RemoveEmoji(string str)
		{
			return Regex.Replace(str, "\\p{Cs}|\\p{So}", "");
		}

		// Token: 0x060034F3 RID: 13555 RVA: 0x0010B5AC File Offset: 0x001097AC
		public static string CheckBadChat(string inputStr)
		{
			return NKCFilterManager.RemoveEmoji(NKCFilterManager.cFilter.Filter(inputStr));
		}

		// Token: 0x060034F4 RID: 13556 RVA: 0x0010B5BE File Offset: 0x001097BE
		public static bool CheckBadGuildname(string inputStr)
		{
			return NKCFilterManager.cGuildnameFilter.CheckNickNameFilter(inputStr.ToCharArray());
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x0010B5D0 File Offset: 0x001097D0
		public static bool CheckNickNameFilter(string data)
		{
			return NKCFilterManager.cFilter.CheckNickNameFilter(data.ToCharArray());
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x0010B5E4 File Offset: 0x001097E4
		public static char FilterEmojiInput(string text, int charIndex, char addedChar)
		{
			UnicodeCategory unicodeCategory = char.GetUnicodeCategory(addedChar);
			if (unicodeCategory == UnicodeCategory.Surrogate || unicodeCategory == UnicodeCategory.OtherSymbol)
			{
				return '\0';
			}
			return addedChar;
		}

		// Token: 0x040032ED RID: 13037
		public static CFilter cFilter = new CFilter();

		// Token: 0x040032EE RID: 13038
		public static CFilter cGuildnameFilter = new CFilter();

		// Token: 0x040032EF RID: 13039
		private const string EMOJI_REMOVE_REGEX = "\\p{Cs}|\\p{So}";
	}
}
