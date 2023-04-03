using System;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200051E RID: 1310
	public class NKMOperatorRandomPassiveTemplet : INKMTemplet
	{
		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x0600256A RID: 9578 RVA: 0x000C0D0D File Offset: 0x000BEF0D
		public int Key
		{
			get
			{
				return this.groupId;
			}
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x000C0D18 File Offset: 0x000BEF18
		public static NKMOperatorRandomPassiveTemplet LoadFromLua(NKMLua lua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(lua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/NKMOperatorRandomPassiveTemplet.cs", 17))
			{
				return null;
			}
			NKMOperatorRandomPassiveTemplet nkmoperatorRandomPassiveTemplet = new NKMOperatorRandomPassiveTemplet();
			if (!(true & lua.GetData("m_OprPassiveGroupID", ref nkmoperatorRandomPassiveTemplet.groupId) & lua.GetData("m_OperSkillID", ref nkmoperatorRandomPassiveTemplet.operSkillId) & lua.GetData("m_Ratio", ref nkmoperatorRandomPassiveTemplet.ratio)))
			{
				return null;
			}
			return nkmoperatorRandomPassiveTemplet;
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x000C0D78 File Offset: 0x000BEF78
		public void Join()
		{
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x000C0D7A File Offset: 0x000BEF7A
		public void Validate()
		{
		}

		// Token: 0x040026BB RID: 9915
		public int groupId;

		// Token: 0x040026BC RID: 9916
		public int operSkillId;

		// Token: 0x040026BD RID: 9917
		public int ratio;
	}
}
