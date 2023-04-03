using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000802 RID: 2050
	public class NKCUIPopupRearmamentConfirmSlotInfo : MonoBehaviour
	{
		// Token: 0x06005129 RID: 20777 RVA: 0x00189DAC File Offset: 0x00187FAC
		public void SetData(int iUnitID)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(iUnitID);
			if (unitTempletBase == null)
			{
				return;
			}
			NKCUtil.SetLabelTextColor(this.m_lbClassType, NKCUtil.GetColorForUnitGrade(unitTempletBase.m_NKM_UNIT_GRADE));
			NKCUtil.SetLabelText(this.m_lbClassType, NKCUtilString.GetUnitStyleMarkString(unitTempletBase));
			NKCUtil.SetLabelText(this.m_lbUnitName, unitTempletBase.GetUnitName());
			NKCUtil.SetLabelText(this.m_lbRole, NKCUtilString.GetRoleText(unitTempletBase));
			NKCUtil.SetImageSprite(this.m_imgRole, NKCResourceUtility.GetOrLoadUnitRoleIcon(unitTempletBase, true), false);
			NKCUtil.SetImageSprite(this.m_imgMoveType, NKCUtil.GetMoveTypeImg(unitTempletBase.m_bAirUnit), false);
			NKCUtil.SetLabelText(this.m_lbMoveType, NKCUtilString.GetMoveTypeText(unitTempletBase.m_bAirUnit));
			if (this.m_lbAttackType != null)
			{
				if (unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc == NKM_FIND_TARGET_TYPE.NFTT_INVALID)
				{
					this.m_imgAttack.sprite = NKCResourceUtility.GetOrLoadUnitAttackTypeIcon(unitTempletBase.m_NKM_FIND_TARGET_TYPE, true);
				}
				else
				{
					this.m_imgAttack.sprite = NKCResourceUtility.GetOrLoadUnitAttackTypeIcon(unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc, true);
				}
			}
			if (this.m_lbAttackType != null)
			{
				if (unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc == NKM_FIND_TARGET_TYPE.NFTT_INVALID)
				{
					this.m_lbAttackType.text = NKCUtilString.GetAtkTypeText(unitTempletBase.m_NKM_FIND_TARGET_TYPE);
				}
				else
				{
					this.m_lbAttackType.text = NKCUtilString.GetAtkTypeText(unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc);
				}
			}
			string unitAbilityName = NKCUtilString.GetUnitAbilityName(unitTempletBase.m_UnitID, "   ");
			NKCUtil.SetGameobjectActive(this.m_objTag, !string.IsNullOrEmpty(unitAbilityName));
			NKCUtil.SetLabelText(this.m_lbTag, unitAbilityName);
			this.SetGrade(unitTempletBase.m_NKM_UNIT_GRADE, unitTempletBase.m_bAwaken);
		}

		// Token: 0x0600512A RID: 20778 RVA: 0x00189F1C File Offset: 0x0018811C
		private void SetGrade(NKM_UNIT_GRADE unitGrade, bool bAwake)
		{
			NKCUtil.SetImageSprite(this.m_imgRarityIcon, NKCUtil.GetSpriteUnitGrade(unitGrade), false);
			NKCUtil.SetGameobjectActive(this.m_objFX_N, unitGrade == NKM_UNIT_GRADE.NUG_N);
			NKCUtil.SetGameobjectActive(this.m_objFX_R, unitGrade == NKM_UNIT_GRADE.NUG_R);
			NKCUtil.SetGameobjectActive(this.m_objFX_SR, unitGrade == NKM_UNIT_GRADE.NUG_SR);
			NKCUtil.SetGameobjectActive(this.m_objFX_SSR, unitGrade == NKM_UNIT_GRADE.NUG_SSR);
			NKCUtil.SetGameobjectActive(this.m_objFX_AwakeSR, bAwake && unitGrade == NKM_UNIT_GRADE.NUG_SR);
			NKCUtil.SetGameobjectActive(this.m_objFX_AwakeSSR, bAwake && unitGrade == NKM_UNIT_GRADE.NUG_SSR);
		}

		// Token: 0x04004189 RID: 16777
		public Text m_lbClassType;

		// Token: 0x0400418A RID: 16778
		public Image m_imgRarityIcon;

		// Token: 0x0400418B RID: 16779
		public GameObject m_objFX_SSR;

		// Token: 0x0400418C RID: 16780
		public GameObject m_objFX_SR;

		// Token: 0x0400418D RID: 16781
		public GameObject m_objFX_R;

		// Token: 0x0400418E RID: 16782
		public GameObject m_objFX_N;

		// Token: 0x0400418F RID: 16783
		public GameObject m_objFX_AwakeSSR;

		// Token: 0x04004190 RID: 16784
		public GameObject m_objFX_AwakeSR;

		// Token: 0x04004191 RID: 16785
		public Text m_lbUnitName;

		// Token: 0x04004192 RID: 16786
		public Text m_lbRole;

		// Token: 0x04004193 RID: 16787
		public Image m_imgRole;

		// Token: 0x04004194 RID: 16788
		public Text m_lbMoveType;

		// Token: 0x04004195 RID: 16789
		public Image m_imgMoveType;

		// Token: 0x04004196 RID: 16790
		public Text m_lbAttackType;

		// Token: 0x04004197 RID: 16791
		public Image m_imgAttack;

		// Token: 0x04004198 RID: 16792
		public GameObject m_objTag;

		// Token: 0x04004199 RID: 16793
		public Text m_lbTag;

		// Token: 0x0400419A RID: 16794
		public Image m_imgTag;
	}
}
