using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.HUD
{
	// Token: 0x02000C47 RID: 3143
	public class NKCUIGameUnitSkillCooltime : MonoBehaviour
	{
		// Token: 0x06009284 RID: 37508 RVA: 0x0032039D File Offset: 0x0031E59D
		public void SetSkillCoolVisible(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_objSkillCoolRoot, value);
			NKCUtil.SetGameobjectActive(this.m_objSkillCoolFx, false);
		}

		// Token: 0x06009285 RID: 37509 RVA: 0x003203B7 File Offset: 0x0031E5B7
		public void SetHyperCoolVisible(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_objHyperCoolRoot, value);
			NKCUtil.SetGameobjectActive(this.m_objHyperCoolFx, false);
		}

		// Token: 0x06009286 RID: 37510 RVA: 0x003203D4 File Offset: 0x0031E5D4
		public void SetUnit(NKMUnitTemplet unitTemplet, NKMUnitData unitData)
		{
			this.SetSkillCoolVisible(this.HaveUnlockedSkill(unitTemplet, unitData));
			this.SetHyperCoolVisible(this.HaveUnlockedHyper(unitTemplet, unitData));
			if (unitTemplet.m_UnitTempletBase.StopDefaultCoolTime)
			{
				NKCUtil.SetImageColor(this.m_imgSkillCool, NKCUtil.GetColor("#FFB830"));
				NKCUtil.SetImageColor(this.m_imgSkillCoolFxBlur, NKCUtil.GetColor("#FFB830"));
				NKCUtil.SetImageColor(this.m_imgHyperCool, NKCUtil.GetColor("#FF7F1B"));
				NKCUtil.SetImageColor(this.m_imgHyperCoolFxBlur, NKCUtil.GetColor("#FF7F1B"));
			}
			else
			{
				NKCUtil.SetImageColor(this.m_imgSkillCool, NKCUtil.GetColor("#008FFF"));
				NKCUtil.SetImageColor(this.m_imgSkillCoolFxBlur, NKCUtil.GetColor("#008FFF"));
				NKCUtil.SetImageColor(this.m_imgHyperCool, NKCUtil.GetColor("#9900FF"));
				NKCUtil.SetImageColor(this.m_imgHyperCoolFxBlur, NKCUtil.GetColor("#9900FF"));
			}
			NKCUtil.SetImageFillAmount(this.m_imgSkillCool, 0f);
			NKCUtil.SetImageFillAmount(this.m_imgHyperCool, 0f);
		}

		// Token: 0x06009287 RID: 37511 RVA: 0x003204D4 File Offset: 0x0031E6D4
		public void SetCooltime(float fSkillCoolRate, float fHyperCoolRate)
		{
			this.SetSkillCooltime(fSkillCoolRate);
			this.SetHyperCooltime(fHyperCoolRate);
		}

		// Token: 0x06009288 RID: 37512 RVA: 0x003204E4 File Offset: 0x0031E6E4
		public void SetSkillCooltime(float fSkillCoolNow, float fSkillCoolMax)
		{
			this.SetSkillCooltime(fSkillCoolNow / fSkillCoolMax);
		}

		// Token: 0x06009289 RID: 37513 RVA: 0x003204F0 File Offset: 0x0031E6F0
		public void SetSkillCooltime(float skillRateNow)
		{
			if (this.m_objSkillCoolRoot.activeSelf)
			{
				float num = 1f - skillRateNow;
				if (this.m_imgSkillCool.fillAmount < 1f && num >= 1f)
				{
					NKCUtil.SetGameobjectActive(this.m_objSkillCoolFx, true);
					if (this.m_objSkillCoolFx.activeInHierarchy)
					{
						this.m_animatorSkillCoolFx.Play("FULL");
					}
				}
				else if (num < 1f)
				{
					NKCUtil.SetGameobjectActive(this.m_objSkillCoolFx, false);
				}
				this.m_imgSkillCool.fillAmount = num;
			}
		}

		// Token: 0x0600928A RID: 37514 RVA: 0x00320577 File Offset: 0x0031E777
		public void SetHyperCooltime(float fHyperSkillCoolNow, float fHyperSkillMax)
		{
			this.SetHyperCooltime(fHyperSkillCoolNow / fHyperSkillMax);
		}

		// Token: 0x0600928B RID: 37515 RVA: 0x00320584 File Offset: 0x0031E784
		public void SetHyperCooltime(float hyperRateNow)
		{
			if (this.m_objHyperCoolRoot.activeSelf)
			{
				float num = 1f - hyperRateNow;
				if (this.m_imgHyperCool.fillAmount < 1f && num >= 1f)
				{
					NKCUtil.SetGameobjectActive(this.m_objHyperCoolFx, true);
					if (this.m_objHyperCoolFx.activeInHierarchy)
					{
						this.m_animatorHyperCoolFx.Play("FULL");
					}
				}
				else if (num < 1f)
				{
					NKCUtil.SetGameobjectActive(this.m_objHyperCoolFx, false);
				}
				this.m_imgHyperCool.fillAmount = num;
			}
		}

		// Token: 0x0600928C RID: 37516 RVA: 0x0032060C File Offset: 0x0031E80C
		private bool HaveUnlockedSkill(NKMUnitTemplet unitTemplet, NKMUnitData unitData)
		{
			if (unitTemplet == null)
			{
				return false;
			}
			if (unitData == null)
			{
				return false;
			}
			if (unitTemplet.m_listSkillStateData.Count <= 0)
			{
				return false;
			}
			if (unitTemplet.m_listSkillStateData[0] == null)
			{
				return false;
			}
			NKMUnitState unitState = unitTemplet.GetUnitState(unitTemplet.m_listSkillStateData[0].m_StateName, true);
			return unitState != null && unitData.IsUnitSkillUnlockedByType(unitState.m_NKM_SKILL_TYPE);
		}

		// Token: 0x0600928D RID: 37517 RVA: 0x00320670 File Offset: 0x0031E870
		private bool HaveUnlockedHyper(NKMUnitTemplet unitTemplet, NKMUnitData unitData)
		{
			if (unitTemplet == null)
			{
				return false;
			}
			if (unitData == null)
			{
				return false;
			}
			if (unitTemplet.m_listHyperSkillStateData.Count <= 0)
			{
				return false;
			}
			if (unitTemplet.m_listHyperSkillStateData[0] == null)
			{
				return false;
			}
			NKMUnitState unitState = unitTemplet.GetUnitState(unitTemplet.m_listHyperSkillStateData[0].m_StateName, true);
			return unitState != null && unitData.IsUnitSkillUnlockedByType(unitState.m_NKM_SKILL_TYPE);
		}

		// Token: 0x04007F84 RID: 32644
		[Header("��ų ��Ÿ��")]
		public GameObject m_objSkillCoolRoot;

		// Token: 0x04007F85 RID: 32645
		public Image m_imgSkillCool;

		// Token: 0x04007F86 RID: 32646
		public GameObject m_objSkillCoolFx;

		// Token: 0x04007F87 RID: 32647
		public Animator m_animatorSkillCoolFx;

		// Token: 0x04007F88 RID: 32648
		public Image m_imgSkillCoolFxBlur;

		// Token: 0x04007F89 RID: 32649
		[Header("������ ��Ÿ��")]
		public GameObject m_objHyperCoolRoot;

		// Token: 0x04007F8A RID: 32650
		public Image m_imgHyperCool;

		// Token: 0x04007F8B RID: 32651
		public GameObject m_objHyperCoolFx;

		// Token: 0x04007F8C RID: 32652
		public Animator m_animatorHyperCoolFx;

		// Token: 0x04007F8D RID: 32653
		public Image m_imgHyperCoolFxBlur;
	}
}
