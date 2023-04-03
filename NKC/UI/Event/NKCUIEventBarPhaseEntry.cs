using System;
using System.Collections;
using System.Collections.Generic;
using NKC.UI.NPC;
using NKM;
using NKM.Event;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BD5 RID: 3029
	public class NKCUIEventBarPhaseEntry : MonoBehaviour
	{
		// Token: 0x06008C70 RID: 35952 RVA: 0x002FC44C File Offset: 0x002FA64C
		public void Init()
		{
			if (this.m_EventBarCocktailSlotArray != null)
			{
				int num = this.m_EventBarCocktailSlotArray.Length;
				for (int i = 0; i < num; i++)
				{
					this.m_EventBarCocktailSlotArray[i].Init();
				}
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnGive, new UnityAction(this.OnClickGive));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnStay, new UnityAction(this.OnClickStay));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnMomoErrand, new UnityAction(this.OnClickMomoErrand));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnScriptPanel, new UnityAction(this.OnClickScriptPanel));
			NKCEventBarManager.RewardPopupUnitID = this.m_unitID;
			NKCUIEventBarBubble eventBarBubble = this.m_eventBarBubble;
			if (eventBarBubble != null)
			{
				eventBarBubble.Init();
			}
			if (this.m_npcSpineIllust != null)
			{
				this.m_npcSpineIllust.m_dOnTouch = new NKCUINPCSpineIllust.OnTouch(this.OnCharacterTouch);
			}
			if (this.m_scrollRect != null)
			{
				NKCUtil.SetScrollHotKey(this.m_scrollRect, null);
				this.m_scrollRect.scrollSensitivity = NKCInputManager.ScrollSensibility;
			}
		}

		// Token: 0x06008C71 RID: 35953 RVA: 0x002FC54C File Offset: 0x002FA74C
		public void SetData(int eventID)
		{
			this.m_eventID = eventID;
			if (this.cockTailIDList.Count <= 0)
			{
				foreach (NKMEventBarTemplet nkmeventBarTemplet in NKMEventBarTemplet.Values)
				{
					if (nkmeventBarTemplet.EventID == eventID && !this.cockTailIDList.Contains(nkmeventBarTemplet.RewardItemId))
					{
						this.cockTailIDList.Add(nkmeventBarTemplet.RewardItemId);
					}
				}
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_unitID);
			NKCUIEventBarBubble eventBarBubble = this.m_eventBarBubble;
			if (eventBarBubble != null)
			{
				eventBarBubble.Hide();
			}
			NKCUtil.SetLabelText(this.m_lbUnitName, (unitTempletBase != null) ? unitTempletBase.GetUnitName() : "");
			if (this.m_scriptCoroutine != null)
			{
				base.StopCoroutine(this.m_scriptCoroutine);
				this.m_scriptCoroutine = null;
			}
			this.HideScript();
			if (NKCEventBarManager.RemainDeliveryLimitValue <= 0)
			{
				this.showDeliverFinishScript = true;
				NKCUtil.SetGameobjectActive(this.m_objLaughFx, true);
				NKCUINPCSpineIllust npcSpineIllust = this.m_npcSpineIllust;
				if (npcSpineIllust != null)
				{
					npcSpineIllust.SetDefaultAnimation(NKCASUIUnitIllust.GetAnimationName(NKCASUIUnitIllust.eAnimation.UNIT_LAUGH));
				}
			}
			else
			{
				this.showDeliverFinishScript = false;
				NKCUIEventBarBubble eventBarBubble2 = this.m_eventBarBubble;
				if (eventBarBubble2 != null)
				{
					eventBarBubble2.Show(NKCEventBarManager.DailyCocktailItemID, true, new NKCUIEventBarBubble.OnTouchBubble(this.OnTouchBubble));
				}
				NKCUtil.SetGameobjectActive(this.m_objLaughFx, false);
				NKCUINPCSpineIllust npcSpineIllust2 = this.m_npcSpineIllust;
				if (npcSpineIllust2 != null)
				{
					npcSpineIllust2.SetDefaultAnimation(NKCASUIUnitIllust.GetAnimationName(NKCASUIUnitIllust.eAnimation.UNIT_IDLE));
				}
			}
			this.m_selectedCocktailID = 0;
			this.SetCocktailInfo();
			bool bValue = this.IsMomoMissionRedDotActive();
			NKCUtil.SetGameobjectActive(this.m_objErrandRedDot, bValue);
			if (this.m_scrollRect != null)
			{
				this.m_scrollRect.verticalNormalizedPosition = 1f;
			}
		}

		// Token: 0x06008C72 RID: 35954 RVA: 0x002FC6EC File Offset: 0x002FA8EC
		public void Refresh()
		{
			this.SetData(this.m_eventID);
		}

		// Token: 0x06008C73 RID: 35955 RVA: 0x002FC6FA File Offset: 0x002FA8FA
		public void Close()
		{
			if (this.cockTailIDList != null)
			{
				this.cockTailIDList.Clear();
			}
			if (this.m_scriptCoroutine != null)
			{
				base.StopCoroutine(this.m_scriptCoroutine);
				this.m_scriptCoroutine = null;
			}
		}

		// Token: 0x06008C74 RID: 35956 RVA: 0x002FC72A File Offset: 0x002FA92A
		public void Hide()
		{
			if (this.m_scriptCoroutine != null)
			{
				base.StopCoroutine(this.m_scriptCoroutine);
				this.m_scriptCoroutine = null;
			}
			this.HideScript();
		}

		// Token: 0x06008C75 RID: 35957 RVA: 0x002FC750 File Offset: 0x002FA950
		private void Update()
		{
			this.m_typeWriter.Update();
			if (this.showDeliverFinishScript)
			{
				string get_STRING_GREMORY_GIVE_END = NKCUtilString.GET_STRING_GREMORY_GIVE_END;
				this.ShowScriptType1(get_STRING_GREMORY_GIVE_END);
				this.m_fScriptTimer = this.m_showScriptTime;
				if (this.m_scriptCoroutine == null)
				{
					this.m_scriptCoroutine = base.StartCoroutine(this.IOnShowRequestScript());
				}
				this.showDeliverFinishScript = false;
			}
		}

		// Token: 0x06008C76 RID: 35958 RVA: 0x002FC7AC File Offset: 0x002FA9AC
		private void SetCocktailInfo()
		{
			if (this.m_EventBarCocktailSlotArray != null)
			{
				int count = this.cockTailIDList.Count;
				int num = this.m_EventBarCocktailSlotArray.Length;
				for (int i = 0; i < num; i++)
				{
					if (count <= i)
					{
						NKCUtil.SetGameobjectActive(this.m_EventBarCocktailSlotArray[i], false);
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_EventBarCocktailSlotArray[i], true);
						this.m_EventBarCocktailSlotArray[i].SetData(this.cockTailIDList[i], new NKCUIEventBarCocktailSlot.OnSelectSlot(this.OnSelectCocktailSlot));
					}
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objBarLock, NKCEventBarManager.RemainDeliveryLimitValue <= 0);
		}

		// Token: 0x06008C77 RID: 35959 RVA: 0x002FC83F File Offset: 0x002FAA3F
		private void ShowScriptType1(string message)
		{
			NKCUtil.SetGameobjectActive(this.m_objScriptRoot, true);
			NKCUtil.SetGameobjectActive(this.m_objScriptType1, true);
			NKCUtil.SetGameobjectActive(this.m_objScriptType2, false);
			this.m_typeWriter.Start(this.m_lbType1Msg, message, 0f, false);
		}

		// Token: 0x06008C78 RID: 35960 RVA: 0x002FC880 File Offset: 0x002FAA80
		private void ShowScriptType2(string message)
		{
			NKCUtil.SetGameobjectActive(this.m_objScriptRoot, true);
			NKCUtil.SetGameobjectActive(this.m_objScriptType1, false);
			NKCUtil.SetGameobjectActive(this.m_objScriptType2, true);
			this.m_typeWriter.Start(this.m_lbType2Msg, message, 0f, false);
			this.ToggleScriptType2Buttons(true);
		}

		// Token: 0x06008C79 RID: 35961 RVA: 0x002FC8D0 File Offset: 0x002FAAD0
		private void HideScript()
		{
			NKCUtil.SetGameobjectActive(this.m_objScriptRoot, false);
			NKCUtil.SetLabelText(this.m_lbType1Msg, "");
			NKCUtil.SetLabelText(this.m_lbType2Msg, "");
		}

		// Token: 0x06008C7A RID: 35962 RVA: 0x002FC900 File Offset: 0x002FAB00
		private void OnCharacterTouch()
		{
			NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_TOUCH, this.m_unitID, 0, false, false);
			if (NKCEventBarManager.RemainDeliveryLimitValue > 0)
			{
				NKMItemMiscTemplet nkmitemMiscTemplet = NKMItemMiscTemplet.Find(NKCEventBarManager.DailyCocktailItemID);
				string message = string.Format(NKCUtilString.GET_STRING_GREMORY_REQUEST, (nkmitemMiscTemplet != null) ? nkmitemMiscTemplet.GetItemName() : "");
				this.ShowScriptType1(message);
				this.m_fScriptTimer = this.m_showScriptTime;
				if (this.m_scriptCoroutine == null)
				{
					this.m_scriptCoroutine = base.StartCoroutine(this.IOnShowRequestScript());
				}
			}
			else
			{
				string get_STRING_GREMORY_GIVE_END = NKCUtilString.GET_STRING_GREMORY_GIVE_END;
				this.ShowScriptType1(get_STRING_GREMORY_GIVE_END);
				this.m_fScriptTimer = this.m_showScriptTime;
				if (this.m_scriptCoroutine == null)
				{
					this.m_scriptCoroutine = base.StartCoroutine(this.IOnShowRequestScript());
				}
			}
			if (NKCEventBarManager.RemainDeliveryLimitValue > 0 && this.m_eventBarBubble != null && !this.m_eventBarBubble.gameObject.activeSelf)
			{
				NKCUIEventBarBubble eventBarBubble = this.m_eventBarBubble;
				if (eventBarBubble != null)
				{
					eventBarBubble.Show(NKCEventBarManager.DailyCocktailItemID, false, new NKCUIEventBarBubble.OnTouchBubble(this.OnTouchBubble));
				}
			}
			NKCUIEventBarBubble eventBarBubble2 = this.m_eventBarBubble;
			if (eventBarBubble2 == null)
			{
				return;
			}
			eventBarBubble2.ResetAnimation();
		}

		// Token: 0x06008C7B RID: 35963 RVA: 0x002FCA08 File Offset: 0x002FAC08
		private IEnumerator IOnShowRequestScript()
		{
			while (this.m_fScriptTimer > 0f)
			{
				this.m_fScriptTimer -= Time.deltaTime;
				yield return null;
			}
			yield return new WaitWhile(() => this.m_typeWriter.IsTyping());
			Animator aniScript = this.m_aniScript;
			if (aniScript != null)
			{
				aniScript.SetTrigger("OUTRO");
			}
			yield return new WaitWhile(() => this.m_scriptCanvasGroup.alpha > 0f);
			this.HideScript();
			this.m_scriptCoroutine = null;
			yield break;
		}

		// Token: 0x06008C7C RID: 35964 RVA: 0x002FCA17 File Offset: 0x002FAC17
		private void ToggleScriptType2Buttons(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_csbtnStay, value);
			NKCUtil.SetGameobjectActive(this.m_csbtnGive, value);
		}

		// Token: 0x06008C7D RID: 35965 RVA: 0x002FCA34 File Offset: 0x002FAC34
		private long GetInventoryCocktailCount(int cocktailID)
		{
			long result = 0L;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				result = nkmuserData.m_InventoryData.GetCountMiscItem(cocktailID);
			}
			return result;
		}

		// Token: 0x06008C7E RID: 35966 RVA: 0x002FCA5C File Offset: 0x002FAC5C
		private int GetDailycocktailCount(int cocktailID)
		{
			NKMEventBarTemplet nkmeventBarTemplet = NKMEventBarTemplet.Find(cocktailID);
			if (nkmeventBarTemplet == null)
			{
				return 0;
			}
			return nkmeventBarTemplet.DeliveryValue;
		}

		// Token: 0x06008C7F RID: 35967 RVA: 0x002FCA7C File Offset: 0x002FAC7C
		private string GetCocktailName(int cocktailID)
		{
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(cocktailID);
			if (itemMiscTempletByID != null)
			{
				return itemMiscTempletByID.GetItemName();
			}
			return "";
		}

		// Token: 0x06008C80 RID: 35968 RVA: 0x002FCAA0 File Offset: 0x002FACA0
		private bool IsMomoMissionRedDotActive()
		{
			bool result = false;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				bool flag = nkmuserData.m_MissionData.CheckCompletableMission(nkmuserData, this.m_errandRewardMissionTabId, false);
				bool flag2 = false;
				NKMMissionData missionDataByGroupId = nkmuserData.m_MissionData.GetMissionDataByGroupId(this.m_cocktailRewardMissionGroupId);
				if (missionDataByGroupId != null)
				{
					flag2 = NKCScenManager.CurrentUserData().m_MissionData.CheckCompletableMission(nkmuserData, missionDataByGroupId.tabId, false);
				}
				result = (flag || flag2);
			}
			return result;
		}

		// Token: 0x06008C81 RID: 35969 RVA: 0x002FCB00 File Offset: 0x002FAD00
		private void OnTouchBubble()
		{
			string message = string.Format(NKCUtilString.GET_STRING_GREMORY_SELECT_COCKTAIL, this.GetCocktailName(NKCEventBarManager.DailyCocktailItemID), this.GetDailycocktailCount(NKCEventBarManager.DailyCocktailItemID));
			this.ShowScriptType1(message);
			this.m_fScriptTimer = this.m_showScriptTime;
			if (this.m_scriptCoroutine == null)
			{
				this.m_scriptCoroutine = base.StartCoroutine(this.IOnShowRequestScript());
			}
		}

		// Token: 0x06008C82 RID: 35970 RVA: 0x002FCB60 File Offset: 0x002FAD60
		private void OnMomoPopupClose()
		{
			bool bValue = this.IsMomoMissionRedDotActive();
			NKCUtil.SetGameobjectActive(this.m_objErrandRedDot, bValue);
		}

		// Token: 0x06008C83 RID: 35971 RVA: 0x002FCB80 File Offset: 0x002FAD80
		private void OnSelectCocktailSlot(GameObject slot)
		{
			if (this.m_EventBarCocktailSlotArray == null || NKCEventBarManager.RemainDeliveryLimitValue <= 0)
			{
				return;
			}
			this.m_selectedCocktailID = 0;
			int num = this.m_EventBarCocktailSlotArray.Length;
			for (int i = 0; i < num; i++)
			{
				if (!slot.Equals(this.m_EventBarCocktailSlotArray[i].gameObject))
				{
					this.m_EventBarCocktailSlotArray[i].SetSelected(false);
				}
				else if (this.m_EventBarCocktailSlotArray[i].Toggle())
				{
					this.m_selectedCocktailID = this.m_EventBarCocktailSlotArray[i].CockTailID;
				}
			}
			if (this.m_selectedCocktailID > 0)
			{
				string message = string.Format(NKCUtilString.GET_STRING_GREMORY_GIVE_DESC, Array.Empty<object>());
				this.ShowScriptType2(message);
			}
			else
			{
				string message = string.Format(NKCUtilString.GET_STRING_GREMORY_SELECT_COCKTAIL, this.GetCocktailName(NKCEventBarManager.DailyCocktailItemID), this.GetDailycocktailCount(NKCEventBarManager.DailyCocktailItemID));
				this.ShowScriptType1(message);
				this.m_fScriptTimer = this.m_showScriptTime;
				if (this.m_scriptCoroutine == null)
				{
					this.m_scriptCoroutine = base.StartCoroutine(this.IOnShowRequestScript());
				}
			}
			this.m_fScriptTimer = this.m_showScriptTime;
			if (this.m_scriptCoroutine == null)
			{
				this.m_scriptCoroutine = base.StartCoroutine(this.IOnShowRequestScript());
			}
		}

		// Token: 0x06008C84 RID: 35972 RVA: 0x002FCCA4 File Offset: 0x002FAEA4
		private void OnClickGive()
		{
			NKMEventBarTemplet nkmeventBarTemplet = NKMEventBarTemplet.Find(this.m_selectedCocktailID);
			if (nkmeventBarTemplet == null)
			{
				return;
			}
			if (this.m_selectedCocktailID == 0)
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(NKCEventBarManager.DailyCocktailItemID);
				string arg = (itemMiscTempletByID == null) ? "" : itemMiscTempletByID.GetItemName();
				this.m_fScriptTimer = this.m_showScriptTime;
				this.ShowScriptType2(string.Format(NKCUtilString.GET_STRING_GREMORY_SELECT_COCKTAIL, arg, nkmeventBarTemplet.DeliveryValue));
				return;
			}
			if (this.m_selectedCocktailID != NKCEventBarManager.DailyCocktailItemID)
			{
				this.m_fScriptTimer = this.m_showScriptTime;
				this.ShowScriptType2(NKCUtilString.GET_STRING_GREMORY_THAT_IS_WRONG_COCKTAIL);
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			if (nkmuserData.m_InventoryData.GetCountMiscItem(this.m_selectedCocktailID) < (long)nkmeventBarTemplet.DeliveryValue)
			{
				this.m_fScriptTimer = this.m_showScriptTime;
				this.ShowScriptType2(string.Format(NKCUtilString.GET_STRING_GREMORY_NEED_MORE_COCKTAIL, nkmeventBarTemplet.DeliveryValue));
				return;
			}
			NKCPacketSender.Send_NKMPacket_EVENT_BAR_GET_REWARD_REQ(this.m_selectedCocktailID);
			if (this.m_scriptCoroutine != null)
			{
				base.StopCoroutine(this.m_scriptCoroutine);
				this.m_scriptCoroutine = null;
			}
			this.HideScript();
		}

		// Token: 0x06008C85 RID: 35973 RVA: 0x002FCDAB File Offset: 0x002FAFAB
		private void OnClickStay()
		{
			this.m_fScriptTimer = this.m_showScriptTime;
			this.ShowScriptType1(NKCUtilString.GET_STRING_GREMORY_GIVE_CANCEL);
		}

		// Token: 0x06008C86 RID: 35974 RVA: 0x002FCDC4 File Offset: 0x002FAFC4
		private void OnClickMomoErrand()
		{
			NKCPopupEventBarMission.Instance.Open(this.m_errandRewardMissionTabId, this.m_cocktailRewardMissionGroupId, new NKCPopupEventBarMission.OnClose(this.OnMomoPopupClose));
		}

		// Token: 0x06008C87 RID: 35975 RVA: 0x002FCDE8 File Offset: 0x002FAFE8
		private void OnClickScriptPanel()
		{
			if (this.m_objScriptType2.activeSelf)
			{
				return;
			}
			if (this.m_typeWriter.IsTyping())
			{
				this.m_typeWriter.Finish();
				return;
			}
			if (this.m_scriptCoroutine != null)
			{
				base.StopCoroutine(this.m_scriptCoroutine);
				this.m_scriptCoroutine = null;
			}
			this.HideScript();
		}

		// Token: 0x06008C88 RID: 35976 RVA: 0x002FCE3D File Offset: 0x002FB03D
		private void OnDestroy()
		{
			if (this.cockTailIDList != null)
			{
				this.cockTailIDList.Clear();
				this.cockTailIDList = null;
			}
			this.m_typeWriter = null;
			if (this.m_scriptCoroutine != null)
			{
				base.StopCoroutine(this.m_scriptCoroutine);
				this.m_scriptCoroutine = null;
			}
		}

		// Token: 0x0400794F RID: 31055
		public NKCUIEventBarCocktailSlot[] m_EventBarCocktailSlotArray;

		// Token: 0x04007950 RID: 31056
		public NKCUINPCSpineIllust m_npcSpineIllust;

		// Token: 0x04007951 RID: 31057
		public GameObject m_objBarLock;

		// Token: 0x04007952 RID: 31058
		public GameObject m_objLaughFx;

		// Token: 0x04007953 RID: 31059
		public ScrollRect m_scrollRect;

		// Token: 0x04007954 RID: 31060
		public NKCUIComStateButton m_csbtnMomoErrand;

		// Token: 0x04007955 RID: 31061
		public GameObject m_objErrandRedDot;

		// Token: 0x04007956 RID: 31062
		[Header("리퀘스트 대사 창 유지 시간")]
		public float m_showScriptTime;

		// Token: 0x04007957 RID: 31063
		[Header("바 유닛 ID")]
		public int m_unitID;

		// Token: 0x04007958 RID: 31064
		[Header("칵테일 버블")]
		public NKCUIEventBarBubble m_eventBarBubble;

		// Token: 0x04007959 RID: 31065
		[Header("대사 창")]
		public Animator m_aniScript;

		// Token: 0x0400795A RID: 31066
		public CanvasGroup m_scriptCanvasGroup;

		// Token: 0x0400795B RID: 31067
		public GameObject m_objScriptRoot;

		// Token: 0x0400795C RID: 31068
		public GameObject m_objScriptType1;

		// Token: 0x0400795D RID: 31069
		public GameObject m_objScriptType2;

		// Token: 0x0400795E RID: 31070
		public Text m_lbUnitName;

		// Token: 0x0400795F RID: 31071
		public Text m_lbType1Msg;

		// Token: 0x04007960 RID: 31072
		public Text m_lbType2Msg;

		// Token: 0x04007961 RID: 31073
		public NKCUIComStateButton m_csbtnGive;

		// Token: 0x04007962 RID: 31074
		public NKCUIComStateButton m_csbtnStay;

		// Token: 0x04007963 RID: 31075
		public NKCUIComStateButton m_csbtnScriptPanel;

		// Token: 0x04007964 RID: 31076
		[Header("모모 공물 보상 미션 TabId")]
		public int m_errandRewardMissionTabId;

		// Token: 0x04007965 RID: 31077
		[Header("칵테일 재료 보상 미션 GroupId")]
		public int m_cocktailRewardMissionGroupId;

		// Token: 0x04007966 RID: 31078
		private NKCUITypeWriter m_typeWriter = new NKCUITypeWriter();

		// Token: 0x04007967 RID: 31079
		private List<int> cockTailIDList = new List<int>();

		// Token: 0x04007968 RID: 31080
		private int m_eventID;

		// Token: 0x04007969 RID: 31081
		private int m_selectedCocktailID;

		// Token: 0x0400796A RID: 31082
		private float m_fScriptTimer;

		// Token: 0x0400796B RID: 31083
		private Coroutine m_scriptCoroutine;

		// Token: 0x0400796C RID: 31084
		private bool showDeliverFinishScript;
	}
}
