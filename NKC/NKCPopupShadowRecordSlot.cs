using System;
using ClientPacket.Mode;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007FA RID: 2042
	public class NKCPopupShadowRecordSlot : MonoBehaviour
	{
		// Token: 0x060050DE RID: 20702 RVA: 0x00187FB0 File Offset: 0x001861B0
		public void SetData(NKMShadowBattleTemplet templet, NKMPalaceDungeonData data)
		{
			TimeSpan timeSpan = TimeSpan.FromSeconds((double)data.recentTime);
			NKCUtil.SetLabelText(this.m_txtTime, NKCUtilString.GetTimeSpanString(timeSpan));
			NKCUtil.SetGameobjectActive(this.m_objNormal, templet.PALACE_BATTLE_TYPE != PALACE_BATTLE_TYPE.PBT_BOSS);
			NKCUtil.SetGameobjectActive(this.m_objBoss, templet.PALACE_BATTLE_TYPE == PALACE_BATTLE_TYPE.PBT_BOSS);
			if (templet.PALACE_BATTLE_TYPE == PALACE_BATTLE_TYPE.PBT_BOSS)
			{
				NKCUtil.SetLabelText(this.m_txtNum, NKCUtilString.GET_SHADOW_RECORD_POPUP_SLOT_BOSS);
				return;
			}
			NKCUtil.SetLabelText(this.m_txtNum, NKCUtilString.GET_SHADOW_RECORD_POPUP_SLOT_NORMAL, new object[]
			{
				templet.BATTLE_ORDER.ToString("D2")
			});
		}

		// Token: 0x04004123 RID: 16675
		public Text m_txtNum;

		// Token: 0x04004124 RID: 16676
		public Text m_txtTime;

		// Token: 0x04004125 RID: 16677
		public GameObject m_objNormal;

		// Token: 0x04004126 RID: 16678
		public GameObject m_objBoss;
	}
}
