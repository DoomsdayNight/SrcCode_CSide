using System;
using ClientPacket.Common;
using ClientPacket.Pvp;
using NKC.UI.Guild;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B69 RID: 2921
	public class NKCUIGauntletAsyncSlotNew : MonoBehaviour
	{
		// Token: 0x0600858F RID: 34191 RVA: 0x002D3238 File Offset: 0x002D1438
		public static NKCUIGauntletAsyncSlotNew GetNewInstance(Transform parent, NKCUIGauntletAsyncSlotNew.OnTouchBattle onTouchBattle, NKCUIGauntletAsyncSlotNew.OnTouchProfile onTouchProfile)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_ASYNC_SLOT_NEW", false, null);
			NKCUIGauntletAsyncSlotNew component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIGauntletAsyncSlotNew>();
			if (component == null)
			{
				Debug.LogError("NKCUIGauntletAsyncSlotNew Prefab null!");
				return null;
			}
			component.m_InstanceData = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.transform.localScale = Vector3.one;
			component.dOnTouchBattle = onTouchBattle;
			component.dOnTouchProfile = onTouchProfile;
			NKCUtil.SetBindFunction(component.m_btnProfle, new UnityAction(component.OnClickProfile));
			NKCUtil.SetBindFunction(component.m_btnBattle, new UnityAction(component.OnClickBattle));
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x06008590 RID: 34192 RVA: 0x002D32F0 File Offset: 0x002D14F0
		public void SetUI(AsyncPvpTarget data, NKM_GAME_TYPE gameType)
		{
			if (data == null || data.asyncDeck == null)
			{
				return;
			}
			this.m_gameType = gameType;
			this.m_friendCode = data.userFriendCode;
			NKCUtil.SetLabelText(this.m_txtLevel, NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, new object[]
			{
				data.userLevel
			});
			NKCUtil.SetLabelText(this.m_txtName, NKCUtilString.GetUserNickname(data.userNickName, true));
			NKCUtil.SetLabelText(this.m_txtCode, NKCUtilString.GetFriendCode(data.userFriendCode, true));
			NKMPvpRankTemplet asyncPvpRankTempletByTier = NKCPVPManager.GetAsyncPvpRankTempletByTier(NKCUtil.FindPVPSeasonIDForAsync(NKCSynchronizedTime.GetServerUTCTime(0.0)), data.tier);
			if (asyncPvpRankTempletByTier != null)
			{
				this.m_leagueTier.SetUI(asyncPvpRankTempletByTier);
			}
			NKCUtil.SetLabelText(this.m_txtScore, data.score.ToString());
			int num = data.mainUnitId;
			int skinID = data.mainUnitSkinId;
			int selfieFrameId = data.selfieFrameId;
			if (num == 0)
			{
				NKMAsyncDeckData asyncDeck = data.asyncDeck;
				if (asyncDeck.units.Count == 0)
				{
					Debug.LogError("Gauntlet Async Slot - target deck cout 0");
					return;
				}
				foreach (NKMAsyncUnitData nkmasyncUnitData in asyncDeck.units)
				{
					if (nkmasyncUnitData.unitId > 0)
					{
						num = nkmasyncUnitData.unitId;
						skinID = nkmasyncUnitData.skinId;
						break;
					}
				}
			}
			this.m_SlotProfile.SetProfiledata(num, skinID, selfieFrameId, null);
			this.SetGuildData(data);
			NKCUtil.SetGameobjectActive(this.m_btnBattle.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_objRevengeResult, false);
		}

		// Token: 0x06008591 RID: 34193 RVA: 0x002D347C File Offset: 0x002D167C
		public void SetUI(RevengePvpTarget data, NKM_GAME_TYPE gameType)
		{
			if (data == null || data.asyncDeck == null)
			{
				return;
			}
			this.SetUI(NKCUIGauntletLobbyAsyncV2.ConventToAsyncPvpTarget(data), gameType);
			this.m_btnBattle.SetLock(!data.revengeAble, false);
			NKCUtil.SetGameobjectActive(this.m_btnBattle.gameObject, data.revengeAble);
			NKCUtil.SetGameobjectActive(this.m_objRevengeResult, !data.revengeAble);
			NKCUtil.SetGameobjectActive(this.m_objRevengeResultWin, data.result == PVP_RESULT.WIN);
			NKCUtil.SetGameobjectActive(this.m_objRevengeResultLose, data.result == PVP_RESULT.LOSE || data.result == PVP_RESULT.DRAW);
		}

		// Token: 0x06008592 RID: 34194 RVA: 0x002D3518 File Offset: 0x002D1718
		private void SetGuildData(AsyncPvpTarget data)
		{
			if (this.m_objGuild != null)
			{
				GameObject objGuild = this.m_objGuild;
				NKMGuildSimpleData guildData = data.guildData;
				NKCUtil.SetGameobjectActive(objGuild, guildData != null && guildData.guildUid > 0L);
				if (this.m_objGuild.activeSelf)
				{
					this.m_GuildBadgeUI.SetData(data.guildData.badgeId, true);
					NKCUtil.SetLabelText(this.m_lbGuildName, NKCUtilString.GetUserGuildName(data.guildData.guildName, true));
				}
			}
		}

		// Token: 0x06008593 RID: 34195 RVA: 0x002D3594 File Offset: 0x002D1794
		private void OnClickBattle()
		{
			NKCUIGauntletAsyncSlotNew.OnTouchBattle onTouchBattle = this.dOnTouchBattle;
			if (onTouchBattle == null)
			{
				return;
			}
			onTouchBattle(this.m_friendCode, this.m_gameType);
		}

		// Token: 0x06008594 RID: 34196 RVA: 0x002D35B2 File Offset: 0x002D17B2
		private void OnClickProfile()
		{
			NKCUIGauntletAsyncSlotNew.OnTouchProfile onTouchProfile = this.dOnTouchProfile;
			if (onTouchProfile == null)
			{
				return;
			}
			onTouchProfile(this.m_friendCode);
		}

		// Token: 0x06008595 RID: 34197 RVA: 0x002D35CA File Offset: 0x002D17CA
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x04007208 RID: 29192
		public Text m_txtLevel;

		// Token: 0x04007209 RID: 29193
		public Text m_txtName;

		// Token: 0x0400720A RID: 29194
		public Text m_txtCode;

		// Token: 0x0400720B RID: 29195
		public NKCUILeagueTier m_leagueTier;

		// Token: 0x0400720C RID: 29196
		public Text m_txtScore;

		// Token: 0x0400720D RID: 29197
		public NKCUISlotProfile m_SlotProfile;

		// Token: 0x0400720E RID: 29198
		public GameObject m_objSelected;

		// Token: 0x0400720F RID: 29199
		public NKCUIComStateButton m_btnProfle;

		// Token: 0x04007210 RID: 29200
		public NKCUIComStateButton m_btnBattle;

		// Token: 0x04007211 RID: 29201
		public GameObject m_objGuild;

		// Token: 0x04007212 RID: 29202
		public NKCUIGuildBadge m_GuildBadgeUI;

		// Token: 0x04007213 RID: 29203
		public Text m_lbGuildName;

		// Token: 0x04007214 RID: 29204
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x04007215 RID: 29205
		private NKCUIGauntletAsyncSlotNew.OnTouchBattle dOnTouchBattle;

		// Token: 0x04007216 RID: 29206
		private NKCUIGauntletAsyncSlotNew.OnTouchProfile dOnTouchProfile;

		// Token: 0x04007217 RID: 29207
		private long m_friendCode;

		// Token: 0x04007218 RID: 29208
		private NKM_GAME_TYPE m_gameType;

		// Token: 0x04007219 RID: 29209
		[Header("������")]
		public GameObject m_objRevengeResult;

		// Token: 0x0400721A RID: 29210
		public GameObject m_objRevengeResultWin;

		// Token: 0x0400721B RID: 29211
		public GameObject m_objRevengeResultLose;

		// Token: 0x0200190C RID: 6412
		// (Invoke) Token: 0x0600B77C RID: 46972
		public delegate void OnTouchBattle(long friendCode, NKM_GAME_TYPE gameType);

		// Token: 0x0200190D RID: 6413
		// (Invoke) Token: 0x0600B780 RID: 46976
		public delegate void OnTouchProfile(long friendCode);
	}
}
