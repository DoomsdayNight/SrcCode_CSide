using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004EA RID: 1258
	public class NKMEventChangeCooltime : IEventConditionOwner, INKMUnitStateEventRollback, INKMUnitStateEvent
	{
		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x0600237B RID: 9083 RVA: 0x000B6FFC File Offset: 0x000B51FC
		public float EventStartTime
		{
			get
			{
				return this.m_fEventAnimTime;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x0600237C RID: 9084 RVA: 0x000B7004 File Offset: 0x000B5204
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x0600237D RID: 9085 RVA: 0x000B7007 File Offset: 0x000B5207
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x0600237E RID: 9086 RVA: 0x000B700F File Offset: 0x000B520F
		public bool bStateEnd
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x0600237F RID: 9087 RVA: 0x000B7012 File Offset: 0x000B5212
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x000B701A File Offset: 0x000B521A
		public void DeepCopyFromSource(NKMEventChangeCooltime source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_TargetStateName = source.m_TargetStateName;
			this.m_fChangeValue = source.m_fChangeValue;
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x000B7048 File Offset: 0x000B5248
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			if (cNKMLua.OpenTable("m_Condition"))
			{
				this.m_Condition.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_fEventAnimTime", ref this.m_fEventAnimTime);
			cNKMLua.GetData("m_bAnimTime", ref this.m_bAnimTime);
			cNKMLua.GetData("m_TargetStateName", ref this.m_TargetStateName);
			cNKMLua.GetDataEnum<NKMEventChangeCooltime.ChangeType>("m_eChangeType", out this.m_eChangeType);
			cNKMLua.GetData("m_fChangeValue", ref this.m_fChangeValue);
			return true;
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x000B70D4 File Offset: 0x000B52D4
		public void ProcessEventRollback(NKMGame cNKMGame, NKMUnit cNKMUnit, float rollbackTime)
		{
			NKMEventChangeCooltime.ChangeType eChangeType = this.m_eChangeType;
			if (eChangeType != NKMEventChangeCooltime.ChangeType.SET_RATIO)
			{
				if (eChangeType == NKMEventChangeCooltime.ChangeType.ADD_SECONDS)
				{
					cNKMUnit.SetStateCoolTimeAdd(this.m_TargetStateName, this.m_fChangeValue);
				}
			}
			else
			{
				cNKMUnit.SetStateCoolTime(this.m_TargetStateName, true, this.m_fChangeValue);
			}
			cNKMUnit.SetPushSync();
		}

		// Token: 0x04002509 RID: 9481
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x0400250A RID: 9482
		public float m_fEventAnimTime;

		// Token: 0x0400250B RID: 9483
		public bool m_bAnimTime = true;

		// Token: 0x0400250C RID: 9484
		public string m_TargetStateName = "";

		// Token: 0x0400250D RID: 9485
		public float m_fChangeValue;

		// Token: 0x0400250E RID: 9486
		public NKMEventChangeCooltime.ChangeType m_eChangeType;

		// Token: 0x02001228 RID: 4648
		public enum ChangeType : short
		{
			// Token: 0x040094FF RID: 38143
			SET_RATIO,
			// Token: 0x04009500 RID: 38144
			ADD_SECONDS
		}
	}
}
