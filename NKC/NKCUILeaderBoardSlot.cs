using System;
using ClientPacket.Common;
using NKC.UI.Guild;
using NKM;
using NKM.Guild;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200095A RID: 2394
	public class NKCUILeaderBoardSlot : MonoBehaviour
	{
		// Token: 0x06005FA3 RID: 24483 RVA: 0x001DC1BC File Offset: 0x001DA3BC
		public void InitUI()
		{
			if (this.m_btn != null)
			{
				this.m_btn.PointerClick.RemoveAllListeners();
				this.m_btn.PointerClick.AddListener(new UnityAction(this.OnClick));
				this.m_btn.PointerDown.RemoveAllListeners();
				this.m_btn.PointerDown.AddListener(delegate(PointerEventData eventData)
				{
					this.OnDragBeginImpl();
				});
			}
			if (this.m_mainUnitSlot != null)
			{
				this.m_mainUnitSlot.Init();
			}
		}

		// Token: 0x06005FA4 RID: 24484 RVA: 0x001DC248 File Offset: 0x001DA448
		public void SetData(LeaderBoardSlotData data, int boardCriteria, NKCUILeaderBoardSlot.OnDragBegin onDragBegin, bool bUsePercentRank = false, bool bShowMyRankIcon = true)
		{
			this.m_slotData = data;
			this.m_dOnDragBegin = onDragBegin;
			NKCUtil.SetGameobjectActive(this.m_mainUnitSlot, !data.bIsGuild);
			NKCUtil.SetGameobjectActive(this.m_objGuild, !data.bIsGuild);
			if (this.m_objGuild != null && this.m_objGuild.activeSelf)
			{
				this.SetUserSlotGuildData();
			}
			NKCUtil.SetGameobjectActive(this.m_guildBadge, data.bIsGuild);
			NKCUtil.SetGameobjectActive(this.m_objMasterName, data.bIsGuild);
			NKCUtil.SetGameobjectActive(this.m_objMemberCount, data.bIsGuild);
			if (data.rank > 0)
			{
				if (!bUsePercentRank)
				{
					NKCUtil.SetLabelText(this.m_lbRank, data.rank.ToString());
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbRank, string.Format("{0}%", data.rank));
				}
				if (data.rank <= 3 && !bUsePercentRank)
				{
					NKCUtil.SetGameobjectActive(this.m_imgMyRankIcon, true);
					NKCUtil.SetImageSprite(this.m_imgMyRankIcon, NKCUtil.GetRankIcon(data.rank), false);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_imgMyRankIcon, false);
				}
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbRank, "-");
				NKCUtil.SetGameobjectActive(this.m_imgMyRankIcon, false);
			}
			if (data.level > 0)
			{
				NKCUtil.SetGameobjectActive(this.m_lbLevel, true);
				NKCUtil.SetLabelText(this.m_lbLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, data.level));
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_lbLevel, false);
			}
			NKCUtil.SetLabelText(this.m_lbPoint, data.score);
			NKCUtil.SetImageSprite(this.m_imgPoint, NKCUtil.GetLeaderBoardPointIcon(data.boardType, boardCriteria, LEAGUE_TIER_ICON.LTI_NONE), false);
			if (!data.bIsGuild)
			{
				if (data.Profile == null || data.Profile.userUid == 0L)
				{
					this.SetEmptySlot();
					return;
				}
				NKCUtil.SetLabelText(this.m_lbName, data.nickname);
				NKCUtil.SetLabelText(this.m_lbFriendCode, NKCUtilString.GetFriendCode(data.Profile.friendCode));
				NKCUtil.SetGameobjectActive(this.m_objMyRank, bShowMyRankIcon && data.Profile.userUid == NKCScenManager.CurrentUserData().m_UserUID);
				NKCUISlotProfile mainUnitSlot = this.m_mainUnitSlot;
				if (mainUnitSlot == null)
				{
					return;
				}
				mainUnitSlot.SetProfiledata(data.Profile, null);
				return;
			}
			else
			{
				NKCUIGuildBadge guildBadge = this.m_guildBadge;
				if (guildBadge != null)
				{
					guildBadge.SetData(data.GuildData.badgeId);
				}
				if (data.GuildData.guildUid == 0L)
				{
					this.SetEmptySlot();
					return;
				}
				NKCUtil.SetLabelText(this.m_lbName, data.GuildData.guildName);
				NKCUtil.SetLabelText(this.m_lbFriendCode, "");
				NKCUtil.SetGameobjectActive(this.m_objMyRank, bShowMyRankIcon && NKCGuildManager.HasGuild() && data.GuildData.guildUid == NKCGuildManager.MyGuildData.guildUid);
				NKCUtil.SetLabelText(this.m_lbMasterName, data.Profile.nickname);
				NKCUtil.SetLabelText(this.m_lbMemberCount, string.Format("{0}/{1}", data.memberCount, NKMTempletContainer<GuildExpTemplet>.Find(data.level).MaxMemberCount));
				return;
			}
		}

		// Token: 0x06005FA5 RID: 24485 RVA: 0x001DC554 File Offset: 0x001DA754
		private void SetEmptySlot()
		{
			NKCUtil.SetLabelText(this.m_lbRank, "-");
			NKCUtil.SetLabelText(this.m_lbPoint, "-");
			NKCUtil.SetGameobjectActive(this.m_lbLevel, false);
			NKCUtil.SetLabelText(this.m_lbMasterName, "-");
			NKCUtil.SetLabelText(this.m_lbMemberCount, "-");
			NKCUtil.SetLabelText(this.m_lbName, "-");
			NKCUtil.SetLabelText(this.m_lbFriendCode, "");
			NKCUtil.SetGameobjectActive(this.m_objMyRank, false);
			NKCUtil.SetGameobjectActive(this.m_objGuild, false);
			NKCUtil.SetGameobjectActive(this.m_mainUnitSlot, true);
			this.m_mainUnitSlot.SetProfiledata(0, 0, 0, null);
			NKCUtil.SetGameobjectActive(this.m_guildBadge, false);
		}

		// Token: 0x06005FA6 RID: 24486 RVA: 0x001DC60C File Offset: 0x001DA80C
		private void SetUserSlotGuildData()
		{
			NKMGuildSimpleData guildData = this.m_slotData.GuildData;
			if (this.m_objGuild != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objGuild, guildData != null && guildData.guildUid > 0L);
				if (this.m_objGuild.activeSelf)
				{
					this.m_BadgeUI.SetData(guildData.badgeId);
					NKCUtil.SetLabelText(this.m_lbGuildName, guildData.guildName);
				}
			}
		}

		// Token: 0x06005FA7 RID: 24487 RVA: 0x001DC680 File Offset: 0x001DA880
		public void OnClick()
		{
			LeaderBoardType boardType = this.m_slotData.boardType;
			if (boardType != LeaderBoardType.BT_ACHIEVE && boardType - LeaderBoardType.BT_SHADOW > 3)
			{
				return;
			}
			if (!this.m_slotData.bIsGuild)
			{
				if (this.m_slotData.userUid == 0L)
				{
					return;
				}
				if (this.m_slotData.boardType == LeaderBoardType.BT_FIERCE)
				{
					NKCPacketSender.Send_NKMPacket_FIERCE_PROFILE_REQ(this.m_slotData.userUid, true);
					return;
				}
				NKCPacketSender.Send_NKMPacket_USER_PROFILE_INFO_REQ(this.m_slotData.userUid, NKM_DECK_TYPE.NDT_NORMAL);
				return;
			}
			else
			{
				if (this.m_slotData.GuildData.guildUid == 0L)
				{
					return;
				}
				NKCPacketSender.Send_NKMPacket_GUILD_DATA_REQ(this.m_slotData.GuildData.guildUid);
				return;
			}
		}

		// Token: 0x06005FA8 RID: 24488 RVA: 0x001DC718 File Offset: 0x001DA918
		private void OnDragBeginImpl()
		{
			if (!this.m_slotData.bIsGuild)
			{
				NKCUILeaderBoardSlot.OnDragBegin dOnDragBegin = this.m_dOnDragBegin;
				if (dOnDragBegin == null)
				{
					return;
				}
				dOnDragBegin();
			}
		}

		// Token: 0x04004BB8 RID: 19384
		[Header("공용")]
		public Image m_imgMyRankIcon;

		// Token: 0x04004BB9 RID: 19385
		public Text m_lbRank;

		// Token: 0x04004BBA RID: 19386
		public Image m_imgPoint;

		// Token: 0x04004BBB RID: 19387
		public Text m_lbPoint;

		// Token: 0x04004BBC RID: 19388
		public NKCUIComStateButton m_btn;

		// Token: 0x04004BBD RID: 19389
		public GameObject m_objMyRank;

		// Token: 0x04004BBE RID: 19390
		public Text m_lbLevel;

		// Token: 0x04004BBF RID: 19391
		public Text m_lbName;

		// Token: 0x04004BC0 RID: 19392
		[Header("유닛슬롯 전용")]
		public Text m_lbFriendCode;

		// Token: 0x04004BC1 RID: 19393
		public NKCUISlotProfile m_mainUnitSlot;

		// Token: 0x04004BC2 RID: 19394
		public GameObject m_objGuild;

		// Token: 0x04004BC3 RID: 19395
		public NKCUIGuildBadge m_BadgeUI;

		// Token: 0x04004BC4 RID: 19396
		public Text m_lbGuildName;

		// Token: 0x04004BC5 RID: 19397
		[Header("길드슬롯 전용")]
		public NKCUIGuildBadge m_guildBadge;

		// Token: 0x04004BC6 RID: 19398
		public GameObject m_objMasterName;

		// Token: 0x04004BC7 RID: 19399
		public Text m_lbMasterName;

		// Token: 0x04004BC8 RID: 19400
		public GameObject m_objMemberCount;

		// Token: 0x04004BC9 RID: 19401
		public Text m_lbMemberCount;

		// Token: 0x04004BCA RID: 19402
		private LeaderBoardSlotData m_slotData;

		// Token: 0x04004BCB RID: 19403
		private NKCUILeaderBoardSlot.OnDragBegin m_dOnDragBegin;

		// Token: 0x020015DC RID: 5596
		// (Invoke) Token: 0x0600AE75 RID: 44661
		public delegate void OnDragBegin();
	}
}
