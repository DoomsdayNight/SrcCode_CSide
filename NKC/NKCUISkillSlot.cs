using System;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007D5 RID: 2005
	public class NKCUISkillSlot : MonoBehaviour
	{
		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06004F18 RID: 20248 RVA: 0x0017E39E File Offset: 0x0017C59E
		public int CurrentSkillID
		{
			get
			{
				if (this.m_CurrentSkillTemplet == null)
				{
					return 0;
				}
				return this.m_CurrentSkillTemplet.m_ID;
			}
		}

		// Token: 0x06004F19 RID: 20249 RVA: 0x0017E3B8 File Offset: 0x0017C5B8
		public void Init(NKCUISkillSlot.OnClickSkillSlot dOnClick)
		{
			if (this.m_cbtnSlot != null)
			{
				this.m_cbtnSlot.PointerClick.RemoveAllListeners();
				this.m_cbtnSlot.PointerClick.AddListener(new UnityAction(this.OnClick));
			}
			this.dOnClickSkillSlot = dOnClick;
			NKCUtil.SetGameobjectActive(this.m_objEmpty, true);
			NKCUtil.SetGameobjectActive(this.m_objHyperEmpty, false);
			NKCUtil.SetGameobjectActive(this.m_objBorder, false);
			NKCUtil.SetGameobjectActive(this.m_objHighlighted, false);
			NKCUtil.SetGameobjectActive(this.m_objHyperHighlighted, false);
			NKCUtil.SetGameobjectActive(this.m_objNormalSkillLocked, false);
			NKCUtil.SetGameobjectActive(this.m_objHyperSkillLocked, false);
			NKCUtil.SetGameobjectActive(this.m_imgSkillIcon, false);
			NKCUtil.SetGameobjectActive(this.m_lbSkillLevel, false);
		}

		// Token: 0x06004F1A RID: 20250 RVA: 0x0017E472 File Offset: 0x0017C672
		public void Cleanup()
		{
			this.SetData(null, false);
		}

		// Token: 0x06004F1B RID: 20251 RVA: 0x0017E47C File Offset: 0x0017C67C
		public void SetData(NKMUnitSkillTemplet unitSkillTemplet, bool bIsHyper)
		{
			this.m_CurrentSkillTemplet = unitSkillTemplet;
			this.m_bIsHyper = bIsHyper;
			if (unitSkillTemplet != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objEmpty, false);
				NKCUtil.SetGameobjectActive(this.m_objHyperEmpty, false);
				NKCUtil.SetGameobjectActive(this.m_objNormalSkillLocked, false);
				NKCUtil.SetGameobjectActive(this.m_objHyperSkillLocked, false);
				NKCUtil.SetGameobjectActive(this.m_imgSkillIcon, true);
				NKCUtil.SetGameobjectActive(this.m_lbSkillLevel, true);
				NKCUtil.SetImageSprite(this.m_imgSkillIcon, NKCUtil.GetSkillIconSprite(unitSkillTemplet), false);
				NKCUtil.SetLabelText(this.m_lbSkillLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, unitSkillTemplet.m_Level));
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objEmpty, !this.m_bIsHyper);
			NKCUtil.SetGameobjectActive(this.m_objHyperEmpty, this.m_bIsHyper);
			NKCUtil.SetGameobjectActive(this.m_objHighlighted, false);
			NKCUtil.SetGameobjectActive(this.m_objHyperHighlighted, false);
			NKCUtil.SetGameobjectActive(this.m_objNormalSkillLocked, false);
			NKCUtil.SetGameobjectActive(this.m_objHyperSkillLocked, false);
			NKCUtil.SetGameobjectActive(this.m_imgSkillIcon, false);
			NKCUtil.SetGameobjectActive(this.m_lbSkillLevel, false);
		}

		// Token: 0x06004F1C RID: 20252 RVA: 0x0017E582 File Offset: 0x0017C782
		public void LockSkill(bool value)
		{
			if (this.m_bIsHyper)
			{
				NKCUtil.SetGameobjectActive(this.m_objHyperSkillLocked, value);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objNormalSkillLocked, value);
		}

		// Token: 0x06004F1D RID: 20253 RVA: 0x0017E5A5 File Offset: 0x0017C7A5
		public void SetHighlight(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_objHyperHighlighted, this.m_bIsHyper && value);
			NKCUtil.SetGameobjectActive(this.m_objHighlighted, !this.m_bIsHyper && value);
		}

		// Token: 0x06004F1E RID: 20254 RVA: 0x0017E5D0 File Offset: 0x0017C7D0
		public void OnClick()
		{
			if (this.dOnClickSkillSlot != null)
			{
				this.dOnClickSkillSlot(this.m_CurrentSkillTemplet);
			}
		}

		// Token: 0x06004F1F RID: 20255 RVA: 0x0017E5EC File Offset: 0x0017C7EC
		public void SetShipData(NKMShipSkillTemplet shipSkillTemplet, bool bLock = false)
		{
			this.m_imgSkillIcon.sprite = NKCUtil.GetSkillIconSprite(shipSkillTemplet);
			NKCUtil.SetGameobjectActive(this.m_objHyperEmpty, false);
			NKCUtil.SetGameobjectActive(this.m_objHighlighted, false);
			NKCUtil.SetGameobjectActive(this.m_objHyperHighlighted, false);
			NKCUtil.SetGameobjectActive(this.m_objNormalSkillLocked, bLock);
			NKCUtil.SetGameobjectActive(this.m_objHyperSkillLocked, false);
			NKCUtil.SetGameobjectActive(this.m_imgSkillIcon, true);
			NKCUtil.SetGameobjectActive(this.m_lbSkillLevel, false);
			NKCUtil.SetGameobjectActive(this.m_imgNew, false);
		}

		// Token: 0x04003F11 RID: 16145
		public NKCUIComButton m_cbtnSlot;

		// Token: 0x04003F12 RID: 16146
		public GameObject m_objEmpty;

		// Token: 0x04003F13 RID: 16147
		public GameObject m_objHyperEmpty;

		// Token: 0x04003F14 RID: 16148
		public GameObject m_objBorder;

		// Token: 0x04003F15 RID: 16149
		public GameObject m_objHighlighted;

		// Token: 0x04003F16 RID: 16150
		public GameObject m_objHyperHighlighted;

		// Token: 0x04003F17 RID: 16151
		public GameObject m_objNormalSkillLocked;

		// Token: 0x04003F18 RID: 16152
		public GameObject m_objHyperSkillLocked;

		// Token: 0x04003F19 RID: 16153
		public Image m_imgSkillIcon;

		// Token: 0x04003F1A RID: 16154
		public Text m_lbSkillLevel;

		// Token: 0x04003F1B RID: 16155
		public Image m_imgNew;

		// Token: 0x04003F1C RID: 16156
		private NKCUISkillSlot.OnClickSkillSlot dOnClickSkillSlot;

		// Token: 0x04003F1D RID: 16157
		private NKMUnitSkillTemplet m_CurrentSkillTemplet;

		// Token: 0x04003F1E RID: 16158
		private bool m_bIsHyper;

		// Token: 0x0200148F RID: 5263
		// (Invoke) Token: 0x0600A93D RID: 43325
		public delegate void OnClickSkillSlot(NKMUnitSkillTemplet selectedSkillTemplet);
	}
}
