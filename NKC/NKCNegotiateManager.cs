using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Common;
using ClientPacket.Negotiation;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006AB RID: 1707
	public static class NKCNegotiateManager
	{
		// Token: 0x06003882 RID: 14466 RVA: 0x001244EC File Offset: 0x001226EC
		public static void LoadFromLua()
		{
			if (NKCNegotiateManager.m_dicNegoSpeech == null)
			{
				NKMLua nkmlua = new NKMLua();
				NKCNegotiateManager.m_dicNegoSpeech = new Dictionary<int, Dictionary<int, NKCNegotiateManager.NKMNegotiateSpeechTemplet>>();
				if (nkmlua.LoadCommonPath("AB_SCRIPT", "LUA_NEGOTIATE_SPEECH", true) && nkmlua.OpenTable("m_NegoSpeech"))
				{
					int num = 1;
					while (nkmlua.OpenTable(num))
					{
						NKCNegotiateManager.NKMNegotiateSpeechTemplet nkmnegotiateSpeechTemplet = NKCNegotiateManager.NKMNegotiateSpeechTemplet.LoadFromLUA(nkmlua);
						if (nkmnegotiateSpeechTemplet != null)
						{
							if (!NKCNegotiateManager.m_dicNegoSpeech.ContainsKey(nkmnegotiateSpeechTemplet.m_UnitID))
							{
								NKCNegotiateManager.m_dicNegoSpeech.Add(nkmnegotiateSpeechTemplet.m_UnitID, new Dictionary<int, NKCNegotiateManager.NKMNegotiateSpeechTemplet>());
							}
							if (!NKCNegotiateManager.m_dicNegoSpeech[nkmnegotiateSpeechTemplet.m_UnitID].ContainsKey(nkmnegotiateSpeechTemplet.m_SkinID))
							{
								NKCNegotiateManager.m_dicNegoSpeech[nkmnegotiateSpeechTemplet.m_UnitID].Add(nkmnegotiateSpeechTemplet.m_SkinID, nkmnegotiateSpeechTemplet);
							}
							else
							{
								Debug.LogError(string.Format("m_dicNegoSpeech Duplicate unitID:{0}, skinID:{1}", nkmnegotiateSpeechTemplet.m_UnitID, nkmnegotiateSpeechTemplet.m_SkinID));
							}
						}
						num++;
						nkmlua.CloseTable();
					}
					nkmlua.CloseTable();
				}
				nkmlua.LuaClose();
			}
		}

		// Token: 0x06003883 RID: 14467 RVA: 0x001245F8 File Offset: 0x001227F8
		public static NKCNegotiateManager.NKMNegotiateSpeechTemplet GetSpeechTemplet(int unitID, int skinID)
		{
			if (NKCNegotiateManager.m_dicNegoSpeech == null)
			{
				NKCNegotiateManager.LoadFromLua();
			}
			if (NKCNegotiateManager.m_dicNegoSpeech.ContainsKey(unitID))
			{
				NKCNegotiateManager.NKMNegotiateSpeechTemplet result;
				if (NKCNegotiateManager.m_dicNegoSpeech[unitID].TryGetValue(skinID, out result))
				{
					return result;
				}
				if (NKCNegotiateManager.m_dicNegoSpeech[unitID].TryGetValue(0, out result))
				{
					return result;
				}
			}
			else
			{
				NKMUnitTempletBase nkmunitTempletBase = NKMUnitTempletBase.Find(unitID);
				if (nkmunitTempletBase.m_BaseUnitID != 0 && nkmunitTempletBase.m_BaseUnitID != unitID)
				{
					NKCNegotiateManager.NKMNegotiateSpeechTemplet result;
					if (NKCNegotiateManager.m_dicNegoSpeech[nkmunitTempletBase.m_BaseUnitID].TryGetValue(skinID, out result))
					{
						return result;
					}
					if (NKCNegotiateManager.m_dicNegoSpeech[nkmunitTempletBase.m_BaseUnitID].TryGetValue(0, out result))
					{
						return result;
					}
				}
			}
			return null;
		}

		// Token: 0x06003884 RID: 14468 RVA: 0x001246A0 File Offset: 0x001228A0
		public static NKM_ERROR_CODE CanTargetNegotiate(NKMUserData userData, NKMUnitData targetUnit)
		{
			if (targetUnit == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_NOT_EXIST;
			}
			if (NKCExpManager.GetUnitExpTemplet(targetUnit) == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_GET_UNIT_EXP_TEMPLET_NULL;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(targetUnit.m_UnitID);
			if (unitTempletBase == null || unitTempletBase.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_TRAINER)
			{
				return NKM_ERROR_CODE.NEC_FAIL_GET_UNIT_BASE_TEMPLET_NULL;
			}
			if (targetUnit.loyalty == 10000 && targetUnit.m_UnitLevel == NKCExpManager.GetUnitMaxLevel(targetUnit))
			{
				return NKM_ERROR_CODE.NEC_FAIL_NEGOTIATION_EXP_LOYALTY_FULL;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06003885 RID: 14469 RVA: 0x00124704 File Offset: 0x00122904
		public static NKM_ERROR_CODE CanStartNegotiate(NKMUserData userData, NKMUnitData targetUnit, NEGOTIATE_BOSS_SELECTION bossSelection, List<MiscItemData> lstMaterials)
		{
			if (targetUnit == null || userData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_NOT_EXIST;
			}
			if (NKCExpManager.GetUnitExpTemplet(targetUnit) == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_GET_UNIT_EXP_TEMPLET_NULL;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(targetUnit.m_UnitID);
			if (unitTempletBase == null || unitTempletBase.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_TRAINER)
			{
				return NKM_ERROR_CODE.NEC_FAIL_GET_UNIT_BASE_TEMPLET_NULL;
			}
			if (lstMaterials == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_ITEM;
			}
			foreach (MiscItemData miscItemData in lstMaterials)
			{
				if (userData.m_InventoryData.GetCountMiscItem(miscItemData.itemId) < (long)miscItemData.count)
				{
					return NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_ITEM;
				}
			}
			if (lstMaterials.Sum((MiscItemData e) => e.count) > NKMCommonConst.Negotiation.MaxMaterialUsageLimit)
			{
				return NKM_ERROR_CODE.NEC_FAIL_NEGOTIATION_INVALID_MATERIAL_COUNT;
			}
			long negotiateSalary = NKCNegotiateManager.GetNegotiateSalary(lstMaterials, bossSelection);
			if (userData.m_InventoryData.GetCountMiscItem(1) < negotiateSalary)
			{
				return NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_CREDIT;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06003886 RID: 14470 RVA: 0x00124800 File Offset: 0x00122A00
		public static long GetNegotiateSalary(List<MiscItemData> lstMaterials, NEGOTIATE_BOSS_SELECTION selection = NEGOTIATE_BOSS_SELECTION.OK)
		{
			long num = 0L;
			for (int i = 0; i < lstMaterials.Count; i++)
			{
				if (lstMaterials[i] != null)
				{
					NegotiationMaterial negotiationMaterial;
					NKMCommonConst.Negotiation.TryGetMaterial(lstMaterials[i].itemId, out negotiationMaterial);
					num += (long)(lstMaterials[i].count * negotiationMaterial.Credit);
				}
			}
			NKCCompanyBuff.SetDiscountOfCreditInNegotiation(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref num);
			switch (selection)
			{
			case NEGOTIATE_BOSS_SELECTION.RAISE:
				num += num * (long)NKMCommonConst.Negotiation.Bonus_CreditIncreasePercent / 100L;
				break;
			case NEGOTIATE_BOSS_SELECTION.PASSION:
				num -= num * (long)NKMCommonConst.Negotiation.Passion_CreditDecreasePercent / 100L;
				break;
			}
			return num;
		}

		// Token: 0x06003887 RID: 14471 RVA: 0x001248AC File Offset: 0x00122AAC
		public static int GetNegotiateExp(List<MiscItemData> lstMaterials, bool bPermanentContract)
		{
			int num = 0;
			for (int i = 0; i < lstMaterials.Count; i++)
			{
				if (lstMaterials[i] != null)
				{
					NegotiationMaterial negotiationMaterial;
					NKMCommonConst.Negotiation.TryGetMaterial(lstMaterials[i].itemId, out negotiationMaterial);
					num += lstMaterials[i].count * negotiationMaterial.Exp;
				}
			}
			if (bPermanentContract)
			{
				num = num * 120 / 100;
			}
			return num;
		}

		// Token: 0x06003888 RID: 14472 RVA: 0x00124910 File Offset: 0x00122B10
		public static int GetNegotiateLoyalty(List<MiscItemData> lstMaterials, NEGOTIATE_BOSS_SELECTION selection = NEGOTIATE_BOSS_SELECTION.OK)
		{
			int num = 0;
			for (int i = 0; i < lstMaterials.Count; i++)
			{
				if (lstMaterials[i] != null)
				{
					NegotiationMaterial negotiationMaterial;
					NKMCommonConst.Negotiation.TryGetMaterial(lstMaterials[i].itemId, out negotiationMaterial);
					num += lstMaterials[i].count * negotiationMaterial.Loyalty;
				}
			}
			switch (selection)
			{
			case NEGOTIATE_BOSS_SELECTION.RAISE:
				num = num * 110 / 100;
				break;
			case NEGOTIATE_BOSS_SELECTION.PASSION:
				return 0;
			}
			return num;
		}

		// Token: 0x06003889 RID: 14473 RVA: 0x00124989 File Offset: 0x00122B89
		public static NKCNegotiateManager.SpeechType GetSpeechType(NEGOTIATE_RESULT resultType)
		{
			if (resultType != NEGOTIATE_RESULT.SUCCESS)
			{
				if (resultType != NEGOTIATE_RESULT.COMPLETE)
				{
				}
				return NKCNegotiateManager.SpeechType.Success;
			}
			return NKCNegotiateManager.SpeechType.GreatSuccess;
		}

		// Token: 0x0600388A RID: 14474 RVA: 0x00124998 File Offset: 0x00122B98
		public static string GetSpeech(NKMUnitData unitData, NKCNegotiateManager.SpeechType type)
		{
			if (unitData == null)
			{
				return "";
			}
			NKCNegotiateManager.NKMNegotiateSpeechTemplet speechTemplet = NKCNegotiateManager.GetSpeechTemplet(unitData.m_UnitID, unitData.m_SkinID);
			if (speechTemplet == null)
			{
				return "";
			}
			string text = string.Empty;
			switch (type)
			{
			case NKCNegotiateManager.SpeechType.Ready:
				text = speechTemplet.m_NegoStanby;
				break;
			case NKCNegotiateManager.SpeechType.GreatSuccess:
				text = speechTemplet.m_NegoGreatSuccess;
				break;
			case NKCNegotiateManager.SpeechType.Success:
				text = speechTemplet.m_NegoSuccess;
				break;
			}
			if (!string.IsNullOrEmpty(text))
			{
				return NKCStringTable.GetString(text, false);
			}
			return "";
		}

		// Token: 0x0600388B RID: 14475 RVA: 0x00124A14 File Offset: 0x00122C14
		public static NKCNegotiateManager.NegotiateResultUIData MakeResultUIData(NKMUserData userDataBefore, NKMPacket_NEGOTIATE_ACK sPacket)
		{
			NKCNegotiateManager.NegotiateResultUIData negotiateResultUIData = new NKCNegotiateManager.NegotiateResultUIData();
			NKMUnitData unitFromUID = userDataBefore.m_ArmyData.GetUnitFromUID(sPacket.targetUnitUid);
			negotiateResultUIData.NegotiateResult = sPacket.negotiateResult;
			negotiateResultUIData.TargetUnitUID = sPacket.targetUnitUid;
			negotiateResultUIData.UnitID = unitFromUID.m_UnitID;
			negotiateResultUIData.CreditUsed = sPacket.finalSalary;
			negotiateResultUIData.UnitLevelBefore = unitFromUID.m_UnitLevel;
			negotiateResultUIData.UnitLevelAfter = sPacket.targetUnitLevel;
			negotiateResultUIData.UnitExpBefore = unitFromUID.m_iUnitLevelEXP;
			negotiateResultUIData.UnitExpAfter = sPacket.targetUnitExp;
			negotiateResultUIData.LoyaltyBefore = unitFromUID.loyalty;
			negotiateResultUIData.LoyaltyAfter = sPacket.targetUnitLoyalty;
			return negotiateResultUIData;
		}

		// Token: 0x040034C7 RID: 13511
		public const int NEGOTIATE_ITEM_REQUIRE_COUNT = 1;

		// Token: 0x040034C8 RID: 13512
		private static Dictionary<int, Dictionary<int, NKCNegotiateManager.NKMNegotiateSpeechTemplet>> m_dicNegoSpeech;

		// Token: 0x02001376 RID: 4982
		public enum SpeechType
		{
			// Token: 0x040099EB RID: 39403
			Ready,
			// Token: 0x040099EC RID: 39404
			GreatSuccess,
			// Token: 0x040099ED RID: 39405
			Success
		}

		// Token: 0x02001377 RID: 4983
		[Serializable]
		public class NKMNegotiateSpeechTemplet
		{
			// Token: 0x0600A5F5 RID: 42485 RVA: 0x0034625C File Offset: 0x0034445C
			public static NKCNegotiateManager.NKMNegotiateSpeechTemplet LoadFromLUA(NKMLua cNKMLua)
			{
				NKCNegotiateManager.NKMNegotiateSpeechTemplet nkmnegotiateSpeechTemplet = new NKCNegotiateManager.NKMNegotiateSpeechTemplet();
				bool flag = true & cNKMLua.GetData("m_UnitID", ref nkmnegotiateSpeechTemplet.m_UnitID);
				cNKMLua.GetData("m_SkinID", ref nkmnegotiateSpeechTemplet.m_SkinID);
				if (!(flag & cNKMLua.GetData("m_NegoStanby", ref nkmnegotiateSpeechTemplet.m_NegoStanby) & cNKMLua.GetData("m_NegoGreatSuccess", ref nkmnegotiateSpeechTemplet.m_NegoGreatSuccess) & cNKMLua.GetData("m_NegoSuccess", ref nkmnegotiateSpeechTemplet.m_NegoSuccess)))
				{
					Debug.LogError("NKMNegotiateSpeechTemplet LoadFromLUA fail");
					return null;
				}
				return nkmnegotiateSpeechTemplet;
			}

			// Token: 0x0600A5F6 RID: 42486 RVA: 0x003462D9 File Offset: 0x003444D9
			public void Join()
			{
			}

			// Token: 0x0600A5F7 RID: 42487 RVA: 0x003462DB File Offset: 0x003444DB
			public void Validate()
			{
			}

			// Token: 0x040099EE RID: 39406
			public int m_UnitID;

			// Token: 0x040099EF RID: 39407
			public int m_SkinID;

			// Token: 0x040099F0 RID: 39408
			public string m_NegoStanby;

			// Token: 0x040099F1 RID: 39409
			public string m_NegoGreatSuccess;

			// Token: 0x040099F2 RID: 39410
			public string m_NegoSuccess;
		}

		// Token: 0x02001378 RID: 4984
		public class NegotiateResultUIData
		{
			// Token: 0x040099F3 RID: 39411
			public NEGOTIATE_RESULT NegotiateResult;

			// Token: 0x040099F4 RID: 39412
			public long TargetUnitUID;

			// Token: 0x040099F5 RID: 39413
			public int UnitID;

			// Token: 0x040099F6 RID: 39414
			public int CreditUsed;

			// Token: 0x040099F7 RID: 39415
			public int UserLevelBefore;

			// Token: 0x040099F8 RID: 39416
			public int UserLevelAfter;

			// Token: 0x040099F9 RID: 39417
			public int UserExpBefore;

			// Token: 0x040099FA RID: 39418
			public int UserExpAfter;

			// Token: 0x040099FB RID: 39419
			public int UnitLevelBefore;

			// Token: 0x040099FC RID: 39420
			public int UnitLevelAfter;

			// Token: 0x040099FD RID: 39421
			public int UnitExpBefore;

			// Token: 0x040099FE RID: 39422
			public int UnitExpAfter;

			// Token: 0x040099FF RID: 39423
			public int LoyaltyBefore;

			// Token: 0x04009A00 RID: 39424
			public int LoyaltyAfter;
		}
	}
}
