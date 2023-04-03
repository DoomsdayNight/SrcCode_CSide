using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM;
using NKM.Guild;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B2D RID: 2861
	public class NKCPopupGuildCoopBossInfoDetail : NKCUIBase
	{
		// Token: 0x1700153C RID: 5436
		// (get) Token: 0x06008244 RID: 33348 RVA: 0x002BF058 File Offset: 0x002BD258
		public static NKCPopupGuildCoopBossInfoDetail Instance
		{
			get
			{
				if (NKCPopupGuildCoopBossInfoDetail.m_Instance == null)
				{
					NKCPopupGuildCoopBossInfoDetail.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupGuildCoopBossInfoDetail>("AB_UI_NKM_UI_CONSORTIUM_COOP", "NKM_UI_POPUP_CONSORTIUM_COOP_RAID_BOSS_INFO", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupGuildCoopBossInfoDetail.CleanupInstance)).GetInstance<NKCPopupGuildCoopBossInfoDetail>();
					if (NKCPopupGuildCoopBossInfoDetail.m_Instance != null)
					{
						NKCPopupGuildCoopBossInfoDetail.m_Instance.InitUI();
					}
				}
				return NKCPopupGuildCoopBossInfoDetail.m_Instance;
			}
		}

		// Token: 0x1700153D RID: 5437
		// (get) Token: 0x06008245 RID: 33349 RVA: 0x002BF0B9 File Offset: 0x002BD2B9
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupGuildCoopBossInfoDetail.m_Instance != null && NKCPopupGuildCoopBossInfoDetail.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008246 RID: 33350 RVA: 0x002BF0D4 File Offset: 0x002BD2D4
		private static void CleanupInstance()
		{
			NKCPopupGuildCoopBossInfoDetail.m_Instance = null;
		}

		// Token: 0x1700153E RID: 5438
		// (get) Token: 0x06008247 RID: 33351 RVA: 0x002BF0DC File Offset: 0x002BD2DC
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700153F RID: 5439
		// (get) Token: 0x06008248 RID: 33352 RVA: 0x002BF0DF File Offset: 0x002BD2DF
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06008249 RID: 33353 RVA: 0x002BF0E8 File Offset: 0x002BD2E8
		private void InitUI()
		{
			if (this.m_btnClose != null)
			{
				this.m_btnClose.PointerClick.RemoveAllListeners();
				this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			}
			if (this.m_loop != null)
			{
				this.m_loop.dOnGetObject += this.GetObject;
				this.m_loop.dOnReturnObject += this.ReturnObject;
				this.m_loop.dOnProvideData += this.ProvideData;
				this.m_loop.PrepareCells(0);
				NKCUtil.SetScrollHotKey(this.m_loop, null);
			}
		}

		// Token: 0x0600824A RID: 33354 RVA: 0x002BF19A File Offset: 0x002BD39A
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0600824B RID: 33355 RVA: 0x002BF1A8 File Offset: 0x002BD3A8
		private void OnDestroy()
		{
			NKCPopupGuildCoopBossInfoDetail.m_Instance = null;
		}

		// Token: 0x0600824C RID: 33356 RVA: 0x002BF1B0 File Offset: 0x002BD3B0
		private RectTransform GetObject(int idx)
		{
			NKCPopupGuildCoopBossInfoDetailSlot nkcpopupGuildCoopBossInfoDetailSlot;
			if (this.m_stkSlot.Count > 0)
			{
				nkcpopupGuildCoopBossInfoDetailSlot = this.m_stkSlot.Pop();
			}
			else
			{
				nkcpopupGuildCoopBossInfoDetailSlot = UnityEngine.Object.Instantiate<NKCPopupGuildCoopBossInfoDetailSlot>(this.m_pfbSlot);
			}
			nkcpopupGuildCoopBossInfoDetailSlot.transform.SetParent(this.m_trParent);
			return nkcpopupGuildCoopBossInfoDetailSlot.GetComponent<RectTransform>();
		}

		// Token: 0x0600824D RID: 33357 RVA: 0x002BF200 File Offset: 0x002BD400
		private void ReturnObject(Transform tr)
		{
			NKCUtil.SetGameobjectActive(tr, false);
			tr.SetParent(base.transform);
			NKCPopupGuildCoopBossInfoDetailSlot component = tr.GetComponent<NKCPopupGuildCoopBossInfoDetailSlot>();
			if (component == null)
			{
				return;
			}
			this.m_stkSlot.Push(component);
		}

		// Token: 0x0600824E RID: 33358 RVA: 0x002BF240 File Offset: 0x002BD440
		private void ProvideData(Transform tr, int idx)
		{
			NKCPopupGuildCoopBossInfoDetailSlot component = tr.GetComponent<NKCPopupGuildCoopBossInfoDetailSlot>();
			if (component == null || this.m_lstRaidTemplet.Count <= idx)
			{
				NKCUtil.SetGameobjectActive(tr, false);
				return;
			}
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(this.m_lstRaidTemplet[idx].GetStageId());
			if (dungeonTempletBase == null)
			{
				NKCUtil.SetGameobjectActive(tr, false);
				return;
			}
			component.SetData(this.m_lstRaidTemplet[idx], dungeonTempletBase);
		}

		// Token: 0x0600824F RID: 33359 RVA: 0x002BF2A8 File Offset: 0x002BD4A8
		public void Open()
		{
			this.m_lstRaidTemplet = GuildDungeonTempletManager.GetRaidTempletList(NKCGuildCoopManager.m_cGuildRaidTemplet.GetSeasonRaidGrouop());
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(this.m_lstRaidTemplet[NKCGuildCoopManager.m_cGuildRaidTemplet.GetStageIndex() - 1].GetStageId());
			if (dungeonTempletBase == null)
			{
				Log.Error(string.Format("dungeonTempletBase is null - id : {0}", this.m_lstRaidTemplet[NKCGuildCoopManager.m_cGuildRaidTemplet.GetStageIndex() - 1].GetStageId()), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Guild/GuildCoop/NKCPopupGuildCoopBossInfoDetail.cs", 146);
				return;
			}
			NKCUtil.SetLabelText(this.m_lbName, dungeonTempletBase.GetDungeonName());
			NKCUtil.SetImageSprite(this.m_imgBoss, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UNIT_FACE_CARD", dungeonTempletBase.m_DungeonIcon, false), false);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_loop.TotalCount = this.m_lstRaidTemplet.Count;
			this.m_loop.SetIndexPosition(0);
			base.UIOpened(true);
		}

		// Token: 0x04006E70 RID: 28272
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONSORTIUM_COOP";

		// Token: 0x04006E71 RID: 28273
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_CONSORTIUM_COOP_RAID_BOSS_INFO";

		// Token: 0x04006E72 RID: 28274
		private static NKCPopupGuildCoopBossInfoDetail m_Instance;

		// Token: 0x04006E73 RID: 28275
		public NKCPopupGuildCoopBossInfoDetailSlot m_pfbSlot;

		// Token: 0x04006E74 RID: 28276
		public NKCUIComStateButton m_btnClose;

		// Token: 0x04006E75 RID: 28277
		public Text m_lbName;

		// Token: 0x04006E76 RID: 28278
		public Image m_imgBoss;

		// Token: 0x04006E77 RID: 28279
		public LoopScrollRect m_loop;

		// Token: 0x04006E78 RID: 28280
		public Transform m_trParent;

		// Token: 0x04006E79 RID: 28281
		private Stack<NKCPopupGuildCoopBossInfoDetailSlot> m_stkSlot = new Stack<NKCPopupGuildCoopBossInfoDetailSlot>();

		// Token: 0x04006E7A RID: 28282
		private List<GuildRaidTemplet> m_lstRaidTemplet = new List<GuildRaidTemplet>();
	}
}
