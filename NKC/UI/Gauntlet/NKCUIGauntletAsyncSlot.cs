using System;
using System.Collections.Generic;
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
	// Token: 0x02000B68 RID: 2920
	public class NKCUIGauntletAsyncSlot : MonoBehaviour
	{
		// Token: 0x06008582 RID: 34178 RVA: 0x002D2A20 File Offset: 0x002D0C20
		public static NKCUIGauntletAsyncSlot GetNewInstance(Transform parent, NKCUIGauntletAsyncSlot.OnTouchBattle onTouchBattle)
		{
			NKCUIGauntletAsyncSlot loadAsset = NKCUIGauntletAsyncSlot.GetLoadAsset(parent, "AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_LOBBY_ASYNC_SLOT");
			if (null == loadAsset)
			{
				return null;
			}
			loadAsset.dOnTouchBattle = onTouchBattle;
			NKCUtil.SetBindFunction(loadAsset.m_btnBattle, new UnityAction(loadAsset.OnTouchBattleBtn));
			NKCUtil.SetBindFunction(loadAsset.m_btnSlot, new UnityAction(loadAsset.OnTouchBattleBtn));
			loadAsset.gameObject.SetActive(false);
			return loadAsset;
		}

		// Token: 0x06008583 RID: 34179 RVA: 0x002D2A8C File Offset: 0x002D0C8C
		public static NKCUIGauntletAsyncSlot GetNewInstance(Transform parent, NKCUIGauntletAsyncSlot.OnTouchBattleAsync onTouchBattle, NKCUIGauntletAsyncSlot.OnTouchProfile onTouchProfile)
		{
			NKCUIGauntletAsyncSlot loadAsset = NKCUIGauntletAsyncSlot.GetLoadAsset(parent, "AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_LOBBY_ASYNC_SLOT_SMALL");
			if (null == loadAsset)
			{
				return null;
			}
			loadAsset.dOnTouchBattleAsync = onTouchBattle;
			loadAsset.dOnTouchProfile = onTouchProfile;
			NKCUtil.SetBindFunction(loadAsset.m_btnSlot, new UnityAction(loadAsset.OnClickProfile));
			NKCUtil.SetBindFunction(loadAsset.m_btnBattle, new UnityAction(loadAsset.OnClickBattleAsync));
			loadAsset.gameObject.SetActive(false);
			return loadAsset;
		}

		// Token: 0x06008584 RID: 34180 RVA: 0x002D2B00 File Offset: 0x002D0D00
		public static NKCUIGauntletAsyncSlot GetLoadAsset(Transform parent, string bundleName, string assetName)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>(bundleName, assetName, false, null);
			NKCUIGauntletAsyncSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIGauntletAsyncSlot>();
			if (component == null)
			{
				Debug.LogError("NKCUIGauntletAsyncSlot Prefab null!");
				return null;
			}
			component.m_InstanceData = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.transform.localScale = Vector3.one;
			return component;
		}

		// Token: 0x06008585 RID: 34181 RVA: 0x002D2B68 File Offset: 0x002D0D68
		public void SetUI(AsyncPvpTarget data)
		{
			if (data == null || data.asyncDeck == null)
			{
				return;
			}
			this.m_friendCode = data.userFriendCode;
			NKCUtil.SetLabelText(this.m_txtLevel, NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, new object[]
			{
				data.userLevel
			});
			NKCUtil.SetLabelText(this.m_txtName, NKCUtilString.GetUserNickname(data.userNickName, true));
			NKCUtil.SetLabelText(this.m_txtCode, NKCUtilString.GetFriendCode(data.userFriendCode, true));
			int seasonID = NKCUtil.FindPVPSeasonIDForAsync(NKCSynchronizedTime.GetServerUTCTime(0.0));
			NKMPvpRankTemplet asyncPvpRankTempletByTier = NKCPVPManager.GetAsyncPvpRankTempletByTier(seasonID, data.tier);
			if (asyncPvpRankTempletByTier != null)
			{
				this.m_leagueTier.SetUI(asyncPvpRankTempletByTier);
			}
			if (data.rank == 0)
			{
				NKCUtil.SetLabelText(this.m_txtRank, "");
			}
			else
			{
				NKCUtil.SetLabelText(this.m_txtRank, string.Format(string.Format("{0}{1}", data.rank, NKCUtilString.GetRankNumber(data.rank, true)), Array.Empty<object>()));
			}
			NKCUtil.SetLabelText(this.m_txtScore, data.score.ToString());
			if (null != this.m_SlotProfile)
			{
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
			}
			PvpState asyncData = NKCScenManager.CurrentUserData().m_AsyncData;
			if (asyncData == null)
			{
				return;
			}
			NKMPvpRankTemplet asyncPvpRankTempletByScore = NKCPVPManager.GetAsyncPvpRankTempletByScore(seasonID, asyncData.Score);
			if (asyncPvpRankTempletByScore != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objAddScore, true);
				int num2 = NKCUtil.CalcAddScore(asyncPvpRankTempletByScore.LeagueType, asyncData.Score, data.score);
				NKCUtil.SetLabelText(this.m_txtAddScore, string.Format("+{0}", num2));
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objAddScore, false);
			}
			NKCUtil.SetGameobjectActive(this.m_objGuild, true);
			this.SetAsyncData(data.asyncDeck);
			this.SetGuildData(data);
			NKCUtil.SetGameobjectActive(this.m_objNPC, false);
			NKCUtil.SetGameobjectActive(this.m_objLock, false);
		}

		// Token: 0x06008586 RID: 34182 RVA: 0x002D2DDC File Offset: 0x002D0FDC
		public void SetUI(AsyncPvpTarget data, NKM_GAME_TYPE gameType)
		{
			this.m_gameType = gameType;
			this.SetUI(data);
		}

		// Token: 0x06008587 RID: 34183 RVA: 0x002D2DEC File Offset: 0x002D0FEC
		public void SetUI(NpcPvpTarget npcTarget)
		{
			if (npcTarget == null || npcTarget.asyncDeck == null)
			{
				return;
			}
			this.m_friendCode = npcTarget.userFriendCode;
			NKCUtil.SetLabelText(this.m_txtLevel, NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, new object[]
			{
				npcTarget.userLevel
			});
			NKCUtil.SetLabelText(this.m_txtName, NKCUtilString.GetUserNickname(npcTarget.userNickName, true));
			NKCUtil.SetLabelText(this.m_txtCode, NKCUtilString.GetFriendCode(npcTarget.userFriendCode, true));
			NKMPvpRankTemplet asyncPvpRankTempletByTier = NKCPVPManager.GetAsyncPvpRankTempletByTier(NKCUtil.FindPVPSeasonIDForAsync(NKCSynchronizedTime.GetServerUTCTime(0.0)), npcTarget.tier);
			if (asyncPvpRankTempletByTier != null)
			{
				this.m_leagueTier.SetUI(asyncPvpRankTempletByTier);
			}
			NKCUtil.SetLabelText(this.m_txtRank, "");
			NKCUtil.SetLabelText(this.m_txtAddScore, npcTarget.score.ToString());
			NKCUtil.SetGameobjectActive(this.m_objAddScore, false);
			NKCUtil.SetGameobjectActive(this.m_objNPC, true);
			NKCUtil.SetGameobjectActive(this.m_objLock, !npcTarget.isOpened);
			this.SetAsyncData(npcTarget.asyncDeck);
			NKCUtil.SetGameobjectActive(this.m_objGuild, false);
		}

		// Token: 0x06008588 RID: 34184 RVA: 0x002D2EFC File Offset: 0x002D10FC
		private void SetAsyncData(NKMAsyncDeckData asyncDeck)
		{
			if (asyncDeck == null)
			{
				return;
			}
			if (asyncDeck.units.Count == 0)
			{
				Debug.LogError("Gauntlet Async Slot - target deck cout 0");
				return;
			}
			if (asyncDeck.ship.unitId > 0)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(asyncDeck.ship.unitId);
				NKCUtil.SetGameobjectActive(this.m_imgShip, true);
				this.m_imgShip.sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, unitTempletBase);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_imgShip, false);
				this.m_imgShip.sprite = null;
			}
			NKCUtil.SetGameobjectActive(this.m_objOperator, false);
			if (!NKCOperatorUtil.IsHide())
			{
				NKCUtil.SetGameobjectActive(this.m_objOperator, true);
				if (asyncDeck.operatorUnit != null && asyncDeck.operatorUnit.id > 0)
				{
					NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(asyncDeck.operatorUnit.id);
					if (unitTempletBase2 != null)
					{
						NKCUtil.SetImageSprite(this.m_imgOperator, NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, unitTempletBase2), false);
					}
				}
				else
				{
					NKCUtil.SetImageSprite(this.m_imgOperator, NKCOperatorUtil.GetSpriteEmptySlot(), false);
				}
			}
			int num = 0;
			using (List<NKMAsyncUnitData>.Enumerator enumerator = asyncDeck.units.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.unitId > 0)
					{
						NKMAsyncUnitData nkmasyncUnitData = asyncDeck.units[num];
						this.m_imgMainUnit.sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, nkmasyncUnitData.unitId, nkmasyncUnitData.skinId);
						break;
					}
					num++;
				}
			}
			for (int i = 0; i < this.m_imgDeck.Length; i++)
			{
				Image image = this.m_imgDeck[i];
				if (i >= asyncDeck.units.Count)
				{
					NKCUtil.SetGameobjectActive(image, false);
					return;
				}
				NKCUtil.SetGameobjectActive(image, true);
				int unitId = asyncDeck.units[i].unitId;
				if (unitId > 0)
				{
					NKMUnitTempletBase unitTempletBase3 = NKMUnitManager.GetUnitTempletBase(unitId);
					image.sprite = NKCResourceUtility.GetOrLoadMinimapFaceIcon(unitTempletBase3, false);
				}
				else
				{
					image.sprite = this.SpriteMiniMapFacePrivate;
				}
			}
			if (asyncDeck.operationPower >= 0)
			{
				NKCUtil.SetLabelText(this.m_txtDeckPower, asyncDeck.operationPower.ToString());
				return;
			}
			NKCUtil.SetLabelText(this.m_txtDeckPower, "???");
		}

		// Token: 0x06008589 RID: 34185 RVA: 0x002D3118 File Offset: 0x002D1318
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

		// Token: 0x0600858A RID: 34186 RVA: 0x002D3194 File Offset: 0x002D1394
		private void OnTouchBattleBtn()
		{
			if (this.m_objLock.activeSelf)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_GAUNTLET_ASYNC_NPC_BLOCK_DESC, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			NKCUIGauntletAsyncSlot.OnTouchBattle onTouchBattle = this.dOnTouchBattle;
			if (onTouchBattle == null)
			{
				return;
			}
			onTouchBattle(this.m_friendCode);
		}

		// Token: 0x0600858B RID: 34187 RVA: 0x002D31CD File Offset: 0x002D13CD
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x0600858C RID: 34188 RVA: 0x002D31EC File Offset: 0x002D13EC
		private void OnClickBattleAsync()
		{
			NKCUIGauntletAsyncSlot.OnTouchBattleAsync onTouchBattleAsync = this.dOnTouchBattleAsync;
			if (onTouchBattleAsync == null)
			{
				return;
			}
			onTouchBattleAsync(this.m_friendCode, this.m_gameType);
		}

		// Token: 0x0600858D RID: 34189 RVA: 0x002D320A File Offset: 0x002D140A
		private void OnClickProfile()
		{
			NKCUIGauntletAsyncSlot.OnTouchProfile onTouchProfile = this.dOnTouchProfile;
			if (onTouchProfile == null)
			{
				return;
			}
			onTouchProfile(this.m_friendCode);
		}

		// Token: 0x040071EA RID: 29162
		public Text m_txtLevel;

		// Token: 0x040071EB RID: 29163
		public Text m_txtName;

		// Token: 0x040071EC RID: 29164
		public Text m_txtCode;

		// Token: 0x040071ED RID: 29165
		public NKCUILeagueTier m_leagueTier;

		// Token: 0x040071EE RID: 29166
		public Text m_txtRank;

		// Token: 0x040071EF RID: 29167
		public Text m_txtScore;

		// Token: 0x040071F0 RID: 29168
		public Text m_txtAddScore;

		// Token: 0x040071F1 RID: 29169
		public GameObject m_objAddScore;

		// Token: 0x040071F2 RID: 29170
		public Image m_imgMainUnit;

		// Token: 0x040071F3 RID: 29171
		public Image m_imgShip;

		// Token: 0x040071F4 RID: 29172
		public GameObject m_objOperator;

		// Token: 0x040071F5 RID: 29173
		public Image m_imgOperator;

		// Token: 0x040071F6 RID: 29174
		public Image[] m_imgDeck = new Image[8];

		// Token: 0x040071F7 RID: 29175
		public Text m_txtDeckPower;

		// Token: 0x040071F8 RID: 29176
		public NKCUIComStateButton m_btnBattle;

		// Token: 0x040071F9 RID: 29177
		public NKCUIComStateButton m_btnSlot;

		// Token: 0x040071FA RID: 29178
		public GameObject m_objGuild;

		// Token: 0x040071FB RID: 29179
		public NKCUIGuildBadge m_GuildBadgeUI;

		// Token: 0x040071FC RID: 29180
		public Text m_lbGuildName;

		// Token: 0x040071FD RID: 29181
		[Header("Sprite")]
		public Sprite SpriteFaceCardPrivate;

		// Token: 0x040071FE RID: 29182
		public Sprite SpriteMiniMapFacePrivate;

		// Token: 0x040071FF RID: 29183
		[Header("NPC")]
		public GameObject m_objNPC;

		// Token: 0x04007200 RID: 29184
		public GameObject m_objLock;

		// Token: 0x04007201 RID: 29185
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x04007202 RID: 29186
		private NKCUIGauntletAsyncSlot.OnTouchBattle dOnTouchBattle;

		// Token: 0x04007203 RID: 29187
		private long m_friendCode;

		// Token: 0x04007204 RID: 29188
		private NKCUIGauntletAsyncSlot.OnTouchBattleAsync dOnTouchBattleAsync;

		// Token: 0x04007205 RID: 29189
		private NKCUIGauntletAsyncSlot.OnTouchProfile dOnTouchProfile;

		// Token: 0x04007206 RID: 29190
		private NKM_GAME_TYPE m_gameType;

		// Token: 0x04007207 RID: 29191
		public NKCUISlotProfile m_SlotProfile;

		// Token: 0x02001909 RID: 6409
		// (Invoke) Token: 0x0600B770 RID: 46960
		public delegate void OnTouchBattle(long friendCode);

		// Token: 0x0200190A RID: 6410
		// (Invoke) Token: 0x0600B774 RID: 46964
		public delegate void OnTouchBattleAsync(long friendCode, NKM_GAME_TYPE gameType);

		// Token: 0x0200190B RID: 6411
		// (Invoke) Token: 0x0600B778 RID: 46968
		public delegate void OnTouchProfile(long friendCode);
	}
}
