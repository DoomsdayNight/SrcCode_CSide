using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NKM.EventPass;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x02000A48 RID: 2632
	public class NKCPopupEventPassPurchase : NKCUIBase
	{
		// Token: 0x1700133C RID: 4924
		// (get) Token: 0x06007379 RID: 29561 RVA: 0x00266A38 File Offset: 0x00264C38
		public static NKCPopupEventPassPurchase Instance
		{
			get
			{
				if (NKCPopupEventPassPurchase.m_Instance == null)
				{
					NKCPopupEventPassPurchase.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupEventPassPurchase>("AB_UI_NKM_UI_EVENT_PASS", "NKM_UI_POPUP_EVENT_PASS_BUY", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupEventPassPurchase.CleanupInstance)).GetInstance<NKCPopupEventPassPurchase>();
					NKCPopupEventPassPurchase instance = NKCPopupEventPassPurchase.m_Instance;
					if (instance != null)
					{
						instance.Init();
					}
				}
				return NKCPopupEventPassPurchase.m_Instance;
			}
		}

		// Token: 0x1700133D RID: 4925
		// (get) Token: 0x0600737A RID: 29562 RVA: 0x00266A8D File Offset: 0x00264C8D
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupEventPassPurchase.m_Instance != null && NKCPopupEventPassPurchase.m_Instance.IsOpen;
			}
		}

		// Token: 0x0600737B RID: 29563 RVA: 0x00266AA8 File Offset: 0x00264CA8
		private static void CleanupInstance()
		{
			NKCPopupEventPassPurchase.m_Instance = null;
		}

		// Token: 0x1700133E RID: 4926
		// (get) Token: 0x0600737C RID: 29564 RVA: 0x00266AB0 File Offset: 0x00264CB0
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x1700133F RID: 4927
		// (get) Token: 0x0600737D RID: 29565 RVA: 0x00266AB3 File Offset: 0x00264CB3
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_EVENTPASS_EVENT_PASS_MENU_TITLE;
			}
		}

		// Token: 0x17001340 RID: 4928
		// (get) Token: 0x0600737E RID: 29566 RVA: 0x00266ABA File Offset: 0x00264CBA
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				if (NKCUIEventPass.HasInstance)
				{
					return NKCUIEventPass.Instance.UpsideMenuShowResourceList;
				}
				return base.UpsideMenuShowResourceList;
			}
		}

		// Token: 0x0600737F RID: 29567 RVA: 0x00266AD4 File Offset: 0x00264CD4
		private void Init()
		{
			NKCEventPassDataManager eventPassDataManager = NKCScenManager.GetScenManager().GetEventPassDataManager();
			if (eventPassDataManager == null)
			{
				return;
			}
			if (NKMTempletContainer<NKMEventPassTemplet>.Find(eventPassDataManager.EventPassId) == null)
			{
				return;
			}
			NKCUIComEventPassBuySlot corePassPlan = this.m_corePassPlan;
			if (corePassPlan != null)
			{
				corePassPlan.Init();
			}
			NKCUIComEventPassBuySlot corePassPlanPlus = this.m_corePassPlanPlus;
			if (corePassPlanPlus != null)
			{
				corePassPlanPlus.Init();
			}
			NKCUICharacterView characterView = this.m_characterView;
			if (characterView != null)
			{
				characterView.Init(null, null);
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnClose, new UnityAction(this.OnClickClose));
		}

		// Token: 0x06007380 RID: 29568 RVA: 0x00266B4A File Offset: 0x00264D4A
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007381 RID: 29569 RVA: 0x00266B58 File Offset: 0x00264D58
		public void Open(bool endTimeNotice, NKCPopupEventPassPurchase.EventTimeCheck eventTimeCheck)
		{
			NKCEventPassDataManager eventPassDataManager = NKCScenManager.GetScenManager().GetEventPassDataManager();
			if (eventPassDataManager == null)
			{
				return;
			}
			NKMEventPassTemplet nkmeventPassTemplet = NKMTempletContainer<NKMEventPassTemplet>.Find(eventPassDataManager.EventPassId);
			if (nkmeventPassTemplet == null)
			{
				return;
			}
			NKMEventPassCoreProductTemplet corePassProduct = nkmeventPassTemplet.CorePassProduct;
			NKMEventPassCorePlusProductTemplet corePassPlusPrdouct = nkmeventPassTemplet.CorePassPlusPrdouct;
			if (corePassProduct != null)
			{
				string @string = NKCStringTable.GetString(corePassProduct.StrId, false);
				string string2 = NKCStringTable.GetString(corePassProduct.DescStrId, false);
				this.m_corePassPlan.SetData(@string, string2, NKCUIEventPass.Instance.UserPassLevel, 0, corePassProduct.PriceId, corePassProduct.PriceCount, 0f, 0, new NKCUIComEventPassBuySlot.ClickBuy(this.OnClickCorePassBuy));
			}
			if (corePassPlusPrdouct != null)
			{
				int addPassLevel = corePassPlusPrdouct.PassExp / nkmeventPassTemplet.PassLevelUpExp;
				string string3 = NKCStringTable.GetString(corePassPlusPrdouct.StrId, false);
				string string4 = NKCStringTable.GetString(corePassPlusPrdouct.DescStrId, false);
				int corePassPriceDiscounted = nkmeventPassTemplet.GetCorePassPriceDiscounted(corePassPlusPrdouct.PriceCount);
				this.m_corePassPlanPlus.SetData(string3, string4, NKCUIEventPass.Instance.UserPassLevel, addPassLevel, corePassPlusPrdouct.PriceId, corePassPlusPrdouct.PriceCount, nkmeventPassTemplet.CorePassDiscountPercent, corePassPriceDiscounted, new NKCUIComEventPassBuySlot.ClickBuy(this.OnClickCorePassPlusBuy));
			}
			NKCUIEventPass.SetMaxLevelMainRewardImage(nkmeventPassTemplet, this.m_characterView, this.m_eventPassEquip, this.m_objEquipRoot);
			NKCUtil.SetGameobjectActive(this.m_objBuyNotice, endTimeNotice);
			this.m_dEventTimeCheck = eventTimeCheck;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			base.UIOpened(true);
		}

		// Token: 0x06007382 RID: 29570 RVA: 0x00266CA0 File Offset: 0x00264EA0
		private void OnClickCorePassBuy()
		{
			if (this.m_dEventTimeCheck != null && !this.m_dEventTimeCheck(true))
			{
				return;
			}
			NKCEventPassDataManager eventPassDataManager = NKCScenManager.GetScenManager().GetEventPassDataManager();
			if (eventPassDataManager == null)
			{
				return;
			}
			NKMEventPassTemplet nkmeventPassTemplet = NKMTempletContainer<NKMEventPassTemplet>.Find(eventPassDataManager.EventPassId);
			if (nkmeventPassTemplet == null)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(NKCUtilString.GET_STRING_PURCHASE_POPUP_DESC, NKCStringTable.GetString(nkmeventPassTemplet.CorePassProduct.StrId, false));
			stringBuilder.Append("\n");
			stringBuilder.AppendFormat("<size={0}>{1}</size>", this.m_iRefundImpossibleMsgSize, NKCUtilString.GET_STRING_PURCHASE_REFUND_IMPOSSIBLE);
			NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_NOTICE, stringBuilder.ToString(), nkmeventPassTemplet.CorePassProduct.PriceId, nkmeventPassTemplet.CorePassProduct.PriceCount, delegate()
			{
				if (this.m_dEventTimeCheck != null && !this.m_dEventTimeCheck(true))
				{
					return;
				}
				NKCPacketSender.Send_NKMPacket_EVENT_PASS_PURCHASE_CORE_PASS_REQ();
			}, null, false);
		}

		// Token: 0x06007383 RID: 29571 RVA: 0x00266D68 File Offset: 0x00264F68
		private void OnClickCorePassPlusBuy()
		{
			if (this.m_dEventTimeCheck != null && !this.m_dEventTimeCheck(true))
			{
				return;
			}
			NKCEventPassDataManager eventPassDataManager = NKCScenManager.GetScenManager().GetEventPassDataManager();
			if (eventPassDataManager == null)
			{
				return;
			}
			NKMEventPassTemplet nkmeventPassTemplet = NKMTempletContainer<NKMEventPassTemplet>.Find(eventPassDataManager.EventPassId);
			if (nkmeventPassTemplet == null)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(NKCUtilString.GET_STRING_PURCHASE_POPUP_DESC, NKCStringTable.GetString(nkmeventPassTemplet.CorePassPlusPrdouct.StrId, false));
			stringBuilder.Append("\n");
			stringBuilder.AppendFormat("<size={0}>{1}</size>", this.m_iRefundImpossibleMsgSize, NKCUtilString.GET_STRING_PURCHASE_REFUND_IMPOSSIBLE);
			if (NKCUIEventPass.Instance.IsExpOverflowed(nkmeventPassTemplet, nkmeventPassTemplet.CorePassPlusPrdouct.PassExp))
			{
				stringBuilder.Append("\n");
				string arg = ColorUtility.ToHtmlStringRGB(this.m_colExpOverflowMsg);
				stringBuilder.AppendFormat("<color=#{0}><size={1}>{2}</size></color>", arg, this.m_iExpWarningMsgSize, NKCUtilString.GET_STRING_EVENTPASS_CORE_PASS_PLUS_PURCHASE_EXP_LOSS);
			}
			int requiredCount;
			if (nkmeventPassTemplet.CorePassDiscountPercent > 0f)
			{
				requiredCount = nkmeventPassTemplet.GetCorePassPriceDiscounted(nkmeventPassTemplet.CorePassPlusPrdouct.PriceCount);
			}
			else
			{
				requiredCount = nkmeventPassTemplet.CorePassPlusPrdouct.PriceCount;
			}
			NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_NOTICE, stringBuilder.ToString(), nkmeventPassTemplet.CorePassPlusPrdouct.PriceId, requiredCount, delegate()
			{
				if (this.m_dEventTimeCheck != null && !this.m_dEventTimeCheck(true))
				{
					return;
				}
				NKCPacketSender.Send_NKMPacket_EVENT_PASS_PURCHASE_CORE_PASS_PLUS_REQ();
			}, null, false);
		}

		// Token: 0x06007384 RID: 29572 RVA: 0x00266EA3 File Offset: 0x002650A3
		private void OnClickClose()
		{
			base.Close();
		}

		// Token: 0x06007385 RID: 29573 RVA: 0x00266EAB File Offset: 0x002650AB
		private IEnumerator IClosePopup()
		{
			this.m_animator.SetTrigger("out");
			while (!this.m_animator.GetCurrentAnimatorStateInfo(0).IsName("BUY_OUTRO") || this.m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
			{
				yield return null;
			}
			base.Close();
			yield break;
		}

		// Token: 0x06007386 RID: 29574 RVA: 0x00266EBC File Offset: 0x002650BC
		private void OnDestroy()
		{
			this.m_corePassPlan = null;
			this.m_corePassPlanPlus = null;
			this.m_eventPassEquip = null;
			this.m_characterView = null;
			this.m_objEquipRoot = null;
			this.m_objBuyNotice = null;
			this.m_animator = null;
			this.m_csbtnClose = null;
			NKCUICharacterView characterView = this.m_characterView;
			if (characterView != null)
			{
				characterView.CleanUp();
			}
			this.m_characterView = null;
		}

		// Token: 0x04005F78 RID: 24440
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_EVENT_PASS";

		// Token: 0x04005F79 RID: 24441
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_EVENT_PASS_BUY";

		// Token: 0x04005F7A RID: 24442
		private static NKCPopupEventPassPurchase m_Instance;

		// Token: 0x04005F7B RID: 24443
		public NKCUIComEventPassBuySlot m_corePassPlan;

		// Token: 0x04005F7C RID: 24444
		public NKCUIComEventPassBuySlot m_corePassPlanPlus;

		// Token: 0x04005F7D RID: 24445
		public NKCUIComEventPassEquip m_eventPassEquip;

		// Token: 0x04005F7E RID: 24446
		public NKCUICharacterView m_characterView;

		// Token: 0x04005F7F RID: 24447
		public GameObject m_objEquipRoot;

		// Token: 0x04005F80 RID: 24448
		public GameObject m_objBuyNotice;

		// Token: 0x04005F81 RID: 24449
		public Animator m_animator;

		// Token: 0x04005F82 RID: 24450
		public int m_iRefundImpossibleMsgSize;

		// Token: 0x04005F83 RID: 24451
		public int m_iExpWarningMsgSize;

		// Token: 0x04005F84 RID: 24452
		public Color m_colExpOverflowMsg;

		// Token: 0x04005F85 RID: 24453
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x04005F86 RID: 24454
		private NKCPopupEventPassPurchase.EventTimeCheck m_dEventTimeCheck;

		// Token: 0x02001790 RID: 6032
		// (Invoke) Token: 0x0600B3A4 RID: 45988
		public delegate bool EventTimeCheck(bool alarm);
	}
}
