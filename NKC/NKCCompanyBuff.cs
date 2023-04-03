using System;
using System.Collections.Generic;
using NKC.UI;
using NKM;
using NKM.Templet;

namespace NKC
{
	// Token: 0x02000655 RID: 1621
	public static class NKCCompanyBuff
	{
		// Token: 0x060032C2 RID: 12994 RVA: 0x000FCA9C File Offset: 0x000FAC9C
		public static void UpsertCompanyBuffData(List<NKMCompanyBuffData> companyBuffs, NKMCompanyBuffData data)
		{
			NKMCompanyBuffData nkmcompanyBuffData = companyBuffs.Find((NKMCompanyBuffData ele) => ele.Id == data.Id);
			if (nkmcompanyBuffData == null)
			{
				companyBuffs.Add(data);
				NKMUserData.OnCompanyBuffUpdate dOnCompanyBuffUpdate = NKCScenManager.CurrentUserData().dOnCompanyBuffUpdate;
				if (dOnCompanyBuffUpdate != null)
				{
					dOnCompanyBuffUpdate(NKCScenManager.CurrentUserData());
				}
			}
			else
			{
				NKMCompanyBuffTemplet companyBuffTemplet = NKMCompanyBuffManager.GetCompanyBuffTemplet(data.Id);
				nkmcompanyBuffData.UpdateExpireTicksAsMinutes(companyBuffTemplet.m_CompanyBuffTime);
				data.SetExpireTicks(nkmcompanyBuffData.ExpireTicks);
			}
			NKCPopupMessageCompanyBuff.Instance.Open(data, true);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_HOME)
			{
				NKCScenManager.GetScenManager().Get_SCEN_HOME().RefreshBuff();
			}
		}

		// Token: 0x060032C3 RID: 12995 RVA: 0x000FCB50 File Offset: 0x000FAD50
		public static void SetDiscountOfEterniumInEnteringWarfare(List<NKMCompanyBuffData> companyBuffs, ref int eternium)
		{
			int num = NKCCompanyBuff.GetDiscountOfEterniumInEnteringWarfare(companyBuffs, eternium);
			num += NKCCompanyBuff.GetDiscountOfEterniumInEnteringWarfareDungeon(companyBuffs, eternium);
			eternium = Math.Max(0, eternium - num);
		}

		// Token: 0x060032C4 RID: 12996 RVA: 0x000FCB80 File Offset: 0x000FAD80
		public static void SetDiscountOfEterniumInEnteringDungeon(List<NKMCompanyBuffData> companyBuffs, ref int eternium)
		{
			int discountOfEterniumInEnteringWarfareDungeon = NKCCompanyBuff.GetDiscountOfEterniumInEnteringWarfareDungeon(companyBuffs, eternium);
			eternium = Math.Max(0, eternium - discountOfEterniumInEnteringWarfareDungeon);
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x000FCBA4 File Offset: 0x000FADA4
		public static void SetDiscountOfCreditInNegotiation(List<NKMCompanyBuffData> companyBuffs, ref long credit)
		{
			long discountOfCreditInNegotiation = NKCCompanyBuff.GetDiscountOfCreditInNegotiation(companyBuffs, credit);
			credit = Math.Max(0L, credit - discountOfCreditInNegotiation);
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x000FCBC8 File Offset: 0x000FADC8
		public static void SetDiscountOfCreditInCraft(List<NKMCompanyBuffData> companyBuffs, ref int credit)
		{
			int discountOfCreditInCraft = NKCCompanyBuff.GetDiscountOfCreditInCraft(companyBuffs, credit);
			credit = Math.Max(0, credit - discountOfCreditInCraft);
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x000FCBEC File Offset: 0x000FADEC
		public static void SetDiscountOfCreditInEnchantTuning(List<NKMCompanyBuffData> companyBuffs, ref int credit)
		{
			int discountOfCreditInEnchantTuning = NKCCompanyBuff.GetDiscountOfCreditInEnchantTuning(companyBuffs, credit);
			credit = Math.Max(0, credit - discountOfCreditInEnchantTuning);
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x000FCC10 File Offset: 0x000FAE10
		public static void IncreaseMissioRateInWorldMap(List<NKMCompanyBuffData> companyBuffs, ref int successRate)
		{
			int totalRatio = NKCCompanyBuff.GetTotalRatio(companyBuffs, NKMConst.Buff.BuffType.WORLDMAP_MISSION_COMPLETE_RATIO_BONUS);
			if (totalRatio > 0)
			{
				int num = successRate * totalRatio / 100;
				successRate = Math.Min(100, successRate + num);
			}
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x000FCC3E File Offset: 0x000FAE3E
		public static void IncreaseChargePointOfPvpWithBonusRatio(List<NKMCompanyBuffData> companyBuffs, ref int rewardChargePoint, out int bonusRatio)
		{
			if (rewardChargePoint == 0)
			{
				bonusRatio = 0;
				return;
			}
			bonusRatio = NKCCompanyBuff.GetTotalRatio(companyBuffs, NKMConst.Buff.BuffType.PVP_POINT_CHARGE);
			if (bonusRatio > 0)
			{
				rewardChargePoint += rewardChargePoint * bonusRatio / 100;
			}
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x000FCC64 File Offset: 0x000FAE64
		public static void RemoveExpiredBuffs(List<NKMCompanyBuffData> companyBuffs)
		{
			List<NKMCompanyBuffData> list = new List<NKMCompanyBuffData>();
			for (int i = 0; i < companyBuffs.Count; i++)
			{
				if (NKCSynchronizedTime.IsFinished(companyBuffs[i].ExpireDate))
				{
					list.Add(companyBuffs[i]);
				}
			}
			if (list.Count > 0)
			{
				for (int j = 0; j < list.Count; j++)
				{
					NKCPopupMessageCompanyBuff.Instance.Open(list[j], false);
				}
				companyBuffs.RemoveAll((NKMCompanyBuffData ele) => NKCSynchronizedTime.IsFinished(ele.ExpireDate));
				NKCPacketSender.Send_NKMPacket_REFRESH_COMPANY_BUFF_REQ();
				NKMUserData.OnCompanyBuffUpdate dOnCompanyBuffUpdate = NKCScenManager.CurrentUserData().dOnCompanyBuffUpdate;
				if (dOnCompanyBuffUpdate == null)
				{
					return;
				}
				dOnCompanyBuffUpdate(NKCScenManager.CurrentUserData());
			}
		}

		// Token: 0x060032CB RID: 13003 RVA: 0x000FCD18 File Offset: 0x000FAF18
		public static bool NeedShowEventMark(List<NKMCompanyBuffData> companyBuffs, NKMConst.Buff.BuffType buffType)
		{
			foreach (NKMCompanyBuffData nkmcompanyBuffData in companyBuffs)
			{
				NKMCompanyBuffTemplet companyBuffTemplet = NKMCompanyBuffManager.GetCompanyBuffTemplet(nkmcompanyBuffData.Id);
				using (List<NKMCompanyBuffInfo>.Enumerator enumerator2 = companyBuffTemplet.m_CompanyBuffInfoList.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.m_CompanyBuffType == buffType)
						{
							return companyBuffTemplet.m_ShowEventMark;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x000FCDB8 File Offset: 0x000FAFB8
		public static int GetTotalRatio(List<NKMCompanyBuffData> companyBuffs, NKMConst.Buff.BuffType buffType)
		{
			int num = 0;
			foreach (NKMCompanyBuffData nkmcompanyBuffData in companyBuffs)
			{
				foreach (NKMCompanyBuffInfo nkmcompanyBuffInfo in NKMCompanyBuffManager.GetCompanyBuffTemplet(nkmcompanyBuffData.Id).m_CompanyBuffInfoList)
				{
					if (nkmcompanyBuffInfo.m_CompanyBuffType == buffType)
					{
						num += nkmcompanyBuffInfo.m_CompanyBuffRatio;
					}
				}
			}
			return num;
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x000FCE58 File Offset: 0x000FB058
		private static int GetDiscountOfEterniumInEnteringWarfare(List<NKMCompanyBuffData> companyBuffs, int eternium)
		{
			int totalRatio = NKCCompanyBuff.GetTotalRatio(companyBuffs, NKMConst.Buff.BuffType.WARFARE_ETERNIUM_DISCOUNT);
			if (totalRatio == 0)
			{
				return 0;
			}
			return totalRatio * eternium / 100;
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x000FCE78 File Offset: 0x000FB078
		private static int GetDiscountOfEterniumInEnteringWarfareDungeon(List<NKMCompanyBuffData> companyBuffs, int eternium)
		{
			int totalRatio = NKCCompanyBuff.GetTotalRatio(companyBuffs, NKMConst.Buff.BuffType.WARFARE_DUNGEON_ETERNIUM_DISCOUNT);
			if (totalRatio == 0)
			{
				return 0;
			}
			return totalRatio * eternium / 100;
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x000FCE98 File Offset: 0x000FB098
		private static long GetDiscountOfCreditInNegotiation(List<NKMCompanyBuffData> companyBuffs, long credit)
		{
			int totalRatio = NKCCompanyBuff.GetTotalRatio(companyBuffs, NKMConst.Buff.BuffType.BASE_PERSONNAL_NEGOTIATION_CREDIT_DISCOUNT);
			if (totalRatio == 0)
			{
				return 0L;
			}
			return (long)totalRatio * credit / 100L;
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x000FCEBC File Offset: 0x000FB0BC
		private static int GetDiscountOfCreditInCraft(List<NKMCompanyBuffData> companyBuffs, int credit)
		{
			int totalRatio = NKCCompanyBuff.GetTotalRatio(companyBuffs, NKMConst.Buff.BuffType.BASE_FACTORY_CRAFT_CREDIT_DISCOUNT);
			if (totalRatio == 0)
			{
				return 0;
			}
			return totalRatio * credit / 100;
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x000FCEE0 File Offset: 0x000FB0E0
		private static int GetDiscountOfCreditInEnchantTuning(List<NKMCompanyBuffData> companyBuffs, int credit)
		{
			int totalRatio = NKCCompanyBuff.GetTotalRatio(companyBuffs, NKMConst.Buff.BuffType.BASE_FACTORY_ENCHANT_TUNING_CREDIT_DISCOUNT);
			if (totalRatio == 0)
			{
				return 0;
			}
			return totalRatio * credit / 100;
		}
	}
}
