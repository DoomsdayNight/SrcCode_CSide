using System;
using System.Collections.Generic;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x02000520 RID: 1312
	public class NKMOperatorSkillTemplet : INKMTemplet
	{
		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x0600256F RID: 9583 RVA: 0x000C0D84 File Offset: 0x000BEF84
		public int Key
		{
			get
			{
				return this.m_OperSkillID;
			}
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x000C0D8C File Offset: 0x000BEF8C
		public static NKMOperatorSkillTemplet Find(int id)
		{
			return NKMTempletContainer<NKMOperatorSkillTemplet>.Find(id);
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x000C0D94 File Offset: 0x000BEF94
		public static NKMOperatorSkillTemplet Find(string strID)
		{
			return NKMTempletContainer<NKMOperatorSkillTemplet>.Find(strID);
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x000C0D9C File Offset: 0x000BEF9C
		public static List<string> GetAllSkillStrID_ByType(OperatorSkillType eOperatorSkillType)
		{
			List<string> list = new List<string>();
			foreach (NKMOperatorSkillTemplet nkmoperatorSkillTemplet in NKMTempletContainer<NKMOperatorSkillTemplet>.Values)
			{
				if (nkmoperatorSkillTemplet != null && nkmoperatorSkillTemplet.m_OperSkillType == eOperatorSkillType)
				{
					list.Add(nkmoperatorSkillTemplet.m_OperSkillStrID);
				}
			}
			return list;
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x000C0E00 File Offset: 0x000BF000
		public static NKMOperatorSkillTemplet LoadFromLua(NKMLua lua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(lua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/NKMOperatorSkillTemplet.cs", 54))
			{
				return null;
			}
			NKMOperatorSkillTemplet nkmoperatorSkillTemplet = new NKMOperatorSkillTemplet();
			bool flag = true & lua.GetData("m_OperSkillID", ref nkmoperatorSkillTemplet.m_OperSkillID) & lua.GetData("m_OperSkillStrID", ref nkmoperatorSkillTemplet.m_OperSkillStrID) & lua.GetData("m_OperSkillNameStrID", ref nkmoperatorSkillTemplet.m_OperSkillNameStrID) & lua.GetData("m_OperSkillDescStrID", ref nkmoperatorSkillTemplet.m_OperSkillDescStrID) & lua.GetData("m_OperSkillIcon", ref nkmoperatorSkillTemplet.m_OperSkillIcon) & lua.GetData<OperatorSkillType>("m_OperSkillType", ref nkmoperatorSkillTemplet.m_OperSkillType) & lua.GetData("m_OperSkillTarget", ref nkmoperatorSkillTemplet.m_OperSkillTarget) & lua.GetData("m_MaxSkillLevel", ref nkmoperatorSkillTemplet.m_MaxSkillLevel);
			lua.GetData("UseFilter", ref nkmoperatorSkillTemplet.UseFilter);
			if (!flag)
			{
				return null;
			}
			return nkmoperatorSkillTemplet;
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x000C0ECC File Offset: 0x000BF0CC
		public void Join()
		{
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x000C0ECE File Offset: 0x000BF0CE
		public void Validate()
		{
		}

		// Token: 0x040026C1 RID: 9921
		public int m_OperSkillID;

		// Token: 0x040026C2 RID: 9922
		public string m_OperSkillStrID;

		// Token: 0x040026C3 RID: 9923
		public string m_OperSkillNameStrID;

		// Token: 0x040026C4 RID: 9924
		public string m_OperSkillDescStrID;

		// Token: 0x040026C5 RID: 9925
		public string m_OperSkillIcon;

		// Token: 0x040026C6 RID: 9926
		public OperatorSkillType m_OperSkillType;

		// Token: 0x040026C7 RID: 9927
		public string m_OperSkillTarget;

		// Token: 0x040026C8 RID: 9928
		public int m_MaxSkillLevel;

		// Token: 0x040026C9 RID: 9929
		public bool UseFilter;
	}
}
