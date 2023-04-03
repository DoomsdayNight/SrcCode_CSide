using System;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000981 RID: 2433
	public class NKCUIDiveGameArtifactSlot : MonoBehaviour
	{
		// Token: 0x0600639D RID: 25501 RVA: 0x001F7AB2 File Offset: 0x001F5CB2
		public void InitUI()
		{
			this.m_NKCUISlot.Init();
		}

		// Token: 0x0600639E RID: 25502 RVA: 0x001F7AC0 File Offset: 0x001F5CC0
		public void SetData(NKMDiveArtifactTemplet cNKMDiveArtifactTemplet)
		{
			if (cNKMDiveArtifactTemplet == null)
			{
				NKCUtil.SetGameobjectActive(this.m_NKCUISlot, false);
				NKCUtil.SetLabelText(this.m_lbName, "");
				NKCUtil.SetLabelText(this.m_lbDesc, "");
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NKCUISlot, true);
			this.m_NKCUISlot.SetDiveArtifactData(NKCUISlot.SlotData.MakeDiveArtifactData(cNKMDiveArtifactTemplet.ArtifactID, 1), false, false, true, null);
			this.m_NKCUISlot.SetOnClickAction(new NKCUISlot.SlotClickType[1]);
			NKCUtil.SetLabelText(this.m_lbName, cNKMDiveArtifactTemplet.ArtifactName_Translated);
			NKCUtil.SetLabelText(this.m_lbDesc, cNKMDiveArtifactTemplet.ArtifactMiscDesc_1_Translated);
		}

		// Token: 0x04004F0B RID: 20235
		public NKCUISlot m_NKCUISlot;

		// Token: 0x04004F0C RID: 20236
		public Text m_lbName;

		// Token: 0x04004F0D RID: 20237
		public Text m_lbDesc;
	}
}
