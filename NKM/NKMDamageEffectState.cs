using System;
using System.Collections.Generic;

namespace NKM
{
	// Token: 0x020003C9 RID: 969
	public class NKMDamageEffectState
	{
		// Token: 0x060019AC RID: 6572 RVA: 0x0006C90C File Offset: 0x0006AB0C
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			bool result = true & cNKMLua.GetData("m_StateName", ref this.m_StateName) & cNKMLua.GetData("m_AnimName", ref this.m_AnimName) & cNKMLua.GetData("m_bAnimLoop", ref this.m_bAnimLoop) & cNKMLua.GetData("m_fAnimSpeed", ref this.m_fAnimSpeed) & cNKMLua.GetData<NKM_LIFE_TIME_TYPE>("m_NKM_LIFE_TIME_TYPE", ref this.m_NKM_LIFE_TIME_TYPE) & cNKMLua.GetData("m_LifeTimeAnimCount", ref this.m_LifeTimeAnimCount) & cNKMLua.GetData("m_LifeTime", ref this.m_LifeTime) & cNKMLua.GetData("m_bNoMove", ref this.m_bNoMove) & cNKMLua.GetData("m_StateTimeChangeStateTime", ref this.m_StateTimeChangeStateTime) & cNKMLua.GetData("m_StateTimeChangeState", ref this.m_StateTimeChangeState) & cNKMLua.GetData("m_DamageCountChangeStateCount", ref this.m_DamageCountChangeStateCount) & cNKMLua.GetData("m_DamageCountChangeState", ref this.m_DamageCountChangeState) & cNKMLua.GetData("m_TargetDistFarChangeStateDist", ref this.m_TargetDistFarChangeStateDist) & cNKMLua.GetData("m_TargetDistFarChangeState", ref this.m_TargetDistFarChangeState) & cNKMLua.GetData("m_TargetDistNearChangeStateDist", ref this.m_TargetDistNearChangeStateDist) & cNKMLua.GetData("m_TargetDistNearChangeState", ref this.m_TargetDistNearChangeState) & cNKMLua.GetData("m_AnimEndChangeState", ref this.m_AnimEndChangeState) & cNKMLua.GetData("m_FootOnLandChangeState", ref this.m_FootOnLandChangeState);
			NKMUnitState.LoadEventList<NKMEventTargetDirSpeed>(cNKMLua, "m_listNKMEventTargetDirSpeed", ref this.m_listNKMEventTargetDirSpeed, null);
			NKMUnitState.LoadEventList<NKMEventDirSpeed>(cNKMLua, "m_listNKMEventDirSpeed", ref this.m_listNKMEventDirSpeed, null);
			NKMUnitState.LoadEventList<NKMEventSpeed>(cNKMLua, "m_listNKMEventSpeed", ref this.m_listNKMEventSpeed, null);
			NKMUnitState.LoadEventList<NKMEventSpeedX>(cNKMLua, "m_listNKMEventSpeedX", ref this.m_listNKMEventSpeedX, null);
			NKMUnitState.LoadEventList<NKMEventSpeedY>(cNKMLua, "m_listNKMEventSpeedY", ref this.m_listNKMEventSpeedY, null);
			NKMUnitState.LoadEventList<NKMEventMove>(cNKMLua, "m_listNKMEventMove", ref this.m_listNKMEventMove, null);
			NKMUnitState.LoadEventList<NKMEventAttack>(cNKMLua, "m_listNKMEventAttack", ref this.m_listNKMEventAttack, null);
			NKMUnitState.LoadEventList<NKMEventSound>(cNKMLua, "m_listNKMEventSound", ref this.m_listNKMEventSound, null);
			NKMUnitState.LoadEventList<NKMEventCameraCrash>(cNKMLua, "m_listNKMEventCameraCrash", ref this.m_listNKMEventCameraCrash, null);
			NKMUnitState.LoadEventList<NKMEventEffect>(cNKMLua, "m_listNKMEventEffect", ref this.m_listNKMEventEffect, null);
			NKMUnitState.LoadEventList<NKMEventDamageEffect>(cNKMLua, "m_listNKMEventDamageEffect", ref this.m_listNKMEventDamageEffect, null);
			NKMUnitState.LoadEventList<NKMEventDissolve>(cNKMLua, "m_listNKMEventDissolve", ref this.m_listNKMEventDissolve, null);
			NKMUnitState.LoadEventList<NKMEventBuff>(cNKMLua, "m_listNKMEventBuff", ref this.m_listNKMEventBuff, null);
			NKMUnitState.LoadEventList<NKMEventStatus>(cNKMLua, "m_listNKMEventStatus", ref this.m_listNKMEventStatus, null);
			NKMUnitState.LoadEventList<NKMEventHeal>(cNKMLua, "m_listNKMEventHeal", ref this.m_listNKMEventHeal, null);
			return result;
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x0006CB6C File Offset: 0x0006AD6C
		public void DeepCopyFromSource(NKMDamageEffectState source)
		{
			this.m_StateName = source.m_StateName;
			this.m_AnimName = source.m_AnimName;
			this.m_bAnimLoop = source.m_bAnimLoop;
			this.m_fAnimSpeed = source.m_fAnimSpeed;
			this.m_NKM_LIFE_TIME_TYPE = source.m_NKM_LIFE_TIME_TYPE;
			this.m_LifeTimeAnimCount = source.m_LifeTimeAnimCount;
			this.m_LifeTime = source.m_LifeTime;
			this.m_bNoMove = source.m_bNoMove;
			this.m_StateTimeChangeStateTime = source.m_StateTimeChangeStateTime;
			this.m_StateTimeChangeState = source.m_StateTimeChangeState;
			this.m_DamageCountChangeStateCount = source.m_DamageCountChangeStateCount;
			this.m_DamageCountChangeState = source.m_DamageCountChangeState;
			this.m_TargetDistFarChangeStateDist = source.m_TargetDistFarChangeStateDist;
			this.m_TargetDistFarChangeState = source.m_TargetDistFarChangeState;
			this.m_TargetDistNearChangeStateDist = source.m_TargetDistNearChangeStateDist;
			this.m_TargetDistNearChangeState = source.m_TargetDistNearChangeState;
			this.m_AnimEndChangeState = source.m_AnimEndChangeState;
			this.m_FootOnLandChangeState = source.m_FootOnLandChangeState;
			NKMUnitState.DeepCopy<NKMEventTargetDirSpeed>(source.m_listNKMEventTargetDirSpeed, ref this.m_listNKMEventTargetDirSpeed, delegate(NKMEventTargetDirSpeed t, NKMEventTargetDirSpeed s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventDirSpeed>(source.m_listNKMEventDirSpeed, ref this.m_listNKMEventDirSpeed, delegate(NKMEventDirSpeed t, NKMEventDirSpeed s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventSpeed>(source.m_listNKMEventSpeed, ref this.m_listNKMEventSpeed, delegate(NKMEventSpeed t, NKMEventSpeed s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventSpeedX>(source.m_listNKMEventSpeedX, ref this.m_listNKMEventSpeedX, delegate(NKMEventSpeedX t, NKMEventSpeedX s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventSpeedY>(source.m_listNKMEventSpeedY, ref this.m_listNKMEventSpeedY, delegate(NKMEventSpeedY t, NKMEventSpeedY s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventMove>(source.m_listNKMEventMove, ref this.m_listNKMEventMove, delegate(NKMEventMove t, NKMEventMove s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventAttack>(source.m_listNKMEventAttack, ref this.m_listNKMEventAttack, delegate(NKMEventAttack t, NKMEventAttack s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventSound>(source.m_listNKMEventSound, ref this.m_listNKMEventSound, delegate(NKMEventSound t, NKMEventSound s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventCameraCrash>(source.m_listNKMEventCameraCrash, ref this.m_listNKMEventCameraCrash, delegate(NKMEventCameraCrash t, NKMEventCameraCrash s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventEffect>(source.m_listNKMEventEffect, ref this.m_listNKMEventEffect, delegate(NKMEventEffect t, NKMEventEffect s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventDamageEffect>(source.m_listNKMEventDamageEffect, ref this.m_listNKMEventDamageEffect, delegate(NKMEventDamageEffect t, NKMEventDamageEffect s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventDissolve>(source.m_listNKMEventDissolve, ref this.m_listNKMEventDissolve, delegate(NKMEventDissolve t, NKMEventDissolve s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventBuff>(source.m_listNKMEventBuff, ref this.m_listNKMEventBuff, delegate(NKMEventBuff t, NKMEventBuff s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventStatus>(source.m_listNKMEventStatus, ref this.m_listNKMEventStatus, delegate(NKMEventStatus t, NKMEventStatus s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventHeal>(source.m_listNKMEventHeal, ref this.m_listNKMEventHeal, delegate(NKMEventHeal t, NKMEventHeal s)
			{
				t.DeepCopyFromSource(s);
			});
		}

		// Token: 0x04001215 RID: 4629
		public string m_StateName = "";

		// Token: 0x04001216 RID: 4630
		public string m_AnimName = "";

		// Token: 0x04001217 RID: 4631
		public bool m_bAnimLoop;

		// Token: 0x04001218 RID: 4632
		public float m_fAnimSpeed = 1f;

		// Token: 0x04001219 RID: 4633
		public NKM_LIFE_TIME_TYPE m_NKM_LIFE_TIME_TYPE = NKM_LIFE_TIME_TYPE.NLTT_TIME;

		// Token: 0x0400121A RID: 4634
		public float m_LifeTimeAnimCount;

		// Token: 0x0400121B RID: 4635
		public float m_LifeTime;

		// Token: 0x0400121C RID: 4636
		public bool m_bNoMove;

		// Token: 0x0400121D RID: 4637
		public float m_StateTimeChangeStateTime = -1f;

		// Token: 0x0400121E RID: 4638
		public string m_StateTimeChangeState = "";

		// Token: 0x0400121F RID: 4639
		public int m_DamageCountChangeStateCount;

		// Token: 0x04001220 RID: 4640
		public string m_DamageCountChangeState = "";

		// Token: 0x04001221 RID: 4641
		public float m_TargetDistFarChangeStateDist;

		// Token: 0x04001222 RID: 4642
		public string m_TargetDistFarChangeState = "";

		// Token: 0x04001223 RID: 4643
		public float m_TargetDistNearChangeStateDist;

		// Token: 0x04001224 RID: 4644
		public string m_TargetDistNearChangeState = "";

		// Token: 0x04001225 RID: 4645
		public string m_AnimEndChangeState = "";

		// Token: 0x04001226 RID: 4646
		public string m_FootOnLandChangeState = "";

		// Token: 0x04001227 RID: 4647
		public List<NKMEventTargetDirSpeed> m_listNKMEventTargetDirSpeed = new List<NKMEventTargetDirSpeed>();

		// Token: 0x04001228 RID: 4648
		public List<NKMEventDirSpeed> m_listNKMEventDirSpeed = new List<NKMEventDirSpeed>();

		// Token: 0x04001229 RID: 4649
		public List<NKMEventSpeed> m_listNKMEventSpeed = new List<NKMEventSpeed>();

		// Token: 0x0400122A RID: 4650
		public List<NKMEventSpeedX> m_listNKMEventSpeedX = new List<NKMEventSpeedX>();

		// Token: 0x0400122B RID: 4651
		public List<NKMEventSpeedY> m_listNKMEventSpeedY = new List<NKMEventSpeedY>();

		// Token: 0x0400122C RID: 4652
		public List<NKMEventMove> m_listNKMEventMove = new List<NKMEventMove>();

		// Token: 0x0400122D RID: 4653
		public List<NKMEventAttack> m_listNKMEventAttack = new List<NKMEventAttack>();

		// Token: 0x0400122E RID: 4654
		public List<NKMEventSound> m_listNKMEventSound = new List<NKMEventSound>();

		// Token: 0x0400122F RID: 4655
		public List<NKMEventCameraCrash> m_listNKMEventCameraCrash = new List<NKMEventCameraCrash>();

		// Token: 0x04001230 RID: 4656
		public List<NKMEventEffect> m_listNKMEventEffect = new List<NKMEventEffect>();

		// Token: 0x04001231 RID: 4657
		public List<NKMEventDamageEffect> m_listNKMEventDamageEffect = new List<NKMEventDamageEffect>();

		// Token: 0x04001232 RID: 4658
		public List<NKMEventDissolve> m_listNKMEventDissolve = new List<NKMEventDissolve>();

		// Token: 0x04001233 RID: 4659
		public List<NKMEventBuff> m_listNKMEventBuff = new List<NKMEventBuff>();

		// Token: 0x04001234 RID: 4660
		public List<NKMEventStatus> m_listNKMEventStatus = new List<NKMEventStatus>();

		// Token: 0x04001235 RID: 4661
		public List<NKMEventHeal> m_listNKMEventHeal = new List<NKMEventHeal>();
	}
}
