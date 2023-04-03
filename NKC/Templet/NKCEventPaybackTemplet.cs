using System;
using NKM;
using NKM.Templet.Base;

namespace NKC.Templet
{
	// Token: 0x02000849 RID: 2121
	public class NKCEventPaybackTemplet : INKMTemplet
	{
		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x0600546D RID: 21613 RVA: 0x0019C11A File Offset: 0x0019A31A
		public int Key
		{
			get
			{
				return this.paybackEventId;
			}
		}

		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x0600546E RID: 21614 RVA: 0x0019C122 File Offset: 0x0019A322
		public string IntervalTag
		{
			get
			{
				return this.intervalTag;
			}
		}

		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x0600546F RID: 21615 RVA: 0x0019C12A File Offset: 0x0019A32A
		public string UnitStrId
		{
			get
			{
				return this.unitStrId;
			}
		}

		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x06005470 RID: 21616 RVA: 0x0019C132 File Offset: 0x0019A332
		public int SkinId
		{
			get
			{
				return this.skinId;
			}
		}

		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x06005471 RID: 21617 RVA: 0x0019C13A File Offset: 0x0019A33A
		public int MissionTabId
		{
			get
			{
				return this.missionTabId;
			}
		}

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x06005472 RID: 21618 RVA: 0x0019C142 File Offset: 0x0019A342
		public string BannerPrefabId
		{
			get
			{
				return this.bannerPrefabId;
			}
		}

		// Token: 0x06005473 RID: 21619 RVA: 0x0019C14A File Offset: 0x0019A34A
		public static NKCEventPaybackTemplet Find(int eventId)
		{
			return NKMTempletContainer<NKCEventPaybackTemplet>.Find(eventId);
		}

		// Token: 0x06005474 RID: 21620 RVA: 0x0019C154 File Offset: 0x0019A354
		public static NKCEventPaybackTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCEventPaybackTemplet.cs", 30))
			{
				return null;
			}
			NKCEventPaybackTemplet nkceventPaybackTemplet = new NKCEventPaybackTemplet();
			bool flag = true & cNKMLua.GetData("PaybackEventID", ref nkceventPaybackTemplet.paybackEventId) & cNKMLua.GetData("OpenTag", ref nkceventPaybackTemplet.openTag) & cNKMLua.GetData("IntervalTag", ref nkceventPaybackTemplet.intervalTag) & cNKMLua.GetData("MissionTabID", ref nkceventPaybackTemplet.missionTabId) & cNKMLua.GetData("ShortCutShopID", ref nkceventPaybackTemplet.shortCutShopId) & cNKMLua.GetData("BannerPrefabID", ref nkceventPaybackTemplet.bannerPrefabId);
			cNKMLua.GetData("UnitStrID", ref nkceventPaybackTemplet.unitStrId);
			cNKMLua.GetData("SkinID", ref nkceventPaybackTemplet.skinId);
			if (!flag)
			{
				return null;
			}
			return nkceventPaybackTemplet;
		}

		// Token: 0x06005475 RID: 21621 RVA: 0x0019C20E File Offset: 0x0019A40E
		public void Join()
		{
		}

		// Token: 0x06005476 RID: 21622 RVA: 0x0019C210 File Offset: 0x0019A410
		public void Validate()
		{
		}

		// Token: 0x04004365 RID: 17253
		private int paybackEventId;

		// Token: 0x04004366 RID: 17254
		private string openTag;

		// Token: 0x04004367 RID: 17255
		private string intervalTag;

		// Token: 0x04004368 RID: 17256
		private int missionTabId;

		// Token: 0x04004369 RID: 17257
		private string shortCutShopId;

		// Token: 0x0400436A RID: 17258
		private string bannerPrefabId;

		// Token: 0x0400436B RID: 17259
		private string unitStrId = "";

		// Token: 0x0400436C RID: 17260
		private int skinId;
	}
}
