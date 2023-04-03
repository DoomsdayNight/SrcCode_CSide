using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009FA RID: 2554
	public class NKCUIUnitSelectListEventDeckSlot : NKCUIUnitSelectListSlot, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IDropHandler
	{
		// Token: 0x06006F16 RID: 28438 RVA: 0x0024B5CD File Offset: 0x002497CD
		public void SetData(NKMUnitData unitData, int index, bool bEnableLayoutElement, NKCUIUnitSelectListEventDeckSlot.OnSelectEventDeckSlot onSelectEventDeckSlot, bool bShowFierceInfo)
		{
			this.SetData(unitData, NKMDeckIndex.None, bEnableLayoutElement, null);
			this.m_index = index;
			this.dOnSelectEventDeckSlot = onSelectEventDeckSlot;
			base.SetEnableEquipListData(true);
			base.SetEquipListData(this.m_NKMUnitData);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_FIERCE_BATTLE, false);
		}

		// Token: 0x06006F17 RID: 28439 RVA: 0x0024B60C File Offset: 0x0024980C
		public void InitEventSlot(NKMDungeonEventDeckTemplet.EventDeckSlot eventSlotData, int index, bool bEnableLayoutElement, NKCUIUnitSelectListEventDeckSlot.OnSelectEventDeckSlot onSelectEventDeckSlot, NKCUIUnitSelectListEventDeckSlot.OnUnitDetail onUnitDetail)
		{
			this.m_index = index;
			this.dOnSelectEventDeckSlot = onSelectEventDeckSlot;
			this.dOnUnitDetail = onUnitDetail;
			this.m_eSlotType = eventSlotData.m_eType;
			NKCUtil.SetButtonClickDelegate(this.m_csbtnDetail, new UnityAction(this.OnBtnDetail));
			NKMDungeonEventDeckTemplet.SLOT_TYPE eType = eventSlotData.m_eType;
			if (eType - NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FIXED <= 2)
			{
				this.SetTargetUnit(eventSlotData.m_ID);
			}
			else
			{
				this.SetTargetUnit(0);
			}
			this.SetSlotTypeLabel(eventSlotData.m_eType);
			switch (eventSlotData.m_eType)
			{
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_CLOSED:
				base.SetClosed(bEnableLayoutElement, null);
				goto IL_D6;
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_GUEST:
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_NPC:
			{
				NKMUnitData cNKMUnitData = NKMDungeonManager.MakeUnitDataFromID(eventSlotData.m_ID, -1L, eventSlotData.m_Level, -1, eventSlotData.m_SkinID, 0);
				this.SetData(cNKMUnitData, NKMDeckIndex.None, bEnableLayoutElement, null);
				goto IL_D6;
			}
			}
			this.SetEmpty(bEnableLayoutElement, null, null);
			IL_D6:
			base.SetEnableEquipListData(true);
			base.SetEquipListData(this.m_NKMUnitData);
		}

		// Token: 0x06006F18 RID: 28440 RVA: 0x0024B704 File Offset: 0x00249904
		public void SetSlotTypeLabel(NKMDungeonEventDeckTemplet.SLOT_TYPE slotType)
		{
			NKCUtil.SetGameobjectActive(this.m_objGuest, slotType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_GUEST);
			NKCUtil.SetGameobjectActive(this.m_objCounterOnly, slotType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_COUNTER);
			NKCUtil.SetGameobjectActive(this.m_objSoldierOnly, slotType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_SOLDIER);
			NKCUtil.SetGameobjectActive(this.m_objMechOnly, slotType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_MECHANIC);
			NKCUtil.SetGameobjectActive(this.m_objNPC, slotType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_NPC);
		}

		// Token: 0x06006F19 RID: 28441 RVA: 0x0024B75C File Offset: 0x0024995C
		public void SetTargetUnit(int unitID)
		{
			this.m_targetUnit = unitID;
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
			this.m_ImgTargetUnit.sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, unitTempletBase);
		}

		// Token: 0x06006F1A RID: 28442 RVA: 0x0024B78C File Offset: 0x0024998C
		protected override void SetMode(NKCUIUnitSelectListSlotBase.eUnitSlotMode mode)
		{
			base.SetMode(mode);
			bool bValue;
			if (this.m_eSlotType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FIXED && mode == NKCUIUnitSelectListSlotBase.eUnitSlotMode.Character)
			{
				bValue = (this.m_targetUnit != 0 && !this.HasUnit());
			}
			else
			{
				bValue = (this.m_targetUnit != 0 && mode == NKCUIUnitSelectListSlotBase.eUnitSlotMode.Empty);
			}
			NKCUtil.SetGameobjectActive(this.m_objGuestPlus, this.m_eSlotType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_GUEST && !this.HasUnit());
			NKCUtil.SetGameobjectActive(this.m_ImgTargetUnit, bValue);
			NKCUtil.SetGameobjectActive(this.m_objTouchToEntry, mode == NKCUIUnitSelectListSlotBase.eUnitSlotMode.Empty);
			NKCUtil.SetGameobjectActive(this.m_csbtnDetail, this.HasUnit());
		}

		// Token: 0x06006F1B RID: 28443 RVA: 0x0024B81F File Offset: 0x00249A1F
		protected override void OnClick()
		{
			if (this.dOnSelectEventDeckSlot != null)
			{
				this.dOnSelectEventDeckSlot(this.m_index);
			}
		}

		// Token: 0x06006F1C RID: 28444 RVA: 0x0024B83A File Offset: 0x00249A3A
		private void OnBtnDetail()
		{
			NKCUIUnitSelectListEventDeckSlot.OnUnitDetail onUnitDetail = this.dOnUnitDetail;
			if (onUnitDetail == null)
			{
				return;
			}
			onUnitDetail(this.m_NKMUnitData);
		}

		// Token: 0x06006F1D RID: 28445 RVA: 0x0024B852 File Offset: 0x00249A52
		private bool HasUnit()
		{
			return this.m_NKMUnitData != null && this.m_NKMUnitData.m_UnitUID > 0L;
		}

		// Token: 0x06006F1E RID: 28446 RVA: 0x0024B870 File Offset: 0x00249A70
		public bool ConfirmLeader(int leaderIndex)
		{
			if (!this.CanBecomeLeader())
			{
				NKCUtil.SetGameobjectActive(this.m_objLeaderSelectFx, false);
				NKCUtil.SetGameobjectActive(this.m_objLeaderMark, false);
				return false;
			}
			bool flag = this.m_index == leaderIndex;
			NKCUtil.SetGameobjectActive(this.m_objLeaderSelectFx, false);
			NKCUtil.SetGameobjectActive(this.m_objLeaderMark, flag);
			if (this.m_NKMUnitTempletBase != null)
			{
				NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(this.m_NKMUnitTempletBase.m_UnitID);
				if (unitStatTemplet != null)
				{
					int respawnCost = unitStatTemplet.GetRespawnCost(flag, null);
					if (flag)
					{
						NKCUtil.SetLabelText(this.m_lbSummonCost, string.Format("<color=#FFCD07>{0}</color>", respawnCost));
					}
					else
					{
						NKCUtil.SetLabelText(this.m_lbSummonCost, string.Format("{0}", respawnCost));
					}
				}
			}
			return flag;
		}

		// Token: 0x06006F1F RID: 28447 RVA: 0x0024B922 File Offset: 0x00249B22
		public bool CanBecomeLeader()
		{
			return this.m_eSlotType > NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_CLOSED;
		}

		// Token: 0x06006F20 RID: 28448 RVA: 0x0024B92D File Offset: 0x00249B2D
		public void LeaderSelectState(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_objLeaderSelectFx, this.m_eSlotType != NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_CLOSED && value);
		}

		// Token: 0x06006F21 RID: 28449 RVA: 0x0024B946 File Offset: 0x00249B46
		public void SetDragHandler(NKCUIUnitSelectListEventDeckSlot.DragHandler onBeginDrag, NKCUIUnitSelectListEventDeckSlot.DragHandler onDrag, NKCUIUnitSelectListEventDeckSlot.DragHandler onEndDrag, NKCUIUnitSelectListEventDeckSlot.DragHandler onDrop)
		{
			this.dOnBeginDrag = onBeginDrag;
			this.dOnDrag = onDrag;
			this.dOnEndDrag = onEndDrag;
			this.dOnDrop = onDrop;
		}

		// Token: 0x06006F22 RID: 28450 RVA: 0x0024B965 File Offset: 0x00249B65
		public void OnBeginDrag(PointerEventData eventData)
		{
			NKCUIUnitSelectListEventDeckSlot.DragHandler dragHandler = this.dOnBeginDrag;
			if (dragHandler == null)
			{
				return;
			}
			dragHandler(eventData, this.m_index);
		}

		// Token: 0x06006F23 RID: 28451 RVA: 0x0024B97E File Offset: 0x00249B7E
		public void OnEndDrag(PointerEventData eventData)
		{
			NKCUIUnitSelectListEventDeckSlot.DragHandler dragHandler = this.dOnEndDrag;
			if (dragHandler == null)
			{
				return;
			}
			dragHandler(eventData, this.m_index);
		}

		// Token: 0x06006F24 RID: 28452 RVA: 0x0024B997 File Offset: 0x00249B97
		public void OnDrag(PointerEventData eventData)
		{
			NKCUIUnitSelectListEventDeckSlot.DragHandler dragHandler = this.dOnDrag;
			if (dragHandler == null)
			{
				return;
			}
			dragHandler(eventData, this.m_index);
		}

		// Token: 0x06006F25 RID: 28453 RVA: 0x0024B9B0 File Offset: 0x00249BB0
		public void OnDrop(PointerEventData eventData)
		{
			NKCUIUnitSelectListEventDeckSlot.DragHandler dragHandler = this.dOnDrop;
			if (dragHandler == null)
			{
				return;
			}
			dragHandler(eventData, this.m_index);
		}

		// Token: 0x04005A7B RID: 23163
		[Header("이벤트 덱 관련")]
		public Image m_ImgTargetUnit;

		// Token: 0x04005A7C RID: 23164
		public GameObject m_objTouchToEntry;

		// Token: 0x04005A7D RID: 23165
		public GameObject m_objGuestPlus;

		// Token: 0x04005A7E RID: 23166
		public GameObject m_objGuest;

		// Token: 0x04005A7F RID: 23167
		public GameObject m_objCounterOnly;

		// Token: 0x04005A80 RID: 23168
		public GameObject m_objSoldierOnly;

		// Token: 0x04005A81 RID: 23169
		public GameObject m_objMechOnly;

		// Token: 0x04005A82 RID: 23170
		public GameObject m_objNPC;

		// Token: 0x04005A83 RID: 23171
		[Header("리더 선택")]
		public GameObject m_objLeaderSelectFx;

		// Token: 0x04005A84 RID: 23172
		public GameObject m_objLeaderMark;

		// Token: 0x04005A85 RID: 23173
		public NKCUIComStateButton m_csbtnDetail;

		// Token: 0x04005A86 RID: 23174
		private NKMDungeonEventDeckTemplet.SLOT_TYPE m_eSlotType;

		// Token: 0x04005A87 RID: 23175
		private int m_index;

		// Token: 0x04005A88 RID: 23176
		private NKCUIUnitSelectListEventDeckSlot.OnSelectEventDeckSlot dOnSelectEventDeckSlot;

		// Token: 0x04005A89 RID: 23177
		private NKCUIUnitSelectListEventDeckSlot.OnUnitDetail dOnUnitDetail;

		// Token: 0x04005A8A RID: 23178
		private int m_targetUnit;

		// Token: 0x04005A8B RID: 23179
		private NKCUIUnitSelectListEventDeckSlot.DragHandler dOnBeginDrag;

		// Token: 0x04005A8C RID: 23180
		private NKCUIUnitSelectListEventDeckSlot.DragHandler dOnDrag;

		// Token: 0x04005A8D RID: 23181
		private NKCUIUnitSelectListEventDeckSlot.DragHandler dOnEndDrag;

		// Token: 0x04005A8E RID: 23182
		private NKCUIUnitSelectListEventDeckSlot.DragHandler dOnDrop;

		// Token: 0x02001730 RID: 5936
		// (Invoke) Token: 0x0600B292 RID: 45714
		public delegate void OnSelectEventDeckSlot(int index);

		// Token: 0x02001731 RID: 5937
		// (Invoke) Token: 0x0600B296 RID: 45718
		public delegate void OnUnitDetail(NKMUnitData unitData);

		// Token: 0x02001732 RID: 5938
		// (Invoke) Token: 0x0600B29A RID: 45722
		public delegate void DragHandler(PointerEventData eventData, int index);
	}
}
