using System;
using System.Collections;
using ClientPacket.Event;
using NKC.UI.Guide;
using NKM;
using NKM.Event;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BE4 RID: 3044
	public class NKCUIEventSubUIRace : NKCUIEventSubUIBase
	{
		// Token: 0x1700167D RID: 5757
		// (set) Token: 0x06008D35 RID: 36149 RVA: 0x003007F4 File Offset: 0x002FE9F4
		public static int RaceDay
		{
			set
			{
				NKCUIEventSubUIRace.m_raceDay = value;
			}
		}

		// Token: 0x1700167E RID: 5758
		// (get) Token: 0x06008D36 RID: 36150 RVA: 0x003007FC File Offset: 0x002FE9FC
		// (set) Token: 0x06008D37 RID: 36151 RVA: 0x00300803 File Offset: 0x002FEA03
		public static NKMRaceSummary RaceSummary
		{
			get
			{
				return NKCUIEventSubUIRace.m_raceSummary;
			}
			set
			{
				NKCUIEventSubUIRace.m_raceSummary = value;
			}
		}

		// Token: 0x06008D38 RID: 36152 RVA: 0x0030080C File Offset: 0x002FEA0C
		public override void Init()
		{
			base.Init();
			NKCUtil.SetButtonClickDelegate(this.m_csbtnHelp, new UnityAction(this.OnClickHelp));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnMission, new UnityAction(this.OnClickMission));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnRewardInfo, new UnityAction(this.OnClickRewardInfo));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnStartRace, new UnityAction(this.OnClickStartRace));
			this.m_redTeamCharacter.Init(null, null);
			this.m_blueTeamCharacter.Init(null, null);
			NKCUtil.SetHotkey(this.m_csbtnHelp, HotkeyEventType.Help, null, false);
			NKCUtil.SetHotkey(this.m_csbtnStartRace, HotkeyEventType.Confirm, null, false);
			this.MaxPlayCount = NKMCommonConst.EVENT_RACE_PLAY_COUNT;
			int num = 0;
			foreach (NKMEventRaceTemplet nkmeventRaceTemplet in NKMTempletContainer<NKMEventRaceTemplet>.Values)
			{
				num++;
			}
			if (num <= 0)
			{
				NKMTempletContainer<NKMEventRaceTemplet>.Load("AB_SCRIPT", "LUA_EVENT_RACE_TEMPLET", "EVENT_RACE_TEMPLET", new Func<NKMLua, NKMEventRaceTemplet>(NKMEventRaceTemplet.LoadFromLua));
			}
		}

		// Token: 0x06008D39 RID: 36153 RVA: 0x00300920 File Offset: 0x002FEB20
		public override void Open(NKMEventTabTemplet tabTemplet)
		{
			if (tabTemplet == null)
			{
				return;
			}
			this.m_tabTemplet = tabTemplet;
			NKCUIEventSubUIRace.m_eventId = tabTemplet.m_EventID;
			NKCUtil.SetLabelText(this.m_lbEventDesc, NKCStringTable.GetString(this.m_tabTemplet.m_EventHelpDesc, false));
			base.SetDateLimit();
			this.Refresh();
			bool flag = NKCUIEventSubUIRace.m_raceSummary == null || NKCUIEventSubUIRace.m_raceSummary.racePrivate == null;
			int @int = PlayerPrefs.GetInt("EVENT_RACE_ALREADY_OPENED");
			if (@int == 0 && flag)
			{
				this.m_bBeginnerOpen = true;
				base.StartCoroutine(this.OpenHelp());
				return;
			}
			if (@int == 0)
			{
				PlayerPrefs.SetInt("EVENT_RACE_ALREADY_OPENED", 1);
			}
			this.m_bBeginnerOpen = false;
		}

		// Token: 0x06008D3A RID: 36154 RVA: 0x003009C0 File Offset: 0x002FEBC0
		public override void Refresh()
		{
			long num = 0L;
			long num2 = 0L;
			bool bValue = false;
			bool bValue2 = false;
			if (NKCUIEventSubUIRace.m_raceSummary != null)
			{
				if (NKCUIEventSubUIRace.m_raceSummary.raceResult != null)
				{
					num = NKCUIEventSubUIRace.m_raceSummary.raceResult.TeamAPoint;
					num2 = NKCUIEventSubUIRace.m_raceSummary.raceResult.TeamBPoint;
				}
				if (NKCUIEventSubUIRace.m_raceSummary.racePrivate != null)
				{
					RaceTeam selectTeam = NKCUIEventSubUIRace.m_raceSummary.racePrivate.SelectTeam;
					if (selectTeam != RaceTeam.TeamA)
					{
						if (selectTeam == RaceTeam.TeamB)
						{
							bValue2 = true;
						}
					}
					else
					{
						bValue = true;
					}
				}
			}
			NKCUtil.SetLabelText(this.m_lbDaysElapsed, string.Format(NKCStringTable.GetString("SI_PF_EVENT_RACE_COMMON_UI_REWARD_TEXT", false), NKCUIEventSubUIRace.m_raceDay + 1));
			NKCUtil.SetLabelText(this.m_lbTeamRedScore, num.ToString());
			NKCUtil.SetLabelText(this.m_lbTeamBlueScore, num2.ToString());
			NKCUtil.SetGameobjectActive(this.m_objRedTeamSelect, bValue);
			NKCUtil.SetGameobjectActive(this.m_objBlueTeamSelect, bValue2);
			NKCUtil.SetGameobjectActive(this.m_objTeamRedWin, false);
			NKCUtil.SetGameobjectActive(this.m_objTeamRedSymbol, false);
			NKCUtil.SetGameobjectActive(this.m_objTeamBlueWin, false);
			NKCUtil.SetGameobjectActive(this.m_objTeamBlueSymbol, false);
			if (num > 0L || num2 > 0L)
			{
				bool bValue3 = num >= num2;
				bool bValue4 = num <= num2;
				NKCUtil.SetGameobjectActive(this.m_objTeamRedWin, bValue3);
				NKCUtil.SetGameobjectActive(this.m_objTeamRedSymbol, bValue3);
				NKCUtil.SetGameobjectActive(this.m_objTeamBlueWin, bValue4);
				NKCUtil.SetGameobjectActive(this.m_objTeamBlueSymbol, bValue4);
			}
			if (this.m_tabTemplet != null)
			{
				this.SetCharacter(this.m_tabTemplet.m_EventID);
				this.SetRaceStartButtonInfo(this.m_tabTemplet.m_EventID);
				NKCUtil.SetGameobjectActive(this.m_objMissionRedDot, NKCPopupEventRaceMission.Instance.GetMissionRedDotState(this.m_tabTemplet.m_EventID));
			}
		}

		// Token: 0x06008D3B RID: 36155 RVA: 0x00300B68 File Offset: 0x002FED68
		public static void OpenRace()
		{
			NKMEventRaceTemplet nkmeventRaceTemplet = NKMEventRaceTemplet.Find(NKCUIEventSubUIRace.m_eventId);
			if (nkmeventRaceTemplet == null)
			{
				return;
			}
			if (NKCUIEventSubUIRace.m_raceSummary == null || NKCUIEventSubUIRace.m_raceSummary.racePrivate == null)
			{
				return;
			}
			string sdunitName = NKCUIEventSubUIRace.GetSDUnitName(nkmeventRaceTemplet.TeamAUnitImageType, nkmeventRaceTemplet.TeamAUnitId);
			string sdunitName2 = NKCUIEventSubUIRace.GetSDUnitName(nkmeventRaceTemplet.TeamBUnitImageType, nkmeventRaceTemplet.TeamBUnitId);
			NKCPopupEventRace.Instance.Open(NKCUIEventSubUIRace.m_raceSummary.racePrivate.SelectTeam, sdunitName, sdunitName2);
		}

		// Token: 0x06008D3C RID: 36156 RVA: 0x00300BD7 File Offset: 0x002FEDD7
		public override void Close()
		{
			base.Close();
			this.m_redTeamCharacter.CleanUp();
			this.m_blueTeamCharacter.CleanUp();
		}

		// Token: 0x06008D3D RID: 36157 RVA: 0x00300BF5 File Offset: 0x002FEDF5
		private IEnumerator OpenHelp()
		{
			float delayTime = 1f;
			while (delayTime > 0f)
			{
				delayTime -= Time.deltaTime;
				yield return null;
			}
			PlayerPrefs.SetInt("EVENT_RACE_ALREADY_OPENED", 1);
			this.m_bBeginnerOpen = false;
			this.OnClickHelp();
			yield break;
		}

		// Token: 0x06008D3E RID: 36158 RVA: 0x00300C04 File Offset: 0x002FEE04
		private void SetElapsedDays(DateTime startUtc)
		{
			int num = 0;
			DateTime serverUTCTime = NKCSynchronizedTime.GetServerUTCTime(0.0);
			if (serverUTCTime > startUtc)
			{
				num = (serverUTCTime - startUtc).Days + 1;
			}
			NKCUtil.SetLabelText(this.m_lbDaysElapsed, string.Format(NKCStringTable.GetString("SI_PF_EVENT_RACE_COMMON_UI_REWARD_TEXT", false), num));
		}

		// Token: 0x06008D3F RID: 36159 RVA: 0x00300C60 File Offset: 0x002FEE60
		private void SetRaceStartButtonInfo(int eventId)
		{
			NKMEventRaceTemplet nkmeventRaceTemplet = NKMEventRaceTemplet.Find(eventId);
			if (nkmeventRaceTemplet == null)
			{
				return;
			}
			Sprite orLoadMiscItemSmallIcon = NKCResourceUtility.GetOrLoadMiscItemSmallIcon(NKMItemManager.GetItemMiscTempletByID(nkmeventRaceTemplet.RaceTryItemId));
			NKCUtil.SetImageSprite(this.m_imgTicket, orLoadMiscItemSmallIcon, false);
			int num = this.MaxPlayCount;
			if (NKCUIEventSubUIRace.m_raceSummary != null && NKCUIEventSubUIRace.m_raceSummary.racePrivate != null)
			{
				num = this.MaxPlayCount - NKCUIEventSubUIRace.m_raceSummary.racePrivate.racePlayCount;
			}
			long num2 = 0L;
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null && myUserData.m_InventoryData != null)
			{
				num2 = myUserData.m_InventoryData.GetCountMiscItem(nkmeventRaceTemplet.RaceTryItemId);
			}
			string arg = "#" + ColorUtility.ToHtmlStringRGB((num > 0) ? this.m_enableText : this.m_disableText);
			NKCUtil.SetLabelText(this.m_lbRaceRemainCount, string.Format(NKCStringTable.GetString("SI_PF_EVENT_RACE_COMMON_UI_START_COUNT_TEXT", false), arg, num, this.MaxPlayCount));
			string arg2 = "#" + ColorUtility.ToHtmlStringRGB((num2 > 0L) ? this.m_enableText : this.m_disableText);
			NKCUtil.SetLabelText(this.m_lbRaceTicketCount, string.Format("<color={0}>{1}</color>", arg2, num2));
			this.m_csbtnStartRace.SetLock(num <= 0 || num2 <= 0L, false);
		}

		// Token: 0x06008D40 RID: 36160 RVA: 0x00300DA4 File Offset: 0x002FEFA4
		private void SetCharacter(int eventId)
		{
			NKMEventRaceTemplet nkmeventRaceTemplet = NKMEventRaceTemplet.Find(eventId);
			if (nkmeventRaceTemplet != null)
			{
				this.SetCharacterIllust(this.m_redTeamCharacter, nkmeventRaceTemplet.TeamAUnitImageType, nkmeventRaceTemplet.TeamAUnitId);
				this.SetCharacterIllust(this.m_blueTeamCharacter, nkmeventRaceTemplet.TeamBUnitImageType, nkmeventRaceTemplet.TeamBUnitId);
			}
		}

		// Token: 0x06008D41 RID: 36161 RVA: 0x00300DEC File Offset: 0x002FEFEC
		private void SetCharacterIllust(NKCUICharacterView characterView, string type, int Id)
		{
			if (type != null && type == "SKIN")
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(Id);
				characterView.SetCharacterIllust(skinTemplet, false, false, 0);
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(Id);
			characterView.SetCharacterIllust(unitTempletBase, 0, false, false, 0);
		}

		// Token: 0x06008D42 RID: 36162 RVA: 0x00300E30 File Offset: 0x002FF030
		private static string GetSDUnitName(string type, int id)
		{
			if (type != null && type == "SKIN")
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(id);
				if (skinTemplet == null)
				{
					return "";
				}
				return skinTemplet.m_SpineSDName;
			}
			else
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(id);
				if (unitTempletBase == null)
				{
					return "";
				}
				return unitTempletBase.m_SpineSDName;
			}
		}

		// Token: 0x06008D43 RID: 36163 RVA: 0x00300E7A File Offset: 0x002FF07A
		private void OnClickHelp()
		{
			if (this.m_bBeginnerOpen)
			{
				return;
			}
			NKCUIPopupTutorialImagePanel.Instance.Open(this.m_guideId, null);
		}

		// Token: 0x06008D44 RID: 36164 RVA: 0x00300E96 File Offset: 0x002FF096
		private void OnClickMission()
		{
			if (this.m_bBeginnerOpen)
			{
				return;
			}
			if (this.m_tabTemplet != null)
			{
				NKCPopupEventRaceMission.Instance.Open(this.m_tabTemplet.m_EventID);
			}
		}

		// Token: 0x06008D45 RID: 36165 RVA: 0x00300EBE File Offset: 0x002FF0BE
		private void OnClickRewardInfo()
		{
			if (this.m_bBeginnerOpen)
			{
				return;
			}
			if (this.m_tabTemplet != null)
			{
				NKCPopupEventRaceReward.Instance.Open(this.m_tabTemplet.m_EventID);
			}
		}

		// Token: 0x06008D46 RID: 36166 RVA: 0x00300EE8 File Offset: 0x002FF0E8
		private void OnClickStartRace()
		{
			if (this.m_bBeginnerOpen)
			{
				return;
			}
			if (NKCUIEventSubUIRace.m_raceSummary == null || NKCUIEventSubUIRace.m_raceSummary.racePrivate == null)
			{
				if (this.m_tabTemplet != null)
				{
					NKCPopupEventRaceTeamSelect.Instance.Open(this.m_tabTemplet.m_EventID);
					return;
				}
			}
			else
			{
				NKCUIEventSubUIRace.OpenRace();
			}
		}

		// Token: 0x06008D47 RID: 36167 RVA: 0x00300F34 File Offset: 0x002FF134
		private void OnDestroy()
		{
			this.m_tabTemplet = null;
			if (this.m_redTeamCharacter != null)
			{
				this.m_redTeamCharacter.CleanUp();
				this.m_redTeamCharacter = null;
			}
			if (this.m_blueTeamCharacter != null)
			{
				this.m_blueTeamCharacter.CleanUp();
				this.m_blueTeamCharacter = null;
			}
		}

		// Token: 0x04007A07 RID: 31239
		public Text m_lbDaysElapsed;

		// Token: 0x04007A08 RID: 31240
		public Text m_lbEventDesc;

		// Token: 0x04007A09 RID: 31241
		[Header("팀 점수")]
		public Text m_lbTeamRedScore;

		// Token: 0x04007A0A RID: 31242
		public Text m_lbTeamBlueScore;

		// Token: 0x04007A0B RID: 31243
		[Header("승리 표시 오브젝트")]
		public GameObject m_objTeamRedWin;

		// Token: 0x04007A0C RID: 31244
		public GameObject m_objTeamRedSymbol;

		// Token: 0x04007A0D RID: 31245
		public GameObject m_objTeamBlueWin;

		// Token: 0x04007A0E RID: 31246
		public GameObject m_objTeamBlueSymbol;

		// Token: 0x04007A0F RID: 31247
		[Header("팀 선택 표시 오브젝트")]
		public GameObject m_objRedTeamSelect;

		// Token: 0x04007A10 RID: 31248
		public GameObject m_objBlueTeamSelect;

		// Token: 0x04007A11 RID: 31249
		[Header("캐릭터")]
		public NKCUICharacterView m_redTeamCharacter;

		// Token: 0x04007A12 RID: 31250
		public NKCUICharacterView m_blueTeamCharacter;

		// Token: 0x04007A13 RID: 31251
		[Header("버튼")]
		public NKCUIComStateButton m_csbtnHelp;

		// Token: 0x04007A14 RID: 31252
		public NKCUIComStateButton m_csbtnRewardInfo;

		// Token: 0x04007A15 RID: 31253
		public NKCUIComStateButton m_csbtnMission;

		// Token: 0x04007A16 RID: 31254
		public NKCUIComStateButton m_csbtnStartRace;

		// Token: 0x04007A17 RID: 31255
		[Header("시작 버튼 표시")]
		public Image m_imgTicket;

		// Token: 0x04007A18 RID: 31256
		public Text m_lbRaceRemainCount;

		// Token: 0x04007A19 RID: 31257
		public Text m_lbRaceTicketCount;

		// Token: 0x04007A1A RID: 31258
		public Color m_enableText;

		// Token: 0x04007A1B RID: 31259
		public Color m_disableText;

		// Token: 0x04007A1C RID: 31260
		[Header("레드 닷")]
		public GameObject m_objMissionRedDot;

		// Token: 0x04007A1D RID: 31261
		[Header("GUIDE ID(숫자)")]
		public int m_guideId;

		// Token: 0x04007A1E RID: 31262
		private int MaxPlayCount;

		// Token: 0x04007A1F RID: 31263
		private bool m_bBeginnerOpen;

		// Token: 0x04007A20 RID: 31264
		private const string ALREADY_OPENED_KEY = "EVENT_RACE_ALREADY_OPENED";

		// Token: 0x04007A21 RID: 31265
		private static NKMRaceSummary m_raceSummary;

		// Token: 0x04007A22 RID: 31266
		private static int m_raceDay;

		// Token: 0x04007A23 RID: 31267
		private static int m_eventId;
	}
}
