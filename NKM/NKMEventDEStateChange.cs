using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004E6 RID: 1254
	public class NKMEventDEStateChange : IEventConditionOwner, INKMUnitStateEventRollback, INKMUnitStateEvent
	{
		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600235F RID: 9055 RVA: 0x000B6B83 File Offset: 0x000B4D83
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06002360 RID: 9056 RVA: 0x000B6B8B File Offset: 0x000B4D8B
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06002361 RID: 9057 RVA: 0x000B6B93 File Offset: 0x000B4D93
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06002362 RID: 9058 RVA: 0x000B6B96 File Offset: 0x000B4D96
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06002363 RID: 9059 RVA: 0x000B6B9E File Offset: 0x000B4D9E
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x000B6BD8 File Offset: 0x000B4DD8
		public void DeepCopyFromSource(NKMEventDEStateChange source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_DamageEffectID = source.m_DamageEffectID;
			this.m_ChangeState = source.m_ChangeState;
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x000B6C34 File Offset: 0x000B4E34
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			if (cNKMLua.OpenTable("m_Condition"))
			{
				this.m_Condition.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_bAnimTime", ref this.m_bAnimTime);
			cNKMLua.GetData("m_fEventTime", ref this.m_fEventTime);
			cNKMLua.GetData("m_bStateEndTime", ref this.m_bStateEndTime);
			cNKMLua.GetData("m_DamageEffectID", ref this.m_DamageEffectID);
			cNKMLua.GetData("m_ChangeState", ref this.m_ChangeState);
			return true;
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x000B6CC0 File Offset: 0x000B4EC0
		public void ProcessEventRollback(NKMGame cNKMGame, NKMUnit cNKMUnit, float rollbackTime)
		{
			foreach (NKMDamageEffect nkmdamageEffect in cNKMUnit.llDamageEffect)
			{
				if (nkmdamageEffect != null && nkmdamageEffect.GetTemplet() != null && nkmdamageEffect.GetTemplet().m_DamageEffectID.Equals(this.m_DamageEffectID))
				{
					nkmdamageEffect.StateChangeByUnitState(this.m_ChangeState, true);
				}
			}
		}

		// Token: 0x040024F4 RID: 9460
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x040024F5 RID: 9461
		public bool m_bAnimTime = true;

		// Token: 0x040024F6 RID: 9462
		public float m_fEventTime;

		// Token: 0x040024F7 RID: 9463
		public bool m_bStateEndTime;

		// Token: 0x040024F8 RID: 9464
		public string m_DamageEffectID = "";

		// Token: 0x040024F9 RID: 9465
		public string m_ChangeState = "";
	}
}
