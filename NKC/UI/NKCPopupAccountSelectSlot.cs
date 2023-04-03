using System;
using ClientPacket.Account;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200091A RID: 2330
	public class NKCPopupAccountSelectSlot : MonoBehaviour
	{
		// Token: 0x06005D49 RID: 23881 RVA: 0x001CC37C File Offset: 0x001CA57C
		public void InitData()
		{
			this.m_creditCount.text = "0";
			this.m_eterniumCount.text = "0";
			this.m_cashCount.text = "0";
			this.m_medalCount.text = "0";
			this.m_nickName.text = "";
			this.m_level.text = "";
			this.m_publisherCode.text = "";
			NKCUtil.SetGameobjectActive(this.m_iconSteam, false);
			NKCUtil.SetGameobjectActive(this.m_iconMobile, false);
		}

		// Token: 0x06005D4A RID: 23882 RVA: 0x001CC414 File Offset: 0x001CA614
		public void SetData(NKMAccountLinkUserProfile userProfile, UnityAction<bool> onToggleSelected)
		{
			this.m_creditCount.text = userProfile.creditCount.ToString();
			this.m_eterniumCount.text = userProfile.eterniumCount.ToString();
			this.m_cashCount.text = userProfile.cashCount.ToString();
			this.m_medalCount.text = userProfile.medalCount.ToString();
			this.m_nickName.text = userProfile.commonProfile.nickname;
			this.m_level.text = userProfile.commonProfile.level.ToString();
			this.m_publisherCode.text = NKCUtilString.GetFriendCode(userProfile.commonProfile.friendCode);
			NKCUtil.SetGameobjectActive(this.m_iconSteam, userProfile.publisherType == NKM_PUBLISHER_TYPE.NPT_STEAM);
			NKCUtil.SetGameobjectActive(this.m_iconMobile, userProfile.publisherType != NKM_PUBLISHER_TYPE.NPT_STEAM);
			this.m_button.OnValueChanged.RemoveAllListeners();
			if (onToggleSelected == null)
			{
				this.m_button.Select(true, true, true);
				return;
			}
			this.m_button.OnValueChanged.AddListener(onToggleSelected);
		}

		// Token: 0x04004958 RID: 18776
		public Text m_creditCount;

		// Token: 0x04004959 RID: 18777
		public Text m_eterniumCount;

		// Token: 0x0400495A RID: 18778
		public Text m_cashCount;

		// Token: 0x0400495B RID: 18779
		public Text m_medalCount;

		// Token: 0x0400495C RID: 18780
		public Text m_nickName;

		// Token: 0x0400495D RID: 18781
		public Text m_level;

		// Token: 0x0400495E RID: 18782
		public Text m_publisherCode;

		// Token: 0x0400495F RID: 18783
		public GameObject m_highlight;

		// Token: 0x04004960 RID: 18784
		public GameObject m_grey;

		// Token: 0x04004961 RID: 18785
		public GameObject m_iconSteam;

		// Token: 0x04004962 RID: 18786
		public GameObject m_iconMobile;

		// Token: 0x04004963 RID: 18787
		public NKCUIComToggle m_button;
	}
}
