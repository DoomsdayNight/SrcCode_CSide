using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Tooltip
{
	// Token: 0x02000B0B RID: 2827
	public class NKCUITooltipTacticalCommand : NKCUITooltipBase
	{
		// Token: 0x06008061 RID: 32865 RVA: 0x002B4AF0 File Offset: 0x002B2CF0
		public override void Init()
		{
		}

		// Token: 0x06008062 RID: 32866 RVA: 0x002B4AF4 File Offset: 0x002B2CF4
		public override void SetData(NKCUITooltip.Data data)
		{
			NKCUITooltip.TacticalCommandData tacticalCommandData = data as NKCUITooltip.TacticalCommandData;
			if (tacticalCommandData == null)
			{
				Debug.LogError("Tooltip TacticalCommandData is null");
				return;
			}
			NKMTacticalCommandTemplet tacticalCommandTemplet = tacticalCommandData.TacticalCommandTemplet;
			NKCUtil.SetLabelText(this.m_lbName, tacticalCommandTemplet.GetTCName());
			this.m_imgIcon.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_TACTICAL_COMMAND_ICON", tacticalCommandTemplet.m_TCIconName, false);
		}

		// Token: 0x04006CA3 RID: 27811
		public Image m_imgIcon;

		// Token: 0x04006CA4 RID: 27812
		public Text m_lbName;
	}
}
