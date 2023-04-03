using System;
using System.Collections.Generic;
using System.Linq;
using NKM.Templet;
using NKM.Templet.Base;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004CC RID: 1228
	public class NKMEventRespawn : IEventConditionOwner, INKMUnitStateEvent
	{
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06002287 RID: 8839 RVA: 0x000B2EBC File Offset: 0x000B10BC
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06002288 RID: 8840 RVA: 0x000B2EC4 File Offset: 0x000B10C4
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06002289 RID: 8841 RVA: 0x000B2ECC File Offset: 0x000B10CC
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x0600228A RID: 8842 RVA: 0x000B2ECF File Offset: 0x000B10CF
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x0600228B RID: 8843 RVA: 0x000B2ED7 File Offset: 0x000B10D7
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x000B2F20 File Offset: 0x000B1120
		public void DeepCopyFromSource(NKMEventRespawn source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_UnitStrID = source.m_UnitStrID;
			this.m_fOffsetX = source.m_fOffsetX;
			this.m_bShipSkillPos = source.m_bShipSkillPos;
			this.m_bUseMasterLevel = source.m_bUseMasterLevel;
			this.m_bUsePerSkillLevel = source.m_bUsePerSkillLevel;
			this.m_bUseMasterData = source.m_bUseMasterData;
			this.m_MaxCount = source.m_MaxCount;
			this.m_bMaxCountReRespawn = source.m_bMaxCountReRespawn;
			this.m_ReduceHPRate = source.m_ReduceHPRate;
			this.m_RespawnState = source.m_RespawnState;
			if (source.m_dicSummonSkin != null)
			{
				this.m_dicSummonSkin = new Dictionary<int, int>(source.m_dicSummonSkin);
				return;
			}
			this.m_dicSummonSkin = null;
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x000B2FFC File Offset: 0x000B11FC
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
			cNKMLua.GetData("m_UnitStrID", ref this.m_UnitStrID);
			cNKMLua.GetData("m_fOffsetX", ref this.m_fOffsetX);
			cNKMLua.GetData("m_bShipSkillPos", ref this.m_bShipSkillPos);
			cNKMLua.GetData("m_bUseMasterLevel", ref this.m_bUseMasterLevel);
			cNKMLua.GetData("m_bUsePerSkillLevel", ref this.m_bUsePerSkillLevel);
			cNKMLua.GetData("m_bUseMasterData", ref this.m_bUseMasterData);
			cNKMLua.GetData("m_MaxCount", ref this.m_MaxCount);
			cNKMLua.GetData("m_bMaxCountReRespawn", ref this.m_bMaxCountReRespawn);
			cNKMLua.GetData("m_ReduceHPRate", ref this.m_ReduceHPRate);
			cNKMLua.GetData("m_RespawnState", ref this.m_RespawnState);
			if (cNKMLua.OpenTable("m_dicSummonedUnitSkin"))
			{
				this.m_dicSummonSkin = new Dictionary<int, int>();
				int num = 1;
				while (cNKMLua.OpenTable(num))
				{
					int key;
					int value;
					if (cNKMLua.GetData(1, out key, 0) && cNKMLua.GetData(2, out value, 0))
					{
						this.m_dicSummonSkin.Add(key, value);
					}
					num++;
					cNKMLua.CloseTable();
				}
				cNKMLua.CloseTable();
			}
			return true;
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x000B3174 File Offset: 0x000B1374
		public void ValidateSummon(NKMUnitTempletBase templet)
		{
			string name = base.GetType().Name;
			if (templet.m_bMonster)
			{
				return;
			}
			if (string.IsNullOrEmpty(this.m_UnitStrID))
			{
				NKMTempletError.Add(string.Concat(new string[]
				{
					"[EventCondition] ",
					templet.DebugName,
					" 유닛의 ",
					name,
					"의 m_UnitStrID가 비어있습니다."
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitStateEvent.cs", 1461);
				return;
			}
			NKMUnitTemplet nkmunitTemplet;
			if (!NKMUnitManager.m_dicNKMUnitTempletStrID.TryGetValue(this.m_UnitStrID, out nkmunitTemplet))
			{
				NKMTempletError.Add(string.Concat(new string[]
				{
					"[EventCondition] ",
					templet.DebugName,
					" 유닛의 ",
					name,
					"에있는 유닛아이디 (",
					this.m_UnitStrID,
					")가 잘못되었습니다."
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitStateEvent.cs", 1467);
				return;
			}
			if (nkmunitTemplet.m_UnitTempletBase.m_bMonster)
			{
				return;
			}
			IEventConditionOwner[] array = nkmunitTemplet.m_listHitCriticalFeedBack.Cast<IEventConditionOwner>().Union(nkmunitTemplet.m_listPhaseChangeData.Cast<IEventConditionOwner>()).Union(nkmunitTemplet.m_listReflectionBuffData.Cast<IEventConditionOwner>()).Union(nkmunitTemplet.m_listStaticBuffData.Cast<IEventConditionOwner>()).Union(nkmunitTemplet.m_listHitFeedBack.Cast<IEventConditionOwner>()).Union(nkmunitTemplet.m_listAccumStateChangePack.Cast<IEventConditionOwner>()).Union(nkmunitTemplet.m_listBuffUnitDieEvent.Cast<IEventConditionOwner>()).Union(nkmunitTemplet.m_listStartStateData.Cast<IEventConditionOwner>()).Union(nkmunitTemplet.m_listStandStateData.Cast<IEventConditionOwner>()).Union(nkmunitTemplet.m_listRunStateData.Cast<IEventConditionOwner>()).Union(nkmunitTemplet.m_listAttackStateData.Cast<IEventConditionOwner>()).Union(nkmunitTemplet.m_listAirAttackStateData.Cast<IEventConditionOwner>()).Union(nkmunitTemplet.m_listSkillStateData.Cast<IEventConditionOwner>()).Union(nkmunitTemplet.m_listAirSkillStateData.Cast<IEventConditionOwner>()).Union(nkmunitTemplet.m_listHyperSkillStateData.Cast<IEventConditionOwner>()).Union(nkmunitTemplet.m_listAirHyperSkillStateData.Cast<IEventConditionOwner>()).Union(nkmunitTemplet.m_listHitEvadeFeedBack.Cast<IEventConditionOwner>()).Union(nkmunitTemplet.m_listKillFeedBack.Cast<IEventConditionOwner>()).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventDirSpeed.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventSpeed.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventSpeedX.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventSpeedY.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventMove.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventAttack.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventStopTime.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventInvincible.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventInvincibleGlobal.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventSuperArmor.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventSound.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventColor.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventCameraCrash.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventCameraMove.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventFadeWorld.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventDissolve.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventMotionBlur.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventEffect.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventHyperSkillCutIn.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventDamageEffect.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventDEStateChange.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventGameSpeed.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventBuff.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventStatus.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventRespawn.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventDie.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventChangeState.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventAgro.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventHeal.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventStun.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventCost.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventDefence.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventDispel.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventChangeCooltime.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventCatchEnd.Cast<IEventConditionOwner>())).Union(nkmunitTemplet.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventFindTarget.Cast<IEventConditionOwner>())).Union((from e in nkmunitTemplet.m_dicNKMUnitState.Values
			select e.m_NKMEventUnitChange into e
			where e != null
			select e).Cast<IEventConditionOwner>()).ToArray<IEventConditionOwner>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].ValidateMasterSkillId(templet, nkmunitTemplet.m_UnitTempletBase);
			}
		}

		// Token: 0x040023F1 RID: 9201
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x040023F2 RID: 9202
		public bool m_bAnimTime = true;

		// Token: 0x040023F3 RID: 9203
		public float m_fEventTime;

		// Token: 0x040023F4 RID: 9204
		public bool m_bStateEndTime;

		// Token: 0x040023F5 RID: 9205
		public string m_UnitStrID = "";

		// Token: 0x040023F6 RID: 9206
		public float m_fOffsetX;

		// Token: 0x040023F7 RID: 9207
		public bool m_bShipSkillPos;

		// Token: 0x040023F8 RID: 9208
		public bool m_bUseMasterLevel = true;

		// Token: 0x040023F9 RID: 9209
		public byte m_bUsePerSkillLevel;

		// Token: 0x040023FA RID: 9210
		public bool m_bUseMasterData;

		// Token: 0x040023FB RID: 9211
		public byte m_MaxCount = 1;

		// Token: 0x040023FC RID: 9212
		public bool m_bMaxCountReRespawn;

		// Token: 0x040023FD RID: 9213
		public float m_ReduceHPRate;

		// Token: 0x040023FE RID: 9214
		public string m_RespawnState = string.Empty;

		// Token: 0x040023FF RID: 9215
		public Dictionary<int, int> m_dicSummonSkin;
	}
}
