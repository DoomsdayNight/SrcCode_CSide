using System;
using ClientPacket.Common;
using NKC.UI.Guild;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Component
{
	// Token: 0x02000C4F RID: 3151
	public class NKCUIComUserSimpleInfo : MonoBehaviour
	{
		// Token: 0x060092CA RID: 37578 RVA: 0x003218AC File Offset: 0x0031FAAC
		private void Init()
		{
			this.m_bInit = true;
			if (this.m_Slot != null)
			{
				this.m_Slot.Init();
			}
		}

		// Token: 0x060092CB RID: 37579 RVA: 0x003218CE File Offset: 0x0031FACE
		public void SetData(NKMUserProfileData userProfileData)
		{
			if (userProfileData == null)
			{
				return;
			}
			this.SetData(userProfileData.commonProfile, userProfileData.guildData);
		}

		// Token: 0x060092CC RID: 37580 RVA: 0x003218E8 File Offset: 0x0031FAE8
		public void SetData(NKMCommonProfile profile, NKMGuildSimpleData guildData)
		{
			if (!this.m_bInit)
			{
				this.Init();
			}
			if (profile != null)
			{
				NKCUtil.SetLabelText(this.m_lbName, profile.nickname);
				NKCUtil.SetLabelText(this.m_lbLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, profile.level));
				NKCUtil.SetLabelText(this.m_lbUID, NKCUtilString.GetFriendCode(profile.friendCode));
				if (this.m_Slot != null)
				{
					NKCUtil.SetGameobjectActive(this.m_Slot, true);
					this.m_Slot.SetProfiledata(profile, null);
				}
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbName, "");
				NKCUtil.SetLabelText(this.m_lbLevel, "");
				NKCUtil.SetLabelText(this.m_lbUID, "");
				NKCUtil.SetGameobjectActive(this.m_Slot, false);
			}
			if (guildData != null && guildData.guildUid > 0L)
			{
				NKCUtil.SetGameobjectActive(this.m_GuildBadge, true);
				NKCUtil.SetGameobjectActive(this.m_objGuildName, true);
				if (this.m_GuildBadge != null)
				{
					this.m_GuildBadge.SetData(guildData.badgeId);
				}
				NKCUtil.SetLabelText(this.m_lbGuildName, guildData.guildName);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_GuildBadge, false);
			NKCUtil.SetGameobjectActive(this.m_objGuildName, false);
		}

		// Token: 0x04007FBD RID: 32701
		[Header("기본정보")]
		public Text m_lbLevel;

		// Token: 0x04007FBE RID: 32702
		public Text m_lbName;

		// Token: 0x04007FBF RID: 32703
		public Text m_lbUID;

		// Token: 0x04007FC0 RID: 32704
		[Header("프로필 슬롯")]
		public NKCUISlotProfile m_Slot;

		// Token: 0x04007FC1 RID: 32705
		[Header("컨소시움")]
		public NKCUIGuildBadge m_GuildBadge;

		// Token: 0x04007FC2 RID: 32706
		public GameObject m_objGuildName;

		// Token: 0x04007FC3 RID: 32707
		public Text m_lbGuildName;

		// Token: 0x04007FC4 RID: 32708
		private bool m_bInit;
	}
}
