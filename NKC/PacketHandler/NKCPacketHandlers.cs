using System;
using NKC.UI;
using NKM;
using UnityEngine;

namespace NKC.PacketHandler
{
	// Token: 0x0200089D RID: 2205
	public static class NKCPacketHandlers
	{
		// Token: 0x06005810 RID: 22544 RVA: 0x001A6324 File Offset: 0x001A4524
		public static bool Check_NKM_ERROR_CODE(NKM_ERROR_CODE eNKM_ERROR_CODE, bool bCloseWaitBox = true, NKCPopupOKCancel.OnButton onOK_Button = null, int addErrorCode = -2147483648)
		{
			if (bCloseWaitBox)
			{
				NKMPopUpBox.CloseWaitBox();
			}
			if (eNKM_ERROR_CODE == NKM_ERROR_CODE.NEC_OK)
			{
				return true;
			}
			string text = NKCPacketHandlers.GetErrorMessage(eNKM_ERROR_CODE);
			Debug.LogWarning("Server Error Code : " + eNKM_ERROR_CODE.ToString());
			if (-2147483648 != addErrorCode)
			{
				text = text + " (" + addErrorCode.ToString() + ")";
			}
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, text, onOK_Button, "");
			return false;
		}

		// Token: 0x06005811 RID: 22545 RVA: 0x001A6394 File Offset: 0x001A4594
		public static string GetErrorMessage(NKM_ERROR_CODE eNKM_ERROR_CODE)
		{
			string text = null;
			if (NKCStringTable.CheckExistString(eNKM_ERROR_CODE.ToString()))
			{
				string @string = NKCStringTable.GetString(eNKM_ERROR_CODE.ToString(), false);
				if (!string.IsNullOrEmpty(@string))
				{
					text = @string;
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				text = NKCStringTable.GetString("SI_ERROR_DEFAULT_MESSAGE", false);
			}
			if (NKCScenManager.CurrentUserData() != null && NKCScenManager.CurrentUserData().m_eAuthLevel > NKM_USER_AUTH_LEVEL.NORMAL_USER)
			{
				text = string.Format("{0}\n({1} {2})", text, (int)eNKM_ERROR_CODE, eNKM_ERROR_CODE);
			}
			else
			{
				text = string.Format("{0}\n({1})", text, (int)eNKM_ERROR_CODE);
			}
			return text;
		}
	}
}
