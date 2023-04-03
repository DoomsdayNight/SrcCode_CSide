using System;
using System.Collections;
using System.Collections.Generic;
using NKM;
using NKM.Contract2;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Contract
{
	// Token: 0x02000BEC RID: 3052
	public class NKCUIContractPopupRateV2 : NKCUIBase
	{
		// Token: 0x17001689 RID: 5769
		// (get) Token: 0x06008D7D RID: 36221 RVA: 0x00301AFC File Offset: 0x002FFCFC
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700168A RID: 5770
		// (get) Token: 0x06008D7E RID: 36222 RVA: 0x00301AFF File Offset: 0x002FFCFF
		public override string MenuName
		{
			get
			{
				return "세부 확률";
			}
		}

		// Token: 0x06008D7F RID: 36223 RVA: 0x00301B08 File Offset: 0x002FFD08
		public override void CloseInternal()
		{
			for (int i = 0; i < this.m_DummyPickUpSlot.Count; i++)
			{
				if (this.m_DummyPickUpSlot[i] != null)
				{
					UnityEngine.Object.Destroy(this.m_DummyPickUpSlot[i]);
					this.m_DummyPickUpSlot[i] = null;
				}
			}
			this.m_NKM_UI_POPUP_CONTRACT_RATE_SCROLL.ClearCells();
			this.m_DummyPickUpSlot.Clear();
			base.gameObject.SetActive(false);
		}

		// Token: 0x1700168B RID: 5771
		// (get) Token: 0x06008D80 RID: 36224 RVA: 0x00301B80 File Offset: 0x002FFD80
		public static NKCUIContractPopupRateV2 Instance
		{
			get
			{
				if (NKCUIContractPopupRateV2.m_Instance == null)
				{
					NKCUIContractPopupRateV2.m_Instance = NKCUIManager.OpenNewInstance<NKCUIContractPopupRateV2>("ab_ui_nkm_ui_popup_contract", "NKM_UI_POPUP_CONTRACT_RATE", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIContractPopupRateV2.CleanupInstance)).GetInstance<NKCUIContractPopupRateV2>();
					NKCUIContractPopupRateV2.m_Instance.Init();
				}
				return NKCUIContractPopupRateV2.m_Instance;
			}
		}

		// Token: 0x06008D81 RID: 36225 RVA: 0x00301BCF File Offset: 0x002FFDCF
		private static void CleanupInstance()
		{
			NKCUIContractPopupRateV2.m_Instance = null;
		}

		// Token: 0x06008D82 RID: 36226 RVA: 0x00301BD7 File Offset: 0x002FFDD7
		private void OnDestroy()
		{
			NKCUIContractPopupRateV2.m_Instance = null;
		}

		// Token: 0x06008D83 RID: 36227 RVA: 0x00301BE0 File Offset: 0x002FFDE0
		public void Init()
		{
			if (this.m_NKM_UI_POPUP_CONTRACT_RATE_SCROLL != null)
			{
				this.m_NKM_UI_POPUP_CONTRACT_RATE_SCROLL.dOnGetObject += this.MakeProbabilitySlot;
				this.m_NKM_UI_POPUP_CONTRACT_RATE_SCROLL.dOnReturnObject += this.ReturnProbabilitySlot;
				this.m_NKM_UI_POPUP_CONTRACT_RATE_SCROLL.dOnProvideData += this.ProvideProbabilitySlotData;
				NKCUtil.SetScrollHotKey(this.m_NKM_UI_POPUP_CONTRACT_RATE_SCROLL, null);
			}
			NKCUtil.SetBindFunction(this.m_BG, new UnityAction(base.Close));
			NKCUtil.SetBindFunction(this.m_NKM_UI_POPUP_CLOSEBUTTON, new UnityAction(base.Close));
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_CONTRACT_RATE_TEXT_TWN, NKCStringTable.GetNationalCode() == NKM_NATIONAL_CODE.NNC_TRADITIONAL_CHINESE);
		}

		// Token: 0x06008D84 RID: 36228 RVA: 0x00301C8D File Offset: 0x002FFE8D
		private RectTransform MakeProbabilitySlot(int index)
		{
			return UnityEngine.Object.Instantiate<NKCUIContractPopupRateSlot>(this.m_NKM_UI_POPUP_CONTRACT_RATE_SLOT).GetComponent<RectTransform>();
		}

		// Token: 0x06008D85 RID: 36229 RVA: 0x00301C9F File Offset: 0x002FFE9F
		private void ReturnProbabilitySlot(Transform go)
		{
			NKCUtil.SetGameobjectActive(go, false);
			go.SetParent(base.transform);
			UnityEngine.Object.Destroy(go.gameObject);
		}

		// Token: 0x06008D86 RID: 36230 RVA: 0x00301CC0 File Offset: 0x002FFEC0
		private void ProvideProbabilitySlotData(Transform tr, int idx)
		{
			NKCUIContractPopupRateSlot component = tr.GetComponent<NKCUIContractPopupRateSlot>();
			if (component != null)
			{
				if (this.m_lstUnitSlot.Count <= idx || idx < 0)
				{
					Debug.LogError(string.Format("m_lstUnitSlot - 잘못된 인덱스 입니다, {0}", idx));
					return;
				}
				component.SetData(this.m_lstUnitSlot[idx], this.m_lstUnitSlot[idx].Pickup);
			}
		}

		// Token: 0x06008D87 RID: 36231 RVA: 0x00301D28 File Offset: 0x002FFF28
		public void Open(int ContractID)
		{
			ContractTempletV2 contractTempletV = ContractTempletV2.Find(ContractID);
			if (contractTempletV != null)
			{
				this.Open(contractTempletV);
			}
		}

		// Token: 0x06008D88 RID: 36232 RVA: 0x00301D48 File Offset: 0x002FFF48
		public void Open(ContractTempletV2 templet)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_OK_BOX_TOP_TEXT, string.Format(NKCUtilString.GET_STRING_CONTRACT_POPUP_RATE_DETAIL_PERCENT_01, templet.GetContractName()));
			this.m_RandomGradeTemplet = templet.RandomGradeTemplet;
			this.m_UnitPoolTemplet = templet.UnitPoolTemplet;
			this.m_lstUnitSlot.Clear();
			this.m_lstPickUnitSlot.Clear();
			foreach (RandomUnitTempletV2 randomUnitTempletV in this.m_UnitPoolTemplet.UnitTemplets)
			{
				ContractUnitSlotData item = new ContractUnitSlotData(randomUnitTempletV.UnitTemplet.m_UnitID, randomUnitTempletV.UnitTemplet.m_NKM_UNIT_GRADE, randomUnitTempletV.UnitTemplet.m_NKM_UNIT_STYLE_TYPE, randomUnitTempletV.UnitTemplet.m_NKM_UNIT_ROLE_TYPE, randomUnitTempletV.UnitTemplet.m_bAwaken, randomUnitTempletV.UnitTemplet.IsRearmUnit, randomUnitTempletV.UnitTemplet.GetUnitName(), randomUnitTempletV.FinalRatePercent, randomUnitTempletV.RatioUpTarget, randomUnitTempletV.PickUpTarget);
				if (randomUnitTempletV.PickUpTarget)
				{
					this.m_lstPickUnitSlot.Add(item);
				}
				else
				{
					this.m_lstUnitSlot.Add(item);
				}
			}
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CONTRACT_SETTING_3_SSR_COUNT, string.Format("{0}%", this.m_RandomGradeTemplet.FinalRateSsrPercent));
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CONTRACT_SETTING_3_SR_COUNT, string.Format("{0}%", this.m_RandomGradeTemplet.FinalRateSrPercent));
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CONTRACT_SETTING_3_R_COUNT, string.Format("{0}%", this.m_RandomGradeTemplet.FinalRateRPercent));
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CONTRACT_SETTING_3_N_COUNT, string.Format("{0}%", this.m_RandomGradeTemplet.FinalRateNPercent));
			this.UpdatePickUpTable();
			if (this.m_rtm_NKM_UI_POPUP_CONTRACT_RATE_SCROLL != null)
			{
				this.m_rtm_NKM_UI_POPUP_CONTRACT_RATE_SCROLL.transform.SetAsLastSibling();
			}
			base.StartCoroutine(this.WaitForSetupChildLayout());
			base.UIOpened(true);
		}

		// Token: 0x06008D89 RID: 36233 RVA: 0x00301F44 File Offset: 0x00300144
		public void Open(NKMItemMiscTemplet contractItemTemplet)
		{
			if (!contractItemTemplet.IsContractItem)
			{
				return;
			}
			if (contractItemTemplet.MiscContractTemplet == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_OK_BOX_TOP_TEXT, string.Format(NKCUtilString.GET_STRING_CONTRACT_POPUP_RATE_DETAIL_PERCENT_01, contractItemTemplet.GetItemName()));
			this.m_RandomGradeTemplet = contractItemTemplet.MiscContractTemplet.RandomGradeTemplet;
			this.m_UnitPoolTemplet = contractItemTemplet.MiscContractTemplet.UnitPoolTemplet;
			this.m_lstUnitSlot.Clear();
			this.m_lstPickUnitSlot.Clear();
			foreach (RandomUnitTempletV2 randomUnitTempletV in this.m_UnitPoolTemplet.UnitTemplets)
			{
				ContractUnitSlotData item = new ContractUnitSlotData(randomUnitTempletV.UnitTemplet.m_UnitID, randomUnitTempletV.UnitTemplet.m_NKM_UNIT_GRADE, randomUnitTempletV.UnitTemplet.m_NKM_UNIT_STYLE_TYPE, randomUnitTempletV.UnitTemplet.m_NKM_UNIT_ROLE_TYPE, randomUnitTempletV.UnitTemplet.m_bAwaken, randomUnitTempletV.UnitTemplet.IsRearmUnit, randomUnitTempletV.UnitTemplet.GetUnitName(), randomUnitTempletV.FinalRatePercent, randomUnitTempletV.RatioUpTarget, randomUnitTempletV.PickUpTarget);
				if (randomUnitTempletV.PickUpTarget)
				{
					this.m_lstPickUnitSlot.Add(item);
				}
				else
				{
					this.m_lstUnitSlot.Add(item);
				}
			}
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CONTRACT_SETTING_3_SSR_COUNT, string.Format("{0}%", this.m_RandomGradeTemplet.FinalRateSsrPercent));
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CONTRACT_SETTING_3_SR_COUNT, string.Format("{0}%", this.m_RandomGradeTemplet.FinalRateSrPercent));
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CONTRACT_SETTING_3_R_COUNT, string.Format("{0}%", this.m_RandomGradeTemplet.FinalRateRPercent));
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CONTRACT_SETTING_3_N_COUNT, string.Format("{0}%", this.m_RandomGradeTemplet.FinalRateNPercent));
			this.UpdatePickUpTable();
			if (this.m_rtm_NKM_UI_POPUP_CONTRACT_RATE_SCROLL != null)
			{
				this.m_rtm_NKM_UI_POPUP_CONTRACT_RATE_SCROLL.transform.SetAsLastSibling();
			}
			base.StartCoroutine(this.WaitForSetupChildLayout());
			base.UIOpened(true);
		}

		// Token: 0x06008D8A RID: 36234 RVA: 0x0030215C File Offset: 0x0030035C
		private IEnumerator WaitForSetupChildLayout()
		{
			yield return new WaitForEndOfFrame();
			this.m_NKM_UI_POPUP_CONTRACT_RATE_SCROLL.PrepareCells(0);
			this.m_NKM_UI_POPUP_CONTRACT_RATE_SCROLL.TotalCount = this.m_UnitPoolTemplet.TotalUnitCount - this.m_lstPickUnitSlot.Count;
			this.m_NKM_UI_POPUP_CONTRACT_RATE_SCROLL.velocity = new Vector2(0f, 0f);
			this.m_NKM_UI_POPUP_CONTRACT_RATE_SCROLL.SetIndexPosition(0);
			yield break;
		}

		// Token: 0x06008D8B RID: 36235 RVA: 0x0030216C File Offset: 0x0030036C
		private void UpdatePickUpTable()
		{
			if (this.m_lstPickUnitSlot.Count >= 4)
			{
				List<ContractUnitSlotData> list = new List<ContractUnitSlotData>();
				list.AddRange(this.m_lstPickUnitSlot);
				list.AddRange(this.m_lstUnitSlot);
				this.m_lstPickUnitSlot.Clear();
				this.m_lstUnitSlot = list;
			}
			if (this.m_lstPickUnitSlot.Count <= 0)
			{
				NKCUtil.SetGameobjectActive(this.m_objNKM_UI_POPUP_CONTRACT_RATE_FEATURED_BASE, false);
				return;
			}
			for (int i = 0; i < this.m_lstPickUnitSlot.Count; i++)
			{
				if (this.m_lstPickUnitSlot[i] == null)
				{
					Debug.LogError(string.Format("Error - UpdatePickUpTable : Slot Index : {0}", i));
				}
				else if (i == 0)
				{
					NKCUtil.SetGameobjectActive(this.m_objNKM_UI_POPUP_CONTRACT_RATE_FEATURED_BASE, true);
					this.m_NKM_UI_POPUP_CONTRACT_RATE_FEATURED_BASE.SetData(this.m_lstPickUnitSlot[i], true);
				}
				else
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_NKM_UI_POPUP_CONTRACT_RATE_FEATURED);
					if (gameObject != null)
					{
						gameObject.transform.SetParent(this.m_rtNKM_UI_POPUP_CONTRACT_RATE_FEATURED, false);
						NKCUIContractPopupRateSlot componentInChildren = gameObject.GetComponentInChildren<NKCUIContractPopupRateSlot>();
						if (componentInChildren != null)
						{
							componentInChildren.SetData(this.m_lstPickUnitSlot[i], true);
						}
						this.m_DummyPickUpSlot.Add(gameObject);
					}
				}
			}
		}

		// Token: 0x04007A50 RID: 31312
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_contract";

		// Token: 0x04007A51 RID: 31313
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_CONTRACT_RATE";

		// Token: 0x04007A52 RID: 31314
		private static NKCUIContractPopupRateV2 m_Instance;

		// Token: 0x04007A53 RID: 31315
		[Header("타이틀")]
		public Text m_NKM_UI_POPUP_OK_BOX_TOP_TEXT;

		// Token: 0x04007A54 RID: 31316
		[Header("버튼")]
		public NKCUIComStateButton m_NKM_UI_POPUP_CLOSEBUTTON;

		// Token: 0x04007A55 RID: 31317
		public NKCUIComStateButton m_BG;

		// Token: 0x04007A56 RID: 31318
		[Header("우측 스크롤")]
		public VerticalLayoutGroup NKM_UI_POPUP_CONTRACT_SLOT_layoutgroup;

		// Token: 0x04007A57 RID: 31319
		public RectTransform m_rtm_NKM_UI_POPUP_CONTRACT_RATE_SCROLL;

		// Token: 0x04007A58 RID: 31320
		public LoopVerticalScrollRect m_NKM_UI_POPUP_CONTRACT_RATE_SCROLL;

		// Token: 0x04007A59 RID: 31321
		[Header("등급별 확률")]
		public Text m_NKM_UI_POPUP_CONTRACT_SETTING_3_SSR_COUNT;

		// Token: 0x04007A5A RID: 31322
		public Text m_NKM_UI_POPUP_CONTRACT_SETTING_3_SR_COUNT;

		// Token: 0x04007A5B RID: 31323
		public Text m_NKM_UI_POPUP_CONTRACT_SETTING_3_R_COUNT;

		// Token: 0x04007A5C RID: 31324
		public Text m_NKM_UI_POPUP_CONTRACT_SETTING_3_N_COUNT;

		// Token: 0x04007A5D RID: 31325
		[Header("픽업 확률")]
		public RectTransform m_rtNKM_UI_POPUP_CONTRACT_RATE_FEATURED;

		// Token: 0x04007A5E RID: 31326
		public RectTransform m_NKM_UI_POPUP_CONTRACT_RATE_SCROLL_Contents;

		// Token: 0x04007A5F RID: 31327
		[Header("기본 확률 슬롯")]
		public GameObject m_objNKM_UI_POPUP_CONTRACT_RATE_FEATURED_BASE;

		// Token: 0x04007A60 RID: 31328
		public NKCUIContractPopupRateSlot m_NKM_UI_POPUP_CONTRACT_RATE_FEATURED_BASE;

		// Token: 0x04007A61 RID: 31329
		[Header("추가 슬롯")]
		public GameObject m_NKM_UI_POPUP_CONTRACT_RATE_FEATURED;

		// Token: 0x04007A62 RID: 31330
		public NKCUIContractPopupRateSlot m_NKM_UI_POPUP_CONTRACT_RATE_SLOT;

		// Token: 0x04007A63 RID: 31331
		[Space]
		public GameObject m_NKM_UI_POPUP_CONTRACT_RATE_TEXT_TWN;

		// Token: 0x04007A64 RID: 31332
		private List<GameObject> m_DummyPickUpSlot = new List<GameObject>();

		// Token: 0x04007A65 RID: 31333
		private List<ContractUnitSlotData> m_lstPickUnitSlot = new List<ContractUnitSlotData>();

		// Token: 0x04007A66 RID: 31334
		private List<ContractUnitSlotData> m_lstUnitSlot = new List<ContractUnitSlotData>();

		// Token: 0x04007A67 RID: 31335
		private IRandomUnitPool m_UnitPoolTemplet;

		// Token: 0x04007A68 RID: 31336
		private RandomGradeTempletV2 m_RandomGradeTemplet;
	}
}
