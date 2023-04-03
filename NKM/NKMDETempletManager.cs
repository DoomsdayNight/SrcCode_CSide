using System;
using System.Collections.Generic;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x020003CB RID: 971
	public class NKMDETempletManager
	{
		// Token: 0x060019B3 RID: 6579 RVA: 0x0006D913 File Offset: 0x0006BB13
		private NKMDETempletManager()
		{
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x0006D91C File Offset: 0x0006BB1C
		public static bool LoadFromLUA(List<string> listFileName, bool bReload)
		{
			bool result = true;
			foreach (string fileName in listFileName)
			{
				NKMDETempletManager.LoadFromLUA(fileName, bReload);
			}
			return result;
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x0006D96C File Offset: 0x0006BB6C
		private static void LoadFromLUA(string fileName, bool bReload)
		{
			using (NKMLua nkmlua = new NKMLua())
			{
				if (nkmlua.LoadCommonPath("AB_SCRIPT_EFFECT", fileName, true) && nkmlua.OpenTable("m_dicNKMDamageEffectTemplet"))
				{
					int num = 1;
					while (nkmlua.OpenTable(num))
					{
						NKMDamageEffectTemplet nkmdamageEffectTemplet = new NKMDamageEffectTemplet();
						string text = "";
						if (nkmlua.GetData("m_BASE_ID", ref text) && text.Length > 1)
						{
							if (NKMDETempletManager.m_dicNKMDamageEffectTemplet.ContainsKey(text))
							{
								nkmdamageEffectTemplet.DeepCopyFromSource(NKMDETempletManager.m_dicNKMDamageEffectTemplet[text]);
								nkmdamageEffectTemplet.LoadFromLUA(nkmlua);
							}
							else
							{
								NKMTempletError.Add("NKMDETempletManager LoadFromLUA m_BASE_ID dont exist m_DamageEffectID: " + nkmdamageEffectTemplet.m_DamageEffectID + ", m_BASE_ID: " + text, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDamageEffectTemplet.cs", 509);
							}
						}
						else
						{
							nkmdamageEffectTemplet.LoadFromLUA(nkmlua);
						}
						if (!NKMDETempletManager.m_dicNKMDamageEffectTemplet.ContainsKey(nkmdamageEffectTemplet.m_DamageEffectID))
						{
							NKMDETempletManager.m_dicNKMDamageEffectTemplet.Add(nkmdamageEffectTemplet.m_DamageEffectID, nkmdamageEffectTemplet);
						}
						else if (bReload)
						{
							NKMDETempletManager.m_dicNKMDamageEffectTemplet[nkmdamageEffectTemplet.m_DamageEffectID].DeepCopyFromSource(nkmdamageEffectTemplet);
						}
						else
						{
							NKMTempletError.Add("NKMDETempletManager LoadFromLUA duplicate TempletID m_DamageEffectID: " + nkmdamageEffectTemplet.m_DamageEffectID, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDamageEffectTemplet.cs", 528);
						}
						num++;
						nkmlua.CloseTable();
					}
				}
			}
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x0006DAC0 File Offset: 0x0006BCC0
		public static NKMDamageEffectTemplet GetDETemplet(string deID)
		{
			if (NKMDETempletManager.m_dicNKMDamageEffectTemplet.ContainsKey(deID))
			{
				return NKMDETempletManager.m_dicNKMDamageEffectTemplet[deID];
			}
			NKMTempletError.Add("GetDETemplet null: " + deID, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDamageEffectTemplet.cs", 545);
			return null;
		}

		// Token: 0x04001263 RID: 4707
		public static Dictionary<string, NKMDamageEffectTemplet> m_dicNKMDamageEffectTemplet = new Dictionary<string, NKMDamageEffectTemplet>();
	}
}
