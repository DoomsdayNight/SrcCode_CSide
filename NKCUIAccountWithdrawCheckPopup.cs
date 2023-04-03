using System;
using System.Collections;
using System.Collections.Generic;
using Cs.Engine.Util;
using NKC;
using NKC.Publisher;
using NKC.UI;
using NKM;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000015 RID: 21
public class NKCUIAccountWithdrawCheckPopup : NKCUIBase
{
	// Token: 0x1700002D RID: 45
	// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003930 File Offset: 0x00001B30
	public override NKCUIBase.eMenutype eUIType
	{
		get
		{
			return NKCUIBase.eMenutype.Popup;
		}
	}

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003933 File Offset: 0x00001B33
	public override string MenuName { get; }

	// Token: 0x060000B9 RID: 185 RVA: 0x0000393B File Offset: 0x00001B3B
	public override void CloseInternal()
	{
		this.DeActive();
	}

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x060000BA RID: 186 RVA: 0x00003944 File Offset: 0x00001B44
	public static NKCUIAccountWithdrawCheckPopup Instance
	{
		get
		{
			if (NKCUIAccountWithdrawCheckPopup.m_Instance == null)
			{
				NKCUIManager.LoadedUIData loadedUIData = NKCUIManager.OpenNewInstance<NKCUIAccountWithdrawCheckPopup>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_SIGN_OUT", NKCUIManager.eUIBaseRect.UIOverlay, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIAccountWithdrawCheckPopup.CleanupInstance));
				if (loadedUIData != null)
				{
					NKCUIAccountWithdrawCheckPopup.m_Instance = loadedUIData.GetInstance<NKCUIAccountWithdrawCheckPopup>();
				}
			}
			return NKCUIAccountWithdrawCheckPopup.m_Instance;
		}
	}

	// Token: 0x060000BB RID: 187 RVA: 0x0000398E File Offset: 0x00001B8E
	private static void CleanupInstance()
	{
		NKCUIAccountWithdrawCheckPopup.m_Instance = null;
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00003998 File Offset: 0x00001B98
	public void OnClickCheckButton(bool bInitAccount = false)
	{
		if (NKCDefineManager.DEFINE_NXTOY() || NKCDefineManager.DEFINE_SB_GB())
		{
			if (string.IsNullOrEmpty(this.m_InputField.text) || !string.Equals(this.m_SignOutText.text.ToUpper(), this.m_InputField.text.ToUpper()))
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_OPTION_SIGN_OUT_MESSAGE_MISS_MATCHED, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
		}
		else if (string.IsNullOrEmpty(this.m_InputField.text) || !string.Equals(this.m_SignOutText.text.ToUpper(), this.m_InputField.text.ToUpper()))
		{
			return;
		}
		if (bInitAccount)
		{
			NKCPacketSender.Send_NKMPacket_ACCOUNT_UNLINK_REQ();
			return;
		}
		if (NKCDefineManager.DEFINE_SB_GB())
		{
			base.StartCoroutine(this.WithdrawProcess());
			return;
		}
		this.ResetConnection();
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00003A5E File Offset: 0x00001C5E
	private void OnCompleteWithdraw(NKC_PUBLISHER_RESULT_CODE result, string additionalError = null)
	{
		this.ResetConnection();
	}

	// Token: 0x060000BE RID: 190 RVA: 0x00003A66 File Offset: 0x00001C66
	private void ResetConnection()
	{
		NKCScenManager.GetScenManager().GetConnectLogin().ResetConnection();
		NKCScenManager.GetScenManager().GetConnectGame().ResetConnection();
		NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_LOGIN, true);
	}

	// Token: 0x060000BF RID: 191 RVA: 0x00003A94 File Offset: 0x00001C94
	public void OpenUI(bool bInitAccount = false)
	{
		if (this.m_InputField != null)
		{
			this.m_InputField.text = "";
		}
		if (NKCDefineManager.DEFINE_SELECT_SERVER())
		{
			if (bInitAccount)
			{
				NKCUtil.SetLabelText(this.m_SignOutText, NKCStringTable.GetString("SI_PF_POPUP_SERVER_INITIALIZATION_TEXT", false));
				NKCUtil.SetLabelText(this.m_Desc, NKCStringTable.GetString("SI_PF_POPUP_SERVER_INITIALIZATION_DESC", false));
			}
			else
			{
				NKCUtil.SetLabelText(this.m_SignOutText, NKCStringTable.GetString("SI_PF_POPUP_DELETE_ACCOUNT_TEXT", false));
				NKCUtil.SetLabelText(this.m_Desc, NKCStringTable.GetString("SI_PF_POPUP_DELETE_ACCOUNT_DESC", false));
			}
		}
		else
		{
			NKCUtil.SetLabelText(this.m_SignOutText, NKCStringTable.GetString("SI_PF_POPUP_SIGN_OUT_TEXT", false));
			NKCUtil.SetLabelText(this.m_Desc, NKCStringTable.GetString("SI_PF_POPUP_SIGN_OUT_INFO_TEXT", false));
		}
		NKCUtil.SetGameobjectActive(base.gameObject, true);
		this.m_OK_BUTTON.PointerClick.RemoveAllListeners();
		this.m_OK_BUTTON.PointerClick.AddListener(delegate()
		{
			this.OnClickCheckButton(bInitAccount);
		});
		this.m_CANCLE_BUTTON.PointerClick.RemoveAllListeners();
		this.m_CANCLE_BUTTON.PointerClick.AddListener(new UnityAction(this.CloseUI));
		base.UIOpened(true);
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x00003BD5 File Offset: 0x00001DD5
	public void CloseUI()
	{
		this.DeActive();
		base.Close();
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x00003BE3 File Offset: 0x00001DE3
	private void DeActive()
	{
		NKCUtil.SetGameobjectActive(base.gameObject, false);
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x00003BF1 File Offset: 0x00001DF1
	private IEnumerator WithdrawProcess()
	{
		NKMPopUpBox.OpenWaitBox(NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, 0f, "", null);
		bool bSuccess = true;
		foreach (NKCConnectionInfo.LoginServerInfo loginServerInfo in NKCConnectionInfo.LoginServerInfos)
		{
			using (WithdrawPacketController controller = new WithdrawPacketController())
			{
				yield return controller.WithdrawPacketProcess(loginServerInfo.m_serviceIP, loginServerInfo.m_servicePort);
				if (controller.Ack == null)
				{
					bSuccess = false;
					break;
				}
			}
			WithdrawPacketController controller = null;
			continue;
			break;
		}
		IEnumerator<NKCConnectionInfo.LoginServerInfo> enumerator = null;
		if (bSuccess)
		{
			if (NKCDefineManager.DEFINE_SB_GB())
			{
				if (NKCPublisherModule.Auth.IsGuest())
				{
					NKCPublisherModule.Auth.Withdraw(delegate(NKC_PUBLISHER_RESULT_CODE result, string additionalError)
					{
						NKCScenManager.GetScenManager().GetConnectLogin().ResetConnection();
						NKCScenManager.GetScenManager().GetConnectGame().ResetConnection();
						NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_LOGIN, true);
					});
				}
				else
				{
					NKCPublisherModule.Auth.TemporaryWithdrawal(delegate(NKC_PUBLISHER_RESULT_CODE result, string additionalError)
					{
						NKCScenManager.GetScenManager().GetConnectLogin().ResetConnection();
						NKCScenManager.GetScenManager().GetConnectGame().ResetConnection();
						NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_LOGIN, true);
					});
				}
			}
		}
		else
		{
			NKCPopupOKCancel.OpenOKBox(NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_CHANGEACCOUNT_FAIL_QUIT, null, null, "");
		}
		NKMPopUpBox.CloseWaitBox();
		this.CloseUI();
		yield break;
		yield break;
	}

	// Token: 0x0400004C RID: 76
	public Text m_SignOutText;

	// Token: 0x0400004D RID: 77
	public Text m_Desc;

	// Token: 0x0400004E RID: 78
	public InputField m_InputField;

	// Token: 0x0400004F RID: 79
	public NKCUIComStateButton m_OK_BUTTON;

	// Token: 0x04000050 RID: 80
	public NKCUIComStateButton m_CANCLE_BUTTON;

	// Token: 0x04000051 RID: 81
	private static NKCUIAccountWithdrawCheckPopup m_Instance;
}
