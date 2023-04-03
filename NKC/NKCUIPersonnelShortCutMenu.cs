using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x020009C8 RID: 2504
	public class NKCUIPersonnelShortCutMenu : MonoBehaviour
	{
		// Token: 0x06006AD9 RID: 27353 RVA: 0x0022B610 File Offset: 0x00229810
		private void Init()
		{
			this.m_tglNegotiate.m_bGetCallbackWhileLocked = true;
			this.m_tglNegotiate.OnValueChanged.RemoveAllListeners();
			this.m_tglNegotiate.OnValueChanged.AddListener(new UnityAction<bool>(this.OnNegotiate));
			this.m_tglLifetime.m_bGetCallbackWhileLocked = true;
			this.m_tglLifetime.OnValueChanged.RemoveAllListeners();
			this.m_tglLifetime.OnValueChanged.AddListener(new UnityAction<bool>(this.OnLifetime));
			this.m_tglScout.m_bGetCallbackWhileLocked = true;
			this.m_tglScout.OnValueChanged.RemoveAllListeners();
			this.m_tglScout.OnValueChanged.AddListener(new UnityAction<bool>(this.OnScout));
			this.bInitComplete = true;
		}

		// Token: 0x06006ADA RID: 27354 RVA: 0x0022B6CC File Offset: 0x002298CC
		public void SetData(NKC_SCEN_BASE.eUIOpenReserve selectedType)
		{
			if (!this.bInitComplete)
			{
				this.Init();
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.BASE_PERSONNAL, 0, 0))
			{
				this.m_tglNegotiate.Lock(false);
			}
			else
			{
				this.m_tglNegotiate.UnLock(false);
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.BASE_PERSONNAL, 0, 0))
			{
				this.m_tglLifetime.Lock(false);
			}
			else
			{
				this.m_tglLifetime.UnLock(false);
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.BASE_PERSONNAL, 0, 0))
			{
				this.m_tglScout.Lock(false);
			}
			else
			{
				this.m_tglScout.UnLock(false);
			}
			switch (selectedType)
			{
			case NKC_SCEN_BASE.eUIOpenReserve.Personnel_Negotiate:
				this.m_tglNegotiate.Select(true, true, false);
				break;
			case NKC_SCEN_BASE.eUIOpenReserve.Personnel_Lifetime:
				this.m_tglLifetime.Select(true, true, false);
				break;
			case NKC_SCEN_BASE.eUIOpenReserve.Personnel_Scout:
				this.m_tglScout.Select(true, true, false);
				break;
			}
			NKCUtil.SetGameobjectActive(this.m_objNegotiateEvent, NKCCompanyBuff.NeedShowEventMark(NKCScenManager.CurrentUserData().m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_PERSONNAL_NEGOTIATION_CREDIT_DISCOUNT));
		}

		// Token: 0x06006ADB RID: 27355 RVA: 0x0022B7BA File Offset: 0x002299BA
		private void OnNegotiate(bool bSet)
		{
			if (this.m_tglNegotiate.m_bLock)
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.BASE_PERSONNAL, 0);
				return;
			}
			if (bSet)
			{
				NKCUIManager.OnBackButton();
				NKCScenManager.GetScenManager().Get_SCEN_BASE().SetOpenReserve(NKC_SCEN_BASE.eUIOpenReserve.Personnel_Negotiate, 0L, false);
			}
		}

		// Token: 0x06006ADC RID: 27356 RVA: 0x0022B7EF File Offset: 0x002299EF
		private void OnLifetime(bool bSet)
		{
			if (this.m_tglLifetime.m_bLock)
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.BASE_PERSONNAL, 0);
				return;
			}
			if (bSet)
			{
				NKCUIManager.OnBackButton();
				NKCScenManager.GetScenManager().Get_SCEN_BASE().SetOpenReserve(NKC_SCEN_BASE.eUIOpenReserve.Personnel_Lifetime, 0L, false);
			}
		}

		// Token: 0x06006ADD RID: 27357 RVA: 0x0022B824 File Offset: 0x00229A24
		private void OnScout(bool bSet)
		{
			if (this.m_tglScout.m_bLock)
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.BASE_PERSONNAL, 0);
				return;
			}
			if (bSet)
			{
				NKCUIManager.OnBackButton();
				NKCScenManager.GetScenManager().Get_SCEN_BASE().SetOpenReserve(NKC_SCEN_BASE.eUIOpenReserve.Personnel_Scout, 0L, false);
			}
		}

		// Token: 0x0400568C RID: 22156
		public NKCUIComToggle m_tglNegotiate;

		// Token: 0x0400568D RID: 22157
		public GameObject m_objNegotiateEvent;

		// Token: 0x0400568E RID: 22158
		public NKCUIComToggle m_tglLifetime;

		// Token: 0x0400568F RID: 22159
		public NKCUIComToggle m_tglScout;

		// Token: 0x04005690 RID: 22160
		private bool bInitComplete;
	}
}
