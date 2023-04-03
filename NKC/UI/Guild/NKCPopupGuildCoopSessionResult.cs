using System;
using System.Collections.Generic;
using ClientPacket.Guild;
using Cs.Logging;
using NKC.UI.Result;
using NKM;
using NKM.Guild;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B31 RID: 2865
	public class NKCPopupGuildCoopSessionResult : NKCUIBase
	{
		// Token: 0x17001544 RID: 5444
		// (get) Token: 0x0600826E RID: 33390 RVA: 0x002C00A8 File Offset: 0x002BE2A8
		public static NKCPopupGuildCoopSessionResult Instance
		{
			get
			{
				if (NKCPopupGuildCoopSessionResult.m_Instance == null)
				{
					NKCPopupGuildCoopSessionResult.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupGuildCoopSessionResult>("AB_UI_NKM_UI_CONSORTIUM_COOP", "NKM_UI_POPUP_CONSORTIUM_COOP_RESULT", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupGuildCoopSessionResult.CloseInstance)).GetInstance<NKCPopupGuildCoopSessionResult>();
					if (NKCPopupGuildCoopSessionResult.m_Instance != null)
					{
						NKCPopupGuildCoopSessionResult.m_Instance.InitUI();
					}
				}
				return NKCPopupGuildCoopSessionResult.m_Instance;
			}
		}

		// Token: 0x17001545 RID: 5445
		// (get) Token: 0x0600826F RID: 33391 RVA: 0x002C0109 File Offset: 0x002BE309
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupGuildCoopSessionResult.m_Instance != null && NKCPopupGuildCoopSessionResult.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008270 RID: 33392 RVA: 0x002C0124 File Offset: 0x002BE324
		private static void CloseInstance()
		{
			NKCPopupGuildCoopSessionResult.m_Instance = null;
		}

		// Token: 0x17001546 RID: 5446
		// (get) Token: 0x06008271 RID: 33393 RVA: 0x002C012C File Offset: 0x002BE32C
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001547 RID: 5447
		// (get) Token: 0x06008272 RID: 33394 RVA: 0x002C012F File Offset: 0x002BE32F
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06008273 RID: 33395 RVA: 0x002C0138 File Offset: 0x002BE338
		private void InitUI()
		{
			if (this.m_btnStatus != null)
			{
				this.m_btnStatus.PointerClick.RemoveAllListeners();
				this.m_btnStatus.PointerClick.AddListener(new UnityAction(this.OnClickStatus));
			}
			NKCUtil.SetButtonClickDelegate(this.m_btnOK, new UnityAction(base.Close));
			NKCUtil.SetHotkey(this.m_btnOK, HotkeyEventType.Confirm, null, false);
			if (this.m_loopReward != null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, true);
				this.m_loopReward.dOnGetObject += this.GetObjectReward;
				this.m_loopReward.dOnReturnObject += this.ReturnObjectReward;
				this.m_loopReward.dOnProvideData += this.ProvideDataReward;
				this.m_loopReward.PrepareCells(0);
				NKCUtil.SetGameobjectActive(base.gameObject, false);
			}
		}

		// Token: 0x06008274 RID: 33396 RVA: 0x002C021B File Offset: 0x002BE41B
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06008275 RID: 33397 RVA: 0x002C0229 File Offset: 0x002BE429
		private void OnDestroy()
		{
			NKCPopupGuildCoopSessionResult.m_Instance = null;
		}

		// Token: 0x06008276 RID: 33398 RVA: 0x002C0234 File Offset: 0x002BE434
		private RectTransform GetObjectReward(int idx)
		{
			NKCUIWRRewardSlot nkcuiwrrewardSlot;
			if (this.m_stkRewardSlot.Count > 0)
			{
				nkcuiwrrewardSlot = this.m_stkRewardSlot.Pop();
			}
			else
			{
				nkcuiwrrewardSlot = NKCUIWRRewardSlot.GetNewInstance(this.m_trRewardParent);
			}
			nkcuiwrrewardSlot.transform.SetParent(base.transform);
			return nkcuiwrrewardSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06008277 RID: 33399 RVA: 0x002C0284 File Offset: 0x002BE484
		private void ReturnObjectReward(Transform tr)
		{
			NKCUtil.SetGameobjectActive(tr, false);
			tr.SetParent(base.transform);
			NKCUIWRRewardSlot component = tr.GetComponent<NKCUIWRRewardSlot>();
			if (component != null)
			{
				this.m_stkRewardSlot.Push(component);
			}
		}

		// Token: 0x06008278 RID: 33400 RVA: 0x002C02C0 File Offset: 0x002BE4C0
		private void ProvideDataReward(Transform tr, int idx)
		{
			NKCUIWRRewardSlot component = tr.GetComponent<NKCUIWRRewardSlot>();
			if (component == null || idx > this.m_lstClearRewardData.Count + this.m_lstArtifactRewardData.Count)
			{
				NKCUtil.SetGameobjectActive(tr, false);
				return;
			}
			if (idx < this.m_lstClearRewardData.Count)
			{
				component.SetUI(this.m_lstClearRewardData[idx], idx);
				component.SetKillRewardMark(true);
				component.SetArtifactMark(false);
			}
			else
			{
				component.SetUI(this.m_lstArtifactRewardData[idx - this.m_lstClearRewardData.Count], idx);
				component.SetKillRewardMark(false);
				component.SetArtifactMark(true);
			}
			component.InvalidAni();
		}

		// Token: 0x06008279 RID: 33401 RVA: 0x002C0364 File Offset: 0x002BE564
		public void Open(NKMPacket_GUILD_DUNGEON_SESSION_REWARD_ACK sPacket)
		{
			if (sPacket == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				Log.Error("NKMPacket_GUILD_DUNGEON_SESSION_REWARD_ACK is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Guild/GuildCoop/NKCPopupGuildCoopSessionResult.cs", 173);
				return;
			}
			GuildSeasonTemplet guildSeasonTemplet = GuildSeasonTemplet.Find(NKCGuildCoopManager.m_SeasonId);
			GuildRaidTemplet guildRaidTemplet;
			if (GuildDungeonTempletManager.GetRaidTempletList(guildSeasonTemplet.GetSeasonRaidGroup()).Count > sPacket.stageIndex)
			{
				guildRaidTemplet = GuildDungeonTempletManager.GetRaidTempletList(guildSeasonTemplet.GetSeasonRaidGroup())[sPacket.stageIndex];
			}
			else
			{
				guildRaidTemplet = GuildDungeonTempletManager.GetRaidTempletList(guildSeasonTemplet.GetSeasonRaidGroup())[sPacket.stageIndex - 1];
			}
			if (guildRaidTemplet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				Log.Error(string.Format("dungeonTempletBase is null - stageIndex : {0}", sPacket.stageIndex), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Guild/GuildCoop/NKCPopupGuildCoopSessionResult.cs", 192);
				return;
			}
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(guildRaidTemplet.GetStageId());
			if (dungeonTempletBase == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				Log.Error(string.Format("dungeonTempletBase is null - stageID : {0}", guildRaidTemplet.GetStageId()), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Guild/GuildCoop/NKCPopupGuildCoopSessionResult.cs", 200);
				return;
			}
			if (sPacket.stageIndex == 0)
			{
				sPacket.remainHp = (long)NKMDungeonManager.GetBossHp(guildRaidTemplet.GetStageId(), dungeonTempletBase.m_DungeonLevel);
			}
			NKCUtil.SetGameobjectActive(this.m_imgBossFaceCard, true);
			NKCUtil.SetImageSprite(this.m_imgBossFaceCard, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_unit_face_card", guildRaidTemplet.GetRaidBossFaceCardName(), false), false);
			if (sPacket.stageIndex == 0)
			{
				NKCUtil.SetGameobjectActive(this.m_objClear, false);
				NKCUtil.SetLabelText(this.m_lbTitle, NKCUtilString.GET_STRING_POPUP_CONSORTIUM_COOP_RESULT_TITLE02);
				NKCUtil.SetLabelText(this.m_lbStep, NKCUtilString.GET_STRING_CONSORTIUM_DUNGEON_RESULT_BOSS_SUMMARY_INFO_FAIL);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objClear, true);
				NKCUtil.SetLabelText(this.m_lbTitle, NKCUtilString.GET_STRING_POPUP_CONSORTIUM_COOP_RESULT_TITLE01);
				NKCUtil.SetLabelText(this.m_lbStep, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_DUNGEON_RESULT_BOSS_SUMMARY_INFO, sPacket.stageIndex));
			}
			float bossHp = NKMDungeonManager.GetBossHp(guildRaidTemplet.GetStageId(), dungeonTempletBase.m_DungeonLevel);
			NKCUtil.SetLabelText(this.m_lbRemainHP, string.Format("{0} ({1:0.##}%)", sPacket.remainHp.ToString("N0"), (float)sPacket.remainHp / bossHp * 100f));
			this.m_imgBossHp.fillAmount = (float)sPacket.remainHp / bossHp;
			NKCUtil.SetLabelText(this.m_lbDamagePoint, sPacket.clearPoint.ToString("N0"));
			this.m_lstClearRewardData.Clear();
			for (int i = 0; i < sPacket.rewardList.Count; i++)
			{
				this.m_lstClearRewardData.Add(NKCUISlot.SlotData.MakeMiscItemData(sPacket.rewardList[i], 0));
			}
			this.m_lstArtifactRewardData.Clear();
			for (int j = 0; j < sPacket.artifactReward.Count; j++)
			{
				this.m_lstArtifactRewardData.Add(NKCUISlot.SlotData.MakeMiscItemData(sPacket.artifactReward[j], 0));
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_loopReward.TotalCount = this.m_lstClearRewardData.Count + this.m_lstArtifactRewardData.Count;
			this.m_loopReward.SetIndexPosition(0);
			NKCUtil.SetGameobjectActive(this.m_objNone, this.m_loopReward.TotalCount == 0);
			base.UIOpened(true);
		}

		// Token: 0x0600827A RID: 33402 RVA: 0x002C0675 File Offset: 0x002BE875
		private void OnClickStatus()
		{
			NKCPopupGuildCoopStatus.Instance.Open();
		}

		// Token: 0x04006EA2 RID: 28322
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONSORTIUM_COOP";

		// Token: 0x04006EA3 RID: 28323
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_CONSORTIUM_COOP_RESULT";

		// Token: 0x04006EA4 RID: 28324
		private static NKCPopupGuildCoopSessionResult m_Instance;

		// Token: 0x04006EA5 RID: 28325
		[Header("좌측")]
		public Text m_lbTitle;

		// Token: 0x04006EA6 RID: 28326
		public NKCUIComStateButton m_btnStatus;

		// Token: 0x04006EA7 RID: 28327
		[Header("보스")]
		public Image m_imgBossFaceCard;

		// Token: 0x04006EA8 RID: 28328
		public Text m_lbStep;

		// Token: 0x04006EA9 RID: 28329
		public Text m_lbRemainHP;

		// Token: 0x04006EAA RID: 28330
		public Text m_lbDamagePoint;

		// Token: 0x04006EAB RID: 28331
		public Image m_imgBossHp;

		// Token: 0x04006EAC RID: 28332
		public GameObject m_objClear;

		// Token: 0x04006EAD RID: 28333
		[Header("전리품")]
		public LoopScrollRect m_loopReward;

		// Token: 0x04006EAE RID: 28334
		public Transform m_trRewardParent;

		// Token: 0x04006EAF RID: 28335
		public GameObject m_objNone;

		// Token: 0x04006EB0 RID: 28336
		[Header("")]
		public NKCUIComStateButton m_btnOK;

		// Token: 0x04006EB1 RID: 28337
		private Stack<NKCUIWRRewardSlot> m_stkRewardSlot = new Stack<NKCUIWRRewardSlot>();

		// Token: 0x04006EB2 RID: 28338
		private List<NKCUISlot.SlotData> m_lstClearRewardData = new List<NKCUISlot.SlotData>();

		// Token: 0x04006EB3 RID: 28339
		private List<NKCUISlot.SlotData> m_lstArtifactRewardData = new List<NKCUISlot.SlotData>();
	}
}
