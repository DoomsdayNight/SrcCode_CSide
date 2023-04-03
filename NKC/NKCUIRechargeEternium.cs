using System;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007D4 RID: 2004
	public class NKCUIRechargeEternium : MonoBehaviour
	{
		// Token: 0x06004F13 RID: 20243 RVA: 0x0017E1E4 File Offset: 0x0017C3E4
		public void UpdateData(NKMUserData userData)
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.RECHARGE_FUND, 0, 0))
			{
				return;
			}
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			NKMUserExpTemplet userExpTemplet = NKCExpManager.GetUserExpTemplet(userData);
			if (userExpTemplet == null)
			{
				Debug.LogError(string.Format("자원정보를 얻어오지 못했습니다.{0}", userData.m_UserLevel));
				return;
			}
			this.m_lCurMaxEternium = userExpTemplet.m_EterniumCap;
			if (this.IsMaxEterniumCap())
			{
				NKCUtil.SetLabelText(this.m_ChargeTimeText, "MAX");
				base.StopCoroutine(this.OnStartRechargeCount());
			}
			else
			{
				this.m_nextUpdateTime = NKCScenManager.CurrentUserData().lastEterniumUpdateDate + NKMCommonConst.RECHARGE_TIME;
				if (base.gameObject.activeInHierarchy)
				{
					base.StartCoroutine(this.OnStartRechargeCount());
				}
			}
			NKCUtil.SetLabelText(this.m_ChargeEterniumText, "+" + userExpTemplet.m_RechargeEternium.ToString());
			NKCUtil.SetLabelText(this.m_lbEterniumCap, string.Format("/{0}", this.m_lCurMaxEternium));
			this.UpdateEterniumUI(userData);
		}

		// Token: 0x06004F14 RID: 20244 RVA: 0x0017E2DD File Offset: 0x0017C4DD
		private bool IsMaxEterniumCap()
		{
			return NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(2) >= this.m_lCurMaxEternium || NKCScenManager.CurrentUserData().lastEterniumUpdateDate == DateTime.MinValue;
		}

		// Token: 0x06004F15 RID: 20245 RVA: 0x0017E310 File Offset: 0x0017C510
		private void UpdateEterniumUI(NKMUserData userData)
		{
			if (this.m_imgEterniumGauge != null)
			{
				float eterniumCapProgress = userData.GetEterniumCapProgress();
				if (base.gameObject.activeInHierarchy)
				{
					this.m_imgEterniumGauge.DOFillAmount(eterniumCapProgress, 0.3f).SetEase(Ease.OutCubic);
				}
				else
				{
					this.m_imgEterniumGauge.fillAmount = eterniumCapProgress;
				}
				NKCUtil.SetGameobjectActive(this.m_EterniumFullGlow, eterniumCapProgress >= 1f);
			}
		}

		// Token: 0x06004F16 RID: 20246 RVA: 0x0017E37C File Offset: 0x0017C57C
		private IEnumerator OnStartRechargeCount()
		{
			while (!this.IsMaxEterniumCap())
			{
				TimeSpan timeLeft = NKCSynchronizedTime.GetTimeLeft(this.m_nextUpdateTime);
				NKCUtil.SetLabelText(this.m_ChargeTimeText, string.Format("{0:00}:{1:00}", timeLeft.Minutes, timeLeft.Seconds));
				yield return new WaitForSeconds(1f);
			}
			yield return null;
			yield break;
		}

		// Token: 0x04003F08 RID: 16136
		public GameObject m_ChargeEternium;

		// Token: 0x04003F09 RID: 16137
		public Text m_ChargeTimeText;

		// Token: 0x04003F0A RID: 16138
		public Text m_ChargeEterniumText;

		// Token: 0x04003F0B RID: 16139
		public GameObject m_EterniumGauge;

		// Token: 0x04003F0C RID: 16140
		public GameObject m_EterniumFullGlow;

		// Token: 0x04003F0D RID: 16141
		public Image m_imgEterniumGauge;

		// Token: 0x04003F0E RID: 16142
		public Text m_lbEterniumCap;

		// Token: 0x04003F0F RID: 16143
		private long m_lCurMaxEternium;

		// Token: 0x04003F10 RID: 16144
		private DateTime m_nextUpdateTime = DateTime.MinValue;
	}
}
