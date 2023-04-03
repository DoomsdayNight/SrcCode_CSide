using System;
using System.Collections.Generic;
using System.Linq;
using NKM;
using NKM.Templet.Base;

namespace NKC.Templet
{
	// Token: 0x0200084C RID: 2124
	public class NKCGuideTemplet : INKMTemplet
	{
		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x06005488 RID: 21640 RVA: 0x0019C4D0 File Offset: 0x0019A6D0
		public int Key
		{
			get
			{
				return this.ID;
			}
		}

		// Token: 0x06005489 RID: 21641 RVA: 0x0019C4D8 File Offset: 0x0019A6D8
		public NKCGuideTemplet(int id, IEnumerable<NKCGuideTempletImage> imageTemplets)
		{
			this.ID = id;
			this.lstImages.AddRange(imageTemplets);
			this.ID_STRING = this.lstImages[0].ID_STRING;
		}

		// Token: 0x0600548A RID: 21642 RVA: 0x0019C518 File Offset: 0x0019A718
		public void Join()
		{
			foreach (NKCGuideTempletImage nkcguideTempletImage in this.lstImages)
			{
				nkcguideTempletImage.Join();
			}
		}

		// Token: 0x0600548B RID: 21643 RVA: 0x0019C568 File Offset: 0x0019A768
		public void Validate()
		{
		}

		// Token: 0x0600548C RID: 21644 RVA: 0x0019C56C File Offset: 0x0019A76C
		public static void LoadFromLua()
		{
			NKMTempletContainer<NKCGuideTemplet>.Load(from e in NKMTempletLoader<NKCGuideTempletImage>.LoadGroup("AB_SCRIPT", "LUA_GUIDE_TEMPLET", "m_GuideTemplet", new Func<NKMLua, NKCGuideTempletImage>(NKCGuideTempletImage.LoadFromLUA))
			select new NKCGuideTemplet(e.Key, e.Value), (NKCGuideTemplet e) => e.ID_STRING);
			NKCGuideTemplet.Loaded = true;
		}

		// Token: 0x0600548D RID: 21645 RVA: 0x0019C5E7 File Offset: 0x0019A7E7
		public static NKCGuideTemplet Find(int id)
		{
			if (!NKCGuideTemplet.Loaded)
			{
				NKCGuideTemplet.LoadFromLua();
			}
			return NKMTempletContainer<NKCGuideTemplet>.Find(id);
		}

		// Token: 0x0600548E RID: 21646 RVA: 0x0019C5FB File Offset: 0x0019A7FB
		public static NKCGuideTemplet Find(string strID)
		{
			if (!NKCGuideTemplet.Loaded)
			{
				NKCGuideTemplet.LoadFromLua();
			}
			return NKMTempletContainer<NKCGuideTemplet>.Find(strID);
		}

		// Token: 0x04004379 RID: 17273
		public int ID;

		// Token: 0x0400437A RID: 17274
		public string ID_STRING;

		// Token: 0x0400437B RID: 17275
		public List<NKCGuideTempletImage> lstImages = new List<NKCGuideTempletImage>();

		// Token: 0x0400437C RID: 17276
		private static bool Loaded;
	}
}
