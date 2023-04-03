using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020003CF RID: 975
	public class NKMHitBuff : IEventConditionOwner
	{
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060019BF RID: 6591 RVA: 0x0006DDA6 File Offset: 0x0006BFA6
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x0006DDE1 File Offset: 0x0006BFE1
		public bool Validate()
		{
			return string.IsNullOrEmpty(this.m_HitBuff) || NKMBuffManager.GetBuffTempletByStrID(this.m_HitBuff) != null;
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x0006DE00 File Offset: 0x0006C000
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			if (cNKMLua.OpenTable("m_Condition"))
			{
				this.m_Condition.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_HitBuff", ref this.m_HitBuff);
			byte b = 0;
			if (cNKMLua.GetData("m_HitBuffBaseLevel", ref b))
			{
				this.m_HitBuffStatBaseLevel = b;
				this.m_HitBuffTimeBaseLevel = b;
			}
			byte b2 = 0;
			if (cNKMLua.GetData("m_HitBuffAddLVBySkillLV", ref b2))
			{
				this.m_HitBuffStatAddLVBySkillLV = b2;
				this.m_HitBuffTimeAddLVBySkillLV = b2;
			}
			cNKMLua.GetData("m_HitBuffStatBaseLevel", ref this.m_HitBuffStatBaseLevel);
			cNKMLua.GetData("m_HitBuffStatAddLVBySkillLV", ref this.m_HitBuffStatAddLVBySkillLV);
			cNKMLua.GetData("m_HitBuffTimeBaseLevel", ref this.m_HitBuffTimeBaseLevel);
			cNKMLua.GetData("m_HitBuffTimeAddLVBySkillLV", ref this.m_HitBuffTimeAddLVBySkillLV);
			cNKMLua.GetData("m_bRemove", ref this.m_bRemove);
			cNKMLua.GetData("m_HitBuffOverlap", ref this.m_HitBuffOverlap);
			return true;
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x0006DEEC File Offset: 0x0006C0EC
		public void DeepCopyFromSource(NKMHitBuff source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_HitBuff = source.m_HitBuff;
			this.m_HitBuffStatBaseLevel = source.m_HitBuffStatBaseLevel;
			this.m_HitBuffStatAddLVBySkillLV = source.m_HitBuffStatAddLVBySkillLV;
			this.m_HitBuffTimeBaseLevel = source.m_HitBuffTimeBaseLevel;
			this.m_HitBuffTimeAddLVBySkillLV = source.m_HitBuffTimeAddLVBySkillLV;
			this.m_bRemove = source.m_bRemove;
			this.m_HitBuffOverlap = source.m_HitBuffOverlap;
		}

		// Token: 0x04001281 RID: 4737
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x04001282 RID: 4738
		public string m_HitBuff = "";

		// Token: 0x04001283 RID: 4739
		public byte m_HitBuffStatBaseLevel = 1;

		// Token: 0x04001284 RID: 4740
		public byte m_HitBuffStatAddLVBySkillLV;

		// Token: 0x04001285 RID: 4741
		public byte m_HitBuffTimeBaseLevel = 1;

		// Token: 0x04001286 RID: 4742
		public byte m_HitBuffTimeAddLVBySkillLV;

		// Token: 0x04001287 RID: 4743
		public byte m_HitBuffOverlap = 1;

		// Token: 0x04001288 RID: 4744
		public bool m_bRemove;
	}
}
