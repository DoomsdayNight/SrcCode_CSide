using System;
using System.Collections.Generic;
using System.Text;
using ClientPacket.Contract;
using NKC.Templet;
using NKC.UI.Component;
using NKC.UI.Tooltip;
using NKM;
using NKM.Contract2;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Contract
{
	// Token: 0x02000BF0 RID: 3056
	public class NKCUIContractV3 : NKCUIBase
	{
		// Token: 0x17001695 RID: 5781
		// (get) Token: 0x06008DC8 RID: 36296 RVA: 0x00303BF6 File Offset: 0x00301DF6
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001696 RID: 5782
		// (get) Token: 0x06008DC9 RID: 36297 RVA: 0x00303BF9 File Offset: 0x00301DF9
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_CONTRACT;
			}
		}

		// Token: 0x17001697 RID: 5783
		// (get) Token: 0x06008DCA RID: 36298 RVA: 0x00303C00 File Offset: 0x00301E00
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Normal;
			}
		}

		// Token: 0x17001698 RID: 5784
		// (get) Token: 0x06008DCB RID: 36299 RVA: 0x00303C03 File Offset: 0x00301E03
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.DEFAULT;
			}
		}

		// Token: 0x17001699 RID: 5785
		// (get) Token: 0x06008DCC RID: 36300 RVA: 0x00303C06 File Offset: 0x00301E06
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_SYSTEM_CONTRACT";
			}
		}

		// Token: 0x1700169A RID: 5786
		// (get) Token: 0x06008DCD RID: 36301 RVA: 0x00303C0D File Offset: 0x00301E0D
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return this.BuildUpsideMenuResources();
			}
		}

		// Token: 0x06008DCE RID: 36302 RVA: 0x00303C18 File Offset: 0x00301E18
		private List<int> BuildUpsideMenuResources()
		{
			HashSet<int> other = new HashSet<int>
			{
				401
			};
			HashSet<int> hashSet = new HashSet<int>();
			ContractTempletBase contractTempletBase = ContractTempletBase.FindBase(this.m_SelectedContractID);
			hashSet.UnionWith(contractTempletBase.GetPriceItemIDSet());
			bool flag = hashSet.Remove(102);
			hashSet.ExceptWith(other);
			hashSet.Remove(0);
			List<int> list = new List<int>();
			ContractTempletV2 contractTempletV = contractTempletBase as ContractTempletV2;
			if (contractTempletV != null)
			{
				foreach (RewardUnit rewardUnit in contractTempletV.m_ResultRewards)
				{
					if (rewardUnit.RewardType == NKM_REWARD_TYPE.RT_MISC && rewardUnit.Count > 0)
					{
						list.Add(rewardUnit.ItemID);
					}
				}
			}
			List<int> list2 = new List<int>(hashSet);
			list2.Sort((int x, int y) => y.CompareTo(x));
			List<int> list3 = new List<int>();
			list3.AddRange(list);
			list3.AddRange(list2);
			if (flag)
			{
				list3.Add(102);
			}
			if (list3.Count == 0)
			{
				list3.Add(1001);
				list3.Add(1034);
				list3.Add(101);
			}
			return list3;
		}

		// Token: 0x06008DCF RID: 36303 RVA: 0x00303D64 File Offset: 0x00301F64
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06008DD0 RID: 36304 RVA: 0x00303D74 File Offset: 0x00301F74
		public void Init()
		{
			if (this.m_btnContractPool != null)
			{
				this.m_btnContractPool.PointerClick.RemoveAllListeners();
				this.m_btnContractPool.PointerClick.AddListener(new UnityAction(this.OnClickPool));
			}
			if (this.m_btnShopShortcut != null)
			{
				this.m_btnShopShortcut.PointerClick.RemoveAllListeners();
				this.m_btnShopShortcut.PointerClick.AddListener(new UnityAction(this.OnClickShortcut));
			}
			if (this.m_btnContractLeft != null)
			{
				this.m_btnContractLeft.Init();
			}
			if (this.m_btnContractRight != null)
			{
				this.m_btnContractRight.Init();
			}
			if (this.m_btnSelectContract != null)
			{
				this.m_btnSelectContract.Init();
				this.m_btnSelectContract.PointerClick.RemoveAllListeners();
				this.m_btnSelectContract.PointerClick.AddListener(new UnityAction(this.OnClickSelectContract));
			}
			if (this.m_btnConfirmContract != null)
			{
				this.m_btnSelectContract.Init();
			}
			if (this.m_btnConfirmation != null)
			{
				this.m_btnConfirmation.PointerClick.RemoveAllListeners();
				this.m_btnConfirmation.PointerClick.AddListener(new UnityAction(this.OnClickConfirmation));
			}
			if (this.m_btnConfirmationInfo != null)
			{
				this.m_btnConfirmationInfo.PointerDown.RemoveAllListeners();
				this.m_btnConfirmationInfo.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnClickConfirmationTooltip));
				this.m_btnConfirmationInfo.PointerUp.RemoveAllListeners();
				this.m_btnConfirmationInfo.PointerUp.AddListener(delegate()
				{
					NKCUITooltip.Instance.Close();
				});
			}
			if (this.m_flTabs != null)
			{
				this.m_flTabs.m_pfbMajor = this.m_pfbTab;
				this.m_flTabs.m_pfbMinor = this.m_pfbSubTab;
				this.m_flTabs.m_bCanSelectMajor = false;
			}
			NKCUtil.SetBindFunction(this.m_csbtnToolTip, delegate()
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_CONTRACT_COUNT_CLOSE_TOOLTIP_TITLE, NKCUtilString.GET_STRING_CONTRACT_COUNT_CLOSE_TOOLTIP_DESC, null, "");
			});
			this.m_iCurRemainFreeContractCount = 0;
		}

		// Token: 0x06008DD1 RID: 36305 RVA: 0x00303FA8 File Offset: 0x003021A8
		private void OnDestroy()
		{
			for (int i = 0; i < NKCUIContractV3.m_listNKCAssetResourceData.Count; i++)
			{
				NKCAssetResourceManager.CloseInstance(NKCUIContractV3.m_listNKCAssetResourceData[i]);
			}
			NKCUIContractV3.m_listNKCAssetResourceData.Clear();
		}

		// Token: 0x06008DD2 RID: 36306 RVA: 0x00303FE4 File Offset: 0x003021E4
		public override void OnBackButton()
		{
			base.OnBackButton();
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, false);
		}

		// Token: 0x06008DD3 RID: 36307 RVA: 0x00303FF8 File Offset: 0x003021F8
		public void Open(string reservedOpenID = "")
		{
			this.BuildFoldableList();
			if (!string.IsNullOrEmpty(reservedOpenID))
			{
				this.SelectRecruitBanner(reservedOpenID);
				this.m_strReservedOpenID = reservedOpenID;
			}
			else
			{
				this.SelectFirstBanner();
			}
			this.CheckInstantContractList();
			this.UpdateTabRedDot();
			this.m_LastUIOpenTimeUTC = NKCSynchronizedTime.GetServerUTCTime(0.0);
			base.UIOpened(true);
			this.TutorialCheck();
		}

		// Token: 0x06008DD4 RID: 36308 RVA: 0x00304056 File Offset: 0x00302256
		private void CheckInstantContractList()
		{
			NKCPacketSender.NKMPacket_INSTANT_CONTRACT_LIST_REQ();
		}

		// Token: 0x06008DD5 RID: 36309 RVA: 0x00304060 File Offset: 0x00302260
		private void BuildFoldableList()
		{
			this.m_dicTemplet.Clear();
			NKCContractDataMgr nkccontractDataMgr = NKCScenManager.GetScenManager().GetNKCContractDataMgr();
			List<NKCUIComFoldableList.Element> list = new List<NKCUIComFoldableList.Element>();
			foreach (ContractTempletBase contractTempletBase in NKMTempletContainer<ContractTempletBase>.Values)
			{
				if (NKCSynchronizedTime.IsEventTime(contractTempletBase.EventIntervalTemplet) && (nkccontractDataMgr == null || nkccontractDataMgr.CheckOpenCond(contractTempletBase)))
				{
					NKCUIComFoldableList.Element item = default(NKCUIComFoldableList.Element);
					if (!this.m_dicTemplet.ContainsKey(contractTempletBase.Category))
					{
						NKCContractCategoryTemplet nkccontractCategoryTemplet = NKCContractCategoryTemplet.Find(contractTempletBase.Category);
						if (nkccontractCategoryTemplet == null)
						{
							Debug.LogError(string.Format("ContractCategoryTemplet is null - id : {0}", contractTempletBase.Category));
							continue;
						}
						if (nkccontractCategoryTemplet.m_Type == NKCContractCategoryTemplet.TabType.Hidden)
						{
							continue;
						}
						item.MajorKey = nkccontractCategoryTemplet.m_CategoryID;
						item.MinorKey = nkccontractCategoryTemplet.IDX;
						item.isMajor = true;
						item.MinorSortKey = nkccontractCategoryTemplet.IDX;
						list.Add(item);
						this.m_dicTemplet.Add(contractTempletBase.Category, new List<ContractTempletBase>());
					}
					item.MajorKey = contractTempletBase.Category;
					item.MinorKey = contractTempletBase.Key;
					item.isMajor = false;
					item.MinorSortKey = contractTempletBase.Order;
					list.Add(item);
					this.m_dicTemplet[contractTempletBase.Category].Add(contractTempletBase);
				}
			}
			this.m_flTabs.BuildList(list, new NKCUIComFoldableList.OnSelectList(this.OnSelectTab));
		}

		// Token: 0x06008DD6 RID: 36310 RVA: 0x00304204 File Offset: 0x00302404
		public void ResetContractUI(bool bForce = false)
		{
			this.RefreshBanner(bForce);
			this.UpdateChildUI();
			base.UpdateUpsideMenu();
			this.UpdateTabRedDot();
		}

		// Token: 0x06008DD7 RID: 36311 RVA: 0x00304220 File Offset: 0x00302420
		private void UpdateTabRedDot()
		{
			NKCContractDataMgr nkccontractDataMgr = NKCScenManager.GetScenManager().GetNKCContractDataMgr();
			foreach (KeyValuePair<int, List<ContractTempletBase>> keyValuePair in this.m_dicTemplet)
			{
				NKCUIContractListSlot nkcuicontractListSlot = this.m_flTabs.GetMajorSlot(keyValuePair.Key) as NKCUIContractListSlot;
				if (nkcuicontractListSlot != null)
				{
					nkcuicontractListSlot.SetActiveRedDot(false);
				}
				for (int i = 0; i < keyValuePair.Value.Count; i++)
				{
					int key = keyValuePair.Value[i].Key;
					bool flag = false;
					if (!NKCSynchronizedTime.IsFinished(nkccontractDataMgr.GetNextResetTime(key)))
					{
						flag = (nkccontractDataMgr.GetRemainFreeChangeCnt(keyValuePair.Value[i].Key) > 0 && nkccontractDataMgr.CheckOpenCond(keyValuePair.Value[i]));
					}
					else
					{
						ContractTempletV2 contractTempletV = keyValuePair.Value[i] as ContractTempletV2;
						if (contractTempletV != null && contractTempletV.m_FreeTryCnt > 0)
						{
							if (contractTempletV.m_resetFreeCount)
							{
								flag = (contractTempletV.m_FreeTryCnt > 0);
							}
							else if (!NKCSynchronizedTime.IsFinished(contractTempletV.GetDateStartUtc().AddDays((double)contractTempletV.m_freeCountDays)))
							{
								flag = true;
							}
						}
					}
					NKCUIContractListSlot nkcuicontractListSlot2 = this.m_flTabs.GetMinorSlot(keyValuePair.Key, keyValuePair.Value[i].Key) as NKCUIContractListSlot;
					if (nkcuicontractListSlot2 != null)
					{
						nkcuicontractListSlot2.SetActiveRedDot(flag);
					}
					if (flag)
					{
						NKCUIContractListSlot nkcuicontractListSlot3 = this.m_flTabs.GetMajorSlot(keyValuePair.Key) as NKCUIContractListSlot;
						if (nkcuicontractListSlot3 != null)
						{
							nkcuicontractListSlot3.SetActiveRedDot(flag);
						}
					}
				}
			}
		}

		// Token: 0x06008DD8 RID: 36312 RVA: 0x003043E0 File Offset: 0x003025E0
		private void UpdateShopPackageItem()
		{
			if (this.m_ShopItem == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_ShopItem, false);
			bool flag = NKMOpenTagManager.IsOpened("CLASSIFIED_CONTRACT_BUY_BUTTON");
			ContractTempletV2 contractTempletV = ContractTempletV2.Find(this.m_SelectedContractID);
			if (contractTempletV == null)
			{
				return;
			}
			NKCContractCategoryTemplet nkccontractCategoryTemplet = NKCContractCategoryTemplet.Find(contractTempletV.Category);
			if (nkccontractCategoryTemplet == null)
			{
				return;
			}
			switch (nkccontractCategoryTemplet.m_Type)
			{
			case NKCContractCategoryTemplet.TabType.Basic:
				return;
			case NKCContractCategoryTemplet.TabType.FollowTarget:
			{
				bool flag2 = false;
				foreach (RandomUnitTempletV2 randomUnitTempletV in contractTempletV.UnitPoolTemplet.UnitTemplets)
				{
					if (randomUnitTempletV.PickUpTarget && randomUnitTempletV.UnitTemplet.m_bAwaken)
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					return;
				}
				break;
			}
			case NKCContractCategoryTemplet.TabType.Confirm:
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_ShopItem, flag);
			if (flag)
			{
				this.m_ShopItem.SetData();
			}
		}

		// Token: 0x06008DD9 RID: 36313 RVA: 0x003044DC File Offset: 0x003026DC
		protected void RefreshBanner(bool bForce = false)
		{
			if (!this.CheckContractEnd() && !bForce)
			{
				return;
			}
			if (NKCPopupResourceConfirmBox.IsInstanceOpen)
			{
				NKCPopupResourceConfirmBox.Instance.Close();
			}
			this.BuildFoldableList();
			if (bForce && !string.IsNullOrEmpty(this.m_strReservedOpenID))
			{
				this.SelectRecruitBanner(this.m_strReservedOpenID);
				this.m_strReservedOpenID = "";
			}
			else
			{
				this.SelectFirstBanner();
			}
			base.UpdateUpsideMenu();
		}

		// Token: 0x06008DDA RID: 36314 RVA: 0x00304544 File Offset: 0x00302744
		private bool CheckContractEnd()
		{
			NKCContractDataMgr nkccontractDataMgr = NKCScenManager.GetScenManager().GetNKCContractDataMgr();
			foreach (List<ContractTempletBase> list in this.m_dicTemplet.Values)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (nkccontractDataMgr != null && !nkccontractDataMgr.CheckOpenCond(list[i]) && list[i].Key == this.m_SelectedContractID)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06008DDB RID: 36315 RVA: 0x003045E0 File Offset: 0x003027E0
		public void OnContractCompleteAck()
		{
			this.RefreshBanner(false);
			this.UpdateChildUI();
			this.UpdateTabRedDot();
		}

		// Token: 0x06008DDC RID: 36316 RVA: 0x003045F8 File Offset: 0x003027F8
		public void UpdateChildUI()
		{
			ContractTempletBase contractTempletBase = ContractTempletBase.FindBase(this.m_SelectedContractID);
			if (contractTempletBase is SelectableContractTemplet)
			{
				SelectableContractTemplet selectableContractTemplet = contractTempletBase as SelectableContractTemplet;
				NKCUtil.SetGameobjectActive(this.m_btnContractLeft, false);
				NKCUtil.SetGameobjectActive(this.m_btnContractRight, false);
				NKCUtil.SetGameobjectActive(this.m_btnSelectContract, true);
				NKCUtil.SetGameobjectActive(this.m_btnConfirmContract, false);
				NKCUtil.SetGameobjectActive(this.m_objEvent, false);
				NKCUtil.SetGameobjectActive(this.m_btnConfirmation, false);
				int selectableContractChangeCnt = NKCScenManager.GetScenManager().GetNKCContractDataMgr().GetSelectableContractChangeCnt(contractTempletBase.Key);
				NKCUtil.SetLabelText(this.m_lbSelectContractRemainCount, string.Format("{0}/{1}", selectableContractChangeCnt, selectableContractTemplet.m_UnitPoolChangeCount));
				NKCUtil.SetGameobjectActive(this.m_btnShopShortcut, false);
				return;
			}
			if (contractTempletBase is ContractTempletV2)
			{
				NKCContractCategoryTemplet nkccontractCategoryTemplet = NKCContractCategoryTemplet.Find(contractTempletBase.Category);
				if (nkccontractCategoryTemplet != null)
				{
					if (nkccontractCategoryTemplet.m_Type == NKCContractCategoryTemplet.TabType.Confirm)
					{
						NKCUtil.SetGameobjectActive(this.m_btnContractLeft, false);
						NKCUtil.SetGameobjectActive(this.m_btnContractRight, false);
						NKCUtil.SetGameobjectActive(this.m_btnSelectContract, false);
						NKCUtil.SetGameobjectActive(this.m_btnConfirmContract, true);
						NKCUtil.SetGameobjectActive(this.m_objEvent, false);
						NKCUtil.SetGameobjectActive(this.m_btnConfirmation, false);
						NKCUtil.SetGameobjectActive(this.m_btnShopShortcut, false);
						ContractTempletV2 contractTempletV = contractTempletBase as ContractTempletV2;
						for (int i = 0; i < contractTempletV.m_SingleTryRequireItems.Length; i++)
						{
							MiscItemUnit reqItem = contractTempletV.m_SingleTryRequireItems[i];
							if (reqItem != null)
							{
								ContractCostType costTypeLeft;
								if (i == 0)
								{
									costTypeLeft = ContractCostType.Ticket;
								}
								else
								{
									costTypeLeft = ContractCostType.Money;
								}
								NKCUtil.SetLabelText(this.m_lbConfirmContractDesc, string.Format(NKCUtilString.GET_STRING_CONTRACT_BUTTON_DESC, 1));
								this.m_btnConfirmContract.PointerClick.RemoveAllListeners();
								this.m_btnConfirmContract.SetData(reqItem.ItemId, reqItem.Count32);
								if (NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(reqItem.ItemId) >= reqItem.Count)
								{
									NKMItemMiscTemplet miscTemplet = NKMItemMiscTemplet.Find(reqItem.ItemId);
									if (miscTemplet != null)
									{
										this.m_costTypeLeft = costTypeLeft;
										this.m_btnConfirmContract.PointerClick.AddListener(delegate()
										{
											NKCPopupResourceConfirmBox.Instance.OpenForContract(NKCUtilString.GET_STRING_NOTICE, string.Format(NKCUtilString.GET_STRING_CONTRACT_REQ_DESC_03, miscTemplet.GetItemName(), reqItem.Count, 1), reqItem.ItemId, reqItem.Count32, delegate
											{
												NKCPacketSender.Send_NKMPacket_CONTRACT_REQ(this.m_SelectedContractID, this.m_costTypeLeft, 1);
											}, null, this.m_SelectedContractID, 1);
										});
										return;
									}
								}
								else
								{
									this.m_btnConfirmContract.PointerClick.AddListener(delegate()
									{
										NKCShopManager.OpenItemLackPopup(reqItem.ItemId, reqItem.Count32);
									});
								}
							}
						}
						return;
					}
					NKCUtil.SetGameobjectActive(this.m_btnContractLeft, true);
					NKCUtil.SetGameobjectActive(this.m_btnContractRight, true);
					NKCUtil.SetGameobjectActive(this.m_btnSelectContract, false);
					NKCUtil.SetGameobjectActive(this.m_btnConfirmContract, false);
					NKCUtil.SetGameobjectActive(this.m_btnShopShortcut, (contractTempletBase as ContractTempletV2).m_ResultRewards.Find((RewardUnit x) => x.ItemID == 401) != null);
					ContractTempletV2 templet = contractTempletBase as ContractTempletV2;
					this.UpdateContractLimitUI(templet);
					this.UpdateBonusMileage(templet);
					int num;
					bool flag;
					this.UpdateFreeContractUI(templet, out num, out flag);
					if (num <= 0 && !flag)
					{
						this.UpdateContractUI(templet, num);
					}
				}
			}
		}

		// Token: 0x06008DDD RID: 36317 RVA: 0x00304928 File Offset: 0x00302B28
		private void UpdateContractLimitUI(ContractTempletV2 templet)
		{
			if (templet == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objEvent, true);
			this.m_iCurContractLimitCount = -1;
			NKCContractDataMgr nkccontractDataMgr = NKCScenManager.GetScenManager().GetNKCContractDataMgr();
			if (nkccontractDataMgr.hasContractLimit(this.m_SelectedContractID))
			{
				this.m_iCurContractLimitCount = nkccontractDataMgr.GetContractLimitCnt(this.m_SelectedContractID);
			}
			NKCUtil.SetGameobjectActive(this.m_objEventRemainCount, this.m_iCurContractLimitCount >= 0);
			NKCUtil.SetGameobjectActive(this.m_objToolTip, this.m_SelectedContractID == this.m_iDisplayToolTipTargetContractID);
			if (this.m_objEventRemainCount.activeSelf)
			{
				NKCUtil.SetLabelText(this.m_lbEventRemainCount, string.Format(NKCUtilString.GET_STRING_CONTRACT_REMAIN_COUNT_DESC, this.m_iCurContractLimitCount));
			}
			NKCUtil.SetGameobjectActive(this.m_objEventDate, !string.IsNullOrEmpty(templet.ContractDescId));
			if (this.m_objEventDate.activeSelf)
			{
				NKCUtil.SetLabelText(this.m_lbEventDate, NKCStringTable.GetString(templet.ContractDescId, false));
			}
			if (nkccontractDataMgr.IsDailyContractLimit(templet))
			{
				this.m_bUpdateLimitCount = true;
				return;
			}
			this.m_bUpdateLimitCount = false;
		}

		// Token: 0x06008DDE RID: 36318 RVA: 0x00304A2C File Offset: 0x00302C2C
		private void UpdateBonusMileage(ContractTempletV2 templet)
		{
			NKCContractDataMgr nkccontractDataMgr = NKCScenManager.GetScenManager().GetNKCContractDataMgr();
			bool flag = false;
			if (nkccontractDataMgr != null && templet.m_ContractBonusCountGroupID != 0)
			{
				flag = nkccontractDataMgr.IsActiveContrctConfirmation(templet.Key);
			}
			if (flag)
			{
				int contractBonusCnt = NKCScenManager.GetScenManager().GetNKCContractDataMgr().GetContractBonusCnt(templet.m_ContractBonusCountGroupID);
				int contractBounsItemReqireCount = templet.m_ContractBounsItemReqireCount;
				if (contractBounsItemReqireCount - contractBonusCnt <= 1)
				{
					NKCUtil.SetLabelText(this.m_lbConfirmationCount, NKCUtilString.GET_STRING_CONTRACT_CONFIRMATION_DESC);
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbConfirmationCount, string.Format("<color=#FFCF3B>{0}</color>/{1}", contractBonusCnt, contractBounsItemReqireCount));
				}
				NKCUtil.SetGameobjectActive(this.m_btnConfirmation, true);
				List<NKMUnitTempletBase> lstGetableUnit = new List<NKMUnitTempletBase>();
				foreach (RandomUnitTempletV2 randomUnitTempletV in templet.UnitPoolTemplet.UnitTemplets)
				{
					if (randomUnitTempletV.PickUpTarget && randomUnitTempletV.UnitTemplet.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_SSR)
					{
						lstGetableUnit.Add(randomUnitTempletV.UnitTemplet);
					}
				}
				List<int> curSelectableUnitList = NKCScenManager.GetScenManager().GetNKCContractDataMgr().GetCurSelectableUnitList(templet.Key);
				bool flag2 = true;
				int cnt2;
				int cnt;
				Predicate<int> <>9__0;
				for (cnt = 0; cnt < this.m_lstSDFace.Count; cnt = cnt2)
				{
					if (cnt < lstGetableUnit.Count)
					{
						NKCUtil.SetGameobjectActive(this.m_lstSDFace[cnt].m_objRoot, true);
						if (curSelectableUnitList.Count > 0)
						{
							List<int> list = curSelectableUnitList;
							Predicate<int> match;
							if ((match = <>9__0) == null)
							{
								match = (<>9__0 = ((int e) => e == lstGetableUnit[cnt].m_UnitID));
							}
							flag2 = (list.Find(match) > 0);
						}
						NKCUtil.SetImageSprite(this.m_lstSDFace[cnt].m_imgFace, NKCResourceUtility.GetOrLoadMinimapFaceIcon(lstGetableUnit[cnt], false), false);
						NKCUtil.SetGameobjectActive(this.m_lstSDFace[cnt].m_objCheck, !flag2);
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_lstSDFace[cnt].m_objRoot, false);
					}
					cnt2 = cnt + 1;
				}
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_btnConfirmation, false);
		}

		// Token: 0x06008DDF RID: 36319 RVA: 0x00304C80 File Offset: 0x00302E80
		private void UpdateFreeContractUI(ContractTempletV2 templet, out int remainFreeChance, out bool bSkipUpdateContractUI)
		{
			bSkipUpdateContractUI = false;
			remainFreeChance = 0;
			this.m_iCurRemainFreeContractCount = 0;
			this.m_btnContractLeft.PointerClick.RemoveAllListeners();
			this.m_btnContractRight.PointerClick.RemoveAllListeners();
			NKCContractDataMgr nkccontractDataMgr = NKCScenManager.GetScenManager().GetNKCContractDataMgr();
			if (nkccontractDataMgr != null)
			{
				if (templet.m_FreeTryCnt > 0)
				{
					if (nkccontractDataMgr.IsHasContractStateData(templet.Key) && nkccontractDataMgr.IsActiveNextFreeChance(templet.Key))
					{
						NKCPacketSender.Send_NKMPacket_CONTRACT_STATE_LIST_REQ();
						return;
					}
					remainFreeChance = nkccontractDataMgr.GetRemainFreeChangeCnt(templet.Key);
					if (nkccontractDataMgr.hasContractLimit(this.m_SelectedContractID) && this.m_iCurContractLimitCount >= 0)
					{
						remainFreeChance = Mathf.Min(remainFreeChance, this.m_iCurContractLimitCount);
					}
					if (!templet.m_resetFreeCount)
					{
						bool flag = NKCSynchronizedTime.IsFinished(templet.EventIntervalTemplet.GetStartDateUtc().AddDays((double)templet.m_freeCountDays));
						NKCUtil.SetGameobjectActive(this.m_objFreeLeft, !flag);
						NKCUtil.SetGameobjectActive(this.m_objFreeRight, !flag);
						if (!flag)
						{
							NKCUtil.SetLabelText(this.m_lbFreeLeft, string.Format(NKCStringTable.GetString("SI_CONTRACT_RESET_LIMIT", false), templet.m_FreeTryCnt));
							NKCUtil.SetLabelText(this.m_lbFreeRight, string.Format(NKCStringTable.GetString("SI_CONTRACT_RESET_LIMIT", false), templet.m_FreeTryCnt));
						}
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_objFreeLeft, remainFreeChance > 0);
						NKCUtil.SetGameobjectActive(this.m_objFreeRight, remainFreeChance > 0);
						NKCUtil.SetLabelText(this.m_lbFreeLeft, NKCStringTable.GetString("SI_DP_CONTRACT_RESET_TIME_DESC", false));
						NKCUtil.SetLabelText(this.m_lbFreeRight, NKCStringTable.GetString("SI_DP_CONTRACT_RESET_TIME_DESC", false));
					}
					if (remainFreeChance >= 1)
					{
						this.m_btnContractLeft.PointerClick.AddListener(new UnityAction(this.OnClickFreeContract));
						this.m_btnContractRight.PointerClick.AddListener(new UnityAction(this.OnClickFreeContractMulti));
						NKCUtil.SetGameobjectActive(this.m_imgContractLeftIcon, false);
						NKCUtil.SetGameobjectActive(this.m_imgContractRightIcon, false);
						NKCUtil.SetImageSprite(this.m_imgContractLeftBG, NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_YELLOW), false);
						NKCUtil.SetLabelText(this.m_lbContractLeftDesc, string.Format(NKCUtilString.GET_STRING_CONTRACT_FREE_BUTTON_DESC_01, 1));
						NKCUtil.SetLabelTextColor(this.m_lbContractLeftDesc, (remainFreeChance >= 1) ? NKCUtil.GetColor("#582817") : NKCUtil.GetColor("#212122"));
						NKCUtil.SetLabelText(this.m_lbContractLeftCount, string.Format("1/{0}", remainFreeChance));
						NKCUtil.SetLabelTextColor(this.m_lbContractLeftCount, NKCUtil.GetColor("#FFCF3B"));
						if (remainFreeChance > 1)
						{
							this.m_iCurRemainFreeContractCount = Math.Min(remainFreeChance, 10);
							NKCUtil.SetLabelText(this.m_lbContractRightDesc, string.Format(NKCUtilString.GET_STRING_CONTRACT_FREE_BUTTON_DESC_01, this.m_iCurRemainFreeContractCount));
							NKCUtil.SetLabelText(this.m_lbContractRightCount, string.Format("{0}/{1}", this.m_iCurRemainFreeContractCount, remainFreeChance));
						}
						else
						{
							NKCUtil.SetLabelText(this.m_lbContractRightDesc, NKCUtilString.GET_STRING_CONTRACT_FREE_BUTTON_DESC);
							NKCUtil.SetLabelText(this.m_lbContractRightCount, "-");
						}
						Sprite sp = (remainFreeChance > 1) ? NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_YELLOW) : NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY);
						NKCUtil.SetImageSprite(this.m_imgContractRightBG, sp, false);
						NKCUtil.SetLabelTextColor(this.m_lbContractRightDesc, (remainFreeChance > 1) ? NKCUtil.GetColor("#582817") : NKCUtil.GetColor("#212122"));
						NKCUtil.SetLabelTextColor(this.m_lbContractRightCount, (remainFreeChance > 1) ? NKCUtil.GetColor("#FFCF3B") : NKCUtil.GetColor("#212122"));
						return;
					}
					if (templet.IsFreeOnlyContract)
					{
						bSkipUpdateContractUI = true;
						NKCUtil.SetBindFunction(this.m_btnContractLeft, delegate()
						{
							NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_CONTRACT_FREE_TRY_EXIT_DESC, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
						});
						NKCUtil.SetBindFunction(this.m_btnContractRight, delegate()
						{
							NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_CONTRACT_FREE_TRY_EXIT_DESC, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
						});
						NKCUtil.SetGameobjectActive(this.m_imgContractLeftIcon, false);
						NKCUtil.SetGameobjectActive(this.m_imgContractRightIcon, false);
						NKCUtil.SetLabelTextColor(this.m_lbContractLeftDesc, NKCUtil.GetColor("#212122"));
						NKCUtil.SetLabelTextColor(this.m_lbContractRightDesc, NKCUtil.GetColor("#212122"));
						NKCUtil.SetImageSprite(this.m_imgContractLeftBG, NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY), false);
						NKCUtil.SetImageSprite(this.m_imgContractRightBG, NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY), false);
						NKCUtil.SetLabelText(this.m_lbContractLeftDesc, string.Format(NKCUtilString.GET_STRING_CONTRACT_FREE_BUTTON_DESC_01, 1));
						NKCUtil.SetLabelText(this.m_lbContractRightDesc, string.Format(NKCUtilString.GET_STRING_CONTRACT_FREE_BUTTON_DESC_01, 10));
						NKCUtil.SetLabelText(this.m_lbContractLeftCount, "-");
						NKCUtil.SetLabelText(this.m_lbContractRightCount, "-");
						return;
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objFreeLeft, false);
					NKCUtil.SetGameobjectActive(this.m_objFreeRight, false);
				}
			}
		}

		// Token: 0x06008DE0 RID: 36320 RVA: 0x00305118 File Offset: 0x00303318
		private void UpdateContractUI(ContractTempletV2 templet, int FreeContractCnt)
		{
			this.m_iCurMultiContractTryCnt = 0;
			NKMInventoryData inventoryData = NKCScenManager.CurrentUserData().m_InventoryData;
			this.m_btnContractLeft.PointerClick.RemoveAllListeners();
			this.m_btnContractRight.PointerClick.RemoveAllListeners();
			bool flag = false;
			bool flag2 = false;
			if (NKCContractCategoryTemplet.Find(templet.Category) == null)
			{
				return;
			}
			NKCContractDataMgr nkccontractDataMgr = NKCScenManager.GetScenManager().GetNKCContractDataMgr();
			bool flag3 = false;
			if (nkccontractDataMgr != null && templet.m_ContractBonusCountGroupID != 0)
			{
				flag3 = nkccontractDataMgr.IsActiveContrctConfirmation(templet.Key);
			}
			for (int i = 0; i < templet.m_SingleTryRequireItems.Length; i++)
			{
				MiscItemUnit reqItem = templet.m_SingleTryRequireItems[i];
				if (reqItem != null)
				{
					if (flag)
					{
						break;
					}
					ContractCostType contractCostType;
					if (i == 0)
					{
						contractCostType = ContractCostType.Ticket;
					}
					else
					{
						contractCostType = ContractCostType.Money;
					}
					int num = (int)inventoryData.GetCountMiscItem(reqItem.ItemId);
					int val = num / reqItem.Count32;
					if (this.m_iCurContractLimitCount >= 0)
					{
						val = Math.Min(val, this.m_iCurContractLimitCount);
					}
					if (flag3)
					{
						int contractBonusCnt = NKCScenManager.GetScenManager().GetNKCContractDataMgr().GetContractBonusCnt(templet.m_ContractBonusCountGroupID);
						int contractBounsItemReqireCount = templet.m_ContractBounsItemReqireCount;
						val = Math.Min(val, contractBounsItemReqireCount - contractBonusCnt);
					}
					this.m_iCurMultiContractTryCnt = Math.Min(val, 10);
					if (reqItem.ItemId > 0)
					{
						NKCUtil.SetGameobjectActive(this.m_imgContractLeftIcon, true);
						NKCUtil.SetGameobjectActive(this.m_imgContractRightIcon, true);
					}
					NKMItemMiscTemplet miscTemplet = NKMItemManager.GetItemMiscTempletByID(reqItem.ItemId);
					if (!flag)
					{
						this.m_btnContractLeft.PointerClick.RemoveAllListeners();
						this.m_btnContractLeft.SetData(reqItem.ItemId, reqItem.Count32);
						if ((long)num >= reqItem.Count)
						{
							flag = true;
							this.m_costTypeLeft = contractCostType;
							this.m_btnContractLeft.PointerClick.AddListener(delegate()
							{
								NKCPopupResourceConfirmBox.Instance.OpenForContract(NKCUtilString.GET_STRING_NOTICE, string.Format(NKCUtilString.GET_STRING_CONTRACT_REQ_DESC_03, miscTemplet.GetItemName(), reqItem.Count, 1), reqItem.ItemId, reqItem.Count32, delegate
								{
									NKCPacketSender.Send_NKMPacket_CONTRACT_REQ(this.m_SelectedContractID, this.m_costTypeLeft, 1);
								}, null, this.m_SelectedContractID, 1);
							});
						}
						else
						{
							this.m_btnContractLeft.PointerClick.AddListener(delegate()
							{
								NKCShopManager.OpenItemLackPopup(reqItem.ItemId, reqItem.Count32);
							});
						}
					}
					if (!flag2)
					{
						this.m_btnContractRight.PointerClick.RemoveAllListeners();
						this.m_btnContractRight.SetData(reqItem.ItemId, reqItem.Count32 * this.m_iCurMultiContractTryCnt);
						if (this.m_iCurMultiContractTryCnt > 0)
						{
							if (num >= reqItem.Count32 * this.m_iCurMultiContractTryCnt)
							{
								flag2 = true;
								this.m_costTypeRight = contractCostType;
								this.m_btnContractRight.PointerClick.AddListener(delegate()
								{
									NKCPopupResourceConfirmBox.Instance.OpenForContract(NKCUtilString.GET_STRING_NOTICE, string.Format(NKCUtilString.GET_STRING_CONTRACT_REQ_DESC_03, miscTemplet.GetItemName(), reqItem.Count * (long)this.m_iCurMultiContractTryCnt, this.m_iCurMultiContractTryCnt), reqItem.ItemId, reqItem.Count32 * this.m_iCurMultiContractTryCnt, delegate
									{
										NKCPacketSender.Send_NKMPacket_CONTRACT_REQ(this.m_SelectedContractID, this.m_costTypeRight, this.m_iCurMultiContractTryCnt);
									}, null, this.m_SelectedContractID, this.m_iCurMultiContractTryCnt);
								});
								NKCUtil.SetLabelText(this.m_lbContractRightDesc, string.Format(NKCUtilString.GET_STRING_CONTRACT_BUTTON_DESC, this.m_iCurMultiContractTryCnt));
								NKCUtil.SetLabelText(this.m_lbContractRightCount, ((long)this.m_iCurMultiContractTryCnt * reqItem.Count).ToString());
							}
							else
							{
								this.m_btnContractRight.PointerClick.AddListener(delegate()
								{
									NKCShopManager.OpenItemLackPopup(reqItem.ItemId, reqItem.Count32 * this.m_iCurMultiContractTryCnt);
								});
							}
						}
						else
						{
							this.m_btnContractRight.SetText("-");
							NKCUtil.SetLabelText(this.m_lbContractRightDesc, string.Format(NKCUtilString.GET_STRING_CONTRACT_BUTTON_DESC, 1));
							NKCUtil.SetLabelText(this.m_lbContractRightCount, "-");
						}
						if (NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(reqItem.ItemId) >= (long)(reqItem.Count32 * this.m_iCurMultiContractTryCnt))
						{
							NKCUtil.SetLabelTextColor(this.m_lbContractRightCount, NKCUtil.GetColor("#FFFFFF"));
						}
						else
						{
							NKCUtil.SetLabelTextColor(this.m_lbContractRightCount, NKCUtil.GetColor("#CD2121"));
						}
					}
				}
			}
			NKCUtil.SetLabelText(this.m_lbContractLeftDesc, string.Format(NKCUtilString.GET_STRING_CONTRACT_BUTTON_DESC, 1));
			Color col = (this.m_iCurMultiContractTryCnt > 0) ? NKCUtil.GetColor("#582817") : NKCUtil.GetColor("#212122");
			Sprite sp = (this.m_iCurMultiContractTryCnt > 0) ? NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_YELLOW) : NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY);
			NKCUtil.SetImageSprite(this.m_imgContractRightBG, sp, false);
			NKCUtil.SetLabelTextColor(this.m_lbContractRightDesc, col);
			if (this.m_bUpdateLimitCount)
			{
				NKCUtil.SetLabelTextColor(this.m_lbContractLeftDesc, NKCUtil.GetColor("#212122"));
				NKCUtil.SetImageSprite(this.m_imgContractLeftBG, NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY), false);
				NKCUtil.SetImageSprite(this.m_imgContractRightBG, NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY), false);
				this.m_btnContractLeft.PointerClick.RemoveAllListeners();
				this.m_btnContractRight.PointerClick.RemoveAllListeners();
				return;
			}
			NKCUtil.SetLabelTextColor(this.m_lbContractLeftDesc, NKCUtil.GetColor("#582817"));
			NKCUtil.SetImageSprite(this.m_imgContractLeftBG, NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_YELLOW), false);
		}

		// Token: 0x06008DE1 RID: 36321 RVA: 0x003055C0 File Offset: 0x003037C0
		private void UpdateContractRemainTime()
		{
			NKCUtil.SetGameobjectActive(this.m_objRemainTime, true);
			NKCUtil.SetGameobjectActive(this.m_lbRemainTime, true);
			NKCUtil.SetLabelText(this.m_lbRemainTime, NKCUtilString.GetRemainTimeStringEx(this.m_CurDateEndTime));
			if (NKCSynchronizedTime.IsFinished(this.m_CurDateEndTime))
			{
				this.m_bUpdateTime = false;
			}
		}

		// Token: 0x06008DE2 RID: 36322 RVA: 0x00305610 File Offset: 0x00303810
		private bool UpdateLimitContractRemainTime()
		{
			if (!NKCSynchronizedTime.IsFinished(this.m_CurLimitContractEndTimeUTC))
			{
				TimeSpan timeLeft = NKCSynchronizedTime.GetTimeLeft(this.m_CurLimitContractEndTimeUTC);
				string msg;
				if (timeLeft.Days > 0)
				{
					msg = string.Format(NKCUtilString.GET_STRING_CONTRACT_LIMIT_TIME_DAY, timeLeft.Days, timeLeft.Hours);
				}
				else
				{
					msg = string.Format(NKCUtilString.GET_STRING_CONTRACT_LIMIT_TIME_HOUR, timeLeft.Hours, timeLeft.Minutes);
				}
				NKCUtil.SetLabelText(this.m_lbTimeLimitContract, msg);
				return true;
			}
			return false;
		}

		// Token: 0x06008DE3 RID: 36323 RVA: 0x0030569C File Offset: 0x0030389C
		private void SelectFirstBanner()
		{
			ContractTempletBase contractTempletBase = null;
			int num = 0;
			foreach (List<ContractTempletBase> list in this.m_dicTemplet.Values)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (contractTempletBase == null)
					{
						contractTempletBase = list[i];
					}
					if (num < list[i].Priority)
					{
						num = list[i].Priority;
						contractTempletBase = list[i];
					}
					else if (num == list[i].Priority && contractTempletBase.Key < list[i].Key)
					{
						contractTempletBase = list[i];
					}
				}
			}
			if (contractTempletBase != null)
			{
				this.SelectRecruitBanner(contractTempletBase, true);
			}
		}

		// Token: 0x06008DE4 RID: 36324 RVA: 0x0030577C File Offset: 0x0030397C
		public void SelectRecruitBanner(string contractStrID)
		{
			ContractTempletBase contractTempletBase = ContractTempletBase.Find(contractStrID);
			if (contractTempletBase != null && NKCScenManager.GetScenManager().GetNKCContractDataMgr().CheckOpenCond(contractTempletBase))
			{
				this.SelectRecruitBanner(contractTempletBase, true);
				return;
			}
			this.SelectFirstBanner();
		}

		// Token: 0x06008DE5 RID: 36325 RVA: 0x003057B4 File Offset: 0x003039B4
		public void SelectRecruitBanner(int contractID)
		{
			this.SelectRecruitBanner(ContractTempletBase.FindBase(contractID), true);
		}

		// Token: 0x06008DE6 RID: 36326 RVA: 0x003057C4 File Offset: 0x003039C4
		public void SelectRecruitBanner(ContractTempletBase templet, bool bForceUpdate = false)
		{
			if (templet == null)
			{
				return;
			}
			if (templet.Key == this.m_SelectedContractID && !bForceUpdate)
			{
				return;
			}
			this.m_flTabs.SelectMinorSlot(templet.Category, templet.Key);
			this.m_bUpdateTime = false;
			if (templet.IsPickUp())
			{
				DateTime serverUTCTime = NKCSynchronizedTime.GetServerUTCTime(-1.0);
				if (NKCSynchronizedTime.GetTimeLeft(templet.GetDateEndUtc()).TotalDays > (double)NKCSynchronizedTime.UNLIMITD_REMAIN_DAYS)
				{
					this.m_bUpdateTime = false;
				}
				else if (!templet.IsAvailableTime(NKMTime.UTCtoLocal(serverUTCTime, 0)))
				{
					NKCUtil.SetLabelText(this.m_lbRemainTime, NKCUtilString.GET_STRING_CONTRACT_END_RECRUIT_TIME);
					this.m_bUpdateTime = false;
				}
				else
				{
					this.m_CurDateEndTime = templet.GetDateEndUtc();
					this.m_bUpdateTime = true;
					this.UpdateContractRemainTime();
					this.m_fDeltaTime = 0f;
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objContractInfo, this.m_bUpdateTime || !string.IsNullOrEmpty(templet.ContractBannerDescID));
			NKCUtil.SetGameobjectActive(this.m_objRemainTime, this.m_bUpdateTime);
			NKCUtil.SetGameobjectActive(this.m_lbContractInfo, !string.IsNullOrEmpty(templet.ContractBannerDescID));
			NKCUtil.SetGameobjectActive(this.m_objSubTitle, !string.IsNullOrEmpty(templet.ContractBannerNameID));
			if (this.m_objSubTitle.activeSelf)
			{
				StringBuilder stringBuilder = new StringBuilder();
				string empty = string.Empty;
				if (templet is ContractTempletV2)
				{
					int key = 0;
					foreach (RandomUnitTempletV2 randomUnitTempletV in (templet as ContractTempletV2).UnitPoolTemplet.UnitTemplets)
					{
						if (randomUnitTempletV.PickUpTarget)
						{
							key = randomUnitTempletV.UnitTemplet.m_UnitID;
							break;
						}
					}
					NKMUnitTempletBase nkmunitTempletBase = NKMUnitTempletBase.Find(key);
					if (nkmunitTempletBase != null)
					{
						stringBuilder.Append(nkmunitTempletBase.GetUnitName());
					}
				}
				NKCUtil.SetLabelText(this.m_lbSubTitle, string.Format(NKCStringTable.GetString(templet.ContractBannerNameID, false), stringBuilder.ToString()));
				stringBuilder.Clear();
			}
			NKCUtil.SetLabelText(this.m_lbContractInfo, NKCStringTable.GetString(templet.ContractBannerDescID, false));
			NKCUtil.SetGameobjectActive(this.m_btnContractPool.gameObject, templet is ContractTempletV2);
			this.m_SelectedContractID = templet.Key;
			(this.m_flTabs.GetMajorSlot(templet.Category) as NKCUIContractListSlot).SetImage(templet.ImageName);
			this.UpdateChildUI();
			NKCUtil.SetGameobjectActive(this.m_objDevTest, false);
			if (this.m_dicSubUI.ContainsKey(this.m_SelectedContractID) && this.m_dicSubUI[this.m_SelectedContractID].gameObject.activeSelf)
			{
				return;
			}
			foreach (NKCUIContractBanner targetMono in this.m_dicSubUI.Values)
			{
				NKCUtil.SetGameobjectActive(targetMono, false);
			}
			if (this.m_dicSubUI.ContainsKey(this.m_SelectedContractID))
			{
				NKCUtil.SetGameobjectActive(this.m_dicSubUI[this.m_SelectedContractID].gameObject, true);
			}
			else
			{
				NKCUIContractBanner nkcuicontractBanner = NKCUIContractV3.OpenInstanceByAssetName<NKCUIContractBanner>(templet.GetMainBannerName(), templet.GetMainBannerName(), this.m_trBannerParent);
				if (nkcuicontractBanner != null)
				{
					this.m_dicSubUI.Add(this.m_SelectedContractID, nkcuicontractBanner);
					nkcuicontractBanner.transform.localPosition = Vector3.zero;
					NKCUtil.SetGameobjectActive(nkcuicontractBanner, true);
					nkcuicontractBanner.SetActiveEventTag(false);
					nkcuicontractBanner.SetEnableAnimator(true);
				}
			}
			NKCContractDataMgr nkccontractDataMgr = NKCScenManager.GetScenManager().GetNKCContractDataMgr();
			if (nkccontractDataMgr != null)
			{
				this.m_CurLimitContractEndTimeUTC = nkccontractDataMgr.GetInstantContractEndDateTime(templet.Key);
				this.m_CurLimitContractEndTimeUTC = NKCSynchronizedTime.ToUtcTime(this.m_CurLimitContractEndTimeUTC);
				TimeSpan timeLeft = NKCSynchronizedTime.GetTimeLeft(this.m_CurLimitContractEndTimeUTC);
				if (!NKCSynchronizedTime.IsFinished(this.m_CurLimitContractEndTimeUTC) && timeLeft.Days < 365)
				{
					this.m_bLimitContractUpateTime = true;
					NKCUtil.SetGameobjectActive(this.m_objTimeLimitContract, true);
				}
				else
				{
					this.m_bLimitContractUpateTime = false;
					NKCUtil.SetGameobjectActive(this.m_objTimeLimitContract, false);
				}
			}
			this.UpdateShopPackageItem();
			base.UpdateUpsideMenu();
		}

		// Token: 0x06008DE7 RID: 36327 RVA: 0x00305BC4 File Offset: 0x00303DC4
		private void Update()
		{
			this.m_fDeltaTime += Time.deltaTime;
			if (this.m_fDeltaTime > 1f)
			{
				this.m_fDeltaTime -= 1f;
				if (this.m_bUpdateTime)
				{
					this.UpdateContractRemainTime();
				}
				if (this.m_bLimitContractUpateTime && !this.UpdateLimitContractRemainTime())
				{
					NKCUtil.SetGameobjectActive(this.m_objTimeLimitContract, false);
					this.m_bLimitContractUpateTime = false;
					this.ResetContractUI(false);
				}
				if (NKCSynchronizedTime.IsFinished(NKMTime.GetNextResetTime(this.m_LastUIOpenTimeUTC, NKM_MISSION_RESET_INTERVAL.DAILY)))
				{
					this.m_LastUIOpenTimeUTC = NKMTime.GetNextResetTime(this.m_LastUIOpenTimeUTC, NKM_MISSION_RESET_INTERVAL.DAILY);
					this.ResetContractUI(false);
				}
			}
		}

		// Token: 0x06008DE8 RID: 36328 RVA: 0x00305C66 File Offset: 0x00303E66
		private void OnSelectTab(int major, int minor)
		{
			this.SelectRecruitBanner(minor);
		}

		// Token: 0x06008DE9 RID: 36329 RVA: 0x00305C70 File Offset: 0x00303E70
		public void OnClickPool()
		{
			ContractTempletV2 contractTempletV = ContractTempletV2.Find(this.m_SelectedContractID);
			if (contractTempletV != null)
			{
				if (NKCSynchronizedTime.IsFinished(contractTempletV.GetDateEndUtc()) || !contractTempletV.IsAvailableTime(NKMTime.UTCtoLocal(NKCSynchronizedTime.GetServerUTCTime(0.0), 0)))
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, string.Format(NKCUtilString.GET_STRING_CONTRACT_POPUP_RATE_EVENT_TIME_OVER_01, contractTempletV.GetDateEnd()), null, "");
					return;
				}
				NKCUIContractPopupRateV2.Instance.Open(contractTempletV);
			}
		}

		// Token: 0x06008DEA RID: 36330 RVA: 0x00305CE6 File Offset: 0x00303EE6
		public void OnClickShortcut()
		{
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_SHOP, "TAB_EXCHANGE_TASK_PLANET", false);
		}

		// Token: 0x06008DEB RID: 36331 RVA: 0x00305CF8 File Offset: 0x00303EF8
		private void OnClickFreeContract()
		{
			NKCPopupResourceConfirmBox.Instance.OpenForContract(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_CONTRACT_FREE_TRY_DESC, 0, 0, delegate
			{
				NKCPacketSender.Send_NKMPacket_CONTRACT_REQ(this.m_SelectedContractID, ContractCostType.FreeChance, 1);
			}, null, this.m_SelectedContractID, 1);
		}

		// Token: 0x06008DEC RID: 36332 RVA: 0x00305D30 File Offset: 0x00303F30
		private void OnClickFreeContractMulti()
		{
			NKCScenManager.GetScenManager().GetNKCContractDataMgr();
			if (this.m_iCurRemainFreeContractCount > 1)
			{
				NKCPopupResourceConfirmBox.Instance.OpenForContract(NKCUtilString.GET_STRING_NOTICE, string.Format(NKCUtilString.GET_STRING_CONTRACT_FREE_02_TRY_DESC_01, this.m_iCurRemainFreeContractCount), 0, 0, delegate
				{
					NKCPacketSender.Send_NKMPacket_CONTRACT_REQ(this.m_SelectedContractID, ContractCostType.FreeChance, this.m_iCurRemainFreeContractCount);
				}, null, this.m_SelectedContractID, this.m_iCurRemainFreeContractCount);
			}
		}

		// Token: 0x06008DED RID: 36333 RVA: 0x00305D90 File Offset: 0x00303F90
		private void OnClickSelectContract()
		{
			ContractTempletBase contractTempletBase = ContractTempletBase.FindBase(this.m_SelectedContractID);
			if (contractTempletBase != null && contractTempletBase is SelectableContractTemplet)
			{
				SelectableContractTemplet selectableTemplet = contractTempletBase as SelectableContractTemplet;
				if (selectableTemplet != null)
				{
					bool flag = false;
					NKCContractDataMgr nkccontractDataMgr = NKCScenManager.GetScenManager().GetNKCContractDataMgr();
					if (nkccontractDataMgr != null && nkccontractDataMgr.GetSelectableContractChangeCnt(selectableTemplet.Key) > 0)
					{
						flag = true;
						NKCUIContractSelection.Instance.Open(nkccontractDataMgr.GetSelectableContractState());
					}
					if (!flag)
					{
						NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, string.Format(NKCUtilString.GET_STRING_SELECTABLE_CONTRACT_DESC, selectableTemplet.GetContractName(), selectableTemplet.m_UnitPoolChangeCount), delegate()
						{
							NKCPacketSender.Send_NKMPacket_SELECTABLE_CONTRACT_CHANGE_POOL_REQ(selectableTemplet.Key);
						}, null, false);
					}
				}
			}
		}

		// Token: 0x06008DEE RID: 36334 RVA: 0x00305E4C File Offset: 0x0030404C
		private void OnClickConfirmation()
		{
			ContractTempletV2 contractTempletV = ContractTempletV2.Find(this.m_SelectedContractID);
			if (contractTempletV == null)
			{
				return;
			}
			NKCPopupContractConfirmation.Instance.Open(contractTempletV);
		}

		// Token: 0x06008DEF RID: 36335 RVA: 0x00305E74 File Offset: 0x00304074
		private void OnClickConfirmationTooltip(PointerEventData e)
		{
			NKCUITooltip.Instance.Open(NKCUISlot.eSlotMode.Etc, NKCUtilString.GET_STRING_CONTRACT_CONFIRM_TOOLTIP_TITLE, NKCUtilString.GET_STRING_CONTRACT_CONFIRM_TOOLTIP_DESC, new Vector2?(e.position));
		}

		// Token: 0x06008DF0 RID: 36336 RVA: 0x00305E98 File Offset: 0x00304098
		public static T OpenInstanceByAssetName<T>(string BundleName, string AssetName, Transform parent) where T : MonoBehaviour
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>(BundleName, AssetName, false, parent);
			if (nkcassetInstanceData != null && nkcassetInstanceData.m_Instant != null)
			{
				GameObject instant = nkcassetInstanceData.m_Instant;
				T t = instant.GetComponent<T>();
				if (t == null)
				{
					t = instant.AddComponent<T>();
				}
				NKCUIContractV3.m_listNKCAssetResourceData.Add(nkcassetInstanceData);
				return t;
			}
			Debug.LogWarning("prefab is null - " + BundleName + "/" + AssetName);
			return default(T);
		}

		// Token: 0x06008DF1 RID: 36337 RVA: 0x00305F0F File Offset: 0x0030410F
		public void TutorialCheck()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.Contract, true);
		}

		// Token: 0x06008DF2 RID: 36338 RVA: 0x00305F19 File Offset: 0x00304119
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			base.OnInventoryChange(itemData);
			this.ResetContractUI(false);
			this.UpdateShopPackageItem();
		}

		// Token: 0x04007ABF RID: 31423
		public const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONTRACT_V2";

		// Token: 0x04007AC0 RID: 31424
		public const string UI_ASSET_NAME = "NKM_UI_CONTRACT_V2";

		// Token: 0x04007AC1 RID: 31425
		[Header("좌측 탭")]
		public NKCUIComFoldableList m_flTabs;

		// Token: 0x04007AC2 RID: 31426
		public NKCUIContractListSlot m_pfbTab;

		// Token: 0x04007AC3 RID: 31427
		public NKCUIContractListSlot m_pfbSubTab;

		// Token: 0x04007AC4 RID: 31428
		[Header("채용 배너 최상단")]
		public Transform m_trBannerParent;

		// Token: 0x04007AC5 RID: 31429
		[Header("좌상단 이벤트")]
		public GameObject m_objEvent;

		// Token: 0x04007AC6 RID: 31430
		public GameObject m_objEventRemainCount;

		// Token: 0x04007AC7 RID: 31431
		public Text m_lbEventRemainCount;

		// Token: 0x04007AC8 RID: 31432
		public GameObject m_objEventDate;

		// Token: 0x04007AC9 RID: 31433
		public Text m_lbEventDate;

		// Token: 0x04007ACA RID: 31434
		public GameObject m_objToolTip;

		// Token: 0x04007ACB RID: 31435
		public NKCUIComStateButton m_csbtnToolTip;

		// Token: 0x04007ACC RID: 31436
		public int m_iDisplayToolTipTargetContractID = 4002;

		// Token: 0x04007ACD RID: 31437
		[Header("우상단 채용확률")]
		public NKCUIComStateButton m_btnContractPool;

		// Token: 0x04007ACE RID: 31438
		[Header("우상단 개발자권한 오브젝트")]
		public GameObject m_objDevTest;

		// Token: 0x04007ACF RID: 31439
		[Header("우하단 확률업 관련")]
		public GameObject m_objSubTitle;

		// Token: 0x04007AD0 RID: 31440
		public Text m_lbSubTitle;

		// Token: 0x04007AD1 RID: 31441
		[Header("우하단 채용안내")]
		public GameObject m_objContractInfo;

		// Token: 0x04007AD2 RID: 31442
		public Text m_lbContractInfo;

		// Token: 0x04007AD3 RID: 31443
		public GameObject m_objRemainTime;

		// Token: 0x04007AD4 RID: 31444
		public Text m_lbRemainTime;

		// Token: 0x04007AD5 RID: 31445
		[Header("상점 바로가기")]
		public NKCUIComStateButton m_btnShopShortcut;

		// Token: 0x04007AD6 RID: 31446
		[Header("채용버튼 왼쪽")]
		public NKCUIComResourceButton m_btnContractLeft;

		// Token: 0x04007AD7 RID: 31447
		public Image m_imgContractLeftBG;

		// Token: 0x04007AD8 RID: 31448
		public Image m_imgContractLeftIcon;

		// Token: 0x04007AD9 RID: 31449
		public Text m_lbContractLeftDesc;

		// Token: 0x04007ADA RID: 31450
		public Text m_lbContractLeftCount;

		// Token: 0x04007ADB RID: 31451
		public GameObject m_objFreeLeft;

		// Token: 0x04007ADC RID: 31452
		public Text m_lbFreeLeft;

		// Token: 0x04007ADD RID: 31453
		[Header("채용버튼 오른쪽")]
		public NKCUIComResourceButton m_btnContractRight;

		// Token: 0x04007ADE RID: 31454
		public Image m_imgContractRightBG;

		// Token: 0x04007ADF RID: 31455
		public Image m_imgContractRightIcon;

		// Token: 0x04007AE0 RID: 31456
		public Text m_lbContractRightDesc;

		// Token: 0x04007AE1 RID: 31457
		public Text m_lbContractRightCount;

		// Token: 0x04007AE2 RID: 31458
		public GameObject m_objFreeRight;

		// Token: 0x04007AE3 RID: 31459
		public Text m_lbFreeRight;

		// Token: 0x04007AE4 RID: 31460
		[Header("선별채용 채용버튼")]
		public NKCUIComResourceButton m_btnSelectContract;

		// Token: 0x04007AE5 RID: 31461
		public Text m_lbSelectContractRemainCount;

		// Token: 0x04007AE6 RID: 31462
		[Header("확정채용 채용버튼")]
		public NKCUIComResourceButton m_btnConfirmContract;

		// Token: 0x04007AE7 RID: 31463
		public Text m_lbConfirmContractDesc;

		// Token: 0x04007AE8 RID: 31464
		[Header("확정채용")]
		public NKCUIComStateButton m_btnConfirmation;

		// Token: 0x04007AE9 RID: 31465
		public Text m_lbConfirmationCount;

		// Token: 0x04007AEA RID: 31466
		public NKCUIComStateButton m_btnConfirmationInfo;

		// Token: 0x04007AEB RID: 31467
		public RectTransform m_Content;

		// Token: 0x04007AEC RID: 31468
		public List<ContractConfirmationSDFace> m_lstSDFace = new List<ContractConfirmationSDFace>(5);

		// Token: 0x04007AED RID: 31469
		[Header("기밀채용 패키지")]
		public NKCUIComShopItem m_ShopItem;

		// Token: 0x04007AEE RID: 31470
		[Header("기간한정 채용")]
		public GameObject m_objTimeLimitContract;

		// Token: 0x04007AEF RID: 31471
		public NKCComTMPUIText m_lbTimeLimitContract;

		// Token: 0x04007AF0 RID: 31472
		private const string m_ShopPackageActiveTag = "CLASSIFIED_CONTRACT_BUY_BUTTON";

		// Token: 0x04007AF1 RID: 31473
		private static List<NKCAssetInstanceData> m_listNKCAssetResourceData = new List<NKCAssetInstanceData>();

		// Token: 0x04007AF2 RID: 31474
		private Dictionary<int, List<ContractTempletBase>> m_dicTemplet = new Dictionary<int, List<ContractTempletBase>>();

		// Token: 0x04007AF3 RID: 31475
		private Dictionary<int, NKCUIContractBanner> m_dicSubUI = new Dictionary<int, NKCUIContractBanner>();

		// Token: 0x04007AF4 RID: 31476
		private int m_SelectedContractID;

		// Token: 0x04007AF5 RID: 31477
		private bool m_bUpdateTime;

		// Token: 0x04007AF6 RID: 31478
		private DateTime m_CurDateEndTime;

		// Token: 0x04007AF7 RID: 31479
		private bool m_bUpdateLimitCount;

		// Token: 0x04007AF8 RID: 31480
		private int m_iCurRemainFreeContractCount;

		// Token: 0x04007AF9 RID: 31481
		private DateTime m_LastUIOpenTimeUTC;

		// Token: 0x04007AFA RID: 31482
		private DateTime m_ShopPackageNextResetTime;

		// Token: 0x04007AFB RID: 31483
		private ContractCostType m_costTypeLeft;

		// Token: 0x04007AFC RID: 31484
		private ContractCostType m_costTypeRight;

		// Token: 0x04007AFD RID: 31485
		private bool m_bLimitContractUpateTime;

		// Token: 0x04007AFE RID: 31486
		private DateTime m_CurLimitContractEndTimeUTC;

		// Token: 0x04007AFF RID: 31487
		private string m_strReservedOpenID;

		// Token: 0x04007B00 RID: 31488
		private int m_iCurContractLimitCount = -1;

		// Token: 0x04007B01 RID: 31489
		private int m_iCurMultiContractTryCnt;

		// Token: 0x04007B02 RID: 31490
		private float m_fDeltaTime;
	}
}
