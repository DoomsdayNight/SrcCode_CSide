using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000962 RID: 2402
	public class NKCDeckViewList : MonoBehaviour
	{
		// Token: 0x06005FD6 RID: 24534 RVA: 0x001DD474 File Offset: 0x001DB674
		public void Init(NKCDeckViewList.OnSelectDeck onSelectDeck, NKCDeckViewList.OnSelectDeck onDeckUnlockRequest, NKCDeckViewList.OnChangedMuiltiSelectedDeckCount onChangedMuiltiSelectedDeckCount, NKCDeckViewList.OnSupportList onSupportList)
		{
			this.dOnSelectDeck = onSelectDeck;
			this.dOnDeckUnloakRequest = onDeckUnlockRequest;
			this.dOnChangedMuiltiSelectedDeckCount = onChangedMuiltiSelectedDeckCount;
			this.dOnSupportList = onSupportList;
			byte b = 0;
			while ((int)b < this.m_listDeckListButton.Count)
			{
				NKCDeckListButton cNKCDeckListButton = this.m_listDeckListButton[(int)b];
				cNKCDeckListButton.Init((int)b, new NKCDeckListButton.dOnChangedSelected(this.OnChangedMultiSelectedCount));
				cNKCDeckListButton.m_cbtnButton.PointerClick.RemoveAllListeners();
				cNKCDeckListButton.m_cbtnButton.PointerClick.AddListener(delegate()
				{
					this.DeckViewListClick(new NKMDeckIndex(this.m_eCurrentDeckType, (int)cNKCDeckListButton.m_DeckIndex.m_iIndex));
				});
				cNKCDeckListButton.m_ctToggleForMulti.SetbReverseSeqCallbackCall(true);
				cNKCDeckListButton.m_ctToggleForMulti.SetToggleGroup(this.m_ToggleGroup);
				b += 1;
			}
			this.m_supportListButton.Init(new UnityAction(this.OnClickSupportButton));
		}

		// Token: 0x06005FD7 RID: 24535 RVA: 0x001DD568 File Offset: 0x001DB768
		private void OnChangedMultiSelectedCount(int index, bool bSet)
		{
			if (bSet)
			{
				this.m_lstMultiSelectedIndex.Add(index);
			}
			else
			{
				for (int i = 0; i < this.m_lstMultiSelectedIndex.Count; i++)
				{
					if (this.m_lstMultiSelectedIndex[i] == index)
					{
						this.m_lstMultiSelectedIndex.RemoveAt(i);
					}
				}
			}
			this.UpdateMultiSelectedSeqUI();
			if (this.dOnChangedMuiltiSelectedDeckCount != null)
			{
				this.dOnChangedMuiltiSelectedDeckCount(this.GetMultiSelectedCount());
			}
		}

		// Token: 0x06005FD8 RID: 24536 RVA: 0x001DD5D8 File Offset: 0x001DB7D8
		public List<NKMDeckIndex> GetMultiSelectedDeckIndexList()
		{
			List<NKMDeckIndex> list = new List<NKMDeckIndex>();
			for (int i = 0; i < this.m_lstMultiSelectedIndex.Count; i++)
			{
				list.Add(new NKMDeckIndex(this.m_eCurrentDeckType, this.m_lstMultiSelectedIndex[i]));
			}
			return list;
		}

		// Token: 0x06005FD9 RID: 24537 RVA: 0x001DD620 File Offset: 0x001DB820
		public int GetMultiSelectedCount()
		{
			int num = 0;
			byte b = 0;
			while ((int)b < this.m_listDeckListButton.Count)
			{
				if (this.m_listDeckListButton[(int)b].m_ctToggleForMulti.m_bChecked)
				{
					num++;
				}
				b += 1;
			}
			return num;
		}

		// Token: 0x06005FDA RID: 24538 RVA: 0x001DD664 File Offset: 0x001DB864
		public void Open(bool bMultiSelect, NKMArmyData armyData, NKM_DECK_TYPE eDeckType, int selectedIndex, NKCUIDeckViewer.DeckViewerOption deckViewOption)
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
			this.m_rtDeckViewList.SetWidth(310f);
			this.m_eCurrentDeckType = eDeckType;
			this.SetDeckListButton(bMultiSelect, armyData, deckViewOption, selectedIndex, true);
			this.m_ToggleGroup.transform.localPosition = Vector3.zero;
			bool enabled = true;
			RectTransform content = this.m_scrollRect.content;
			LayoutRebuilder.ForceRebuildLayoutImmediate(content);
			if (this.m_listDeckListButton.Count > 0)
			{
				enabled = (content.GetHeight() > this.m_scrollRect.GetComponent<RectTransform>().GetHeight());
			}
			this.m_scrollRect.enabled = enabled;
			this.m_lstMultiSelectedIndex.Clear();
			if (bMultiSelect)
			{
				if (deckViewOption.lstMultiSelectedDeckIndex != null)
				{
					List<NKMDeckIndex> lstMultiSelectedDeckIndex = deckViewOption.lstMultiSelectedDeckIndex;
					for (int i = 0; i < lstMultiSelectedDeckIndex.Count; i++)
					{
						this.m_lstMultiSelectedIndex.Add((int)lstMultiSelectedDeckIndex[i].m_iIndex);
					}
				}
				this.ProcessMultiSelectUI_WhenOpen();
			}
		}

		// Token: 0x06005FDB RID: 24539 RVA: 0x001DD768 File Offset: 0x001DB968
		private void ProcessMultiSelectUI_WhenOpen()
		{
			for (int i = 0; i < this.m_listDeckListButton.Count; i++)
			{
				NKCDeckListButton nkcdeckListButton = this.m_listDeckListButton[i];
				if (nkcdeckListButton != null)
				{
					bool bSelect = false;
					if (this.m_lstMultiSelectedIndex != null)
					{
						for (int j = 0; j < this.m_lstMultiSelectedIndex.Count; j++)
						{
							if ((int)nkcdeckListButton.m_DeckIndex.m_iIndex == this.m_lstMultiSelectedIndex[j])
							{
								bSelect = true;
								break;
							}
						}
					}
					nkcdeckListButton.m_ctToggleForMulti.Select(bSelect, true, false);
				}
			}
			this.UpdateMultiSelectedSeqUI();
		}

		// Token: 0x06005FDC RID: 24540 RVA: 0x001DD7F8 File Offset: 0x001DB9F8
		private void UpdateMultiSelectedSeqUI()
		{
			for (int i = 0; i < this.m_listDeckListButton.Count; i++)
			{
				NKCDeckListButton nkcdeckListButton = this.m_listDeckListButton[i];
				if (nkcdeckListButton != null && this.m_lstMultiSelectedIndex != null)
				{
					for (int j = 0; j < this.m_lstMultiSelectedIndex.Count; j++)
					{
						if ((int)nkcdeckListButton.m_DeckIndex.m_iIndex == this.m_lstMultiSelectedIndex[j])
						{
							NKCUtil.SetLabelText(nkcdeckListButton.m_lbMultiSelectedSeq, (j + 1).ToString());
							break;
						}
					}
				}
			}
		}

		// Token: 0x06005FDD RID: 24541 RVA: 0x001DD882 File Offset: 0x001DBA82
		public void Close()
		{
			if (!this.m_bOpen)
			{
				return;
			}
			this.m_bOpen = false;
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x06005FDE RID: 24542 RVA: 0x001DD8B0 File Offset: 0x001DBAB0
		public void SetDeckListButton(bool bMultiSelect, NKMArmyData armyData, NKCUIDeckViewer.DeckViewerOption deckViewOption, int selectedIndex, bool bSelectDeck = true)
		{
			this.m_ToggleGroup.m_MaxMultiCount = deckViewOption.maxMultiSelectCount;
			NKCUtil.SetGameobjectActive(this.m_supportListButton, deckViewOption.bUsableSupporter);
			this.m_eCurrentDeckType = deckViewOption.DeckIndex.m_eDeckType;
			this.m_iUnlockedDeckCount = (int)armyData.GetUnlockedDeckCount(this.m_eCurrentDeckType);
			bool flag = NKCContentManager.IsContentsUnlocked(ContentsType.DECKVIEW_LIST, 0, 0);
			bool flag2 = NKCTutorialManager.TutorialCompleted(TutorialStep.SecondDeckSetup);
			for (int i = 0; i < this.m_listDeckListButton.Count; i++)
			{
				NKCDeckListButton nkcdeckListButton = this.m_listDeckListButton[i];
				if (nkcdeckListButton != null)
				{
					nkcdeckListButton.SetMultiSelect(bMultiSelect);
					nkcdeckListButton.SetTrimDeckSelect(deckViewOption.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck);
				}
				bool flag3 = true;
				if (deckViewOption.ShowDeckIndexList != null && deckViewOption.ShowDeckIndexList.Count > 0)
				{
					flag3 = deckViewOption.ShowDeckIndexList.Contains(i);
				}
				if (deckViewOption.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
				{
					NKCUtil.SetGameobjectActive(nkcdeckListButton, flag3);
					if (flag3)
					{
						NKCUtil.SetGameobjectActive(nkcdeckListButton, flag3);
						nkcdeckListButton.UnLock();
					}
				}
				else if (i < (int)armyData.GetUnlockedDeckCount(this.m_eCurrentDeckType))
				{
					if (i == 0 || flag || flag2)
					{
						NKCUtil.SetGameobjectActive(nkcdeckListButton, flag3);
						if (nkcdeckListButton != null && flag3)
						{
							nkcdeckListButton.SetData(armyData, new NKMDeckIndex(this.m_eCurrentDeckType, i), deckViewOption.DeckListButtonStateText);
							nkcdeckListButton.UnLock();
						}
					}
					else
					{
						NKCUtil.SetGameobjectActive(nkcdeckListButton, false);
					}
				}
				else if (i < (int)armyData.GetMaxDeckCount(this.m_eCurrentDeckType))
				{
					if (flag || flag2)
					{
						NKCUtil.SetGameobjectActive(nkcdeckListButton, flag3);
						if (nkcdeckListButton != null)
						{
							nkcdeckListButton.Lock();
						}
					}
					else
					{
						NKCUtil.SetGameobjectActive(nkcdeckListButton, false);
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(nkcdeckListButton, false);
				}
			}
			if (bSelectDeck)
			{
				this.SelectDeckList(armyData, selectedIndex);
			}
		}

		// Token: 0x06005FDF RID: 24543 RVA: 0x001DDA50 File Offset: 0x001DBC50
		public void UpdateDeckListButton(NKMArmyData armyData, NKMDeckIndex targetindex)
		{
			if (this.m_listDeckListButton[(int)targetindex.m_iIndex] == null)
			{
				return;
			}
			if (targetindex.m_iIndex < armyData.GetUnlockedDeckCount(targetindex.m_eDeckType))
			{
				this.m_listDeckListButton[(int)targetindex.m_iIndex].SetData(armyData, targetindex, "");
				this.m_listDeckListButton[(int)targetindex.m_iIndex].UnLock();
				return;
			}
			this.m_listDeckListButton[(int)targetindex.m_iIndex].Lock();
		}

		// Token: 0x06005FE0 RID: 24544 RVA: 0x001DDAD8 File Offset: 0x001DBCD8
		public void UpdateDeckState()
		{
			for (int i = 0; i < this.m_listDeckListButton.Count; i++)
			{
				this.m_listDeckListButton[i].UpdateUI();
			}
		}

		// Token: 0x06005FE1 RID: 24545 RVA: 0x001DDB0C File Offset: 0x001DBD0C
		public void SelectDeckList(NKMArmyData armyData, int selectedIndex)
		{
			bool flag = false;
			for (int i = 0; i < this.m_listDeckListButton.Count; i++)
			{
				NKCDeckListButton nkcdeckListButton = this.m_listDeckListButton[i];
				if (i < armyData.GetDeckCount(this.m_eCurrentDeckType))
				{
					if (i == selectedIndex)
					{
						nkcdeckListButton.ButtonSelect();
						flag = true;
					}
					else
					{
						nkcdeckListButton.ButtonDeSelect();
					}
				}
			}
			this.SelectSupButton(!flag);
		}

		// Token: 0x06005FE2 RID: 24546 RVA: 0x001DDB6B File Offset: 0x001DBD6B
		public void DeckViewListClick(NKMDeckIndex index)
		{
			if ((int)index.m_iIndex < this.m_iUnlockedDeckCount)
			{
				if (this.dOnSelectDeck != null)
				{
					this.dOnSelectDeck(index);
					return;
				}
			}
			else if (this.dOnDeckUnloakRequest != null)
			{
				this.dOnDeckUnloakRequest(index);
			}
		}

		// Token: 0x06005FE3 RID: 24547 RVA: 0x001DDBA4 File Offset: 0x001DBDA4
		public NKCDeckListButton GetDeckListButton(int index)
		{
			if (index < this.m_listDeckListButton.Count)
			{
				return this.m_listDeckListButton[index];
			}
			return null;
		}

		// Token: 0x06005FE4 RID: 24548 RVA: 0x001DDBC4 File Offset: 0x001DBDC4
		public void SetScrollPosition(int index)
		{
			int childCount = this.m_scrollRect.content.transform.childCount;
			float num = 0f;
			if (childCount > 0)
			{
				num = (float)index / (float)childCount;
			}
			this.m_scrollRect.verticalNormalizedPosition = 1f - num;
		}

		// Token: 0x06005FE5 RID: 24549 RVA: 0x001DDC09 File Offset: 0x001DBE09
		private void OnClickSupportButton()
		{
			NKCDeckViewList.OnSupportList onSupportList = this.dOnSupportList;
			if (onSupportList == null)
			{
				return;
			}
			onSupportList();
		}

		// Token: 0x06005FE6 RID: 24550 RVA: 0x001DDC1B File Offset: 0x001DBE1B
		public void SelectSupButton(bool bSet)
		{
			if (bSet)
			{
				this.m_supportListButton.On();
				return;
			}
			this.m_supportListButton.Off();
		}

		// Token: 0x04004C1C RID: 19484
		private bool m_bOpen;

		// Token: 0x04004C1D RID: 19485
		public RectTransform m_rtDeckViewList;

		// Token: 0x04004C1E RID: 19486
		public ScrollRect m_scrollRect;

		// Token: 0x04004C1F RID: 19487
		public NKCUIComToggleGroup m_ToggleGroup;

		// Token: 0x04004C20 RID: 19488
		public List<NKCDeckListButton> m_listDeckListButton;

		// Token: 0x04004C21 RID: 19489
		public NKCSimpleButton m_supportListButton;

		// Token: 0x04004C22 RID: 19490
		private int m_iUnlockedDeckCount;

		// Token: 0x04004C23 RID: 19491
		private NKCDeckViewList.OnSelectDeck dOnSelectDeck;

		// Token: 0x04004C24 RID: 19492
		private NKCDeckViewList.OnSelectDeck dOnDeckUnloakRequest;

		// Token: 0x04004C25 RID: 19493
		private NKCDeckViewList.OnChangedMuiltiSelectedDeckCount dOnChangedMuiltiSelectedDeckCount;

		// Token: 0x04004C26 RID: 19494
		private NKCDeckViewList.OnSupportList dOnSupportList;

		// Token: 0x04004C27 RID: 19495
		private NKM_DECK_TYPE m_eCurrentDeckType;

		// Token: 0x04004C28 RID: 19496
		private List<int> m_lstMultiSelectedIndex = new List<int>();

		// Token: 0x020015E0 RID: 5600
		// (Invoke) Token: 0x0600AE85 RID: 44677
		public delegate void OnSelectDeck(NKMDeckIndex index);

		// Token: 0x020015E1 RID: 5601
		// (Invoke) Token: 0x0600AE89 RID: 44681
		public delegate void OnChangedMuiltiSelectedDeckCount(int selectedCount);

		// Token: 0x020015E2 RID: 5602
		// (Invoke) Token: 0x0600AE8D RID: 44685
		public delegate void OnSupportList();
	}
}
