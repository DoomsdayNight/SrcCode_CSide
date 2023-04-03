using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A9E RID: 2718
	public class NKCUIRearmamentSubUISelectList : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IScrollHandler
	{
		// Token: 0x06007869 RID: 30825 RVA: 0x0027F738 File Offset: 0x0027D938
		public void Init(NKCUIRearmamentSubUISelectList.OnSelectedUnitID func)
		{
			foreach (NKCUIUnitSelectListSlot nkcuiunitSelectListSlot in this.m_lstRearmUnitSlots)
			{
				nkcuiunitSelectListSlot.Init(true);
			}
			NKCUtil.SetBindFunction(this.m_csbtnLeftArrow, new UnityAction(this.OnClickRearmLeftArrow));
			NKCUtil.SetBindFunction(this.m_csbtnRightArrow, new UnityAction(this.OnClickRearmRightArrow));
			this.dSelected = func;
			this.m_bDrag = false;
		}

		// Token: 0x0600786A RID: 30826 RVA: 0x0027F7C8 File Offset: 0x0027D9C8
		public void SetData(int baseUnitID, int iSelectedRearmUnitID = 0, long iSelectedRearmBaseUnitUID = 0L)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(baseUnitID);
			if (unitTempletBase == null)
			{
				return;
			}
			if (!NKCRearmamentUtil.IsCanRearmamentUnit(unitTempletBase))
			{
				return;
			}
			this.m_lstRearmTargetTemplets = NKCRearmamentUtil.GetRearmamentTargetTemplets(unitTempletBase);
			if (this.m_lstRearmTargetTemplets.Count > 0)
			{
				if (iSelectedRearmUnitID != 0)
				{
					using (List<NKMUnitRearmamentTemplet>.Enumerator enumerator = this.m_lstRearmTargetTemplets.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.Key == iSelectedRearmUnitID)
							{
								this.UpdateRearmTargetSlotUI(iSelectedRearmUnitID, true, iSelectedRearmBaseUnitUID);
								return;
							}
						}
					}
				}
				this.UpdateRearmTargetSlotUI(this.m_lstRearmTargetTemplets[0].Key, true, 0L);
			}
		}

		// Token: 0x0600786B RID: 30827 RVA: 0x0027F874 File Offset: 0x0027DA74
		public void UpdateReamUnitSlotData(long iSelectedRearmBaseUnitUID = 0L)
		{
			int tacticLevel = 0;
			if (iSelectedRearmBaseUnitUID != 0L)
			{
				NKMUnitData unitFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(iSelectedRearmBaseUnitUID);
				if (unitFromUID != null)
				{
					tacticLevel = unitFromUID.tacticLevel;
				}
			}
			for (int i = 0; i < this.m_lstRearmUnitSlots.Count; i++)
			{
				if (this.m_lstRearmTargetTemplets.Count <= i)
				{
					NKCUtil.SetGameobjectActive(this.m_lstRearmUnitSlots[i], false);
				}
				else
				{
					NKMUnitData nkmunitData = new NKMUnitData();
					nkmunitData.m_UnitID = this.m_lstRearmTargetTemplets[i].Key;
					nkmunitData.m_SkinID = 0;
					nkmunitData.m_UnitLevel = 1;
					nkmunitData.tacticLevel = tacticLevel;
					this.m_lstRearmUnitSlots[i].SetData(nkmunitData, new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE), false, new NKCUIUnitSelectListSlotBase.OnSelectThisSlot(this.OnSlotClicked));
					this.m_lstRearmUnitSlots[i].SetSlotDisable(this.m_lstRearmTargetTemplets[i].Key != this.m_iCurSelectedUnitID);
				}
			}
		}

		// Token: 0x0600786C RID: 30828 RVA: 0x0027F968 File Offset: 0x0027DB68
		private void InitRearmSlot(int targetRearmID, long iSelectedRearmBaseUnitUID = 0L)
		{
			this.bMoving = false;
			this.m_iCurSelectedUnitID = this.GetTargetID(targetRearmID);
			int tacticLevel = 0;
			if (iSelectedRearmBaseUnitUID != 0L)
			{
				NKMUnitData unitFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(iSelectedRearmBaseUnitUID);
				if (unitFromUID != null)
				{
					tacticLevel = unitFromUID.tacticLevel;
				}
			}
			for (int i = 0; i < this.m_lstRearmUnitSlots.Count; i++)
			{
				if (this.m_lstRearmTargetTemplets.Count <= i)
				{
					NKCUtil.SetGameobjectActive(this.m_lstRearmUnitSlots[i], false);
				}
				else
				{
					NKMUnitData nkmunitData = new NKMUnitData();
					nkmunitData.m_UnitID = this.m_lstRearmTargetTemplets[i].Key;
					nkmunitData.m_SkinID = 0;
					nkmunitData.m_UnitLevel = 1;
					nkmunitData.tacticLevel = tacticLevel;
					this.m_lstRearmUnitSlots[i].SetSlotDisable(this.m_lstRearmTargetTemplets[i].Key != this.m_iCurSelectedUnitID);
					this.m_lstRearmUnitSlots[i].SetDataForRearm(nkmunitData, new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE), false, new NKCUIUnitSelectListSlotBase.OnSelectThisSlot(this.OnSlotClicked), false, false, i != 0);
					NKCUtil.SetGameobjectActive(this.m_lstRearmUnitSlots[i], true);
				}
			}
			if (null != this.m_rtLayoutGroup)
			{
				LayoutRebuilder.ForceRebuildLayoutImmediate(this.m_rtLayoutGroup);
			}
		}

		// Token: 0x0600786D RID: 30829 RVA: 0x0027FAA0 File Offset: 0x0027DCA0
		private void UpdateRearmTargetSlotUI(int targetRearmID, bool bInit = false, long iSelectedRearmBaseUnitUID = 0L)
		{
			if (this.m_lstRearmTargetTemplets.Count <= 0)
			{
				return;
			}
			this.m_iCurSelectedUnitID = this.GetTargetID(targetRearmID);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_iCurSelectedUnitID);
			if (bInit)
			{
				this.InitRearmSlot(targetRearmID, iSelectedRearmBaseUnitUID);
			}
			NKCUtil.SetLabelText(this.m_lbRearmName, unitTempletBase.GetUnitTitle());
			this.MoveSlot();
			NKCUIRearmamentSubUISelectList.OnSelectedUnitID onSelectedUnitID = this.dSelected;
			if (onSelectedUnitID != null)
			{
				onSelectedUnitID(this.m_iCurSelectedUnitID);
			}
			Debug.Log(string.Format("<color=red> Selected UnitID {0}</color>", this.m_iCurSelectedUnitID));
		}

		// Token: 0x0600786E RID: 30830 RVA: 0x0027FB28 File Offset: 0x0027DD28
		private void MoveSlot()
		{
			if (this.bMoving)
			{
				return;
			}
			this.bMoving = true;
			int num = 2;
			for (int i = 0; i < this.m_lstRearmTargetTemplets.Count; i++)
			{
				if (this.m_lstRearmTargetTemplets[i].Key == this.m_iCurSelectedUnitID)
				{
					if (i == 0)
					{
						num = 2;
					}
					else if (i == 1)
					{
						num = 1;
					}
					else if (i == 2)
					{
						num = 0;
					}
				}
			}
			for (int j = 0; j < this.m_lstRearmTargetTemplets.Count; j++)
			{
				if (this.m_lstRearmTargetTemplets[j].Key == this.m_iCurSelectedUnitID)
				{
					if (this.m_lstRearmTargetTemplets.Count == 1)
					{
						this.m_lstRearmUnitSlots[j].transform.DOMove(this.m_rtMoveTarget[num + j].position, this.m_fSlotMoveSpeed, false).OnComplete(new TweenCallback(this.EndMove));
					}
					else
					{
						this.m_lstRearmUnitSlots[j].transform.DOMove(this.m_rtMoveTarget[num + j].position, this.m_fSlotMoveSpeed, false);
					}
				}
				else
				{
					this.m_lstRearmUnitSlots[j].transform.DOMove(this.m_rtMoveTarget[num + j].position, this.m_fSlotMoveSpeed, false).SetDelay(this.m_fSlotDelaySpeed).OnComplete(new TweenCallback(this.EndMove));
				}
				this.m_lstRearmUnitSlots[j].SetSlotDisable(this.m_lstRearmTargetTemplets[j].Key != this.m_iCurSelectedUnitID);
			}
		}

		// Token: 0x0600786F RID: 30831 RVA: 0x0027FCC8 File Offset: 0x0027DEC8
		private int GetTargetID(int targetRearmID)
		{
			int num = 0;
			using (List<NKMUnitRearmamentTemplet>.Enumerator enumerator = this.m_lstRearmTargetTemplets.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Key == targetRearmID)
					{
						num = targetRearmID;
						break;
					}
				}
			}
			if (num == 0)
			{
				return this.m_lstRearmTargetTemplets[0].Key;
			}
			return num;
		}

		// Token: 0x06007870 RID: 30832 RVA: 0x0027FD38 File Offset: 0x0027DF38
		private void EndMove()
		{
			this.bMoving = false;
			int num = 2;
			for (int i = 0; i < this.m_lstRearmTargetTemplets.Count; i++)
			{
				if (this.m_lstRearmTargetTemplets[i].Key == this.m_iCurSelectedUnitID)
				{
					if (i == 0)
					{
						num = 2;
					}
					else if (i == 1)
					{
						num = 1;
					}
					else if (i == 2)
					{
						num = 0;
					}
				}
			}
			for (int j = 0; j < this.m_lstRearmTargetTemplets.Count; j++)
			{
				this.m_lstRearmUnitSlots[j].transform.SetParent(this.m_rtMoveTarget[j + num]);
				this.m_lstRearmUnitSlots[j].transform.localPosition = Vector3.zero;
			}
		}

		// Token: 0x06007871 RID: 30833 RVA: 0x0027FDE7 File Offset: 0x0027DFE7
		public void OnSlotClicked(NKMUnitData unitData, NKMUnitTempletBase unitTempletBase, NKMDeckIndex deckIndex, NKCUnitSortSystem.eUnitState slotState, NKCUIUnitSelectList.eUnitSlotSelectState unitSlotSelectState)
		{
			if (this.m_iCurSelectedUnitID == unitData.m_UnitID)
			{
				return;
			}
			this.UpdateRearmTargetSlotUI(unitData.m_UnitID, false, 0L);
		}

		// Token: 0x06007872 RID: 30834 RVA: 0x0027FE08 File Offset: 0x0027E008
		private void OnClickRearmLeftArrow()
		{
			if (this.m_lstRearmTargetTemplets.Count > 1)
			{
				for (int i = 0; i < this.m_lstRearmTargetTemplets.Count; i++)
				{
					if (i > 0 && this.m_lstRearmTargetTemplets[i].Key == this.m_iCurSelectedUnitID)
					{
						this.UpdateRearmTargetSlotUI(this.m_lstRearmTargetTemplets[i - 1].Key, false, 0L);
					}
				}
			}
		}

		// Token: 0x06007873 RID: 30835 RVA: 0x0027FE74 File Offset: 0x0027E074
		private void OnClickRearmRightArrow()
		{
			if (this.m_lstRearmTargetTemplets.Count > 1)
			{
				for (int i = 0; i < this.m_lstRearmTargetTemplets.Count; i++)
				{
					if (i < this.m_lstRearmTargetTemplets.Count - 1 && this.m_lstRearmTargetTemplets[i].Key == this.m_iCurSelectedUnitID)
					{
						this.UpdateRearmTargetSlotUI(this.m_lstRearmTargetTemplets[i + 1].Key, false, 0L);
					}
				}
			}
		}

		// Token: 0x06007874 RID: 30836 RVA: 0x0027FEEA File Offset: 0x0027E0EA
		public void OnScroll(PointerEventData eventData)
		{
			if (eventData.scrollDelta.y > 0f)
			{
				this.OnClickRearmLeftArrow();
				return;
			}
			if (eventData.scrollDelta.y < 0f)
			{
				this.OnClickRearmRightArrow();
			}
		}

		// Token: 0x06007875 RID: 30837 RVA: 0x0027FF1D File Offset: 0x0027E11D
		public void OnBeginDrag(PointerEventData eventData)
		{
			this.m_bDrag = true;
			this.m_fDragOffset = 0f;
		}

		// Token: 0x06007876 RID: 30838 RVA: 0x0027FF34 File Offset: 0x0027E134
		public void OnDrag(PointerEventData eventData)
		{
			if (this.m_bDrag)
			{
				this.m_fDragOffset += eventData.delta.x;
				if (this.m_fDragOffset > this.DRAG_THRESHOLD * 2f)
				{
					this.OnClickRearmLeftArrow();
					this.m_bDrag = false;
				}
				if (this.m_fDragOffset < -this.DRAG_THRESHOLD * 2f)
				{
					this.OnClickRearmRightArrow();
					this.m_bDrag = false;
				}
			}
		}

		// Token: 0x06007877 RID: 30839 RVA: 0x0027FFA4 File Offset: 0x0027E1A4
		public void OnEndDrag(PointerEventData eventData)
		{
			if (this.m_bDrag)
			{
				this.m_bDrag = false;
				if (this.m_fDragOffset > this.DRAG_THRESHOLD)
				{
					this.OnClickRearmLeftArrow();
				}
				if (this.m_fDragOffset < -this.DRAG_THRESHOLD)
				{
					this.OnClickRearmRightArrow();
				}
			}
		}

		// Token: 0x040064F9 RID: 25849
		[Header("슬롯 이동 속도")]
		public float m_fSlotMoveSpeed = 0.1f;

		// Token: 0x040064FA RID: 25850
		public float m_fSlotDelaySpeed = 0.05f;

		// Token: 0x040064FB RID: 25851
		[Header("선택된 재무장 유닛 이름&타입")]
		public Text m_lbRearmName;

		// Token: 0x040064FC RID: 25852
		public RectTransform m_rtLayoutGroup;

		// Token: 0x040064FD RID: 25853
		private NKCUIRearmamentSubUISelectList.OnSelectedUnitID dSelected;

		// Token: 0x040064FE RID: 25854
		[Header("재무장 화살표")]
		public NKCUIComStateButton m_csbtnLeftArrow;

		// Token: 0x040064FF RID: 25855
		public NKCUIComStateButton m_csbtnRightArrow;

		// Token: 0x04006500 RID: 25856
		[Header("재무장 유닛 슬롯(0번이 메인 슬롯(Facecard_Focus))")]
		public List<NKCUIUnitSelectListSlot> m_lstRearmUnitSlots;

		// Token: 0x04006501 RID: 25857
		private List<NKMUnitRearmamentTemplet> m_lstRearmTargetTemplets;

		// Token: 0x04006502 RID: 25858
		public List<RectTransform> m_rtMoveTarget = new List<RectTransform>();

		// Token: 0x04006503 RID: 25859
		private int m_iCurSelectedUnitID;

		// Token: 0x04006504 RID: 25860
		private bool bMoving;

		// Token: 0x04006505 RID: 25861
		public float DRAG_THRESHOLD = 100f;

		// Token: 0x04006506 RID: 25862
		private bool m_bDrag;

		// Token: 0x04006507 RID: 25863
		private float m_fDragOffset;

		// Token: 0x020017F6 RID: 6134
		// (Invoke) Token: 0x0600B4C4 RID: 46276
		public delegate void OnSelectedUnitID(int unitID);
	}
}
