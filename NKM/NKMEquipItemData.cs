using System;
using System.Collections.Generic;
using Cs.Logging;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x020003F2 RID: 1010
	public class NKMEquipItemData : ISerializable
	{
		// Token: 0x06001ADD RID: 6877 RVA: 0x00075F0D File Offset: 0x0007410D
		public NKMEquipItemData()
		{
			this.m_Stat.Add(new EQUIP_ITEM_STAT());
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x00075F38 File Offset: 0x00074138
		public NKMEquipItemData(long itemUid, NKMEquipTemplet templet)
		{
			this.m_ItemUid = itemUid;
			this.m_ItemEquipID = templet.m_ItemEquipID;
			this.m_Stat.Add(new EQUIP_ITEM_STAT
			{
				type = templet.m_StatType,
				stat_value = templet.m_StatData.m_fBaseValue,
				stat_level_value = templet.m_StatData.m_fLevelUpValue,
				stat_factor = 0f
			});
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x00075FBC File Offset: 0x000741BC
		public void AddSubStat(EQUIP_ITEM_STAT subStat)
		{
			if (this.m_Stat.Count >= 3)
			{
				Log.Error(string.Format("[Equip] add substat failed. m_Stat.Count:{0}", this.m_Stat.Count), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMEquipItemData.cs", 54);
				return;
			}
			this.m_Stat.Add(subStat);
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x0007600A File Offset: 0x0007420A
		public EQUIP_ITEM_STAT GetStat(int index)
		{
			if (index < 0 || index >= this.m_Stat.Count)
			{
				return null;
			}
			return this.m_Stat[index];
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x0007602C File Offset: 0x0007422C
		public EQUIP_ITEM_STAT GetSubStatOrDefault(int index)
		{
			EQUIP_ITEM_STAT result = new EQUIP_ITEM_STAT();
			if (index <= 0 || index >= this.m_Stat.Count)
			{
				return result;
			}
			return this.m_Stat[index];
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x00076060 File Offset: 0x00074260
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_ItemUid);
			stream.PutOrGet(ref this.m_ItemEquipID);
			stream.PutOrGet(ref this.m_EnchantLevel);
			stream.PutOrGet(ref this.m_EnchantExp);
			stream.PutOrGet<EQUIP_ITEM_STAT>(ref this.m_Stat);
			stream.PutOrGet(ref this.m_OwnerUnitUID);
			stream.PutOrGet(ref this.m_bLock);
			stream.PutOrGet(ref this.m_Precision);
			stream.PutOrGet(ref this.m_Precision2);
			stream.PutOrGet(ref this.m_SetOptionId);
			stream.PutOrGet(ref this.m_ImprintUnitId);
			stream.PutOrGet<NKMPotentialOption>(ref this.potentialOption);
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x00076100 File Offset: 0x00074300
		public void UpgradeEquip(NKMEquipTemplet templet, int upgradeEquipLevel, int upgradeEquipExp)
		{
			this.m_ItemEquipID = templet.m_ItemEquipID;
			this.m_Stat[0].stat_value = templet.m_StatData.m_fBaseValue;
			this.m_Stat[0].stat_level_value = templet.m_StatData.m_fLevelUpValue;
			this.m_Stat[0].stat_factor = 0f;
			this.m_Stat[1] = templet.StatGroups[0].GenerateSubStat(this.m_Stat[1].type, this.m_Precision);
			this.m_Stat[2] = templet.StatGroups[1].GenerateSubStat(this.m_Stat[2].type, this.m_Precision2);
			this.m_EnchantLevel = upgradeEquipLevel;
			this.m_EnchantExp = upgradeEquipExp;
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x000761E0 File Offset: 0x000743E0
		public float CalculatePotentialPercent()
		{
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(this.m_ItemEquipID);
			if (equipTemplet == null)
			{
				return 0f;
			}
			if (!equipTemplet.m_bRelic)
			{
				return 0f;
			}
			if (this.potentialOption == null)
			{
				return 0f;
			}
			NKMPotentialOptionTemplet nkmpotentialOptionTemplet = NKMPotentialOptionTemplet.Find(this.potentialOption.optionKey);
			if (nkmpotentialOptionTemplet == null)
			{
				return 0f;
			}
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			int num5 = this.potentialOption.sockets.Length;
			for (int i = 0; i < num5; i++)
			{
				if (this.potentialOption.sockets[i] != null)
				{
					NKMPotentialSocketTemplet nkmpotentialSocketTemplet = nkmpotentialOptionTemplet.sockets[i];
					if (nkmpotentialSocketTemplet != null)
					{
						num += this.potentialOption.sockets[i].statValue;
						num2 += this.potentialOption.sockets[i].statFactor;
						num3 += nkmpotentialSocketTemplet.MaxStat;
						num4 += nkmpotentialSocketTemplet.MinStat;
					}
				}
			}
			if (num2 > 0f)
			{
				return (num2 - num4) / (num3 - num4);
			}
			if (num > 0f)
			{
				return (num - num4) / (num3 - num4);
			}
			return 0f;
		}

		// Token: 0x040013D1 RID: 5073
		public long m_ItemUid;

		// Token: 0x040013D2 RID: 5074
		public int m_ItemEquipID;

		// Token: 0x040013D3 RID: 5075
		public int m_EnchantLevel;

		// Token: 0x040013D4 RID: 5076
		public int m_EnchantExp;

		// Token: 0x040013D5 RID: 5077
		public List<EQUIP_ITEM_STAT> m_Stat = new List<EQUIP_ITEM_STAT>();

		// Token: 0x040013D6 RID: 5078
		public long m_OwnerUnitUID = -1L;

		// Token: 0x040013D7 RID: 5079
		public bool m_bLock;

		// Token: 0x040013D8 RID: 5080
		public int m_Precision;

		// Token: 0x040013D9 RID: 5081
		public int m_Precision2;

		// Token: 0x040013DA RID: 5082
		public int m_SetOptionId;

		// Token: 0x040013DB RID: 5083
		public int m_ImprintUnitId;

		// Token: 0x040013DC RID: 5084
		public NKMPotentialOption potentialOption;

		// Token: 0x020011E2 RID: 4578
		public enum NKM_EQUIP_STAT_TYPE
		{
			// Token: 0x040093BA RID: 37818
			NESI_DEFAULT,
			// Token: 0x040093BB RID: 37819
			NESI_RANDOM,
			// Token: 0x040093BC RID: 37820
			NESI_RANDOM2,
			// Token: 0x040093BD RID: 37821
			NESI_MAX
		}
	}
}
