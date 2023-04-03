using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Tooltip
{
	// Token: 0x02000B0D RID: 2829
	public class NKCUITooltipUnit : NKCUITooltipShip
	{
		// Token: 0x06008067 RID: 32871 RVA: 0x002B4B90 File Offset: 0x002B2D90
		public override void SetData(NKCUITooltip.Data data)
		{
			NKCUITooltip.UnitData unitData = data as NKCUITooltip.UnitData;
			if (unitData == null)
			{
				return;
			}
			NKMUnitTempletBase unitTempletBase = unitData.UnitTempletBase;
			if (unitTempletBase == null)
			{
				return;
			}
			base.SetData(data);
			Sprite orLoadUnitRoleIcon = NKCResourceUtility.GetOrLoadUnitRoleIcon(unitTempletBase, true);
			NKCUtil.SetImageSprite(this.m_roleIcon, orLoadUnitRoleIcon, true);
			NKCUtil.SetLabelText(this.m_roleText, NKCUtilString.GetRoleText(unitTempletBase));
			NKM_FIND_TARGET_TYPE nkm_FIND_TARGET_TYPE = unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc;
			if (unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc == NKM_FIND_TARGET_TYPE.NFTT_INVALID)
			{
				nkm_FIND_TARGET_TYPE = unitTempletBase.m_NKM_FIND_TARGET_TYPE;
			}
			Sprite orLoadUnitAttackTypeIcon = NKCResourceUtility.GetOrLoadUnitAttackTypeIcon(nkm_FIND_TARGET_TYPE, true);
			NKCUtil.SetImageSprite(this.m_attackTypeIcon, orLoadUnitAttackTypeIcon, true);
			NKCUtil.SetLabelText(this.m_attackTypeText, NKCUtilString.GetAtkTypeText(nkm_FIND_TARGET_TYPE));
		}

		// Token: 0x04006CA6 RID: 27814
		[Header("유닛 타입")]
		public Image m_roleIcon;

		// Token: 0x04006CA7 RID: 27815
		public Text m_roleText;

		// Token: 0x04006CA8 RID: 27816
		public Image m_attackTypeIcon;

		// Token: 0x04006CA9 RID: 27817
		public Text m_attackTypeText;
	}
}
