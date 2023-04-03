using System;
using System.Collections.Generic;
using Cs.Math;
using NKM.Templet;
using NKM.Templet.Base;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004D1 RID: 1233
	public class NKMEventHeal : IEventConditionOwner, INKMTemplet, INKMUnitStateEventRollback, INKMUnitStateEvent
	{
		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x060022B1 RID: 8881 RVA: 0x000B4091 File Offset: 0x000B2291
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x060022B2 RID: 8882 RVA: 0x000B4099 File Offset: 0x000B2299
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x060022B3 RID: 8883 RVA: 0x000B40A1 File Offset: 0x000B22A1
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x060022B4 RID: 8884 RVA: 0x000B40A4 File Offset: 0x000B22A4
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x060022B5 RID: 8885 RVA: 0x000B40AC File Offset: 0x000B22AC
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x060022B6 RID: 8886 RVA: 0x000B40B4 File Offset: 0x000B22B4
		public int Key
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x000B40B8 File Offset: 0x000B22B8
		public void DeepCopyFromSource(NKMEventHeal source)
		{
			this.m_EventStrID = source.m_EventStrID;
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_fHeal = source.m_fHeal;
			this.m_fHealRate = source.m_fHealRate;
			this.m_bEnableSelfHeal = source.m_bEnableSelfHeal;
			this.m_fRangeMin = source.m_fRangeMin;
			this.m_fRangeMax = source.m_fRangeMax;
			this.m_MaxCount = source.m_MaxCount;
			this.m_fHealPowerPerSkillLevel = source.m_fHealPowerPerSkillLevel;
			this.m_HealCountPerSkillLevel = source.m_HealCountPerSkillLevel;
			this.m_bIgnoreShip = source.m_bIgnoreShip;
			this.m_IgnoreStyleType.Clear();
			foreach (NKM_UNIT_STYLE_TYPE item in source.m_IgnoreStyleType)
			{
				this.m_IgnoreStyleType.Add(item);
			}
			this.m_AllowStyleType.Clear();
			foreach (NKM_UNIT_STYLE_TYPE item2 in source.m_AllowStyleType)
			{
				this.m_AllowStyleType.Add(item2);
			}
			this.m_bSplashNearTarget = source.m_bSplashNearTarget;
			this.m_bSelfTargetingOnly = source.m_bSelfTargetingOnly;
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x000B4234 File Offset: 0x000B2434
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			cNKMLua.GetData("m_EventStrID", ref this.m_EventStrID);
			if (cNKMLua.OpenTable("m_Condition"))
			{
				this.m_Condition.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_bAnimTime", ref this.m_bAnimTime);
			cNKMLua.GetData("m_fEventTime", ref this.m_fEventTime);
			cNKMLua.GetData("m_bStateEndTime", ref this.m_bStateEndTime);
			cNKMLua.GetData("m_fHeal", ref this.m_fHeal);
			cNKMLua.GetData("m_fHealRate", ref this.m_fHealRate);
			cNKMLua.GetData("m_bEnableSelfHeal", ref this.m_bEnableSelfHeal);
			cNKMLua.GetData("m_fRangeMin", ref this.m_fRangeMin);
			cNKMLua.GetData("m_fRangeMax", ref this.m_fRangeMax);
			cNKMLua.GetData("m_MaxCount", ref this.m_MaxCount);
			cNKMLua.GetData("m_fHealPowerPerSkillLevel", ref this.m_fHealPowerPerSkillLevel);
			cNKMLua.GetData("m_HealCountPerSkillLevel", ref this.m_HealCountPerSkillLevel);
			cNKMLua.GetData("m_bIgnoreShip", ref this.m_bIgnoreShip);
			cNKMLua.GetData("m_bSplashNearTarget", ref this.m_bSplashNearTarget);
			cNKMLua.GetData("m_bSelfTargetingOnly", ref this.m_bSelfTargetingOnly);
			this.m_IgnoreStyleType.Clear();
			if (cNKMLua.OpenTable("m_IgnoreStyleType"))
			{
				bool flag = true;
				int num = 1;
				NKM_UNIT_STYLE_TYPE item = NKM_UNIT_STYLE_TYPE.NUST_INVALID;
				while (flag)
				{
					flag = cNKMLua.GetData<NKM_UNIT_STYLE_TYPE>(num, ref item);
					if (flag)
					{
						this.m_IgnoreStyleType.Add(item);
					}
					num++;
				}
				cNKMLua.CloseTable();
			}
			this.m_AllowStyleType.Clear();
			if (cNKMLua.OpenTable("m_AllowStyleType"))
			{
				bool flag2 = true;
				int num2 = 1;
				NKM_UNIT_STYLE_TYPE item2 = NKM_UNIT_STYLE_TYPE.NUST_INVALID;
				while (flag2)
				{
					flag2 = cNKMLua.GetData<NKM_UNIT_STYLE_TYPE>(num2, ref item2);
					if (flag2)
					{
						this.m_AllowStyleType.Add(item2);
					}
					num2++;
				}
				cNKMLua.CloseTable();
			}
			return true;
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x000B4407 File Offset: 0x000B2607
		public static NKMEventHeal LoadFromLUAStatic(NKMLua cNKMLua)
		{
			NKMEventHeal nkmeventHeal = new NKMEventHeal();
			nkmeventHeal.LoadFromLUA(cNKMLua);
			nkmeventHeal.Validate();
			return nkmeventHeal;
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x000B441C File Offset: 0x000B261C
		public float CalcHealAmount(int skillLevel, float maxHp)
		{
			float num = 1f;
			if (skillLevel > 1)
			{
				num += (float)(this.m_HealCountPerSkillLevel * (skillLevel - 1));
			}
			if (this.m_fHeal > 0f)
			{
				return this.m_fHeal * num;
			}
			return maxHp * this.m_fHealRate * num;
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x000B4461 File Offset: 0x000B2661
		public void Join()
		{
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x000B4464 File Offset: 0x000B2664
		public void Validate()
		{
			if (this.m_fHeal.IsNearlyZero(1E-05f) && this.m_fHealRate.IsNearlyZero(1E-05f))
			{
				NKMTempletError.Add(string.Format("[EventHeal:{0}] 수치가 올바르지 않음. heal:{1} healRate:{2}", this.m_EventStrID, this.m_fHeal, this.m_fHealRate), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitStateEvent.cs", 1953);
			}
			if (this.m_bSelfTargetingOnly)
			{
				if (this.m_bSplashNearTarget)
				{
					NKMTempletError.Add("[EventHeal:" + this.m_EventStrID + "] 개인 이벤트힐은 m_bSplashNearTarget 설정 불가.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitStateEvent.cs", 1960);
				}
				if (this.m_IgnoreStyleType.Count > 0)
				{
					NKMTempletError.Add("[EventHeal:" + this.m_EventStrID + "] 개인 이벤트힐은 m_IgnoreStyleType 설정 불가.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitStateEvent.cs", 1965);
				}
				if (!this.m_bEnableSelfHeal)
				{
					NKMTempletError.Add("[EventHeal:" + this.m_EventStrID + "] 개인 이벤트힐은 m_bEnableSelfHeal 설정 필수.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitStateEvent.cs", 1970);
				}
				if (this.m_MaxCount > 1)
				{
					NKMTempletError.Add(string.Format("[EventHeal:{0}] 개인 이벤트힐은 m_MaxCount = 0 or 1 제한. maxCount:{1}", this.m_EventStrID, this.m_MaxCount), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitStateEvent.cs", 1975);
				}
				if (this.m_HealCountPerSkillLevel != 0)
				{
					NKMTempletError.Add(string.Format("[EventHeal:{0}] 개인 이벤트힐은 m_HealCountPerSkillLevel = 0 설정 필수. m_HealCountPerSkillLevel:{1}", this.m_EventStrID, this.m_HealCountPerSkillLevel), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitStateEvent.cs", 1980);
				}
				if (!this.m_fRangeMin.IsNearlyZero(1E-05f) || !this.m_fRangeMax.IsNearlyZero(1E-05f))
				{
					NKMTempletError.Add(string.Format("[EventHeal:{0}] 개인 이벤트힐은 범위 지정하지 마세요. range min:{1} max:{2}", this.m_EventStrID, this.m_fRangeMin, this.m_fRangeMax), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitStateEvent.cs", 1985);
				}
			}
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x000B461D File Offset: 0x000B281D
		public void ProcessEventRollback(NKMGame cNKMGame, NKMUnit cNKMUnit, float rollbackTime)
		{
			cNKMUnit.SetEventHeal(this, cNKMUnit.GetUnitSyncData().m_PosX);
		}

		// Token: 0x04002419 RID: 9241
		public string m_EventStrID = "";

		// Token: 0x0400241A RID: 9242
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x0400241B RID: 9243
		public bool m_bAnimTime = true;

		// Token: 0x0400241C RID: 9244
		public float m_fEventTime;

		// Token: 0x0400241D RID: 9245
		public bool m_bStateEndTime;

		// Token: 0x0400241E RID: 9246
		public float m_fHeal;

		// Token: 0x0400241F RID: 9247
		public float m_fHealRate;

		// Token: 0x04002420 RID: 9248
		public bool m_bEnableSelfHeal = true;

		// Token: 0x04002421 RID: 9249
		public float m_fRangeMin;

		// Token: 0x04002422 RID: 9250
		public float m_fRangeMax;

		// Token: 0x04002423 RID: 9251
		public int m_MaxCount;

		// Token: 0x04002424 RID: 9252
		public float m_fHealPowerPerSkillLevel;

		// Token: 0x04002425 RID: 9253
		public int m_HealCountPerSkillLevel;

		// Token: 0x04002426 RID: 9254
		public HashSet<NKM_UNIT_STYLE_TYPE> m_AllowStyleType = new HashSet<NKM_UNIT_STYLE_TYPE>();

		// Token: 0x04002427 RID: 9255
		public HashSet<NKM_UNIT_STYLE_TYPE> m_IgnoreStyleType = new HashSet<NKM_UNIT_STYLE_TYPE>();

		// Token: 0x04002428 RID: 9256
		public bool m_bIgnoreShip = true;

		// Token: 0x04002429 RID: 9257
		public bool m_bSplashNearTarget;

		// Token: 0x0400242A RID: 9258
		public bool m_bSelfTargetingOnly;
	}
}
