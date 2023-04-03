using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004DD RID: 1245
	public class NKMEventCameraCrash : IEventConditionOwner, INKMUnitStateEvent
	{
		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x0600231B RID: 8987 RVA: 0x000B5913 File Offset: 0x000B3B13
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x0600231C RID: 8988 RVA: 0x000B591B File Offset: 0x000B3B1B
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x0600231D RID: 8989 RVA: 0x000B5923 File Offset: 0x000B3B23
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x0600231E RID: 8990 RVA: 0x000B5926 File Offset: 0x000B3B26
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x0600231F RID: 8991 RVA: 0x000B592E File Offset: 0x000B3B2E
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x000B5958 File Offset: 0x000B3B58
		public void DeepCopyFromSource(NKMEventCameraCrash source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_CameraCrashType = source.m_CameraCrashType;
			this.m_fCameraCrashSpeed = source.m_fCameraCrashSpeed;
			this.m_fCameraCrashAccel = source.m_fCameraCrashAccel;
			this.m_fCameraCrashGap = source.m_fCameraCrashGap;
			this.m_fCameraCrashTime = source.m_fCameraCrashTime;
			this.m_fCrashRadius = source.m_fCrashRadius;
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x000B59E4 File Offset: 0x000B3BE4
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
			cNKMLua.GetData<NKM_CAMERA_CRASH_TYPE>("m_CameraCrashType", ref this.m_CameraCrashType);
			cNKMLua.GetData("m_fCameraCrashSpeed", ref this.m_fCameraCrashSpeed);
			cNKMLua.GetData("m_fCameraCrashAccel", ref this.m_fCameraCrashAccel);
			cNKMLua.GetData("m_fCameraCrashGap", ref this.m_fCameraCrashGap);
			cNKMLua.GetData("m_fCameraCrashTime", ref this.m_fCameraCrashTime);
			cNKMLua.GetData("m_fCrashRadius", ref this.m_fCrashRadius);
			return true;
		}

		// Token: 0x04002483 RID: 9347
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x04002484 RID: 9348
		public bool m_bAnimTime = true;

		// Token: 0x04002485 RID: 9349
		public float m_fEventTime;

		// Token: 0x04002486 RID: 9350
		public bool m_bStateEndTime;

		// Token: 0x04002487 RID: 9351
		public NKM_CAMERA_CRASH_TYPE m_CameraCrashType = NKM_CAMERA_CRASH_TYPE.NCCT_DOWN;

		// Token: 0x04002488 RID: 9352
		public float m_fCameraCrashSpeed;

		// Token: 0x04002489 RID: 9353
		public float m_fCameraCrashAccel;

		// Token: 0x0400248A RID: 9354
		public float m_fCameraCrashGap;

		// Token: 0x0400248B RID: 9355
		public float m_fCameraCrashTime;

		// Token: 0x0400248C RID: 9356
		public float m_fCrashRadius;
	}
}
