using System;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200052B RID: 1323
	public class NKCEquipAutoWeightTemplet : INKMTemplet
	{
		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060025B7 RID: 9655 RVA: 0x000C24D5 File Offset: 0x000C06D5
		public int Key
		{
			get
			{
				return (int)this.m_OptionWeight;
			}
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x000C24E0 File Offset: 0x000C06E0
		public static NKCEquipAutoWeightTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			NKCEquipAutoWeightTemplet nkcequipAutoWeightTemplet = new NKCEquipAutoWeightTemplet();
			bool flag = true & cNKMLua.GetData<NKM_STAT_TYPE>("m_OptionWeight", ref nkcequipAutoWeightTemplet.m_OptionWeight);
			cNKMLua.GetData<NKM_FIND_TARGET_TYPE>("m_OptionWeight_Exception", ref nkcequipAutoWeightTemplet.m_OptionWeight_Exception);
			bool flag2 = flag & cNKMLua.GetData("m_Prefer_Atk", ref nkcequipAutoWeightTemplet.m_Prefer_Atk) & cNKMLua.GetData("m_Prefer_Def", ref nkcequipAutoWeightTemplet.m_Prefer_Def) & cNKMLua.GetData("NURT_DEFENDER", ref nkcequipAutoWeightTemplet.NURT_DEFENDER) & cNKMLua.GetData("NURT_RANGER", ref nkcequipAutoWeightTemplet.NURT_RANGER) & cNKMLua.GetData("NURT_STRIKER", ref nkcequipAutoWeightTemplet.NURT_STRIKER) & cNKMLua.GetData("NURT_SNIPER", ref nkcequipAutoWeightTemplet.NURT_SNIPER) & cNKMLua.GetData("NURT_SUPPORTER", ref nkcequipAutoWeightTemplet.NURT_SUPPORTER) & cNKMLua.GetData("NURT_TOWER", ref nkcequipAutoWeightTemplet.NURT_TOWER) & cNKMLua.GetData("NURT_SIEGE", ref nkcequipAutoWeightTemplet.NURT_SIEGE);
			cNKMLua.GetData("NURT_INVALID", ref nkcequipAutoWeightTemplet.NURT_INVALID);
			nkcequipAutoWeightTemplet.CheckValidation();
			return nkcequipAutoWeightTemplet;
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x000C25D4 File Offset: 0x000C07D4
		public void Join()
		{
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x000C25D6 File Offset: 0x000C07D6
		public void Validate()
		{
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x000C25D8 File Offset: 0x000C07D8
		private void CheckValidation()
		{
		}

		// Token: 0x04002739 RID: 10041
		public int index;

		// Token: 0x0400273A RID: 10042
		public NKM_STAT_TYPE m_OptionWeight;

		// Token: 0x0400273B RID: 10043
		public NKM_FIND_TARGET_TYPE m_OptionWeight_Exception;

		// Token: 0x0400273C RID: 10044
		public float m_Prefer_Atk;

		// Token: 0x0400273D RID: 10045
		public float m_Prefer_Def;

		// Token: 0x0400273E RID: 10046
		public float NURT_DEFENDER;

		// Token: 0x0400273F RID: 10047
		public float NURT_RANGER;

		// Token: 0x04002740 RID: 10048
		public float NURT_STRIKER;

		// Token: 0x04002741 RID: 10049
		public float NURT_SNIPER;

		// Token: 0x04002742 RID: 10050
		public float NURT_SUPPORTER;

		// Token: 0x04002743 RID: 10051
		public float NURT_TOWER;

		// Token: 0x04002744 RID: 10052
		public float NURT_SIEGE;

		// Token: 0x04002745 RID: 10053
		public float NURT_INVALID;
	}
}
