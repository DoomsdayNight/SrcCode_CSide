using System;
using Cs.Protocol;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200044B RID: 1099
	public sealed class NKMOperator : ISerializable
	{
		// Token: 0x06001DE4 RID: 7652 RVA: 0x0008E568 File Offset: 0x0008C768
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.id);
			stream.PutOrGet(ref this.uid);
			stream.PutOrGet(ref this.level);
			stream.PutOrGet(ref this.exp);
			stream.PutOrGet(ref this.bLock);
			stream.PutOrGet<NKMOperatorSkill>(ref this.mainSkill);
			stream.PutOrGet<NKMOperatorSkill>(ref this.subSkill);
			stream.PutOrGet(ref this.fromContract);
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x0008E5D8 File Offset: 0x0008C7D8
		public static int CalculateOperationPower(int unitID, int level, int mainSkillID, int mainSkillLevel, int subSkillID, int subSkillLevel)
		{
			int result = 0;
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
			if (unitTempletBase != null)
			{
				NKMOperatorSkillTemplet nkmoperatorSkillTemplet = NKMTempletContainer<NKMOperatorSkillTemplet>.Find(mainSkillID);
				if (nkmoperatorSkillTemplet == null)
				{
					return 0;
				}
				NKMOperatorSkillTemplet nkmoperatorSkillTemplet2 = NKMTempletContainer<NKMOperatorSkillTemplet>.Find(subSkillID);
				if (nkmoperatorSkillTemplet2 == null)
				{
					return 0;
				}
				if (mainSkillLevel == 0 || subSkillLevel == 0)
				{
					return 0;
				}
				float num = (float)mainSkillLevel / (float)nkmoperatorSkillTemplet.m_MaxSkillLevel * 3000f;
				float num2 = (float)subSkillLevel / (float)nkmoperatorSkillTemplet2.m_MaxSkillLevel * 3000f;
				float num3 = (float)level / (float)NKMCommonConst.OperatorConstTemplet.unitMaximumLevel * 3000f;
				float num4 = 3000f;
				switch (unitTempletBase.m_NKM_UNIT_GRADE)
				{
				case NKM_UNIT_GRADE.NUG_N:
					num4 *= 0.1f;
					break;
				case NKM_UNIT_GRADE.NUG_R:
					num4 *= 0.3f;
					break;
				case NKM_UNIT_GRADE.NUG_SR:
					num4 *= 0.6f;
					break;
				}
				result = (int)(num + num2 + num3 + num4 + 0.5f);
			}
			return result;
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x0008E6B0 File Offset: 0x0008C8B0
		public int CalculateOperatorOperationPower()
		{
			return NKMOperator.CalculateOperationPower(this.id, this.level, this.mainSkill.id, (int)this.mainSkill.level, this.subSkill.id, (int)this.subSkill.level);
		}

		// Token: 0x04001E55 RID: 7765
		public long uid;

		// Token: 0x04001E56 RID: 7766
		public int id;

		// Token: 0x04001E57 RID: 7767
		public int level;

		// Token: 0x04001E58 RID: 7768
		public int exp;

		// Token: 0x04001E59 RID: 7769
		public NKMOperatorSkill mainSkill;

		// Token: 0x04001E5A RID: 7770
		public NKMOperatorSkill subSkill;

		// Token: 0x04001E5B RID: 7771
		public bool bLock;

		// Token: 0x04001E5C RID: 7772
		public bool fromContract;

		// Token: 0x04001E5D RID: 7773
		public int power;
	}
}
