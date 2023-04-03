using System;
using ClientPacket.Common;
using NKC.UI.Guild;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A8B RID: 2699
	public class NKCPopupTopPlayerSlot : MonoBehaviour
	{
		// Token: 0x06007770 RID: 30576 RVA: 0x0027B8C8 File Offset: 0x00279AC8
		public void SetData(NKMCommonProfile commonProfileData, NKMGuildSimpleData guildData, string score, int raidTryCount, int raidTryMaxCount, int rank)
		{
			if (this.m_btn != null)
			{
				this.m_btn.PointerClick.RemoveAllListeners();
				this.m_btn.PointerClick.AddListener(delegate()
				{
					this.OnClickProfile(null, 0);
				});
			}
			if (this.m_slotProfile != null)
			{
				NKCUtil.SetGameobjectActive(this.m_slotProfile, true);
				NKCUISlotProfile slotProfile = this.m_slotProfile;
				if (slotProfile != null)
				{
					slotProfile.SetProfiledata(commonProfileData, null);
				}
			}
			this.m_UserUid = commonProfileData.userUid;
			NKCUtil.SetGameobjectActive(this.m_lbLevel, true);
			NKCUtil.SetGameobjectActive(this.m_lbLevel, commonProfileData.level > 0);
			if (commonProfileData.level > 0)
			{
				NKCUtil.SetLabelText(this.m_lbLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, commonProfileData.level));
			}
			NKCUtil.SetLabelText(this.m_lbName, commonProfileData.nickname);
			NKCUtil.SetLabelText(this.m_lbFriendCode, string.Format("#{0}", commonProfileData.friendCode));
			if (rank > 0)
			{
				NKCUtil.SetLabelText(this.m_lbRank, rank.ToString());
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbRank, "");
			}
			if (guildData != null && guildData.guildUid > 0L)
			{
				NKCUtil.SetGameobjectActive(this.m_objGuild, true);
				NKCUIGuildBadge guildBadge = this.m_GuildBadge;
				if (guildBadge != null)
				{
					guildBadge.SetData(guildData.badgeId);
				}
				NKCUtil.SetLabelText(this.m_lbGuildName, guildData.guildName);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objGuild, false);
			}
			if (raidTryCount > 0)
			{
				NKCUtil.SetGameobjectActive(this.m_lbRaidTryCount, true);
				NKCUtil.SetLabelText(this.m_lbRaidTryCount, string.Format("{0}/{1}", raidTryCount, raidTryMaxCount));
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_lbRaidTryCount, false);
			}
			NKCUtil.SetLabelText(this.m_lbScore, score);
		}

		// Token: 0x06007771 RID: 30577 RVA: 0x0027BA88 File Offset: 0x00279C88
		public void SetEmpty()
		{
			if (this.m_btn != null)
			{
				this.m_btn.PointerClick.RemoveAllListeners();
			}
			if (this.m_slotProfile != null)
			{
				NKCUtil.SetGameobjectActive(this.m_slotProfile, true);
				this.m_slotProfile.SetProfiledata(0, 0, 0, null);
			}
			this.m_UserUid = 0L;
			NKCUtil.SetGameobjectActive(this.m_lbLevel, false);
			NKCUtil.SetLabelText(this.m_lbLevel, "-");
			NKCUtil.SetLabelText(this.m_lbName, "-");
			NKCUtil.SetLabelText(this.m_lbFriendCode, "");
			NKCUtil.SetGameobjectActive(this.m_lbRaidTryCount, false);
			NKCUtil.SetGameobjectActive(this.m_objGuild, false);
			NKCUtil.SetLabelText(this.m_lbScore, "-");
		}

		// Token: 0x06007772 RID: 30578 RVA: 0x0027BB48 File Offset: 0x00279D48
		public void OnClickProfile(NKCUISlotProfile slot, int frameID)
		{
			NKCPacketSender.Send_NKMPacket_USER_PROFILE_INFO_REQ(this.m_UserUid, NKM_DECK_TYPE.NDT_NORMAL);
		}

		// Token: 0x040063FE RID: 25598
		public NKCUIComStateButton m_btn;

		// Token: 0x040063FF RID: 25599
		public NKCUISlotProfile m_slotProfile;

		// Token: 0x04006400 RID: 25600
		public Text m_lbLevel;

		// Token: 0x04006401 RID: 25601
		public Text m_lbName;

		// Token: 0x04006402 RID: 25602
		public Text m_lbFriendCode;

		// Token: 0x04006403 RID: 25603
		public GameObject m_objGuild;

		// Token: 0x04006404 RID: 25604
		public NKCUIGuildBadge m_GuildBadge;

		// Token: 0x04006405 RID: 25605
		public Text m_lbGuildName;

		// Token: 0x04006406 RID: 25606
		public Text m_lbScore;

		// Token: 0x04006407 RID: 25607
		public Text m_lbRank;

		// Token: 0x04006408 RID: 25608
		[Header("레이드 전용")]
		public Text m_lbRaidTryCount;

		// Token: 0x04006409 RID: 25609
		private long m_UserUid;
	}
}
