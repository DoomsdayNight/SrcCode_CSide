using System;
using NKM;
using NKM.Templet.Base;

namespace NKC
{
	// Token: 0x02000747 RID: 1863
	public class NKCMonsterTagInfoTemplet : INKMTemplet
	{
		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06004A70 RID: 19056 RVA: 0x001654C2 File Offset: 0x001636C2
		public int Key
		{
			get
			{
				return this.m_MonsterTagID;
			}
		}

		// Token: 0x06004A71 RID: 19057 RVA: 0x001654CA File Offset: 0x001636CA
		public static NKCMonsterTagInfoTemplet Find(int idx)
		{
			return NKMTempletContainer<NKCMonsterTagInfoTemplet>.Find(idx);
		}

		// Token: 0x06004A72 RID: 19058 RVA: 0x001654D4 File Offset: 0x001636D4
		public static NKCMonsterTagInfoTemplet LoadFromLUA(NKMLua lua)
		{
			NKCMonsterTagInfoTemplet nkcmonsterTagInfoTemplet = new NKCMonsterTagInfoTemplet();
			if (!(true & lua.GetData("m_MonsterTagID", ref nkcmonsterTagInfoTemplet.m_MonsterTagID) & lua.GetData("m_MonsterTagDesc", ref nkcmonsterTagInfoTemplet.m_MonsterTagDesc) & lua.GetData("m_MonsterTagIcon", ref nkcmonsterTagInfoTemplet.m_MonsterTagIcon)))
			{
				return null;
			}
			return nkcmonsterTagInfoTemplet;
		}

		// Token: 0x06004A73 RID: 19059 RVA: 0x00165523 File Offset: 0x00163723
		public void Join()
		{
		}

		// Token: 0x06004A74 RID: 19060 RVA: 0x00165525 File Offset: 0x00163725
		public void Validate()
		{
		}

		// Token: 0x0400394A RID: 14666
		public int m_MonsterTagID;

		// Token: 0x0400394B RID: 14667
		public string m_MonsterTagDesc;

		// Token: 0x0400394C RID: 14668
		public string m_MonsterTagIcon;
	}
}
