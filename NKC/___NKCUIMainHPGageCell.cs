using System;
using System.Text;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000629 RID: 1577
	public class ___NKCUIMainHPGageCell
	{
		// Token: 0x060030B4 RID: 12468 RVA: 0x000F1398 File Offset: 0x000EF598
		public ___NKCUIMainHPGageCell(GameObject cNKM_UI_HUD_MAIN_GAGE, bool bGreen, int index, bool bLong = false)
		{
			this.m_bGreen = bGreen;
			this.m_bLong = bLong;
			this.m_NKM_UI_HUD_MAIN_GAGE = cNKM_UI_HUD_MAIN_GAGE;
			this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
			if (bLong)
			{
				if (bGreen)
				{
					this.m_StringBuilder.AppendFormat("NKM_UI_HUD_MAIN_GAGE_GREEN_MOD/NKM_UI_HUD_MAIN_GAGE_GREEN_CELL{0}", index);
				}
				else
				{
					this.m_StringBuilder.AppendFormat("NKM_UI_HUD_MAIN_GAGE_RED_MOD/NKM_UI_HUD_MAIN_GAGE_RED_CELL{0}", index);
				}
			}
			else if (bGreen)
			{
				this.m_StringBuilder.AppendFormat("NKM_UI_HUD_MAIN_GAGE_GREEN/NKM_UI_HUD_MAIN_GAGE_GREEN_CELL{0}", index);
			}
			else
			{
				this.m_StringBuilder.AppendFormat("NKM_UI_HUD_MAIN_GAGE_RED/NKM_UI_HUD_MAIN_GAGE_RED_CELL{0}", index);
			}
			this.m_NKM_UI_HUD_MAIN_GAGE_CELL = this.m_NKM_UI_HUD_MAIN_GAGE.transform.Find(this.m_StringBuilder.ToString()).gameObject;
			this.m_NKM_UI_HUD_MAIN_GAGE_CELL_Image = this.m_NKM_UI_HUD_MAIN_GAGE_CELL.GetComponent<Image>();
			this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
			if (bLong)
			{
				if (bGreen)
				{
					this.m_StringBuilder.AppendFormat("NKM_UI_HUD_MAIN_GAGE_GREEN_MOD/NKM_UI_HUD_MAIN_GAGE_GREEN_CELL{0}_INNER", index);
				}
				else
				{
					this.m_StringBuilder.AppendFormat("NKM_UI_HUD_MAIN_GAGE_RED_MOD/NKM_UI_HUD_MAIN_GAGE_RED_CELL{0}_INNER", index);
				}
			}
			else if (bGreen)
			{
				this.m_StringBuilder.AppendFormat("NKM_UI_HUD_MAIN_GAGE_GREEN/NKM_UI_HUD_MAIN_GAGE_GREEN_CELL{0}_INNER", index);
			}
			else
			{
				this.m_StringBuilder.AppendFormat("NKM_UI_HUD_MAIN_GAGE_RED/NKM_UI_HUD_MAIN_GAGE_RED_CELL{0}_INNER", index);
			}
			this.m_NKM_UI_HUD_MAIN_GAGE_CELL_INNER = this.m_NKM_UI_HUD_MAIN_GAGE.transform.Find(this.m_StringBuilder.ToString()).gameObject;
			this.m_NKM_UI_HUD_MAIN_GAGE_CELL_INNER_Image = this.m_NKM_UI_HUD_MAIN_GAGE_CELL_INNER.GetComponent<Image>();
			this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
			if (bLong)
			{
				if (bGreen)
				{
					this.m_StringBuilder.AppendFormat("NKM_UI_HUD_MAIN_GAGE_GREEN_MOD/NKM_UI_HUD_MAIN_GAGE_GREEN_CELL_FX/NKM_UI_HUD_MAIN_GAGE_GREEN_CELL{0}_FX", index);
				}
				else
				{
					this.m_StringBuilder.AppendFormat("NKM_UI_HUD_MAIN_GAGE_RED_MOD/NKM_UI_HUD_MAIN_GAGE_RED_CELL_FX/NKM_UI_HUD_MAIN_GAGE_RED_CELL{0}_FX", index);
				}
			}
			else if (bGreen)
			{
				this.m_StringBuilder.AppendFormat("NKM_UI_HUD_MAIN_GAGE_GREEN/NKM_UI_HUD_MAIN_GAGE_GREEN_CELL_FX/NKM_UI_HUD_MAIN_GAGE_GREEN_CELL{0}_FX", index);
			}
			else
			{
				this.m_StringBuilder.AppendFormat("NKM_UI_HUD_MAIN_GAGE_RED/NKM_UI_HUD_MAIN_GAGE_RED_CELL_FX/NKM_UI_HUD_MAIN_GAGE_RED_CELL{0}_FX", index);
			}
			this.m_NKM_UI_HUD_MAIN_GAGE_GREEN_CELL_FX = this.m_NKM_UI_HUD_MAIN_GAGE.transform.Find(this.m_StringBuilder.ToString()).gameObject;
			this.m_NKM_UI_HUD_MAIN_GAGE_GREEN_CELL_FX_RectTransform = this.m_NKM_UI_HUD_MAIN_GAGE_GREEN_CELL_FX.GetComponent<RectTransform>();
			this.m_NKM_UI_HUD_MAIN_GAGE_GREEN_CELL_FX_Animator = this.m_NKM_UI_HUD_MAIN_GAGE_GREEN_CELL_FX.GetComponent<Animator>();
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x000F1634 File Offset: 0x000EF834
		public void Init()
		{
			this.m_NKM_UI_HUD_MAIN_GAGE_CELL_Image.fillAmount = 0f;
			this.m_NKM_UI_HUD_MAIN_GAGE_CELL_INNER_Image.fillAmount = 0f;
			this.m_InnerSize.SetNowValue(0f);
			this.m_InnerAlpha.SetNowValue(1f);
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x000F1684 File Offset: 0x000EF884
		public void SetMainGage(float fHPRate)
		{
			if (this.m_NKM_UI_HUD_MAIN_GAGE_CELL_Image.fillAmount == fHPRate)
			{
				return;
			}
			if (this.m_NKM_UI_HUD_MAIN_GAGE_CELL_Image.fillAmount < 0.999f && fHPRate >= 0.999f)
			{
				if (this.m_NKM_UI_HUD_MAIN_GAGE_GREEN_CELL_FX_Animator.gameObject.activeInHierarchy)
				{
					this.m_NKM_UI_HUD_MAIN_GAGE_GREEN_CELL_FX_Animator.Play("FULL", -1);
				}
			}
			else if (this.m_NKM_UI_HUD_MAIN_GAGE_CELL_Image.fillAmount > 0f && fHPRate <= 0f && this.m_NKM_UI_HUD_MAIN_GAGE_GREEN_CELL_FX_Animator.gameObject.activeInHierarchy)
			{
				this.m_NKM_UI_HUD_MAIN_GAGE_GREEN_CELL_FX_Animator.Play("BASE", -1);
			}
			if (this.m_NKM_UI_HUD_MAIN_GAGE_CELL_Image.fillAmount != fHPRate)
			{
				this.m_InnerAlpha.SetNowValue(0.8f);
				this.m_InnerAlpha.SetTracking(0f, 1.7f, TRACKING_DATA_TYPE.TDT_SLOWER);
			}
			Vector3 localScale = this.m_NKM_UI_HUD_MAIN_GAGE_GREEN_CELL_FX_RectTransform.localScale;
			if (!this.m_bLong)
			{
				if (this.m_bGreen)
				{
					localScale.x = fHPRate;
				}
				else
				{
					localScale.x = -fHPRate;
				}
			}
			else if (!this.m_bGreen)
			{
				localScale.x = fHPRate;
			}
			else
			{
				localScale.x = -fHPRate;
			}
			this.m_NKM_UI_HUD_MAIN_GAGE_GREEN_CELL_FX_RectTransform.localScale = localScale;
			this.m_NKM_UI_HUD_MAIN_GAGE_CELL_Image.fillAmount = fHPRate;
			this.m_InnerSize.SetTracking(fHPRate, 2f, TRACKING_DATA_TYPE.TDT_SLOWER);
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x000F17C8 File Offset: 0x000EF9C8
		public void Update(float fDeltaTime)
		{
			this.m_InnerSize.Update(fDeltaTime);
			this.m_InnerAlpha.Update(fDeltaTime);
			this.m_NKM_UI_HUD_MAIN_GAGE_CELL_INNER_Image.fillAmount = this.m_InnerSize.GetNowValue();
			if (this.m_InnerAlpha.IsTracking())
			{
				Color color = this.m_NKM_UI_HUD_MAIN_GAGE_CELL_INNER_Image.color;
				color.a = this.m_InnerAlpha.GetNowValue();
				this.m_NKM_UI_HUD_MAIN_GAGE_CELL_INNER_Image.color = color;
			}
		}

		// Token: 0x04003027 RID: 12327
		private GameObject m_NKM_UI_HUD_MAIN_GAGE;

		// Token: 0x04003028 RID: 12328
		private GameObject m_NKM_UI_HUD_MAIN_GAGE_CELL;

		// Token: 0x04003029 RID: 12329
		private Image m_NKM_UI_HUD_MAIN_GAGE_CELL_Image;

		// Token: 0x0400302A RID: 12330
		private GameObject m_NKM_UI_HUD_MAIN_GAGE_CELL_INNER;

		// Token: 0x0400302B RID: 12331
		private Image m_NKM_UI_HUD_MAIN_GAGE_CELL_INNER_Image;

		// Token: 0x0400302C RID: 12332
		private GameObject m_NKM_UI_HUD_MAIN_GAGE_GREEN_CELL_FX;

		// Token: 0x0400302D RID: 12333
		private RectTransform m_NKM_UI_HUD_MAIN_GAGE_GREEN_CELL_FX_RectTransform;

		// Token: 0x0400302E RID: 12334
		private Animator m_NKM_UI_HUD_MAIN_GAGE_GREEN_CELL_FX_Animator;

		// Token: 0x0400302F RID: 12335
		private StringBuilder m_StringBuilder = new StringBuilder();

		// Token: 0x04003030 RID: 12336
		public NKMTrackingFloat m_InnerSize = new NKMTrackingFloat();

		// Token: 0x04003031 RID: 12337
		public NKMTrackingFloat m_InnerAlpha = new NKMTrackingFloat();

		// Token: 0x04003032 RID: 12338
		private bool m_bGreen = true;

		// Token: 0x04003033 RID: 12339
		private bool m_bLong = true;
	}
}
