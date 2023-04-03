using System;
using System.Collections.Generic;
using ClientPacket.Guild;
using NKM;
using NKM.Guild;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B34 RID: 2868
	public class NKCPopupGuildCoopStatusSlot : MonoBehaviour
	{
		// Token: 0x0600828A RID: 33418 RVA: 0x002C0984 File Offset: 0x002BEB84
		public void InitUI()
		{
			this.m_btnSlot.PointerClick.RemoveAllListeners();
			this.m_btnSlot.PointerClick.AddListener(new UnityAction(this.OnClickSlot));
			this.m_slotLeader.Init();
		}

		// Token: 0x0600828B RID: 33419 RVA: 0x002C09C0 File Offset: 0x002BEBC0
		public void SetData(GuildDungeonMemberInfo memberInfo, int rank)
		{
			this.m_UserUid = memberInfo.profile.userUid;
			NKCUtil.SetLabelText(this.m_lbRank, rank.ToString());
			this.m_slotLeader.SetProfiledata(memberInfo.profile, null);
			NKCUtil.SetLabelText(this.m_lbLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, memberInfo.profile.level));
			NKCUtil.SetLabelText(this.m_lbName, memberInfo.profile.nickname);
			NKCUtil.SetLabelText(this.m_lbPoint, memberInfo.bossPoint.ToString("N0"));
			NKCUtil.SetGameobjectActive(this.m_objGuildMaster, NKCGuildManager.MyGuildData.members.Find((NKMGuildMemberData x) => x.commonProfile.userUid == this.m_UserUid).grade == GuildMemberGrade.Master);
			for (int i = 0; i < this.m_lstHistory.Count; i++)
			{
				if (memberInfo.arenaList.Count <= i)
				{
					NKCUtil.SetGameobjectActive(this.m_lstHistory[i].m_objParent, i < NKMCommonConst.GuildDungeonConstTemplet.ArenaPlayCountBasic);
					NKCUtil.SetImageSprite(this.m_lstHistory[i].m_ImgIcon, this.GetMissionStatusIcon(-1), false);
					NKCUtil.SetLabelText(this.m_lstHistory[i].m_lbArenaName, string.Empty);
				}
				else if (NKCGuildCoopManager.m_dicGuildDungeonInfoTemplet.ContainsKey(memberInfo.arenaList[i].arenaId))
				{
					GuildDungeonInfoTemplet guildDungeonInfoTemplet = NKCGuildCoopManager.m_dicGuildDungeonInfoTemplet[memberInfo.arenaList[i].arenaId];
					if (guildDungeonInfoTemplet != null)
					{
						NKMDungeonManager.GetDungeonTempletBase(guildDungeonInfoTemplet.GetSeasonDungeonId());
						NKCUtil.SetImageSprite(this.m_lstHistory[i].m_ImgIcon, this.GetMissionStatusIcon(memberInfo.arenaList[i].grade), false);
						NKCUtil.SetLabelText(this.m_lstHistory[i].m_lbArenaName, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_DUNGEON_DUNGEON_UI_ARENA_INFO, memberInfo.arenaList[i].arenaId));
						NKCUtil.SetGameobjectActive(this.m_lstHistory[i].m_objParent, memberInfo.arenaList.Count > i);
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstHistory[i].m_objParent, false);
				}
			}
		}

		// Token: 0x0600828C RID: 33420 RVA: 0x002C0C00 File Offset: 0x002BEE00
		private Sprite GetMissionStatusIcon(int grade)
		{
			switch (grade)
			{
			case -1:
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_CONSORTIUM_COOP_Sprite", "NKM_UI_CONSORTIUM_COOP_MEDAL_ICON_NON", false);
			case 0:
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_CONSORTIUM_COOP_Sprite", "NKM_UI_CONSORTIUM_COOP_MEDAL_ICON_FAIL", false);
			case 1:
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_CONSORTIUM_COOP_Sprite", "NKM_UI_CONSORTIUM_COOP_MEDAL_ICON_03", false);
			case 2:
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_CONSORTIUM_COOP_Sprite", "NKM_UI_CONSORTIUM_COOP_MEDAL_ICON_02", false);
			case 3:
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_CONSORTIUM_COOP_Sprite", "NKM_UI_CONSORTIUM_COOP_MEDAL_ICON_01", false);
			default:
				return null;
			}
		}

		// Token: 0x0600828D RID: 33421 RVA: 0x002C0C81 File Offset: 0x002BEE81
		public void OnClickSlot()
		{
			NKCPacketSender.Send_NKMPacket_USER_PROFILE_INFO_REQ(this.m_UserUid, NKM_DECK_TYPE.NDT_NORMAL);
		}

		// Token: 0x04006EC1 RID: 28353
		private const string ICON_ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONSORTIUM_COOP_Sprite";

		// Token: 0x04006EC2 RID: 28354
		public NKCUIComStateButton m_btnSlot;

		// Token: 0x04006EC3 RID: 28355
		public Text m_lbRank;

		// Token: 0x04006EC4 RID: 28356
		public NKCUISlotProfile m_slotLeader;

		// Token: 0x04006EC5 RID: 28357
		public GameObject m_objGuildMaster;

		// Token: 0x04006EC6 RID: 28358
		public Text m_lbLevel;

		// Token: 0x04006EC7 RID: 28359
		public Text m_lbName;

		// Token: 0x04006EC8 RID: 28360
		public List<GuildCoopHistory> m_lstHistory = new List<GuildCoopHistory>();

		// Token: 0x04006EC9 RID: 28361
		public Text m_lbPoint;

		// Token: 0x04006ECA RID: 28362
		private long m_UserUid;
	}
}
