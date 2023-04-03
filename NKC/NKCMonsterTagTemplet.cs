using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet.Base;

namespace NKC
{
	// Token: 0x02000748 RID: 1864
	public class NKCMonsterTagTemplet : INKMTemplet
	{
		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06004A76 RID: 19062 RVA: 0x0016552F File Offset: 0x0016372F
		public int Key
		{
			get
			{
				return this.m_UnitID;
			}
		}

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06004A77 RID: 19063 RVA: 0x00165537 File Offset: 0x00163737
		public List<int> lstTags
		{
			get
			{
				return this.m_MonsterTagID;
			}
		}

		// Token: 0x06004A78 RID: 19064 RVA: 0x0016553F File Offset: 0x0016373F
		public static NKCMonsterTagTemplet Find(int idx)
		{
			return NKMTempletContainer<NKCMonsterTagTemplet>.Find(idx);
		}

		// Token: 0x06004A79 RID: 19065 RVA: 0x00165548 File Offset: 0x00163748
		public static NKCMonsterTagTemplet LoadFromLUA(NKMLua lua)
		{
			NKCMonsterTagTemplet nkcmonsterTagTemplet = new NKCMonsterTagTemplet();
			if (!(true & lua.GetData("m_UnitID", ref nkcmonsterTagTemplet.m_UnitID) & lua.GetDataList("m_MonsterTagID", out nkcmonsterTagTemplet.m_MonsterTagID)))
			{
				return null;
			}
			return nkcmonsterTagTemplet;
		}

		// Token: 0x06004A7A RID: 19066 RVA: 0x00165585 File Offset: 0x00163785
		public void Join()
		{
		}

		// Token: 0x06004A7B RID: 19067 RVA: 0x00165587 File Offset: 0x00163787
		public void Validate()
		{
		}

		// Token: 0x0400394D RID: 14669
		public int m_UnitID;

		// Token: 0x0400394E RID: 14670
		private List<int> m_MonsterTagID;
	}
}
