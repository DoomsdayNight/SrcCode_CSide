using System;
using System.Collections.Generic;
using ClientPacket.Contract;
using NKM;
using NKM.Contract2;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Contract
{
	// Token: 0x02000BED RID: 3053
	public class NKCUIContractSelection : NKCUIBase
	{
		// Token: 0x1700168C RID: 5772
		// (get) Token: 0x06008D8D RID: 36237 RVA: 0x003022BB File Offset: 0x003004BB
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x1700168D RID: 5773
		// (get) Token: 0x06008D8E RID: 36238 RVA: 0x003022BE File Offset: 0x003004BE
		public override string MenuName
		{
			get
			{
				return "채용 후보 리스트";
			}
		}

		// Token: 0x1700168E RID: 5774
		// (get) Token: 0x06008D8F RID: 36239 RVA: 0x003022C8 File Offset: 0x003004C8
		public static NKCUIContractSelection Instance
		{
			get
			{
				if (NKCUIContractSelection.m_Instance == null)
				{
					NKCUIContractSelection.m_Instance = NKCUIManager.OpenNewInstance<NKCUIContractSelection>("AB_UI_NKM_UI_CONTRACT_V2", "NKM_UI_CONTRACT_V2_SELECTION_BEGINNER_RESULT", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIContractSelection.CleanupInstance)).GetInstance<NKCUIContractSelection>();
					NKCUIContractSelection.m_Instance.Init();
				}
				return NKCUIContractSelection.m_Instance;
			}
		}

		// Token: 0x06008D90 RID: 36240 RVA: 0x00302317 File Offset: 0x00300517
		private static void CleanupInstance()
		{
			NKCUIContractSelection.m_Instance = null;
		}

		// Token: 0x1700168F RID: 5775
		// (get) Token: 0x06008D91 RID: 36241 RVA: 0x0030231F File Offset: 0x0030051F
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIContractSelection.m_Instance != null && NKCUIContractSelection.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008D92 RID: 36242 RVA: 0x0030233A File Offset: 0x0030053A
		private void OnDestroy()
		{
			NKCUIContractSelection.m_Instance = null;
		}

		// Token: 0x06008D93 RID: 36243 RVA: 0x00302344 File Offset: 0x00300544
		private void Init()
		{
			if (this.NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT == null)
			{
				return;
			}
			if (this.NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT.Length < 10)
			{
				string format = "선택슬롯 갯수 확인 필요 - 현재 갯수 : {0}";
				NKCUIUnitSelectListSlot[] nkm_UI_UNIT_SELECT_LIST_UNIT_SLOT = this.NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT;
				Debug.LogError(string.Format(format, (nkm_UI_UNIT_SELECT_LIST_UNIT_SLOT != null) ? new int?(nkm_UI_UNIT_SELECT_LIST_UNIT_SLOT.Length) : null));
			}
			NKCUIUnitSelectListSlot[] nkm_UI_UNIT_SELECT_LIST_UNIT_SLOT2 = this.NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT;
			for (int i = 0; i < nkm_UI_UNIT_SELECT_LIST_UNIT_SLOT2.Length; i++)
			{
				nkm_UI_UNIT_SELECT_LIST_UNIT_SLOT2[i].gameObject.SetActive(false);
			}
			NKCUtil.SetBindFunction(this.UPSIDE_BACK_BUTTON, delegate()
			{
				base.Close();
			});
			if (this.SELECTION_BEGINNER_RESULT_Button01_TEXT != null)
			{
				this.m_ChangeConfirmTextOriginalColor = this.SELECTION_BEGINNER_RESULT_Button01_TEXT.color;
			}
		}

		// Token: 0x06008D94 RID: 36244 RVA: 0x003023EF File Offset: 0x003005EF
		public override void CloseInternal()
		{
			UnityEngine.Object.Destroy(NKCUIContractSelection.m_Instance.gameObject);
			NKCUIContractSelection.m_Instance = null;
			if (NKCGameEventManager.IsWaiting())
			{
				NKCGameEventManager.WaitFinished();
			}
		}

		// Token: 0x06008D95 RID: 36245 RVA: 0x00302412 File Offset: 0x00300612
		public void Open(NKMSelectableContractState state)
		{
			if (state != null)
			{
				this.Open(state.contractId, state.unitIdList, state.unitPoolChangeCount);
			}
		}

		// Token: 0x06008D96 RID: 36246 RVA: 0x00302430 File Offset: 0x00300630
		public void Open(int contractID, List<int> unitIdList, int poolChangeCount = 0)
		{
			if (unitIdList.Count > 10)
			{
				return;
			}
			this.m_lstUnit.Clear();
			for (int i = 0; i < unitIdList.Count; i++)
			{
				NKMUnitData nkmunitData = NKCUtil.MakeDummyUnit(unitIdList[i], false);
				if (nkmunitData != null)
				{
					this.m_lstUnit.Add(nkmunitData);
				}
			}
			this.SELECTION_BEGINNER_RESULT_Button02.OnShow(false);
			this.m_bWaitForGameObjectActive = true;
			SelectableContractTemplet selectableContractTemplet = SelectableContractTemplet.Find(contractID);
			if (selectableContractTemplet != null)
			{
				this.m_SelectableContractTemplet = selectableContractTemplet;
				if (this.m_SelectableContractTemplet.m_RequireItem != null && this.m_SelectableContractTemplet.m_RequireItem.Count != 0L)
				{
					this.SELECTION_BEGINNER_RESULT_Button02.OnShow(true);
					this.SELECTION_BEGINNER_RESULT_Button02.SetData(this.m_SelectableContractTemplet.m_RequireItem.ItemId, this.m_SelectableContractTemplet.m_RequireItem.Count32);
				}
				this.UpdateUI(poolChangeCount);
			}
			base.UIOpened(true);
			this.CheckTutorial();
		}

		// Token: 0x06008D97 RID: 36247 RVA: 0x0030250F File Offset: 0x0030070F
		private void Update()
		{
			if (this.m_bWaitForGameObjectActive && base.gameObject.activeSelf)
			{
				this.m_bWaitForGameObjectActive = false;
				this.UpdateUnitSlot(this.m_lstUnit);
			}
		}

		// Token: 0x06008D98 RID: 36248 RVA: 0x0030253C File Offset: 0x0030073C
		private void UpdateUnitSlot(List<NKMUnitData> lstUnit)
		{
			if (lstUnit.Count != this.NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT.Length)
			{
				return;
			}
			for (int i = 0; i < lstUnit.Count; i++)
			{
				if (lstUnit[i] == null)
				{
					Debug.LogError(string.Format("NKCUIContractSelection::UpdateUnitSlot unit data is null : {0}", i));
				}
				else if (this.NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT[i] == null)
				{
					Debug.LogError(string.Format("NKCUIContractSelection::UpdateUnitSlot slot data is null : {0}", i));
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT[i].gameObject, true);
					this.NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT[i].SetDataForContractSelection(lstUnit[i], false);
					Animator component = this.NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT[i].GetComponent<Animator>();
					if (component == null)
					{
						Debug.LogError(string.Format("NKCUIContractSelection::UpdateUnitSlot Animator data is null : {0}", i));
					}
					else
					{
						NKM_UNIT_GRADE unitGrade = lstUnit[i].GetUnitGrade();
						if (unitGrade != NKM_UNIT_GRADE.NUG_SR)
						{
							if (unitGrade == NKM_UNIT_GRADE.NUG_SSR)
							{
								component.SetTrigger("SSR");
							}
							else
							{
								component.SetTrigger("NONE");
							}
						}
						else
						{
							component.SetTrigger("SR");
						}
					}
				}
			}
		}

		// Token: 0x06008D99 RID: 36249 RVA: 0x00302650 File Offset: 0x00300850
		private void UpdateUI(int CurCnt = 0)
		{
			NKCUtil.SetLabelText(this.TOP_TEXT, string.Format(NKCUtilString.GET_STRING_CONTRACT_SELECTION_TITLE, this.m_SelectableContractTemplet.GetContractName()));
			int unitPoolChangeCount = this.m_SelectableContractTemplet.m_UnitPoolChangeCount;
			if (this.PROCEEDING_COUNT_TEXT != null)
			{
				this.PROCEEDING_COUNT_TEXT.text = string.Format("{0}/{1}", CurCnt, unitPoolChangeCount);
			}
			bool flag = true;
			if (this.m_SelectableContractTemplet.m_RequireItem != null && this.m_SelectableContractTemplet.m_RequireItem.ItemId != 0)
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData != null && nkmuserData.m_InventoryData.GetCountMiscItem(this.m_SelectableContractTemplet.m_RequireItem.ItemId) < this.m_SelectableContractTemplet.m_RequireItem.Count)
				{
					flag = false;
				}
				if (flag)
				{
					if (this.m_SelectableContractTemplet.m_RequireItem.Count > 0L)
					{
						NKCUtil.SetBindFunction(this.SELECTION_BEGINNER_RESULT_Button02, new UnityAction(this.SelectableContractConfirmUseItem));
					}
					else
					{
						NKCUtil.SetBindFunction(this.SELECTION_BEGINNER_RESULT_Button02, new UnityAction(this.SelectableContractConfirm));
					}
				}
				else
				{
					NKCUtil.SetBindFunction(this.SELECTION_BEGINNER_RESULT_Button02, delegate()
					{
						NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(this.m_SelectableContractTemplet.m_RequireItem.ItemId);
						NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_NOTICE, string.Format(NKCUtilString.GET_STRING_SELECTABLE_CONTRACT_NOT_ENOUGH, itemMiscTempletByID.GetItemName()), this.m_SelectableContractTemplet.m_RequireItem.ItemId, this.m_SelectableContractTemplet.m_RequireItem.Count32, null, null, false);
					});
				}
			}
			else
			{
				NKCUtil.SetBindFunction(this.SELECTION_BEGINNER_RESULT_Button02, new UnityAction(this.SelectableContractConfirm));
			}
			if (unitPoolChangeCount <= CurCnt)
			{
				NKCUIComStateButton selection_BEGINNER_RESULT_Button = this.SELECTION_BEGINNER_RESULT_Button01;
				if (selection_BEGINNER_RESULT_Button != null)
				{
					selection_BEGINNER_RESULT_Button.PointerClick.RemoveAllListeners();
				}
				NKCUtil.SetImageSprite(this.img_SELECTION_BEGINNER_RESULT_Button01, NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY), false);
				NKCUtil.SetLabelTextColor(this.SELECTION_BEGINNER_RESULT_Button01_TEXT, NKCUtil.GetColor("#212122"));
				return;
			}
			NKCUtil.SetBindFunction(this.SELECTION_BEGINNER_RESULT_Button01, new UnityAction(this.ChangeUnitPool));
			NKCUtil.SetImageSprite(this.img_SELECTION_BEGINNER_RESULT_Button01, NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_YELLOW), false);
			NKCUtil.SetLabelTextColor(this.SELECTION_BEGINNER_RESULT_Button01_TEXT, this.m_ChangeConfirmTextOriginalColor);
		}

		// Token: 0x06008D9A RID: 36250 RVA: 0x0030280C File Offset: 0x00300A0C
		private void ChangeUnitPool()
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_SELECTABLE_CONTRACT_UNIT_POOL_CHANGE_CONFIRM, delegate()
			{
				NKCPacketSender.Send_NKMPacket_SELECTABLE_CONTRACT_CHANGE_POOL_REQ(this.m_SelectableContractTemplet.Key);
			}, null, false);
		}

		// Token: 0x06008D9B RID: 36251 RVA: 0x0030282C File Offset: 0x00300A2C
		private void SelectableContractConfirmUseItem()
		{
			NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_SELECTABLE_CONTRACT_USE_ITEM, this.m_SelectableContractTemplet.m_RequireItem.ItemId, this.m_SelectableContractTemplet.m_RequireItem.Count32, delegate()
			{
				NKCPacketSender.Send_NKMPacket_SELECTABLE_CONTRACT_CONFIRM_REQ(this.m_SelectableContractTemplet.Key);
			}, null, false);
		}

		// Token: 0x06008D9C RID: 36252 RVA: 0x0030287B File Offset: 0x00300A7B
		private void SelectableContractConfirm()
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_SELECTABLE_CONTRACT_CONFIRM, delegate()
			{
				NKCPacketSender.Send_NKMPacket_SELECTABLE_CONTRACT_CONFIRM_REQ(this.m_SelectableContractTemplet.Key);
			}, null, false);
		}

		// Token: 0x06008D9D RID: 36253 RVA: 0x0030289A File Offset: 0x00300A9A
		private void CheckTutorial()
		{
			if (NKCGameEventManager.IsWaiting())
			{
				NKCGameEventManager.WaitFinished();
				return;
			}
			NKCTutorialManager.TutorialRequired(TutorialPoint.ContractSelection, true);
		}

		// Token: 0x04007A69 RID: 31337
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONTRACT_V2";

		// Token: 0x04007A6A RID: 31338
		private const string UI_ASSET_NAME = "NKM_UI_CONTRACT_V2_SELECTION_BEGINNER_RESULT";

		// Token: 0x04007A6B RID: 31339
		private static NKCUIContractSelection m_Instance;

		// Token: 0x04007A6C RID: 31340
		[Header("채용 타이틀")]
		public NKCComText TOP_TEXT;

		// Token: 0x04007A6D RID: 31341
		[Header("채용 유닛 슬롯")]
		public NKCUIUnitSelectListSlot[] NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT;

		// Token: 0x04007A6E RID: 31342
		[Header("버튼")]
		public NKCUIComStateButton SELECTION_BEGINNER_RESULT_Button01;

		// Token: 0x04007A6F RID: 31343
		public Image img_SELECTION_BEGINNER_RESULT_Button01;

		// Token: 0x04007A70 RID: 31344
		public Text SELECTION_BEGINNER_RESULT_Button01_TEXT;

		// Token: 0x04007A71 RID: 31345
		public NKCComText PROCEEDING_COUNT_TEXT;

		// Token: 0x04007A72 RID: 31346
		[Space]
		public NKCUIComResourceButton SELECTION_BEGINNER_RESULT_Button02;

		// Token: 0x04007A73 RID: 31347
		public NKCUIComStateButton UPSIDE_BACK_BUTTON;

		// Token: 0x04007A74 RID: 31348
		private Color m_ChangeConfirmTextOriginalColor;

		// Token: 0x04007A75 RID: 31349
		private SelectableContractTemplet m_SelectableContractTemplet;

		// Token: 0x04007A76 RID: 31350
		private List<NKMUnitData> m_lstUnit = new List<NKMUnitData>();

		// Token: 0x04007A77 RID: 31351
		private bool m_bWaitForGameObjectActive;
	}
}
