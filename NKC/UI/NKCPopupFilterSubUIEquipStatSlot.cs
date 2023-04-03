using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A51 RID: 2641
	public class NKCPopupFilterSubUIEquipStatSlot : MonoBehaviour
	{
		// Token: 0x060073ED RID: 29677 RVA: 0x00268C9D File Offset: 0x00266E9D
		public NKCUIComStateButton GetButton()
		{
			return this.m_btn;
		}

		// Token: 0x060073EE RID: 29678 RVA: 0x00268CA5 File Offset: 0x00266EA5
		public NKM_STAT_TYPE GetStatType()
		{
			return this.m_StatType;
		}

		// Token: 0x060073EF RID: 29679 RVA: 0x00268CAD File Offset: 0x00266EAD
		public int GetSetOptionID()
		{
			return this.m_SetOptionID;
		}

		// Token: 0x17001357 RID: 4951
		// (get) Token: 0x060073F0 RID: 29680 RVA: 0x00268CB5 File Offset: 0x00266EB5
		public bool IsSetOptionSlot
		{
			get
			{
				return this.m_bIsSetOptionSlot;
			}
		}

		// Token: 0x060073F1 RID: 29681 RVA: 0x00268CC0 File Offset: 0x00266EC0
		public void SetData(NKM_STAT_TYPE statType, bool bSelected = false)
		{
			this.m_btn.Select(bSelected, true, true);
			if (statType != NKM_STAT_TYPE.NST_RANDOM)
			{
				NKCUtil.SetLabelText(this.m_lbNameOff, NKCUtilString.GetStatShortName(statType));
				NKCUtil.SetLabelText(this.m_lbNameOn, NKCUtilString.GetStatShortName(statType));
				NKCUtil.SetLabelText(this.m_lbNameLock, NKCUtilString.GetStatShortName(statType));
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbNameOff, NKCUtilString.GET_STRING_FILTER_EQUIP_OPTION_SEARCH);
				NKCUtil.SetLabelText(this.m_lbNameOn, NKCUtilString.GET_STRING_FILTER_EQUIP_OPTION_SEARCH);
				NKCUtil.SetLabelText(this.m_lbNameLock, NKCUtilString.GET_STRING_FILTER_EQUIP_OPTION_SEARCH);
			}
			NKCUtil.SetGameobjectActive(this.m_imgIconOff, false);
			NKCUtil.SetGameobjectActive(this.m_imgIconOn, false);
			NKCUtil.SetGameobjectActive(this.m_imgIconLock, false);
			this.m_StatType = statType;
			this.m_SetOptionID = 0;
			this.m_bIsSetOptionSlot = false;
		}

		// Token: 0x060073F2 RID: 29682 RVA: 0x00268D80 File Offset: 0x00266F80
		public void SetData(int setOptionID, bool bSelected = false)
		{
			NKMItemEquipSetOptionTemplet equipSetOptionTemplet = NKMItemManager.GetEquipSetOptionTemplet(setOptionID);
			this.SetData(equipSetOptionTemplet, bSelected);
		}

		// Token: 0x060073F3 RID: 29683 RVA: 0x00268D9C File Offset: 0x00266F9C
		public void SetData(NKMItemEquipSetOptionTemplet setOptionTemplet, bool bSelected = false)
		{
			this.m_btn.Select(bSelected, true, true);
			if (setOptionTemplet != null)
			{
				NKCUtil.SetGameobjectActive(this.m_imgIconOff, true);
				NKCUtil.SetGameobjectActive(this.m_imgIconOn, true);
				NKCUtil.SetGameobjectActive(this.m_imgIconLock, true);
				NKCUtil.SetLabelText(this.m_lbNameOff, NKCStringTable.GetString(setOptionTemplet.m_EquipSetName, false));
				NKCUtil.SetLabelText(this.m_lbNameOn, NKCStringTable.GetString(setOptionTemplet.m_EquipSetName, false));
				NKCUtil.SetLabelText(this.m_lbNameLock, NKCStringTable.GetString(setOptionTemplet.m_EquipSetName, false));
				NKCUtil.SetImageSprite(this.m_imgIconOff, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_COMMON_EQUIP_SET_ICON", setOptionTemplet.m_EquipSetIcon, false), false);
				NKCUtil.SetImageSprite(this.m_imgIconOn, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_COMMON_EQUIP_SET_ICON", setOptionTemplet.m_EquipSetIcon, false), false);
				NKCUtil.SetImageSprite(this.m_imgIconLock, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_COMMON_EQUIP_SET_ICON", setOptionTemplet.m_EquipSetIcon, false), false);
				this.m_SetOptionID = setOptionTemplet.m_EquipSetID;
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_imgIconOff, false);
				NKCUtil.SetGameobjectActive(this.m_imgIconOn, false);
				NKCUtil.SetGameobjectActive(this.m_imgIconLock, false);
				NKCUtil.SetLabelText(this.m_lbNameOff, NKCUtilString.GET_STRING_SORT_SETOPTION);
				NKCUtil.SetLabelText(this.m_lbNameOn, NKCUtilString.GET_STRING_SORT_SETOPTION);
				NKCUtil.SetLabelText(this.m_lbNameLock, NKCUtilString.GET_STRING_SORT_SETOPTION);
				this.m_SetOptionID = 0;
			}
			this.m_StatType = NKM_STAT_TYPE.NST_RANDOM;
			this.m_bIsSetOptionSlot = true;
		}

		// Token: 0x04005FE0 RID: 24544
		public NKCUIComStateButton m_btn;

		// Token: 0x04005FE1 RID: 24545
		public Image m_imgIconOff;

		// Token: 0x04005FE2 RID: 24546
		public Text m_lbNameOff;

		// Token: 0x04005FE3 RID: 24547
		public Image m_imgIconOn;

		// Token: 0x04005FE4 RID: 24548
		public Text m_lbNameOn;

		// Token: 0x04005FE5 RID: 24549
		public Image m_imgIconLock;

		// Token: 0x04005FE6 RID: 24550
		public Text m_lbNameLock;

		// Token: 0x04005FE7 RID: 24551
		private NKM_STAT_TYPE m_StatType = NKM_STAT_TYPE.NST_RANDOM;

		// Token: 0x04005FE8 RID: 24552
		private int m_SetOptionID;

		// Token: 0x04005FE9 RID: 24553
		private bool m_bIsSetOptionSlot;
	}
}
