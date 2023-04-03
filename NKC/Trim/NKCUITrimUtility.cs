using System;
using System.Collections.Generic;
using System.Text;
using ClientPacket.Common;
using Cs.Logging;
using NKC.Templet;
using NKC.Trim;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI.Trim
{
	// Token: 0x02000AB5 RID: 2741
	public static class NKCUITrimUtility
	{
		// Token: 0x1700146A RID: 5226
		// (get) Token: 0x060079F4 RID: 31220 RVA: 0x00289B3C File Offset: 0x00287D3C
		public static bool OpenTagEnabled
		{
			get
			{
				return NKMOpenTagManager.IsOpened("DIMENSION_TRIM");
			}
		}

		// Token: 0x060079F5 RID: 31221 RVA: 0x00289B48 File Offset: 0x00287D48
		public static void TrimIntervalJoin()
		{
			if (NKCUITrimUtility.intervalTempletJoined)
			{
				return;
			}
			NKMTrimIntervalTemplet.Join();
			NKCUITrimUtility.intervalTempletJoined = true;
		}

		// Token: 0x060079F6 RID: 31222 RVA: 0x00289B60 File Offset: 0x00287D60
		public static void InitBattleCondition(Transform battleCondRoot, bool showToolTip)
		{
			if (battleCondRoot == null)
			{
				return;
			}
			NKCUIComBattleCondition[] componentsInChildren = battleCondRoot.GetComponentsInChildren<NKCUIComBattleCondition>(true);
			int num = (componentsInChildren != null) ? componentsInChildren.Length : 0;
			for (int i = 0; i < num; i++)
			{
				if (showToolTip)
				{
					componentsInChildren[i].Init(new NKCUIComBattleCondition.OnDownButton(NKCUITrimUtility.OnBCButtonDown));
				}
				else
				{
					componentsInChildren[i].Init(null);
				}
			}
		}

		// Token: 0x060079F7 RID: 31223 RVA: 0x00289BB8 File Offset: 0x00287DB8
		private static void OnBCButtonDown(string battleCondId, Vector3 position)
		{
			NKMBattleConditionTemplet templetByStrID = NKMBattleConditionManager.GetTempletByStrID(battleCondId);
			if (templetByStrID == null)
			{
				return;
			}
			NKCUITrimToolTip.Instance.Open(templetByStrID.BattleCondDesc_Translated, new Vector2?(position));
		}

		// Token: 0x060079F8 RID: 31224 RVA: 0x00289BEC File Offset: 0x00287DEC
		public static void SetBattleCondition(Transform battleCondRoot, NKMTrimTemplet trimTemplet, int trimLevel, bool showToolTip)
		{
			if (battleCondRoot == null)
			{
				return;
			}
			SortedSet<int> conditionList = new SortedSet<int>();
			if (trimTemplet != null)
			{
				Predicate<NKMTrimDungeonTemplet> <>9__0;
				Action<string> <>9__1;
				foreach (KeyValuePair<int, List<NKMTrimDungeonTemplet>> keyValuePair in trimTemplet.TrimDungeonTemplets)
				{
					List<NKMTrimDungeonTemplet> value = keyValuePair.Value;
					Predicate<NKMTrimDungeonTemplet> match;
					if ((match = <>9__0) == null)
					{
						match = (<>9__0 = ((NKMTrimDungeonTemplet e) => e.TrimLevelLow <= trimLevel && e.TrimLevelHigh >= trimLevel));
					}
					NKMTrimDungeonTemplet nkmtrimDungeonTemplet = value.Find(match);
					if (nkmtrimDungeonTemplet != null)
					{
						List<string> trimDungeonBattleCondition = nkmtrimDungeonTemplet.TrimDungeonBattleCondition;
						Action<string> action;
						if ((action = <>9__1) == null)
						{
							action = (<>9__1 = delegate(string e)
							{
								NKMBattleConditionTemplet templetByStrID = NKMBattleConditionManager.GetTempletByStrID(e);
								if (templetByStrID != null)
								{
									conditionList.Add(templetByStrID.BattleCondID);
								}
							});
						}
						trimDungeonBattleCondition.ForEach(action);
					}
				}
			}
			NKCUIComBattleCondition[] componentsInChildren = battleCondRoot.GetComponentsInChildren<NKCUIComBattleCondition>(true);
			int num = (componentsInChildren != null) ? componentsInChildren.Length : 0;
			int count = conditionList.Count;
			int num2 = 0;
			foreach (int data in conditionList)
			{
				NKCUIComBattleCondition nkcuicomBattleCondition;
				if (num2 >= num)
				{
					nkcuicomBattleCondition = NKCUIComBattleCondition.GetNewInstance("AB_UI_TRIM", "AB_UI_TRIM_BATTLE_CONDITION", battleCondRoot);
					if (showToolTip)
					{
						nkcuicomBattleCondition.SetButtonDownFunc(new NKCUIComBattleCondition.OnDownButton(NKCUITrimUtility.OnBCButtonDown));
					}
				}
				else
				{
					nkcuicomBattleCondition = componentsInChildren[num2];
				}
				if (nkcuicomBattleCondition != null)
				{
					NKCUtil.SetGameobjectActive(nkcuicomBattleCondition.gameObject, true);
					nkcuicomBattleCondition.SetData(data);
				}
				num2++;
			}
			for (int i = num2; i < num; i++)
			{
				if (!(componentsInChildren[i] == null))
				{
					NKCUtil.SetGameobjectActive(componentsInChildren[i].gameObject, false);
				}
			}
		}

		// Token: 0x060079F9 RID: 31225 RVA: 0x00289DA4 File Offset: 0x00287FA4
		public static string GetTrimDeckKey(int trimId, int trimDungeonId, long userUId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TRIM_");
			stringBuilder.Append(trimId);
			stringBuilder.Append("_");
			stringBuilder.Append(trimDungeonId);
			stringBuilder.Append("_");
			stringBuilder.Append(userUId);
			return stringBuilder.ToString();
		}

		// Token: 0x060079FA RID: 31226 RVA: 0x00289DF8 File Offset: 0x00287FF8
		public static int GetRemainEnterCount(NKMTrimIntervalTemplet trimInterval)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKMTrimIntervalData nkmtrimIntervalData = (nkmuserData != null) ? nkmuserData.TrimData.TrimIntervalData : null;
			int num = (trimInterval != null) ? trimInterval.WeeklyEnterLimit : 0;
			if (nkmtrimIntervalData != null)
			{
				num -= nkmtrimIntervalData.trimTryCount;
			}
			return num;
		}

		// Token: 0x060079FB RID: 31227 RVA: 0x00289E38 File Offset: 0x00288038
		public static string GetEnterLimitMsg(NKMTrimIntervalTemplet trimInterval)
		{
			int num = 0;
			if (trimInterval != null)
			{
				num = trimInterval.WeeklyEnterLimit;
			}
			int num2 = NKCUITrimUtility.GetRemainEnterCount(trimInterval);
			if (num2 < 0)
			{
				Log.Debug("���� Ƚ���� ���� ���� ���� ���� �Ѿ ����", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Trim/NKCUITrimUtility.cs", 152);
				num2 = 0;
			}
			string value = string.Format(NKCUtilString.GET_STRING_POPUP_DUNGEON_ENTER_LIMIT_DESC_WEEK_02, num2, num);
			StringBuilder stringBuilder = new StringBuilder();
			if (num2 > 0)
			{
				stringBuilder.Append("<color=#ffffffff>");
			}
			else
			{
				stringBuilder.Append("<color=#ff0000ff>");
			}
			stringBuilder.Append(value);
			stringBuilder.Append("</color>");
			return stringBuilder.ToString();
		}

		// Token: 0x060079FC RID: 31228 RVA: 0x00289EC8 File Offset: 0x002880C8
		public static DateTime GetRemainDateMsg()
		{
			NKCScenManager scenManager = NKCScenManager.GetScenManager();
			NKC_SCEN_TRIM nkc_SCEN_TRIM = (scenManager != null) ? scenManager.Get_NKC_SCEN_TRIM() : null;
			if (nkc_SCEN_TRIM != null)
			{
				NKMTrimIntervalTemplet nkmtrimIntervalTemplet = NKMTrimIntervalTemplet.Find(nkc_SCEN_TRIM.TrimIntervalId);
				if (nkmtrimIntervalTemplet != null)
				{
					return NKCSynchronizedTime.ToUtcTime(nkmtrimIntervalTemplet.IntervalTemplet.EndDate);
				}
			}
			return DateTime.MinValue;
		}

		// Token: 0x060079FD RID: 31229 RVA: 0x00289F10 File Offset: 0x00288110
		public static int GetTrimLevelScore(int trimId, int trimLevel)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKMTrimClearData nkmtrimClearData;
			if (nkmuserData == null)
			{
				nkmtrimClearData = null;
			}
			else
			{
				NKCTrimData trimData = nkmuserData.TrimData;
				if (trimData == null)
				{
					nkmtrimClearData = null;
				}
				else
				{
					List<NKMTrimClearData> trimClearList = trimData.TrimClearList;
					nkmtrimClearData = ((trimClearList != null) ? trimClearList.Find((NKMTrimClearData e) => e.trimId == trimId && e.trimLevel == trimLevel) : null);
				}
			}
			NKMTrimClearData nkmtrimClearData2 = nkmtrimClearData;
			if (nkmtrimClearData2 == null)
			{
				return 0;
			}
			return nkmtrimClearData2.score;
		}

		// Token: 0x060079FE RID: 31230 RVA: 0x00289F74 File Offset: 0x00288174
		public static int GetRecommendedPower(int trimGroup, int trimLevel)
		{
			NKMTrimPointTemplet nkmtrimPointTemplet = NKMTrimPointTemplet.Find(trimGroup, trimLevel);
			if (nkmtrimPointTemplet == null)
			{
				return 0;
			}
			return nkmtrimPointTemplet.RecommendCombatPoint;
		}

		// Token: 0x060079FF RID: 31231 RVA: 0x00289F94 File Offset: 0x00288194
		public static bool IsEnterCountRemaining(NKMTrimIntervalTemplet trimInterval)
		{
			return NKCUITrimUtility.GetRemainEnterCount(trimInterval) > 0;
		}

		// Token: 0x06007A00 RID: 31232 RVA: 0x00289FA0 File Offset: 0x002881A0
		public static bool IsRestoreEnterCountEnable(NKMTrimIntervalTemplet trimInterval, NKMUserData userData)
		{
			int num = (trimInterval != null) ? trimInterval.RestoreLimitCount : 0;
			int num2 = (userData != null) ? userData.TrimData.TrimIntervalData.trimRestoreCount : 0;
			return num > num2;
		}

		// Token: 0x06007A01 RID: 31233 RVA: 0x00289FD4 File Offset: 0x002881D4
		public static int GetRemainRestoreCount(NKMTrimIntervalTemplet trimInterval, NKMUserData userData)
		{
			int num = (trimInterval != null) ? trimInterval.RestoreLimitCount : 0;
			if (userData != null)
			{
				num -= userData.TrimData.TrimIntervalData.trimRestoreCount;
			}
			return num;
		}

		// Token: 0x06007A02 RID: 31234 RVA: 0x0028A005 File Offset: 0x00288205
		public static int GetRestoreLimitCount(NKMTrimIntervalTemplet trimInterval)
		{
			if (trimInterval == null)
			{
				return 0;
			}
			return trimInterval.RestoreLimitCount;
		}

		// Token: 0x06007A03 RID: 31235 RVA: 0x0028A012 File Offset: 0x00288212
		public static int GetRestoreItemReqId(NKMTrimIntervalTemplet trimInterval)
		{
			if (trimInterval == null)
			{
				return 0;
			}
			return trimInterval.RestoreLimitReqItemId;
		}

		// Token: 0x06007A04 RID: 31236 RVA: 0x0028A020 File Offset: 0x00288220
		public static int GetRestoreItemReqCount(NKMTrimIntervalTemplet trimInterval, NKMUserData userData)
		{
			if (trimInterval == null || userData == null)
			{
				return 0;
			}
			int trimRestoreCount = userData.TrimData.TrimIntervalData.trimRestoreCount;
			if (trimInterval.RestoreLimitReqItemCount.Length <= trimRestoreCount)
			{
				Log.Debug("���� Ƚ�� ���� ���� Ƚ�� �Ѱ� �ʰ�", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Trim/NKCUITrimUtility.cs", 247);
				return 0;
			}
			return trimInterval.RestoreLimitReqItemCount[trimRestoreCount];
		}

		// Token: 0x06007A05 RID: 31237 RVA: 0x0028A070 File Offset: 0x00288270
		public static int GetClearedTrimLevel(NKMUserData userData, int trimId)
		{
			int result = 0;
			if (userData != null)
			{
				result = userData.TrimData.GetClearedTrimLevel(trimId);
			}
			return result;
		}

		// Token: 0x06007A06 RID: 31238 RVA: 0x0028A090 File Offset: 0x00288290
		public static bool IsEnterCountLimited(NKMTrimIntervalTemplet trimInterval)
		{
			return trimInterval != null && !trimInterval.IsWeeklyUnLimit;
		}

		// Token: 0x06007A07 RID: 31239 RVA: 0x0028A0A0 File Offset: 0x002882A0
		public static bool IsRestoreEnterCountLimited(NKMTrimIntervalTemplet trimInterval)
		{
			return trimInterval != null && !trimInterval.IsRestoreUnLimit;
		}

		// Token: 0x06007A08 RID: 31240 RVA: 0x0028A0B0 File Offset: 0x002882B0
		public static bool IsUnlockInfoActive(UnlockInfo unlockInfo)
		{
			return unlockInfo.reqValue != 0 || unlockInfo.eReqType > STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_WARFARE;
		}

		// Token: 0x06007A09 RID: 31241 RVA: 0x0028A0C8 File Offset: 0x002882C8
		public static bool HaveEventDrop()
		{
			NKMTrimIntervalTemplet nkmtrimIntervalTemplet = NKMTrimIntervalTemplet.Find(NKCSynchronizedTime.ServiceTime);
			if (nkmtrimIntervalTemplet == null)
			{
				return false;
			}
			int[] trimSlot = nkmtrimIntervalTemplet.TrimSlot;
			int num = trimSlot.Length;
			for (int i = 0; i < num; i++)
			{
				if (NKCUITrimUtility.HaveEventDrop(trimSlot[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007A0A RID: 31242 RVA: 0x0028A10C File Offset: 0x0028830C
		public static bool HaveEventDrop(int trimId)
		{
			NKMTrimTemplet nkmtrimTemplet = NKMTrimTemplet.Find(trimId);
			if (nkmtrimTemplet == null)
			{
				return false;
			}
			for (int i = 1; i <= nkmtrimTemplet.MaxTrimLevel; i++)
			{
				if (NKCUITrimUtility.HaveEventDrop(trimId, i))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007A0B RID: 31243 RVA: 0x0028A144 File Offset: 0x00288344
		public static bool HaveEventDrop(int trimId, int trimLevel)
		{
			NKCTrimRewardTemplet nkctrimRewardTemplet = NKCTrimRewardTemplet.Find(trimId, trimLevel);
			if (nkctrimRewardTemplet == null)
			{
				return false;
			}
			bool result = false;
			int count = nkctrimRewardTemplet.EventDropIndex.Count;
			for (int i = 0; i < count; i++)
			{
				NKMRewardGroupTemplet rewardGroup = NKMRewardManager.GetRewardGroup(nkctrimRewardTemplet.EventDropIndex[i]);
				if (rewardGroup != null)
				{
					int count2 = rewardGroup.List.Count;
					for (int j = 0; j < count2; j++)
					{
						if (rewardGroup.List[j].intervalTemplet.IsValidTime(NKCSynchronizedTime.ServiceTime))
						{
							result = true;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x040066BE RID: 26302
		private const string openTag = "DIMENSION_TRIM";

		// Token: 0x040066BF RID: 26303
		private static bool intervalTempletJoined;
	}
}
