using System;
using System.Collections.Generic;
using System.Linq;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x02000475 RID: 1141
	public static class NKMSkinManager
	{
		// Token: 0x06001F10 RID: 7952 RVA: 0x0009397F File Offset: 0x00091B7F
		public static void LoadFromLua()
		{
			NKMSkinManager.m_dicSkinTemplet = NKMTempletLoader.LoadDictionary<NKMSkinTemplet>("AB_SCRIPT", "LUA_SKIN_TEMPLET", "SkinTemplet", new Func<NKMLua, NKMSkinTemplet>(NKMSkinTemplet.LoadFromLUA));
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x000939A8 File Offset: 0x00091BA8
		public static NKMSkinTemplet GetSkinTemplet(int skinID)
		{
			if (skinID == 0)
			{
				return null;
			}
			NKMSkinTemplet result;
			if (NKMSkinManager.m_dicSkinTemplet.TryGetValue(skinID, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x000939CC File Offset: 0x00091BCC
		public static NKMSkinTemplet GetSkinTemplet(int skinID, int unitID)
		{
			NKMSkinTemplet nkmskinTemplet;
			if (NKMSkinManager.m_dicSkinTemplet.TryGetValue(skinID, out nkmskinTemplet) && NKMSkinManager.IsSkinForCharacter(unitID, nkmskinTemplet))
			{
				return nkmskinTemplet;
			}
			return null;
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x000939F4 File Offset: 0x00091BF4
		public static NKMSkinTemplet GetSkinTemplet(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				return null;
			}
			NKMSkinTemplet nkmskinTemplet;
			if (unitData.m_SkinID != 0 && NKMSkinManager.m_dicSkinTemplet.TryGetValue(unitData.m_SkinID, out nkmskinTemplet) && NKMSkinManager.IsSkinForCharacter(unitData.m_UnitID, nkmskinTemplet))
			{
				return nkmskinTemplet;
			}
			return null;
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x00093A34 File Offset: 0x00091C34
		public static bool IsSkinForCharacter(int unitID, NKMSkinTemplet templet)
		{
			if (templet == null)
			{
				return false;
			}
			if (templet.m_SkinEquipUnitID == unitID)
			{
				return true;
			}
			NKMUnitTempletBase nkmunitTempletBase = NKMUnitTempletBase.Find(unitID);
			return nkmunitTempletBase != null && nkmunitTempletBase.m_NKM_UNIT_STYLE_TYPE != NKM_UNIT_STYLE_TYPE.NUST_TRAINER && nkmunitTempletBase.IsSameBaseUnit(templet.m_SkinEquipUnitID);
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x00093A74 File Offset: 0x00091C74
		public static bool IsSkinForCharacter(int unitID, int skinID)
		{
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(skinID);
			return NKMSkinManager.IsSkinForCharacter(unitID, skinTemplet);
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x00093A90 File Offset: 0x00091C90
		public static NKM_ERROR_CODE CanEquipSkin(NKMUserData userData, NKMUnitData unitData, int newSkinID)
		{
			if (unitData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_NOT_EXIST;
			}
			if (newSkinID != 0)
			{
				if (unitData.IsSeized)
				{
					return NKM_ERROR_CODE.NEC_FAIL_UNIT_IS_SEIZED;
				}
				if (!userData.m_InventoryData.HasItemSkin(newSkinID))
				{
					return NKM_ERROR_CODE.NEC_FAIL_SKIN_NOT_OWNED;
				}
				if (!NKMSkinManager.IsSkinForCharacter(unitData.m_UnitID, newSkinID))
				{
					return NKM_ERROR_CODE.NEC_FAIL_SKIN_UNIT_NOT_MATCH;
				}
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x00093AE0 File Offset: 0x00091CE0
		public static List<NKMSkinTemplet> GetSkinlistForCharacter(int unitID, NKMInventoryData inventoryData)
		{
			if (NKMSkinManager.m_dicSkinForCharacter == null)
			{
				NKMSkinManager.MakeCharacterSkinList();
			}
			unitID = NKMSkinManager.GetBaseUnitID(unitID);
			List<NKMSkinTemplet> list;
			if (!NKMSkinManager.m_dicSkinForCharacter.TryGetValue(unitID, out list))
			{
				return null;
			}
			if (inventoryData != null)
			{
				return list.FindAll((NKMSkinTemplet x) => x.EnableByTag && (!x.m_bExclude || inventoryData.HasItemSkin(x.m_SkinID)));
			}
			return list.FindAll((NKMSkinTemplet x) => x.EnableByTag);
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x00093B60 File Offset: 0x00091D60
		private static void MakeCharacterSkinList()
		{
			NKMSkinManager.m_dicSkinForCharacter = new Dictionary<int, List<NKMSkinTemplet>>();
			foreach (NKMSkinTemplet nkmskinTemplet in NKMSkinManager.m_dicSkinTemplet.Values)
			{
				if (nkmskinTemplet.EnableByTag)
				{
					if (NKMSkinManager.m_dicSkinForCharacter.ContainsKey(nkmskinTemplet.m_SkinEquipUnitID))
					{
						NKMSkinManager.m_dicSkinForCharacter[nkmskinTemplet.m_SkinEquipUnitID].Add(nkmskinTemplet);
					}
					else
					{
						List<NKMSkinTemplet> list = new List<NKMSkinTemplet>();
						list.Add(nkmskinTemplet);
						NKMSkinManager.m_dicSkinForCharacter[nkmskinTemplet.m_SkinEquipUnitID] = list;
					}
				}
			}
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x00093C0C File Offset: 0x00091E0C
		public static bool IsCharacterHasSkin(int unitID)
		{
			if (NKMSkinManager.m_dicSkinForCharacter == null)
			{
				NKMSkinManager.MakeCharacterSkinList();
			}
			unitID = NKMSkinManager.GetBaseUnitID(unitID);
			return NKMSkinManager.m_dicSkinForCharacter.ContainsKey(unitID);
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x00093C30 File Offset: 0x00091E30
		public static string GetSkillCutin(NKMUnitData unitData, string origName)
		{
			if (unitData == null)
			{
				return origName;
			}
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(unitData);
			if (!string.IsNullOrEmpty((skinTemplet != null) ? skinTemplet.m_HyperSkillCutin : null))
			{
				return skinTemplet.m_HyperSkillCutin;
			}
			return origName;
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x00093C64 File Offset: 0x00091E64
		private static int GetBaseUnitID(int unitID)
		{
			NKMUnitTempletBase nkmunitTempletBase = NKMUnitTempletBase.Find(unitID);
			if (nkmunitTempletBase.m_BaseUnitID != 0 && nkmunitTempletBase.m_NKM_UNIT_STYLE_TYPE != NKM_UNIT_STYLE_TYPE.NUST_TRAINER)
			{
				return nkmunitTempletBase.m_BaseUnitID;
			}
			return unitID;
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x00093C94 File Offset: 0x00091E94
		public static List<NKMSkinTemplet> GetSkinTemplets(string skinStrID)
		{
			return (from templet in NKMSkinManager.m_dicSkinTemplet.Values
			where templet.m_SkinStrID == skinStrID
			select templet).ToList<NKMSkinTemplet>();
		}

		// Token: 0x04001F92 RID: 8082
		public static Dictionary<int, NKMSkinTemplet> m_dicSkinTemplet;

		// Token: 0x04001F93 RID: 8083
		private static Dictionary<int, List<NKMSkinTemplet>> m_dicSkinForCharacter;
	}
}
