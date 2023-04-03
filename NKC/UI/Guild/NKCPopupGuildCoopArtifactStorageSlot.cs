using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B2B RID: 2859
	public class NKCPopupGuildCoopArtifactStorageSlot : MonoBehaviour
	{
		// Token: 0x06008231 RID: 33329 RVA: 0x002BEB98 File Offset: 0x002BCD98
		public void Init()
		{
			if (this.m_slot != null)
			{
				this.m_slot.Init();
				this.m_slot.SetUseBigImg(false);
			}
		}

		// Token: 0x06008232 RID: 33330 RVA: 0x002BEBC0 File Offset: 0x002BCDC0
		public void SetData(NKCUIComGuildArtifactContent.ArtifactSlotData slotData)
		{
			if (!slotData.bIsArenaNum && slotData.id == 0)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			this.m_bIsArenaNum = slotData.bIsArenaNum;
			NKCUtil.SetGameobjectActive(this.m_objNormalSlot, !this.m_bIsArenaNum);
			NKCUtil.SetGameobjectActive(this.m_objTextSlot, this.m_bIsArenaNum);
			if (this.m_bIsArenaNum)
			{
				NKCUtil.SetLabelText(this.m_lbArenaNum, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_DUNGEON_DUNGEON_UI_ARENA_INFO, slotData.id));
				return;
			}
			if (this.m_slot != null)
			{
				NKCUtil.SetGameobjectActive(this.m_slot, true);
				this.m_slot.SetData(NKCUISlot.SlotData.MakeGuildArtifactData(slotData.id, 1), true, null);
			}
			NKCUtil.SetLabelText(this.m_lbName, slotData.name);
			NKCUtil.SetLabelText(this.m_lbDesc, slotData.desc);
		}

		// Token: 0x04006E5B RID: 28251
		[Header("일반 슬롯")]
		public GameObject m_objNormalSlot;

		// Token: 0x04006E5C RID: 28252
		public NKCUISlot m_slot;

		// Token: 0x04006E5D RID: 28253
		public Text m_lbName;

		// Token: 0x04006E5E RID: 28254
		public Text m_lbDesc;

		// Token: 0x04006E5F RID: 28255
		public bool m_bIsArenaNum;

		// Token: 0x04006E60 RID: 28256
		[Header("텍스트 슬롯")]
		public GameObject m_objTextSlot;

		// Token: 0x04006E61 RID: 28257
		public Text m_lbArenaNum;
	}
}
