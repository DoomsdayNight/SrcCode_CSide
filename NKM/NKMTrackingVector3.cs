using System;

namespace NKM
{
	// Token: 0x02000480 RID: 1152
	public class NKMTrackingVector3
	{
		// Token: 0x06001F5C RID: 8028 RVA: 0x00094D04 File Offset: 0x00092F04
		public NKMTrackingVector3()
		{
			this.m_NowValue = default(NKMVector3);
			this.m_BeforeValue = default(NKMVector3);
			this.m_StartValue = default(NKMVector3);
			this.m_TargetValue = default(NKMVector3);
			this.m_fTime = 0f;
			this.m_fDeltaTime = 0f;
			this.m_eTrackingType = TRACKING_DATA_TYPE.TDT_NORMAL;
			this.m_bPause = false;
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x00094D6B File Offset: 0x00092F6B
		public void SetPause(bool bSet)
		{
			this.m_bPause = bSet;
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x00094D74 File Offset: 0x00092F74
		public bool GetPause()
		{
			return this.m_bPause;
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x00094D7C File Offset: 0x00092F7C
		public void SetNowValue(float fX, float fY, float fZ)
		{
			this.m_NowValue.x = fX;
			this.m_NowValue.y = fY;
			this.m_NowValue.z = fZ;
			this.m_TargetValue = this.m_NowValue;
			this.m_fDeltaTime = this.m_fTime;
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x00094DBA File Offset: 0x00092FBA
		public NKMVector3 GetNowValue()
		{
			return this.m_NowValue;
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x00094DC2 File Offset: 0x00092FC2
		public float GetNowValueX()
		{
			return this.m_NowValue.x;
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x00094DCF File Offset: 0x00092FCF
		public float GetNowValueY()
		{
			return this.m_NowValue.y;
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x00094DDC File Offset: 0x00092FDC
		public float GetNowValueZ()
		{
			return this.m_NowValue.z;
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x00094DE9 File Offset: 0x00092FE9
		public NKMVector3 GetDelta()
		{
			return this.m_NowValue - this.m_BeforeValue;
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x00094DFC File Offset: 0x00092FFC
		public NKMVector3 GetBeforeValue()
		{
			return this.m_BeforeValue;
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x00094E04 File Offset: 0x00093004
		public NKMVector3 GetTargetValue()
		{
			return this.m_TargetValue;
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x00094E0C File Offset: 0x0009300C
		public bool IsTracking()
		{
			return this.m_fDeltaTime < this.m_fTime;
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x00094E1C File Offset: 0x0009301C
		public void Update(float deltaTime)
		{
			if (this.m_fDeltaTime < this.m_fTime)
			{
				if (this.m_bPause)
				{
					return;
				}
				this.m_fDeltaTime += deltaTime;
				float num = NKMTrackingFloat.TrackRatio(this.m_eTrackingType, this.m_fDeltaTime, this.m_fTime, 3f);
				this.m_BeforeValue = this.m_NowValue;
				this.m_NowValue.x = this.m_StartValue.x + (this.m_TargetValue.x - this.m_StartValue.x) * num;
				this.m_NowValue.y = this.m_StartValue.y + (this.m_TargetValue.y - this.m_StartValue.y) * num;
				this.m_NowValue.z = this.m_StartValue.z + (this.m_TargetValue.z - this.m_StartValue.z) * num;
				if (this.m_fDeltaTime >= this.m_fTime)
				{
					this.m_fDeltaTime = this.m_fTime;
					this.m_NowValue = this.m_TargetValue;
					this.m_BeforeValue = this.m_TargetValue;
				}
			}
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x00094F3C File Offset: 0x0009313C
		public void StopTracking()
		{
			this.m_fDeltaTime = this.m_fTime;
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x00094F4A File Offset: 0x0009314A
		public void SetTracking(NKMVector3 targetVal, float fTime, TRACKING_DATA_TYPE eTrackingType)
		{
			this.SetTracking(targetVal.x, targetVal.y, targetVal.z, fTime, eTrackingType);
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x00094F66 File Offset: 0x00093166
		public void SetTracking(float fX, float fY, float fZ, float fTime, TRACKING_DATA_TYPE eTrackingType)
		{
			this.m_bPause = false;
			this.m_StartValue = this.m_NowValue;
			this.m_TargetValue.Set(fX, fY, fZ);
			this.m_fTime = fTime;
			this.m_fDeltaTime = 0f;
			this.m_eTrackingType = eTrackingType;
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x00094FA4 File Offset: 0x000931A4
		public float GetTime()
		{
			return this.m_fTime;
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x00094FAC File Offset: 0x000931AC
		public float GetDeltaTime()
		{
			return this.m_fDeltaTime;
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x00094FB4 File Offset: 0x000931B4
		public TRACKING_DATA_TYPE GetTrackingType()
		{
			return this.m_eTrackingType;
		}

		// Token: 0x04001FDE RID: 8158
		private NKMVector3 m_NowValue;

		// Token: 0x04001FDF RID: 8159
		private NKMVector3 m_BeforeValue;

		// Token: 0x04001FE0 RID: 8160
		private NKMVector3 m_StartValue;

		// Token: 0x04001FE1 RID: 8161
		private NKMVector3 m_TargetValue;

		// Token: 0x04001FE2 RID: 8162
		private float m_fTime;

		// Token: 0x04001FE3 RID: 8163
		private float m_fDeltaTime;

		// Token: 0x04001FE4 RID: 8164
		private TRACKING_DATA_TYPE m_eTrackingType;

		// Token: 0x04001FE5 RID: 8165
		private bool m_bPause;
	}
}
