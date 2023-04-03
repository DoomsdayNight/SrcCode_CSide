using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x0200098F RID: 2447
	public class NKCUIFactoryShortCutMenu : MonoBehaviour
	{
		// Token: 0x06006520 RID: 25888 RVA: 0x002027BC File Offset: 0x002009BC
		private void Init()
		{
			if (this.m_tglCraft != null)
			{
				this.m_tglCraft.m_bGetCallbackWhileLocked = true;
				this.m_tglCraft.OnValueChanged.RemoveAllListeners();
				this.m_tglCraft.OnValueChanged.AddListener(new UnityAction<bool>(this.OnCraft));
			}
			if (this.m_tglEnchant != null)
			{
				this.m_tglEnchant.m_bGetCallbackWhileLocked = true;
				this.m_tglEnchant.SetbReverseSeqCallbackCall(true);
				this.m_tglEnchant.OnValueChanged.RemoveAllListeners();
				this.m_tglEnchant.OnValueChanged.AddListener(new UnityAction<bool>(this.OnEnchant));
			}
			else
			{
				Debug.LogError("m_tglEnchant Null!");
			}
			if (this.m_tglTuning != null)
			{
				this.m_tglTuning.m_bGetCallbackWhileLocked = true;
				this.m_tglTuning.SetbReverseSeqCallbackCall(true);
				this.m_tglTuning.OnValueChanged.RemoveAllListeners();
				this.m_tglTuning.OnValueChanged.AddListener(new UnityAction<bool>(this.OnTuning));
			}
			else
			{
				Debug.LogError("m_tglTuning Null!");
			}
			if (this.m_tglHiddenOption != null)
			{
				bool flag = NKMOpenTagManager.IsOpened("EQUIP_POTENTIAL");
				NKCUtil.SetGameobjectActive(this.m_tglHiddenOption, flag);
				if (flag)
				{
					this.m_tglHiddenOption.m_bGetCallbackWhileLocked = true;
					this.m_tglHiddenOption.SetbReverseSeqCallbackCall(true);
					this.m_tglHiddenOption.OnValueChanged.RemoveAllListeners();
					this.m_tglHiddenOption.OnValueChanged.AddListener(new UnityAction<bool>(this.OnHiddenOption));
				}
			}
			else
			{
				Debug.LogError("m_tglHiddenOption Null!");
			}
			this.bInitComplete = true;
		}

		// Token: 0x06006521 RID: 25889 RVA: 0x0020294A File Offset: 0x00200B4A
		public void SetData(NKCUIForge.NKC_FORGE_TAB tabType)
		{
			switch (tabType)
			{
			case NKCUIForge.NKC_FORGE_TAB.NFT_ENCHANT:
				this.SetData(ContentsType.FACTORY_ENCHANT);
				return;
			case NKCUIForge.NKC_FORGE_TAB.NFT_TUNING:
				this.SetData(ContentsType.FACTORY_TUNING);
				return;
			case NKCUIForge.NKC_FORGE_TAB.NFT_HIDDEN_OPTION:
				this.SetData(ContentsType.FACTORY_HIDDEN_OPTION);
				return;
			default:
				return;
			}
		}

		// Token: 0x06006522 RID: 25890 RVA: 0x0020297C File Offset: 0x00200B7C
		public void SetData(ContentsType selectedType)
		{
			if (!this.bInitComplete)
			{
				this.Init();
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_CRAFT, 0, 0))
			{
				NKCUIComToggle tglCraft = this.m_tglCraft;
				if (tglCraft != null)
				{
					tglCraft.Lock(false);
				}
			}
			else
			{
				NKCUIComToggle tglCraft2 = this.m_tglCraft;
				if (tglCraft2 != null)
				{
					tglCraft2.UnLock(false);
				}
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_ENCHANT, 0, 0))
			{
				this.m_tglEnchant.Lock(false);
			}
			else
			{
				this.m_tglEnchant.UnLock(false);
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_TUNING, 0, 0))
			{
				this.m_tglTuning.Lock(false);
			}
			else
			{
				this.m_tglTuning.UnLock(false);
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_HIDDEN_OPTION, 0, 0))
			{
				this.m_tglHiddenOption.Lock(false);
			}
			else
			{
				this.m_tglHiddenOption.UnLock(false);
			}
			switch (selectedType)
			{
			case ContentsType.FACTORY_CRAFT:
			{
				NKCUIComToggle tglCraft3 = this.m_tglCraft;
				if (tglCraft3 != null)
				{
					tglCraft3.Select(true, true, false);
				}
				break;
			}
			case ContentsType.FACTORY_ENCHANT:
				this.m_tglEnchant.Select(true, true, false);
				break;
			case ContentsType.FACTORY_TUNING:
				this.m_tglTuning.Select(true, true, false);
				break;
			case ContentsType.FACTORY_HIDDEN_OPTION:
				this.m_tglHiddenOption.Select(true, true, false);
				break;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objCraftEvent, NKCCompanyBuff.NeedShowEventMark(nkmuserData.m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_FACTORY_CRAFT_CREDIT_DISCOUNT));
			NKCUtil.SetGameobjectActive(this.m_objEnchantEvent, NKCCompanyBuff.NeedShowEventMark(nkmuserData.m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_FACTORY_ENCHANT_TUNING_CREDIT_DISCOUNT));
			NKCUtil.SetGameobjectActive(this.m_objTuningEvent, NKCCompanyBuff.NeedShowEventMark(nkmuserData.m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_FACTORY_ENCHANT_TUNING_CREDIT_DISCOUNT));
		}

		// Token: 0x06006523 RID: 25891 RVA: 0x00202AF0 File Offset: 0x00200CF0
		private void OnCraft(bool bSet)
		{
			if (null != this.m_tglCraft && this.m_tglCraft.m_bLock)
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.FACTORY_CRAFT, 0);
				return;
			}
			if (bSet && !NKCUIForgeCraft.IsInstanceOpen)
			{
				if (NKCUIForge.IsInstanceOpen)
				{
					UnityAction craftCallBackFunc = this.CraftCallBackFunc;
					if (craftCallBackFunc == null)
					{
						return;
					}
					craftCallBackFunc();
					return;
				}
				else
				{
					this.MoveToCraft();
				}
			}
		}

		// Token: 0x06006524 RID: 25892 RVA: 0x00202B4C File Offset: 0x00200D4C
		private void OnEnchant(bool bSet)
		{
			if (this.m_tglEnchant.m_bLock)
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.FACTORY_ENCHANT, 0);
				return;
			}
			if (bSet)
			{
				if (!NKCUIForge.Instance.IsHiddenOptionEffectStopped())
				{
					this.m_tglHiddenOption.Select(true, true, false);
					return;
				}
				if (!NKCUIForge.IsInstanceOpen)
				{
					NKCUIManager.OnBackButton();
					NKCUIForge.Instance.Open(NKCUIForge.NKC_FORGE_TAB.NFT_ENCHANT, 0L, null);
					return;
				}
				NKCUIForge.Instance.SetTab(NKCUIForge.NKC_FORGE_TAB.NFT_ENCHANT);
			}
		}

		// Token: 0x06006525 RID: 25893 RVA: 0x00202BB8 File Offset: 0x00200DB8
		private void OnTuning(bool bSet)
		{
			if (this.m_tglTuning.m_bLock)
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.FACTORY_TUNING, 0);
				return;
			}
			if (bSet)
			{
				if (!NKCUIForge.Instance.IsHiddenOptionEffectStopped())
				{
					this.m_tglHiddenOption.Select(true, true, false);
					return;
				}
				if (!NKCUIForge.IsInstanceOpen)
				{
					NKCUIManager.OnBackButton();
					NKCUIForge.Instance.Open(NKCUIForge.NKC_FORGE_TAB.NFT_TUNING, 0L, null);
					return;
				}
				NKCUIForge.Instance.SetTab(NKCUIForge.NKC_FORGE_TAB.NFT_TUNING);
			}
		}

		// Token: 0x06006526 RID: 25894 RVA: 0x00202C24 File Offset: 0x00200E24
		private void OnHiddenOption(bool bSet)
		{
			if (this.m_tglHiddenOption.m_bLock)
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.FACTORY_HIDDEN_OPTION, 0);
				return;
			}
			if (bSet)
			{
				if (!NKCUIForge.Instance.IsHiddenOptionEffectStopped())
				{
					return;
				}
				if (!NKCUIForge.IsInstanceOpen)
				{
					NKCUIManager.OnBackButton();
					NKCUIForge.Instance.Open(NKCUIForge.NKC_FORGE_TAB.NFT_HIDDEN_OPTION, 0L, null);
					return;
				}
				NKCUIForge.Instance.SetTab(NKCUIForge.NKC_FORGE_TAB.NFT_HIDDEN_OPTION);
			}
		}

		// Token: 0x06006527 RID: 25895 RVA: 0x00202C7E File Offset: 0x00200E7E
		public void OnConfirmBeforeChangeToCraft(UnityAction action)
		{
			this.CraftCallBackFunc = action;
		}

		// Token: 0x06006528 RID: 25896 RVA: 0x00202C87 File Offset: 0x00200E87
		public void MoveToCraft()
		{
			NKCUIManager.OnBackButton();
			NKCUIForgeCraft.Instance.Open();
		}

		// Token: 0x040050C5 RID: 20677
		public NKCUIComToggle m_tglCraft;

		// Token: 0x040050C6 RID: 20678
		public NKCUIComToggle m_tglEnchant;

		// Token: 0x040050C7 RID: 20679
		public NKCUIComToggle m_tglTuning;

		// Token: 0x040050C8 RID: 20680
		public NKCUIComToggle m_tglHiddenOption;

		// Token: 0x040050C9 RID: 20681
		public GameObject m_objCraftEvent;

		// Token: 0x040050CA RID: 20682
		public GameObject m_objEnchantEvent;

		// Token: 0x040050CB RID: 20683
		public GameObject m_objTuningEvent;

		// Token: 0x040050CC RID: 20684
		public GameObject m_objHiddinOptionEvent;

		// Token: 0x040050CD RID: 20685
		private bool bInitComplete;

		// Token: 0x040050CE RID: 20686
		private UnityAction CraftCallBackFunc;
	}
}
