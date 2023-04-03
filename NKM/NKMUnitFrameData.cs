using System;
using System.Collections.Generic;

namespace NKM
{
	// Token: 0x0200048F RID: 1167
	public class NKMUnitFrameData
	{
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001F85 RID: 8069 RVA: 0x0009565C File Offset: 0x0009385C
		public NKM_SUPER_ARMOR_LEVEL CurrentSuperArmorLevel
		{
			get
			{
				if (this.m_BuffSuperArmorLevel <= this.m_SuperArmorLevel)
				{
					return this.m_SuperArmorLevel;
				}
				return this.m_BuffSuperArmorLevel;
			}
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x00095760 File Offset: 0x00093960
		public void RespawnInit()
		{
			this.m_fInitHP = 0f;
			this.m_fLiveTime = 0f;
			this.m_fStateTimeBack = 0f;
			this.m_fStateTime = 0f;
			this.m_fAnimTimeBack = 0f;
			this.m_fAnimTime = 0f;
			this.m_fAnimTimeMax = 0f;
			this.m_AnimPlayCount = 0;
			this.m_bAnimPlayCountAddThisFrame = false;
			this.m_fFindTargetTime = 0f;
			this.m_fFindSubTargetTime = 0f;
			this.m_fAttackedTarget = 0f;
			this.m_bInvincible = false;
			this.m_fAddAttackRange = 0f;
			this.m_SuperArmorLevel = NKM_SUPER_ARMOR_LEVEL.NSAL_NO;
			this.m_BuffSuperArmorLevel = NKM_SUPER_ARMOR_LEVEL.NSAL_NO;
			this.m_BarrierBuffData = null;
			this.m_bImmortalStart = false;
			this.m_fDamageTransferGameUnitUID = 0;
			this.m_fDamageTransfer = 0f;
			this.m_fDamageReflection = 0f;
			this.m_GuardGameUnitUID = 0;
			this.m_fHealFeedback = 0f;
			this.m_fHealFeedbackMasterGameUnitUID = 0;
			this.m_BuffUnitLevel = 0;
			this.m_bFootOnLand = true;
			this.m_PhaseNow = 0;
			this.m_LastTargetPosX = -1f;
			this.m_LastTargetPosZ = -1f;
			this.m_LastTargetJumpYPos = -1f;
			this.m_PosXBefore = -1f;
			this.m_PosZBefore = -1f;
			this.m_JumpYPosBefore = -1f;
			this.m_PosXCalc = 0f;
			this.m_PosZCalc = 0f;
			this.m_JumpYPosCalc = 0f;
			this.m_fSpeedX = 0f;
			this.m_fSpeedY = 0f;
			this.m_fSpeedZ = 0f;
			this.m_fDamageSpeedX = 0f;
			this.m_fDamageSpeedZ = 0f;
			this.m_fDamageSpeedJumpY = 0f;
			this.m_fDamageSpeedKeepTimeX = 0f;
			this.m_fDamageSpeedKeepTimeZ = 0f;
			this.m_fDamageSpeedKeepTimeJumpY = 0f;
			this.m_fStopReserveTime = 0f;
			for (int i = 0; i < this.m_StopTime.Length; i++)
			{
				this.m_StopTime[i] = 0f;
			}
			this.m_fHitLightTime = 0f;
			for (int j = 0; j < this.m_listHitFeedBackCount.Count; j++)
			{
				this.m_listHitFeedBackCount[j] = 0;
			}
			for (int k = 0; k < this.m_listHitCriticalFeedBackCount.Count; k++)
			{
				this.m_listHitCriticalFeedBackCount[k] = 0;
			}
			for (int l = 0; l < this.m_listHitEvadeFeedBackCount.Count; l++)
			{
				this.m_listHitEvadeFeedBackCount[l] = 0;
			}
			this.m_bFindTargetThisFrame = false;
			this.m_bTargetChangeThisFrame = false;
			this.m_fDamageThisFrame = 0f;
			this.m_fDamageBeforeFrame = 0f;
			this.m_fTargetLostDurationTime = 0f;
			for (int m = 0; m < this.m_listUnitAccumStateData.Count; m++)
			{
				this.m_listUnitAccumStateData[m].m_dicAccumStateChange.Clear();
			}
			this.m_fDangerChargeTime = -1f;
			this.m_fDangerChargeDamage = 0f;
			this.m_DangerChargeHitCount = 0;
			this.m_AddAttackUnitCount = 0;
			this.m_KillCount = 0;
			this.m_hashNoReuseBuffID.Clear();
			this.m_hsStatus.Clear();
			this.m_hsImmuneStatus.Clear();
			this.m_dicStatusTime.Clear();
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x00095A7E File Offset: 0x00093C7E
		public void AddNoReuseBuff(short buffID)
		{
			if (!this.m_hashNoReuseBuffID.Contains(buffID))
			{
				this.m_hashNoReuseBuffID.Add(buffID);
			}
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x00095A9B File Offset: 0x00093C9B
		public bool IsNoReuseBuff(short buffID)
		{
			return this.m_hashNoReuseBuffID.Contains(buffID);
		}

		// Token: 0x040020B2 RID: 8370
		public NKMStatData m_StatData = new NKMStatData();

		// Token: 0x040020B3 RID: 8371
		public float m_fInitHP;

		// Token: 0x040020B4 RID: 8372
		public float m_fLiveTime;

		// Token: 0x040020B5 RID: 8373
		public float m_fStateTimeBack;

		// Token: 0x040020B6 RID: 8374
		public float m_fStateTime;

		// Token: 0x040020B7 RID: 8375
		public float m_fAnimTimeBack;

		// Token: 0x040020B8 RID: 8376
		public float m_fAnimTime;

		// Token: 0x040020B9 RID: 8377
		public float m_fAnimTimeMax;

		// Token: 0x040020BA RID: 8378
		public int m_AnimPlayCount;

		// Token: 0x040020BB RID: 8379
		public bool m_bAnimPlayCountAddThisFrame;

		// Token: 0x040020BC RID: 8380
		public float m_fAnimSpeedOrg = 1f;

		// Token: 0x040020BD RID: 8381
		public float m_fAnimSpeed = 1f;

		// Token: 0x040020BE RID: 8382
		public float m_fAirHigh;

		// Token: 0x040020BF RID: 8383
		public float m_fFindTargetTime;

		// Token: 0x040020C0 RID: 8384
		public float m_fFindSubTargetTime;

		// Token: 0x040020C1 RID: 8385
		public float m_fAttackedTarget;

		// Token: 0x040020C2 RID: 8386
		public bool m_bInvincible;

		// Token: 0x040020C3 RID: 8387
		public float m_fAddAttackRange;

		// Token: 0x040020C4 RID: 8388
		public NKM_SUPER_ARMOR_LEVEL m_SuperArmorLevel = NKM_SUPER_ARMOR_LEVEL.NSAL_NO;

		// Token: 0x040020C5 RID: 8389
		public NKM_SUPER_ARMOR_LEVEL m_BuffSuperArmorLevel = NKM_SUPER_ARMOR_LEVEL.NSAL_NO;

		// Token: 0x040020C6 RID: 8390
		public NKMBuffData m_BarrierBuffData;

		// Token: 0x040020C7 RID: 8391
		public HashSet<NKM_UNIT_STATUS_EFFECT> m_hsImmuneStatus = new HashSet<NKM_UNIT_STATUS_EFFECT>();

		// Token: 0x040020C8 RID: 8392
		public HashSet<NKM_UNIT_STATUS_EFFECT> m_hsStatus = new HashSet<NKM_UNIT_STATUS_EFFECT>();

		// Token: 0x040020C9 RID: 8393
		public Dictionary<NKM_UNIT_STATUS_EFFECT, float> m_dicStatusTime = new Dictionary<NKM_UNIT_STATUS_EFFECT, float>();

		// Token: 0x040020CA RID: 8394
		public bool m_bNotCastSummon;

		// Token: 0x040020CB RID: 8395
		public bool m_bImmortalStart;

		// Token: 0x040020CC RID: 8396
		public short m_fDamageTransferGameUnitUID;

		// Token: 0x040020CD RID: 8397
		public float m_fDamageTransfer;

		// Token: 0x040020CE RID: 8398
		public float m_fDamageReflection;

		// Token: 0x040020CF RID: 8399
		public float m_fHealFeedback;

		// Token: 0x040020D0 RID: 8400
		public short m_fHealFeedbackMasterGameUnitUID;

		// Token: 0x040020D1 RID: 8401
		public short m_GuardGameUnitUID;

		// Token: 0x040020D2 RID: 8402
		public int m_BuffUnitLevel;

		// Token: 0x040020D3 RID: 8403
		public bool m_bFootOnLand = true;

		// Token: 0x040020D4 RID: 8404
		public int m_PhaseNow;

		// Token: 0x040020D5 RID: 8405
		public float m_LastTargetPosX;

		// Token: 0x040020D6 RID: 8406
		public float m_LastTargetPosZ;

		// Token: 0x040020D7 RID: 8407
		public float m_LastTargetJumpYPos;

		// Token: 0x040020D8 RID: 8408
		public float m_PosXBefore;

		// Token: 0x040020D9 RID: 8409
		public float m_PosZBefore;

		// Token: 0x040020DA RID: 8410
		public float m_JumpYPosBefore;

		// Token: 0x040020DB RID: 8411
		public float m_PosXCalc;

		// Token: 0x040020DC RID: 8412
		public float m_PosZCalc;

		// Token: 0x040020DD RID: 8413
		public float m_JumpYPosCalc;

		// Token: 0x040020DE RID: 8414
		public float m_fSpeedX;

		// Token: 0x040020DF RID: 8415
		public float m_fSpeedY;

		// Token: 0x040020E0 RID: 8416
		public float m_fSpeedZ;

		// Token: 0x040020E1 RID: 8417
		public float m_fDamageSpeedX;

		// Token: 0x040020E2 RID: 8418
		public float m_fDamageSpeedZ;

		// Token: 0x040020E3 RID: 8419
		public float m_fDamageSpeedJumpY;

		// Token: 0x040020E4 RID: 8420
		public float m_fDamageSpeedKeepTimeX;

		// Token: 0x040020E5 RID: 8421
		public float m_fDamageSpeedKeepTimeZ;

		// Token: 0x040020E6 RID: 8422
		public float m_fDamageSpeedKeepTimeJumpY;

		// Token: 0x040020E7 RID: 8423
		public float m_fStopReserveTime;

		// Token: 0x040020E8 RID: 8424
		public float[] m_StopTime = new float[3];

		// Token: 0x040020E9 RID: 8425
		public float m_fHitLightTime;

		// Token: 0x040020EA RID: 8426
		public List<byte> m_listHitFeedBackCount = new List<byte>();

		// Token: 0x040020EB RID: 8427
		public List<byte> m_listHitCriticalFeedBackCount = new List<byte>();

		// Token: 0x040020EC RID: 8428
		public List<byte> m_listHitEvadeFeedBackCount = new List<byte>();

		// Token: 0x040020ED RID: 8429
		public float m_fColorEventTime;

		// Token: 0x040020EE RID: 8430
		public NKMTrackingFloat m_ColorR = new NKMTrackingFloat();

		// Token: 0x040020EF RID: 8431
		public NKMTrackingFloat m_ColorG = new NKMTrackingFloat();

		// Token: 0x040020F0 RID: 8432
		public NKMTrackingFloat m_ColorB = new NKMTrackingFloat();

		// Token: 0x040020F1 RID: 8433
		public bool m_bFindTargetThisFrame;

		// Token: 0x040020F2 RID: 8434
		public bool m_bFindSubTargetThisFrame;

		// Token: 0x040020F3 RID: 8435
		public bool m_bTargetChangeThisFrame;

		// Token: 0x040020F4 RID: 8436
		public float m_fDamageThisFrame;

		// Token: 0x040020F5 RID: 8437
		public float m_fDamageBeforeFrame;

		// Token: 0x040020F6 RID: 8438
		public bool m_bSyncShipSkill;

		// Token: 0x040020F7 RID: 8439
		public NKMShipSkillTemplet m_ShipSkillTemplet;

		// Token: 0x040020F8 RID: 8440
		public float m_fShipSkillPosX;

		// Token: 0x040020F9 RID: 8441
		public float m_fTargetLostDurationTime;

		// Token: 0x040020FA RID: 8442
		public List<NKMUnitAccumStateData> m_listUnitAccumStateData = new List<NKMUnitAccumStateData>();

		// Token: 0x040020FB RID: 8443
		public Dictionary<short, NKMBuffData> m_dicBuffData = new Dictionary<short, NKMBuffData>();

		// Token: 0x040020FC RID: 8444
		public HashSet<short> m_hashNoReuseBuffID = new HashSet<short>();

		// Token: 0x040020FD RID: 8445
		public float m_fDangerChargeTime = -1f;

		// Token: 0x040020FE RID: 8446
		public float m_fDangerChargeDamage;

		// Token: 0x040020FF RID: 8447
		public int m_DangerChargeHitCount;

		// Token: 0x04002100 RID: 8448
		public byte m_AddAttackUnitCount;

		// Token: 0x04002101 RID: 8449
		public int m_KillCount;
	}
}
