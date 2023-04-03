using System;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Fierce
{
	// Token: 0x02000BB7 RID: 2999
	public class NKCUIFierceBattleSelfPenaltySlot : MonoBehaviour
	{
		// Token: 0x1700161C RID: 5660
		// (get) Token: 0x06008A8B RID: 35467 RVA: 0x002F1F43 File Offset: 0x002F0143
		public NKMFiercePenaltyTemplet TempletData
		{
			get
			{
				return this.m_templet;
			}
		}

		// Token: 0x06008A8C RID: 35468 RVA: 0x002F1F4B File Offset: 0x002F014B
		public void Init()
		{
			NKCUtil.SetBindFunction(this.m_csbtnClick, new UnityAction(this.OnClickSlot));
		}

		// Token: 0x06008A8D RID: 35469 RVA: 0x002F1F64 File Offset: 0x002F0164
		public void SetData(NKMFiercePenaltyTemplet PenaltyTemplt, OnClickPenalty OnClick)
		{
			if (PenaltyTemplt == null)
			{
				return;
			}
			this.m_templet = PenaltyTemplt;
			string @string = NKCStringTable.GetString(PenaltyTemplt.PenaltyLevelDesc, false);
			NKCUtil.SetLabelText(this.m_lbDesc, @string);
			NKCUtil.SetLabelText(this.m_lbLevel, PenaltyTemplt.PenaltyLevel.ToString());
			NKCUtil.SetImageSprite(this.m_imgIcon, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_FIERCE_BATTLE_TEXTURE", PenaltyTemplt.PenaltyIcon, false), false);
			NKCUtil.SetImageSprite(this.m_imgIconBG, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_FIERCE_BATTLE_TEXTURE", PenaltyTemplt.PenaltyIconBG, false), false);
			this.Select(false);
			this.Disable(false);
			this.dOnClick = OnClick;
		}

		// Token: 0x06008A8E RID: 35470 RVA: 0x002F1FFD File Offset: 0x002F01FD
		public void Select(bool bSelect)
		{
			NKCUtil.SetGameobjectActive(this.m_objSelect, bSelect);
		}

		// Token: 0x06008A8F RID: 35471 RVA: 0x002F200B File Offset: 0x002F020B
		public void Disable(bool bDisable)
		{
			NKCUtil.SetGameobjectActive(this.m_objDisable, bDisable);
		}

		// Token: 0x06008A90 RID: 35472 RVA: 0x002F2019 File Offset: 0x002F0219
		private void OnClickSlot()
		{
			OnClickPenalty onClickPenalty = this.dOnClick;
			if (onClickPenalty == null)
			{
				return;
			}
			onClickPenalty(this.m_templet);
		}

		// Token: 0x04007754 RID: 30548
		public NKCUIComStateButton m_csbtnClick;

		// Token: 0x04007755 RID: 30549
		public Image m_imgIcon;

		// Token: 0x04007756 RID: 30550
		public Image m_imgIconBG;

		// Token: 0x04007757 RID: 30551
		public GameObject m_objSelect;

		// Token: 0x04007758 RID: 30552
		public NKCComText m_lbDesc;

		// Token: 0x04007759 RID: 30553
		public NKCComText m_lbLevel;

		// Token: 0x0400775A RID: 30554
		public GameObject m_objDisable;

		// Token: 0x0400775B RID: 30555
		private OnClickPenalty dOnClick;

		// Token: 0x0400775C RID: 30556
		private NKMFiercePenaltyTemplet m_templet;
	}
}
