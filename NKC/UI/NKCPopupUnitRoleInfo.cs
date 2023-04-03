using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NKC.UI
{
	// Token: 0x02000A8F RID: 2703
	public class NKCPopupUnitRoleInfo : NKCUIBase
	{
		// Token: 0x17001401 RID: 5121
		// (get) Token: 0x060077A1 RID: 30625 RVA: 0x0027C540 File Offset: 0x0027A740
		public static NKCPopupUnitRoleInfo Instance
		{
			get
			{
				if (NKCPopupUnitRoleInfo.m_Instance == null)
				{
					NKCPopupUnitRoleInfo.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupUnitRoleInfo>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_UNIT_INFOPOPUP", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupUnitRoleInfo.CleanupInstance)).GetInstance<NKCPopupUnitRoleInfo>();
					NKCPopupUnitRoleInfo.m_Instance.InitUI();
				}
				return NKCPopupUnitRoleInfo.m_Instance;
			}
		}

		// Token: 0x060077A2 RID: 30626 RVA: 0x0027C58F File Offset: 0x0027A78F
		private static void CleanupInstance()
		{
			NKCPopupUnitRoleInfo.m_Instance = null;
		}

		// Token: 0x17001402 RID: 5122
		// (get) Token: 0x060077A3 RID: 30627 RVA: 0x0027C597 File Offset: 0x0027A797
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001403 RID: 5123
		// (get) Token: 0x060077A4 RID: 30628 RVA: 0x0027C59A File Offset: 0x0027A79A
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_UNIT_ROLE_INFO;
			}
		}

		// Token: 0x060077A5 RID: 30629 RVA: 0x0027C5A4 File Offset: 0x0027A7A4
		private void InitUI()
		{
			if (this.m_bInitComplete)
			{
				return;
			}
			this.m_openAni = new NKCUIOpenAnimator(base.gameObject);
			this.m_lstTopToggle[0].OnValueChanged.RemoveAllListeners();
			this.m_lstTopToggle[0].OnValueChanged.AddListener(delegate(bool <p0>)
			{
				this.OnClickTab(0);
			});
			this.m_lstTopToggle[1].OnValueChanged.RemoveAllListeners();
			this.m_lstTopToggle[1].OnValueChanged.AddListener(delegate(bool <p0>)
			{
				this.OnClickTab(1);
			});
			this.m_btnClose.PointerDown.RemoveAllListeners();
			this.m_btnClose.PointerDown.AddListener(delegate(PointerEventData eventData)
			{
				this.OnClickClose();
			});
			this.m_btnBG.PointerClick.RemoveAllListeners();
			this.m_btnBG.PointerClick.AddListener(new UnityAction(this.OnClickClose));
			this.m_bInitComplete = true;
		}

		// Token: 0x060077A6 RID: 30630 RVA: 0x0027C69A File Offset: 0x0027A89A
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060077A7 RID: 30631 RVA: 0x0027C6A8 File Offset: 0x0027A8A8
		public void OpenDefaultPopup()
		{
			if (this.m_openAni != null)
			{
				this.m_openAni.PlayOpenAni();
			}
			NKCUtil.SetGameobjectActive(this.m_objStyleCounter, false);
			NKCUtil.SetGameobjectActive(this.m_objStyleSoldier, false);
			NKCUtil.SetGameobjectActive(this.m_objStyleMechanic, false);
			NKCUtil.SetGameobjectActive(this.m_objStyleEtc, false);
			NKCUtil.SetGameobjectActive(this.m_objRoleStriker, false);
			NKCUtil.SetGameobjectActive(this.m_objRoleRanger, false);
			NKCUtil.SetGameobjectActive(this.m_objRoleSniper, false);
			NKCUtil.SetGameobjectActive(this.m_objRoleDefender, false);
			NKCUtil.SetGameobjectActive(this.m_objRoleSiege, false);
			NKCUtil.SetGameobjectActive(this.m_objRoleSupporter, false);
			NKCUtil.SetGameobjectActive(this.m_objRoleTower, false);
			NKCUtil.SetGameobjectActive(this.m_objAttackTypeGround, false);
			NKCUtil.SetGameobjectActive(this.m_objAttackTypeAir, false);
			NKCUtil.SetGameobjectActive(this.m_objAttackTypeAll, false);
			NKCUtil.SetGameobjectActive(this.m_objPatrol, false);
			NKCUtil.SetGameobjectActive(this.m_objSwingby, false);
			NKCUtil.SetGameobjectActive(this.m_objRespawnFreePos, false);
			NKCUtil.SetGameobjectActive(this.m_objRevenge, false);
			NKCUtil.SetGameobjectActive(this.m_objFury, false);
			base.UIOpened(true);
		}

		// Token: 0x060077A8 RID: 30632 RVA: 0x0027C7B3 File Offset: 0x0027A9B3
		public void OpenPopup(NKMUnitData unitData)
		{
			this.OpenPopup(NKMUnitManager.GetUnitTempletBase(unitData));
		}

		// Token: 0x060077A9 RID: 30633 RVA: 0x0027C7C4 File Offset: 0x0027A9C4
		public void OpenPopup(NKMUnitTempletBase unitTempletBase)
		{
			if (!this.m_bInitComplete)
			{
				this.InitUI();
			}
			if (unitTempletBase == null)
			{
				Debug.LogError("unitTempletBase is null");
				return;
			}
			if (this.m_openAni != null)
			{
				this.m_openAni.PlayOpenAni();
			}
			this.SetUnitStyleType(unitTempletBase.m_NKM_UNIT_STYLE_TYPE);
			this.SetUnitRoleType(unitTempletBase.m_NKM_UNIT_ROLE_TYPE);
			if (unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc == NKM_FIND_TARGET_TYPE.NFTT_INVALID)
			{
				this.SetAttackType(unitTempletBase.m_NKM_FIND_TARGET_TYPE);
			}
			else
			{
				this.SetAttackType(unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc);
			}
			NKCUtil.SetGameobjectActive(this.m_objPatrol, unitTempletBase.m_bTagPatrol);
			NKCUtil.SetGameobjectActive(this.m_objSwingby, unitTempletBase.m_bTagSwingby);
			NKCUtil.SetGameobjectActive(this.m_objRespawnFreePos, unitTempletBase.m_bRespawnFreePos);
			NKCUtil.SetGameobjectActive(this.m_objRevenge, unitTempletBase.m_bTagRevenge);
			NKCUtil.SetGameobjectActive(this.m_objFury, unitTempletBase.StopDefaultCoolTime);
			this.m_lstTopToggle[0].Select(true, false, false);
			this.OnClickTab(0);
			base.UIOpened(true);
		}

		// Token: 0x060077AA RID: 30634 RVA: 0x0027C8B2 File Offset: 0x0027AAB2
		private void Update()
		{
			if (base.IsOpen && this.m_openAni != null)
			{
				this.m_openAni.Update();
			}
		}

		// Token: 0x060077AB RID: 30635 RVA: 0x0027C8D0 File Offset: 0x0027AAD0
		private void SetUnitStyleType(NKM_UNIT_STYLE_TYPE styleType)
		{
			NKCUtil.SetGameobjectActive(this.m_objStyleCounter, styleType == NKM_UNIT_STYLE_TYPE.NUST_COUNTER);
			NKCUtil.SetGameobjectActive(this.m_objStyleSoldier, styleType == NKM_UNIT_STYLE_TYPE.NUST_SOLDIER);
			NKCUtil.SetGameobjectActive(this.m_objStyleMechanic, styleType == NKM_UNIT_STYLE_TYPE.NUST_MECHANIC);
			if (styleType != NKM_UNIT_STYLE_TYPE.NUST_COUNTER && styleType != NKM_UNIT_STYLE_TYPE.NUST_SOLDIER && styleType != NKM_UNIT_STYLE_TYPE.NUST_MECHANIC)
			{
				NKCUtil.SetGameobjectActive(this.m_objStyleEtc, true);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objStyleEtc, false);
		}

		// Token: 0x060077AC RID: 30636 RVA: 0x0027C930 File Offset: 0x0027AB30
		private void SetUnitRoleType(NKM_UNIT_ROLE_TYPE roleType)
		{
			NKCUtil.SetGameobjectActive(this.m_objRoleStriker, roleType == NKM_UNIT_ROLE_TYPE.NURT_STRIKER);
			NKCUtil.SetGameobjectActive(this.m_objRoleRanger, roleType == NKM_UNIT_ROLE_TYPE.NURT_RANGER);
			NKCUtil.SetGameobjectActive(this.m_objRoleSniper, roleType == NKM_UNIT_ROLE_TYPE.NURT_SNIPER);
			NKCUtil.SetGameobjectActive(this.m_objRoleDefender, roleType == NKM_UNIT_ROLE_TYPE.NURT_DEFENDER);
			NKCUtil.SetGameobjectActive(this.m_objRoleSiege, roleType == NKM_UNIT_ROLE_TYPE.NURT_SIEGE);
			NKCUtil.SetGameobjectActive(this.m_objRoleSupporter, roleType == NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER);
			NKCUtil.SetGameobjectActive(this.m_objRoleTower, roleType == NKM_UNIT_ROLE_TYPE.NURT_TOWER);
		}

		// Token: 0x060077AD RID: 30637 RVA: 0x0027C9A8 File Offset: 0x0027ABA8
		private void SetAttackType(NKM_FIND_TARGET_TYPE attackType)
		{
			NKCUtil.SetGameobjectActive(this.m_objAttackTypeGround, false);
			NKCUtil.SetGameobjectActive(this.m_objAttackTypeAir, false);
			NKCUtil.SetGameobjectActive(this.m_objAttackTypeAll, false);
			switch (attackType)
			{
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY:
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_AIR_FIRST:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_BOSS_LAST:
			case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_ONLY:
			case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS:
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM:
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LOW_HP:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_MY_TEAM:
				NKCUtil.SetGameobjectActive(this.m_objAttackTypeAll, true);
				return;
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_LAND:
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_LAND_RANGER_SUPPORTER_SNIPER_FIRST:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_LAND:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_LAND_BOSS_LAST:
			case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_LAND:
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LAND:
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LOW_HP_LAND:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_MY_TEAM_LAND:
				NKCUtil.SetGameobjectActive(this.m_objAttackTypeGround, true);
				return;
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_AIR:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_AIR:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_AIR_BOSS_LAST:
			case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_AIR:
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_AIR:
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LOW_HP_AIR:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_MY_TEAM_AIR:
				NKCUtil.SetGameobjectActive(this.m_objAttackTypeAir, true);
				return;
			default:
				return;
			}
		}

		// Token: 0x060077AE RID: 30638 RVA: 0x0027CA68 File Offset: 0x0027AC68
		public void OnClickTab(int tabIndex)
		{
			for (int i = 0; i < this.m_lstPage.Count; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_lstPage[i], tabIndex == i);
			}
		}

		// Token: 0x060077AF RID: 30639 RVA: 0x0027CAA0 File Offset: 0x0027ACA0
		public void OnClickClose()
		{
			base.Close();
		}

		// Token: 0x04006435 RID: 25653
		private const string UI_ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_ok_cancel_box";

		// Token: 0x04006436 RID: 25654
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_UNIT_INFOPOPUP";

		// Token: 0x04006437 RID: 25655
		private static NKCPopupUnitRoleInfo m_Instance;

		// Token: 0x04006438 RID: 25656
		[Header("탑 메뉴")]
		public List<NKCUIComToggle> m_lstTopToggle = new List<NKCUIComToggle>();

		// Token: 0x04006439 RID: 25657
		public NKCUIComStateButton m_btnClose;

		// Token: 0x0400643A RID: 25658
		public NKCUIComStateButton m_btnBG;

		// Token: 0x0400643B RID: 25659
		[Header("페이지 오브젝트")]
		public List<GameObject> m_lstPage = new List<GameObject>();

		// Token: 0x0400643C RID: 25660
		[Header("유닛타입")]
		public GameObject m_objStyleCounter;

		// Token: 0x0400643D RID: 25661
		public GameObject m_objStyleSoldier;

		// Token: 0x0400643E RID: 25662
		public GameObject m_objStyleMechanic;

		// Token: 0x0400643F RID: 25663
		public GameObject m_objStyleEtc;

		// Token: 0x04006440 RID: 25664
		[Header("롤 타입")]
		public GameObject m_objRoleStriker;

		// Token: 0x04006441 RID: 25665
		public GameObject m_objRoleRanger;

		// Token: 0x04006442 RID: 25666
		public GameObject m_objRoleSniper;

		// Token: 0x04006443 RID: 25667
		public GameObject m_objRoleDefender;

		// Token: 0x04006444 RID: 25668
		public GameObject m_objRoleSiege;

		// Token: 0x04006445 RID: 25669
		public GameObject m_objRoleSupporter;

		// Token: 0x04006446 RID: 25670
		public GameObject m_objRoleTower;

		// Token: 0x04006447 RID: 25671
		[Header("공격 타입")]
		public GameObject m_objAttackTypeGround;

		// Token: 0x04006448 RID: 25672
		public GameObject m_objAttackTypeAir;

		// Token: 0x04006449 RID: 25673
		public GameObject m_objAttackTypeAll;

		// Token: 0x0400644A RID: 25674
		[Header("태그")]
		public GameObject m_objPatrol;

		// Token: 0x0400644B RID: 25675
		public GameObject m_objSwingby;

		// Token: 0x0400644C RID: 25676
		public GameObject m_objRespawnFreePos;

		// Token: 0x0400644D RID: 25677
		public GameObject m_objRevenge;

		// Token: 0x0400644E RID: 25678
		public GameObject m_objFury;

		// Token: 0x0400644F RID: 25679
		private bool m_bInitComplete;

		// Token: 0x04006450 RID: 25680
		private NKCUIOpenAnimator m_openAni;
	}
}
