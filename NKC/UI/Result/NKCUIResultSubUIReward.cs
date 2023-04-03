using System;
using System.Collections;
using System.Collections.Generic;
using ClientPacket.Common;
using DG.Tweening;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Result
{
	// Token: 0x02000BA3 RID: 2979
	public class NKCUIResultSubUIReward : NKCUIResultSubUIBase
	{
		// Token: 0x060089BC RID: 35260 RVA: 0x002EAF82 File Offset: 0x002E9182
		public void SetReservedDoubleTokenAddCount(long count)
		{
			this.m_bReservedDoubleTokenAddCount = true;
			this.m_DoubleTokenAddCount = count;
		}

		// Token: 0x060089BD RID: 35261 RVA: 0x002EAF94 File Offset: 0x002E9194
		public void Init(Text lbDoubleTokenCount, Animator amtorDoubleToken)
		{
			this.m_amtorDoubleToken = amtorDoubleToken;
			this.m_animator = base.transform.GetComponent<Animator>();
			if (this.m_animator != null)
			{
				this.m_animator.speed = 1.5f;
			}
			this.m_loopRewardList = base.transform.Find("MASK/LoopScroll").GetComponent<LoopVerticalScrollRect>();
			this.m_trSlotObjectRoot = base.transform.Find("MASK/LoopScroll/item");
			this.m_trIdleObjectRoot = base.transform.Find("IdleObjectParent");
			this.m_lstSlot = new List<NKCUIResultRewardSlot>();
			this.m_stkSlotIdle = new Stack<NKCUIResultRewardSlot>();
			this.m_lstRewardSlotData = new List<NKCUISlot.SlotData>();
			this.m_iSlotCount = 0;
			this.m_loopRewardList.dOnGetObject += this.GetObject;
			this.m_loopRewardList.dOnReturnObject += this.ReturnObject;
			this.m_loopRewardList.dOnProvideData += this.ProvideData;
			this.m_loopRewardList.ContentConstraintCount = 5;
			this.m_loopRewardList.PrepareCells(0);
			NKCUtil.SetScrollHotKey(this.m_loopRewardList, null);
			this.m_orgSlotRootPos = this.m_trSlotObjectRoot.localPosition;
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerDown;
			entry.callback = new EventTrigger.TriggerEvent();
			entry.callback.AddListener(new UnityAction<BaseEventData>(this.OnUserInputEvent));
			this.m_eventTrigger = base.gameObject.GetComponent<EventTrigger>();
			this.m_eventTrigger.triggers.Add(entry);
			this.m_assetName = "FX_UI_ITEM_RESULT_LISTUP";
			this.m_lbDoubleTokenCount = lbDoubleTokenCount;
		}

		// Token: 0x060089BE RID: 35262 RVA: 0x002EB123 File Offset: 0x002E9323
		public void OnUserInputByResult()
		{
			if (!this.m_bWaitForUnitGain)
			{
				this.m_bHadUserInput = true;
			}
		}

		// Token: 0x060089BF RID: 35263 RVA: 0x002EB134 File Offset: 0x002E9334
		private void OnUserInputEvent(BaseEventData eventData)
		{
			if (!this.m_bWaitForUnitGain)
			{
				this.m_bHadUserInput = true;
			}
		}

		// Token: 0x060089C0 RID: 35264 RVA: 0x002EB145 File Offset: 0x002E9345
		public override bool IsProcessFinished()
		{
			return this.m_bFinished;
		}

		// Token: 0x060089C1 RID: 35265 RVA: 0x002EB14D File Offset: 0x002E934D
		protected override IEnumerator InnerProcess(bool bAutoSkip)
		{
			this.m_bFinished = false;
			this.m_bAutoSkip = bAutoSkip;
			this.m_animator.enabled = false;
			this.m_bHadUserInput = false;
			this.m_canvasGroup.blocksRaycasts = false;
			NKCUtil.SetGameobjectActive(this.m_amtorDoubleToken.gameObject, false);
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_lstSlot[i], false);
				this.m_lstSlot[i].SetEffectEnable(false);
				this.m_lstSlot[i].m_NKCUISlot.m_lbItemCount.DOKill(false);
				if (this.m_bReservedDoubleTokenAddCount && this.m_lstSlot[i].m_NKCUISlot.GetSlotData() != null && this.m_lstSlot[i].m_NKCUISlot.GetSlotData().ID == 5)
				{
					this.m_lstSlot[i].m_NKCUISlot.m_lbItemCount.text = ((long)this.m_lstSlot[i].m_NKCUISlot.GetCount() - this.m_DoubleTokenAddCount).ToString();
					break;
				}
			}
			yield return base.WaitTimeOrUserInput(0.2f);
			while (this.m_queueSlot.Count > 0)
			{
				NKCUIResultRewardSlot slot = this.m_queueSlot.Dequeue();
				int index = slot.GetIdx();
				if (index > this.m_iLastIndex)
				{
					if (index < this.m_lstRewardSlotData.Count)
					{
						if (this.m_bHadUserInput)
						{
							this.ShowAllSlot();
							break;
						}
						this.m_fElapsedTime = 0f;
						NKCUtil.SetGameobjectActive(slot, true);
						slot.SetEffectEnable(true);
						NKCSoundManager.PlaySound(this.m_assetName, 1f, base.transform.position.x, 0f, false, 0f, false, 0f);
						slot.GetComponent<CanvasGroup>().alpha = 0.1f;
						if (index % this.m_loopRewardList.ContentConstraintCount == 0 && index > this.m_loopRewardList.ContentConstraintCount)
						{
							yield return null;
							this.m_loopRewardList.ScrollToCell(index, 0.4f, LoopScrollRect.ScrollTarget.Center, null);
							yield return base.WaitTimeOrUserInput(0.1f);
						}
						while (this.m_fElapsedTime < 0.1f)
						{
							this.m_fElapsedTime += Time.deltaTime;
							float num = this.m_fElapsedTime / 0.1f;
							if (num < 0f)
							{
								num = 0f;
							}
							if (num >= 1f)
							{
								num = 1f;
							}
							slot.GetComponent<CanvasGroup>().alpha = num;
							yield return null;
						}
					}
					this.m_iLastIndex = index;
					if (index == this.m_loopRewardList.TotalCount - 1)
					{
						break;
					}
					slot = null;
				}
			}
			if (this.m_bReservedDoubleTokenAddCount)
			{
				yield return new WaitForSeconds(0.8f);
				NKCUtil.SetGameobjectActive(this.m_amtorDoubleToken.gameObject, true);
				this.m_amtorDoubleToken.Play("NKM_UI_RESULT_PVPPOINTX2_INTRO");
				yield return new WaitForSeconds(0.8f);
				long countMiscItem = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetCountMiscItem(301);
				if (this.m_lbDoubleTokenCount.text.CompareTo(countMiscItem.ToString()) != 0)
				{
					this.m_lbDoubleTokenCount.DOText(countMiscItem.ToString(), 0.4f, true, ScrambleMode.Numerals, null);
				}
				for (int j = 0; j < this.m_lstSlot.Count; j++)
				{
					NKCUIResultRewardSlot nkcuiresultRewardSlot = this.m_lstSlot[j];
					if (!(nkcuiresultRewardSlot == null) && nkcuiresultRewardSlot.m_NKCUISlot.GetSlotData() != null && nkcuiresultRewardSlot.m_NKCUISlot.GetSlotData().ID == 5)
					{
						nkcuiresultRewardSlot.m_NKCUISlot.SetNewCountAni(this.m_DoubleTokenAddCount + (long)nkcuiresultRewardSlot.m_NKCUISlot.GetCount(), 0.4f);
						break;
					}
				}
				yield return new WaitForSeconds(1.4f);
			}
			this.FinishProcess();
			yield break;
		}

		// Token: 0x060089C2 RID: 35266 RVA: 0x002EB164 File Offset: 0x002E9364
		public override void FinishProcess()
		{
			if (!base.gameObject.activeInHierarchy || this.bWaiting)
			{
				return;
			}
			this.m_bNeedEnqueue = false;
			this.bWaiting = true;
			base.StopAllCoroutines();
			this.ShowAllSlot();
			if (this.m_bReservedDoubleTokenAddCount)
			{
				NKCUtil.SetGameobjectActive(this.m_amtorDoubleToken.gameObject, true);
				this.m_amtorDoubleToken.Play("NKM_UI_RESULT_PVPPOINTX2_IDLE");
				long countMiscItem = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetCountMiscItem(301);
				if (DOTween.IsTweening(this.m_lbDoubleTokenCount, false))
				{
					this.m_lbDoubleTokenCount.DOKill(false);
				}
				if (this.m_lbDoubleTokenCount.text.CompareTo(countMiscItem.ToString()) != 0)
				{
					this.m_lbDoubleTokenCount.text = countMiscItem.ToString();
				}
			}
			this.m_canvasGroup.blocksRaycasts = true;
			this.m_bHadUserInput = false;
			base.StartCoroutine(this.WaitForCloseAnimation());
		}

		// Token: 0x060089C3 RID: 35267 RVA: 0x002EB250 File Offset: 0x002E9450
		public IEnumerator WaitForCloseAnimation()
		{
			this.m_bHadUserInput = false;
			if (this.m_bWillPlayCountdown)
			{
				yield return base.WaitTimeOrUserInput(1f);
			}
			else
			{
				yield return base.WaitAniOrInput(null);
			}
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				this.m_lstSlot[i].SetEffectEnable(false);
			}
			this.m_animator.enabled = true;
			if (NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetAlarmRepeatOperationSuccess())
			{
				this.m_bPause = true;
				NKCScenManager.GetScenManager().GetNKCRepeatOperaion().SetAlarmRepeatOperationSuccess(false);
				NKCScenManager.GetScenManager().GetNKCRepeatOperaion().Init();
				NKCScenManager.GetScenManager().GetNKCRepeatOperaion().SetStopReason(NKCUtilString.GET_STRING_REPEAT_OPERATION_IS_TERMINATED);
				NKCPopupRepeatOperation.Instance.OpenForResult(delegate
				{
					this.m_bPause = false;
				});
			}
			while (this.m_bPause)
			{
				yield return null;
			}
			if (this.m_bReservedDoubleTokenAddCount)
			{
				NKCUtil.SetGameobjectActive(this.m_amtorDoubleToken.gameObject, true);
				this.m_amtorDoubleToken.Play("NKM_UI_RESULT_PVPPOINTX2_OUTRO");
			}
			if (!this.m_bWillPlayCountdown)
			{
				yield return base.PlayCloseAnimation(this.m_animator);
			}
			this.m_bFinished = true;
			this.bWaiting = false;
			yield break;
		}

		// Token: 0x060089C4 RID: 35268 RVA: 0x002EB260 File Offset: 0x002E9460
		private void ShowAllSlot()
		{
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				if (i < this.m_iSlotCount)
				{
					NKCUtil.SetGameobjectActive(this.m_lstSlot[i], true);
					this.m_lstSlot[i].GetComponent<CanvasGroup>().alpha = 1f;
					this.m_lstSlot[i].SetEffectEnable(true);
				}
			}
			this.m_loopRewardList.SetIndexPosition(this.m_loopRewardList.TotalCount - 1);
		}

		// Token: 0x060089C5 RID: 35269 RVA: 0x002EB2E3 File Offset: 0x002E94E3
		public override void Close()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.CleanUpData();
		}

		// Token: 0x060089C6 RID: 35270 RVA: 0x002EB2F8 File Offset: 0x002E94F8
		private void CleanUpData()
		{
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				this.m_lstSlot[i].SetEffectEnable(false);
			}
			this.m_queueSlot.Clear();
			this.m_loopRewardList.PrepareCells(0);
			this.m_canvasGroup.alpha = 1f;
			this.m_lstRewardSlotData.Clear();
			this.m_bReservedDoubleTokenAddCount = false;
		}

		// Token: 0x060089C7 RID: 35271 RVA: 0x002EB368 File Offset: 0x002E9568
		public void SetData(NKCUIResultSubUIReward.Data data)
		{
			this.m_bReservedDoubleTokenAddCount = false;
			if (data == null)
			{
				base.ProcessRequired = false;
				return;
			}
			if (!data.bAllowRewardDataNull && data.rewardData == null)
			{
				base.ProcessRequired = false;
				return;
			}
			this.m_bIgnoreAutoClose = data.bIgnoreAutoClose;
			this.m_canvasGroup.alpha = 1f;
			this.m_trSlotObjectRoot.localPosition = this.m_orgSlotRootPos;
			this.m_lstRewardSlotData.Clear();
			NKCUtil.SetGameobjectActive(this.m_objRefund, true);
			if (data.battleResultType == BATTLE_RESULT_TYPE.BRT_LOSE)
			{
				NKCUtil.SetGameobjectActive(this.m_objRefund, true);
				this.m_loopRewardList.scrollSensitivity = 0f;
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objRefund, false);
				this.m_loopRewardList.scrollSensitivity = 1f;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_iFirstRewardCount = 0;
			if (data.firstRewardData != null)
			{
				this.m_lstRewardSlotData.AddRange(NKCUISlot.MakeSlotDataListFromReward(data.firstRewardData, false, false));
				this.m_iFirstRewardCount = this.m_lstRewardSlotData.Count;
			}
			this.m_iFirstAllClearRewardCnt = 0;
			if (data.firstAllClearRewardData != null)
			{
				List<NKCUISlot.SlotData> list = NKCUISlot.MakeSlotDataListFromReward(data.firstAllClearRewardData, false, false);
				this.m_iFirstAllClearRewardCnt = list.Count;
				this.m_lstRewardSlotData.AddRange(list);
			}
			this.m_iOnetimeRewardCount = 0;
			if (data.onetimeRewardData != null)
			{
				List<NKCUISlot.SlotData> list2 = NKCUISlot.MakeSlotDataListFromReward(data.onetimeRewardData, false, false);
				this.m_iOnetimeRewardCount = list2.Count;
				this.m_lstRewardSlotData.AddRange(list2);
			}
			if (data.rewardData != null)
			{
				this.m_lstRewardSlotData.AddRange(NKCUISlot.MakeSlotDataListFromReward(data.rewardData, false, false));
			}
			if (data.additionalReward != null)
			{
				this.m_lstRewardSlotData.AddRange(NKCUISlot.MakeSlotDataListFromReward(data.additionalReward));
			}
			this.m_iSelectRewardCount = 0;
			this.m_strSelectRewardText = data.selectSlotText;
			if (data.selectRewardData != null)
			{
				List<NKCUISlot.SlotData> list3 = NKCUISlot.MakeSlotDataListFromReward(data.selectRewardData, false, false);
				this.m_lstRewardSlotData.AddRange(list3);
				this.m_iSelectRewardCount = list3.Count;
			}
			this.m_iSlotCount = this.m_lstRewardSlotData.Count;
			if (!data.bAllowRewardDataNull)
			{
				base.ProcessRequired = (this.m_iSlotCount != 0);
			}
			else
			{
				base.ProcessRequired = true;
			}
			if (this.m_iSlotCount > 0)
			{
				this.m_bNeedEnqueue = base.ProcessRequired;
			}
			else
			{
				this.m_bNeedEnqueue = false;
			}
			this.m_iLastIndex = -1;
			this.m_loopRewardList.TotalCount = this.m_iSlotCount;
			this.m_loopRewardList.RefreshCells(false);
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				this.m_lstSlot[i].GetComponent<CanvasGroup>().alpha = 0f;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060089C8 RID: 35272 RVA: 0x002EB604 File Offset: 0x002E9804
		public void SetBoxGainData(List<NKCUISlot.SlotData> lstReward)
		{
			if (lstReward == null || lstReward.Count == 0)
			{
				base.ProcessRequired = false;
				return;
			}
			this.m_bReservedDoubleTokenAddCount = false;
			this.m_canvasGroup.alpha = 1f;
			this.m_trSlotObjectRoot.localPosition = this.m_orgSlotRootPos;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_objRefund, false);
			this.m_iFirstRewardCount = 0;
			this.m_iFirstAllClearRewardCnt = 0;
			this.m_iOnetimeRewardCount = 0;
			this.m_iSelectRewardCount = 0;
			this.m_strSelectRewardText = "";
			this.m_lstRewardSlotData = lstReward;
			this.m_iSlotCount = ((this.m_lstRewardSlotData != null) ? this.m_lstRewardSlotData.Count : 0);
			base.ProcessRequired = (this.m_iSlotCount != 0);
			this.m_bNeedEnqueue = base.ProcessRequired;
			this.m_iLastIndex = -1;
			this.m_loopRewardList.TotalCount = this.m_iSlotCount;
			this.m_loopRewardList.RefreshCells(false);
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				this.m_lstSlot[i].GetComponent<CanvasGroup>().alpha = 0f;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060089C9 RID: 35273 RVA: 0x002EB72A File Offset: 0x002E992A
		public void SetActiveLoopScrollList(bool bActivate)
		{
			this.m_loopRewardList.enabled = bActivate;
			if (bActivate)
			{
				this.m_loopRewardList.RefreshCells(false);
			}
		}

		// Token: 0x060089CA RID: 35274 RVA: 0x002EB748 File Offset: 0x002E9948
		private RectTransform GetObject(int index)
		{
			if (this.m_stkSlotIdle.Count > 0)
			{
				NKCUIResultRewardSlot nkcuiresultRewardSlot = this.m_stkSlotIdle.Pop();
				NKCUtil.SetGameobjectActive(nkcuiresultRewardSlot, true);
				this.m_lstSlot.Add(nkcuiresultRewardSlot);
				nkcuiresultRewardSlot.SetEffectEnable(false);
				return nkcuiresultRewardSlot.GetComponent<RectTransform>();
			}
			NKCUIResultRewardSlot nkcuiresultRewardSlot2 = UnityEngine.Object.Instantiate<NKCUIResultRewardSlot>(this.m_pfbSlot);
			nkcuiresultRewardSlot2.Init();
			nkcuiresultRewardSlot2.transform.localScale = Vector3.one;
			nkcuiresultRewardSlot2.transform.localPosition = Vector3.zero;
			nkcuiresultRewardSlot2.gameObject.AddComponent<CanvasGroup>();
			NKCUtil.SetGameobjectActive(nkcuiresultRewardSlot2, true);
			this.m_lstSlot.Add(nkcuiresultRewardSlot2);
			return nkcuiresultRewardSlot2.GetComponent<RectTransform>();
		}

		// Token: 0x060089CB RID: 35275 RVA: 0x002EB7E8 File Offset: 0x002E99E8
		private void ReturnObject(Transform go)
		{
			NKCUtil.SetGameobjectActive(go, false);
			go.SetParent(this.m_trIdleObjectRoot);
			NKCUIResultRewardSlot component = go.GetComponent<NKCUIResultRewardSlot>();
			component.SetEffectEnable(false);
			if (this.m_lstSlot.Contains(component))
			{
				this.m_lstSlot.Remove(component);
			}
			if (!this.m_stkSlotIdle.Contains(component))
			{
				this.m_stkSlotIdle.Push(component);
			}
		}

		// Token: 0x060089CC RID: 35276 RVA: 0x002EB84C File Offset: 0x002E9A4C
		private void ProvideData(Transform transform, int idx)
		{
			NKCUIResultRewardSlot component = transform.GetComponent<NKCUIResultRewardSlot>();
			if (this.m_lstRewardSlotData.Count > idx)
			{
				component.SetData(this.m_lstRewardSlotData[idx], idx);
				component.SetFirstRewardMark(idx < this.m_iFirstRewardCount);
				component.SetFirstAllClearRewardMark(idx >= this.m_iFirstRewardCount && idx < this.m_iFirstAllClearRewardCnt + this.m_iFirstRewardCount);
				component.SetOnetimeRewardMark(idx >= this.m_iFirstRewardCount + this.m_iFirstAllClearRewardCnt && idx < this.m_iOnetimeRewardCount + this.m_iFirstRewardCount + this.m_iFirstAllClearRewardCnt);
				bool flag = idx >= this.m_lstRewardSlotData.Count - this.m_iSelectRewardCount;
				component.SetSelect(flag);
				component.SetText(flag ? this.m_strSelectRewardText : "");
				NKCUtil.SetGameobjectActive(component, true);
			}
			else
			{
				NKCUtil.SetGameobjectActive(component, false);
			}
			if (this.m_bNeedEnqueue && idx > this.m_iLastIndex)
			{
				component.SetEffectEnable(false);
				component.GetComponent<CanvasGroup>().alpha = 0f;
				this.m_queueSlot.Enqueue(component);
			}
			else
			{
				component.GetComponent<CanvasGroup>().alpha = 1f;
			}
			if (idx == this.m_loopRewardList.TotalCount)
			{
				this.m_bNeedEnqueue = false;
			}
		}

		// Token: 0x060089CD RID: 35277 RVA: 0x002EB986 File Offset: 0x002E9B86
		public override void OnUserInput()
		{
			if (!this.m_bWaitForUnitGain)
			{
				this.m_bHadUserInput = true;
			}
		}

		// Token: 0x04007624 RID: 30244
		private const string REWARD_SOUND_BUNDLE_NAME = "ab_sound";

		// Token: 0x04007625 RID: 30245
		private const string REWARD_SOUND_ASSET_NAME = "FX_UI_ITEM_RESULT_LISTUP";

		// Token: 0x04007626 RID: 30246
		private const float SLOT_APPEAR_DELAY = 0.1f;

		// Token: 0x04007627 RID: 30247
		public NKCUIResultRewardSlot m_pfbSlot;

		// Token: 0x04007628 RID: 30248
		private Animator m_animator;

		// Token: 0x04007629 RID: 30249
		public CanvasGroup m_canvasGroup;

		// Token: 0x0400762A RID: 30250
		private LoopVerticalScrollRect m_loopRewardList;

		// Token: 0x0400762B RID: 30251
		private Transform m_trSlotObjectRoot;

		// Token: 0x0400762C RID: 30252
		private Transform m_trIdleObjectRoot;

		// Token: 0x0400762D RID: 30253
		public GameObject m_objRefund;

		// Token: 0x0400762E RID: 30254
		private List<NKCUIResultRewardSlot> m_lstSlot;

		// Token: 0x0400762F RID: 30255
		private Stack<NKCUIResultRewardSlot> m_stkSlotIdle;

		// Token: 0x04007630 RID: 30256
		private List<NKCUISlot.SlotData> m_lstRewardSlotData;

		// Token: 0x04007631 RID: 30257
		private EventTrigger m_eventTrigger;

		// Token: 0x04007632 RID: 30258
		private int m_iSlotCount;

		// Token: 0x04007633 RID: 30259
		private int m_iFirstRewardCount;

		// Token: 0x04007634 RID: 30260
		private int m_iFirstAllClearRewardCnt;

		// Token: 0x04007635 RID: 30261
		private int m_iOnetimeRewardCount;

		// Token: 0x04007636 RID: 30262
		private int m_iSelectRewardCount;

		// Token: 0x04007637 RID: 30263
		private string m_strSelectRewardText;

		// Token: 0x04007638 RID: 30264
		private bool m_bFinished;

		// Token: 0x04007639 RID: 30265
		private bool m_bWaitForUnitGain;

		// Token: 0x0400763A RID: 30266
		private Vector3 m_orgSlotRootPos;

		// Token: 0x0400763B RID: 30267
		private string m_assetName = "";

		// Token: 0x0400763C RID: 30268
		private bool m_bAutoSkip;

		// Token: 0x0400763D RID: 30269
		private Queue<NKCUIResultRewardSlot> m_queueSlot = new Queue<NKCUIResultRewardSlot>();

		// Token: 0x0400763E RID: 30270
		private bool m_bNeedEnqueue;

		// Token: 0x0400763F RID: 30271
		private int m_iLastIndex = -1;

		// Token: 0x04007640 RID: 30272
		private bool m_bReservedDoubleTokenAddCount;

		// Token: 0x04007641 RID: 30273
		private long m_DoubleTokenAddCount;

		// Token: 0x04007642 RID: 30274
		private Text m_lbDoubleTokenCount;

		// Token: 0x04007643 RID: 30275
		private Animator m_amtorDoubleToken;

		// Token: 0x04007644 RID: 30276
		private float m_fElapsedTime;

		// Token: 0x04007645 RID: 30277
		private bool bWaiting;

		// Token: 0x02001968 RID: 6504
		public class Data
		{
			// Token: 0x0400ABC0 RID: 43968
			public NKMRewardData rewardData;

			// Token: 0x0400ABC1 RID: 43969
			public NKMArmyData armyData;

			// Token: 0x0400ABC2 RID: 43970
			public bool bAutoSkipUnitGain;

			// Token: 0x0400ABC3 RID: 43971
			public NKMRewardData firstRewardData;

			// Token: 0x0400ABC4 RID: 43972
			public bool bIgnoreAutoClose;

			// Token: 0x0400ABC5 RID: 43973
			public NKMRewardData selectRewardData;

			// Token: 0x0400ABC6 RID: 43974
			public string selectSlotText = "";

			// Token: 0x0400ABC7 RID: 43975
			public bool bAllowRewardDataNull;

			// Token: 0x0400ABC8 RID: 43976
			public NKMRewardData onetimeRewardData;

			// Token: 0x0400ABC9 RID: 43977
			public NKMRewardData firstAllClearRewardData;

			// Token: 0x0400ABCA RID: 43978
			public NKMAdditionalReward additionalReward;

			// Token: 0x0400ABCB RID: 43979
			public BATTLE_RESULT_TYPE battleResultType;
		}
	}
}
