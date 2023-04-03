using System;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B63 RID: 2915
	public class NKCPopupGauntletLeagueEnterCondition : NKCUIBase
	{
		// Token: 0x1700159E RID: 5534
		// (get) Token: 0x06008518 RID: 34072 RVA: 0x002CFEBC File Offset: 0x002CE0BC
		public static NKCPopupGauntletLeagueEnterCondition Instance
		{
			get
			{
				if (NKCPopupGauntletLeagueEnterCondition.m_Instance == null)
				{
					NKCPopupGauntletLeagueEnterCondition.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupGauntletLeagueEnterCondition>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_ENTER_CONDITION", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupGauntletLeagueEnterCondition.CleanupInstance)).GetInstance<NKCPopupGauntletLeagueEnterCondition>();
					NKCPopupGauntletLeagueEnterCondition.m_Instance.Init();
				}
				return NKCPopupGauntletLeagueEnterCondition.m_Instance;
			}
		}

		// Token: 0x1700159F RID: 5535
		// (get) Token: 0x06008519 RID: 34073 RVA: 0x002CFF0B File Offset: 0x002CE10B
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupGauntletLeagueEnterCondition.m_Instance != null && NKCPopupGauntletLeagueEnterCondition.m_Instance.IsOpen;
			}
		}

		// Token: 0x0600851A RID: 34074 RVA: 0x002CFF26 File Offset: 0x002CE126
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupGauntletLeagueEnterCondition.m_Instance != null && NKCPopupGauntletLeagueEnterCondition.m_Instance.IsOpen)
			{
				NKCPopupGauntletLeagueEnterCondition.m_Instance.Close();
			}
		}

		// Token: 0x0600851B RID: 34075 RVA: 0x002CFF4B File Offset: 0x002CE14B
		private static void CleanupInstance()
		{
			NKCPopupGauntletLeagueEnterCondition.m_Instance = null;
		}

		// Token: 0x170015A0 RID: 5536
		// (get) Token: 0x0600851C RID: 34076 RVA: 0x002CFF53 File Offset: 0x002CE153
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170015A1 RID: 5537
		// (get) Token: 0x0600851D RID: 34077 RVA: 0x002CFF56 File Offset: 0x002CE156
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x0600851E RID: 34078 RVA: 0x002CFF5D File Offset: 0x002CE15D
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x0600851F RID: 34079 RVA: 0x002CFF6B File Offset: 0x002CE16B
		public void Init()
		{
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
		}

		// Token: 0x06008520 RID: 34080 RVA: 0x002CFF9C File Offset: 0x002CE19C
		public void Open()
		{
			NKCUtil.SetLabelText(this.m_lbUnitCount, string.Format(NKCUtilString.GET_STRING_GAUNTLET_LEAGUE_START_REQ_UNIT_POPUP_DESC, NKMPvpCommonConst.Instance.DraftBan.MinUnitCount));
			NKCUtil.SetLabelText(this.m_lbShipCount, string.Format(NKCUtilString.GET_STRING_GAUNTLET_LEAGUE_START_REQ_SHIP_POPUP_DESC, NKMPvpCommonConst.Instance.DraftBan.MinShipCount));
			if (NKCScenManager.CurrentUserData().m_ArmyData.GetUnitTypeCount() < NKMPvpCommonConst.Instance.DraftBan.MinUnitCount)
			{
				NKCUtil.SetLabelTextColor(this.m_lbUnitCount, NKCUtil.GetColor("#FF2626"));
			}
			else
			{
				NKCUtil.SetLabelTextColor(this.m_lbUnitCount, Color.white);
			}
			if (NKCScenManager.CurrentUserData().m_ArmyData.GetShipTypeCount() < NKMPvpCommonConst.Instance.DraftBan.MinShipCount)
			{
				NKCUtil.SetLabelTextColor(this.m_lbShipCount, NKCUtil.GetColor("#FF2626"));
			}
			else
			{
				NKCUtil.SetLabelTextColor(this.m_lbShipCount, Color.white);
			}
			base.UIOpened(true);
		}

		// Token: 0x0400718A RID: 29066
		public const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_GAUNTLET";

		// Token: 0x0400718B RID: 29067
		public const string UI_ASSET_NAME = "NKM_UI_ENTER_CONDITION";

		// Token: 0x0400718C RID: 29068
		private static NKCPopupGauntletLeagueEnterCondition m_Instance;

		// Token: 0x0400718D RID: 29069
		public Text m_lbUnitCount;

		// Token: 0x0400718E RID: 29070
		public Text m_lbShipCount;

		// Token: 0x0400718F RID: 29071
		public NKCUIComStateButton m_btnClose;
	}
}
