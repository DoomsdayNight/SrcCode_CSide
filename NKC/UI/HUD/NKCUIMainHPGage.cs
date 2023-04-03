using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.HUD
{
	// Token: 0x02000C4D RID: 3149
	public class NKCUIMainHPGage : MonoBehaviour
	{
		// Token: 0x060092BD RID: 37565 RVA: 0x00321625 File Offset: 0x0031F825
		public NKCUIMainGageBuff GetMainGageBuff()
		{
			return this.m_NKCUIMainGageBuff;
		}

		// Token: 0x060092BE RID: 37566 RVA: 0x0032162D File Offset: 0x0031F82D
		public void InitUI()
		{
			if (this.m_NKCUIMainGageBuff != null)
			{
				this.m_NKCUIMainGageBuff.InitUI();
			}
		}

		// Token: 0x060092BF RID: 37567 RVA: 0x00321648 File Offset: 0x0031F848
		public void InitData()
		{
			this.m_InnerSize.SetNowValue(0f);
			this.m_InnerAlpha.SetNowValue(0f);
			NKCUtil.SetImageFillAmount(this.m_imgHPGage, 0f);
			NKCUtil.SetImageFillAmount(this.m_imgHPGageInner, 0f);
			NKCUtil.SetGameobjectActive(this.m_objRageGageEffect, false);
		}

		// Token: 0x060092C0 RID: 37568 RVA: 0x003216A1 File Offset: 0x0031F8A1
		public void SetMainGageVisible(bool bSet)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, bSet);
		}

		// Token: 0x060092C1 RID: 37569 RVA: 0x003216AF File Offset: 0x0031F8AF
		public bool IsVisibleMainGage()
		{
			return base.gameObject.activeSelf;
		}

		// Token: 0x060092C2 RID: 37570 RVA: 0x003216BC File Offset: 0x0031F8BC
		public void UpdateGage(float fDeltaTime)
		{
			this.m_InnerSize.Update(fDeltaTime);
			this.m_InnerAlpha.Update(fDeltaTime);
			if (this.m_imgHPGageInner != null)
			{
				this.m_imgHPGageInner.fillAmount = this.m_InnerSize.GetNowValue();
				if (this.m_InnerAlpha.IsTracking())
				{
					Color color = this.m_imgHPGageInner.color;
					color.a = this.m_InnerAlpha.GetNowValue();
					this.m_imgHPGageInner.color = color;
				}
			}
		}

		// Token: 0x060092C3 RID: 37571 RVA: 0x0032173C File Offset: 0x0031F93C
		public void SetMainGage(float fHPRate)
		{
			if (this.m_imgHPGage.fillAmount == fHPRate)
			{
				return;
			}
			if (this.m_animFX != null && this.m_animFX.gameObject.activeInHierarchy)
			{
				if (this.m_imgHPGage.fillAmount < 0.999f && fHPRate >= 0.999f)
				{
					this.m_animFX.Play("FULL", -1);
				}
				else if (this.m_imgHPGage.fillAmount > 0f && fHPRate <= 0f)
				{
					this.m_animFX.Play("BASE", -1);
				}
			}
			if (this.m_imgHPGage.fillAmount != fHPRate)
			{
				this.m_InnerAlpha.SetNowValue(0.8f);
				this.m_InnerAlpha.SetTracking(0f, 1.7f, TRACKING_DATA_TYPE.TDT_SLOWER);
			}
			this.m_imgHPGage.fillAmount = fHPRate;
			this.m_InnerSize.SetTracking(fHPRate, 2f, TRACKING_DATA_TYPE.TDT_SLOWER);
		}

		// Token: 0x060092C4 RID: 37572 RVA: 0x00321822 File Offset: 0x0031FA22
		public void SetRageMode(bool bOn)
		{
			NKCUtil.SetGameobjectActive(this.m_objRageGageEffect, bOn);
		}

		// Token: 0x04007FB4 RID: 32692
		public NKCUIMainGageBuff m_NKCUIMainGageBuff;

		// Token: 0x04007FB5 RID: 32693
		private NKMTrackingFloat m_InnerSize = new NKMTrackingFloat();

		// Token: 0x04007FB6 RID: 32694
		private NKMTrackingFloat m_InnerAlpha = new NKMTrackingFloat();

		// Token: 0x04007FB7 RID: 32695
		public Image m_imgHPGage;

		// Token: 0x04007FB8 RID: 32696
		public Image m_imgHPGageInner;

		// Token: 0x04007FB9 RID: 32697
		public Animator m_animFX;

		// Token: 0x04007FBA RID: 32698
		public GameObject m_objRageGageEffect;
	}
}
