using System;

namespace NKC.UI
{
	// Token: 0x0200097E RID: 2430
	public class NKCUICutscenDungeon : NKCUIBase
	{
		// Token: 0x060062EB RID: 25323 RVA: 0x001F1F7A File Offset: 0x001F017A
		public static NKCUIManager.LoadedUIData OpenNewInstance()
		{
			if (!NKCUIManager.IsValid(NKCUICutscenDungeon.s_LoadedUIData))
			{
				NKCUICutscenDungeon.s_LoadedUIData = NKCUIManager.OpenNewInstance<NKCUICutscenDungeon>("ab_ui_nkm_ui_cutscen", "NKM_CUTSCEN_DUNGEON_Panel", NKCUIManager.eUIBaseRect.UIFrontCommon, null);
			}
			return NKCUICutscenDungeon.s_LoadedUIData;
		}

		// Token: 0x060062EC RID: 25324 RVA: 0x001F1FA3 File Offset: 0x001F01A3
		private void OnDestroy()
		{
			NKCUICutscenDungeon.s_LoadedUIData = null;
		}

		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x060062ED RID: 25325 RVA: 0x001F1FAB File Offset: 0x001F01AB
		public override string MenuName
		{
			get
			{
				return "컷신 던전";
			}
		}

		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x060062EE RID: 25326 RVA: 0x001F1FB2 File Offset: 0x001F01B2
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x060062EF RID: 25327 RVA: 0x001F1FB5 File Offset: 0x001F01B5
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x060062F0 RID: 25328 RVA: 0x001F1FB8 File Offset: 0x001F01B8
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			if (NKCUICutScenPlayer.HasInstance)
			{
				NKCUICutScenPlayer.Instance.StopWithCallBack();
			}
		}

		// Token: 0x060062F1 RID: 25329 RVA: 0x001F1FD7 File Offset: 0x001F01D7
		public void Open()
		{
			base.UIOpened(true);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060062F2 RID: 25330 RVA: 0x001F1FEC File Offset: 0x001F01EC
		public override void OnBackButton()
		{
			if (NKCUICutScenPlayer.IsInstanceOpen && NKCUICutScenPlayer.Instance.IsPlaying())
			{
				NKCUICutScenPlayer.Instance.StopWithCallBack();
			}
		}

		// Token: 0x04004EAD RID: 20141
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_cutscen";

		// Token: 0x04004EAE RID: 20142
		private const string UI_ASSET_NAME = "NKM_CUTSCEN_DUNGEON_Panel";

		// Token: 0x04004EAF RID: 20143
		private static NKCUIManager.LoadedUIData s_LoadedUIData;
	}
}
