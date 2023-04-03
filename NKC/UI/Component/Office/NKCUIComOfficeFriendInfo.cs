using System;
using ClientPacket.Common;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Component.Office
{
	// Token: 0x02000C68 RID: 3176
	public class NKCUIComOfficeFriendInfo : MonoBehaviour
	{
		// Token: 0x060093BE RID: 37822 RVA: 0x003272DC File Offset: 0x003254DC
		public void SetData(NKMUserProfileData profileData)
		{
			if (profileData == null)
			{
				base.gameObject.SetActive(false);
				return;
			}
			NKCUtil.SetLabelText(this.m_lbFriendInfo, NKCUtilString.GET_STRING_INGAME_USER_A_NAME_TWO_PARAM, new object[]
			{
				profileData.commonProfile.level,
				profileData.commonProfile.nickname
			});
		}

		// Token: 0x060093BF RID: 37823 RVA: 0x00327330 File Offset: 0x00325530
		private void Update()
		{
			if (base.transform.lossyScale.x < 0f)
			{
				base.transform.localScale = new Vector3(-base.transform.localScale.x, base.transform.localScale.y, base.transform.localScale.z);
			}
		}

		// Token: 0x040080B8 RID: 32952
		public Text m_lbFriendInfo;
	}
}
