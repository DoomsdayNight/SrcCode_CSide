using System;

namespace NKM
{
	// Token: 0x020003C4 RID: 964
	public class NKMDamageEffectData
	{
		// Token: 0x06001958 RID: 6488 RVA: 0x000685A4 File Offset: 0x000667A4
		public NKMDamageEffectData()
		{
			this.Init();
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x000685F0 File Offset: 0x000667F0
		public void Init()
		{
			this.m_MasterGameUnitUID = 0;
			this.m_TargetGameUnitUID = 0;
			this.m_NKM_TEAM_TYPE = NKM_TEAM_TYPE.NTT_INVALID;
			this.m_MasterUnit = null;
			this.m_StatData = null;
			this.m_UnitData = null;
			this.m_bDie = false;
			this.m_fFindTargetTimeNow = 0f;
			this.m_bStateEndStop = false;
			this.m_DamageCountNow = 0;
			this.m_PosXBefore = -1f;
			this.m_PosZBefore = -1f;
			this.m_JumpYPosBefore = -1f;
			this.m_PosX = 0f;
			this.m_PosZ = 0f;
			this.m_JumpYPos = 0f;
			this.m_fLastTargetPosX = 0f;
			this.m_fLastTargetPosZ = 0f;
			this.m_fLastTargetPosJumpY = 0f;
			this.m_fOffsetX = 0f;
			this.m_fOffsetY = 0f;
			this.m_fOffsetZ = 0f;
			this.m_fAddRotate = 0f;
			this.m_bUseZScale = true;
			this.m_fSpeedFactorX = 0f;
			this.m_fSpeedFactorY = 0f;
			this.m_TargetDirSpeed.SetNowValue(this.m_DirVector.x);
			this.m_fDirSpeed = 0f;
			this.m_SpeedX = 0f;
			this.m_SpeedY = 0f;
			this.m_SpeedZ = 0f;
			this.m_DirVector.Set(1f, 0f, 0f);
			this.m_DirVectorTrackX.SetNowValue(this.m_DirVector.x);
			this.m_DirVectorTrackY.SetNowValue(this.m_DirVector.y);
			this.m_DirVectorTrackZ.SetNowValue(this.m_DirVector.z);
			this.m_bRight = true;
			this.m_fSeeTargetTimeNow = 0f;
			this.m_fLifeTimeMax = 0f;
			this.m_bStateFirstFrame = true;
			this.m_fStateTimeBack = 0f;
			this.m_fStateTime = 0f;
			this.m_fAnimTimeBack = 0f;
			this.m_fAnimTime = 0f;
			this.m_fAnimTimeMax = 0f;
			this.m_AnimPlayCount = 0f;
			this.m_bFootOnLand = false;
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x000687FE File Offset: 0x000669FE
		public int GetMasterSkinID()
		{
			if (this.m_MasterUnit == null)
			{
				return 0;
			}
			if (this.m_MasterUnit.GetUnitData() == null)
			{
				return 0;
			}
			return this.m_MasterUnit.GetUnitData().m_SkinID;
		}

		// Token: 0x040011B7 RID: 4535
		public short m_MasterGameUnitUID;

		// Token: 0x040011B8 RID: 4536
		public short m_TargetGameUnitUID;

		// Token: 0x040011B9 RID: 4537
		public NKM_TEAM_TYPE m_NKM_TEAM_TYPE;

		// Token: 0x040011BA RID: 4538
		public NKMUnit m_MasterUnit;

		// Token: 0x040011BB RID: 4539
		public NKMStatData m_StatData;

		// Token: 0x040011BC RID: 4540
		public NKMUnitData m_UnitData;

		// Token: 0x040011BD RID: 4541
		public bool m_bDie;

		// Token: 0x040011BE RID: 4542
		public float m_fFindTargetTimeNow;

		// Token: 0x040011BF RID: 4543
		public bool m_bStateEndStop;

		// Token: 0x040011C0 RID: 4544
		public int m_DamageCountNow;

		// Token: 0x040011C1 RID: 4545
		public float m_PosXBefore;

		// Token: 0x040011C2 RID: 4546
		public float m_PosZBefore;

		// Token: 0x040011C3 RID: 4547
		public float m_JumpYPosBefore;

		// Token: 0x040011C4 RID: 4548
		public float m_PosX;

		// Token: 0x040011C5 RID: 4549
		public float m_PosZ;

		// Token: 0x040011C6 RID: 4550
		public float m_JumpYPos;

		// Token: 0x040011C7 RID: 4551
		public float m_fLastTargetPosX;

		// Token: 0x040011C8 RID: 4552
		public float m_fLastTargetPosZ;

		// Token: 0x040011C9 RID: 4553
		public float m_fLastTargetPosJumpY;

		// Token: 0x040011CA RID: 4554
		public float m_fLastFollowPosX;

		// Token: 0x040011CB RID: 4555
		public float m_fLastFollowPosZ;

		// Token: 0x040011CC RID: 4556
		public float m_fLastFollowPosJumpY;

		// Token: 0x040011CD RID: 4557
		public float m_fOffsetX;

		// Token: 0x040011CE RID: 4558
		public float m_fOffsetY;

		// Token: 0x040011CF RID: 4559
		public float m_fOffsetZ;

		// Token: 0x040011D0 RID: 4560
		public float m_fRotate;

		// Token: 0x040011D1 RID: 4561
		public float m_fAddRotate;

		// Token: 0x040011D2 RID: 4562
		public bool m_bUseZScale;

		// Token: 0x040011D3 RID: 4563
		public float m_fSpeedFactorX;

		// Token: 0x040011D4 RID: 4564
		public float m_fSpeedFactorY;

		// Token: 0x040011D5 RID: 4565
		public NKMTrackingFloat m_TargetDirSpeed = new NKMTrackingFloat();

		// Token: 0x040011D6 RID: 4566
		public float m_fDirSpeed;

		// Token: 0x040011D7 RID: 4567
		public float m_SpeedX;

		// Token: 0x040011D8 RID: 4568
		public float m_SpeedY;

		// Token: 0x040011D9 RID: 4569
		public float m_SpeedZ;

		// Token: 0x040011DA RID: 4570
		public NKMVector3 m_DirVector;

		// Token: 0x040011DB RID: 4571
		public NKMTrackingFloat m_DirVectorTrackX = new NKMTrackingFloat();

		// Token: 0x040011DC RID: 4572
		public NKMTrackingFloat m_DirVectorTrackY = new NKMTrackingFloat();

		// Token: 0x040011DD RID: 4573
		public NKMTrackingFloat m_DirVectorTrackZ = new NKMTrackingFloat();

		// Token: 0x040011DE RID: 4574
		public bool m_bRight;

		// Token: 0x040011DF RID: 4575
		public float m_fSeeTargetTimeNow;

		// Token: 0x040011E0 RID: 4576
		public float m_fLifeTimeMax;

		// Token: 0x040011E1 RID: 4577
		public bool m_bStateFirstFrame;

		// Token: 0x040011E2 RID: 4578
		public float m_fStateTimeBack;

		// Token: 0x040011E3 RID: 4579
		public float m_fStateTime;

		// Token: 0x040011E4 RID: 4580
		public float m_fAnimTimeBack;

		// Token: 0x040011E5 RID: 4581
		public float m_fAnimTime;

		// Token: 0x040011E6 RID: 4582
		public float m_fAnimTimeMax;

		// Token: 0x040011E7 RID: 4583
		public float m_AnimPlayCount;

		// Token: 0x040011E8 RID: 4584
		public bool m_bFootOnLand;

		// Token: 0x040011E9 RID: 4585
		public float m_fFollowTime;

		// Token: 0x040011EA RID: 4586
		public TRACKING_DATA_TYPE m_FollowTrackingDataType = TRACKING_DATA_TYPE.TDT_NORMAL;

		// Token: 0x040011EB RID: 4587
		public float m_fFollowUpdateTime;

		// Token: 0x040011EC RID: 4588
		public float m_fFollowResetTime;
	}
}
