using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A9D RID: 2717
	public class NKCUIRearmamentProcessInfoSummary : MonoBehaviour
	{
		// Token: 0x06007865 RID: 30821 RVA: 0x0027F458 File Offset: 0x0027D658
		private void Init()
		{
			if (this.bInit)
			{
				return;
			}
			foreach (NKCUIComStateButton btn in this.m_csbtnSkill)
			{
				NKCUtil.SetBindFunction(btn, new UnityAction(this.ClickSkillInfo));
			}
			this.bInit = true;
		}

		// Token: 0x06007866 RID: 30822 RVA: 0x0027F4C4 File Offset: 0x0027D6C4
		public void SetData(int unitID, bool bSkill = false)
		{
			this.Init();
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
			if (unitTempletBase == null)
			{
				return;
			}
			NKCUtil.SetImageSprite(this.m_imgRearmUnitFaceCard, NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, unitTempletBase), true);
			int num = 0;
			for (int i = 0; i < unitTempletBase.GetSkillCount(); i++)
			{
				string skillStrID = unitTempletBase.GetSkillStrID(i);
				if (!string.IsNullOrEmpty(skillStrID))
				{
					NKMUnitSkillTemplet skillTemplet = NKMUnitSkillManager.GetSkillTemplet(skillStrID, 1);
					if (skillTemplet != null)
					{
						if (skillTemplet.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_LEADER)
						{
							NKCUtil.SetImageSprite(this.m_imgRearmLeaderSkill, NKCUtil.GetSkillIconSprite(skillTemplet), false);
						}
						else
						{
							NKCUtil.SetImageSprite(this.m_lstSkillImg[num], NKCUtil.GetSkillIconSprite(skillTemplet), false);
							NKCUtil.SetGameobjectActive(this.m_lstSkillImg[num], true);
							num++;
						}
					}
				}
			}
			for (int j = num; j < this.m_lstSkillImg.Count; j++)
			{
				NKCUtil.SetGameobjectActive(this.m_lstSkillImg[j], false);
			}
			string hexRGB = this.m_defaultColor;
			foreach (NKMUnitRearmamentTemplet nkmunitRearmamentTemplet in NKMTempletContainer<NKMUnitRearmamentTemplet>.Values)
			{
				if (nkmunitRearmamentTemplet.Key == unitID)
				{
					if (!string.IsNullOrEmpty(nkmunitRearmamentTemplet.Color))
					{
						hexRGB = nkmunitRearmamentTemplet.Color;
						break;
					}
					break;
				}
			}
			foreach (Image image in this.m_lstPointColor)
			{
				Color color = NKCUtil.GetColor(hexRGB);
				color.a = image.color.a;
				NKCUtil.SetImageColor(image, color);
			}
			NKCUtil.SetLabelText(this.m_lbRearmUnitName, unitTempletBase.GetUnitTitle());
			NKCUtil.SetLabelText(this.m_lbRearmUnitDesc, unitTempletBase.GetUnitDesc());
			NKCUtil.SetGameobjectActive(this.m_objSkill, bSkill);
			NKCUtil.SetGameobjectActive(this.m_objInfo, !bSkill);
			this.m_CurUnitTempletBase = unitTempletBase;
		}

		// Token: 0x06007867 RID: 30823 RVA: 0x0027F6B0 File Offset: 0x0027D8B0
		private void ClickSkillInfo()
		{
			if (this.m_CurUnitTempletBase == null)
			{
				return;
			}
			NKMUnitData nkmunitData = new NKMUnitData();
			nkmunitData.m_UnitID = this.m_CurUnitTempletBase.m_UnitID;
			nkmunitData.m_UnitLevel = 1;
			nkmunitData.m_LimitBreakLevel = 0;
			NKCPopupSkillFullInfo.UnitInstance.OpenForUnit(nkmunitData, this.m_CurUnitTempletBase.GetUnitName(), this.m_CurUnitTempletBase.m_StarGradeMax, 0, this.m_CurUnitTempletBase.IsRearmUnit);
		}

		// Token: 0x040064ED RID: 25837
		[Header("info")]
		public GameObject m_objInfo;

		// Token: 0x040064EE RID: 25838
		public Text m_lbRearmUnitName;

		// Token: 0x040064EF RID: 25839
		public Text m_lbRearmUnitDesc;

		// Token: 0x040064F0 RID: 25840
		public Image m_imgRearmUnitFaceCard;

		// Token: 0x040064F1 RID: 25841
		[Header("Skill")]
		public GameObject m_objSkill;

		// Token: 0x040064F2 RID: 25842
		public Image m_imgRearmLeaderSkill;

		// Token: 0x040064F3 RID: 25843
		public List<Image> m_lstSkillImg;

		// Token: 0x040064F4 RID: 25844
		[Header("Etc")]
		public List<Image> m_lstPointColor;

		// Token: 0x040064F5 RID: 25845
		public string m_defaultColor = "#26216F";

		// Token: 0x040064F6 RID: 25846
		[Header("button")]
		public List<NKCUIComStateButton> m_csbtnSkill = new List<NKCUIComStateButton>();

		// Token: 0x040064F7 RID: 25847
		private bool bInit;

		// Token: 0x040064F8 RID: 25848
		private NKMUnitTempletBase m_CurUnitTempletBase;
	}
}
