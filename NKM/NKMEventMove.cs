using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004C9 RID: 1225
	public class NKMEventMove : IEventConditionOwner, INKMUnitStateEventRollback, INKMUnitStateEvent
	{
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x0600226A RID: 8810 RVA: 0x000B1DAB File Offset: 0x000AFFAB
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x0600226B RID: 8811 RVA: 0x000B1DB3 File Offset: 0x000AFFB3
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x0600226C RID: 8812 RVA: 0x000B1DB6 File Offset: 0x000AFFB6
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x0600226D RID: 8813 RVA: 0x000B1DBE File Offset: 0x000AFFBE
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x0600226E RID: 8814 RVA: 0x000B1DC6 File Offset: 0x000AFFC6
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x000B1E04 File Offset: 0x000B0004
		public void DeepCopyFromSource(NKMEventMove source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_MoveBase = source.m_MoveBase;
			this.m_MoveOffset = source.m_MoveOffset;
			this.m_OffsetX = source.m_OffsetX;
			this.m_OffsetZ = source.m_OffsetZ;
			this.m_OffsetJumpYPos = source.m_OffsetJumpYPos;
			this.m_fMapPosFactor = source.m_fMapPosFactor;
			this.m_MoveTime = source.m_MoveTime;
			this.m_fSpeed = source.m_fSpeed;
			this.m_MoveTrackingType = source.m_MoveTrackingType;
			this.m_bLandMove = source.m_bLandMove;
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x000B1EC0 File Offset: 0x000B00C0
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
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			cNKMLua.GetData("m_bTargetUnit", ref flag);
			cNKMLua.GetData("m_bDependMyShip", ref flag2);
			cNKMLua.GetData("m_bOffsetDependMe", ref flag3);
			if (flag)
			{
				this.m_MoveBase = NKMEventMove.MoveBase.TARGET_UNIT;
				if (flag3)
				{
					this.m_MoveOffset = NKMEventMove.MoveOffset.ME_INV;
				}
			}
			else
			{
				this.m_MoveBase = NKMEventMove.MoveBase.ME;
			}
			if (flag2)
			{
				this.m_MoveOffset = NKMEventMove.MoveOffset.MY_SHIP_INV;
			}
			if (cNKMLua.GetData("m_fMapPosFactor", ref this.m_fMapPosFactor) && this.m_fMapPosFactor >= 0f)
			{
				this.m_MoveBase = NKMEventMove.MoveBase.MAP_RATE;
			}
			cNKMLua.GetData<NKMEventMove.MoveBase>("m_MoveBase", ref this.m_MoveBase);
			cNKMLua.GetData<NKMEventMove.MoveOffset>("m_MoveOffset", ref this.m_MoveOffset);
			cNKMLua.GetData("m_OffsetX", ref this.m_OffsetX);
			cNKMLua.GetData("m_OffsetZ", ref this.m_OffsetZ);
			cNKMLua.GetData("m_OffsetJumpYPos", ref this.m_OffsetJumpYPos);
			cNKMLua.GetData("m_MoveTime", ref this.m_MoveTime);
			cNKMLua.GetData("m_fSpeed", ref this.m_fSpeed);
			cNKMLua.GetData<TRACKING_DATA_TYPE>("m_MoveTrackingType", ref this.m_MoveTrackingType);
			cNKMLua.GetData("m_bLandMove", ref this.m_bLandMove);
			cNKMLua.GetData("m_bSavePosition", ref this.m_bSavePosition);
			return true;
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x000B2058 File Offset: 0x000B0258
		public void ProcessEventRollback(NKMGame cNKMGame, NKMUnit cNKMUnit, float rollbackTime)
		{
			if (this.m_bAnimTime && cNKMUnit.GetUnitFrameData().m_fAnimSpeed == 0f)
			{
				return;
			}
			cNKMUnit.m_EventMovePosX.StopTracking();
			cNKMUnit.m_EventMovePosJumpY.StopTracking();
			cNKMUnit.m_EventMovePosZ.StopTracking();
			float eventMovePosX = cNKMUnit.GetEventMovePosX(this, cNKMUnit.IsATeam());
			float eventMovePosY = cNKMUnit.GetEventMovePosY(this);
			float num;
			if (this.m_bAnimTime)
			{
				num = this.m_fEventTime / cNKMUnit.GetUnitFrameData().m_fAnimSpeed;
			}
			else
			{
				num = this.m_fEventTime;
			}
			float num2 = this.m_fEventTime + this.m_MoveTime;
			if (this.m_MoveTime == 0f || rollbackTime >= num2)
			{
				cNKMUnit.GetUnitFrameData().m_PosXCalc = eventMovePosX;
				if (!this.m_bLandMove)
				{
					cNKMUnit.GetUnitFrameData().m_JumpYPosCalc = eventMovePosY;
					return;
				}
			}
			else
			{
				float deltaTime = rollbackTime - num;
				cNKMUnit.m_EventMovePosX.SetNowValue(cNKMUnit.GetUnitFrameData().m_PosXCalc);
				cNKMUnit.m_EventMovePosX.SetTracking(eventMovePosX, this.m_MoveTime, this.m_MoveTrackingType);
				cNKMUnit.m_EventMovePosX.Update(deltaTime);
				if (!this.m_bLandMove)
				{
					cNKMUnit.m_EventMovePosJumpY.SetNowValue(cNKMUnit.GetUnitFrameData().m_JumpYPosCalc);
					cNKMUnit.m_EventMovePosJumpY.SetTracking(eventMovePosY, this.m_MoveTime, this.m_MoveTrackingType);
					cNKMUnit.m_EventMovePosJumpY.Update(deltaTime);
				}
			}
		}

		// Token: 0x040023AD RID: 9133
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x040023AE RID: 9134
		public bool m_bAnimTime = true;

		// Token: 0x040023AF RID: 9135
		public float m_fEventTime;

		// Token: 0x040023B0 RID: 9136
		public bool m_bStateEndTime;

		// Token: 0x040023B1 RID: 9137
		public bool m_bSavePosition;

		// Token: 0x040023B2 RID: 9138
		public NKMEventMove.MoveBase m_MoveBase;

		// Token: 0x040023B3 RID: 9139
		public NKMEventMove.MoveOffset m_MoveOffset = NKMEventMove.MoveOffset.MY_LOOK_DIR;

		// Token: 0x040023B4 RID: 9140
		public float m_OffsetX;

		// Token: 0x040023B5 RID: 9141
		public float m_OffsetZ;

		// Token: 0x040023B6 RID: 9142
		public float m_OffsetJumpYPos;

		// Token: 0x040023B7 RID: 9143
		public float m_fMapPosFactor = -1f;

		// Token: 0x040023B8 RID: 9144
		public float m_MoveTime;

		// Token: 0x040023B9 RID: 9145
		public float m_fSpeed;

		// Token: 0x040023BA RID: 9146
		public TRACKING_DATA_TYPE m_MoveTrackingType = TRACKING_DATA_TYPE.TDT_SLOWER;

		// Token: 0x040023BB RID: 9147
		public bool m_bLandMove;

		// Token: 0x02001225 RID: 4645
		public enum MoveBase
		{
			// Token: 0x040094BF RID: 38079
			ME,
			// Token: 0x040094C0 RID: 38080
			TARGET_UNIT,
			// Token: 0x040094C1 RID: 38081
			SUB_TARGET_UNIT,
			// Token: 0x040094C2 RID: 38082
			MASTER_UNIT,
			// Token: 0x040094C3 RID: 38083
			MAP_RATE,
			// Token: 0x040094C4 RID: 38084
			MY_SHIP,
			// Token: 0x040094C5 RID: 38085
			ENEMY_SHIP,
			// Token: 0x040094C6 RID: 38086
			SAVE_ONLY,
			// Token: 0x040094C7 RID: 38087
			SAVED_POS
		}

		// Token: 0x02001226 RID: 4646
		public enum MoveOffset
		{
			// Token: 0x040094C9 RID: 38089
			ME,
			// Token: 0x040094CA RID: 38090
			ME_INV,
			// Token: 0x040094CB RID: 38091
			MY_LOOK_DIR,
			// Token: 0x040094CC RID: 38092
			TARGET_UNIT,
			// Token: 0x040094CD RID: 38093
			TARGET_UNIT_INV,
			// Token: 0x040094CE RID: 38094
			SUB_TARGET_UNIT,
			// Token: 0x040094CF RID: 38095
			MASTER_UNIT,
			// Token: 0x040094D0 RID: 38096
			MY_SHIP,
			// Token: 0x040094D1 RID: 38097
			MY_SHIP_INV,
			// Token: 0x040094D2 RID: 38098
			ENEMY_SHIP,
			// Token: 0x040094D3 RID: 38099
			ENEMY_SHIP_INV,
			// Token: 0x040094D4 RID: 38100
			TEAM_DIR,
			// Token: 0x040094D5 RID: 38101
			MAP_RATE,
			// Token: 0x040094D6 RID: 38102
			SAVED_POS
		}
	}
}
