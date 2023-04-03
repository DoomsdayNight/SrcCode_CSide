using System;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009E3 RID: 2531
	public class NKCUIShipInfoMoveType : MonoBehaviour
	{
		// Token: 0x06006CDA RID: 27866 RVA: 0x00239BE0 File Offset: 0x00237DE0
		public void SetData(NKM_UNIT_STYLE_TYPE unitStyleType)
		{
			NKCUtil.SetImageSprite(this.m_imgRightShipStyleIcon, NKCResourceUtility.GetOrLoadUnitStyleIcon(unitStyleType, true), false);
			NKCUtil.SetImageSprite(this.m_imgMoveType, this.GetSpriteMoveType(unitStyleType), false);
			NKCUtil.SetLabelText(this.m_lbRightShipStyleName, NKCUtilString.GetUnitStyleName(unitStyleType));
			NKCUtil.SetLabelText(this.m_lbRightShipStyleDesc, NKCUtilString.GetUnitStyleDesc(unitStyleType));
		}

		// Token: 0x06006CDB RID: 27867 RVA: 0x00239C38 File Offset: 0x00237E38
		private Sprite GetSpriteMoveType(NKM_UNIT_STYLE_TYPE type)
		{
			string stringMoveType = this.GetStringMoveType(type);
			if (string.IsNullOrEmpty(stringMoveType))
			{
				return null;
			}
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_SHIP_INFO_TEXTURE", stringMoveType, false);
		}

		// Token: 0x06006CDC RID: 27868 RVA: 0x00239C64 File Offset: 0x00237E64
		private string GetStringMoveType(NKM_UNIT_STYLE_TYPE type)
		{
			string result = string.Empty;
			switch (type)
			{
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_ASSAULT:
				result = "NKM_UI_SHIP_INFO_TEXTURE_MOVETYPE_1";
				break;
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_HEAVY:
				result = "NKM_UI_SHIP_INFO_TEXTURE_MOVETYPE_4";
				break;
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_CRUISER:
				result = "NKM_UI_SHIP_INFO_TEXTURE_MOVETYPE_2";
				break;
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_SPECIAL:
				result = "NKM_UI_SHIP_INFO_TEXTURE_MOVETYPE_3";
				break;
			}
			return result;
		}

		// Token: 0x040058AE RID: 22702
		public Image m_imgRightShipStyleIcon;

		// Token: 0x040058AF RID: 22703
		public Image m_imgMoveType;

		// Token: 0x040058B0 RID: 22704
		public Text m_lbRightShipStyleName;

		// Token: 0x040058B1 RID: 22705
		public Text m_lbRightShipStyleDesc;
	}
}
