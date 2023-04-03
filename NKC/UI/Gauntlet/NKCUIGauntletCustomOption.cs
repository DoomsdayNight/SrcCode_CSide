using System;
using ClientPacket.Pvp;
using NKM;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B6A RID: 2922
	public class NKCUIGauntletCustomOption : MonoBehaviour
	{
		// Token: 0x06008597 RID: 34199 RVA: 0x002D35F4 File Offset: 0x002D17F4
		public void Init()
		{
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglApplyEquipStat, new UnityAction<bool>(this.OnToggleApplyEquipSet));
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglApplyAllUnitMaxLevel, new UnityAction<bool>(this.OnToggleAllUnitMaxLevel));
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglApplyBanUp, new UnityAction<bool>(this.OnToggleBanUp));
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglDraftBanMode, new UnityAction<bool>(this.OnToggleDraftBanMode));
		}

		// Token: 0x06008598 RID: 34200 RVA: 0x002D3660 File Offset: 0x002D1860
		public void SetOption(NKMPrivateGameConfig privateGameConfig)
		{
			this.m_tglApplyEquipStat.Select(!privateGameConfig.applyEquipStat, true, false);
			this.m_tglApplyAllUnitMaxLevel.Select(privateGameConfig.applyAllUnitMaxLevel, true, false);
			this.m_tglApplyBanUp.Select(privateGameConfig.applyBanUpSystem, true, false);
			NKCUIComToggle tglDraftBanMode = this.m_tglDraftBanMode;
			if (tglDraftBanMode != null)
			{
				tglDraftBanMode.Select(privateGameConfig.draftBanMode, true, false);
			}
			NKCUtil.SetGameobjectActive(this.m_objApplyEquipStat, !privateGameConfig.applyEquipStat);
			NKCUtil.SetGameobjectActive(this.m_objApplyAllUnitMaxLevel, privateGameConfig.applyAllUnitMaxLevel);
			NKCUtil.SetGameobjectActive(this.m_objApplyBanUp, privateGameConfig.applyBanUpSystem);
			NKCUtil.SetGameobjectActive(this.m_objDraftBanMode, privateGameConfig.draftBanMode);
		}

		// Token: 0x06008599 RID: 34201 RVA: 0x002D370D File Offset: 0x002D190D
		public void SetActive(bool value)
		{
			base.gameObject.SetActive(value);
		}

		// Token: 0x0600859A RID: 34202 RVA: 0x002D371B File Offset: 0x002D191B
		private void OnToggleApplyEquipSet(bool value)
		{
			if (this.m_ProhibitToggle)
			{
				this.m_tglApplyEquipStat.Select(!value, true, false);
				return;
			}
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM))
			{
				NKCPrivatePVPRoomMgr.SetApplyEquipSet(!value);
				return;
			}
			NKCPrivatePVPMgr.SetApplyEquipSet(!value);
		}

		// Token: 0x0600859B RID: 34203 RVA: 0x002D3754 File Offset: 0x002D1954
		private void OnToggleAllUnitMaxLevel(bool value)
		{
			if (this.m_ProhibitToggle)
			{
				this.m_tglApplyAllUnitMaxLevel.Select(!value, true, false);
				return;
			}
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM))
			{
				NKCPrivatePVPRoomMgr.SetApplyAllUnitMaxLevel(value);
				return;
			}
			NKCPrivatePVPMgr.SetApplyAllUnitMaxLevel(value);
		}

		// Token: 0x0600859C RID: 34204 RVA: 0x002D3788 File Offset: 0x002D1988
		private void OnToggleBanUp(bool value)
		{
			if (value)
			{
				this.m_tglDraftBanMode.Select(false, true, false);
				if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM))
				{
					if (NKCPrivatePVPRoomMgr.PrivateGameConfig.draftBanMode)
					{
						NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCStringTable.GetString("SI_PF_PVP_FRIENDLY_OPTION_POPUP_BOX_DESC", false), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
						NKCPrivatePVPRoomMgr.SetDraftBanMode(false);
					}
				}
				else if (NKCPrivatePVPMgr.PrivateGameConfig.draftBanMode)
				{
					NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCStringTable.GetString("SI_PF_PVP_FRIENDLY_OPTION_POPUP_BOX_DESC", false), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
					NKCPrivatePVPMgr.SetDraftBanMode(false);
				}
			}
			if (this.m_ProhibitToggle)
			{
				this.m_tglApplyBanUp.Select(!value, true, false);
				return;
			}
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM))
			{
				NKCPrivatePVPRoomMgr.SetApplyBanUp(value);
				return;
			}
			NKCPrivatePVPMgr.SetApplyBanUp(value);
		}

		// Token: 0x0600859D RID: 34205 RVA: 0x002D3850 File Offset: 0x002D1A50
		private void OnToggleDraftBanMode(bool value)
		{
			if (value)
			{
				this.m_tglApplyBanUp.Select(false, true, false);
				if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM))
				{
					if (NKCPrivatePVPRoomMgr.PrivateGameConfig.applyBanUpSystem)
					{
						NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCStringTable.GetString("SI_PF_PVP_FRIENDLY_OPTION_POPUP_BOX_DESC", false), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
						NKCPrivatePVPRoomMgr.SetApplyBanUp(false);
					}
				}
				else if (NKCPrivatePVPMgr.PrivateGameConfig.applyBanUpSystem)
				{
					NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCStringTable.GetString("SI_PF_PVP_FRIENDLY_OPTION_POPUP_BOX_DESC", false), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
					NKCPrivatePVPMgr.SetApplyBanUp(false);
				}
			}
			if (this.m_ProhibitToggle)
			{
				NKCUIComToggle tglDraftBanMode = this.m_tglDraftBanMode;
				if (tglDraftBanMode == null)
				{
					return;
				}
				tglDraftBanMode.Select(!value, true, false);
				return;
			}
			else
			{
				if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM))
				{
					NKCPrivatePVPRoomMgr.SetDraftBanMode(value);
					return;
				}
				NKCPrivatePVPMgr.SetDraftBanMode(value);
				return;
			}
		}

		// Token: 0x0400721C RID: 29212
		public GameObject m_objApplyEquipStat;

		// Token: 0x0400721D RID: 29213
		public GameObject m_objApplyAllUnitMaxLevel;

		// Token: 0x0400721E RID: 29214
		public GameObject m_objApplyBanUp;

		// Token: 0x0400721F RID: 29215
		public GameObject m_objDraftBanMode;

		// Token: 0x04007220 RID: 29216
		public NKCUIComToggle m_tglApplyEquipStat;

		// Token: 0x04007221 RID: 29217
		public NKCUIComToggle m_tglApplyAllUnitMaxLevel;

		// Token: 0x04007222 RID: 29218
		public NKCUIComToggle m_tglApplyBanUp;

		// Token: 0x04007223 RID: 29219
		public NKCUIComToggle m_tglDraftBanMode;

		// Token: 0x04007224 RID: 29220
		public bool m_ProhibitToggle;
	}
}
