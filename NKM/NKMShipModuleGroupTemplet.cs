using System;
using System.Collections.Generic;
using System.Linq;
using Cs.Math.Lottery;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200046B RID: 1131
	public class NKMShipModuleGroupTemplet
	{
		// Token: 0x06001EBF RID: 7871 RVA: 0x00091FD4 File Offset: 0x000901D4
		public static void LoadFromLua()
		{
			NKMShipModuleGroupTemplet.commandModulePassiveTemplets = (from e in NKMTempletLoader<NKMCommandModulePassiveTemplet>.LoadGroup("AB_SCRIPT", "LUA_COMMANDMODULE_PASSIVE_TEMPLET", "COMMANDMODULE_PASSIVE_TEMPLET", new Func<NKMLua, NKMCommandModulePassiveTemplet>(NKMCommandModulePassiveTemplet.LoadFromLUA)).SelectMany((KeyValuePair<int, List<NKMCommandModulePassiveTemplet>> e) => e.Value).ToList<NKMCommandModulePassiveTemplet>()
			group e by e.PassiveGroupId).ToDictionary((IGrouping<int, NKMCommandModulePassiveTemplet> e) => e.Key, (IGrouping<int, NKMCommandModulePassiveTemplet> e) => e.ToList<NKMCommandModulePassiveTemplet>());
			NKMShipModuleGroupTemplet.commandModuleStatTemplets = (from e in NKMTempletLoader<NKMCommandModuleRandomStatTemplet>.LoadGroup("AB_SCRIPT", "LUA_COMMANDMODULE_RANDOM_STAT", "COMMANDMODULE_RANDOM_STAT", new Func<NKMLua, NKMCommandModuleRandomStatTemplet>(NKMCommandModuleRandomStatTemplet.LoadFromLUA)).SelectMany((KeyValuePair<int, List<NKMCommandModuleRandomStatTemplet>> e) => e.Value).ToList<NKMCommandModuleRandomStatTemplet>()
			group e by e.Key).ToDictionary((IGrouping<int, NKMCommandModuleRandomStatTemplet> e) => e.Key, (IGrouping<int, NKMCommandModuleRandomStatTemplet> e) => e.ToList<NKMCommandModuleRandomStatTemplet>());
			if (NKMShipModuleGroupTemplet.commandModulePassiveTemplets == null || NKMShipModuleGroupTemplet.commandModuleStatTemplets == null)
			{
				NKMTempletError.Add("[NKMShipModuleGroupTemplet] �Լ� �̽� ��� ���� load ����", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 896);
			}
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x00092170 File Offset: 0x00090370
		public static void Join()
		{
			foreach (NKMCommandModuleRandomStatTemplet nkmcommandModuleRandomStatTemplet in NKMShipModuleGroupTemplet.commandModuleStatTemplets.Values.SelectMany((List<NKMCommandModuleRandomStatTemplet> e) => e).Distinct<NKMCommandModuleRandomStatTemplet>())
			{
				nkmcommandModuleRandomStatTemplet.Join();
			}
			foreach (NKMCommandModulePassiveTemplet nkmcommandModulePassiveTemplet in NKMShipModuleGroupTemplet.commandModulePassiveTemplets.Values.SelectMany((List<NKMCommandModulePassiveTemplet> e) => e).Distinct<NKMCommandModulePassiveTemplet>())
			{
				RatioLottery<NKMCommandModulePassiveTemplet> ratioLottery;
				if (!NKMShipModuleGroupTemplet.commandModuleRatios.TryGetValue(nkmcommandModulePassiveTemplet.PassiveGroupId, out ratioLottery))
				{
					RatioLottery<NKMCommandModulePassiveTemplet> ratioLottery2 = new RatioLottery<NKMCommandModulePassiveTemplet>();
					ratioLottery2.AddCase(nkmcommandModulePassiveTemplet.Ratio, nkmcommandModulePassiveTemplet);
					NKMShipModuleGroupTemplet.commandModuleRatios.Add(nkmcommandModulePassiveTemplet.PassiveGroupId, ratioLottery2);
				}
				else
				{
					ratioLottery.AddCase(nkmcommandModulePassiveTemplet.Ratio, nkmcommandModulePassiveTemplet);
				}
			}
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x00092294 File Offset: 0x00090494
		public static void Validate()
		{
			foreach (NKMCommandModulePassiveTemplet nkmcommandModulePassiveTemplet in NKMShipModuleGroupTemplet.commandModulePassiveTemplets.Values.SelectMany((List<NKMCommandModulePassiveTemplet> e) => e).Distinct<NKMCommandModulePassiveTemplet>())
			{
				nkmcommandModulePassiveTemplet.Validate();
			}
			foreach (NKMCommandModuleRandomStatTemplet nkmcommandModuleRandomStatTemplet in NKMShipModuleGroupTemplet.commandModuleStatTemplets.Values.SelectMany((List<NKMCommandModuleRandomStatTemplet> e) => e).Distinct<NKMCommandModuleRandomStatTemplet>())
			{
				nkmcommandModuleRandomStatTemplet.Validate();
			}
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x00092374 File Offset: 0x00090574
		public static IReadOnlyList<NKMCommandModulePassiveTemplet> GetPassiveListsByGroupId(int passiveGroupId)
		{
			List<NKMCommandModulePassiveTemplet> result;
			if (!NKMShipModuleGroupTemplet.commandModulePassiveTemplets.TryGetValue(passiveGroupId, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x00092394 File Offset: 0x00090594
		public static IReadOnlyList<NKMCommandModuleRandomStatTemplet> GetStatListsByGroupId(int statGroupId)
		{
			List<NKMCommandModuleRandomStatTemplet> result;
			if (!NKMShipModuleGroupTemplet.commandModuleStatTemplets.TryGetValue(statGroupId, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x000923B4 File Offset: 0x000905B4
		public static NKMCommandModulePassiveTemplet Decide(int passiveGroupId)
		{
			RatioLottery<NKMCommandModulePassiveTemplet> ratioLottery;
			if (!NKMShipModuleGroupTemplet.commandModuleRatios.TryGetValue(passiveGroupId, out ratioLottery))
			{
				return null;
			}
			return ratioLottery.Decide();
		}

		// Token: 0x04001F3F RID: 7999
		private static Dictionary<int, List<NKMCommandModulePassiveTemplet>> commandModulePassiveTemplets = new Dictionary<int, List<NKMCommandModulePassiveTemplet>>();

		// Token: 0x04001F40 RID: 8000
		private static Dictionary<int, List<NKMCommandModuleRandomStatTemplet>> commandModuleStatTemplets = new Dictionary<int, List<NKMCommandModuleRandomStatTemplet>>();

		// Token: 0x04001F41 RID: 8001
		private static Dictionary<int, RatioLottery<NKMCommandModulePassiveTemplet>> commandModuleRatios = new Dictionary<int, RatioLottery<NKMCommandModulePassiveTemplet>>();
	}
}
