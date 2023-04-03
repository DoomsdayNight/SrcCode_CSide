using System;
using ClientPacket.Common;
using NKC.UI.Guild;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B6D RID: 2925
	public class NKCUIGauntletLRSlot : MonoBehaviour
	{
		// Token: 0x060085BF RID: 34239 RVA: 0x002D46C0 File Offset: 0x002D28C0
		public static NKCUIGauntletLRSlot GetNewInstance(Transform parent, NKCUIGauntletLRSlot.OnDragBegin onDragBegin)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_RANK_SLOT", false, null);
			NKCUIGauntletLRSlot retVal = nkcassetInstanceData.m_Instant.GetComponent<NKCUIGauntletLRSlot>();
			if (retVal == null)
			{
				Debug.LogError("NKCUIGauntletLRSlot Prefab null!");
				return null;
			}
			retVal.m_InstanceData = nkcassetInstanceData;
			if (parent != null)
			{
				retVal.transform.SetParent(parent);
			}
			retVal.transform.localScale = new Vector3(1f, 1f, 1f);
			retVal.m_dOnDragBegin = onDragBegin;
			retVal.transform.localPosition = new Vector3(retVal.transform.localPosition.x, retVal.transform.localPosition.y, 0f);
			retVal.m_csbtnSimpleUserInfoSlot.PointerClick.RemoveAllListeners();
			retVal.m_csbtnSimpleUserInfoSlot.PointerClick.AddListener(new UnityAction(retVal.OnClick));
			retVal.m_csbtnSimpleUserInfoSlot.PointerDown.RemoveAllListeners();
			retVal.m_csbtnSimpleUserInfoSlot.PointerDown.AddListener(delegate(PointerEventData eventData)
			{
				retVal.OnDragBeginImpl();
			});
			retVal.gameObject.SetActive(false);
			return retVal;
		}

		// Token: 0x060085C0 RID: 34240 RVA: 0x002D4832 File Offset: 0x002D2A32
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x060085C1 RID: 34241 RVA: 0x002D4851 File Offset: 0x002D2A51
		private void OnDragBeginImpl()
		{
			if (this.m_dOnDragBegin != null)
			{
				this.m_dOnDragBegin();
			}
		}

		// Token: 0x060085C2 RID: 34242 RVA: 0x002D4866 File Offset: 0x002D2A66
		private void OnClick()
		{
			if (this.m_UserUID <= 0L)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_USER_PROFILE_INFO_REQ(this.m_UserUID, NKM_DECK_TYPE.NDT_PVP);
		}

		// Token: 0x060085C3 RID: 34243 RVA: 0x002D4880 File Offset: 0x002D2A80
		public void SetUI(NKMUserSimpleProfileData cNKMUserSimpleProfileData, int rank, NKM_GAME_TYPE game_type)
		{
			if (cNKMUserSimpleProfileData == null)
			{
				return;
			}
			this.m_UserUID = cNKMUserSimpleProfileData.userUid;
			int seasonID = NKCPVPManager.FindPvPSeasonID(game_type, NKCSynchronizedTime.GetServerUTCTime(0.0));
			this.m_NKCUILeagueTier.SetUI(NKCPVPManager.GetTierIconByTier(game_type, seasonID, cNKMUserSimpleProfileData.pvpTier), NKCPVPManager.GetTierNumberByTier(game_type, seasonID, cNKMUserSimpleProfileData.pvpTier));
			NKCUtil.SetGameobjectActive(this.m_obj1STCrown, rank == 1);
			NKCUtil.SetGameobjectActive(this.m_obj1ST_BG, rank == 1);
			NKCUtil.SetGameobjectActive(this.m_obj2NDCrown, rank == 2);
			NKCUtil.SetGameobjectActive(this.m_obj2ND_BG, rank == 2);
			NKCUtil.SetGameobjectActive(this.m_obj3RDCrown, rank == 3);
			NKCUtil.SetGameobjectActive(this.m_obj3RD_BG, rank == 3);
			bool bOpponent = cNKMUserSimpleProfileData.userUid != NKCScenManager.CurrentUserData().m_UserUID;
			this.m_lbName.text = NKCUtilString.GetUserNickname(cNKMUserSimpleProfileData.nickname, bOpponent);
			this.m_lbUID.text = NKCUtilString.GetFriendCode(cNKMUserSimpleProfileData.friendCode, bOpponent);
			this.m_lbScore.text = cNKMUserSimpleProfileData.pvpScore.ToString();
			this.m_lbLevel.text = string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, cNKMUserSimpleProfileData.level);
			this.m_lbRank.text = rank.ToString() + NKCUtilString.GetRankNumber(rank, true);
			bool flag = false;
			if (this.m_UserUID == NKCScenManager.CurrentUserData().m_UserUID)
			{
				flag = true;
			}
			NKCUtil.SetGameobjectActive(this.m_objMySlot, flag);
			if (flag)
			{
				this.m_lbName.color = NKCUtil.GetColor("#FFDF5D");
			}
			else
			{
				this.m_lbName.color = Color.white;
			}
			if (cNKMUserSimpleProfileData.mainUnitId != 0)
			{
				this.m_SlotProfile.SetProfiledata(cNKMUserSimpleProfileData.mainUnitId, cNKMUserSimpleProfileData.mainUnitSkinId, cNKMUserSimpleProfileData.selfieFrameId, null);
			}
			NKCUtil.SetGameobjectActive(this.m_SlotProfile, cNKMUserSimpleProfileData.mainUnitId != 0);
			this.SetGuildData(cNKMUserSimpleProfileData, bOpponent);
		}

		// Token: 0x060085C4 RID: 34244 RVA: 0x002D4A58 File Offset: 0x002D2C58
		private void SetGuildData(NKMUserSimpleProfileData data, bool bOpponent)
		{
			if (this.m_objGuild != null)
			{
				GameObject objGuild = this.m_objGuild;
				NKMGuildSimpleData guildData = data.guildData;
				NKCUtil.SetGameobjectActive(objGuild, guildData != null && guildData.guildUid > 0L);
				if (this.m_objGuild.activeSelf)
				{
					this.m_GuildBadgeUI.SetData(data.guildData.badgeId, bOpponent);
					NKCUtil.SetLabelText(this.m_lbGuildName, NKCUtilString.GetUserGuildName(data.guildData.guildName, bOpponent));
				}
			}
		}

		// Token: 0x04007261 RID: 29281
		public NKCUILeagueTier m_NKCUILeagueTier;

		// Token: 0x04007262 RID: 29282
		public Text m_lbRank;

		// Token: 0x04007263 RID: 29283
		public GameObject m_obj1STCrown;

		// Token: 0x04007264 RID: 29284
		public GameObject m_obj1ST_BG;

		// Token: 0x04007265 RID: 29285
		public GameObject m_obj2NDCrown;

		// Token: 0x04007266 RID: 29286
		public GameObject m_obj2ND_BG;

		// Token: 0x04007267 RID: 29287
		public GameObject m_obj3RDCrown;

		// Token: 0x04007268 RID: 29288
		public GameObject m_obj3RD_BG;

		// Token: 0x04007269 RID: 29289
		public Text m_lbLevel;

		// Token: 0x0400726A RID: 29290
		public Text m_lbName;

		// Token: 0x0400726B RID: 29291
		public Text m_lbUID;

		// Token: 0x0400726C RID: 29292
		public Text m_lbScore;

		// Token: 0x0400726D RID: 29293
		public NKCUIComStateButton m_csbtnSimpleUserInfoSlot;

		// Token: 0x0400726E RID: 29294
		public GameObject m_objMySlot;

		// Token: 0x0400726F RID: 29295
		public GameObject m_objGuild;

		// Token: 0x04007270 RID: 29296
		public NKCUIGuildBadge m_GuildBadgeUI;

		// Token: 0x04007271 RID: 29297
		public Text m_lbGuildName;

		// Token: 0x04007272 RID: 29298
		public NKCUISlotProfile m_SlotProfile;

		// Token: 0x04007273 RID: 29299
		private long m_UserUID;

		// Token: 0x04007274 RID: 29300
		private NKCUIGauntletLRSlot.OnDragBegin m_dOnDragBegin;

		// Token: 0x04007275 RID: 29301
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x02001910 RID: 6416
		// (Invoke) Token: 0x0600B78B RID: 46987
		public delegate void OnDragBegin();
	}
}
