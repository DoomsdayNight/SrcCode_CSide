using System;
using System.Collections.Generic;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004A9 RID: 1193
	public class NKMPhaseChangeData : IEventConditionOwner
	{
		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06002137 RID: 8503 RVA: 0x000A9716 File Offset: 0x000A7916
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x000A9748 File Offset: 0x000A7948
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			if (cNKMLua.OpenTable("m_Condition"))
			{
				this.m_Condition.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_TargetPhase", ref this.m_TargetPhase);
			cNKMLua.GetData("m_fChangeConditionHPRate", ref this.m_fChangeConditionHPRate);
			cNKMLua.GetData("m_bCutHpDamage", ref this.m_bCutHpDamage);
			cNKMLua.GetData("m_fChangeConditionTime", ref this.m_fChangeConditionTime);
			cNKMLua.GetData("m_ChangeConditionMyKill", ref this.m_ChangeConditionMyKill);
			cNKMLua.GetData("m_bChangeConditionImmortalStart", ref this.m_bChangeConditionImmortalStart);
			cNKMLua.GetData("m_ChangeStateName", ref this.m_ChangeStateName);
			if (cNKMLua.OpenTable("m_listChangeCoolTime"))
			{
				int num = 1;
				while (cNKMLua.OpenTable(num))
				{
					NKMPhaseChangeCoolTime nkmphaseChangeCoolTime;
					if (this.m_listChangeCoolTime.Count < num)
					{
						nkmphaseChangeCoolTime = new NKMPhaseChangeCoolTime();
						this.m_listChangeCoolTime.Add(nkmphaseChangeCoolTime);
					}
					else
					{
						nkmphaseChangeCoolTime = this.m_listChangeCoolTime[num - 1];
					}
					nkmphaseChangeCoolTime.LoadFromLUA(cNKMLua);
					num++;
					cNKMLua.CloseTable();
				}
				cNKMLua.CloseTable();
			}
			return true;
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x000A985C File Offset: 0x000A7A5C
		public void DeepCopyFromSource(NKMPhaseChangeData source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_TargetPhase = source.m_TargetPhase;
			this.m_fChangeConditionHPRate = source.m_fChangeConditionHPRate;
			this.m_bCutHpDamage = source.m_bCutHpDamage;
			this.m_fChangeConditionTime = source.m_fChangeConditionTime;
			this.m_ChangeConditionMyKill = source.m_ChangeConditionMyKill;
			this.m_bChangeConditionImmortalStart = source.m_bChangeConditionImmortalStart;
			this.m_ChangeStateName = source.m_ChangeStateName;
			this.m_listChangeCoolTime.Clear();
			for (int i = 0; i < source.m_listChangeCoolTime.Count; i++)
			{
				NKMPhaseChangeCoolTime nkmphaseChangeCoolTime = new NKMPhaseChangeCoolTime();
				nkmphaseChangeCoolTime.DeepCopyFromSource(source.m_listChangeCoolTime[i]);
				this.m_listChangeCoolTime.Add(nkmphaseChangeCoolTime);
			}
		}

		// Token: 0x04002204 RID: 8708
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x04002205 RID: 8709
		public int m_TargetPhase;

		// Token: 0x04002206 RID: 8710
		public float m_fChangeConditionHPRate;

		// Token: 0x04002207 RID: 8711
		public bool m_bCutHpDamage;

		// Token: 0x04002208 RID: 8712
		public float m_fChangeConditionTime;

		// Token: 0x04002209 RID: 8713
		public int m_ChangeConditionMyKill;

		// Token: 0x0400220A RID: 8714
		public bool m_bChangeConditionImmortalStart;

		// Token: 0x0400220B RID: 8715
		public string m_ChangeStateName = "";

		// Token: 0x0400220C RID: 8716
		public List<NKMPhaseChangeCoolTime> m_listChangeCoolTime = new List<NKMPhaseChangeCoolTime>();
	}
}
