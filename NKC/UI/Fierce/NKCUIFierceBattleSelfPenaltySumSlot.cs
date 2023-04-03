using System;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Fierce
{
	// Token: 0x02000BB8 RID: 3000
	public class NKCUIFierceBattleSelfPenaltySumSlot : MonoBehaviour
	{
		// Token: 0x1700161D RID: 5661
		// (get) Token: 0x06008A92 RID: 35474 RVA: 0x002F2039 File Offset: 0x002F0239
		public NKMFiercePenaltyTemplet PenaltyTemplet
		{
			get
			{
				return this.m_iPenaltyTemplet;
			}
		}

		// Token: 0x06008A93 RID: 35475 RVA: 0x002F2044 File Offset: 0x002F0244
		public void SetData(NKMFiercePenaltyTemplet templet)
		{
			if (templet == null)
			{
				return;
			}
			this.m_iPenaltyTemplet = templet;
			if (templet.battleCondition != null)
			{
				if (!string.IsNullOrEmpty(templet.battleCondition.BattleCondIngameIcon))
				{
					Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_fierce_battle_texture", templet.battleCondition.BattleCondIngameIcon, false);
					NKCUtil.SetImageSprite(this.m_imgIcon, orLoadAssetResource, false);
				}
				string @string = NKCStringTable.GetString(templet.battleCondition.BattleCondDesc, false);
				NKCUtil.SetLabelText(this.m_lbDesc, @string);
			}
		}

		// Token: 0x0400775D RID: 30557
		public Text m_lbDesc;

		// Token: 0x0400775E RID: 30558
		public Image m_imgIcon;

		// Token: 0x0400775F RID: 30559
		private NKMFiercePenaltyTemplet m_iPenaltyTemplet;
	}
}
