using System;
using System.Collections.Generic;
using System.Linq;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x02000470 RID: 1136
	public class NKMShipSkillManager
	{
		// Token: 0x06001EEF RID: 7919 RVA: 0x0009326C File Offset: 0x0009146C
		public static bool LoadFromLUA(string fileName)
		{
			bool flag = true;
			Dictionary<int, NKMShipSkillTemplet> dicNKMShipSkillTempletByID = NKMShipSkillManager.m_dicNKMShipSkillTempletByID;
			if (dicNKMShipSkillTempletByID != null)
			{
				dicNKMShipSkillTempletByID.Clear();
			}
			Dictionary<string, NKMShipSkillTemplet> dicNKMShipSkillTempletByStrID = NKMShipSkillManager.m_dicNKMShipSkillTempletByStrID;
			if (dicNKMShipSkillTempletByStrID != null)
			{
				dicNKMShipSkillTempletByStrID.Clear();
			}
			NKMShipSkillManager.m_dicNKMShipSkillTempletByID = NKMTempletLoader.LoadDictionary<NKMShipSkillTemplet>("AB_SCRIPT", fileName, "m_dicNKMShipSkillTempletByID", new Func<NKMLua, NKMShipSkillTemplet>(NKMShipSkillTemplet.LoadFromLUA));
			bool result = flag & NKMShipSkillManager.m_dicNKMShipSkillTempletByID != null;
			if (NKMShipSkillManager.m_dicNKMShipSkillTempletByID != null)
			{
				NKMShipSkillManager.m_dicNKMShipSkillTempletByStrID = NKMShipSkillManager.m_dicNKMShipSkillTempletByID.ToDictionary((KeyValuePair<int, NKMShipSkillTemplet> e) => e.Value.m_ShipSkillStrID, (KeyValuePair<int, NKMShipSkillTemplet> e) => e.Value);
			}
			return result;
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x00093318 File Offset: 0x00091518
		public static int GetSkillID(string strID)
		{
			NKMShipSkillTemplet shipSkillTempletByStrID = NKMShipSkillManager.GetShipSkillTempletByStrID(strID);
			if (shipSkillTempletByStrID != null)
			{
				return shipSkillTempletByStrID.m_ShipSkillID;
			}
			return -1;
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x00093338 File Offset: 0x00091538
		public static string GetSkillStrID(int id)
		{
			NKMShipSkillTemplet shipSkillTempletByID = NKMShipSkillManager.GetShipSkillTempletByID(id);
			if (shipSkillTempletByID != null)
			{
				return shipSkillTempletByID.m_ShipSkillStrID;
			}
			return string.Empty;
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x0009335B File Offset: 0x0009155B
		public static NKMShipSkillTemplet GetShipSkillTempletByID(int shipSkillID)
		{
			if (NKMShipSkillManager.m_dicNKMShipSkillTempletByID.ContainsKey(shipSkillID))
			{
				return NKMShipSkillManager.m_dicNKMShipSkillTempletByID[shipSkillID];
			}
			return null;
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x00093377 File Offset: 0x00091577
		public static NKMShipSkillTemplet GetShipSkillTempletByStrID(string shipSkillStrID)
		{
			if (NKMShipSkillManager.m_dicNKMShipSkillTempletByStrID.ContainsKey(shipSkillStrID))
			{
				return NKMShipSkillManager.m_dicNKMShipSkillTempletByStrID[shipSkillStrID];
			}
			return null;
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x00093393 File Offset: 0x00091593
		public static NKMShipSkillTemplet GetShipSkillTempletByIndex(NKMUnitTempletBase shipTemplet, int index)
		{
			return NKMShipSkillManager.GetShipSkillTempletByStrID(shipTemplet.GetSkillStrID(index));
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x000933A4 File Offset: 0x000915A4
		public static NKMShipSkillTemplet GetShipSkillFromUnitState(NKMUnitTempletBase unitTemplet, string stateName)
		{
			if (unitTemplet == null)
			{
				return null;
			}
			for (int i = 0; i < 5; i++)
			{
				NKMShipSkillTemplet shipSkillTempletByIndex = NKMShipSkillManager.GetShipSkillTempletByIndex(unitTemplet, i);
				if (shipSkillTempletByIndex != null && string.Equals(shipSkillTempletByIndex.m_UnitStateName, stateName))
				{
					return shipSkillTempletByIndex;
				}
			}
			return null;
		}

		// Token: 0x04001F5F RID: 8031
		public static Dictionary<int, NKMShipSkillTemplet> m_dicNKMShipSkillTempletByID;

		// Token: 0x04001F60 RID: 8032
		public static Dictionary<string, NKMShipSkillTemplet> m_dicNKMShipSkillTempletByStrID;
	}
}
