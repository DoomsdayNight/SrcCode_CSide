using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004DE RID: 1246
	public class NKMEventCameraMove : IEventConditionOwner, INKMUnitStateEvent
	{
		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06002323 RID: 8995 RVA: 0x000B5AB5 File Offset: 0x000B3CB5
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06002324 RID: 8996 RVA: 0x000B5ABD File Offset: 0x000B3CBD
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTimeMin;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06002325 RID: 8997 RVA: 0x000B5AC5 File Offset: 0x000B3CC5
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06002326 RID: 8998 RVA: 0x000B5AC8 File Offset: 0x000B3CC8
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06002327 RID: 8999 RVA: 0x000B5AD0 File Offset: 0x000B3CD0
		public bool bStateEnd
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x000B5AF8 File Offset: 0x000B3CF8
		public void DeepCopyFromSource(NKMEventCameraMove source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTimeMin = source.m_fEventTimeMin;
			this.m_fEventTimeMax = source.m_fEventTimeMax;
			this.m_bForce = source.m_bForce;
			this.m_fPosXOffset = source.m_fPosXOffset;
			this.m_fPosYOffset = source.m_fPosYOffset;
			this.m_fZoom = source.m_fZoom;
			this.m_fFocusBlur = source.m_fFocusBlur;
			this.m_fMoveTrackingTime = source.m_fMoveTrackingTime;
			this.m_fZoomTrackingTime = source.m_fZoomTrackingTime;
			this.m_fCameraRadius = source.m_fCameraRadius;
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x000B5B9C File Offset: 0x000B3D9C
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			if (cNKMLua.OpenTable("m_Condition"))
			{
				this.m_Condition.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_bAnimTime", ref this.m_bAnimTime);
			cNKMLua.GetData("m_fEventTimeMin", ref this.m_fEventTimeMin);
			cNKMLua.GetData("m_fEventTimeMax", ref this.m_fEventTimeMax);
			cNKMLua.GetData("m_bForce", ref this.m_bForce);
			cNKMLua.GetData("m_fPosXOffset", ref this.m_fPosXOffset);
			cNKMLua.GetData("m_fPosYOffset", ref this.m_fPosYOffset);
			cNKMLua.GetData("m_fZoom", ref this.m_fZoom);
			cNKMLua.GetData("m_fFocusBlur", ref this.m_fFocusBlur);
			cNKMLua.GetData("m_fMoveTrackingTime", ref this.m_fMoveTrackingTime);
			cNKMLua.GetData("m_fZoomTrackingTime", ref this.m_fZoomTrackingTime);
			cNKMLua.GetData("m_fCameraRadius", ref this.m_fCameraRadius);
			return true;
		}

		// Token: 0x0400248D RID: 9357
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x0400248E RID: 9358
		public bool m_bAnimTime = true;

		// Token: 0x0400248F RID: 9359
		public float m_fEventTimeMin;

		// Token: 0x04002490 RID: 9360
		public float m_fEventTimeMax;

		// Token: 0x04002491 RID: 9361
		public bool m_bForce;

		// Token: 0x04002492 RID: 9362
		public float m_fPosXOffset;

		// Token: 0x04002493 RID: 9363
		public float m_fPosYOffset;

		// Token: 0x04002494 RID: 9364
		public float m_fZoom = -1f;

		// Token: 0x04002495 RID: 9365
		public float m_fFocusBlur;

		// Token: 0x04002496 RID: 9366
		public float m_fMoveTrackingTime;

		// Token: 0x04002497 RID: 9367
		public float m_fZoomTrackingTime;

		// Token: 0x04002498 RID: 9368
		public float m_fCameraRadius;
	}
}
