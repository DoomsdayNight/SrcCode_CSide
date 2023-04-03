using System;
using System.Collections;
using System.Collections.Generic;
using ClientPacket.Contract;
using NKC.UI.Contract;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Result
{
	// Token: 0x02000B9C RID: 2972
	public class NKCUIResultMiscContract : NKCUIResultSubUIBase
	{
		// Token: 0x06008973 RID: 35187 RVA: 0x002EA050 File Offset: 0x002E8250
		public void Init()
		{
			this.m_lstSlot = new List<NKCUIResultRewardSlot>();
			this.m_stkSlotIdle = new Stack<NKCUIResultRewardSlot>();
			this.m_lstRewardSlotData = new List<NKCUISlot.SlotData>();
			this.m_loopRewardList.dOnGetObject += this.GetObject;
			this.m_loopRewardList.dOnReturnObject += this.ReturnObject;
			this.m_loopRewardList.dOnProvideData += this.ProvideData;
			this.m_loopRewardList.ContentConstraintCount = 5;
			this.m_loopRewardList.PrepareCells(0);
			EventTrigger component = base.gameObject.GetComponent<EventTrigger>();
			if (component != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerDown;
				entry.callback = new EventTrigger.TriggerEvent();
				entry.callback.AddListener(new UnityAction<BaseEventData>(this.OnUserInputEvent));
				component.triggers.Add(entry);
			}
			this.m_animator = base.transform.GetComponent<Animator>();
			if (this.m_animator != null)
			{
				this.m_animator.speed = 1.5f;
			}
		}

		// Token: 0x06008974 RID: 35188 RVA: 0x002EA15A File Offset: 0x002E835A
		private void OnUserInputEvent(BaseEventData eventData)
		{
			if (!this.m_bWait)
			{
				this.m_bHadUserInput = true;
			}
		}

		// Token: 0x06008975 RID: 35189 RVA: 0x002EA16C File Offset: 0x002E836C
		public void SetData(List<MiscContractResult> lstContractResult)
		{
			this.m_lstContractResult = lstContractResult;
			this.contractIndex = 0;
			this.m_iLastIndex = -1;
			this.m_canvasGroup.alpha = 1f;
			this.m_lstRewardSlotData.Clear();
			base.ProcessRequired = false;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (this.m_lstContractResult != null)
			{
				foreach (MiscContractResult miscContractResult in this.m_lstContractResult)
				{
					if (miscContractResult != null && miscContractResult.units.Count > 0)
					{
						base.ProcessRequired = true;
						break;
					}
				}
			}
			this.m_bNeedEnqueue = base.ProcessRequired;
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06008976 RID: 35190 RVA: 0x002EA238 File Offset: 0x002E8438
		public void SetData(List<NKMRewardData> lstRewardData)
		{
			List<MiscContractResult> list = new List<MiscContractResult>();
			foreach (NKMRewardData nkmrewardData in lstRewardData)
			{
				if (((nkmrewardData != null) ? nkmrewardData.ContractList : null) != null)
				{
					list.AddRange(nkmrewardData.ContractList);
				}
			}
			if (list.Count > 0)
			{
				this.SetData(list);
				return;
			}
			base.ProcessRequired = false;
		}

		// Token: 0x06008977 RID: 35191 RVA: 0x002EA2B8 File Offset: 0x002E84B8
		public bool WillProcess()
		{
			if (this.m_lstContractResult == null)
			{
				return false;
			}
			this.m_iLastIndex = -1;
			while (this.contractIndex < this.m_lstContractResult.Count)
			{
				this.m_contractResult = this.m_lstContractResult[this.contractIndex];
				this.contractIndex++;
				if (this.m_contractResult != null && this.m_contractResult.units != null && this.m_contractResult.units.Count > 0)
				{
					this.m_lstRewardSlotData = new List<NKCUISlot.SlotData>(NKCUISlot.SlotData.MakeUnitData(this.m_contractResult.units, true));
					this.m_loopRewardList.TotalCount = 0;
					this.m_loopRewardList.RefreshCells(false);
					return true;
				}
			}
			this.m_contractResult = null;
			return false;
		}

		// Token: 0x06008978 RID: 35192 RVA: 0x002EA37C File Offset: 0x002E857C
		protected override IEnumerator InnerProcess(bool bAutoSkip)
		{
			this.m_bFinished = false;
			this.m_bAutoSkip = bAutoSkip;
			this.m_animator.enabled = false;
			this.m_bHadUserInput = false;
			this.m_canvasGroup.blocksRaycasts = false;
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_lstSlot[i], false);
				this.m_lstSlot[i].SetEffectEnable(false);
			}
			NKM_UNIT_GRADE nkm_UNIT_GRADE = NKM_UNIT_GRADE.NUG_N;
			bool bAwaken = false;
			foreach (NKMUnitData unitData in this.m_contractResult.units)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData);
				if (unitTempletBase.m_NKM_UNIT_GRADE > nkm_UNIT_GRADE)
				{
					nkm_UNIT_GRADE = unitTempletBase.m_NKM_UNIT_GRADE;
				}
				if (unitTempletBase.m_bAwaken)
				{
					bAwaken = true;
				}
			}
			this.m_bWait = true;
			NKMRewardData dummyReward = new NKMRewardData();
			dummyReward.SetUnitData(this.m_contractResult.units);
			NKCUIGameResultGetUnit.NKCUIGRGetUnitCallBack <>9__1;
			NKCUIContractSequence.Instance.Open(nkm_UNIT_GRADE, bAwaken, delegate
			{
				NKMRewardData dummyReward = dummyReward;
				NKCUIGameResultGetUnit.NKCUIGRGetUnitCallBack callBack;
				if ((callBack = <>9__1) == null)
				{
					callBack = (<>9__1 = delegate()
					{
						this.m_bWait = false;
					});
				}
				NKCUIGameResultGetUnit.ShowNewUnitGetUI(dummyReward, callBack, bAutoSkip, true, false);
			});
			while (this.m_bWait)
			{
				yield return null;
			}
			this.m_bNeedEnqueue = true;
			this.m_loopRewardList.TotalCount = this.m_lstRewardSlotData.Count;
			this.m_loopRewardList.RefreshCells(false);
			yield return new WaitForSeconds(0.2f);
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
							yield return this.ShowAllSlot();
							break;
						}
						this.m_fElapsedTime = 0f;
						NKCUtil.SetGameobjectActive(slot, true);
						slot.SetEffectEnable(true);
						NKCSoundManager.PlaySound("FX_UI_ITEM_RESULT_LISTUP", 1f, base.transform.position.x, 0f, false, 0f, false, 0f);
						slot.GetComponent<CanvasGroup>().alpha = 0.1f;
						if (index % this.m_loopRewardList.ContentConstraintCount == 0 && index > this.m_loopRewardList.ContentConstraintCount)
						{
							yield return null;
							this.m_loopRewardList.ScrollToCell(index, 0.4f, LoopScrollRect.ScrollTarget.Center, null);
							yield return new WaitForSeconds(0.1f);
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
			this.FinishProcess();
			yield break;
		}

		// Token: 0x06008979 RID: 35193 RVA: 0x002EA394 File Offset: 0x002E8594
		public override void FinishProcess()
		{
			if (!base.gameObject.activeInHierarchy || this.bWaiting)
			{
				return;
			}
			this.m_bNeedEnqueue = false;
			this.bWaiting = true;
			base.StopAllCoroutines();
			base.StartCoroutine(this.ShowAllSlot());
			this.m_canvasGroup.blocksRaycasts = true;
			this.m_bHadUserInput = false;
			base.StartCoroutine(this.WaitForCloseAnimation());
		}

		// Token: 0x0600897A RID: 35194 RVA: 0x002EA3F8 File Offset: 0x002E85F8
		public IEnumerator WaitForCloseAnimation()
		{
			this.m_bHadUserInput = false;
			this.m_fWaitTimeForCloseAnimation = 1f;
			if (this.m_bAutoSkip)
			{
				float currentTime = 0f;
				while (this.m_fWaitTimeForCloseAnimation > currentTime)
				{
					if (this.m_bHadUserInput)
					{
						currentTime += 1f;
						this.m_bHadUserInput = false;
					}
					if (!this.m_bPause)
					{
						currentTime += Time.deltaTime;
					}
					float fWaitTimeForCloseAnimation = this.m_fWaitTimeForCloseAnimation;
					yield return null;
				}
			}
			else
			{
				yield return new WaitForSeconds(0.5f);
				yield return base.WaitAniOrInput(null);
				for (int i = 0; i < this.m_lstSlot.Count; i++)
				{
					this.m_lstSlot[i].SetEffectEnable(false);
				}
			}
			this.m_animator.enabled = true;
			while (this.m_bPause)
			{
				yield return null;
			}
			yield return base.PlayCloseAnimation(this.m_animator);
			this.m_bFinished = true;
			this.bWaiting = false;
			yield break;
		}

		// Token: 0x0600897B RID: 35195 RVA: 0x002EA407 File Offset: 0x002E8607
		private IEnumerator ShowAllSlot()
		{
			int num;
			for (int i = 0; i < this.m_lstSlot.Count; i = num + 1)
			{
				if (i < this.m_lstRewardSlotData.Count)
				{
					NKCUtil.SetGameobjectActive(this.m_lstSlot[i], true);
					yield return null;
					this.m_lstSlot[i].GetComponent<CanvasGroup>().alpha = 1f;
					this.m_lstSlot[i].SetEffectEnable(true);
				}
				num = i;
			}
			yield return null;
			this.m_loopRewardList.SetIndexPosition(this.m_loopRewardList.TotalCount - 1);
			yield break;
		}

		// Token: 0x0600897C RID: 35196 RVA: 0x002EA416 File Offset: 0x002E8616
		public override bool IsProcessFinished()
		{
			return this.m_bFinished;
		}

		// Token: 0x0600897D RID: 35197 RVA: 0x002EA420 File Offset: 0x002E8620
		public void CleanUpData()
		{
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				this.m_lstSlot[i].SetEffectEnable(false);
			}
			this.m_queueSlot.Clear();
			this.m_loopRewardList.PrepareCells(0);
			this.m_canvasGroup.alpha = 1f;
			this.m_contractResult = null;
			this.m_lstRewardSlotData.Clear();
		}

		// Token: 0x0600897E RID: 35198 RVA: 0x002EA48E File Offset: 0x002E868E
		public override void OnUserInput()
		{
			if (!this.m_bWait)
			{
				base.OnUserInput();
			}
		}

		// Token: 0x0600897F RID: 35199 RVA: 0x002EA4A0 File Offset: 0x002E86A0
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

		// Token: 0x06008980 RID: 35200 RVA: 0x002EA540 File Offset: 0x002E8740
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

		// Token: 0x06008981 RID: 35201 RVA: 0x002EA5A4 File Offset: 0x002E87A4
		private void ProvideData(Transform transform, int idx)
		{
			NKCUIResultRewardSlot component = transform.GetComponent<NKCUIResultRewardSlot>();
			if (this.m_lstRewardSlotData.Count > idx)
			{
				NKCUISlot.SlotData data = this.m_lstRewardSlotData[idx];
				component.SetData(data, idx);
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

		// Token: 0x06008982 RID: 35202 RVA: 0x002EA646 File Offset: 0x002E8846
		public void SetActiveLoopScrollList(bool bActivate)
		{
			this.m_loopRewardList.enabled = bActivate;
			if (bActivate)
			{
				this.m_loopRewardList.RefreshCells(false);
			}
		}

		// Token: 0x040075E3 RID: 30179
		public LoopScrollRect m_loopRewardList;

		// Token: 0x040075E4 RID: 30180
		public NKCUIResultRewardSlot m_pfbSlot;

		// Token: 0x040075E5 RID: 30181
		public Transform m_trIdleObjectRoot;

		// Token: 0x040075E6 RID: 30182
		private List<NKCUIResultRewardSlot> m_lstSlot;

		// Token: 0x040075E7 RID: 30183
		private Stack<NKCUIResultRewardSlot> m_stkSlotIdle;

		// Token: 0x040075E8 RID: 30184
		private List<NKCUISlot.SlotData> m_lstRewardSlotData;

		// Token: 0x040075E9 RID: 30185
		private Queue<NKCUIResultRewardSlot> m_queueSlot = new Queue<NKCUIResultRewardSlot>();

		// Token: 0x040075EA RID: 30186
		private bool m_bNeedEnqueue;

		// Token: 0x040075EB RID: 30187
		private int m_iLastIndex = -1;

		// Token: 0x040075EC RID: 30188
		private List<MiscContractResult> m_lstContractResult;

		// Token: 0x040075ED RID: 30189
		private MiscContractResult m_contractResult;

		// Token: 0x040075EE RID: 30190
		private bool m_bWait;

		// Token: 0x040075EF RID: 30191
		private int contractIndex;

		// Token: 0x040075F0 RID: 30192
		private Animator m_animator;

		// Token: 0x040075F1 RID: 30193
		public CanvasGroup m_canvasGroup;

		// Token: 0x040075F2 RID: 30194
		private float m_fWaitTimeForCloseAnimation = 1f;

		// Token: 0x040075F3 RID: 30195
		private bool m_bAutoSkip;

		// Token: 0x040075F4 RID: 30196
		private bool m_bFinished;

		// Token: 0x040075F5 RID: 30197
		private const string REWARD_SOUND_BUNDLE_NAME = "ab_sound";

		// Token: 0x040075F6 RID: 30198
		private const string REWARD_SOUND_ASSET_NAME = "FX_UI_ITEM_RESULT_LISTUP";

		// Token: 0x040075F7 RID: 30199
		private const float SLOT_APPEAR_DELAY = 0.1f;

		// Token: 0x040075F8 RID: 30200
		private float m_fElapsedTime;

		// Token: 0x040075F9 RID: 30201
		private bool bWaiting;
	}
}
