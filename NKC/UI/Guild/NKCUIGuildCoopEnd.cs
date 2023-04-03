using System;
using ClientPacket.Guild;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B3A RID: 2874
	public class NKCUIGuildCoopEnd : NKCUIBase
	{
		// Token: 0x17001552 RID: 5458
		// (get) Token: 0x060082DC RID: 33500 RVA: 0x002C2684 File Offset: 0x002C0884
		public static NKCUIGuildCoopEnd Instance
		{
			get
			{
				if (NKCUIGuildCoopEnd.m_Instance == null)
				{
					NKCUIGuildCoopEnd.m_Instance = NKCUIManager.OpenNewInstance<NKCUIGuildCoopEnd>("AB_UI_NKM_UI_CONSORTIUM_COOP", "NKM_UI_POPUP_CONSORTIUM_COOP_END", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontCommon), null).GetInstance<NKCUIGuildCoopEnd>();
					if (NKCUIGuildCoopEnd.m_Instance != null)
					{
						NKCUIGuildCoopEnd.m_Instance.InitUI();
					}
				}
				return NKCUIGuildCoopEnd.m_Instance;
			}
		}

		// Token: 0x17001553 RID: 5459
		// (get) Token: 0x060082DD RID: 33501 RVA: 0x002C26DA File Offset: 0x002C08DA
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIGuildCoopEnd.m_Instance != null && NKCUIGuildCoopEnd.m_Instance.IsOpen;
			}
		}

		// Token: 0x17001554 RID: 5460
		// (get) Token: 0x060082DE RID: 33502 RVA: 0x002C26F5 File Offset: 0x002C08F5
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001555 RID: 5461
		// (get) Token: 0x060082DF RID: 33503 RVA: 0x002C26F8 File Offset: 0x002C08F8
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x17001556 RID: 5462
		// (get) Token: 0x060082E0 RID: 33504 RVA: 0x002C26FF File Offset: 0x002C08FF
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_GUILD_DUNGEON";
			}
		}

		// Token: 0x060082E1 RID: 33505 RVA: 0x002C2706 File Offset: 0x002C0906
		private void InitUI()
		{
			this.m_btnSeasonReward.PointerClick.RemoveAllListeners();
			this.m_btnSeasonReward.PointerClick.AddListener(new UnityAction(this.OnClickSeasonReward));
		}

		// Token: 0x060082E2 RID: 33506 RVA: 0x002C2734 File Offset: 0x002C0934
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060082E3 RID: 33507 RVA: 0x002C2742 File Offset: 0x002C0942
		private void OnDestroy()
		{
			NKCUIGuildCoopEnd.m_Instance = null;
		}

		// Token: 0x060082E4 RID: 33508 RVA: 0x002C274A File Offset: 0x002C094A
		public override void OnBackButton()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GUILD_LOBBY, true);
		}

		// Token: 0x060082E5 RID: 33509 RVA: 0x002C275C File Offset: 0x002C095C
		public void Open()
		{
			this.m_bIsSeasonEnd = (NKCGuildCoopManager.m_GuildDungeonState == GuildDungeonState.SeasonOut);
			this.m_fDeltaTime = 0f;
			if (this.m_bIsSeasonEnd)
			{
				NKCUtil.SetLabelText(this.m_lbTitle, NKCUtilString.GET_STRING_POPUP_CONSORTIUM_COOP_END_SUB_02_TEXT);
				NKCUtil.SetLabelText(this.m_lbSubTitle, NKCUtilString.GET_STRING_POPUP_CONSORTIUM_COOP_END_SUB_01_TEXT);
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbTitle, NKCUtilString.GET_STRING_POPUP_CONSORTIUM_COOP_SESSION_END_SUB_02_TEXT);
				NKCUtil.SetLabelText(this.m_lbSubTitle, NKCUtilString.GET_STRING_POPUP_CONSORTIUM_COOP_SESSION_END_SUB_01_TEXT);
			}
			this.RefreshSeasonRewardRedDot();
			this.SetRemainTime();
			base.UIOpened(true);
		}

		// Token: 0x060082E6 RID: 33510 RVA: 0x002C27E0 File Offset: 0x002C09E0
		private void SetRemainTime()
		{
			if (this.m_bIsSeasonEnd)
			{
				NKCUtil.SetLabelText(this.m_lbRemainTime, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_SEASION_END_INFORMATION_TEXT, NKCUtilString.GetRemainTimeString(NKCGuildCoopManager.m_NextSessionStartDateUTC, 2)));
				return;
			}
			NKCUtil.SetLabelText(this.m_lbRemainTime, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_SESSION_END_INFORMATION_TEXT, NKCUtilString.GetRemainTimeString(NKCGuildCoopManager.m_NextSessionStartDateUTC, 2)));
		}

		// Token: 0x060082E7 RID: 33511 RVA: 0x002C2836 File Offset: 0x002C0A36
		private void OnClickSeasonReward()
		{
			NKCPopupGuildCoopSeasonReward.Instance.Open(new NKCPopupGuildCoopSeasonReward.OnClose(this.RefreshSeasonRewardRedDot));
		}

		// Token: 0x060082E8 RID: 33512 RVA: 0x002C284E File Offset: 0x002C0A4E
		private void RefreshSeasonRewardRedDot()
		{
			NKCUtil.SetGameobjectActive(this.m_objSeasonRewardRedDot, NKCGuildCoopManager.CheckSeasonRewardEnable());
		}

		// Token: 0x060082E9 RID: 33513 RVA: 0x002C2860 File Offset: 0x002C0A60
		private void Update()
		{
			this.m_fDeltaTime += Time.deltaTime;
			if (this.m_fDeltaTime > 1f)
			{
				this.m_fDeltaTime -= 1f;
				this.SetRemainTime();
				if (NKCSynchronizedTime.IsFinished(NKCGuildCoopManager.m_NextSessionStartDateUTC) && !this.m_bDataRequested)
				{
					this.m_bDataRequested = true;
					NKCGuildCoopManager.ResetGuildCoopState();
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GUILD_LOBBY, true);
				}
			}
		}

		// Token: 0x04006F14 RID: 28436
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONSORTIUM_COOP";

		// Token: 0x04006F15 RID: 28437
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_CONSORTIUM_COOP_END";

		// Token: 0x04006F16 RID: 28438
		private static NKCUIGuildCoopEnd m_Instance;

		// Token: 0x04006F17 RID: 28439
		public Text m_lbTitle;

		// Token: 0x04006F18 RID: 28440
		public Text m_lbSubTitle;

		// Token: 0x04006F19 RID: 28441
		public Text m_lbRemainTime;

		// Token: 0x04006F1A RID: 28442
		public NKCUIComStateButton m_btnSeasonReward;

		// Token: 0x04006F1B RID: 28443
		public GameObject m_objSeasonRewardRedDot;

		// Token: 0x04006F1C RID: 28444
		private bool m_bIsSeasonEnd = true;

		// Token: 0x04006F1D RID: 28445
		private float m_fDeltaTime;

		// Token: 0x04006F1E RID: 28446
		private bool m_bDataRequested;
	}
}
