using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Pvp;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000640 RID: 1600
	public class NKCBanManager
	{
		// Token: 0x060031E4 RID: 12772 RVA: 0x000F7E0D File Offset: 0x000F600D
		public static Dictionary<int, NKMBanData> GetBanData(NKCBanManager.BAN_DATA_TYPE banType = NKCBanManager.BAN_DATA_TYPE.FINAL)
		{
			switch (banType)
			{
			case NKCBanManager.BAN_DATA_TYPE.CASTING:
				return NKCBanManager.m_dicNKMCastingBanDataUnit;
			case NKCBanManager.BAN_DATA_TYPE.ROTATION:
				return NKCBanManager.m_dicNKMBanData;
			}
			return NKCBanManager.m_dicNKMFinalBanDataUnit;
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x000F7E34 File Offset: 0x000F6034
		public static Dictionary<int, NKMBanShipData> GetBanDataShip(NKCBanManager.BAN_DATA_TYPE banType = NKCBanManager.BAN_DATA_TYPE.FINAL)
		{
			switch (banType)
			{
			case NKCBanManager.BAN_DATA_TYPE.CASTING:
				return NKCBanManager.m_dicNKMCastingBanDataShip;
			case NKCBanManager.BAN_DATA_TYPE.ROTATION:
				return NKCBanManager.m_dicNKMBanShipData;
			}
			return NKCBanManager.m_dicNKMFinalBanDataShip;
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x000F7E5B File Offset: 0x000F605B
		public static Dictionary<int, NKMBanOperatorData> GetBanDataOperator(NKCBanManager.BAN_DATA_TYPE banType = NKCBanManager.BAN_DATA_TYPE.FINAL)
		{
			switch (banType)
			{
			case NKCBanManager.BAN_DATA_TYPE.CASTING:
				return NKCBanManager.m_dicNKMCastingBanDataOper;
			case NKCBanManager.BAN_DATA_TYPE.ROTATION:
				return NKCBanManager.m_dicNKMBanOperData;
			}
			return NKCBanManager.m_dicNKMFinalBanDataOper;
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x000F7E82 File Offset: 0x000F6082
		public static bool IsBanUnit(int unitID, NKCBanManager.BAN_DATA_TYPE banType = NKCBanManager.BAN_DATA_TYPE.FINAL)
		{
			return (banType == NKCBanManager.BAN_DATA_TYPE.ROTATION && NKCBanManager.m_dicNKMBanData.ContainsKey(unitID)) || (banType == NKCBanManager.BAN_DATA_TYPE.FINAL && NKCBanManager.m_dicNKMFinalBanDataUnit.ContainsKey(unitID)) || (banType == NKCBanManager.BAN_DATA_TYPE.CASTING && NKCBanManager.m_dicNKMCastingBanDataUnit.ContainsKey(unitID));
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x000F7EBD File Offset: 0x000F60BD
		public static bool IsUpUnit(int unitID)
		{
			return NKCBanManager.m_dicNKMUpData.ContainsKey(unitID);
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x000F7ECF File Offset: 0x000F60CF
		public static bool IsBanShip(int shipGroupId, NKCBanManager.BAN_DATA_TYPE banType = NKCBanManager.BAN_DATA_TYPE.FINAL)
		{
			return shipGroupId != 0 && ((banType == NKCBanManager.BAN_DATA_TYPE.ROTATION && NKCBanManager.m_dicNKMBanShipData.ContainsKey(shipGroupId)) || (banType == NKCBanManager.BAN_DATA_TYPE.FINAL && NKCBanManager.m_dicNKMFinalBanDataShip.ContainsKey(shipGroupId)) || (banType == NKCBanManager.BAN_DATA_TYPE.CASTING && NKCBanManager.m_dicNKMCastingBanDataShip.ContainsKey(shipGroupId)));
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x000F7F0F File Offset: 0x000F610F
		public static bool IsBanOperator(int operID, NKCBanManager.BAN_DATA_TYPE banType = NKCBanManager.BAN_DATA_TYPE.FINAL)
		{
			return NKCBanManager.m_dicNKMBanOperData.ContainsKey(operID);
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x000F7F24 File Offset: 0x000F6124
		public static int GetShipBanLevel(int shipGroupId, NKCBanManager.BAN_DATA_TYPE banType = NKCBanManager.BAN_DATA_TYPE.FINAL)
		{
			if (shipGroupId == 0)
			{
				return 0;
			}
			NKMBanShipData nkmbanShipData;
			if (banType == NKCBanManager.BAN_DATA_TYPE.ROTATION && NKCBanManager.m_dicNKMBanShipData.TryGetValue(shipGroupId, out nkmbanShipData))
			{
				return (int)nkmbanShipData.m_BanLevel;
			}
			NKMBanShipData nkmbanShipData2;
			if (banType == NKCBanManager.BAN_DATA_TYPE.FINAL && NKCBanManager.m_dicNKMFinalBanDataShip.TryGetValue(shipGroupId, out nkmbanShipData2))
			{
				return (int)nkmbanShipData2.m_BanLevel;
			}
			NKMBanShipData nkmbanShipData3;
			if (banType == NKCBanManager.BAN_DATA_TYPE.CASTING && NKCBanManager.m_dicNKMCastingBanDataShip.TryGetValue(shipGroupId, out nkmbanShipData3))
			{
				return (int)nkmbanShipData3.m_BanLevel;
			}
			return 0;
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x000F7F84 File Offset: 0x000F6184
		public static int GetUnitBanLevel(int unitID, NKCBanManager.BAN_DATA_TYPE banType = NKCBanManager.BAN_DATA_TYPE.FINAL)
		{
			if (unitID == 0)
			{
				return 0;
			}
			NKMBanData nkmbanData;
			if (banType == NKCBanManager.BAN_DATA_TYPE.ROTATION && NKCBanManager.m_dicNKMBanData.TryGetValue(unitID, out nkmbanData))
			{
				return (int)nkmbanData.m_BanLevel;
			}
			NKMBanData nkmbanData2;
			if (banType == NKCBanManager.BAN_DATA_TYPE.FINAL && NKCBanManager.m_dicNKMFinalBanDataUnit.TryGetValue(unitID, out nkmbanData2))
			{
				return (int)nkmbanData2.m_BanLevel;
			}
			NKMBanData nkmbanData3;
			if (banType == NKCBanManager.BAN_DATA_TYPE.CASTING && NKCBanManager.m_dicNKMCastingBanDataUnit.TryGetValue(unitID, out nkmbanData3))
			{
				return (int)nkmbanData3.m_BanLevel;
			}
			return 0;
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x000F7FE4 File Offset: 0x000F61E4
		public static int GetOperBanLevel(int operID, NKCBanManager.BAN_DATA_TYPE banType = NKCBanManager.BAN_DATA_TYPE.FINAL)
		{
			if (operID == 0)
			{
				return 0;
			}
			NKMBanOperatorData nkmbanOperatorData;
			if (banType == NKCBanManager.BAN_DATA_TYPE.ROTATION && NKCBanManager.m_dicNKMBanOperData.TryGetValue(operID, out nkmbanOperatorData))
			{
				return (int)nkmbanOperatorData.m_BanLevel;
			}
			NKMBanOperatorData nkmbanOperatorData2;
			if (banType == NKCBanManager.BAN_DATA_TYPE.FINAL && NKCBanManager.m_dicNKMFinalBanDataOper.TryGetValue(operID, out nkmbanOperatorData2))
			{
				return (int)nkmbanOperatorData2.m_BanLevel;
			}
			NKMBanOperatorData nkmbanOperatorData3;
			if (banType == NKCBanManager.BAN_DATA_TYPE.CASTING && NKCBanManager.m_dicNKMCastingBanDataOper.TryGetValue(operID, out nkmbanOperatorData3))
			{
				return (int)nkmbanOperatorData3.m_BanLevel;
			}
			return 0;
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x000F8044 File Offset: 0x000F6244
		public static int GetUnitUpLevel(int unitID)
		{
			if (unitID == 0)
			{
				return 0;
			}
			NKMUnitUpData nkmunitUpData;
			if (NKCBanManager.m_dicNKMUpData.TryGetValue(unitID, out nkmunitUpData))
			{
				return (int)nkmunitUpData.upLevel;
			}
			return 0;
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x000F8070 File Offset: 0x000F6270
		public static bool IsBanUnitByUTB(NKMUnitTempletBase cNKMUnitTempletBase)
		{
			bool result;
			if (cNKMUnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
			{
				result = NKCBanManager.IsBanShip(cNKMUnitTempletBase.m_ShipGroupID, NKCBanManager.BAN_DATA_TYPE.FINAL);
			}
			else
			{
				result = NKCBanManager.IsBanUnit(cNKMUnitTempletBase.m_UnitID, NKCBanManager.BAN_DATA_TYPE.FINAL);
			}
			return result;
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x000F80A8 File Offset: 0x000F62A8
		public static bool IsUpUnitByUTB(NKMUnitTempletBase cNKMUnitTempletBase)
		{
			bool result = false;
			if (cNKMUnitTempletBase.m_NKM_UNIT_TYPE != NKM_UNIT_TYPE.NUT_SHIP)
			{
				result = NKCBanManager.IsUpUnit(cNKMUnitTempletBase.m_UnitID);
			}
			return result;
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x000F80D0 File Offset: 0x000F62D0
		public static int GetUnitBanLevelByUTB(NKMUnitTempletBase cNKMUnitTempletBase)
		{
			int result;
			if (cNKMUnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
			{
				result = NKCBanManager.GetShipBanLevel(cNKMUnitTempletBase.m_ShipGroupID, NKCBanManager.BAN_DATA_TYPE.FINAL);
			}
			else
			{
				result = NKCBanManager.GetUnitBanLevel(cNKMUnitTempletBase.m_UnitID, NKCBanManager.BAN_DATA_TYPE.FINAL);
			}
			return result;
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x000F8108 File Offset: 0x000F6308
		public static int GetUnitUpLevelByUTB(NKMUnitTempletBase cNKMUnitTempletBase)
		{
			int result = 0;
			if (cNKMUnitTempletBase.m_NKM_UNIT_TYPE != NKM_UNIT_TYPE.NUT_SHIP)
			{
				result = NKCBanManager.GetUnitUpLevel(cNKMUnitTempletBase.m_UnitID);
			}
			return result;
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x000F8130 File Offset: 0x000F6330
		public static void UpdatePVPBanData(NKMPvpBanResult pvpBanData)
		{
			if (pvpBanData == null)
			{
				return;
			}
			if (pvpBanData.unitBanList != null)
			{
				NKCBanManager.m_dicNKMBanData = pvpBanData.unitBanList;
			}
			if (pvpBanData.shipBanList != null)
			{
				NKCBanManager.m_dicNKMBanShipData = pvpBanData.shipBanList;
			}
			if (pvpBanData.unitUpList != null)
			{
				NKCBanManager.m_dicNKMUpData = pvpBanData.unitUpList;
			}
			if (pvpBanData.unitFinalBanList != null)
			{
				NKCBanManager.m_dicNKMFinalBanDataUnit = pvpBanData.unitFinalBanList;
			}
			if (pvpBanData.shipFinalBanList != null)
			{
				NKCBanManager.m_dicNKMFinalBanDataShip = pvpBanData.shipFinalBanList;
			}
			if (pvpBanData.shipCastingBanList != null)
			{
				NKCBanManager.m_dicNKMCastingBanDataUnit = pvpBanData.unitCastingBanList;
			}
			if (pvpBanData.shipFinalBanList != null)
			{
				NKCBanManager.m_dicNKMCastingBanDataShip = pvpBanData.shipCastingBanList;
			}
			if (pvpBanData.operatorBanList != null)
			{
				NKCBanManager.m_dicNKMBanOperData = pvpBanData.operatorBanList;
			}
			if (pvpBanData.operatorFinalBanList != null)
			{
				NKCBanManager.m_dicNKMFinalBanDataOper = pvpBanData.operatorFinalBanList;
			}
			if (pvpBanData.operatorCastingBanList != null)
			{
				NKCBanManager.m_dicNKMCastingBanDataOper = pvpBanData.operatorCastingBanList;
			}
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x000F81FF File Offset: 0x000F63FF
		public static void UpdatePVPCastingVoteData(PvpCastingVoteData voteData)
		{
			NKCBanManager.m_CastingVoteData = voteData;
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x000F8208 File Offset: 0x000F6408
		public static bool IsCastingBanVoted()
		{
			return NKCBanManager.m_CastingVoteData != null && (NKCBanManager.m_CastingVoteData.unitIdList.Count > 0 || NKCBanManager.m_CastingVoteData.shipIdList.Count > 0 || NKCBanManager.m_CastingVoteData.operatorIdList.Count > 0);
		}

		// Token: 0x040030F7 RID: 12535
		public static Color UP_COLOR = new Color(0f, 1f, 1f);

		// Token: 0x040030F8 RID: 12536
		private static Dictionary<int, NKMBanData> m_dicNKMBanData = new Dictionary<int, NKMBanData>();

		// Token: 0x040030F9 RID: 12537
		private static Dictionary<int, NKMBanShipData> m_dicNKMBanShipData = new Dictionary<int, NKMBanShipData>();

		// Token: 0x040030FA RID: 12538
		private static Dictionary<int, NKMBanOperatorData> m_dicNKMBanOperData = new Dictionary<int, NKMBanOperatorData>();

		// Token: 0x040030FB RID: 12539
		public static Dictionary<int, NKMUnitUpData> m_dicNKMUpData = new Dictionary<int, NKMUnitUpData>();

		// Token: 0x040030FC RID: 12540
		private static Dictionary<int, NKMBanData> m_dicNKMFinalBanDataUnit = new Dictionary<int, NKMBanData>();

		// Token: 0x040030FD RID: 12541
		private static Dictionary<int, NKMBanShipData> m_dicNKMFinalBanDataShip = new Dictionary<int, NKMBanShipData>();

		// Token: 0x040030FE RID: 12542
		private static Dictionary<int, NKMBanOperatorData> m_dicNKMFinalBanDataOper = new Dictionary<int, NKMBanOperatorData>();

		// Token: 0x040030FF RID: 12543
		private static Dictionary<int, NKMBanData> m_dicNKMCastingBanDataUnit = new Dictionary<int, NKMBanData>();

		// Token: 0x04003100 RID: 12544
		private static Dictionary<int, NKMBanShipData> m_dicNKMCastingBanDataShip = new Dictionary<int, NKMBanShipData>();

		// Token: 0x04003101 RID: 12545
		private static Dictionary<int, NKMBanOperatorData> m_dicNKMCastingBanDataOper = new Dictionary<int, NKMBanOperatorData>();

		// Token: 0x04003102 RID: 12546
		public static PvpCastingVoteData m_CastingVoteData;

		// Token: 0x020012ED RID: 4845
		public enum BAN_DATA_TYPE
		{
			// Token: 0x0400978D RID: 38797
			CASTING,
			// Token: 0x0400978E RID: 38798
			FINAL,
			// Token: 0x0400978F RID: 38799
			ROTATION
		}
	}
}
