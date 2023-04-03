using System;
using DG.Tweening;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000977 RID: 2423
	public class NKCUICCNormalSlot : MonoBehaviour
	{
		// Token: 0x06006229 RID: 25129 RVA: 0x001EC9E9 File Offset: 0x001EABE9
		public void SetOnSelectedItemSlot(NKCUICCNormalSlot.OnSelectedCCSlot _OnSelectedSlot)
		{
			this.m_OnSelectedSlot = _OnSelectedSlot;
		}

		// Token: 0x0600622A RID: 25130 RVA: 0x001EC9F2 File Offset: 0x001EABF2
		public int GetStageIndex()
		{
			return this.m_StageIndex;
		}

		// Token: 0x0600622B RID: 25131 RVA: 0x001EC9FA File Offset: 0x001EABFA
		public bool GetLock()
		{
			return this.m_bLock;
		}

		// Token: 0x0600622C RID: 25132 RVA: 0x001ECA02 File Offset: 0x001EAC02
		private void OnDestroy()
		{
			NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceData);
		}

		// Token: 0x0600622D RID: 25133 RVA: 0x001ECA10 File Offset: 0x001EAC10
		public static NKCUICCNormalSlot GetNewInstance(Transform parent, NKCUICCNormalSlot.OnSelectedCCSlot dOnSelectedItemSlot)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_COUNTER_CASE", "NKM_UI_COUNTER_CASE_NORMAL_SLOT", false, null);
			NKCUICCNormalSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUICCNormalSlot>();
			if (component == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKCUICCNormalSlot Prefab null!");
				return null;
			}
			component.m_NKCAssetInstanceData = nkcassetInstanceData;
			component.SetOnSelectedItemSlot(dOnSelectedItemSlot);
			component.m_NKM_UI_COUNTER_CASE_NORMAL_SLOT_BUTTON_02_Btn.PointerClick.RemoveAllListeners();
			component.m_NKM_UI_COUNTER_CASE_NORMAL_SLOT_BUTTON_02_Btn.PointerClick.AddListener(new UnityAction(component.OnSelectedSlotImpl));
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.transform.localPosition = new Vector3(component.transform.localPosition.x, component.transform.localPosition.y, 0f);
			component.m_NKCUISlot.Init();
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x0600622E RID: 25134 RVA: 0x001ECAF2 File Offset: 0x001EACF2
		public void OnSelectedSlotImpl()
		{
			if (this.m_OnSelectedSlot != null)
			{
				this.m_OnSelectedSlot(this.m_StageIndex, this.m_StageBattleStrID);
			}
		}

		// Token: 0x0600622F RID: 25135 RVA: 0x001ECB14 File Offset: 0x001EAD14
		public void SetData(NKMStageTempletV2 stageTemplet, int dungeonIDForBtnAni = -1)
		{
			if (stageTemplet == null)
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			this.m_bLock = (!NKMEpisodeMgr.CheckEpisodeMission(myUserData, stageTemplet) && !NKMEpisodeMgr.CheckClear(myUserData, stageTemplet));
			this.m_StageIndex = stageTemplet.m_StageIndex;
			this.m_StageBattleStrID = stageTemplet.m_StageBattleStrID;
			base.gameObject.SetActive(true);
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(this.m_StageBattleStrID);
			if (dungeonTempletBase == null)
			{
				return;
			}
			this.m_NKM_UI_COUNTER_CASE_NORMAL_SLOT_TITLE.text = dungeonTempletBase.GetDungeonName();
			this.m_NKM_UI_COUNTER_CASE_NORMAL_SLOT_NUMBER.text = stageTemplet.m_StageUINum.ToString();
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COUNTER_CASE_NORMAL_SLOT_LOCK, this.m_bLock);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COUNTER_CASE_NORMAL_SLOT_BUTTON_01, this.m_bLock);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COUNTER_CASE_NORMAL_SLOT_BUTTON_02, !this.m_bLock);
			this.m_NKM_UI_COUNTER_CASE_NORMAL_SLOT_BUTTON_TEXT.text = NKCUtilString.GET_STRING_COUNTER_CASE_SLOT_BUTTON_LOCK;
			if (this.m_bLock)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COUNTER_CASE_NORMAL_SLOT_RESOURCE, false);
				NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT, false);
				this.m_NKM_UI_COUNTER_CASE_NORMAL_SLOT_TEXT.text = "";
				this.m_NKM_UI_COUNTER_CASE_NORMAL_SLOT_LOCK_TEXT.text = NKCUtilString.GetUnlockConditionRequireDesc(stageTemplet, false);
				return;
			}
			bool completeMark = NKMEpisodeMgr.CheckClear(myUserData, stageTemplet);
			bool flag = myUserData.CheckUnlockedCounterCase(dungeonTempletBase.m_DungeonID);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COUNTER_CASE_NORMAL_SLOT_RESOURCE, !flag);
			this.m_NKM_UI_COUNTER_CASE_NORMAL_SLOT_TEXT.text = dungeonTempletBase.GetDungeonDesc();
			if (!flag)
			{
				this.m_RESOURCE_TEXT.text = ((stageTemplet.UnlockReqItem != null) ? stageTemplet.UnlockReqItem.Count32.ToString() : stageTemplet.m_StageReqItemCount.ToString());
			}
			else
			{
				this.m_NKM_UI_COUNTER_CASE_NORMAL_SLOT_BUTTON_TEXT.text = NKCUtilString.GET_STRING_COUNTER_CASE_SLOT_BUTTON_UNLOCK;
				if (dungeonIDForBtnAni == dungeonTempletBase.m_DungeonID)
				{
					this.m_DOTweenAnimation.DORestart();
					NKCSoundManager.PlaySound("FX_UI_COUNTERCASE_DUNGEON_OPEN", 1f, 0f, 0f, false, 0f, false, 0f);
				}
			}
			FirstRewardData firstRewardData = dungeonTempletBase.GetFirstRewardData();
			if (firstRewardData.Type == NKM_REWARD_TYPE.RT_NONE)
			{
				NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT, true);
			NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(firstRewardData.Type, firstRewardData.RewardId, firstRewardData.RewardQuantity, 0);
			this.m_NKCUISlot.SetData(data, true, null);
			this.m_NKCUISlot.SetCompleteMark(completeMark);
		}

		// Token: 0x06006230 RID: 25136 RVA: 0x001ECD4A File Offset: 0x001EAF4A
		public void SetAlphaAni(int index)
		{
			this.m_CanvasGroup.alpha = 0f;
			this.m_fAlphaAniStartTime = Time.time + (float)index * 0.125f;
		}

		// Token: 0x06006231 RID: 25137 RVA: 0x001ECD70 File Offset: 0x001EAF70
		private void Update()
		{
			if (this.m_fAlphaAniStartTime < Time.time && this.m_CanvasGroup.alpha < 1f)
			{
				this.m_CanvasGroup.alpha += Time.deltaTime * 2.5f;
				if (this.m_CanvasGroup.alpha >= 1f)
				{
					this.m_CanvasGroup.alpha = 1f;
					this.m_fAlphaAniStartTime = float.MaxValue;
				}
			}
		}

		// Token: 0x06006232 RID: 25138 RVA: 0x001ECDE6 File Offset: 0x001EAFE6
		public void SetActive(bool bSet)
		{
			if (base.gameObject.activeSelf == !bSet)
			{
				base.gameObject.SetActive(bSet);
			}
		}

		// Token: 0x06006233 RID: 25139 RVA: 0x001ECE05 File Offset: 0x001EB005
		public bool IsActive()
		{
			return base.gameObject.activeSelf;
		}

		// Token: 0x06006234 RID: 25140 RVA: 0x001ECE12 File Offset: 0x001EB012
		public RectTransform GetBtnRect()
		{
			return this.m_NKM_UI_COUNTER_CASE_NORMAL_SLOT_BUTTON_02_Btn.GetComponent<RectTransform>();
		}

		// Token: 0x04004E19 RID: 19993
		public Text m_NKM_UI_COUNTER_CASE_NORMAL_SLOT_TITLE;

		// Token: 0x04004E1A RID: 19994
		public Text m_NKM_UI_COUNTER_CASE_NORMAL_SLOT_TEXT;

		// Token: 0x04004E1B RID: 19995
		public Text m_NKM_UI_COUNTER_CASE_NORMAL_SLOT_NUMBER;

		// Token: 0x04004E1C RID: 19996
		public GameObject m_NKM_UI_COUNTER_CASE_NORMAL_SLOT_NEW;

		// Token: 0x04004E1D RID: 19997
		public GameObject m_NKM_UI_COUNTER_CASE_NORMAL_SLOT_RESOURCE;

		// Token: 0x04004E1E RID: 19998
		public Text m_RESOURCE_TEXT;

		// Token: 0x04004E1F RID: 19999
		public GameObject m_AB_ICON_SLOT;

		// Token: 0x04004E20 RID: 20000
		public NKCUISlot m_NKCUISlot;

		// Token: 0x04004E21 RID: 20001
		public GameObject m_NKM_UI_COUNTER_CASE_NORMAL_SLOT_BUTTON_01;

		// Token: 0x04004E22 RID: 20002
		public GameObject m_NKM_UI_COUNTER_CASE_NORMAL_SLOT_BUTTON_02;

		// Token: 0x04004E23 RID: 20003
		public NKCUIComButton m_NKM_UI_COUNTER_CASE_NORMAL_SLOT_BUTTON_02_Btn;

		// Token: 0x04004E24 RID: 20004
		public Text m_NKM_UI_COUNTER_CASE_NORMAL_SLOT_BUTTON_TEXT;

		// Token: 0x04004E25 RID: 20005
		public GameObject m_NKM_UI_COUNTER_CASE_NORMAL_SLOT_LOCK;

		// Token: 0x04004E26 RID: 20006
		public Text m_NKM_UI_COUNTER_CASE_NORMAL_SLOT_LOCK_TEXT;

		// Token: 0x04004E27 RID: 20007
		public CanvasGroup m_CanvasGroup;

		// Token: 0x04004E28 RID: 20008
		public DOTweenAnimation m_DOTweenAnimation;

		// Token: 0x04004E29 RID: 20009
		private float m_fAlphaAniStartTime = float.MaxValue;

		// Token: 0x04004E2A RID: 20010
		private NKCUICCNormalSlot.OnSelectedCCSlot m_OnSelectedSlot;

		// Token: 0x04004E2B RID: 20011
		private int m_StageIndex;

		// Token: 0x04004E2C RID: 20012
		private string m_StageBattleStrID = "";

		// Token: 0x04004E2D RID: 20013
		private bool m_bLock;

		// Token: 0x04004E2E RID: 20014
		private NKCAssetInstanceData m_NKCAssetInstanceData;

		// Token: 0x02001622 RID: 5666
		// (Invoke) Token: 0x0600AF4A RID: 44874
		public delegate void OnSelectedCCSlot(int stageIndex, string stageBattleStrID);
	}
}
