using System;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200073E RID: 1854
	public static class NKCServerStringFormatter
	{
		// Token: 0x06004A4F RID: 19023 RVA: 0x00164CB8 File Offset: 0x00162EB8
		public static string TranslateServerFormattedString(string targetString)
		{
			if (string.IsNullOrEmpty(targetString))
			{
				return "";
			}
			string result;
			try
			{
				string[] array = targetString.Split(NKCServerStringFormatter.Seperators, StringSplitOptions.None);
				string[] array2 = new string[array.Length - 1];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = NKCServerStringFormatter.TryPraseItemName(array[i + 1]);
				}
				string @string = NKCStringTable.GetString(array[0], true);
				object[] args = array2;
				result = string.Format(@string, args);
			}
			catch (Exception ex)
			{
				Debug.LogWarning(ex.Message);
				result = "";
			}
			return result;
		}

		// Token: 0x06004A50 RID: 19024 RVA: 0x00164D44 File Offset: 0x00162F44
		public static string TryPraseItemName(string param)
		{
			if (param.StartsWith("<MiscId>"))
			{
				string text = param.Substring("<MiscId>".Length);
				int num;
				if (!int.TryParse(text, out num))
				{
					throw new ArgumentException("MiscId TryPrase Fail string:" + text);
				}
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(num);
				if (itemMiscTempletByID == null)
				{
					throw new ArgumentException(string.Format("MiscId Invalid Id:{0}", num));
				}
				return itemMiscTempletByID.GetItemName();
			}
			else if (param.StartsWith("<EquipId>"))
			{
				string text = param.Substring("<EquipId>".Length);
				int num2;
				if (!int.TryParse(text, out num2))
				{
					throw new ArgumentException("EquipId TryPrase Fail string:" + text);
				}
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(num2);
				if (equipTemplet == null)
				{
					throw new ArgumentException(string.Format("EquipId Invalid Id:{0}", num2));
				}
				return equipTemplet.GetItemName();
			}
			else
			{
				if (!param.StartsWith("<MoldId>"))
				{
					return param;
				}
				string text = param.Substring("<MoldId>".Length);
				int num3;
				if (!int.TryParse(text, out num3))
				{
					throw new ArgumentException("TryPrase Fail string:" + text);
				}
				NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(num3);
				if (itemMoldTempletByID == null)
				{
					throw new ArgumentException(string.Format("MoldId Invalid Id:{0}", num3));
				}
				return itemMoldTempletByID.GetItemName();
			}
		}

		// Token: 0x040038EF RID: 14575
		public static readonly string[] Seperators = new string[]
		{
			"@@"
		};
	}
}
