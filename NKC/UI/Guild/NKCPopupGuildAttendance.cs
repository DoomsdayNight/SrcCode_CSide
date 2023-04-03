using System;
using System.Collections.Generic;
using NKM.Contract2;
using NKM.Guild;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B3C RID: 2876
	public class NKCPopupGuildAttendance : NKCUIBase
	{
		// Token: 0x17001557 RID: 5463
		// (get) Token: 0x060082F6 RID: 33526 RVA: 0x002C2C0C File Offset: 0x002C0E0C
		public static NKCPopupGuildAttendance Instance
		{
			get
			{
				if (NKCPopupGuildAttendance.m_Instance == null)
				{
					NKCPopupGuildAttendance.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupGuildAttendance>("AB_UI_NKM_UI_CONSORTIUM", "NKM_UI_CONSORTIUM_POPUP_ATTENDANCE", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupGuildAttendance.CleanupInstance)).GetInstance<NKCPopupGuildAttendance>();
					if (NKCPopupGuildAttendance.m_Instance != null)
					{
						NKCPopupGuildAttendance.m_Instance.InitUI();
					}
				}
				return NKCPopupGuildAttendance.m_Instance;
			}
		}

		// Token: 0x060082F7 RID: 33527 RVA: 0x002C2C6D File Offset: 0x002C0E6D
		private static void CleanupInstance()
		{
			NKCPopupGuildAttendance.m_Instance = null;
		}

		// Token: 0x17001558 RID: 5464
		// (get) Token: 0x060082F8 RID: 33528 RVA: 0x002C2C75 File Offset: 0x002C0E75
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupGuildAttendance.m_Instance != null && NKCPopupGuildAttendance.m_Instance.IsOpen;
			}
		}

		// Token: 0x060082F9 RID: 33529 RVA: 0x002C2C90 File Offset: 0x002C0E90
		private void OnDestroy()
		{
			NKCPopupGuildAttendance.m_Instance = null;
		}

		// Token: 0x17001559 RID: 5465
		// (get) Token: 0x060082FA RID: 33530 RVA: 0x002C2C98 File Offset: 0x002C0E98
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700155A RID: 5466
		// (get) Token: 0x060082FB RID: 33531 RVA: 0x002C2C9B File Offset: 0x002C0E9B
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x060082FC RID: 33532 RVA: 0x002C2CA4 File Offset: 0x002C0EA4
		public override void CloseInternal()
		{
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				UnityEngine.Object.Destroy(this.m_lstSlot[i].gameObject);
			}
			this.m_lstSlot.Clear();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060082FD RID: 33533 RVA: 0x002C2CF4 File Offset: 0x002C0EF4
		private void InitUI()
		{
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_btnOK.PointerClick.RemoveAllListeners();
			this.m_btnOK.PointerClick.AddListener(new UnityAction(base.Close));
			NKCUtil.SetHotkey(this.m_btnOK, HotkeyEventType.Confirm, null, false);
		}

		// Token: 0x060082FE RID: 33534 RVA: 0x002C2D68 File Offset: 0x002C0F68
		public void Open(int lastAttendanceCount)
		{
			NKCUtil.SetGameobjectActive(this.m_objInvalid, NKCGuildManager.IsFirstDay());
			NKCUtil.SetLabelText(this.m_lbLastAttendanceCount, lastAttendanceCount.ToString());
			this.m_lstSlot.Clear();
			for (int i = 0; i < GuildAttendanceTemplet.Instance.BasicRewards.Count; i++)
			{
				RewardUnit reward = GuildAttendanceTemplet.Instance.BasicRewards[i];
				NKCPopupGuildAttendanceSlot nkcpopupGuildAttendanceSlot = UnityEngine.Object.Instantiate<NKCPopupGuildAttendanceSlot>(this.m_pfbSlot, this.m_trSlotParent);
				nkcpopupGuildAttendanceSlot.SetData(0, reward, false);
				this.m_lstSlot.Add(nkcpopupGuildAttendanceSlot);
			}
			for (int j = 0; j < GuildAttendanceTemplet.Instance.AdditionalRewards.Count; j++)
			{
				GuildAttendanceTemplet.AdditionalReward additionalReward = GuildAttendanceTemplet.Instance.AdditionalRewards[j];
				NKCPopupGuildAttendanceSlot nkcpopupGuildAttendanceSlot2 = UnityEngine.Object.Instantiate<NKCPopupGuildAttendanceSlot>(this.m_pfbSlot, this.m_trSlotParent);
				nkcpopupGuildAttendanceSlot2.SetData(additionalReward.AttendanceCount, additionalReward.Item, additionalReward.AttendanceCount <= lastAttendanceCount);
				this.m_lstSlot.Add(nkcpopupGuildAttendanceSlot2);
			}
			base.UIOpened(true);
		}

		// Token: 0x04006F2E RID: 28462
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONSORTIUM";

		// Token: 0x04006F2F RID: 28463
		private const string UI_ASSET_NAME = "NKM_UI_CONSORTIUM_POPUP_ATTENDANCE";

		// Token: 0x04006F30 RID: 28464
		private static NKCPopupGuildAttendance m_Instance;

		// Token: 0x04006F31 RID: 28465
		public Text m_lbLastAttendanceCount;

		// Token: 0x04006F32 RID: 28466
		public NKCPopupGuildAttendanceSlot m_pfbSlot;

		// Token: 0x04006F33 RID: 28467
		public Transform m_trSlotParent;

		// Token: 0x04006F34 RID: 28468
		public NKCUIComStateButton m_btnClose;

		// Token: 0x04006F35 RID: 28469
		public NKCUIComStateButton m_btnOK;

		// Token: 0x04006F36 RID: 28470
		public GameObject m_objInvalid;

		// Token: 0x04006F37 RID: 28471
		private List<NKCPopupGuildAttendanceSlot> m_lstSlot = new List<NKCPopupGuildAttendanceSlot>();
	}
}
