using System;
using System.Collections.Generic;
using System.Linq;
using NKM;
using NKM.Templet.Base;

namespace NKC.Templet
{
	// Token: 0x0200084A RID: 2122
	public class NKCGuideManualTemplet : INKMTemplet
	{
		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x06005478 RID: 21624 RVA: 0x0019C225 File Offset: 0x0019A425
		public int Key
		{
			get
			{
				return this.ID;
			}
		}

		// Token: 0x06005479 RID: 21625 RVA: 0x0019C22D File Offset: 0x0019A42D
		public NKCGuideManualTemplet(int id, IEnumerable<NKCGuideManualTempletData> manualTemplets)
		{
			this.ID = id;
			this.lstManualTemplets.AddRange(manualTemplets);
			this.ID_STRING = this.lstManualTemplets[0].GUIDE_ID_STRING;
		}

		// Token: 0x0600547A RID: 21626 RVA: 0x0019C26C File Offset: 0x0019A46C
		public void Join()
		{
			foreach (NKCGuideManualTempletData nkcguideManualTempletData in this.lstManualTemplets)
			{
				nkcguideManualTempletData.Join();
			}
		}

		// Token: 0x0600547B RID: 21627 RVA: 0x0019C2BC File Offset: 0x0019A4BC
		public void Validate()
		{
		}

		// Token: 0x0600547C RID: 21628 RVA: 0x0019C2C0 File Offset: 0x0019A4C0
		public static void LoadFromLua()
		{
			NKMTempletContainer<NKCGuideManualTemplet>.Load(from e in NKMTempletLoader<NKCGuideManualTempletData>.LoadGroup("AB_SCRIPT", "LUA_GUIDE_MANUAL_TEMPLET", "m_GuideManualTemplet", new Func<NKMLua, NKCGuideManualTempletData>(NKCGuideManualTempletData.LoadFromLUA))
			select new NKCGuideManualTemplet(e.Key, e.Value), (NKCGuideManualTemplet e) => e.ID_STRING);
			NKCGuideManualTemplet.Loaded = true;
		}

		// Token: 0x0600547D RID: 21629 RVA: 0x0019C33B File Offset: 0x0019A53B
		public static NKCGuideManualTemplet Find(int id)
		{
			if (!NKCGuideManualTemplet.Loaded)
			{
				NKCGuideManualTemplet.LoadFromLua();
			}
			return NKMTempletContainer<NKCGuideManualTemplet>.Find(id);
		}

		// Token: 0x0600547E RID: 21630 RVA: 0x0019C34F File Offset: 0x0019A54F
		public static NKCGuideManualTemplet Find(string strID)
		{
			if (!NKCGuideManualTemplet.Loaded)
			{
				NKCGuideManualTemplet.LoadFromLua();
			}
			return NKMTempletContainer<NKCGuideManualTemplet>.Find(strID);
		}

		// Token: 0x0600547F RID: 21631 RVA: 0x0019C363 File Offset: 0x0019A563
		public string GetTitle()
		{
			return NKCStringTable.GetString(this.lstManualTemplets[0].CATEGORY_TITLE, false);
		}

		// Token: 0x06005480 RID: 21632 RVA: 0x0019C37C File Offset: 0x0019A57C
		public static string GetTitle(string strID)
		{
			string result = "";
			NKCGuideManualTemplet nkcguideManualTemplet = NKCGuideManualTemplet.Find(strID);
			if (nkcguideManualTemplet != null)
			{
				result = NKCStringTable.GetString(nkcguideManualTemplet.lstManualTemplets[0].CATEGORY_TITLE, false);
			}
			return result;
		}

		// Token: 0x06005481 RID: 21633 RVA: 0x0019C3B4 File Offset: 0x0019A5B4
		public string GetTitle(int id)
		{
			string result = "";
			NKCGuideManualTemplet nkcguideManualTemplet = NKCGuideManualTemplet.Find(id);
			if (nkcguideManualTemplet != null)
			{
				result = NKCStringTable.GetString(nkcguideManualTemplet.lstManualTemplets[0].CATEGORY_TITLE, false);
			}
			return result;
		}

		// Token: 0x0400436D RID: 17261
		public int ID;

		// Token: 0x0400436E RID: 17262
		public string ID_STRING;

		// Token: 0x0400436F RID: 17263
		public List<NKCGuideManualTempletData> lstManualTemplets = new List<NKCGuideManualTempletData>();

		// Token: 0x04004370 RID: 17264
		private static bool Loaded;
	}
}
