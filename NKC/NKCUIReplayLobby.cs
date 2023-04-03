using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x0200090E RID: 2318
	public class NKCUIReplayLobby : NKCUIBase
	{
		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x06005CA8 RID: 23720 RVA: 0x001CAA8E File Offset: 0x001C8C8E
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_REPLAY;
			}
		}

		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x06005CA9 RID: 23721 RVA: 0x001CAA95 File Offset: 0x001C8C95
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x06005CAA RID: 23722 RVA: 0x001CAA98 File Offset: 0x001C8C98
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Normal;
			}
		}

		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x06005CAB RID: 23723 RVA: 0x001CAA9B File Offset: 0x001C8C9B
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_REPLAY";
			}
		}

		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x06005CAC RID: 23724 RVA: 0x001CAAA2 File Offset: 0x001C8CA2
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return new List<int>
				{
					101
				};
			}
		}

		// Token: 0x06005CAD RID: 23725 RVA: 0x001CAAB1 File Offset: 0x001C8CB1
		public static NKCAssetResourceData OpenInstanceAsync()
		{
			return NKCUIBase.OpenInstanceAsync<NKCUIBaseSceneMenu>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_LOBBY");
		}

		// Token: 0x06005CAE RID: 23726 RVA: 0x001CAAC2 File Offset: 0x001C8CC2
		public static bool CheckInstanceLoaded(NKCAssetResourceData loadResourceData, out NKCUIReplayLobby retVal)
		{
			return NKCUIBase.CheckInstanceLoaded<NKCUIReplayLobby>(loadResourceData, NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontCommon), out retVal);
		}

		// Token: 0x06005CAF RID: 23727 RVA: 0x001CAAD1 File Offset: 0x001C8CD1
		public void CloseInstance()
		{
			NKCAssetResourceManager.CloseResource("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_LOBBY");
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06005CB0 RID: 23728 RVA: 0x001CAAEE File Offset: 0x001C8CEE
		public void InitUI()
		{
			if (this.m_bInit)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_bInit = true;
		}

		// Token: 0x06005CB1 RID: 23729 RVA: 0x001CAB0C File Offset: 0x001C8D0C
		private void ResetUIByCurrTab()
		{
			base.UpdateUpsideMenu();
		}

		// Token: 0x06005CB2 RID: 23730 RVA: 0x001CAB14 File Offset: 0x001C8D14
		public void Open()
		{
			this.CheckTutorial();
		}

		// Token: 0x06005CB3 RID: 23731 RVA: 0x001CAB1C File Offset: 0x001C8D1C
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06005CB4 RID: 23732 RVA: 0x001CAB2A File Offset: 0x001C8D2A
		public override void OnBackButton()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_INTRO, true);
		}

		// Token: 0x06005CB5 RID: 23733 RVA: 0x001CAB39 File Offset: 0x001C8D39
		private void CheckTutorial()
		{
		}

		// Token: 0x040048EB RID: 18667
		public Animator m_amtorLeft;

		// Token: 0x040048EC RID: 18668
		[Header("Fallback BG")]
		public GameObject m_objBGFallBack;

		// Token: 0x040048ED RID: 18669
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_GAUNTLET";

		// Token: 0x040048EE RID: 18670
		private const string UI_ASSET_NAME = "NKM_UI_GAUNTLET_LOBBY";

		// Token: 0x040048EF RID: 18671
		private bool m_bInit;
	}
}
