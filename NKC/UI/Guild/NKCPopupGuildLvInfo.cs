using System;
using System.Collections.Generic;
using System.Linq;
using NKM.Guild;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B44 RID: 2884
	public class NKCPopupGuildLvInfo : NKCUIBase
	{
		// Token: 0x1700156F RID: 5487
		// (get) Token: 0x06008364 RID: 33636 RVA: 0x002C4A48 File Offset: 0x002C2C48
		public static NKCPopupGuildLvInfo Instance
		{
			get
			{
				if (NKCPopupGuildLvInfo.m_Instance == null)
				{
					NKCPopupGuildLvInfo.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupGuildLvInfo>("AB_UI_NKM_UI_CONSORTIUM", "NKM_UI_CONSORTIUM_POPUP_LV_INFO", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupGuildLvInfo.CleanupInstance)).GetInstance<NKCPopupGuildLvInfo>();
					if (NKCPopupGuildLvInfo.m_Instance != null)
					{
						NKCPopupGuildLvInfo.m_Instance.InitUI();
					}
				}
				return NKCPopupGuildLvInfo.m_Instance;
			}
		}

		// Token: 0x06008365 RID: 33637 RVA: 0x002C4AA9 File Offset: 0x002C2CA9
		private static void CleanupInstance()
		{
			NKCPopupGuildLvInfo.m_Instance = null;
		}

		// Token: 0x17001570 RID: 5488
		// (get) Token: 0x06008366 RID: 33638 RVA: 0x002C4AB1 File Offset: 0x002C2CB1
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupGuildLvInfo.m_Instance != null && NKCPopupGuildLvInfo.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008367 RID: 33639 RVA: 0x002C4ACC File Offset: 0x002C2CCC
		private void OnDestroy()
		{
			NKCPopupGuildLvInfo.m_Instance = null;
		}

		// Token: 0x17001571 RID: 5489
		// (get) Token: 0x06008368 RID: 33640 RVA: 0x002C4AD4 File Offset: 0x002C2CD4
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x17001572 RID: 5490
		// (get) Token: 0x06008369 RID: 33641 RVA: 0x002C4ADB File Offset: 0x002C2CDB
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x0600836A RID: 33642 RVA: 0x002C4AE0 File Offset: 0x002C2CE0
		public override void CloseInternal()
		{
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				UnityEngine.Object.Destroy(this.m_lstSlot[i].gameObject);
			}
			this.m_lstSlot.Clear();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0600836B RID: 33643 RVA: 0x002C4B30 File Offset: 0x002C2D30
		public void InitUI()
		{
			this.m_btnBG.PointerClick.RemoveAllListeners();
			this.m_btnBG.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_btnOK.PointerClick.RemoveAllListeners();
			this.m_btnOK.PointerClick.AddListener(new UnityAction(base.Close));
			NKCUtil.SetHotkey(this.m_btnOK, HotkeyEventType.Confirm, null, false);
		}

		// Token: 0x0600836C RID: 33644 RVA: 0x002C4BA4 File Offset: 0x002C2DA4
		public void Open()
		{
			List<GuildExpTemplet> list = NKMTempletContainer<GuildExpTemplet>.Values.ToList<GuildExpTemplet>();
			list.Sort(new Comparison<GuildExpTemplet>(this.Comparer));
			for (int i = 0; i < list.Count; i++)
			{
				NKCPopupGuildLvInfoSlot nkcpopupGuildLvInfoSlot = UnityEngine.Object.Instantiate<NKCPopupGuildLvInfoSlot>(this.m_pfbSlot, this.m_trSlotParent);
				nkcpopupGuildLvInfoSlot.SetData(list[i]);
				this.m_lstSlot.Add(nkcpopupGuildLvInfoSlot);
			}
			this.m_scList.normalizedPosition = new Vector2(0f, 1f);
			base.UIOpened(true);
		}

		// Token: 0x0600836D RID: 33645 RVA: 0x002C4C2C File Offset: 0x002C2E2C
		private int Comparer(GuildExpTemplet left, GuildExpTemplet right)
		{
			return left.GuildLevel.CompareTo(right.GuildLevel);
		}

		// Token: 0x04006F8C RID: 28556
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONSORTIUM";

		// Token: 0x04006F8D RID: 28557
		private const string UI_ASSET_NAME = "NKM_UI_CONSORTIUM_POPUP_LV_INFO";

		// Token: 0x04006F8E RID: 28558
		private static NKCPopupGuildLvInfo m_Instance;

		// Token: 0x04006F8F RID: 28559
		public NKCPopupGuildLvInfoSlot m_pfbSlot;

		// Token: 0x04006F90 RID: 28560
		public ScrollRect m_scList;

		// Token: 0x04006F91 RID: 28561
		public Transform m_trSlotParent;

		// Token: 0x04006F92 RID: 28562
		public NKCUIComStateButton m_btnBG;

		// Token: 0x04006F93 RID: 28563
		public NKCUIComStateButton m_btnOK;

		// Token: 0x04006F94 RID: 28564
		private List<NKCPopupGuildLvInfoSlot> m_lstSlot = new List<NKCPopupGuildLvInfoSlot>();
	}
}
