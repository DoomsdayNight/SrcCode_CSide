using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Warfare;
using ClientPacket.WorldMap;
using NKC.Office;
using NKC.UI;
using NKC.UI.Guide;
using NKC.UI.HUD;
using NKC.UI.Office;
using NKC.UI.Result;
using NKC.UI.Warfare;
using NKC.UI.Worldmap;
using NKM;
using NKM.Contract2;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NKC
{
	// Token: 0x0200067E RID: 1662
	public static class NKCGameEventManager
	{
		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x060035B6 RID: 13750 RVA: 0x00113C0B File Offset: 0x00111E0B
		// (set) Token: 0x060035B7 RID: 13751 RVA: 0x00113C12 File Offset: 0x00111E12
		public static bool RandomBoxDataCollecting { get; set; }

		// Token: 0x060035B8 RID: 13752 RVA: 0x00113C1A File Offset: 0x00111E1A
		public static void CollectResultData(NKMRewardData rewardData)
		{
			if (rewardData != null && NKCGameEventManager.lstCollectReward != null)
			{
				NKCGameEventManager.lstCollectReward.Add(rewardData);
			}
			if (NKCGameEventManager.GetCurrentEventType() == NKCGameEventManager.GameEventType.OPEN_MISC_ITEM_RANDOM_BOX)
			{
				NKCGameEventManager.ProcessEvent();
			}
		}

		// Token: 0x060035B9 RID: 13753 RVA: 0x00113C3F File Offset: 0x00111E3F
		private static NKCGameEventManager.NKCGameEventTemplet GetCurrentEventTemplet()
		{
			if (NKCGameEventManager.m_lstCurrentEvent == null)
			{
				return null;
			}
			if (NKCGameEventManager.m_CurrentIndex < 0)
			{
				return null;
			}
			if (NKCGameEventManager.m_CurrentIndex >= NKCGameEventManager.m_lstCurrentEvent.Count)
			{
				return null;
			}
			return NKCGameEventManager.m_lstCurrentEvent[NKCGameEventManager.m_CurrentIndex];
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x00113C78 File Offset: 0x00111E78
		public static NKCGameEventManager.GameEventType GetCurrentEventType()
		{
			NKCGameEventManager.NKCGameEventTemplet currentEventTemplet = NKCGameEventManager.GetCurrentEventTemplet();
			if (currentEventTemplet == null)
			{
				return NKCGameEventManager.GameEventType.INVALID;
			}
			return currentEventTemplet.EventType;
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x00113C96 File Offset: 0x00111E96
		private static void LoadGameEvent()
		{
			NKCGameEventManager.LoadFromLua("ab_script", "LUA_GAME_EVENT_TEMPLET", "m_GameEventTable", out NKCGameEventManager.m_dicGameEventTemplet);
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x00113CB4 File Offset: 0x00111EB4
		private static bool LoadFromLua(string bundleName, string fileName, string tableName, out Dictionary<int, List<NKCGameEventManager.NKCGameEventTemplet>> dicTemplet)
		{
			dicTemplet = new Dictionary<int, List<NKCGameEventManager.NKCGameEventTemplet>>();
			NKMLua nkmlua = new NKMLua();
			if (!NKMContentsVersionManager.CheckContentsVersion(nkmlua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCGameEventManager.cs", 268))
			{
				return false;
			}
			if (nkmlua.LoadCommonPath(bundleName, fileName, true) && nkmlua.OpenTable(tableName))
			{
				int num = 1;
				while (nkmlua.OpenTable(num))
				{
					NKCGameEventManager.NKCGameEventTemplet nkcgameEventTemplet = new NKCGameEventManager.NKCGameEventTemplet();
					if (nkcgameEventTemplet.LoadFromLUA(nkmlua))
					{
						if (!dicTemplet.ContainsKey(nkcgameEventTemplet.EventID))
						{
							List<NKCGameEventManager.NKCGameEventTemplet> value = new List<NKCGameEventManager.NKCGameEventTemplet>();
							dicTemplet.Add(nkcgameEventTemplet.EventID, value);
						}
						dicTemplet[nkcgameEventTemplet.EventID].Add(nkcgameEventTemplet);
					}
					num++;
					nkmlua.CloseTable();
				}
				nkmlua.CloseTable();
			}
			nkmlua.LuaClose();
			return true;
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x00113D64 File Offset: 0x00111F64
		private static List<NKCGameEventManager.NKCGameEventTemplet> GetEventTempletList(int eventID)
		{
			if (NKCGameEventManager.m_dicGameEventTemplet == null)
			{
				NKCGameEventManager.LoadGameEvent();
			}
			List<NKCGameEventManager.NKCGameEventTemplet> result;
			if (NKCGameEventManager.m_dicGameEventTemplet.TryGetValue(eventID, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x00113D8F File Offset: 0x00111F8F
		public static bool IsGameCameraStopRequired()
		{
			return NKCGameEventManager.m_lstCurrentEvent != null && NKCGameEventManager.m_bIsPauseEvent;
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x00113DA4 File Offset: 0x00111FA4
		public static bool IsEventPlaying()
		{
			return NKCGameEventManager.m_lstCurrentEvent != null;
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x00113DAE File Offset: 0x00111FAE
		public static int GetCurrentEventID()
		{
			if (NKCGameEventManager.m_lstCurrentEvent == null)
			{
				return -1;
			}
			return NKCGameEventManager.m_lstCurrentEvent[0].EventID;
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x00113DC9 File Offset: 0x00111FC9
		public static bool IsPauseEventPlaying()
		{
			return NKCGameEventManager.m_lstCurrentEvent != null && NKCGameEventManager.m_bIsPauseEvent;
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x00113DD9 File Offset: 0x00111FD9
		public static void Update(float deltaTime)
		{
			if (NKCGameEventManager.GetCurrentEventType() == NKCGameEventManager.GameEventType.WAIT_SECONDS)
			{
				NKCGameEventManager.m_fWaitTime -= deltaTime;
				if (NKCGameEventManager.m_fWaitTime < 0f)
				{
					NKCGameEventManager.ProcessEvent();
				}
			}
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x00113E01 File Offset: 0x00112001
		public static void EndCutScene()
		{
			NKCSoundManager.StopAllSound();
			NKCSoundManager.PlayScenMusic(NKCScenManager.GetScenManager().GetNowScenID(), false);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME)
			{
				NKCScenManager.GetScenManager().GetGameClient().GetGameHud().HUD_UNHIDE();
			}
			NKCGameEventManager.ProcessEvent();
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x00113E40 File Offset: 0x00112040
		public static void PlayGameEvent(int eventID, bool isPauseEvent, NKCGameEventManager.OnEventFinish onEventFinish)
		{
			if (NKCGameEventManager.m_lstCurrentEvent != null)
			{
				NKCGameEventManager.FinishEventTemplet(NKCGameEventManager.m_lstCurrentEvent[NKCGameEventManager.m_CurrentIndex]);
			}
			NKCGameEventManager.m_fGuideScreenAlpha = 0.85f;
			NKCGameEventManager.m_strCurrentTalkInvenIcon = "";
			NKCGameEventManager.m_bIsPauseEvent = isPauseEvent;
			NKCGameEventManager.dOnEventFinish = onEventFinish;
			NKCGameEventManager.m_lstCurrentEvent = NKCGameEventManager.GetEventTempletList(eventID);
			if (NKCGameEventManager.m_lstCurrentEvent == null)
			{
				return;
			}
			NKCGameEventManager.m_CurrentIndex = -1;
			NKCGameEventManager.ProcessEvent();
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x00113EA6 File Offset: 0x001120A6
		public static bool IsWaiting()
		{
			return NKCGameEventManager.m_lstCurrentEvent != null && (NKCGameEventManager.m_CurrentIndex < NKCGameEventManager.m_lstCurrentEvent.Count && NKCGameEventManager.m_lstCurrentEvent[NKCGameEventManager.m_CurrentIndex].EventType == NKCGameEventManager.GameEventType.WAIT);
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x00113EDC File Offset: 0x001120DC
		public static void WaitFinished()
		{
			if (!NKCGameEventManager.IsWaiting())
			{
				return;
			}
			NKCGameEventManager.ProcessEvent();
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x00113EEC File Offset: 0x001120EC
		public static void TutorialCompletePacketSent(int id)
		{
			if (NKCGameEventManager.m_lstCurrentEvent == null)
			{
				return;
			}
			if (NKCGameEventManager.m_CurrentIndex < 0)
			{
				return;
			}
			if (NKCGameEventManager.m_CurrentIndex >= NKCGameEventManager.m_lstCurrentEvent.Count)
			{
				return;
			}
			NKCGameEventManager.NKCGameEventTemplet nkcgameEventTemplet = NKCGameEventManager.m_lstCurrentEvent[NKCGameEventManager.m_CurrentIndex];
			if (nkcgameEventTemplet.EventType == NKCGameEventManager.GameEventType.TUTORIAL_MARK_COMPLETE && nkcgameEventTemplet.Value == id)
			{
				NKCGameEventManager.ProcessEvent();
				return;
			}
			Debug.LogError("Tutorial complete packet sent, but was not waiting");
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x00113F4E File Offset: 0x0011214E
		public static void ResumeEvent()
		{
			if (NKCGameEventManager.IsEventPlaying())
			{
				NKCGameEventManager.FinishEventTemplet(NKCGameEventManager.m_lstCurrentEvent[NKCGameEventManager.m_CurrentIndex]);
				NKCGameEventManager.PlayEventTemplet(NKCGameEventManager.m_lstCurrentEvent[NKCGameEventManager.m_CurrentIndex]);
			}
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x00113F80 File Offset: 0x00112180
		private static void ProcessEvent()
		{
			if (NKCGameEventManager.m_lstCurrentEvent == null)
			{
				return;
			}
			if (NKCGameEventManager.m_CurrentIndex >= 0)
			{
				NKCGameEventManager.NKCGameEventTemplet nkcgameEventTemplet = NKCGameEventManager.m_lstCurrentEvent[NKCGameEventManager.m_CurrentIndex];
				NKCGameEventManager.FinishEventTemplet(nkcgameEventTemplet);
				if (nkcgameEventTemplet != null && nkcgameEventTemplet.EventType == NKCGameEventManager.GameEventType.OPEN_NICKNAME_CHANGE_POPUP)
				{
					NKCAdjustManager.OnCustomEvent("13_username_creation");
				}
			}
			NKCGameEventManager.m_CurrentIndex++;
			if (NKCGameEventManager.m_CurrentIndex < NKCGameEventManager.m_lstCurrentEvent.Count)
			{
				NKCGameEventManager.PlayEventTemplet(NKCGameEventManager.m_lstCurrentEvent[NKCGameEventManager.m_CurrentIndex]);
				return;
			}
			NKCGameEventManager.m_lstCurrentEvent = null;
			NKCGameEventManager.m_CurrentIndex = -1;
			NKCGameEventManager.OnEventFinish onEventFinish = NKCGameEventManager.dOnEventFinish;
			if (onEventFinish != null)
			{
				onEventFinish(NKCGameEventManager.m_bIsPauseEvent);
			}
			NKCGameEventManager.dOnEventFinish = null;
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x00114023 File Offset: 0x00112223
		private static void OpenTutorialGuide(Renderer targetRenderer, string text, UnityAction onComplete, NKCUIComRectScreen.ScreenExpand expandFlag = NKCUIComRectScreen.ScreenExpand.None)
		{
			NKCUIOverlayTutorialGuide.Instance.Open(targetRenderer, text, onComplete, expandFlag);
			NKCUIOverlayTutorialGuide.Instance.SetBGScreenAlpha(NKCGameEventManager.m_fGuideScreenAlpha);
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x00114042 File Offset: 0x00112242
		private static void OpenTutorialGuide(RectTransform rtClickableArea, NKCUIOverlayTutorialGuide.ClickGuideType type, string text, UnityAction onComplete, bool bIsFromMidCanvas = false, NKCUIComRectScreen.ScreenExpand expandFlag = NKCUIComRectScreen.ScreenExpand.None)
		{
			NKCUIOverlayTutorialGuide.Instance.Open(rtClickableArea, type, text, onComplete, bIsFromMidCanvas, expandFlag, 0f);
			NKCUIOverlayTutorialGuide.Instance.SetBGScreenAlpha(NKCGameEventManager.m_fGuideScreenAlpha);
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x0011406C File Offset: 0x0011226C
		private static void PlayEventTemplet(NKCGameEventManager.NKCGameEventTemplet eventTemplet)
		{
			if (eventTemplet == null)
			{
				return;
			}
			switch (eventTemplet.EventType)
			{
			case NKCGameEventManager.GameEventType.HIGHLIGHT_UI:
			{
				GameObject gameObject = NKCGameEventManager.FindGameObject(eventTemplet.StringValue);
				if (gameObject == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				RectTransform component = gameObject.GetComponent<RectTransform>();
				bool bMiddleCanvas = eventTemplet.Value != 0;
				NKCUIOverlayTutorialGuide.ClickGuideType guideType = NKCUIOverlayTutorialGuide.ClickGuideType.None;
				NKCGameEventManager.OpenTutorialGuideBySettedFace(component, guideType, eventTemplet, new UnityAction(NKCGameEventManager.ProcessEvent), bMiddleCanvas);
				return;
			}
			case NKCGameEventManager.GameEventType.TEXT:
				NKCUIOverlayTutorialGuide.Instance.Open(null, NKCUIOverlayTutorialGuide.ClickGuideType.None, eventTemplet.Text, new UnityAction(NKCGameEventManager.ProcessEvent), false, NKCUIComRectScreen.ScreenExpand.None, (float)eventTemplet.Value);
				NKCUIOverlayTutorialGuide.Instance.SetBGScreenAlpha(NKCGameEventManager.m_fGuideScreenAlpha);
				return;
			case NKCGameEventManager.GameEventType.MESSAGE_BOX:
				NKCUIOverlayCharMessage.Instance.Open(eventTemplet.StringValue, eventTemplet.Text, (float)eventTemplet.Value, new UnityAction(NKCGameEventManager.ProcessEvent), false);
				NKCUIOverlayCharMessage.Instance.SetBGScreenAlpha(NKCGameEventManager.m_fGuideScreenAlpha);
				return;
			case NKCGameEventManager.GameEventType.MOVE_CAMERA:
			{
				NKCGameClient gameClient = NKCScenManager.GetScenManager().GetGameClient();
				if (gameClient != null)
				{
					gameClient.TutorialForceCamMove((float)eventTemplet.Value / 100f);
				}
				NKCGameEventManager.ProcessEvent();
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_MARK_COMPLETE:
				if (NKCScenManager.CurrentUserData().m_MissionData.GetCompletedMissionData(eventTemplet.Value) != null)
				{
					NKCGameEventManager.ProcessEvent();
				}
				NKCTutorialManager.CompleteTutorial(eventTemplet.Value);
				return;
			case NKCGameEventManager.GameEventType.TUTORIAL_NEXT:
				NKCTutorialManager.PlayTutorial(eventTemplet.Value);
				return;
			case NKCGameEventManager.GameEventType.WAIT:
				break;
			case NKCGameEventManager.GameEventType.TUTORIAL_UNIT_SKILL_GUIDE:
			{
				NKCGameClient gameClient2 = NKCScenManager.GetScenManager().GetGameClient();
				gameClient2.SetCameraMode(NKM_GAME_CAMERA_MODE.NGCM_STOP);
				NKMUnit nkmunit = gameClient2.GetUnitChain().Find((NKMUnit x) => x.GetUnitData().m_UnitID == eventTemplet.Value);
				if (nkmunit != null && nkmunit is NKCUnitClient)
				{
					NKCUnitClient nkcunitClient = nkmunit as NKCUnitClient;
					GameObject objectUnitSkillGuage = nkcunitClient.GetObjectUnitSkillGuage();
					float posX = nkcunitClient.GetUnitSyncData().m_PosX;
					float fMaxX = gameClient2.GetMapTemplet().m_fMaxX;
					if (fMaxX > 0f)
					{
						gameClient2.TutorialForceCamMove(posX / fMaxX);
					}
					NKCGameEventManager.OpenTutorialGuide(objectUnitSkillGuage.GetComponent<RectTransform>(), NKCUIOverlayTutorialGuide.ClickGuideType.None, eventTemplet.Text, new UnityAction(NKCGameEventManager.ProcessEvent), true, NKCUIComRectScreen.ScreenExpand.None);
					return;
				}
				Debug.LogError(string.Format("GameEventManager {0} - Target Unit(ID : {1} Not Found!", eventTemplet.EventType, eventTemplet.Value));
				NKCGameEventManager.ProcessEvent();
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_UNIT_SUMMON_GUIDE:
			{
				NKCGameHudDeckSlot hudDeckByUnitID = NKCScenManager.GetScenManager().GetGameClient().GetGameHud().GetHudDeckByUnitID(eventTemplet.Value);
				if (!(hudDeckByUnitID != null))
				{
					Debug.LogWarning(string.Format("GameEventManager {0} - Target Unit(ID : {1} Not Found From HUD!", eventTemplet.EventType, eventTemplet.Value));
					NKCGameEventManager.ProcessEvent();
					return;
				}
				if (!hudDeckByUnitID.CanRespawn())
				{
					Debug.LogWarning("Unit Respawn impossibe. processing event");
					NKCGameEventManager.ProcessEvent();
					return;
				}
				hudDeckByUnitID.SetActive(true, true);
				NKCUIOverlayTutorialGuide.ClickGuideType type = NKCUIOverlayTutorialGuide.ClickGuideType.DeckDrag;
				NKCGameEventManager.OpenTutorialGuide(hudDeckByUnitID.m_rtBG, type, eventTemplet.Text, new UnityAction(NKCGameEventManager.ProcessEvent), false, NKCUIComRectScreen.ScreenExpand.None);
				if (!NKCGameEventManager.m_bIsPauseEvent)
				{
					NKCUIOverlayTutorialGuide.Instance.SetScreenActive(false);
					return;
				}
				break;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_UNIT_RE_SUMMON_GUIDE:
			{
				NKCGameHudDeckSlot hudDeckByUnitID2 = NKCScenManager.GetScenManager().GetGameClient().GetGameHud().GetHudDeckByUnitID(eventTemplet.Value);
				if (!(hudDeckByUnitID2 != null))
				{
					Debug.LogWarning(string.Format("GameEventManager {0} - Target Unit(ID : {1} Not Found From HUD!", eventTemplet.EventType, eventTemplet.Value));
					NKCGameEventManager.ProcessEvent();
					return;
				}
				hudDeckByUnitID2.SetActive(true, true);
				NKCUIOverlayTutorialGuide.ClickGuideType type2 = NKCUIOverlayTutorialGuide.ClickGuideType.DeckDrag;
				NKCGameEventManager.OpenTutorialGuide(hudDeckByUnitID2.m_rtBG, type2, eventTemplet.Text, new UnityAction(NKCGameEventManager.ProcessEvent), false, NKCUIComRectScreen.ScreenExpand.None);
				if (!NKCGameEventManager.m_bIsPauseEvent)
				{
					NKCUIOverlayTutorialGuide.Instance.SetScreenActive(false);
					return;
				}
				break;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_UNIT_HYPER_GUIDE:
			case NKCGameEventManager.GameEventType.TUTORIAL_CLICK_UNIT_HYPER:
			{
				NKCGameClient gameClient3 = NKCScenManager.GetScenManager().GetGameClient();
				gameClient3.SetCameraMode(NKM_GAME_CAMERA_MODE.NGCM_STOP);
				NKMUnit nkmunit2 = gameClient3.GetUnitChain().Find((NKMUnit x) => x.GetUnitData().m_UnitID == eventTemplet.Value);
				if (nkmunit2 == null || !(nkmunit2 is NKCUnitClient))
				{
					Debug.LogError(string.Format("GameEventManager {0} - Target Unit(ID : {1} Not Found!", eventTemplet.EventType, eventTemplet.Value));
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCUnitClient unitClient = nkmunit2 as NKCUnitClient;
				GameObject objectUnitHyper = unitClient.GetObjectUnitHyper();
				if (unitClient.GetHyperSkillCoolRate() > 0f)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				float posX2 = unitClient.GetUnitSyncData().m_PosX;
				float fMaxX2 = gameClient3.GetMapTemplet().m_fMaxX;
				if (fMaxX2 > 0f)
				{
					gameClient3.TutorialForceCamMove(posX2 / fMaxX2);
				}
				if (eventTemplet.EventType == NKCGameEventManager.GameEventType.TUTORIAL_CLICK_UNIT_HYPER)
				{
					NKCGameEventManager.OpenTutorialGuideBySettedFace(objectUnitHyper.GetComponent<RectTransform>(), NKCUIOverlayTutorialGuide.ClickGuideType.Touch, eventTemplet, null, true);
					NKCUIOverlayTutorialGuide.Instance.SetStealInput(delegate(BaseEventData x)
					{
						NKCUIOverlayCharMessage.CheckInstanceAndClose();
						NKCUIOverlayTutorialGuide.CheckInstanceAndClose();
						unitClient.UseManualSkill();
						NKCGameEventManager.ProcessEvent();
					});
					return;
				}
				NKCGameEventManager.OpenTutorialGuide(objectUnitHyper.GetComponent<RectTransform>(), NKCUIOverlayTutorialGuide.ClickGuideType.None, eventTemplet.Text, new UnityAction(NKCGameEventManager.ProcessEvent), true, NKCUIComRectScreen.ScreenExpand.None);
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_HIGHLIGHT_DECK:
			{
				NKCGameHudDeckSlot hudDeckByUnitID3 = NKCScenManager.GetScenManager().GetGameClient().GetGameHud().GetHudDeckByUnitID(eventTemplet.Value);
				if (!(hudDeckByUnitID3 != null))
				{
					Debug.LogError(string.Format("GameEventManager {0} - Target Unit(ID : {1} Not Found From HUD!", eventTemplet.EventType, eventTemplet.Value));
					NKCGameEventManager.ProcessEvent();
					return;
				}
				hudDeckByUnitID3.SetActive(true, true);
				NKCUIOverlayTutorialGuide.ClickGuideType type3 = NKCUIOverlayTutorialGuide.ClickGuideType.None;
				string stringValue = eventTemplet.StringValue;
				RectTransform rtClickableArea;
				if (stringValue != null && stringValue == "ATKTYPE")
				{
					rtClickableArea = hudDeckByUnitID3.GetRectATKMark();
				}
				else
				{
					rtClickableArea = hudDeckByUnitID3.m_rtBG;
				}
				NKCGameEventManager.OpenTutorialGuide(rtClickableArea, type3, eventTemplet.Text, new UnityAction(NKCGameEventManager.ProcessEvent), false, NKCUIComRectScreen.ScreenExpand.None);
				if (!NKCGameEventManager.m_bIsPauseEvent)
				{
					NKCUIOverlayTutorialGuide.Instance.SetScreenActive(false);
					return;
				}
				break;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_HIGHLIGHT_UNIT:
			{
				NKCGameClient gameClient4 = NKCScenManager.GetScenManager().GetGameClient();
				gameClient4.SetCameraMode(NKM_GAME_CAMERA_MODE.NGCM_STOP);
				NKMUnit nkmunit3 = gameClient4.GetUnitChain().Find((NKMUnit x) => x.GetUnitData().m_UnitID == eventTemplet.Value);
				if (nkmunit3 == null || !(nkmunit3 is NKCUnitClient))
				{
					Debug.LogError(string.Format("GameEventManager {0} - Target Unit(ID : {1} Not Found!", eventTemplet.EventType, eventTemplet.Value));
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCUnitClient nkcunitClient2 = nkmunit3 as NKCUnitClient;
				float posX3 = nkcunitClient2.GetUnitSyncData().m_PosX;
				float fMaxX3 = gameClient4.GetMapTemplet().m_fMaxX;
				if (fMaxX3 > 0f)
				{
					gameClient4.TutorialForceCamMove(posX3 / fMaxX3);
				}
				NKCASUnitSpineSprite nkcasunitSpineSprite = nkcunitClient2.GetNKCASUnitSpineSprite();
				if (nkcasunitSpineSprite != null)
				{
					NKCGameEventManager.OpenTutorialGuide(nkcasunitSpineSprite.m_MeshRenderer, eventTemplet.Text, new UnityAction(NKCGameEventManager.ProcessEvent), NKCUIComRectScreen.ScreenExpand.None);
					return;
				}
				Debug.LogError(string.Format("GameEventManager {0} - Target Unit(ID : {1} Has no renderer!", eventTemplet.EventType, eventTemplet.Value));
				NKCGameEventManager.ProcessEvent();
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_SHIP_SKILL_GUIDE:
			case NKCGameEventManager.GameEventType.TUTORIAL_UNLOCK_SHIP_SKILL:
			case NKCGameEventManager.GameEventType.TUTORIAL_HIGHLIGHT_SHIP_SKILL:
			{
				NKCUIHudShipSkillDeck hudSkillBySkillID = NKCScenManager.GetScenManager().GetGameClient().GetGameHud().GetHudSkillBySkillID(eventTemplet.Value);
				if (!(hudSkillBySkillID != null))
				{
					Debug.LogError(string.Format("GameEventManager {0} - Target Skill(ID : {1} Not Found From HUD!", eventTemplet.EventType, eventTemplet.Value));
					NKCGameEventManager.ProcessEvent();
					return;
				}
				hudSkillBySkillID.SetActive(true, true);
				if (eventTemplet.EventType == NKCGameEventManager.GameEventType.TUTORIAL_UNLOCK_SHIP_SKILL)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCUIOverlayTutorialGuide.ClickGuideType type4;
				if (eventTemplet.EventType == NKCGameEventManager.GameEventType.TUTORIAL_SHIP_SKILL_GUIDE)
				{
					type4 = NKCUIOverlayTutorialGuide.ClickGuideType.ShipSkill;
				}
				else
				{
					type4 = NKCUIOverlayTutorialGuide.ClickGuideType.None;
				}
				NKCGameEventManager.OpenTutorialGuide(hudSkillBySkillID.m_rtSubRoot, type4, eventTemplet.Text, new UnityAction(NKCGameEventManager.ProcessEvent), false, NKCUIComRectScreen.ScreenExpand.None);
				return;
			}
			case NKCGameEventManager.GameEventType.UNLOCK_TUTORIAL_GAME_RE_RESPAWN:
				if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
				{
					Debug.LogError("게임이 아닌데 튜토리얼 재소환 언락 이벤트를 시도");
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCScenManager.GetScenManager().GetGameClient().UnlockTutorialReRespawn();
				NKCGameEventManager.ProcessEvent();
				return;
			case NKCGameEventManager.GameEventType.TUTORIAL_UNLOCK_DECK:
			{
				NKCGameHud gameHud = NKCScenManager.GetScenManager().GetGameClient().GetGameHud();
				if (gameHud != null)
				{
					gameHud.ShowHud(true);
				}
				NKCGameEventManager.ProcessEvent();
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_UNIT_DEPLOY_AREA_GUIDE:
			{
				NKCGameClient gameClient5 = NKCScenManager.GetScenManager().GetGameClient();
				gameClient5.SetCameraMode(NKM_GAME_CAMERA_MODE.NGCM_STOP);
				Renderer mapInvalidLandRenderer = gameClient5.GetMapInvalidLandRenderer();
				NKCUIComRectScreen.ScreenExpand expandFlag = NKCUIComRectScreen.ScreenExpand.Left | NKCUIComRectScreen.ScreenExpand.Right;
				NKCGameEventManager.OpenTutorialGuide(mapInvalidLandRenderer, eventTemplet.Text, new UnityAction(NKCGameEventManager.ProcessEvent), expandFlag);
				NKCUIOverlayTutorialGuide.Instance.IsShowingInvalidMap = true;
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_SET_TALKER:
				NKCGameEventManager.m_strCurrentTalkInvenIcon = eventTemplet.StringValue;
				NKCGameEventManager.ProcessEvent();
				return;
			case NKCGameEventManager.GameEventType.TUTORIAL_SET_SCREEN_BG_ALPHA:
				NKCGameEventManager.m_fGuideScreenAlpha = (float)eventTemplet.Value / 100f;
				if (NKCGameEventManager.m_fGuideScreenAlpha <= 0f)
				{
					NKCGameEventManager.m_fGuideScreenAlpha = 0.01f;
				}
				NKCGameEventManager.ProcessEvent();
				return;
			case NKCGameEventManager.GameEventType.TUTORIAL_TOUCH_UI:
			{
				GameObject gameObject2 = NKCGameEventManager.FindGameObject(eventTemplet.StringValue);
				if (gameObject2 == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				bool bMiddleCanvas2 = eventTemplet.Value != 0;
				NKCGameEventManager.SetButtonClickSteal(gameObject2.GetComponent<RectTransform>(), eventTemplet, bMiddleCanvas2);
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_HIGHLIGHT_MAINSTREAM:
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_OPERATION)
				{
					NKCScenManager.GetScenManager().Get_SCEN_OPERATION().SetTutorialMainstreamGuide(eventTemplet, new UnityAction(NKCGameEventManager.ProcessEvent));
					return;
				}
				NKCGameEventManager.ProcessEvent();
				return;
			case NKCGameEventManager.GameEventType.TUTORIAL_HIGHLIGHT_EPISODE:
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_OPERATION)
				{
					NKCScenManager.GetScenManager().Get_SCEN_OPERATION().SetTutorialMainstreamGuide(eventTemplet, new UnityAction(NKCGameEventManager.ProcessEvent));
					return;
				}
				NKCGameEventManager.ProcessEvent();
				return;
			case NKCGameEventManager.GameEventType.TUTORIAL_TOUCH_DAILY:
			{
				if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_OPERATION)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				RectTransform dailyRect = NKCScenManager.GetScenManager().Get_SCEN_OPERATION().GetDailyRect();
				if (dailyRect == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.SetButtonClickSteal(dailyRect, eventTemplet, false);
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_TOUCH_STAGE:
			{
				if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_OPERATION)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				RectTransform stageSlot = NKCScenManager.GetScenManager().Get_SCEN_OPERATION().GetStageSlot(eventTemplet.Value);
				if (stageSlot == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.SetButtonClickSteal(stageSlot, eventTemplet, false);
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_TOUCH_ACT:
			{
				if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_OPERATION)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				RectTransform actSlot = NKCScenManager.GetScenManager().Get_SCEN_OPERATION().GetActSlot(eventTemplet.Value);
				if (actSlot == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.SetButtonClickSteal(actSlot, eventTemplet, false);
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_UNLOCK_DECK_BUTTON:
			{
				if (!NKCUIDeckViewer.IsInstanceOpen)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCUIDeckViewer.Instance.SelectDeck(new NKMDeckIndex(NKM_DECK_TYPE.NDT_NORMAL, 0));
				NKCDeckListButton deckListButton = NKCUIDeckViewer.Instance.m_NKCDeckViewList.GetDeckListButton(eventTemplet.Value);
				if (deckListButton == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.OpenTutorialGuide(deckListButton.m_cbtnButton.GetComponent<RectTransform>(), NKCUIOverlayTutorialGuide.ClickGuideType.Touch, eventTemplet.Text, null, false, NKCUIComRectScreen.ScreenExpand.None);
				NKCUIOverlayTutorialGuide.Instance.SetStealInput(delegate(BaseEventData x)
				{
					if (NKCScenManager.CurrentUserData().m_ArmyData.GetDeckData(NKM_DECK_TYPE.NDT_NORMAL, eventTemplet.Value) != null)
					{
						NKCUIDeckViewer.Instance.SelectDeck(new NKMDeckIndex(NKM_DECK_TYPE.NDT_NORMAL, eventTemplet.Value));
					}
					else
					{
						NKCUIDeckViewer.Instance.DeckUnlockRequestPopup(new NKMDeckIndex(NKM_DECK_TYPE.NDT_NORMAL, eventTemplet.Value));
					}
					NKCGameEventManager.ProcessEvent();
				});
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_UNLOCK_DECK_BUTTON_LAST:
			{
				if (!NKCUIDeckViewer.IsInstanceOpen)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				int unlockDeckCount = (int)nkmuserData.m_ArmyData.GetUnlockedDeckCount(NKM_DECK_TYPE.NDT_NORMAL);
				if (unlockDeckCount == (int)nkmuserData.m_ArmyData.GetMaxDeckCount(NKM_DECK_TYPE.NDT_NORMAL))
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCUIDeckViewer.Instance.SetDeckScroll(unlockDeckCount);
				NKCDeckListButton deckListButton2 = NKCUIDeckViewer.Instance.m_NKCDeckViewList.GetDeckListButton(unlockDeckCount);
				if (deckListButton2 == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.OpenTutorialGuide(deckListButton2.m_cbtnButton.GetComponent<RectTransform>(), NKCUIOverlayTutorialGuide.ClickGuideType.Touch, eventTemplet.Text, null, false, NKCUIComRectScreen.ScreenExpand.None);
				NKCUIOverlayTutorialGuide.Instance.SetStealInput(delegate(BaseEventData x)
				{
					NKCUIDeckViewer.Instance.DeckUnlockRequestPopup(new NKMDeckIndex(NKM_DECK_TYPE.NDT_NORMAL, unlockDeckCount));
					NKCGameEventManager.ProcessEvent();
				});
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_SELECT_DECKVIEWER_DECK:
			case NKCGameEventManager.GameEventType.TUTORIAL_HIGHLIGHT_DECKVIEWER_DECK:
			{
				if (!NKCUIDeckViewer.IsInstanceOpen)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCDeckViewUnitSlot nkcdeckViewUnitSlot = null;
				if (eventTemplet.Value >= NKCUIDeckViewer.Instance.m_NKCDeckViewUnit.m_listNKCDeckViewUnitSlot.Count)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				List<NKCDeckViewUnitSlot> listNKCDeckViewUnitSlot = NKCUIDeckViewer.Instance.m_NKCDeckViewUnit.m_listNKCDeckViewUnitSlot;
				if (string.IsNullOrEmpty(eventTemplet.StringValue))
				{
					nkcdeckViewUnitSlot = listNKCDeckViewUnitSlot[eventTemplet.Value];
				}
				else
				{
					string stringValue = eventTemplet.StringValue;
					if (stringValue != null)
					{
						if (!(stringValue == "UNIT"))
						{
							if (stringValue == "EMPTY")
							{
								List<NKCDeckViewUnitSlot> list = listNKCDeckViewUnitSlot.FindAll((NKCDeckViewUnitSlot v) => v.IsEmpty());
								if (eventTemplet.Value < list.Count)
								{
									nkcdeckViewUnitSlot = list[eventTemplet.Value];
								}
							}
						}
						else
						{
							List<NKCDeckViewUnitSlot> list2 = listNKCDeckViewUnitSlot.FindAll((NKCDeckViewUnitSlot v) => !v.IsEmpty());
							if (eventTemplet.Value < list2.Count)
							{
								nkcdeckViewUnitSlot = list2[eventTemplet.Value];
							}
						}
					}
				}
				if (nkcdeckViewUnitSlot == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				int index = listNKCDeckViewUnitSlot.IndexOf(nkcdeckViewUnitSlot);
				if (eventTemplet.EventType == NKCGameEventManager.GameEventType.TUTORIAL_SELECT_DECKVIEWER_DECK)
				{
					NKCGameEventManager.OpenTutorialGuideBySettedFace(nkcdeckViewUnitSlot.m_NKCUIComButton.GetComponent<RectTransform>(), NKCUIOverlayTutorialGuide.ClickGuideType.Touch, eventTemplet, null, false);
					NKCUIOverlayTutorialGuide.Instance.SetStealInput(delegate(BaseEventData x)
					{
						NKCUIDeckViewer.Instance.OnUnitClicked(index);
						NKCGameEventManager.ProcessEvent();
					});
					return;
				}
				NKCGameEventManager.OpenTutorialGuide(nkcdeckViewUnitSlot.m_NKCUIComButton.GetComponent<RectTransform>(), NKCUIOverlayTutorialGuide.ClickGuideType.None, eventTemplet.Text, new UnityAction(NKCGameEventManager.ProcessEvent), false, NKCUIComRectScreen.ScreenExpand.None);
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_SELECT_DECKVIEWER_SHIP:
			{
				if (!NKCUIDeckViewer.IsInstanceOpen)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCUIComButton shipSelectButton = NKCUIDeckViewer.Instance.GetShipSelectButton();
				if (shipSelectButton == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.SetButtonClickSteal(shipSelectButton.GetComponent<RectTransform>(), eventTemplet, false);
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_SELECT_DECKVIEWERLIST_UNIT:
			case NKCGameEventManager.GameEventType.TUTORIAL_SELECT_DECKVIEWERLIST_SHIP:
			{
				NKM_UNIT_TYPE type5 = (eventTemplet.EventType == NKCGameEventManager.GameEventType.TUTORIAL_SELECT_DECKVIEWERLIST_SHIP) ? NKM_UNIT_TYPE.NUT_SHIP : NKM_UNIT_TYPE.NUT_NORMAL;
				if (!NKCUIDeckViewer.IsInstanceOpen)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCUIUnitSelectListSlotBase slot = NKCUIDeckViewer.Instance.SetTutorialSelectUnit(type5, eventTemplet.Value);
				if (slot == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.OpenTutorialGuideBySettedFace(slot.GetComponent<RectTransform>(), NKCUIOverlayTutorialGuide.ClickGuideType.Touch, eventTemplet, null, false);
				NKCUIOverlayTutorialGuide.Instance.SetStealInput(delegate(BaseEventData x)
				{
					slot.InvokeClick();
					NKCGameEventManager.ProcessEvent();
				});
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_SELECT_DECKVIEWERLIST_SLOTTYPE:
			{
				if (!NKCUIDeckViewer.IsInstanceOpen)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCDeckViewUnitSelectList.SlotType type6;
				if (!eventTemplet.StringValue.TryParse(out type6, false))
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCUIUnitSelectListSlotBase slot = NKCUIDeckViewer.Instance.GetTutorialSelectSlotType(type6);
				if (slot == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.OpenTutorialGuideBySettedFace(slot.GetComponent<RectTransform>(), NKCUIOverlayTutorialGuide.ClickGuideType.Touch, eventTemplet, null, false);
				NKCUIOverlayTutorialGuide.Instance.SetStealInput(delegate(BaseEventData x)
				{
					slot.InvokeClick();
					NKCGameEventManager.ProcessEvent();
				});
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_CLICK_WARFARE_UNIT:
			{
				if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_WARFARE_GAME)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCWarfareGame warfareGame = NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareGame();
				if (warfareGame == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				if (string.IsNullOrEmpty(eventTemplet.StringValue))
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKM_WARFARE_MAP_TILE_TYPE nkm_WARFARE_MAP_TILE_TYPE = NKM_WARFARE_MAP_TILE_TYPE.NWMTT_NORMAL;
				if (!eventTemplet.StringValue.TryParse(out nkm_WARFARE_MAP_TILE_TYPE, false))
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
				Dictionary<int, WarfareUnitData>.ValueCollection values = warfareGameData.warfareTeamDataA.warfareUnitDataByUIDMap.Values;
				GameObject gameObject3 = null;
				foreach (WarfareUnitData warfareUnitData in values)
				{
					if (warfareUnitData.hp > 0f)
					{
						WarfareTileData tileData = warfareGameData.GetTileData((int)warfareUnitData.tileIndex);
						if (tileData != null && tileData.tileType == nkm_WARFARE_MAP_TILE_TYPE)
						{
							gameObject3 = warfareGame.GetUnitObject(warfareUnitData.warfareGameUnitUID);
							if (gameObject3 != null)
							{
								break;
							}
						}
					}
				}
				if (gameObject3 == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.SetButtonClickSteal(gameObject3.GetComponent<RectTransform>(), eventTemplet, true);
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_CLICK_WARFARE_TILE:
			{
				if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_WARFARE_GAME)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCWarfareGame warfareGame2 = NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareGame();
				if (warfareGame2 == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				GameObject tileObject = warfareGame2.GetTileObject(eventTemplet.Value);
				if (tileObject == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.OpenTutorialGuideBySettedFace(tileObject.GetComponent<RectTransform>(), NKCUIOverlayTutorialGuide.ClickGuideType.Touch, eventTemplet, null, true);
				NKCUIOverlayTutorialGuide.Instance.SetStealInput(delegate(BaseEventData x)
				{
					NKCWarfareGame warfareGame5 = NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareGame();
					if (warfareGame5 != null)
					{
						warfareGame5.ProcessTutorialTileTouchEvent(eventTemplet);
					}
					NKCGameEventManager.ProcessEvent();
				});
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_HIGHLIGHT_WARFARE_TILE:
			{
				if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_WARFARE_GAME)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCWarfareGame warfareGame3 = NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareGame();
				if (warfareGame3 == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				GameObject tileObject2 = warfareGame3.GetTileObject(eventTemplet.Value);
				if (tileObject2 == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.OpenTutorialGuideBySettedFace(tileObject2.GetComponent<RectTransform>(), NKCUIOverlayTutorialGuide.ClickGuideType.None, eventTemplet, new UnityAction(NKCGameEventManager.ProcessEvent), true);
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_WARFARE_AUTO:
			{
				if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_WARFARE_GAME)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCWarfareGame warfareGame4 = NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareGame();
				if (warfareGame4 == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				bool flag = Convert.ToBoolean(eventTemplet.StringValue);
				if (NKCScenManager.GetScenManager().GetMyUserData().m_UserOption.m_bAutoWarfare == flag)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				warfareGame4.SetAutoForTutorial(flag);
				NKCGameEventManager.ProcessEvent();
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_SELECT_UNITLIST_UNIT:
			{
				NKCUIUnitSelectList openedUIByType = NKCUIManager.GetOpenedUIByType<NKCUIUnitSelectList>();
				if (openedUIByType == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCUnitSortSystem.UnitListOptions unitListOptions = new NKCUnitSortSystem.UnitListOptions
				{
					eDeckType = NKM_DECK_TYPE.NDT_NORMAL,
					setExcludeUnitID = null,
					setOnlyIncludeUnitID = new HashSet<int>(),
					setDuplicateUnitID = null,
					setExcludeUnitUID = null,
					bExcludeLockedUnit = false,
					bExcludeDeckedUnit = false,
					setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>(),
					lstSortOption = NKCUnitSortSystem.GetDefaultSortOptions(NKM_UNIT_TYPE.NUT_NORMAL, false, false),
					bDescending = true,
					bHideDeckedUnit = false,
					bPushBackUnselectable = true,
					bIncludeUndeckableUnit = true
				};
				unitListOptions.setOnlyIncludeUnitID.Add(eventTemplet.Value);
				foreach (string text in eventTemplet.StringValue.Split(new char[]
				{
					',',
					' '
				}))
				{
					if (text != null)
					{
						uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
						if (num <= 2871893995U)
						{
							if (num <= 1214078067U)
							{
								if (num != 494738934U)
								{
									if (num == 1214078067U)
									{
										if (text == "UNSELECTED")
										{
											if (unitListOptions.setExcludeUnitUID == null)
											{
												unitListOptions.setExcludeUnitUID = new HashSet<long>(openedUIByType.GetSelectedUnitList());
											}
											else
											{
												unitListOptions.setExcludeUnitUID.UnionWith(openedUIByType.GetSelectedUnitList());
											}
										}
									}
								}
								else if (text == "HIGHEST_LEVEL")
								{
									unitListOptions.lstSortOption = new List<NKCUnitSortSystem.eSortOption>
									{
										NKCUnitSortSystem.eSortOption.Level_High
									};
									unitListOptions.bDescending = true;
								}
							}
							else if (num != 2377950523U)
							{
								if (num != 2631408967U)
								{
									if (num == 2871893995U)
									{
										if (text == "EXCLUDE_LOCKED_UNIT")
										{
											unitListOptions.bExcludeLockedUnit = true;
										}
									}
								}
								else if (text == "INCLUDE_DECKED_UNIT")
								{
									unitListOptions.bExcludeDeckedUnit = false;
									unitListOptions.bHideDeckedUnit = false;
								}
							}
							else if (text == "FILTER_FUNC_CAN_LIMIT_BREAK")
							{
								unitListOptions.AdditionalExcludeFilterFunc = new NKCUnitSortSystem.UnitListOptions.CustomFilterFunc(NKMUnitLimitBreakManager.CanThisUnitLimitBreak);
							}
						}
						else if (num <= 3734663348U)
						{
							if (num != 3049946409U)
							{
								if (num == 3734663348U)
								{
									if (text == "UID_FIRST")
									{
										unitListOptions.lstSortOption = new List<NKCUnitSortSystem.eSortOption>
										{
											NKCUnitSortSystem.eSortOption.UID_First
										};
										unitListOptions.bDescending = true;
									}
								}
							}
							else if (text == "INCLUDE_LOCKED_UNIT")
							{
								unitListOptions.bExcludeLockedUnit = false;
							}
						}
						else if (num != 3838633650U)
						{
							if (num != 3894207533U)
							{
								if (num == 4015308218U)
								{
									if (text == "UID_LAST")
									{
										unitListOptions.lstSortOption = new List<NKCUnitSortSystem.eSortOption>
										{
											NKCUnitSortSystem.eSortOption.UID_Last
										};
										unitListOptions.bDescending = true;
									}
								}
							}
							else if (text == "EXCLUDE_DECKED_UNIT")
							{
								unitListOptions.bExcludeDeckedUnit = true;
							}
						}
						else if (text == "LOWEST_LEVEL")
						{
							unitListOptions.lstSortOption = new List<NKCUnitSortSystem.eSortOption>
							{
								NKCUnitSortSystem.eSortOption.Level_Low
							};
							unitListOptions.bDescending = false;
						}
					}
				}
				NKMUnitData nkmunitData = new NKCUnitSort(NKCScenManager.CurrentUserData(), unitListOptions).AutoSelect(null, null);
				if (nkmunitData == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.SetButtonClickSteal(openedUIByType.ScrollToUnitAndGetRect(nkmunitData.m_UnitUID), eventTemplet, false);
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_SELECT_UNITLIST_SHIP:
			{
				NKCUIUnitSelectList openedUIByType2 = NKCUIManager.GetOpenedUIByType<NKCUIUnitSelectList>();
				if (openedUIByType2 == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCUnitSortSystem.UnitListOptions unitListOptions2 = new NKCUnitSortSystem.UnitListOptions
				{
					eDeckType = NKM_DECK_TYPE.NDT_NORMAL,
					setExcludeUnitID = null,
					setOnlyIncludeUnitID = new HashSet<int>(),
					setDuplicateUnitID = null,
					setExcludeUnitUID = null,
					bExcludeLockedUnit = false,
					bExcludeDeckedUnit = false,
					setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>(),
					lstSortOption = NKCUnitSortSystem.GetDefaultSortOptions(NKM_UNIT_TYPE.NUT_SHIP, false, false),
					bDescending = true,
					bHideDeckedUnit = false,
					bPushBackUnselectable = true,
					bIncludeUndeckableUnit = true
				};
				unitListOptions2.setOnlyIncludeUnitID.Add(eventTemplet.Value);
				foreach (string text2 in eventTemplet.StringValue.Split(new char[]
				{
					',',
					' '
				}))
				{
					if (text2 != null)
					{
						uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
						if (num <= 2631408967U)
						{
							if (num <= 1214078067U)
							{
								if (num != 494738934U)
								{
									if (num == 1214078067U)
									{
										if (text2 == "UNSELECTED")
										{
											if (unitListOptions2.setExcludeUnitUID == null)
											{
												unitListOptions2.setExcludeUnitUID = new HashSet<long>(openedUIByType2.GetSelectedUnitList());
											}
											else
											{
												unitListOptions2.setExcludeUnitUID.UnionWith(openedUIByType2.GetSelectedUnitList());
											}
										}
									}
								}
								else if (text2 == "HIGHEST_LEVEL")
								{
									unitListOptions2.lstSortOption = new List<NKCUnitSortSystem.eSortOption>
									{
										NKCUnitSortSystem.eSortOption.Level_High
									};
									unitListOptions2.bDescending = true;
								}
							}
							else if (num != 2377950523U)
							{
								if (num == 2631408967U)
								{
									if (text2 == "INCLUDE_DECKED_UNIT")
									{
										unitListOptions2.bExcludeDeckedUnit = false;
										unitListOptions2.bHideDeckedUnit = false;
									}
								}
							}
							else if (text2 == "FILTER_FUNC_CAN_LIMIT_BREAK")
							{
								unitListOptions2.AdditionalExcludeFilterFunc = new NKCUnitSortSystem.UnitListOptions.CustomFilterFunc(NKMUnitLimitBreakManager.CanThisUnitLimitBreak);
							}
						}
						else if (num <= 3049946409U)
						{
							if (num != 2871893995U)
							{
								if (num == 3049946409U)
								{
									if (text2 == "INCLUDE_LOCKED_UNIT")
									{
										unitListOptions2.bExcludeLockedUnit = false;
									}
								}
							}
							else if (text2 == "EXCLUDE_LOCKED_UNIT")
							{
								unitListOptions2.bExcludeLockedUnit = true;
							}
						}
						else if (num != 3838633650U)
						{
							if (num == 3894207533U)
							{
								if (text2 == "EXCLUDE_DECKED_UNIT")
								{
									unitListOptions2.bExcludeDeckedUnit = true;
								}
							}
						}
						else if (text2 == "LOWEST_LEVEL")
						{
							unitListOptions2.lstSortOption = new List<NKCUnitSortSystem.eSortOption>
							{
								NKCUnitSortSystem.eSortOption.Level_Low
							};
							unitListOptions2.bDescending = false;
						}
					}
				}
				NKMUnitData nkmunitData2 = new NKCShipSort(NKCScenManager.CurrentUserData(), unitListOptions2).AutoSelect(null, null);
				if (nkmunitData2 == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.SetButtonClickSteal(openedUIByType2.ScrollToUnitAndGetRect(nkmunitData2.m_UnitUID), eventTemplet, false);
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_SELECT_ITEMLIST_EQUIP:
			{
				NKCUIInventory openedUIByType3 = NKCUIManager.GetOpenedUIByType<NKCUIInventory>();
				if (openedUIByType3 == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCEquipSortSystem.EquipListOptions equipListOptions = default(NKCEquipSortSystem.EquipListOptions);
				equipListOptions.setOnlyIncludeEquipID = new HashSet<int>();
				equipListOptions.setOnlyIncludeEquipID.Add(eventTemplet.Value);
				equipListOptions.bHideEquippedItem = false;
				equipListOptions.bHideLockItem = false;
				equipListOptions.bHideMaxLvItem = false;
				equipListOptions.bLockMaxItem = false;
				equipListOptions.bHideNotPossibleSetOptionItem = false;
				foreach (string text3 in eventTemplet.StringValue.Split(new char[]
				{
					',',
					' '
				}))
				{
					if (text3 != null)
					{
						uint num = <PrivateImplementationDetails>.ComputeStringHash(text3);
						if (num <= 1373615367U)
						{
							if (num <= 634023658U)
							{
								if (num != 494738934U)
								{
									if (num == 634023658U)
									{
										if (text3 == "EXCLUDE_LOCKED_ITEM")
										{
											equipListOptions.bHideLockItem = true;
										}
									}
								}
								else if (text3 == "HIGHEST_LEVEL")
								{
									equipListOptions.lstSortOption = new List<NKCEquipSortSystem.eSortOption>
									{
										NKCEquipSortSystem.eSortOption.Enhance_High
									};
								}
							}
							else if (num != 1214078067U)
							{
								if (num == 1373615367U)
								{
									if (text3 == "EXCLUDE_EQUIPPED_ITEM")
									{
										equipListOptions.bHideEquippedItem = true;
									}
								}
							}
							else if (text3 == "UNSELECTED")
							{
								if (equipListOptions.setExcludeEquipUID == null)
								{
									equipListOptions.setExcludeEquipUID = openedUIByType3.GetSelectedEquips();
								}
								else
								{
									equipListOptions.setExcludeEquipUID.UnionWith(openedUIByType3.GetSelectedEquips());
								}
							}
						}
						else if (num <= 3838633650U)
						{
							if (num != 3734663348U)
							{
								if (num == 3838633650U)
								{
									if (text3 == "LOWEST_LEVEL")
									{
										equipListOptions.lstSortOption = new List<NKCEquipSortSystem.eSortOption>
										{
											NKCEquipSortSystem.eSortOption.Enhance_Low
										};
									}
								}
							}
							else if (text3 == "UID_FIRST")
							{
								equipListOptions.lstSortOption = new List<NKCEquipSortSystem.eSortOption>
								{
									NKCEquipSortSystem.eSortOption.UID_First
								};
							}
						}
						else if (num != 4015308218U)
						{
							if (num == 4278188378U)
							{
								if (text3 == "EXCLUDE_MAX_ITEM")
								{
									equipListOptions.bLockMaxItem = true;
								}
							}
						}
						else if (text3 == "UID_LAST")
						{
							equipListOptions.lstSortOption = new List<NKCEquipSortSystem.eSortOption>
							{
								NKCEquipSortSystem.eSortOption.UID_Last
							};
						}
					}
				}
				NKMEquipItemData nkmequipItemData = new NKCEquipSortSystem(NKCScenManager.CurrentUserData(), equipListOptions).AutoSelect(null, null);
				if (nkmequipItemData == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.SetButtonClickSteal(openedUIByType3.ScrollToUnitAndGetRect(nkmequipItemData.m_ItemUid), eventTemplet, false);
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_AUTO_GUIDE:
			{
				NKCGameHud gameHud2 = NKCScenManager.GetScenManager().GetGameClient().GetGameHud();
				if (gameHud2 == null)
				{
					return;
				}
				gameHud2.SetAutoEnable();
				gameHud2.ToggleAutoRespawn(false);
				NKCUIComButton autoButton = gameHud2.GetAutoButton();
				if (autoButton == null)
				{
					return;
				}
				NKCGameEventManager.SetButtonClickSteal(autoButton.GetComponent<RectTransform>(), eventTemplet, false);
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_TOUCH_ENHANCE_USE_SLOT:
				if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_BASE)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.SetButtonClickSteal(NKCScenManager.GetScenManager().Get_SCEN_BASE().GetUILab().GetEnhanceItemSlotRect(eventTemplet.Value), eventTemplet, false);
				return;
			case NKCGameEventManager.GameEventType.TUTORIAL_TOUCH_SKILL_LEVELUP_ICON:
			{
				NKCUIUnitInfo openedUIByType4 = NKCUIManager.GetOpenedUIByType<NKCUIUnitInfo>();
				if (openedUIByType4 == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				RectTransform skillLevelSlotRect = openedUIByType4.GetSkillLevelSlotRect(eventTemplet.Value);
				if (skillLevelSlotRect == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.SetButtonClickSteal(skillLevelSlotRect, eventTemplet, false);
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_TOUCH_BACK_BUTTON:
				if (NKCUIManager.NKCUIUpsideMenu.btnBackButton == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.OpenTutorialGuideBySettedFace(NKCUIManager.NKCUIUpsideMenu.btnBackButton.GetComponent<RectTransform>(), NKCUIOverlayTutorialGuide.ClickGuideType.Touch, eventTemplet, null, false);
				NKCUIOverlayTutorialGuide.Instance.SetStealInput(delegate(BaseEventData x)
				{
					NKCUIOverlayCharMessage.CheckInstanceAndClose();
					NKCUIOverlayTutorialGuide.CheckInstanceAndClose();
					NKCUIManager.OnBackButton();
					if (!string.IsNullOrEmpty(NKCUIManager.NKCUIUpsideMenu.btnBackButton.m_SoundForPointClick))
					{
						NKCSoundManager.PlaySound(NKCUIManager.NKCUIUpsideMenu.btnBackButton.m_SoundForPointClick, 1f, 0f, 0f, false, 0f, false, 0f);
					}
					if (NKCGameEventManager.GetCurrentEventID() == eventTemplet.EventID)
					{
						NKCGameEventManager.ProcessEvent();
					}
				});
				return;
			case NKCGameEventManager.GameEventType.TUTORIAL_HIGHLIGHT_ACHIEVEMENT_SLOT:
			case NKCGameEventManager.GameEventType.TUTORIAL_CLICK_ACHIEVEMENT_SLOT:
			{
				if (!NKCUIMissionAchievement.IsInstanceOpen)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				RectTransform rectTransform = NKCUIMissionAchievement.Instance.GetRectTransformSlot(eventTemplet.Value);
				if (rectTransform == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				if (!string.IsNullOrEmpty(eventTemplet.StringValue))
				{
					Transform transform = rectTransform.Find(eventTemplet.StringValue);
					if (transform != null)
					{
						rectTransform = transform.GetComponent<RectTransform>();
					}
				}
				bool flag2 = NKCGameEventManager.IsEnableTouch(rectTransform);
				if (eventTemplet.EventType == NKCGameEventManager.GameEventType.TUTORIAL_CLICK_ACHIEVEMENT_SLOT && flag2)
				{
					NKCGameEventManager.SetButtonClickSteal(rectTransform, eventTemplet, false);
					return;
				}
				NKCGameEventManager.OpenTutorialGuideBySettedFace(rectTransform, NKCUIOverlayTutorialGuide.ClickGuideType.None, eventTemplet, new UnityAction(NKCGameEventManager.ProcessEvent), false);
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_HIGHLIGHT_COUNTERCASE:
			case NKCGameEventManager.GameEventType.TUTORIAL_CLICK_COUNTERCASE:
			{
				if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_OPERATION)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				RectTransform counterCaseSlot = NKCScenManager.GetScenManager().Get_SCEN_OPERATION().GetCounterCaseSlot(eventTemplet.Value);
				if (counterCaseSlot == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				if (eventTemplet.EventType == NKCGameEventManager.GameEventType.TUTORIAL_CLICK_COUNTERCASE)
				{
					NKCGameEventManager.SetButtonClickSteal(counterCaseSlot, eventTemplet, false);
					return;
				}
				NKCGameEventManager.OpenTutorialGuideBySettedFace(counterCaseSlot, NKCUIOverlayTutorialGuide.ClickGuideType.None, eventTemplet, new UnityAction(NKCGameEventManager.ProcessEvent), false);
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_HIGHLIGHT_COUNTERCASELIST:
			case NKCGameEventManager.GameEventType.TUTORIAL_CLICK_COUNTERCASELIST:
			{
				if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_OPERATION)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCUICCNormalSlot counterCaseListItem = NKCScenManager.GetScenManager().Get_SCEN_OPERATION().GetCounterCaseListItem(eventTemplet.Value);
				if (counterCaseListItem == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				RectTransform targetRect;
				if (!string.IsNullOrEmpty(eventTemplet.StringValue))
				{
					targetRect = counterCaseListItem.GetBtnRect();
				}
				else
				{
					targetRect = counterCaseListItem.GetComponent<RectTransform>();
				}
				if (eventTemplet.EventType == NKCGameEventManager.GameEventType.TUTORIAL_CLICK_COUNTERCASELIST)
				{
					NKCGameEventManager.SetButtonClickSteal(targetRect, eventTemplet, false);
					return;
				}
				NKCGameEventManager.OpenTutorialGuideBySettedFace(targetRect, NKCUIOverlayTutorialGuide.ClickGuideType.None, eventTemplet, new UnityAction(NKCGameEventManager.ProcessEvent), false);
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_HIGHLIGHT_FORGE_CRAFT_SLOT:
			case NKCGameEventManager.GameEventType.TUTORIAL_CLICK_FORGE_CRAFT_SLOT:
			{
				if (!NKCUIForgeCraft.IsInstanceOpen)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCUIForgeCraftSlot slot3 = NKCUIForgeCraft.Instance.GetSlot(eventTemplet.Value);
				if (slot3 == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				RectTransform targetRect2;
				if (!string.IsNullOrEmpty(eventTemplet.StringValue))
				{
					targetRect2 = slot3.GetButtonRect();
				}
				else
				{
					targetRect2 = slot3.GetComponent<RectTransform>();
				}
				if (eventTemplet.EventType == NKCGameEventManager.GameEventType.TUTORIAL_CLICK_FORGE_CRAFT_SLOT)
				{
					NKCGameEventManager.SetButtonClickSteal(targetRect2, eventTemplet, false);
					return;
				}
				NKCGameEventManager.OpenTutorialGuideBySettedFace(targetRect2, NKCUIOverlayTutorialGuide.ClickGuideType.None, eventTemplet, new UnityAction(NKCGameEventManager.ProcessEvent), false);
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_HIGHLIGHT_FORGE_CRAFT_MOLD:
			case NKCGameEventManager.GameEventType.TUTORIAL_CLICK_FORGE_CRAFT_MOLD:
			{
				if (!NKCUIForgeCraftMold.IsInstanceOpen)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCUIForgeCraftMoldSlot moldSlot = NKCUIForgeCraftMold.Instance.GetMoldSlot(eventTemplet.Value);
				if (moldSlot == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				RectTransform targetRect3;
				if (!string.IsNullOrEmpty(eventTemplet.StringValue))
				{
					targetRect3 = moldSlot.GetButtonRect();
				}
				else
				{
					targetRect3 = moldSlot.GetComponent<RectTransform>();
				}
				if (eventTemplet.EventType == NKCGameEventManager.GameEventType.TUTORIAL_CLICK_FORGE_CRAFT_MOLD)
				{
					NKCGameEventManager.SetButtonClickSteal(targetRect3, eventTemplet, false);
					return;
				}
				NKCGameEventManager.OpenTutorialGuideBySettedFace(targetRect3, NKCUIOverlayTutorialGuide.ClickGuideType.None, eventTemplet, new UnityAction(NKCGameEventManager.ProcessEvent), false);
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_HIGHLIGHT_HANGAR_BUILD_SLOT:
			case NKCGameEventManager.GameEventType.TUTORIAL_CLICK_HANGAR_BUILD_SLOT:
			{
				if (!NKCUIHangarBuild.IsInstanceOpen)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCUIHangarBuildSlot slot2 = NKCUIHangarBuild.Instance.GetSlot(eventTemplet.Value);
				if (slot2 == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				RectTransform rectTransform2 = null;
				if (!string.IsNullOrEmpty(eventTemplet.StringValue))
				{
					rectTransform2 = slot2.GetRect(eventTemplet.StringValue);
				}
				if (rectTransform2 == null)
				{
					rectTransform2 = slot2.GetComponent<RectTransform>();
				}
				if (eventTemplet.EventType == NKCGameEventManager.GameEventType.TUTORIAL_CLICK_HANGAR_BUILD_SLOT)
				{
					NKCGameEventManager.SetButtonClickSteal(rectTransform2, eventTemplet, false);
					return;
				}
				NKCGameEventManager.OpenTutorialGuideBySettedFace(rectTransform2, NKCUIOverlayTutorialGuide.ClickGuideType.None, eventTemplet, new UnityAction(NKCGameEventManager.ProcessEvent), false);
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_IMAGE_GUIDE:
				NKCUIPopupTutorialImagePanel.Instance.Open(eventTemplet.StringValue, new NKCUIPopupTutorialImagePanel.OnClose(NKCGameEventManager.ProcessEvent));
				return;
			case NKCGameEventManager.GameEventType.TUTORIAL_CONTRACT_FIND_BANNER:
				if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_CONTRACT)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				if (ContractTempletBase.Find(eventTemplet.StringValue) == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCScenManager.GetScenManager().GET_SCEN_CONTRACT().SelectRecruitBanner(eventTemplet.StringValue);
				NKCGameEventManager.ProcessEvent();
				return;
			case NKCGameEventManager.GameEventType.BEGIN_SUM_ITEMOPEN_RESULT:
				NKCGameEventManager.RandomBoxDataCollecting = true;
				NKCGameEventManager.lstCollectReward = new List<NKMRewardData>();
				NKCGameEventManager.ProcessEvent();
				return;
			case NKCGameEventManager.GameEventType.END_AND_SHOW_ITEMOPEN_RESULT:
				NKCUIResult.Instance.OpenBoxGain(NKCScenManager.CurrentUserData().m_ArmyData, NKCGameEventManager.lstCollectReward, eventTemplet.Value, new NKCUIResult.OnClose(NKCGameEventManager.ProcessEvent));
				NKCGameEventManager.lstCollectReward = null;
				NKCGameEventManager.RandomBoxDataCollecting = false;
				return;
			case NKCGameEventManager.GameEventType.OPEN_MISC_ITEM_RANDOM_BOX:
			{
				NKMItemMiscTemplet nkmitemMiscTemplet = NKMItemMiscTemplet.Find(eventTemplet.StringValue);
				if (nkmitemMiscTemplet == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				if (!nkmitemMiscTemplet.IsUsable())
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKMItemMiscData itemMisc = NKCScenManager.CurrentUserData().m_InventoryData.GetItemMisc(nkmitemMiscTemplet);
				if (itemMisc != null && itemMisc.TotalCount >= (long)eventTemplet.Value)
				{
					NKCPacketSender.Send_NKMPacket_RANDOM_ITEM_BOX_OPEN_REQ(nkmitemMiscTemplet.m_ItemMiscID, eventTemplet.Value);
					return;
				}
				NKCGameEventManager.ProcessEvent();
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_WORLDMAP_FIND_CITY:
			{
				if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_WORLDMAP)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKMWorldMapCityTemplet cityTemplet = NKMWorldMapManager.GetCityTemplet(eventTemplet.StringValue);
				if (cityTemplet == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.SetButtonClickSteal(NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().GetCityRect(cityTemplet.m_ID), eventTemplet, true);
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_WORLDMAP_FIND_CITY_LEVEL:
			{
				if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_WORLDMAP)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKMWorldMapData worldmapData = NKCScenManager.CurrentUserData().m_WorldmapData;
				if (worldmapData == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				Dictionary<int, NKMWorldMapCityData> worldMapCityDataMap = worldmapData.worldMapCityDataMap;
				if (worldMapCityDataMap == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				int num2 = -1;
				int value = eventTemplet.Value;
				foreach (KeyValuePair<int, NKMWorldMapCityData> keyValuePair in worldMapCityDataMap)
				{
					if (keyValuePair.Value.level >= value)
					{
						num2 = keyValuePair.Value.cityID;
						break;
					}
				}
				if (num2 < 0)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.SetButtonClickSteal(NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().GetCityRect(num2), eventTemplet, true);
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_TOUCH_WORLDMAP_BUILDING_EMPLTY:
			{
				if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_WORLDMAP)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				if (!NKCUIWorldMap.IsInstanceOpen)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				if (!NKCUIWorldMap.GetInstance().m_UICityManagement.IsOpen)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCUIWorldmapCityBuildPanel uicityBuilding = NKCUIWorldMap.GetInstance().m_UICityManagement.m_UICityBuilding;
				if (!uicityBuilding.gameObject.activeInHierarchy)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				RectTransform emptySlot = uicityBuilding.GetEmptySlot();
				if (emptySlot == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.SetButtonClickSteal(emptySlot, eventTemplet, false);
				return;
			}
			case NKCGameEventManager.GameEventType.TUTORIAL_HIGHLIGHT_WORLDMAP_BUILD_SLOT:
			{
				if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_WORLDMAP)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				if (!NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().IsOpenPopupWorldMapNewBuildingList)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCPopupWorldMapNewBuildingList popupWorldMapNewBuildingList = NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().PopupWorldMapNewBuildingList;
				if (popupWorldMapNewBuildingList == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				RectTransform buildingSlot = popupWorldMapNewBuildingList.GetBuildingSlot(eventTemplet.Value);
				if (buildingSlot == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.OpenTutorialGuideBySettedFace(buildingSlot, NKCUIOverlayTutorialGuide.ClickGuideType.None, eventTemplet, new UnityAction(NKCGameEventManager.ProcessEvent), false);
				return;
			}
			case NKCGameEventManager.GameEventType.RESET_WORLDMAP:
				if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_WORLDMAP)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				if (NKCUIWorldMap.IsInstanceOpen)
				{
					NKCUIWorldMap.GetInstance().CloseCityManagementUI();
				}
				NKCGameEventManager.ProcessEvent();
				return;
			case NKCGameEventManager.GameEventType.TUTORIAL_HIGHLIGHT_SHAODW_PALACE_SLOT:
			{
				if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_SHADOW_PALACE)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				if (!NKCUIShadowPalace.IsInstanceOpen)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				RectTransform palaceSlot = NKCUIShadowPalace.GetInstance().GetPalaceSlot(eventTemplet.Value);
				if (palaceSlot == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.OpenTutorialGuideBySettedFace(palaceSlot, NKCUIOverlayTutorialGuide.ClickGuideType.None, eventTemplet, new UnityAction(NKCGameEventManager.ProcessEvent), false);
				return;
			}
			case NKCGameEventManager.GameEventType.REFRESH_SCENE:
				NKCScenManager.GetScenManager().ScenChangeFade(NKCScenManager.GetScenManager().GetNowScenID(), true);
				NKCGameEventManager.ProcessEvent();
				return;
			case NKCGameEventManager.GameEventType.LOBBY_MENU_TAB:
				Debug.LogWarning("LOBBY_MENU_TAB : Obsolete Event!");
				NKCScenManager.GetScenManager().GetNowScenID();
				NKCGameEventManager.ProcessEvent();
				return;
			case NKCGameEventManager.GameEventType.BASE_MENU_TYPE:
			{
				if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_BASE)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCUIBaseSceneMenu.BaseSceneMenuType baseMenuType;
				if (Enum.TryParse<NKCUIBaseSceneMenu.BaseSceneMenuType>(eventTemplet.StringValue, out baseMenuType))
				{
					NKCScenManager.GetScenManager().Get_SCEN_BASE().SetBaseMenuType(baseMenuType);
				}
				NKCGameEventManager.ProcessEvent();
				return;
			}
			case NKCGameEventManager.GameEventType.PLAY_CUTSCENE:
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME)
				{
					if (!NKCGameEventManager.m_bIsPauseEvent)
					{
						Debug.LogError("CUTSCENE EVENT IN GAME PLAY MUST BE PAUSE EVENT!");
						NKCGameEventManager.ProcessEvent();
						return;
					}
					NKMUserData nkmuserData2 = NKCScenManager.CurrentUserData();
					NKCGameClient gameClient6 = NKCScenManager.GetScenManager().GetGameClient();
					if (gameClient6 != null && !NKCScenManager.CurrentUserData().m_UserOption.m_bPlayCutscene && nkmuserData2.CheckStageCleared(gameClient6.GetGameData()))
					{
						Debug.Log("Skipping cutscene..");
						NKCGameEventManager.ProcessEvent();
						return;
					}
				}
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME)
				{
					NKCScenManager.GetScenManager().GetGameClient().GetGameHud().HUD_HIDE(true);
					NKCUICutScenPlayer.Instance.LoadAndPlay(eventTemplet.StringValue, 0, new NKCUICutScenPlayer.CutScenCallBack(NKCGameEventManager.EndCutScene), false);
					return;
				}
				NKCUICutScenPlayer.Instance.LoadAndPlay(eventTemplet.StringValue, 0, new NKCUICutScenPlayer.CutScenCallBack(NKCGameEventManager.EndCutScene), true);
				return;
			case NKCGameEventManager.GameEventType.WAIT_SECONDS:
				NKCGameEventManager.m_fWaitTime = (float)eventTemplet.Value;
				NKCGameEventManager.OpenTutorialGuide(null, NKCUIOverlayTutorialGuide.ClickGuideType.None, "", null, false, NKCUIComRectScreen.ScreenExpand.None);
				return;
			case NKCGameEventManager.GameEventType.RESULT_GET_UNIT:
			{
				string[] array2 = eventTemplet.StringValue.Split(new char[]
				{
					',',
					' '
				});
				List<NKMUnitData> list3 = new List<NKMUnitData>();
				string[] array = array2;
				for (int i = 0; i < array.Length; i++)
				{
					int num3;
					if (int.TryParse(array[i], out num3) && NKMUnitManager.GetUnitTempletBase(num3) != null)
					{
						NKMUnitData item = new NKMUnitData(num3, 0L, false, false, false, false);
						list3.Add(item);
					}
				}
				if (list3.Count == 0)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKMRewardData nkmrewardData = new NKMRewardData();
				nkmrewardData.SetUnitData(list3);
				NKCUIResult.Instance.OpenRewardGain(NKCScenManager.CurrentUserData().m_ArmyData, nkmrewardData, NKCUtilString.GET_STRING_GET_UNIT, "", new NKCUIResult.OnClose(NKCGameEventManager.WaitFinished));
				NKCGameEventManager.ProcessEvent();
				return;
			}
			case NKCGameEventManager.GameEventType.OPEN_NICKNAME_CHANGE_POPUP:
				NKCPopupNickname.Instance.Open(new NKCPopupNickname.OnButton(NKCGameEventManager.ProcessEvent));
				return;
			case NKCGameEventManager.GameEventType.MOVE_MINIMAP:
				if (!NKCUIOfficeMapFront.IsInstanceOpen)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.OpenTutorialGuide(null, NKCUIOverlayTutorialGuide.ClickGuideType.None, "", null, false, NKCUIComRectScreen.ScreenExpand.None);
				NKCUIOverlayTutorialGuide.Instance.SetBGScreenAlpha(0.01f);
				NKCUIOfficeMapFront.GetInstance().MoveMiniMap((float)eventTemplet.Value / 100f, new UnityAction(NKCGameEventManager.ProcessEvent));
				return;
			case NKCGameEventManager.GameEventType.OFFICE_HIGHLIGHT:
			{
				if (!NKCUIOfficeMapFront.IsInstanceOpen)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				IOfficeMinimap currentMinimap = NKCUIOfficeMapFront.GetInstance().GetCurrentMinimap();
				if (currentMinimap == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				RectTransform tileRectTransform = currentMinimap.GetTileRectTransform(eventTemplet.Value);
				if (tileRectTransform == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.OpenTutorialGuideBySettedFace(tileRectTransform, NKCUIOverlayTutorialGuide.ClickGuideType.None, eventTemplet, new UnityAction(NKCGameEventManager.ProcessEvent), false);
				return;
			}
			case NKCGameEventManager.GameEventType.OFFICE_TOUCH:
			{
				if (!NKCUIOfficeMapFront.IsInstanceOpen)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				IOfficeMinimap currentMinimap2 = NKCUIOfficeMapFront.GetInstance().GetCurrentMinimap();
				if (currentMinimap2 == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				RectTransform tileRectTransform2 = currentMinimap2.GetTileRectTransform(eventTemplet.Value);
				if (tileRectTransform2 == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.OpenTutorialGuideBySettedFace(tileRectTransform2, NKCUIOverlayTutorialGuide.ClickGuideType.Touch, eventTemplet, new UnityAction(NKCGameEventManager.ProcessEvent), false);
				return;
			}
			case NKCGameEventManager.GameEventType.OFFICE_UNITLIST_UNIT:
			{
				if (!NKCUIPopupOfficeMemberEdit.IsInstanceOpen)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCUIPopupOfficeMemberEdit.Instance.SortSpecifitUnitFirst(eventTemplet.Value);
				RectTransform rectTransformUnitSlot = NKCUIPopupOfficeMemberEdit.Instance.GetRectTransformUnitSlot(eventTemplet.Value);
				if (rectTransformUnitSlot == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.SetButtonClickSteal(rectTransformUnitSlot, eventTemplet, false);
				return;
			}
			case NKCGameEventManager.GameEventType.OFFICE_ITEMLIST_FURNITURE:
			{
				if (!NKCUIPopupOfficeInteriorSelect.IsInstanceOpen)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				RectTransform tutorialItemSlot = NKCUIPopupOfficeInteriorSelect.Instance.GetTutorialItemSlot(eventTemplet.Value);
				if (tutorialItemSlot == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.SetButtonClickSteal(tutorialItemSlot, eventTemplet, false);
				return;
			}
			case NKCGameEventManager.GameEventType.OFFICE_FURNITURE_HIGHLIGHT:
			{
				GameObject gameObject4 = NKCGameEventManager.FindGameObject(eventTemplet.StringValue);
				if (gameObject4 == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCOfficeFuniture component2 = gameObject4.GetComponent<NKCOfficeFuniture>();
				if (component2 == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				RectTransform rectTransform3 = component2.MakeHighlightRect();
				Vector3 centerWorldPos = rectTransform3.GetCenterWorldPos();
				NKCCamera.SetPos(centerWorldPos.x, centerWorldPos.y, -1f, true, false);
				NKCGameEventManager.OpenTutorialGuideBySettedFace(rectTransform3, NKCUIOverlayTutorialGuide.ClickGuideType.None, eventTemplet, new UnityAction(NKCGameEventManager.ProcessEvent), true);
				return;
			}
			case NKCGameEventManager.GameEventType.OFFICE_FURNITURE_TOUCH:
			{
				GameObject gameObject5 = NKCGameEventManager.FindGameObject(eventTemplet.StringValue);
				if (gameObject5 == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCOfficeFuniture component3 = gameObject5.GetComponent<NKCOfficeFuniture>();
				if (component3 == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				Vector3 centerWorldPos2 = component3.MakeHighlightRect().GetCenterWorldPos();
				NKCCamera.SetPos(centerWorldPos2.x, centerWorldPos2.y, -1f, true, false);
				NKCGameEventManager.SetFurnitureButtonSteal(component3, eventTemplet);
				return;
			}
			case NKCGameEventManager.GameEventType.REARM_UNITLIST_UNIT:
			{
				if (!NKCUIRearmament.IsInstanceOpen)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				RectTransform rearmSlotRectTransform = NKCUIRearmament.Instance.GetRearmSlotRectTransform(eventTemplet.Value);
				if (rearmSlotRectTransform == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.SetButtonClickSteal(rearmSlotRectTransform, eventTemplet, false);
				return;
			}
			case NKCGameEventManager.GameEventType.EXTRACT_SLOT_SELECT:
			{
				if (!NKCUIRearmament.IsInstanceOpen)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				RectTransform extractSlotRectTransform = NKCUIRearmament.Instance.GetExtractSlotRectTransform(eventTemplet.Value);
				if (extractSlotRectTransform == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.SetButtonClickSteal(extractSlotRectTransform, eventTemplet, false);
				return;
			}
			case NKCGameEventManager.GameEventType.EXTRACT_UNITLIST_UNIT:
			{
				if (!NKCUIRearmament.IsInstanceOpen || !NKCUIUnitSelectList.IsInstanceOpen)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				RectTransform extractSlotRectTransform2 = NKCUIRearmament.Instance.GetExtractSlotRectTransform(eventTemplet.Value);
				if (extractSlotRectTransform2 == null)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.SetButtonClickSteal(extractSlotRectTransform2, eventTemplet, false);
				return;
			}
			case NKCGameEventManager.GameEventType.TOGGLE_UI_CANVAS:
			{
				NKCUIManager.eUIBaseRect type7;
				if (!Enum.TryParse<NKCUIManager.eUIBaseRect>(eventTemplet.StringValue, out type7))
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCUtil.SetGameobjectActive(NKCUIManager.GetUIBaseRect(type7), eventTemplet.Value != 0);
				NKCGameEventManager.ProcessEvent();
				return;
			}
			case NKCGameEventManager.GameEventType.PLAY_MUSIC:
			{
				float num4 = (float)eventTemplet.Value / 100f;
				if (num4 < 0f)
				{
					num4 = NKCSoundManager.GetMusicTime();
				}
				NKCSoundManager.PlayMusic(eventTemplet.StringValue, true, 1f, true, num4, 0f);
				NKCGameEventManager.ProcessEvent();
				return;
			}
			default:
				Debug.LogWarning("Not Implemented yet");
				NKCGameEventManager.ProcessEvent();
				break;
			}
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x00116BE0 File Offset: 0x00114DE0
		private static void SetButtonClickSteal(RectTransform targetRect, NKCGameEventManager.NKCGameEventTemplet eventTemplet, bool bMiddleCanvas = false)
		{
			if (targetRect == null)
			{
				NKCGameEventManager.ProcessEvent();
				return;
			}
			NKCUIComButton comButton = targetRect.GetComponentInChildren<NKCUIComButton>();
			NKCUIComStateButton stateButton = targetRect.GetComponentInChildren<NKCUIComStateButton>();
			NKCUIComToggle comToggle = targetRect.GetComponentInChildren<NKCUIComToggle>();
			NKCGameEventManager.OpenTutorialGuideBySettedFace(targetRect, NKCUIOverlayTutorialGuide.ClickGuideType.Touch, eventTemplet, null, bMiddleCanvas);
			NKCUIOverlayTutorialGuide.Instance.SetStealInput(delegate(BaseEventData x)
			{
				if (comButton != null && comButton.enabled)
				{
					UnityEvent pointerClick = comButton.PointerClick;
					if (pointerClick != null)
					{
						pointerClick.Invoke();
					}
					if (!string.IsNullOrEmpty(comButton.m_SoundForPointClick))
					{
						NKCSoundManager.PlaySound(comButton.m_SoundForPointClick, 1f, 0f, 0f, false, 0f, false, 0f);
					}
				}
				if (stateButton != null && stateButton.enabled)
				{
					UnityEvent pointerClick2 = stateButton.PointerClick;
					if (pointerClick2 != null)
					{
						pointerClick2.Invoke();
					}
					if (!string.IsNullOrEmpty(stateButton.m_SoundForPointClick))
					{
						NKCSoundManager.PlaySound(stateButton.m_SoundForPointClick, 1f, 0f, 0f, false, 0f, false, 0f);
					}
				}
				if (comToggle != null && comToggle.enabled)
				{
					comToggle.Select(!comToggle.m_bChecked, false, false);
					if (!string.IsNullOrEmpty(comToggle.m_SoundForPointClick))
					{
						NKCSoundManager.PlaySound(comToggle.m_SoundForPointClick, 1f, 0f, 0f, false, 0f, false, 0f);
					}
				}
				if (NKCGameEventManager.GetCurrentEventID() == eventTemplet.EventID)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.FinishEventTemplet(eventTemplet);
			});
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x00116C54 File Offset: 0x00114E54
		private static void SetFurnitureButtonSteal(NKCOfficeFuniture furniture, NKCGameEventManager.NKCGameEventTemplet eventTemplet)
		{
			if (furniture == null)
			{
				NKCGameEventManager.ProcessEvent();
				return;
			}
			NKCGameEventManager.OpenTutorialGuideBySettedFace(furniture.MakeHighlightRect(), NKCUIOverlayTutorialGuide.ClickGuideType.Touch, eventTemplet, null, true);
			NKCUIOverlayTutorialGuide.Instance.SetStealInput(delegate(BaseEventData x)
			{
				furniture.InvokeTouchEvent();
				if (NKCGameEventManager.GetCurrentEventID() == eventTemplet.EventID)
				{
					NKCGameEventManager.ProcessEvent();
					return;
				}
				NKCGameEventManager.FinishEventTemplet(eventTemplet);
			});
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x00116CB8 File Offset: 0x00114EB8
		public static void OpenTutorialGuideBySettedFace(RectTransform targetRect, NKCUIOverlayTutorialGuide.ClickGuideType guideType, NKCGameEventManager.NKCGameEventTemplet eventTemplet, UnityAction onComplete, bool bMiddleCanvas = false)
		{
			if (!string.IsNullOrWhiteSpace(NKCGameEventManager.m_strCurrentTalkInvenIcon))
			{
				NKCGameEventManager.OpenTutorialGuide(targetRect.GetComponent<RectTransform>(), guideType, "", onComplete, bMiddleCanvas, NKCUIComRectScreen.ScreenExpand.None);
				if (!string.IsNullOrWhiteSpace(eventTemplet.Text))
				{
					NKCUIOverlayCharMessage.Instance.Open(NKCGameEventManager.m_strCurrentTalkInvenIcon, eventTemplet.Text, 9999f, null, false);
					NKCUIOverlayCharMessage.Instance.SetBGScreenAlpha(NKCGameEventManager.m_fGuideScreenAlpha);
					return;
				}
			}
			else
			{
				NKCGameEventManager.OpenTutorialGuide(targetRect.GetComponent<RectTransform>(), guideType, eventTemplet.Text, onComplete, bMiddleCanvas, NKCUIComRectScreen.ScreenExpand.None);
			}
		}

		// Token: 0x060035D0 RID: 13776 RVA: 0x00116D35 File Offset: 0x00114F35
		private static void FinishEventTemplet(NKCGameEventManager.NKCGameEventTemplet eventTemplet)
		{
			if (eventTemplet == null)
			{
				return;
			}
			NKCUIOverlayTutorialGuide.CheckInstanceAndClose();
			NKCUIOverlayCharMessage.CheckInstanceAndClose();
			NKCUIPopupTutorialImagePanel.CheckInstanceAndClose();
			if (eventTemplet.EventType == NKCGameEventManager.GameEventType.PLAY_CUTSCENE)
			{
				NKCUICutScenPlayer.CheckInstanceAndClose();
				NKCTutorialManager.TutorialRequiredByLastPoint();
			}
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x00116D5E File Offset: 0x00114F5E
		private static bool IsEnableTouch(RectTransform rect)
		{
			return rect.GetComponent<NKCUIComButton>() || rect.GetComponent<NKCUIComStateButton>() || rect.GetComponent<NKCUIComToggle>();
		}

		// Token: 0x060035D2 RID: 13778 RVA: 0x00116D88 File Offset: 0x00114F88
		private static GameObject FindGameObject(string name)
		{
			UnityEngine.Object[] array = Resources.FindObjectsOfTypeAll(typeof(GameObject));
			if (array == null || array.Length == 0)
			{
				return null;
			}
			List<UnityEngine.Object> list = array.ToList<UnityEngine.Object>().FindAll((UnityEngine.Object v) => v.name == name);
			if (list == null || list.Count == 0)
			{
				return null;
			}
			if (list.Count > 1)
			{
				for (int i = 0; i < list.Count; i++)
				{
					GameObject gameObject = list[i] as GameObject;
					if (gameObject.activeInHierarchy)
					{
						return gameObject;
					}
				}
			}
			return list[0] as GameObject;
		}

		// Token: 0x060035D3 RID: 13779 RVA: 0x00116E20 File Offset: 0x00115020
		public static void ClearEvent()
		{
			if (NKCGameEventManager.m_lstCurrentEvent == null)
			{
				return;
			}
			if (NKCGameEventManager.m_CurrentIndex >= 0)
			{
				NKCGameEventManager.FinishEventTemplet(NKCGameEventManager.m_lstCurrentEvent[NKCGameEventManager.m_CurrentIndex]);
			}
			NKCGameEventManager.m_lstCurrentEvent = null;
			NKCGameEventManager.m_CurrentIndex = -1;
			NKCGameEventManager.OnEventFinish onEventFinish = NKCGameEventManager.dOnEventFinish;
			if (onEventFinish != null)
			{
				onEventFinish(NKCGameEventManager.m_bIsPauseEvent);
			}
			NKCGameEventManager.dOnEventFinish = null;
		}

		// Token: 0x0400334E RID: 13134
		private static NKCGameEventManager.OnEventFinish dOnEventFinish;

		// Token: 0x0400334F RID: 13135
		private static Dictionary<int, List<NKCGameEventManager.NKCGameEventTemplet>> m_dicGameEventTemplet;

		// Token: 0x04003350 RID: 13136
		private static Dictionary<int, List<NKCGameEventManager.NKCGameEventTemplet>> m_dicTutorialEventTemplet;

		// Token: 0x04003351 RID: 13137
		private static List<NKCGameEventManager.NKCGameEventTemplet> m_lstCurrentEvent;

		// Token: 0x04003352 RID: 13138
		private static int m_CurrentIndex;

		// Token: 0x04003353 RID: 13139
		private static bool m_bIsPauseEvent;

		// Token: 0x04003354 RID: 13140
		private static string m_strCurrentTalkInvenIcon;

		// Token: 0x04003355 RID: 13141
		private const float DEFAULT_GUIDE_SCREEN_ALPHA = 0.85f;

		// Token: 0x04003356 RID: 13142
		private static float m_fGuideScreenAlpha = 0.85f;

		// Token: 0x04003358 RID: 13144
		private static List<NKMRewardData> lstCollectReward;

		// Token: 0x04003359 RID: 13145
		private static float m_fWaitTime = 0f;

		// Token: 0x02001325 RID: 4901
		public enum GameEventType
		{
			// Token: 0x04009884 RID: 39044
			INVALID,
			// Token: 0x04009885 RID: 39045
			HIGHLIGHT_UI,
			// Token: 0x04009886 RID: 39046
			TEXT,
			// Token: 0x04009887 RID: 39047
			MESSAGE_BOX,
			// Token: 0x04009888 RID: 39048
			MOVE_CAMERA,
			// Token: 0x04009889 RID: 39049
			TUTORIAL_MARK_COMPLETE,
			// Token: 0x0400988A RID: 39050
			TUTORIAL_NEXT,
			// Token: 0x0400988B RID: 39051
			WAIT,
			// Token: 0x0400988C RID: 39052
			TUTORIAL_UNIT_SKILL_GUIDE,
			// Token: 0x0400988D RID: 39053
			TUTORIAL_UNIT_SUMMON_GUIDE,
			// Token: 0x0400988E RID: 39054
			TUTORIAL_UNIT_RE_SUMMON_GUIDE,
			// Token: 0x0400988F RID: 39055
			TUTORIAL_UNIT_HYPER_GUIDE,
			// Token: 0x04009890 RID: 39056
			TUTORIAL_CLICK_UNIT_HYPER,
			// Token: 0x04009891 RID: 39057
			TUTORIAL_HIGHLIGHT_DECK,
			// Token: 0x04009892 RID: 39058
			TUTORIAL_HIGHLIGHT_UNIT,
			// Token: 0x04009893 RID: 39059
			TUTORIAL_SHIP_SKILL_GUIDE,
			// Token: 0x04009894 RID: 39060
			TUTORIAL_UNLOCK_SHIP_SKILL,
			// Token: 0x04009895 RID: 39061
			TUTORIAL_HIGHLIGHT_SHIP_SKILL,
			// Token: 0x04009896 RID: 39062
			UNLOCK_TUTORIAL_GAME_RE_RESPAWN,
			// Token: 0x04009897 RID: 39063
			TUTORIAL_UNLOCK_DECK,
			// Token: 0x04009898 RID: 39064
			TUTORIAL_UNIT_DEPLOY_AREA_GUIDE,
			// Token: 0x04009899 RID: 39065
			TUTORIAL_SET_TALKER,
			// Token: 0x0400989A RID: 39066
			TUTORIAL_SET_SCREEN_BG_ALPHA,
			// Token: 0x0400989B RID: 39067
			TUTORIAL_TOUCH_UI,
			// Token: 0x0400989C RID: 39068
			TUTORIAL_HIGHLIGHT_MAINSTREAM,
			// Token: 0x0400989D RID: 39069
			TUTORIAL_HIGHLIGHT_EPISODE,
			// Token: 0x0400989E RID: 39070
			TUTORIAL_TOUCH_DAILY,
			// Token: 0x0400989F RID: 39071
			TUTORIAL_TOUCH_STAGE,
			// Token: 0x040098A0 RID: 39072
			TUTORIAL_TOUCH_ACT,
			// Token: 0x040098A1 RID: 39073
			TUTORIAL_UNLOCK_DECK_BUTTON,
			// Token: 0x040098A2 RID: 39074
			TUTORIAL_UNLOCK_DECK_BUTTON_LAST,
			// Token: 0x040098A3 RID: 39075
			TUTORIAL_SELECT_DECKVIEWER_DECK,
			// Token: 0x040098A4 RID: 39076
			TUTORIAL_SELECT_DECKVIEWER_SHIP,
			// Token: 0x040098A5 RID: 39077
			TUTORIAL_HIGHLIGHT_DECKVIEWER_DECK,
			// Token: 0x040098A6 RID: 39078
			TUTORIAL_SELECT_DECKVIEWERLIST_UNIT,
			// Token: 0x040098A7 RID: 39079
			TUTORIAL_SELECT_DECKVIEWERLIST_SHIP,
			// Token: 0x040098A8 RID: 39080
			TUTORIAL_SELECT_DECKVIEWERLIST_SLOTTYPE,
			// Token: 0x040098A9 RID: 39081
			TUTORIAL_CLICK_WARFARE_UNIT,
			// Token: 0x040098AA RID: 39082
			TUTORIAL_CLICK_WARFARE_TILE,
			// Token: 0x040098AB RID: 39083
			TUTORIAL_HIGHLIGHT_WARFARE_TILE,
			// Token: 0x040098AC RID: 39084
			TUTORIAL_WARFARE_AUTO,
			// Token: 0x040098AD RID: 39085
			TUTORIAL_SELECT_UNITLIST_UNIT,
			// Token: 0x040098AE RID: 39086
			TUTORIAL_SELECT_UNITLIST_SHIP,
			// Token: 0x040098AF RID: 39087
			TUTORIAL_SELECT_ITEMLIST_EQUIP,
			// Token: 0x040098B0 RID: 39088
			TUTORIAL_AUTO_GUIDE,
			// Token: 0x040098B1 RID: 39089
			TUTORIAL_TOUCH_ENHANCE_USE_SLOT,
			// Token: 0x040098B2 RID: 39090
			TUTORIAL_TOUCH_SKILL_LEVELUP_ICON,
			// Token: 0x040098B3 RID: 39091
			TUTORIAL_TOUCH_BACK_BUTTON,
			// Token: 0x040098B4 RID: 39092
			TUTORIAL_HIGHLIGHT_ACHIEVEMENT_SLOT,
			// Token: 0x040098B5 RID: 39093
			TUTORIAL_CLICK_ACHIEVEMENT_SLOT,
			// Token: 0x040098B6 RID: 39094
			TUTORIAL_HIGHLIGHT_COUNTERCASE,
			// Token: 0x040098B7 RID: 39095
			TUTORIAL_CLICK_COUNTERCASE,
			// Token: 0x040098B8 RID: 39096
			TUTORIAL_HIGHLIGHT_COUNTERCASELIST,
			// Token: 0x040098B9 RID: 39097
			TUTORIAL_CLICK_COUNTERCASELIST,
			// Token: 0x040098BA RID: 39098
			TUTORIAL_HIGHLIGHT_FORGE_CRAFT_SLOT,
			// Token: 0x040098BB RID: 39099
			TUTORIAL_CLICK_FORGE_CRAFT_SLOT,
			// Token: 0x040098BC RID: 39100
			TUTORIAL_HIGHLIGHT_FORGE_CRAFT_MOLD,
			// Token: 0x040098BD RID: 39101
			TUTORIAL_CLICK_FORGE_CRAFT_MOLD,
			// Token: 0x040098BE RID: 39102
			TUTORIAL_HIGHLIGHT_HANGAR_BUILD_SLOT,
			// Token: 0x040098BF RID: 39103
			TUTORIAL_CLICK_HANGAR_BUILD_SLOT,
			// Token: 0x040098C0 RID: 39104
			TUTORIAL_IMAGE_GUIDE,
			// Token: 0x040098C1 RID: 39105
			TUTORIAL_CONTRACT_FIND_BANNER,
			// Token: 0x040098C2 RID: 39106
			BEGIN_SUM_ITEMOPEN_RESULT,
			// Token: 0x040098C3 RID: 39107
			END_AND_SHOW_ITEMOPEN_RESULT,
			// Token: 0x040098C4 RID: 39108
			OPEN_MISC_ITEM_RANDOM_BOX,
			// Token: 0x040098C5 RID: 39109
			TUTORIAL_WORLDMAP_FIND_CITY,
			// Token: 0x040098C6 RID: 39110
			TUTORIAL_WORLDMAP_FIND_CITY_LEVEL,
			// Token: 0x040098C7 RID: 39111
			TUTORIAL_TOUCH_WORLDMAP_BUILDING_EMPLTY,
			// Token: 0x040098C8 RID: 39112
			TUTORIAL_HIGHLIGHT_WORLDMAP_BUILD_SLOT,
			// Token: 0x040098C9 RID: 39113
			RESET_WORLDMAP,
			// Token: 0x040098CA RID: 39114
			TUTORIAL_HIGHLIGHT_SHAODW_PALACE_SLOT,
			// Token: 0x040098CB RID: 39115
			REFRESH_SCENE,
			// Token: 0x040098CC RID: 39116
			LOBBY_MENU_TAB,
			// Token: 0x040098CD RID: 39117
			BASE_MENU_TYPE,
			// Token: 0x040098CE RID: 39118
			PLAY_CUTSCENE,
			// Token: 0x040098CF RID: 39119
			WAIT_SECONDS,
			// Token: 0x040098D0 RID: 39120
			RESULT_GET_UNIT,
			// Token: 0x040098D1 RID: 39121
			OPEN_NICKNAME_CHANGE_POPUP,
			// Token: 0x040098D2 RID: 39122
			MOVE_MINIMAP,
			// Token: 0x040098D3 RID: 39123
			OFFICE_HIGHLIGHT,
			// Token: 0x040098D4 RID: 39124
			OFFICE_TOUCH,
			// Token: 0x040098D5 RID: 39125
			OFFICE_UNITLIST_UNIT,
			// Token: 0x040098D6 RID: 39126
			OFFICE_ITEMLIST_FURNITURE,
			// Token: 0x040098D7 RID: 39127
			OFFICE_FURNITURE_HIGHLIGHT,
			// Token: 0x040098D8 RID: 39128
			OFFICE_FURNITURE_TOUCH,
			// Token: 0x040098D9 RID: 39129
			REARM_UNITLIST_UNIT,
			// Token: 0x040098DA RID: 39130
			EXTRACT_SLOT_SELECT,
			// Token: 0x040098DB RID: 39131
			EXTRACT_UNITLIST_UNIT,
			// Token: 0x040098DC RID: 39132
			TOGGLE_UI_CANVAS,
			// Token: 0x040098DD RID: 39133
			PLAY_MUSIC
		}

		// Token: 0x02001326 RID: 4902
		public enum TextBoxPosType
		{
			// Token: 0x040098DF RID: 39135
			DEFAULT,
			// Token: 0x040098E0 RID: 39136
			CENTERUP,
			// Token: 0x040098E1 RID: 39137
			CENTER,
			// Token: 0x040098E2 RID: 39138
			CENTERDOWN,
			// Token: 0x040098E3 RID: 39139
			RIGHTUP,
			// Token: 0x040098E4 RID: 39140
			RIGHTDOWN,
			// Token: 0x040098E5 RID: 39141
			LEFTUP,
			// Token: 0x040098E6 RID: 39142
			LEFTDOWN
		}

		// Token: 0x02001327 RID: 4903
		public class NKCGameEventTemplet
		{
			// Token: 0x0600A52F RID: 42287 RVA: 0x00345024 File Offset: 0x00343224
			public bool LoadFromLUA(NKMLua cNKMLua)
			{
				if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCGameEventManager.cs", 198))
				{
					return false;
				}
				string nationalPostfix = NKCStringTable.GetNationalPostfix(NKCStringTable.GetNationalCode());
				bool result = true & cNKMLua.GetData("EventID", ref this.EventID) & cNKMLua.GetData<NKCGameEventManager.GameEventType>("EventType", ref this.EventType);
				cNKMLua.GetData("Text" + nationalPostfix, ref this.Text);
				cNKMLua.GetData("Value", ref this.Value);
				cNKMLua.GetData("StringValue", ref this.StringValue);
				return result;
			}

			// Token: 0x040098E7 RID: 39143
			public int EventID;

			// Token: 0x040098E8 RID: 39144
			public NKCGameEventManager.GameEventType EventType;

			// Token: 0x040098E9 RID: 39145
			public string Text = "";

			// Token: 0x040098EA RID: 39146
			public int Value;

			// Token: 0x040098EB RID: 39147
			public string StringValue = "";
		}

		// Token: 0x02001328 RID: 4904
		// (Invoke) Token: 0x0600A532 RID: 42290
		public delegate void OnEventFinish(bool bUnpause);
	}
}
