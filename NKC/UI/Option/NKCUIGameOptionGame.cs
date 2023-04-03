using System;
using ClientPacket.Common;
using NKC.Localization;
using NKC.Publisher;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Option
{
	// Token: 0x02000B8C RID: 2956
	public class NKCUIGameOptionGame : NKCUIGameOptionContentBase
	{
		// Token: 0x06008877 RID: 34935 RVA: 0x002E2AC0 File Offset: 0x002E0CC0
		public override void Init()
		{
			this.GAME_CAMERA_SHAKE_STRINGS = new string[]
			{
				NKCUtilString.GET_STRING_NO_EXIST,
				NKCUtilString.GET_STRING_WEAK,
				NKCUtilString.GET_STRING_NORMAL
			};
			this.GAME_DAMAGE_NUMBER_STRINGS = new string[]
			{
				NKCUtilString.GET_STRING_NO_EXIST,
				NKCUtilString.GET_STRING_LIMITED,
				NKCUtilString.GET_STRING_EXIST
			};
			this.GAME_STREAMING_OPTION_STRINGS = new string[]
			{
				NKCStringTable.GetString("SI_DP_OPTION_STREAMING_NORMAL", false),
				NKCStringTable.GetString("SI_DP_OPTION_STREAMING_HIDE_OPPONENT", false),
				NKCStringTable.GetString("SI_DP_OPTION_STREAMING_HIDE_ALL", false)
			};
			this.GAME_PRIVATE_PVP_INVITE_OPTION_STRINGS = new string[]
			{
				NKCStringTable.GetString("SI_DP_OPTION_FRIENDLY_MATCH_INVITE_FRIEND", false),
				NKCStringTable.GetString("SI_DP_OPTION_FRIENDLY_MATCH_INVITE_CONSORTIUM_MEMBER", false),
				NKCStringTable.GetString("SI_DP_OPTION_FRIENDLY_MATCH_INVITE_REJECT_ALL", false),
				NKCStringTable.GetString("SI_DP_OPTION_FRIENDLY_MATCH_INVITE_ACCEPT_ALL", false)
			};
			this.GAME_CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO_STRINGS = new string[]
			{
				NKCUtilString.GET_STRING_OPTION_CUTSCEN_NEXT_TALK_SPEED_WHEN_AUTO_FAST,
				NKCUtilString.GET_STRING_OPTION_CUTSCEN_NEXT_TALK_SPEED_WHEN_AUTO_NORMAL,
				NKCUtilString.GET_STRING_OPTION_CUTSCEN_NEXT_TALK_SPEED_WHEN_AUTO_SLOW
			};
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			this.m_NKM_UI_GAME_OPTION_GAME_SLOT1_BTN.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickUseActionCameraButton));
			this.m_NKM_UI_GAME_OPTION_GAME_SLOT2_BTN.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickUseTrackCameraButton));
			this.m_NKM_UI_GAME_OPTION_GAME_SLOT3_BTN.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickViewSkillCutInButton));
			this.m_NKM_UI_GAME_OPTION_GAME_SLOT7_BTN.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickUseUseClassGuideButton));
			NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_GAME_SLOT8_BTN_Auto_Respawn, new UnityAction(this.OnClickUsePvPAutoRespawn));
			this.m_NKM_UI_GAME_OPTION_GAME_SLOT9_Auto_Sync_Friend_Deck.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickAutoSyncFriendDeck));
			this.m_NKM_UI_GAME_OPTION_GAME_NPC_SUBTITLE.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickNpcSubtitle));
			this.m_NKM_UI_GAME_OPTION_GAME_NPC_SUBTITLE_SHOW_NORMAL_AFTER_LIFE_TIME.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickNpcSubtitleShowNormalAfterLifeTime));
			NKCUtil.SetGameobjectActive(this.m_objNpcSubtitle, NKCContentManager.IsContentsUnlocked(ContentsType.NPC_SUBTITLE, 0, 0));
			this.m_NKM_UI_GAME_OPTION_GAME_SLOT4_BTN.Init(0, 2, (int)gameOptionData.CameraShakeLevel, this.GAME_CAMERA_SHAKE_STRINGS, new NKCUIGameOptionMultiStateButton.OnClicked(this.OnClickUseCameraShakeButton));
			this.m_NKM_UI_GAME_OPTION_GAME_DAMAGE_AND_BUFF_NUMBER_FX.Init(0, 2, (int)gameOptionData.UseDamageAndBuffNumberFx, this.GAME_DAMAGE_NUMBER_STRINGS, new NKCUIGameOptionMultiStateButton.OnClicked(this.OnClickUseDamageNumberFx));
			this.m_NKM_UI_GAME_OPTION_GAME_CUTSCEN_NEXT_TALK_CHANGE_SPEED.Init(0, 2, (int)gameOptionData.CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO, this.GAME_CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO_STRINGS, new NKCUIGameOptionMultiStateButton.OnClicked(this.OnClickUseCutscenNextTalkChangeSpeedButton));
			if (this.m_MBtnStreamingOption != null)
			{
				this.m_MBtnStreamingOption.Init(0, 2, (int)gameOptionData.eStreamingOption, this.GAME_STREAMING_OPTION_STRINGS, new NKCUIGameOptionMultiStateButton.OnClicked(this.OnClickBtnStreamingOption));
			}
			if (this.m_MBtnPrivatePvpInviteOption != null)
			{
				this.m_MBtnPrivatePvpInviteOption.Init(0, 3, (int)gameOptionData.ePrivatePvpInviteOption, this.GAME_PRIVATE_PVP_INVITE_OPTION_STRINGS, new NKCUIGameOptionMultiStateButton.OnClicked(this.OnClickBtnPrivatePvpInviteOption));
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_GAME_LANG_KEY_ONLY.gameObject, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_GAME_LANG_SLOT, NKCLocalization.GetSelectLanguageSet().Count > 1);
			this.m_Reserved_NKM_NATIONAL_CODE_ToChange = NKM_NATIONAL_CODE.NNC_END;
			this.m_NKM_UI_GAME_OPTION_GAME_LANG_CHANGE_Text.text = NKCUtilString.GET_STRING_OPTION_LANGUAGE_CHANGE;
			if (this.m_NKM_UI_GAME_OPTION_GAME_MEM_OPTIMIZE != null)
			{
				this.m_NKM_UI_GAME_OPTION_GAME_MEM_OPTIMIZE.OnValueChanged.RemoveAllListeners();
				this.m_NKM_UI_GAME_OPTION_GAME_MEM_OPTIMIZE.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickMemOptimize));
			}
			if (this.m_tglChatContent != null)
			{
				this.m_tglChatContent.OnValueChanged.RemoveAllListeners();
				this.m_tglChatContent.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickChatNotify));
			}
		}

		// Token: 0x06008878 RID: 34936 RVA: 0x002E2E3C File Offset: 0x002E103C
		public void OnValueKeyOnlyChanged(bool bValue)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			gameOptionData.UseKeyStringView = bValue;
		}

		// Token: 0x06008879 RID: 34937 RVA: 0x002E2E60 File Offset: 0x002E1060
		public override void SetContent()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			this.m_NKM_UI_GAME_OPTION_GAME_SLOT1_BTN.Select(gameOptionData.ActionCamera, true, false);
			this.m_NKM_UI_GAME_OPTION_GAME_SLOT2_BTN.Select(gameOptionData.TrackCamera, true, false);
			this.m_NKM_UI_GAME_OPTION_GAME_SLOT3_BTN.Select(gameOptionData.ViewSkillCutIn, true, false);
			this.m_NKM_UI_GAME_OPTION_GAME_SLOT4_BTN.ChangeValue((int)gameOptionData.CameraShakeLevel);
			this.m_NKM_UI_GAME_OPTION_GAME_SLOT7_BTN.Select(gameOptionData.UseClassGuide, true, false);
			this.m_NKM_UI_GAME_OPTION_GAME_DAMAGE_AND_BUFF_NUMBER_FX.ChangeValue((int)gameOptionData.UseDamageAndBuffNumberFx);
			this.OnClickUsePvPAutoRespawn(gameOptionData.PvPAutoRespawn);
			this.m_NKM_UI_GAME_OPTION_GAME_SLOT9_Auto_Sync_Friend_Deck.Select(gameOptionData.AutSyncFriendDeck, true, false);
			this.m_MBtnStreamingOption.ChangeValue((int)gameOptionData.eStreamingOption);
			this.m_MBtnPrivatePvpInviteOption.ChangeValue((int)gameOptionData.ePrivatePvpInviteOption);
			this.m_NKM_UI_GAME_OPTION_GAME_LANG_CHANGE.PointerClick.RemoveAllListeners();
			this.m_NKM_UI_GAME_OPTION_GAME_LANG_CHANGE.PointerClick.AddListener(new UnityAction(this.OpenLangChangeList));
			this.m_NKM_UI_GAME_OPTION_GAME_CUTSCEN_NEXT_TALK_CHANGE_SPEED.ChangeValue((int)gameOptionData.CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO);
			this.m_NKM_UI_GAME_OPTION_GAME_NPC_SUBTITLE.Select(gameOptionData.UseNpcSubtitle, true, false);
			this.m_NKM_UI_GAME_OPTION_GAME_NPC_SUBTITLE_SHOW_NORMAL_AFTER_LIFE_TIME.Select(gameOptionData.UseShowNormalSubtitleAfterLifeTime, true, false);
			if (!NKCDefineManager.DEFINE_SERVICE() || NKCUIGameOption.GetShowHiddenOption())
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_GAME_MEM_OPTIMIZE.transform.parent, true);
				if (this.m_NKM_UI_GAME_OPTION_GAME_MEM_OPTIMIZE != null)
				{
					this.m_NKM_UI_GAME_OPTION_GAME_MEM_OPTIMIZE.Select(gameOptionData.UseMemoryOptimize, true, false);
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_GAME_MEM_OPTIMIZE.transform.parent, false);
			}
			this.m_Reserved_NKM_NATIONAL_CODE_ToChange = NKM_NATIONAL_CODE.NNC_END;
			if (!NKMOpenTagManager.IsOpened("CHAT_PRIVATE"))
			{
				NKCUtil.SetGameobjectActive(this.m_objChatContent, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objChatContent, true);
			NKCUIComToggle tglChatContent = this.m_tglChatContent;
			if (tglChatContent == null)
			{
				return;
			}
			tglChatContent.Select(gameOptionData.UseChatContent, true, false);
		}

		// Token: 0x0600887A RID: 34938 RVA: 0x002E3039 File Offset: 0x002E1239
		private void OpenLangChangeList()
		{
			NKCUIPopupLanguageSelect.Instance.Open(NKCLocalization.GetSelectLanguageSet(), new NKCUIPopupLanguageSelect.OnClose(this.ChangeLangDoubleCheck));
		}

		// Token: 0x0600887B RID: 34939 RVA: 0x002E3058 File Offset: 0x002E1258
		private void OnClickOKToChangeLang()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			if (this.m_Reserved_NKM_NATIONAL_CODE_ToChange != NKM_NATIONAL_CODE.NNC_END)
			{
				gameOptionData.NKM_NATIONAL_CODE = this.m_Reserved_NKM_NATIONAL_CODE_ToChange;
				NKCGameOptionData.SaveOnlyLang(this.m_Reserved_NKM_NATIONAL_CODE_ToChange);
				NKCPublisherModule.Localization.SetPublisherModuleLanguage(this.m_Reserved_NKM_NATIONAL_CODE_ToChange);
				Application.Quit();
			}
			this.m_Reserved_NKM_NATIONAL_CODE_ToChange = NKM_NATIONAL_CODE.NNC_END;
		}

		// Token: 0x0600887C RID: 34940 RVA: 0x002E30B2 File Offset: 0x002E12B2
		private void ChangeLangDoubleCheck(NKM_NATIONAL_CODE eNKM_NATIONAL_CODE)
		{
			if (NKCStringTable.GetNationalCode() == eNKM_NATIONAL_CODE)
			{
				return;
			}
			this.m_Reserved_NKM_NATIONAL_CODE_ToChange = eNKM_NATIONAL_CODE;
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_OPTION_GAME_LANG_CHANGE_REQ, new NKCPopupOKCancel.OnButton(this.OnClickOKToChangeLang), null, false);
		}

		// Token: 0x0600887D RID: 34941 RVA: 0x002E30E4 File Offset: 0x002E12E4
		private void OnClickUseActionCameraButton(bool use)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			gameOptionData.SetUseActionCamera(use, false);
		}

		// Token: 0x0600887E RID: 34942 RVA: 0x002E3108 File Offset: 0x002E1308
		private void OnClickUseTrackCameraButton(bool use)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			gameOptionData.SetUseTrackCamera(use, false);
		}

		// Token: 0x0600887F RID: 34943 RVA: 0x002E312C File Offset: 0x002E132C
		private void OnClickViewSkillCutInButton(bool view)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			gameOptionData.SetViewSkillCutIn(view, false);
		}

		// Token: 0x06008880 RID: 34944 RVA: 0x002E3150 File Offset: 0x002E1350
		private void OnClickNpcSubtitle(bool use)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			gameOptionData.UseNpcSubtitle = use;
		}

		// Token: 0x06008881 RID: 34945 RVA: 0x002E3174 File Offset: 0x002E1374
		private void OnClickNpcSubtitleShowNormalAfterLifeTime(bool use)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			gameOptionData.UseShowNormalSubtitleAfterLifeTime = use;
		}

		// Token: 0x06008882 RID: 34946 RVA: 0x002E3198 File Offset: 0x002E1398
		public void OnClickMemOptimize(bool use)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			gameOptionData.UseMemoryOptimize = use;
			NKCPacketObjectPool.SetUsePool(use);
		}

		// Token: 0x06008883 RID: 34947 RVA: 0x002E31C4 File Offset: 0x002E13C4
		public void OnClickChatNotify(bool bUse)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			gameOptionData.UseChatContent = bUse;
		}

		// Token: 0x06008884 RID: 34948 RVA: 0x002E31E8 File Offset: 0x002E13E8
		private void OnClickUseCameraShakeButton()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			int value = this.m_NKM_UI_GAME_OPTION_GAME_SLOT4_BTN.GetValue();
			gameOptionData.CameraShakeLevel = (NKCGameOptionDataSt.GameOptionCameraShake)value;
		}

		// Token: 0x06008885 RID: 34949 RVA: 0x002E3218 File Offset: 0x002E1418
		private void OnClickUseCutscenNextTalkChangeSpeedButton()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			int value = this.m_NKM_UI_GAME_OPTION_GAME_CUTSCEN_NEXT_TALK_CHANGE_SPEED.GetValue();
			gameOptionData.CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO = (NKC_GAME_OPTION_CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO)value;
		}

		// Token: 0x06008886 RID: 34950 RVA: 0x002E3248 File Offset: 0x002E1448
		private void OnClickBtnStreamingOption()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			int value = this.m_MBtnStreamingOption.GetValue();
			gameOptionData.eStreamingOption = (NKCGameOptionDataSt.GameOptionStreamingOption)value;
		}

		// Token: 0x06008887 RID: 34951 RVA: 0x002E3278 File Offset: 0x002E1478
		private void OnClickBtnPrivatePvpInviteOption()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			int value = this.m_MBtnPrivatePvpInviteOption.GetValue();
			gameOptionData.ePrivatePvpInviteOption = (PrivatePvpInvitation)value;
		}

		// Token: 0x06008888 RID: 34952 RVA: 0x002E32A8 File Offset: 0x002E14A8
		private void OnClickUseUseClassGuideButton(bool use)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			gameOptionData.UseClassGuide = use;
		}

		// Token: 0x06008889 RID: 34953 RVA: 0x002E32CC File Offset: 0x002E14CC
		private void OnClickUseDamageNumberFx()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			int value = this.m_NKM_UI_GAME_OPTION_GAME_DAMAGE_AND_BUFF_NUMBER_FX.GetValue();
			gameOptionData.UseDamageAndBuffNumberFx = (NKCGameOptionDataSt.GameOptionDamageNumber)value;
		}

		// Token: 0x0600888A RID: 34954 RVA: 0x002E32FC File Offset: 0x002E14FC
		private void OnClickUsePvPAutoRespawn()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			switch (gameOptionData.PvPAutoRespawn)
			{
			case NKM_GAME_AUTO_RESPAWN_TYPE.OFF:
				this.OnClickUsePvPAutoRespawn(NKM_GAME_AUTO_RESPAWN_TYPE.ALL);
				return;
			case NKM_GAME_AUTO_RESPAWN_TYPE.ALL:
				this.OnClickUsePvPAutoRespawn(NKM_GAME_AUTO_RESPAWN_TYPE.STRATEGY);
				return;
			}
			this.OnClickUsePvPAutoRespawn(NKM_GAME_AUTO_RESPAWN_TYPE.OFF);
		}

		// Token: 0x0600888B RID: 34955 RVA: 0x002E334C File Offset: 0x002E154C
		private void OnClickUsePvPAutoRespawn(NKM_GAME_AUTO_RESPAWN_TYPE type)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_ObjAutoRespawnALL, type == NKM_GAME_AUTO_RESPAWN_TYPE.ALL);
			NKCUtil.SetGameobjectActive(this.m_ObjAutoRespawnASYNC, type == NKM_GAME_AUTO_RESPAWN_TYPE.STRATEGY);
			NKCUtil.SetGameobjectActive(this.m_ObjAutoRespawnOFF, type == NKM_GAME_AUTO_RESPAWN_TYPE.OFF);
			gameOptionData.SetPvPAutoRespawn(type, false);
		}

		// Token: 0x0600888C RID: 34956 RVA: 0x002E33A0 File Offset: 0x002E15A0
		private void OnClickAutoSyncFriendDeck(bool use)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			gameOptionData.SetAutoSyncFriendDeck(use, false);
		}

		// Token: 0x040074F4 RID: 29940
		private string[] GAME_CAMERA_SHAKE_STRINGS;

		// Token: 0x040074F5 RID: 29941
		private string[] GAME_DAMAGE_NUMBER_STRINGS;

		// Token: 0x040074F6 RID: 29942
		private string[] GAME_CUTSCEN_NEXT_TALK_CHANGE_SPEED_WHEN_AUTO_STRINGS;

		// Token: 0x040074F7 RID: 29943
		private string[] GAME_STREAMING_OPTION_STRINGS;

		// Token: 0x040074F8 RID: 29944
		private string[] GAME_PRIVATE_PVP_INVITE_OPTION_STRINGS;

		// Token: 0x040074F9 RID: 29945
		[Header("스킬 카메라")]
		public NKCUIComToggle m_NKM_UI_GAME_OPTION_GAME_SLOT1_BTN;

		// Token: 0x040074FA RID: 29946
		[Header("카메라 추적")]
		public NKCUIComToggle m_NKM_UI_GAME_OPTION_GAME_SLOT2_BTN;

		// Token: 0x040074FB RID: 29947
		[Header("스킬 사용시 컷인")]
		public NKCUIComToggle m_NKM_UI_GAME_OPTION_GAME_SLOT3_BTN;

		// Token: 0x040074FC RID: 29948
		[Header("카메라 흔들림")]
		public NKCUIGameOptionMultiStateButton m_NKM_UI_GAME_OPTION_GAME_SLOT4_BTN;

		// Token: 0x040074FD RID: 29949
		[Header("상성표 표시")]
		public NKCUIComToggle m_NKM_UI_GAME_OPTION_GAME_SLOT7_BTN;

		// Token: 0x040074FE RID: 29950
		[Header("피해량/버프")]
		public NKCUIGameOptionMultiStateButton m_NKM_UI_GAME_OPTION_GAME_DAMAGE_AND_BUFF_NUMBER_FX;

		// Token: 0x040074FF RID: 29951
		[Header("랭크전 자동전투")]
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_GAME_SLOT8_BTN_Auto_Respawn;

		// Token: 0x04007500 RID: 29952
		public GameObject m_ObjAutoRespawnOFF;

		// Token: 0x04007501 RID: 29953
		public GameObject m_ObjAutoRespawnALL;

		// Token: 0x04007502 RID: 29954
		public GameObject m_ObjAutoRespawnASYNC;

		// Token: 0x04007503 RID: 29955
		[Header("대표 소대 갱신")]
		public NKCUIComToggle m_NKM_UI_GAME_OPTION_GAME_SLOT9_Auto_Sync_Friend_Deck;

		// Token: 0x04007504 RID: 29956
		[Header("언어")]
		public GameObject m_NKM_UI_GAME_OPTION_GAME_LANG_SLOT;

		// Token: 0x04007505 RID: 29957
		public NKCUIComToggle m_NKM_UI_GAME_OPTION_GAME_LANG_KEY_ONLY;

		// Token: 0x04007506 RID: 29958
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_GAME_LANG_CHANGE;

		// Token: 0x04007507 RID: 29959
		public Text m_NKM_UI_GAME_OPTION_GAME_LANG_CHANGE_Text;

		// Token: 0x04007508 RID: 29960
		[Header("대화 속도")]
		public NKCUIGameOptionMultiStateButton m_NKM_UI_GAME_OPTION_GAME_CUTSCEN_NEXT_TALK_CHANGE_SPEED;

		// Token: 0x04007509 RID: 29961
		[Header("자막 관련")]
		public GameObject m_objNpcSubtitle;

		// Token: 0x0400750A RID: 29962
		public NKCUIComToggle m_NKM_UI_GAME_OPTION_GAME_NPC_SUBTITLE;

		// Token: 0x0400750B RID: 29963
		public NKCUIComToggle m_NKM_UI_GAME_OPTION_GAME_NPC_SUBTITLE_SHOW_NORMAL_AFTER_LIFE_TIME;

		// Token: 0x0400750C RID: 29964
		[Header("메모리 최적화")]
		public NKCUIComToggle m_NKM_UI_GAME_OPTION_GAME_MEM_OPTIMIZE;

		// Token: 0x0400750D RID: 29965
		[Header("친선전 초대")]
		public NKCUIGameOptionMultiStateButton m_MBtnPrivatePvpInviteOption;

		// Token: 0x0400750E RID: 29966
		[Header("스트리밍 옵션")]
		public NKCUIGameOptionMultiStateButton m_MBtnStreamingOption;

		// Token: 0x0400750F RID: 29967
		[Header("채팅 기능")]
		public GameObject m_objChatContent;

		// Token: 0x04007510 RID: 29968
		public NKCUIComToggle m_tglChatContent;

		// Token: 0x04007511 RID: 29969
		private NKM_NATIONAL_CODE m_Reserved_NKM_NATIONAL_CODE_ToChange = NKM_NATIONAL_CODE.NNC_END;
	}
}
