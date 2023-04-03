using System;
using System.Collections.Generic;
using System.Linq;
using Cs.Logging;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200038D RID: 909
	public sealed class NKMPotentialOptionTemplet
	{
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x0005E736 File Offset: 0x0005C936
		public string DebugName
		{
			get
			{
				return string.Format("[{0}|{1}]", this.optionKey, this.StatType);
			}
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x0005E758 File Offset: 0x0005C958
		public static NKMPotentialOptionTemplet LoadFromLUA(NKMLua lua)
		{
			NKMPotentialOptionTemplet nkmpotentialOptionTemplet = new NKMPotentialOptionTemplet();
			nkmpotentialOptionTemplet.groupId = lua.GetInt32("m_PotentialOptionGroupID");
			nkmpotentialOptionTemplet.optionKey = lua.GetInt32("OptionKey");
			nkmpotentialOptionTemplet.StatType = lua.GetEnum<NKM_STAT_TYPE>("Socket1_StatType");
			nkmpotentialOptionTemplet.precisionWeightId = lua.GetInt32("PrecisionWeightId");
			for (int i = 0; i < 3; i++)
			{
				int socketNumber = i + 1;
				nkmpotentialOptionTemplet.sockets[i] = NKMPotentialSocketTemplet.Create(nkmpotentialOptionTemplet, socketNumber, lua);
			}
			if (NKMPotentialOptionTemplet.options.ContainsKey(nkmpotentialOptionTemplet.optionKey))
			{
				NKMTempletError.Add(string.Format("[PotentialOption] duplicated optionkey:{0}", nkmpotentialOptionTemplet.optionKey), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMPotentialOptionTemplet.cs", 151);
				return null;
			}
			NKMPotentialOptionTemplet.options.Add(nkmpotentialOptionTemplet.optionKey, nkmpotentialOptionTemplet);
			return nkmpotentialOptionTemplet;
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x0005E819 File Offset: 0x0005CA19
		public static void Drop()
		{
			NKMPotentialOptionTemplet.options.Clear();
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0005E828 File Offset: 0x0005CA28
		public static NKMPotentialOptionTemplet Find(int optionKey)
		{
			NKMPotentialOptionTemplet result;
			NKMPotentialOptionTemplet.options.TryGetValue(optionKey, out result);
			return result;
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x0005E844 File Offset: 0x0005CA44
		public void Join()
		{
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0005E848 File Offset: 0x0005CA48
		public void Validate()
		{
			if (!NKMPotentialOptionGroupTemplet.EnableByTag)
			{
				return;
			}
			if (this.StatType <= NKM_STAT_TYPE.NST_RANDOM || this.StatType >= NKM_STAT_TYPE.NST_END)
			{
				NKMTempletError.Add(string.Format("[Potential Option] Validate StatType:{0}", this.StatType), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMPotentialOptionTemplet.cs", 184);
			}
			NKMPotentialSocketTemplet[] array = this.sockets;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Validate(this.optionKey);
			}
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x0005E8B8 File Offset: 0x0005CAB8
		public static bool CanOpenSocket(NKMEquipItemData equipItemData, int socketIndex)
		{
			if (socketIndex < 0 || socketIndex >= 3)
			{
				return false;
			}
			switch (socketIndex)
			{
			case 0:
				return equipItemData.m_EnchantLevel >= 2;
			case 1:
				return equipItemData.m_EnchantLevel >= 5;
			case 2:
				return equipItemData.m_EnchantLevel >= 7;
			default:
				return false;
			}
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x0005E90C File Offset: 0x0005CB0C
		public NKMPotentialOption GeneratePotentialOption(IReadOnlyList<int> precisions)
		{
			if (precisions.Count != 3)
			{
				Log.Error(string.Format("[PotentialOption] invalid precision count:{0} optionKey:{1}", precisions.Count, this.optionKey), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMPotentialOptionTemplet.cs", 214);
				return null;
			}
			if (precisions[0] < 0)
			{
				Log.Error(string.Format("[PotentialOption] invalid 1st precision value:{0} optionKey:{1}", precisions[0], this.optionKey), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMPotentialOptionTemplet.cs", 220);
				return null;
			}
			NKMPotentialOption nkmpotentialOption = new NKMPotentialOption
			{
				optionKey = this.optionKey,
				statType = this.StatType
			};
			foreach (int num in Enumerable.Range(0, this.sockets.Length))
			{
				int num2 = precisions[num];
				if (num2 < 0)
				{
					break;
				}
				nkmpotentialOption.sockets[num] = new NKMPotentialOption.SocketData
				{
					precision = num2,
					statValue = this.sockets[num].CalcStatValue(num2),
					statFactor = this.sockets[num].CalcStatFactor(num2)
				};
			}
			return nkmpotentialOption;
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x0005EA3C File Offset: 0x0005CC3C
		public NKMPotentialOption.SocketData GenerateSocket(int index, int precision)
		{
			return new NKMPotentialOption.SocketData
			{
				precision = precision,
				statValue = this.sockets[index].CalcStatValue(precision),
				statFactor = this.sockets[index].CalcStatFactor(precision)
			};
		}

		// Token: 0x04000FC3 RID: 4035
		private static readonly Dictionary<int, NKMPotentialOptionTemplet> options = new Dictionary<int, NKMPotentialOptionTemplet>();

		// Token: 0x04000FC4 RID: 4036
		public int groupId;

		// Token: 0x04000FC5 RID: 4037
		public int optionKey;

		// Token: 0x04000FC6 RID: 4038
		public NKM_STAT_TYPE StatType = NKM_STAT_TYPE.NST_RANDOM;

		// Token: 0x04000FC7 RID: 4039
		public int precisionWeightId;

		// Token: 0x04000FC8 RID: 4040
		public NKMPotentialSocketTemplet[] sockets = new NKMPotentialSocketTemplet[3];
	}
}
