using System;
using System.Collections;
using NKC.Publisher;
using NKC.UI;
using UnityEngine;

namespace NKC
{
	// Token: 0x020007F0 RID: 2032
	public class NKMPopUpBox
	{
		// Token: 0x0600508D RID: 20621 RVA: 0x00185D4C File Offset: 0x00183F4C
		public static void Init()
		{
			NKMPopUpBox.m_NUF_POPUP_WAIT_BOX_Panel = NKCUIManager.OpenUI("NUF_POPUP_WAIT_BOX_Panel");
			NKMPopUpBox.m_Cog_3 = NKMPopUpBox.m_NUF_POPUP_WAIT_BOX_Panel.transform.Find("Cog 3").gameObject;
			NKMPopUpBox.m_NKM_UI_WAIT = NKMPopUpBox.m_NUF_POPUP_WAIT_BOX_Panel.transform.Find("NKM_UI_WAIT").gameObject;
			NKMPopUpBox.m_Cog_3_Small = NKMPopUpBox.m_NUF_POPUP_WAIT_BOX_Panel.transform.Find("Cog 3_Small").gameObject;
			NKCUtil.SetGameobjectActive(NKMPopUpBox.m_NUF_POPUP_WAIT_BOX_Panel, false);
		}

		// Token: 0x0600508E RID: 20622 RVA: 0x00185DD0 File Offset: 0x00183FD0
		public static void Update()
		{
			if (NKMPopUpBox.m_NUF_POPUP_WAIT_BOX_Panel.activeSelf)
			{
				if (NKMPopUpBox.dWaitFlagGetter != null && !NKMPopUpBox.dWaitFlagGetter())
				{
					NKMPopUpBox.m_WaitBoxTimeMax = 0f;
					NKMPopUpBox.CloseWaitBox();
					return;
				}
				if (NKMPopUpBox.m_WaitBoxTimeMax > 0f)
				{
					NKMPopUpBox.m_WaitBoxTimeMax -= Time.deltaTime;
					if (NKMPopUpBox.m_WaitBoxTimeMax <= 0f)
					{
						NKMPopUpBox.CloseWaitBox();
						if (!string.IsNullOrEmpty(NKMPopUpBox.m_WaitBoxTimeOutMsg))
						{
							NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_FAIL_NET, NKMPopUpBox.m_WaitBoxTimeOutMsg, null, "");
						}
						NKMPopUpBox.m_WaitBoxTimeMax = 0f;
					}
				}
			}
		}

		// Token: 0x0600508F RID: 20623 RVA: 0x00185E64 File Offset: 0x00184064
		public static void OpenWaitBox(NKC_OPEN_WAIT_BOX_TYPE eNKC_OPEN_WAIT_BOX_TYPE, float fWaitTimeMax = 0f, string timeOutMsg = "", NKMPopUpBox.WaitFlagGetter waitFlagGetter = null)
		{
			if (waitFlagGetter != null && !waitFlagGetter())
			{
				return;
			}
			NKMPopUpBox.dWaitFlagGetter = waitFlagGetter;
			switch (eNKC_OPEN_WAIT_BOX_TYPE)
			{
			case NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL:
				NKMPopUpBox.OpenWaitBox(fWaitTimeMax, timeOutMsg);
				return;
			case NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL:
				NKMPopUpBox.OpenSmallWaitBox(fWaitTimeMax, timeOutMsg);
				return;
			case NKC_OPEN_WAIT_BOX_TYPE.NOWBT_CLOSE:
				NKMPopUpBox.CloseWaitBox();
				return;
			default:
				return;
			}
		}

		// Token: 0x06005090 RID: 20624 RVA: 0x00185EA2 File Offset: 0x001840A2
		public static void OpenPublisherAPIWaitBox(NKC_OPEN_WAIT_BOX_TYPE eNKC_OPEN_WAIT_BOX_TYPE)
		{
			if (!NKCPublisherModule.Busy)
			{
				return;
			}
			NKMPopUpBox.OpenWaitBox(eNKC_OPEN_WAIT_BOX_TYPE, 10f, NKCPublisherModule.GetErrorMessage(NKC_PUBLISHER_RESULT_CODE.NPRC_TIMEOUT, null), new NKMPopUpBox.WaitFlagGetter(NKCPublisherModule.IsBusy));
		}

		// Token: 0x06005091 RID: 20625 RVA: 0x00185ECA File Offset: 0x001840CA
		public static IEnumerator Wait(NKC_OPEN_WAIT_BOX_TYPE eNKC_OPEN_WAIT_BOX_TYPE, float fWaitTimeMax = 0f, string timeOutMsg = "", NKMPopUpBox.WaitFlagGetter waitFlagGetter = null)
		{
			NKMPopUpBox.OpenWaitBox(eNKC_OPEN_WAIT_BOX_TYPE, fWaitTimeMax, timeOutMsg, waitFlagGetter);
			while (NKMPopUpBox.IsOpenedWaitBox())
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x06005092 RID: 20626 RVA: 0x00185EEE File Offset: 0x001840EE
		public static IEnumerator WaitPublisherAPI(NKC_OPEN_WAIT_BOX_TYPE eNKC_OPEN_WAIT_BOX_TYPE)
		{
			NKMPopUpBox.OpenPublisherAPIWaitBox(eNKC_OPEN_WAIT_BOX_TYPE);
			while (NKMPopUpBox.IsOpenedWaitBox())
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x06005093 RID: 20627 RVA: 0x00185EFD File Offset: 0x001840FD
		public static void OpenSmallWaitBox(float fWaitTimeMax = 0f, string timeOutMsg = "")
		{
			NKMPopUpBox.m_WaitBoxTimeMax = fWaitTimeMax;
			NKMPopUpBox.m_WaitBoxTimeOutMsg = timeOutMsg;
			NKCUtil.SetGameobjectActive(NKMPopUpBox.m_NUF_POPUP_WAIT_BOX_Panel, true);
			NKCUtil.SetGameobjectActive(NKMPopUpBox.m_Cog_3, false);
			NKCUtil.SetGameobjectActive(NKMPopUpBox.m_NKM_UI_WAIT, false);
			NKCUtil.SetGameobjectActive(NKMPopUpBox.m_Cog_3_Small, true);
		}

		// Token: 0x06005094 RID: 20628 RVA: 0x00185F37 File Offset: 0x00184137
		public static void OpenWaitBox(float fWaitTimeMax = 0f, string timeOutMsg = "")
		{
			NKMPopUpBox.m_WaitBoxTimeMax = fWaitTimeMax;
			NKMPopUpBox.m_WaitBoxTimeOutMsg = timeOutMsg;
			NKCUtil.SetGameobjectActive(NKMPopUpBox.m_NUF_POPUP_WAIT_BOX_Panel, true);
			NKCUtil.SetGameobjectActive(NKMPopUpBox.m_Cog_3, true);
			NKCUtil.SetGameobjectActive(NKMPopUpBox.m_NKM_UI_WAIT, true);
			NKCUtil.SetGameobjectActive(NKMPopUpBox.m_Cog_3_Small, false);
		}

		// Token: 0x06005095 RID: 20629 RVA: 0x00185F71 File Offset: 0x00184171
		public static void CloseWaitBox()
		{
			NKMPopUpBox.dWaitFlagGetter = null;
			NKCUtil.SetGameobjectActive(NKMPopUpBox.m_NUF_POPUP_WAIT_BOX_Panel, false);
		}

		// Token: 0x06005096 RID: 20630 RVA: 0x00185F84 File Offset: 0x00184184
		public static bool IsOpenedWaitBox()
		{
			return NKMPopUpBox.m_NUF_POPUP_WAIT_BOX_Panel.activeSelf;
		}

		// Token: 0x04004084 RID: 16516
		private static GameObject m_NUF_POPUP_WAIT_BOX_Panel;

		// Token: 0x04004085 RID: 16517
		private static string m_WaitBoxTimeOutMsg = "";

		// Token: 0x04004086 RID: 16518
		private static float m_WaitBoxTimeMax = 0f;

		// Token: 0x04004087 RID: 16519
		private static GameObject m_Cog_3;

		// Token: 0x04004088 RID: 16520
		private static GameObject m_NKM_UI_WAIT;

		// Token: 0x04004089 RID: 16521
		private static GameObject m_Cog_3_Small;

		// Token: 0x0400408A RID: 16522
		private static NKMPopUpBox.WaitFlagGetter dWaitFlagGetter;

		// Token: 0x020014AB RID: 5291
		// (Invoke) Token: 0x0600A991 RID: 43409
		public delegate bool WaitFlagGetter();
	}
}
