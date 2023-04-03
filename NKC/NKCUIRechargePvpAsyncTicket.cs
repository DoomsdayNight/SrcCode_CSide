using System;
using System.Collections;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000799 RID: 1945
	public class NKCUIRechargePvpAsyncTicket : MonoBehaviour
	{
		// Token: 0x06004C58 RID: 19544 RVA: 0x0016DBA4 File Offset: 0x0016BDA4
		public void UpdateData()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			this.m_lCurMaxTicketCount = (long)NKMPvpCommonConst.Instance.AsyncTicketMaxCount;
			if (this.IsMaxTicketCap())
			{
				NKCUtil.SetGameobjectActive(this.m_lbChargeTicketCount, false);
				NKCUtil.SetLabelText(this.m_ChargeTimeText, "MAX");
				base.StopCoroutine(this.OnStartRechargeCount());
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_lbChargeTicketCount, true);
				NKCUtil.SetLabelText(this.m_lbChargeTicketCount, string.Format("+{0}", NKMPvpCommonConst.Instance.AsyncTicketChargeCount));
				this.m_nextUpdateTime = NKCScenManager.CurrentUserData().lastAsyncTicketUpdateDate.AddSeconds((double)NKMPvpCommonConst.Instance.AsyncTicketChargeInterval);
				if (base.gameObject.activeInHierarchy)
				{
					base.StartCoroutine(this.OnStartRechargeCount());
				}
			}
			NKCUtil.SetLabelText(this.m_lbTicketCap, string.Format("/{0}", this.m_lCurMaxTicketCount));
		}

		// Token: 0x06004C59 RID: 19545 RVA: 0x0016DC8F File Offset: 0x0016BE8F
		private bool IsMaxTicketCap()
		{
			return NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(13) >= this.m_lCurMaxTicketCount || NKCScenManager.CurrentUserData().lastAsyncTicketUpdateDate == DateTime.MinValue;
		}

		// Token: 0x06004C5A RID: 19546 RVA: 0x0016DCC0 File Offset: 0x0016BEC0
		private IEnumerator OnStartRechargeCount()
		{
			while (!this.IsMaxTicketCap())
			{
				TimeSpan timeLeft = NKCSynchronizedTime.GetTimeLeft(NKCSynchronizedTime.ToUtcTime(this.m_nextUpdateTime));
				NKCUtil.SetLabelText(this.m_ChargeTimeText, string.Format("{0:00}:{1:00}:{2:00}", timeLeft.Hours, timeLeft.Minutes, timeLeft.Seconds));
				yield return new WaitForSeconds(1f);
			}
			yield return null;
			yield break;
		}

		// Token: 0x04003C12 RID: 15378
		public GameObject m_ChargeTicket;

		// Token: 0x04003C13 RID: 15379
		public Text m_ChargeTimeText;

		// Token: 0x04003C14 RID: 15380
		public Text m_lbChargeTicketCount;

		// Token: 0x04003C15 RID: 15381
		public Text m_lbTicketCap;

		// Token: 0x04003C16 RID: 15382
		private long m_lCurMaxTicketCount;

		// Token: 0x04003C17 RID: 15383
		private DateTime m_nextUpdateTime = DateTime.MinValue;
	}
}
