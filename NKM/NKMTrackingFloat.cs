using System;

namespace NKM
{
	// Token: 0x0200047E RID: 1150
	public class NKMTrackingFloat
	{
		// Token: 0x06001F3F RID: 7999 RVA: 0x0009479C File Offset: 0x0009299C
		public NKMTrackingFloat()
		{
			this.Init();
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x000947AC File Offset: 0x000929AC
		public void Init()
		{
			this.m_NowValue = 0f;
			this.m_BeforeValue = 0f;
			this.m_StartValue = 0f;
			this.m_TargetValue = 0f;
			this.m_fTime = 0f;
			this.m_fDeltaTime = 0f;
			this.m_eTrackingType = TRACKING_DATA_TYPE.TDT_NORMAL;
			this.m_fIntensity = 3f;
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x0009480D File Offset: 0x00092A0D
		public void SetNowValue(float NowValue)
		{
			this.m_NowValue = NowValue;
			this.m_TargetValue = NowValue;
			this.m_fDeltaTime = this.m_fTime;
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x00094829 File Offset: 0x00092A29
		public float GetNowValue()
		{
			return this.m_NowValue;
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x00094831 File Offset: 0x00092A31
		public float GetDelta()
		{
			return this.m_NowValue - this.m_BeforeValue;
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x00094840 File Offset: 0x00092A40
		public float GetBeforeValue()
		{
			return this.m_BeforeValue;
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x00094848 File Offset: 0x00092A48
		public float GetTargetValue()
		{
			return this.m_TargetValue;
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x00094850 File Offset: 0x00092A50
		public bool IsTracking()
		{
			return this.m_fDeltaTime < this.m_fTime || this.m_bTrackingLastFrame;
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x00094868 File Offset: 0x00092A68
		public void SetIntensity(float fValue)
		{
			this.m_fIntensity = fValue;
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x00094874 File Offset: 0x00092A74
		public void Update(float deltaTime)
		{
			NKMProfiler.BeginSample("NKMTrackingFloat.Update");
			if (this.m_fDeltaTime < this.m_fTime)
			{
				this.m_fDeltaTime += deltaTime;
				float num = NKMTrackingFloat.TrackRatio(this.m_eTrackingType, this.m_fDeltaTime, this.m_fTime, this.m_fIntensity);
				this.m_BeforeValue = this.m_NowValue;
				this.m_NowValue = this.m_StartValue + (this.m_TargetValue - this.m_StartValue) * num;
				if (this.m_fDeltaTime >= this.m_fTime)
				{
					this.m_fDeltaTime = this.m_fTime;
					this.m_NowValue = this.m_TargetValue;
					this.m_BeforeValue = this.m_TargetValue;
					this.m_bTrackingLastFrame = true;
				}
			}
			else
			{
				this.m_bTrackingLastFrame = false;
			}
			NKMProfiler.EndSample();
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x00094938 File Offset: 0x00092B38
		public static float TrackRatio(TRACKING_DATA_TYPE type, float deltaTime, float endTime, float Intensity = 3f)
		{
			float num = deltaTime / endTime;
			switch (type)
			{
			case TRACKING_DATA_TYPE.TDT_FASTER:
				return (float)Math.Pow((double)num, (double)Intensity);
			case TRACKING_DATA_TYPE.TDT_SLOWER:
				return 1f - (float)Math.Pow((double)(1f - num), 3.0);
			case TRACKING_DATA_TYPE.TDT_SIN:
				return (float)Math.Sin((double)NKMUtil.NKMToRadian(deltaTime * 50f));
			case TRACKING_DATA_TYPE.TDT_SIN_PLUS:
				return Math.Abs((float)Math.Sin((double)NKMUtil.NKMToRadian(deltaTime * 100f))) * 0.5f;
			case TRACKING_DATA_TYPE.TDT_SIN_PLUS_FAST:
				return Math.Abs((float)Math.Sin((double)NKMUtil.NKMToRadian(deltaTime * 100f)));
			case TRACKING_DATA_TYPE.TDT_SIN_PLUS_FAST2:
				return Math.Abs((float)Math.Sin((double)NKMUtil.NKMToRadian(deltaTime * 200f)));
			case TRACKING_DATA_TYPE.TDT_SIN_PLUS_FAST4:
				return Math.Abs((float)Math.Sin((double)NKMUtil.NKMToRadian(deltaTime * 400f)));
			case TRACKING_DATA_TYPE.TDT_BACK_OUT:
				return (num -= 1f) * num * (2.70158f * num + 1.70158f) + 1f;
			case TRACKING_DATA_TYPE.TDT_BOUNCE_OUT:
				if ((double)num < 0.36363636363636365)
				{
					return 7.5625f * num * num;
				}
				if ((double)num < 0.7272727272727273)
				{
					return 7.5625f * (num -= 0.54545456f) * num + 0.75f;
				}
				if ((double)num < 0.9090909090909091)
				{
					return 7.5625f * (num -= 0.8181818f) * num + 0.9375f;
				}
				return 7.5625f * (num -= 0.95454544f) * num + 0.984375f;
			default:
				return num;
			}
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x00094ABB File Offset: 0x00092CBB
		public void StopTracking()
		{
			this.m_fDeltaTime = this.m_fTime;
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x00094AC9 File Offset: 0x00092CC9
		public void SetTracking(float targetVal, float fTime, TRACKING_DATA_TYPE eTrackingType)
		{
			this.m_StartValue = this.m_NowValue;
			this.m_TargetValue = targetVal;
			this.m_fTime = fTime;
			this.m_fDeltaTime = 0f;
			this.m_eTrackingType = eTrackingType;
		}

		// Token: 0x06001F4C RID: 8012 RVA: 0x00094AF7 File Offset: 0x00092CF7
		public float GetTime()
		{
			return this.m_fTime;
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x00094AFF File Offset: 0x00092CFF
		public float GetDeltaTime()
		{
			return this.m_fDeltaTime;
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x00094B07 File Offset: 0x00092D07
		public TRACKING_DATA_TYPE GetTrackingType()
		{
			return this.m_eTrackingType;
		}

		// Token: 0x04001FCE RID: 8142
		private float m_NowValue;

		// Token: 0x04001FCF RID: 8143
		private float m_BeforeValue;

		// Token: 0x04001FD0 RID: 8144
		private float m_StartValue;

		// Token: 0x04001FD1 RID: 8145
		private float m_TargetValue;

		// Token: 0x04001FD2 RID: 8146
		private float m_fTime;

		// Token: 0x04001FD3 RID: 8147
		private float m_fDeltaTime;

		// Token: 0x04001FD4 RID: 8148
		private bool m_bTrackingLastFrame;

		// Token: 0x04001FD5 RID: 8149
		private TRACKING_DATA_TYPE m_eTrackingType;

		// Token: 0x04001FD6 RID: 8150
		private float m_fIntensity;
	}
}
