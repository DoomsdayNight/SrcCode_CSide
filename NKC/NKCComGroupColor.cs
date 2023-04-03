using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000619 RID: 1561
	public class NKCComGroupColor : MonoBehaviour
	{
		// Token: 0x06003041 RID: 12353 RVA: 0x000ED8E9 File Offset: 0x000EBAE9
		private void Awake()
		{
			this.InitImage();
			this.InitSpriteRenderer();
			this.SetColor(1f, 1f, 1f, 1f, 0f);
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x000ED916 File Offset: 0x000EBB16
		private void Start()
		{
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x000ED918 File Offset: 0x000EBB18
		private void InitImage()
		{
			if (this.m_ImageArray != null)
			{
				return;
			}
			this.m_ImageArray = base.gameObject.GetComponentsInChildren<Image>(true);
			this.m_ImageColorOrgArray = new Color[this.m_ImageArray.Length];
			for (int i = 0; i < this.m_ImageArray.Length; i++)
			{
				this.m_ImageColorOrgArray[i] = this.m_ImageArray[i].color;
			}
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x000ED980 File Offset: 0x000EBB80
		private void InitSpriteRenderer()
		{
			if (this.m_SpriteRendererArray != null)
			{
				return;
			}
			this.m_SpriteRendererArray = base.gameObject.GetComponentsInChildren<SpriteRenderer>(true);
			this.m_SpriteRendererColorOrgArray = new Color[this.m_SpriteRendererArray.Length];
			for (int i = 0; i < this.m_SpriteRendererArray.Length; i++)
			{
				this.m_SpriteRendererColorOrgArray[i] = this.m_SpriteRendererArray[i].color;
			}
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x000ED9E8 File Offset: 0x000EBBE8
		private void Update()
		{
			this.m_fR.Update(Time.deltaTime);
			this.m_fG.Update(Time.deltaTime);
			this.m_fB.Update(Time.deltaTime);
			this.m_fA.Update(Time.deltaTime);
			if (this.m_fR.IsTracking() || this.m_fG.IsTracking() || this.m_fB.IsTracking() || this.m_fA.IsTracking())
			{
				this.UpdateColor();
			}
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x000EDA6F File Offset: 0x000EBC6F
		public void SetColor(Color color, float fTime = 0f)
		{
			this.SetColor(color.r, color.g, color.b, color.a, fTime);
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x000EDA90 File Offset: 0x000EBC90
		public void SetColor(float fR = -1f, float fG = -1f, float fB = -1f, float fA = -1f, float fTime = 0f)
		{
			bool flag = false;
			if (fR != -1f && this.m_fR.GetNowValue() != fR)
			{
				if (fTime == 0f)
				{
					this.m_fR.SetNowValue(fR);
				}
				else
				{
					this.m_fR.SetTracking(fR, fTime, TRACKING_DATA_TYPE.TDT_NORMAL);
				}
				flag = true;
			}
			if (fG != -1f && this.m_fG.GetNowValue() != fG)
			{
				if (fTime == 0f)
				{
					this.m_fG.SetNowValue(fG);
				}
				else
				{
					this.m_fG.SetTracking(fG, fTime, TRACKING_DATA_TYPE.TDT_NORMAL);
				}
				flag = true;
			}
			if (fB != -1f && this.m_fB.GetNowValue() != fB)
			{
				if (fTime == 0f)
				{
					this.m_fB.SetNowValue(fB);
				}
				else
				{
					this.m_fB.SetTracking(fB, fTime, TRACKING_DATA_TYPE.TDT_NORMAL);
				}
				flag = true;
			}
			if (fA != -1f && this.m_fA.GetNowValue() != fA)
			{
				if (fTime == 0f)
				{
					this.m_fA.SetNowValue(fA);
				}
				else
				{
					this.m_fA.SetTracking(fA, fTime, TRACKING_DATA_TYPE.TDT_NORMAL);
				}
				flag = true;
			}
			if (flag)
			{
				this.UpdateColor();
			}
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x000EDBA4 File Offset: 0x000EBDA4
		private void UpdateColor()
		{
			if (this.m_ImageArray != null)
			{
				for (int i = 0; i < this.m_ImageArray.Length; i++)
				{
					this.m_ColorTemp.r = this.m_ImageColorOrgArray[i].r * this.m_fR.GetNowValue();
					this.m_ColorTemp.g = this.m_ImageColorOrgArray[i].g * this.m_fG.GetNowValue();
					this.m_ColorTemp.b = this.m_ImageColorOrgArray[i].b * this.m_fB.GetNowValue();
					this.m_ColorTemp.a = this.m_ImageColorOrgArray[i].a * this.m_fA.GetNowValue();
					this.m_ImageArray[i].color = this.m_ColorTemp;
				}
			}
			if (this.m_SpriteRendererArray != null)
			{
				for (int j = 0; j < this.m_SpriteRendererArray.Length; j++)
				{
					this.m_ColorTemp.r = this.m_SpriteRendererColorOrgArray[j].r * this.m_fR.GetNowValue();
					this.m_ColorTemp.g = this.m_SpriteRendererColorOrgArray[j].g * this.m_fG.GetNowValue();
					this.m_ColorTemp.b = this.m_SpriteRendererColorOrgArray[j].b * this.m_fB.GetNowValue();
					this.m_ColorTemp.a = this.m_SpriteRendererColorOrgArray[j].a * this.m_fA.GetNowValue();
					this.m_SpriteRendererArray[j].color = this.m_ColorTemp;
				}
			}
		}

		// Token: 0x04002FA3 RID: 12195
		private Image[] m_ImageArray;

		// Token: 0x04002FA4 RID: 12196
		private Color[] m_ImageColorOrgArray;

		// Token: 0x04002FA5 RID: 12197
		private SpriteRenderer[] m_SpriteRendererArray;

		// Token: 0x04002FA6 RID: 12198
		private Color[] m_SpriteRendererColorOrgArray;

		// Token: 0x04002FA7 RID: 12199
		private NKMTrackingFloat m_fR = new NKMTrackingFloat();

		// Token: 0x04002FA8 RID: 12200
		private NKMTrackingFloat m_fG = new NKMTrackingFloat();

		// Token: 0x04002FA9 RID: 12201
		private NKMTrackingFloat m_fB = new NKMTrackingFloat();

		// Token: 0x04002FAA RID: 12202
		private NKMTrackingFloat m_fA = new NKMTrackingFloat();

		// Token: 0x04002FAB RID: 12203
		private Color m_ColorTemp;
	}
}
