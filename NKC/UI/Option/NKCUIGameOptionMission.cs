using System;
using System.Text;
using ClientPacket.Warfare;
using NKC.Trim;
using NKC.UI.Guide;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Option
{
	// Token: 0x02000B8F RID: 2959
	public class NKCUIGameOptionMission : NKCUIGameOptionContentBase
	{
		// Token: 0x170015F5 RID: 5621
		// (get) Token: 0x060088B4 RID: 34996 RVA: 0x002E3F0E File Offset: 0x002E210E
		private string MISSION_MEDAL_CONDITION_STRING
		{
			get
			{
				return NKCUtilString.GET_STRING_OPTION_MEDAL_COND;
			}
		}

		// Token: 0x170015F6 RID: 5622
		// (get) Token: 0x060088B5 RID: 34997 RVA: 0x002E3F15 File Offset: 0x002E2115
		private string MISSION_RANK_CONDITION_STRING
		{
			get
			{
				return NKCUtilString.GET_STRING_OPTION_RANK_COND;
			}
		}

		// Token: 0x170015F7 RID: 5623
		// (get) Token: 0x060088B6 RID: 34998 RVA: 0x002E3F1C File Offset: 0x002E211C
		private string MISSION_GIVE_UP_TITLE_STRING
		{
			get
			{
				return NKCUtilString.GET_STRING_WARNING;
			}
		}

		// Token: 0x170015F8 RID: 5624
		// (get) Token: 0x060088B7 RID: 34999 RVA: 0x002E3F23 File Offset: 0x002E2123
		private string MISSION_GIVE_UP_CONTENT_STRING
		{
			get
			{
				return NKCUtilString.GET_STRING_OPTION_MISSION_GIVE_UP_WARNING;
			}
		}

		// Token: 0x170015F9 RID: 5625
		// (get) Token: 0x060088B8 RID: 35000 RVA: 0x002E3F2A File Offset: 0x002E212A
		private string MISSION_GIVE_UP_MULTIPLY_CONTENT_STRING
		{
			get
			{
				return NKCUtilString.GET_STRING_OPTION_MISSION_GIVE_UP_WARNING_MULTIPLY;
			}
		}

		// Token: 0x060088B9 RID: 35001 RVA: 0x002E3F34 File Offset: 0x002E2134
		public override void Init()
		{
			this.m_MissionConditions[0] = this.NKM_UI_GAME_OPTION_MISSION_WARFARE;
			this.m_MissionConditions[0].m_GiveUpButton.PointerClick.AddListener(new UnityAction(this.OnClickWarfareGiveUpButton));
			this.m_MissionConditions[0].m_LeaveButton.PointerClick.AddListener(new UnityAction(this.OnClickWarfareLeaveButton));
			this.m_MissionConditions[1] = this.NKM_UI_GAME_OPTION_MISSION_MISSION;
			this.m_MissionConditions[1].m_GiveUpButton.PointerClick.AddListener(new UnityAction(this.OnClickDungeonGiveUpButton));
			this.m_MissionConditions[2] = this.NKM_UI_GAME_OPTION_MISSION_DIVE;
			this.m_MissionConditions[2].m_GiveUpButton.PointerClick.AddListener(new UnityAction(this.OnClickDiveGiveUpButton));
			this.m_MissionConditions[2].m_LeaveButton.PointerClick.AddListener(new UnityAction(this.OnClickDiveLeaveButton));
			this.m_csbtnClassInfoHelp.PointerClick.AddListener(new UnityAction(this.OnClassInfoHelp));
		}

		// Token: 0x060088BA RID: 35002 RVA: 0x002E4038 File Offset: 0x002E2238
		public override void SetContent()
		{
			NKC_GAME_OPTION_MENU_TYPE menuType = NKCUIGameOption.Instance.GetMenuType();
			NKCUIGameOptionMission.MissionContentGroup missionContentGroupByMenuType = this.GetMissionContentGroupByMenuType(menuType);
			this.ChangeMissionContent(missionContentGroupByMenuType);
		}

		// Token: 0x060088BB RID: 35003 RVA: 0x002E4060 File Offset: 0x002E2260
		private NKCUIGameOptionMission.MissionContentGroup GetMissionContentGroupByMenuType(NKC_GAME_OPTION_MENU_TYPE menuType)
		{
			NKCUIGameOptionMission.MissionContentGroup result = NKCUIGameOptionMission.MissionContentGroup.None;
			switch (menuType)
			{
			case NKC_GAME_OPTION_MENU_TYPE.WARFARE:
				result = NKCUIGameOptionMission.MissionContentGroup.Warfare;
				break;
			case NKC_GAME_OPTION_MENU_TYPE.DUNGEON:
				result = NKCUIGameOptionMission.MissionContentGroup.Mission;
				break;
			case NKC_GAME_OPTION_MENU_TYPE.DIVE:
				result = NKCUIGameOptionMission.MissionContentGroup.Dive;
				break;
			}
			return result;
		}

		// Token: 0x060088BC RID: 35004 RVA: 0x002E4090 File Offset: 0x002E2290
		private NKCUIGameOptionMission.MissionData GetMissionDataByMissionContentGroup(NKCUIGameOptionMission.MissionContentGroup contentGroup)
		{
			NKCUIGameOptionMission.MissionData result = default(NKCUIGameOptionMission.MissionData);
			result.m_StageID = 0;
			if (contentGroup != NKCUIGameOptionMission.MissionContentGroup.Warfare)
			{
				if (contentGroup == NKCUIGameOptionMission.MissionContentGroup.Mission)
				{
					NKCGameClient gameClient = NKCScenManager.GetScenManager().GetGameClient();
					if (gameClient != null)
					{
						NKMGameData gameData = gameClient.GetGameData();
						if (gameData != null)
						{
							if (NKCPhaseManager.IsCurrentPhaseDungeon(gameData.m_DungeonID))
							{
								NKMPhaseTemplet phaseTemplet = NKCPhaseManager.GetPhaseTemplet();
								if (phaseTemplet != null)
								{
									result.m_StageID = ((phaseTemplet.StageTemplet != null) ? phaseTemplet.StageTemplet.Key : 0);
									result.m_MissionName = phaseTemplet.GetName();
									result.m_MissionCondition1Text = NKCUtilString.GetDGMissionTextWithProgress(gameClient, DUNGEON_GAME_MISSION_TYPE.DGMT_CLEAR, 1);
									result.m_MissionCondition2Text = NKCUtilString.GetDGMissionTextWithProgress(gameClient, phaseTemplet.m_DGMissionType_1, phaseTemplet.m_DGMissionValue_1);
									result.m_MissionCondition3Text = NKCUtilString.GetDGMissionTextWithProgress(gameClient, phaseTemplet.m_DGMissionType_2, phaseTemplet.m_DGMissionValue_2);
								}
							}
							else
							{
								NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(gameData.m_DungeonID);
								if (dungeonTempletBase != null)
								{
									bool flag = false;
									if (gameData.m_WarfareID > 0)
									{
										NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(gameData.m_WarfareID);
										if (nkmwarfareTemplet != null)
										{
											result.m_StageID = ((nkmwarfareTemplet.StageTemplet != null) ? nkmwarfareTemplet.StageTemplet.Key : 0);
											result.m_MissionName = nkmwarfareTemplet.GetWarfareName();
											flag = true;
										}
									}
									if (!flag)
									{
										result.m_StageID = ((dungeonTempletBase.StageTemplet != null) ? dungeonTempletBase.StageTemplet.Key : 0);
										result.m_MissionName = dungeonTempletBase.GetDungeonName();
									}
									result.m_MissionCondition1Text = NKCUtilString.GetDGMissionTextWithProgress(gameClient, DUNGEON_GAME_MISSION_TYPE.DGMT_CLEAR, 1);
									result.m_MissionCondition2Text = NKCUtilString.GetDGMissionTextWithProgress(gameClient, dungeonTempletBase.m_DGMissionType_1, dungeonTempletBase.m_DGMissionValue_1);
									result.m_MissionCondition3Text = NKCUtilString.GetDGMissionTextWithProgress(gameClient, dungeonTempletBase.m_DGMissionType_2, dungeonTempletBase.m_DGMissionValue_2);
								}
							}
						}
					}
				}
			}
			else if (NKCScenManager.GetScenManager().GetMyUserData() != null)
			{
				WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
				if (warfareGameData != null)
				{
					NKMWarfareTemplet nkmwarfareTemplet2 = NKMWarfareTemplet.Find(warfareGameData.warfareTempletID);
					if (nkmwarfareTemplet2 != null)
					{
						result.m_StageID = ((nkmwarfareTemplet2.StageTemplet != null) ? nkmwarfareTemplet2.StageTemplet.Key : 0);
						result.m_MissionName = nkmwarfareTemplet2.GetWarfareName();
						result.m_MissionCondition3Text = NKCUtilString.GetWFMissionTextWithProgress(warfareGameData, WARFARE_GAME_MISSION_TYPE.WFMT_CLEAR, 1);
						result.m_MissionCondition2Text = NKCUtilString.GetWFMissionTextWithProgress(warfareGameData, nkmwarfareTemplet2.m_WFMissionType_1, nkmwarfareTemplet2.m_WFMissionValue_1);
						result.m_MissionCondition1Text = NKCUtilString.GetWFMissionTextWithProgress(warfareGameData, nkmwarfareTemplet2.m_WFMissionType_2, nkmwarfareTemplet2.m_WFMissionValue_2);
					}
				}
			}
			return result;
		}

		// Token: 0x060088BD RID: 35005 RVA: 0x002E42F0 File Offset: 0x002E24F0
		private void ChangeMissionContent(NKCUIGameOptionMission.MissionContentGroup contentGroup)
		{
			NKCUIGameOptionMission.MissionData missionData = default(NKCUIGameOptionMission.MissionData);
			string text = "";
			string text2 = "";
			string text3 = "";
			if (contentGroup != NKCUIGameOptionMission.MissionContentGroup.Dive)
			{
				bool flag = false;
				missionData = this.GetMissionDataByMissionContentGroup(contentGroup);
				if (contentGroup >= NKCUIGameOptionMission.MissionContentGroup.Warfare && contentGroup <= NKCUIGameOptionMission.MissionContentGroup.Mission)
				{
					flag = true;
				}
				if (!flag)
				{
					this.SetDeactiveMissionContentWithoutButton(contentGroup);
					return;
				}
			}
			this.SetActiveMissionContent(true, contentGroup);
			if (contentGroup == NKCUIGameOptionMission.MissionContentGroup.Dive)
			{
				NKMDiveGameData diveGameData = NKCScenManager.GetScenManager().GetMyUserData().m_DiveGameData;
				if (diveGameData != null && diveGameData.Floor.Templet != null)
				{
					this.m_MissionConditions[2].m_LV_COUNT.text = diveGameData.Floor.Templet.StageLevel.ToString();
					this.m_MissionConditions[2].m_RANDOM_SET_COUNT.text = diveGameData.Floor.Templet.RandomSetCount.ToString();
					this.m_MissionConditions[2].m_NAME_TEXT.text = diveGameData.Floor.Templet.Get_STAGE_NAME();
					this.m_MissionConditions[2].m_NAME_SUB_TEXT.text = diveGameData.Floor.Templet.Get_STAGE_NAME_SUB();
				}
			}
			else
			{
				string text4 = missionData.m_MissionName;
				NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(missionData.m_StageID);
				if (nkmstageTempletV != null)
				{
					NKMEpisodeTempletV2 episodeTemplet = nkmstageTempletV.EpisodeTemplet;
					if (episodeTemplet != null)
					{
						text = NKCUtilString.GetEpisodeTitle(episodeTemplet, nkmstageTempletV);
						text2 = NKCUtilString.GetEpisodeNumber(episodeTemplet, nkmstageTempletV);
						text3 = nkmstageTempletV.GetStageDesc();
					}
				}
				if (NKCTrimManager.TrimModeState != null)
				{
					NKMTrimTemplet nkmtrimTemplet = NKMTrimTemplet.Find(NKCTrimManager.TrimModeState.trimId);
					string value = (nkmtrimTemplet != null) ? NKCStringTable.GetString(nkmtrimTemplet.TirmGroupName, false) : " - ";
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(value);
					stringBuilder.Append(" ");
					stringBuilder.Append(NKCStringTable.GetString("SI_PF_TRIM_MAIN_LEVEL_TEXT", false));
					stringBuilder.Append(NKCTrimManager.TrimModeState.trimLevel);
					text4 = stringBuilder.ToString();
					text3 = ((nkmtrimTemplet != null) ? NKCStringTable.GetString(nkmtrimTemplet.TirmGroupDesc, false) : "");
				}
				this.m_NKM_UI_GAME_OPTION_EPISODE_TEXT_TITLE.text = text;
				this.m_NKM_UI_GAME_OPTION_EPISODE_TEXT_TITLE2.text = text2;
				this.m_NKM_UI_GAME_OPTION_EPISODE_TEXT_NAME.text = text4;
				this.m_NKM_UI_GAME_OPTION_EPISODE_TEXT_DESC.text = text3;
				string text5 = "";
				if (contentGroup != NKCUIGameOptionMission.MissionContentGroup.Warfare)
				{
					if (contentGroup == NKCUIGameOptionMission.MissionContentGroup.Mission)
					{
						text5 = this.MISSION_RANK_CONDITION_STRING;
					}
				}
				else
				{
					text5 = this.MISSION_MEDAL_CONDITION_STRING;
				}
				this.m_NKM_UI_GAME_OPTION_MISSION_TEXT_TITLE.text = text5;
				this.m_NKM_UI_GAME_OPTION_MISSION_TEXT_CONDITION1.text = missionData.m_MissionCondition1Text;
				this.m_NKM_UI_GAME_OPTION_MISSION_TEXT_CONDITION2.text = missionData.m_MissionCondition2Text;
				this.m_NKM_UI_GAME_OPTION_MISSION_TEXT_CONDITION3.text = missionData.m_MissionCondition3Text;
				NKCUtil.SetGameobjectActive(this.m_MissionConditions[(int)contentGroup].m_ConditionImage_1, !string.IsNullOrEmpty(missionData.m_MissionCondition1Text));
				NKCUtil.SetGameobjectActive(this.m_MissionConditions[(int)contentGroup].m_ConditionImage_2, !string.IsNullOrEmpty(missionData.m_MissionCondition2Text));
				NKCUtil.SetGameobjectActive(this.m_MissionConditions[(int)contentGroup].m_ConditionImage_3, !string.IsNullOrEmpty(missionData.m_MissionCondition3Text));
			}
			if (contentGroup == NKCUIGameOptionMission.MissionContentGroup.Mission)
			{
				NKCGameClient gameClient = NKCScenManager.GetScenManager().GetGameClient();
				if (gameClient != null)
				{
					NKMGameData gameData = gameClient.GetGameData();
					if (gameData != null)
					{
						NKCUtil.SetGameobjectActive(this.m_MissionConditions[1].m_GiveUpButton, NKCTutorialManager.CanGiveupDungeon(gameData.m_DungeonID));
					}
				}
			}
		}

		// Token: 0x060088BE RID: 35006 RVA: 0x002E4628 File Offset: 0x002E2828
		private void SetActiveMissionContent(bool active, NKCUIGameOptionMission.MissionContentGroup contentGroup = NKCUIGameOptionMission.MissionContentGroup.None)
		{
			if (contentGroup == NKCUIGameOptionMission.MissionContentGroup.Dive)
			{
				NKCUtil.SetGameobjectActive(this.NKM_UI_GAME_OPTION_MISSION_TEXT, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.NKM_UI_GAME_OPTION_MISSION_TEXT, active);
			}
			for (int i = 0; i < 3; i++)
			{
				bool bValue;
				if (i == (int)contentGroup)
				{
					bValue = active;
				}
				else
				{
					bValue = !active;
				}
				NKCUtil.SetGameobjectActive(this.m_MissionConditions[i], bValue);
				NKCUtil.SetGameobjectActive(this.m_MissionConditions[i].m_Condition, bValue);
				NKCUtil.SetGameobjectActive(this.m_MissionConditions[i].m_Button, bValue);
			}
		}

		// Token: 0x060088BF RID: 35007 RVA: 0x002E46A4 File Offset: 0x002E28A4
		private void SetDeactiveMissionContentWithoutButton(NKCUIGameOptionMission.MissionContentGroup contentGroup)
		{
			NKCUtil.SetGameobjectActive(this.NKM_UI_GAME_OPTION_MISSION_TEXT, false);
			for (int i = 0; i < 3; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_MissionConditions[i], true);
				NKCUtil.SetGameobjectActive(this.m_MissionConditions[i].m_Condition, false);
				if (i == (int)contentGroup)
				{
					NKCUtil.SetGameobjectActive(this.m_MissionConditions[i].m_Button, true);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_MissionConditions[i].m_Button, false);
				}
			}
		}

		// Token: 0x060088C0 RID: 35008 RVA: 0x002E4718 File Offset: 0x002E2918
		private void OnClickDungeonGiveUpButton()
		{
			NKCGameClient gameClient = NKCScenManager.GetScenManager().GetGameClient();
			if (gameClient == null)
			{
				return;
			}
			NKM_GAME_TYPE gameType = gameClient.GetGameData().GetGameType();
			if (gameType != NKM_GAME_TYPE.NGT_PRACTICE)
			{
				string content;
				if (gameType != NKM_GAME_TYPE.NGT_FIERCE)
				{
					if (gameType != NKM_GAME_TYPE.NGT_TRIM)
					{
						if (gameClient.GetGameData().GetGameType() != NKM_GAME_TYPE.NGT_WARFARE && NKCScenManager.GetScenManager().GetNKCRepeatOperaion().CheckRepeatOperationRealStop())
						{
							return;
						}
						if (gameClient.MultiplyReward > 1)
						{
							content = this.MISSION_GIVE_UP_MULTIPLY_CONTENT_STRING;
						}
						else
						{
							content = this.MISSION_GIVE_UP_CONTENT_STRING;
						}
					}
					else
					{
						NKMTrimIntervalTemplet nkmtrimIntervalTemplet = NKMTrimIntervalTemplet.Find(NKCSynchronizedTime.ServiceTime);
						bool flag = false;
						if (nkmtrimIntervalTemplet != null)
						{
							flag = (NKCSynchronizedTime.IsStarted(nkmtrimIntervalTemplet.IntervalTemplet.StartDate) && !NKCSynchronizedTime.IsFinished(nkmtrimIntervalTemplet.IntervalTemplet.EndDate));
						}
						if (flag && nkmtrimIntervalTemplet != null && !nkmtrimIntervalTemplet.IsResetUnLimit)
						{
							if (NKCScenManager.CurrentUserData().TrimData.TrimIntervalData.trimRetryCount > 0)
							{
								content = NKCStringTable.GetString("SI_PF_TRIM_DUNGEON_RESULT_RESET_COUNT_EXIT", new object[]
								{
									NKCScenManager.CurrentUserData().TrimData.TrimIntervalData.trimRetryCount
								});
							}
							else
							{
								content = NKCStringTable.GetString("SI_PF_TRIM_DUNGEON_RESULT_RESET_NO_COUNT_EXIT", false);
							}
						}
						else
						{
							content = NKCUtilString.GET_STRING_OPTION_MISSION_GIVE_UP_WARNING;
						}
					}
				}
				else
				{
					content = NKCUtilString.GET_FIERCE_BATTLE_GIVE_UP_DESC;
				}
				NKCPopupOKCancel.OpenOKCancelBox(this.MISSION_GIVE_UP_TITLE_STRING, content, new NKCPopupOKCancel.OnButton(NKCUIGameOptionMission.OnClickDungeonGiveUpOKButton), null, false);
				return;
			}
			gameClient.GetGameHud().PracticeGoBack();
		}

		// Token: 0x060088C1 RID: 35009 RVA: 0x002E4863 File Offset: 0x002E2A63
		private static void OnClickDungeonGiveUpOKButton()
		{
			NKCPacketSender.Send_NKMPacket_GAME_GIVEUP_REQ();
		}

		// Token: 0x060088C2 RID: 35010 RVA: 0x002E486C File Offset: 0x002E2A6C
		private void OnClickWarfareGiveUpButton()
		{
			NKC_SCEN_WARFARE_GAME nkc_SCEN_WARFARE_GAME = NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME();
			if (nkc_SCEN_WARFARE_GAME == null)
			{
				return;
			}
			nkc_SCEN_WARFARE_GAME.TryGiveUp();
		}

		// Token: 0x060088C3 RID: 35011 RVA: 0x002E4890 File Offset: 0x002E2A90
		private void OnClickWarfareLeaveButton()
		{
			NKC_SCEN_WARFARE_GAME nkc_SCEN_WARFARE_GAME = NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME();
			if (nkc_SCEN_WARFARE_GAME == null)
			{
				return;
			}
			nkc_SCEN_WARFARE_GAME.TryTempLeave();
		}

		// Token: 0x060088C4 RID: 35012 RVA: 0x002E48B4 File Offset: 0x002E2AB4
		private void OnClickDiveGiveUpButton()
		{
			NKC_SCEN_DIVE nkc_SCEN_DIVE = NKCScenManager.GetScenManager().Get_NKC_SCEN_DIVE();
			if (nkc_SCEN_DIVE == null)
			{
				return;
			}
			nkc_SCEN_DIVE.TryGiveUp();
		}

		// Token: 0x060088C5 RID: 35013 RVA: 0x002E48D8 File Offset: 0x002E2AD8
		private void OnClickDiveLeaveButton()
		{
			NKC_SCEN_DIVE nkc_SCEN_DIVE = NKCScenManager.GetScenManager().Get_NKC_SCEN_DIVE();
			if (nkc_SCEN_DIVE == null)
			{
				return;
			}
			nkc_SCEN_DIVE.TryTempLeave();
		}

		// Token: 0x060088C6 RID: 35014 RVA: 0x002E48FA File Offset: 0x002E2AFA
		private void OnClassInfoHelp()
		{
			NKCUIPopupTutorialImagePanel.Instance.Open("GUIDE_BATTLE_UNIT_2", null);
		}

		// Token: 0x0400752F RID: 29999
		public GameObject NKM_UI_GAME_OPTION_MISSION_TEXT;

		// Token: 0x04007530 RID: 30000
		public Text m_NKM_UI_GAME_OPTION_EPISODE_TEXT_TITLE;

		// Token: 0x04007531 RID: 30001
		public Text m_NKM_UI_GAME_OPTION_EPISODE_TEXT_TITLE2;

		// Token: 0x04007532 RID: 30002
		public Text m_NKM_UI_GAME_OPTION_EPISODE_TEXT_NAME;

		// Token: 0x04007533 RID: 30003
		public Text m_NKM_UI_GAME_OPTION_EPISODE_TEXT_DESC;

		// Token: 0x04007534 RID: 30004
		public Text m_NKM_UI_GAME_OPTION_MISSION_TEXT_TITLE;

		// Token: 0x04007535 RID: 30005
		public Text m_NKM_UI_GAME_OPTION_MISSION_TEXT_CONDITION1;

		// Token: 0x04007536 RID: 30006
		public Text m_NKM_UI_GAME_OPTION_MISSION_TEXT_CONDITION2;

		// Token: 0x04007537 RID: 30007
		public Text m_NKM_UI_GAME_OPTION_MISSION_TEXT_CONDITION3;

		// Token: 0x04007538 RID: 30008
		public NKCUIComStateButton m_csbtnClassInfoHelp;

		// Token: 0x04007539 RID: 30009
		private NKCUIGameOptionMissionContentBase[] m_MissionConditions = new NKCUIGameOptionMissionContentBase[3];

		// Token: 0x0400753A RID: 30010
		public NKCUIGameOptionMissionContentBase NKM_UI_GAME_OPTION_MISSION_WARFARE;

		// Token: 0x0400753B RID: 30011
		public NKCUIGameOptionMissionContentBase NKM_UI_GAME_OPTION_MISSION_MISSION;

		// Token: 0x0400753C RID: 30012
		public NKCUIGameOptionMissionContentBase NKM_UI_GAME_OPTION_MISSION_DIVE;

		// Token: 0x02001936 RID: 6454
		private enum MissionContentGroup
		{
			// Token: 0x0400AAF1 RID: 43761
			None = -1,
			// Token: 0x0400AAF2 RID: 43762
			Warfare,
			// Token: 0x0400AAF3 RID: 43763
			Mission,
			// Token: 0x0400AAF4 RID: 43764
			Dive,
			// Token: 0x0400AAF5 RID: 43765
			Max
		}

		// Token: 0x02001937 RID: 6455
		private struct MissionData
		{
			// Token: 0x0400AAF6 RID: 43766
			public int m_StageID;

			// Token: 0x0400AAF7 RID: 43767
			public string m_MissionName;

			// Token: 0x0400AAF8 RID: 43768
			public string m_MissionCondition1Text;

			// Token: 0x0400AAF9 RID: 43769
			public string m_MissionCondition2Text;

			// Token: 0x0400AAFA RID: 43770
			public string m_MissionCondition3Text;
		}
	}
}
