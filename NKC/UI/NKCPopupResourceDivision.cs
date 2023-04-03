using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A79 RID: 2681
	public class NKCPopupResourceDivision : NKCUIBase
	{
		// Token: 0x170013C7 RID: 5063
		// (get) Token: 0x060076B1 RID: 30385 RVA: 0x00278494 File Offset: 0x00276694
		public static NKCPopupResourceDivision Instance
		{
			get
			{
				if (NKCPopupResourceDivision.m_Instance == null)
				{
					NKCPopupResourceDivision.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupResourceDivision>("AB_UI_NKM_UI_GAME_OPTION", "NKM_UI_GAME_OPTION_RESOURCE_DIVISION", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupResourceDivision.CleanupInstance)).GetInstance<NKCPopupResourceDivision>();
					NKCPopupResourceDivision instance = NKCPopupResourceDivision.m_Instance;
					if (instance != null)
					{
						instance.Init();
					}
				}
				return NKCPopupResourceDivision.m_Instance;
			}
		}

		// Token: 0x170013C8 RID: 5064
		// (get) Token: 0x060076B2 RID: 30386 RVA: 0x002784E9 File Offset: 0x002766E9
		public static bool HasInstance
		{
			get
			{
				return NKCPopupResourceDivision.m_Instance != null;
			}
		}

		// Token: 0x170013C9 RID: 5065
		// (get) Token: 0x060076B3 RID: 30387 RVA: 0x002784F6 File Offset: 0x002766F6
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupResourceDivision.m_Instance != null && NKCPopupResourceDivision.m_Instance.IsOpen;
			}
		}

		// Token: 0x060076B4 RID: 30388 RVA: 0x00278511 File Offset: 0x00276711
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupResourceDivision.m_Instance != null && NKCPopupResourceDivision.m_Instance.IsOpen)
			{
				NKCPopupResourceDivision.m_Instance.Close();
			}
		}

		// Token: 0x060076B5 RID: 30389 RVA: 0x00278536 File Offset: 0x00276736
		private static void CleanupInstance()
		{
			NKCPopupResourceDivision.m_Instance = null;
		}

		// Token: 0x170013CA RID: 5066
		// (get) Token: 0x060076B6 RID: 30390 RVA: 0x0027853E File Offset: 0x0027673E
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170013CB RID: 5067
		// (get) Token: 0x060076B7 RID: 30391 RVA: 0x00278541 File Offset: 0x00276741
		public override string MenuName
		{
			get
			{
				return "Resource Division";
			}
		}

		// Token: 0x060076B8 RID: 30392 RVA: 0x00278548 File Offset: 0x00276748
		private void Init()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			if (this.m_eventTriggerBg != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(delegate(BaseEventData eventData)
				{
					base.Close();
				});
				this.m_eventTriggerBg.triggers.Add(entry);
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnConfirm, new UnityAction(base.Close));
			Sprite orLoadMiscItemIcon = NKCResourceUtility.GetOrLoadMiscItemIcon(102);
			NKCUtil.SetImageSprite(this.m_imgPakagaMedalFree, orLoadMiscItemIcon, false);
			NKCUtil.SetImageSprite(this.m_imgPakagaMedalPaid, orLoadMiscItemIcon, false);
			orLoadMiscItemIcon = NKCResourceUtility.GetOrLoadMiscItemIcon(101);
			NKCUtil.SetImageSprite(this.m_imgQuartzFree, orLoadMiscItemIcon, false);
			NKCUtil.SetImageSprite(this.m_imgQuartzPaid, orLoadMiscItemIcon, false);
			orLoadMiscItemIcon = NKCResourceUtility.GetOrLoadMiscItemIcon(1034);
			NKCUtil.SetImageSprite(this.m_imgContractClassifiedFree, orLoadMiscItemIcon, false);
			NKCUtil.SetImageSprite(this.m_imgContractClassifiedPaid, orLoadMiscItemIcon, false);
		}

		// Token: 0x060076B9 RID: 30393 RVA: 0x00278629 File Offset: 0x00276829
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x060076BA RID: 30394 RVA: 0x00278638 File Offset: 0x00276838
		public void Open()
		{
			long countMiscItem = NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(102, true);
			NKCUtil.SetLabelText(this.m_lbPakageMedalPaidCount, countMiscItem.ToString());
			long countMiscItem2 = NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(102, false);
			NKCUtil.SetLabelText(this.m_lbPakageMedalFreeCount, countMiscItem2.ToString());
			countMiscItem = NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(101, true);
			NKCUtil.SetLabelText(this.m_lbQuartzPaidCount, countMiscItem.ToString());
			countMiscItem2 = NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(101, false);
			NKCUtil.SetLabelText(this.m_lbQuartzFreeCount, countMiscItem2.ToString());
			countMiscItem = NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(1034, true);
			NKCUtil.SetLabelText(this.m_lbContractClassifiedPaidCount, countMiscItem.ToString());
			countMiscItem2 = NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(1034, false);
			NKCUtil.SetLabelText(this.m_lbContractClassifiedFreeCount, countMiscItem2.ToString());
			base.gameObject.SetActive(true);
			NKCUIOpenAnimator nkcuiopenAnimator = this.m_NKCUIOpenAnimator;
			if (nkcuiopenAnimator != null)
			{
				nkcuiopenAnimator.PlayOpenAni();
			}
			base.UIOpened(true);
		}

		// Token: 0x060076BB RID: 30395 RVA: 0x0027874D File Offset: 0x0027694D
		private void Update()
		{
			if (!base.IsOpen)
			{
				return;
			}
			NKCUIOpenAnimator nkcuiopenAnimator = this.m_NKCUIOpenAnimator;
			if (nkcuiopenAnimator == null)
			{
				return;
			}
			nkcuiopenAnimator.Update();
		}

		// Token: 0x0400632D RID: 25389
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_GAME_OPTION";

		// Token: 0x0400632E RID: 25390
		private const string UI_ASSET_NAME = "NKM_UI_GAME_OPTION_RESOURCE_DIVISION";

		// Token: 0x0400632F RID: 25391
		private static NKCPopupResourceDivision m_Instance;

		// Token: 0x04006330 RID: 25392
		public EventTrigger m_eventTriggerBg;

		// Token: 0x04006331 RID: 25393
		public NKCUIComStateButton m_csbtnConfirm;

		// Token: 0x04006332 RID: 25394
		public Image m_imgPakagaMedalPaid;

		// Token: 0x04006333 RID: 25395
		public Text m_lbPakageMedalPaidCount;

		// Token: 0x04006334 RID: 25396
		public Image m_imgPakagaMedalFree;

		// Token: 0x04006335 RID: 25397
		public Text m_lbPakageMedalFreeCount;

		// Token: 0x04006336 RID: 25398
		public Image m_imgQuartzPaid;

		// Token: 0x04006337 RID: 25399
		public Text m_lbQuartzPaidCount;

		// Token: 0x04006338 RID: 25400
		public Image m_imgQuartzFree;

		// Token: 0x04006339 RID: 25401
		public Text m_lbQuartzFreeCount;

		// Token: 0x0400633A RID: 25402
		public Image m_imgContractClassifiedPaid;

		// Token: 0x0400633B RID: 25403
		public Text m_lbContractClassifiedPaidCount;

		// Token: 0x0400633C RID: 25404
		public Image m_imgContractClassifiedFree;

		// Token: 0x0400633D RID: 25405
		public Text m_lbContractClassifiedFreeCount;

		// Token: 0x0400633E RID: 25406
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;
	}
}
