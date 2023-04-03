using System;
using Cs.Protocol;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200044C RID: 1100
	public class NKMOperatorSkill : ISerializable
	{
		// Token: 0x06001DE8 RID: 7656 RVA: 0x0008E6F7 File Offset: 0x0008C8F7
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.id);
			stream.PutOrGet(ref this.level);
			stream.PutOrGet(ref this.exp);
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x0008E720 File Offset: 0x0008C920
		public NKMBattleConditionTemplet GetBattleCondTemplet()
		{
			NKMOperatorSkillTemplet nkmoperatorSkillTemplet = NKMTempletContainer<NKMOperatorSkillTemplet>.Find(this.id);
			if (nkmoperatorSkillTemplet == null)
			{
				return null;
			}
			if (nkmoperatorSkillTemplet.m_OperSkillType != OperatorSkillType.m_Passive)
			{
				return null;
			}
			return NKMBattleConditionManager.GetTempletByStrID(nkmoperatorSkillTemplet.m_OperSkillTarget);
		}

		// Token: 0x04001E5E RID: 7774
		public int id;

		// Token: 0x04001E5F RID: 7775
		public byte level;

		// Token: 0x04001E60 RID: 7776
		public int exp;
	}
}
