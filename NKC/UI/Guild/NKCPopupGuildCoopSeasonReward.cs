using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Guild;
using Cs.Logging;
using NKM.Guild;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B2F RID: 2863
	public class NKCPopupGuildCoopSeasonReward : NKCUIBase
	{
		// Token: 0x17001540 RID: 5440
		// (get) Token: 0x06008253 RID: 33363 RVA: 0x002BF450 File Offset: 0x002BD650
		public static NKCPopupGuildCoopSeasonReward Instance
		{
			get
			{
				if (NKCPopupGuildCoopSeasonReward.m_Instance == null)
				{
					NKCPopupGuildCoopSeasonReward.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupGuildCoopSeasonReward>("AB_UI_NKM_UI_CONSORTIUM_COOP", "NKM_UI_POPUP_CONSORTIUM_COOP_REWARD", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), null).GetInstance<NKCPopupGuildCoopSeasonReward>();
					if (NKCPopupGuildCoopSeasonReward.m_Instance != null)
					{
						NKCPopupGuildCoopSeasonReward.m_Instance.InitUI();
					}
				}
				return NKCPopupGuildCoopSeasonReward.m_Instance;
			}
		}

		// Token: 0x17001541 RID: 5441
		// (get) Token: 0x06008254 RID: 33364 RVA: 0x002BF4A6 File Offset: 0x002BD6A6
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupGuildCoopSeasonReward.m_Instance != null && NKCPopupGuildCoopSeasonReward.m_Instance.IsOpen;
			}
		}

		// Token: 0x17001542 RID: 5442
		// (get) Token: 0x06008255 RID: 33365 RVA: 0x002BF4C1 File Offset: 0x002BD6C1
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001543 RID: 5443
		// (get) Token: 0x06008256 RID: 33366 RVA: 0x002BF4C4 File Offset: 0x002BD6C4
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06008257 RID: 33367 RVA: 0x002BF4CC File Offset: 0x002BD6CC
		public void InitUI()
		{
			if (this.m_btnClose != null)
			{
				this.m_btnClose.PointerClick.RemoveAllListeners();
				this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			}
			if (this.m_tglDamagePoint != null)
			{
				this.m_tglDamagePoint.OnValueChanged.RemoveAllListeners();
				this.m_tglDamagePoint.OnValueChanged.AddListener(new UnityAction<bool>(this.OnChangedKillPoint));
			}
			if (this.m_tglBattleCount != null)
			{
				this.m_tglBattleCount.OnValueChanged.RemoveAllListeners();
				this.m_tglBattleCount.OnValueChanged.AddListener(new UnityAction<bool>(this.OnChangedDungeonTry));
			}
			if (this.m_btnMoveToLeft != null)
			{
				this.m_btnMoveToLeft.PointerClick.RemoveAllListeners();
				this.m_btnMoveToLeft.PointerClick.AddListener(new UnityAction(this.MoveToProgress));
			}
			if (this.m_btnMoveToRight != null)
			{
				this.m_btnMoveToRight.PointerClick.RemoveAllListeners();
				this.m_btnMoveToRight.PointerClick.AddListener(new UnityAction(this.MoveToProgress));
			}
			if (this.m_loop != null)
			{
				this.m_loop.dOnGetObject += this.GetObject;
				this.m_loop.dOnReturnObject += this.ReturnObject;
				this.m_loop.dOnProvideData += this.ProvideData;
				this.m_loop.PrepareCells(0);
				NKCUtil.SetScrollHotKey(this.m_loop, null);
			}
			if (this.m_slotProfile != null)
			{
				this.m_slotProfile.Init();
			}
		}

		// Token: 0x06008258 RID: 33368 RVA: 0x002BF67F File Offset: 0x002BD87F
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			NKCPopupGuildCoopSeasonReward.OnClose dOnClose = this.m_dOnClose;
			if (dOnClose == null)
			{
				return;
			}
			dOnClose();
		}

		// Token: 0x06008259 RID: 33369 RVA: 0x002BF69D File Offset: 0x002BD89D
		private void OnDestroy()
		{
			NKCPopupGuildCoopSeasonReward.m_Instance = null;
		}

		// Token: 0x0600825A RID: 33370 RVA: 0x002BF6A5 File Offset: 0x002BD8A5
		public override void UnHide()
		{
			base.UnHide();
			this.RefreshUI();
		}

		// Token: 0x0600825B RID: 33371 RVA: 0x002BF6B4 File Offset: 0x002BD8B4
		private RectTransform GetObject(int idx)
		{
			NKCPopupGuildCoopSeasonRewardSlot nkcpopupGuildCoopSeasonRewardSlot;
			if (this.m_stkSlot.Count > 0)
			{
				nkcpopupGuildCoopSeasonRewardSlot = this.m_stkSlot.Pop();
			}
			else
			{
				nkcpopupGuildCoopSeasonRewardSlot = UnityEngine.Object.Instantiate<NKCPopupGuildCoopSeasonRewardSlot>(this.m_pfbSlot);
				nkcpopupGuildCoopSeasonRewardSlot.InitUI();
			}
			nkcpopupGuildCoopSeasonRewardSlot.transform.SetParent(this.m_trSlotParent);
			return nkcpopupGuildCoopSeasonRewardSlot.GetComponent<RectTransform>();
		}

		// Token: 0x0600825C RID: 33372 RVA: 0x002BF708 File Offset: 0x002BD908
		private void ReturnObject(Transform tr)
		{
			NKCUtil.SetGameobjectActive(tr, false);
			tr.SetParent(base.transform);
			this.m_stkSlot.Push(tr.GetComponent<NKCPopupGuildCoopSeasonRewardSlot>());
		}

		// Token: 0x0600825D RID: 33373 RVA: 0x002BF730 File Offset: 0x002BD930
		private void ProvideData(Transform tr, int idx)
		{
			NKCPopupGuildCoopSeasonRewardSlot component = tr.GetComponent<NKCPopupGuildCoopSeasonRewardSlot>();
			if (component == null)
			{
				NKCUtil.SetGameobjectActive(tr, false);
				return;
			}
			tr.SetParent(this.m_trSlotParent);
			NKCUtil.SetGameobjectActive(tr, true);
			GuildDungeonSeasonRewardData guildDungeonSeasonRewardData = NKCGuildCoopManager.m_LastReceivedSeasonRewardData.Find((GuildDungeonSeasonRewardData x) => x.category == this.m_CurCategory);
			if (this.m_CurCategory == GuildDungeonRewardCategory.RANK)
			{
				float gaugeProgress = 0f;
				if (idx == 0)
				{
					gaugeProgress = (float)NKCGuildCoopManager.m_KillPoint / (float)this.m_lstRewardByRank[0].GetRewardCountValue();
					component.SetData(null, false, false, false, null);
					component.SetGaugeProgress(gaugeProgress);
					return;
				}
				GuildSeasonRewardTemplet guildSeasonRewardTemplet = this.m_lstRewardByRank[idx - 1];
				GuildSeasonRewardTemplet guildSeasonRewardTemplet2 = (idx < this.m_lstRewardByRank.Count) ? this.m_lstRewardByRank[idx] : null;
				if (guildSeasonRewardTemplet2 != null && NKCGuildCoopManager.m_KillPoint > (long)guildSeasonRewardTemplet.GetRewardCountValue())
				{
					long num = (long)(guildSeasonRewardTemplet2.GetRewardCountValue() - guildSeasonRewardTemplet.GetRewardCountValue());
					gaugeProgress = (float)(NKCGuildCoopManager.m_KillPoint - (long)guildSeasonRewardTemplet.GetRewardCountValue()) / (float)num;
				}
				int rewardCountValue = guildSeasonRewardTemplet.GetRewardCountValue();
				component.SetData(this.m_lstRewardByRank[idx - 1], NKCGuildCoopManager.m_KillPoint >= (long)rewardCountValue && guildDungeonSeasonRewardData.receivedValue < rewardCountValue, guildDungeonSeasonRewardData.receivedValue >= rewardCountValue, idx == this.m_lstRewardByRank.Count, new NKCPopupGuildCoopSeasonRewardSlot.OnClickSlot(this.OnClickSlot));
				component.SetGaugeProgress(gaugeProgress);
				return;
			}
			else
			{
				float num2 = 0f;
				if (idx == 0)
				{
					num2 = (float)NKCGuildCoopManager.m_TryCount / (float)this.m_lstRewardByTry[0].GetRewardCountValue();
					component.SetData(null, false, false, false, null);
					component.SetGaugeProgress((float)NKCGuildCoopManager.m_TryCount / (float)this.m_lstRewardByTry[0].GetRewardCountValue());
					if (num2 >= 1f && guildDungeonSeasonRewardData.receivedValue < this.m_lstRewardByTry[0].GetRewardCountValue())
					{
						NKCUtil.SetGameobjectActive(this.m_objDungeonTryRedDot, true);
					}
					return;
				}
				GuildSeasonRewardTemplet guildSeasonRewardTemplet3 = this.m_lstRewardByTry[idx - 1];
				GuildSeasonRewardTemplet guildSeasonRewardTemplet4 = (idx < this.m_lstRewardByTry.Count) ? this.m_lstRewardByTry[idx] : null;
				if (guildSeasonRewardTemplet4 != null && NKCGuildCoopManager.m_TryCount > guildSeasonRewardTemplet3.GetRewardCountValue())
				{
					long num3 = (long)(guildSeasonRewardTemplet4.GetRewardCountValue() - guildSeasonRewardTemplet3.GetRewardCountValue());
					num2 = (float)((long)(NKCGuildCoopManager.m_TryCount - guildSeasonRewardTemplet3.GetRewardCountValue())) / (float)num3;
				}
				int rewardCountValue2 = guildSeasonRewardTemplet3.GetRewardCountValue();
				component.SetData(this.m_lstRewardByTry[idx - 1], NKCGuildCoopManager.m_TryCount >= rewardCountValue2 && guildDungeonSeasonRewardData.receivedValue < rewardCountValue2, guildDungeonSeasonRewardData != null && guildDungeonSeasonRewardData.receivedValue >= rewardCountValue2, idx == this.m_lstRewardByTry.Count, new NKCPopupGuildCoopSeasonRewardSlot.OnClickSlot(this.OnClickSlot));
				component.SetGaugeProgress(num2);
				if (num2 >= 1f && guildDungeonSeasonRewardData.receivedValue < this.m_lstRewardByTry[idx - 1].GetRewardCountValue())
				{
					NKCUtil.SetGameobjectActive(this.m_objDungeonTryRedDot, true);
				}
				return;
			}
		}

		// Token: 0x0600825E RID: 33374 RVA: 0x002BFA04 File Offset: 0x002BDC04
		public void Open(NKCPopupGuildCoopSeasonReward.OnClose onClose)
		{
			this.m_dOnClose = onClose;
			this.m_CurCategory = GuildDungeonRewardCategory.RANK;
			List<GuildSeasonRewardTemplet> seasonRewardList = GuildDungeonTempletManager.GetSeasonRewardList(NKCGuildCoopManager.m_SeasonId);
			if (seasonRewardList == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			this.m_GuildSeasonTemplet = GuildDungeonTempletManager.GetGuildSeasonTemplet(NKCGuildCoopManager.m_SeasonId);
			this.m_lstRewardByRank = seasonRewardList.FindAll((GuildSeasonRewardTemplet x) => x.GetRewardCategory() == GuildDungeonRewardCategory.RANK);
			this.m_lstRewardByTry = seasonRewardList.FindAll((GuildSeasonRewardTemplet x) => x.GetRewardCategory() == GuildDungeonRewardCategory.DUNGEON_TRY);
			if (this.m_CurCategory == GuildDungeonRewardCategory.DUNGEON_TRY)
			{
				this.m_tglBattleCount.Select(true, true, true);
			}
			else
			{
				this.m_tglDamagePoint.Select(true, true, true);
			}
			if (NKCGuildManager.HasGuild())
			{
				NKMGuildMemberData nkmguildMemberData = NKCGuildManager.MyGuildData.members.Find((NKMGuildMemberData x) => x.commonProfile.userUid == NKCScenManager.CurrentUserData().m_UserUID);
				if (nkmguildMemberData != null)
				{
					this.m_slotProfile.SetProfiledata(nkmguildMemberData.commonProfile, null);
				}
			}
			else
			{
				NKMUserProfileData userProfileData = NKCScenManager.CurrentUserData().UserProfileData;
				this.m_slotProfile.SetProfiledata(userProfileData, null);
			}
			NKCUtil.SetLabelText(this.m_lbTitle, "[" + NKCStringTable.GetString(this.m_GuildSeasonTemplet.GetSeasonNameID(), false) + "]");
			this.SetRemainTime();
			this.RefreshUI();
			base.UIOpened(true);
		}

		// Token: 0x0600825F RID: 33375 RVA: 0x002BFB6C File Offset: 0x002BDD6C
		private void SetRemainTime()
		{
			NKCUtil.SetLabelText(this.m_lbRemainTime, NKCUtilString.GetRemainTimeStringEx(NKMTime.LocalToUTC(this.m_GuildSeasonTemplet.GetSeasonEndDate(), 0)));
		}

		// Token: 0x06008260 RID: 33376 RVA: 0x002BFB90 File Offset: 0x002BDD90
		public void RefreshUI()
		{
			NKCUtil.SetGameobjectActive(this.m_objDungeonTryRedDot, NKCGuildCoopManager.CheckSeasonRewardEnable(GuildDungeonRewardCategory.DUNGEON_TRY));
			NKCUtil.SetGameobjectActive(this.m_objKillPointRedDot, NKCGuildCoopManager.CheckSeasonRewardEnable(GuildDungeonRewardCategory.RANK));
			if (this.m_CurCategory == GuildDungeonRewardCategory.RANK)
			{
				NKCUtil.SetLabelText(this.m_lbMyPointTitle, NKCUtilString.GET_STRING_CONSORTIUM_DUNGEON_MENU_SEASON_REWARD_KILL_SCORE_STATUS);
				NKCUtil.SetLabelText(this.m_lbMyPoint, NKCGuildCoopManager.m_KillPoint.ToString("N0"));
				this.m_loop.TotalCount = this.m_lstRewardByRank.Count + 1;
			}
			else if (this.m_CurCategory == GuildDungeonRewardCategory.DUNGEON_TRY)
			{
				NKCUtil.SetLabelText(this.m_lbMyPointTitle, NKCUtilString.GET_STRING_CONSORTIUM_DUNGEON_MENU_SEASON_REWARD_PARTICIPATION_SCORE_STATUS);
				NKCUtil.SetLabelText(this.m_lbMyPoint, NKCGuildCoopManager.m_TryCount.ToString("N0"));
				this.m_loop.TotalCount = this.m_lstRewardByTry.Count + 1;
			}
			if (!base.gameObject.activeSelf)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, true);
			}
			this.m_loop.SetIndexPosition(this.GetCurrentIndex());
		}

		// Token: 0x06008261 RID: 33377 RVA: 0x002BFC88 File Offset: 0x002BDE88
		private int GetCurrentIndex()
		{
			List<GuildSeasonRewardTemplet> list = new List<GuildSeasonRewardTemplet>();
			GuildDungeonRewardCategory curCategory = this.m_CurCategory;
			if (curCategory != GuildDungeonRewardCategory.RANK)
			{
				if (curCategory == GuildDungeonRewardCategory.DUNGEON_TRY)
				{
					list = this.m_lstRewardByTry.FindAll((GuildSeasonRewardTemplet x) => x.GetRewardCountValue() <= NKCGuildCoopManager.GetLastReceivedPoint(GuildDungeonRewardCategory.DUNGEON_TRY));
				}
			}
			else
			{
				list = this.m_lstRewardByRank.FindAll((GuildSeasonRewardTemplet x) => x.GetRewardCountValue() <= NKCGuildCoopManager.GetLastReceivedPoint(GuildDungeonRewardCategory.RANK));
			}
			return list.Count;
		}

		// Token: 0x06008262 RID: 33378 RVA: 0x002BFD09 File Offset: 0x002BDF09
		private void OnChangedKillPoint(bool bValue)
		{
			if (bValue)
			{
				this.m_tglDamagePoint.Select(true, false, false);
				this.m_CurCategory = GuildDungeonRewardCategory.RANK;
				this.RefreshUI();
			}
		}

		// Token: 0x06008263 RID: 33379 RVA: 0x002BFD2A File Offset: 0x002BDF2A
		private void OnChangedDungeonTry(bool bValue)
		{
			if (bValue)
			{
				this.m_tglBattleCount.Select(true, false, false);
				this.m_CurCategory = GuildDungeonRewardCategory.DUNGEON_TRY;
				this.RefreshUI();
			}
		}

		// Token: 0x06008264 RID: 33380 RVA: 0x002BFD4B File Offset: 0x002BDF4B
		private void MoveToProgress()
		{
		}

		// Token: 0x06008265 RID: 33381 RVA: 0x002BFD4D File Offset: 0x002BDF4D
		private void Update()
		{
			this.m_fDeltaTime += Time.deltaTime;
			if (this.m_fDeltaTime > 1f)
			{
				this.m_fDeltaTime -= 1f;
				this.SetRemainTime();
			}
		}

		// Token: 0x06008266 RID: 33382 RVA: 0x002BFD88 File Offset: 0x002BDF88
		private void OnClickSlot()
		{
			GuildSeasonRewardTemplet guildSeasonRewardTemplet = null;
			GuildDungeonRewardCategory curCategory = this.m_CurCategory;
			if (curCategory != GuildDungeonRewardCategory.RANK)
			{
				if (curCategory == GuildDungeonRewardCategory.DUNGEON_TRY)
				{
					int num = this.m_lstRewardByTry.FindIndex((GuildSeasonRewardTemplet x) => x.GetRewardCountValue() == NKCGuildCoopManager.GetLastReceivedPoint(GuildDungeonRewardCategory.DUNGEON_TRY));
					if (this.m_lstRewardByTry.Count > num + 1)
					{
						guildSeasonRewardTemplet = this.m_lstRewardByTry[num + 1];
					}
				}
			}
			else
			{
				int num = this.m_lstRewardByRank.FindIndex((GuildSeasonRewardTemplet x) => x.GetRewardCountValue() == NKCGuildCoopManager.GetLastReceivedPoint(GuildDungeonRewardCategory.RANK));
				if (this.m_lstRewardByRank.Count > num + 1)
				{
					guildSeasonRewardTemplet = this.m_lstRewardByRank[num + 1];
				}
			}
			if (guildSeasonRewardTemplet != null)
			{
				NKCPacketSender.Send_NKMPacket_GUILD_DUNGEON_SEASON_REWARD_REQ(guildSeasonRewardTemplet.GetRewardCategory(), guildSeasonRewardTemplet.GetRewardCountValue());
				return;
			}
			Log.Error("GuildSeasonRewardTemplet is null - ", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Guild/GuildCoop/NKCPopupGuildCoopSeasonReward.cs", 421);
		}

		// Token: 0x04006E7F RID: 28287
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONSORTIUM_COOP";

		// Token: 0x04006E80 RID: 28288
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_CONSORTIUM_COOP_REWARD";

		// Token: 0x04006E81 RID: 28289
		private static NKCPopupGuildCoopSeasonReward m_Instance;

		// Token: 0x04006E82 RID: 28290
		public NKCPopupGuildCoopSeasonRewardSlot m_pfbSlot;

		// Token: 0x04006E83 RID: 28291
		public Text m_lbTitle;

		// Token: 0x04006E84 RID: 28292
		public Text m_lbRemainTime;

		// Token: 0x04006E85 RID: 28293
		public NKCUIComStateButton m_btnClose;

		// Token: 0x04006E86 RID: 28294
		[Header("처치점수 / 전투참여 토글버튼")]
		public NKCUIComToggle m_tglDamagePoint;

		// Token: 0x04006E87 RID: 28295
		public NKCUIComToggle m_tglBattleCount;

		// Token: 0x04006E88 RID: 28296
		public GameObject m_objKillPointRedDot;

		// Token: 0x04006E89 RID: 28297
		public GameObject m_objDungeonTryRedDot;

		// Token: 0x04006E8A RID: 28298
		[Header("좌우 보상있을경우 이동버튼")]
		public NKCUIComStateButton m_btnMoveToLeft;

		// Token: 0x04006E8B RID: 28299
		public NKCUIComStateButton m_btnMoveToRight;

		// Token: 0x04006E8C RID: 28300
		[Header("내 점수")]
		public NKCUISlotProfile m_slotProfile;

		// Token: 0x04006E8D RID: 28301
		public Image m_imgPoint;

		// Token: 0x04006E8E RID: 28302
		public Text m_lbMyPointTitle;

		// Token: 0x04006E8F RID: 28303
		public Text m_lbMyPoint;

		// Token: 0x04006E90 RID: 28304
		[Header("포인트 보상")]
		public LoopScrollRect m_loop;

		// Token: 0x04006E91 RID: 28305
		public Transform m_trSlotParent;

		// Token: 0x04006E92 RID: 28306
		private NKCPopupGuildCoopSeasonReward.OnClose m_dOnClose;

		// Token: 0x04006E93 RID: 28307
		private Stack<NKCPopupGuildCoopSeasonRewardSlot> m_stkSlot = new Stack<NKCPopupGuildCoopSeasonRewardSlot>();

		// Token: 0x04006E94 RID: 28308
		private List<GuildSeasonRewardTemplet> m_lstRewardByRank = new List<GuildSeasonRewardTemplet>();

		// Token: 0x04006E95 RID: 28309
		private List<GuildSeasonRewardTemplet> m_lstRewardByTry = new List<GuildSeasonRewardTemplet>();

		// Token: 0x04006E96 RID: 28310
		private GuildSeasonTemplet m_GuildSeasonTemplet;

		// Token: 0x04006E97 RID: 28311
		private GuildDungeonRewardCategory m_CurCategory;

		// Token: 0x04006E98 RID: 28312
		private float m_fDeltaTime;

		// Token: 0x020018C8 RID: 6344
		// (Invoke) Token: 0x0600B6B1 RID: 46769
		public delegate void OnClose();
	}
}
