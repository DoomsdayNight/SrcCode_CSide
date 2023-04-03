using System;
using Cs.Logging;
using NKC.Templet;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Lobby
{
	// Token: 0x02000C0F RID: 3087
	public class NKCUILobbyMenuCurrentEvent : NKCUILobbyMenuButtonBase
	{
		// Token: 0x06008EE4 RID: 36580 RVA: 0x00309B91 File Offset: 0x00307D91
		public void Init()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnCurrentEvent, new UnityAction(this.OnBtn));
		}

		// Token: 0x06008EE5 RID: 36581 RVA: 0x00309BAC File Offset: 0x00307DAC
		protected override void ContentsUpdate(NKMUserData userData)
		{
			this.m_summaryTemplet = NKMEpisodeMgr.GetMainSummaryTemplet();
			if (this.m_summaryTemplet == null)
			{
				NKCUtil.SetGameobjectActive(this.m_imgCurrentEvent, false);
				return;
			}
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>(NKMAssetName.ParseBundleName("AB_UI_LOBBY_THUMB_EPISODE", this.m_summaryTemplet.m_LobbyResourceID));
			if (orLoadAssetResource == null)
			{
				Log.Error("[NKCUILobbyMenuCurrentEvent] summarty m_LobbyResourceID " + this.m_summaryTemplet.m_LobbyResourceID + " not found.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Lobby/NKCUILobbyMenuCurrentEvent.cs", 37);
				NKCUtil.SetGameobjectActive(this.m_imgCurrentEvent, false);
				return;
			}
			NKCUtil.SetImageSprite(this.m_imgCurrentEvent, orLoadAssetResource, false);
			NKCUtil.SetGameobjectActive(this.m_imgCurrentEvent, true);
		}

		// Token: 0x06008EE6 RID: 36582 RVA: 0x00309C49 File Offset: 0x00307E49
		private void OnBtn()
		{
			if (this.m_summaryTemplet == null)
			{
				return;
			}
			NKCContentManager.MoveToShortCut(this.m_summaryTemplet.m_ShortcutType, this.m_summaryTemplet.m_ShortcutParam, true);
		}

		// Token: 0x04007BED RID: 31725
		public NKCUIComStateButton m_csbtnCurrentEvent;

		// Token: 0x04007BEE RID: 31726
		public Image m_imgCurrentEvent;

		// Token: 0x04007BEF RID: 31727
		private NKCEpisodeSummaryTemplet m_summaryTemplet;
	}
}
