using System;
using Cs.Logging;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x02000388 RID: 904
	public class NKMItemEquipSetOptionTemplet : INKMTemplet
	{
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060016F9 RID: 5881 RVA: 0x0005BFDD File Offset: 0x0005A1DD
		public int Key
		{
			get
			{
				return this.m_EquipSetID;
			}
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x0005BFE8 File Offset: 0x0005A1E8
		public static NKMItemEquipSetOptionTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemManager.cs", 359))
			{
				return null;
			}
			NKMItemEquipSetOptionTemplet nkmitemEquipSetOptionTemplet = new NKMItemEquipSetOptionTemplet();
			bool flag = true & cNKMLua.GetData("m_EquipSetID", ref nkmitemEquipSetOptionTemplet.m_EquipSetID) & cNKMLua.GetData("m_EquipSetStrID", ref nkmitemEquipSetOptionTemplet.m_EquipSetStrID) & cNKMLua.GetData("m_EquipSetPart", ref nkmitemEquipSetOptionTemplet.m_EquipSetPart) & cNKMLua.GetData("m_EquipSetIconEffect", ref nkmitemEquipSetOptionTemplet.m_EquipSetIconEffect) & cNKMLua.GetData("m_EquipSetName", ref nkmitemEquipSetOptionTemplet.m_EquipSetName) & cNKMLua.GetData("m_EquipSetIcon", ref nkmitemEquipSetOptionTemplet.m_EquipSetIcon) & cNKMLua.GetData<NKM_STAT_TYPE>("m_StatType_1", ref nkmitemEquipSetOptionTemplet.m_StatType_1);
			cNKMLua.GetData("m_StatValue_1", ref nkmitemEquipSetOptionTemplet.m_StatValue_1);
			cNKMLua.GetData("m_StatRate_1", ref nkmitemEquipSetOptionTemplet.m_StatRate_1);
			cNKMLua.GetData<NKM_STAT_TYPE>("m_StatType_2", ref nkmitemEquipSetOptionTemplet.m_StatType_2);
			cNKMLua.GetData("m_StatValue_2", ref nkmitemEquipSetOptionTemplet.m_StatValue_2);
			cNKMLua.GetData("m_StatRate_2", ref nkmitemEquipSetOptionTemplet.m_StatRate_2);
			cNKMLua.GetData("UseFilter", ref nkmitemEquipSetOptionTemplet.UseFilter);
			cNKMLua.GetData("m_OpenTag", ref nkmitemEquipSetOptionTemplet.m_OpenTag);
			if (!flag)
			{
				Log.Error(string.Format("NKMItemEquipSetOptionTemplet Load fail - {0}", nkmitemEquipSetOptionTemplet.m_EquipSetID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemManager.cs", 384);
				return null;
			}
			return nkmitemEquipSetOptionTemplet;
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x0005C135 File Offset: 0x0005A335
		public void Join()
		{
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x0005C137 File Offset: 0x0005A337
		public void Validate()
		{
			if (this.m_StatType_1 == NKM_STAT_TYPE.NST_RANDOM)
			{
				NKMTempletError.Add(string.Format("NKMItemEquipSetOptionTemplet: id: {0} 의 StatType:{1}을 확인해주세요", this.m_EquipSetID, this.m_StatType_1), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMItemManager.cs", 399);
			}
		}

		// Token: 0x04000F87 RID: 3975
		public int m_EquipSetID;

		// Token: 0x04000F88 RID: 3976
		public string m_OpenTag;

		// Token: 0x04000F89 RID: 3977
		public string m_EquipSetStrID;

		// Token: 0x04000F8A RID: 3978
		public int m_EquipSetPart;

		// Token: 0x04000F8B RID: 3979
		public string m_EquipSetIconEffect;

		// Token: 0x04000F8C RID: 3980
		public string m_EquipSetName;

		// Token: 0x04000F8D RID: 3981
		public string m_EquipSetIcon;

		// Token: 0x04000F8E RID: 3982
		public NKM_STAT_TYPE m_StatType_1 = NKM_STAT_TYPE.NST_RANDOM;

		// Token: 0x04000F8F RID: 3983
		public float m_StatValue_1;

		// Token: 0x04000F90 RID: 3984
		public float m_StatRate_1;

		// Token: 0x04000F91 RID: 3985
		public NKM_STAT_TYPE m_StatType_2 = NKM_STAT_TYPE.NST_RANDOM;

		// Token: 0x04000F92 RID: 3986
		public float m_StatValue_2;

		// Token: 0x04000F93 RID: 3987
		public float m_StatRate_2;

		// Token: 0x04000F94 RID: 3988
		public bool UseFilter;
	}
}
