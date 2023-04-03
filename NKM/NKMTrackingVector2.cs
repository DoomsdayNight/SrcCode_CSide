using System;

namespace NKM
{
	// Token: 0x0200047F RID: 1151
	public class NKMTrackingVector2
	{
		// Token: 0x06001F4F RID: 8015 RVA: 0x00094B10 File Offset: 0x00092D10
		public NKMTrackingVector2()
		{
			this.m_NowValue = default(NKMVector2);
			this.m_BeforeValue = default(NKMVector2);
			this.m_StartValue = default(NKMVector2);
			this.m_TargetValue = default(NKMVector2);
			this.m_fTime = 0f;
			this.m_fDeltaTime = 0f;
			this.m_eTrackingType = TRACKING_DATA_TYPE.TDT_NORMAL;
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x00094B70 File Offset: 0x00092D70
		public void SetNowValue(NKMVector2 NowValue)
		{
			this.m_NowValue = NowValue;
			this.m_TargetValue = NowValue;
			this.m_fDeltaTime = this.m_fTime;
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x00094B8C File Offset: 0x00092D8C
		public NKMVector2 GetNowValue()
		{
			return this.m_NowValue;
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x00094B94 File Offset: 0x00092D94
		public NKMVector2 GetDelta()
		{
			return this.m_NowValue - this.m_BeforeValue;
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x00094BA7 File Offset: 0x00092DA7
		public NKMVector2 GetBeforeValue()
		{
			return this.m_BeforeValue;
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x00094BAF File Offset: 0x00092DAF
		public NKMVector2 GetTargetValue()
		{
			return this.m_TargetValue;
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x00094BB7 File Offset: 0x00092DB7
		public bool IsTracking()
		{
			return this.m_fDeltaTime < this.m_fTime;
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x00094BC8 File Offset: 0x00092DC8
		public void Update(float deltaTime)
		{
			if (this.m_fDeltaTime < this.m_fTime)
			{
				this.m_fDeltaTime += deltaTime;
				float num = NKMTrackingFloat.TrackRatio(this.m_eTrackingType, this.m_fDeltaTime, this.m_fTime, 3f);
				this.m_BeforeValue = this.m_NowValue;
				this.m_NowValue.x = this.m_StartValue.x + (this.m_TargetValue.x - this.m_StartValue.x) * num;
				this.m_NowValue.y = this.m_StartValue.y + (this.m_TargetValue.y - this.m_StartValue.y) * num;
				if (this.m_fDeltaTime >= this.m_fTime)
				{
					this.m_fDeltaTime = this.m_fTime;
					this.m_NowValue = this.m_TargetValue;
					this.m_BeforeValue = this.m_TargetValue;
				}
			}
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x00094CAF File Offset: 0x00092EAF
		public void StopTracking()
		{
			this.m_fDeltaTime = this.m_fTime;
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x00094CBD File Offset: 0x00092EBD
		public void SetTracking(NKMVector2 targetVal, float fTime, TRACKING_DATA_TYPE eTrackingType)
		{
			this.m_StartValue = this.m_NowValue;
			this.m_TargetValue = targetVal;
			this.m_fTime = fTime;
			this.m_fDeltaTime = 0f;
			this.m_eTrackingType = eTrackingType;
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x00094CEB File Offset: 0x00092EEB
		public float GetTime()
		{
			return this.m_fTime;
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x00094CF3 File Offset: 0x00092EF3
		public float GetDeltaTime()
		{
			return this.m_fDeltaTime;
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x00094CFB File Offset: 0x00092EFB
		public TRACKING_DATA_TYPE GetTrackingType()
		{
			return this.m_eTrackingType;
		}

		// Token: 0x04001FD7 RID: 8151
		private NKMVector2 m_NowValue;

		// Token: 0x04001FD8 RID: 8152
		private NKMVector2 m_BeforeValue;

		// Token: 0x04001FD9 RID: 8153
		private NKMVector2 m_StartValue;

		// Token: 0x04001FDA RID: 8154
		private NKMVector2 m_TargetValue;

		// Token: 0x04001FDB RID: 8155
		private float m_fTime;

		// Token: 0x04001FDC RID: 8156
		private float m_fDeltaTime;

		// Token: 0x04001FDD RID: 8157
		private TRACKING_DATA_TYPE m_eTrackingType;
	}
}
