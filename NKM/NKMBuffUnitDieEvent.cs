using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004EC RID: 1260
	public class NKMBuffUnitDieEvent : IEventConditionOwner
	{
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x0600238C RID: 9100 RVA: 0x000B721D File Offset: 0x000B541D
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x000B7294 File Offset: 0x000B5494
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			if (cNKMLua.OpenTable("m_Condition"))
			{
				this.m_Condition.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_BuffStrID", ref this.m_BuffStrID);
			cNKMLua.GetData("m_fSkillCoolTime", ref this.m_fSkillCoolTime);
			cNKMLua.GetData("m_fHyperSkillCoolTime", ref this.m_fHyperSkillCoolTime);
			cNKMLua.GetData("m_fSkillCoolTimeAdd", ref this.m_fSkillCoolTimeAdd);
			cNKMLua.GetData("m_fHyperSkillCoolTimeAdd", ref this.m_fHyperSkillCoolTimeAdd);
			cNKMLua.GetData("m_fHPRate", ref this.m_fHPRate);
			cNKMLua.GetData("m_OutBuffStrID", ref this.m_OutBuffStrID);
			byte b = 0;
			if (cNKMLua.GetData("m_OutBuffLevel", ref b))
			{
				this.m_OutBuffStatLevel = b;
				this.m_OutBuffTimeLevel = b;
			}
			cNKMLua.GetData("m_OutBuffStatLevel", ref this.m_OutBuffStatLevel);
			cNKMLua.GetData("m_OutBuffTimeLevel", ref this.m_OutBuffTimeLevel);
			int num = 0;
			if (cNKMLua.GetData("m_AddOverlap", ref num))
			{
				if (num >= 0)
				{
					this.m_Overlap = num + 1;
				}
				else
				{
					this.m_Overlap = num;
				}
			}
			cNKMLua.GetData("m_Overlap", ref this.m_Overlap);
			if (this.m_Overlap >= 255)
			{
				Log.ErrorAndExit(string.Format("[NKMBuffUnitDieEvent] Overlap is to big [{0}/{1}] BuffID[{2}]", this.m_Overlap, byte.MaxValue, this.m_BuffStrID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitStateEvent.cs", 3970);
				return false;
			}
			NKMUnitState.LoadAndMergeEventList<NKMEventRespawn>(cNKMLua, "m_listNKMEventRespawn", ref this.m_listNKMEventRespawn, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventDamageEffect>(cNKMLua, "m_listNKMEventDamageEffect", ref this.m_listNKMEventDamageEffect, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventCost>(cNKMLua, "m_listNKMEventCost", ref this.m_listNKMEventCost, null);
			return true;
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x000B7438 File Offset: 0x000B5638
		public void DeepCopyFromSource(NKMBuffUnitDieEvent source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_BuffStrID = source.m_BuffStrID;
			this.m_fSkillCoolTime = source.m_fSkillCoolTime;
			this.m_fHyperSkillCoolTime = source.m_fHyperSkillCoolTime;
			this.m_fSkillCoolTimeAdd = source.m_fSkillCoolTimeAdd;
			this.m_fHyperSkillCoolTimeAdd = source.m_fHyperSkillCoolTimeAdd;
			this.m_fHPRate = source.m_fHPRate;
			this.m_OutBuffStrID = source.m_OutBuffStrID;
			this.m_OutBuffStatLevel = source.m_OutBuffStatLevel;
			this.m_OutBuffTimeLevel = source.m_OutBuffTimeLevel;
			this.m_Overlap = source.m_Overlap;
			NKMUnitState.DeepCopy<NKMEventRespawn>(source.m_listNKMEventRespawn, ref this.m_listNKMEventRespawn, delegate(NKMEventRespawn t, NKMEventRespawn s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventDamageEffect>(source.m_listNKMEventDamageEffect, ref this.m_listNKMEventDamageEffect, delegate(NKMEventDamageEffect t, NKMEventDamageEffect s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventCost>(source.m_listNKMEventCost, ref this.m_listNKMEventCost, delegate(NKMEventCost t, NKMEventCost s)
			{
				t.DeepCopyFromSource(s);
			});
		}

		// Token: 0x04002513 RID: 9491
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x04002514 RID: 9492
		public string m_BuffStrID = "";

		// Token: 0x04002515 RID: 9493
		public float m_fSkillCoolTime;

		// Token: 0x04002516 RID: 9494
		public float m_fHyperSkillCoolTime;

		// Token: 0x04002517 RID: 9495
		public float m_fSkillCoolTimeAdd;

		// Token: 0x04002518 RID: 9496
		public float m_fHyperSkillCoolTimeAdd;

		// Token: 0x04002519 RID: 9497
		public float m_fHPRate;

		// Token: 0x0400251A RID: 9498
		public string m_OutBuffStrID = "";

		// Token: 0x0400251B RID: 9499
		public byte m_OutBuffStatLevel = 1;

		// Token: 0x0400251C RID: 9500
		public byte m_OutBuffTimeLevel = 1;

		// Token: 0x0400251D RID: 9501
		public int m_Overlap = 1;

		// Token: 0x0400251E RID: 9502
		public List<NKMEventRespawn> m_listNKMEventRespawn = new List<NKMEventRespawn>();

		// Token: 0x0400251F RID: 9503
		public List<NKMEventDamageEffect> m_listNKMEventDamageEffect = new List<NKMEventDamageEffect>();

		// Token: 0x04002520 RID: 9504
		public List<NKMEventCost> m_listNKMEventCost = new List<NKMEventCost>();
	}
}
