using System;
using NKC.UI.Collection;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Contract
{
	// Token: 0x02000BE8 RID: 3048
	public class NKCUIComContractInfo : MonoBehaviour
	{
		// Token: 0x06008D62 RID: 36194 RVA: 0x00301598 File Offset: 0x002FF798
		public void Awake()
		{
			if (this.m_btnDetail != null)
			{
				this.m_btnDetail.PointerClick.RemoveAllListeners();
				this.m_btnDetail.PointerClick.AddListener(new UnityAction(this.OnClickDetail));
			}
			this.SetData();
		}

		// Token: 0x06008D63 RID: 36195 RVA: 0x003015E5 File Offset: 0x002FF7E5
		private void OnEnable()
		{
			this.SetData();
		}

		// Token: 0x06008D64 RID: 36196 RVA: 0x003015F0 File Offset: 0x002FF7F0
		public void SetData()
		{
			this.m_UnitTempletBase = NKMUnitTempletBase.Find(this.m_UnitStrID);
			if (this.m_UnitTempletBase == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			NKCUtil.SetImageSprite(this.m_imgRankIcon, this.GetSpriteUnitGrade(this.m_UnitTempletBase.m_NKM_UNIT_GRADE), false);
			NKCUtil.SetGameobjectActive(this.m_objAwakenFX, this.m_UnitTempletBase.m_bAwaken);
			NKCUtil.SetLabelText(this.m_lbName, this.m_UnitTempletBase.GetUnitName());
			NKCUtil.SetImageSprite(this.m_imgClass, this.GetUnitRoleIconAssetName(this.m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE), false);
			NKCUtil.SetLabelText(this.m_lbClass, NKCUtilString.GetRoleText(this.m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE, false));
			NKCUtil.SetGameobjectActive(this.m_lbSubName, this.m_UnitTempletBase.m_bAwaken);
			NKCUtil.SetLabelText(this.m_lbSubName, this.m_UnitTempletBase.GetUnitTitle());
			NKCUtil.SetGameobjectActive(this.m_objNotHave, !NKCScenManager.CurrentUserData().m_ArmyData.HaveUnit(this.m_UnitTempletBase.m_UnitID, true));
		}

		// Token: 0x06008D65 RID: 36197 RVA: 0x003016FC File Offset: 0x002FF8FC
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

		// Token: 0x06008D66 RID: 36198 RVA: 0x00301750 File Offset: 0x002FF950
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

		// Token: 0x06008D67 RID: 36199 RVA: 0x003017D5 File Offset: 0x002FF9D5
		private void OnClickDetail()
		{
			NKCUICollectionUnitInfo.CheckInstanceAndOpen(NKCUtil.MakeDummyUnit(this.m_UnitTempletBase.m_UnitID, true), null, null, NKCUICollectionUnitInfo.eCollectionState.CS_PROFILE, false, NKCUIUpsideMenu.eMode.BackButtonOnly);
		}

		// Token: 0x04007A31 RID: 31281
		private const string SpriteBundleName = "BANNER_COMMON_PREFAB_Sprite";

		// Token: 0x04007A32 RID: 31282
		public string m_UnitStrID;

		// Token: 0x04007A33 RID: 31283
		public Image m_imgRankIcon;

		// Token: 0x04007A34 RID: 31284
		public GameObject m_objAwakenFX;

		// Token: 0x04007A35 RID: 31285
		public Text m_lbName;

		// Token: 0x04007A36 RID: 31286
		public Text m_lbSubName;

		// Token: 0x04007A37 RID: 31287
		public Image m_imgClass;

		// Token: 0x04007A38 RID: 31288
		public Text m_lbClass;

		// Token: 0x04007A39 RID: 31289
		public NKCUIComStateButton m_btnDetail;

		// Token: 0x04007A3A RID: 31290
		[Header("미획득 표기")]
		public GameObject m_objNotHave;

		// Token: 0x04007A3B RID: 31291
		private NKMUnitTempletBase m_UnitTempletBase;
	}
}
