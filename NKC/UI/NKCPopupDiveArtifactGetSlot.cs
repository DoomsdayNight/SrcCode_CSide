using System;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A3B RID: 2619
	public class NKCPopupDiveArtifactGetSlot : MonoBehaviour
	{
		// Token: 0x060072C0 RID: 29376 RVA: 0x00261C54 File Offset: 0x0025FE54
		public void InitUI(int index)
		{
			this.m_Index = index;
			NKCUtil.SetGameobjectActive(this.m_objDisabled, false);
			this.m_csbtnSelect.PointerClick.RemoveAllListeners();
			this.m_csbtnSelect.PointerClick.AddListener(new UnityAction(this.OnClickSelect));
			this.m_NKCUISlot.SetUseBigImg(true);
		}

		// Token: 0x060072C1 RID: 29377 RVA: 0x00261CAC File Offset: 0x0025FEAC
		public void SetData(NKMDiveArtifactTemplet cNKMDiveArtifactTemplet)
		{
			NKCUtil.SetGameobjectActive(this.m_objDisabled, false);
			if (cNKMDiveArtifactTemplet == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objNoMoreExistArtifact, true);
				NKCUtil.SetGameobjectActive(this.m_objNormal, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objNoMoreExistArtifact, false);
			NKCUtil.SetGameobjectActive(this.m_objNormal, true);
			NKCUtil.SetLabelText(this.m_lbArtifactName, cNKMDiveArtifactTemplet.ArtifactName_Translated);
			NKCUtil.SetLabelText(this.m_lbArtifactDesc, cNKMDiveArtifactTemplet.ArtifactMiscDesc_1_Translated);
			this.m_NKCUISlot.SetData(NKCUISlot.SlotData.MakeDiveArtifactData(cNKMDiveArtifactTemplet.ArtifactID, 1), false, false, true, null);
			NKCUtil.SetGameobjectActive(this.m_objReturnItem, cNKMDiveArtifactTemplet.RewardId > 0);
			NKCUtil.SetLabelText(this.m_lbReturnItemCount, cNKMDiveArtifactTemplet.RewardQuantity.ToString());
			Sprite orLoadMiscItemSmallIcon = NKCResourceUtility.GetOrLoadMiscItemSmallIcon(cNKMDiveArtifactTemplet.RewardId);
			NKCUtil.SetImageSprite(this.m_imgReturnItemIcon, orLoadMiscItemSmallIcon, true);
		}

		// Token: 0x060072C2 RID: 29378 RVA: 0x00261D7C File Offset: 0x0025FF7C
		public void OnClickSelect()
		{
			if (this.m_NKCUISlot == null || this.m_NKCUISlot.GetSlotData() == null)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_DIVE_SELECT_ARTIFACT_REQ(this.m_NKCUISlot.GetSlotData().ID);
			NKC_SCEN_DIVE nkc_SCEN_DIVE = NKCScenManager.GetScenManager().Get_NKC_SCEN_DIVE();
			nkc_SCEN_DIVE.GetDiveGame().SetLastSelectedArtifactSlotIndex(this.m_Index);
			if (nkc_SCEN_DIVE.GetDiveGame().IsOpenNKCPopupDiveArtifactGet)
			{
				nkc_SCEN_DIVE.GetDiveGame().NKCPopupDiveArtifactGet.InvalidAuto();
			}
		}

		// Token: 0x04005EA6 RID: 24230
		public NKCUISlot m_NKCUISlot;

		// Token: 0x04005EA7 RID: 24231
		public Text m_lbArtifactName;

		// Token: 0x04005EA8 RID: 24232
		public Text m_lbArtifactDesc;

		// Token: 0x04005EA9 RID: 24233
		public NKCUIComStateButton m_csbtnSelect;

		// Token: 0x04005EAA RID: 24234
		public GameObject m_objNormal;

		// Token: 0x04005EAB RID: 24235
		public GameObject m_objDisabled;

		// Token: 0x04005EAC RID: 24236
		public GameObject m_objNoMoreExistArtifact;

		// Token: 0x04005EAD RID: 24237
		public GameObject m_objReturnItem;

		// Token: 0x04005EAE RID: 24238
		public Text m_lbReturnItemCount;

		// Token: 0x04005EAF RID: 24239
		public Image m_imgReturnItemIcon;

		// Token: 0x04005EB0 RID: 24240
		private int m_Index = -1;
	}
}
