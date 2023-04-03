using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000626 RID: 1574
	public class NKCGameHudComboSlot : MonoBehaviour
	{
		// Token: 0x06003098 RID: 12440 RVA: 0x000F0870 File Offset: 0x000EEA70
		public void SetUI(NKMTacticalCombo cNKMTacticalCombo, bool bArrow = false)
		{
			if (cNKMTacticalCombo == null)
			{
				return;
			}
			bool flag = cNKMTacticalCombo.m_NKM_TACTICAL_COMBO_TYPE == NKM_TACTICAL_COMBO_TYPE.NTCBT_UNIT_ROLE_TYPE || cNKMTacticalCombo.m_NKM_TACTICAL_COMBO_TYPE == NKM_TACTICAL_COMBO_TYPE.NTCBT_UNIT_STYLE_TYPE || cNKMTacticalCombo.m_NKM_TACTICAL_COMBO_TYPE == NKM_TACTICAL_COMBO_TYPE.NTCBT_UNIT_STR_ID;
			NKCUtil.SetGameobjectActive(this.m_img, flag);
			NKCUtil.SetGameobjectActive(this.m_imgActive, flag);
			NKCUtil.SetGameobjectActive(this.m_lbRespawnCost, !flag);
			NKCUtil.SetGameobjectActive(this.m_lbRespawnCostActive, !flag);
			if (cNKMTacticalCombo.m_NKM_TACTICAL_COMBO_TYPE == NKM_TACTICAL_COMBO_TYPE.NTCBT_UNIT_ROLE_TYPE)
			{
				if (this.m_img != null)
				{
					this.m_img.sprite = NKCResourceUtility.GetOrLoadUnitRoleIcon(cNKMTacticalCombo.m_NKM_UNIT_ROLE_TYPE, true, false);
				}
			}
			else if (cNKMTacticalCombo.m_NKM_TACTICAL_COMBO_TYPE == NKM_TACTICAL_COMBO_TYPE.NTCBT_UNIT_STYLE_TYPE)
			{
				if (this.m_img != null)
				{
					this.m_img.sprite = NKCResourceUtility.GetOrLoadUnitStyleIcon(cNKMTacticalCombo.m_NKM_UNIT_STYLE_TYPE, true);
				}
			}
			else if (cNKMTacticalCombo.m_NKM_TACTICAL_COMBO_TYPE == NKM_TACTICAL_COMBO_TYPE.NTCBT_UNIT_STR_ID)
			{
				if (this.m_img != null)
				{
					int skinID = 0;
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(cNKMTacticalCombo.m_Value);
					this.m_img.sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, unitTempletBase, skinID);
				}
			}
			else if (cNKMTacticalCombo.m_NKM_TACTICAL_COMBO_TYPE == NKM_TACTICAL_COMBO_TYPE.NTCBT_RESPAWN_COST)
			{
				NKCUtil.SetLabelText(this.m_lbRespawnCost, cNKMTacticalCombo.m_ValueInt.ToString());
			}
			NKCUtil.SetGameobjectActive(this.m_objArrow, bArrow);
			if (this.m_imgActive != null && this.m_img != null)
			{
				this.m_imgActive.sprite = this.m_img.sprite;
			}
			if (this.m_lbRespawnCost != null)
			{
				NKCUtil.SetLabelText(this.m_lbRespawnCostActive, this.m_lbRespawnCost.text);
			}
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x000F09F8 File Offset: 0x000EEBF8
		public void SetComboSucess(bool bSuccess)
		{
			NKCUtil.SetGameobjectActive(this.m_objActive, bSuccess);
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x000F0A06 File Offset: 0x000EEC06
		private void OnDestroy()
		{
			if (this.m_img != null)
			{
				this.m_img.sprite = null;
			}
			if (this.m_imgActive != null)
			{
				this.m_imgActive.sprite = null;
			}
		}

		// Token: 0x0400300E RID: 12302
		public Image m_img;

		// Token: 0x0400300F RID: 12303
		public Text m_lbRespawnCost;

		// Token: 0x04003010 RID: 12304
		public GameObject m_objArrow;

		// Token: 0x04003011 RID: 12305
		public GameObject m_objActive;

		// Token: 0x04003012 RID: 12306
		public Image m_imgActive;

		// Token: 0x04003013 RID: 12307
		public Text m_lbRespawnCostActive;
	}
}
