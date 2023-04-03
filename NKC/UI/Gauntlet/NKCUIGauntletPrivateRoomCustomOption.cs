using System;
using ClientPacket.Pvp;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B81 RID: 2945
	public class NKCUIGauntletPrivateRoomCustomOption : MonoBehaviour
	{
		// Token: 0x060087CC RID: 34764 RVA: 0x002DF108 File Offset: 0x002DD308
		public void Init()
		{
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglApplyEquipStat, new UnityAction<bool>(this.OnToggleApplyEquipSet));
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglApplyAllUnitMaxLevel, new UnityAction<bool>(this.OnToggleAllUnitMaxLevel));
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglApplyBanUp, new UnityAction<bool>(this.OnToggleBanUp));
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglDraftBanMode, new UnityAction<bool>(this.OnToggleDraftBanMode));
		}

		// Token: 0x060087CD RID: 34765 RVA: 0x002DF174 File Offset: 0x002DD374
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
			NKCUtil.SetGameobjectActive(this.m_objApplyEquipStat, true);
			NKCUtil.SetGameobjectActive(this.m_objApplyAllUnitMaxLevel, true);
			NKCUtil.SetGameobjectActive(this.m_objApplyBanUp, true);
			NKCUtil.SetGameobjectActive(this.m_objDraftBanMode, true);
		}

		// Token: 0x060087CE RID: 34766 RVA: 0x002DF20A File Offset: 0x002DD40A
		public void SetActive(bool value)
		{
			base.gameObject.SetActive(value);
		}

		// Token: 0x060087CF RID: 34767 RVA: 0x002DF218 File Offset: 0x002DD418
		private void OnToggleApplyEquipSet(bool value)
		{
			if (this.m_ProhibitToggle)
			{
				this.m_tglApplyEquipStat.Select(!value, true, false);
				return;
			}
		}

		// Token: 0x060087D0 RID: 34768 RVA: 0x002DF235 File Offset: 0x002DD435
		private void OnToggleAllUnitMaxLevel(bool value)
		{
			if (this.m_ProhibitToggle)
			{
				this.m_tglApplyAllUnitMaxLevel.Select(!value, true, false);
				return;
			}
		}

		// Token: 0x060087D1 RID: 34769 RVA: 0x002DF252 File Offset: 0x002DD452
		private void OnToggleBanUp(bool value)
		{
			if (value)
			{
				this.m_tglApplyBanUp.Select(!value, true, false);
				return;
			}
			if (this.m_ProhibitToggle)
			{
				this.m_tglApplyBanUp.Select(!value, true, false);
				return;
			}
		}

		// Token: 0x060087D2 RID: 34770 RVA: 0x002DF285 File Offset: 0x002DD485
		private void OnToggleDraftBanMode(bool value)
		{
			if (value)
			{
				this.m_tglApplyBanUp.Select(false, true, false);
			}
			if (!this.m_ProhibitToggle)
			{
				return;
			}
			NKCUIComToggle tglDraftBanMode = this.m_tglDraftBanMode;
			if (tglDraftBanMode == null)
			{
				return;
			}
			tglDraftBanMode.Select(!value, true, false);
		}

		// Token: 0x04007425 RID: 29733
		public GameObject m_objApplyEquipStat;

		// Token: 0x04007426 RID: 29734
		public GameObject m_objApplyAllUnitMaxLevel;

		// Token: 0x04007427 RID: 29735
		public GameObject m_objApplyBanUp;

		// Token: 0x04007428 RID: 29736
		public GameObject m_objDraftBanMode;

		// Token: 0x04007429 RID: 29737
		public NKCUIComToggle m_tglApplyEquipStat;

		// Token: 0x0400742A RID: 29738
		public NKCUIComToggle m_tglApplyAllUnitMaxLevel;

		// Token: 0x0400742B RID: 29739
		public NKCUIComToggle m_tglApplyBanUp;

		// Token: 0x0400742C RID: 29740
		public NKCUIComToggle m_tglDraftBanMode;

		// Token: 0x0400742D RID: 29741
		public bool m_ProhibitToggle;
	}
}
