using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ClientPacket.Event;
using Cs.Logging;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BCA RID: 3018
	public class NKCPopupEventRace : NKCUIBase
	{
		// Token: 0x17001653 RID: 5715
		// (get) Token: 0x06008BB0 RID: 35760 RVA: 0x002F7AA0 File Offset: 0x002F5CA0
		public static NKCPopupEventRace Instance
		{
			get
			{
				if (NKCPopupEventRace.m_Instance == null)
				{
					NKCPopupEventRace.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupEventRace>("AB_UI_NKM_UI_EVENT_PF_RACE", "NKM_UI_EVENT_RACE_HUD", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupEventRace.CleanupInstance)).GetInstance<NKCPopupEventRace>();
					NKCPopupEventRace.m_Instance.InitUI();
				}
				return NKCPopupEventRace.m_Instance;
			}
		}

		// Token: 0x06008BB1 RID: 35761 RVA: 0x002F7AEF File Offset: 0x002F5CEF
		private static void CleanupInstance()
		{
			NKCPopupEventRace.m_Instance = null;
		}

		// Token: 0x17001654 RID: 5716
		// (get) Token: 0x06008BB2 RID: 35762 RVA: 0x002F7AF7 File Offset: 0x002F5CF7
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupEventRace.m_Instance != null && NKCPopupEventRace.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008BB3 RID: 35763 RVA: 0x002F7B12 File Offset: 0x002F5D12
		private void OnDestroy()
		{
			NKCPopupEventRace.m_Instance = null;
		}

		// Token: 0x17001655 RID: 5717
		// (get) Token: 0x06008BB4 RID: 35764 RVA: 0x002F7B1A File Offset: 0x002F5D1A
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001656 RID: 5718
		// (get) Token: 0x06008BB5 RID: 35765 RVA: 0x002F7B1D File Offset: 0x002F5D1D
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06008BB6 RID: 35766 RVA: 0x002F7B24 File Offset: 0x002F5D24
		public void InitUI()
		{
			this.m_btnSkip.PointerClick.RemoveAllListeners();
			this.m_btnSkip.PointerClick.AddListener(new UnityAction(this.OnClickSkip));
			this.m_btnTest.PointerClick.RemoveAllListeners();
			this.m_btnTest.PointerClick.AddListener(new UnityAction(this.ResetPosition));
			this.m_btnAdd.PointerClick.RemoveAllListeners();
			this.m_btnAdd.PointerClick.AddListener(new UnityAction(this.OnClickAddAnimationEvent));
			this.m_btnSelectLine1.PointerClick.RemoveAllListeners();
			this.m_btnSelectLine1.PointerClick.AddListener(new UnityAction(this.OnClickSelectLine1));
			this.m_btnSelectLine2.PointerClick.RemoveAllListeners();
			this.m_btnSelectLine2.PointerClick.AddListener(new UnityAction(this.OnClickSelectLine2));
		}

		// Token: 0x06008BB7 RID: 35767 RVA: 0x002F7C10 File Offset: 0x002F5E10
		public void Open(RaceTeam selectedTeam, string teamA_SDUnitName, string teamB_SDUnitName)
		{
			this.ResetPosition();
			if (!NKCEventRaceAnimationManager.DataExist)
			{
				NKCEventRaceAnimationManager.LoadFromLua();
			}
			if (!NKCAnimationEventManager.DataExist)
			{
				NKCAnimationEventManager.LoadFromLua();
			}
			NKCUtil.SetGameobjectActive(this.m_objTestParent, NKCDefineManager.DEFINE_USE_CHEAT() && NKCScenManager.CurrentUserData().IsSuperUser());
			this.m_SelectedTeam = selectedTeam;
			this.m_teamA_SDUnitName = teamA_SDUnitName;
			this.m_teamB_SDUnitName = teamB_SDUnitName;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCSoundManager.StopAllSound();
			NKCSoundManager.StopMusic();
			NKCSoundManager.PlayMusic("UI_SPORT_RACE_02", true, 1f, false, 0f, 0f);
			this.m_AnimatorIntro.Play("NKM_UI_EVENT_RACE_HUD_INTRO_INTRO");
			base.UIOpened(true);
		}

		// Token: 0x06008BB8 RID: 35768 RVA: 0x002F7CBC File Offset: 0x002F5EBC
		private void ResetData()
		{
			this.m_bUpdate = false;
			this.m_bFirstGoal = true;
			this.m_bWaitForPacket = false;
			foreach (KeyValuePair<NKCASUIUnitIllust, Queue<List<NKCPopupEventRace.NKCRaceEventData>>> keyValuePair in this.m_dicRaceEventSet)
			{
				if (keyValuePair.Value != null && keyValuePair.Value.Count > 0)
				{
					List<NKCPopupEventRace.NKCRaceEventData> list = keyValuePair.Value.Dequeue();
					for (int i = 0; i < list.Count; i++)
					{
						list[i].m_AniSet.Close();
					}
				}
			}
			this.m_dicRaceEventSet.Clear();
			for (int j = 0; j < this.m_lstUsedEventData.Count; j++)
			{
				this.m_lstUsedEventData[j].m_AniSet.Close();
			}
			this.m_lstUsedEventData.Clear();
			this.m_queueDataWin.Clear();
			this.m_queueDataLose.Clear();
			this.m_AnimationActor1.transform.localPosition = Vector3.zero;
			this.m_AnimationActor2.transform.localPosition = Vector3.zero;
		}

		// Token: 0x06008BB9 RID: 35769 RVA: 0x002F7DEC File Offset: 0x002F5FEC
		public override void OnBackButton()
		{
		}

		// Token: 0x06008BBA RID: 35770 RVA: 0x002F7DEE File Offset: 0x002F5FEE
		public override void CloseInternal()
		{
			this.ResetData();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			NKCSoundManager.StopAllSound();
			NKCSoundManager.StopMusic();
			NKCSoundManager.PlayScenMusic(NKCScenManager.GetScenManager().GetNowScenID(), false);
		}

		// Token: 0x06008BBB RID: 35771 RVA: 0x002F7E1C File Offset: 0x002F601C
		private void Update()
		{
			if (!this.m_bUpdate)
			{
				return;
			}
			if (this.m_dicRaceEventSet.Count > 0)
			{
				foreach (KeyValuePair<NKCASUIUnitIllust, Queue<List<NKCPopupEventRace.NKCRaceEventData>>> keyValuePair in this.m_dicRaceEventSet)
				{
					Queue<List<NKCPopupEventRace.NKCRaceEventData>> value = keyValuePair.Value;
					if (value.Count != 0)
					{
						if (keyValuePair.Key == this.m_MyActor.GetSpineIllust())
						{
							this.m_trMoveObjParent.localPosition = new Vector3(-Mathf.Lerp(-this.m_trMoveObjParent.localPosition.x, this.m_MyActor.transform.localPosition.x, this.m_lerpValue * Time.deltaTime), this.m_trMoveObjParent.localPosition.y, this.m_trMoveObjParent.localPosition.z);
						}
						this.m_objSelectMark.transform.position = this.m_MyActor.m_trSDParent.position + new Vector3(0f, this.m_fArrowPosY, 0f);
						this.CheckBubble();
						List<NKCPopupEventRace.NKCRaceEventData> list = value.Peek();
						if (list != null)
						{
							int i = 0;
							while (i < list.Count)
							{
								NKCPopupEventRace.NKCRaceEventData nkcraceEventData = list[i];
								nkcraceEventData.m_AniSet.Update(Time.deltaTime);
								if (nkcraceEventData.m_AniSet.IsFinished())
								{
									nkcraceEventData.m_AniSet.RemoveEffect();
									this.m_lstUsedEventData.AddRange(value.Dequeue());
									if (value.Count != 0)
									{
										break;
									}
									if (keyValuePair.Key == this.m_MyActor.GetSpineIllust() == this.m_bUserWin)
									{
										keyValuePair.Key.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SD_WIN, true, false, 0f);
										keyValuePair.Key.SetTimeScale(1f);
									}
									else
									{
										keyValuePair.Key.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, false, 0f);
										keyValuePair.Key.SetTimeScale(1f);
									}
									if (this.m_bFirstGoal)
									{
										this.m_bFirstGoal = false;
										base.StopAllCoroutines();
										base.StartCoroutine(this.EndRace());
										break;
									}
									NKCUtil.SetGameobjectActive(this.m_AnimatorIntro.gameObject, false);
									this.m_bUpdate = false;
									break;
								}
								else
								{
									i++;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06008BBC RID: 35772 RVA: 0x002F8090 File Offset: 0x002F6290
		private void CheckBubble()
		{
			if (Mathf.Abs(-this.m_trMoveObjParent.localPosition.x - this.m_OtherActor.transform.localPosition.x) <= (float)(Screen.width / 2))
			{
				NKCUtil.SetGameobjectActive(this.m_objBubbleLeft, false);
				NKCUtil.SetGameobjectActive(this.m_objBubbleRight, false);
				return;
			}
			if (-this.m_trMoveObjParent.localPosition.x > this.m_OtherActor.transform.localPosition.x)
			{
				NKCUtil.SetGameobjectActive(this.m_objBubbleLeft, true);
				NKCUtil.SetGameobjectActive(this.m_objBubbleRight, false);
				NKMEventRaceTemplet nkmeventRaceTemplet = NKMEventRaceTemplet.Find(this.m_NKMPACKET_RACE_START_ACK.racePrivate.RaceId);
				if (this.m_SelectedTeam == RaceTeam.TeamA)
				{
					NKCUtil.SetImageSprite(this.m_imgBubbleLeft, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_EVENT_RACE_BUBBLE", nkmeventRaceTemplet.TeamBBubbleSad, false), false);
					return;
				}
				NKCUtil.SetImageSprite(this.m_imgBubbleLeft, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_EVENT_RACE_BUBBLE", nkmeventRaceTemplet.TeamABubbleSad, false), false);
				return;
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objBubbleLeft, false);
				NKCUtil.SetGameobjectActive(this.m_objBubbleRight, true);
				NKMEventRaceTemplet nkmeventRaceTemplet2 = NKMEventRaceTemplet.Find(this.m_NKMPACKET_RACE_START_ACK.racePrivate.RaceId);
				if (this.m_SelectedTeam == RaceTeam.TeamA)
				{
					NKCUtil.SetImageSprite(this.m_imgBubbleRight, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_EVENT_RACE_BUBBLE", nkmeventRaceTemplet2.TeamBBubbleHappy, false), false);
					return;
				}
				NKCUtil.SetImageSprite(this.m_imgBubbleRight, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_EVENT_RACE_BUBBLE", nkmeventRaceTemplet2.TeamABubbleHappy, false), false);
				return;
			}
		}

		// Token: 0x06008BBD RID: 35773 RVA: 0x002F81FA File Offset: 0x002F63FA
		private IEnumerator EndRace()
		{
			NKCUtil.SetGameobjectActive(this.m_AnimatorIntro.gameObject, false);
			NKCUtil.SetGameobjectActive(this.m_AnimatorGoal.gameObject, true);
			if (this.m_NKMPACKET_RACE_START_ACK.racePrivate.SelectTeam == RaceTeam.TeamA)
			{
				if (this.m_bUserWin)
				{
					NKCUtil.SetImageSprite(this.m_imgGoal, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_EVENT_RACE_SPRITE", "NKM_UI_EVENT_RACE_HUD_GOAL_RED", false), false);
				}
				else
				{
					NKCUtil.SetImageSprite(this.m_imgGoal, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_EVENT_RACE_SPRITE", "NKM_UI_EVENT_RACE_HUD_GOAL_BLUE", false), false);
				}
			}
			else if (!this.m_bUserWin)
			{
				NKCUtil.SetImageSprite(this.m_imgGoal, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_EVENT_RACE_SPRITE", "NKM_UI_EVENT_RACE_HUD_GOAL_RED", false), false);
			}
			else
			{
				NKCUtil.SetImageSprite(this.m_imgGoal, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_EVENT_RACE_SPRITE", "NKM_UI_EVENT_RACE_HUD_GOAL_BLUE", false), false);
			}
			this.m_AnimatorGoal.Play("NKM_UI_EVENT_RACE_HUD_GOAL");
			yield return new WaitForSeconds(2.5f);
			while (this.m_bUpdate)
			{
				yield return null;
			}
			NKCSoundManager.StopAllSound();
			NKCSoundManager.StopMusic();
			NKCPopupEventRaceResult.Instance.Open(this.m_NKMPACKET_RACE_START_ACK);
			yield break;
		}

		// Token: 0x06008BBE RID: 35774 RVA: 0x002F8209 File Offset: 0x002F6409
		private void OnClickSelectLine1()
		{
			if (this.m_bWaitForPacket)
			{
				return;
			}
			this.m_bWaitForPacket = true;
			this.m_bUpdate = false;
			this.m_SelectedLine = 0;
			NKCPacketSender.Send_NKMPACKET_RACE_START_REQ(0);
		}

		// Token: 0x06008BBF RID: 35775 RVA: 0x002F822F File Offset: 0x002F642F
		private void OnClickSelectLine2()
		{
			if (this.m_bWaitForPacket)
			{
				return;
			}
			this.m_bWaitForPacket = true;
			this.m_bUpdate = false;
			this.m_SelectedLine = 1;
			NKCPacketSender.Send_NKMPACKET_RACE_START_REQ(1);
		}

		// Token: 0x06008BC0 RID: 35776 RVA: 0x002F8258 File Offset: 0x002F6458
		private void OnClickSkip()
		{
			this.m_bUpdate = false;
			this.m_bFirstGoal = false;
			float x = this.m_lstEventPoint[this.m_lstEventPoint.Count - 1].localPosition.x;
			this.m_AnimationActor1.transform.localPosition = new Vector3(x, 0f, 0f);
			this.m_AnimationActor1.m_Ani.Play("UNIT_EVENT_RACE_SD_BASE");
			this.m_AnimationActor2.transform.localPosition = new Vector3(x, 0f, 0f);
			this.m_AnimationActor2.m_Ani.Play("UNIT_EVENT_RACE_SD_BASE");
			foreach (KeyValuePair<NKCASUIUnitIllust, Queue<List<NKCPopupEventRace.NKCRaceEventData>>> keyValuePair in this.m_dicRaceEventSet)
			{
				if (keyValuePair.Key == this.m_MyActor.GetSpineIllust() == this.m_bUserWin)
				{
					keyValuePair.Key.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SD_WIN, true, false, 0f);
				}
				else
				{
					keyValuePair.Key.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, false, 0f);
				}
			}
			this.m_trMoveObjParent.localPosition = new Vector3(-this.m_MyActor.transform.localPosition.x, this.m_trMoveObjParent.localPosition.y, this.m_trMoveObjParent.localPosition.z);
			this.CheckBubble();
			base.StopAllCoroutines();
			base.StartCoroutine(this.EndRace());
		}

		// Token: 0x06008BC1 RID: 35777 RVA: 0x002F83EC File Offset: 0x002F65EC
		private void ResetPosition()
		{
			this.m_bUpdate = false;
			NKCEventRaceAnimationManager.LoadFromLua();
			NKCAnimationEventManager.LoadFromLua();
			this.m_objSelectMark.transform.SetParent(base.transform);
			NKCUtil.SetGameobjectActive(this.m_objSelectMark, false);
			NKCUtil.SetGameobjectActive(this.m_AnimatorIntro.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_AnimatorGoal.gameObject, false);
			NKCUtil.SetGameobjectActive(this.m_objBubbleLeft, false);
			NKCUtil.SetGameobjectActive(this.m_objBubbleRight, false);
			if (this.m_UnitRed != null)
			{
				this.m_UnitRed.Unload();
				this.m_UnitRed = null;
			}
			if (this.m_UnitBlue != null)
			{
				this.m_UnitBlue.Unload();
				this.m_UnitBlue = null;
			}
			for (int i = 0; i < this.m_lstUsedObject.Count; i++)
			{
				this.m_lstUsedObject[i].Unload();
				this.m_lstUsedObject[i] = null;
			}
			this.m_lstUsedObject.Clear();
			this.m_trMoveObjParent.localPosition = Vector3.zero;
			if (this.m_AnimatorIntro != null)
			{
				this.m_AnimatorIntro.Play("NKM_UI_EVENT_RACE_HUD_INTRO_INTRO");
			}
			NKCUtil.SetGameobjectActive(this.m_AnimationActor1, false);
			NKCUtil.SetGameobjectActive(this.m_AnimationActor2, false);
			NKCUtil.SetGameobjectActive(this.m_AnimatorIntro.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_AnimatorGoal.gameObject, false);
			this.ResetData();
		}

		// Token: 0x06008BC2 RID: 35778 RVA: 0x002F854C File Offset: 0x002F674C
		private NKCAnimationActor GetActorByTeam(bool bSelectedThisTeam)
		{
			int selectedLine = this.m_SelectedLine;
			if (selectedLine != 0)
			{
				if (selectedLine != 1)
				{
				}
				if (bSelectedThisTeam)
				{
					return this.m_AnimationActor2;
				}
				return this.m_AnimationActor1;
			}
			else
			{
				if (bSelectedThisTeam)
				{
					return this.m_AnimationActor1;
				}
				return this.m_AnimationActor2;
			}
		}

		// Token: 0x06008BC3 RID: 35779 RVA: 0x002F858A File Offset: 0x002F678A
		private NKCAnimationActor GetActorByWin(bool bWin)
		{
			if (this.m_bUserWin == bWin)
			{
				return this.m_MyActor;
			}
			if (this.m_AnimationActor1 == this.m_MyActor)
			{
				return this.m_AnimationActor2;
			}
			return this.m_AnimationActor1;
		}

		// Token: 0x06008BC4 RID: 35780 RVA: 0x002F85BC File Offset: 0x002F67BC
		private List<RaceEventType> MakeRaceEventList(out float totalTime)
		{
			List<RaceEventType> list = new List<RaceEventType>();
			System.Random random = new System.Random();
			int i = 0;
			int num = this.m_lstEventPoint.Count - 1;
			totalTime = 0f;
			while (i < this.m_lstEventPoint.Count - 1)
			{
				List<RaceEventType> usableRaceType = this.GetUsableRaceType(list, i, num);
				if (usableRaceType.Count == 0)
				{
					Debug.LogError(string.Format("UsableType is null!! - StartIdx : {0}, remainCapacity : {1}", i, num));
					StringBuilder stringBuilder = new StringBuilder();
					for (int j = 0; j < this.m_lstEventPoint.Count; j++)
					{
						stringBuilder.Append(string.Format(" {0}", this.m_lstEventPoint[j]));
					}
					Debug.LogError(stringBuilder.ToString());
					break;
				}
				RaceEventType raceEventType = usableRaceType[random.Next(0, usableRaceType.Count)];
				list.Add(raceEventType);
				totalTime += NKCEventRaceAnimationManager.GetTotalTime(raceEventType);
				int capacity = NKCEventRaceAnimationManager.GetCapacity(raceEventType);
				i += capacity;
				num -= capacity;
			}
			return list;
		}

		// Token: 0x06008BC5 RID: 35781 RVA: 0x002F86C0 File Offset: 0x002F68C0
		private List<RaceEventType> GetUsableRaceType(List<RaceEventType> curRaceEventType, int startIndex, int remainCapacity)
		{
			List<RaceEventType> list = new List<RaceEventType>();
			for (int i = 0; i < 14; i++)
			{
				RaceEventType targetType = (RaceEventType)i;
				if (NKCEventRaceAnimationManager.Find(targetType) != null)
				{
					List<RaceEventType> list2 = curRaceEventType.FindAll((RaceEventType x) => x == targetType);
					if ((list2 == null || list2.Count < NKCEventRaceAnimationManager.GetMaxCount(targetType)) && startIndex >= NKCEventRaceAnimationManager.GetMinIndex(targetType) && startIndex <= NKCEventRaceAnimationManager.GetMaxIndex(targetType))
					{
						int capacity = NKCEventRaceAnimationManager.GetCapacity(targetType);
						if (remainCapacity >= capacity)
						{
							list.Add(targetType);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06008BC6 RID: 35782 RVA: 0x002F8764 File Offset: 0x002F6964
		private List<NKCPopupEventRace.NKCRaceEventData> MakeEventSet(RaceEventType eventType, NKCASUIUnitIllust mainChar, NKCAnimationActor actor, int startpoint, int capacity)
		{
			List<NKCPopupEventRace.NKCRaceEventData> list = new List<NKCPopupEventRace.NKCRaceEventData>();
			List<NKCEventRaceAnimationTemplet> list2 = NKCEventRaceAnimationManager.Find(eventType);
			for (int i = 0; i < list2.Count; i++)
			{
				NKCPopupEventRace.NKCRaceEventData nkcraceEventData = new NKCPopupEventRace.NKCRaceEventData();
				List<NKCAnimationEventTemplet> list3 = NKCAnimationEventManager.Find(list2[i].m_AnimationEventSetID);
				if (list3 == null)
				{
					Log.Error("NKCAnimationEventTemplet is null - " + list2[i].m_AnimationEventSetID, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Event/NKCPopupEventRace.cs", 630);
				}
				else
				{
					if (string.IsNullOrEmpty(list2[i].m_TargetObjName))
					{
						nkcraceEventData.m_Actor = actor;
						actor.SetSpineIllust(mainChar, false);
					}
					else
					{
						string[] array = list2[i].m_TargetObjName.Split(new char[]
						{
							'@'
						});
						if (array.Length == 2)
						{
							NKCASUISpineIllust nkcasuispineIllust = NKCResourceUtility.OpenSpineSD(array[0], array[1], false);
							nkcasuispineIllust.SetParent(actor.transform.parent, false);
							RectTransform rectTransform = nkcasuispineIllust.GetRectTransform();
							if (rectTransform != null)
							{
								NKCAnimationEventTemplet nkcanimationEventTemplet = list3.Find((NKCAnimationEventTemplet x) => x.m_AniEventType == AnimationEventType.ANIMATION_SPINE && x.m_StartTime == 0f);
								if (nkcanimationEventTemplet != null)
								{
									nkcasuispineIllust.SetDefaultAnimation((NKCASUIUnitIllust.eAnimation)Enum.Parse(typeof(NKCASUIUnitIllust.eAnimation), nkcanimationEventTemplet.m_StrValue), true, false, 0f);
								}
								Vector3 localPosition = NKCUtil.Lerp(this.m_lstEventPoint[startpoint].localPosition, this.m_lstEventPoint[startpoint + capacity].localPosition, list2[i].m_SpawnPosX);
								rectTransform.localPosition = localPosition;
								rectTransform.localScale = Vector3.one * list2[i].m_Size;
								rectTransform.gameObject.AddComponent<NKCAnimationActor>();
								nkcraceEventData.m_Actor = rectTransform.GetComponent<NKCAnimationActor>();
								nkcraceEventData.m_Actor.m_trSDParent = rectTransform;
								rectTransform.GetComponent<NKCAnimationActor>().SetSpineIllust(nkcasuispineIllust, false);
							}
							this.m_lstUsedObject.Add(nkcasuispineIllust);
						}
					}
					nkcraceEventData.m_startPoint = startpoint;
					nkcraceEventData.m_capacity = list2[i].m_SlotCapacity;
					nkcraceEventData.m_AniSet = new NKCAnimationInstance(nkcraceEventData.m_Actor, this.m_trMoveObjParent, list3, this.m_lstEventPoint[startpoint].localPosition, this.m_lstEventPoint[startpoint + capacity].localPosition);
					list.Add(nkcraceEventData);
				}
			}
			return list;
		}

		// Token: 0x06008BC7 RID: 35783 RVA: 0x002F89C0 File Offset: 0x002F6BC0
		public void OnRecv(NKMPACKET_RACE_START_ACK sPacket)
		{
			this.ResetData();
			this.m_NKMPACKET_RACE_START_ACK = sPacket;
			this.m_bUserWin = sPacket.isWin;
			float num = 0f;
			float num2 = 0f;
			List<RaceEventType> list = this.MakeRaceEventList(out num);
			List<RaceEventType> list2 = this.MakeRaceEventList(out num2);
			while (num == num2)
			{
				list2 = this.MakeRaceEventList(out num2);
			}
			List<RaceEventType> list3 = (num < num2) ? list : list2;
			List<RaceEventType> list4 = (num > num2) ? list : list2;
			if (this.m_UnitRed != null)
			{
				this.m_UnitRed.Unload();
				this.m_UnitRed = null;
			}
			this.m_UnitRed = NKCResourceUtility.OpenSpineSD(this.m_teamA_SDUnitName, false);
			if (this.m_UnitRed != null)
			{
				this.m_UnitRed.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, false, 0f);
				this.m_UnitRed.SetAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, 0, true, 0f, true);
				this.m_UnitRed.SetParent(this.GetActorByTeam(this.m_SelectedTeam == RaceTeam.TeamA).m_trSDParent, false);
				RectTransform rectTransform = this.m_UnitRed.GetRectTransform();
				if (rectTransform != null)
				{
					rectTransform.localPosition = Vector3.zero;
					rectTransform.localScale = Vector3.one;
				}
				this.GetActorByTeam(this.m_SelectedTeam == RaceTeam.TeamA).SetSpineIllust(this.m_UnitRed, false);
			}
			if (this.m_UnitBlue != null)
			{
				this.m_UnitBlue.Unload();
				this.m_UnitBlue = null;
			}
			this.m_UnitBlue = NKCResourceUtility.OpenSpineSD(this.m_teamB_SDUnitName, false);
			if (this.m_UnitBlue != null)
			{
				this.m_UnitBlue.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, false, 0f);
				this.m_UnitBlue.SetAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, 0, true, 0f, true);
				this.m_UnitBlue.SetParent(this.GetActorByTeam(this.m_SelectedTeam == RaceTeam.TeamB).m_trSDParent, false);
				RectTransform rectTransform2 = this.m_UnitBlue.GetRectTransform();
				if (rectTransform2 != null)
				{
					rectTransform2.localPosition = Vector3.zero;
					rectTransform2.localScale = Vector3.one;
				}
				this.GetActorByTeam(this.m_SelectedTeam == RaceTeam.TeamB).SetSpineIllust(this.m_UnitBlue, false);
			}
			this.m_MyActor = ((this.m_SelectedLine == 0) ? this.m_AnimationActor1 : this.m_AnimationActor2);
			this.m_OtherActor = ((this.m_SelectedLine == 0) ? this.m_AnimationActor2 : this.m_AnimationActor1);
			NKCUtil.SetGameobjectActive(this.m_AnimationActor1, true);
			NKCUtil.SetGameobjectActive(this.m_AnimationActor2, true);
			this.m_queueDataWin.Clear();
			this.m_queueDataLose.Clear();
			this.m_dicRaceEventSet.Clear();
			int num3 = 0;
			for (int i = 0; i < list3.Count; i++)
			{
				int capacity = NKCEventRaceAnimationManager.GetCapacity(list3[i]);
				List<NKCPopupEventRace.NKCRaceEventData> item = this.MakeEventSet(list3[i], this.GetActorByWin(true).GetSpineIllust(), this.GetActorByWin(true), num3, capacity);
				this.m_queueDataWin.Enqueue(item);
				num3 += capacity;
			}
			this.m_dicRaceEventSet.Add(this.GetActorByWin(true).GetSpineIllust(), this.m_queueDataWin);
			num3 = 0;
			for (int j = 0; j < list4.Count; j++)
			{
				int capacity2 = NKCEventRaceAnimationManager.GetCapacity(list4[j]);
				List<NKCPopupEventRace.NKCRaceEventData> item2 = this.MakeEventSet(list4[j], this.GetActorByWin(false).GetSpineIllust(), this.GetActorByWin(false), num3, capacity2);
				this.m_queueDataLose.Enqueue(item2);
				num3 += capacity2;
			}
			this.m_dicRaceEventSet.Add(this.GetActorByWin(false).GetSpineIllust(), this.m_queueDataLose);
			if (this.m_SelectedTeam == RaceTeam.TeamA)
			{
				this.m_objSelectMark.transform.SetParent(this.GetActorByTeam(this.m_SelectedTeam == RaceTeam.TeamA).transform);
				NKCUtil.SetImageSprite(this.m_imgSelectMark, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_EVENT_RACE_SPRITE", "NKM_UI_EVENT_RACE_HUD_TEAM_RED", false), false);
			}
			else
			{
				this.m_objSelectMark.transform.SetParent(this.GetActorByTeam(this.m_SelectedTeam == RaceTeam.TeamB).transform);
				NKCUtil.SetImageSprite(this.m_imgSelectMark, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_EVENT_RACE_SPRITE", "NKM_UI_EVENT_RACE_HUD_TEAM_BLUE", false), false);
			}
			NKCUtil.SetGameobjectActive(this.m_objSelectMark, true);
			this.m_objSelectMark.transform.localPosition = new Vector3(0f, this.m_fArrowPosY, 0f);
			this.m_AnimationActor1.transform.SetAsLastSibling();
			this.m_AnimationActor2.transform.SetAsLastSibling();
			base.StopAllCoroutines();
			base.StartCoroutine(this.StartRace());
		}

		// Token: 0x06008BC8 RID: 35784 RVA: 0x002F8E2C File Offset: 0x002F702C
		private IEnumerator StartRace()
		{
			this.m_AnimatorIntro.Play("NKM_UI_EVENT_RACE_HUD_INTRO_COUNTDOWN");
			yield return new WaitForSeconds(this.m_CountdownWaitSeconds);
			this.m_bUpdate = true;
			NKCSoundManager.StopAllSound();
			NKCSoundManager.StopMusic();
			NKCSoundManager.PlayMusic("UI_SPORT_RACE", true, 1f, false, 0f, 0f);
			this.m_AnimatorIntro.Play("NKM_UI_EVENT_RACE_HUD_INTRO_OUTRO");
			yield break;
		}

		// Token: 0x06008BC9 RID: 35785 RVA: 0x002F8E3C File Offset: 0x002F703C
		private void OnClickAddAnimationEvent()
		{
			this.ResetPosition();
			if (this.m_UnitRed != null)
			{
				this.m_UnitRed.Unload();
				this.m_UnitRed = null;
			}
			this.m_UnitRed = NKCResourceUtility.OpenSpineSD(this.m_teamA_SDUnitName, false);
			if (this.m_UnitRed != null)
			{
				this.m_UnitRed.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, false, 0f);
				this.m_UnitRed.SetAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, 0, true, 0f, true);
				this.m_UnitRed.SetParent(this.m_AnimationActor1.m_trSDParent, false);
				RectTransform rectTransform = this.m_UnitRed.GetRectTransform();
				if (rectTransform != null)
				{
					rectTransform.localPosition = Vector3.zero;
					rectTransform.localScale = Vector3.one;
				}
			}
			if (this.m_UnitBlue != null)
			{
				this.m_UnitBlue.Unload();
				this.m_UnitBlue = null;
			}
			this.m_UnitBlue = NKCResourceUtility.OpenSpineSD(this.m_teamB_SDUnitName, false);
			if (this.m_UnitBlue != null)
			{
				this.m_UnitBlue.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, false, 0f);
				this.m_UnitBlue.SetAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, 0, true, 0f, true);
				this.m_UnitBlue.SetParent(this.m_AnimationActor2.m_trSDParent, false);
				RectTransform rectTransform2 = this.m_UnitBlue.GetRectTransform();
				if (rectTransform2 != null)
				{
					rectTransform2.localPosition = Vector3.zero;
					rectTransform2.localScale = Vector3.one;
				}
			}
			RaceEventType raceEventType = (RaceEventType)Enum.Parse(typeof(RaceEventType), this.m_Input.text.ToUpper());
			int num = this.m_lstEventPoint.Count - 1;
			int i = 0;
			this.m_AnimationActor1.SetSpineIllust(this.m_UnitRed, false);
			this.m_AnimationActor2.SetSpineIllust(this.m_UnitBlue, false);
			int num2 = 0;
			while (i < this.m_lstEventPoint.Count - 1)
			{
				int capacity = NKCEventRaceAnimationManager.GetCapacity(raceEventType);
				if (capacity > num)
				{
					raceEventType = RaceEventType.RUN;
					capacity = NKCEventRaceAnimationManager.GetCapacity(raceEventType);
				}
				List<NKCPopupEventRace.NKCRaceEventData> item = this.MakeEventSet(raceEventType, this.m_UnitBlue, this.m_AnimationActor2, i, capacity);
				this.m_queueDataLose.Enqueue(item);
				i += capacity;
				num -= capacity;
				num2++;
			}
			this.m_MyActor = this.m_AnimationActor2;
			this.m_OtherActor = this.m_AnimationActor1;
			NKCUtil.SetGameobjectActive(this.m_MyActor, true);
			this.m_dicRaceEventSet.Add(this.m_UnitBlue, this.m_queueDataLose);
			this.m_AnimationActor1.transform.SetAsLastSibling();
			this.m_AnimationActor2.transform.SetAsLastSibling();
			this.m_NKMPACKET_RACE_START_ACK = this.MakeDummyPacket();
			this.m_AnimatorIntro.Play("NKM_UI_EVENT_RACE_HUD_INTRO_OUTRO");
			NKCSoundManager.PlayMusic("UI_SPORT_RACE", true, 1f, false, 0f, 0f);
			this.m_bUpdate = true;
		}

		// Token: 0x06008BCA RID: 35786 RVA: 0x002F90F4 File Offset: 0x002F72F4
		private NKMPACKET_RACE_START_ACK MakeDummyPacket()
		{
			NKMPACKET_RACE_START_ACK nkmpacket_RACE_START_ACK = new NKMPACKET_RACE_START_ACK();
			nkmpacket_RACE_START_ACK.racePrivate = new NKMRacePrivate();
			using (IEnumerator<NKMEventRaceTemplet> enumerator = NKMEventRaceTemplet.Values.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					NKMEventRaceTemplet nkmeventRaceTemplet = enumerator.Current;
					nkmpacket_RACE_START_ACK.racePrivate.RaceId = nkmeventRaceTemplet.Key;
				}
			}
			return nkmpacket_RACE_START_ACK;
		}

		// Token: 0x0400787E RID: 30846
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_EVENT_PF_RACE";

		// Token: 0x0400787F RID: 30847
		private const string UI_ASSET_NAME = "NKM_UI_EVENT_RACE_HUD";

		// Token: 0x04007880 RID: 30848
		private static NKCPopupEventRace m_Instance;

		// Token: 0x04007881 RID: 30849
		public List<Transform> m_lstEventPoint = new List<Transform>();

		// Token: 0x04007882 RID: 30850
		public Animator m_AnimatorIntro;

		// Token: 0x04007883 RID: 30851
		public Animator m_AnimatorGoal;

		// Token: 0x04007884 RID: 30852
		public Image m_imgGoal;

		// Token: 0x04007885 RID: 30853
		private const string ANIMATION_INTRO = "NKM_UI_EVENT_RACE_HUD_INTRO_INTRO";

		// Token: 0x04007886 RID: 30854
		private const string ANIMATION_COUNTDOWN = "NKM_UI_EVENT_RACE_HUD_INTRO_COUNTDOWN";

		// Token: 0x04007887 RID: 30855
		private const string ANIMATION_OUTRO = "NKM_UI_EVENT_RACE_HUD_INTRO_OUTRO";

		// Token: 0x04007888 RID: 30856
		private const string ANIMATION_GOAL = "NKM_UI_EVENT_RACE_HUD_GOAL";

		// Token: 0x04007889 RID: 30857
		private const string GOAL_BUNDLE_NAME = "AB_UI_NKM_UI_EVENT_RACE_SPRITE";

		// Token: 0x0400788A RID: 30858
		private const string GOAL_RED = "NKM_UI_EVENT_RACE_HUD_GOAL_RED";

		// Token: 0x0400788B RID: 30859
		private const string GOAL_BLUE = "NKM_UI_EVENT_RACE_HUD_GOAL_BLUE";

		// Token: 0x0400788C RID: 30860
		public NKCASUISpineIllust m_UnitRed;

		// Token: 0x0400788D RID: 30861
		public NKCASUISpineIllust m_UnitBlue;

		// Token: 0x0400788E RID: 30862
		public NKCAnimationActor m_AnimationActor1;

		// Token: 0x0400788F RID: 30863
		public NKCAnimationActor m_AnimationActor2;

		// Token: 0x04007890 RID: 30864
		public Transform m_trMoveObjParent;

		// Token: 0x04007891 RID: 30865
		public GameObject m_objSelectMark;

		// Token: 0x04007892 RID: 30866
		public Image m_imgSelectMark;

		// Token: 0x04007893 RID: 30867
		public NKCUIComStateButton m_btnSkip;

		// Token: 0x04007894 RID: 30868
		public NKCUIComStateButton m_btnSelectLine1;

		// Token: 0x04007895 RID: 30869
		public NKCUIComStateButton m_btnSelectLine2;

		// Token: 0x04007896 RID: 30870
		public GameObject m_objBubbleLeft;

		// Token: 0x04007897 RID: 30871
		public GameObject m_objBubbleRight;

		// Token: 0x04007898 RID: 30872
		public Image m_imgBubbleLeft;

		// Token: 0x04007899 RID: 30873
		public Image m_imgBubbleRight;

		// Token: 0x0400789A RID: 30874
		[Header("테스트코드")]
		public GameObject m_objTestParent;

		// Token: 0x0400789B RID: 30875
		public NKCUIComStateButton m_btnTest;

		// Token: 0x0400789C RID: 30876
		public InputField m_Input;

		// Token: 0x0400789D RID: 30877
		public NKCUIComStateButton m_btnAdd;

		// Token: 0x0400789E RID: 30878
		public float m_lerpValue = 2f;

		// Token: 0x0400789F RID: 30879
		public float m_CountdownWaitSeconds = 3.3f;

		// Token: 0x040078A0 RID: 30880
		public float m_fArrowPosY = 320f;

		// Token: 0x040078A1 RID: 30881
		private const string ARROW_BUNDLE_NAME = "AB_UI_NKM_UI_EVENT_RACE_SPRITE";

		// Token: 0x040078A2 RID: 30882
		private const string BLUE_ARROW_SPRITE_NAME = "NKM_UI_EVENT_RACE_HUD_TEAM_BLUE";

		// Token: 0x040078A3 RID: 30883
		private const string RED_ARROW_SPRITE_NAME = "NKM_UI_EVENT_RACE_HUD_TEAM_RED";

		// Token: 0x040078A4 RID: 30884
		private NKCAnimationActor m_MyActor;

		// Token: 0x040078A5 RID: 30885
		private NKCAnimationActor m_OtherActor;

		// Token: 0x040078A6 RID: 30886
		private Dictionary<NKCASUIUnitIllust, Queue<List<NKCPopupEventRace.NKCRaceEventData>>> m_dicRaceEventSet = new Dictionary<NKCASUIUnitIllust, Queue<List<NKCPopupEventRace.NKCRaceEventData>>>();

		// Token: 0x040078A7 RID: 30887
		private Queue<List<NKCPopupEventRace.NKCRaceEventData>> m_queueDataWin = new Queue<List<NKCPopupEventRace.NKCRaceEventData>>();

		// Token: 0x040078A8 RID: 30888
		private Queue<List<NKCPopupEventRace.NKCRaceEventData>> m_queueDataLose = new Queue<List<NKCPopupEventRace.NKCRaceEventData>>();

		// Token: 0x040078A9 RID: 30889
		private List<NKCPopupEventRace.NKCRaceEventData> m_lstUsedEventData = new List<NKCPopupEventRace.NKCRaceEventData>();

		// Token: 0x040078AA RID: 30890
		private NKMPACKET_RACE_START_ACK m_NKMPACKET_RACE_START_ACK;

		// Token: 0x040078AB RID: 30891
		private List<NKCASUIUnitIllust> m_lstUsedObject = new List<NKCASUIUnitIllust>();

		// Token: 0x040078AC RID: 30892
		private bool m_bUpdate;

		// Token: 0x040078AD RID: 30893
		private bool m_bUserWin = true;

		// Token: 0x040078AE RID: 30894
		private int m_SelectedLine;

		// Token: 0x040078AF RID: 30895
		private RaceTeam m_SelectedTeam;

		// Token: 0x040078B0 RID: 30896
		private bool m_bFirstGoal = true;

		// Token: 0x040078B1 RID: 30897
		private string m_teamA_SDUnitName;

		// Token: 0x040078B2 RID: 30898
		private string m_teamB_SDUnitName;

		// Token: 0x040078B3 RID: 30899
		private bool m_bWaitForPacket;

		// Token: 0x0200199C RID: 6556
		private enum RaceState
		{
			// Token: 0x0400AC6B RID: 44139
			Ready,
			// Token: 0x0400AC6C RID: 44140
			Play,
			// Token: 0x0400AC6D RID: 44141
			End
		}

		// Token: 0x0200199D RID: 6557
		public class NKCRaceEventData
		{
			// Token: 0x0400AC6E RID: 44142
			public NKCAnimationActor m_Actor;

			// Token: 0x0400AC6F RID: 44143
			public int m_capacity;

			// Token: 0x0400AC70 RID: 44144
			public int m_startPoint;

			// Token: 0x0400AC71 RID: 44145
			public NKCAnimationInstance m_AniSet;
		}
	}
}
