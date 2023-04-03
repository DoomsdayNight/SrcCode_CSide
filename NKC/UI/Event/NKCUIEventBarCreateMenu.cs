using System;
using System.Collections.Generic;
using System.Linq;
using NKM;
using NKM.Event;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BD2 RID: 3026
	public class NKCUIEventBarCreateMenu : MonoBehaviour, IScrollHandler, IEventSystemHandler
	{
		// Token: 0x17001674 RID: 5748
		// (get) Token: 0x06008C3C RID: 35900 RVA: 0x002FB1CA File Offset: 0x002F93CA
		public NKCUIEventBarCreateMenu.Step CurrentStep
		{
			get
			{
				return this.m_step;
			}
		}

		// Token: 0x06008C3D RID: 35901 RVA: 0x002FB1D4 File Offset: 0x002F93D4
		public void Init()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnNextStep, new UnityAction(this.OnClickNextStep));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnShake, new UnityAction(this.OnClickShake));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnStir, new UnityAction(this.OnClickStir));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnUp, new UnityAction(this.OnClickUp));
			this.m_csbtnUp.dOnPointerHoldPress = new NKCUIComStateButtonBase.OnPointerHoldPress(this.OnClickUp);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnDown, new UnityAction(this.OnClickDown));
			this.m_csbtnDown.dOnPointerHoldPress = new NKCUIComStateButtonBase.OnPointerHoldPress(this.OnClickDown);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnMax, new UnityAction(this.OnClickMax));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnOK, new UnityAction(this.OnClickOK));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnCancel, new UnityAction(this.OnClickCancel));
			if (this.m_ingradientSlotArray != null)
			{
				int num = this.m_ingradientSlotArray.Length;
				for (int i = 0; i < num; i++)
				{
					this.m_ingradientSlotArray[i].Init();
				}
			}
			NKCUtil.SetHotkey(this.m_csbtnUp, HotkeyEventType.Plus, null, false);
			NKCUtil.SetHotkey(this.m_csbtnDown, HotkeyEventType.Minus, null, false);
			NKCUtil.SetHotkey(this.m_csbtnMax, HotkeyEventType.Down, null, false);
			NKCUtil.SetHotkey(this.m_csbtnNextStep, HotkeyEventType.Right, null, false);
			NKCUtil.SetHotkey(this.m_csbtnOK, HotkeyEventType.Confirm, null, false);
			NKCUtil.SetHotkey(this.m_csbtnCancel, HotkeyEventType.Left, null, false);
		}

		// Token: 0x06008C3E RID: 35902 RVA: 0x002FB348 File Offset: 0x002F9548
		public void Open(int eventID, int bartenderID, NKCUIEventBarCreateMenu.OnSelectMenu onSelectMenu, NKCUIEventBarCreateMenu.OnChangeUnitAnimation onChangeUnitAnimation, NKCUIEventBarCreateMenu.OnCreate onCreate = null, NKCUIEventBarCreateMenu.OnCreateRefuse onCreateRefuse = null)
		{
			this.m_dOnSelectMenu = onSelectMenu;
			this.m_dOnChangeUnitAnimation = onChangeUnitAnimation;
			this.m_dOnCreate = onCreate;
			this.m_dOnCreateRefuse = onCreateRefuse;
			this.m_iEventID = eventID;
			this.m_creatingCocktail = 0;
			this.m_creatingCount = 0;
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(bartenderID);
			if (unitTempletBase != null)
			{
				NKCUtil.SetLabelText(this.m_lbUnitName, unitTempletBase.GetUnitName());
			}
			this.SetStep1State(eventID);
		}

		// Token: 0x06008C3F RID: 35903 RVA: 0x002FB3AC File Offset: 0x002F95AC
		public void Refresh()
		{
			NKCUIEventBarCreateMenu.Step step = this.m_step;
			if (step == NKCUIEventBarCreateMenu.Step.Step1)
			{
				this.m_creatingCocktail = 0;
				this.m_creatingCount = 0;
				this.SetStep1State(this.m_iEventID);
				return;
			}
			if (step != NKCUIEventBarCreateMenu.Step.Step2)
			{
				return;
			}
			this.SetStep2State();
		}

		// Token: 0x06008C40 RID: 35904 RVA: 0x002FB3E9 File Offset: 0x002F95E9
		public void OnScroll(PointerEventData eventData)
		{
			if (this.m_step != NKCUIEventBarCreateMenu.Step.Step2)
			{
				return;
			}
			if (eventData.scrollDelta.y < 0f)
			{
				this.OnClickDown();
				return;
			}
			if (eventData.scrollDelta.y > 0f)
			{
				this.OnClickUp();
			}
		}

		// Token: 0x06008C41 RID: 35905 RVA: 0x002FB426 File Offset: 0x002F9626
		public void Close()
		{
			List<int> ingradientList = this.m_ingradientList;
			if (ingradientList != null)
			{
				ingradientList.Clear();
			}
			List<int> selectedIngradient = this.m_selectedIngradient;
			if (selectedIngradient != null)
			{
				selectedIngradient.Clear();
			}
			this.m_dOnSelectMenu = null;
			this.m_dOnChangeUnitAnimation = null;
			this.m_dOnCreate = null;
		}

		// Token: 0x06008C42 RID: 35906 RVA: 0x002FB460 File Offset: 0x002F9660
		private void SetStep1State(int eventID)
		{
			if (this.m_ingradientList.Count <= 0)
			{
				foreach (NKMEventBarTemplet nkmeventBarTemplet in NKMEventBarTemplet.Values)
				{
					if (nkmeventBarTemplet.EventID == eventID)
					{
						if (!this.m_ingradientList.Contains(nkmeventBarTemplet.MaterialItemId01))
						{
							this.m_ingradientList.Add(nkmeventBarTemplet.MaterialItemId01);
						}
						if (!this.m_ingradientList.Contains(nkmeventBarTemplet.MaterialItemId02))
						{
							this.m_ingradientList.Add(nkmeventBarTemplet.MaterialItemId02);
						}
					}
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objStepTechnique, true);
			NKCUtil.SetGameobjectActive(this.m_objStepAmount, false);
			if (this.m_ingradientSlotArray != null)
			{
				int num = this.m_ingradientSlotArray.Length;
				for (int i = 0; i < num; i++)
				{
					if (this.m_ingradientList.Count <= i)
					{
						NKCUtil.SetGameobjectActive(this.m_ingradientSlotArray[i], false);
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_ingradientSlotArray[i], true);
						this.m_ingradientSlotArray[i].SetData(this.m_ingradientList[i], new NKCUIEventBarIngradientSlot.OnSelectIngradient(this.OnSelectIngredient));
					}
				}
			}
			this.m_selectedIngradient.Clear();
			NKCUIComStateButton csbtnNextStep = this.m_csbtnNextStep;
			if (csbtnNextStep != null)
			{
				csbtnNextStep.SetLock(true, false);
			}
			NKCUIComStateButton csbtnShake = this.m_csbtnShake;
			if (csbtnShake != null)
			{
				csbtnShake.Select(false, false, false);
			}
			NKCUIComStateButton csbtnStir = this.m_csbtnStir;
			if (csbtnStir != null)
			{
				csbtnStir.Select(false, false, false);
			}
			this.m_selectedTechnique = ManufacturingTechnique.none;
			this.m_step = NKCUIEventBarCreateMenu.Step.Step1;
			NKCUtil.SetLabelText(this.m_lbIngradientCount, string.Format(NKCUtilString.GET_STRING_GREMORY_MATERIAL_COUNT, this.m_selectedIngradient.Count, 2));
		}

		// Token: 0x06008C43 RID: 35907 RVA: 0x002FB610 File Offset: 0x002F9810
		private void SetStep2State()
		{
			NKCUtil.SetGameobjectActive(this.m_objStepTechnique, false);
			NKCUtil.SetGameobjectActive(this.m_objStepAmount, true);
			this.SetCocktailMakingScript();
			this.m_creatingCount = 1;
			NKMEventBarTemplet nkmeventBarTemplet = NKMEventBarTemplet.Find(this.m_creatingCocktail);
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmeventBarTemplet != null && nkmuserData != null)
			{
				long countMiscItem = nkmuserData.m_InventoryData.GetCountMiscItem(nkmeventBarTemplet.MaterialItemId01);
				NKCUIItemCostSlot ingradientSlot = this.m_ingradientSlot1;
				if (ingradientSlot != null)
				{
					ingradientSlot.SetData(nkmeventBarTemplet.MaterialItemId01, nkmeventBarTemplet.MaterialItemValue01 * this.m_creatingCount, countMiscItem, true, true, false);
				}
				countMiscItem = nkmuserData.m_InventoryData.GetCountMiscItem(nkmeventBarTemplet.MaterialItemId02);
				NKCUIItemCostSlot ingradientSlot2 = this.m_ingradientSlot2;
				if (ingradientSlot2 != null)
				{
					ingradientSlot2.SetData(nkmeventBarTemplet.MaterialItemId02, nkmeventBarTemplet.MaterialItemValue02 * this.m_creatingCount, countMiscItem, true, true, false);
				}
			}
			this.ChangeStep2State(this.m_creatingCount);
			this.m_step = NKCUIEventBarCreateMenu.Step.Step2;
		}

		// Token: 0x06008C44 RID: 35908 RVA: 0x002FB6E4 File Offset: 0x002F98E4
		private void SetCocktailMakingScript()
		{
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(this.m_creatingCocktail);
			if (itemMiscTempletByID != null)
			{
				ManufacturingTechnique selectedTechnique = this.m_selectedTechnique;
				if (selectedTechnique == ManufacturingTechnique.stir)
				{
					NKCUtil.SetLabelText(this.m_lbScript, string.Format(NKCUtilString.GET_STRING_GREMORY_BARTENDER_STIR, itemMiscTempletByID.GetItemName()));
					return;
				}
				if (selectedTechnique == ManufacturingTechnique.shake)
				{
					NKCUtil.SetLabelText(this.m_lbScript, string.Format(NKCUtilString.GET_STRING_GREMORY_BARTENDER_SHAKE, itemMiscTempletByID.GetItemName()));
					return;
				}
				NKCUtil.SetLabelText(this.m_lbScript, " - ");
			}
		}

		// Token: 0x06008C45 RID: 35909 RVA: 0x002FB758 File Offset: 0x002F9958
		private int GetCreatedCocktailID()
		{
			if (this.m_selectedIngradient.Count != 2 || this.m_selectedTechnique == ManufacturingTechnique.none)
			{
				return 0;
			}
			int result = 0;
			NKMEventBarTemplet nkmeventBarTemplet = NKMEventBarTemplet.Values.FirstOrDefault((NKMEventBarTemplet e) => e.EventID == this.m_iEventID && this.m_selectedIngradient.Contains(e.MaterialItemId01) && this.m_selectedIngradient.Contains(e.MaterialItemId02) && this.m_selectedTechnique == e.Technique);
			if (nkmeventBarTemplet != null)
			{
				result = nkmeventBarTemplet.RewardItemId;
			}
			return result;
		}

		// Token: 0x06008C46 RID: 35910 RVA: 0x002FB7A4 File Offset: 0x002F99A4
		private void ChangeStep1State()
		{
			this.m_creatingCocktail = this.GetCreatedCocktailID();
			NKCUIComStateButton csbtnNextStep = this.m_csbtnNextStep;
			if (csbtnNextStep != null)
			{
				csbtnNextStep.SetLock(this.m_creatingCocktail == 0, false);
			}
			if (this.m_dOnSelectMenu != null)
			{
				this.m_dOnSelectMenu(this.m_creatingCocktail);
			}
		}

		// Token: 0x06008C47 RID: 35911 RVA: 0x002FB7F4 File Offset: 0x002F99F4
		private void ChangeIngradientSlotCount()
		{
			NKMEventBarTemplet nkmeventBarTemplet = NKMEventBarTemplet.Find(this.m_creatingCocktail);
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmeventBarTemplet != null && nkmuserData != null)
			{
				long countMiscItem = nkmuserData.m_InventoryData.GetCountMiscItem(nkmeventBarTemplet.MaterialItemId01);
				NKCUIItemCostSlot ingradientSlot = this.m_ingradientSlot1;
				if (ingradientSlot != null)
				{
					ingradientSlot.SetCount(nkmeventBarTemplet.MaterialItemValue01 * this.m_creatingCount, countMiscItem);
				}
				countMiscItem = nkmuserData.m_InventoryData.GetCountMiscItem(nkmeventBarTemplet.MaterialItemId02);
				NKCUIItemCostSlot ingradientSlot2 = this.m_ingradientSlot2;
				if (ingradientSlot2 == null)
				{
					return;
				}
				ingradientSlot2.SetCount(nkmeventBarTemplet.MaterialItemValue02 * this.m_creatingCount, countMiscItem);
			}
		}

		// Token: 0x06008C48 RID: 35912 RVA: 0x002FB87C File Offset: 0x002F9A7C
		private void ChangeStep2State(int destCount)
		{
			if (this.CanCreateDestCount(destCount))
			{
				NKCUtil.SetLabelTextColor(this.m_lbCreateCount, Color.white);
			}
			else
			{
				NKCUtil.SetLabelTextColor(this.m_lbCreateCount, Color.red);
			}
			this.m_creatingCount = destCount;
			NKCUtil.SetLabelText(this.m_lbCreateCount, this.m_creatingCount.ToString("D3"));
		}

		// Token: 0x06008C49 RID: 35913 RVA: 0x002FB8D8 File Offset: 0x002F9AD8
		private bool CanCreateDestCount(int destCount)
		{
			NKMEventBarTemplet nkmeventBarTemplet = NKMEventBarTemplet.Find(this.m_creatingCocktail);
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmeventBarTemplet == null || nkmuserData == null)
			{
				return false;
			}
			long countMiscItem = nkmuserData.m_InventoryData.GetCountMiscItem(this.m_ingradientSlot1.ItemID);
			long countMiscItem2 = nkmuserData.m_InventoryData.GetCountMiscItem(this.m_ingradientSlot2.ItemID);
			return countMiscItem >= (long)(nkmeventBarTemplet.MaterialItemValue01 * destCount) && countMiscItem2 >= (long)(nkmeventBarTemplet.MaterialItemValue02 * destCount);
		}

		// Token: 0x06008C4A RID: 35914 RVA: 0x002FB948 File Offset: 0x002F9B48
		private bool OnSelectIngredient(int ingradientID, bool select)
		{
			if (select && this.m_selectedIngradient.Count >= 2)
			{
				return false;
			}
			if (select)
			{
				this.m_selectedIngradient.Add(ingradientID);
			}
			else
			{
				this.m_selectedIngradient.Remove(ingradientID);
			}
			NKCUtil.SetLabelText(this.m_lbIngradientCount, string.Format(NKCUtilString.GET_STRING_GREMORY_MATERIAL_COUNT, this.m_selectedIngradient.Count, 2));
			this.ChangeStep1State();
			return select;
		}

		// Token: 0x06008C4B RID: 35915 RVA: 0x002FB9B8 File Offset: 0x002F9BB8
		private void OnClickNextStep()
		{
			this.SetStep2State();
			if (this.m_dOnChangeUnitAnimation != null)
			{
				this.m_dOnChangeUnitAnimation(this.order);
			}
		}

		// Token: 0x06008C4C RID: 35916 RVA: 0x002FB9DC File Offset: 0x002F9BDC
		private void OnClickShake()
		{
			if (this.m_csbtnShake.m_bSelect)
			{
				return;
			}
			NKCUIComStateButton csbtnShake = this.m_csbtnShake;
			if (csbtnShake != null)
			{
				csbtnShake.Select(true, false, false);
			}
			NKCUIComStateButton csbtnStir = this.m_csbtnStir;
			if (csbtnStir != null)
			{
				csbtnStir.Select(false, false, false);
			}
			this.m_selectedTechnique = ManufacturingTechnique.shake;
			this.ChangeStep1State();
		}

		// Token: 0x06008C4D RID: 35917 RVA: 0x002FBA30 File Offset: 0x002F9C30
		private void OnClickStir()
		{
			if (this.m_csbtnStir.m_bSelect)
			{
				return;
			}
			NKCUIComStateButton csbtnShake = this.m_csbtnShake;
			if (csbtnShake != null)
			{
				csbtnShake.Select(false, false, false);
			}
			NKCUIComStateButton csbtnStir = this.m_csbtnStir;
			if (csbtnStir != null)
			{
				csbtnStir.Select(true, false, false);
			}
			this.m_selectedTechnique = ManufacturingTechnique.stir;
			this.ChangeStep1State();
		}

		// Token: 0x06008C4E RID: 35918 RVA: 0x002FBA84 File Offset: 0x002F9C84
		private void OnClickUp()
		{
			this.SetCocktailMakingScript();
			int num = Mathf.Min(this.m_creatingCount + 1, 999);
			if (this.CanCreateDestCount(num))
			{
				this.m_creatingCount = num;
				NKCUtil.SetLabelTextColor(this.m_lbCreateCount, Color.white);
				NKCUtil.SetLabelText(this.m_lbCreateCount, this.m_creatingCount.ToString("D3"));
				this.ChangeIngradientSlotCount();
			}
		}

		// Token: 0x06008C4F RID: 35919 RVA: 0x002FBAEC File Offset: 0x002F9CEC
		private void OnClickDown()
		{
			this.SetCocktailMakingScript();
			int destCount = Mathf.Max(this.m_creatingCount - 1, 1);
			this.ChangeStep2State(destCount);
			this.ChangeIngradientSlotCount();
		}

		// Token: 0x06008C50 RID: 35920 RVA: 0x002FBB1C File Offset: 0x002F9D1C
		private void OnClickMax()
		{
			this.SetCocktailMakingScript();
			NKMEventBarTemplet nkmeventBarTemplet = NKMEventBarTemplet.Find(this.m_creatingCocktail);
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmeventBarTemplet == null || nkmuserData == null)
			{
				return;
			}
			long countMiscItem = nkmuserData.m_InventoryData.GetCountMiscItem(this.m_ingradientSlot1.ItemID);
			long countMiscItem2 = nkmuserData.m_InventoryData.GetCountMiscItem(this.m_ingradientSlot2.ItemID);
			long num = countMiscItem / (long)nkmeventBarTemplet.MaterialItemValue01;
			long num2 = countMiscItem2 / (long)nkmeventBarTemplet.MaterialItemValue02;
			this.m_creatingCount = (int)Mathf.Min((float)num, (float)num2);
			this.m_creatingCount = Mathf.Min(this.m_creatingCount, 999);
			if (this.m_creatingCount >= 1)
			{
				NKCUtil.SetLabelTextColor(this.m_lbCreateCount, Color.white);
			}
			else
			{
				NKCUtil.SetLabelTextColor(this.m_lbCreateCount, Color.red);
				this.m_creatingCount = 1;
			}
			NKCUtil.SetLabelText(this.m_lbCreateCount, this.m_creatingCount.ToString("D3"));
			this.ChangeIngradientSlotCount();
		}

		// Token: 0x06008C51 RID: 35921 RVA: 0x002FBC04 File Offset: 0x002F9E04
		private void OnClickOK()
		{
			if (NKCUIEventBarResult.IsInstanceOpen)
			{
				return;
			}
			if (!this.CanCreateDestCount(this.m_creatingCount))
			{
				if (this.m_dOnChangeUnitAnimation != null)
				{
					this.m_dOnChangeUnitAnimation(this.insufficient);
				}
				if (this.m_dOnCreateRefuse != null)
				{
					this.m_dOnCreateRefuse();
				}
				return;
			}
			if (this.m_dOnChangeUnitAnimation != null)
			{
				this.m_dOnChangeUnitAnimation(this.creating);
			}
			if (this.m_dOnCreate != null)
			{
				this.m_dOnCreate();
			}
			NKCPacketSender.Send_NKMPacket_EVENT_BAR_CREATE_COCKTAIL_REQ(this.m_creatingCocktail, this.m_creatingCount);
		}

		// Token: 0x06008C52 RID: 35922 RVA: 0x002FBC91 File Offset: 0x002F9E91
		private void OnClickCancel()
		{
			NKCUtil.SetGameobjectActive(this.m_objStepTechnique, true);
			NKCUtil.SetGameobjectActive(this.m_objStepAmount, false);
			this.m_step = NKCUIEventBarCreateMenu.Step.Step1;
		}

		// Token: 0x06008C53 RID: 35923 RVA: 0x002FBCB4 File Offset: 0x002F9EB4
		private void OnDestroy()
		{
			if (this.m_ingradientList != null)
			{
				this.m_ingradientList.Clear();
				this.m_ingradientList = null;
			}
			if (this.m_selectedIngradient != null)
			{
				this.m_selectedIngradient.Clear();
				this.m_selectedIngradient = null;
			}
			this.m_dOnSelectMenu = null;
			this.m_dOnChangeUnitAnimation = null;
			this.m_dOnCreate = null;
		}

		// Token: 0x0400790A RID: 30986
		public GameObject m_objStepTechnique;

		// Token: 0x0400790B RID: 30987
		public GameObject m_objStepAmount;

		// Token: 0x0400790C RID: 30988
		[Header("Step1 Resource")]
		public NKCUIEventBarIngradientSlot[] m_ingradientSlotArray;

		// Token: 0x0400790D RID: 30989
		public Text m_lbIngradientCount;

		// Token: 0x0400790E RID: 30990
		public NKCUIComStateButton m_csbtnShake;

		// Token: 0x0400790F RID: 30991
		public NKCUIComStateButton m_csbtnStir;

		// Token: 0x04007910 RID: 30992
		public NKCUIComStateButton m_csbtnNextStep;

		// Token: 0x04007911 RID: 30993
		[Header("Step2 Resource")]
		public Text m_lbUnitName;

		// Token: 0x04007912 RID: 30994
		public Text m_lbScript;

		// Token: 0x04007913 RID: 30995
		public Text m_lbCreateCount;

		// Token: 0x04007914 RID: 30996
		public NKCUIItemCostSlot m_ingradientSlot1;

		// Token: 0x04007915 RID: 30997
		public NKCUIItemCostSlot m_ingradientSlot2;

		// Token: 0x04007916 RID: 30998
		public NKCUIComStateButton m_csbtnUp;

		// Token: 0x04007917 RID: 30999
		public NKCUIComStateButton m_csbtnDown;

		// Token: 0x04007918 RID: 31000
		public NKCUIComStateButton m_csbtnMax;

		// Token: 0x04007919 RID: 31001
		public NKCUIComStateButton m_csbtnOK;

		// Token: 0x0400791A RID: 31002
		public NKCUIComStateButton m_csbtnCancel;

		// Token: 0x0400791B RID: 31003
		[Header("애니메이션")]
		public NKCASUIUnitIllust.eAnimation order;

		// Token: 0x0400791C RID: 31004
		public NKCASUIUnitIllust.eAnimation creating;

		// Token: 0x0400791D RID: 31005
		public NKCASUIUnitIllust.eAnimation insufficient;

		// Token: 0x0400791E RID: 31006
		private List<int> m_ingradientList = new List<int>();

		// Token: 0x0400791F RID: 31007
		private List<int> m_selectedIngradient = new List<int>();

		// Token: 0x04007920 RID: 31008
		private ManufacturingTechnique m_selectedTechnique;

		// Token: 0x04007921 RID: 31009
		private NKCUIEventBarCreateMenu.Step m_step;

		// Token: 0x04007922 RID: 31010
		private int m_iEventID;

		// Token: 0x04007923 RID: 31011
		private int m_creatingCocktail;

		// Token: 0x04007924 RID: 31012
		private int m_creatingCount;

		// Token: 0x04007925 RID: 31013
		private const int MinCreatingCount = 1;

		// Token: 0x04007926 RID: 31014
		private const int MaxCreatingCount = 999;

		// Token: 0x04007927 RID: 31015
		private const int MaxIngradient = 2;

		// Token: 0x04007928 RID: 31016
		private NKCUIEventBarCreateMenu.OnSelectMenu m_dOnSelectMenu;

		// Token: 0x04007929 RID: 31017
		private NKCUIEventBarCreateMenu.OnChangeUnitAnimation m_dOnChangeUnitAnimation;

		// Token: 0x0400792A RID: 31018
		private NKCUIEventBarCreateMenu.OnCreate m_dOnCreate;

		// Token: 0x0400792B RID: 31019
		private NKCUIEventBarCreateMenu.OnCreateRefuse m_dOnCreateRefuse;

		// Token: 0x020019A8 RID: 6568
		public enum Step
		{
			// Token: 0x0400AC85 RID: 44165
			Step1,
			// Token: 0x0400AC86 RID: 44166
			Step2
		}

		// Token: 0x020019A9 RID: 6569
		// (Invoke) Token: 0x0600B99E RID: 47518
		public delegate void OnSelectMenu(int cocktailID);

		// Token: 0x020019AA RID: 6570
		// (Invoke) Token: 0x0600B9A2 RID: 47522
		public delegate void OnChangeUnitAnimation(NKCASUIUnitIllust.eAnimation animation);

		// Token: 0x020019AB RID: 6571
		// (Invoke) Token: 0x0600B9A6 RID: 47526
		public delegate void OnCreate();

		// Token: 0x020019AC RID: 6572
		// (Invoke) Token: 0x0600B9AA RID: 47530
		public delegate void OnCreateRefuse();
	}
}
