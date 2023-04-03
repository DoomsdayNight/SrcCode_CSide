using System;
using NKC.PacketHandler;
using NKC.UI.Guide;
using NKM;
using NKM.Guild;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B2C RID: 2860
	public class NKCPopupGuildCoopBossInfo : NKCUIBase
	{
		// Token: 0x17001538 RID: 5432
		// (get) Token: 0x06008234 RID: 33332 RVA: 0x002BECA4 File Offset: 0x002BCEA4
		public static NKCPopupGuildCoopBossInfo Instance
		{
			get
			{
				if (NKCPopupGuildCoopBossInfo.m_Instance == null)
				{
					NKCPopupGuildCoopBossInfo.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupGuildCoopBossInfo>("AB_UI_NKM_UI_CONSORTIUM_COOP", "NKM_UI_POPUP_CONSORTIUM_COOP_BOSS_INFO", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupGuildCoopBossInfo.CleanupInstance)).GetInstance<NKCPopupGuildCoopBossInfo>();
					if (NKCPopupGuildCoopBossInfo.m_Instance != null)
					{
						NKCPopupGuildCoopBossInfo.m_Instance.InitUI();
					}
				}
				return NKCPopupGuildCoopBossInfo.m_Instance;
			}
		}

		// Token: 0x17001539 RID: 5433
		// (get) Token: 0x06008235 RID: 33333 RVA: 0x002BED05 File Offset: 0x002BCF05
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupGuildCoopBossInfo.m_Instance != null && NKCPopupGuildCoopBossInfo.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008236 RID: 33334 RVA: 0x002BED20 File Offset: 0x002BCF20
		private static void CleanupInstance()
		{
			NKCPopupGuildCoopBossInfo.m_Instance = null;
		}

		// Token: 0x1700153A RID: 5434
		// (get) Token: 0x06008237 RID: 33335 RVA: 0x002BED28 File Offset: 0x002BCF28
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700153B RID: 5435
		// (get) Token: 0x06008238 RID: 33336 RVA: 0x002BED2B File Offset: 0x002BCF2B
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06008239 RID: 33337 RVA: 0x002BED34 File Offset: 0x002BCF34
		public void InitUI()
		{
			if (this.m_btnClose != null)
			{
				this.m_btnClose.PointerClick.RemoveAllListeners();
				this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			}
			if (this.m_btnDetail != null)
			{
				this.m_btnDetail.PointerClick.RemoveAllListeners();
				this.m_btnDetail.PointerClick.AddListener(new UnityAction(this.OnClickDetail));
			}
			if (this.m_btnStart != null)
			{
				this.m_btnStart.PointerClick.RemoveAllListeners();
				this.m_btnStart.PointerClick.AddListener(new UnityAction(this.OnClickStart));
				this.m_btnStart.m_bGetCallbackWhileLocked = true;
			}
			if (this.m_Artifact != null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, true);
				this.m_Artifact.Init();
				NKCUtil.SetGameobjectActive(base.gameObject, false);
			}
			if (this.m_btnGuide != null)
			{
				this.m_btnGuide.PointerClick.RemoveAllListeners();
				this.m_btnGuide.PointerClick.AddListener(new UnityAction(this.OnClickGuide));
			}
		}

		// Token: 0x0600823A RID: 33338 RVA: 0x002BEE66 File Offset: 0x002BD066
		public override void CloseInternal()
		{
			this.m_Artifact.Close();
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_COOP().OnCloseInfoPopup();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_dOnStartBoss = null;
		}

		// Token: 0x0600823B RID: 33339 RVA: 0x002BEE95 File Offset: 0x002BD095
		public override void OnCloseInstance()
		{
			NKCPopupGuildCoopBossInfo.m_Instance = null;
			base.OnCloseInstance();
		}

		// Token: 0x0600823C RID: 33340 RVA: 0x002BEEA4 File Offset: 0x002BD0A4
		public void Open(GuildRaidTemplet templet, NKCPopupGuildCoopBossInfo.OnStartBoss onStartBoss)
		{
			this.m_GuildRaidTemplet = templet;
			this.m_dOnStartBoss = onStartBoss;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_Artifact.SetData(NKCGuildCoopManager.GetMyArtifactDictionary());
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(templet.GetStageId());
			if (dungeonTempletBase != null)
			{
				NKCUtil.SetLabelText(this.m_lbName, dungeonTempletBase.GetDungeonName());
			}
			this.UpdateBossInfo();
			this.UpdateButtonState();
			base.UIOpened(true);
		}

		// Token: 0x0600823D RID: 33341 RVA: 0x002BEF10 File Offset: 0x002BD110
		private void UpdateBossInfo()
		{
			NKCUtil.SetLabelText(this.m_lbRage, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_DUNGEON_RESULT_BOSS_LEVEL_INFO, NKCGuildCoopManager.m_cGuildRaidTemplet.GetStageIndex()));
			float num = NKCGuildCoopManager.m_BossRemainHp / NKCGuildCoopManager.m_BossMaxHp;
			NKCUtil.SetLabelText(this.m_lbHP, string.Format("{0} ({1:0.##}%)", NKCGuildCoopManager.m_BossRemainHp.ToString("N0"), num * 100f));
			this.m_imgHP.fillAmount = num;
			NKCUtil.SetGameobjectActive(this.m_btnGuide, !string.IsNullOrEmpty(this.m_GuildRaidTemplet.GetGuideShortCut()));
		}

		// Token: 0x0600823E RID: 33342 RVA: 0x002BEFAA File Offset: 0x002BD1AA
		private void UpdateButtonState()
		{
			if (NKCGuildCoopManager.CanStartBoss() != NKM_ERROR_CODE.NEC_OK)
			{
				this.m_btnStart.Lock(false);
				return;
			}
			this.m_btnStart.UnLock(false);
		}

		// Token: 0x0600823F RID: 33343 RVA: 0x002BEFCC File Offset: 0x002BD1CC
		public void OnClickDetail()
		{
			NKCPopupGuildCoopBossInfoDetail.Instance.Open();
		}

		// Token: 0x06008240 RID: 33344 RVA: 0x002BEFD8 File Offset: 0x002BD1D8
		public void OnClickStart()
		{
			if (this.m_btnStart.m_bLock)
			{
				NKCPacketHandlers.Check_NKM_ERROR_CODE(NKCGuildCoopManager.CanStartBoss(), true, null, int.MinValue);
				return;
			}
			NKCPopupGuildCoopBossInfo.OnStartBoss dOnStartBoss = this.m_dOnStartBoss;
			if (dOnStartBoss == null)
			{
				return;
			}
			dOnStartBoss();
		}

		// Token: 0x06008241 RID: 33345 RVA: 0x002BF00A File Offset: 0x002BD20A
		private void OnClickGuide()
		{
			if (string.IsNullOrEmpty(this.m_GuildRaidTemplet.GetGuideShortCut()))
			{
				return;
			}
			NKCUIPopupTutorialImagePanel.Instance.Open(this.m_GuildRaidTemplet.GetGuideShortCut(), null);
		}

		// Token: 0x06008242 RID: 33346 RVA: 0x002BF035 File Offset: 0x002BD235
		public void Refresh()
		{
			this.m_Artifact.RefreshUI(false);
			this.UpdateBossInfo();
			this.UpdateButtonState();
		}

		// Token: 0x04006E62 RID: 28258
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONSORTIUM_COOP";

		// Token: 0x04006E63 RID: 28259
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_CONSORTIUM_COOP_BOSS_INFO";

		// Token: 0x04006E64 RID: 28260
		private static NKCPopupGuildCoopBossInfo m_Instance;

		// Token: 0x04006E65 RID: 28261
		public NKCUIComStateButton m_btnClose;

		// Token: 0x04006E66 RID: 28262
		public NKCUIComStateButton m_btnDetail;

		// Token: 0x04006E67 RID: 28263
		public NKCUIComStateButton m_btnStart;

		// Token: 0x04006E68 RID: 28264
		public NKCUIComStateButton m_btnGuide;

		// Token: 0x04006E69 RID: 28265
		public Text m_lbRage;

		// Token: 0x04006E6A RID: 28266
		public Text m_lbName;

		// Token: 0x04006E6B RID: 28267
		public Text m_lbHP;

		// Token: 0x04006E6C RID: 28268
		public Image m_imgHP;

		// Token: 0x04006E6D RID: 28269
		public NKCUIComGuildArtifactContent m_Artifact;

		// Token: 0x04006E6E RID: 28270
		private GuildRaidTemplet m_GuildRaidTemplet;

		// Token: 0x04006E6F RID: 28271
		private NKCPopupGuildCoopBossInfo.OnStartBoss m_dOnStartBoss;

		// Token: 0x020018C7 RID: 6343
		// (Invoke) Token: 0x0600B6AD RID: 46765
		public delegate void OnStartBoss();
	}
}
