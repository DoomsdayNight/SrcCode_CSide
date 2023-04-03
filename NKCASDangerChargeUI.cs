using System;
using NKC;
using UnityEngine;
using UnityEngine.UI;

namespace NKM
{
	// Token: 0x02000362 RID: 866
	public class NKCASDangerChargeUI : NKMObjectPoolData
	{
		// Token: 0x060014A5 RID: 5285 RVA: 0x0004DBAD File Offset: 0x0004BDAD
		public NKCASDangerChargeUI(bool bAsync = false)
		{
			this.m_NKM_OBJECT_POOL_TYPE = NKM_OBJECT_POOL_TYPE.NOPT_NKCASDangerChargeUI;
			this.m_bUnloadable = true;
			this.Load(bAsync);
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x0004DBCB File Offset: 0x0004BDCB
		public override void Load(bool bAsync)
		{
			this.m_Instant = NKCAssetResourceManager.OpenInstance<GameObject>("AB_FX_UI_DANGER", "AB_FX_UI_DANGER", bAsync, null);
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x0004DBE4 File Offset: 0x0004BDE4
		public override bool LoadComplete()
		{
			if (this.m_Instant == null || this.m_Instant.m_Instant == null)
			{
				return false;
			}
			this.m_Instant.m_Instant.transform.SetParent(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_GAME_BATTLE_UNIT_VIEWER().transform, false);
			this.m_DANGER_CHARGE_WAIT_TIME_Image = this.m_Instant.m_Instant.transform.Find("AB_FX_UI_DANGER/DANGER_GAUGE/DANGER_GAUGE_BACK/DANGER_CHARGE_WAIT_TIME").gameObject.GetComponent<Image>();
			this.m_DANGER_CHARGE_DAMAGE_Image = this.m_Instant.m_Instant.transform.Find("AB_FX_UI_DANGER/DANGER_GAUGE/DANGER_GAUGE_BACK/DANGER_CHARGE_DAMAGE").gameObject.GetComponent<Image>();
			this.CloseDangerCharge();
			return true;
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x0004DC98 File Offset: 0x0004BE98
		public override void Open()
		{
			this.CloseDangerCharge();
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x0004DCA0 File Offset: 0x0004BEA0
		public override void Close()
		{
			if (this.m_Instant.m_Instant.activeSelf)
			{
				this.m_Instant.m_Instant.SetActive(false);
			}
			this.CloseDangerCharge();
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x0004DCCB File Offset: 0x0004BECB
		public override void Unload()
		{
			NKCAssetResourceManager.CloseInstance(this.m_Instant);
			this.m_Instant = null;
			this.m_DANGER_CHARGE_WAIT_TIME_Image = null;
			this.m_DANGER_CHARGE_DAMAGE_Image = null;
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x0004DCF0 File Offset: 0x0004BEF0
		public void OpenDangerCharge(float fTimeMax, float fDamageMax, float fHitCountMax)
		{
			this.m_bOpen = true;
			this.m_fTimeMax = fTimeMax;
			this.m_fDamageMax = fDamageMax;
			this.m_fHitCountMax = fHitCountMax;
			this.m_DANGER_CHARGE_WAIT_TIME_Image.fillAmount = 0f;
			this.m_DANGER_CHARGE_DAMAGE_Image.fillAmount = 0f;
			if (!this.m_Instant.m_Instant.activeSelf)
			{
				this.m_Instant.m_Instant.SetActive(true);
			}
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x0004DD5C File Offset: 0x0004BF5C
		public void CloseDangerCharge()
		{
			this.m_bOpen = false;
			if (this.m_Instant.m_Instant.activeSelf)
			{
				this.m_Instant.m_Instant.SetActive(false);
			}
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x0004DD88 File Offset: 0x0004BF88
		public void SetPos(ref Vector3 m_Pos)
		{
			if (!this.m_bOpen)
			{
				return;
			}
			this.m_Instant.m_Instant.transform.localPosition = m_Pos;
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0004DDB0 File Offset: 0x0004BFB0
		public void Update(float fRemainTime, float fDamage, float fHitCount)
		{
			if (!this.m_bOpen)
			{
				return;
			}
			this.m_DANGER_CHARGE_WAIT_TIME_Image.fillAmount = (this.m_fTimeMax - fRemainTime) / this.m_fTimeMax;
			if (this.m_fDamageMax > 0f)
			{
				this.m_DANGER_CHARGE_DAMAGE_Image.fillAmount = fDamage / this.m_fDamageMax;
				return;
			}
			if (this.m_fHitCountMax > 0f)
			{
				this.m_DANGER_CHARGE_DAMAGE_Image.fillAmount = fHitCount / this.m_fHitCountMax;
			}
		}

		// Token: 0x04000E58 RID: 3672
		private NKCAssetInstanceData m_Instant;

		// Token: 0x04000E59 RID: 3673
		private Image m_DANGER_CHARGE_WAIT_TIME_Image;

		// Token: 0x04000E5A RID: 3674
		private Image m_DANGER_CHARGE_DAMAGE_Image;

		// Token: 0x04000E5B RID: 3675
		private bool m_bOpen;

		// Token: 0x04000E5C RID: 3676
		private float m_fTimeMax;

		// Token: 0x04000E5D RID: 3677
		private float m_fDamageMax;

		// Token: 0x04000E5E RID: 3678
		private float m_fHitCountMax;
	}
}
