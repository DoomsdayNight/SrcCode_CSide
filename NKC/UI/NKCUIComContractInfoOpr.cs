using System;
using System.Collections.Generic;
using NKC.UI.Collection;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000952 RID: 2386
	public class NKCUIComContractInfoOpr : MonoBehaviour
	{
		// Token: 0x06005F23 RID: 24355 RVA: 0x001D8FB4 File Offset: 0x001D71B4
		public void Awake()
		{
			if (this.m_btnDetail != null)
			{
				this.m_btnDetail.PointerClick.RemoveAllListeners();
				this.m_btnDetail.PointerClick.AddListener(new UnityAction(this.OnClickDetail));
			}
			if (!this.m_bSetDataOnStart)
			{
				this.SetData();
			}
		}

		// Token: 0x06005F24 RID: 24356 RVA: 0x001D9009 File Offset: 0x001D7209
		private void Start()
		{
			if (this.m_bSetDataOnStart)
			{
				this.SetData();
			}
		}

		// Token: 0x06005F25 RID: 24357 RVA: 0x001D901C File Offset: 0x001D721C
		public void SetData()
		{
			this.m_UnitTempletBase = NKMUnitTempletBase.Find(this.m_OperatorStrID);
			if (this.m_UnitTempletBase == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			if (this.m_UnitTempletBase.m_NKM_UNIT_TYPE != NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			NKCUtil.SetImageSprite(this.m_imgGrade, this.GetSpriteUnitGrade(this.m_UnitTempletBase.m_NKM_UNIT_GRADE), false);
			NKCUtil.SetLabelText(this.m_lbName, this.m_UnitTempletBase.GetUnitName());
			NKMOperatorSkillTemplet skillTemplet = NKCOperatorUtil.GetSkillTemplet(this.m_UnitTempletBase.m_lstSkillStrID[0]);
			if (skillTemplet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			if (skillTemplet.m_OperSkillType == OperatorSkillType.m_Tactical)
			{
				NKMTacticalCommandTemplet tacticalCommandTempletByStrID = NKMTacticalCommandManager.GetTacticalCommandTempletByStrID(skillTemplet.m_OperSkillTarget);
				if (tacticalCommandTempletByStrID.m_listComboType != null && tacticalCommandTempletByStrID.m_listComboType.Count > 0)
				{
					List<NKMTacticalCombo> listComboType = tacticalCommandTempletByStrID.m_listComboType;
					for (int i = 0; i < this.m_lstComboSlot.Count; i++)
					{
						if (listComboType.Count <= i)
						{
							NKCUtil.SetGameobjectActive(this.m_lstComboSlot[i].gameObject, false);
						}
						else
						{
							NKCUtil.SetGameobjectActive(this.m_lstComboSlot[i].gameObject, true);
							NKCUtil.SetImageSprite(this.m_lstComboSlot[i].m_img, this.GetUnitRoleIconAssetName(listComboType[i].m_NKM_UNIT_ROLE_TYPE), false);
						}
					}
				}
			}
			if (this.m_Skill != null)
			{
				this.m_Skill.SetDataForCollection(this.m_UnitTempletBase.m_UnitID, -1);
			}
			NKCUtil.SetGameobjectActive(this.m_objNotHave, NKCScenManager.CurrentUserData().m_ArmyData.GetUnitCountByID(this.m_UnitTempletBase.m_UnitID) == 0);
		}

		// Token: 0x06005F26 RID: 24358 RVA: 0x001D91C0 File Offset: 0x001D73C0
		private Sprite GetSpriteUnitGrade(NKM_UNIT_GRADE grade)
		{
			string assetName;
			switch (grade)
			{
			default:
				assetName = "BANNER_COMMON_PREFAB_RANK_N";
				break;
			case NKM_UNIT_GRADE.NUG_R:
				assetName = "BANNER_COMMON_PREFAB_RANK_R";
				break;
			case NKM_UNIT_GRADE.NUG_SR:
				assetName = "BANNER_COMMON_PREFAB_RANK_SR";
				break;
			case NKM_UNIT_GRADE.NUG_SSR:
				assetName = "BANNER_COMMON_PREFAB_RANK_SSR";
				break;
			}
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("BANNER_COMMON_PREFAB_Sprite", assetName, false);
		}

		// Token: 0x06005F27 RID: 24359 RVA: 0x001D9214 File Offset: 0x001D7414
		private Sprite GetUnitRoleIconAssetName(NKM_UNIT_ROLE_TYPE roleType)
		{
			string assetName;
			switch (roleType)
			{
			case NKM_UNIT_ROLE_TYPE.NURT_STRIKER:
				assetName = "BANNER_COMMON_PREFAB_CLASS_STRIKER";
				goto IL_6C;
			case NKM_UNIT_ROLE_TYPE.NURT_RANGER:
				assetName = "BANNER_COMMON_PREFAB_CLASS_RANGER";
				goto IL_6C;
			case NKM_UNIT_ROLE_TYPE.NURT_DEFENDER:
				assetName = "BANNER_COMMON_PREFAB_CLASS_DEFENCE";
				goto IL_6C;
			case NKM_UNIT_ROLE_TYPE.NURT_SNIPER:
				assetName = "BANNER_COMMON_PREFAB_CLASS_SNIPER";
				goto IL_6C;
			case NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER:
				assetName = "BANNER_COMMON_PREFAB_CLASS_SUPPORTER";
				goto IL_6C;
			case NKM_UNIT_ROLE_TYPE.NURT_SIEGE:
				assetName = "BANNER_COMMON_PREFAB_CLASS_SIEGE";
				goto IL_6C;
			case NKM_UNIT_ROLE_TYPE.NURT_TOWER:
				assetName = "BANNER_COMMON_PREFAB_CLASS_TOWER";
				goto IL_6C;
			}
			assetName = "";
			IL_6C:
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("BANNER_COMMON_PREFAB_Sprite", assetName, false);
		}

		// Token: 0x06005F28 RID: 24360 RVA: 0x001D929C File Offset: 0x001D749C
		private void OnClickDetail()
		{
			NKMOperator dummyOperator = NKCOperatorUtil.GetDummyOperator(this.m_UnitTempletBase.m_UnitID, true);
			NKCUICollectionOperatorInfo.Instance.Open(dummyOperator, null, NKCUICollectionOperatorInfo.eCollectionState.CS_PROFILE, NKCUIUpsideMenu.eMode.BackButtonOnly, false, false);
		}

		// Token: 0x04004B39 RID: 19257
		private const string SpriteBundleName = "BANNER_COMMON_PREFAB_Sprite";

		// Token: 0x04004B3A RID: 19258
		public string m_OperatorStrID;

		// Token: 0x04004B3B RID: 19259
		public bool m_bSetDataOnStart;

		// Token: 0x04004B3C RID: 19260
		[Header("등급")]
		public Image m_imgGrade;

		// Token: 0x04004B3D RID: 19261
		[Header("오퍼레이터 이름")]
		public Text m_lbName;

		// Token: 0x04004B3E RID: 19262
		[Header("상세보기 버튼")]
		public NKCUIComStateButton m_btnDetail;

		// Token: 0x04004B3F RID: 19263
		[Header("스킬")]
		public NKCUIOperatorSkill m_Skill;

		// Token: 0x04004B40 RID: 19264
		[Header("스킬 콤보")]
		public List<NKCGameHudComboSlot> m_lstComboSlot;

		// Token: 0x04004B41 RID: 19265
		[Header("미획득 표기")]
		public GameObject m_objNotHave;

		// Token: 0x04004B42 RID: 19266
		private NKMUnitTempletBase m_UnitTempletBase;
	}
}
