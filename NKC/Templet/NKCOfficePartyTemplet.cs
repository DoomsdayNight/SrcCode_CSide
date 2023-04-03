using System;
using System.Collections.Generic;
using NKC.Util;
using NKM;
using NKM.Templet.Base;

namespace NKC.Templet
{
	// Token: 0x02000857 RID: 2135
	public class NKCOfficePartyTemplet : INKMTemplet
	{
		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x060054C9 RID: 21705 RVA: 0x0019CFA1 File Offset: 0x0019B1A1
		public NKMAssetName IllustName
		{
			get
			{
				return NKMAssetName.ParseBundleName(this.IllustID, this.IllustID);
			}
		}

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x060054CA RID: 21706 RVA: 0x0019CFB4 File Offset: 0x0019B1B4
		public int Key
		{
			get
			{
				return this.IDX;
			}
		}

		// Token: 0x060054CB RID: 21707 RVA: 0x0019CFBC File Offset: 0x0019B1BC
		public static NKCOfficePartyTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCOfficePartyTemplet.cs", 24))
			{
				return null;
			}
			NKCOfficePartyTemplet nkcofficePartyTemplet = new NKCOfficePartyTemplet();
			if (!(true & cNKMLua.GetData("IDX", ref nkcofficePartyTemplet.IDX) & cNKMLua.GetData("IllustID", ref nkcofficePartyTemplet.IllustID) & cNKMLua.GetData("IllustRatio", ref nkcofficePartyTemplet.IllustRatio) & cNKMLua.GetDataList("PartyEndAni", out nkcofficePartyTemplet.PartyEndAni)))
			{
				return null;
			}
			return nkcofficePartyTemplet;
		}

		// Token: 0x060054CC RID: 21708 RVA: 0x0019D02E File Offset: 0x0019B22E
		public static NKCOfficePartyTemplet Find(int ID)
		{
			return NKMTempletContainer<NKCOfficePartyTemplet>.Find(ID);
		}

		// Token: 0x060054CD RID: 21709 RVA: 0x0019D036 File Offset: 0x0019B236
		public static NKCOfficePartyTemplet GetRandomTemplet()
		{
			return NKCTempletUtility.PickRatio<NKCOfficePartyTemplet>(NKMTempletContainer<NKCOfficePartyTemplet>.Values, (NKCOfficePartyTemplet x) => x.IllustRatio);
		}

		// Token: 0x060054CE RID: 21710 RVA: 0x0019D061 File Offset: 0x0019B261
		public void Join()
		{
		}

		// Token: 0x060054CF RID: 21711 RVA: 0x0019D063 File Offset: 0x0019B263
		public void Validate()
		{
		}

		// Token: 0x040043BA RID: 17338
		public int IDX;

		// Token: 0x040043BB RID: 17339
		public string IllustID;

		// Token: 0x040043BC RID: 17340
		public int IllustRatio;

		// Token: 0x040043BD RID: 17341
		public List<string> PartyEndAni;
	}
}
