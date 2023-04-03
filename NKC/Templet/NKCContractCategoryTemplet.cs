using System;
using NKM;
using NKM.Templet.Base;

namespace NKC.Templet
{
	// Token: 0x02000845 RID: 2117
	public class NKCContractCategoryTemplet : INKMTemplet
	{
		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x06005450 RID: 21584 RVA: 0x0019BB84 File Offset: 0x00199D84
		public int Key
		{
			get
			{
				return this.m_CategoryID;
			}
		}

		// Token: 0x06005451 RID: 21585 RVA: 0x0019BB8C File Offset: 0x00199D8C
		public static NKCContractCategoryTemplet Find(int id)
		{
			return NKMTempletContainer<NKCContractCategoryTemplet>.Find(id);
		}

		// Token: 0x06005452 RID: 21586 RVA: 0x0019BB94 File Offset: 0x00199D94
		public string GetName()
		{
			return NKCStringTable.GetString(this.m_Name, false);
		}

		// Token: 0x06005453 RID: 21587 RVA: 0x0019BBA4 File Offset: 0x00199DA4
		public static NKCContractCategoryTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCContractCategoryTemplet.cs", 35))
			{
				return null;
			}
			NKCContractCategoryTemplet nkccontractCategoryTemplet = new NKCContractCategoryTemplet();
			bool flag = true & cNKMLua.GetData("m_CategoryID", ref nkccontractCategoryTemplet.m_CategoryID);
			cNKMLua.GetData("IDX", ref nkccontractCategoryTemplet.IDX);
			cNKMLua.GetData("m_Name", ref nkccontractCategoryTemplet.m_Name);
			cNKMLua.GetData<NKCContractCategoryTemplet.TabType>("m_Type", ref nkccontractCategoryTemplet.m_Type);
			if (!flag)
			{
				return null;
			}
			return nkccontractCategoryTemplet;
		}

		// Token: 0x06005454 RID: 21588 RVA: 0x0019BC16 File Offset: 0x00199E16
		public void Join()
		{
		}

		// Token: 0x06005455 RID: 21589 RVA: 0x0019BC18 File Offset: 0x00199E18
		public void Validate()
		{
		}

		// Token: 0x0400434A RID: 17226
		public int m_CategoryID;

		// Token: 0x0400434B RID: 17227
		public int IDX;

		// Token: 0x0400434C RID: 17228
		public string m_Name;

		// Token: 0x0400434D RID: 17229
		public NKCContractCategoryTemplet.TabType m_Type;

		// Token: 0x020014EA RID: 5354
		public enum TabType
		{
			// Token: 0x04009F5E RID: 40798
			Basic,
			// Token: 0x04009F5F RID: 40799
			Awaken,
			// Token: 0x04009F60 RID: 40800
			FollowTarget,
			// Token: 0x04009F61 RID: 40801
			Hidden,
			// Token: 0x04009F62 RID: 40802
			Confirm
		}
	}
}
