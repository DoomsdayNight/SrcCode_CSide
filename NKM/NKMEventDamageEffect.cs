using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004E5 RID: 1253
	public class NKMEventDamageEffect : IEventConditionOwner, INKMUnitStateEvent
	{
		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06002357 RID: 9047 RVA: 0x000B6803 File Offset: 0x000B4A03
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06002358 RID: 9048 RVA: 0x000B680B File Offset: 0x000B4A0B
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06002359 RID: 9049 RVA: 0x000B6813 File Offset: 0x000B4A13
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Warning;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600235A RID: 9050 RVA: 0x000B6816 File Offset: 0x000B4A16
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x0600235B RID: 9051 RVA: 0x000B681E File Offset: 0x000B4A1E
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x000B6864 File Offset: 0x000B4A64
		public void DeepCopyFromSource(NKMEventDamageEffect source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_bIgnoreTarget = source.m_bIgnoreTarget;
			this.m_bIgnoreNoTarget = source.m_bIgnoreNoTarget;
			this.m_DEName = source.m_DEName;
			this.m_DENamePVP = source.m_DENamePVP;
			this.m_OffsetX = source.m_OffsetX;
			this.m_OffsetY = source.m_OffsetY;
			this.m_OffsetZ = source.m_OffsetZ;
			this.m_fAddRotate = source.m_fAddRotate;
			this.m_bUseZScale = source.m_bUseZScale;
			this.m_fSpeedFactorX = source.m_fSpeedFactorX;
			this.m_fSpeedFactorY = source.m_fSpeedFactorY;
			this.m_bTargetPos = source.m_bTargetPos;
			this.m_bShipSkillPos = source.m_bShipSkillPos;
			this.m_bUseMapPos = source.m_bUseMapPos;
			this.m_fMapPosRate = source.m_fMapPosRate;
			this.m_fReserveTime = source.m_fReserveTime;
			this.m_bStateEndStop = source.m_bStateEndStop;
			this.m_bHold = source.m_bHold;
			this.m_FollowType = source.m_FollowType;
			this.m_FollowTime = source.m_FollowTime;
			this.m_FollowUpdateTime = source.m_FollowUpdateTime;
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x000B69A4 File Offset: 0x000B4BA4
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
			cNKMLua.GetData("m_bIgnoreTarget", ref this.m_bIgnoreTarget);
			cNKMLua.GetData("m_bIgnoreNoTarget", ref this.m_bIgnoreNoTarget);
			cNKMLua.GetData("m_DEName", ref this.m_DEName);
			cNKMLua.GetData("m_DENamePVP", ref this.m_DENamePVP);
			cNKMLua.GetData("m_OffsetX", ref this.m_OffsetX);
			cNKMLua.GetData("m_OffsetY", ref this.m_OffsetY);
			cNKMLua.GetData("m_OffsetZ", ref this.m_OffsetZ);
			cNKMLua.GetData("m_fAddRotate", ref this.m_fAddRotate);
			cNKMLua.GetData("m_bUseZScale", ref this.m_bUseZScale);
			cNKMLua.GetData("m_fSpeedFactorX", ref this.m_fSpeedFactorX);
			cNKMLua.GetData("m_fSpeedFactorY", ref this.m_fSpeedFactorY);
			cNKMLua.GetData("m_bTargetPos", ref this.m_bTargetPos);
			cNKMLua.GetData("m_bShipSkillPos", ref this.m_bShipSkillPos);
			cNKMLua.GetData("m_bUseMapPos", ref this.m_bUseMapPos);
			cNKMLua.GetData("m_fMapPosRate", ref this.m_fMapPosRate);
			cNKMLua.GetData("m_fReserveTime", ref this.m_fReserveTime);
			cNKMLua.GetData("m_bStateEndStop", ref this.m_bStateEndStop);
			cNKMLua.GetData("m_bHold", ref this.m_bHold);
			cNKMLua.GetData<TRACKING_DATA_TYPE>("m_FollowType", ref this.m_FollowType);
			cNKMLua.GetData("m_FollowTime", ref this.m_FollowTime);
			cNKMLua.GetData("m_FollowUpdateTime", ref this.m_FollowUpdateTime);
			return true;
		}

		// Token: 0x040024DB RID: 9435
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x040024DC RID: 9436
		public bool m_bAnimTime = true;

		// Token: 0x040024DD RID: 9437
		public float m_fEventTime;

		// Token: 0x040024DE RID: 9438
		public bool m_bStateEndTime;

		// Token: 0x040024DF RID: 9439
		public bool m_bIgnoreTarget;

		// Token: 0x040024E0 RID: 9440
		public bool m_bIgnoreNoTarget;

		// Token: 0x040024E1 RID: 9441
		public string m_DEName = "";

		// Token: 0x040024E2 RID: 9442
		public string m_DENamePVP = "";

		// Token: 0x040024E3 RID: 9443
		public float m_OffsetX;

		// Token: 0x040024E4 RID: 9444
		public float m_OffsetY;

		// Token: 0x040024E5 RID: 9445
		public float m_OffsetZ;

		// Token: 0x040024E6 RID: 9446
		public float m_fAddRotate;

		// Token: 0x040024E7 RID: 9447
		public bool m_bUseZScale = true;

		// Token: 0x040024E8 RID: 9448
		public float m_fSpeedFactorX;

		// Token: 0x040024E9 RID: 9449
		public float m_fSpeedFactorY;

		// Token: 0x040024EA RID: 9450
		public bool m_bTargetPos;

		// Token: 0x040024EB RID: 9451
		public bool m_bShipSkillPos;

		// Token: 0x040024EC RID: 9452
		public bool m_bUseMapPos;

		// Token: 0x040024ED RID: 9453
		public float m_fMapPosRate;

		// Token: 0x040024EE RID: 9454
		public float m_fReserveTime;

		// Token: 0x040024EF RID: 9455
		public bool m_bStateEndStop;

		// Token: 0x040024F0 RID: 9456
		public bool m_bHold;

		// Token: 0x040024F1 RID: 9457
		public TRACKING_DATA_TYPE m_FollowType = TRACKING_DATA_TYPE.TDT_NORMAL;

		// Token: 0x040024F2 RID: 9458
		public float m_FollowTime;

		// Token: 0x040024F3 RID: 9459
		public float m_FollowUpdateTime;
	}
}
