using System;
using System.Collections.Generic;
using System.Linq;
using NKC.UI.Collection;
using NKC.UI.NPC;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009A1 RID: 2465
	public class NKCUIHangarBuild : NKCUIBase
	{
		// Token: 0x170011DF RID: 4575
		// (get) Token: 0x060066AA RID: 26282 RVA: 0x0020D890 File Offset: 0x0020BA90
		public static NKCUIHangarBuild Instance
		{
			get
			{
				if (NKCUIHangarBuild.m_Instance == null)
				{
					NKCUIHangarBuild.m_Instance = NKCUIManager.OpenNewInstance<NKCUIHangarBuild>("ab_ui_nkm_ui_hangar", "NKM_UI_HANGAR_BUILD", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIHangarBuild.CleanupInstance)).GetInstance<NKCUIHangarBuild>();
					NKCUIHangarBuild.m_Instance.Init();
				}
				return NKCUIHangarBuild.m_Instance;
			}
		}

		// Token: 0x060066AB RID: 26283 RVA: 0x0020D8DF File Offset: 0x0020BADF
		private static void CleanupInstance()
		{
			NKCUIHangarBuild.m_Instance = null;
		}

		// Token: 0x060066AC RID: 26284 RVA: 0x0020D8E7 File Offset: 0x0020BAE7
		public static void CheckInstanceAndClose()
		{
			if (NKCUIHangarBuild.m_Instance != null && NKCUIHangarBuild.m_Instance.IsOpen)
			{
				NKCUIHangarBuild.m_Instance.Close();
			}
		}

		// Token: 0x170011E0 RID: 4576
		// (get) Token: 0x060066AD RID: 26285 RVA: 0x0020D90C File Offset: 0x0020BB0C
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIHangarBuild.m_Instance != null && NKCUIHangarBuild.m_Instance.IsOpen;
			}
		}

		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x060066AE RID: 26286 RVA: 0x0020D927 File Offset: 0x0020BB27
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x060066AF RID: 26287 RVA: 0x0020D92A File Offset: 0x0020BB2A
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_HANGAR_BUILD;
			}
		}

		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x060066B0 RID: 26288 RVA: 0x0020D931 File Offset: 0x0020BB31
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_SHIP_MAKE";
			}
		}

		// Token: 0x060066B1 RID: 26289 RVA: 0x0020D938 File Offset: 0x0020BB38
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(this, false);
			if (this.m_UINPC_NaHeeRin == null)
			{
				this.m_UINPC_NaHeeRin = null;
			}
		}

		// Token: 0x060066B2 RID: 26290 RVA: 0x0020D956 File Offset: 0x0020BB56
		public override void OnCloseInstance()
		{
			this.ClearBuildSlot();
		}

		// Token: 0x060066B3 RID: 26291 RVA: 0x0020D95E File Offset: 0x0020BB5E
		public override void OnBackButton()
		{
			NKCUtil.SetGameobjectActive(this, false);
			base.OnBackButton();
		}

		// Token: 0x060066B4 RID: 26292 RVA: 0x0020D970 File Offset: 0x0020BB70
		public void Init()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (this.m_NKM_UI_HANGAR_BUILD_TOGGLE != null)
			{
				this.m_NKM_UI_HANGAR_BUILD_TOGGLE.OnValueChanged.RemoveAllListeners();
				this.m_NKM_UI_HANGAR_BUILD_TOGGLE.OnValueChanged.AddListener(new UnityAction<bool>(this.OnToggleChange));
			}
			if (this.m_UINPC_NaHeeRin == null)
			{
				this.m_UINPC_NaHeeRin = this.m_NPCTouchArea.GetComponent<NKCUINPCHangarNaHeeRin>();
				this.m_UINPC_NaHeeRin.Init(true);
			}
			else
			{
				this.m_UINPC_NaHeeRin.PlayAni(NPC_ACTION_TYPE.START, false);
			}
			if (this.m_LoopHorizontalScrollRect != null)
			{
				this.m_LoopHorizontalScrollRect.dOnGetObject += this.MakeShipBuildSlot;
				this.m_LoopHorizontalScrollRect.dOnProvideData += this.ProvideShipBuildSlot;
				this.m_LoopHorizontalScrollRect.dOnReturnObject += this.ReturnShipBuildSlot;
				this.m_LoopHorizontalScrollRect.PrepareCells(0);
				NKCUtil.SetScrollHotKey(this.m_LoopHorizontalScrollRect, null);
			}
			NKCUtil.SetBindFunction(this.m_NKM_UI_HANGAR_BUILD_SHORTCUT_MENU_BUTTON_SHIPCOLLECTION, delegate()
			{
				NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_COLLECTION_SHIP, "", false);
			});
			this.InitShipBuildData();
		}

		// Token: 0x060066B5 RID: 26293 RVA: 0x0020DA98 File Offset: 0x0020BC98
		private RectTransform MakeShipBuildSlot(int index)
		{
			if (this.m_stkShipBuildSlotPool.Count > 0)
			{
				RectTransform rectTransform = this.m_stkShipBuildSlotPool.Pop();
				NKCUtil.SetGameobjectActive(rectTransform, true);
				return rectTransform;
			}
			NKCUIHangarBuildSlot shipBuildSlot = this.GetShipBuildSlot();
			if (shipBuildSlot != null)
			{
				shipBuildSlot.transform.localPosition = Vector3.zero;
				shipBuildSlot.transform.localScale = Vector3.one;
				return shipBuildSlot.GetComponent<RectTransform>();
			}
			return null;
		}

		// Token: 0x060066B6 RID: 26294 RVA: 0x0020DB00 File Offset: 0x0020BD00
		private NKCUIHangarBuildSlot GetShipBuildSlot()
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("ab_ui_nkm_ui_hangar", "NKM_UI_HANGAR_BUILD_SLOT_LIST", false, null);
			if (nkcassetInstanceData.m_Instant != null)
			{
				NKCUIHangarBuildSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIHangarBuildSlot>();
				component.InitUI(new UnityAction(this.OpenConfirmPopup), new UnityAction(this.CloseConfirmPopup), new NKCUIHangarBuildSlot.OnShipInfo(this.OpenShipInfo));
				this.m_lstHangarSlotInstance.Add(nkcassetInstanceData);
				return component;
			}
			Debug.LogError("missing prefab path ab_ui_nkm_ui_hangar, NKM_UI_HANGAR_BUILD_SLOT_LIST");
			return null;
		}

		// Token: 0x060066B7 RID: 26295 RVA: 0x0020DB7C File Offset: 0x0020BD7C
		private void ProvideShipBuildSlot(Transform tr, int idx)
		{
			NKCUIHangarBuildSlot component = tr.GetComponent<NKCUIHangarBuildSlot>();
			if (component == null)
			{
				return;
			}
			component.SetData(this.m_lstShipBuild[idx], idx < this.m_iNewBuildCnt);
		}

		// Token: 0x060066B8 RID: 26296 RVA: 0x0020DBB5 File Offset: 0x0020BDB5
		private void ReturnShipBuildSlot(Transform go)
		{
			NKCUtil.SetGameobjectActive(go, false);
			go.SetParent(this.m_rtShipBuildSlotPool);
			this.m_stkShipBuildSlotPool.Push(go.GetComponent<RectTransform>());
		}

		// Token: 0x060066B9 RID: 26297 RVA: 0x0020DBDB File Offset: 0x0020BDDB
		private void OnToggleChange(bool bChange)
		{
			this.m_bHideHasShip = bChange;
			this.UpdateUI(true);
		}

		// Token: 0x060066BA RID: 26298 RVA: 0x0020DBEC File Offset: 0x0020BDEC
		private void ClearBuildSlot()
		{
			foreach (NKCAssetInstanceData nkcassetInstanceData in this.m_lstHangarSlotInstance)
			{
				if (nkcassetInstanceData != null)
				{
					NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				}
			}
			this.m_lstHangarSlotInstance.Clear();
			foreach (KeyValuePair<int, NKCUIHangarBuildSlot> keyValuePair in this.m_dicBuildSlots)
			{
				if (keyValuePair.Value.gameObject != null)
				{
					UnityEngine.Object.Destroy(keyValuePair.Value.gameObject);
				}
			}
			this.m_dicBuildSlots.Clear();
		}

		// Token: 0x060066BB RID: 26299 RVA: 0x0020DCB8 File Offset: 0x0020BEB8
		public void Open()
		{
			NKCUtil.SetGameobjectActive(this, true);
			if (this.m_NKM_UI_HANGAR_BUILD_SLOT_LIST_Content != null)
			{
				this.m_NKM_UI_HANGAR_BUILD_SLOT_LIST_Content.anchoredPosition = Vector2.zero;
			}
			this.m_CurShipTabType = NKMShipBuildTemplet.ShipUITabType.SHIP_NORMAL;
			NKCUIComToggle ctglNormalTab = this.m_ctglNormalTab;
			if (ctglNormalTab != null)
			{
				ctglNormalTab.Select(true, true, false);
			}
			this.UpdateUI(true);
			base.UIOpened(true);
			this.CheckTutorial();
		}

		// Token: 0x060066BC RID: 26300 RVA: 0x0020DD1C File Offset: 0x0020BF1C
		private void InitShipBuildData()
		{
			this.m_dicBuildTemplet.Clear();
			foreach (NKMShipBuildTemplet nkmshipBuildTemplet in NKMTempletContainer<NKMShipBuildTemplet>.Values)
			{
				if (nkmshipBuildTemplet.ShipBuildUnlockType != NKMShipBuildTemplet.BuildUnlockType.BUT_UNABLE)
				{
					this.m_dicBuildTemplet.Add(nkmshipBuildTemplet.ShipID, nkmshipBuildTemplet);
				}
			}
			if (null != this.m_ctglNormalTab)
			{
				this.m_ctglNormalTab.OnValueChanged.RemoveAllListeners();
				this.m_ctglNormalTab.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickNormalTab));
			}
			if (null != this.m_ctglEventTab)
			{
				this.m_ctglEventTab.OnValueChanged.RemoveAllListeners();
				this.m_ctglEventTab.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickEventTab));
			}
			NKCUtil.SetGameobjectActive(this.m_ctglEventTab.gameObject, NKMOpenTagManager.IsOpened(this.m_eventTabOpenTag));
		}

		// Token: 0x060066BD RID: 26301 RVA: 0x0020DE18 File Offset: 0x0020C018
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			if (itemData != null)
			{
				this.UpdateUI(true);
			}
		}

		// Token: 0x060066BE RID: 26302 RVA: 0x0020DE24 File Offset: 0x0020C024
		private void SrotingBuildList()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			this.m_iNewBuildCnt = 0;
			List<NKMShipBuildTemplet> list = new List<NKMShipBuildTemplet>();
			List<NKMShipBuildTemplet> list2 = new List<NKMShipBuildTemplet>();
			List<NKMShipBuildTemplet> list3 = new List<NKMShipBuildTemplet>();
			List<NKMShipBuildTemplet> list4 = new List<NKMShipBuildTemplet>();
			List<NKMShipBuildTemplet> list5 = new List<NKMShipBuildTemplet>();
			List<NKMShipBuildTemplet> list6 = new List<NKMShipBuildTemplet>();
			foreach (KeyValuePair<int, NKMShipBuildTemplet> keyValuePair in this.m_dicBuildTemplet)
			{
				if (this.m_CurShipTabType == keyValuePair.Value.ShipType)
				{
					bool flag = false;
					foreach (KeyValuePair<long, NKMUnitData> keyValuePair2 in NKCScenManager.CurrentUserData().m_ArmyData.m_dicMyShip)
					{
						if (NKMShipManager.IsSameKindShip(keyValuePair2.Value.m_UnitID, keyValuePair.Key))
						{
							flag = true;
							break;
						}
					}
					if (NKMShipManager.CanUnlockShip(nkmuserData, keyValuePair.Value))
					{
						if (!flag && this.IsFirstCheck(nkmuserData.m_UserUID, keyValuePair.Key))
						{
							list.Add(keyValuePair.Value);
						}
						else
						{
							List<BuildMaterial> buildMaterialList = keyValuePair.Value.BuildMaterialList;
							bool flag2 = true;
							for (int i = 0; i < buildMaterialList.Count; i++)
							{
								if (nkmuserData.m_InventoryData.GetCountMiscItem(buildMaterialList[i].m_ShipBuildMaterialID) < (long)buildMaterialList[i].m_ShipBuildMaterialCount)
								{
									flag2 = false;
									break;
								}
							}
							if (flag2)
							{
								if (this.IsHasShip(keyValuePair.Key))
								{
									if (!this.m_bHideHasShip)
									{
										list3.Add(keyValuePair.Value);
									}
								}
								else
								{
									list2.Add(keyValuePair.Value);
								}
							}
							else if (this.IsHasShip(keyValuePair.Key))
							{
								if (!this.m_bHideHasShip)
								{
									list5.Add(keyValuePair.Value);
								}
							}
							else
							{
								list4.Add(keyValuePair.Value);
							}
						}
					}
					else
					{
						list6.Add(keyValuePair.Value);
					}
				}
			}
			this.m_lstShipBuild.Clear();
			this.SortingBuildListDetail(list, 0);
			this.SortingBuildListDetail(list2, 0);
			this.SortingBuildListDetail(list3, 0);
			this.SortingBuildListDetail(list4, 0);
			this.SortingBuildListDetail(list5, 0);
			this.SortingBuildListDetail(list6, 0);
			this.m_iNewBuildCnt = list.Count;
			if (this.m_dicBuildTemplet.Count > 0)
			{
				List<KeyValuePair<int, NKMShipBuildTemplet>> list7 = this.m_dicBuildTemplet.ToList<KeyValuePair<int, NKMShipBuildTemplet>>().FindAll((KeyValuePair<int, NKMShipBuildTemplet> e) => e.Value.ShipType == this.m_CurShipTabType);
				NKCUtil.SetGameobjectActive(this.m_objNoneText, list7.Count <= 0);
			}
		}

		// Token: 0x060066BF RID: 26303 RVA: 0x0020E0E8 File Offset: 0x0020C2E8
		private void SortingBuildListDetail(List<NKMShipBuildTemplet> targetList, int type = 0)
		{
			if (type == 0)
			{
				if (targetList.Count < 1)
				{
					return;
				}
				Dictionary<NKM_UNIT_GRADE, List<NKMShipBuildTemplet>> dictionary = new Dictionary<NKM_UNIT_GRADE, List<NKMShipBuildTemplet>>();
				foreach (NKMShipBuildTemplet nkmshipBuildTemplet in targetList)
				{
					if (nkmshipBuildTemplet != null)
					{
						NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(nkmshipBuildTemplet.ShipID);
						if (unitTempletBase != null && unitTempletBase.CollectionEnableByTag)
						{
							if (!dictionary.ContainsKey(unitTempletBase.m_NKM_UNIT_GRADE))
							{
								dictionary.Add(unitTempletBase.m_NKM_UNIT_GRADE, new List<NKMShipBuildTemplet>
								{
									nkmshipBuildTemplet
								});
							}
							else
							{
								dictionary[unitTempletBase.m_NKM_UNIT_GRADE].Add(nkmshipBuildTemplet);
							}
						}
					}
				}
				List<NKM_UNIT_GRADE> list = dictionary.Keys.ToList<NKM_UNIT_GRADE>();
				list.Sort((NKM_UNIT_GRADE x, NKM_UNIT_GRADE y) => y.CompareTo(x));
				using (List<NKM_UNIT_GRADE>.Enumerator enumerator2 = list.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						NKM_UNIT_GRADE key = enumerator2.Current;
						if (dictionary.ContainsKey(key))
						{
							if (dictionary[key].Count > 1)
							{
								this.SortingBuildListDetail(dictionary[key], 1);
							}
							else
							{
								this.AddShipBuildList(dictionary[key]);
							}
						}
					}
					return;
				}
			}
			if (type == 1)
			{
				if (targetList.Count < 1)
				{
					return;
				}
				Dictionary<NKM_UNIT_STYLE_TYPE, List<NKMShipBuildTemplet>> dictionary2 = new Dictionary<NKM_UNIT_STYLE_TYPE, List<NKMShipBuildTemplet>>();
				foreach (NKMShipBuildTemplet nkmshipBuildTemplet2 in targetList)
				{
					if (nkmshipBuildTemplet2 != null)
					{
						NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(nkmshipBuildTemplet2.ShipID);
						if (unitTempletBase2 != null && unitTempletBase2.CollectionEnableByTag)
						{
							if (!dictionary2.ContainsKey(unitTempletBase2.m_NKM_UNIT_STYLE_TYPE))
							{
								dictionary2.Add(unitTempletBase2.m_NKM_UNIT_STYLE_TYPE, new List<NKMShipBuildTemplet>
								{
									nkmshipBuildTemplet2
								});
							}
							else
							{
								dictionary2[unitTempletBase2.m_NKM_UNIT_STYLE_TYPE].Add(nkmshipBuildTemplet2);
							}
						}
					}
				}
				List<NKM_UNIT_STYLE_TYPE> list2 = dictionary2.Keys.ToList<NKM_UNIT_STYLE_TYPE>();
				list2.Sort((NKM_UNIT_STYLE_TYPE x, NKM_UNIT_STYLE_TYPE y) => x.CompareTo(y));
				foreach (NKM_UNIT_STYLE_TYPE key2 in list2)
				{
					if (dictionary2.ContainsKey(key2))
					{
						if (dictionary2[key2].Count > 1)
						{
							dictionary2[key2].Sort((NKMShipBuildTemplet x, NKMShipBuildTemplet y) => x.ShipID.CompareTo(y.ShipID));
						}
						this.AddShipBuildList(dictionary2[key2]);
					}
				}
			}
		}

		// Token: 0x060066C0 RID: 26304 RVA: 0x0020E3AC File Offset: 0x0020C5AC
		private void AddShipBuildList(List<NKMShipBuildTemplet> addList)
		{
			if (addList.Count > 0)
			{
				this.m_lstShipBuild.AddRange(addList);
			}
		}

		// Token: 0x060066C1 RID: 26305 RVA: 0x0020E3C4 File Offset: 0x0020C5C4
		private bool IsHasShip(int shipID)
		{
			bool result = false;
			foreach (KeyValuePair<long, NKMUnitData> keyValuePair in NKCScenManager.CurrentUserData().m_ArmyData.m_dicMyShip)
			{
				if (NKMShipManager.IsSameKindShip(keyValuePair.Value.m_UnitID, shipID))
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x060066C2 RID: 26306 RVA: 0x0020E434 File Offset: 0x0020C634
		private bool IsFirstCheck(long userUID, int shipID)
		{
			string key = string.Format("{0}_{1}_{2}", "SHIP_BUILD_SLOT_CHECK", userUID, shipID);
			if (!PlayerPrefs.HasKey(key))
			{
				PlayerPrefs.SetInt(key, 0);
				return true;
			}
			return false;
		}

		// Token: 0x060066C3 RID: 26307 RVA: 0x0020E46F File Offset: 0x0020C66F
		public void UpdateUI(bool bSlotUpdate = false)
		{
			this.SrotingBuildList();
			if (bSlotUpdate)
			{
				this.m_LoopHorizontalScrollRect.TotalCount = this.m_lstShipBuild.Count;
				this.m_LoopHorizontalScrollRect.velocity = Vector2.zero;
				this.m_LoopHorizontalScrollRect.SetIndexPosition(0);
			}
		}

		// Token: 0x060066C4 RID: 26308 RVA: 0x0020E4AC File Offset: 0x0020C6AC
		public void OpenShipInfo(int shipID)
		{
			int num = 0;
			bool flag = true;
			List<NKMUnitData> list = new List<NKMUnitData>();
			list.Clear();
			foreach (KeyValuePair<int, NKCUIHangarBuildSlot> keyValuePair in this.m_dicBuildSlots)
			{
				if (this.m_bHideHasShip)
				{
					bool flag2 = false;
					Dictionary<long, NKMUnitData> dicMyShip = NKCScenManager.CurrentUserData().m_ArmyData.m_dicMyShip;
					if (dicMyShip != null)
					{
						foreach (KeyValuePair<long, NKMUnitData> keyValuePair2 in dicMyShip)
						{
							if (keyValuePair2.Value.m_UnitID == keyValuePair.Key)
							{
								flag2 = true;
								break;
							}
						}
						if (flag2)
						{
							continue;
						}
					}
				}
				if (flag)
				{
					if (keyValuePair.Key == shipID)
					{
						flag = false;
					}
					else
					{
						num++;
					}
				}
				list.Add(new NKMUnitData(keyValuePair.Key, 0L, false, false, false, false));
			}
			NKMUnitData shipData = NKCUtil.MakeDummyUnit(NKMShipManager.GetMaxLevelShipID(shipID), true);
			NKCUICollectionShipInfo.Instance.Open(shipData, NKMDeckIndex.None, null, null, false);
		}

		// Token: 0x060066C5 RID: 26309 RVA: 0x0020E5D4 File Offset: 0x0020C7D4
		public void OpenConfirmPopup()
		{
			NKCUtil.SetGameobjectActive(this.m_AB_NPC_NA_HEE_RIN, false);
		}

		// Token: 0x060066C6 RID: 26310 RVA: 0x0020E5E2 File Offset: 0x0020C7E2
		public void CloseConfirmPopup()
		{
			NKCUtil.SetGameobjectActive(this.m_AB_NPC_NA_HEE_RIN, true);
		}

		// Token: 0x060066C7 RID: 26311 RVA: 0x0020E5F0 File Offset: 0x0020C7F0
		private void OnClickNormalTab(bool bVal)
		{
			if (this.m_CurShipTabType == NKMShipBuildTemplet.ShipUITabType.SHIP_NORMAL)
			{
				return;
			}
			this.m_CurShipTabType = NKMShipBuildTemplet.ShipUITabType.SHIP_NORMAL;
			this.UpdateUI(true);
		}

		// Token: 0x060066C8 RID: 26312 RVA: 0x0020E609 File Offset: 0x0020C809
		private void OnClickEventTab(bool bVal)
		{
			if (this.m_CurShipTabType == NKMShipBuildTemplet.ShipUITabType.SHIP_EVENT)
			{
				return;
			}
			this.m_CurShipTabType = NKMShipBuildTemplet.ShipUITabType.SHIP_EVENT;
			this.UpdateUI(true);
		}

		// Token: 0x060066C9 RID: 26313 RVA: 0x0020E623 File Offset: 0x0020C823
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Q))
			{
				this.OnClickNormalTab(true);
				return;
			}
			if (Input.GetKeyDown(KeyCode.W))
			{
				this.OnClickEventTab(true);
			}
		}

		// Token: 0x060066CA RID: 26314 RVA: 0x0020E646 File Offset: 0x0020C846
		public void CheckTutorial()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.HangerBuild, true);
		}

		// Token: 0x060066CB RID: 26315 RVA: 0x0020E654 File Offset: 0x0020C854
		public NKCUIHangarBuildSlot GetSlot(int shipID)
		{
			int num = this.m_lstShipBuild.FindIndex((NKMShipBuildTemplet v) => v.ShipID == shipID);
			if (num >= 0)
			{
				this.m_LoopHorizontalScrollRect.SetIndexPosition(num);
				NKCUIHangarBuildSlot[] componentsInChildren = this.m_LoopHorizontalScrollRect.content.GetComponentsInChildren<NKCUIHangarBuildSlot>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					if (componentsInChildren[i].ShipID == shipID)
					{
						return componentsInChildren[i];
					}
				}
			}
			return null;
		}

		// Token: 0x0400527A RID: 21114
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_hangar";

		// Token: 0x0400527B RID: 21115
		private const string UI_ASSET_NAME = "NKM_UI_HANGAR_BUILD";

		// Token: 0x0400527C RID: 21116
		private static NKCUIHangarBuild m_Instance;

		// Token: 0x0400527D RID: 21117
		[Header("숏컷")]
		public NKCUIComStateButton m_NKM_UI_HANGAR_BUILD_SHORTCUT_MENU_BUTTON_SHIPCOLLECTION;

		// Token: 0x0400527E RID: 21118
		[Header("토글")]
		public NKCUIComToggle m_NKM_UI_HANGAR_BUILD_TOGGLE;

		// Token: 0x0400527F RID: 21119
		[Header("리스트")]
		public LoopHorizontalScrollRect m_LoopHorizontalScrollRect;

		// Token: 0x04005280 RID: 21120
		public RectTransform m_NKM_UI_HANGAR_BUILD_SLOT_LIST_Viewport;

		// Token: 0x04005281 RID: 21121
		public RectTransform m_NKM_UI_HANGAR_BUILD_SLOT_LIST_Content;

		// Token: 0x04005282 RID: 21122
		public RectTransform m_rtShipBuildSlotPool;

		// Token: 0x04005283 RID: 21123
		private Stack<RectTransform> m_stkShipBuildSlotPool = new Stack<RectTransform>();

		// Token: 0x04005284 RID: 21124
		[Header("프리팹")]
		public NKCUIHangarBuildSlot m_pfNKM_UI_HANGAR_BUILD_SLOT_LIST;

		// Token: 0x04005285 RID: 21125
		[Header("NPC")]
		public GameObject m_AB_NPC_NA_HEE_RIN;

		// Token: 0x04005286 RID: 21126
		public GameObject m_NPCTouchArea;

		// Token: 0x04005287 RID: 21127
		private NKCUINPCHangarNaHeeRin m_UINPC_NaHeeRin;

		// Token: 0x04005288 RID: 21128
		private List<NKCAssetInstanceData> m_lstHangarSlotInstance = new List<NKCAssetInstanceData>();

		// Token: 0x04005289 RID: 21129
		private bool m_bHideHasShip;

		// Token: 0x0400528A RID: 21130
		private Dictionary<int, NKMShipBuildTemplet> m_dicBuildTemplet = new Dictionary<int, NKMShipBuildTemplet>();

		// Token: 0x0400528B RID: 21131
		private Dictionary<int, NKCUIHangarBuildSlot> m_dicBuildSlots = new Dictionary<int, NKCUIHangarBuildSlot>();

		// Token: 0x0400528C RID: 21132
		[Header("이벤트 버튼 오픈태그")]
		public string m_eventTabOpenTag = "TAG_HANGAR_BUILD_TYPE_EVENT";

		// Token: 0x0400528D RID: 21133
		private List<NKMShipBuildTemplet> m_lstShipBuild = new List<NKMShipBuildTemplet>();

		// Token: 0x0400528E RID: 21134
		private int m_iNewBuildCnt;

		// Token: 0x0400528F RID: 21135
		public const string ShipBuildListChecked = "SHIP_BUILD_SLOT_CHECK";

		// Token: 0x04005290 RID: 21136
		private NKMShipBuildTemplet.ShipUITabType m_CurShipTabType;

		// Token: 0x04005291 RID: 21137
		public GameObject m_objNoneText;

		// Token: 0x04005292 RID: 21138
		public NKCUIComToggle m_ctglNormalTab;

		// Token: 0x04005293 RID: 21139
		public NKCUIComToggle m_ctglEventTab;
	}
}
