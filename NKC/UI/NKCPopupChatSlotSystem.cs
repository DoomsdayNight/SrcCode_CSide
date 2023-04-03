using System;
using ClientPacket.Common;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200091E RID: 2334
	public class NKCPopupChatSlotSystem : MonoBehaviour
	{
		// Token: 0x06005D6C RID: 23916 RVA: 0x001CD030 File Offset: 0x001CB230
		public void SetData(NKMChatMessageData data)
		{
			NKCUtil.SetLabelText(this.m_lbMessage, NKCServerStringFormatter.TranslateServerFormattedString(data.message));
		}

		// Token: 0x04004998 RID: 18840
		public Text m_lbMessage;
	}
}
