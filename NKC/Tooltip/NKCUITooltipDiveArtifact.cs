using System;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Tooltip
{
	// Token: 0x02000B04 RID: 2820
	public class NKCUITooltipDiveArtifact : NKCUITooltipBase
	{
		// Token: 0x06008049 RID: 32841 RVA: 0x002B4463 File Offset: 0x002B2663
		public override void Init()
		{
		}

		// Token: 0x0600804A RID: 32842 RVA: 0x002B4468 File Offset: 0x002B2668
		public override void SetData(NKCUITooltip.Data data)
		{
			NKCUITooltip.DiveArtifactData diveArtifactData = data as NKCUITooltip.DiveArtifactData;
			if (diveArtifactData == null)
			{
				Debug.LogError("Tooltip DiveArtifactData is null");
				return;
			}
			NKMDiveArtifactTemplet nkmdiveArtifactTemplet = NKMDiveArtifactTemplet.Find(diveArtifactData.Slot.ID);
			if (nkmdiveArtifactTemplet == null)
			{
				return;
			}
			NKCUtil.SetLabelText(this.m_lbName, nkmdiveArtifactTemplet.ArtifactName_Translated);
			NKCUtil.SetLabelText(this.m_lbDesc, nkmdiveArtifactTemplet.ArtifactMiscDesc_1_Translated);
		}

		// Token: 0x04006C85 RID: 27781
		public Text m_lbName;

		// Token: 0x04006C86 RID: 27782
		public Text m_lbDesc;
	}
}
