using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM;
using NKM.Templet.Base;

namespace NKC
{
	// Token: 0x0200069D RID: 1693
	public static class NKCEventRaceAnimationManager
	{
		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x060037B3 RID: 14259 RVA: 0x0011F3A1 File Offset: 0x0011D5A1
		public static bool DataExist
		{
			get
			{
				return NKCEventRaceAnimationManager.m_dicRaceEventSet.Count > 0;
			}
		}

		// Token: 0x060037B4 RID: 14260 RVA: 0x0011F3B0 File Offset: 0x0011D5B0
		public static bool LoadFromLua()
		{
			NKCEventRaceAnimationManager.m_dicRaceEventSet.Clear();
			IEnumerable<NKCEventRaceAnimationTemplet> enumerable = NKMTempletLoader.LoadCommonPath<NKCEventRaceAnimationTemplet>("AB_SCRIPT", "LUA_EVENT_RACE_ANIMATION_TEMPLET", "EVENT_RACE_ANIMATION_TEMPLET", new Func<NKMLua, NKCEventRaceAnimationTemplet>(NKCEventRaceAnimationTemplet.LoadFromLua));
			if (NKCEventRaceAnimationManager.m_dicRaceEventSet == null)
			{
				Log.Error("[NKCEventRaceAnimationTemplet] m_dicEventSet is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCManager/NKCEventRaceAnimationManager.cs", 22);
				return false;
			}
			foreach (NKCEventRaceAnimationTemplet nkceventRaceAnimationTemplet in enumerable)
			{
				if (!NKCEventRaceAnimationManager.m_dicRaceEventSet.ContainsKey(nkceventRaceAnimationTemplet.Key))
				{
					NKCEventRaceAnimationManager.m_dicRaceEventSet.Add(nkceventRaceAnimationTemplet.Key, new List<NKCEventRaceAnimationTemplet>());
				}
				NKCEventRaceAnimationManager.m_dicRaceEventSet[nkceventRaceAnimationTemplet.Key].Add(nkceventRaceAnimationTemplet);
			}
			return true;
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x0011F474 File Offset: 0x0011D674
		public static List<NKCEventRaceAnimationTemplet> Find(RaceEventType raceEventType)
		{
			List<NKCEventRaceAnimationTemplet> result;
			if (!NKCEventRaceAnimationManager.m_dicRaceEventSet.TryGetValue(raceEventType, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x060037B6 RID: 14262 RVA: 0x0011F494 File Offset: 0x0011D694
		public static int GetMaxCount(RaceEventType raceEventType)
		{
			NKCEventRaceAnimationTemplet nkceventRaceAnimationTemplet = NKCEventRaceAnimationManager.Find(raceEventType).Find((NKCEventRaceAnimationTemplet x) => string.IsNullOrEmpty(x.m_TargetObjName));
			if (nkceventRaceAnimationTemplet != null)
			{
				return nkceventRaceAnimationTemplet.m_MaxCount;
			}
			return 0;
		}

		// Token: 0x060037B7 RID: 14263 RVA: 0x0011F4D8 File Offset: 0x0011D6D8
		public static int GetMinIndex(RaceEventType raceEventType)
		{
			NKCEventRaceAnimationTemplet nkceventRaceAnimationTemplet = NKCEventRaceAnimationManager.Find(raceEventType).Find((NKCEventRaceAnimationTemplet x) => string.IsNullOrEmpty(x.m_TargetObjName));
			if (nkceventRaceAnimationTemplet != null)
			{
				return nkceventRaceAnimationTemplet.m_MinIndex;
			}
			return 0;
		}

		// Token: 0x060037B8 RID: 14264 RVA: 0x0011F51C File Offset: 0x0011D71C
		public static int GetMaxIndex(RaceEventType raceEventType)
		{
			NKCEventRaceAnimationTemplet nkceventRaceAnimationTemplet = NKCEventRaceAnimationManager.Find(raceEventType).Find((NKCEventRaceAnimationTemplet x) => string.IsNullOrEmpty(x.m_TargetObjName));
			if (nkceventRaceAnimationTemplet != null)
			{
				return nkceventRaceAnimationTemplet.m_MaxIndex;
			}
			return int.MaxValue;
		}

		// Token: 0x060037B9 RID: 14265 RVA: 0x0011F564 File Offset: 0x0011D764
		public static int GetCapacity(RaceEventType raceEventType)
		{
			NKCEventRaceAnimationTemplet nkceventRaceAnimationTemplet = NKCEventRaceAnimationManager.Find(raceEventType).Find((NKCEventRaceAnimationTemplet x) => string.IsNullOrEmpty(x.m_TargetObjName));
			if (nkceventRaceAnimationTemplet != null)
			{
				return nkceventRaceAnimationTemplet.m_SlotCapacity;
			}
			return 0;
		}

		// Token: 0x060037BA RID: 14266 RVA: 0x0011F5A7 File Offset: 0x0011D7A7
		public static float GetTotalTime(RaceEventType raceEventType)
		{
			return NKCEventRaceAnimationManager.GetTotalTime(NKCEventRaceAnimationManager.Find(raceEventType));
		}

		// Token: 0x060037BB RID: 14267 RVA: 0x0011F5B4 File Offset: 0x0011D7B4
		public static float GetTotalTime(List<NKCEventRaceAnimationTemplet> lstTemplet)
		{
			NKCEventRaceAnimationTemplet nkceventRaceAnimationTemplet = lstTemplet.Find((NKCEventRaceAnimationTemplet x) => string.IsNullOrEmpty(x.m_TargetObjName));
			if (nkceventRaceAnimationTemplet == null)
			{
				return 0f;
			}
			List<NKCAnimationEventTemplet> list = NKCAnimationEventManager.Find(nkceventRaceAnimationTemplet.m_AnimationEventSetID);
			if (list == null)
			{
				return 0f;
			}
			List<NKCAnimationEventTemplet> list2 = list.FindAll((NKCAnimationEventTemplet x) => x.m_AniEventType == AnimationEventType.SET_MOVE_SPEED);
			list2.Sort(new Comparison<NKCAnimationEventTemplet>(NKCEventRaceAnimationManager.CompByStartTime));
			float num = 0f;
			float num2 = 0f;
			float num3 = 1f;
			if (list2.Count > 0)
			{
				for (int i = 0; i < list2.Count; i++)
				{
					if (i > 0)
					{
						num2 += (list2[i].m_StartTime - list2[i - 1].m_StartTime) * list2[i - 1].m_FloatValue;
						num = list2[i].m_StartTime;
						num3 = 1f - num2;
					}
					if (i == list2.Count - 1)
					{
						num += num3 / list2[i].m_FloatValue;
					}
				}
			}
			return num;
		}

		// Token: 0x060037BC RID: 14268 RVA: 0x0011F6E1 File Offset: 0x0011D8E1
		private static int CompByStartTime(NKCAnimationEventTemplet left, NKCAnimationEventTemplet right)
		{
			return left.m_StartTime.CompareTo(right.m_StartTime);
		}

		// Token: 0x04003447 RID: 13383
		private static Dictionary<RaceEventType, List<NKCEventRaceAnimationTemplet>> m_dicRaceEventSet = new Dictionary<RaceEventType, List<NKCEventRaceAnimationTemplet>>();
	}
}
