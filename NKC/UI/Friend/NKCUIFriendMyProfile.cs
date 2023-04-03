using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Community;
using NKC.UI.Guild;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Friend
{
	// Token: 0x02000B15 RID: 2837
	public class NKCUIFriendMyProfile : MonoBehaviour
	{
		// Token: 0x17001513 RID: 5395
		// (get) Token: 0x060080DA RID: 32986 RVA: 0x002B67E0 File Offset: 0x002B49E0
		private NKCPopupEmblemList NKCPopupEmblemList
		{
			get
			{
				if (this.m_NKCPopupEmblemList == null)
				{
					NKCUIManager.LoadedUIData loadedUIData = NKCUIManager.OpenNewInstance<NKCPopupEmblemList>("AB_UI_NKM_UI_FRIEND", "NKM_UI_POPUP_EMBLEM", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), null);
					this.m_NKCPopupEmblemList = loadedUIData.GetInstance<NKCPopupEmblemList>();
					NKCPopupEmblemList nkcpopupEmblemList = this.m_NKCPopupEmblemList;
					if (nkcpopupEmblemList != null)
					{
						nkcpopupEmblemList.InitUI();
					}
				}
				return this.m_NKCPopupEmblemList;
			}
		}

		// Token: 0x17001514 RID: 5396
		// (get) Token: 0x060080DB RID: 32987 RVA: 0x002B6835 File Offset: 0x002B4A35
		public bool IsNKCPopupEmblemListOpen
		{
			get
			{
				return this.m_NKCPopupEmblemList != null && this.m_NKCPopupEmblemList.IsOpen;
			}
		}

		// Token: 0x060080DC RID: 32988 RVA: 0x002B6852 File Offset: 0x002B4A52
		public void CheckNKCPopupEmblemListAndClose()
		{
			if (this.m_NKCPopupEmblemList != null && this.m_NKCPopupEmblemList.IsOpen)
			{
				this.m_NKCPopupEmblemList.Close();
			}
		}

		// Token: 0x060080DD RID: 32989 RVA: 0x002B687C File Offset: 0x002B4A7C
		public void Init()
		{
			for (int i = 0; i < this.m_lstNKCDeckViewUnitSlot.Count; i++)
			{
				NKCDeckViewUnitSlot nkcdeckViewUnitSlot = this.m_lstNKCDeckViewUnitSlot[i];
				if (nkcdeckViewUnitSlot != null)
				{
					nkcdeckViewUnitSlot.Init(i, true);
				}
			}
			NKCUIComStateButton csbtnNicknameChange = this.m_csbtnNicknameChange;
			if (csbtnNicknameChange != null)
			{
				csbtnNicknameChange.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton csbtnNicknameChange2 = this.m_csbtnNicknameChange;
			if (csbtnNicknameChange2 != null)
			{
				csbtnNicknameChange2.PointerClick.AddListener(new UnityAction(this.OnClickChangeNickname));
			}
			this.m_cbtnCommentChange.PointerClick.RemoveAllListeners();
			this.m_cbtnCommentChange.PointerClick.AddListener(new UnityAction(this.OnClickChangeComment));
			this.m_IFComment.onValidateInput = new InputField.OnValidateInput(NKCFilterManager.FilterEmojiInput);
			this.m_IFComment.onValueChanged.RemoveAllListeners();
			this.m_IFComment.onValueChanged.AddListener(new UnityAction<string>(this.OnValueChangedComment));
			this.m_IFComment.onEndEdit.RemoveAllListeners();
			this.m_IFComment.onEndEdit.AddListener(new UnityAction<string>(this.OnDoneComment));
			this.m_cbtnCopy.PointerClick.RemoveAllListeners();
			this.m_cbtnCopy.PointerClick.AddListener(new UnityAction(this.OnClickCopy));
			NKCUtil.SetButtonClickDelegate(this.m_btnProfileUnitChange, new UnityAction(this.OpenProfileImageChange));
			NKCUtil.SetButtonClickDelegate(this.m_btnProfileFrameChange, new UnityAction(this.OpenProfileFrameChange));
			NKCUtil.SetButtonClickDelegate(this.m_btnProfileDeckChange, new UnityAction(this.OpenMainDeckSelectWindow));
			for (int j = 0; j < this.m_lstEmblem.Count; j++)
			{
				this.m_lstEmblem[j].Init();
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.GUILD, 0, 0))
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_PROFILE_CONSORTIUM_POLYARROW, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_PROFILE_CONSORTIUM_TEXT, false);
			}
			if (this.m_OperatorSlot != null)
			{
				this.m_OperatorSlot.Init(null);
			}
		}

		// Token: 0x060080DE RID: 32990 RVA: 0x002B6A65 File Offset: 0x002B4C65
		private void OnOKToChange(int id)
		{
			if (this.m_EmblemIndexToChange < 0)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_SET_EMBLEM_REQ(this.m_EmblemIndexToChange, id);
		}

		// Token: 0x060080DF RID: 32991 RVA: 0x002B6A7D File Offset: 0x002B4C7D
		private void OnClickChangeEmblem0(NKCUISlot.SlotData slotData, bool bLocked)
		{
			this.m_EmblemIndexToChange = 0;
			this.OpenEmblemPopup();
		}

		// Token: 0x060080E0 RID: 32992 RVA: 0x002B6A8C File Offset: 0x002B4C8C
		private void OnClickChangeEmblem1(NKCUISlot.SlotData slotData, bool bLocked)
		{
			this.m_EmblemIndexToChange = 1;
			this.OpenEmblemPopup();
		}

		// Token: 0x060080E1 RID: 32993 RVA: 0x002B6A9B File Offset: 0x002B4C9B
		private void OnClickChangeEmblem2(NKCUISlot.SlotData slotData, bool bLocked)
		{
			this.m_EmblemIndexToChange = 2;
			this.OpenEmblemPopup();
		}

		// Token: 0x060080E2 RID: 32994 RVA: 0x002B6AAC File Offset: 0x002B4CAC
		private void OpenEmblemPopup()
		{
			if (this.NKCPopupEmblemList == null)
			{
				return;
			}
			NKMUserProfileData userProfileData = NKCScenManager.CurrentUserData().UserProfileData;
			if (userProfileData == null)
			{
				return;
			}
			bool bUseEmpty = true;
			NKCScenManager.CurrentUserData().m_InventoryData.GetEmblemData();
			for (int i = 0; i < userProfileData.emblems.Count; i++)
			{
				if (userProfileData.emblems[i] != null && i == this.m_EmblemIndexToChange && userProfileData.emblems[i].id == 0)
				{
					bUseEmpty = false;
				}
			}
			this.NKCPopupEmblemList.Open(userProfileData.emblems[this.m_EmblemIndexToChange].id, bUseEmpty, new NKCPopupEmblemList.dOnClickOK(this.OnOKToChange));
		}

		// Token: 0x060080E3 RID: 32995 RVA: 0x002B6B59 File Offset: 0x002B4D59
		public bool IsOpen()
		{
			return base.gameObject.activeSelf;
		}

		// Token: 0x060080E4 RID: 32996 RVA: 0x002B6B68 File Offset: 0x002B4D68
		public void OnRecv(NKMPacket_SET_EMBLEM_ACK cNKMPacket_SET_EMBLEM_ACK)
		{
			if (cNKMPacket_SET_EMBLEM_ACK.index >= 0 && this.m_lstEmblem.Count > (int)cNKMPacket_SET_EMBLEM_ACK.index)
			{
				int index = (int)cNKMPacket_SET_EMBLEM_ACK.index;
				int itemId = cNKMPacket_SET_EMBLEM_ACK.itemId;
				long count = cNKMPacket_SET_EMBLEM_ACK.count;
				if (itemId > 0 && NKMItemManager.GetItemMiscTempletByID(itemId) != null)
				{
					this.m_lstEmblem[index].SetMiscItemData(itemId, count, false, true, true, this.GetOnClickMethod(index));
				}
				else
				{
					this.m_lstEmblem[index].SetEmpty(this.GetOnClickMethod(index));
				}
				NKCUtil.SetGameobjectActive(this.m_lstEmblemEffect[index], false);
				NKCUtil.SetGameobjectActive(this.m_lstEmblemEffect[index], true);
			}
		}

		// Token: 0x060080E5 RID: 32997 RVA: 0x002B6C10 File Offset: 0x002B4E10
		private void OnEnable()
		{
			for (int i = 0; i < this.m_lstEmblemEffect.Count; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_lstEmblemEffect[i], false);
			}
		}

		// Token: 0x060080E6 RID: 32998 RVA: 0x002B6C45 File Offset: 0x002B4E45
		public NKCUISlot.OnClick GetOnClickMethod(int index)
		{
			if (index == 0)
			{
				return new NKCUISlot.OnClick(this.OnClickChangeEmblem0);
			}
			if (index == 1)
			{
				return new NKCUISlot.OnClick(this.OnClickChangeEmblem1);
			}
			if (index == 2)
			{
				return new NKCUISlot.OnClick(this.OnClickChangeEmblem2);
			}
			return new NKCUISlot.OnClick(this.OnClickChangeEmblem0);
		}

		// Token: 0x060080E7 RID: 32999 RVA: 0x002B6C88 File Offset: 0x002B4E88
		public void UpdateMainCharUI()
		{
			NKMUserProfileData userProfileData = NKCScenManager.CurrentUserData().UserProfileData;
			if (userProfileData != null)
			{
				NKCUISlotProfile slotMyProfile = this.m_SlotMyProfile;
				if (slotMyProfile == null)
				{
					return;
				}
				slotMyProfile.SetProfiledata(userProfileData, null);
			}
		}

		// Token: 0x060080E8 RID: 33000 RVA: 0x002B6CB8 File Offset: 0x002B4EB8
		public void UpdateGuildData()
		{
			if (this.m_objGuild != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objGuild, NKCGuildManager.HasGuild());
				if (this.m_objGuild.activeSelf)
				{
					this.m_BadgeUI.SetData(NKCGuildManager.MyGuildData.badgeId);
					NKCUtil.SetLabelText(this.m_lbGuildName, NKCGuildManager.MyGuildData.name);
					NKCUtil.SetLabelText(this.m_lbGuildLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, NKCGuildManager.MyGuildData.guildLevel));
				}
			}
		}

		// Token: 0x060080E9 RID: 33001 RVA: 0x002B6D40 File Offset: 0x002B4F40
		public void UpdateCommentUI()
		{
			NKMUserProfileData userProfileData = NKCScenManager.CurrentUserData().UserProfileData;
			if (userProfileData != null && !string.IsNullOrEmpty(userProfileData.friendIntro))
			{
				this.m_IFComment.text = NKCFilterManager.CheckBadChat(userProfileData.friendIntro);
			}
		}

		// Token: 0x060080EA RID: 33002 RVA: 0x002B6D80 File Offset: 0x002B4F80
		public void UpdateDeckUI()
		{
			NKMUserProfileData userProfileData = NKCScenManager.CurrentUserData().UserProfileData;
			if (userProfileData != null && userProfileData.profileDeck != null)
			{
				NKMUnitTempletBase nkmunitTempletBase = (userProfileData.profileDeck.Ship != null) ? NKMUnitManager.GetUnitTempletBase(userProfileData.profileDeck.Ship.UnitId) : null;
				if (nkmunitTempletBase != null)
				{
					this.m_ANIM_SHIP_IMG.sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, nkmunitTempletBase);
				}
				else
				{
					this.m_ANIM_SHIP_IMG.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_DECK_VIEW_SPRITE", "NKM_DECK_VIEW_SHIP_UNKNOWN", false);
				}
				for (int i = 0; i < this.m_lstNKCDeckViewUnitSlot.Count; i++)
				{
					if (i < 8)
					{
						NKMUnitData nkmunitData = null;
						if (userProfileData.profileDeck.List[i] != null)
						{
							nkmunitData = new NKMUnitData();
							nkmunitData.FillDataFromDummy(userProfileData.profileDeck.List[i]);
						}
						this.m_lstNKCDeckViewUnitSlot[i].SetData(nkmunitData, false);
					}
				}
				if (NKCOperatorUtil.IsHide())
				{
					NKCUtil.SetGameobjectActive(this.m_OperatorSlot, false);
					return;
				}
				NKCUtil.SetGameobjectActive(this.m_OperatorSlot, true);
				if (userProfileData.profileDeck.operatorUnit == null)
				{
					this.m_OperatorSlot.SetLock();
					return;
				}
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(userProfileData.profileDeck.operatorUnit.UnitId);
				if (unitTempletBase != null)
				{
					this.m_OperatorSlot.SetData(unitTempletBase, userProfileData.profileDeck.operatorUnit.UnitLevel);
					return;
				}
				this.m_OperatorSlot.SetEmpty();
				return;
			}
			else
			{
				NKCUtil.SetImageSprite(this.m_ANIM_SHIP_IMG, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_DECK_VIEW_SPRITE", "NKM_DECK_VIEW_SHIP_UNKNOWN", false), false);
				for (int j = 0; j < this.m_lstNKCDeckViewUnitSlot.Count; j++)
				{
					if (j < 8)
					{
						this.m_lstNKCDeckViewUnitSlot[j].SetData(null, true);
					}
				}
				if (NKCOperatorUtil.IsHide())
				{
					NKCUtil.SetGameobjectActive(this.m_OperatorSlot, false);
					return;
				}
				NKCUtil.SetGameobjectActive(this.m_OperatorSlot, true);
				this.m_OperatorSlot.SetLock();
				return;
			}
		}

		// Token: 0x060080EB RID: 33003 RVA: 0x002B6F50 File Offset: 0x002B5150
		private void UpdateGauntletTierUI()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			PvpState pvpState;
			NKM_GAME_TYPE gameType;
			if (myUserData.m_PvpData.Score >= myUserData.m_AsyncData.Score)
			{
				pvpState = myUserData.m_PvpData;
				gameType = NKM_GAME_TYPE.NGT_PVP_RANK;
			}
			else
			{
				pvpState = myUserData.m_AsyncData;
				gameType = NKM_GAME_TYPE.NGT_ASYNC_PVP;
			}
			int num = NKCPVPManager.FindPvPSeasonID(gameType, NKCSynchronizedTime.GetServerUTCTime(0.0));
			int leagueScore = pvpState.Score;
			if (num != pvpState.SeasonID)
			{
				leagueScore = NKCPVPManager.GetResetScore(pvpState.SeasonID, pvpState.Score, gameType);
				NKMPvpRankTemplet rankTempletByScore = NKCPVPManager.GetRankTempletByScore(gameType, num, leagueScore);
				if (rankTempletByScore != null)
				{
					this.m_NKCUILeagueTier.SetUI(rankTempletByScore);
					this.m_lbGauntletTier.text = rankTempletByScore.GetLeagueName();
				}
				else
				{
					this.m_lbGauntletTier.text = "";
				}
			}
			else
			{
				NKMPvpRankTemplet rankTempletByTier = NKCPVPManager.GetRankTempletByTier(gameType, num, pvpState.LeagueTierID);
				if (rankTempletByTier != null)
				{
					this.m_NKCUILeagueTier.SetUI(rankTempletByTier);
					this.m_lbGauntletTier.text = rankTempletByTier.GetLeagueName();
				}
				else
				{
					this.m_lbGauntletTier.text = "";
				}
			}
			this.m_lbGauntletScore.text = leagueScore.ToString();
		}

		// Token: 0x060080EC RID: 33004 RVA: 0x002B706C File Offset: 0x002B526C
		private void UpdateEmblemUI()
		{
			NKMUserProfileData userProfileData = NKCScenManager.CurrentUserData().UserProfileData;
			if (userProfileData == null)
			{
				for (int i = 0; i < this.m_lstEmblem.Count; i++)
				{
					this.m_lstEmblem[i].SetEmpty(null);
				}
				return;
			}
			for (int j = 0; j < this.m_lstEmblem.Count; j++)
			{
				NKCUISlot nkcuislot = this.m_lstEmblem[j];
				if (j < userProfileData.emblems.Count && userProfileData.emblems[j] != null && userProfileData.emblems[j].id > 0 && NKMItemManager.GetItemMiscTempletByID(userProfileData.emblems[j].id) != null)
				{
					if (j <= 3)
					{
						nkcuislot.SetMiscItemData(userProfileData.emblems[j].id, userProfileData.emblems[j].count, false, true, true, this.GetOnClickMethod(j));
					}
					else
					{
						nkcuislot.SetEmpty(null);
					}
				}
				else if (j <= 3)
				{
					nkcuislot.SetEmpty(this.GetOnClickMethod(j));
				}
				else
				{
					nkcuislot.SetEmpty(null);
				}
			}
		}

		// Token: 0x060080ED RID: 33005 RVA: 0x002B717C File Offset: 0x002B537C
		public void Open()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.UpdateMainCharUI();
			this.UpdateDeckUI();
			this.UpdateGuildData();
			this.UpdateCommentUI();
			this.UpdateGauntletTierUI();
			this.m_EmblemIndexToChange = -1;
			this.UpdateEmblemUI();
			for (int i = 0; i < this.m_lstEmblemEffect.Count; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_lstEmblemEffect[i], false);
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				this.m_NKM_UI_FRIEND_PROFILE_INFO_NAME.text = myUserData.m_UserNickName;
				this.m_NKM_UI_FRIEND_PROFILE_INFO_LEVEL.text = string.Format(NKCUtilString.GET_STRING_FRIEND_INFO_LEVEL_ONE_PARAM, myUserData.UserLevel);
				this.m_UID_TEXT.text = NKCUtilString.GetFriendCode(myUserData.m_FriendCode);
			}
			NKCUtil.SetGameobjectActive(this.m_btnProfileFrameChange, NKCContentManager.IsContentsUnlocked(ContentsType.PROFILE_FRAME, 0, 0));
		}

		// Token: 0x060080EE RID: 33006 RVA: 0x002B7251 File Offset: 0x002B5451
		private void OnValueChangedComment(string str)
		{
			this.m_IFComment.textComponent.color = NKCUtil.GetColor("#4EC2F3");
			this.m_IFComment.textComponent.fontStyle = FontStyle.Normal;
		}

		// Token: 0x060080EF RID: 33007 RVA: 0x002B7280 File Offset: 0x002B5480
		private void OnDoneComment(string str)
		{
			if (!EventSystem.current.alreadySelecting)
			{
				EventSystem.current.SetSelectedGameObject(null);
			}
			NKMUserProfileData userProfileData = NKCScenManager.CurrentUserData().UserProfileData;
			if (userProfileData == null)
			{
				return;
			}
			this.m_IFComment.text = NKCFilterManager.CheckBadChat(this.m_IFComment.text);
			if (this.m_IFComment.text == NKCFilterManager.CheckBadChat(userProfileData.friendIntro))
			{
				if (NKCUIManager.FrontCanvas != null)
				{
					this.m_bCommentChangeButtonClicked = RectTransformUtility.RectangleContainsScreenPoint(this.m_cbtnCommentChange.GetComponent<RectTransform>(), Input.mousePosition, NKCUIManager.FrontCanvas.worldCamera);
				}
				if (NKCInputManager.IsChatSubmitEnter())
				{
					this.m_bCommentChangeButtonClicked = false;
				}
				return;
			}
			NKMPacket_FRIEND_PROFILE_MODIFY_INTRO_REQ nkmpacket_FRIEND_PROFILE_MODIFY_INTRO_REQ = new NKMPacket_FRIEND_PROFILE_MODIFY_INTRO_REQ();
			if (this.m_IFComment.text.Length >= 20)
			{
				nkmpacket_FRIEND_PROFILE_MODIFY_INTRO_REQ.intro = this.m_IFComment.text.Substring(0, 20);
			}
			else
			{
				nkmpacket_FRIEND_PROFILE_MODIFY_INTRO_REQ.intro = this.m_IFComment.text;
			}
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_FRIEND_PROFILE_MODIFY_INTRO_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060080F0 RID: 33008 RVA: 0x002B738C File Offset: 0x002B558C
		public void RefreshNickname()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				this.m_NKM_UI_FRIEND_PROFILE_INFO_NAME.text = myUserData.m_UserNickName;
			}
		}

		// Token: 0x060080F1 RID: 33009 RVA: 0x002B73B8 File Offset: 0x002B55B8
		private void OnClickChangeNickname()
		{
			if (NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(510) > 0L)
			{
				NKCPopupNickname.Instance.Open(null);
				return;
			}
			NKCShopManager.OpenItemLackPopup(510, 1);
		}

		// Token: 0x060080F2 RID: 33010 RVA: 0x002B73E9 File Offset: 0x002B55E9
		public void OnClickChangeComment()
		{
			if (this.m_bCommentChangeButtonClicked)
			{
				this.m_bCommentChangeButtonClicked = false;
				return;
			}
			this.m_IFComment.Select();
			this.m_IFComment.ActivateInputField();
		}

		// Token: 0x060080F3 RID: 33011 RVA: 0x002B7414 File Offset: 0x002B5614
		public void OnClickCopy()
		{
			NKMUserProfileData userProfileData = NKCScenManager.CurrentUserData().UserProfileData;
			if (userProfileData == null)
			{
				return;
			}
			TextEditor textEditor = new TextEditor();
			textEditor.text = userProfileData.commonProfile.friendCode.ToString();
			textEditor.OnFocus();
			textEditor.Copy();
		}

		// Token: 0x060080F4 RID: 33012 RVA: 0x002B7458 File Offset: 0x002B5658
		private void SendMainUnitChangeREQ(NKCUISlot slot)
		{
			if (slot == null)
			{
				return;
			}
			if (slot.GetSlotData() == null)
			{
				return;
			}
			if (slot.GetSlotData().eType != NKCUISlot.eSlotMode.Unit)
			{
				return;
			}
			if (!NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.m_illustrateUnit.Contains(slot.GetSlotData().ID))
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString("SI_DP_PROFILE_REPRESENT_SKIN_ONLY_NO_UNIT_ERROR", false), null, "");
				return;
			}
			NKMPacket_FRIEND_PROFILE_MODIFY_MAIN_CHAR_REQ nkmpacket_FRIEND_PROFILE_MODIFY_MAIN_CHAR_REQ = new NKMPacket_FRIEND_PROFILE_MODIFY_MAIN_CHAR_REQ();
			nkmpacket_FRIEND_PROFILE_MODIFY_MAIN_CHAR_REQ.mainCharId = slot.GetSlotData().ID;
			nkmpacket_FRIEND_PROFILE_MODIFY_MAIN_CHAR_REQ.mainCharSkinId = slot.GetSlotData().Data;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_FRIEND_PROFILE_MODIFY_MAIN_CHAR_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060080F5 RID: 33013 RVA: 0x002B7503 File Offset: 0x002B5703
		public void OpenProfileImageChange()
		{
			NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().OpenImageChangeForUnit(new NKCPopupImageChange.OnClickOK(this.SendMainUnitChangeREQ));
		}

		// Token: 0x060080F6 RID: 33014 RVA: 0x002B7520 File Offset: 0x002B5720
		public void OpenProfileFrameChange()
		{
			NKMUserProfileData userProfileData = NKCScenManager.CurrentUserData().UserProfileData;
			NKCUIPopupProfileFrameChange.Instance.Open(userProfileData, new NKCUIPopupProfileFrameChange.OnSelectFrame(this.OnFrameChanged));
		}

		// Token: 0x060080F7 RID: 33015 RVA: 0x002B754F File Offset: 0x002B574F
		public void OnFrameChanged(int frameID)
		{
			if (NKCScenManager.CurrentUserData().UserProfileData.commonProfile.frameId != frameID)
			{
				NKCPacketSender.Send_NKMPacket_USER_PROFILE_CHANGE_FRAME_REQ(frameID);
			}
		}

		// Token: 0x060080F8 RID: 33016 RVA: 0x002B7570 File Offset: 0x002B5770
		public void OpenMainDeckSelectWindow()
		{
			NKCUIDeckViewer.DeckViewerOption options = default(NKCUIDeckViewer.DeckViewerOption);
			options.MenuName = NKCUtilString.GET_STRING_FRIEND_MAIN_DECK;
			options.eDeckviewerMode = NKCUIDeckViewer.DeckViewerMode.MainDeckSelect;
			options.dOnSideMenuButtonConfirm = new NKCUIDeckViewer.DeckViewerOption.OnDeckSideButtonConfirm(this.OnClickMainDeckSelect);
			options.DeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_DAILY, 0);
			options.SelectLeaderUnitOnOpen = false;
			options.bEnableDefaultBackground = true;
			options.bUpsideMenuHomeButton = false;
			options.StageBattleStrID = string.Empty;
			NKCUIDeckViewer.Instance.Open(options, true);
		}

		// Token: 0x060080F9 RID: 33017 RVA: 0x002B75EC File Offset: 0x002B57EC
		public void OnClickMainDeckSelect(NKMDeckIndex selectedDeckIndex)
		{
			NKMPacket_FRIEND_PROFILE_MODIFY_DECK_REQ nkmpacket_FRIEND_PROFILE_MODIFY_DECK_REQ = new NKMPacket_FRIEND_PROFILE_MODIFY_DECK_REQ();
			nkmpacket_FRIEND_PROFILE_MODIFY_DECK_REQ.deckIndex = selectedDeckIndex;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_FRIEND_PROFILE_MODIFY_DECK_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060080FA RID: 33018 RVA: 0x002B7619 File Offset: 0x002B5819
		public void Close()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.CheckNKCPopupEmblemListAndClose();
		}

		// Token: 0x04006CF5 RID: 27893
		public Text m_NKM_UI_FRIEND_PROFILE_INFO_NAME;

		// Token: 0x04006CF6 RID: 27894
		public Text m_NKM_UI_FRIEND_PROFILE_INFO_LEVEL;

		// Token: 0x04006CF7 RID: 27895
		public Text m_UID_TEXT;

		// Token: 0x04006CF8 RID: 27896
		public InputField m_IFComment;

		// Token: 0x04006CF9 RID: 27897
		public GameObject m_objGuild;

		// Token: 0x04006CFA RID: 27898
		public NKCUIGuildBadge m_BadgeUI;

		// Token: 0x04006CFB RID: 27899
		public Text m_lbGuildName;

		// Token: 0x04006CFC RID: 27900
		public Text m_lbGuildLevel;

		// Token: 0x04006CFD RID: 27901
		public GameObject m_NKM_UI_FRIEND_PROFILE_CONSORTIUM_POLYARROW;

		// Token: 0x04006CFE RID: 27902
		public GameObject m_NKM_UI_FRIEND_PROFILE_CONSORTIUM_TEXT;

		// Token: 0x04006CFF RID: 27903
		public Image m_ANIM_SHIP_IMG;

		// Token: 0x04006D00 RID: 27904
		public NKCUIOperatorDeckSlot m_OperatorSlot;

		// Token: 0x04006D01 RID: 27905
		public List<NKCDeckViewUnitSlot> m_lstNKCDeckViewUnitSlot;

		// Token: 0x04006D02 RID: 27906
		public NKCUILeagueTier m_NKCUILeagueTier;

		// Token: 0x04006D03 RID: 27907
		public Text m_lbGauntletTier;

		// Token: 0x04006D04 RID: 27908
		public Text m_lbGauntletScore;

		// Token: 0x04006D05 RID: 27909
		public NKCUISlotProfile m_SlotMyProfile;

		// Token: 0x04006D06 RID: 27910
		public NKCUIComStateButton m_csbtnNicknameChange;

		// Token: 0x04006D07 RID: 27911
		public NKCUIComButton m_cbtnCopy;

		// Token: 0x04006D08 RID: 27912
		public NKCUIComButton m_cbtnCommentChange;

		// Token: 0x04006D09 RID: 27913
		public NKCUIComButton m_btnProfileUnitChange;

		// Token: 0x04006D0A RID: 27914
		public NKCUIComButton m_btnProfileFrameChange;

		// Token: 0x04006D0B RID: 27915
		public NKCUIComButton m_btnProfileDeckChange;

		// Token: 0x04006D0C RID: 27916
		public List<NKCUISlot> m_lstEmblem;

		// Token: 0x04006D0D RID: 27917
		public List<GameObject> m_lstEmblemEffect;

		// Token: 0x04006D0E RID: 27918
		private int m_EmblemIndexToChange = -1;

		// Token: 0x04006D0F RID: 27919
		private bool m_bCommentChangeButtonClicked;

		// Token: 0x04006D10 RID: 27920
		private NKCPopupEmblemList m_NKCPopupEmblemList;
	}
}
