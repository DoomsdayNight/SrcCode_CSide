using System;
using ClientPacket.Pvp;
using NKC.UI.Guild;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B85 RID: 2949
	public class NKCUIGauntletReplaySlot : MonoBehaviour
	{
		// Token: 0x06008808 RID: 34824 RVA: 0x002E04A4 File Offset: 0x002DE6A4
		public static NKCUIGauntletReplaySlot GetNewInstance(Transform parent, NKCUIGauntletReplaySlot.OnSelectReplayData onSelectReplayData, NKCUIGauntletReplaySlot.OnPlayReplay onPlayReplay)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_LOBBY_REPLAY_SLOT", false, null);
			NKCUIGauntletReplaySlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIGauntletReplaySlot>();
			if (component == null)
			{
				Debug.LogError("NKCUIGauntletReplaySlot Prefab null!");
				return null;
			}
			component.m_InstanceData = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.transform.localScale = Vector3.one;
			component.dOnSelectReplayData = onSelectReplayData;
			component.dOnPlayReplay = onPlayReplay;
			component.m_btnBattle.PointerClick.RemoveAllListeners();
			component.m_btnBattle.PointerClick.AddListener(new UnityAction(component.OnPlayReplayBtn));
			component.m_btnSlot.PointerClick.RemoveAllListeners();
			component.m_btnSlot.PointerClick.AddListener(new UnityAction(component.OnSelectReplayDataBtn));
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x06008809 RID: 34825 RVA: 0x002E0584 File Offset: 0x002DE784
		public void SetUI(int index, ReplayData cReplayData)
		{
			if (cReplayData == null)
			{
				return;
			}
			this.m_replayDataIndex = index;
			int userLevel = cReplayData.gameData.m_NKMGameTeamDataB.m_UserLevel;
			string userNickname = cReplayData.gameData.m_NKMGameTeamDataB.m_UserNickname;
			long friendCode = cReplayData.gameData.m_NKMGameTeamDataB.m_FriendCode;
			int num = 0;
			int score = cReplayData.gameData.m_NKMGameTeamDataB.m_Score;
			int tier = cReplayData.gameData.m_NKMGameTeamDataB.m_Tier;
			string msg = "";
			long guildUid = cReplayData.gameData.m_NKMGameTeamDataB.guildSimpleData.guildUid;
			long data = 0L;
			int num2 = 0;
			int unitID = cReplayData.gameData.m_NKMGameTeamDataB.m_MainShip.m_UnitID;
			NKMOperator @operator = cReplayData.gameData.m_NKMGameTeamDataB.m_Operator;
			NKCUtil.SetLabelText(this.m_txtLevel, NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, new object[]
			{
				userLevel
			});
			NKCUtil.SetLabelText(this.m_txtName, NKCUtilString.GetUserNickname(userNickname, true));
			NKCUtil.SetLabelText(this.m_txtCode, NKCUtilString.GetFriendCode(friendCode, true));
			int seasonID = NKCUtil.FindPVPSeasonIDForAsync(NKCSynchronizedTime.GetServerUTCTime(0.0));
			this.m_leagueTier.SetUI(NKCPVPManager.GetTierIconByTier(cReplayData.gameData.m_NKM_GAME_TYPE, seasonID, tier), NKCPVPManager.GetTierNumberByTier(cReplayData.gameData.m_NKM_GAME_TYPE, seasonID, tier));
			if (num == 0)
			{
				NKCUtil.SetLabelText(this.m_txtRank, "");
			}
			else
			{
				NKCUtil.SetLabelText(this.m_txtRank, string.Format(string.Format("{0}{1}", num, NKCUtilString.GetRankNumber(num, true)), Array.Empty<object>()));
			}
			NKCUtil.SetLabelText(this.m_txtScore, score.ToString());
			NKCUtil.SetGameobjectActive(this.m_objAddScore, false);
			if (unitID > 0)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
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
				if (@operator != null && @operator.id > 0)
				{
					NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(@operator.id);
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
			NKMUnitData leaderUnitData = cReplayData.gameData.m_NKMGameTeamDataB.GetLeaderUnitData();
			if (leaderUnitData != null && leaderUnitData.m_UnitID > 0)
			{
				this.m_imgMainUnit.sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, leaderUnitData.m_UnitID, leaderUnitData.m_SkinID);
			}
			else
			{
				this.m_imgMainUnit.sprite = this.SpriteFaceCardPrivate;
			}
			for (int i = 0; i < this.m_imgDeck.Length; i++)
			{
				Image image = this.m_imgDeck[i];
				if (i >= cReplayData.gameData.m_NKMGameTeamDataB.m_listUnitData.Count)
				{
					NKCUtil.SetGameobjectActive(image, false);
					return;
				}
				NKCUtil.SetGameobjectActive(image, true);
				NKMUnitData nkmunitData = cReplayData.gameData.m_NKMGameTeamDataB.m_listUnitData[i];
				if (nkmunitData.m_UnitID > 0)
				{
					NKMUnitTempletBase unitTempletBase3 = NKMUnitManager.GetUnitTempletBase(nkmunitData.m_UnitID);
					image.sprite = NKCResourceUtility.GetOrLoadMinimapFaceIcon(unitTempletBase3, false);
				}
				else
				{
					image.sprite = this.SpriteMiniMapFacePrivate;
				}
			}
			if (num2 >= 0)
			{
				NKCUtil.SetLabelText(this.m_txtDeckPower, num2.ToString());
			}
			else
			{
				NKCUtil.SetLabelText(this.m_txtDeckPower, "???");
			}
			if (this.m_objGuild != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objGuild, guildUid > 0L);
				if (this.m_objGuild.activeSelf)
				{
					this.m_GuildBadgeUI.SetData(data);
					NKCUtil.SetLabelText(this.m_lbGuildName, msg);
				}
			}
		}

		// Token: 0x0600880A RID: 34826 RVA: 0x002E093C File Offset: 0x002DEB3C
		private void OnPlayReplayBtn()
		{
			NKCUIGauntletReplaySlot.OnPlayReplay onPlayReplay = this.dOnPlayReplay;
			if (onPlayReplay == null)
			{
				return;
			}
			onPlayReplay(this.m_replayDataIndex);
		}

		// Token: 0x0600880B RID: 34827 RVA: 0x002E0954 File Offset: 0x002DEB54
		private void OnSelectReplayDataBtn()
		{
			NKCUIGauntletReplaySlot.OnSelectReplayData onSelectReplayData = this.dOnSelectReplayData;
			if (onSelectReplayData == null)
			{
				return;
			}
			onSelectReplayData(this.m_replayDataIndex);
		}

		// Token: 0x0600880C RID: 34828 RVA: 0x002E096C File Offset: 0x002DEB6C
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x04007463 RID: 29795
		public Text m_txtLevel;

		// Token: 0x04007464 RID: 29796
		public Text m_txtName;

		// Token: 0x04007465 RID: 29797
		public Text m_txtCode;

		// Token: 0x04007466 RID: 29798
		public NKCUILeagueTier m_leagueTier;

		// Token: 0x04007467 RID: 29799
		public Text m_txtRank;

		// Token: 0x04007468 RID: 29800
		public Text m_txtScore;

		// Token: 0x04007469 RID: 29801
		public Text m_txtAddScore;

		// Token: 0x0400746A RID: 29802
		public GameObject m_objAddScore;

		// Token: 0x0400746B RID: 29803
		public Image m_imgMainUnit;

		// Token: 0x0400746C RID: 29804
		public Image m_imgShip;

		// Token: 0x0400746D RID: 29805
		public GameObject m_objOperator;

		// Token: 0x0400746E RID: 29806
		public Image m_imgOperator;

		// Token: 0x0400746F RID: 29807
		public Image[] m_imgDeck = new Image[8];

		// Token: 0x04007470 RID: 29808
		public Text m_txtDeckPower;

		// Token: 0x04007471 RID: 29809
		public NKCUIComStateButton m_btnBattle;

		// Token: 0x04007472 RID: 29810
		public NKCUIComStateButton m_btnSlot;

		// Token: 0x04007473 RID: 29811
		public GameObject m_objGuild;

		// Token: 0x04007474 RID: 29812
		public NKCUIGuildBadge m_GuildBadgeUI;

		// Token: 0x04007475 RID: 29813
		public Text m_lbGuildName;

		// Token: 0x04007476 RID: 29814
		[Header("Sprite")]
		public Sprite SpriteFaceCardPrivate;

		// Token: 0x04007477 RID: 29815
		public Sprite SpriteMiniMapFacePrivate;

		// Token: 0x04007478 RID: 29816
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x04007479 RID: 29817
		private NKCUIGauntletReplaySlot.OnSelectReplayData dOnSelectReplayData;

		// Token: 0x0400747A RID: 29818
		private NKCUIGauntletReplaySlot.OnPlayReplay dOnPlayReplay;

		// Token: 0x0400747B RID: 29819
		private int m_replayDataIndex;

		// Token: 0x0200192A RID: 6442
		// (Invoke) Token: 0x0600B7D1 RID: 47057
		public delegate void OnSelectReplayData(int replayIndex);

		// Token: 0x0200192B RID: 6443
		// (Invoke) Token: 0x0600B7D5 RID: 47061
		public delegate void OnPlayReplay(int replayIndex);
	}
}
