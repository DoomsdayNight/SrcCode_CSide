using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NKC.UI
{
	// Token: 0x02000966 RID: 2406
	public class NKCDeckViewUnit : NKCUIInstantiatable
	{
		// Token: 0x17001131 RID: 4401
		// (get) Token: 0x06006044 RID: 24644 RVA: 0x001E05C0 File Offset: 0x001DE7C0
		public List<NKCDeckViewUnitSlot> DeckViewUnitSlotList
		{
			get
			{
				return this.m_listNKCDeckViewUnitSlot;
			}
		}

		// Token: 0x06006045 RID: 24645 RVA: 0x001E05C8 File Offset: 0x001DE7C8
		public static NKCDeckViewUnit OpenInstance(string bundleName, string assetName, Transform trParent, NKCDeckViewUnit.OnClickUnit onClick, NKCDeckViewUnit.OnDragUnitEnd onDragUnitEnd)
		{
			NKCDeckViewUnit nkcdeckViewUnit = NKCUIInstantiatable.OpenInstance<NKCDeckViewUnit>(bundleName, assetName, trParent);
			if (nkcdeckViewUnit == null)
			{
				return nkcdeckViewUnit;
			}
			nkcdeckViewUnit.Init(onClick, onDragUnitEnd);
			return nkcdeckViewUnit;
		}

		// Token: 0x06006046 RID: 24646 RVA: 0x001E05E0 File Offset: 0x001DE7E0
		public void CloseResource(string bundleName, string assetName)
		{
			base.CloseInstance(bundleName, assetName);
		}

		// Token: 0x06006047 RID: 24647 RVA: 0x001E05EC File Offset: 0x001DE7EC
		public void Init(NKCDeckViewUnit.OnClickUnit onClick, NKCDeckViewUnit.OnDragUnitEnd onDragUnitEnd)
		{
			this.dOnClickUnit = onClick;
			this.dOnDragUnitEnd = onDragUnitEnd;
			for (int i = 0; i < this.m_listNKCDeckViewUnitSlot.Count; i++)
			{
				NKCDeckViewUnitSlot cNKCDeckListButton = this.m_listNKCDeckViewUnitSlot[i];
				cNKCDeckListButton.Init(i, true);
				if (cNKCDeckListButton.m_NKCUIComButton != null)
				{
					cNKCDeckListButton.m_NKCUIComButton.PointerClick.AddListener(delegate()
					{
						this.OnClick(cNKCDeckListButton.m_Index);
					});
				}
				if (cNKCDeckListButton.m_NKCUIComDrag != null)
				{
					cNKCDeckListButton.m_NKCUIComDrag.BeginDrag.AddListener(delegate(PointerEventData eventData)
					{
						this.DeckViewUnitBeginDrag(cNKCDeckListButton.m_Index, eventData);
					});
					cNKCDeckListButton.m_NKCUIComDrag.Drag.AddListener(delegate(PointerEventData eventData)
					{
						this.DeckViewUnitDrag(cNKCDeckListButton.m_Index, eventData);
					});
					cNKCDeckListButton.m_NKCUIComDrag.EndDrag.AddListener(delegate(PointerEventData eventData)
					{
						this.DeckViewUnitEndDrag(cNKCDeckListButton.m_Index, eventData);
					});
				}
			}
			this.m_ScaleX.SetNowValue(this.m_fSelectedScale);
			this.m_ScaleY.SetNowValue(this.m_fSelectedScale);
			this.SlotResetPos(false);
		}

		// Token: 0x06006048 RID: 24648 RVA: 0x001E0724 File Offset: 0x001DE924
		public void Open(NKMArmyData armyData, NKMDeckIndex deckIndex, NKCUIDeckViewer.DeckViewerOption deckViewerOption)
		{
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			for (int i = 0; i < this.m_listNKCDeckViewUnitSlot.Count; i++)
			{
				NKCDeckViewUnitSlot nkcdeckViewUnitSlot = this.m_listNKCDeckViewUnitSlot[i];
				if (!(nkcdeckViewUnitSlot == null))
				{
					nkcdeckViewUnitSlot.SetEnableShowBan(NKCUtil.CheckPossibleShowBan(deckViewerOption.eDeckviewerMode));
					nkcdeckViewUnitSlot.SetEnableShowUpUnit(NKCUtil.CheckPossibleShowUpUnit(deckViewerOption.eDeckviewerMode));
				}
			}
			this.SetDeckListButton(armyData, deckIndex, deckViewerOption);
		}

		// Token: 0x06006049 RID: 24649 RVA: 0x001E07A4 File Offset: 0x001DE9A4
		public void OpenDummy(NKMDummyUnitData[] dummyUnitList, int leaderIndex)
		{
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			for (int i = 0; i < this.m_listNKCDeckViewUnitSlot.Count; i++)
			{
				NKCDeckViewUnitSlot nkcdeckViewUnitSlot = this.m_listNKCDeckViewUnitSlot[i];
				nkcdeckViewUnitSlot.SetEnableShowBan(false);
				NKMDummyUnitData nkmdummyUnitData = dummyUnitList[i];
				nkcdeckViewUnitSlot.EnableDrag(false);
				if (nkmdummyUnitData == null)
				{
					nkcdeckViewUnitSlot.SetData(null, false);
				}
				else
				{
					NKMUnitData nkmunitData = new NKMUnitData();
					nkmunitData.FillDataFromDummy(nkmdummyUnitData);
					nkcdeckViewUnitSlot.SetData(nkmunitData, false);
					if (leaderIndex == i)
					{
						nkcdeckViewUnitSlot.SetLeader(true, false);
					}
					else
					{
						nkcdeckViewUnitSlot.SetLeader(false, false);
					}
				}
			}
		}

		// Token: 0x0600604A RID: 24650 RVA: 0x001E0837 File Offset: 0x001DEA37
		public void Close()
		{
			this.CancelAllDrag();
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x0600604B RID: 24651 RVA: 0x001E0858 File Offset: 0x001DEA58
		public void Update()
		{
			this.m_ScaleX.Update(Time.deltaTime);
			this.m_ScaleY.Update(Time.deltaTime);
			if (this.m_ScaleX.IsTracking())
			{
				this.SlotResetPos(false);
			}
			else if (this.m_bTrackingStarted)
			{
				this.m_bTrackingStarted = false;
				this.SlotResetPos(true);
			}
			Vector2 v = this.m_rectAnchor.localScale;
			v.Set(this.m_ScaleX.GetNowValue(), this.m_ScaleY.GetNowValue());
			this.m_rectAnchor.localScale = v;
		}

		// Token: 0x0600604C RID: 24652 RVA: 0x001E08F0 File Offset: 0x001DEAF0
		public void SetDeckListButton(NKMArmyData armyData, NKMDeckIndex deckIndex, NKCUIDeckViewer.DeckViewerOption deckViewerOption)
		{
			for (int i = 0; i < this.m_listNKCDeckViewUnitSlot.Count; i++)
			{
				this.SetUnitSlotData(armyData, deckIndex, i, false, deckViewerOption);
			}
		}

		// Token: 0x0600604D RID: 24653 RVA: 0x001E0920 File Offset: 0x001DEB20
		public void SetUnitSlotData(NKMArmyData armyData, NKMDeckIndex deckIndex, int unitSlotIndex, bool bEffect, NKCUIDeckViewer.DeckViewerOption deckViewerOption)
		{
			NKCDeckViewUnitSlot nkcdeckViewUnitSlot = this.m_listNKCDeckViewUnitSlot[unitSlotIndex];
			if (deckViewerOption.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
			{
				long localUnitData = NKCLocalDeckDataManager.GetLocalUnitData((int)deckIndex.m_iIndex, unitSlotIndex);
				NKMUnitData unitFromUID = armyData.GetUnitFromUID(localUnitData);
				nkcdeckViewUnitSlot.SetData(unitFromUID, deckViewerOption, true);
				nkcdeckViewUnitSlot.EnableDrag(true);
				int localLeaderIndex = NKCLocalDeckDataManager.GetLocalLeaderIndex((int)deckIndex.m_iIndex);
				if (unitSlotIndex == localLeaderIndex)
				{
					nkcdeckViewUnitSlot.SetLeader(true, false);
				}
				else
				{
					nkcdeckViewUnitSlot.SetLeader(false, false);
				}
				if (bEffect)
				{
					nkcdeckViewUnitSlot.PlayEffect();
				}
				return;
			}
			NKMUnitData deckUnitByIndex = armyData.GetDeckUnitByIndex(deckIndex, unitSlotIndex);
			nkcdeckViewUnitSlot.SetData(deckUnitByIndex, deckViewerOption, true);
			nkcdeckViewUnitSlot.EnableDrag(true);
			NKMDeckData deckData = armyData.GetDeckData(deckIndex);
			if (deckData != null && unitSlotIndex == (int)deckData.m_LeaderIndex)
			{
				nkcdeckViewUnitSlot.SetLeader(true, false);
			}
			else
			{
				nkcdeckViewUnitSlot.SetLeader(false, false);
			}
			if (bEffect)
			{
				nkcdeckViewUnitSlot.PlayEffect();
			}
		}

		// Token: 0x0600604E RID: 24654 RVA: 0x001E09E8 File Offset: 0x001DEBE8
		public void SetLeader(int leaderIndex, bool bEffect)
		{
			for (int i = 0; i < this.m_listNKCDeckViewUnitSlot.Count; i++)
			{
				NKCDeckViewUnitSlot nkcdeckViewUnitSlot = this.m_listNKCDeckViewUnitSlot[i];
				if (i == leaderIndex)
				{
					nkcdeckViewUnitSlot.SetLeader(true, bEffect);
				}
				else
				{
					nkcdeckViewUnitSlot.SetLeader(false, bEffect);
				}
			}
		}

		// Token: 0x0600604F RID: 24655 RVA: 0x001E0A2E File Offset: 0x001DEC2E
		public void OnClick(int index)
		{
			if (this.dOnClickUnit != null)
			{
				this.dOnClickUnit(index);
			}
			this.SelectDeckViewUnit(index, false);
		}

		// Token: 0x06006050 RID: 24656 RVA: 0x001E0A4C File Offset: 0x001DEC4C
		public void SelectDeckViewUnit(int selectedIndex, bool bForce = false)
		{
			this.m_iSelectedSlotIndex = selectedIndex;
			for (int i = 0; i < this.m_listNKCDeckViewUnitSlot.Count; i++)
			{
				NKCDeckViewUnitSlot nkcdeckViewUnitSlot = this.m_listNKCDeckViewUnitSlot[i];
				if (i != selectedIndex)
				{
					nkcdeckViewUnitSlot.ButtonDeSelect(false, false);
				}
				else
				{
					nkcdeckViewUnitSlot.ButtonSelect();
				}
			}
		}

		// Token: 0x06006051 RID: 24657 RVA: 0x001E0A97 File Offset: 0x001DEC97
		public void DeckViewUnitBeginDrag(int index, PointerEventData eventData)
		{
			this.CancelAllDrag();
			this.m_listNKCDeckViewUnitSlot[index].BeginDrag();
		}

		// Token: 0x06006052 RID: 24658 RVA: 0x001E0AB0 File Offset: 0x001DECB0
		public void DeckViewUnitDrag(int index, PointerEventData eventData)
		{
			if (!this.m_listNKCDeckViewUnitSlot[index].GetInDrag())
			{
				return;
			}
			this.m_listNKCDeckViewUnitSlot[index].Drag(eventData);
			bool flag = false;
			for (int i = 0; i < this.m_listNKCDeckViewUnitSlot.Count; i++)
			{
				if (index != i)
				{
					NKCDeckViewUnitSlot nkcdeckViewUnitSlot = this.m_listNKCDeckViewUnitSlot[i];
					if (!flag && nkcdeckViewUnitSlot.IsEnter(this.m_listNKCDeckViewUnitSlot[index].m_rectMain.position))
					{
						flag = true;
						nkcdeckViewUnitSlot.Swap(this.m_listNKCDeckViewUnitSlot[index]);
					}
					else
					{
						nkcdeckViewUnitSlot.ReturnToOrg();
					}
				}
			}
		}

		// Token: 0x06006053 RID: 24659 RVA: 0x001E0B4C File Offset: 0x001DED4C
		public void DeckViewUnitEndDrag(int index, PointerEventData eventData)
		{
			if (this.m_listNKCDeckViewUnitSlot[index].GetInDrag())
			{
				bool flag = false;
				for (int i = 0; i < this.m_listNKCDeckViewUnitSlot.Count; i++)
				{
					if (index != i)
					{
						NKCDeckViewUnitSlot nkcdeckViewUnitSlot = this.m_listNKCDeckViewUnitSlot[i];
						if (!flag && nkcdeckViewUnitSlot.IsEnter(this.m_listNKCDeckViewUnitSlot[index].m_rectMain.position))
						{
							nkcdeckViewUnitSlot.Swap(this.m_listNKCDeckViewUnitSlot[index]);
							this.m_listNKCDeckViewUnitSlot[index].Swap(nkcdeckViewUnitSlot);
							this.m_listNKCDeckViewUnitSlot[index].ReturnToParent();
							if (this.dOnDragUnitEnd != null)
							{
								this.dOnDragUnitEnd(index, nkcdeckViewUnitSlot.m_Index);
							}
							flag = true;
						}
						else
						{
							nkcdeckViewUnitSlot.ReturnToOrg();
						}
					}
				}
				if (flag)
				{
					return;
				}
			}
			this.m_listNKCDeckViewUnitSlot[index].EndDrag();
			for (int j = 0; j < this.m_listNKCDeckViewUnitSlot.Count; j++)
			{
				if (index != j)
				{
					this.m_listNKCDeckViewUnitSlot[j].ReturnToOrg();
				}
			}
		}

		// Token: 0x06006054 RID: 24660 RVA: 0x001E0C5C File Offset: 0x001DEE5C
		public void CancelAllDrag()
		{
			for (int i = 0; i < this.m_listNKCDeckViewUnitSlot.Count; i++)
			{
				NKCDeckViewUnitSlot nkcdeckViewUnitSlot = this.m_listNKCDeckViewUnitSlot[i];
				nkcdeckViewUnitSlot.EndDrag();
				nkcdeckViewUnitSlot.ButtonDeSelect(true, true);
			}
		}

		// Token: 0x06006055 RID: 24661 RVA: 0x001E0C98 File Offset: 0x001DEE98
		public void Enable()
		{
			this.m_ScaleX.SetTracking(this.m_fSelectedScale, 0.3f, TRACKING_DATA_TYPE.TDT_SLOWER);
			this.m_ScaleY.SetTracking(this.m_fSelectedScale, 0.3f, TRACKING_DATA_TYPE.TDT_SLOWER);
			this.m_bTrackingStarted = true;
		}

		// Token: 0x06006056 RID: 24662 RVA: 0x001E0CD0 File Offset: 0x001DEED0
		public void Disable()
		{
			if (this.m_iSelectedSlotIndex >= 0)
			{
				this.m_listNKCDeckViewUnitSlot[this.m_iSelectedSlotIndex].ButtonDeSelect(false, false);
			}
			this.m_ScaleX.SetTracking(this.m_fDeselectedScale, 0.3f, TRACKING_DATA_TYPE.TDT_SLOWER);
			this.m_ScaleY.SetTracking(this.m_fDeselectedScale, 0.3f, TRACKING_DATA_TYPE.TDT_SLOWER);
			this.m_bTrackingStarted = true;
		}

		// Token: 0x06006057 RID: 24663 RVA: 0x001E0D34 File Offset: 0x001DEF34
		public void SlotResetPos(bool bImmediate = false)
		{
			for (int i = 0; i < this.m_listNKCDeckViewUnitSlot.Count; i++)
			{
				this.m_listNKCDeckViewUnitSlot[i].ResetPos(bImmediate);
			}
		}

		// Token: 0x06006058 RID: 24664 RVA: 0x001E0D6C File Offset: 0x001DEF6C
		public void UpdateUnit(NKMUnitData unitData, NKCUIDeckViewer.DeckViewerOption deckViewerOption)
		{
			if (unitData == null)
			{
				return;
			}
			foreach (NKCDeckViewUnitSlot nkcdeckViewUnitSlot in this.m_listNKCDeckViewUnitSlot)
			{
				if (nkcdeckViewUnitSlot.m_NKMUnitData != null && nkcdeckViewUnitSlot.m_NKMUnitData.m_UnitUID == unitData.m_UnitUID)
				{
					nkcdeckViewUnitSlot.SetData(unitData, deckViewerOption, true);
				}
			}
		}

		// Token: 0x04004CA6 RID: 19622
		public RectTransform m_rectAnchor;

		// Token: 0x04004CA7 RID: 19623
		public List<NKCDeckViewUnitSlot> m_listNKCDeckViewUnitSlot;

		// Token: 0x04004CA8 RID: 19624
		public int m_iSelectedSlotIndex;

		// Token: 0x04004CA9 RID: 19625
		public float m_fSelectedScale = 1f;

		// Token: 0x04004CAA RID: 19626
		public float m_fDeselectedScale = 0.7f;

		// Token: 0x04004CAB RID: 19627
		private NKMTrackingFloat m_ScaleX = new NKMTrackingFloat();

		// Token: 0x04004CAC RID: 19628
		private NKMTrackingFloat m_ScaleY = new NKMTrackingFloat();

		// Token: 0x04004CAD RID: 19629
		private NKCDeckViewUnit.OnClickUnit dOnClickUnit;

		// Token: 0x04004CAE RID: 19630
		private NKCDeckViewUnit.OnDragUnitEnd dOnDragUnitEnd;

		// Token: 0x04004CAF RID: 19631
		private bool m_bTrackingStarted;

		// Token: 0x020015F1 RID: 5617
		// (Invoke) Token: 0x0600AEB8 RID: 44728
		public delegate void OnClickUnit(int index);

		// Token: 0x020015F2 RID: 5618
		// (Invoke) Token: 0x0600AEBC RID: 44732
		public delegate void OnDragUnitEnd(int oldIndex, int newIndex);
	}
}
