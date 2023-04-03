using System;
using NKC.UI.Tooltip;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A24 RID: 2596
	public class NKCUIOperatorSkill : MonoBehaviour
	{
		// Token: 0x060071C0 RID: 29120 RVA: 0x0025D104 File Offset: 0x0025B304
		public void SetData(int skillID, int skillLevel, bool Banned = false)
		{
			NKMOperatorSkillTemplet skillTemplet = NKCOperatorUtil.GetSkillTemplet(skillID);
			this.SetData(skillTemplet, skillLevel, Banned);
		}

		// Token: 0x060071C1 RID: 29121 RVA: 0x0025D124 File Offset: 0x0025B324
		public void SetData(NKMOperatorSkillTemplet skillTemplet, int skillLevel, bool Banned = false)
		{
			if (skillTemplet == null)
			{
				this.m_skillID = 0;
				return;
			}
			NKCUtil.SetImageSprite(this.m_SkillIcon, NKCUtil.GetSkillIconSprite(skillTemplet), false);
			NKCUtil.SetLabelText(this.m_lbSkillLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, skillLevel));
			NKCUtil.SetLabelTextColor(this.m_lbSkillLevel, Banned ? NKCOperatorUtil.BAN_COLOR_RED : Color.white);
			NKCUtil.SetLabelText(this.m_lbSkillName, NKCStringTable.GetString(skillTemplet.m_OperSkillNameStrID, false));
			NKCUtil.SetLabelText(this.m_lbSkillDesc, NKCOperatorUtil.MakeOperatorSkillDesc(skillTemplet, skillLevel));
			this.m_skillID = skillTemplet.m_OperSkillID;
			this.m_skillLevel = skillLevel;
			if (this.m_csbtnButton != null)
			{
				this.m_csbtnButton.PointerDown.RemoveAllListeners();
				this.m_csbtnButton.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnPointDownSkill));
			}
		}

		// Token: 0x060071C2 RID: 29122 RVA: 0x0025D1FC File Offset: 0x0025B3FC
		public void SetDataForCollection(int OperatorID, int level = -1)
		{
			this.m_skillID = 0;
			this.m_skillLevel = 1;
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(OperatorID);
			if (unitTempletBase == null)
			{
				return;
			}
			if (unitTempletBase.m_NKM_UNIT_TYPE != NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				return;
			}
			if (unitTempletBase.m_lstSkillStrID != null && unitTempletBase.m_lstSkillStrID.Count > 0)
			{
				NKMOperatorSkillTemplet skillTemplet = NKCOperatorUtil.GetSkillTemplet(unitTempletBase.m_lstSkillStrID[0]);
				if (skillTemplet != null)
				{
					this.m_skillID = skillTemplet.m_OperSkillID;
					this.m_skillLevel = ((level < 0) ? skillTemplet.m_MaxSkillLevel : level);
					NKCUtil.SetImageSprite(this.m_SkillIcon, NKCUtil.GetSkillIconSprite(skillTemplet), false);
					NKCUtil.SetLabelText(this.m_lbSkillLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, this.m_skillLevel));
					NKCUtil.SetLabelText(this.m_lbSkillName, NKCStringTable.GetString(skillTemplet.m_OperSkillNameStrID, false));
					NKCUtil.SetLabelText(this.m_lbSkillDesc, NKCOperatorUtil.MakeOperatorSkillDesc(skillTemplet, this.m_skillLevel));
				}
			}
			if (this.m_csbtnButton != null)
			{
				this.m_csbtnButton.PointerDown.RemoveAllListeners();
				this.m_csbtnButton.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnPointDownSkill));
			}
		}

		// Token: 0x060071C3 RID: 29123 RVA: 0x0025D316 File Offset: 0x0025B516
		private void OnPointDownSkill(PointerEventData eventData)
		{
			NKCUITooltip.Instance.Open(this.m_skillID, this.m_skillLevel, new Vector2?(eventData.position));
		}

		// Token: 0x04005DAF RID: 23983
		public Image m_SkillIcon;

		// Token: 0x04005DB0 RID: 23984
		public Text m_lbSkillName;

		// Token: 0x04005DB1 RID: 23985
		public Text m_lbSkillDesc;

		// Token: 0x04005DB2 RID: 23986
		public Text m_lbSkillLevel;

		// Token: 0x04005DB3 RID: 23987
		public NKCUIComStateButton m_csbtnButton;

		// Token: 0x04005DB4 RID: 23988
		private int m_skillID;

		// Token: 0x04005DB5 RID: 23989
		private int m_skillLevel;
	}
}
