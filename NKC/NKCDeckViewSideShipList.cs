using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x020007A2 RID: 1954
	public class NKCDeckViewSideShipList : MonoBehaviour
	{
		// Token: 0x06004CD0 RID: 19664 RVA: 0x00170354 File Offset: 0x0016E554
		public void Init(NKCDeckViewShipListSlot.OnShipChange dOnShipChange)
		{
			this.m_rectRoot = base.GetComponent<RectTransform>();
			base.gameObject.SetActive(false);
			this.m_NKM_DECK_VIEW_SIDE_SHIP_LIST_OrgX = this.m_rectRoot.anchoredPosition.x;
			this.m_NKM_DECK_VIEW_SIDE_SHIP_LIST_PosX.SetNowValue(this.m_NKM_DECK_VIEW_SIDE_SHIP_LIST_OrgX + 900f);
			for (int i = 0; i < 50; i++)
			{
				NKCDeckViewShipListSlot cNKCDeckViewShipListSlot = NKCDeckViewShipListSlot.GetNewInstance(i, this.m_rectListContent, dOnShipChange);
				if (!(cNKCDeckViewShipListSlot == null))
				{
					this.m_NKCDeckViewShipListSlot.Add(cNKCDeckViewShipListSlot);
					cNKCDeckViewShipListSlot.GetNKCUIComButton().PointerClick.RemoveAllListeners();
					cNKCDeckViewShipListSlot.GetNKCUIComButton().PointerClick.AddListener(delegate()
					{
						this.DeckViewShipListSlotClick(cNKCDeckViewShipListSlot);
					});
				}
			}
		}

		// Token: 0x06004CD1 RID: 19665 RVA: 0x00170428 File Offset: 0x0016E628
		public void Open(NKMArmyData armyData, NKMDeckIndex SelectedIndex)
		{
			if (this.m_bOpen)
			{
				return;
			}
			this.m_bOpen = true;
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			this.SetShipListData(armyData, SelectedIndex.m_eDeckType, SelectedIndex, true);
			this.Enable(true);
		}

		// Token: 0x06004CD2 RID: 19666 RVA: 0x00170474 File Offset: 0x0016E674
		public void Close()
		{
			if (!this.m_bOpen)
			{
				return;
			}
			this.m_bOpen = false;
			this.Enable(false);
		}

		// Token: 0x06004CD3 RID: 19667 RVA: 0x0017048D File Offset: 0x0016E68D
		public bool IsActive()
		{
			return base.gameObject.activeSelf;
		}

		// Token: 0x06004CD4 RID: 19668 RVA: 0x0017049C File Offset: 0x0016E69C
		public void Update()
		{
			this.m_NKM_DECK_VIEW_SIDE_SHIP_LIST_PosX.Update(Time.deltaTime);
			this.Update_PosX();
			if (!this.m_NKM_DECK_VIEW_SIDE_SHIP_LIST_PosX.IsTracking() && !this.m_ShipListEnable && base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x06004CD5 RID: 19669 RVA: 0x001704F0 File Offset: 0x0016E6F0
		public void Update_PosX()
		{
			if (!this.m_NKM_DECK_VIEW_SIDE_SHIP_LIST_PosX.IsTracking())
			{
				return;
			}
			Vector2 anchoredPosition = this.m_rectRoot.anchoredPosition;
			anchoredPosition.x = this.m_NKM_DECK_VIEW_SIDE_SHIP_LIST_PosX.GetNowValue();
			this.m_rectRoot.anchoredPosition = anchoredPosition;
		}

		// Token: 0x06004CD6 RID: 19670 RVA: 0x00170538 File Offset: 0x0016E738
		public void SetShipListData(NKMArmyData armyData, NKM_DECK_TYPE eCurrentDeckType, NKMDeckIndex SelectedIndex, bool bAnimate)
		{
			int num = 0;
			Dictionary<long, NKMUnitData>.Enumerator enumerator = armyData.m_dicMyShip.GetEnumerator();
			NKMDeckData deckData = armyData.GetDeckData(SelectedIndex);
			while (enumerator.MoveNext())
			{
				KeyValuePair<long, NKMUnitData> keyValuePair = enumerator.Current;
				NKMUnitData value = keyValuePair.Value;
				this.m_NKCDeckViewShipListSlot[num].SetData(value, eCurrentDeckType, armyData.GetShipDeckIndex(SelectedIndex.m_eDeckType, value.m_UnitUID), bAnimate);
				if (deckData != null && deckData.m_ShipUID == value.m_UnitUID)
				{
					this.DeckViewShipListSlotClick(this.m_NKCDeckViewShipListSlot[num]);
				}
				num++;
			}
			Vector2 sizeDelta = this.m_rectListContent.sizeDelta;
			sizeDelta.y = (float)(100 + num * 230);
			this.m_rectListContent.sizeDelta = sizeDelta;
			if (num < this.m_NKCDeckViewShipListSlot.Count)
			{
				for (int i = num; i < this.m_NKCDeckViewShipListSlot.Count; i++)
				{
					this.m_NKCDeckViewShipListSlot[num].SetData(null, eCurrentDeckType, new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE), false);
				}
			}
		}

		// Token: 0x06004CD7 RID: 19671 RVA: 0x00170635 File Offset: 0x0016E835
		public void DeckViewShipListSlotClick(NKCDeckViewShipListSlot cNKCDeckViewShipListSlot)
		{
			if (this.m_SelectShipListSlot != null)
			{
				this.m_SelectShipListSlot.DeSelect();
			}
			this.m_SelectShipListSlot = cNKCDeckViewShipListSlot;
			this.m_SelectShipListSlot.Select();
		}

		// Token: 0x06004CD8 RID: 19672 RVA: 0x00170664 File Offset: 0x0016E864
		public void Enable(bool bEnable)
		{
			if (this.m_ShipListEnable != bEnable)
			{
				if (bEnable)
				{
					this.m_NKM_DECK_VIEW_SIDE_SHIP_LIST_PosX.SetTracking(this.m_NKM_DECK_VIEW_SIDE_SHIP_LIST_OrgX, 0.3f, TRACKING_DATA_TYPE.TDT_SLOWER);
					if (!base.gameObject.activeSelf)
					{
						base.gameObject.SetActive(true);
					}
					int num = 0;
					while (num < this.m_NKCDeckViewShipListSlot.Count && this.m_NKCDeckViewShipListSlot[num].FadeInMove())
					{
						num++;
					}
					Vector2 anchoredPosition = this.m_rectListContent.anchoredPosition;
					anchoredPosition.y = 0f;
					this.m_rectListContent.anchoredPosition = anchoredPosition;
				}
				else
				{
					this.m_NKM_DECK_VIEW_SIDE_SHIP_LIST_PosX.SetTracking(this.m_NKM_DECK_VIEW_SIDE_SHIP_LIST_OrgX + 900f, 0.3f, TRACKING_DATA_TYPE.TDT_SLOWER);
				}
				this.m_ShipListEnable = bEnable;
			}
		}

		// Token: 0x04003C8F RID: 15503
		private bool m_bOpen;

		// Token: 0x04003C90 RID: 15504
		private bool m_ShipListEnable = true;

		// Token: 0x04003C91 RID: 15505
		public RectTransform m_rectRoot;

		// Token: 0x04003C92 RID: 15506
		private float m_NKM_DECK_VIEW_SIDE_SHIP_LIST_OrgX;

		// Token: 0x04003C93 RID: 15507
		private NKMTrackingFloat m_NKM_DECK_VIEW_SIDE_SHIP_LIST_PosX = new NKMTrackingFloat();

		// Token: 0x04003C94 RID: 15508
		public RectTransform m_rectListContent;

		// Token: 0x04003C95 RID: 15509
		private NKCDeckViewShipListSlot m_SelectShipListSlot;

		// Token: 0x04003C96 RID: 15510
		private List<NKCDeckViewShipListSlot> m_NKCDeckViewShipListSlot = new List<NKCDeckViewShipListSlot>();
	}
}
