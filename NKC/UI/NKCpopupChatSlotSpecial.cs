using System;
using ClientPacket.Common;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000924 RID: 2340
	public class NKCpopupChatSlotSpecial : MonoBehaviour
	{
		// Token: 0x06005DC9 RID: 24009 RVA: 0x001CF334 File Offset: 0x001CD534
		public void SetData(NKMChatMessageData data)
		{
			if (data.messageType == ChatMessageType.SystemLevelUp)
			{
				NKCUtil.SetLabelText(this.m_lbTitle, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, data.typeParam));
			}
			NKCUtil.SetLabelText(this.m_lbDesc, NKCServerStringFormatter.TranslateServerFormattedString(data.message));
		}

		// Token: 0x04004A03 RID: 18947
		public Text m_lbTitle;

		// Token: 0x04004A04 RID: 18948
		public Text m_lbDesc;
	}
}
