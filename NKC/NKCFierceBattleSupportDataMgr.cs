using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientPacket.Common;
using ClientPacket.Game;
using ClientPacket.LeaderBoard;
using NKC.UI;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000675 RID: 1653
	public class NKCFierceBattleSupportDataMgr
	{
		// Token: 0x060034A4 RID: 13476 RVA: 0x00109C18 File Offset: 0x00107E18
		public static void LoadFromLua()
		{
			NKMTempletContainer<NKMFierceTemplet>.Load("AB_SCRIPT", "LUA_FIERCE_TEMPLET", "FIERCE_TEMPLET", new Func<NKMLua, NKMFierceTemplet>(NKMFierceTemplet.LoadFromLUA));
			NKMTempletContainer<NKMFierceBossGroupTemplet>.Load("AB_SCRIPT", "LUA_FIERCE_BOSS_GROUP_TEMPLET", "FIERCE_GROUP_TEMPLET", new Func<NKMLua, NKMFierceBossGroupTemplet>(NKMFierceBossGroupTemplet.LoadFromLUA));
			NKMTempletContainer<NKMFiercePointRewardTemplet>.Load("AB_SCRIPT", "LUA_FIERCE_POINT_REWARD", "FIERCE_POINT_REWARD_TEMPLET", new Func<NKMLua, NKMFiercePointRewardTemplet>(NKMFiercePointRewardTemplet.LoadFromLUA));
			NKMTempletContainer<NKMFierceRankRewardTemplet>.Load("AB_SCRIPT", "LUA_FIERCE_RANK_REWARD", "FIERCE_RANK_REWARD_TEMPLET", new Func<NKMLua, NKMFierceRankRewardTemplet>(NKMFierceRankRewardTemplet.LoadFromLUA));
			NKMTempletContainer<NKMFiercePenaltyTemplet>.Load("AB_SCRIPT", "LUA_FIERCE_PENALTY", "FIERCE_PENALTY", new Func<NKMLua, NKMFiercePenaltyTemplet>(NKMFiercePenaltyTemplet.LoadFromLua));
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x060034A5 RID: 13477 RVA: 0x00109CC5 File Offset: 0x00107EC5
		public NKMFierceTemplet FierceTemplet
		{
			get
			{
				return this.m_FierceTemplet;
			}
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x00109CD0 File Offset: 0x00107ED0
		public void Init(int fierceID)
		{
			foreach (NKMFierceTemplet nkmfierceTemplet in NKMFierceTemplet.Values)
			{
				if (nkmfierceTemplet.FierceID == fierceID)
				{
					this.m_FierceTemplet = nkmfierceTemplet;
					this.ResetBossData();
					return;
				}
			}
			Debug.Log(string.Format("격전지원 정보를 확인 할 수 없습니다. - {0}", fierceID));
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x00109D44 File Offset: 0x00107F44
		private void ResetBossData()
		{
			if (this.m_FierceTemplet != null)
			{
				this.m_iCurFierceBossGroupID = this.m_FierceTemplet.FierceBossGroupIdList[0];
				if (!NKMFierceBossGroupTemplet.Groups.ContainsKey(this.m_iCurFierceBossGroupID))
				{
					Debug.Log("격전 지원 데이터 확인 필요!");
					return;
				}
				NKMFierceBossGroupTemplet nkmfierceBossGroupTemplet = NKMFierceBossGroupTemplet.Groups[this.m_iCurFierceBossGroupID].First<NKMFierceBossGroupTemplet>();
				this.m_iCurBossID = nkmfierceBossGroupTemplet.FierceBossID;
			}
		}

		// Token: 0x060034A8 RID: 13480 RVA: 0x00109DAF File Offset: 0x00107FAF
		public int GetFierceBattleID()
		{
			if (this.m_FierceTemplet != null)
			{
				return this.m_FierceTemplet.FierceID;
			}
			return 0;
		}

		// Token: 0x060034A9 RID: 13481 RVA: 0x00109DC6 File Offset: 0x00107FC6
		public int GetCurSelectedBossLv()
		{
			return this.GetCurSelectedBossLv(this.m_iCurFierceBossGroupID);
		}

		// Token: 0x060034AA RID: 13482 RVA: 0x00109DD4 File Offset: 0x00107FD4
		public int GetCurSelectedBossLv(int iFierceBossGroupID)
		{
			if (!NKMFierceBossGroupTemplet.Groups.ContainsKey(iFierceBossGroupID))
			{
				return 0;
			}
			foreach (NKMFierceBossGroupTemplet nkmfierceBossGroupTemplet in NKMFierceBossGroupTemplet.Groups[iFierceBossGroupID])
			{
				if (nkmfierceBossGroupTemplet.FierceBossID == this.m_iCurBossID)
				{
					return nkmfierceBossGroupTemplet.Level;
				}
			}
			return 0;
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x060034AB RID: 13483 RVA: 0x00109E50 File Offset: 0x00108050
		public int CurBossID
		{
			get
			{
				return this.m_iCurBossID;
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x060034AC RID: 13484 RVA: 0x00109E58 File Offset: 0x00108058
		public int CurBossGroupID
		{
			get
			{
				return this.m_iCurFierceBossGroupID;
			}
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x00109E60 File Offset: 0x00108060
		public NKCFierceBattleSupportDataMgr.FIERCE_STATUS GetStatus()
		{
			if (NKCContentManager.IsContentAlwaysLocked(ContentsType.FIERCE, 0))
			{
				return NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_UNUSABLE;
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.FIERCE, 0, 0))
			{
				return NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_LOCKED;
			}
			if (this.m_FierceTemplet == null)
			{
				return NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_LOCKED;
			}
			if (!this.m_FierceTemplet.EnableByTag)
			{
				return NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_WAIT;
			}
			if (NKCSynchronizedTime.IsFinished(NKMTime.LocalToUTC(this.m_FierceTemplet.FierceGameStart, 0)))
			{
				if (NKCSynchronizedTime.IsEventTime(NKMTime.LocalToUTC(this.m_FierceTemplet.FierceGameStart, 0), NKMTime.LocalToUTC(this.m_FierceTemplet.FierceGameEnd, 0)))
				{
					return NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_ACTIVATE;
				}
				if (NKCSynchronizedTime.IsEventTime(NKMTime.LocalToUTC(this.m_FierceTemplet.FierceRewardPeriodStart, 0), NKMTime.LocalToUTC(this.m_FierceTemplet.FierceRewardPeriodEnd, 0)))
				{
					if (this.IsPossibleRankReward())
					{
						return NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_REWARD;
					}
					return NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_COMPLETE;
				}
			}
			return NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_WAIT;
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x00109F1C File Offset: 0x0010811C
		private void UpdateNextStepUTCTime()
		{
			this.m_NextStepTime = DateTime.MinValue;
			switch (this.GetStatus())
			{
			case NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_UNUSABLE:
			case NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_LOCKED:
				break;
			case NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_WAIT:
				this.m_NextStepTime = NKMTime.LocalToUTC(this.m_FierceTemplet.FierceGameStart, 0);
				return;
			case NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_ACTIVATE:
				this.m_NextStepTime = NKMTime.LocalToUTC(this.m_FierceTemplet.FierceGameEnd, 0);
				return;
			case NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_REWARD:
			case NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_COMPLETE:
				this.m_NextStepTime = NKMTime.LocalToUTC(this.m_FierceTemplet.FierceRewardPeriodEnd, 0);
				break;
			default:
				return;
			}
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x00109FA4 File Offset: 0x001081A4
		public string GetLeftTimeString()
		{
			string result = "";
			if (this.m_NextStepTime == DateTime.MinValue)
			{
				this.UpdateNextStepUTCTime();
			}
			TimeSpan timeLeft = NKCSynchronizedTime.GetTimeLeft(this.m_NextStepTime);
			if (this.m_NextStepTime != DateTime.MinValue && timeLeft.Ticks <= 0L)
			{
				this.UpdateNextStepUTCTime();
				timeLeft = NKCSynchronizedTime.GetTimeLeft(this.m_NextStepTime);
			}
			switch (this.GetStatus())
			{
			case NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_WAIT:
				if (timeLeft.TotalDays >= 1.0)
				{
					result = string.Format(NKCUtilString.GET_FIERCE_WAIT_ACTIVATE_DAY_DESC_01, (int)timeLeft.TotalDays);
				}
				else if (timeLeft.TotalHours >= 1.0)
				{
					result = string.Format(NKCUtilString.GET_FIERCE_WAIT_ACTIVATE_HOUR_DESC_01, (int)timeLeft.TotalHours);
				}
				else if (timeLeft.TotalMinutes >= 1.0)
				{
					result = string.Format(NKCUtilString.GET_FIERCE_WAIT_ACTIVATE_MINUTE_DESC_01, (int)timeLeft.TotalMinutes);
				}
				else
				{
					result = string.Format(NKCUtilString.GET_FIERCE_WAIT_ACTIVATE_SECOND_DESC_01, (int)timeLeft.TotalSeconds);
				}
				break;
			case NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_ACTIVATE:
				if (timeLeft.TotalDays >= 1.0)
				{
					result = string.Format(NKCUtilString.GET_FIERCE_WAIT_REWARD_DAY_DESC_01, (int)timeLeft.TotalDays);
				}
				else if (timeLeft.TotalHours >= 1.0)
				{
					result = string.Format(NKCUtilString.GET_FIERCE_WAIT_REWARD_HOUR_DESC_01, (int)timeLeft.TotalHours);
				}
				else if (timeLeft.TotalMinutes >= 1.0)
				{
					result = string.Format(NKCUtilString.GET_FIERCE_WAIT_REWARD_MINUTE_DESC_01, (int)timeLeft.TotalMinutes);
				}
				else
				{
					result = string.Format(NKCUtilString.GET_FIERCE_WAIT_REWARD_SECOND_DESC_01, (int)timeLeft.TotalSeconds);
				}
				break;
			case NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_REWARD:
			case NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_COMPLETE:
				if (timeLeft.TotalDays >= 1.0)
				{
					result = string.Format(NKCUtilString.GET_FIERCE_WAIT_END_DAY_DESC_01, (int)timeLeft.TotalDays);
				}
				else if (timeLeft.TotalHours >= 1.0)
				{
					result = string.Format(NKCUtilString.GET_FIERCE_WAIT_END_HOUR_DESC_01, (int)timeLeft.TotalHours);
				}
				else if (timeLeft.TotalMinutes >= 1.0)
				{
					result = string.Format(NKCUtilString.GET_FIERCE_WAIT_END_MINUTE_DESC_01, (int)timeLeft.TotalMinutes);
				}
				else
				{
					result = string.Format(NKCUtilString.GET_FIERCE_WAIT_END_SECOND_DESC_01, (int)timeLeft.TotalSeconds);
				}
				break;
			}
			return result;
		}

		// Token: 0x060034B0 RID: 13488 RVA: 0x0010A220 File Offset: 0x00108420
		public bool IsCanAccessFierce()
		{
			NKCFierceBattleSupportDataMgr.FIERCE_STATUS status = this.GetStatus();
			return status - NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_ACTIVATE <= 2;
		}

		// Token: 0x060034B1 RID: 13489 RVA: 0x0010A240 File Offset: 0x00108440
		public string GetAccessDeniedMessage()
		{
			if (this.GetStatus() != NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_WAIT || this.m_FierceTemplet == null)
			{
				return NKCUtilString.GET_FIERCE_CAN_NOT_ENTER_FIERCE_BATTLE_SUPPORT;
			}
			if (!NKCSynchronizedTime.IsFinished(NKMTime.LocalToUTC(this.m_FierceTemplet.FierceGameStart, 0)))
			{
				return string.Format(NKCUtilString.GET_FIERCE_ENTER_WAIT_DESC_01, NKCSynchronizedTime.GetTimeLeftString(NKMTime.LocalToUTC(this.m_FierceTemplet.FierceGameStart, 0)));
			}
			return NKCUtilString.GET_FIERCE_BATTLE_ENTER_SEASON_END;
		}

		// Token: 0x060034B2 RID: 13490 RVA: 0x0010A2A2 File Offset: 0x001084A2
		public void SetCurBossID(int iCurBossID)
		{
			this.m_iCurBossID = iCurBossID;
		}

		// Token: 0x060034B3 RID: 13491 RVA: 0x0010A2AC File Offset: 0x001084AC
		public int GetBossGroupPoint()
		{
			int num = 0;
			IReadOnlyList<NKMFierceBossGroupTemplet> bossGroupList = this.GetBossGroupList();
			if (bossGroupList != null && bossGroupList.Count > 0)
			{
				using (IEnumerator<NKMFierceBossGroupTemplet> enumerator = bossGroupList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						NKMFierceBossGroupTemplet group = enumerator.Current;
						NKMFierceBoss nkmfierceBoss = this.m_lstFierceBoss.Find((NKMFierceBoss e) => e.bossId == group.FierceBossID);
						if (nkmfierceBoss != null && num < nkmfierceBoss.point)
						{
							num = nkmfierceBoss.point;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x060034B4 RID: 13492 RVA: 0x0010A340 File Offset: 0x00108540
		public void UpdateFierceData(NKMPacket_FIERCE_DATA_ACK sPacket)
		{
			this.m_PointReward = sPacket.pointRewardHistory;
			this.totalRankPercent = sPacket.rankPercent;
			this.totalRankNumber = sPacket.rankNumber;
			this.m_bReceivedRankReward = sPacket.isRankRewardGotten;
			this.m_lstFierceBoss.Clear();
			this.m_lstFierceBoss = sPacket.bossList;
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x0010A394 File Offset: 0x00108594
		public void SetReceivedRankReward()
		{
			this.m_bReceivedRankReward = true;
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x060034B6 RID: 13494 RVA: 0x0010A39D File Offset: 0x0010859D
		// (set) Token: 0x060034B7 RID: 13495 RVA: 0x0010A3A5 File Offset: 0x001085A5
		public bool m_fierceDailyRewardReceived { get; private set; }

		// Token: 0x060034B8 RID: 13496 RVA: 0x0010A3AE File Offset: 0x001085AE
		public void SetDailyRewardReceived(bool bfierceDailyRewardReceived)
		{
			this.m_fierceDailyRewardReceived = bfierceDailyRewardReceived;
		}

		// Token: 0x060034B9 RID: 13497 RVA: 0x0010A3B8 File Offset: 0x001085B8
		public void UpdateFierceData(NKMPacket_LEADERBOARD_FIERCE_BOSSGROUP_LIST_ACK sPacket)
		{
			if (this.m_dicFierceRanking.ContainsKey(sPacket.fierceBossGroupId))
			{
				NKCFierceBattleSupportDataMgr.FierceRankData value = this.m_dicFierceRanking[sPacket.fierceBossGroupId];
				value.IsAll = sPacket.isAll;
				value.LeaderBoardFierceData = sPacket.leaderBoardfierceData;
				value.UserRank = sPacket.userRank;
				this.m_dicFierceRanking[sPacket.fierceBossGroupId] = value;
				return;
			}
			NKCFierceBattleSupportDataMgr.FierceRankData value2 = default(NKCFierceBattleSupportDataMgr.FierceRankData);
			value2.IsAll = sPacket.isAll;
			value2.LeaderBoardFierceData = sPacket.leaderBoardfierceData;
			value2.UserRank = sPacket.userRank;
			this.m_dicFierceRanking.Add(sPacket.fierceBossGroupId, value2);
		}

		// Token: 0x060034BA RID: 13498 RVA: 0x0010A465 File Offset: 0x00108665
		public bool IsReceivedPointReward(int rewardID)
		{
			return this.m_PointReward != null && this.m_PointReward.Contains(rewardID);
		}

		// Token: 0x060034BB RID: 13499 RVA: 0x0010A480 File Offset: 0x00108680
		public bool IsCanReceivePointReward()
		{
			if (NKMFiercePointRewardTemplet.Groups.ContainsKey(this.m_FierceTemplet.PointRewardGroupID))
			{
				List<NKMFiercePointRewardTemplet> list = NKMFiercePointRewardTemplet.Groups[this.m_FierceTemplet.PointRewardGroupID];
				int totalPoint = this.GetTotalPoint();
				foreach (NKMFiercePointRewardTemplet nkmfiercePointRewardTemplet in list)
				{
					if (nkmfiercePointRewardTemplet.Point <= totalPoint && !this.IsReceivedPointReward(nkmfiercePointRewardTemplet.FiercePointRewardID))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060034BC RID: 13500 RVA: 0x0010A518 File Offset: 0x00108718
		public string GetRankingDesc()
		{
			int clearLevel = this.GetClearLevel(0);
			if (NKMFierceBossGroupTemplet.Groups.ContainsKey(this.m_iCurFierceBossGroupID))
			{
				using (List<NKMFierceBossGroupTemplet>.Enumerator enumerator = NKMFierceBossGroupTemplet.Groups[this.m_iCurFierceBossGroupID].GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						NKMFierceBossGroupTemplet data = enumerator.Current;
						NKMFierceBoss nkmfierceBoss = this.m_lstFierceBoss.Find((NKMFierceBoss i) => i.bossId == data.FierceBossID);
						if (nkmfierceBoss != null && data.Level == clearLevel)
						{
							if (nkmfierceBoss.rankNumber != 0 && nkmfierceBoss.rankNumber <= 100)
							{
								return string.Format(NKCUtilString.GET_FIERCE_RANK_IN_TOP_100_DESC_01, nkmfierceBoss.rankNumber);
							}
							return string.Format(NKCUtilString.GET_FIERCE_RANK_DESC_01, nkmfierceBoss.rankPercent);
						}
					}
				}
			}
			return string.Format(NKCUtilString.GET_FIERCE_RANK_DESC_01, 100);
		}

		// Token: 0x060034BD RID: 13501 RVA: 0x0010A620 File Offset: 0x00108820
		public string GetRankingTotalDesc()
		{
			if (this.totalRankNumber != 0 && this.totalRankNumber <= 100)
			{
				return string.Format(NKCUtilString.GET_FIERCE_RANK_IN_TOP_100_DESC_01, this.totalRankNumber);
			}
			return string.Format(NKCUtilString.GET_FIERCE_RANK_DESC_01, this.totalRankPercent);
		}

		// Token: 0x060034BE RID: 13502 RVA: 0x0010A65F File Offset: 0x0010885F
		public int GetRankingTotalNumber()
		{
			return this.totalRankNumber;
		}

		// Token: 0x060034BF RID: 13503 RVA: 0x0010A667 File Offset: 0x00108867
		public int GetRankingTotalPercent()
		{
			return this.totalRankPercent;
		}

		// Token: 0x060034C0 RID: 13504 RVA: 0x0010A670 File Offset: 0x00108870
		public int GetTotalPoint()
		{
			int num = 0;
			if (this.m_FierceTemplet != null)
			{
				foreach (int targetGroupID in this.m_FierceTemplet.FierceBossGroupIdList)
				{
					num += this.GetMaxPoint(targetGroupID);
				}
			}
			return num;
		}

		// Token: 0x060034C1 RID: 13505 RVA: 0x0010A6D8 File Offset: 0x001088D8
		public int GetMaxPoint(int targetGroupID = 0)
		{
			int num = 0;
			targetGroupID = ((targetGroupID == 0) ? this.m_iCurFierceBossGroupID : targetGroupID);
			if (NKMFierceBossGroupTemplet.Groups.ContainsKey(targetGroupID))
			{
				using (List<NKMFierceBossGroupTemplet>.Enumerator enumerator = NKMFierceBossGroupTemplet.Groups[targetGroupID].GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						NKMFierceBossGroupTemplet data = enumerator.Current;
						NKMFierceBoss nkmfierceBoss = this.m_lstFierceBoss.Find((NKMFierceBoss i) => i.bossId == data.FierceBossID);
						if (nkmfierceBoss != null)
						{
							num = Math.Max(nkmfierceBoss.point, num);
						}
					}
				}
			}
			return num;
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x0010A77C File Offset: 0x0010897C
		public int GetClearLevel(int bossGroupID = 0)
		{
			bossGroupID = ((bossGroupID == 0) ? this.m_iCurFierceBossGroupID : bossGroupID);
			int num = 0;
			if (NKMFierceBossGroupTemplet.Groups.ContainsKey(bossGroupID))
			{
				using (List<NKMFierceBossGroupTemplet>.Enumerator enumerator = NKMFierceBossGroupTemplet.Groups[bossGroupID].GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						NKMFierceBossGroupTemplet data = enumerator.Current;
						NKMFierceBoss nkmfierceBoss = this.m_lstFierceBoss.Find((NKMFierceBoss i) => i.bossId == data.FierceBossID);
						if (nkmfierceBoss != null && nkmfierceBoss.isCleared)
						{
							num = data.Level;
						}
					}
				}
			}
			Debug.Log(string.Format("보스 클리어 레벨 - bossGroupID : {0}, clearLevel : {1}", bossGroupID, num));
			return num;
		}

		// Token: 0x060034C3 RID: 13507 RVA: 0x0010A840 File Offset: 0x00108A40
		public int GetTargetDungeonID()
		{
			IReadOnlyList<NKMFierceBossGroupTemplet> bossGroupList = this.GetBossGroupList();
			if (bossGroupList != null)
			{
				int curSelectedBossLv = this.GetCurSelectedBossLv(this.m_iCurFierceBossGroupID);
				foreach (NKMFierceBossGroupTemplet nkmfierceBossGroupTemplet in bossGroupList)
				{
					if (nkmfierceBossGroupTemplet.Level == curSelectedBossLv)
					{
						return nkmfierceBossGroupTemplet.DungeonID;
					}
				}
				return 0;
			}
			return 0;
		}

		// Token: 0x060034C4 RID: 13508 RVA: 0x0010A8B0 File Offset: 0x00108AB0
		public string GetCurBossName()
		{
			return this.GetTargetBossName(this.m_iCurFierceBossGroupID, this.GetCurSelectedBossLv(this.m_iCurFierceBossGroupID));
		}

		// Token: 0x060034C5 RID: 13509 RVA: 0x0010A8CC File Offset: 0x00108ACC
		public string GetTargetBossName(int targetBossGroupID, int curBossLv = 0)
		{
			IReadOnlyList<NKMFierceBossGroupTemplet> bossGroupList = this.GetBossGroupList(targetBossGroupID);
			if (bossGroupList != null && bossGroupList.Count > 0)
			{
				foreach (NKMFierceBossGroupTemplet nkmfierceBossGroupTemplet in bossGroupList)
				{
					if (nkmfierceBossGroupTemplet.Level == curBossLv)
					{
						NKMDungeonTemplet dungeonTemplet = NKMDungeonManager.GetDungeonTemplet(nkmfierceBossGroupTemplet.DungeonID);
						if (dungeonTemplet != null)
						{
							NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(dungeonTemplet.m_BossUnitStrID);
							if (unitTempletBase != null)
							{
								return unitTempletBase.GetUnitName();
							}
						}
					}
				}
			}
			return "";
		}

		// Token: 0x060034C6 RID: 13510 RVA: 0x0010A960 File Offset: 0x00108B60
		public string GetCurBossDesc()
		{
			NKMFierceBossGroupTemplet bossGroupTemplet = this.GetBossGroupTemplet();
			if (bossGroupTemplet != null && !string.IsNullOrEmpty(bossGroupTemplet.UI_BossDesc))
			{
				return NKCStringTable.GetString(bossGroupTemplet.UI_BossDesc, false);
			}
			return "";
		}

		// Token: 0x060034C7 RID: 13511 RVA: 0x0010A998 File Offset: 0x00108B98
		public List<NKMBattleConditionTemplet> GetCurBattleCondition(bool bAdditionSelfPenalty = false)
		{
			List<NKMBattleConditionTemplet> list = new List<NKMBattleConditionTemplet>();
			foreach (NKMFierceBossGroupTemplet nkmfierceBossGroupTemplet in NKMFierceBossGroupTemplet.Values)
			{
				if (nkmfierceBossGroupTemplet.FierceBossID == this.m_iCurBossID)
				{
					NKMBattleConditionTemplet templetByStrID = NKMBattleConditionManager.GetTempletByStrID(nkmfierceBossGroupTemplet.BCondStrID_1);
					if (templetByStrID != null)
					{
						list.Add(templetByStrID);
					}
					NKMBattleConditionTemplet templetByStrID2 = NKMBattleConditionManager.GetTempletByStrID(nkmfierceBossGroupTemplet.BCondStrID_2);
					if (templetByStrID2 != null)
					{
						list.Add(templetByStrID2);
						break;
					}
					break;
				}
			}
			if (bAdditionSelfPenalty)
			{
				List<NKMBattleConditionTemplet> selfPenaltyBattleCondList = this.GetSelfPenaltyBattleCondList();
				if (selfPenaltyBattleCondList != null)
				{
					list.AddRange(selfPenaltyBattleCondList);
				}
			}
			return list;
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x0010AA3C File Offset: 0x00108C3C
		public IReadOnlyList<NKMFierceBossGroupTemplet> GetBossGroupList()
		{
			return this.GetBossGroupList(this.m_iCurFierceBossGroupID);
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x0010AA4A File Offset: 0x00108C4A
		public IReadOnlyList<NKMFierceBossGroupTemplet> GetBossGroupList(int fierceBossGroupID)
		{
			if (NKMFierceBossGroupTemplet.Groups.ContainsKey(fierceBossGroupID))
			{
				return NKMFierceBossGroupTemplet.Groups[fierceBossGroupID];
			}
			return null;
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x0010AA68 File Offset: 0x00108C68
		public NKMEventDeckData GetBestLineUp()
		{
			NKMEventDeckData result = null;
			NKMFierceBossGroupTemplet[] array = this.GetBossGroupList().ToArray<NKMFierceBossGroupTemplet>();
			if (array != null)
			{
				int num = 0;
				foreach (NKMFierceBoss nkmfierceBoss in this.m_lstFierceBoss)
				{
					NKMFierceBossGroupTemplet[] array2 = array;
					int i = 0;
					while (i < array2.Length)
					{
						if (array2[i].FierceBossID == nkmfierceBoss.bossId && nkmfierceBoss.deckData != null)
						{
							if (nkmfierceBoss.deckData.m_dicUnit.Count > 0 && nkmfierceBoss.point > num)
							{
								num = nkmfierceBoss.point;
								result = nkmfierceBoss.deckData;
								break;
							}
							break;
						}
						else
						{
							i++;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x0010AB30 File Offset: 0x00108D30
		public string GetStringCurBossSelfPenalty()
		{
			string result = "";
			List<int> selfPenalty = this.GetSelfPenalty();
			if (selfPenalty != null && selfPenalty.Count > 0)
			{
				float num = 0f;
				foreach (int key in selfPenalty)
				{
					NKMFiercePenaltyTemplet nkmfiercePenaltyTemplet = NKMTempletContainer<NKMFiercePenaltyTemplet>.Find(key);
					if (nkmfiercePenaltyTemplet != null)
					{
						num += nkmfiercePenaltyTemplet.FierceScoreRate;
					}
				}
				num *= 0.01f;
				if (num < 0f)
				{
					num *= -1f;
					result = string.Format(NKCUtilString.GET_STRING_FIERCE_PENALTY_SCORE_MINUS_DESC, num);
				}
				else
				{
					result = string.Format(NKCUtilString.GET_STRING_FIERCE_PENALTY_SCORE_PLUS_DESC, num);
				}
			}
			return result;
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x0010ABF0 File Offset: 0x00108DF0
		public bool IsCanStart()
		{
			NKMFierceBossGroupTemplet bossGroupTemplet = this.GetBossGroupTemplet(this.m_iCurBossID);
			if (bossGroupTemplet != null)
			{
				NKMInventoryData inventoryData = NKCScenManager.CurrentUserData().m_InventoryData;
				if (inventoryData != null && inventoryData.GetCountMiscItem(bossGroupTemplet.StageReqItemID) < (long)bossGroupTemplet.StageReqItemCount)
				{
					return false;
				}
			}
			if (NKCScenManager.CurrentUserData() != null && this.m_FierceTemplet.DailyEnterLimit - NKCScenManager.CurrentUserData().GetStatePlayCnt(NKMFierceConst.StageId, true, false, false) <= 0)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_FIERCE_BATTLE_ENTER_LIMIT, null, "");
				return false;
			}
			return true;
		}

		// Token: 0x060034CD RID: 13517 RVA: 0x0010AC72 File Offset: 0x00108E72
		public int GetRecommandOperationPower()
		{
			return this.GetBossGroupTemplet(this.m_iCurBossID).OperationPower;
		}

		// Token: 0x060034CE RID: 13518 RVA: 0x0010AC85 File Offset: 0x00108E85
		public NKMFierceBossGroupTemplet GetBossGroupTemplet()
		{
			return this.GetBossGroupTemplet(this.m_iCurBossID);
		}

		// Token: 0x060034CF RID: 13519 RVA: 0x0010AC93 File Offset: 0x00108E93
		private NKMFierceBossGroupTemplet GetBossGroupTemplet(int bossID)
		{
			return NKMFierceBossGroupTemplet.Find(bossID);
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x0010AC9C File Offset: 0x00108E9C
		public bool IsHasFierceRankingData(bool All = false)
		{
			bool flag = false;
			if (this.m_dicFierceRanking.ContainsKey(this.m_iCurFierceBossGroupID))
			{
				flag = (this.m_dicFierceRanking[this.m_iCurFierceBossGroupID].LeaderBoardFierceData != null && this.m_dicFierceRanking[this.m_iCurFierceBossGroupID].LeaderBoardFierceData.fierceData != null && this.m_dicFierceRanking[this.m_iCurFierceBossGroupID].LeaderBoardFierceData.fierceData.Count > 0);
			}
			if (All && flag)
			{
				flag = this.m_dicFierceRanking[this.m_iCurFierceBossGroupID].IsAll;
			}
			return flag;
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x0010AD38 File Offset: 0x00108F38
		public int GetBossGroupRankingDataCnt(int targetBossGroupID = 0)
		{
			targetBossGroupID = ((targetBossGroupID == 0) ? this.m_iCurFierceBossGroupID : targetBossGroupID);
			if (this.m_dicFierceRanking.ContainsKey(targetBossGroupID) && this.m_dicFierceRanking[targetBossGroupID].LeaderBoardFierceData != null && this.m_dicFierceRanking[targetBossGroupID].LeaderBoardFierceData.fierceData != null)
			{
				return this.m_dicFierceRanking[targetBossGroupID].LeaderBoardFierceData.fierceData.Count;
			}
			return 0;
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x0010ADAC File Offset: 0x00108FAC
		public NKMFierceData GetFierceRankingData(int Rank)
		{
			if (!this.m_dicFierceRanking.ContainsKey(this.m_iCurFierceBossGroupID))
			{
				return null;
			}
			NKMLeaderBoardFierceData leaderBoardFierceData = this.m_dicFierceRanking[this.m_iCurFierceBossGroupID].LeaderBoardFierceData;
			if (leaderBoardFierceData != null && leaderBoardFierceData.fierceData != null && leaderBoardFierceData.fierceData.Count > Rank)
			{
				return leaderBoardFierceData.fierceData[Rank];
			}
			return null;
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x0010AE0C File Offset: 0x0010900C
		public bool IsPossibleRankReward()
		{
			return this.GetTotalPoint() > 0 && !this.m_bReceivedRankReward;
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x0010AE22 File Offset: 0x00109022
		public void UpdateRecevePointRewardID(int receivedPointRewardID)
		{
			if (this.m_PointReward != null && !this.m_PointReward.Contains(receivedPointRewardID))
			{
				this.m_PointReward.Add(receivedPointRewardID);
			}
		}

		// Token: 0x060034D5 RID: 13525 RVA: 0x0010AE48 File Offset: 0x00109048
		public List<NKMBattleConditionTemplet> GetSelfPenaltyBattleCondList()
		{
			List<NKMBattleConditionTemplet> list = new List<NKMBattleConditionTemplet>();
			foreach (int key in this.GetSelfPenalty())
			{
				NKMFiercePenaltyTemplet nkmfiercePenaltyTemplet = NKMTempletContainer<NKMFiercePenaltyTemplet>.Find(key);
				if (nkmfiercePenaltyTemplet != null && nkmfiercePenaltyTemplet.battleCondition != null)
				{
					list.Add(nkmfiercePenaltyTemplet.battleCondition);
				}
			}
			return list;
		}

		// Token: 0x060034D6 RID: 13526 RVA: 0x0010AEB8 File Offset: 0x001090B8
		public List<int> GetSelfPenalty()
		{
			List<int> list = new List<int>();
			string curPenaltyKey = NKCFierceBattleSupportDataMgr.GetCurPenaltyKey(this.m_iCurBossID);
			if (PlayerPrefs.HasKey(curPenaltyKey))
			{
				string @string = PlayerPrefs.GetString(curPenaltyKey);
				Debug.Log("<color=red>[격전지원:패널티]GetCurBossSelfPenalty - " + @string + "</color>");
				string[] array = @string.Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					int num;
					int.TryParse(array[i], out num);
					NKMFiercePenaltyTemplet nkmfiercePenaltyTemplet = NKMTempletContainer<NKMFiercePenaltyTemplet>.Find(num);
					if (nkmfiercePenaltyTemplet != null)
					{
						list.Add(num);
					}
				}
			}
			return list;
		}

		// Token: 0x060034D7 RID: 13527 RVA: 0x0010AF44 File Offset: 0x00109144
		public void SetSelfPenalty(List<int> lstPenalyIDs)
		{
			string curPenaltyKey = NKCFierceBattleSupportDataMgr.GetCurPenaltyKey(this.m_iCurBossID);
			if (lstPenalyIDs != null && lstPenalyIDs.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (int num in lstPenalyIDs)
				{
					stringBuilder.Append(string.Format("{0},", num));
				}
				Debug.Log("<color=red>[격전지원:패널티]SetCurBossSelfPenalty - " + stringBuilder.ToString() + "</color>");
				PlayerPrefs.SetString(curPenaltyKey, stringBuilder.ToString());
				return;
			}
			PlayerPrefs.DeleteKey(curPenaltyKey);
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x0010AFF0 File Offset: 0x001091F0
		public void SendPenaltyReq()
		{
			foreach (NKMFierceBossGroupTemplet nkmfierceBossGroupTemplet in this.GetBossGroupList())
			{
				if (nkmfierceBossGroupTemplet.FierceBossID == this.m_iCurBossID && !nkmfierceBossGroupTemplet.IsBossMaxLevel())
				{
					return;
				}
			}
			List<int> selfPenalty = this.GetSelfPenalty();
			NKCPacketSender.Send_NKMPacket_FIERCE_PENALTY_REQ(this.m_iCurBossID, selfPenalty);
		}

		// Token: 0x060034D9 RID: 13529 RVA: 0x0010B064 File Offset: 0x00109264
		private static string GetCurPenaltyKey(int targetBossID)
		{
			long userUID = NKCScenManager.CurrentUserData().m_UserUID;
			return string.Format(string.Format("FIERCE_PENALTY_{0}_{1}", userUID.ToString(), targetBossID), Array.Empty<object>());
		}

		// Token: 0x060034DA RID: 13530 RVA: 0x0010B0A0 File Offset: 0x001092A0
		public static void DeleteCurBossPenaltyData(int targetBossID)
		{
			string curPenaltyKey = NKCFierceBattleSupportDataMgr.GetCurPenaltyKey(targetBossID);
			if (!string.IsNullOrEmpty(curPenaltyKey))
			{
				PlayerPrefs.DeleteKey(curPenaltyKey);
			}
		}

		// Token: 0x040032DD RID: 13021
		public const int FIERCE_RANKING_DISPLAY_SIZE = 50;

		// Token: 0x040032DE RID: 13022
		private NKMFierceTemplet m_FierceTemplet;

		// Token: 0x040032DF RID: 13023
		private int m_iCurBossID;

		// Token: 0x040032E0 RID: 13024
		private int m_iCurFierceBossGroupID;

		// Token: 0x040032E1 RID: 13025
		private DateTime m_NextStepTime = DateTime.MinValue;

		// Token: 0x040032E2 RID: 13026
		private List<NKMFierceBoss> m_lstFierceBoss = new List<NKMFierceBoss>();

		// Token: 0x040032E3 RID: 13027
		private HashSet<int> m_PointReward = new HashSet<int>();

		// Token: 0x040032E4 RID: 13028
		private bool m_bReceivedRankReward;

		// Token: 0x040032E5 RID: 13029
		private int totalRankPercent;

		// Token: 0x040032E6 RID: 13030
		private int totalRankNumber;

		// Token: 0x040032E8 RID: 13032
		private Dictionary<int, NKCFierceBattleSupportDataMgr.FierceRankData> m_dicFierceRanking = new Dictionary<int, NKCFierceBattleSupportDataMgr.FierceRankData>();

		// Token: 0x0200131D RID: 4893
		public enum FIERCE_STATUS
		{
			// Token: 0x04009871 RID: 39025
			FS_UNUSABLE,
			// Token: 0x04009872 RID: 39026
			FS_LOCKED,
			// Token: 0x04009873 RID: 39027
			FS_WAIT,
			// Token: 0x04009874 RID: 39028
			FS_ACTIVATE,
			// Token: 0x04009875 RID: 39029
			FS_REWARD,
			// Token: 0x04009876 RID: 39030
			FS_COMPLETE
		}

		// Token: 0x0200131E RID: 4894
		private struct FierceBossData
		{
			// Token: 0x0600A523 RID: 42275 RVA: 0x00344F68 File Offset: 0x00343168
			public FierceBossData(int _point, bool _clear, NKMEventDeckData _deckData)
			{
				this.point = _point;
				this.bClear = _clear;
				this.deckData = _deckData;
			}

			// Token: 0x04009877 RID: 39031
			public int point;

			// Token: 0x04009878 RID: 39032
			public bool bClear;

			// Token: 0x04009879 RID: 39033
			public NKMEventDeckData deckData;
		}

		// Token: 0x0200131F RID: 4895
		private struct FierceRankData
		{
			// Token: 0x0400987A RID: 39034
			public NKMLeaderBoardFierceData LeaderBoardFierceData;

			// Token: 0x0400987B RID: 39035
			public int UserRank;

			// Token: 0x0400987C RID: 39036
			public bool IsAll;
		}
	}
}
