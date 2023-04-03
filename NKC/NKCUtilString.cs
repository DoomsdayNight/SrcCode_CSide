using System;
using System.Collections.Generic;
using System.Text;
using ClientPacket.Negotiation;
using ClientPacket.Warfare;
using Cs.Core.Util;
using Cs.Logging;
using NKC.Publisher;
using NKC.UI;
using NKC.UI.Collection;
using NKM;
using NKM.Guild;
using NKM.Shop;
using NKM.Templet;
using NKM.Templet.Base;
using NKM.Templet.Office;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006F7 RID: 1783
	public class NKCUtilString
	{
		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06003F4E RID: 16206 RVA: 0x00149003 File Offset: 0x00147203
		public static string GET_STRING_CONTENTS_VERSION_CHANGE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTENTS_VERSION_CHANGE", false);
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06003F4F RID: 16207 RVA: 0x00149010 File Offset: 0x00147210
		public static string GET_STRING_CUTSCENE_MOVIE_SKIP_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CUTSCENE_MOVIE_SKIP_TITLE", false);
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06003F50 RID: 16208 RVA: 0x0014901D File Offset: 0x0014721D
		public static string GET_STRING_CUTSCENE_MOVIE_SKIP_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CUTSCENE_MOVIE_SKIP_DESC", false);
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06003F51 RID: 16209 RVA: 0x0014902A File Offset: 0x0014722A
		public static string GET_STRING_COPY_COMPLETE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_COPY_COMPLETE", false);
			}
		}

		// Token: 0x06003F52 RID: 16210 RVA: 0x00149037 File Offset: 0x00147237
		public static string GetPlayTimeWarning(int hour)
		{
			return string.Format(NKCStringTable.GetString("SI_DP_PC_PLAYTIME_WARNING", false), hour);
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06003F53 RID: 16211 RVA: 0x0014904F File Offset: 0x0014724F
		public static string GET_STRING_SHUTDOWN_ALARM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHUTDOWN_ALARM", false);
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06003F54 RID: 16212 RVA: 0x0014905C File Offset: 0x0014725C
		public static string GET_STRING_COMMON_GRADE_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_COMMON_GRADE_ONE_PARAM", false);
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06003F55 RID: 16213 RVA: 0x00149069 File Offset: 0x00147269
		public static string GET_STRING_MAIN_SHIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MAIN_SHIP", false);
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06003F56 RID: 16214 RVA: 0x00149076 File Offset: 0x00147276
		public static string GET_STRING_MANAGEMENT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MANAGEMENT", false);
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06003F57 RID: 16215 RVA: 0x00149083 File Offset: 0x00147283
		public static string GET_STRING_EDITOR_NO_SUPPORT_THIS_FUNC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EDITOR_NO_SUPPORT_THIS_FUNC", false);
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06003F58 RID: 16216 RVA: 0x00149090 File Offset: 0x00147290
		public static string GET_STRING_QUIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_QUIT", false);
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06003F59 RID: 16217 RVA: 0x0014909D File Offset: 0x0014729D
		public static string GET_STRING_NO_EXIST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NO_EXIST", false);
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06003F5A RID: 16218 RVA: 0x001490AA File Offset: 0x001472AA
		public static string GET_STRING_WEAK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WEAK", false);
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06003F5B RID: 16219 RVA: 0x001490B7 File Offset: 0x001472B7
		public static string GET_STRING_NORMAL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NORMAL", false);
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06003F5C RID: 16220 RVA: 0x001490C4 File Offset: 0x001472C4
		public static string GET_STRING_CUSTOM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CUSTOM", false);
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06003F5D RID: 16221 RVA: 0x001490D1 File Offset: 0x001472D1
		public static string GET_STRING_WORST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORST", false);
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06003F5E RID: 16222 RVA: 0x001490DE File Offset: 0x001472DE
		public static string GET_STRING_LOW
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LOW", false);
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06003F5F RID: 16223 RVA: 0x001490EB File Offset: 0x001472EB
		public static string GET_STRING_GOOD
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GOOD", false);
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06003F60 RID: 16224 RVA: 0x001490F8 File Offset: 0x001472F8
		public static string GET_STRING_BEST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_BEST", false);
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06003F61 RID: 16225 RVA: 0x00149105 File Offset: 0x00147305
		public static string GET_STRING_LOW2
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LOW2", false);
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06003F62 RID: 16226 RVA: 0x00149112 File Offset: 0x00147312
		public static string GET_STRING_HIGH
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_HIGH", false);
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06003F63 RID: 16227 RVA: 0x0014911F File Offset: 0x0014731F
		public static string GET_STRING_ATTACK_READY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ATTACK_READY", false);
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06003F64 RID: 16228 RVA: 0x0014912C File Offset: 0x0014732C
		public static string GET_STRING_ATTACK_PREPARING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ATTACK_PREPARING", false);
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06003F65 RID: 16229 RVA: 0x00149139 File Offset: 0x00147339
		public static string GET_STRING_ATTACK_WAITING_OPPONENT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ATTACK_WAITING_OPPONENT", false);
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06003F66 RID: 16230 RVA: 0x00149146 File Offset: 0x00147346
		public static string GET_STRING_ATTACK_COST_IS_NOT_ENOUGH
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ATTACK_COST_IS_NOT_ENOUGH", false);
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06003F67 RID: 16231 RVA: 0x00149153 File Offset: 0x00147353
		public static string GET_STRING_ORGANIZATION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ORGANIZATION", false);
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06003F68 RID: 16232 RVA: 0x00149160 File Offset: 0x00147360
		public static string GET_STRING_SAVE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SAVE", false);
			}
		}

		// Token: 0x06003F69 RID: 16233 RVA: 0x00149170 File Offset: 0x00147370
		public static string GetRankNumber(int rank, bool bUpper = false)
		{
			int num = rank % 100;
			if (num - 11 <= 2)
			{
				if (!bUpper)
				{
					return "th";
				}
				return "TH";
			}
			else
			{
				switch (rank % 10)
				{
				case 1:
					if (!bUpper)
					{
						return "st";
					}
					return "ST";
				case 2:
					if (!bUpper)
					{
						return "nd";
					}
					return "ND";
				case 3:
					if (!bUpper)
					{
						return "rd";
					}
					return "RD";
				default:
					if (!bUpper)
					{
						return "th";
					}
					return "TH";
				}
			}
		}

		// Token: 0x06003F6A RID: 16234 RVA: 0x001491F0 File Offset: 0x001473F0
		public static string GetDeckNumberString(NKMDeckIndex deckIndex)
		{
			switch (deckIndex.m_eDeckType)
			{
			case NKM_DECK_TYPE.NDT_NORMAL:
				return NKCUtilString.GET_DECK_NUMBER_STRING_WARFARE + ((int)(deckIndex.m_iIndex + 1)).ToString();
			case NKM_DECK_TYPE.NDT_PVP:
				return NKCUtilString.GET_STRING_PVP + ((int)(deckIndex.m_iIndex + 1)).ToString();
			case NKM_DECK_TYPE.NDT_DAILY:
				return NKCUtilString.GET_DECK_NUMBER_STRING_DAILY + ((int)(deckIndex.m_iIndex + 1)).ToString();
			case NKM_DECK_TYPE.NDT_RAID:
				return NKCUtilString.GET_DECK_NUMBER_STRING_RAID + ((int)(deckIndex.m_iIndex + 1)).ToString();
			case NKM_DECK_TYPE.NDT_FRIEND:
				return NKCUtilString.GET_DECK_NUMBER_STRING_FRIEND + ((int)(deckIndex.m_iIndex + 1)).ToString();
			case NKM_DECK_TYPE.NDT_PVP_DEFENCE:
				return NKCUtilString.GET_DECK_NUMBER_STRING_PVP_DEFENCE;
			case NKM_DECK_TYPE.NDT_TRIM:
				return NKCUtilString.GET_DECK_NUMBER_STRING_TRIM + ((int)(deckIndex.m_iIndex + 1)).ToString();
			case NKM_DECK_TYPE.NDT_DIVE:
				return NKCUtilString.GET_DECK_NUMBER_STRING_DIVE + ((int)(deckIndex.m_iIndex + 1)).ToString();
			default:
				return "";
			}
		}

		// Token: 0x06003F6B RID: 16235 RVA: 0x001492FC File Offset: 0x001474FC
		public static string GetBuffStatValueShortString(NKM_STAT_TYPE statType, int Value, bool TTRate = false)
		{
			int num = Value / 100;
			int num2 = Value % 100;
			if (TTRate)
			{
				num = Value / 10000;
				num2 = Value % 10000;
			}
			bool flag = false;
			if (num < 0 || num2 < 0)
			{
				flag = true;
			}
			num = Math.Abs(num);
			num2 = Math.Abs(num2);
			if (NKCUtilString.IsNameReversedIfNegative(statType))
			{
				if (num2 == 0)
				{
					return string.Format("{0} +{1}", NKCUtilString.GetStatShortName(statType, Value), num);
				}
				return string.Format("{0} +{1}.{2:0#}", NKCUtilString.GetStatShortName(statType, Value), num, num2);
			}
			else if (num2 == 0)
			{
				if (!flag)
				{
					return string.Format("{0} +{1}", NKCUtilString.GetStatShortName(statType), num);
				}
				return string.Format("{0} -{1}", NKCUtilString.GetStatShortName(statType), num);
			}
			else
			{
				if (!flag)
				{
					return string.Format("{0} +{1}.{2:0#}", NKCUtilString.GetStatShortName(statType), num, num2);
				}
				return string.Format("{0} -{1}.{2:0#}", NKCUtilString.GetStatShortName(statType), num, num2);
			}
		}

		// Token: 0x06003F6C RID: 16236 RVA: 0x001493F4 File Offset: 0x001475F4
		public static string GetBuffStatFactorShortString(NKM_STAT_TYPE statType, int Factor, bool TTRate = false)
		{
			int num = Factor / 1;
			int num2 = Factor % 1;
			if (TTRate)
			{
				num = Factor / 100;
				num2 = Factor % 100;
			}
			bool flag = false;
			if (num < 0 || num2 < 0)
			{
				flag = true;
			}
			num = Math.Abs(num);
			num2 = Math.Abs(num2);
			if (NKCUtilString.IsNameReversedIfNegative(statType))
			{
				if (num2 == 0)
				{
					return string.Format("{0} +{1}%", NKCUtilString.GetStatShortName(statType, Factor), num);
				}
				return string.Format("{0} +{1}.{2:0#}%", NKCUtilString.GetStatShortName(statType, Factor), num, num2);
			}
			else if (num2 == 0)
			{
				if (!flag)
				{
					return string.Format("{0} +{1}%", NKCUtilString.GetStatShortName(statType, Factor), num);
				}
				return string.Format("{0} -{1}%", NKCUtilString.GetStatShortName(statType, Factor), num);
			}
			else
			{
				if (!flag)
				{
					return string.Format("{0} +{1}.{2:0#}%", NKCUtilString.GetStatShortName(statType, Factor), num, num2);
				}
				return string.Format("{0} -{1}.{2:0#}%", NKCUtilString.GetStatShortName(statType, Factor), num, num2);
			}
		}

		// Token: 0x06003F6D RID: 16237 RVA: 0x001494E8 File Offset: 0x001476E8
		public static string GetStatShortNameForInvenEquip(NKM_STAT_TYPE statType, float minValue, float maxValue, bool bPercent)
		{
			if (bPercent)
			{
				decimal num = new decimal(minValue);
				num = Math.Round(num * 1000m) / 1000m;
				decimal num2 = new decimal(maxValue);
				num2 = Math.Round(num2 * 1000m) / 1000m;
				if (NKCUtilString.IsNameReversedIfNegative(statType) && num2 < 0m)
				{
					return NKCUtilString.GetStatShortName(statType, true) + " " + string.Format("{0:P1}~{1:P1}", -num2, -num);
				}
				return NKCUtilString.GetStatShortName(statType) + " " + string.Format("{0:P1}~{1:P1}", num, num2);
			}
			else
			{
				if (NKCUtilString.IsNameReversedIfNegative(statType) && maxValue < 0f)
				{
					return NKCUtilString.GetStatShortName(statType, true) + " " + string.Format("{0}~{1}", Mathf.Abs(maxValue), Mathf.Abs(minValue));
				}
				return NKCUtilString.GetStatShortName(statType) + " " + string.Format("{0}~{1}", minValue, maxValue);
			}
		}

		// Token: 0x06003F6E RID: 16238 RVA: 0x0014962C File Offset: 0x0014782C
		public static string GetStatShortName(NKM_STAT_TYPE statType, bool bNegative)
		{
			if (statType == NKM_STAT_TYPE.NST_RANDOM || statType == NKM_STAT_TYPE.NST_END)
			{
				return "";
			}
			NKCStatInfoTemplet nkcstatInfoTemplet = NKMTempletContainer<NKCStatInfoTemplet>.Find((NKCStatInfoTemplet x) => x.StatType == statType);
			if (nkcstatInfoTemplet == null)
			{
				Log.Error(string.Format("NKCStatInfoTemplet is null - StatType : {0}", statType), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCUtilString.cs", 321);
				return NKCUtilString.GetStatShortName(statType);
			}
			if (bNegative && !string.IsNullOrEmpty(nkcstatInfoTemplet.Stat_Negative_Name))
			{
				return NKCStringTable.GetString(nkcstatInfoTemplet.Stat_Negative_Name, false);
			}
			return NKCStringTable.GetString(nkcstatInfoTemplet.Stat_Name, false);
		}

		// Token: 0x06003F6F RID: 16239 RVA: 0x001496CD File Offset: 0x001478CD
		public static string GetStatShortName(NKM_STAT_TYPE statType, decimal value)
		{
			return NKCUtilString.GetStatShortName(statType, value < 0m);
		}

		// Token: 0x06003F70 RID: 16240 RVA: 0x001496E0 File Offset: 0x001478E0
		public static string GetStatShortName(NKM_STAT_TYPE statType, int value)
		{
			return NKCUtilString.GetStatShortName(statType, value < 0);
		}

		// Token: 0x06003F71 RID: 16241 RVA: 0x001496EC File Offset: 0x001478EC
		public static string GetStatShortName(NKM_STAT_TYPE statType, float value)
		{
			return NKCUtilString.GetStatShortName(statType, value < 0f);
		}

		// Token: 0x06003F72 RID: 16242 RVA: 0x001496FC File Offset: 0x001478FC
		public static string GetStatShortString(NKM_STAT_TYPE statType, float value, bool bPercent)
		{
			if (bPercent)
			{
				return NKCUtilString.GetStatFactorShortString(statType, value, "{0} {1:+#.0%;-#.0%;0%}");
			}
			return NKCUtilString.GetStatValueShortString(statType, value, "{0} {1:+#;-#;''}");
		}

		// Token: 0x06003F73 RID: 16243 RVA: 0x0014971A File Offset: 0x0014791A
		public static string GetStatShortString(NKM_STAT_TYPE statType, float factor, float value)
		{
			if (factor != 0f)
			{
				return NKCUtilString.GetStatFactorShortString(statType, factor, "{0} {1:+#.0%;-#.0%;0%}");
			}
			return NKCUtilString.GetStatValueShortString(statType, value, "{0} {1:+#;-#;''}");
		}

		// Token: 0x06003F74 RID: 16244 RVA: 0x0014973D File Offset: 0x0014793D
		public static string GetStatShortString(string format, NKM_STAT_TYPE statType, float value)
		{
			if (NKCUtilString.IsNameReversedIfNegative(statType))
			{
				return string.Format(format, NKCUtilString.GetStatShortName(statType, value), Mathf.Abs(value));
			}
			return string.Format(format, NKCUtilString.GetStatShortName(statType), value);
		}

		// Token: 0x06003F75 RID: 16245 RVA: 0x00149774 File Offset: 0x00147974
		public static string GetStatShortString(string format, NKM_STAT_TYPE statType, decimal value)
		{
			if (NKCUtilString.IsNameReversedIfNegative(statType) && value < 0m)
			{
				return string.Format(format, NKCUtilString.GetStatShortName(statType, value), -value);
			}
			return string.Format(format, NKCUtilString.GetStatShortName(statType), value);
		}

		// Token: 0x06003F76 RID: 16246 RVA: 0x001497C1 File Offset: 0x001479C1
		public static string GetStatValueShortString(NKM_STAT_TYPE statType, float value, string format = "{0} {1:+#;-#;''}")
		{
			return NKCUtilString.GetStatShortString(format, statType, value);
		}

		// Token: 0x06003F77 RID: 16247 RVA: 0x001497CB File Offset: 0x001479CB
		public static string GetStatFactorShortString(NKM_STAT_TYPE statType, float factor, string format = "{0} {1:+#.0%;-#.0%;0%}")
		{
			return NKCUtilString.GetStatShortString(format, statType, factor);
		}

		// Token: 0x06003F78 RID: 16248 RVA: 0x001497D8 File Offset: 0x001479D8
		public static string GetSlotOptionString(NKMShipCmdSlot slotData, string format = "[{0}] {1}")
		{
			if (slotData == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (slotData.targetStyleType.Count > 0)
			{
				stringBuilder.Append("[");
				bool flag = false;
				foreach (NKM_UNIT_STYLE_TYPE unitType in slotData.targetStyleType)
				{
					if (flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(NKCUtilString.GetUnitStyleName(unitType) ?? "");
					flag = true;
				}
				stringBuilder.Append("] ");
			}
			if (slotData.targetRoleType.Count > 0)
			{
				stringBuilder.Append("[");
				bool flag2 = false;
				foreach (NKM_UNIT_ROLE_TYPE roleType in slotData.targetRoleType)
				{
					if (flag2)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(NKCUtilString.GetRoleText(roleType, false) ?? "");
					flag2 = true;
				}
				stringBuilder.Append("] ");
			}
			if (NKCUtilString.IsNameReversedIfNegative(slotData.statType) && slotData.statValue + slotData.statFactor < 0f)
			{
				stringBuilder.Append(string.Format(format, NKCUtilString.GetStatShortName(slotData.statType, true), NKCUtilString.GetShipModuleStatValue(slotData.statType, slotData.statValue, slotData.statFactor)));
			}
			else
			{
				stringBuilder.Append(string.Format(format, NKCUtilString.GetStatShortName(slotData.statType), NKCUtilString.GetShipModuleStatValue(slotData.statType, slotData.statValue, slotData.statFactor)));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003F79 RID: 16249 RVA: 0x0014999C File Offset: 0x00147B9C
		public static string GetShipModuleStatValue(NKM_STAT_TYPE statType, float statValue, float statFactor)
		{
			float value = statValue + statFactor;
			decimal num = new decimal(value);
			num = Math.Round(num * 1000m) / 1000m;
			if (num < 0m && NKCUtilString.IsNameReversedIfNegative(statType))
			{
				num = -num;
			}
			if (!(num > 0m))
			{
				return string.Empty;
			}
			if (NKMShipManager.IsPercentStat(statType, statFactor))
			{
				return string.Format("{0:P1}", num);
			}
			return string.Format("{0}", num);
		}

		// Token: 0x06003F7A RID: 16250 RVA: 0x00149A34 File Offset: 0x00147C34
		public static bool IsNameReversedIfNegative(NKM_STAT_TYPE statType)
		{
			NKCStatInfoTemplet nkcstatInfoTemplet = NKMTempletContainer<NKCStatInfoTemplet>.Find((NKCStatInfoTemplet x) => x.StatType == statType);
			return nkcstatInfoTemplet != null && !string.IsNullOrEmpty(nkcstatInfoTemplet.Stat_Negative_Name);
		}

		// Token: 0x06003F7B RID: 16251 RVA: 0x00149A74 File Offset: 0x00147C74
		public static string GetStatShortName(NKM_STAT_TYPE statType)
		{
			if (statType == NKM_STAT_TYPE.NST_RANDOM || statType == NKM_STAT_TYPE.NST_END)
			{
				return "";
			}
			NKCStatInfoTemplet nkcstatInfoTemplet = NKMTempletContainer<NKCStatInfoTemplet>.Find((NKCStatInfoTemplet x) => x.StatType == statType);
			if (nkcstatInfoTemplet != null)
			{
				return NKCStringTable.GetString(nkcstatInfoTemplet.Stat_Name, false);
			}
			Log.Error(string.Format("NKCStatInfoTemplet is null - StatType : {0}", statType), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCUtilString.cs", 491);
			return "";
		}

		// Token: 0x06003F7C RID: 16252 RVA: 0x00149AF4 File Offset: 0x00147CF4
		public static string GetUnitStyleName(NKM_UNIT_STYLE_TYPE unitType)
		{
			switch (unitType)
			{
			case NKM_UNIT_STYLE_TYPE.NUST_COUNTER:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_NAME_NUST_COUNTER", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SOLDIER:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_NAME_NUST_SOLDIER", false);
			case NKM_UNIT_STYLE_TYPE.NUST_MECHANIC:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_NAME_NUST_MECHANIC", false);
			case NKM_UNIT_STYLE_TYPE.NUST_CORRUPTED:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_NAME_NUST_CORRUPTED", false);
			case NKM_UNIT_STYLE_TYPE.NUST_REPLACER:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_NAME_NUST_REPLACER", false);
			case NKM_UNIT_STYLE_TYPE.NUST_TRAINER:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_NAME_NUST_TRAINER", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_ASSAULT:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_NAME_NUST_SHIP_ASSAULT", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_HEAVY:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_NAME_NUST_SHIP_HEAVY", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_CRUISER:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_NAME_NUST_SHIP_CRUISER", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_SPECIAL:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_NAME_NUST_SHIP_SPECIAL", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_PATROL:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_NAME_NUST_SHIP_PATROL", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_ETC:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_NAME_NUST_SHIP_ETC", false);
			case NKM_UNIT_STYLE_TYPE.NUST_ENCHANT:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_NAME_NUST_ENCHANT", false);
			case NKM_UNIT_STYLE_TYPE.NUST_OPERATOR:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_NAME_NUST_OPERATOR", false);
			}
			return "";
		}

		// Token: 0x06003F7D RID: 16253 RVA: 0x00149BFC File Offset: 0x00147DFC
		public static string GetUnitStyleEngName(NKM_UNIT_STYLE_TYPE unitType)
		{
			switch (unitType)
			{
			case NKM_UNIT_STYLE_TYPE.NUST_COUNTER:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_ENG_NAME_NUST_COUNTER", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SOLDIER:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_ENG_NAME_NUST_SOLDIER", false);
			case NKM_UNIT_STYLE_TYPE.NUST_MECHANIC:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_ENG_NAME_NUST_MECHANIC", false);
			case NKM_UNIT_STYLE_TYPE.NUST_CORRUPTED:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_ENG_NAME_NUST_CORRUPTED", false);
			case NKM_UNIT_STYLE_TYPE.NUST_REPLACER:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_ENG_NAME_NUST_REPLACER", false);
			case NKM_UNIT_STYLE_TYPE.NUST_TRAINER:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_ENG_NAME_NUST_TRAINER", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_ASSAULT:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_ENG_NAME_NUST_SHIP_ASSAULT", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_HEAVY:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_ENG_NAME_NUST_SHIP_HEAVY", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_CRUISER:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_ENG_NAME_NUST_SHIP_CRUISER", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_SPECIAL:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_ENG_NAME_NUST_SHIP_SPECIAL", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_PATROL:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_ENG_NAME_NUST_SHIP_PATROL", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_ETC:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_ENG_NAME_NUST_SHIP_ETC", false);
			default:
				return "";
			}
		}

		// Token: 0x06003F7E RID: 16254 RVA: 0x00149CDC File Offset: 0x00147EDC
		public static string GetUnitStyleDesc(NKM_UNIT_STYLE_TYPE unitType)
		{
			switch (unitType)
			{
			case NKM_UNIT_STYLE_TYPE.NUST_COUNTER:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_DESC_NUST_COUNTER", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SOLDIER:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_DESC_NUST_SOLDIER", false);
			case NKM_UNIT_STYLE_TYPE.NUST_MECHANIC:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_DESC_NUST_MECHANIC", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_ASSAULT:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_DESC_NUST_SHIP_ASSAULT", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_HEAVY:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_DESC_NUST_SHIP_HEAVY", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_CRUISER:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_DESC_NUST_SHIP_CRUISER", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_SPECIAL:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_DESC_NUST_SHIP_SPECIAL", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_PATROL:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_DESC_NUST_SHIP_PATROL", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_ETC:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_DESC_NUST_SHIP_ETC", false);
			}
			return "";
		}

		// Token: 0x06003F7F RID: 16255 RVA: 0x00149D94 File Offset: 0x00147F94
		public static string GetUnitStyleString(NKMUnitTempletBase unitTemplet)
		{
			if (unitTemplet == null)
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(NKCUtilString.GetUnitStyleName(unitTemplet.m_NKM_UNIT_STYLE_TYPE));
			if (unitTemplet.m_NKM_UNIT_STYLE_TYPE_SUB != NKM_UNIT_STYLE_TYPE.NUST_INVALID)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(NKCUtilString.GetUnitStyleName(unitTemplet.m_NKM_UNIT_STYLE_TYPE_SUB));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003F80 RID: 16256 RVA: 0x00149DF0 File Offset: 0x00147FF0
		public static string GetUnitStyleMarkString(NKMUnitTempletBase unitTemplet)
		{
			if (unitTemplet == null)
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(NKCUtilString.GetUnitStyleEngName(unitTemplet.m_NKM_UNIT_STYLE_TYPE));
			if (unitTemplet.m_NKM_UNIT_STYLE_TYPE_SUB != NKM_UNIT_STYLE_TYPE.NUST_INVALID)
			{
				stringBuilder.Append(" ");
				string unitSubTypeColorCode = NKCUtilString.GetUnitSubTypeColorCode(unitTemplet.m_NKM_UNIT_STYLE_TYPE_SUB);
				if (!string.IsNullOrEmpty(unitSubTypeColorCode))
				{
					stringBuilder.AppendFormat("<color=#{0}>", unitSubTypeColorCode);
				}
				stringBuilder.Append(NKCUtilString.GetUnitStyleEngName(unitTemplet.m_NKM_UNIT_STYLE_TYPE_SUB));
				if (!string.IsNullOrEmpty(unitSubTypeColorCode))
				{
					stringBuilder.Append("</color>");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003F81 RID: 16257 RVA: 0x00149E7F File Offset: 0x0014807F
		public static string GetUnitSubTypeColorCode(NKM_UNIT_STYLE_TYPE unitType)
		{
			if (unitType == NKM_UNIT_STYLE_TYPE.NUST_CORRUPTED)
			{
				return "EC2200";
			}
			if (unitType != NKM_UNIT_STYLE_TYPE.NUST_REPLACER)
			{
				return "";
			}
			return "FF0081";
		}

		// Token: 0x06003F82 RID: 16258 RVA: 0x00149E9A File Offset: 0x0014809A
		public static string GetEquipPosSimpleStrByUnitStyle(NKM_UNIT_STYLE_TYPE unitType, ITEM_EQUIP_POSITION equipPosition)
		{
			return string.Format("{0} {1}", NKCUtilString.GetUnitStyleName(unitType), NKCUtilString.GetEquipPositionString(equipPosition));
		}

		// Token: 0x06003F83 RID: 16259 RVA: 0x00149EB4 File Offset: 0x001480B4
		public static string GetSkillTypeName(NKM_SKILL_TYPE type)
		{
			switch (type)
			{
			case NKM_SKILL_TYPE.NST_PASSIVE:
				return NKCStringTable.GetString("SI_DP_SKILL_TYPE_NAME_NST_PASSIVE", false);
			case NKM_SKILL_TYPE.NST_ATTACK:
				return NKCStringTable.GetString("SI_DP_SKILL_TYPE_NAME_NST_ATTACK", false);
			case NKM_SKILL_TYPE.NST_SKILL:
				return NKCStringTable.GetString("SI_DP_SKILL_TYPE_NAME_NST_SKILL", false);
			case NKM_SKILL_TYPE.NST_HYPER:
				return NKCStringTable.GetString("SI_DP_SKILL_TYPE_NAME_NST_HYPER", false);
			case NKM_SKILL_TYPE.NST_SHIP_ACTIVE:
				return NKCStringTable.GetString("SI_DP_SKILL_TYPE_NAME_NST_SHIP_ACTIVE", false);
			case NKM_SKILL_TYPE.NST_LEADER:
				return NKCStringTable.GetString("SI_PF_REARM_LEADER_SKILL", false);
			default:
				Debug.LogError("Unknown skill type");
				return "";
			}
		}

		// Token: 0x06003F84 RID: 16260 RVA: 0x00149F3A File Offset: 0x0014813A
		public static string GetRoleText(NKMUnitTempletBase unitTempletBase)
		{
			if (unitTempletBase == null)
			{
				return "";
			}
			return NKCUtilString.GetRoleText(unitTempletBase.m_NKM_UNIT_ROLE_TYPE, unitTempletBase.m_bAwaken);
		}

		// Token: 0x06003F85 RID: 16261 RVA: 0x00149F58 File Offset: 0x00148158
		public static string GetRoleText(NKM_UNIT_ROLE_TYPE roleType, bool bAwaken)
		{
			if (bAwaken)
			{
				switch (roleType)
				{
				case NKM_UNIT_ROLE_TYPE.NURT_STRIKER:
					return NKCStringTable.GetString("SI_DP_ROLE_TEXT_NURT_STRIKER_AWAKEN", false);
				case NKM_UNIT_ROLE_TYPE.NURT_RANGER:
					return NKCStringTable.GetString("SI_DP_ROLE_TEXT_NURT_RANGER_AWAKEN", false);
				case NKM_UNIT_ROLE_TYPE.NURT_DEFENDER:
					return NKCStringTable.GetString("SI_DP_ROLE_TEXT_NURT_DEFENDER_AWAKEN", false);
				case NKM_UNIT_ROLE_TYPE.NURT_SNIPER:
					return NKCStringTable.GetString("SI_DP_ROLE_TEXT_NURT_SNIPER_AWAKEN", false);
				case NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER:
					return NKCStringTable.GetString("SI_DP_ROLE_TEXT_NURT_SUPPORTER_AWAKEN", false);
				case NKM_UNIT_ROLE_TYPE.NURT_SIEGE:
					return NKCStringTable.GetString("SI_DP_ROLE_TEXT_NURT_SIEGE_AWAKEN", false);
				case NKM_UNIT_ROLE_TYPE.NURT_TOWER:
					return NKCStringTable.GetString("SI_DP_ROLE_TEXT_NURT_TOWER_AWAKEN", false);
				default:
					Debug.LogError("not implemented type : " + roleType.ToString());
					break;
				}
			}
			else
			{
				switch (roleType)
				{
				case NKM_UNIT_ROLE_TYPE.NURT_STRIKER:
					return NKCStringTable.GetString("SI_DP_ROLE_TEXT_NURT_STRIKER", false);
				case NKM_UNIT_ROLE_TYPE.NURT_RANGER:
					return NKCStringTable.GetString("SI_DP_ROLE_TEXT_NURT_RANGER", false);
				case NKM_UNIT_ROLE_TYPE.NURT_DEFENDER:
					return NKCStringTable.GetString("SI_DP_ROLE_TEXT_NURT_DEFENDER", false);
				case NKM_UNIT_ROLE_TYPE.NURT_SNIPER:
					return NKCStringTable.GetString("SI_DP_ROLE_TEXT_NURT_SNIPER", false);
				case NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER:
					return NKCStringTable.GetString("SI_DP_ROLE_TEXT_NURT_SUPPORTER", false);
				case NKM_UNIT_ROLE_TYPE.NURT_SIEGE:
					return NKCStringTable.GetString("SI_DP_ROLE_TEXT_NURT_SIEGE", false);
				case NKM_UNIT_ROLE_TYPE.NURT_TOWER:
					return NKCStringTable.GetString("SI_DP_ROLE_TEXT_NURT_TOWER", false);
				default:
					Debug.LogError("not implemented type : " + roleType.ToString());
					break;
				}
			}
			return "";
		}

		// Token: 0x06003F86 RID: 16262 RVA: 0x0014A0A4 File Offset: 0x001482A4
		public static string GetMoveTypeText(bool bAirUnit)
		{
			string @string;
			if (bAirUnit)
			{
				@string = NKCStringTable.GetString("SI_DP_MOVE_TYPE_TEXT_MT_AIR", false);
			}
			else
			{
				@string = NKCStringTable.GetString("SI_DP_MOVE_TYPE_TEXT_MT_LAND", false);
			}
			return @string;
		}

		// Token: 0x06003F87 RID: 16263 RVA: 0x0014A0D8 File Offset: 0x001482D8
		public static string GetAtkTypeText(NKM_FIND_TARGET_TYPE targetType)
		{
			switch (targetType)
			{
			case NKM_FIND_TARGET_TYPE.NFTT_NO:
				return NKCStringTable.GetString("SI_DP_ATK_TYPE_TEXT_NFTT_NO", false);
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY:
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_AIR_FIRST:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_BOSS_LAST:
				return NKCStringTable.GetString("SI_DP_ATK_TYPE_TEXT_NFTT_FAR_ENEMY", false);
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_LAND:
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_LAND_RANGER_SUPPORTER_SNIPER_FIRST:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_LAND:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_LAND_BOSS_LAST:
				return NKCStringTable.GetString("SI_DP_ATK_TYPE_TEXT_NFTT_FAR_ENEMY_LAND", false);
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_AIR:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_AIR:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_AIR_BOSS_LAST:
				return NKCStringTable.GetString("SI_DP_ATK_TYPE_TEXT_NFTT_FAR_ENEMY_AIR", false);
			case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_ONLY:
			case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS:
				return NKCStringTable.GetString("SI_DP_ATK_TYPE_TEXT_NFTT_ENEMY_BOSS", false);
			case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_LAND:
				return NKCStringTable.GetString("SI_DP_ATK_TYPE_TEXT_NFTT_ENEMY_BOSS_LAND", false);
			case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_AIR:
				return NKCStringTable.GetString("SI_DP_ATK_TYPE_TEXT_NFTT_ENEMY_BOSS_AIR", false);
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM:
				return NKCStringTable.GetString("SI_DP_ATK_TYPE_TEXT_NFTT_NEAR_MY_TEAM", false);
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LAND:
				return NKCStringTable.GetString("SI_DP_ATK_TYPE_TEXT_NFTT_NEAR_MY_TEAM_LAND", false);
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_AIR:
				return NKCStringTable.GetString("SI_DP_ATK_TYPE_TEXT_NFTT_NEAR_MY_TEAM_AIR", false);
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LOW_HP:
				return NKCStringTable.GetString("SI_DP_ATK_TYPE_TEXT_NFTT_NEAR_MY_TEAM_LOW_HP", false);
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LOW_HP_LAND:
				return NKCStringTable.GetString("SI_DP_ATK_TYPE_TEXT_NFTT_NEAR_MY_TEAM_LOW_HP_LAND", false);
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LOW_HP_AIR:
				return NKCStringTable.GetString("SI_DP_ATK_TYPE_TEXT_NFTT_NEAR_MY_TEAM_LOW_HP_AIR", false);
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_MY_TEAM:
				return NKCStringTable.GetString("SI_DP_ATK_TYPE_TEXT_NFTT_FAR_MY_TEAM", false);
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_MY_TEAM_LAND:
				return NKCStringTable.GetString("SI_DP_ATK_TYPE_TEXT_NFTT_FAR_MY_TEAM_LAND", false);
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_MY_TEAM_AIR:
				return NKCStringTable.GetString("SI_DP_ATK_TYPE_TEXT_NFTT_FAR_MY_TEAM_AIR", false);
			default:
				return "";
			}
		}

		// Token: 0x06003F88 RID: 16264 RVA: 0x0014A21C File Offset: 0x0014841C
		public static string GetGradeString(NKM_UNIT_GRADE grade)
		{
			switch (grade)
			{
			default:
				return NKCStringTable.GetString("SI_DP_UNIT_GRADE_STRING_NUG_N", false);
			case NKM_UNIT_GRADE.NUG_R:
				return NKCStringTable.GetString("SI_DP_UNIT_GRADE_STRING_NUG_R", false);
			case NKM_UNIT_GRADE.NUG_SR:
				return NKCStringTable.GetString("SI_DP_UNIT_GRADE_STRING_NUG_SR", false);
			case NKM_UNIT_GRADE.NUG_SSR:
				return NKCStringTable.GetString("SI_DP_UNIT_GRADE_STRING_NUG_SSR", false);
			}
		}

		// Token: 0x06003F89 RID: 16265 RVA: 0x0014A26E File Offset: 0x0014846E
		public static string GetItemEquipTier(int tier)
		{
			return string.Format(string.Format("T{0}", tier), Array.Empty<object>());
		}

		// Token: 0x06003F8A RID: 16266 RVA: 0x0014A28A File Offset: 0x0014848A
		public static string GetItemEquipNameWithTier(NKMEquipTemplet equipTemplet)
		{
			if (equipTemplet != null)
			{
				return string.Format(equipTemplet.GetItemName() + " " + NKCUtilString.GetItemEquipTier(equipTemplet.m_NKM_ITEM_TIER), Array.Empty<object>());
			}
			return "";
		}

		// Token: 0x06003F8B RID: 16267 RVA: 0x0014A2BC File Offset: 0x001484BC
		public static string GetSlotModeTypeString(NKCUISlot.eSlotMode type, int ID)
		{
			switch (type)
			{
			case NKCUISlot.eSlotMode.Unit:
			case NKCUISlot.eSlotMode.UnitCount:
				return NKCStringTable.GetString("SI_DP_SLOT_MODE_TYPE_STRING_UNIT", false);
			case NKCUISlot.eSlotMode.ItemMisc:
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(ID);
				if (itemMiscTempletByID != null)
				{
					return NKCUtilString.GetMiscTypeString(itemMiscTempletByID.m_ItemMiscType);
				}
				break;
			}
			case NKCUISlot.eSlotMode.Equip:
			case NKCUISlot.eSlotMode.EquipCount:
				return NKCStringTable.GetString("SI_DP_SLOT_MODE_TYPE_STRING_EQUIP", false);
			case NKCUISlot.eSlotMode.Skin:
				return NKCStringTable.GetString("SI_DP_SLOT_MODE_TYPE_STRING_SKIN", false);
			case NKCUISlot.eSlotMode.Mold:
				return NKCStringTable.GetString("SI_DP_SLOT_MODE_TYPE_STRING_MOLD", false);
			}
			return "";
		}

		// Token: 0x06003F8C RID: 16268 RVA: 0x0014A340 File Offset: 0x00148540
		public static string GetUnitAbilityName(int unitID, string seperator = "   ")
		{
			StringBuilder stringBuilder = new StringBuilder();
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
			bool flag = false;
			if (unitTempletBase != null)
			{
				if (unitTempletBase.m_bRespawnFreePos)
				{
					stringBuilder.Append(NKCStringTable.GetString("SI_DP_UNIT_ABILITY_NAME_TAG_RESPAWN_FREE_POS", false));
					flag = true;
				}
				if (unitTempletBase.m_bTagRevenge)
				{
					if (flag)
					{
						stringBuilder.Append(seperator);
					}
					stringBuilder.Append(NKCStringTable.GetString("SI_DP_UNIT_ABILITY_NAME_TAG_REVENGE", false));
					flag = true;
				}
				if (unitTempletBase.m_bTagPatrol)
				{
					if (flag)
					{
						stringBuilder.Append(seperator);
					}
					stringBuilder.Append(NKCStringTable.GetString("SI_DP_UNIT_ABILITY_NAME_TAG_PATROL", false));
					flag = true;
				}
				if (unitTempletBase.m_bTagSwingby)
				{
					if (flag)
					{
						stringBuilder.Append(seperator);
					}
					stringBuilder.Append(NKCStringTable.GetString("SI_DP_UNIT_ABILITY_NAME_TAG_SWINGBY", false));
					flag = true;
				}
				if (unitTempletBase.StopDefaultCoolTime)
				{
					if (flag)
					{
						stringBuilder.Append(seperator);
					}
					stringBuilder.Append(NKCStringTable.GetString("SI_PF_POPUP_UNIT_INFOPOPUP_FURY", false));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003F8D RID: 16269 RVA: 0x0014A420 File Offset: 0x00148620
		public static string GetEquipTypeString(NKMEquipTemplet equipTemplet)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(equipTemplet.GetPrivateUnitID());
			if (unitTempletBase != null)
			{
				return string.Format(NKCStringTable.GetString("SI_DP_EQUIP_TYPE_STRING_PRIVATE", false), unitTempletBase.GetUnitName(), NKCUtilString.GetEquipPositionString(equipTemplet.m_ItemEquipPosition));
			}
			return string.Format(NKCStringTable.GetString("SI_DP_EQUIP_TYPE_STRING_ELSE", false), NKCUtilString.GetUnitStyleName(equipTemplet.m_EquipUnitStyleType), NKCUtilString.GetEquipPositionString(equipTemplet.m_ItemEquipPosition));
		}

		// Token: 0x06003F8E RID: 16270 RVA: 0x0014A484 File Offset: 0x00148684
		public static string GetEquipPositionStringByUnitStyle(NKMEquipTemplet equipTemplet, bool skipPrivateUnit = false)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(equipTemplet.GetPrivateUnitID());
			if (unitTempletBase != null && !skipPrivateUnit)
			{
				return string.Format(NKCStringTable.GetString("SI_DP_EQUIP_POSITION_STRING_BY_UNIT_STYLE_PRIVATE", false), unitTempletBase.GetUnitName());
			}
			ITEM_EQUIP_POSITION itemEquipPosition = equipTemplet.m_ItemEquipPosition;
			return NKCUtilString.GetEquipPosSimpleStrByUnitStyle(equipTemplet.m_EquipUnitStyleType, itemEquipPosition);
		}

		// Token: 0x06003F8F RID: 16271 RVA: 0x0014A4D0 File Offset: 0x001486D0
		public static string GetRespawnCountText(int unitID)
		{
			string result = "";
			NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitID);
			int num = (unitStatTemplet != null) ? unitStatTemplet.m_RespawnCount : 1;
			if (num > 1)
			{
				result = string.Format("x" + num.ToString(), Array.Empty<object>());
			}
			return result;
		}

		// Token: 0x06003F90 RID: 16272 RVA: 0x0014A518 File Offset: 0x00148718
		public static string GetFriendCode(long friendCode, bool bOpponent)
		{
			if (friendCode <= 0L)
			{
				return "";
			}
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData != null)
			{
				if (bOpponent)
				{
					if (gameOptionData.StreamingHideOpponentInfo)
					{
						return "";
					}
				}
				else if (gameOptionData.StreamingHideMyInfo)
				{
					return "";
				}
			}
			return string.Format(string.Format("#{0}", friendCode), Array.Empty<object>());
		}

		// Token: 0x06003F91 RID: 16273 RVA: 0x0014A577 File Offset: 0x00148777
		public static string GetFriendCode(long friendCode)
		{
			if (friendCode <= 0L)
			{
				return "";
			}
			return string.Format(string.Format("#{0}", friendCode), Array.Empty<object>());
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06003F92 RID: 16274 RVA: 0x0014A59E File Offset: 0x0014879E
		public static string GET_STRING_NICKNAME_CHANGE_RECHECK_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PROFILE_NICKNAME_CHANGE_RECHECK_ONE_PARAM", false);
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06003F93 RID: 16275 RVA: 0x0014A5AB File Offset: 0x001487AB
		public static string GET_STRING_ERROR
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ERROR", false);
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06003F94 RID: 16276 RVA: 0x0014A5B8 File Offset: 0x001487B8
		public static string GET_STRING_NOTICE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NOTICE", false);
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06003F95 RID: 16277 RVA: 0x0014A5C5 File Offset: 0x001487C5
		public static string GET_STRING_WARNING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARNING", false);
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06003F96 RID: 16278 RVA: 0x0014A5D2 File Offset: 0x001487D2
		public static string GET_STRING_UNLOCK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_UNLOCK", false);
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06003F97 RID: 16279 RVA: 0x0014A5DF File Offset: 0x001487DF
		public static string GET_STRING_INFORMATION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INFORMATION", false);
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06003F98 RID: 16280 RVA: 0x0014A5EC File Offset: 0x001487EC
		public static string GET_STRING_CONFIRM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONFIRM", false);
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06003F99 RID: 16281 RVA: 0x0014A5F9 File Offset: 0x001487F9
		public static string GET_STRING_CANCEL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CANCEL", false);
			}
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x0014A606 File Offset: 0x00148806
		public static string GET_STRING_CONFIRM_BY_ALL_SEARCH()
		{
			if (NKCStringTable.CheckExistString("SI_DP_CONFIRM"))
			{
				return NKCStringTable.GetString("SI_DP_CONFIRM", false);
			}
			if (NKCStringTable.CheckExistString("SI_DP_PATCHER_CONFIRM"))
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_CONFIRM", false);
			}
			return "";
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x0014A63D File Offset: 0x0014883D
		public static string GET_STRING_CANCEL_BY_ALL_SEARCH()
		{
			if (NKCStringTable.CheckExistString("SI_DP_CANCEL"))
			{
				return NKCStringTable.GetString("SI_DP_CANCEL", false);
			}
			if (NKCStringTable.CheckExistString("SI_DP_PATCHER_CANCEL"))
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_CANCEL", false);
			}
			return "";
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06003F9C RID: 16284 RVA: 0x0014A674 File Offset: 0x00148874
		public static string GET_STRING_SQUAD_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SQUAD_ONE_PARAM", false);
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06003F9D RID: 16285 RVA: 0x0014A681 File Offset: 0x00148881
		public static string GET_STRING_SQUAD_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SQUAD_TWO_PARAM", false);
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06003F9E RID: 16286 RVA: 0x0014A68E File Offset: 0x0014888E
		public static string GET_STRING_CHOICE_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CHOICE_ONE_PARAM", false);
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06003F9F RID: 16287 RVA: 0x0014A69B File Offset: 0x0014889B
		public static string GET_STRING_NO_EXIST_EQUIP_TO_CHANGE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NO_EXIST_EQUIP_TO_CHANGE", false);
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06003FA0 RID: 16288 RVA: 0x0014A6A8 File Offset: 0x001488A8
		public static string GET_STRING_COMING_SOON_SYSTEM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_COMING_SOON_SYSTEM", false);
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06003FA1 RID: 16289 RVA: 0x0014A6B5 File Offset: 0x001488B5
		public static string GET_STRING_COUNTING_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_COUNTING_ONE_PARAM", false);
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06003FA2 RID: 16290 RVA: 0x0014A6C2 File Offset: 0x001488C2
		public static string GET_STRING_TOTAL_RANK_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOTAL_RANK_ONE_PARAM", false);
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06003FA3 RID: 16291 RVA: 0x0014A6CF File Offset: 0x001488CF
		public static string GET_STRING_RANK_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_RANK_ONE_PARAM", false);
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06003FA4 RID: 16292 RVA: 0x0014A6DC File Offset: 0x001488DC
		public static string GET_STRING_NO_EXIST_TARGET_TO_SELECT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NO_EXIST_TARGET_TO_SELECT", false);
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06003FA5 RID: 16293 RVA: 0x0014A6E9 File Offset: 0x001488E9
		public static string GET_STRING_PROFILE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PROFILE", false);
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06003FA6 RID: 16294 RVA: 0x0014A6F6 File Offset: 0x001488F6
		public static string GET_STRING_REWARD_LIST_POPUP_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REWARD_LIST_POPUP_TITLE", false);
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06003FA7 RID: 16295 RVA: 0x0014A703 File Offset: 0x00148903
		public static string GET_STRING_REWARD_LIST_POPUP_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REWARD_LIST_POPUP_DESC", false);
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06003FA8 RID: 16296 RVA: 0x0014A710 File Offset: 0x00148910
		public static string GET_STRING_POPUP_RESOURCE_CONFIRM_REWARD_DESC_02
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_POPUP_RESOURCE_CONFIRM_REWARD_DESC_02", false);
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06003FA9 RID: 16297 RVA: 0x0014A71D File Offset: 0x0014891D
		public static string GET_STRING_WIN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WIN", false);
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06003FAA RID: 16298 RVA: 0x0014A72A File Offset: 0x0014892A
		public static string GET_STRING_LOSE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LOSE", false);
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06003FAB RID: 16299 RVA: 0x0014A737 File Offset: 0x00148937
		public static string GET_STRING_DRAW
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DRAW", false);
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06003FAC RID: 16300 RVA: 0x0014A744 File Offset: 0x00148944
		public static string GET_STRING_LEVEL_UP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LEVEL_UP", false);
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06003FAD RID: 16301 RVA: 0x0014A751 File Offset: 0x00148951
		public static string GET_STRING_LEVEL_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LEVEL_ONE_PARAM", false);
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06003FAE RID: 16302 RVA: 0x0014A75E File Offset: 0x0014895E
		public static string GET_STRING_EXP_PLUS_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EXP_PLUS_ONE_PARAM", false);
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06003FAF RID: 16303 RVA: 0x0014A76B File Offset: 0x0014896B
		public static string GET_STRING_PLUS_EXP_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PLUS_EXP_ONE_PARAM", false);
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06003FB0 RID: 16304 RVA: 0x0014A778 File Offset: 0x00148978
		public static string GET_STRING_SHIP_INFO
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHIP_INFO", false);
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06003FB1 RID: 16305 RVA: 0x0014A785 File Offset: 0x00148985
		public static string GET_STRING_ITEM_GAIN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ITEM_GAIN", false);
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06003FB2 RID: 16306 RVA: 0x0014A792 File Offset: 0x00148992
		public static string GET_STRING_CONGRATULATION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONGRATULATION", false);
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06003FB3 RID: 16307 RVA: 0x0014A79F File Offset: 0x0014899F
		public static string GET_STRING_UNIT_INFO
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_UNIT_INFO", false);
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06003FB4 RID: 16308 RVA: 0x0014A7AC File Offset: 0x001489AC
		public static string GET_STRING_VOTE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_VOTE", false);
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06003FB5 RID: 16309 RVA: 0x0014A7B9 File Offset: 0x001489B9
		public static string GET_STRING_I_D
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_I_D", false);
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06003FB6 RID: 16310 RVA: 0x0014A7C6 File Offset: 0x001489C6
		public static string GET_STRING_SLOT_FIRST_REWARD
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SLOT_FIRST_REWARD", false);
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06003FB7 RID: 16311 RVA: 0x0014A7D3 File Offset: 0x001489D3
		public static string GET_STRING_KILL_COUNT_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_KILL_COUNT_ONE_PARAM", false);
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06003FB8 RID: 16312 RVA: 0x0014A7E0 File Offset: 0x001489E0
		public static string GET_STRING_ATTACK_COUNT_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ATTACK_COUNT_ONE_PARAM", false);
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06003FB9 RID: 16313 RVA: 0x0014A7ED File Offset: 0x001489ED
		public static string GET_STRING_REMAIN_COUNT_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REMAIN_COUNT_TWO_PARAM", false);
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06003FBA RID: 16314 RVA: 0x0014A7FA File Offset: 0x001489FA
		public static string GET_STRING_ITEM_COUNT_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ITEM_COUNT_ONE_PARAM", false);
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06003FBB RID: 16315 RVA: 0x0014A807 File Offset: 0x00148A07
		public static string GET_STRING_ERROR_SERVER_GAME_DATA
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ERROR_SERVER_GAME_DATA", false);
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06003FBC RID: 16316 RVA: 0x0014A814 File Offset: 0x00148A14
		public static string GET_STRING_ERROR_SERVER_GAME_DATA_AND_GO_LOBBY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ERROR_SERVER_GAME_DATA_AND_GO_LOBBY", false);
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06003FBD RID: 16317 RVA: 0x0014A821 File Offset: 0x00148A21
		public static string GET_STRING_ERROR_RECONNECT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ERROR_RECONNECT", false);
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06003FBE RID: 16318 RVA: 0x0014A82E File Offset: 0x00148A2E
		public static string GET_STRING_ERROR_DECONNECT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ERROR_DECONNECT", false);
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06003FBF RID: 16319 RVA: 0x0014A83B File Offset: 0x00148A3B
		public static string GET_STRING_ERROR_FAIL_CONNECT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ERROR_FAIL_CONNECT", false);
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06003FC0 RID: 16320 RVA: 0x0014A848 File Offset: 0x00148A48
		public static string GET_STRING_TRY_AGAIN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TRY_AGAIN", false);
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06003FC1 RID: 16321 RVA: 0x0014A855 File Offset: 0x00148A55
		public static string GET_STRING_FILTER_ALL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FILTER_ALL", false);
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06003FC2 RID: 16322 RVA: 0x0014A862 File Offset: 0x00148A62
		public static string GET_STRING_SORT_ENHANCE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SORT_ENHANCE", false);
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06003FC3 RID: 16323 RVA: 0x0014A86F File Offset: 0x00148A6F
		public static string GET_STRING_SORT_TIER
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SORT_TIER", false);
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06003FC4 RID: 16324 RVA: 0x0014A87C File Offset: 0x00148A7C
		public static string GET_STRING_SORT_RARITY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SORT_RARITY", false);
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06003FC5 RID: 16325 RVA: 0x0014A889 File Offset: 0x00148A89
		public static string GET_STRING_SORT_UID
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SORT_UID", false);
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06003FC6 RID: 16326 RVA: 0x0014A896 File Offset: 0x00148A96
		public static string GET_STRING_SORT_LEVEL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SORT_LEVEL", false);
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06003FC7 RID: 16327 RVA: 0x0014A8A3 File Offset: 0x00148AA3
		public static string GET_STRING_SORT_IDX
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SORT_IDX", false);
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06003FC8 RID: 16328 RVA: 0x0014A8B0 File Offset: 0x00148AB0
		public static string GET_STRING_SORT_ATTACK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SORT_ATTACK", false);
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06003FC9 RID: 16329 RVA: 0x0014A8BD File Offset: 0x00148ABD
		public static string GET_STRING_SORT_CRIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SORT_CRIT", false);
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06003FCA RID: 16330 RVA: 0x0014A8CA File Offset: 0x00148ACA
		public static string GET_STRING_SORT_DEFENSE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SORT_DEFENSE", false);
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06003FCB RID: 16331 RVA: 0x0014A8D7 File Offset: 0x00148AD7
		public static string GET_STRING_SORT_EVADE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SORT_EVADE", false);
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06003FCC RID: 16332 RVA: 0x0014A8E4 File Offset: 0x00148AE4
		public static string GET_STRING_SORT_HEALTH
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SORT_HEALTH", false);
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06003FCD RID: 16333 RVA: 0x0014A8F1 File Offset: 0x00148AF1
		public static string GET_STRING_SORT_HIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SORT_HIT", false);
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06003FCE RID: 16334 RVA: 0x0014A8FE File Offset: 0x00148AFE
		public static string GET_STRING_SORT_POPWER
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SORT_POWER", false);
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06003FCF RID: 16335 RVA: 0x0014A90B File Offset: 0x00148B0B
		public static string GET_STRING_SORT_COST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SORT_COST", false);
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06003FD0 RID: 16336 RVA: 0x0014A918 File Offset: 0x00148B18
		public static string GET_STRING_SORT_PLAYER_LEVEL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SORT_PLAYER_LEVEL", false);
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06003FD1 RID: 16337 RVA: 0x0014A925 File Offset: 0x00148B25
		public static string GET_STRING_SORT_LOGIN_TIME
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SORT_LOGIN_TIME", false);
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06003FD2 RID: 16338 RVA: 0x0014A932 File Offset: 0x00148B32
		public static string GET_STRING_SORT_ENHANCE_PROGRESS
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SORT_ENHANCE_PROGRESS", false);
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06003FD3 RID: 16339 RVA: 0x0014A93F File Offset: 0x00148B3F
		public static string GET_STRING_SORT_SCOUT_PROGRESS
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_PERSONNEL_SCOUT_PIECE_HAVE", false);
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06003FD4 RID: 16340 RVA: 0x0014A94C File Offset: 0x00148B4C
		public static string GET_STRING_SORT_INTERIOR_POINT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_POPUP_SORT_LIST_INTERIOR", false);
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06003FD5 RID: 16341 RVA: 0x0014A959 File Offset: 0x00148B59
		public static string GET_STRING_SORT_PLACE_TYPE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_POPUP_SORT_LIST_PLACE_TYPE", false);
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06003FD6 RID: 16342 RVA: 0x0014A966 File Offset: 0x00148B66
		public static string GET_STRING_SORT_LIMIT_BREAK_PROGRESS
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_BASE_MENU_LAB_TRANSCENDENCE_TEXT", false);
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06003FD7 RID: 16343 RVA: 0x0014A973 File Offset: 0x00148B73
		public static string GET_STRING_SORT_TRANSCENDENCE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SORT_TRANSCENDENCE", false);
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06003FD8 RID: 16344 RVA: 0x0014A980 File Offset: 0x00148B80
		public static string GET_STRING_SORT_LOYALTY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SORT_LOYALTY", false);
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06003FD9 RID: 16345 RVA: 0x0014A98D File Offset: 0x00148B8D
		public static string GET_STRING_SORT_SQUAD_DUNGEON
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SORT_DUNGEON_SQUAD", false);
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06003FDA RID: 16346 RVA: 0x0014A99A File Offset: 0x00148B9A
		public static string GET_STRING_SORT_SQUAD_WARFARE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SORT_WARFARE_SQUAD", false);
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06003FDB RID: 16347 RVA: 0x0014A9A7 File Offset: 0x00148BA7
		public static string GET_STRING_SORT_SQUAD_PVP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SORT_GAUNTLET", false);
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06003FDC RID: 16348 RVA: 0x0014A9B4 File Offset: 0x00148BB4
		public static string GET_STRING_SORT_FAVORITE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SORT_FAVOURITE", false);
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06003FDD RID: 16349 RVA: 0x0014A9C1 File Offset: 0x00148BC1
		public static string GET_STRING_FAVORITES_NO_ENTRY
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FAVORITES_NO_ENTRY", false);
			}
		}

		// Token: 0x06003FDE RID: 16350 RVA: 0x0014A9CE File Offset: 0x00148BCE
		public static string GetEnhanceProgressString(float progressPercent)
		{
			if (progressPercent >= 1f)
			{
				return "MAX";
			}
			return string.Format("{0}%", Mathf.FloorToInt(progressPercent * 100f));
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06003FDF RID: 16351 RVA: 0x0014A9F9 File Offset: 0x00148BF9
		public static string GET_STRING_FAIL_NET
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FAIL_NET", false);
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06003FE0 RID: 16352 RVA: 0x0014AA06 File Offset: 0x00148C06
		public static string GET_STRING_PVP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PVP", false);
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06003FE1 RID: 16353 RVA: 0x0014AA13 File Offset: 0x00148C13
		public static string GET_STRING_SKILL_COOLTIME_INC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SKILL_COOLTIME_INC", false);
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06003FE2 RID: 16354 RVA: 0x0014AA20 File Offset: 0x00148C20
		public static string GET_STRING_SKILL_COOLTIME_DEC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SKILL_COOLTIME_DEC", false);
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06003FE3 RID: 16355 RVA: 0x0014AA2D File Offset: 0x00148C2D
		public static string GET_STRING_UPSIDE_MENU_WAIT_ITEM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_UPSIDE_MENU_WAIT_ITEM", false);
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06003FE4 RID: 16356 RVA: 0x0014AA3A File Offset: 0x00148C3A
		public static string GET_STRING_UI_LOADING_ERROR
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_UI_LOADING_ERROR", false);
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06003FE5 RID: 16357 RVA: 0x0014AA47 File Offset: 0x00148C47
		public static string GET_STRING_BUSINESS_MOTO_INPUT_PLEASE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_BUSINESS_MOTO_INPUT_PLEASE", false);
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06003FE6 RID: 16358 RVA: 0x0014AA54 File Offset: 0x00148C54
		public static string GET_STRING_REGISTERTIME_DATE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REGISTERTIME_DATE", false);
			}
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06003FE7 RID: 16359 RVA: 0x0014AA61 File Offset: 0x00148C61
		public static string GET_STRING_NO_RANK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NO_RANK", false);
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06003FE8 RID: 16360 RVA: 0x0014AA6E File Offset: 0x00148C6E
		public static string GET_STRING_UNIT_BAN_COST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_UNIT_BAN_COST", false);
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06003FE9 RID: 16361 RVA: 0x0014AA7B File Offset: 0x00148C7B
		public static string GET_STRING_UNIT_UP_COST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_UNIT_UP_COST", false);
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06003FEA RID: 16362 RVA: 0x0014AA88 File Offset: 0x00148C88
		public static string GET_STRING_EMBLEM_EQUIPPED_EMBLEM_UNEQUIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EMBLEM_EQUIPPED_EMBLEM_UNEQUIP", false);
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06003FEB RID: 16363 RVA: 0x0014AA95 File Offset: 0x00148C95
		public static string GET_STRING_EMBLEM_EQUIPPED_EMBLEM_UNEQUIP_CONFIRM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EMBLEM_EQUIPPED_EMBLEM_UNEQUIP_CONFIRM", false);
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06003FEC RID: 16364 RVA: 0x0014AAA2 File Offset: 0x00148CA2
		public static string GET_STRING_EMBLEM_EQUIPPED_EMBLEM_CHANGE_CONFIRM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EMBLEM_EQUIPPED_EMBLEM_CHANGE_CONFIRM", false);
			}
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06003FED RID: 16365 RVA: 0x0014AAAF File Offset: 0x00148CAF
		public static string GET_STRING_EMBLEM_EQUIP_CONFIRM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EMBLEM_EQUIP_CONFIRM", false);
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06003FEE RID: 16366 RVA: 0x0014AABC File Offset: 0x00148CBC
		public static string GET_STRING_EMOTICON_ENEMY_GAME_OUT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EMOTICON_ENEMY_GAME_OUT", false);
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06003FEF RID: 16367 RVA: 0x0014AAC9 File Offset: 0x00148CC9
		public static string GET_STRING_CONTRACT_RESULT_NONE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_RESULT_NONE", false);
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06003FF0 RID: 16368 RVA: 0x0014AAD6 File Offset: 0x00148CD6
		public static string GET_STRING_CONTRACT_PROGRESS_NONE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_PROGRESS_NONE", false);
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06003FF1 RID: 16369 RVA: 0x0014AAE3 File Offset: 0x00148CE3
		public static string GET_STRING_CONTRACT_NOT_ENOUGH_QUICK_ITEM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_NOT_ENOUGH_QUICK_ITEM", false);
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06003FF2 RID: 16370 RVA: 0x0014AAF0 File Offset: 0x00148CF0
		public static string GET_STRING_CONTRACT_CHARGE_UNIT_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_CHARGE_UNIT_TEXT", false);
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06003FF3 RID: 16371 RVA: 0x0014AAFD File Offset: 0x00148CFD
		public static string GET_STRING_CONTRACT_CHARGE_SHIP_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_CHARGE_SHIP_TEXT", false);
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06003FF4 RID: 16372 RVA: 0x0014AB0A File Offset: 0x00148D0A
		public static string GET_STRING_CONTRACT_MILEAGE_POINT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_MILEAGE_POINT", false);
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06003FF5 RID: 16373 RVA: 0x0014AB17 File Offset: 0x00148D17
		public static string GET_STRING_CONTRACT_MILEAGE_EVENT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_MILEAGE_EVENT", false);
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06003FF6 RID: 16374 RVA: 0x0014AB24 File Offset: 0x00148D24
		public static string GET_STRING_CONTRACT_USE_ALL_QUICK_ITEM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_USE_ALL_QUICK_ITEM", false);
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06003FF7 RID: 16375 RVA: 0x0014AB31 File Offset: 0x00148D31
		public static string GET_STRING_CONTRACT_USE_ALL_QUICK_ITEM_DISCRIPTION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_USE_ALL_QUICK_ITEM_DISCRIPTION", false);
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06003FF8 RID: 16376 RVA: 0x0014AB3E File Offset: 0x00148D3E
		public static string GET_STRING_CONTRACT_NOT_AVAILABLE_TIME
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_NOT_AVAILABLE_TIME", false);
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06003FF9 RID: 16377 RVA: 0x0014AB4B File Offset: 0x00148D4B
		public static string GET_STRING_CONTRACT_HAVE_COUNT_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_HAVE_COUNT_ONE_PARAM", false);
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06003FFA RID: 16378 RVA: 0x0014AB58 File Offset: 0x00148D58
		public static string GET_STRING_CONTRACT_INSTANT_UNIT_CONTRACT_USE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_INSTANT_UNIT_CONTRACT_USE", false);
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06003FFB RID: 16379 RVA: 0x0014AB65 File Offset: 0x00148D65
		public static string GET_STRING_CONTRACT_UNIT_EMERGENCY_COUPON_USE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_UNIT_EMERGENCY_COUPON_USE", false);
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06003FFC RID: 16380 RVA: 0x0014AB72 File Offset: 0x00148D72
		public static string GET_STRING_CONTRACT_INSTANT_SHIP_CONTRACT_USE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_INSTANT_SHIP_CONTRACT_USE", false);
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06003FFD RID: 16381 RVA: 0x0014AB7F File Offset: 0x00148D7F
		public static string GET_STRING_CONTRACT_SHIP_NUMBER
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_SHIP_NUMBER", false);
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06003FFE RID: 16382 RVA: 0x0014AB8C File Offset: 0x00148D8C
		public static string GET_STRING_CONTRACT_SHIP_EMERGENCY_COUPON_USE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_SHIP_EMERGENCY_COUPON_USE", false);
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06003FFF RID: 16383 RVA: 0x0014AB99 File Offset: 0x00148D99
		public static string GET_STRING_CONTRACT_SHIP_EMERGENCY_CONTRACT_COUPON_USE_REQ
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_SHIP_EMERGENCY_CONTRACT_COUPON_USE_REQ", false);
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06004000 RID: 16384 RVA: 0x0014ABA6 File Offset: 0x00148DA6
		public static string GET_STRING_CONTRACT_SHIP_NEW_CONTRACT_SLOT_OPEN_REQ
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_SHIP_NEW_CONTRACT_SLOT_OPEN_REQ", false);
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06004001 RID: 16385 RVA: 0x0014ABB3 File Offset: 0x00148DB3
		public static string GET_STRING_CONTRACT_SHIP_COMPLETE_CONTRACT_EXIST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_SHIP_COMPLETE_CONTRACT_EXIST", false);
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06004002 RID: 16386 RVA: 0x0014ABC0 File Offset: 0x00148DC0
		public static string GET_STRING_CONTRACT_SHIP_ON_GOING_CONTRACT_NUMBER_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_SHIP_ON_GOING_CONTRACT_NUMBER_ONE_PARAM", false);
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06004003 RID: 16387 RVA: 0x0014ABCD File Offset: 0x00148DCD
		public static string GET_STRING_CONTRACT_SHIP_ON_GOING_CONTRACT_NO_EXIST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_SHIP_ON_GOING_CONTRACT_NO_EXIST", false);
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06004004 RID: 16388 RVA: 0x0014ABDA File Offset: 0x00148DDA
		public static string GET_STRING_CONTRACT_UNIT_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_UNIT_FAIL", false);
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06004005 RID: 16389 RVA: 0x0014ABE7 File Offset: 0x00148DE7
		public static string GET_STRING_CONTRACT_SHIP_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_SHIP_FAIL", false);
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06004006 RID: 16390 RVA: 0x0014ABF4 File Offset: 0x00148DF4
		public static string GET_STRING_CONTRACT_FAIL_PROGRESS
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_FAIL_PROGRESS", false);
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06004007 RID: 16391 RVA: 0x0014AC01 File Offset: 0x00148E01
		public static string GET_STRING_CONTRACT_FAIL_COMPLETE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_FAIL_COMPLETE", false);
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06004008 RID: 16392 RVA: 0x0014AC0E File Offset: 0x00148E0E
		public static string GET_STRING_CONTRACT_FAIL_EMPTY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_FAIL_EMPTY", false);
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06004009 RID: 16393 RVA: 0x0014AC1B File Offset: 0x00148E1B
		public static string GET_STRING_CONTRACT_FAIL_QUIICK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_FAIL_QUIICK", false);
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x0600400A RID: 16394 RVA: 0x0014AC28 File Offset: 0x00148E28
		public static string GET_STRING_CONTRACT_FAIL_QUIICK_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_FAIL_QUIICK_DESC", false);
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x0600400B RID: 16395 RVA: 0x0014AC35 File Offset: 0x00148E35
		public static string GET_STRING_CONTRACT_SLOT_UNLOCK_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_SLOT_UNLOCK_FAIL", false);
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x0600400C RID: 16396 RVA: 0x0014AC42 File Offset: 0x00148E42
		public static string GET_STRING_CONTRACT_SLOT_UNLOCK_FAIL_MAX
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_SLOT_UNLOCK_FAIL_MAX", false);
			}
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x0600400D RID: 16397 RVA: 0x0014AC4F File Offset: 0x00148E4F
		public static string GET_STRING_CONTRACT_SLOT_UNLOCK_FAIL_COST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_SLOT_UNLOCK_FAIL_COST", false);
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x0600400E RID: 16398 RVA: 0x0014AC5C File Offset: 0x00148E5C
		public static string GET_STRING_CONTRACT_COUNT_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_COUNT_ONE_PARAM", false);
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x0600400F RID: 16399 RVA: 0x0014AC69 File Offset: 0x00148E69
		public static string GET_STRING_CONTRACT_FREE_TRY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_FREE_TRY_DESC", false);
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06004010 RID: 16400 RVA: 0x0014AC76 File Offset: 0x00148E76
		public static string GET_STRING_CONTRACT_FREE_02_TRY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_MULTI_FREE_TRY_DESC", false);
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06004011 RID: 16401 RVA: 0x0014AC83 File Offset: 0x00148E83
		public static string GET_STRING_CONTRACT_FREE_02_TRY_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_FREE_MULTI_TRY_POPUP_DESC", false);
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06004012 RID: 16402 RVA: 0x0014AC90 File Offset: 0x00148E90
		public static string GET_STRING_CONTRACT_CONFIRMATION_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_CONFIRMATION_DESC_01", false);
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06004013 RID: 16403 RVA: 0x0014AC9D File Offset: 0x00148E9D
		public static string GET_STRING_CONTRACT_CONFIRMATION_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_CONFIRMATION_DESC", false);
			}
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06004014 RID: 16404 RVA: 0x0014ACAA File Offset: 0x00148EAA
		public static string GET_STRING_CONTRACT_CONFIRMATION_ONE_LEFT_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONTRACT_RECRUIT_CONTRACT_BONUS_SPECIAL_TEXT", false);
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06004015 RID: 16405 RVA: 0x0014ACB7 File Offset: 0x00148EB7
		public static string GET_STRING_CONTRACT_CONFIRM_TOOLTIP_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONTRACT_CONFIRM_TOOLTIP_TITLE", false);
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06004016 RID: 16406 RVA: 0x0014ACC4 File Offset: 0x00148EC4
		public static string GET_STRING_CONTRACT_CONFIRM_TOOLTIP_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONTRACT_CONFIRM_TOOLTIP_DESC", false);
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06004017 RID: 16407 RVA: 0x0014ACD1 File Offset: 0x00148ED1
		public static string GET_STRING_CONTRACT_CONFIRM_BOTTOM_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_CONFIRM_BOTTOM_DESC", false);
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06004018 RID: 16408 RVA: 0x0014ACDE File Offset: 0x00148EDE
		public static string GET_STRING_CONTRACT_CONFIRM_BOTTOM_DESC_OPERATOR
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONTRACT_CONFIRM_BOTTOM_DESC_OPR", false);
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06004019 RID: 16409 RVA: 0x0014ACEB File Offset: 0x00148EEB
		public static string GET_STRING_CONTRACT_CONFIRMATION_POPUP_TITLE_01
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_CONFIRMATION_POPUP_TITLE_01", false);
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x0600401A RID: 16410 RVA: 0x0014ACF8 File Offset: 0x00148EF8
		public static string GET_STRING_CONTRACT_CONFIRMATION_POPUP_TITLE_01_OPERATOR
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONTRACT_CONFIRM_BOTTOM_TITLE_OPR", false);
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x0600401B RID: 16411 RVA: 0x0014AD05 File Offset: 0x00148F05
		public static string GET_STRING_CONTRACT_POPUP_RATE_EVENT_TIME_OVER_01
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_POPUP_RATE_EVENT_TIME_OVER_01", false);
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x0600401C RID: 16412 RVA: 0x0014AD12 File Offset: 0x00148F12
		public static string GET_STRING_CONTRACT_POPUP_RATE_DETAIL_PERCENT_01
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_POPUP_RATE_DETAIL_PERCENT_01", false);
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x0600401D RID: 16413 RVA: 0x0014AD1F File Offset: 0x00148F1F
		public static string GET_STRING_CONTRACT_MISC_NOT_ENOUGH_01
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_MISC_NOT_ENOUGH_01", false);
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x0600401E RID: 16414 RVA: 0x0014AD2C File Offset: 0x00148F2C
		public static string GET_STRING_CONTRACT_REQ_DESC_03
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_REQ_DESC_03", false);
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x0600401F RID: 16415 RVA: 0x0014AD39 File Offset: 0x00148F39
		public static string GET_STRING_SELECTABLE_CONTRACT_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SELECTABLE_CONTRACT_DESC", false);
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06004020 RID: 16416 RVA: 0x0014AD46 File Offset: 0x00148F46
		public static string GET_STRING_SELECTABLE_CONTRACT_UNIT_POOL_CHANGE_CONFIRM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SELECTABLE_CONTRACT_UNIT_POOL_CHANGE_CONFIRM", false);
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06004021 RID: 16417 RVA: 0x0014AD53 File Offset: 0x00148F53
		public static string GET_STRING_SELECTABLE_CONTRACT_CONFIRM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SELECTABLE_CONTRACT_CONFIRM", false);
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06004022 RID: 16418 RVA: 0x0014AD60 File Offset: 0x00148F60
		public static string GET_STRING_SELECTABLE_CONTRACT_NOT_ENOUGH
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SELECTABLE_CONTRACT_NOT_ENOUGH", false);
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06004023 RID: 16419 RVA: 0x0014AD6D File Offset: 0x00148F6D
		public static string GET_STRING_SELECTABLE_CONTRACT_USE_ITEM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SELECTABLE_CONTRACT_USE_ITEM", false);
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06004024 RID: 16420 RVA: 0x0014AD7A File Offset: 0x00148F7A
		public static string GET_STRING_CONTRACT_FREE_EVENT_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_FREE_EVENT_DESC", false);
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06004025 RID: 16421 RVA: 0x0014AD87 File Offset: 0x00148F87
		public static string GET_STRING_CONTRACT_REMAIN_COUNT_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_COUNT_CLOSE_DESC", false);
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06004026 RID: 16422 RVA: 0x0014AD94 File Offset: 0x00148F94
		public static string GET_STRING_CONTRACT_CLOSE_TOOLTIP_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONTRACT_COUNT_CLOSE_TOOLTIP_TITLE", false);
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06004027 RID: 16423 RVA: 0x0014ADA1 File Offset: 0x00148FA1
		public static string GET_STRING_CONTRACT_CLOSE_TOOLTIP_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONTRACT_COUNT_CLOSE_TOOLTIP_DESC", false);
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06004028 RID: 16424 RVA: 0x0014ADAE File Offset: 0x00148FAE
		public static string GET_STRING_CONTRACT_FREE_BUTTON_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONTRACT_FREE_BUTTON_DESC", false);
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06004029 RID: 16425 RVA: 0x0014ADBB File Offset: 0x00148FBB
		public static string GET_STRING_CONTRACT_FREE_BUTTON_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_FREE_BUTTON_DESC_01", false);
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x0600402A RID: 16426 RVA: 0x0014ADC8 File Offset: 0x00148FC8
		public static string GET_STRING_CONTRACT_BUTTON_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_BUTTON_DESC", false);
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x0600402B RID: 16427 RVA: 0x0014ADD5 File Offset: 0x00148FD5
		public static string GET_STRING_CONTRACT_NOT_ENOUGH_FREE_TRY
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONTRACT_NOT_ENOUGH_FREE_TRY", false);
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x0600402C RID: 16428 RVA: 0x0014ADE2 File Offset: 0x00148FE2
		public static string GET_STRING_CONTRACT_NOT_ENOUGH_ITEM_TRY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_NOT_ENOUGH_ITEM_TRY", false);
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x0600402D RID: 16429 RVA: 0x0014ADEF File Offset: 0x00148FEF
		public static string GET_STRING_CONTRACT_LIMIT_TIME
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_LIMIT_TIME", false);
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x0600402E RID: 16430 RVA: 0x0014ADFC File Offset: 0x00148FFC
		public static string GET_STRING_CONTRACT_LIMIT_TIME_ADD_DAY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_LIMIT_TIME_ADD_DAY", false);
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x0600402F RID: 16431 RVA: 0x0014AE09 File Offset: 0x00149009
		public static string GET_STRING_CONTRACT_LIMIT_TIME_DAY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_LIMIT_TIME_DAY", false);
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06004030 RID: 16432 RVA: 0x0014AE16 File Offset: 0x00149016
		public static string GET_STRING_CONTRACT_LIMIT_TIME_HOUR
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_LIMIT_TIME_HOUR", false);
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06004031 RID: 16433 RVA: 0x0014AE23 File Offset: 0x00149023
		public static string GET_STRING_CONTRACT_LIMIT_TIME_SECOND
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_LIMIT_TIME_SECOND", false);
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06004032 RID: 16434 RVA: 0x0014AE30 File Offset: 0x00149030
		public static string GET_STRING_CONTRACT_END_RECRUIT_TIME
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_END_RECRUIT_TIME", false);
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06004033 RID: 16435 RVA: 0x0014AE3D File Offset: 0x0014903D
		public static string GET_STRING_CONTRACT_SELECTION_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONTRACT_SELECTION_TITLE", false);
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06004034 RID: 16436 RVA: 0x0014AE4A File Offset: 0x0014904A
		public static string GET_STRING_CONTRACT_DAILY_RETRY_ENOUGH_TIMER_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_DAILY_RETRY_ENOUGH_TIMER_DESC", false);
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06004035 RID: 16437 RVA: 0x0014AE57 File Offset: 0x00149057
		public static string GET_STRING_CONTRACT_ALL_COMPLETE_CONFIRM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_ALL_COMPLETE_CONFIRM", false);
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06004036 RID: 16438 RVA: 0x0014AE64 File Offset: 0x00149064
		public static string GET_STRING_CONTRACT_POINT_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_RESULT_CONTRACT_POINT", false);
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06004037 RID: 16439 RVA: 0x0014AE71 File Offset: 0x00149071
		public static string GET_STRING_CONTRACT_FREE_TRY_EXIT_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONTRACT_FREE_TRY_EXIT_DESC", false);
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06004038 RID: 16440 RVA: 0x0014AE7E File Offset: 0x0014907E
		public static string GET_STRING_UNIT_SELECT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_UNIT_SELECT", false);
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06004039 RID: 16441 RVA: 0x0014AE8B File Offset: 0x0014908B
		public static string GET_STRING_UNIT_SELECT_UNIT_NO_EXIST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_UNIT_SELECT_UNIT_NO_EXIST", false);
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x0600403A RID: 16442 RVA: 0x0014AE98 File Offset: 0x00149098
		public static string GET_STRING_UNIT_SELECT_SHIP_NO_EXIST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_UNIT_SELECT_SHIP_NO_EXIST", false);
			}
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x0600403B RID: 16443 RVA: 0x0014AEA5 File Offset: 0x001490A5
		public static string GET_STRING_UNIT_SELECT_TROPHY_NO_EXIST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_UNIT_SELECT_TROPHY_NO_EXIST", false);
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x0600403C RID: 16444 RVA: 0x0014AEB2 File Offset: 0x001490B2
		public static string GET_STRING_UNIT_SELECT_OPERATOR_NO_EXIST
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_UNIT_SELECT_OPERATOR_NO_EXIST", false);
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x0600403D RID: 16445 RVA: 0x0014AEBF File Offset: 0x001490BF
		public static string GET_STRING_UNIT_SELECT_HAVE_COUNT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CHOICE_ALREADY_HAVE", false);
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x0600403E RID: 16446 RVA: 0x0014AECC File Offset: 0x001490CC
		public static string GET_STRING_UNIT_SELECT_UNIT_COUNT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_UNIT_SELECT_UNIT_COUNT", false);
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x0600403F RID: 16447 RVA: 0x0014AED9 File Offset: 0x001490D9
		public static string GET_STRING_UNIT_SELECT_SHIP_COUNT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_UNIT_SELECT_SHIP_COUNT", false);
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06004040 RID: 16448 RVA: 0x0014AEE6 File Offset: 0x001490E6
		public static string GET_STRING_UNIT_SELECT_MAIN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_UNIT_SELECT_MAIN", false);
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06004041 RID: 16449 RVA: 0x0014AEF3 File Offset: 0x001490F3
		public static string GET_STRING_UNIT_SELECT_SUB_MAIN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_UNIT_SELECT_SUB_MAIN", false);
			}
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06004042 RID: 16450 RVA: 0x0014AF00 File Offset: 0x00149100
		public static string GET_STRING_UNIT_SELECT_IMPOSSIBLE_DUPLICATE_ORGANIZE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_UNIT_SELECT_IMPOSSIBLE_DUPLICATE_ORGANIZE", false);
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06004043 RID: 16451 RVA: 0x0014AF0D File Offset: 0x0014910D
		public static string GET_STRING_COLLECTION_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_COLLECTION_EMPLOYEE", false);
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06004044 RID: 16452 RVA: 0x0014AF1A File Offset: 0x0014911A
		public static string GET_STRING_COLLECTION_SHIP
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_COLLECTION_SHIP", false);
			}
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06004045 RID: 16453 RVA: 0x0014AF27 File Offset: 0x00149127
		public static string GET_STRING_COLLECTION_ALLOWED_RANGE_VOTE_COMPLETE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_COLLECTION_ALLOWED_RANGE_VOTE_COMPLETE", false);
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06004046 RID: 16454 RVA: 0x0014AF34 File Offset: 0x00149134
		public static string GET_STRING_COLLECTION_TRAINING_MODE_CHANGE_REQ
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_COLLECTION_TRAINING_MODE_CHANGE_REQ", false);
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06004047 RID: 16455 RVA: 0x0014AF41 File Offset: 0x00149141
		public static string GET_STRING_COLLECTION_STORY_SUB_TITLE_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_COLLECTION_STORY_SUB_TITLE_ONE_PARAM", false);
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06004048 RID: 16456 RVA: 0x0014AF4E File Offset: 0x0014914E
		public static string GET_STRING_COLLECTION_STORY_EXTRA_TITLE_ONE_PARAM
		{
			get
			{
				return "Extra. {0}";
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06004049 RID: 16457 RVA: 0x0014AF55 File Offset: 0x00149155
		public static string GET_STRING_COLLECTION_STORY_ETC_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FILTER_MOLD_TYPE_ETC", false);
			}
		}

		// Token: 0x0600404A RID: 16458 RVA: 0x0014AF64 File Offset: 0x00149164
		public static string GetCollectionRateStrByType(NKCUICollection.CollectionType type)
		{
			string @string = NKCStringTable.GetString("SI_DP_COLLECTION_RATE_STRING_BY_TYPE_CT_TEAM_UP", false);
			switch (type)
			{
			case NKCUICollection.CollectionType.CT_TEAM_UP:
				@string = NKCStringTable.GetString("SI_DP_COLLECTION_RATE_STRING_BY_TYPE_CT_TEAM_UP", false);
				break;
			case NKCUICollection.CollectionType.CT_UNIT:
				@string = NKCStringTable.GetString("SI_DP_COLLECTION_RATE_STRING_BY_TYPE_CT_UNIT", false);
				break;
			case NKCUICollection.CollectionType.CT_SHIP:
				@string = NKCStringTable.GetString("SI_DP_COLLECTION_RATE_STRING_BY_TYPE_CT_SHIP", false);
				break;
			case NKCUICollection.CollectionType.CT_OPERATOR:
				@string = NKCStringTable.GetString("SI_DP_COLLECTION_RATE_STRING_BY_TYPE_CT_OPERATOR", false);
				break;
			case NKCUICollection.CollectionType.CT_ILLUST:
				@string = NKCStringTable.GetString("SI_DP_COLLECTION_RATE_STRING_BY_TYPE_CT_ILLUST", false);
				break;
			case NKCUICollection.CollectionType.CT_STORY:
				@string = NKCStringTable.GetString("SI_DP_COLLECTION_RATE_STRING_BY_TYPE_CT_STORY", false);
				break;
			}
			return @string;
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x0600404B RID: 16459 RVA: 0x0014AFF0 File Offset: 0x001491F0
		public static string GET_STRING_REVIEW_DELETE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REVIEW_DELETE", false);
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x0600404C RID: 16460 RVA: 0x0014AFFD File Offset: 0x001491FD
		public static string GET_STRING_POPUP_UNIT_REVIEW_DELETE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_POPUP_UNIT_REVIEW_DELETE", false);
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x0600404D RID: 16461 RVA: 0x0014B00A File Offset: 0x0014920A
		public static string GET_STRING_POPUP_UNIT_REVIEW_SCORE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_POPUP_UNIT_REVIEW_SCORE", false);
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x0600404E RID: 16462 RVA: 0x0014B017 File Offset: 0x00149217
		public static string GET_STRING_UNIT_REVIEW_SCORE_VOTE_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_UNIT_REVIEW_SCORE_VOTE_ONE_PARAM", false);
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x0600404F RID: 16463 RVA: 0x0014B024 File Offset: 0x00149224
		public static string GET_STRING_UNIT_REVIEW_SCORE_VOTE_PLUS_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_UNIT_REVIEW_SCORE_VOTE_PLUS_ONE_PARAM", false);
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06004050 RID: 16464 RVA: 0x0014B031 File Offset: 0x00149231
		public static string GET_STRING_UNIT_REVIEW
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_UNIT_REVIEW", false);
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06004051 RID: 16465 RVA: 0x0014B03E File Offset: 0x0014923E
		public static string GET_STRING_REVIEW_DELETE_AND_WRITE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REVIEW_DELETE_AND_WRITE", false);
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06004052 RID: 16466 RVA: 0x0014B04B File Offset: 0x0014924B
		public static string GET_STRING_REVIEW_IS_ALREADY_DELETE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REVIEW_IS_ALREADY_DELETE", false);
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06004053 RID: 16467 RVA: 0x0014B058 File Offset: 0x00149258
		public static string GET_STRING_COLLECTION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_COLLECTION", false);
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06004054 RID: 16468 RVA: 0x0014B065 File Offset: 0x00149265
		public static string GET_STRING_COLLECTION_SKIN_SLOT_NAME
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_COLLECTION_SKIN_SLOT_NAME", false);
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06004055 RID: 16469 RVA: 0x0014B072 File Offset: 0x00149272
		public static string GET_STRING_SKIN_STORY_REPLAY_CONFIRM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SKIN_STORY_REPLAY_CONFIRM", false);
			}
		}

		// Token: 0x06004056 RID: 16470 RVA: 0x0014B080 File Offset: 0x00149280
		public static string GetCollectionStoryCategory(NKCCollectionManager.COLLECTION_STORY_CATEGORY category)
		{
			switch (category)
			{
			case NKCCollectionManager.COLLECTION_STORY_CATEGORY.MAINSTREAM:
				return NKCStringTable.GetString("SI_DP_EPISODE_CATEGORY_EC_MAINSTREAM", false);
			case NKCCollectionManager.COLLECTION_STORY_CATEGORY.SIDESTORY:
				return NKCStringTable.GetString("SI_DP_EPISODE_CATEGORY_EC_SIDESTORY", false);
			case NKCCollectionManager.COLLECTION_STORY_CATEGORY.EVENT:
				return NKCUtilString.GET_STRING_EPISODE_CATEGORY_EC_EVENT;
			case NKCCollectionManager.COLLECTION_STORY_CATEGORY.WORLDMAP:
				return NKCUtilString.GET_STRING_WORLDMAP;
			case NKCCollectionManager.COLLECTION_STORY_CATEGORY.ETC:
				return NKCUtilString.GET_STRING_COLLECTION_STORY_ETC_TITLE;
			default:
				return NKCStringTable.GetString("SI_DP_EPISODE_CATEGORY_EC_DEFAULT", false);
			}
		}

		// Token: 0x06004057 RID: 16471 RVA: 0x0014B0DE File Offset: 0x001492DE
		public static string GetFinalMailContents(string originContents)
		{
			if (NKCPublisherModule.Localization.IsPossibleJson(originContents))
			{
				return NKCPublisherModule.Localization.GetTranslationIfJson(originContents);
			}
			return NKCServerStringFormatter.TranslateServerFormattedString(originContents);
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06004058 RID: 16472 RVA: 0x0014B0FF File Offset: 0x001492FF
		public static string GET_STRING_MAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MAIL", false);
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06004059 RID: 16473 RVA: 0x0014B10C File Offset: 0x0014930C
		public static string GET_STRING_MAIL_HAVE_COUNT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MAIL_HAVE_COUNT", false);
			}
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x0600405A RID: 16474 RVA: 0x0014B119 File Offset: 0x00149319
		public static string GET_STRING_INVEN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INVEN", false);
			}
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x0600405B RID: 16475 RVA: 0x0014B126 File Offset: 0x00149326
		public static string GET_STRING_INVEN_EQUIP_SELECT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INVEN_EQUIP_SELECT", false);
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x0600405C RID: 16476 RVA: 0x0014B133 File Offset: 0x00149333
		public static string GET_STRING_INVEN_EQUIP_CHANGE_WARNING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INVEN_EQUIP_CHANGE_WARNING", false);
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x0600405D RID: 16477 RVA: 0x0014B140 File Offset: 0x00149340
		public static string GET_STRING_INVEN_MISC_NO_EXIST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INVEN_MISC_NO_EXIST", false);
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x0600405E RID: 16478 RVA: 0x0014B14D File Offset: 0x0014934D
		public static string GET_STRING_INVEN_EQUIP_NO_EXIST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INVEN_EQUIP_NO_EXIST", false);
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x0600405F RID: 16479 RVA: 0x0014B15A File Offset: 0x0014935A
		public static string GET_STRING_INVEN_GIVE_RANDOM_OPTION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INVEN_GIVE_RANDOM_OPTION", false);
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06004060 RID: 16480 RVA: 0x0014B167 File Offset: 0x00149367
		public static string GET_STRING_INVEN_RANDOM_OPTION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_STAT_SHORT_NAME_FOR_INVEN_EQUIP_RANDOM_OPTION", false);
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06004061 RID: 16481 RVA: 0x0014B174 File Offset: 0x00149374
		public static string GET_STRING_HAVE_COUNT_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_HAVE_COUNT_ONE_PARAM", false);
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06004062 RID: 16482 RVA: 0x0014B181 File Offset: 0x00149381
		public static string GET_STRING_FACTORY_UPGRADE_OPTION_SUCCESSION
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FACTORY_UPGRADE_OPTION_SUCCESSION", false);
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06004063 RID: 16483 RVA: 0x0014B18E File Offset: 0x0014938E
		public static string GET_STRING_FILTER_EQUIP_OPTION_SEARCH
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FILTER_EQUIP_OPTION_SEARCH", false);
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06004064 RID: 16484 RVA: 0x0014B19B File Offset: 0x0014939B
		public static string GET_STRING_MENU_UNEQUIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MENU_UNEQUIP", false);
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06004065 RID: 16485 RVA: 0x0014B1A8 File Offset: 0x001493A8
		public static string GET_STRING_MENU_EQUIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MENU_EQUIP", false);
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06004066 RID: 16486 RVA: 0x0014B1B5 File Offset: 0x001493B5
		public static string GET_STRING_INVEN_THERE_IS_NO_UNIT_TO_EQUIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INVEN_THERE_IS_NO_UNIT_TO_EQUIP", false);
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06004067 RID: 16487 RVA: 0x0014B1C2 File Offset: 0x001493C2
		public static string GET_STRING_TOOLTIP_QUANTITY_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOOLTIP_QUANTITY_ONE_PARAM", false);
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06004068 RID: 16488 RVA: 0x0014B1CF File Offset: 0x001493CF
		public static string GET_STRING_ITEM_LACK_DESC_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ITEM_LACK_DESC_ONE_PARAM", false);
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06004069 RID: 16489 RVA: 0x0014B1DC File Offset: 0x001493DC
		public static string GET_STRING_USE_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_USE_ONE_PARAM", false);
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x0600406A RID: 16490 RVA: 0x0014B1E9 File Offset: 0x001493E9
		public static string GET_STRING_USE_PACKAGE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_USE_PACKAGE", false);
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x0600406B RID: 16491 RVA: 0x0014B1F6 File Offset: 0x001493F6
		public static string GET_STRING_USE_CHOICE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_USE_CHOICE", false);
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x0600406C RID: 16492 RVA: 0x0014B203 File Offset: 0x00149403
		public static string GET_STRING_CHOICE_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CHOICE_TITLE_UNIT", false);
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x0600406D RID: 16493 RVA: 0x0014B210 File Offset: 0x00149410
		public static string GET_STRING_CHOICE_SHIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CHOICE_TITLE_SHIP", false);
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x0600406E RID: 16494 RVA: 0x0014B21D File Offset: 0x0014941D
		public static string GET_STRING_CHOICE_EQUIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CHOICE_TITLE_EQUIP", false);
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x0600406F RID: 16495 RVA: 0x0014B22A File Offset: 0x0014942A
		public static string GET_STRING_CHOICE_MISC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CHOICE_TITLE_MISC", false);
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06004070 RID: 16496 RVA: 0x0014B237 File Offset: 0x00149437
		public static string GET_STRING_CHOICE_RECHECK_DECISION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CHOICE_RECHECK_DECISION", false);
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06004071 RID: 16497 RVA: 0x0014B244 File Offset: 0x00149444
		public static string GET_STRING_CHOICE_UNIT_CONFIRM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CHOICE_UNIT_CONFIRM", false);
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06004072 RID: 16498 RVA: 0x0014B251 File Offset: 0x00149451
		public static string GET_STRING_CHOICE_SHIP_CONFIRM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CHOICE_SHIP_CONFIRM", false);
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06004073 RID: 16499 RVA: 0x0014B25E File Offset: 0x0014945E
		public static string GET_STRING_CHOICE_EQUIP_CONFIRM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CHOICE_EQUIP_CONFIRM", false);
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06004074 RID: 16500 RVA: 0x0014B26B File Offset: 0x0014946B
		public static string GET_STRING_CHOICE_MISC_CONFIRM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CHOICE_MISC_CONFIRM", false);
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06004075 RID: 16501 RVA: 0x0014B278 File Offset: 0x00149478
		public static string GET_STRING_CHOICE_SKIN_CONFIRM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CHOICE_SKIN_CONFIRM", false);
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06004076 RID: 16502 RVA: 0x0014B285 File Offset: 0x00149485
		public static string GET_STRING_CHOICE_UNIT_RECHECK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CHOICE_UNIT_CONFIRM_RECHECK", false);
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06004077 RID: 16503 RVA: 0x0014B292 File Offset: 0x00149492
		public static string GET_STRING_CHOICE_SHIP_RECHECK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CHOICE_SHIP_CONFIRM_RECHECK", false);
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06004078 RID: 16504 RVA: 0x0014B29F File Offset: 0x0014949F
		public static string GET_STRING_CHOICE_EQUIP_RECHECK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CHOICE_EQUIP_CONFIRM_RECHECK", false);
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06004079 RID: 16505 RVA: 0x0014B2AC File Offset: 0x001494AC
		public static string GET_STRING_CHOICE_MISC_RECHECK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CHOICE_MISC_CONFIRM_RECHECK", false);
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x0600407A RID: 16506 RVA: 0x0014B2B9 File Offset: 0x001494B9
		public static string GET_STRING_CHOICE_SKIN_RECHECK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CHOICE_SKIN_CONFIRM_RECHECK", false);
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x0600407B RID: 16507 RVA: 0x0014B2C6 File Offset: 0x001494C6
		public static string GET_STRING_CANNOT_EQUIP_ITEM_PRIVATE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EQUIP_POPUP_EQUIP_ITEM_FAIL_PRIVATE", false);
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x0600407C RID: 16508 RVA: 0x0014B2D3 File Offset: 0x001494D3
		public static string GET_STRING_ALREADY_FULL_EQUIPMENT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EQUIP_ALREADY_FULL_EQUIPMENT", false);
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x0600407D RID: 16509 RVA: 0x0014B2E0 File Offset: 0x001494E0
		public static string GET_STRING_INVENTORY_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INVENTORY_UNIT", false);
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x0600407E RID: 16510 RVA: 0x0014B2ED File Offset: 0x001494ED
		public static string GET_STRING_INVENTORY_SHIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INVENTORY_SHIP", false);
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x0600407F RID: 16511 RVA: 0x0014B2FA File Offset: 0x001494FA
		public static string GET_STRING_INVENTORY_EQUIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INVENTORY_EQUIP", false);
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06004080 RID: 16512 RVA: 0x0014B307 File Offset: 0x00149507
		public static string GET_STRING_TROPHY_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INVENTORY_TROPHY", false);
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06004081 RID: 16513 RVA: 0x0014B314 File Offset: 0x00149514
		public static string GET_STRING_FILTER_EQUIP_TYPE_STAT_MAIN
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FILTER_EQUIP_TYPE_STAT_MAIN", false);
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06004082 RID: 16514 RVA: 0x0014B321 File Offset: 0x00149521
		public static string GET_STRING_FILTER_EQUIP_TYPE_STAT_SUB1
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FILTER_EQUIP_TYPE_STAT_SUB1", false);
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06004083 RID: 16515 RVA: 0x0014B32E File Offset: 0x0014952E
		public static string GET_STRING_FILTER_EQUIP_TYPE_STAT_SUB2
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FILTER_EQUIP_TYPE_STAT_SUB2", false);
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06004084 RID: 16516 RVA: 0x0014B33B File Offset: 0x0014953B
		public static string GET_STRING_FILTER_EQUIP_TYPE_STAT_SET
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FILTER_EQUIP_TYPE_STAT_SET", false);
			}
		}

		// Token: 0x06004085 RID: 16517 RVA: 0x0014B348 File Offset: 0x00149548
		public static string GetExpandDesc(NKM_INVENTORY_EXPAND_TYPE type, bool isFullMsg = false)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			switch (type)
			{
			case NKM_INVENTORY_EXPAND_TYPE.NIET_EQUIP:
				text = NKCStringTable.GetString("SI_DP_EXPAND_DESC_NIET_EQUIP_DESC", false).Split(new char[]
				{
					'\n'
				})[0];
				text2 = NKCStringTable.GetString("SI_DP_EXPAND_DESC_NIET_EQUIP_FULL", false);
				break;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_UNIT:
				text = NKCStringTable.GetString("SI_DP_EXPAND_DESC_NIET_UNIT_DESC", false).Split(new char[]
				{
					'\n'
				})[0];
				text2 = NKCStringTable.GetString("SI_DP_EXPAND_DESC_NIET_UNIT_FULL", false);
				break;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_SHIP:
				text = NKCStringTable.GetString("SI_DP_EXPAND_DESC_NIET_SHIP_DESC", false).Split(new char[]
				{
					'\n'
				})[0];
				text2 = NKCStringTable.GetString("SI_DP_EXPAND_DESC_NIET_SHIP_FULL", false);
				break;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_OPERATOR:
				text = NKCUtilString.GET_STRING_INVENTORY_OPERATOR_ADD_DESC.Split(new char[]
				{
					'\n'
				})[0];
				text2 = NKCUtilString.GET_STRING_INVENTORY_OPERATOR_ADD_FULL;
				break;
			}
			if (!isFullMsg)
			{
				if (NKCAdManager.IsAdRewardInventory(type))
				{
					text = text + "\n" + NKCStringTable.GetString("SI_DP_AD_EXPAND_DESC", false);
				}
				return text;
			}
			if (NKCAdManager.IsAdRewardInventory(type))
			{
				return string.Concat(new string[]
				{
					text2.Split(new char[]
					{
						'\n'
					})[0],
					" ",
					text,
					"\n",
					NKCStringTable.GetString("SI_DP_AD_EXPAND_DESC", false)
				});
			}
			return text2 + text;
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06004086 RID: 16518 RVA: 0x0014B497 File Offset: 0x00149697
		public static string GET_STRING_ID
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ID", false);
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06004087 RID: 16519 RVA: 0x0014B4A4 File Offset: 0x001496A4
		public static string GET_STRING_DECK_BATCH_FAIL_STATE_WARFARE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_BATCH_FAIL_STATE_WARFARE", false);
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06004088 RID: 16520 RVA: 0x0014B4B1 File Offset: 0x001496B1
		public static string GET_STRING_DECK_BATCH_FAIL_STATE_DIVE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_BATCH_FAIL_STATE_DIVE", false);
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06004089 RID: 16521 RVA: 0x0014B4BE File Offset: 0x001496BE
		public static string GET_STRING_EVENT_DECK_FIXED_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_DECK_FIXED_TWO_PARAM", false);
			}
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x0600408A RID: 16522 RVA: 0x0014B4CB File Offset: 0x001496CB
		public static string GET_STRING_NO_EXIST_SELECT_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NO_EXIST_SELECT_UNIT", false);
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x0600408B RID: 16523 RVA: 0x0014B4D8 File Offset: 0x001496D8
		public static string GET_STRING_SKIN_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SKIN_ONE_PARAM", false);
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x0600408C RID: 16524 RVA: 0x0014B4E5 File Offset: 0x001496E5
		public static string GET_STRING_UNIT_ROLE_INFO
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_UNIT_ROLE_INFO", false);
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x0600408D RID: 16525 RVA: 0x0014B4F2 File Offset: 0x001496F2
		public static string GET_STRING_DECK_CHANGE_UNIT_WARNING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_CHANGE_UNIT_WARNING", false);
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x0600408E RID: 16526 RVA: 0x0014B4FF File Offset: 0x001496FF
		public static string GET_STRING_DECK_BUTTON_ACTION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_BUTTON_ACTION", false);
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x0600408F RID: 16527 RVA: 0x0014B50C File Offset: 0x0014970C
		public static string GET_STRING_DECK_CANNOT_START
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_CANNOT_START", false);
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06004090 RID: 16528 RVA: 0x0014B519 File Offset: 0x00149719
		public static string GET_STRING_DECK_STATE_DOING_MISSION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_STATE_DOING_MISSION", false);
			}
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06004091 RID: 16529 RVA: 0x0014B526 File Offset: 0x00149726
		public static string GET_STRING_DECK_STATE_DOING_WARFARE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_STATE_DOING_WARFARE", false);
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06004092 RID: 16530 RVA: 0x0014B533 File Offset: 0x00149733
		public static string GET_STRING_DECK_STATE_DOING_DIVE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_STATE_DOING_DIVE", false);
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06004093 RID: 16531 RVA: 0x0014B540 File Offset: 0x00149740
		public static string GET_STRING_DECK_STATE_FREE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_STATE_FREE", false);
			}
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06004094 RID: 16532 RVA: 0x0014B54D File Offset: 0x0014974D
		public static string GET_STRING_DECK_UNIT_STATE_DOING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_UNIT_STATE_DOING", false);
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06004095 RID: 16533 RVA: 0x0014B55A File Offset: 0x0014975A
		public static string GET_STRING_DECK_UNIT_STATE_DOING_MISSION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_UNIT_STATE_DOING_MISSION", false);
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06004096 RID: 16534 RVA: 0x0014B567 File Offset: 0x00149767
		public static string GET_STRING_DECK_UNIT_STATE_MISSION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_UNIT_STATE_MISSION", false);
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06004097 RID: 16535 RVA: 0x0014B574 File Offset: 0x00149774
		public static string GET_STRING_DECK_UNIT_STATE_DUPLICATE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_UNIT_STATE_DUPLICATE", false);
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06004098 RID: 16536 RVA: 0x0014B581 File Offset: 0x00149781
		public static string GET_STRING_DECK_UNIT_STATE_DECKED
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_UNIT_STATE_DECKED", false);
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06004099 RID: 16537 RVA: 0x0014B58E File Offset: 0x0014978E
		public static string GET_STRING_DECK_UNIT_STATE_LOCKED
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_UNIT_STATE_LOCKED", false);
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x0600409A RID: 16538 RVA: 0x0014B59B File Offset: 0x0014979B
		public static string GET_STRING_DECK_UNIT_STATE_MAINUNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_UNIT_STATE_MAINUNIT", false);
			}
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x0600409B RID: 16539 RVA: 0x0014B5A8 File Offset: 0x001497A8
		public static string GET_STRING_DECK_BUTTON_START
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_BUTTON_START", false);
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x0600409C RID: 16540 RVA: 0x0014B5B5 File Offset: 0x001497B5
		public static string GET_STRING_DECK_BUTTON_OK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_BUTTON_OK", false);
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x0600409D RID: 16541 RVA: 0x0014B5C2 File Offset: 0x001497C2
		public static string GET_STRING_DECK_BUTTON_SELECT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_BUTTON_SELECT", false);
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x0600409E RID: 16542 RVA: 0x0014B5CF File Offset: 0x001497CF
		public static string GET_STRING_DECK_BUTTON_BATCH
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_BUTTON_BATCH", false);
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x0600409F RID: 16543 RVA: 0x0014B5DC File Offset: 0x001497DC
		public static string GET_STRING_DECK_BUTTON_PVP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_BUTTON_PVP", false);
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x060040A0 RID: 16544 RVA: 0x0014B5E9 File Offset: 0x001497E9
		public static string GET_STRING_DECK_SELECT_SHIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_SELECT_SHIP", false);
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x060040A1 RID: 16545 RVA: 0x0014B5F6 File Offset: 0x001497F6
		public static string GET_STRING_COOLTIME_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_COOLTIME_ONE_PARAM", false);
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x060040A2 RID: 16546 RVA: 0x0014B603 File Offset: 0x00149803
		public static string GET_STRING_SKIN_LOCK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SKIN_LOCK", false);
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x060040A3 RID: 16547 RVA: 0x0014B610 File Offset: 0x00149810
		public static string GET_STRING_SKIN_GRADE_PREMIUM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SKIN_GRADE_PREMIUM", false);
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x060040A4 RID: 16548 RVA: 0x0014B61D File Offset: 0x0014981D
		public static string GET_STRING_UNIT_SKILL_INFO_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_UNIT_SKILL_INFO_ONE_PARAM", false);
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x060040A5 RID: 16549 RVA: 0x0014B62A File Offset: 0x0014982A
		public static string GET_STRING_DECK_SUCCESS_RATE_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_SUCCESS_RATE_ONE_PARAM", false);
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x060040A6 RID: 16550 RVA: 0x0014B637 File Offset: 0x00149837
		public static string GET_STRING_DECK_AVG_SUMMON_COST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_AVG_SUMMON_COST", false);
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x060040A7 RID: 16551 RVA: 0x0014B644 File Offset: 0x00149844
		public static string GET_STRING_DECK_SLOT_UNLOCK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_SLOT_UNLOCK", false);
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x060040A8 RID: 16552 RVA: 0x0014B651 File Offset: 0x00149851
		public static string GET_STRING_EVENT_DECK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_DECK", false);
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x060040A9 RID: 16553 RVA: 0x0014B65E File Offset: 0x0014985E
		public static string GET_STRING_MOVE_TO_TEST_MODE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MOVE_TO_TEST_MODE", false);
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x060040AA RID: 16554 RVA: 0x0014B66B File Offset: 0x0014986B
		public static string GET_STRING_MOVE_TO_COLLECTION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MOVE_TO_COLLECTION", false);
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x060040AB RID: 16555 RVA: 0x0014B678 File Offset: 0x00149878
		public static string GET_STRING_MOVE_TO_LAB
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MOVE_TO_LAB", false);
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x060040AC RID: 16556 RVA: 0x0014B685 File Offset: 0x00149885
		public static string GET_STRING_MOVE_TO_SHIPYARD
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MOVE_TO_SHIPYARD", false);
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x060040AD RID: 16557 RVA: 0x0014B692 File Offset: 0x00149892
		public static string GET_STRING_UNKNOWN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_UNKNOWN", false);
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x060040AE RID: 16558 RVA: 0x0014B69F File Offset: 0x0014989F
		public static string GET_STRING_BASE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_BASE", false);
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x060040AF RID: 16559 RVA: 0x0014B6AC File Offset: 0x001498AC
		public static string GET_STRING_BASE_SKIN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_BASE_SKIN", false);
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x060040B0 RID: 16560 RVA: 0x0014B6B9 File Offset: 0x001498B9
		public static string GET_STRING_SKIN_GRADE_NORMAL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SKIN_GRADE_NORMAL", false);
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x060040B1 RID: 16561 RVA: 0x0014B6C6 File Offset: 0x001498C6
		public static string GET_STRING_SKIN_GRADE_RARE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SKIN_GRADE_RARE", false);
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x060040B2 RID: 16562 RVA: 0x0014B6D3 File Offset: 0x001498D3
		public static string GET_STRING_SKIN_GRADE_SPECIAL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SKIN_GRADE_SPECIAL", false);
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x060040B3 RID: 16563 RVA: 0x0014B6E0 File Offset: 0x001498E0
		public static string GET_STRING_REMAIN_UNIT_COUNT_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REMAIN_UNIT_COUNT_ONE_PARAM", false);
			}
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x060040B4 RID: 16564 RVA: 0x0014B6ED File Offset: 0x001498ED
		public static string GET_STRING_SKILL_TRAINING_COMPLETE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SKILL_TRAINING_COMPLETE", false);
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x060040B5 RID: 16565 RVA: 0x0014B6FA File Offset: 0x001498FA
		public static string GET_STRING_LOYALTY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LOYALTY", false);
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x060040B6 RID: 16566 RVA: 0x0014B707 File Offset: 0x00149907
		public static string GET_STRING_LOYALTY_LIFETIME
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LOYALTY_LIFETIME", false);
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x060040B7 RID: 16567 RVA: 0x0014B714 File Offset: 0x00149914
		public static string GET_STRING_DECK_VIEW_EMPTY_SLOT_COST
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_DECKVIEW_BASIC_COST", false);
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x060040B8 RID: 16568 RVA: 0x0014B721 File Offset: 0x00149921
		public static string GET_STRING_MOVE_TO_NEGOTIATE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MOVE_TO_NEGOTIATE", false);
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x060040B9 RID: 16569 RVA: 0x0014B72E File Offset: 0x0014992E
		public static string GET_STRING_REVIEW_BAN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REVIEW_BAN", false);
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x060040BA RID: 16570 RVA: 0x0014B73B File Offset: 0x0014993B
		public static string GET_STRING_REVIEW_UNBAN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REVIEW_UNBAN", false);
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x060040BB RID: 16571 RVA: 0x0014B748 File Offset: 0x00149948
		public static string GET_STRING_REVIEW_BAN_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REVIEW_BAN_DESC", false);
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x060040BC RID: 16572 RVA: 0x0014B755 File Offset: 0x00149955
		public static string GET_STRING_REVIEW_BAN_CANCEL_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REVIEW_BAN_CANCEL_DESC", false);
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x060040BD RID: 16573 RVA: 0x0014B762 File Offset: 0x00149962
		public static string GET_STRING_REVIEW_BANNED_CONTENT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REVIEW_BANNED_CONTENT", false);
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x060040BE RID: 16574 RVA: 0x0014B76F File Offset: 0x0014996F
		public static string GET_STRING_DECK_UNIT_STATE_SEIZURE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_UNIT_STATE_SEIZURE", false);
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x060040BF RID: 16575 RVA: 0x0014B77C File Offset: 0x0014997C
		public static string GET_STRING_EVENT_DECK_ENEMY_LEVEL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHADOW_PALACE_LEVEL", false);
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x060040C0 RID: 16576 RVA: 0x0014B789 File Offset: 0x00149989
		private static string GET_DECK_NUMBER_STRING_WARFARE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_NUMBER_STRING_WARFARE", false);
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x060040C1 RID: 16577 RVA: 0x0014B796 File Offset: 0x00149996
		private static string GET_DECK_NUMBER_STRING_DAILY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_NUMBER_STRING_DAILY", false);
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x060040C2 RID: 16578 RVA: 0x0014B7A3 File Offset: 0x001499A3
		private static string GET_DECK_NUMBER_STRING_FRIEND
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_NUMBER_STRING_FRIEND", false);
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x060040C3 RID: 16579 RVA: 0x0014B7B0 File Offset: 0x001499B0
		private static string GET_DECK_NUMBER_STRING_RAID
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_NUMBER_STRING_RAID", false);
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x060040C4 RID: 16580 RVA: 0x0014B7BD File Offset: 0x001499BD
		private static string GET_DECK_NUMBER_STRING_PVP_DEFENCE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_NUMBER_STRING_PVP_DEFENCE", false);
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x060040C5 RID: 16581 RVA: 0x0014B7CA File Offset: 0x001499CA
		private static string GET_DECK_NUMBER_STRING_DIVE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_NUMBER_STRING_DIVE", false);
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x060040C6 RID: 16582 RVA: 0x0014B7D7 File Offset: 0x001499D7
		private static string GET_DECK_NUMBER_STRING_TRIM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECK_NUMBER_STRING_TRIM", false);
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x060040C7 RID: 16583 RVA: 0x0014B7E4 File Offset: 0x001499E4
		public static string GET_STRING_EP_TRAINING_NUMBER
		{
			get
			{
				return "TR.{0}";
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x060040C8 RID: 16584 RVA: 0x0014B7EB File Offset: 0x001499EB
		public static string GET_STRING_EP_CUTSCEN_NUMBER
		{
			get
			{
				return "#{0}";
			}
		}

		// Token: 0x060040C9 RID: 16585 RVA: 0x0014B7F4 File Offset: 0x001499F4
		public static string GetEpisodeCategory(EPISODE_CATEGORY episode_category)
		{
			switch (episode_category)
			{
			case EPISODE_CATEGORY.EC_MAINSTREAM:
				return NKCStringTable.GetString("SI_DP_EPISODE_CATEGORY_EC_MAINSTREAM", false);
			case EPISODE_CATEGORY.EC_DAILY:
				return NKCStringTable.GetString("SI_DP_EPISODE_CATEGORY_EC_DAILY", false);
			case EPISODE_CATEGORY.EC_COUNTERCASE:
				return NKCStringTable.GetString("SI_DP_EPISODE_CATEGORY_EC_COUNTERCASE", false);
			case EPISODE_CATEGORY.EC_SIDESTORY:
				return NKCStringTable.GetString("SI_DP_EPISODE_CATEGORY_EC_SUBSTREAM", false);
			case EPISODE_CATEGORY.EC_FIELD:
				return NKCStringTable.GetString("SI_DP_EPISODE_CATEGORY_EC_FIELD", false);
			case EPISODE_CATEGORY.EC_EVENT:
				return NKCUtilString.GET_STRING_EPISODE_CATEGORY_EC_EVENT;
			case EPISODE_CATEGORY.EC_SUPPLY:
				return NKCStringTable.GetString("SI_OPERATION_MENU_TEXT_SUPPLYMISSION", false);
			case EPISODE_CATEGORY.EC_CHALLENGE:
				return NKCStringTable.GetString("SI_DP_EPISODE_CATEGORY_EC_CHALLENGE", false);
			case EPISODE_CATEGORY.EC_TIMEATTACK:
				return NKCStringTable.GetString("SI_DP_EPISODE_CATEGORY_EC_TIMEATTACK", false);
			case EPISODE_CATEGORY.EC_TRIM:
				return NKCStringTable.GetString("SI_DP_EPISODE_CATEGORY_EC_TRIM", false);
			case EPISODE_CATEGORY.EC_FIERCE:
				return NKCStringTable.GetString("SI_DP_EPISODE_CATEGORY_EC_FIERCE", false);
			case EPISODE_CATEGORY.EC_SHADOW:
				return NKCStringTable.GetString("SI_PF_WORLD_MAP_SHADOW_BUTTON_TEXT", false);
			default:
				return NKCStringTable.GetString("SI_DP_EPISODE_CATEGORY_EC_DEFAULT", false);
			}
		}

		// Token: 0x060040CA RID: 16586 RVA: 0x0014B8D4 File Offset: 0x00149AD4
		public static string GetEpisodeCategoryEx1(EPISODE_CATEGORY episode_category)
		{
			switch (episode_category)
			{
			case EPISODE_CATEGORY.EC_MAINSTREAM:
				return NKCStringTable.GetString("SI_OPERATION_MENU_TEXT_MAINSTREAM", false);
			case EPISODE_CATEGORY.EC_DAILY:
				return NKCStringTable.GetString("SI_OPERATION_MENU_TEXT_DAILYMISSION", false);
			case EPISODE_CATEGORY.EC_COUNTERCASE:
				return NKCStringTable.GetString("SI_OPERATION_MENU_TEXT_COUNTERCASE", false);
			case EPISODE_CATEGORY.EC_SIDESTORY:
				return NKCStringTable.GetString("SI_OPERATION_MENU_TEXT_ANOTHERSTORY3", false);
			case EPISODE_CATEGORY.EC_FIELD:
				return NKCStringTable.GetString("SI_OPERATION_MENU_TEXT_FREECONTRACT", false);
			case EPISODE_CATEGORY.EC_EVENT:
				return NKCStringTable.GetString("SI_OPERATION_MENU_TEXT_EVENT", false);
			case EPISODE_CATEGORY.EC_SUPPLY:
				return NKCStringTable.GetString("SI_OPERATION_MENU_TEXT_SUPPLYMISSION", false);
			case EPISODE_CATEGORY.EC_CHALLENGE:
				return NKCStringTable.GetString("SI_OPERATION_MENU_TEXT_CHALLENGE", false);
			default:
				return NKCStringTable.GetString("SI_DP_EPISODE_CATEGORY_EC_DEFAULT", false);
			}
		}

		// Token: 0x060040CB RID: 16587 RVA: 0x0014B974 File Offset: 0x00149B74
		public static string GetEpisodeName(EPISODE_CATEGORY episode_category, string episode_name, string episode_title)
		{
			if (episode_category == EPISODE_CATEGORY.EC_DAILY)
			{
				return string.Format(NKCStringTable.GetString("SI_DP_EPISODE_NAME_EC_DAILY", false), episode_name, "");
			}
			if (episode_category == EPISODE_CATEGORY.EC_CHALLENGE)
			{
				return string.Format(NKCStringTable.GetString("SI_DP_EPISODE_NAME_DEFAULT", false), episode_title, "");
			}
			return string.Format(NKCStringTable.GetString("SI_DP_EPISODE_NAME_DEFAULT", false), episode_title, episode_name);
		}

		// Token: 0x060040CC RID: 16588 RVA: 0x0014B9CC File Offset: 0x00149BCC
		public static string GetSidestoryUnlockRequireDesc(NKMStageTempletV2 stageTemplet)
		{
			NKMStageTempletV2 nkmstageTempletV = null;
			STAGE_UNLOCK_REQ_TYPE eReqType = stageTemplet.m_UnlockInfo.eReqType;
			if (eReqType != STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_WARFARE)
			{
				if (eReqType != STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DUNGEON)
				{
					if (eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_PHASE)
					{
						nkmstageTempletV = NKMPhaseTemplet.Find(stageTemplet.m_UnlockInfo.reqValue).StageTemplet;
					}
				}
				else
				{
					nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(NKMDungeonManager.GetDungeonStrID(stageTemplet.m_UnlockInfo.reqValue));
				}
			}
			else
			{
				nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(NKMWarfareTemplet.Find(stageTemplet.m_UnlockInfo.reqValue).m_WarfareStrID);
			}
			if (nkmstageTempletV == null)
			{
				Debug.LogError(string.Format("StageTemplet is null - {0}", stageTemplet.m_UnlockInfo.reqValue));
				return "";
			}
			NKMDungeonTempletBase dungeonTempletBase = nkmstageTempletV.DungeonTempletBase;
			if (dungeonTempletBase != null && dungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_CUTSCENE)
			{
				return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_CLEAR_CUTSCENE_AND_BUY_EC_SIDESTORY", false), nkmstageTempletV.EpisodeTemplet.GetEpisodeTitle(), nkmstageTempletV.GetDungeonName());
			}
			return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_CLEAR_DUNGEON_AND_BUY_EC_SIDESTORY", false), nkmstageTempletV.EpisodeTemplet.GetEpisodeTitle(), nkmstageTempletV.ActId, nkmstageTempletV.m_StageUINum);
		}

		// Token: 0x060040CD RID: 16589 RVA: 0x0014BAD3 File Offset: 0x00149CD3
		public static string GetUnlockConditionRequireDesc(NKMStageTempletV2 stageTemplet, bool bSimple = false)
		{
			if (stageTemplet == null)
			{
				return "";
			}
			return NKCUtilString.GetUnlockConditionRequireDesc(stageTemplet.m_UnlockInfo, bSimple);
		}

		// Token: 0x060040CE RID: 16590 RVA: 0x0014BAEA File Offset: 0x00149CEA
		public static string GetUnlockConditionRequireDesc(UnlockInfo unlockInfo, bool bSimple = false)
		{
			return NKCUtilString.GetUnlockConditionRequireDesc(unlockInfo.eReqType, unlockInfo.reqValue, unlockInfo.reqValueStr, unlockInfo.reqDateTime, bSimple);
		}

		// Token: 0x060040CF RID: 16591 RVA: 0x0014BB0C File Offset: 0x00149D0C
		public static string GetUnlockConditionRequireDesc(List<UnlockInfo> lstUnlockInfo, bool bLockedOnly, bool bSimple = false)
		{
			if (lstUnlockInfo == null)
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder();
			NKMUserData cNKMUserData = NKCScenManager.CurrentUserData();
			foreach (UnlockInfo unlockInfo in lstUnlockInfo)
			{
				if (!bLockedOnly || !NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, unlockInfo, false))
				{
					stringBuilder.AppendLine(NKCUtilString.GetUnlockConditionRequireDesc(unlockInfo, bSimple));
				}
			}
			return stringBuilder.ToString().TrimEnd(Array.Empty<char>());
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x0014BB94 File Offset: 0x00149D94
		public static string GetStageUnlockConditionRequireDesc(NKMStageTempletV2 stageTempletRequire, bool bSimple)
		{
			if (stageTempletRequire != null)
			{
				switch (stageTempletRequire.EpisodeCategory)
				{
				case EPISODE_CATEGORY.EC_MAINSTREAM:
				{
					NKMEpisodeTempletV2 episodeTemplet = stageTempletRequire.EpisodeTemplet;
					if (episodeTemplet != null)
					{
						string text = (stageTempletRequire.m_Difficulty == EPISODE_DIFFICULTY.NORMAL) ? "" : string.Format("[{0}]", NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_ADDON_HARD", false));
						if (bSimple)
						{
							return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_CLEAR_WARFARE_SIMPLE", false), new object[]
							{
								episodeTemplet.GetEpisodeTitle(),
								stageTempletRequire.ActId,
								stageTempletRequire.m_StageUINum,
								text
							});
						}
						return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_CLEAR_WARFARE_DEFAULT", false), new object[]
						{
							episodeTemplet.GetEpisodeTitle(),
							stageTempletRequire.ActId,
							stageTempletRequire.m_StageUINum,
							text
						});
					}
					break;
				}
				case EPISODE_CATEGORY.EC_DAILY:
				{
					NKMEpisodeTempletV2 episodeTemplet2 = stageTempletRequire.EpisodeTemplet;
					return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_CLEAR_DUNGEON_EC_DAILY", false), episodeTemplet2.GetEpisodeName(), stageTempletRequire.m_StageUINum);
				}
				case EPISODE_CATEGORY.EC_COUNTERCASE:
				{
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(stageTempletRequire.ActId);
					if (unitTempletBase != null)
					{
						return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_CLEAR_DUNGEON_EC_COUNTERCASE", false), unitTempletBase.GetUnitName(), stageTempletRequire.m_StageUINum);
					}
					break;
				}
				case EPISODE_CATEGORY.EC_SIDESTORY:
				{
					NKMEpisodeTempletV2 episodeTemplet3 = stageTempletRequire.EpisodeTemplet;
					NKMDungeonTempletBase dungeonTempletBase = stageTempletRequire.DungeonTempletBase;
					if (dungeonTempletBase != null && dungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_CUTSCENE)
					{
						return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_CLEAR_CUTSCENE_AND_BUY_EC_SIDESTORY", false), episodeTemplet3.GetEpisodeName(), stageTempletRequire.GetDungeonName());
					}
					return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_CLEAR_DUNGEON_EC_SIDESTORY", false), episodeTemplet3.GetEpisodeName(), stageTempletRequire.ActId, stageTempletRequire.m_StageUINum);
				}
				case EPISODE_CATEGORY.EC_FIELD:
				{
					NKMEpisodeTempletV2 episodeTemplet4 = stageTempletRequire.EpisodeTemplet;
					if (episodeTemplet4 != null)
					{
						if (bSimple)
						{
							return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_CLEAR_FIELD_SIMPLE", false), episodeTemplet4.GetEpisodeTitle(), stageTempletRequire.ActId, stageTempletRequire.m_StageUINum);
						}
						return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_CLEAR_FIELD_DEFAULT", false), episodeTemplet4.GetEpisodeTitle(), stageTempletRequire.ActId, stageTempletRequire.m_StageUINum);
					}
					break;
				}
				case EPISODE_CATEGORY.EC_EVENT:
					return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_START_DATETIME", false), stageTempletRequire.ActId);
				case EPISODE_CATEGORY.EC_CHALLENGE:
				{
					NKMEpisodeTempletV2 episodeTemplet5 = stageTempletRequire.EpisodeTemplet;
					return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_CLEAR_DUNGEON_EC_CHALLENGE", false), episodeTemplet5.GetEpisodeTitle(), stageTempletRequire.ActId, stageTempletRequire.m_StageUINum);
				}
				}
			}
			return "";
		}

		// Token: 0x060040D1 RID: 16593 RVA: 0x0014BE20 File Offset: 0x0014A020
		public static string GetUnlockConditionRequireDesc(STAGE_UNLOCK_REQ_TYPE eReqType, int reqValue, string reqValueStr, DateTime reqDateTime, bool bSimple = false)
		{
			switch (eReqType)
			{
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_WARFARE:
			{
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(reqValue);
				if (nkmwarfareTemplet != null)
				{
					return NKCUtilString.GetStageUnlockConditionRequireDesc(NKMEpisodeMgr.FindStageTempletByBattleStrID(nkmwarfareTemplet.m_WarfareStrID), bSimple);
				}
				break;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_CITY_COUNT:
				if (bSimple)
				{
					return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_CITY_COUNT_SIMPLE", false), reqValue);
				}
				return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_CITY_COUNT_DEFAULT", false), reqValue);
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_GET:
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(reqValue);
				if (unitTempletBase != null)
				{
					return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_UNIT_GET", false), unitTempletBase.GetUnitName());
				}
				break;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_LEVEL_20:
			{
				NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(reqValue);
				if (unitTempletBase2 != null)
				{
					return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_UNIT_LEVEL_20", false), unitTempletBase2.GetUnitName());
				}
				break;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_LEVEL_25:
			{
				NKMUnitTempletBase unitTempletBase3 = NKMUnitManager.GetUnitTempletBase(reqValue);
				if (unitTempletBase3 != null)
				{
					return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_UNIT_LEVEL_25", false), unitTempletBase3.GetUnitName());
				}
				break;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_LEVEL_50:
			{
				NKMUnitTempletBase unitTempletBase4 = NKMUnitManager.GetUnitTempletBase(reqValue);
				if (unitTempletBase4 != null)
				{
					return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_UNIT_LEVEL_50", false), unitTempletBase4.GetUnitName());
				}
				break;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_LEVEL_80:
			{
				NKMUnitTempletBase unitTempletBase5 = NKMUnitManager.GetUnitTempletBase(reqValue);
				if (unitTempletBase5 != null)
				{
					return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_UNIT_LEVEL_80", false), unitTempletBase5.GetUnitName());
				}
				break;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_LEVEL_100:
			{
				NKMUnitTempletBase unitTempletBase6 = NKMUnitManager.GetUnitTempletBase(reqValue);
				if (unitTempletBase6 != null)
				{
					return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_UNIT_LEVEL_100", false), unitTempletBase6.GetUnitName());
				}
				break;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_LIMIT_GARDE_1:
			{
				NKMUnitTempletBase unitTempletBase7 = NKMUnitManager.GetUnitTempletBase(reqValue);
				if (unitTempletBase7 != null)
				{
					return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_UNIT_LIMIT_GARDE_1", false), unitTempletBase7.GetUnitName());
				}
				break;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_LIMIT_GARDE_2:
			{
				NKMUnitTempletBase unitTempletBase8 = NKMUnitManager.GetUnitTempletBase(reqValue);
				if (unitTempletBase8 != null)
				{
					return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_UNIT_LIMIT_GARDE_2", false), unitTempletBase8.GetUnitName());
				}
				break;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_LIMIT_GARDE_3:
			{
				NKMUnitTempletBase unitTempletBase9 = NKMUnitManager.GetUnitTempletBase(reqValue);
				if (unitTempletBase9 != null)
				{
					return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_UNIT_LIMIT_GARDE_3", false), unitTempletBase9.GetUnitName());
				}
				break;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_DEVOTION:
			{
				NKMUnitTempletBase unitTempletBase10 = NKMUnitManager.GetUnitTempletBase(reqValue);
				if (unitTempletBase10 != null)
				{
					return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_UNIT_DEVOTION", false), unitTempletBase10.GetUnitName());
				}
				break;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_PLAYER_LEVEL:
				if (bSimple)
				{
					return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_PLAYER_LEVEL_SIMPLE", false), reqValue);
				}
				return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_PLAYER_LEVEL_DEFAULT", false), reqValue);
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DUNGEON:
			{
				string stageUnlockConditionRequireDesc = NKCUtilString.GetStageUnlockConditionRequireDesc(NKMEpisodeMgr.FindStageTempletByBattleStrID(NKMDungeonManager.GetDungeonStrID(reqValue)), bSimple);
				if (!string.IsNullOrEmpty(stageUnlockConditionRequireDesc))
				{
					return stageUnlockConditionRequireDesc;
				}
				NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(reqValue);
				if (dungeonTempletBase != null)
				{
					if (dungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_CUTSCENE)
					{
						return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_CLEAR_DUNGEON_NDT_CUTSCENE", false), dungeonTempletBase.GetDungeonName());
					}
					if (bSimple)
					{
						return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_CLEAR_DUNGEON_ELSE_SIMPLE", false), dungeonTempletBase.GetDungeonName());
					}
					return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_CLEAR_DUNGEON_ELSE_DEFAULT", false), dungeonTempletBase.GetDungeonName());
				}
				break;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DIVE:
				Debug.LogError("UnExpected!");
				break;
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_PHASE:
			{
				NKMPhaseTemplet nkmphaseTemplet = NKMPhaseTemplet.Find(reqValue);
				if (nkmphaseTemplet != null)
				{
					return NKCUtilString.GetStageUnlockConditionRequireDesc(nkmphaseTemplet.StageTemplet, bSimple);
				}
				break;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_TRIM:
			{
				NKMTrimTemplet nkmtrimTemplet = NKMTrimTemplet.Find(reqValue);
				int num;
				if (!int.TryParse(reqValueStr, out num))
				{
					num = 1;
				}
				return NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_CLEAR_TRIM", new object[]
				{
					NKCStringTable.GetString(nkmtrimTemplet.TirmGroupName, false),
					num
				});
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_START_DATETIME:
				if (reqDateTime <= DateTime.MinValue)
				{
					Debug.LogError("STAGE_UNLOCK_REQ_TYPE.SURT_START_DATETIME 사용 시 reqValueStr 에 시간이 들어가야함 (2020-07-01 04:00)");
				}
				else
				{
					if (!(reqDateTime <= ServiceTime.Recent))
					{
						return NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_ALWAYS_LOCKED", false);
					}
					Debug.LogError("잠겨있지 않으므로 여기 들어오면 안됨");
				}
				break;
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_WARFARE_START_DATETIME:
			{
				NKMUserData cNKMUserData = NKCScenManager.CurrentUserData();
				UnlockInfo unlockInfo = new UnlockInfo(STAGE_UNLOCK_REQ_TYPE.SURT_START_DATETIME, reqValue, reqDateTime);
				if (!NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, unlockInfo, false))
				{
					return NKCUtilString.GetUnlockConditionRequireDesc(STAGE_UNLOCK_REQ_TYPE.SURT_START_DATETIME, reqValue, reqValueStr, reqDateTime, bSimple);
				}
				NKMUserData cNKMUserData2 = NKCScenManager.CurrentUserData();
				unlockInfo = new UnlockInfo(STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_WARFARE, reqValue);
				if (!NKMContentUnlockManager.IsContentUnlocked(cNKMUserData2, unlockInfo, false))
				{
					return NKCUtilString.GetUnlockConditionRequireDesc(STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_WARFARE, reqValue, reqValueStr, reqDateTime, bSimple);
				}
				break;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DUNGEON_START_DATETIME:
			{
				NKMUserData cNKMUserData3 = NKCScenManager.CurrentUserData();
				UnlockInfo unlockInfo = new UnlockInfo(STAGE_UNLOCK_REQ_TYPE.SURT_START_DATETIME, reqValue, reqDateTime);
				if (!NKMContentUnlockManager.IsContentUnlocked(cNKMUserData3, unlockInfo, false))
				{
					return NKCUtilString.GetUnlockConditionRequireDesc(STAGE_UNLOCK_REQ_TYPE.SURT_START_DATETIME, reqValue, reqValueStr, reqDateTime, bSimple);
				}
				NKMUserData cNKMUserData4 = NKCScenManager.CurrentUserData();
				unlockInfo = new UnlockInfo(STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DUNGEON, reqValue);
				if (!NKMContentUnlockManager.IsContentUnlocked(cNKMUserData4, unlockInfo, false))
				{
					return NKCUtilString.GetUnlockConditionRequireDesc(STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DUNGEON, reqValue, reqValueStr, reqDateTime, bSimple);
				}
				break;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_ALWAYS_LOCKED:
			case STAGE_UNLOCK_REQ_TYPE.SURT_ALWAYS_HIDDEN:
				return NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_ALWAYS_LOCKED", false);
			case STAGE_UNLOCK_REQ_TYPE.SURT_GUILD_LEVEL:
				if (bSimple)
				{
					return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_GUILD_LEVEL_SIMPLE", false), reqValue);
				}
				return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_GUILD_LEVEL_DEFAULT", false), reqValue);
			case STAGE_UNLOCK_REQ_TYPE.SURT_NEWBIE_USER:
			{
				int num2;
				if (int.TryParse(reqValueStr, out num2))
				{
					DateTime registerTime = NKCScenManager.CurrentUserData().m_NKMUserDateData.m_RegisterTime;
					DateTime dateTime = registerTime.AddDays((double)num2);
					if (dateTime > NKCSynchronizedTime.GetServerUTCTime(0.0))
					{
						return NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_NEWBIE_USER_BEFORE", new object[]
						{
							NKCUtilString.GetRemainTimeString(dateTime, 1)
						});
					}
				}
				if (NKCScenManager.CurrentUserData().m_NKMUserDateData.m_RegisterTime.AddDays((double)reqValue) > NKCSynchronizedTime.GetServerUTCTime(0.0))
				{
					return NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_NEWBIE_USER_AFTER", new object[]
					{
						reqValue
					});
				}
				Debug.LogError("잠겨있지 않으므로 여기 들어오면 안됨");
				break;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_MISSION_CLEAR:
			{
				NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(reqValue);
				if (missionTemplet != null)
				{
					return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_SURT_MISSION_CLEAR_DEFAULT", false), missionTemplet.GetTitle());
				}
				break;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_LAST_STAGE:
			{
				NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(reqValue);
				StringBuilder stringBuilder = new StringBuilder();
				switch (nkmstageTempletV.EpisodeCategory)
				{
				case EPISODE_CATEGORY.EC_MAINSTREAM:
					stringBuilder.Append(string.Format("{0} {1} {2}{3}", new object[]
					{
						NKCStringTable.GetString("SI_DP_EPISODE_CATEGORY_EC_MAINSTREAM", false),
						nkmstageTempletV.EpisodeTemplet.GetEpisodeTitle(),
						NKCStringTable.GetString("SI_DP_CONTENTS_UNLOCK_ACT_DISPLAY", false),
						nkmstageTempletV.ActId
					}));
					break;
				case EPISODE_CATEGORY.EC_DAILY:
					stringBuilder.Append(NKCStringTable.GetString("SI_DP_EPISODE_CATEGORY_EC_DAILY", false) + " " + nkmstageTempletV.EpisodeTemplet.GetEpisodeName());
					break;
				case EPISODE_CATEGORY.EC_COUNTERCASE:
					stringBuilder.Append(NKCStringTable.GetString("SI_DP_EPISODE_CATEGORY_EC_COUNTERCASE", false) + " " + nkmstageTempletV.EpisodeTemplet.GetEpisodeName());
					break;
				case EPISODE_CATEGORY.EC_SIDESTORY:
					stringBuilder.Append(string.Format("{0} {1} {2}{3}", new object[]
					{
						NKCStringTable.GetString("SI_DP_EPISODE_CATEGORY_EC_SIDESTORY", false),
						nkmstageTempletV.EpisodeTemplet.GetEpisodeTitle(),
						NKCStringTable.GetString("SI_DP_CONTENTS_UNLOCK_ACT_DISPLAY", false),
						nkmstageTempletV.ActId
					}));
					break;
				case EPISODE_CATEGORY.EC_FIELD:
					stringBuilder.Append(string.Format("{0} {1} {2}{3}", new object[]
					{
						NKCStringTable.GetString("SI_DP_EPISODE_CATEGORY_EC_FIELD", false),
						nkmstageTempletV.EpisodeTemplet.GetEpisodeTitle(),
						NKCStringTable.GetString("SI_DP_CONTENTS_UNLOCK_ACT_DISPLAY", false),
						nkmstageTempletV.ActId
					}));
					break;
				case EPISODE_CATEGORY.EC_EVENT:
					stringBuilder.Append(string.Format("{0} {1} {2}{3}", new object[]
					{
						NKCStringTable.GetString("SI_DP_EPISODE_CATEGORY_EC_EVENT", false),
						nkmstageTempletV.EpisodeTemplet.GetEpisodeTitle(),
						NKCStringTable.GetString("SI_DP_CONTENTS_UNLOCK_ACT_DISPLAY", false),
						nkmstageTempletV.ActId
					}));
					break;
				case EPISODE_CATEGORY.EC_SUPPLY:
					stringBuilder.Append(NKCStringTable.GetString("SI_DP_EPISODE_CATEGORY_EC_SUPPLY", false) + " " + nkmstageTempletV.EpisodeTemplet.GetEpisodeName());
					break;
				case EPISODE_CATEGORY.EC_CHALLENGE:
					stringBuilder.Append(string.Format("{0} {1} {2}{3}", new object[]
					{
						NKCStringTable.GetString("SI_DP_EPISODE_CATEGORY_EC_CHALLENGE", false),
						nkmstageTempletV.EpisodeTemplet.GetEpisodeTitle(),
						NKCStringTable.GetString("SI_DP_CONTENTS_UNLOCK_ACT_DISPLAY", false),
						nkmstageTempletV.ActId
					}));
					break;
				default:
					return NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_ALWAYS_LOCKED", false);
				}
				if (nkmstageTempletV.m_Difficulty == EPISODE_DIFFICULTY.HARD)
				{
					stringBuilder.Append(" [" + NKCStringTable.GetString("SI_OPERATION_DIFF_HARD1", false) + "]");
				}
				return string.Format(NKCStringTable.GetString("SI_DP_CONTENTS_UNLOCK_ALL_CLEAR", false), stringBuilder.ToString());
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_MISSION_TAB_UNLOCKED:
			{
				NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(reqValue);
				if (missionTabTemplet != null)
				{
					return NKCUtilString.GetUnlockConditionRequireDesc(missionTabTemplet.m_UnlockInfo, true, bSimple);
				}
				break;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_STAGE:
				return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_CLEAR_STAGE", false), Array.Empty<object>());
			case STAGE_UNLOCK_REQ_TYPE.SURT_INTERVAL:
				if (!NKCSynchronizedTime.IsEventTime(reqValueStr))
				{
					return NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_ALWAYS_LOCKED", false);
				}
				Debug.LogError("잠겨있지 않으므로 여기 들어오면 안됨");
				break;
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_WARFARE_INTERVAL:
			{
				if (!NKCSynchronizedTime.IsEventTime(reqValueStr))
				{
					return NKCUtilString.GetUnlockConditionRequireDesc(STAGE_UNLOCK_REQ_TYPE.SURT_INTERVAL, reqValue, reqValueStr, reqDateTime, bSimple);
				}
				NKMUserData cNKMUserData5 = NKCScenManager.CurrentUserData();
				UnlockInfo unlockInfo = new UnlockInfo(STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DUNGEON, reqValue);
				if (!NKMContentUnlockManager.IsContentUnlocked(cNKMUserData5, unlockInfo, false))
				{
					return NKCUtilString.GetUnlockConditionRequireDesc(STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_WARFARE, reqValue, reqValueStr, reqDateTime, bSimple);
				}
				break;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DUNGEON_INTERVAL:
			{
				if (!NKCSynchronizedTime.IsEventTime(reqValueStr))
				{
					return NKCUtilString.GetUnlockConditionRequireDesc(STAGE_UNLOCK_REQ_TYPE.SURT_INTERVAL, reqValue, reqValueStr, reqDateTime, bSimple);
				}
				NKMUserData cNKMUserData6 = NKCScenManager.CurrentUserData();
				UnlockInfo unlockInfo = new UnlockInfo(STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_WARFARE, reqValue);
				if (!NKMContentUnlockManager.IsContentUnlocked(cNKMUserData6, unlockInfo, false))
				{
					return NKCUtilString.GetUnlockConditionRequireDesc(STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DUNGEON, reqValue, reqValueStr, reqDateTime, bSimple);
				}
				break;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_PHASE_INTERVAL:
			{
				if (!NKCSynchronizedTime.IsEventTime(reqValueStr))
				{
					return NKCUtilString.GetUnlockConditionRequireDesc(STAGE_UNLOCK_REQ_TYPE.SURT_INTERVAL, reqValue, reqValueStr, reqDateTime, bSimple);
				}
				NKMUserData cNKMUserData7 = NKCScenManager.CurrentUserData();
				UnlockInfo unlockInfo = new UnlockInfo(STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_PHASE, reqValue);
				if (!NKMContentUnlockManager.IsContentUnlocked(cNKMUserData7, unlockInfo, false))
				{
					return NKCUtilString.GetUnlockConditionRequireDesc(STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_PHASE, reqValue, reqValueStr, reqDateTime, bSimple);
				}
				break;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_DIVE_HISTORY_CLEARED:
			{
				NKMDiveTemplet nkmdiveTemplet = NKMDiveTemplet.Find(reqValue);
				if (nkmdiveTemplet != null)
				{
					return string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_DIVE_HISTORY_CLEARED", false), nkmdiveTemplet.IndexID);
				}
				break;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_OPEN_ROOM:
			{
				NKMOfficeRoomTemplet nkmofficeRoomTemplet = NKMOfficeRoomTemplet.Find(reqValue);
				string arg = NKCStringTable.GetString(nkmofficeRoomTemplet.SectionTemplet.SectionName, false) + " " + NKCStringTable.GetString(nkmofficeRoomTemplet.Name, false);
				return string.Format(NKCStringTable.GetString("SI_DP_OFFICE_ROOM_UNLOCK_WARNING", false), arg);
			}
			}
			Debug.LogError(string.Format("UnlockCondition string case not found for :{0}. using default string.", eReqType.ToString()));
			return NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_ALWAYS_LOCKED", false);
		}

		// Token: 0x060040D2 RID: 16594 RVA: 0x0014C852 File Offset: 0x0014AA52
		public static string GetEpisodeTitle(NKMEpisodeTempletV2 cNKMEpisodeTemplet, NKMStageTempletV2 stageTemplet)
		{
			if (cNKMEpisodeTemplet != null && stageTemplet != null)
			{
				return cNKMEpisodeTemplet.GetEpisodeTitle();
			}
			return "";
		}

		// Token: 0x060040D3 RID: 16595 RVA: 0x0014C868 File Offset: 0x0014AA68
		public static string GetEpisodeNumber(NKMEpisodeTempletV2 cNKMEpisodeTemplet, NKMStageTempletV2 stageTemplet)
		{
			if (cNKMEpisodeTemplet == null || stageTemplet == null)
			{
				return "";
			}
			if (stageTemplet.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_PRACTICE)
			{
				return string.Format(NKCUtilString.GET_STRING_EP_TRAINING_NUMBER, stageTemplet.m_StageUINum);
			}
			bool flag = false;
			if (stageTemplet.m_STAGE_TYPE == STAGE_TYPE.ST_DUNGEON)
			{
				NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(stageTemplet.m_StageBattleStrID);
				if (dungeonTempletBase != null && dungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_CUTSCENE)
				{
					flag = true;
				}
			}
			if (flag)
			{
				return string.Format(NKCUtilString.GET_STRING_EP_CUTSCEN_NUMBER, stageTemplet.m_StageUINum);
			}
			return string.Format("{0}-{1}", stageTemplet.ActId, stageTemplet.m_StageUINum);
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x060040D4 RID: 16596 RVA: 0x0014C901 File Offset: 0x0014AB01
		public static string GET_STRING_EPISODE_GIVE_UP_WARFARE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EPISODE_GIVE_UP_WARFARE", false);
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x060040D5 RID: 16597 RVA: 0x0014C90E File Offset: 0x0014AB0E
		public static string GET_STRING_SIDE_STORY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SIDE_STORY", false);
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x060040D6 RID: 16598 RVA: 0x0014C91B File Offset: 0x0014AB1B
		public static string GET_STRING_MAIN_STREAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MAIN_STREAM", false);
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x060040D7 RID: 16599 RVA: 0x0014C928 File Offset: 0x0014AB28
		public static string GET_STRING_MENU_NAME_OPERATION_VIEWER
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MENU_NAME_OPERATION_VIEWER", false);
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x060040D8 RID: 16600 RVA: 0x0014C935 File Offset: 0x0014AB35
		public static string GET_STRING_MENU_NAME_CC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MENU_NAME_CC", false);
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x060040D9 RID: 16601 RVA: 0x0014C942 File Offset: 0x0014AB42
		public static string GET_STRING_MENU_NAME_CCS
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MENU_NAME_CCS", false);
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x060040DA RID: 16602 RVA: 0x0014C94F File Offset: 0x0014AB4F
		public static string GET_STRING_COUNTER_CASE_SLOT_BUTTON_LOCK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_COUNTER_CASE_SLOT_BUTTON_LOCK", false);
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x060040DB RID: 16603 RVA: 0x0014C95C File Offset: 0x0014AB5C
		public static string GET_STRING_COUNTER_CASE_SLOT_BUTTON_UNLOCK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_COUNTER_CASE_SLOT_BUTTON_UNLOCK", false);
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x060040DC RID: 16604 RVA: 0x0014C969 File Offset: 0x0014AB69
		public static string GET_STRING_COUNTER_CASE_UNLOCK_BUTTON
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_COUNTER_CASE_UNLOCK_BUTTON", false);
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x060040DD RID: 16605 RVA: 0x0014C976 File Offset: 0x0014AB76
		public static string GET_STRING_COUNTER_CASE_LOCK_BUTTON
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_COUNTER_CASE_LOCK_BUTTON", false);
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x060040DE RID: 16606 RVA: 0x0014C983 File Offset: 0x0014AB83
		public static string GET_STRING_DAILY_CHECK_DAY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DAILY_CHECK_DAY", false);
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x060040DF RID: 16607 RVA: 0x0014C990 File Offset: 0x0014AB90
		public static string GET_STRING_CONTENTS_UNLOCK_CLEAR_STAGE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_CLEAR_STAGE", false);
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x060040E0 RID: 16608 RVA: 0x0014C99D File Offset: 0x0014AB9D
		public static string GET_STRING_NO_EVENT
		{
			get
			{
				return NKCStringTable.GetString("SI_TOAST_EP_CATEGORY_HAVE_NO_EVENT", false);
			}
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x060040E1 RID: 16609 RVA: 0x0014C9AA File Offset: 0x0014ABAA
		public static string GET_STRING_FREE_ORDER
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FREE_ORDER", false);
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x060040E2 RID: 16610 RVA: 0x0014C9B7 File Offset: 0x0014ABB7
		public static string GET_STRING_EVENT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT", false);
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x060040E3 RID: 16611 RVA: 0x0014C9C4 File Offset: 0x0014ABC4
		public static string GET_STRING_EPISODE_CATEGORY_EC_EVENT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EPISODE_CATEGORY_EC_EVENT", false);
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x060040E4 RID: 16612 RVA: 0x0014C9D1 File Offset: 0x0014ABD1
		public static string GET_STRING_EPISODE_PROGRESS
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EPISODE_PROGRESS", false);
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x060040E5 RID: 16613 RVA: 0x0014C9DE File Offset: 0x0014ABDE
		public static string GET_STRING_EPISODE_SUBSTREAM_DATA_EXPUNGED
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EPISODE_SUBSTREAM_DATA_EXPUNGED", false);
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x060040E6 RID: 16614 RVA: 0x0014C9EB File Offset: 0x0014ABEB
		public static string GET_STRING_OPERATION_SUBSTREAM_SHORTCUT_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OPERATION_SUBSTREAM_SHORTCUT_TITLE", false);
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x060040E7 RID: 16615 RVA: 0x0014C9F8 File Offset: 0x0014ABF8
		public static string GET_STRING_EPISODE_CATEGORY_EC_SIDESTORY
		{
			get
			{
				return NKCStringTable.GetString("SI_EPISODE_CATEGORY_EC_SIDESTORY", false);
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x060040E8 RID: 16616 RVA: 0x0014CA05 File Offset: 0x0014AC05
		public static string GET_STRING_EPISODE_SUPPLEMENT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EPISODE_SUPPLEMENT", false);
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x060040E9 RID: 16617 RVA: 0x0014CA12 File Offset: 0x0014AC12
		public static string GET_STRING_WARFARE_FIRST_ALL_CLEAR
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_FIRST_ALL_CLEAR", false);
			}
		}

		// Token: 0x060040EA RID: 16618 RVA: 0x0014CA20 File Offset: 0x0014AC20
		public static string GetWFMissionText(WARFARE_GAME_MISSION_TYPE missionType, int missionValue)
		{
			StringBuilder stringBuilder = new StringBuilder();
			switch (missionType)
			{
			case WARFARE_GAME_MISSION_TYPE.WFMT_CLEAR:
				stringBuilder.Append(NKCStringTable.GetString("SI_DP_WF_MISSION_TEXT_WFMT_CLEAR", false));
				break;
			case WARFARE_GAME_MISSION_TYPE.WFMT_ALLKILL:
				stringBuilder.Append(NKCStringTable.GetString("SI_DP_WF_MISSION_TEXT_WFMT_ALLKILL", false));
				break;
			case WARFARE_GAME_MISSION_TYPE.WFMT_PHASE:
				stringBuilder.Append(string.Format(NKCStringTable.GetString("SI_DP_WF_MISSION_TEXT_WFMT_PHASE", false), missionValue));
				break;
			case WARFARE_GAME_MISSION_TYPE.WFMT_NO_SHIPWRECK:
				stringBuilder.Append(string.Format(NKCStringTable.GetString("SI_DP_WF_MISSION_TEXT_WFMT_NO_SHIPWRECK", false), Array.Empty<object>()));
				break;
			case WARFARE_GAME_MISSION_TYPE.WFMT_KILL:
				stringBuilder.Append(string.Format(NKCStringTable.GetString("SI_DP_WF_MISSION_TEXT_WFMT_KILL", false), missionValue));
				break;
			case WARFARE_GAME_MISSION_TYPE.WFMT_FIRST_ATTACK:
				stringBuilder.Append(string.Format(NKCStringTable.GetString("SI_DP_WF_MISSION_TEXT_WFMT_FIRST_ATTACK", false), missionValue));
				break;
			case WARFARE_GAME_MISSION_TYPE.WFMT_ASSIST:
				stringBuilder.Append(string.Format(NKCStringTable.GetString("SI_DP_WF_MISSION_TEXT_WFMT_ASSIST", false), missionValue));
				break;
			case WARFARE_GAME_MISSION_TYPE.WFMT_CONTAINER:
				stringBuilder.Append(string.Format(NKCStringTable.GetString("SI_DP_WF_MISSION_TEXT_WFMT_CONTAINER", false), missionValue));
				break;
			case WARFARE_GAME_MISSION_TYPE.WFMT_NOSUPPLY_WIN:
				stringBuilder.Append(string.Format(NKCStringTable.GetString("SI_DP_WF_MISSION_TEXT_WFMT_NOSUPPLY_WIN", false), missionValue));
				break;
			case WARFARE_GAME_MISSION_TYPE.WFMT_NOSUPPLY_ALLKILL:
				stringBuilder.Append(string.Format(NKCStringTable.GetString("SI_DP_WF_MISSION_TEXT_WFMT_NOSUPPLY_ALLKILL", false), missionValue));
				break;
			default:
				return "";
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060040EB RID: 16619 RVA: 0x0014CBA8 File Offset: 0x0014ADA8
		public static string GetWFWinContionText(WARFARE_GAME_CONDITION winCondition)
		{
			switch (winCondition)
			{
			case WARFARE_GAME_CONDITION.WFC_KILL_BOSS:
				return NKCStringTable.GetString("SI_DP_WF_WIN_CONDITION_TEXT_WFC_KILL_BOSS", false);
			case WARFARE_GAME_CONDITION.WFC_KILL_ALL:
				return NKCStringTable.GetString("SI_DP_WF_WIN_CONDITION_TEXT_WFC_KILL_ALL", false);
			case WARFARE_GAME_CONDITION.WFC_KILL_TARGET:
				return NKCStringTable.GetString("SI_DP_WF_WIN_CONDITION_TEXT_WFC_KILL_TARGET", false);
			case WARFARE_GAME_CONDITION.WFC_TILE_ENTER:
				return NKCStringTable.GetString("SI_DP_WF_WIN_CONDITION_TEXT_WFC_TILE_ENTER", false);
			case WARFARE_GAME_CONDITION.WFC_PHASE_TILE_HOLD:
				return NKCStringTable.GetString("SI_DP_WF_WIN_CONDITION_TEXT_WFC_PHASE_TILE_HOLD", false);
			}
			return "";
		}

		// Token: 0x060040EC RID: 16620 RVA: 0x0014CC1C File Offset: 0x0014AE1C
		public static string GetWFLoseConditionText(WARFARE_GAME_CONDITION loseCondition)
		{
			switch (loseCondition)
			{
			case WARFARE_GAME_CONDITION.WFC_KILL_BOSS:
				return NKCStringTable.GetString("SI_DP_WF_LOSE_CONDITION_TEXT_WFC_KILL_BOSS", false);
			case WARFARE_GAME_CONDITION.WFC_KILL_ALL:
				return NKCStringTable.GetString("SI_DP_WF_LOSE_CONDITION_TEXT_WFC_KILL_ALL", false);
			case WARFARE_GAME_CONDITION.WFC_KILL_COUNT:
				return NKCStringTable.GetString("SI_DP_WF_LOSE_CONDITION_TEXT_WFC_KILL_COUNT", false);
			case WARFARE_GAME_CONDITION.WFC_TILE_ENTER:
				return NKCStringTable.GetString("SI_DP_WF_LOSE_CONDITION_TEXT_WFC_TILE_ENTER", false);
			case WARFARE_GAME_CONDITION.WFC_PHASE:
				return NKCStringTable.GetString("SI_DP_WF_LOSE_CONDITION_TEXT_WFC_PHASE", false);
			}
			return "";
		}

		// Token: 0x060040ED RID: 16621 RVA: 0x0014CC8C File Offset: 0x0014AE8C
		public static string GetWFEpisodeNumber(NKMWarfareTemplet cNKMWarfareTemplet)
		{
			if (cNKMWarfareTemplet != null)
			{
				NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(cNKMWarfareTemplet.m_WarfareStrID);
				if (nkmstageTempletV != null)
				{
					NKMEpisodeTempletV2 episodeTemplet = nkmstageTempletV.EpisodeTemplet;
					if (episodeTemplet != null)
					{
						string episodeNumber = NKCUtilString.GetEpisodeNumber(episodeTemplet, nkmstageTempletV);
						if (!string.IsNullOrEmpty(episodeNumber))
						{
							NKCUtilString.m_sStringBuilder.Clear();
							NKCUtilString.m_sStringBuilder.Append(episodeTemplet.GetEpisodeTitle());
							NKCUtilString.m_sStringBuilder.Append(" ");
							NKCUtilString.m_sStringBuilder.Append(episodeNumber);
							return NKCUtilString.m_sStringBuilder.ToString();
						}
					}
				}
			}
			return "";
		}

		// Token: 0x060040EE RID: 16622 RVA: 0x0014CD10 File Offset: 0x0014AF10
		public static string GetWFMissionTextWithProgress(WarfareGameData warfareGameData, WARFARE_GAME_MISSION_TYPE missionType, int missionValue)
		{
			if (missionType == WARFARE_GAME_MISSION_TYPE.WFMT_NONE)
			{
				return string.Empty;
			}
			string wfmissionText = NKCUtilString.GetWFMissionText(missionType, missionValue);
			int currentMissionValue = NKCWarfareManager.GetCurrentMissionValue(warfareGameData, missionType);
			return string.Format("{0} ({1}/{2})", wfmissionText, currentMissionValue, missionValue);
		}

		// Token: 0x060040EF RID: 16623 RVA: 0x0014CD4D File Offset: 0x0014AF4D
		public static string GetCurrentProgress(int turnCount, int value)
		{
			return string.Format(" ({0}/{1})", turnCount, value);
		}

		// Token: 0x060040F0 RID: 16624 RVA: 0x0014CD65 File Offset: 0x0014AF65
		public static string GetPlayingWarfare(string episodeTitle, int stageActID, int _StageUINum)
		{
			return string.Format(NKCStringTable.GetString("SI_DP_PLAYING_WARFARE", false), episodeTitle, stageActID, _StageUINum);
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x060040F1 RID: 16625 RVA: 0x0014CD84 File Offset: 0x0014AF84
		public static string GET_STRING_MENU_NAME_WARFARE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MENU_NAME_WARFARE", false);
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x060040F2 RID: 16626 RVA: 0x0014CD91 File Offset: 0x0014AF91
		public static string GET_STRING_WARFARE_REPAIR_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_REPAIR_TITLE", false);
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x060040F3 RID: 16627 RVA: 0x0014CD9E File Offset: 0x0014AF9E
		public static string GET_STRING_WARFARE_REPAIR_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_REPAIR_DESC", false);
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x060040F4 RID: 16628 RVA: 0x0014CDAB File Offset: 0x0014AFAB
		public static string GET_STRING_WARFARE_SUPPLY_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_SUPPLY_TITLE", false);
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x060040F5 RID: 16629 RVA: 0x0014CDB8 File Offset: 0x0014AFB8
		public static string GET_STRING_WARFARE_SUPPLY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_SUPPLY_DESC", false);
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x060040F6 RID: 16630 RVA: 0x0014CDC5 File Offset: 0x0014AFC5
		public static string GET_STRING_WARFARE_WARNING_GAME_START
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_WARNING_GAME_START", false);
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x060040F7 RID: 16631 RVA: 0x0014CDD2 File Offset: 0x0014AFD2
		public static string GET_STRING_WARFARE_WARNING_FINISH_TURN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_WARNING_FINISH_TURN", false);
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x060040F8 RID: 16632 RVA: 0x0014CDDF File Offset: 0x0014AFDF
		public static string GET_STRING_WARFARE_WARNING_GIVE_UP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_WARNING_GIVE_UP", false);
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x060040F9 RID: 16633 RVA: 0x0014CDEC File Offset: 0x0014AFEC
		public static string GET_STRING_WARFARE_WARNING_SUPPLY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_WARNING_SUPPLY", false);
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x060040FA RID: 16634 RVA: 0x0014CDF9 File Offset: 0x0014AFF9
		public static string GET_STRING_WARFARE_WARNING_NO_EXIST_SUPPLY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_WARNING_NO_EXIST_SUPPLY", false);
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x060040FB RID: 16635 RVA: 0x0014CE06 File Offset: 0x0014B006
		public static string GET_STRING_WARFARE_PHASE_FINISH
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_PHASE_FINISH", false);
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x060040FC RID: 16636 RVA: 0x0014CE13 File Offset: 0x0014B013
		public static string GET_STRING_WARFARE_WAVE_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_WAVE_ONE_PARAM", false);
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x060040FD RID: 16637 RVA: 0x0014CE20 File Offset: 0x0014B020
		public static string GET_STRING_WARFARE_RESULT_GAME_TIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_RESULT_GAME_TIP1", false) + NKCStringTable.GetString("SI_DP_WARFARE_RESULT_GAME_TIP2", false) + NKCStringTable.GetString("SI_DP_WARFARE_RESULT_GAME_TIP3", false) + NKCStringTable.GetString("SI_DP_WARFARE_RESULT_GAME_TIP4", false);
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x060040FE RID: 16638 RVA: 0x0014CE53 File Offset: 0x0014B053
		public static string GET_STRING_DUNGEON_RESULT_GAME_TIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DUNGEON_RESULT_GAME_TIP", false);
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x060040FF RID: 16639 RVA: 0x0014CE60 File Offset: 0x0014B060
		public static string GET_STRING_WARFARE_POPUP_ENEMY_INFO_KILL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_POPUP_ENEMY_INFO_KILL", false);
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06004100 RID: 16640 RVA: 0x0014CE6D File Offset: 0x0014B06D
		public static string GET_STRING_WARFARE_POPUP_ENEMY_INFO_WAVE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_POPUP_ENEMY_INFO_WAVE", false);
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06004101 RID: 16641 RVA: 0x0014CE7A File Offset: 0x0014B07A
		public static string GET_STRING_WARFARE_POPUP_ENEMY_INFO_WAVE_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_POPUP_ENEMY_INFO_WAVE_ONE_PARAM", false);
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06004102 RID: 16642 RVA: 0x0014CE87 File Offset: 0x0014B087
		public static string GET_STRING_WARFARE_SUPPORTER_SUPPLY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_SUPPORTER_SUPPLY", false);
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06004103 RID: 16643 RVA: 0x0014CE94 File Offset: 0x0014B094
		public static string GET_STRING_WARFARE_SUPPORTER_REPAIR
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_SUPPORTER_REPAIR", false);
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06004104 RID: 16644 RVA: 0x0014CEA1 File Offset: 0x0014B0A1
		public static string GET_STRING_WARFARE_CANNOT_START_BECAUSE_NO_USER_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_CANNOT_START_BECAUSE_NO_USER_UNIT", false);
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06004105 RID: 16645 RVA: 0x0014CEAE File Offset: 0x0014B0AE
		public static string GET_STRING_WARFARE_CANNOT_FIND_RETRY_DATA
		{
			get
			{
				return NKCStringTable.GetString("SI_TOAST_REPEAT_UNABLE_BY_RECONNECT", false);
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06004106 RID: 16646 RVA: 0x0014CEBB File Offset: 0x0014B0BB
		public static string GET_STRING_WARFARE_CANNOT_PAUSE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_CANNOT_PAUSE", false);
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06004107 RID: 16647 RVA: 0x0014CEC8 File Offset: 0x0014B0C8
		public static string GET_STRING_WARFARE_RECOVERY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_RECOVERY", false);
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06004108 RID: 16648 RVA: 0x0014CED5 File Offset: 0x0014B0D5
		public static string GET_STRING_WARFARE_RECOVERY_COUNT_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_RECOVERY_COUNT_ONE_PARAM", false);
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06004109 RID: 16649 RVA: 0x0014CEE2 File Offset: 0x0014B0E2
		public static string GET_STRING_WARFARE_RECOVERY_NO_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_RECOVERY_NO_UNIT", false);
			}
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x0600410A RID: 16650 RVA: 0x0014CEEF File Offset: 0x0014B0EF
		public static string GET_STRING_WARFARE_RECOVERY_NO_TILE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_RECOVERY_NO_TILE", false);
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x0600410B RID: 16651 RVA: 0x0014CEFC File Offset: 0x0014B0FC
		public static string GET_STRING_WARFARE_RECOVERABLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_RECOVERABLE", false);
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x0600410C RID: 16652 RVA: 0x0014CF09 File Offset: 0x0014B109
		public static string GET_STRING_WARFARE_RECOVERY_CONFIRM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_RECOVERY_CONFIRM", false);
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x0600410D RID: 16653 RVA: 0x0014CF16 File Offset: 0x0014B116
		public static string GET_STRING_WARFARE_GAME_HUD_RESTORE_LIMIT_DESC_DAY_02
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_GAME_HUD_RESTORE_LIMIT_DESC_DAY_02", false);
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x0600410E RID: 16654 RVA: 0x0014CF23 File Offset: 0x0014B123
		public static string GET_STRING_WARFARE_GAME_HUD_RESTORE_LIMIT_DESC_WEEK_02
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_GAME_HUD_RESTORE_LIMIT_DESC_WEEK_02", false);
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x0600410F RID: 16655 RVA: 0x0014CF30 File Offset: 0x0014B130
		public static string GET_STRING_WARFARE_GAME_HUD_RESTORE_LIMIT_DESC_MONTH_02
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_GAME_HUD_RESTORE_LIMIT_DESC_MONTH_02", false);
			}
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06004110 RID: 16656 RVA: 0x0014CF3D File Offset: 0x0014B13D
		public static string GET_STRING_WARFARE_GAEM_HUD_RESTORE_LIMIT_OVER_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_GAEM_HUD_RESTORE_LIMIT_OVER_DESC", false);
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06004111 RID: 16657 RVA: 0x0014CF4A File Offset: 0x0014B14A
		public static string GET_STRING_WARFARE_GAME_HUD_OPERATION_START
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_GAME_HUD_OPERATION_START", false);
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06004112 RID: 16658 RVA: 0x0014CF57 File Offset: 0x0014B157
		public static string GET_STRING_WARFARE_GAME_HUD_OPERATION_RESTORE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_GAME_HUD_OPERATION_RESTORE", false);
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06004113 RID: 16659 RVA: 0x0014CF64 File Offset: 0x0014B164
		public static string GET_STRING_WARFARE_SUPPLY_DESC_MULTIPLY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MULTIPLY_OPERATION_SUPPLY_POPUP_DESC", false);
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06004114 RID: 16660 RVA: 0x0014CF71 File Offset: 0x0014B171
		public static string GET_STRING_WARFARE_WARNING_GIVE_UP_MULTIPLY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MULTIPLY_OPERATION_WARFARE_OUT_POPUP_DESC", false);
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06004115 RID: 16661 RVA: 0x0014CF7E File Offset: 0x0014B17E
		public static string GET_STRING_DIVE_RESET
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DIVE_RESET", false);
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06004116 RID: 16662 RVA: 0x0014CF8B File Offset: 0x0014B18B
		public static string GET_STRING_DIVE_RESET_CONFIRM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DIVE_RESET_CONFIRM", false);
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06004117 RID: 16663 RVA: 0x0014CF98 File Offset: 0x0014B198
		public static string GET_STRING_DIVE_READY_FIRST_REWARD
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DIVE_READY_FIRST_REWARD", false);
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06004118 RID: 16664 RVA: 0x0014CFA5 File Offset: 0x0014B1A5
		public static string GET_STRING_DIVE_READY_EXPLORE_REWARD
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DIVE_READY_EXPLORE_REWARD", false);
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06004119 RID: 16665 RVA: 0x0014CFB2 File Offset: 0x0014B1B2
		public static string GET_STRING_DIVE_READY_SAFE_MINING_REWARD
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_DIVE_SAFE_MINING_REWARD", false);
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x0600411A RID: 16666 RVA: 0x0014CFBF File Offset: 0x0014B1BF
		public static string GET_STRING_DIVE_ARTIFACT_EXCHANGE_TOTAL_GET_ITEM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DIVE_ARTIFACT_EXCHANGE_TOTAL_GET_ITEM", false);
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x0600411B RID: 16667 RVA: 0x0014CFCC File Offset: 0x0014B1CC
		public static string GET_STRING_DIVE_REMAIN_TIME_TO_RESET
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DIVE_REMAIN_TIME_TO_RESET", false);
			}
		}

		// Token: 0x0600411C RID: 16668 RVA: 0x0014CFDC File Offset: 0x0014B1DC
		public static string GetDiveArtifactTotalViewDesc(List<int> lstArtifact)
		{
			NKCUtilString.m_sStringBuilder.Clear();
			if (lstArtifact.Count <= 0)
			{
				return NKCUtilString.m_sStringBuilder.ToString();
			}
			List<NKMDiveArtifactTemplet> list = new List<NKMDiveArtifactTemplet>();
			for (int i = 0; i < lstArtifact.Count; i++)
			{
				NKMDiveArtifactTemplet nkmdiveArtifactTemplet = NKMDiveArtifactTemplet.Find(lstArtifact[i]);
				if (nkmdiveArtifactTemplet != null)
				{
					list.Add(nkmdiveArtifactTemplet);
				}
			}
			list.Sort(new NKCUtilString.CompNKMDiveArtifactTemplet());
			int num = Enum.GetNames(typeof(NKM_DIVE_ARTIFACT_CATEGORY)).Length;
			List<bool> list2 = new List<bool>();
			for (int j = 0; j < num; j++)
			{
				list2.Add(false);
			}
			int num2 = -1;
			for (int k = 0; k < list.Count; k++)
			{
				NKMDiveArtifactTemplet nkmdiveArtifactTemplet2 = list[k];
				if (nkmdiveArtifactTemplet2 != null && nkmdiveArtifactTemplet2.BattleConditionID > 0)
				{
					int category = (int)nkmdiveArtifactTemplet2.Category;
					if (num2 != -1 && num2 != category)
					{
						NKCUtilString.m_sStringBuilder.AppendLine();
					}
					num2 = category;
					if (!list2[category])
					{
						list2[category] = true;
						if (category == 0)
						{
							NKCUtilString.m_sStringBuilder.AppendLine(NKCStringTable.GetString("SI_DP_DIVE_ARTIFACT_TOTAL_VIEW_DESC_CATEGORY_CHECK_NDAC_ALL", false));
						}
						else if (category == 1)
						{
							NKCUtilString.m_sStringBuilder.AppendLine(NKCStringTable.GetString("SI_DP_DIVE_ARTIFACT_TOTAL_VIEW_DESC_CATEGORY_CHECK_NDAC_COUNTER", false));
						}
						else if (category == 2)
						{
							NKCUtilString.m_sStringBuilder.AppendLine(NKCStringTable.GetString("SI_DP_DIVE_ARTIFACT_TOTAL_VIEW_DESC_CATEGORY_CHECK_NDAC_SOLDIER", false));
						}
						else if (category == 3)
						{
							NKCUtilString.m_sStringBuilder.AppendLine(NKCStringTable.GetString("SI_DP_DIVE_ARTIFACT_TOTAL_VIEW_DESC_CATEGORY_CHECK_NDAC_MECHANIC", false));
						}
						else if (category == 4)
						{
							NKCUtilString.m_sStringBuilder.AppendLine(NKCStringTable.GetString("SI_DP_DIVE_ARTIFACT_TOTAL_VIEW_DESC_CATEGORY_CHECK_NDAC_ETC", false));
						}
					}
					NKCUtilString.m_sStringBuilder.AppendLine("·" + nkmdiveArtifactTemplet2.ArtifactMiscDesc_2_Translated);
				}
			}
			return NKCUtilString.m_sStringBuilder.ToString();
		}

		// Token: 0x0600411D RID: 16669 RVA: 0x0014D1A0 File Offset: 0x0014B3A0
		public static string GetGuildArtifactTotalViewDesc(List<int> lstArtifact)
		{
			NKCUtilString.m_sStringBuilder.Clear();
			if (lstArtifact.Count <= 0)
			{
				return NKCUtilString.m_sStringBuilder.ToString();
			}
			List<GuildDungeonArtifactTemplet> list = new List<GuildDungeonArtifactTemplet>();
			for (int i = 0; i < lstArtifact.Count; i++)
			{
				GuildDungeonArtifactTemplet artifactTemplet = GuildDungeonTempletManager.GetArtifactTemplet(lstArtifact[i]);
				if (artifactTemplet != null)
				{
					list.Add(artifactTemplet);
				}
			}
			list.Sort(new NKCUtilString.CompGuildDungeonArtifactTemplet());
			int num = Enum.GetNames(typeof(NKM_DIVE_ARTIFACT_CATEGORY)).Length;
			List<bool> list2 = new List<bool>();
			for (int j = 0; j < num; j++)
			{
				list2.Add(false);
			}
			int num2 = -1;
			for (int k = 0; k < list.Count; k++)
			{
				GuildDungeonArtifactTemplet guildDungeonArtifactTemplet = list[k];
				if (guildDungeonArtifactTemplet != null)
				{
					int category = (int)guildDungeonArtifactTemplet.GetCategory();
					if (num2 != -1 && num2 != category)
					{
						NKCUtilString.m_sStringBuilder.AppendLine();
					}
					num2 = category;
					if (!list2[category])
					{
						list2[category] = true;
						if (category == 0)
						{
							NKCUtilString.m_sStringBuilder.AppendLine(NKCStringTable.GetString("SI_DP_DIVE_ARTIFACT_TOTAL_VIEW_DESC_CATEGORY_CHECK_NDAC_ALL", false));
						}
						else if (category == 1)
						{
							NKCUtilString.m_sStringBuilder.AppendLine(NKCStringTable.GetString("SI_DP_DIVE_ARTIFACT_TOTAL_VIEW_DESC_CATEGORY_CHECK_NDAC_COUNTER", false));
						}
						else if (category == 2)
						{
							NKCUtilString.m_sStringBuilder.AppendLine(NKCStringTable.GetString("SI_DP_DIVE_ARTIFACT_TOTAL_VIEW_DESC_CATEGORY_CHECK_NDAC_SOLDIER", false));
						}
						else if (category == 3)
						{
							NKCUtilString.m_sStringBuilder.AppendLine(NKCStringTable.GetString("SI_DP_DIVE_ARTIFACT_TOTAL_VIEW_DESC_CATEGORY_CHECK_NDAC_MECHANIC", false));
						}
						else if (category == 4)
						{
							NKCUtilString.m_sStringBuilder.AppendLine(NKCStringTable.GetString("SI_DP_DIVE_ARTIFACT_TOTAL_VIEW_DESC_CATEGORY_CHECK_NDAC_ETC", false));
						}
					}
					NKCUtilString.m_sStringBuilder.AppendLine("·" + guildDungeonArtifactTemplet.GetDescShort());
				}
			}
			return NKCUtilString.m_sStringBuilder.ToString();
		}

		// Token: 0x0600411E RID: 16670 RVA: 0x0014D354 File Offset: 0x0014B554
		public static void GetDiveEventText(NKM_DIVE_EVENT_TYPE type, out string title, out string subTitle)
		{
			title = "";
			subTitle = "";
			switch (type)
			{
			case NKM_DIVE_EVENT_TYPE.NDET_DUNGEON:
			case NKM_DIVE_EVENT_TYPE.NDET_DUNGEON_BOSS:
			case NKM_DIVE_EVENT_TYPE.NDET_BLANK:
			case NKM_DIVE_EVENT_TYPE.NDET_LOSTSHIP_RANDOM:
			case NKM_DIVE_EVENT_TYPE.NDET_BEACON_RANDOM:
				break;
			case NKM_DIVE_EVENT_TYPE.NDET_ITEM:
				title = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_ITEM_TITLE", false);
				subTitle = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_ITEM_SUBTITLE", false);
				return;
			case NKM_DIVE_EVENT_TYPE.NDET_UNIT:
				title = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_UNIT_TITLE", false);
				subTitle = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_UNIT_SUBTITLE", false);
				return;
			case NKM_DIVE_EVENT_TYPE.NDET_REPAIR:
				title = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_REPAIR_TITLE", false);
				subTitle = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_REPAIR_SUBTITLE", false);
				return;
			case NKM_DIVE_EVENT_TYPE.NDET_SUPPLY:
				title = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_SUPPLY_TITLE", false);
				subTitle = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_SUPPLY_SUBTITLE", false);
				return;
			case NKM_DIVE_EVENT_TYPE.NDET_LOSTSHIP_ITEM:
				title = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_LOSTSHIP_ITEM_TITLE", false);
				subTitle = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_LOSTSHIP_ITEM_SUBTITLE", false);
				return;
			case NKM_DIVE_EVENT_TYPE.NDET_LOSTSHIP_UNIT:
				title = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_LOSTSHIP_UNIT_TITLE", false);
				subTitle = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_LOSTSHIP_UNIT_SUBTITLE", false);
				return;
			case NKM_DIVE_EVENT_TYPE.NDET_LOSTSHIP_REPAIR:
				title = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_LOSTSHIP_REPAIR_TITLE", false);
				subTitle = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_LOSTSHIP_REPAIR_SUBTITLE", false);
				return;
			case NKM_DIVE_EVENT_TYPE.NDET_LOSTSHIP_SUPPLY:
				title = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_LOSTSHIP_SUPPLY_TITLE", false);
				subTitle = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_LOSTSHIP_SUPPLY_SUBTITLE", false);
				return;
			case NKM_DIVE_EVENT_TYPE.NDET_BEACON_DUNGEON:
				title = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_BEACON_DUNGEON_TITLE", false);
				subTitle = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_BEACON_DUNGEON_SUBTITLE", false);
				return;
			case NKM_DIVE_EVENT_TYPE.NDET_BEACON_BLANK:
				title = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_BEACON_BLANK_TITLE", false);
				subTitle = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_BEACON_BLANK_SUBTITLE", false);
				return;
			case NKM_DIVE_EVENT_TYPE.NDET_BEACON_ITEM:
				title = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_BEACON_ITEM_TITLE", false);
				subTitle = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_BEACON_ITEM_SUBTITLE", false);
				return;
			case NKM_DIVE_EVENT_TYPE.NDET_BEACON_UNIT:
				title = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_BEACON_UNIT_TITLE", false);
				subTitle = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_BEACON_UNIT_SUBTITLE", false);
				return;
			case NKM_DIVE_EVENT_TYPE.NDET_BEACON_STORM:
				title = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_BEACON_STORM_TITLE", false);
				subTitle = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_BEACON_STORM_SUBTITLE", false);
				return;
			case NKM_DIVE_EVENT_TYPE.NDET_ARTIFACT:
				title = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_BLANK_TITLE", false);
				subTitle = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_BLANK_SUBTITLE", false);
				break;
			default:
				return;
			}
		}

		// Token: 0x0600411F RID: 16671 RVA: 0x0014D540 File Offset: 0x0014B740
		internal static string GetStatusName(NKM_UNIT_STATUS_EFFECT status)
		{
			NKMUnitStatusTemplet nkmunitStatusTemplet = NKMUnitStatusTemplet.Find(status);
			if (nkmunitStatusTemplet != null)
			{
				return NKCStringTable.GetString(nkmunitStatusTemplet.m_StatusStrID, false);
			}
			return "";
		}

		// Token: 0x06004120 RID: 16672 RVA: 0x0014D56C File Offset: 0x0014B76C
		internal static string GetStatusImmuneName(NKM_UNIT_STATUS_EFFECT status)
		{
			string statusName = NKCUtilString.GetStatusName(status);
			if (string.IsNullOrEmpty(statusName))
			{
				return "";
			}
			return NKCStringTable.GetString("SI_BATTLE_IMMUNE_COMMON", new object[]
			{
				statusName
			});
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06004121 RID: 16673 RVA: 0x0014D5A2 File Offset: 0x0014B7A2
		public static string GET_STRING_DIVE_RESULT_GAME_TIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DIVE_RESULT_GAME_TIP1", false) + NKCStringTable.GetString("SI_DP_DIVE_RESULT_GAME_TIP2", false) + NKCStringTable.GetString("SI_DP_DIVE_RESULT_GAME_TIP3", false);
			}
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06004122 RID: 16674 RVA: 0x0014D5CA File Offset: 0x0014B7CA
		public static string GET_STRING_DIVE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DIVE", false);
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06004123 RID: 16675 RVA: 0x0014D5D7 File Offset: 0x0014B7D7
		public static string GET_STRING_DIVE_READY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DIVE_READY", false);
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06004124 RID: 16676 RVA: 0x0014D5E4 File Offset: 0x0014B7E4
		public static string GET_STRING_DIVE_GIVE_UP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DIVE_GIVE_UP", false);
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06004125 RID: 16677 RVA: 0x0014D5F1 File Offset: 0x0014B7F1
		public static string GET_STRING_DIVE_WARNING_SUPPLY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DIVE_NONE_BULLET_POPUP_DESC", false);
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06004126 RID: 16678 RVA: 0x0014D5FE File Offset: 0x0014B7FE
		public static string GET_STRING_DIVE_FLOOR_LEVEL_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DIVE_FLOOR_LEVEL_ONE_PARAM", false);
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06004127 RID: 16679 RVA: 0x0014D60B File Offset: 0x0014B80B
		public static string GET_STRING_DIVE_LEFT_COUNT_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DIVE_LEFT_COUNT_ONE_PARAM", false);
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06004128 RID: 16680 RVA: 0x0014D618 File Offset: 0x0014B818
		public static string GET_STRING_DIVE_SQUAD_NO_EXIST_HP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DIVE_SQUAD_NO_EXIST_HP", false);
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06004129 RID: 16681 RVA: 0x0014D625 File Offset: 0x0014B825
		public static string GET_STRING_DIVE_SQUAD_NO_EXIST_SUPPLY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DIVE_SQUAD_NO_EXIST_SUPPLY", false);
			}
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x0600412A RID: 16682 RVA: 0x0014D632 File Offset: 0x0014B832
		public static string GET_STRING_DIVE_EVENT_POPUP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DIVE_EVENT_POPUP", false);
			}
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x0600412B RID: 16683 RVA: 0x0014D63F File Offset: 0x0014B83F
		public static string GET_STRING_DIVE_NO_SELECT_DECK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DIVE_NO_SELECT_DECK", false);
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x0600412C RID: 16684 RVA: 0x0014D64C File Offset: 0x0014B84C
		public static string GET_STRING_DIVE_NO_EXIST_COST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DIVE_NO_EXIST_COST", false);
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x0600412D RID: 16685 RVA: 0x0014D659 File Offset: 0x0014B859
		public static string GET_STRING_DIVE_NO_ENOUGH_DECK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DIVE_NO_ENOUGH_DECK", false);
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x0600412E RID: 16686 RVA: 0x0014D666 File Offset: 0x0014B866
		public static string GET_STRING_SELECT_SQUAD
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SELECT_SQUAD", false);
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x0600412F RID: 16687 RVA: 0x0014D673 File Offset: 0x0014B873
		public static string GET_STRING_DIVE_GIVE_UP_AND_START
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DIVE_GIVE_UP_AND_START", false);
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06004130 RID: 16688 RVA: 0x0014D680 File Offset: 0x0014B880
		public static string GET_STRING_DIVE_NO_EXIST_SQUAD
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DIVE_NO_EXIST_SQUAD", false);
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06004131 RID: 16689 RVA: 0x0014D68D File Offset: 0x0014B88D
		public static string GET_STRING_DIVE_GO
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DIVE_GO", false);
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06004132 RID: 16690 RVA: 0x0014D69A File Offset: 0x0014B89A
		public static string GET_STRING_DIVE_ARTIFACT_GET_SKIP_CHECK_REQ
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DIVE_ARTIFACT_GET_SKIP_CHECK_REQ", false);
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06004133 RID: 16691 RVA: 0x0014D6A7 File Offset: 0x0014B8A7
		public static string GET_STRING_DIVE_ARTIFACT_ALREADY_FULL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DIVE_ARTIFACT_ALREADY_FULL", false);
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06004134 RID: 16692 RVA: 0x0014D6B4 File Offset: 0x0014B8B4
		public static string GET_STRING_DIVE_GIVE_UP_RECOMMEND
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DIVE_RETREAT_POPUP_DESC", false);
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06004135 RID: 16693 RVA: 0x0014D6C1 File Offset: 0x0014B8C1
		public static string GET_STRING_DIVE_SAFE_MINING
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_DIVE_SAFE_MINING", false);
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06004136 RID: 16694 RVA: 0x0014D6CE File Offset: 0x0014B8CE
		public static string GET_STRING_DIVE_SAFE_MINING_START
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_DIVE_SAFE_MINING_START", false);
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06004137 RID: 16695 RVA: 0x0014D6DB File Offset: 0x0014B8DB
		public static string GET_STRING_DIVE_SAFE_MINING_ON
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_DIVE_SAFE_MINING_ON", false);
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06004138 RID: 16696 RVA: 0x0014D6E8 File Offset: 0x0014B8E8
		public static string GET_STRING_DIVE_SAFE_MINING_RESULT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_DIVE_SAFE_MINING_RESULT", false);
			}
		}

		// Token: 0x06004139 RID: 16697 RVA: 0x0014D6F8 File Offset: 0x0014B8F8
		public static string GetDGMissionText(DUNGEON_GAME_MISSION_TYPE missionType, int missionValue)
		{
			switch (missionType)
			{
			case DUNGEON_GAME_MISSION_TYPE.DGMT_CLEAR:
				return NKCStringTable.GetString("SI_DP_DG_MISSION_TEXT_GAME_MISSION_TYPE_DGMT_CLEAR", false);
			case DUNGEON_GAME_MISSION_TYPE.DGMT_TIME:
				if (missionValue % 60 > 0)
				{
					return string.Format(NKCStringTable.GetString("SI_DP_DG_MISSION_TEXT_GAME_MISSION_TYPE_DGMT_TIME_HAVE_SEC", false), missionValue / 60, missionValue % 60);
				}
				return string.Format(NKCStringTable.GetString("SI_DP_DG_MISSION_TEXT_GAME_MISSION_TYPE_DGMT_TIME_ELSE", false), missionValue / 60);
			case DUNGEON_GAME_MISSION_TYPE.DGMT_COST:
				return string.Format(NKCStringTable.GetString("SI_DP_DG_MISSION_TEXT_GAME_MISSION_TYPE_DGMT_COST", false), missionValue);
			case DUNGEON_GAME_MISSION_TYPE.DGMT_RESPAWN:
				return string.Format(NKCStringTable.GetString("SI_DP_DG_MISSION_TEXT_GAME_MISSION_TYPE_DGMT_RESPAWN", false), missionValue);
			case DUNGEON_GAME_MISSION_TYPE.DGMT_SHIP_HP_DAMAGE:
				return string.Format(NKCStringTable.GetString("SI_DP_DG_MISSION_TEXT_GAME_MISSION_TYPE_DGMT_SHIP_HP_DAMAGE", false), missionValue);
			case DUNGEON_GAME_MISSION_TYPE.DGMT_DECKCOUNT_SOLDIER:
				return string.Format(NKCStringTable.GetString("SI_DP_DG_MISSION_TEXT_GAME_MISSION_TYPE_DGMT_DECKCOUNT_SOLDIER", false), missionValue);
			case DUNGEON_GAME_MISSION_TYPE.DGMT_DECKCOUNT_MECHANIC:
				return string.Format(NKCStringTable.GetString("SI_DP_DG_MISSION_TEXT_GAME_MISSION_TYPE_DGMT_DECKCOUNT_MECHANIC", false), missionValue);
			case DUNGEON_GAME_MISSION_TYPE.DGMT_DECKCOUNT_COUNTER:
				return string.Format(NKCStringTable.GetString("SI_DP_DG_MISSION_TEXT_GAME_MISSION_TYPE_DGMT_DECKCOUNT_COUNTER", false), missionValue);
			case DUNGEON_GAME_MISSION_TYPE.DGMT_DECKCOUNT_DEFENDER:
				return string.Format(NKCStringTable.GetString("SI_DP_DG_MISSION_TEXT_GAME_MISSION_TYPE_DGMT_DECKCOUNT_DEFENDER", false), missionValue);
			case DUNGEON_GAME_MISSION_TYPE.DGMT_DECKCOUNT_STRIKER:
				return string.Format(NKCStringTable.GetString("SI_DP_DG_MISSION_TEXT_GAME_MISSION_TYPE_DGMT_DECKCOUNT_STRIKER", false), missionValue);
			case DUNGEON_GAME_MISSION_TYPE.DGMT_DECKCOUNT_RANGER:
				return string.Format(NKCStringTable.GetString("SI_DP_DG_MISSION_TEXT_GAME_MISSION_TYPE_DGMT_DECKCOUNT_RANGER", false), missionValue);
			case DUNGEON_GAME_MISSION_TYPE.DGMT_DECKCOUNT_SNIPER:
				return string.Format(NKCStringTable.GetString("SI_DP_DG_MISSION_TEXT_GAME_MISSION_TYPE_DGMT_DECKCOUNT_SNIPER", false), missionValue);
			case DUNGEON_GAME_MISSION_TYPE.DGMT_DECKCOUNT_TOWER:
				return string.Format(NKCStringTable.GetString("SI_DP_DG_MISSION_TEXT_GAME_MISSION_TYPE_DGMT_DECKCOUNT_TOWER", false), missionValue);
			case DUNGEON_GAME_MISSION_TYPE.DGMT_DECKCOUNT_SIEGE:
				return string.Format(NKCStringTable.GetString("SI_DP_DG_MISSION_TEXT_GAME_MISSION_TYPE_DGMT_DECKCOUNT_SIEGE", false), missionValue);
			case DUNGEON_GAME_MISSION_TYPE.DGMT_DECKCOUNT_SUPPORTER:
				return string.Format(NKCStringTable.GetString("SI_DP_DG_MISSION_TEXT_GAME_MISSION_TYPE_DGMT_DECKCOUNT_SUPPORTER", false), missionValue);
			default:
				return "";
			}
		}

		// Token: 0x0600413A RID: 16698 RVA: 0x0014D8CE File Offset: 0x0014BACE
		public static string GetDailyDungeonLVDesc(int stageIndex)
		{
			return string.Format(NKCStringTable.GetString("SI_DP_DAILY_DUNGEON_LV_DESC", false), stageIndex);
		}

		// Token: 0x0600413B RID: 16699 RVA: 0x0014D8E8 File Offset: 0x0014BAE8
		public static string GetDGMissionTextWithProgress(NKMGame game, DUNGEON_GAME_MISSION_TYPE missionType, int missionValue)
		{
			if (missionType == DUNGEON_GAME_MISSION_TYPE.DGMT_NONE)
			{
				return string.Empty;
			}
			string dgmissionText = NKCUtilString.GetDGMissionText(missionType, missionValue);
			int currentMissionValue = NKMDungeonManager.GetCurrentMissionValue(game, missionType);
			if (missionType == DUNGEON_GAME_MISSION_TYPE.DGMT_SHIP_HP_DAMAGE)
			{
				return string.Format("{0} ({1}/{2} %)", dgmissionText, currentMissionValue, missionValue);
			}
			return string.Format("{0} ({1}/{2})", dgmissionText, currentMissionValue, missionValue);
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x0600413C RID: 16700 RVA: 0x0014D941 File Offset: 0x0014BB41
		public static string GET_STRING_MENU_NAME_DUNGEON_POPUP
		{
			get
			{
				return "DungeonPopup";
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x0600413D RID: 16701 RVA: 0x0014D948 File Offset: 0x0014BB48
		public static string GET_STRING_ENEMY_LIST_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ENEMY_LIST_TITLE", false);
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x0600413E RID: 16702 RVA: 0x0014D955 File Offset: 0x0014BB55
		public static string GET_STRING_ENEMY_LIST_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ENEMY_LIST_DESC", false);
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x0600413F RID: 16703 RVA: 0x0014D962 File Offset: 0x0014BB62
		public static string GET_STRING_OPERATION_POPUP_BUTTON
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OPERATION_POPUP_BUTTON", false);
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06004140 RID: 16704 RVA: 0x0014D96F File Offset: 0x0014BB6F
		public static string GET_STRING_OPERATION_POPUP_BUTTON_PLAYING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OPERATION_POPUP_BUTTON_PLAYING", false);
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06004141 RID: 16705 RVA: 0x0014D97C File Offset: 0x0014BB7C
		public static string GET_STRING_DUNGEON_LEVEL_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DUNGEON_LEVEL_ONE_PARAM", false);
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06004142 RID: 16706 RVA: 0x0014D989 File Offset: 0x0014BB89
		public static string GET_STRING_DUNGEON_MISSION_COST_FAIL_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DUNGEON_MISSION_COST_FAIL_ONE_PARAM", false);
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06004143 RID: 16707 RVA: 0x0014D996 File Offset: 0x0014BB96
		public static string GET_STRING_DUNGEON_MISSION_COST_WARNING_THREE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DUNGEON_MISSION_COST_WARNING_THREE_PARAM", false);
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06004144 RID: 16708 RVA: 0x0014D9A3 File Offset: 0x0014BBA3
		public static string GET_STRING_DUNGEON_MISSION_COST_THREE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DUNGEON_MISSION_COST_THREE_PARAM", false);
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06004145 RID: 16709 RVA: 0x0014D9B0 File Offset: 0x0014BBB0
		public static string GET_STRING_DUNGEON_MISSION_TIME_FAIL_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DUNGEON_MISSION_TIME_FAIL_ONE_PARAM", false);
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06004146 RID: 16710 RVA: 0x0014D9BD File Offset: 0x0014BBBD
		public static string GET_STRING_DUNGEON_MISSION_TIME_WARNING_THREE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DUNGEON_MISSION_TIME_WARNING_THREE_PARAM", false);
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06004147 RID: 16711 RVA: 0x0014D9CA File Offset: 0x0014BBCA
		public static string GET_STRING_DUNGEON_MISSION_TIME_THREE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DUNGEON_MISSION_TIME_THREE_PARAM", false);
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06004148 RID: 16712 RVA: 0x0014D9D7 File Offset: 0x0014BBD7
		public static string GET_STRING_DUNGEON_MISSION_HP_FAIL_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DUNGEON_MISSION_HP_FAIL_ONE_PARAM", false);
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06004149 RID: 16713 RVA: 0x0014D9E4 File Offset: 0x0014BBE4
		public static string GET_STRING_DUNGEON_MISSION_HP_WARNING_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DUNGEON_MISSION_HP_WARNING_TWO_PARAM", false);
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x0600414A RID: 16714 RVA: 0x0014D9F1 File Offset: 0x0014BBF1
		public static string GET_STRING_DUNGEON_MISSION_HP_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DUNGEON_MISSION_HP_TWO_PARAM", false);
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x0600414B RID: 16715 RVA: 0x0014D9FE File Offset: 0x0014BBFE
		public static string GET_STRING_POPUP_DUNGEON_ENTER_LIMIT_DESC_DAY_02
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_POPUP_DUNGEON_ENTER_LIMIT_DESC_ACTIVE_02", false);
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x0600414C RID: 16716 RVA: 0x0014DA0B File Offset: 0x0014BC0B
		public static string GET_STRING_POPUP_DUNGEON_ENTER_LIMIT_DESC_WEEK_02
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_POPUP_DUNGEON_ENTER_LIMIT_DESC_WEEK_02", false);
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x0600414D RID: 16717 RVA: 0x0014DA18 File Offset: 0x0014BC18
		public static string GET_STRING_POPUP_DUNGEON_ENTER_LIMIT_DESC_MONTH_02
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_POPUP_DUNGEON_ENTER_LIMIT_DESC_MONTH_02", false);
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x0600414E RID: 16718 RVA: 0x0014DA25 File Offset: 0x0014BC25
		public static string GET_STRING_ENTER_LIMIT_OVER
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ENTER_LIMIT_OVER", false);
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x0600414F RID: 16719 RVA: 0x0014DA32 File Offset: 0x0014BC32
		public static string GET_STRING_ACT_DUNGEON_SLOT_FIGHT_POWER_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ACT_DUNGEON_SLOT_FIGHT_POWER_DESC", false);
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06004150 RID: 16720 RVA: 0x0014DA3F File Offset: 0x0014BC3F
		public static string GET_STRING_POPUP_DUNGEON_GET_MAIN_REWARD
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_POPUP_DUNGEON_GET_MAIN_REWARD", false);
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06004151 RID: 16721 RVA: 0x0014DA4C File Offset: 0x0014BC4C
		public static string GET_STRING_INGAME_UNIT_COUNT_MAX_SAME_TIME
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INGAME_UNIT_COUNT_MAX_SAME_TIME", false);
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06004152 RID: 16722 RVA: 0x0014DA59 File Offset: 0x0014BC59
		public static string GET_STRING_INGAME_USER_A_NAME_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INGAME_USER_A_NAME_TWO_PARAM", false);
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06004153 RID: 16723 RVA: 0x0014DA66 File Offset: 0x0014BC66
		public static string GET_STRING_INGAME_USER_B_NAME_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INGAME_USER_B_NAME_TWO_PARAM", false);
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06004154 RID: 16724 RVA: 0x0014DA73 File Offset: 0x0014BC73
		public static string GET_STRING_INGAME_USER_B_LEVEL_ONE_PARAM
		{
			get
			{
				return NKCUtilString.GET_STRING_LEVEL_ONE_PARAM;
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06004155 RID: 16725 RVA: 0x0014DA7A File Offset: 0x0014BC7A
		public static string GET_STRING_INGAME_TEAM_A_NAME_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INGAME_TEAM_A_NAME_TWO_PARAM", false);
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06004156 RID: 16726 RVA: 0x0014DA87 File Offset: 0x0014BC87
		public static string GET_STRING_INGAME_TEAM_B_NAME_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INGAME_TEAM_B_NAME_TWO_PARAM", false);
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06004157 RID: 16727 RVA: 0x0014DA94 File Offset: 0x0014BC94
		public static string GET_STRING_INGAME_RESPAWN_FAIL_STATE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INGAME_RESPAWN_FAIL_STATE", false);
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06004158 RID: 16728 RVA: 0x0014DAA1 File Offset: 0x0014BCA1
		public static string GET_STRING_INGAME_RESPAWN_FAIL_ALREADY_SPAWN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INGAME_RESPAWN_FAIL_ALREADY_SPAWN", false);
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06004159 RID: 16729 RVA: 0x0014DAAE File Offset: 0x0014BCAE
		public static string GET_STRING_INGAME_RESPAWN_FAIL_UNIT_DATA
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INGAME_RESPAWN_FAIL_UNIT_DATA", false);
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x0600415A RID: 16730 RVA: 0x0014DABB File Offset: 0x0014BCBB
		public static string GET_STRING_INGAME_RESPAWN_FAIL_COST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INGAME_RESPAWN_FAIL_COST", false);
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x0600415B RID: 16731 RVA: 0x0014DAC8 File Offset: 0x0014BCC8
		public static string GET_STRING_INGAME_RESPAWN_FAIL_MAP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INGAME_RESPAWN_FAIL_MAP", false);
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x0600415C RID: 16732 RVA: 0x0014DAD5 File Offset: 0x0014BCD5
		public static string GET_STRING_INGAME_SHIP_SKILL_FAIL_STATE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INGAME_SHIP_SKILL_FAIL_STATE", false);
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x0600415D RID: 16733 RVA: 0x0014DAE2 File Offset: 0x0014BCE2
		public static string GET_STRING_INGAME_SHIP_SKILL_FAIL_DIE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INGAME_SHIP_SKILL_FAIL_DIE", false);
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x0600415E RID: 16734 RVA: 0x0014DAEF File Offset: 0x0014BCEF
		public static string GET_STRING_INGAME_SHIP_SKILL_FAIL_USE_OTHER_SKILL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INGAME_SHIP_SKILL_FAIL_USE_OTHER_SKILL", false);
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x0600415F RID: 16735 RVA: 0x0014DAFC File Offset: 0x0014BCFC
		public static string GET_STRING_INGAME_SHIP_SKILL_FAIL_COOLTIME
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INGAME_SHIP_SKILL_FAIL_COOLTIME", false);
			}
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06004160 RID: 16736 RVA: 0x0014DB09 File Offset: 0x0014BD09
		public static string GET_STRING_INGAME_SHIP_SKILL_FAIL_SILENCE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INGAME_SHIP_SKILL_FAIL_SILENCE", false);
			}
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06004161 RID: 16737 RVA: 0x0014DB16 File Offset: 0x0014BD16
		public static string GET_STRING_INGAME_SHIP_SKILL_FAIL_SLEEP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INGAME_SHIP_SKILL_FAIL_SLEEP", false);
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06004162 RID: 16738 RVA: 0x0014DB23 File Offset: 0x0014BD23
		public static string GET_STRING_DANGER_MSG_UNIT_RESPAWN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DUNGEON_NO_UNIT_WARNING", false);
			}
		}

		// Token: 0x06004163 RID: 16739 RVA: 0x0014DB30 File Offset: 0x0014BD30
		public static string GetRemainTimeStringExWithoutEnd(DateTime endTime)
		{
			TimeSpan timeLeft = NKCSynchronizedTime.GetTimeLeft(endTime);
			if (timeLeft.TotalDays >= 1.0)
			{
				return string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_EX_DAYS_AND_HOURS", false), timeLeft.Days, timeLeft.Hours);
			}
			if (timeLeft.TotalHours >= 1.0)
			{
				return string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_EX_HOURS_AND_MINUTES", false), timeLeft.Hours, timeLeft.Minutes);
			}
			if (timeLeft.TotalMinutes >= 1.0)
			{
				return string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_EX_MINUTES", false), timeLeft.Minutes);
			}
			if (timeLeft.TotalSeconds >= 1.0)
			{
				return string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_EX_SECONDS", false), timeLeft.Seconds);
			}
			return NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_EX_END_SOON", false);
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06004164 RID: 16740 RVA: 0x0014DC23 File Offset: 0x0014BE23
		public static string GET_STRING_COUNT_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_COUNT_ONE_PARAM", false);
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06004165 RID: 16741 RVA: 0x0014DC30 File Offset: 0x0014BE30
		public static string GET_STRING_TIME
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TIME", false);
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06004166 RID: 16742 RVA: 0x0014DC3D File Offset: 0x0014BE3D
		public static string GET_STRING_TIME_PERIOD
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TIME_PERIOD", false);
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06004167 RID: 16743 RVA: 0x0014DC4A File Offset: 0x0014BE4A
		public static string GET_STRING_TIME_DAY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TIME_DAY", false);
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06004168 RID: 16744 RVA: 0x0014DC57 File Offset: 0x0014BE57
		public static string GET_STRING_TIME_HOUR
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TIME_HOUR", false);
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06004169 RID: 16745 RVA: 0x0014DC64 File Offset: 0x0014BE64
		public static string GET_STRING_TIME_MINUTE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TIME_MINUTE", false);
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x0600416A RID: 16746 RVA: 0x0014DC71 File Offset: 0x0014BE71
		public static string GET_STRING_TIME_SECOND
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TIME_SECOND", false);
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x0600416B RID: 16747 RVA: 0x0014DC7E File Offset: 0x0014BE7E
		public static string GET_STRING_TIME_DAY_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TIME_DAY_ONE_PARAM", false);
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x0600416C RID: 16748 RVA: 0x0014DC8B File Offset: 0x0014BE8B
		public static string GET_STRING_TIME_HOUR_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TIME_HOUR_ONE_PARAM", false);
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x0600416D RID: 16749 RVA: 0x0014DC98 File Offset: 0x0014BE98
		public static string GET_STRING_TIME_MINUTE_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TIME_MINUTE_ONE_PARAM", false);
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x0600416E RID: 16750 RVA: 0x0014DCA5 File Offset: 0x0014BEA5
		public static string GET_STRING_TIME_SECOND_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TIME_SECOND_ONE_PARAM", false);
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x0600416F RID: 16751 RVA: 0x0014DCB2 File Offset: 0x0014BEB2
		public static string GET_STRING_TIME_REMAIN_DAY_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TIME_REMAIN_DAY_ONE_PARAM", false);
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06004170 RID: 16752 RVA: 0x0014DCBF File Offset: 0x0014BEBF
		public static string GET_STRING_TIME_REMAIN_HOUR_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TIME_REMAIN_HOUR_ONE_PARAM", false);
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06004171 RID: 16753 RVA: 0x0014DCCC File Offset: 0x0014BECC
		public static string GET_STRING_TIME_REMAIN_MINUTE_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TIME_REMAIN_MINUTE_ONE_PARAM", false);
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06004172 RID: 16754 RVA: 0x0014DCD9 File Offset: 0x0014BED9
		public static string GET_STRING_TIME_REMAIN_SECOND_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TIME_REMAIN_SECOND_ONE_PARAM", false);
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06004173 RID: 16755 RVA: 0x0014DCE6 File Offset: 0x0014BEE6
		public static string GET_STRING_TIME_REMAIN_SHOP_DAY_OVER
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TIME_REMAIN_SHOP_DAY_OVER", false);
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06004174 RID: 16756 RVA: 0x0014DCF3 File Offset: 0x0014BEF3
		public static string GET_STRING_TIME_REMAIN_SHOP_EXPIRE_TODAY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TIME_REMAIN_SHOP_EXPIRE_TODAY", false);
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06004175 RID: 16757 RVA: 0x0014DD00 File Offset: 0x0014BF00
		public static string GET_STRING_TIME_CLOSING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TIME_CLOSING", false);
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06004176 RID: 16758 RVA: 0x0014DD0D File Offset: 0x0014BF0D
		public static string GET_STRING_TIME_DAY_AGO_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TIME_DAY_AGO_ONE_PARAM", false);
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06004177 RID: 16759 RVA: 0x0014DD1A File Offset: 0x0014BF1A
		public static string GET_STRING_TIME_HOUR_AGO_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TIME_HOUR_AGO_ONE_PARAM", false);
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06004178 RID: 16760 RVA: 0x0014DD27 File Offset: 0x0014BF27
		public static string GET_STRING_TIME_MINUTE_AGO_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TIME_MINUTE_AGO_ONE_PARAM", false);
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06004179 RID: 16761 RVA: 0x0014DD34 File Offset: 0x0014BF34
		public static string GET_STRING_TIME_SECOND_AGO_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TIME_SECOND_AGO_ONE_PARAM", false);
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x0600417A RID: 16762 RVA: 0x0014DD41 File Offset: 0x0014BF41
		public static string GET_STRING_TIME_A_SECOND_AGO
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TIME_A_SECOND_AGO", false);
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x0600417B RID: 16763 RVA: 0x0014DD4E File Offset: 0x0014BF4E
		public static string GET_STRING_DATE_FOUR_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DATE_FOUR_PARAM", false);
			}
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x0600417C RID: 16764 RVA: 0x0014DD5B File Offset: 0x0014BF5B
		public static string GET_STRING_TIME_NO_LIMIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TIME_NO_LIMIT", false);
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x0600417D RID: 16765 RVA: 0x0014DD68 File Offset: 0x0014BF68
		public static string GET_STRING_TIME_DAY_HOUR_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TIME_DAY_HOUR_TWO_PARAM", false);
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x0600417E RID: 16766 RVA: 0x0014DD75 File Offset: 0x0014BF75
		public static string GET_STRING_TIME_HOUR_MINUTE_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TIME_HOUR_MINUTE_TWO_PARAM", false);
			}
		}

		// Token: 0x0600417F RID: 16767 RVA: 0x0014DD84 File Offset: 0x0014BF84
		public static string GetDayString(DayOfWeek dayOfWeek)
		{
			switch (dayOfWeek)
			{
			case DayOfWeek.Sunday:
				return NKCStringTable.GetString("SI_DP_DAY_STRING_SUNDAY", false);
			case DayOfWeek.Monday:
				return NKCStringTable.GetString("SI_DP_DAY_STRING_MONDAY", false);
			case DayOfWeek.Tuesday:
				return NKCStringTable.GetString("SI_DP_DAY_STRING_TUESDAY", false);
			case DayOfWeek.Wednesday:
				return NKCStringTable.GetString("SI_DP_DAY_STRING_WEDNESDAY", false);
			case DayOfWeek.Thursday:
				return NKCStringTable.GetString("SI_DP_DAY_STRING_THURSDAY", false);
			case DayOfWeek.Friday:
				return NKCStringTable.GetString("SI_DP_DAY_STRING_FRIDAY", false);
			case DayOfWeek.Saturday:
				return NKCStringTable.GetString("SI_DP_DAY_STRING_SATURDAY", false);
			default:
				return "";
			}
		}

		// Token: 0x06004180 RID: 16768 RVA: 0x0014DE10 File Offset: 0x0014C010
		public static string GetTimeString(DateTime endTime, bool bSeconds = true)
		{
			TimeSpan timeLeft = NKCSynchronizedTime.GetTimeLeft(endTime);
			if (timeLeft.TotalSeconds <= 0.0)
			{
				return " - ";
			}
			if (timeLeft.Days > 0)
			{
				return string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_DAYS", false), timeLeft.Days);
			}
			if (timeLeft.Hours > 0)
			{
				return string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_HOURS", false), timeLeft.Hours);
			}
			if (timeLeft.Minutes > 0)
			{
				return string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_MINUTES", false), timeLeft.Minutes);
			}
			if (bSeconds)
			{
				return string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_SECONDS", false), timeLeft.Seconds);
			}
			return string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_MINUTES", false), 1);
		}

		// Token: 0x06004181 RID: 16769 RVA: 0x0014DEE8 File Offset: 0x0014C0E8
		public static string GetRemainTimeStringOneParam(DateTime endTime)
		{
			TimeSpan timeLeft = NKCSynchronizedTime.GetTimeLeft(endTime);
			if (timeLeft.TotalDays >= 1.0)
			{
				return string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_EX_DAYS", false), timeLeft.Days);
			}
			if (timeLeft.TotalHours >= 1.0)
			{
				return string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_EX_HOURS", false), timeLeft.Hours);
			}
			if (timeLeft.TotalMinutes >= 1.0)
			{
				return string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_EX_MINUTES", false), timeLeft.Minutes);
			}
			if (timeLeft.TotalSeconds >= 1.0)
			{
				return string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_EX_SECONDS", false), timeLeft.Seconds);
			}
			if (timeLeft.TotalSeconds > 0.0)
			{
				return NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_EX_END_SOON", false);
			}
			return NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_EX_END", false);
		}

		// Token: 0x06004182 RID: 16770 RVA: 0x0014DFE4 File Offset: 0x0014C1E4
		public static string GetRemainTimeStringEx(DateTime endTime)
		{
			TimeSpan timeLeft = NKCSynchronizedTime.GetTimeLeft(endTime);
			if (timeLeft.TotalDays >= 1.0)
			{
				return string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_EX_DAYS_AND_HOURS", false), timeLeft.Days, timeLeft.Hours);
			}
			if (timeLeft.TotalHours >= 1.0)
			{
				return string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_EX_HOURS_AND_MINUTES", false), timeLeft.Hours, timeLeft.Minutes);
			}
			if (timeLeft.TotalMinutes >= 1.0)
			{
				return string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_EX_MINUTES", false), timeLeft.Minutes);
			}
			if (timeLeft.TotalSeconds >= 1.0)
			{
				return string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_EX_SECONDS", false), timeLeft.Seconds);
			}
			if (timeLeft.TotalSeconds > 0.0)
			{
				return NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_EX_END_SOON", false);
			}
			return NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_EX_END", false);
		}

		// Token: 0x06004183 RID: 16771 RVA: 0x0014E0F5 File Offset: 0x0014C2F5
		public static string GetRemainTimeString(DateTime endTime, int maxWordCount)
		{
			return NKCUtilString.GetRemainTimeString(NKCSynchronizedTime.GetTimeLeft(endTime), maxWordCount, true);
		}

		// Token: 0x06004184 RID: 16772 RVA: 0x0014E104 File Offset: 0x0014C304
		public static string GetRemainTimeString(TimeSpan timeSpan, int maxWordCount, bool bShowSecond = true)
		{
			if (timeSpan.TotalSeconds <= 0.0)
			{
				return NKCUtilString.GET_STRING_QUIT;
			}
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			if (timeSpan.TotalDays >= 1.0 && num < maxWordCount)
			{
				stringBuilder.Append(string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_DAYS", false), timeSpan.Days));
				num++;
			}
			if (timeSpan.TotalHours >= 1.0 && num < maxWordCount)
			{
				if (num > 0)
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_HOURS", false), timeSpan.Hours));
				num++;
			}
			if (timeSpan.TotalMinutes >= 1.0 && num < maxWordCount)
			{
				if (num > 0)
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_MINUTES", false), timeSpan.Minutes));
				num++;
			}
			if (timeSpan.TotalSeconds >= 1.0 && bShowSecond && num < maxWordCount)
			{
				if (num > 0)
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_SECONDS", false), timeSpan.Seconds));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004185 RID: 16773 RVA: 0x0014E25C File Offset: 0x0014C45C
		public static string GetTimeStringFromSeconds(int second)
		{
			TimeSpan timeSpan = TimeSpan.FromSeconds((double)second);
			return string.Format("{0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
		}

		// Token: 0x06004186 RID: 16774 RVA: 0x0014E2A0 File Offset: 0x0014C4A0
		public static string GetTimeStringFromMinutes(int minutes)
		{
			TimeSpan timeSpan = TimeSpan.FromMinutes((double)minutes);
			return string.Format("{0:00}:{1:00}:00", timeSpan.Hours, timeSpan.Minutes);
		}

		// Token: 0x06004187 RID: 16775 RVA: 0x0014E2D7 File Offset: 0x0014C4D7
		public static string GetRemainTimeStringForGauntletWeekly()
		{
			if (!NKCSynchronizedTime.IsFinished(NKCPVPManager.WeekCalcStartDateUtc))
			{
				return NKCUtilString.GetRemainTimeStringEx(NKCPVPManager.WeekCalcStartDateUtc);
			}
			return NKCUtilString.GET_STRING_TIME_CLOSING;
		}

		// Token: 0x06004188 RID: 16776 RVA: 0x0014E2F5 File Offset: 0x0014C4F5
		public static string GetTimeSpanString(TimeSpan timeSpan)
		{
			return string.Format("{0:00}:{1:00}:{2:00}", timeSpan.Hours + timeSpan.Days * 24, timeSpan.Minutes, timeSpan.Seconds);
		}

		// Token: 0x06004189 RID: 16777 RVA: 0x0014E330 File Offset: 0x0014C530
		public static string GetTimeSpanStringMS(TimeSpan timeSpan)
		{
			return string.Format("{0:00}:{1:00}", timeSpan.Hours * 60 + timeSpan.Minutes, timeSpan.Seconds);
		}

		// Token: 0x0600418A RID: 16778 RVA: 0x0014E360 File Offset: 0x0014C560
		public static string GetTimeSpanStringDay(TimeSpan timeSpan)
		{
			if (timeSpan.Days > 0)
			{
				return string.Format("{0} {1:00}:{2:00}:{3:00}", new object[]
				{
					string.Format(NKCUtilString.GET_STRING_TIME_DAY_ONE_PARAM, timeSpan.Days),
					timeSpan.Hours,
					timeSpan.Minutes,
					timeSpan.Seconds
				});
			}
			return NKCUtilString.GetTimeSpanString(timeSpan);
		}

		// Token: 0x0600418B RID: 16779 RVA: 0x0014E3D4 File Offset: 0x0014C5D4
		public static string GetTimeSpanStringDHM(TimeSpan timeSpan)
		{
			if (timeSpan.Days > 0)
			{
				return string.Format(NKCUtilString.GET_STRING_TIME_DAY_ONE_PARAM, timeSpan.Days);
			}
			if (timeSpan.Hours > 0)
			{
				return string.Format(NKCUtilString.GET_STRING_TIME_HOUR_ONE_PARAM, timeSpan.Hours);
			}
			return string.Format(NKCUtilString.GET_STRING_TIME_MINUTE_ONE_PARAM, timeSpan.Minutes);
		}

		// Token: 0x0600418C RID: 16780 RVA: 0x0014E43C File Offset: 0x0014C63C
		public static string GetLastTimeString(DateTime lastTime)
		{
			TimeSpan timeSpan = NKCSynchronizedTime.GetServerUTCTime(0.0) - lastTime;
			if (timeSpan.TotalDays >= 1.0)
			{
				return string.Format(NKCUtilString.GET_STRING_TIME_DAY_AGO_ONE_PARAM, (int)timeSpan.TotalDays);
			}
			if (timeSpan.TotalHours >= 1.0)
			{
				return string.Format(NKCUtilString.GET_STRING_TIME_HOUR_AGO_ONE_PARAM, (int)timeSpan.TotalHours);
			}
			if (timeSpan.TotalMinutes >= 1.0)
			{
				return string.Format(NKCUtilString.GET_STRING_TIME_MINUTE_AGO_ONE_PARAM, (int)timeSpan.TotalMinutes);
			}
			if (timeSpan.TotalSeconds >= 1.0)
			{
				return string.Format(NKCUtilString.GET_STRING_TIME_SECOND_AGO_ONE_PARAM, (int)timeSpan.TotalSeconds);
			}
			return NKCUtilString.GET_STRING_TIME_A_SECOND_AGO;
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x0600418D RID: 16781 RVA: 0x0014E50B File Offset: 0x0014C70B
		public static string GET_STRING_SEASON_TIME_UP_TO_END_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SEASON_TIME_UP_TO_END_ONE_PARAM", false);
			}
		}

		// Token: 0x0600418E RID: 16782 RVA: 0x0014E518 File Offset: 0x0014C718
		public static string GetResetTimeString(DateTime baseTimeUTC, NKMTime.TimePeriod timePeriod, int maxWordCount)
		{
			DateTime nextResetTime = NKMTime.GetNextResetTime(baseTimeUTC, timePeriod);
			TimeSpan timeLeft = NKCSynchronizedTime.GetTimeLeft(nextResetTime);
			string text = "FFDF5D";
			if (timePeriod == NKMTime.TimePeriod.Day)
			{
				if (timeLeft.TotalHours < 6.0)
				{
					text = "cd2121";
				}
			}
			else if (timePeriod == NKMTime.TimePeriod.Week && timeLeft.TotalDays < 1.0)
			{
				text = "cd2121";
			}
			string arg = string.Concat(new string[]
			{
				"<color=#",
				text,
				">",
				NKCUtilString.GetRemainTimeString(nextResetTime, maxWordCount),
				"</color>"
			});
			return string.Format(NKCUtilString.GET_STRING_SHOP_CHAIN_NEXT_RESET_ONE_PARAM, arg);
		}

		// Token: 0x0600418F RID: 16783 RVA: 0x0014E5B0 File Offset: 0x0014C7B0
		public static string GetTimeIntervalString(DateTime startTimeLocal, DateTime endTimeLocal, int IntervalFromUTC, bool bDateOnly = false)
		{
			if (bDateOnly)
			{
				return string.Format("{0} ~ {1}", startTimeLocal.ToString("yyyy-MM-dd"), endTimeLocal.ToString("yyyy-MM-dd"));
			}
			return string.Format("{0} ~ {1} (UTC{2:+#;-#;''})", startTimeLocal.ToString("yyyy-MM-dd"), endTimeLocal.ToString("yyyy-MM-dd HH:mm"), IntervalFromUTC);
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06004190 RID: 16784 RVA: 0x0014E60B File Offset: 0x0014C80B
		public static string GET_STRING_REMAIN_TIME_LEFT_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_REMAIN_TIME_LEFT_ONE_PARAM", false);
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06004191 RID: 16785 RVA: 0x0014E618 File Offset: 0x0014C818
		public static string GET_STRING_EVENT_DATE_UNLIMITED_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENT_DATE_LIMITED_TEXT", false);
			}
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06004192 RID: 16786 RVA: 0x0014E625 File Offset: 0x0014C825
		public static string GET_STRING_SHOP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP", false);
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06004193 RID: 16787 RVA: 0x0014E632 File Offset: 0x0014C832
		public static string GET_STRING_SHOP_NEXT_REFRESH_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_NEXT_REFRESH_ONE_PARAM", false);
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06004194 RID: 16788 RVA: 0x0014E63F File Offset: 0x0014C83F
		public static string GET_STRING_SHOP_REMAIN_NUMBER_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_REMAIN_NUMBER_TWO_PARAM", false);
			}
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06004195 RID: 16789 RVA: 0x0014E64C File Offset: 0x0014C84C
		public static string GET_STRING_SHOP_SUPPLY_LIST_INSTANTLY_REFRESH_REQ
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_SUPPLY_LIST_INSTANTLY_REFRESH_REQ", false);
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06004196 RID: 16790 RVA: 0x0014E659 File Offset: 0x0014C859
		public static string GET_STRING_SHOP_SKIN_INFO
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_SKIN_INFO", false);
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06004197 RID: 16791 RVA: 0x0014E666 File Offset: 0x0014C866
		public static string GET_STRING_SHOP_PACKAGE_INFO
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_PACKAGE_INFO", false);
			}
		}

		// Token: 0x06004198 RID: 16792 RVA: 0x0014E673 File Offset: 0x0014C873
		public static string GetInAppPurchasePriceString(object price, int productID)
		{
			return NKCPublisherModule.InAppPurchase.GetCurrencyMark(productID) + " " + price.ToString();
		}

		// Token: 0x06004199 RID: 16793 RVA: 0x0014E690 File Offset: 0x0014C890
		public static string GetShopDescriptionText(string desc, bool bFirstBuy)
		{
			if (string.IsNullOrEmpty(desc))
			{
				return "";
			}
			string[] array = desc.Split(new char[]
			{
				'|'
			});
			if (array.Length >= 2)
			{
				if (array.Length > 2)
				{
					Debug.LogWarning("Too many tokens in shopstring : " + desc);
				}
				return array[bFirstBuy ? 1 : 0];
			}
			return desc;
		}

		// Token: 0x0600419A RID: 16794 RVA: 0x0014E6E4 File Offset: 0x0014C8E4
		public static string GetShopItemBuyMessage(ShopItemTemplet productTemplet, bool bItemToMail = false)
		{
			if (productTemplet == null)
			{
				return "";
			}
			if (bItemToMail)
			{
				return string.Format(NKCStringTable.GetString("SI_DP_SHOP_ITEM_BUY_MESSAGE_ITEM_TO_MAIL", false), productTemplet.GetItemName());
			}
			return string.Format(NKCStringTable.GetString("SI_DP_SHOP_ITEM_BUY_MESSAGE_ELSE", false), productTemplet.GetItemName());
		}

		// Token: 0x0600419B RID: 16795 RVA: 0x0014E720 File Offset: 0x0014C920
		public static string GetShopItemBuyMessage(NKMShopRandomListData randomListData)
		{
			if (randomListData == null)
			{
				return "";
			}
			switch (randomListData.itemType)
			{
			case NKM_REWARD_TYPE.RT_UNIT:
			case NKM_REWARD_TYPE.RT_OPERATOR:
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(randomListData.itemId);
				if (unitTempletBase != null)
				{
					return string.Format(NKCStringTable.GetString("SI_DP_SHOP_ITEM_BUY_MESSAGE_RT_UNIT", false), unitTempletBase.GetUnitName());
				}
				return "";
			}
			case NKM_REWARD_TYPE.RT_SHIP:
			{
				NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(randomListData.itemId);
				if (unitTempletBase2 != null)
				{
					return string.Format(NKCStringTable.GetString("SI_DP_SHOP_ITEM_BUY_MESSAGE_RT_SHIP", false), unitTempletBase2.GetUnitName());
				}
				return "";
			}
			case NKM_REWARD_TYPE.RT_MISC:
			case NKM_REWARD_TYPE.RT_MISSION_POINT:
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(randomListData.itemId);
				if (itemMiscTempletByID != null)
				{
					return string.Format(NKCStringTable.GetString("SI_DP_SHOP_ITEM_BUY_MESSAGE_RT_MISC", false), itemMiscTempletByID.GetItemName(), randomListData.itemCount);
				}
				return "";
			}
			case NKM_REWARD_TYPE.RT_USER_EXP:
			{
				NKMItemMiscTemplet itemMiscTempletByRewardType = NKMItemManager.GetItemMiscTempletByRewardType(randomListData.itemType);
				if (itemMiscTempletByRewardType != null)
				{
					return string.Format(NKCStringTable.GetString("SI_DP_SHOP_ITEM_BUY_MESSAGE_RT_USER_EXP", false), itemMiscTempletByRewardType.GetItemName(), randomListData.itemCount);
				}
				return "";
			}
			case NKM_REWARD_TYPE.RT_EQUIP:
			{
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(randomListData.itemId);
				return string.Format(NKCStringTable.GetString("SI_DP_SHOP_ITEM_BUY_MESSAGE_RT_EQUIP", false), NKCUtilString.GetItemEquipNameWithTier(equipTemplet));
			}
			case NKM_REWARD_TYPE.RT_MOLD:
			{
				NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(randomListData.itemId);
				if (itemMoldTempletByID != null)
				{
					return string.Format(NKCStringTable.GetString("SI_DP_SHOP_ITEM_BUY_MESSAGE_RT_MOLD", false), itemMoldTempletByID.GetItemName());
				}
				return "";
			}
			case NKM_REWARD_TYPE.RT_SKIN:
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(randomListData.itemId);
				if (skinTemplet != null)
				{
					return string.Format(NKCStringTable.GetString("SI_DP_SHOP_ITEM_BUY_MESSAGE_RT_SKIN", false), skinTemplet.GetTitle());
				}
				return "";
			}
			}
			return "";
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x0600419C RID: 16796 RVA: 0x0014E8D2 File Offset: 0x0014CAD2
		public static string GET_STRING_SHOP_PURCHASE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_PURCHASE", false);
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x0600419D RID: 16797 RVA: 0x0014E8DF File Offset: 0x0014CADF
		public static string GET_STRING_SHOP_ACCOUNT_PURCHASE_COUNT_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_ACCOUNT_PURCHASE_COUNT_TWO_PARAM", false);
			}
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x0600419E RID: 16798 RVA: 0x0014E8EC File Offset: 0x0014CAEC
		public static string GET_STRING_SHOP_DAY_PURCHASE_COUNT_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_DAY_PURCHASE_COUNT_TWO_PARAM", false);
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x0600419F RID: 16799 RVA: 0x0014E8F9 File Offset: 0x0014CAF9
		public static string GET_STRING_SHOP_MONTH_PURCHASE_COUNT_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_MONTH_PURCHASE_COUNT_TWO_PARAM", false);
			}
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x060041A0 RID: 16800 RVA: 0x0014E906 File Offset: 0x0014CB06
		public static string GET_STRING_SHOP_WEEK_PURCHASE_COUNT_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_WEEK_PURCHASE_COUNT_TWO_PARAM", false);
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x060041A1 RID: 16801 RVA: 0x0014E913 File Offset: 0x0014CB13
		public static string GET_STRING_SHOP_FIRST_PURCHASE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_FIRST_PURCHASE", false);
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x060041A2 RID: 16802 RVA: 0x0014E920 File Offset: 0x0014CB20
		public static string GET_STRING_SHOP_LIMIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_LIMIT", false);
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x060041A3 RID: 16803 RVA: 0x0014E92D File Offset: 0x0014CB2D
		public static string GET_STRING_SHOP_WAS_NOT_ABLE_TO_GET_PRODUCT_LIST_FROM_SERVER
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_WAS_NOT_ABLE_TO_GET_PRODUCT_LIST_FROM_SERVER", false);
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x060041A4 RID: 16804 RVA: 0x0014E93A File Offset: 0x0014CB3A
		public static string GET_STRING_SHOP_SUPPLY_LIST_GET_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_SUPPLY_LIST_GET_FAIL", false);
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x060041A5 RID: 16805 RVA: 0x0014E947 File Offset: 0x0014CB47
		public static string GET_STRING_PURCHASE_POPUP_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PURCHASE_POPUP_TITLE", false);
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x060041A6 RID: 16806 RVA: 0x0014E954 File Offset: 0x0014CB54
		public static string GET_STRING_PURCHASE_POPUP_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PURCHASE_POPUP_DESC", false);
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x060041A7 RID: 16807 RVA: 0x0014E961 File Offset: 0x0014CB61
		public static string GET_STRING_SHOP_COMPLETE_PURCHASE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_FIRST_PURCHASE", false);
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x060041A8 RID: 16808 RVA: 0x0014E96E File Offset: 0x0014CB6E
		public static string GET_STRING_SHOP_TIME_LIMIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_SALE_PERIOD", false);
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x060041A9 RID: 16809 RVA: 0x0014E97B File Offset: 0x0014CB7B
		public static string GET_STRING_SHOP_POPULAR
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_POPULAR", false);
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x060041AA RID: 16810 RVA: 0x0014E988 File Offset: 0x0014CB88
		public static string GET_STRING_SHOP_NEW
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_NEW", false);
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x060041AB RID: 16811 RVA: 0x0014E995 File Offset: 0x0014CB95
		public static string GET_STRING_SHOP_BEST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_BEST", false);
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x060041AC RID: 16812 RVA: 0x0014E9A2 File Offset: 0x0014CBA2
		public static string GET_STRING_SHOP_NOT_REGISTED_PRODUCT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_NOT_REGISTED_PRODUCT", false);
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x060041AD RID: 16813 RVA: 0x0014E9AF File Offset: 0x0014CBAF
		public static string GET_STRING_SHOP_FREE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_FREE", false);
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x060041AE RID: 16814 RVA: 0x0014E9BC File Offset: 0x0014CBBC
		public static string GET_STRING_SHOP_NOT_ENOUGH_REQUIREMENT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_NOT_ENOUGH_REQUIREMENT", false);
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x060041AF RID: 16815 RVA: 0x0014E9C9 File Offset: 0x0014CBC9
		public static string GET_STRING_SHOP_CHAIN_NEXT_RESET_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_CHAIN_NEXT_RESET_ONE_PARAM", false);
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x060041B0 RID: 16816 RVA: 0x0014E9D6 File Offset: 0x0014CBD6
		public static string GET_STRING_SHOP_CHAIN_NEXT_RESET_ONE_PARAM_CLOSE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_CHAIN_NEXT_RESET_ONE_PARAM_CLOSE", false);
			}
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x060041B1 RID: 16817 RVA: 0x0014E9E3 File Offset: 0x0014CBE3
		public static string GET_STRING_SHOP_PURCHASE_COUNT_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_PURCHASE_COUNT_TWO_PARAM", false);
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x060041B2 RID: 16818 RVA: 0x0014E9F0 File Offset: 0x0014CBF0
		public static string GET_STRING_SHOP_BUY_ALL_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_BUY_ALL_TITLE", false);
			}
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x060041B3 RID: 16819 RVA: 0x0014E9FD File Offset: 0x0014CBFD
		public static string GET_STRING_SHOP_BUY_ALL_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_BUY_ALL_DESC", false);
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x060041B4 RID: 16820 RVA: 0x0014EA0A File Offset: 0x0014CC0A
		public static string GET_STRING_SHOP_CHAIN_LOCKED
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_CHAIN_LOCKED", false);
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x060041B5 RID: 16821 RVA: 0x0014EA17 File Offset: 0x0014CC17
		public static string GET_STRING_SHOP_SPECIAL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_SPECIAL", false);
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x060041B6 RID: 16822 RVA: 0x0014EA24 File Offset: 0x0014CC24
		public static string GET_STRING_SHOP_SUBSCRIBE_DAY_ENOUGH_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_SUBSCRIBE_DAY_ENOUGH_DESC", false);
			}
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x060041B7 RID: 16823 RVA: 0x0014EA31 File Offset: 0x0014CC31
		public static string GET_STRING_SHOP_SKIN_STORY_MSG
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_SKIN_STORY_MSG", false);
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x060041B8 RID: 16824 RVA: 0x0014EA3E File Offset: 0x0014CC3E
		public static string GET_STRING_SHOP_SKIN_LOGIN_CUTIN_MSG
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHOP_SKIN_LOGIN_CUTIN_MSG", false);
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x060041B9 RID: 16825 RVA: 0x0014EA4B File Offset: 0x0014CC4B
		public static string GET_STRING_SHOP_BUY_SHORTCUT_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ITEM_NOT_ENOUGH_PRODUCT_POPUP_DESC", false);
			}
		}

		// Token: 0x060041BA RID: 16826 RVA: 0x0014EA58 File Offset: 0x0014CC58
		public static string GetAppVersionText()
		{
			string str;
			if (NKCStringTable.GetNationalCode() != NKM_NATIONAL_CODE.NNC_CENSORED_CHINESE)
			{
				str = "App Version : ";
			}
			else
			{
				str = "";
			}
			RuntimePlatform platform = Application.platform;
			if (platform <= RuntimePlatform.IPhonePlayer)
			{
				if (platform > RuntimePlatform.OSXPlayer && platform != RuntimePlatform.IPhonePlayer)
				{
					goto IL_5C;
				}
			}
			else
			{
				if (platform == RuntimePlatform.Android)
				{
					return str + Application.version + "A";
				}
				if (platform != RuntimePlatform.tvOS)
				{
					goto IL_5C;
				}
			}
			return str + Application.version + "I";
			IL_5C:
			return str + Application.version + "U";
		}

		// Token: 0x060041BB RID: 16827 RVA: 0x0014EAD4 File Offset: 0x0014CCD4
		public static string GetProtocolVersionText()
		{
			if (NKCStringTable.GetNationalCode() != NKM_NATIONAL_CODE.NNC_CENSORED_CHINESE)
			{
				return string.Concat(new string[]
				{
					"Protocol Version : ",
					845.ToString(),
					" / Data Version : ",
					NKMDataVersion.DataVersion.ToString(),
					" / StreamID ",
					-1.ToString()
				});
			}
			return string.Concat(new string[]
			{
				845.ToString(),
				" / ",
				NKMDataVersion.DataVersion.ToString(),
				" / ",
				-1.ToString()
			});
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x060041BC RID: 16828 RVA: 0x0014EB7A File Offset: 0x0014CD7A
		public static string GET_STRING_LOGIN_NOT_READY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LOGIN_NOT_READY", false);
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x060041BD RID: 16829 RVA: 0x0014EB87 File Offset: 0x0014CD87
		public static string GET_STRING_INITIALIZE_FAILED
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_INITIALIZE_FAILED", false);
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x060041BE RID: 16830 RVA: 0x0014EB94 File Offset: 0x0014CD94
		public static string GET_STRING_DECONNECT_AND_GO_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_DECONNECT_AND_GO_TITLE", false);
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x060041BF RID: 16831 RVA: 0x0014EBA1 File Offset: 0x0014CDA1
		public static string GET_STRING_TOY_LOGGED_IN_GUEST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_LOGGED_IN_GUEST", false);
			}
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x060041C0 RID: 16832 RVA: 0x0014EBAE File Offset: 0x0014CDAE
		public static string GET_STRING_TOY_LOGGED_IN_NEXON
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_LOGGED_IN_NEXON", false);
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x060041C1 RID: 16833 RVA: 0x0014EBBB File Offset: 0x0014CDBB
		public static string GET_STRING_TOY_LOGGED_IN_FACEBOOK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_LOGGED_IN_FACEBOOK", false);
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x060041C2 RID: 16834 RVA: 0x0014EBC8 File Offset: 0x0014CDC8
		public static string GET_STRING_TOY_LOGGED_IN_GOOGLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_LOGGED_IN_GOOGLE", false);
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x060041C3 RID: 16835 RVA: 0x0014EBD5 File Offset: 0x0014CDD5
		public static string GET_STRING_TOY_LOGGED_IN_APPLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_LOGGED_IN_APPLE", false);
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x060041C4 RID: 16836 RVA: 0x0014EBE2 File Offset: 0x0014CDE2
		public static string GET_STRING_TOY_GUEST_ACCOUNT_TRANSFER_BACKUP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_GUEST_ACCOUNT_TRANSFER_BACKUP", false);
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x060041C5 RID: 16837 RVA: 0x0014EBEF File Offset: 0x0014CDEF
		public static string GET_STRING_TOY_GUEST_ACCOUNT_TRANSFER_RESTORE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_GUEST_ACCOUNT_TRANSFER_RESTORE", false);
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x060041C6 RID: 16838 RVA: 0x0014EBFC File Offset: 0x0014CDFC
		public static string GET_STRING_ERROR_MULTIPLE_CONNECT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ERROR_MULTIPLE_CONNECT", false);
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x060041C7 RID: 16839 RVA: 0x0014EC09 File Offset: 0x0014CE09
		public static string GET_STRING_ALREADY_LOGGED_IN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ALREADY_LOGGED_IN", false);
			}
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x060041C8 RID: 16840 RVA: 0x0014EC16 File Offset: 0x0014CE16
		public static string GET_STRING_FAIL_TO_PROCESS_TERMS
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FAIL_TO_PROCESS_TERMS", false);
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x060041C9 RID: 16841 RVA: 0x0014EC23 File Offset: 0x0014CE23
		public static string GET_STRING_TOY_IS_BEING_DISABLED
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_IS_BEING_DISABLED", false);
			}
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x060041CA RID: 16842 RVA: 0x0014EC30 File Offset: 0x0014CE30
		public static string GET_STRING_TOY_LOCAL_PUSH_CONTRACT_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_LOCAL_PUSH_CONTRACT_TITLE", false);
			}
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x060041CB RID: 16843 RVA: 0x0014EC3D File Offset: 0x0014CE3D
		public static string GET_STRING_TOY_LOCAL_PUSH_CONTRACT_DESCRIPTION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_LOCAL_PUSH_CONTRACT_DESCRIPTION", false);
			}
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x060041CC RID: 16844 RVA: 0x0014EC4A File Offset: 0x0014CE4A
		public static string GET_STRING_TOY_LOCAL_PUSH_WORLD_MAP_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_LOCAL_PUSH_WORLD_MAP_TITLE", false);
			}
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x060041CD RID: 16845 RVA: 0x0014EC57 File Offset: 0x0014CE57
		public static string GET_STRING_TOY_LOCAL_PUSH_WORLD_MAP_DESCRIPTION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_LOCAL_PUSH_WORLD_MAP_DESCRIPTION", false);
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x060041CE RID: 16846 RVA: 0x0014EC64 File Offset: 0x0014CE64
		public static string GET_STRING_TOY_LOCAL_PUSH_AUTO_SUPPLY_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_LOCAL_PUSH_AUTO_SUPPLY_TITLE", false);
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x060041CF RID: 16847 RVA: 0x0014EC71 File Offset: 0x0014CE71
		public static string GET_STRING_TOY_LOCAL_PUSH_AUTO_SUPPLY_DESCRIPTION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_LOCAL_PUSH_AUTO_SUPPLY_DESCRIPTION", false);
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x060041D0 RID: 16848 RVA: 0x0014EC7E File Offset: 0x0014CE7E
		public static string GET_STRING_TOY_LOCAL_PUSH_GEAR_CRAFT_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_LOCAL_PUSH_GEAR_CRAFT_TITLE", false);
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x060041D1 RID: 16849 RVA: 0x0014EC8B File Offset: 0x0014CE8B
		public static string GET_STRING_TOY_LOCAL_PUSH_GEAR_CRAFT_DESCRIPTION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_LOCAL_PUSH_GEAR_CRAFT_DESCRIPTION", false);
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x060041D2 RID: 16850 RVA: 0x0014EC98 File Offset: 0x0014CE98
		public static string GET_STRING_TOY_LOCAL_PUSH_PVP_POINT_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_LOCAL_PUSH_PVP_POINT_TITLE", false);
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x060041D3 RID: 16851 RVA: 0x0014ECA5 File Offset: 0x0014CEA5
		public static string GET_STRING_TOY_LOCAL_PUSH_PVP_POINT_DESCRIPTION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_LOCAL_PUSH_PVP_POINT_DESCRIPTION", false);
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x060041D4 RID: 16852 RVA: 0x0014ECB2 File Offset: 0x0014CEB2
		public static string GET_STRING_ERROR_TOY_SHOP_LIST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ERROR_TOY_SHOP_LIST", false);
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x060041D5 RID: 16853 RVA: 0x0014ECBF File Offset: 0x0014CEBF
		public static string GET_STRING_TOY_LOCAL_PUSH_NOT_CONNECTED_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_LOCAL_PUSH_NOT_CONNECTED_TITLE", false);
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x060041D6 RID: 16854 RVA: 0x0014ECCC File Offset: 0x0014CECC
		public static string GET_STRING_TOY_LOCAL_PUSH_NOT_CONNECTED_DESCRIPTION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_LOCAL_PUSH_NOT_CONNECTED_DESCRIPTION", false);
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x060041D7 RID: 16855 RVA: 0x0014ECD9 File Offset: 0x0014CED9
		public static string GET_STRING_TOY_LOGOUT_SUCCESS
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_LOGOUT_SUCCESS", false);
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x060041D8 RID: 16856 RVA: 0x0014ECE6 File Offset: 0x0014CEE6
		public static string GET_STRING_TOY_COMMUNITY_CONNECT_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_COMMUNITY_CONNECT_FAIL", false);
			}
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x060041D9 RID: 16857 RVA: 0x0014ECF3 File Offset: 0x0014CEF3
		public static string GET_STRING_TOY_SYNC_ACCOUNT_CANCEL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_SYNC_ACCOUNT_CANCEL", false);
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x060041DA RID: 16858 RVA: 0x0014ED00 File Offset: 0x0014CF00
		public static string GET_STRING_TOY_SYNC_ACCOUNT_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_SYNC_ACCOUNT_FAIL", false);
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x060041DB RID: 16859 RVA: 0x0014ED0D File Offset: 0x0014CF0D
		public static string GET_STRING_TOY_SERVICE_WITHDRAWAL_REQ_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_SERVICE_WITHDRAWAL_REQ_FAIL", false);
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x060041DC RID: 16860 RVA: 0x0014ED1A File Offset: 0x0014CF1A
		public static string GET_STRING_CHANGEACCOUNT_FAIL_GUEST_ALREADY_MAPPED
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CHANGEACCOUNT_FAIL_GUEST_ALREADY_MAPPED", false);
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x060041DD RID: 16861 RVA: 0x0014ED27 File Offset: 0x0014CF27
		public static string GET_STRING_CHANGEACCOUNT_SUCCESS_GUEST_SYNC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CHANGEACCOUNT_SUCCESS_GUEST_SYNC", false);
			}
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x060041DE RID: 16862 RVA: 0x0014ED34 File Offset: 0x0014CF34
		public static string GET_STRING_AUTH_LOGIN_QUIT_USER
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_AUTH_LOGIN_QUIT_USER", false);
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x060041DF RID: 16863 RVA: 0x0014ED41 File Offset: 0x0014CF41
		public static string GET_STRING_TOY_BILLING_PAYMENT_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_BILLING_PAYMENT_FAIL", false);
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x060041E0 RID: 16864 RVA: 0x0014ED4E File Offset: 0x0014CF4E
		public static string GET_STRING_TOY_BILLING_PAYMENT_FAIL_DESC_1
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_BILLING_PAYMENT_FAIL_DESC_1", false);
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x060041E1 RID: 16865 RVA: 0x0014ED5B File Offset: 0x0014CF5B
		public static string GET_STRING_TOY_BILLING_PAYMENT_FAIL_DESC_2
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_BILLING_PAYMENT_FAIL_DESC_2", false);
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x060041E2 RID: 16866 RVA: 0x0014ED68 File Offset: 0x0014CF68
		public static string GET_STRING_TOY_BILLING_RESTORE_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_BILLING_RESTORE_FAIL", false);
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x060041E3 RID: 16867 RVA: 0x0014ED75 File Offset: 0x0014CF75
		public static string GET_STRING_TOY_BILLING_RESTORE_REQ_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_BILLING_RESTORE_REQ_FAIL", false);
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x060041E4 RID: 16868 RVA: 0x0014ED82 File Offset: 0x0014CF82
		public static string GET_STRING_TOY_NOT_EXIST_RESTORE_ITEM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_NOT_EXIST_RESTORE_ITEM", false);
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x060041E5 RID: 16869 RVA: 0x0014ED8F File Offset: 0x0014CF8F
		public static string GET_STRING_TOY_BILLING_RESTORE_INVALID_WORK_DESC_1
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_BILLING_RESTORE_INVALID_WORK_DESC_1", false);
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x060041E6 RID: 16870 RVA: 0x0014ED9C File Offset: 0x0014CF9C
		public static string GET_STRING_TOY_BILLING_RESTORE_NETWORK_ISSUE_DESC_1
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_BILLING_RESTORE_NETWORK_ISSUE_DESC_1", false);
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x060041E7 RID: 16871 RVA: 0x0014EDA9 File Offset: 0x0014CFA9
		public static string GET_STRING_TOY_BILLING_RESTORE_INVALID_WORK_DESC_2
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_BILLING_RESTORE_INVALID_WORK_DESC_2", false);
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x060041E8 RID: 16872 RVA: 0x0014EDB6 File Offset: 0x0014CFB6
		public static string GET_STRING_TOY_BILLING_RESTORE_NETWORK_ISSUE_DESC_2
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_BILLING_RESTORE_NETWORK_ISSUE_DESC_2", false);
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x060041E9 RID: 16873 RVA: 0x0014EDC3 File Offset: 0x0014CFC3
		public static string GET_STRING_TOY_BACKUP_CODE_REQ_CANCEL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_BACKUP_CODE_REQ_CANCEL", false);
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x060041EA RID: 16874 RVA: 0x0014EDD0 File Offset: 0x0014CFD0
		public static string GET_STRING_TOY_GUEST_ACCOUT_RESTORE_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_GUEST_ACCOUT_RESTORE_FAIL", false);
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x060041EB RID: 16875 RVA: 0x0014EDDD File Offset: 0x0014CFDD
		public static string GET_STRING_TOY_NEXON_UNREGISTER_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_NEXON_UNREGISTER_FAIL", false);
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x060041EC RID: 16876 RVA: 0x0014EDEA File Offset: 0x0014CFEA
		public static string GET_STRING_TOY_USER_INFO_UPDATE_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_USER_INFO_UPDATE_FAIL", false);
			}
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x060041ED RID: 16877 RVA: 0x0014EDF7 File Offset: 0x0014CFF7
		public static string GET_STRING_TOY_RE_TRY_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_RE_TRY_TITLE", false);
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x060041EE RID: 16878 RVA: 0x0014EE04 File Offset: 0x0014D004
		public static string GET_STRING_TOY_RE_TRY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_RE_TRY_DESC", false);
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x060041EF RID: 16879 RVA: 0x0014EE11 File Offset: 0x0014D011
		public static string GET_STRING_TOY_LOGGED_IN_GUEST_KOR
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_LOGGED_IN_GUEST_KOR", false);
			}
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x060041F0 RID: 16880 RVA: 0x0014EE1E File Offset: 0x0014D01E
		public static string GET_STRING_TOY_CONNECTED_ACCOUNT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_CONNECTED_ACCOUNT", false);
			}
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x060041F1 RID: 16881 RVA: 0x0014EE2B File Offset: 0x0014D02B
		public static string GET_STRING_TOY_CUSTOMER_CENTER_RESPOND
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_CUSTOMER_CENTER_RESPOND", false);
			}
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x060041F2 RID: 16882 RVA: 0x0014EE38 File Offset: 0x0014D038
		public static string GET_STRING_TOY_LOGIN_CHANGE_ACCOUNT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_LOGIN_CHANGE_ACCOUNT", false);
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x060041F3 RID: 16883 RVA: 0x0014EE45 File Offset: 0x0014D045
		public static string GET_STRING_TOY_LOGIN_NO_AUTH
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOY_LOGIN_NO_AUTH", false);
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x060041F4 RID: 16884 RVA: 0x0014EE52 File Offset: 0x0014D052
		public static string GET_STRING_LOBBY_CHECK_QUIT_GAME
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LOBBY_CHECK_QUIT_GAME", false);
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x060041F5 RID: 16885 RVA: 0x0014EE5F File Offset: 0x0014D05F
		public static string GET_STRING_LOBBY_CONTRACT_COMPLETED
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LOBBY_CONTRACT_COMPLETED", false);
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x060041F6 RID: 16886 RVA: 0x0014EE6C File Offset: 0x0014D06C
		public static string GET_STRING_LOBBY_CONTRACT_PROGRESSING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LOBBY_CONTRACT_PROGRESSING", false);
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x060041F7 RID: 16887 RVA: 0x0014EE79 File Offset: 0x0014D079
		public static string GET_STRING_LOBBY_USER_BUFF_NONE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LOBBY_USER_BUFF_NONE", false);
			}
		}

		// Token: 0x060041F8 RID: 16888 RVA: 0x0014EE86 File Offset: 0x0014D086
		public static string GetBuffString()
		{
			return string.Format("", Array.Empty<object>());
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x060041F9 RID: 16889 RVA: 0x0014EE97 File Offset: 0x0014D097
		public static string GET_STRING_LOBBY_UNIT_CAPTAIN
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_SLOT_CARD_LOBBY_CAPTAIN", false);
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x060041FA RID: 16890 RVA: 0x0014EEA4 File Offset: 0x0014D0A4
		public static string GET_STRING_LOBBY_BG_SELECT_CAPTAIN
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_LOBBY_BACKGROUND_CAPTAIN_TEXT", false);
			}
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x060041FB RID: 16891 RVA: 0x0014EEB1 File Offset: 0x0014D0B1
		public static string GET_STRING_LOBBY_CITY_MISSION_ONGOING
		{
			get
			{
				return NKCStringTable.GetString("SI_LOBBY_RIGHT_MENU_1_WORLDMAP_PROGRESS_TEXT", false);
			}
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x060041FC RID: 16892 RVA: 0x0014EEBE File Offset: 0x0014D0BE
		public static string GET_STRING_LOBBY_CITY_MISSION_COMPLETE
		{
			get
			{
				return NKCStringTable.GetString("SI_LOBBY_RIGHT_MENU_1_WORLDMAP_PROGRESS_COMPLETE_TEXT", false);
			}
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x060041FD RID: 16893 RVA: 0x0014EECB File Offset: 0x0014D0CB
		public static string GET_STRING_LOBBY_FIERCEBATTLE_TIME_REMAIN
		{
			get
			{
				return NKCStringTable.GetString("SI_LOBBY_FIERCEBATTLE_TIME_REMAIN", false);
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x060041FE RID: 16894 RVA: 0x0014EED8 File Offset: 0x0014D0D8
		public static string GET_STRING_MISSION_SHORTCUT_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MISSION_SHORTCUT_FAIL", false);
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x060041FF RID: 16895 RVA: 0x0014EEE5 File Offset: 0x0014D0E5
		public static string GET_STRING_MISSION_EXPIRED
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MISSION_EXPIRED", false);
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06004200 RID: 16896 RVA: 0x0014EEF2 File Offset: 0x0014D0F2
		public static string GET_STRING_MISSION_COMPLETE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MISSION_COMPLETE", false);
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06004201 RID: 16897 RVA: 0x0014EEFF File Offset: 0x0014D0FF
		public static string GET_STRING_MISSION_COMPLETE_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MISSION_COMPLETE_ONE_PARAM", false);
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06004202 RID: 16898 RVA: 0x0014EF0C File Offset: 0x0014D10C
		public static string GET_STRING_MISSION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MISSION", false);
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06004203 RID: 16899 RVA: 0x0014EF19 File Offset: 0x0014D119
		public static string GET_STRING_MISSION_RESET_INTERVAL_DAILY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MISSION_RESET_INTERVAL_DAILY", false);
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06004204 RID: 16900 RVA: 0x0014EF26 File Offset: 0x0014D126
		public static string GET_STRING_MISSION_RESET_INTERVAL_WEEKLY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MISSION_RESET_INTERVAL_WEEKLY", false);
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06004205 RID: 16901 RVA: 0x0014EF33 File Offset: 0x0014D133
		public static string GET_STRING_MISSION_RESET_INTERVAL_MONTHLY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MISSION_RESET_INTERVAL_MONTHLY", false);
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06004206 RID: 16902 RVA: 0x0014EF40 File Offset: 0x0014D140
		public static string GET_STRING_MISSION_REMAIN_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MISSION_REMAIN_TWO_PARAM", false);
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06004207 RID: 16903 RVA: 0x0014EF4D File Offset: 0x0014D14D
		public static string GET_STRING_MISSION_COMPLETE_GROWTH_TAB
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MISSION_COMPLETE_GROWTH_TAB", false);
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06004208 RID: 16904 RVA: 0x0014EF5A File Offset: 0x0014D15A
		public static string GET_STRING_MISSION_LOCK_GROWTH_TAB
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MISSION_LOCK_GROWTH_TAB", false);
			}
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06004209 RID: 16905 RVA: 0x0014EF67 File Offset: 0x0014D167
		public static string GET_STRING_MISSION_TAB_GROWTH_01
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MISSION_TAB_GROWTH_01", false);
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x0600420A RID: 16906 RVA: 0x0014EF74 File Offset: 0x0014D174
		public static string GET_STRING_MISSION_TAB_GROWTH_02
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MISSION_TAB_GROWTH_02", false);
			}
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x0600420B RID: 16907 RVA: 0x0014EF81 File Offset: 0x0014D181
		public static string GET_STRING_MISSION_TAB_GROWTH_03
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MISSION_TAB_GROWTH_03", false);
			}
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x0600420C RID: 16908 RVA: 0x0014EF8E File Offset: 0x0014D18E
		public static string GET_STRING_MISSION_UNAVAILABLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MISSION_UNAVAILABLE", false);
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x0600420D RID: 16909 RVA: 0x0014EF9B File Offset: 0x0014D19B
		public static string GET_STRING_MISSION_NEED_GROWTH_ALL_COMPLETE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MISSION_NEED_GROWTH_ALL_COMPLETE", false);
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x0600420E RID: 16910 RVA: 0x0014EFA8 File Offset: 0x0014D1A8
		public static string GET_STRING_MISSION_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MISSION_ONE_PARAM", false);
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x0600420F RID: 16911 RVA: 0x0014EFB5 File Offset: 0x0014D1B5
		public static string GET_STRING_GAUNTLET_OPEN_RANK_MODE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_OPEN_RANK_MODE", false);
			}
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06004210 RID: 16912 RVA: 0x0014EFC2 File Offset: 0x0014D1C2
		public static string GET_STRING_GAUNTLET_NOT_OPEN_RANK_MODE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_NOT_OPEN_RANK_MODE", false);
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06004211 RID: 16913 RVA: 0x0014EFCF File Offset: 0x0014D1CF
		public static string GET_STRING_GAUNTLET_LEAGUE_TAG_CLOSE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_GAUNTLET_LEAGUE_TAG_CLOSE", false);
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06004212 RID: 16914 RVA: 0x0014EFDC File Offset: 0x0014D1DC
		public static string GET_STRING_GAUNTLET_LEAGUE_TAG_CLOSE_MESSAGE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_GAUNTLET_LEAGUE_TAG_CLOSE_MESSAGE", false);
			}
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06004213 RID: 16915 RVA: 0x0014EFE9 File Offset: 0x0014D1E9
		public static string GET_STRING_GAUNTLET_ASYNC_TICKET_USE_POPUP_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_GAUNTLET_ASYNC_TICKET_USE_POPUP_TEXT", false);
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06004214 RID: 16916 RVA: 0x0014EFF6 File Offset: 0x0014D1F6
		public static string GET_STRING_GAUNTLET_MATCHING_FAIL_ALARM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_MATCHING_FAIL_ALARM", false);
			}
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06004215 RID: 16917 RVA: 0x0014F003 File Offset: 0x0014D203
		public static string GET_STRING_GAUNTLET_BAN_LEVEL_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_BAN_LEVEL", false);
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06004216 RID: 16918 RVA: 0x0014F010 File Offset: 0x0014D210
		public static string GET_STRING_GAUNTLET_UP_LEVEL_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_UP_LEVEL", false);
			}
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06004217 RID: 16919 RVA: 0x0014F01D File Offset: 0x0014D21D
		public static string GET_STRING_GAUNTLET_BAN_APPLY_DESC_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_BAN_DEBUFF_SHIP", false);
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06004218 RID: 16920 RVA: 0x0014F02A File Offset: 0x0014D22A
		public static string GET_STRING_GAUNTLET
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET", false);
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06004219 RID: 16921 RVA: 0x0014F037 File Offset: 0x0014D237
		public static string GET_STRING_GAUNTLET_DEMOTE_WARNING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_DEMOTE_WARNING", false);
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x0600421A RID: 16922 RVA: 0x0014F044 File Offset: 0x0014D244
		public static string GET_STRING_GAUNTLET_THIS_WEEK_LEAGUE_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_THIS_WEEK_LEAGUE_ONE_PARAM", false);
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x0600421B RID: 16923 RVA: 0x0014F051 File Offset: 0x0014D251
		public static string GET_STRING_GAUNTLET_LEAUGE_GUIDE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_LEAUGE_GUIDE", false);
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x0600421C RID: 16924 RVA: 0x0014F05E File Offset: 0x0014D25E
		public static string GET_STRING_GAUNTLET_SEASON_TITLE_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_SEASON_TITLE_ONE_PARAM", false);
			}
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x0600421D RID: 16925 RVA: 0x0014F06B File Offset: 0x0014D26B
		public static string GET_STRING_GAUNTLET_SEASON_LEAUGE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_SEASON_LEAUGE_DESC", false);
			}
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x0600421E RID: 16926 RVA: 0x0014F078 File Offset: 0x0014D278
		public static string GET_STRING_GAUNTLET_WEEK_LEAGUE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_WEEK_LEAGUE", false);
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x0600421F RID: 16927 RVA: 0x0014F085 File Offset: 0x0014D285
		public static string GET_STRING_GAUNTLET_WEEK_LEAGUE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_WEEK_LEAGUE_DESC", false);
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06004220 RID: 16928 RVA: 0x0014F092 File Offset: 0x0014D292
		public static string GET_STRING_GAUNTLET_BATTLE_RECORD
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_BATTLE_RECORD", false);
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06004221 RID: 16929 RVA: 0x0014F09F File Offset: 0x0014D29F
		public static string GET_STRING_GAUNTLET_WIN_STREAK_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_WIN_STREAK_ONE_PARAM", false);
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06004222 RID: 16930 RVA: 0x0014F0AC File Offset: 0x0014D2AC
		public static string GET_STRING_GAUNTLET_SEASON_NUMBERING_NAME
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_SEASON_NUMBERING_NAME", false);
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06004223 RID: 16931 RVA: 0x0014F0B9 File Offset: 0x0014D2B9
		public static string GET_STRING_GAUNTLET_WIN_COUNT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_WIN_COUNT", false);
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06004224 RID: 16932 RVA: 0x0014F0C6 File Offset: 0x0014D2C6
		public static string GET_STRING_GAUNTLET_RANK_GAME
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_RANK_GAME", false);
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06004225 RID: 16933 RVA: 0x0014F0D3 File Offset: 0x0014D2D3
		public static string GET_STRING_GAUNTLET_NORMAL_GAME
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_NORMAL_GAME", false);
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06004226 RID: 16934 RVA: 0x0014F0E0 File Offset: 0x0014D2E0
		public static string GET_STRING_GAUNTLET_SEASON_REWARD
		{
			get
			{
				return NKCStringTable.GetString("SI_POPUP_SEASON_RESULT_TITLE", false);
			}
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06004227 RID: 16935 RVA: 0x0014F0ED File Offset: 0x0014D2ED
		public static string GET_STRING_GAUNTLET_WEEKLY_REWARD
		{
			get
			{
				return NKCStringTable.GetString("SI_POPUP_WEEKLY_RESULT_TITLE", false);
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06004228 RID: 16936 RVA: 0x0014F0FA File Offset: 0x0014D2FA
		public static string GET_STRING_GAUNTLET_RANK_HELP_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_INFO_DESC", false);
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06004229 RID: 16937 RVA: 0x0014F107 File Offset: 0x0014D307
		public static string GET_STRING_GAUNTLET_ASYNC_SEASON_LEAGUE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_ASYNC_SEASON_LEAUGE_DESC", false);
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x0600422A RID: 16938 RVA: 0x0014F114 File Offset: 0x0014D314
		public static string GET_STRING_GAUNTLET_ASYNC_WEEK_LEAGUE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_ASYNC_WEEK_LEAGUE_DESC", false);
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x0600422B RID: 16939 RVA: 0x0014F121 File Offset: 0x0014D321
		public static string GET_STRING_GAUNTLET_ASYNC_HELP_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_ASYNC_NEW_INFO_LEAGUE_DESC", false);
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x0600422C RID: 16940 RVA: 0x0014F12E File Offset: 0x0014D32E
		public static string GET_STRING_GAUNTLET_LEAGUE_SEASON_LEAGUE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_SEASON_LEAUGE_MODE_DESC", false);
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x0600422D RID: 16941 RVA: 0x0014F13B File Offset: 0x0014D33B
		public static string GET_STRING_GAUNTLET_LEAGUE_HELP_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_LEAGUE_INFO_DESC", false);
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x0600422E RID: 16942 RVA: 0x0014F148 File Offset: 0x0014D348
		public static string GET_STRING_FIREND_NO_EXIST_ASYNC_LOG
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_GAUNTLET_HAVE_NO_DEFENSE_DECK", false);
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x0600422F RID: 16943 RVA: 0x0014F155 File Offset: 0x0014D355
		public static string GET_STRING_GAUNTLET_RANK_NO_JOIN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_RANK_NO_JOIN", false);
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06004230 RID: 16944 RVA: 0x0014F162 File Offset: 0x0014D362
		public static string GET_STRING_GAUNTLET_THIS_SEASON_LEAGUE_BEING_EVALUATED
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_THIS_SEASON_LEAGUE_BEING_EVALUATED", false);
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06004231 RID: 16945 RVA: 0x0014F16F File Offset: 0x0014D36F
		public static string GET_STRING_GAUNTLET_THIS_SEASON_LEAGUE_REMAIN_TIME_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_THIS_SEASON_LEAGUE_REMAIN_TIME_ONE_PARAM", false);
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06004232 RID: 16946 RVA: 0x0014F17C File Offset: 0x0014D37C
		public static string GET_STRING_GAUNTLET_ASYNC_GAME_READY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_ASYNC_GAME_READY", false);
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06004233 RID: 16947 RVA: 0x0014F189 File Offset: 0x0014D389
		public static string GET_STRING_GAUNTLET_ASYNC_GAME_START
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_ASYNC_GAME_START", false);
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06004234 RID: 16948 RVA: 0x0014F196 File Offset: 0x0014D396
		public static string GET_STRING_GAUNTLET_ASYNC_GAME
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_ASYNC_GAME", false);
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06004235 RID: 16949 RVA: 0x0014F1A3 File Offset: 0x0014D3A3
		public static string GET_STRING_GAUNTLET_ASYNC_LOCK_DEFENSE_DECK
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_GAUNTLET_NEED_DEFENSE_DECK_DESC", false);
			}
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06004236 RID: 16950 RVA: 0x0014F1B0 File Offset: 0x0014D3B0
		public static string GET_STRING_GAUNTLET_ASYNC_LOCK_CLOSING
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_GAUNTLET_ASYNC_TIME_CLOSING", false);
			}
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06004237 RID: 16951 RVA: 0x0014F1BD File Offset: 0x0014D3BD
		public static string GET_STRING_GAUNTLET_SELECT_IMPOSSIBLE
		{
			get
			{
				return NKCStringTable.GetString("SI_GAUNTLET_LOBBY_CASTING_BAN_SELECT_IMPOSSIBLE", false);
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06004238 RID: 16952 RVA: 0x0014F1CA File Offset: 0x0014D3CA
		public static string GET_STRING_GAUNTLET_CASTING_BAN_SELECT_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_PVP_CASTING_BAN_SELECT_UNIT_START", false);
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06004239 RID: 16953 RVA: 0x0014F1D7 File Offset: 0x0014D3D7
		public static string GET_STRING_GAUNTLET_CASTING_BAN_SELECT_SHIP
		{
			get
			{
				return NKCStringTable.GetString("SI_PVP_CASTING_BAN_SELECT_SHIP_START", false);
			}
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x0600423A RID: 16954 RVA: 0x0014F1E4 File Offset: 0x0014D3E4
		public static string GET_STRING_GAUNTLET_CASTING_BAN_SELECT_OPER
		{
			get
			{
				return NKCStringTable.GetString("SI_PVP_CASTING_BAN_SELECT_OPR_START", false);
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x0600423B RID: 16955 RVA: 0x0014F1F1 File Offset: 0x0014D3F1
		public static string GET_STRING_GAUNTLET_CASTING_BAN_SELECT_LIST_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PVP_CASTING_BAN_SELECT", false);
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x0600423C RID: 16956 RVA: 0x0014F1FE File Offset: 0x0014D3FE
		public static string GET_STRING_GAUNTLET_THIS_WEEK_LEAGUE_CASTING_BEN_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_PVP_CASTING_BAN_SELECT_FINISH_DATE", false);
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x0600423D RID: 16957 RVA: 0x0014F20B File Offset: 0x0014D40B
		public static string GET_STRING_GAUNTLET_BAN_POPUP_DESC_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_BAN_POPUP_DESC_UNIT", false);
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x0600423E RID: 16958 RVA: 0x0014F218 File Offset: 0x0014D418
		public static string GET_STRING_GAUNTLET_BAN_POPUP_DESC_SHIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_BAN_POPUP_DESC_SHIP", false);
			}
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x0600423F RID: 16959 RVA: 0x0014F225 File Offset: 0x0014D425
		public static string GET_STRING_GAUNTLET_BAN_POPUP_DESC_OPER
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_BAN_POPUP_DESC_OPR", false);
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06004240 RID: 16960 RVA: 0x0014F232 File Offset: 0x0014D432
		public static string GET_STRING_GAUNTLET_BAN_POPUP_DESC_UP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_UP_POPUP_DESC_UNIT", false);
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06004241 RID: 16961 RVA: 0x0014F23F File Offset: 0x0014D43F
		public static string GET_STRING_GAUNTLET_CASTING_BAN_SELECT_COMPLET
		{
			get
			{
				return NKCStringTable.GetString("SI_PVP_CASTING_BAN_SELECT_COMPLETE", false);
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06004242 RID: 16962 RVA: 0x0014F24C File Offset: 0x0014D44C
		public static string GET_STRING_GAUNTLET_ASYNC_NPC_BLOCK_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_ASYNC_NEW_NPC_NOTICE_DESC", false);
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06004243 RID: 16963 RVA: 0x0014F259 File Offset: 0x0014D459
		public static string GET_STRING_GAUNTLET_DECK_UNIT_NOT_ALL_EQUIPED_GEAR_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PVP_POPUP_UNIT_NON_EQUIP", false);
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06004244 RID: 16964 RVA: 0x0014F266 File Offset: 0x0014D466
		public static string GET_STRING_FRIEND
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND", false);
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06004245 RID: 16965 RVA: 0x0014F273 File Offset: 0x0014D473
		public static string GET_STRING_FRIEND_SHOP_COMING_SOON
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_SHOP_COMING_SOON", false);
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06004246 RID: 16966 RVA: 0x0014F280 File Offset: 0x0014D480
		public static string GET_STRING_FRIEND_CHANGE_IMAGE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_CHANGE_IMAGE", false);
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06004247 RID: 16967 RVA: 0x0014F28D File Offset: 0x0014D48D
		public static string GET_STRING_FRIEND_MAIN_DECK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_MAIN_DECK", false);
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06004248 RID: 16968 RVA: 0x0014F29A File Offset: 0x0014D49A
		public static string GET_STRING_FRIEND_LAST_CONNECT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_LAST_CONNECT", false);
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06004249 RID: 16969 RVA: 0x0014F2A7 File Offset: 0x0014D4A7
		public static string GET_STRING_FRIEND_LEVEL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_LEVEL", false);
			}
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x0600424A RID: 16970 RVA: 0x0014F2B4 File Offset: 0x0014D4B4
		public static string GET_STRING_FRIEND_SEARCH
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_SEARCH", false);
			}
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x0600424B RID: 16971 RVA: 0x0014F2C1 File Offset: 0x0014D4C1
		public static string GET_STRING_FRIEND_INFO_LEVEL_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_INFO_LEVEL_ONE_PARAM", false);
			}
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x0600424C RID: 16972 RVA: 0x0014F2CE File Offset: 0x0014D4CE
		public static string GET_STRING_FIREND_NO_EXIST_PVP_LOG
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FIREND_NO_EXIST_PVP_LOG", false);
			}
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x0600424D RID: 16973 RVA: 0x0014F2DB File Offset: 0x0014D4DB
		public static string GET_STRING_FRIEND_LIST_IS_EMPTY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_LIST_IS_EMPTY", false);
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x0600424E RID: 16974 RVA: 0x0014F2E8 File Offset: 0x0014D4E8
		public static string GET_STRING_FRIEND_LIST_BLOCK_IS_EMPTY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_LIST_BLOCK_IS_EMPTY", false);
			}
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x0600424F RID: 16975 RVA: 0x0014F2F5 File Offset: 0x0014D4F5
		public static string GET_STRING_FRIEND_LIST_RECV_IS_EMPTY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_LIST_RECV_IS_EMPTY", false);
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06004250 RID: 16976 RVA: 0x0014F302 File Offset: 0x0014D502
		public static string GET_STRING_FRIEND_LIST_REQ_IS_EMPTY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_LIST_REQ_IS_EMPTY", false);
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06004251 RID: 16977 RVA: 0x0014F30F File Offset: 0x0014D50F
		public static string GET_STRING_FRIEND_COUNT_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_COUNT_TWO_PARAM", false);
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06004252 RID: 16978 RVA: 0x0014F31C File Offset: 0x0014D51C
		public static string GET_STRING_FRIEND_BLOCK_COUNT_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_BLOCK_COUNT_TWO_PARAM", false);
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06004253 RID: 16979 RVA: 0x0014F329 File Offset: 0x0014D529
		public static string GET_STRING_FRIEND_RECV_COUNT_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_RECV_COUNT_TWO_PARAM", false);
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06004254 RID: 16980 RVA: 0x0014F336 File Offset: 0x0014D536
		public static string GET_STRING_FRIEND_REQ_COUNT_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_REQ_COUNT_TWO_PARAM", false);
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06004255 RID: 16981 RVA: 0x0014F343 File Offset: 0x0014D543
		public static string GET_STRING_FRIEND_ADD_REQ_COMPLETE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_ADD_REQ_COMPLETE", false);
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06004256 RID: 16982 RVA: 0x0014F350 File Offset: 0x0014D550
		public static string GET_STRING_FRIEND_EMBLEM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_EMBLEM", false);
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06004257 RID: 16983 RVA: 0x0014F35D File Offset: 0x0014D55D
		public static string GET_STRING_FRIEND_DELETE_REQ
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_DELETE_REQ", false);
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06004258 RID: 16984 RVA: 0x0014F36A File Offset: 0x0014D56A
		public static string GET_STRING_FRIEND_BLOCK_REQ_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_BLOCK_REQ_ONE_PARAM", false);
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x06004259 RID: 16985 RVA: 0x0014F377 File Offset: 0x0014D577
		public static string GET_STRING_FRIEND_BLOCK_CANCEL_NOTICE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_BLOCK_CANCEL_NOTICE", false);
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x0600425A RID: 16986 RVA: 0x0014F384 File Offset: 0x0014D584
		public static string GET_STRING_FRIEND_BLOCK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_BLOCK", false);
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x0600425B RID: 16987 RVA: 0x0014F391 File Offset: 0x0014D591
		public static string GET_STRING_FRIEND_UNBLOCK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_UNBLOCK", false);
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x0600425C RID: 16988 RVA: 0x0014F39E File Offset: 0x0014D59E
		public static string GET_STRING_FRIEND_REQ
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_REQ", false);
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x0600425D RID: 16989 RVA: 0x0014F3AB File Offset: 0x0014D5AB
		public static string GET_STRING_FRIEND_REQ_CANCEL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_REQ_CANCEL", false);
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x0600425E RID: 16990 RVA: 0x0014F3B8 File Offset: 0x0014D5B8
		public static string GET_STRING_FRIEND_REMOVE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_REMOVE", false);
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x0600425F RID: 16991 RVA: 0x0014F3C5 File Offset: 0x0014D5C5
		public static string GET_STRING_FRIEND_ACCEPT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_ACCEPT", false);
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06004260 RID: 16992 RVA: 0x0014F3D2 File Offset: 0x0014D5D2
		public static string GET_STRING_FRIEND_REFUSE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_REFUSE", false);
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06004261 RID: 16993 RVA: 0x0014F3DF File Offset: 0x0014D5DF
		public static string GET_STRING_FRIEND_TALK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_TALK", false);
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06004262 RID: 16994 RVA: 0x0014F3EC File Offset: 0x0014D5EC
		public static string GET_STRING_FRIEND_ROOM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_ROOM", false);
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06004263 RID: 16995 RVA: 0x0014F3F9 File Offset: 0x0014D5F9
		public static string GET_STRING_FRIEND_PVP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_PVP", false);
			}
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06004264 RID: 16996 RVA: 0x0014F406 File Offset: 0x0014D606
		public static string GET_STRING_ACCEPT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ACCEPT", false);
			}
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06004265 RID: 16997 RVA: 0x0014F413 File Offset: 0x0014D613
		public static string GET_STRING_REFUSE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REFUSE", false);
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06004266 RID: 16998 RVA: 0x0014F420 File Offset: 0x0014D620
		public static string GET_STRING_WORLDMAP_EVENT_STATE_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_EVENT_STATE_FAIL", false);
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06004267 RID: 16999 RVA: 0x0014F42D File Offset: 0x0014D62D
		public static string GET_STRING_WORLDMAP_EVENT_STATE_TIME_EXPIRED
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_EVENT_STATE_TIME_EXPIRED", false);
			}
		}

		// Token: 0x06004268 RID: 17000 RVA: 0x0014F43C File Offset: 0x0014D63C
		public static string GetWorldMapMissionType(NKMWorldMapMissionTemplet.WorldMapMissionType type)
		{
			switch (type)
			{
			case NKMWorldMapMissionTemplet.WorldMapMissionType.WMT_EXPLORE:
				return NKCStringTable.GetString("SI_DP_WORLD_MAP_MISSION_TYPE_WMT_EXPLORE", false);
			case NKMWorldMapMissionTemplet.WorldMapMissionType.WMT_DEFENCE:
				return NKCStringTable.GetString("SI_DP_WORLD_MAP_MISSION_TYPE_WMT_DEFENCE", false);
			case NKMWorldMapMissionTemplet.WorldMapMissionType.WMT_MINING:
				return NKCStringTable.GetString("SI_DP_WORLD_MAP_MISSION_TYPE_WMT_MINING", false);
			case NKMWorldMapMissionTemplet.WorldMapMissionType.WMT_OFFICE:
				return NKCStringTable.GetString("SI_DP_WORLD_MAP_MISSION_TYPE_WMT_OFFICE", false);
			default:
				return NKCStringTable.GetString("SI_DP_WORLD_MAP_MISSION_TYPE_WMT_DEFENCE", false);
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06004269 RID: 17001 RVA: 0x0014F49E File Offset: 0x0014D69E
		public static string GET_STRING_WORLDMAP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP", false);
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x0600426A RID: 17002 RVA: 0x0014F4AB File Offset: 0x0014D6AB
		public static string GET_STRING_WORLDMAP_RAID_WARNING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_RAID_WARNING", false);
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x0600426B RID: 17003 RVA: 0x0014F4B8 File Offset: 0x0014D6B8
		public static string GET_STRING_WORLDMAP_DIVE_WARNING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_DIVE_WARNING", false);
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x0600426C RID: 17004 RVA: 0x0014F4C5 File Offset: 0x0014D6C5
		public static string GET_STRING_MENU_NAME_WORLDMAP_BUILDING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MENU_NAME_WORLDMAP_BUILDING", false);
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x0600426D RID: 17005 RVA: 0x0014F4D2 File Offset: 0x0014D6D2
		public static string GET_STRING_WORLDMAP_BUILDING_BUILD
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_BUILDING_BUILD", false);
			}
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x0600426E RID: 17006 RVA: 0x0014F4DF File Offset: 0x0014D6DF
		public static string GET_STRING_WORLDMAP_BUILDING_MAX_LEVEL_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_BUILDING_MAX_LEVEL_ONE_PARAM", false);
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x0600426F RID: 17007 RVA: 0x0014F4EC File Offset: 0x0014D6EC
		public static string GET_STRING_WORLDMAP_BUILDING_SLOT_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_BUILDING_SLOT_TWO_PARAM", false);
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06004270 RID: 17008 RVA: 0x0014F4F9 File Offset: 0x0014D6F9
		public static string GET_STRING_WORLDMAP_BUILDING_REQ_CITY_LEVEL_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_BUILDING_REQ_CITY_LEVEL_ONE_PARAM", false);
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06004271 RID: 17009 RVA: 0x0014F506 File Offset: 0x0014D706
		public static string GET_STRING_WORLDMAP_BUILDING_REQ_BUILD_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_BUILDING_REQ_BUILD_TWO_PARAM", false);
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06004272 RID: 17010 RVA: 0x0014F513 File Offset: 0x0014D713
		public static string GET_STRING_WORLDMAP_BUILDING_CITY_COUNT_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_BUILDING_CITY_COUNT_ONE_PARAM", false);
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06004273 RID: 17011 RVA: 0x0014F520 File Offset: 0x0014D720
		public static string GET_STRING_WORLDMAP_BUILDING_CREDIT_REQ_LEVEL_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_BUILDING_CREDIT_REQ_LEVEL_ONE_PARAM", false);
			}
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06004274 RID: 17012 RVA: 0x0014F52D File Offset: 0x0014D72D
		public static string GET_STRING_WORLDMAP_BUILDING_DIVE_CLEAR_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_BUILDING_REQ_BUILD_DIVE_CLEAR_ONE_PARAM", false);
			}
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06004275 RID: 17013 RVA: 0x0014F53A File Offset: 0x0014D73A
		public static string GET_STRING_WORLDMAP_NO_EXIST_EVENT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_NO_EXIST_EVENT", false);
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06004276 RID: 17014 RVA: 0x0014F547 File Offset: 0x0014D747
		public static string GET_STRING_WORLDMAP_NO_EXIST_JOIN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_NO_EXIST_JOIN", false);
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06004277 RID: 17015 RVA: 0x0014F554 File Offset: 0x0014D754
		public static string GET_STRING_WORLDMAP_NO_EXIST_COOP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_NO_EXIST_COOP", false);
			}
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06004278 RID: 17016 RVA: 0x0014F561 File Offset: 0x0014D761
		public static string GET_STRING_WORLDMAP_GO_BUTTON
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_GO_BUTTON", false);
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06004279 RID: 17017 RVA: 0x0014F56E File Offset: 0x0014D76E
		public static string GET_STRING_WORLDMAP_BUILDING_ALREADY_BUILD
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_BUILDING_ALREADY_BUILD", false);
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x0600427A RID: 17018 RVA: 0x0014F57B File Offset: 0x0014D77B
		public static string GET_STRING_WORLDMAP_BUILDING_REQ_BUILDING_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_BUILDING_REQ_BUILDING_ONE_PARAM", false);
			}
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x0600427B RID: 17019 RVA: 0x0014F588 File Offset: 0x0014D788
		public static string GET_STRING_WORLDMAP_CITY_LEADER
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_CITY_LEADER", false);
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x0600427C RID: 17020 RVA: 0x0014F595 File Offset: 0x0014D795
		public static string GET_STRING_WORLDMAP_ANOTHER_CITY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_ANOTHER_CITY", false);
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x0600427D RID: 17021 RVA: 0x0014F5A2 File Offset: 0x0014D7A2
		public static string GET_STRING_WORLDMAP_CITY_SET_LEADER
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_CITY_SET_LEADER", false);
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x0600427E RID: 17022 RVA: 0x0014F5AF File Offset: 0x0014D7AF
		public static string GET_STRING_WORLDMAP_CITY_CHANGE_LEADER
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_CITY_CHANGE_LEADER", false);
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x0600427F RID: 17023 RVA: 0x0014F5BC File Offset: 0x0014D7BC
		public static string GET_STRING_WORLDMAP_CITY_SELECT_LEADER
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_CITY_SELECT_LEADER", false);
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06004280 RID: 17024 RVA: 0x0014F5C9 File Offset: 0x0014D7C9
		public static string GET_STRING_WORLDMAP_CITY_MISSION_CONFIRM_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_CITY_MISSION_CONFIRM_TWO_PARAM", false);
			}
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06004281 RID: 17025 RVA: 0x0014F5D6 File Offset: 0x0014D7D6
		public static string GET_STRING_WORLDMAP_CITY_MISSION_SELECT_SQUAD
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_CITY_MISSION_SELECT_SQUAD", false);
			}
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06004282 RID: 17026 RVA: 0x0014F5E3 File Offset: 0x0014D7E3
		public static string GET_STRING_WORLDMAP_CITY_MISSION_REFRESH
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_CITY_MISSION_REFRESH", false);
			}
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06004283 RID: 17027 RVA: 0x0014F5F0 File Offset: 0x0014D7F0
		public static string GET_STRING_WORLDMAP_CITY_MISSION_CANCEL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_CITY_MISSION_CANCEL", false);
			}
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06004284 RID: 17028 RVA: 0x0014F5FD File Offset: 0x0014D7FD
		public static string GET_STRING_WORLDMAP_CITY_MISSION_REQ_LEVEL_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_CITY_MISSION_REQ_LEVEL_ONE_PARAM", false);
			}
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06004285 RID: 17029 RVA: 0x0014F60A File Offset: 0x0014D80A
		public static string GET_STRING_WORLDMAP_CITY_MISSION_REWARD_ADD_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_CITY_MISSION_REWARD_ADD_TEXT", false);
			}
		}

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06004286 RID: 17030 RVA: 0x0014F617 File Offset: 0x0014D817
		public static string GET_STRING_WORLDMAP_CITY_MISSION_DOING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_CITY_MISSION_DOING", false);
			}
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06004287 RID: 17031 RVA: 0x0014F624 File Offset: 0x0014D824
		public static string GET_STRING_WORLDMAP_BUILDING_REMOVE_DESC_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_BUILDING_REMOVE_DESC_TWO_PARAM", false);
			}
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06004288 RID: 17032 RVA: 0x0014F631 File Offset: 0x0014D831
		public static string GET_STRING_WORLDMAP_BUILDING_REMOVE_POINT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_BUILDING_REMOVE_POINT", false);
			}
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06004289 RID: 17033 RVA: 0x0014F63E File Offset: 0x0014D83E
		public static string GET_STRING_WORLDMAP_HELP_BUTTON
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_HELP_BUTTON", false);
			}
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x0600428A RID: 17034 RVA: 0x0014F64B File Offset: 0x0014D84B
		public static string GET_STRING_WORLDMAP_PROGRESS_BUTTON_POPUP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_PROGRESS_BUTTON_POPUP", false);
			}
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x0600428B RID: 17035 RVA: 0x0014F658 File Offset: 0x0014D858
		public static string GET_STRING_MENU_NAME_WORLDMAP_NEW_BUILDING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MENU_NAME_WORLDMAP_NEW_BUILDING", false);
			}
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x0600428C RID: 17036 RVA: 0x0014F665 File Offset: 0x0014D865
		public static string GET_STRING_WORLDMAP_CITY_NO_EXIST_LEADER
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_CITY_NO_EXIST_LEADER", false);
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x0600428D RID: 17037 RVA: 0x0014F672 File Offset: 0x0014D872
		public static string GET_STRING_WORLDMAP_CITY_DATA_IS_NULL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_CITY_DATA_IS_NULL", false);
			}
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x0600428E RID: 17038 RVA: 0x0014F67F File Offset: 0x0014D87F
		public static string GET_STRING_POPUP_RESOURCE_WITHDRAW
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_POPUP_RESOURCE_WITHDRAW", false);
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x0600428F RID: 17039 RVA: 0x0014F68C File Offset: 0x0014D88C
		public static string GET_STRING_WORLDMAP_BUILDING_REMOVE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_BUILDING_REMOVE", false);
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06004290 RID: 17040 RVA: 0x0014F699 File Offset: 0x0014D899
		public static string GET_STRING_WORLDMAP_CITY_MAKE_COMPLETE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_CITY_MAKE_COMPLETE", false);
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06004291 RID: 17041 RVA: 0x0014F6A6 File Offset: 0x0014D8A6
		public static string GET_STRING_WORLDMAP_EVENT_POPUP_OK_CANCEL_RAID_LEVEL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_EVENT_POPUP_OK_CANCEL_RAID_LEVEL", false);
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06004292 RID: 17042 RVA: 0x0014F6B3 File Offset: 0x0014D8B3
		public static string GET_STRING_WORLDMAP_EVENT_POPUP_OK_CANCEL_DIVE_LEVEL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_EVENT_POPUP_OK_CANCEL_DIVE_LEVEL", false);
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06004293 RID: 17043 RVA: 0x0014F6C0 File Offset: 0x0014D8C0
		public static string GET_STRING_WORLDMAP_EVENT_POPUP_OK_CANCEL_NEW_RAID_DELETE_WARN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_EVENT_POPUP_OK_CANCEL_NEW_RAID_DELETE_WARN", false);
			}
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06004294 RID: 17044 RVA: 0x0014F6CD File Offset: 0x0014D8CD
		public static string GET_STRING_WORLDMAP_EVENT_POPUP_OK_CANCEL_NEW_DIVE_DELETE_WARN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_EVENT_POPUP_OK_CANCEL_NEW_DIVE_DELETE_WARN", false);
			}
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06004295 RID: 17045 RVA: 0x0014F6DA File Offset: 0x0014D8DA
		public static string GET_STRING_WORLDMAP_EVENT_POPUP_OK_CANCEL_ON_GOING_RAID_DELETE_WARN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_EVENT_POPUP_OK_CANCEL_ON_GOING_RAID_DELETE_WARN", false);
			}
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06004296 RID: 17046 RVA: 0x0014F6E7 File Offset: 0x0014D8E7
		public static string GET_STRING_WORLDMAP_EVENT_POPUP_OK_CANCEL_ON_GOING_DIVE_DELETE_WARN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLDMAP_EVENT_POPUP_OK_CANCEL_ON_GOING_DIVE_DELETE_WARN", false);
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06004297 RID: 17047 RVA: 0x0014F6F4 File Offset: 0x0014D8F4
		public static string GET_STRING_WORLDMAP_CITY_UNLOCK_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLD_MAP_CITY_UNLOCK_DESC", false);
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06004298 RID: 17048 RVA: 0x0014F701 File Offset: 0x0014D901
		public static string GET_STRING_ATTENDANCE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ATTENDANCE", false);
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06004299 RID: 17049 RVA: 0x0014F70E File Offset: 0x0014D90E
		public static string GET_STRING_NEWS_DOES_NOT_HAVE_NOTICE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NEWS_DOES_NOT_HAVE_NOTICE", false);
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x0600429A RID: 17050 RVA: 0x0014F71B File Offset: 0x0014D91B
		public static string GET_STRING_NEWS_DOES_NOT_HAVE_NEWS
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NEWS_DOES_NOT_HAVE_NEWS", false);
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x0600429B RID: 17051 RVA: 0x0014F728 File Offset: 0x0014D928
		public static string GET_STRING_RAID
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_RAID", false);
			}
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x0600429C RID: 17052 RVA: 0x0014F735 File Offset: 0x0014D935
		public static string GET_STRING_RAID_REQ_SUPPORT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_RAID_REQ_SUPPORT", false);
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x0600429D RID: 17053 RVA: 0x0014F742 File Offset: 0x0014D942
		public static string GET_STRING_RAID_REMAIN_COUNT_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_RAID_REMAIN_COUNT_ONE_PARAM", false);
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x0600429E RID: 17054 RVA: 0x0014F74F File Offset: 0x0014D94F
		public static string GET_STRING_RAID_COOP_REQ_WARNING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_RAID_COOP_REQ_WARNING", false);
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x0600429F RID: 17055 RVA: 0x0014F75C File Offset: 0x0014D95C
		public static string GET_STRING_RAID_SUPPORT_LIST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_RAID_SUPPORT_LIST", false);
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x060042A0 RID: 17056 RVA: 0x0014F769 File Offset: 0x0014D969
		public static string GET_STRING_WORLD_MAP_RAID_REMAIN_TIME
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WORLD_MAP_RAID_REMAIN_TIME", false);
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x060042A1 RID: 17057 RVA: 0x0014F776 File Offset: 0x0014D976
		public static string GET_STRING_WORLD_MAP_RAID_RIGHT_SUPPORT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_WORLD_MAP_RAID_RIGHT_SUPPORT", false);
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x060042A2 RID: 17058 RVA: 0x0014F783 File Offset: 0x0014D983
		public static string GET_STRING_WORLD_MAP_RAID_NOT_ASSIST_CHANGE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_WORLD_MAP_RAID_NOT_ASSIST_CHANGE", false);
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x060042A3 RID: 17059 RVA: 0x0014F790 File Offset: 0x0014D990
		public static string GET_STRING_WORLD_MAP_RAID_NOT_SCORE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_WORLD_MAP_RAID_NOT_SCORE", false);
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x060042A4 RID: 17060 RVA: 0x0014F79D File Offset: 0x0014D99D
		public static string GET_STRING_WORLD_MAP_RAID_NO_ASSIST_COUNT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_WORLD_MAP_RAID_NO_ASSIST_COUNT", false);
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x060042A5 RID: 17061 RVA: 0x0014F7AA File Offset: 0x0014D9AA
		public static string GET_STRING_ICON_SLOT_RAID_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_ICON_SLOT_RAID_01", false);
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x060042A6 RID: 17062 RVA: 0x0014F7B7 File Offset: 0x0014D9B7
		public static string GET_STRING_ICON_SLOT_RAID_02
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_ICON_SLOT_RAID_02", false);
			}
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x060042A7 RID: 17063 RVA: 0x0014F7C4 File Offset: 0x0014D9C4
		public static string GET_STRING_WORLD_MAP_RAID_SEASON_END
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_WORLD_MAP_RAID_SEASON_END", false);
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x060042A8 RID: 17064 RVA: 0x0014F7D1 File Offset: 0x0014D9D1
		public static string GET_STRING_CONTRACT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT", false);
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x060042A9 RID: 17065 RVA: 0x0014F7DE File Offset: 0x0014D9DE
		public static string GET_STRING_CONTRACT_EMERGENCY_CONTRACT_COUPON_USE_REQ
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_EMERGENCY_CONTRACT_COUPON_USE_REQ", false);
			}
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x060042AA RID: 17066 RVA: 0x0014F7EB File Offset: 0x0014D9EB
		public static string GET_STRING_CONTRACT_NEW_CONTRACT_SLOT_OPEN_REQ
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_NEW_CONTRACT_SLOT_OPEN_REQ", false);
			}
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x060042AB RID: 17067 RVA: 0x0014F7F8 File Offset: 0x0014D9F8
		public static string GET_STRING_CONTRACT_COMPLETE_CONTRACT_EXIST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_COMPLETE_CONTRACT_EXIST", false);
			}
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x060042AC RID: 17068 RVA: 0x0014F805 File Offset: 0x0014DA05
		public static string GET_STRING_CONTRACT_ON_GOING_CONTRACT_NUMBER_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_ON_GOING_CONTRACT_NUMBER_ONE_PARAM", false);
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x060042AD RID: 17069 RVA: 0x0014F812 File Offset: 0x0014DA12
		public static string GET_STRING_CONTRACT_ON_GOING_CONTRACT_NO_EXIST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_ON_GOING_CONTRACT_NO_EXIST", false);
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x060042AE RID: 17070 RVA: 0x0014F81F File Offset: 0x0014DA1F
		public static string GET_STRING_CONTRACT_AVAILABLE_SLOT_NO_EXIST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_AVAILABLE_SLOT_NO_EXIST", false);
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x060042AF RID: 17071 RVA: 0x0014F82C File Offset: 0x0014DA2C
		public static string GET_STRING_CONTRACT_UNIT_NUMBER
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_UNIT_NUMBER", false);
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x060042B0 RID: 17072 RVA: 0x0014F839 File Offset: 0x0014DA39
		public static string GET_STRING_CONTRACT_COUNT_CLOSE_TOOLTIP_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONTRACT_COUNT_CLOSE_TOOLTIP_TITLE", false);
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x060042B1 RID: 17073 RVA: 0x0014F846 File Offset: 0x0014DA46
		public static string GET_STRING_CONTRACT_COUNT_CLOSE_TOOLTIP_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONTRACT_COUNT_CLOSE_TOOLTIP_DESC", false);
			}
		}

		// Token: 0x060042B2 RID: 17074 RVA: 0x0014F854 File Offset: 0x0014DA54
		public static string GetUnitStyleType(NKM_UNIT_STYLE_TYPE type)
		{
			switch (type)
			{
			case NKM_UNIT_STYLE_TYPE.NUST_INVALID:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_TYPE_NUST_INVALID", false);
			case NKM_UNIT_STYLE_TYPE.NUST_COUNTER:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_TYPE_NUST_COUNTER", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SOLDIER:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_TYPE_NUST_SOLDIER", false);
			case NKM_UNIT_STYLE_TYPE.NUST_MECHANIC:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_TYPE_NUST_MECHANIC", false);
			case NKM_UNIT_STYLE_TYPE.NUST_CORRUPTED:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_TYPE_NUST_CORRUPTED", false);
			case NKM_UNIT_STYLE_TYPE.NUST_REPLACER:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_TYPE_NUST_REPLACER", false);
			case NKM_UNIT_STYLE_TYPE.NUST_TRAINER:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_TYPE_NUST_TRAINER", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_ASSAULT:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_TYPE_NUST_SHIP_ASSAULT", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_HEAVY:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_TYPE_NUST_SHIP_HEAVY", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_CRUISER:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_TYPE_NUST_SHIP_CRUISER", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_SPECIAL:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_TYPE_NUST_SHIP_SPECIAL", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_PATROL:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_TYPE_NUST_SHIP_PATROL", false);
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_ETC:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_TYPE_NUST_SHIP_ETC", false);
			default:
				return NKCStringTable.GetString("SI_DP_UNIT_STYLE_TYPE_UNKNOWN", false);
			}
		}

		// Token: 0x060042B3 RID: 17075 RVA: 0x0014F948 File Offset: 0x0014DB48
		public static string GetUnitRoleType(NKM_UNIT_ROLE_TYPE type, bool bAwaken)
		{
			if (bAwaken)
			{
				switch (type)
				{
				case NKM_UNIT_ROLE_TYPE.NURT_INVALID:
					return NKCStringTable.GetString("SI_DP_UNIT_ROLE_TYPE_NURT_INVALID", false);
				case NKM_UNIT_ROLE_TYPE.NURT_STRIKER:
					return NKCStringTable.GetString("SI_DP_UNIT_ROLE_TYPE_NURT_STRIKER_AWAKEN", false);
				case NKM_UNIT_ROLE_TYPE.NURT_RANGER:
					return NKCStringTable.GetString("SI_DP_UNIT_ROLE_TYPE_NURT_RANGER_AWAKEN", false);
				case NKM_UNIT_ROLE_TYPE.NURT_DEFENDER:
					return NKCStringTable.GetString("SI_DP_UNIT_ROLE_TYPE_NURT_DEFENDER_AWAKEN", false);
				case NKM_UNIT_ROLE_TYPE.NURT_SNIPER:
					return NKCStringTable.GetString("SI_DP_UNIT_ROLE_TYPE_NURT_SNIPER_AWAKEN", false);
				case NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER:
					return NKCStringTable.GetString("SI_DP_UNIT_ROLE_TYPE_NURT_SUPPORTER_AWAKEN", false);
				case NKM_UNIT_ROLE_TYPE.NURT_SIEGE:
					return NKCStringTable.GetString("SI_DP_UNIT_ROLE_TYPE_NURT_SIEGE_AWAKEN", false);
				case NKM_UNIT_ROLE_TYPE.NURT_TOWER:
					return NKCStringTable.GetString("SI_DP_UNIT_ROLE_TYPE_NURT_TOWER_AWAKEN", false);
				}
			}
			else
			{
				switch (type)
				{
				case NKM_UNIT_ROLE_TYPE.NURT_INVALID:
					return NKCStringTable.GetString("SI_DP_UNIT_ROLE_TYPE_NURT_INVALID", false);
				case NKM_UNIT_ROLE_TYPE.NURT_STRIKER:
					return NKCStringTable.GetString("SI_DP_UNIT_ROLE_TYPE_NURT_STRIKER", false);
				case NKM_UNIT_ROLE_TYPE.NURT_RANGER:
					return NKCStringTable.GetString("SI_DP_UNIT_ROLE_TYPE_NURT_RANGER", false);
				case NKM_UNIT_ROLE_TYPE.NURT_DEFENDER:
					return NKCStringTable.GetString("SI_DP_UNIT_ROLE_TYPE_NURT_DEFENDER", false);
				case NKM_UNIT_ROLE_TYPE.NURT_SNIPER:
					return NKCStringTable.GetString("SI_DP_UNIT_ROLE_TYPE_NURT_SNIPER", false);
				case NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER:
					return NKCStringTable.GetString("SI_DP_UNIT_ROLE_TYPE_NURT_SUPPORTER", false);
				case NKM_UNIT_ROLE_TYPE.NURT_SIEGE:
					return NKCStringTable.GetString("SI_DP_UNIT_ROLE_TYPE_NURT_SIEGE", false);
				case NKM_UNIT_ROLE_TYPE.NURT_TOWER:
					return NKCStringTable.GetString("SI_DP_UNIT_ROLE_TYPE_NURT_TOWER", false);
				}
			}
			return NKCStringTable.GetString("SI_DP_UNIT_ROLE_TYPE_UNKNOWN", false);
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x060042B4 RID: 17076 RVA: 0x0014FA79 File Offset: 0x0014DC79
		public static string GET_STRING_CONTRACT_SLOT_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_SLOT_UNIT", false);
			}
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x060042B5 RID: 17077 RVA: 0x0014FA86 File Offset: 0x0014DC86
		public static string GET_STRING_CONTRACT_NOT_ENOUGH_LIMIT_COUNT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONTRACT_NOT_ENOUGH_TRY_COUNT", false);
			}
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x060042B6 RID: 17078 RVA: 0x0014FA93 File Offset: 0x0014DC93
		public static string GET_STRING_CONTRACT_NOT_ENOUGH_LIMIT_REQ_ITEM_COUNT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONTRACT_NOT_ENOUGH_LIMIT_REQ_ITEM_COUNT", false);
			}
		}

		// Token: 0x060042B7 RID: 17079 RVA: 0x0014FAA0 File Offset: 0x0014DCA0
		public static string GetMenuName(NKCUIUnitInfo.UNIT_INFO_TAB_STATE state)
		{
			switch (state)
			{
			default:
				return "";
			case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.NEGOTIATION:
				return NKCUtilString.GET_STRING_NEGOTIATE;
			case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.LIMIT_BREAK:
				return NKCStringTable.GetString("SI_DP_LAB_SELECT_UNIT_MENU_NAME_LDS_UNIT_LIMITBREAK", false);
			case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.SKILL_TRAIN:
				return NKCStringTable.GetString("SI_DP_LAB_SELECT_UNIT_MENU_NAME_LDS_UNIT_SKILL_TRAIN", false);
			}
		}

		// Token: 0x060042B8 RID: 17080 RVA: 0x0014FADD File Offset: 0x0014DCDD
		public static string GetEmptyMessage(NKCUIUnitInfo.UNIT_INFO_TAB_STATE state)
		{
			switch (state)
			{
			default:
				return "";
			case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.NEGOTIATION:
				return NKCUtilString.GET_STRING_NEGOTIATE_NO_EXIST_UNIT;
			case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.LIMIT_BREAK:
				return NKCStringTable.GetString("SI_DP_LAB_SELECT_UNIT_EMPTY_MSG_LIMITBREAK", false);
			case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.SKILL_TRAIN:
				return NKCStringTable.GetString("SI_DP_LAB_SELECT_UNIT_EMPTY_MSG_SKILL_TRAIN", false);
			}
		}

		// Token: 0x060042B9 RID: 17081 RVA: 0x0014FB1C File Offset: 0x0014DD1C
		public static string GetLabMenuName(NKCUILab.LAB_DETAIL_STATE state)
		{
			switch (state)
			{
			default:
				return NKCStringTable.GetString("SI_DP_LAB_MENU_NAME_LDS_MENU", false);
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_ENHANCE:
				return NKCStringTable.GetString("SI_DP_LAB_MENU_NAME_LDS_UNIT_ENHANCE", false);
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_LIMITBREAK:
				return NKCStringTable.GetString("SI_DP_LAB_MENU_NAME_LDS_UNIT_LIMITBREAK", false);
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_SKILL_TRAIN:
				return NKCStringTable.GetString("SI_DP_LAB_MENU_NAME_LDS_UNIT_SKILL_TRAIN", false);
			}
		}

		// Token: 0x060042BA RID: 17082 RVA: 0x0014FB70 File Offset: 0x0014DD70
		public static string GetLabSelectUnitMenuName(NKCUILab.LAB_DETAIL_STATE state)
		{
			switch (state)
			{
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_ENHANCE:
				return NKCStringTable.GetString("SI_DP_LAB_SELECT_UNIT_MENU_NAME_LDS_UNIT_ENHANCE", false);
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_LIMITBREAK:
				return NKCStringTable.GetString("SI_DP_LAB_SELECT_UNIT_MENU_NAME_LDS_UNIT_LIMITBREAK", false);
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_SKILL_TRAIN:
				return NKCStringTable.GetString("SI_DP_LAB_SELECT_UNIT_MENU_NAME_LDS_UNIT_SKILL_TRAIN", false);
			default:
				return "";
			}
		}

		// Token: 0x060042BB RID: 17083 RVA: 0x0014FBBC File Offset: 0x0014DDBC
		public static string GetLabSelectUnitEmptyMsg(NKCUILab.LAB_DETAIL_STATE state)
		{
			switch (state)
			{
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_ENHANCE:
				return NKCStringTable.GetString("SI_DP_LAB_SELECT_UNIT_EMPTY_MSG_ENHANCE", false);
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_LIMITBREAK:
				return NKCStringTable.GetString("SI_DP_LAB_SELECT_UNIT_EMPTY_MSG_LIMITBREAK", false);
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_SKILL_TRAIN:
				return NKCStringTable.GetString("SI_DP_LAB_SELECT_UNIT_EMPTY_MSG_SKILL_TRAIN", false);
			default:
				return "";
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x060042BC RID: 17084 RVA: 0x0014FC08 File Offset: 0x0014DE08
		public static string GET_STRING_ALREADY_ENHANCE_MAX
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ALREADY_ENHANCE_MAX", false);
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x060042BD RID: 17085 RVA: 0x0014FC15 File Offset: 0x0014DE15
		public static string GET_STRING_LIMITBREAK_GROWTH_INFO_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LIMITBREAK_GROWTH_INFO_ONE_PARAM", false);
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x060042BE RID: 17086 RVA: 0x0014FC22 File Offset: 0x0014DE22
		public static string GET_STRING_LIMITBREAK_NO_EXIST_CONSUME_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LIMITBREAK_NO_EXIST_CONSUME_UNIT", false);
			}
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x060042BF RID: 17087 RVA: 0x0014FC2F File Offset: 0x0014DE2F
		public static string GET_STRING_LIMITBTEAK_INFO
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LIMITBTEAK_INFO", false);
			}
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x060042C0 RID: 17088 RVA: 0x0014FC3C File Offset: 0x0014DE3C
		public static string GET_STRING_LIMITBREAK_WARNING_SELECT_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LIMITBREAK_SELECT_HIGH_GRADE", false);
			}
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x060042C1 RID: 17089 RVA: 0x0014FC49 File Offset: 0x0014DE49
		public static string GET_STRING_LIMITBREAK_WARNING_RUN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LIMITBREAK_INCLUDE_HIGH_GRADE", false);
			}
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x060042C2 RID: 17090 RVA: 0x0014FC56 File Offset: 0x0014DE56
		public static string GET_STRING_LIMITBREAK_WARNING_CANCEL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LIMITBREAK_CANCELED", false);
			}
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x060042C3 RID: 17091 RVA: 0x0014FC63 File Offset: 0x0014DE63
		public static string GET_STRING_LIMITBREAK_CONFIRM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LIMITBREAK_CONFIRM", false);
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x060042C4 RID: 17092 RVA: 0x0014FC70 File Offset: 0x0014DE70
		public static string GET_STRING_LIMITBREAK_CONFIRM_AWAKEN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LIMITBREAK_CONFIRM_AWAKEN", false);
			}
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x060042C5 RID: 17093 RVA: 0x0014FC7D File Offset: 0x0014DE7D
		public static string GET_STRING_LIMITBREAK_TRANSCENDENCE_LEVEL_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LIMITBREAK_TRANSCENDENCE_LEVEL_ONE_PARAM", false);
			}
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x060042C6 RID: 17094 RVA: 0x0014FC8A File Offset: 0x0014DE8A
		public static string GET_STRING_SKILL_ATTACK_COUNT_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SKILL_ATTACK_COUNT_ONE_PARAM", false);
			}
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x060042C7 RID: 17095 RVA: 0x0014FC97 File Offset: 0x0014DE97
		public static string GET_STRING_ENHANCE_NEED_SET_TARGET_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ENHANCE_NEED_SET_TARGET_UNIT", false);
			}
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x060042C8 RID: 17096 RVA: 0x0014FCA4 File Offset: 0x0014DEA4
		public static string GET_STRING_ENHANCE_NEED_SET_CONSUME_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ENHANCE_NEED_SET_CONSUME_UNIT", false);
			}
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x060042C9 RID: 17097 RVA: 0x0014FCB1 File Offset: 0x0014DEB1
		public static string GET_STRING_ENHANCE_SELECT_CONSUM_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ENHANCE_SELECT_CONSUM_UNIT", false);
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x060042CA RID: 17098 RVA: 0x0014FCBE File Offset: 0x0014DEBE
		public static string GET_STRING_ENHANCE_NO_EXIST_CONSUME_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ENHANCE_NO_EXIST_CONSUME_UNIT", false);
			}
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x060042CB RID: 17099 RVA: 0x0014FCCB File Offset: 0x0014DECB
		public static string GET_STRING_LIMITBREAK_RESULT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LIMITBREAK_RESULT", false);
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x060042CC RID: 17100 RVA: 0x0014FCD8 File Offset: 0x0014DED8
		public static string GET_STRING_ALREADY_LIMITBREAK_MAX
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ALREADY_LIMITBREAK_MAX", false);
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x060042CD RID: 17101 RVA: 0x0014FCE5 File Offset: 0x0014DEE5
		public static string GET_STRING_LIMITBREAK_RESULT_GROWTH_INFO_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LIMITBREAK_RESULT_GROWTH_INFO_ONE_PARAM", false);
			}
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x060042CE RID: 17102 RVA: 0x0014FCF2 File Offset: 0x0014DEF2
		public static string GET_STRING_OPTION_CUTSCEN_NEXT_TALK_SPEED_WHEN_AUTO_FAST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OPTION_CUTSCENE_FAST", false);
			}
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x060042CF RID: 17103 RVA: 0x0014FCFF File Offset: 0x0014DEFF
		public static string GET_STRING_OPTION_CUTSCEN_NEXT_TALK_SPEED_WHEN_AUTO_NORMAL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OPTION_CUTSCENE_NORMAL", false);
			}
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x060042D0 RID: 17104 RVA: 0x0014FD0C File Offset: 0x0014DF0C
		public static string GET_STRING_OPTION_CUTSCEN_NEXT_TALK_SPEED_WHEN_AUTO_SLOW
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OPTION_CUTSCENE_SLOW", false);
			}
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x060042D1 RID: 17105 RVA: 0x0014FD19 File Offset: 0x0014DF19
		public static string GET_STRING_OPTION_RESET_WARNING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OPTION_RESET_WARNING", false);
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x060042D2 RID: 17106 RVA: 0x0014FD26 File Offset: 0x0014DF26
		public static string GET_STRING_OPTION_DROPOUT_WARNING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OPTION_DROPOUT_WARNING", false);
			}
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x060042D3 RID: 17107 RVA: 0x0014FD33 File Offset: 0x0014DF33
		public static string GET_STRING_OPTION_DROPOUT_WARNING_INSTANT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OPTION_DROPOUT_WARNING_INSTANT", false);
			}
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x060042D4 RID: 17108 RVA: 0x0014FD40 File Offset: 0x0014DF40
		public static string GET_STRING_OPTION_CONNECTED
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OPTION_CONNECTED", false);
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x060042D5 RID: 17109 RVA: 0x0014FD4D File Offset: 0x0014DF4D
		public static string GET_STRING_OPTION_DISCONNECTED
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OPTION_DISCONNECTED", false);
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x060042D6 RID: 17110 RVA: 0x0014FD5A File Offset: 0x0014DF5A
		public static string GET_STRING_OPTION_LOGOUT_REQ
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OPTION_LOGOUT_REQ", false);
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x060042D7 RID: 17111 RVA: 0x0014FD67 File Offset: 0x0014DF67
		public static string GET_STRING_OPTION_CANNOT_LOG_OUT_WHEN_IN_GAME_BATTLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OPTION_CANNOT_LOG_OUT_WHEN_IN_GAME_BATTLE", false);
			}
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x060042D8 RID: 17112 RVA: 0x0014FD74 File Offset: 0x0014DF74
		public static string GET_STRING_OPTION_HIGH_QUALITY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OPTION_HIGH_QUALITY", false);
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x060042D9 RID: 17113 RVA: 0x0014FD81 File Offset: 0x0014DF81
		public static string GET_STRING_OPTION_CHANGE_WARNING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OPTION_CHANGE_WARNING", false);
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x060042DA RID: 17114 RVA: 0x0014FD8E File Offset: 0x0014DF8E
		public static string GET_STRING_OPTION_30_FPS
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OPTION_30_FPS", false);
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x060042DB RID: 17115 RVA: 0x0014FD9B File Offset: 0x0014DF9B
		public static string GET_STRING_OPTION_60_FPS
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OPTION_60_FPS", false);
			}
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x060042DC RID: 17116 RVA: 0x0014FDA8 File Offset: 0x0014DFA8
		public static string GET_STRING_OPTION_MISSION_GIVE_UP_WARNING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OPTION_MISSION_GIVE_UP_WARNING", false);
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x060042DD RID: 17117 RVA: 0x0014FDB5 File Offset: 0x0014DFB5
		public static string GET_STRING_OPTION_GAME_LANG_CHANGE_REQ
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OPTION_GAME_LANG_CHANGE_REQ", false);
			}
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x060042DE RID: 17118 RVA: 0x0014FDC2 File Offset: 0x0014DFC2
		public static string GET_STRING_OPTION_MEDAL_COND
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OPTION_MEDAL_COND", false);
			}
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x060042DF RID: 17119 RVA: 0x0014FDCF File Offset: 0x0014DFCF
		public static string GET_STRING_OPTION_RANK_COND
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OPTION_RANK_COND", false);
			}
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x060042E0 RID: 17120 RVA: 0x0014FDDC File Offset: 0x0014DFDC
		public static string GET_STRING_EXIST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EXIST", false);
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x060042E1 RID: 17121 RVA: 0x0014FDE9 File Offset: 0x0014DFE9
		public static string GET_STRING_LIMITED
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LIMITED", false);
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x060042E2 RID: 17122 RVA: 0x0014FDF6 File Offset: 0x0014DFF6
		public static string GET_STRING_OPTION_LANGUAGE_CHANGE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OPTION_LANGUAGE_CHANGE", false);
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x060042E3 RID: 17123 RVA: 0x0014FE03 File Offset: 0x0014E003
		public static string GET_STRING_GAME_OPTION_ACCOUNT_NEXON_WITHDRAWAL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAME_OPTION_ACCOUNT_NEXON_WITHDRAWAL", false);
			}
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x060042E4 RID: 17124 RVA: 0x0014FE10 File Offset: 0x0014E010
		public static string GET_STRING_OPTION_DROPOUT_WARNING_NEXON
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OPTION_DROPOUT_WARNING_NEXON", false);
			}
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x060042E5 RID: 17125 RVA: 0x0014FE1D File Offset: 0x0014E01D
		public static string GET_STRING_OPTION_MISSION_GIVE_UP_WARNING_MULTIPLY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MULTIPLY_OPERATION_DUNGEON_OUT_POPUP_DESC", false);
			}
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x060042E6 RID: 17126 RVA: 0x0014FE2A File Offset: 0x0014E02A
		public static string GET_STRING_OPTION_SIGN_OUT_MESSAGE_MISS_MATCHED
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_POPUP_SIGN_OUT_ERROR_TEXT", false);
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x060042E7 RID: 17127 RVA: 0x0014FE37 File Offset: 0x0014E037
		public static string GET_STRING_FORGE_CRAFT_COUNT_INFINITE_SYMBOL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_CRAFT_COUNT_INFINITE_SYMBOL", false);
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x060042E8 RID: 17128 RVA: 0x0014FE44 File Offset: 0x0014E044
		public static string GET_STRING_FORGE_CRAFT_POPUP_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_CRAFT_POPUP_TITLE", false);
			}
		}

		// Token: 0x060042E9 RID: 17129 RVA: 0x0014FE54 File Offset: 0x0014E054
		public static string GetRefineResultMsg(NKM_EQUIP_REFINE_RESULT eNKM_EQUIP_REFINE_RESULT)
		{
			if (eNKM_EQUIP_REFINE_RESULT == NKM_EQUIP_REFINE_RESULT.NERR_SUCCESS)
			{
				return NKCStringTable.GetString("SI_DP_REFINE_RESULT_MSG_NERR_SUCCESS", false);
			}
			if (eNKM_EQUIP_REFINE_RESULT == NKM_EQUIP_REFINE_RESULT.NERR_GREAT_SUCCESS)
			{
				return NKCStringTable.GetString("SI_DP_REFINE_RESULT_MSG_NERR_GREAT_SUCCESS", false);
			}
			if (eNKM_EQUIP_REFINE_RESULT == NKM_EQUIP_REFINE_RESULT.NERR_FAIL)
			{
				return NKCStringTable.GetString("SI_DP_REFINE_RESULT_MSG_NERR_FAIL", false);
			}
			if (eNKM_EQUIP_REFINE_RESULT == NKM_EQUIP_REFINE_RESULT.NERR_GREAT_FAIL)
			{
				return NKCStringTable.GetString("SI_DP_REFINE_RESULT_MSG_NERR_GREAT_FAIL", false);
			}
			return NKCStringTable.GetString("SI_DP_REFINE_RESULT_MSG_UNKNOWN", false);
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x060042EA RID: 17130 RVA: 0x0014FEAB File Offset: 0x0014E0AB
		public static string GET_STRING_FORGE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE", false);
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x060042EB RID: 17131 RVA: 0x0014FEB8 File Offset: 0x0014E0B8
		public static string GET_STRING_NO_EXIST_ENCHANT_EQUIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NO_EXIST_ENCHANT_EQUIP", false);
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x060042EC RID: 17132 RVA: 0x0014FEC5 File Offset: 0x0014E0C5
		public static string GET_STRING_NO_EXIST_TUNING_EQUIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NO_EXIST_TUNING_EQUIP", false);
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x060042ED RID: 17133 RVA: 0x0014FED2 File Offset: 0x0014E0D2
		public static string GET_STRING_NO_EXIST_HIDDEN_OPTION_EQUIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NO_EXIST_HIDDEN_OPTION_EQUIP", false);
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x060042EE RID: 17134 RVA: 0x0014FEDF File Offset: 0x0014E0DF
		public static string GET_STRING_FORGE_CRAFT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_CRAFT", false);
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x060042EF RID: 17135 RVA: 0x0014FEEC File Offset: 0x0014E0EC
		public static string GET_STRING_FORGE_CRAFT_USE_MISC_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_CRAFT_USE_MISC_TWO_PARAM", false);
			}
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x060042F0 RID: 17136 RVA: 0x0014FEF9 File Offset: 0x0014E0F9
		public static string GET_STRING_FORGE_CRAFT_COUNT_INFINITE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_CRAFT_COUNT_INFINITE", false);
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x060042F1 RID: 17137 RVA: 0x0014FF06 File Offset: 0x0014E106
		public static string GET_STRING_FORGE_CRAFT_MOLD
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_CRAFT_MOLD", false);
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x060042F2 RID: 17138 RVA: 0x0014FF13 File Offset: 0x0014E113
		public static string GET_STRING_FORGE_CRAFT_SLOT_ADD
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_CRAFT_SLOT_ADD", false);
			}
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x060042F3 RID: 17139 RVA: 0x0014FF20 File Offset: 0x0014E120
		public static string GET_STRING_FORGE_CRAFT_WAIT_NAME
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_CRAFT_WAIT_NAME", false);
			}
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x060042F4 RID: 17140 RVA: 0x0014FF2D File Offset: 0x0014E12D
		public static string GET_STRING_FORGE_CRAFT_WAIT_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_CRAFT_WAIT_TEXT", false);
			}
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x060042F5 RID: 17141 RVA: 0x0014FF3A File Offset: 0x0014E13A
		public static string GET_STRING_FORGE_CRAFT_ING_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_CRAFT_ING_TEXT", false);
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x060042F6 RID: 17142 RVA: 0x0014FF47 File Offset: 0x0014E147
		public static string GET_STRING_FORGE_CRAFT_COMPLETED_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_CRAFT_COMPLETED_TEXT", false);
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x060042F7 RID: 17143 RVA: 0x0014FF54 File Offset: 0x0014E154
		public static string GET_STRING_FORGE_ENCHANT_LEVEL_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_ENCHANT_LEVEL_ONE_PARAM", false);
			}
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x060042F8 RID: 17144 RVA: 0x0014FF61 File Offset: 0x0014E161
		public static string GET_STRING_FORGE_ENCHANT_ALREADY_MAX
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_ENCHANT_ALREADY_MAX", false);
			}
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x060042F9 RID: 17145 RVA: 0x0014FF6E File Offset: 0x0014E16E
		public static string GET_STRING_FORGE_ENCHANT_NEED_CONSUME
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_ENCHANT_NEED_CONSUME", false);
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x060042FA RID: 17146 RVA: 0x0014FF7B File Offset: 0x0014E17B
		public static string GET_STRING_FORGE_ENCHANT_NO_EXIST_CONSUME
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_ENCHANT_NO_EXIST_CONSUME", false);
			}
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x060042FB RID: 17147 RVA: 0x0014FF88 File Offset: 0x0014E188
		public static string GET_STRING_FORGE_TUNING_CONFIRM_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_TUNING_CONFIRM_TITLE", false);
			}
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x060042FC RID: 17148 RVA: 0x0014FF95 File Offset: 0x0014E195
		public static string GET_STRING_FORGE_TUNING_CONFIRM_DESC_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_TUNING_CONFIRM_DESC_TWO_PARAM", false);
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x060042FD RID: 17149 RVA: 0x0014FFA2 File Offset: 0x0014E1A2
		public static string GET_STRING_FORGE_TUNING_HAS_RESERVED_EQUIP_TUNING
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FORGE_TUNING_HAS_RESERVED_EQUIP_TUNING", false);
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x060042FE RID: 17150 RVA: 0x0014FFAF File Offset: 0x0014E1AF
		public static string GET_STRING_FORGE_TUNNING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_TUNNING", false);
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x060042FF RID: 17151 RVA: 0x0014FFBC File Offset: 0x0014E1BC
		public static string GET_STRING_FORGE_TUNING_STAT_CURRENT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_TUNING_STAT_CURRENT", false);
			}
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06004300 RID: 17152 RVA: 0x0014FFC9 File Offset: 0x0014E1C9
		public static string GET_STRING_FORGE_TUNING_STAT_CHANGE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_TUNING_STAT_CHANGE", false);
			}
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06004301 RID: 17153 RVA: 0x0014FFD6 File Offset: 0x0014E1D6
		public static string GET_STRING_EQUIP_BREAK_UP_NO_EXIST_EQUIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EQUIP_BREAK_UP_NO_EXIST_EQUIP", false);
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06004302 RID: 17154 RVA: 0x0014FFE3 File Offset: 0x0014E1E3
		public static string GET_STRING_NO_EXIST_SELECTED_EQUIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NO_EXIST_SELECTED_EQUIP", false);
			}
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06004303 RID: 17155 RVA: 0x0014FFF0 File Offset: 0x0014E1F0
		public static string GET_STRING_EQUIP_BREAK_UP_WARNING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EQUIP_BREAK_UP_WARNING", false);
			}
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06004304 RID: 17156 RVA: 0x0014FFFD File Offset: 0x0014E1FD
		public static string GET_STRING_TUNING_OPTIN_NONE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TUNING_OPTIN_NONE", false);
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06004305 RID: 17157 RVA: 0x0015000A File Offset: 0x0014E20A
		public static string GET_STRING_TUNING_OPTIN_CAN_NOT_CHANGE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TUNING_OPTIN_CAN_NOT_CHANGE", false);
			}
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06004306 RID: 17158 RVA: 0x00150017 File Offset: 0x0014E217
		public static string GET_STRING_TUNING_OPTION_SLOT_EXCLUSIVE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TUNING_OPTION_SLOT_EXCLUSIVE", false);
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06004307 RID: 17159 RVA: 0x00150024 File Offset: 0x0014E224
		public static string GET_STRING_TUNING_OPTION_SLOT_OPTION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TUNING_OPTION_SLOT_OPTION", false);
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06004308 RID: 17160 RVA: 0x00150031 File Offset: 0x0014E231
		public static string GET_STRING_FORGE_CRAFT_ITEM_NO_FOUND
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_CRAFT_ITEM_NO_FOUND", false);
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06004309 RID: 17161 RVA: 0x0015003E File Offset: 0x0014E23E
		public static string GET_STRING_FORGE_CRAFT_POPUP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_CRAFT_POPUP", false);
			}
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x0600430A RID: 17162 RVA: 0x0015004B File Offset: 0x0014E24B
		public static string GET_STRING_FORGE_TUNING_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_TUNING_FAIL", false);
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x0600430B RID: 17163 RVA: 0x00150058 File Offset: 0x0014E258
		public static string GET_STRING_FORGE_TUNING_PRECISION_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_TUNING_PRECISION_ONE_PARAM", false);
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x0600430C RID: 17164 RVA: 0x00150065 File Offset: 0x0014E265
		public static string GET_STRING_FORGE_TUNING_STAT_BASE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_TUNING_STAT_BASE", false);
			}
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x0600430D RID: 17165 RVA: 0x00150072 File Offset: 0x0014E272
		public static string GET_STRING_FORGE_TUNING_STAT_RESULT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_TUNING_STAT_RESULT", false);
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x0600430E RID: 17166 RVA: 0x0015007F File Offset: 0x0014E27F
		public static string GET_STRING_FORGE_TUNING_COMPLETE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_TUNING_COMPLETE", false);
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x0600430F RID: 17167 RVA: 0x0015008C File Offset: 0x0014E28C
		public static string GET_STRING_EQUIP_BREAK_UP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EQUIP_BREAK_UP", false);
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06004310 RID: 17168 RVA: 0x00150099 File Offset: 0x0014E299
		public static string GET_STRING_FORGE_SET_OPTION_CHANGE_POPUP_CONFIRM_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_SET_POPUP_CONFIRM_TITLE", false);
			}
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06004311 RID: 17169 RVA: 0x001500A6 File Offset: 0x0014E2A6
		public static string GET_STRING_FORGE_SET_OPTION_CHANGE_POPUP_CONFIRM_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_SET_POPUP_CONFIRM_DESC", false);
			}
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06004312 RID: 17170 RVA: 0x001500B3 File Offset: 0x0014E2B3
		public static string GET_STRING_EQUIP_SELECT_ACC_1
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EQUIP_WHERE_ACC_SLOT_1", false);
			}
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06004313 RID: 17171 RVA: 0x001500C0 File Offset: 0x0014E2C0
		public static string GET_STRING_EQUIP_SELECT_ACC_2
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EQUIP_WHERE_ACC_SLOT_2", false);
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06004314 RID: 17172 RVA: 0x001500CD File Offset: 0x0014E2CD
		public static string GET_STRING_EQUIP_ACC_2_LOCKED_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_TOAST_EQUIP_SLOT_ACC2_IS_LOCKED", false);
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06004315 RID: 17173 RVA: 0x001500DA File Offset: 0x0014E2DA
		public static string GET_STRING_FORGE_TUNING_SET_NO_OPTION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_SET_NO_OPTION", false);
			}
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06004316 RID: 17174 RVA: 0x001500E7 File Offset: 0x0014E2E7
		public static string GET_STRING_FORGE_TUNING_SET_OPTION_CANNOT_CHANGE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_SET_CANNOT_CHANGE", false);
			}
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06004317 RID: 17175 RVA: 0x001500F4 File Offset: 0x0014E2F4
		public static string GET_STRING_FORGE_CRAFT_USE_MISC_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_CRAFT_USE_MISC_ONE_PARAM", false);
			}
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x06004318 RID: 17176 RVA: 0x00150101 File Offset: 0x0014E301
		public static string GET_STRING_FORGE_CRAFT_MOLD_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_CRAFT_MOLD_DESC", false);
			}
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x06004319 RID: 17177 RVA: 0x0015010E File Offset: 0x0014E30E
		public static string GET_STRING_FORGE_TUNING_EXIT_CONFIRM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_TUNING_EXIT_CONFIRM", false);
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x0600431A RID: 17178 RVA: 0x0015011B File Offset: 0x0014E31B
		public static string GET_STRING_FORGE_SET_OPTION_TUNING_EXIT_CONFIRM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FORGE_SET_EXIT_CONFIRM", false);
			}
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x0600431B RID: 17179 RVA: 0x00150128 File Offset: 0x0014E328
		public static string GET_STRING_SORT_CRAFTABLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SORT_CRAFTABLE", false);
			}
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x0600431C RID: 17180 RVA: 0x00150135 File Offset: 0x0014E335
		public static string GET_STRING_SORT_SETOPTION
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_SORT_EQUIP_SET_OPTION", false);
			}
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x0600431D RID: 17181 RVA: 0x00150142 File Offset: 0x0014E342
		public static string GET_STRING_IMPOSSIBLE_TUNING_BY_WARFARE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_IMPOSSIBLE_TUNING_BY_WARFARE", false);
			}
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x0600431E RID: 17182 RVA: 0x0015014F File Offset: 0x0014E34F
		public static string GET_STRING_IMPOSSIBLE_TUNING_BY_DIVE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_IMPOSSIBLE_TUNING_BY_DIVE", false);
			}
		}

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x0600431F RID: 17183 RVA: 0x0015015C File Offset: 0x0014E35C
		public static string GET_STRING_SETOPTION_CHANGE_NOTICE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_SETOPTION_CHANGE_NOTICE", false);
			}
		}

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06004320 RID: 17184 RVA: 0x00150169 File Offset: 0x0014E369
		public static string GET_STRING_HANGAR_SHIPYARD_SKILL_NEW
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_HANGAR_SHIPYARD_SKILL_NEW", false);
			}
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06004321 RID: 17185 RVA: 0x00150176 File Offset: 0x0014E376
		public static string GET_STRING_HANGAR_SHIPYARD_SKILL_UPGRADE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_HANGAR_SHIPYARD_SKILL_UPGRADE", false);
			}
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06004322 RID: 17186 RVA: 0x00150183 File Offset: 0x0014E383
		public static string GET_STRING_HANGAR_BUILD
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_HANGAR_BUILD", false);
			}
		}

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06004323 RID: 17187 RVA: 0x00150190 File Offset: 0x0014E390
		public static string GET_STRING_HANGAR_BUILD_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_HANGAR_BUILD_FAIL", false);
			}
		}

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06004324 RID: 17188 RVA: 0x0015019D File Offset: 0x0014E39D
		public static string GET_STRING_HANGAR_CONFIRM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_HANGAR_CONFIRM", false);
			}
		}

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06004325 RID: 17189 RVA: 0x001501AA File Offset: 0x0014E3AA
		public static string GET_STRING_HANGAR_CONFIRM_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_HANGAR_CONFIRM_FAIL", false);
			}
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06004326 RID: 17190 RVA: 0x001501B7 File Offset: 0x0014E3B7
		public static string GET_STRING_HANGAR_SHIPYARD
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_HANGAR_SHIPYARD", false);
			}
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x06004327 RID: 17191 RVA: 0x001501C4 File Offset: 0x0014E3C4
		public static string GET_STRING_SELECT_SHIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SELECT_SHIP", false);
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06004328 RID: 17192 RVA: 0x001501D1 File Offset: 0x0014E3D1
		public static string GET_STRING_NO_EXIST_SELECT_SHIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NO_EXIST_SELECT_SHIP", false);
			}
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06004329 RID: 17193 RVA: 0x001501DE File Offset: 0x0014E3DE
		public static string GET_STRING_HANGAR_SHIP_LEVEL_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_HANGAR_SHIP_LEVEL_TWO_PARAM", false);
			}
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x0600432A RID: 17194 RVA: 0x001501EB File Offset: 0x0014E3EB
		public static string GET_STRING_HANGAR_LVUP
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_HANGAR_LVUP", false);
			}
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x0600432B RID: 17195 RVA: 0x001501F8 File Offset: 0x0014E3F8
		public static string GET_STRING_HANGAR_SHIPYARD_POPUP_LEVEL_UP_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_HANGAR_SHIPYARD_POPUP_LEVEL_UP_TEXT", false);
			}
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x0600432C RID: 17196 RVA: 0x00150205 File Offset: 0x0014E405
		public static string GET_STRING_HANGAR_SHIPYARD_POPUP_UPGRADE_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_HANGAR_SHIPYARD_POPUP_UPGRADE_TITLE", false);
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x0600432D RID: 17197 RVA: 0x00150212 File Offset: 0x0014E412
		public static string GET_STRING_HANGAR_SHIPYARD_POPUP_UPGRADE_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_HANGAR_SHIPYARD_POPUP_UPGRADE_TEXT", false);
			}
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x0600432E RID: 17198 RVA: 0x0015021F File Offset: 0x0014E41F
		public static string GET_STRING_HANGAR_SHIPYARD_CANNOT_CHANGE_SHIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_HANGAR_SHIPYARD_CANNOT_CHANGE_SHIP", false);
			}
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x0600432F RID: 17199 RVA: 0x0015022C File Offset: 0x0014E42C
		public static string GET_STRING_HANGAR_SHIPYARD_CANNOT_FIND_INFORMATION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_HANGAR_SHIPYARD_CANNOT_FIND_INFORMATION", false);
			}
		}

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06004330 RID: 17200 RVA: 0x00150239 File Offset: 0x0014E439
		public static string GET_STRING_HANGAR_SHIP_LEVEL_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_HANGAR_SHIP_LEVEL_ONE_PARAM", false);
			}
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06004331 RID: 17201 RVA: 0x00150246 File Offset: 0x0014E446
		public static string GET_STRING_REMOVE_SHIP_NO_EXIST_SHIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REMOVE_SHIP_NO_EXIST_SHIP", false);
			}
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06004332 RID: 17202 RVA: 0x00150253 File Offset: 0x0014E453
		public static string GET_STRING_REMOVE_SHIP_WARNING_MSG
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REMOVE_SHIP_WARNING_MSG", false);
			}
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06004333 RID: 17203 RVA: 0x00150260 File Offset: 0x0014E460
		public static string GET_STRING_REMOVE_SHIP_SELECT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REMOVE_SHIP_SELECT", false);
			}
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06004334 RID: 17204 RVA: 0x0015026D File Offset: 0x0014E46D
		public static string GET_STRING_REMOVE_SHIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REMOVE_SHIP", false);
			}
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06004335 RID: 17205 RVA: 0x0015027A File Offset: 0x0014E47A
		public static string GET_STRING_ATTACK_SPEED_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ATTACK_SPEED_ONE_PARAM", false);
			}
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06004336 RID: 17206 RVA: 0x00150287 File Offset: 0x0014E487
		public static string GET_STRING_HANGAR_UPGRADE_RESULT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_HANGAR_UPGRADE_RESULT", false);
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06004337 RID: 17207 RVA: 0x00150294 File Offset: 0x0014E494
		public static string GET_STRING_SHIP_BUILD_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHIP_BUILD_FAIL", false);
			}
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06004338 RID: 17208 RVA: 0x001502A1 File Offset: 0x0014E4A1
		public static string GET_STRING_SHIP_LEVEL_UP_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHIP_LEVEL_UP_FAIL", false);
			}
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06004339 RID: 17209 RVA: 0x001502AE File Offset: 0x0014E4AE
		public static string GET_STRING_SHIP_DIVISION_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHIP_DIVISION_FAIL", false);
			}
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x0600433A RID: 17210 RVA: 0x001502BB File Offset: 0x0014E4BB
		public static string GET_STRING_SHIP_UPGRADE_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHIP_UPGRADE_FAIL", false);
			}
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x0600433B RID: 17211 RVA: 0x001502C8 File Offset: 0x0014E4C8
		public static string GET_STRING_SHIP_BUILD_CONDITION_FAIL_PLAYER_LEVEL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHIP_BUILD_CONDITION_FAIL_PLAYER_LEVEL", false);
			}
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x0600433C RID: 17212 RVA: 0x001502D5 File Offset: 0x0014E4D5
		public static string GET_STRING_SHIP_BUILD_CONDITION_FAIL_DUNGEON_CLEAR
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHIP_BUILD_CONDITION_FAIL_DUNGEON_CLEAR", false);
			}
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x0600433D RID: 17213 RVA: 0x001502E2 File Offset: 0x0014E4E2
		public static string GET_STRING_SHIP_BUILD_CONDITION_FAIL_SHIP_COLLECT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHIP_BUILD_CONDITION_FAIL_SHIP_COLLECT", false);
			}
		}

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x0600433E RID: 17214 RVA: 0x001502EF File Offset: 0x0014E4EF
		public static string GET_STRING_SHIP_BUILD_CONDITION_FAIL_WARFARE_CLEAR
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHIP_BUILD_CONDITION_FAIL_WARFARE_CLEAR", false);
			}
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x0600433F RID: 17215 RVA: 0x001502FC File Offset: 0x0014E4FC
		public static string GET_STRING_SHIP_BUILD_CONDITION_FAIL_SHIP_LEVEL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHIP_BUILD_CONDITION_FAIL_SHIP_LEVEL", false);
			}
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06004340 RID: 17216 RVA: 0x00150309 File Offset: 0x0014E509
		public static string GET_STRING_SHIP_BUILD_CONDITION_FAIL_SHADOW_CLEAR
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHIP_BUILD_CONDITION_FAIL_SHADOW_CLEAR", false);
			}
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06004341 RID: 17217 RVA: 0x00150316 File Offset: 0x0014E516
		public static string GET_STRING_POPUP_MENU_NAME_BUILD_CONFIRM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_POPUP_MENU_NAME_BUILD_CONFIRM", false);
			}
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06004342 RID: 17218 RVA: 0x00150323 File Offset: 0x0014E523
		public static string GET_STRING_POPUP_MENU_NAME_SHIPYARD_CONFIRM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_POPUP_MENU_NAME_SHIPYARD_CONFIRM", false);
			}
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06004343 RID: 17219 RVA: 0x00150330 File Offset: 0x0014E530
		public static string GET_STRING_HANGAR_SHIPYARD_POPUP_LEVEL_UP_MISC_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_HANGAR_SHIPYARD_POPUP_LEVEL_UP_MISC_TEXT", false);
			}
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06004344 RID: 17220 RVA: 0x0015033D File Offset: 0x0014E53D
		public static string GET_STRING_HANGAR_SHIPYARD_POPUP_UPGRADE_MISC_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_HANGAR_SHIPYARD_POPUP_UPGRADE_MISC_TEXT", false);
			}
		}

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06004345 RID: 17221 RVA: 0x0015034A File Offset: 0x0014E54A
		public static string GET_STRING_HANGAR_SHIPYARD_POPUP_DESC_LEVEL_UP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_HANGAR_SHIPYARD_POPUP_DESC_LEVEL_UP", false);
			}
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06004346 RID: 17222 RVA: 0x00150357 File Offset: 0x0014E557
		public static string GET_STRING_HANGAR_SHIPYARD_POPUP_DESC_UPGRADE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_HANGAR_SHIPYARD_POPUP_DESC_UPGRADE", false);
			}
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06004347 RID: 17223 RVA: 0x00150364 File Offset: 0x0014E564
		public static string GET_STRING_HANGAR_UPGRADE_COST
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_HANGAR_UPGRADE_COST", false);
			}
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06004348 RID: 17224 RVA: 0x00150371 File Offset: 0x0014E571
		public static string GET_STRING_HANGAR_UPGRADE_COST_2
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_HANGAR_UPGRADE_COST_2", false);
			}
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06004349 RID: 17225 RVA: 0x0015037E File Offset: 0x0014E57E
		public static string GET_STRING_SHIP_INFO_01_SHIPYARD_MODULE_STEP_INFO
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_SHIP_INFO_01_SHIPYARD_MODULE_STEP_INFO", false);
			}
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x0600434A RID: 17226 RVA: 0x0015038B File Offset: 0x0014E58B
		public static string GET_STRING_SHIP_LIMITBREAK_NOT_CHOICE_SHIP
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_SHIP_LIMITBREAK_NOT_CHOICE_SHIP", false);
			}
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x0600434B RID: 17227 RVA: 0x00150398 File Offset: 0x0014E598
		public static string GET_STRING_SHIP_COMMAND_MODULE_SLOT_OPTION
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_SHIP_COMMAND_MODULE_SLOT_OPTION", false);
			}
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x0600434C RID: 17228 RVA: 0x001503A5 File Offset: 0x0014E5A5
		public static string GET_STRING_SHIP_COMMANDMODULE_SLOT_LOCK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHIP_COMMANDMODULE_SLOT_LOCK", false);
			}
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x0600434D RID: 17229 RVA: 0x001503B2 File Offset: 0x0014E5B2
		public static string GET_STRING_SHIP_COMMANDMODULE_SLOT_UNLOCK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHIP_COMMANDMODULE_SLOT_UNLOCK", false);
			}
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x0600434E RID: 17230 RVA: 0x001503BF File Offset: 0x0014E5BF
		public static string GET_STRING_SHIP_COMMAND_MODULE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_SHIP_COMMAND_MODULE", false);
			}
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x0600434F RID: 17231 RVA: 0x001503CC File Offset: 0x0014E5CC
		public static string GET_STRING_SHIP_COMMAND_MODULE_SLOT_LOCK_COST_NOT_RECOVERED
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_SHIP_COMMAND_MODULE_SLOT_LOCK_COST_NOT_RECOVERED", false);
			}
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06004350 RID: 17232 RVA: 0x001503D9 File Offset: 0x0014E5D9
		public static string GET_STRING_NEC_FAIL_SHIP_COMMAND_MODULE_LOCK_NO_CONFIRM
		{
			get
			{
				return NKCStringTable.GetString("NEC_FAIL_SHIP_COMMAND_MODULE_LOCK_NO_CONFIRM", false);
			}
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06004351 RID: 17233 RVA: 0x001503E6 File Offset: 0x0014E5E6
		public static string GET_STRING_SHIP_COMMANDMODULE_EXIT_CONFIRM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHIP_COMMANDMODULE_EXIT_CONFIRM", false);
			}
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06004352 RID: 17234 RVA: 0x001503F3 File Offset: 0x0014E5F3
		public static string GET_STRING_SHIP_COMMAND_MODULE_SLOT_ALL_LOCK
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_SHIP_COMMAND_MODULE_SLOT_ALL_LOCK", false);
			}
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06004353 RID: 17235 RVA: 0x00150400 File Offset: 0x0014E600
		public static string GET_STRING_SHIP_LIMITBREAK
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_SHIP_LIMITBREAK", false);
			}
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06004354 RID: 17236 RVA: 0x0015040D File Offset: 0x0014E60D
		public static string GET_STRING_SHIP_LIMITBREAK_POPUP_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHIP_LIMITBREAK_POPUP_DESC", false);
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06004355 RID: 17237 RVA: 0x0015041A File Offset: 0x0014E61A
		public static string GET_STRING_SHIP_COMMANDMODULE_OPEN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHIP_COMMANDMODULE_OPEN", false);
			}
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06004356 RID: 17238 RVA: 0x00150427 File Offset: 0x0014E627
		public static string GET_STRING_SHIP_LIMITBREAK_GRADE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHIP_LIMITBREAK_GRADE", false);
			}
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06004357 RID: 17239 RVA: 0x00150434 File Offset: 0x0014E634
		public static string GET_STRING_SHIP_LIMITBREAK_GRADE_COMMANDMODULE_UNLOCK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHIP_LIMITBREAK_GRADE_COMMANDMODULE_UNLOCK", false);
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06004358 RID: 17240 RVA: 0x00150441 File Offset: 0x0014E641
		public static string GET_STRING_SHIP_COMMAND_MODULE_NOT_LIMITBREAK
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_SHIP_COMMAND_MODULE_NOT_LIMITBREAK", false);
			}
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06004359 RID: 17241 RVA: 0x0015044E File Offset: 0x0014E64E
		public static string GET_STRING_SHIP_INFO_COMMAND_MODULE_NO_LIMITBREAK
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_SHIP_INFO_COMMAND_MODULE_NO_LIMITBREAK", false);
			}
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x0600435A RID: 17242 RVA: 0x0015045B File Offset: 0x0014E65B
		public static string GET_STRING_SHIP_LIMITBREAK_WARNING
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_SHIP_LIMITBREAK_WARNING", false);
			}
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x0600435B RID: 17243 RVA: 0x00150468 File Offset: 0x0014E668
		public static string GET_STRING_SHIP_INFO_MODULE_STEP_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_SHIP_INFO_MODULE_STEP_TEXT", false);
			}
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x0600435C RID: 17244 RVA: 0x00150475 File Offset: 0x0014E675
		public static string GET_STRING_SHIP_COMMAND_MODULE_SLOT_NOT_OPEN
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_SHIP_COMMAND_MODULE_SLOT_NOT_OPEN", false);
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x0600435D RID: 17245 RVA: 0x00150482 File Offset: 0x0014E682
		public static string GET_STRING_SHIP_COMMAND_MODULE_SLOT_HAS_RESERVED
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_SHIP_COMMAND_MODULE_SLOT_HAS_RESERVED", false);
			}
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x0600435E RID: 17246 RVA: 0x0015048F File Offset: 0x0014E68F
		public static string GET_STRING_REMOVE_UNIT_NO_EXIST_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REMOVE_UNIT_NO_EXIST_UNIT", false);
			}
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x0600435F RID: 17247 RVA: 0x0015049C File Offset: 0x0014E69C
		public static string GET_STRING_REMOVE_UNIT_NO_EXIST_TROPHY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REMOVE_UNIT_NO_EXIST_TROPHY", false);
			}
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06004360 RID: 17248 RVA: 0x001504A9 File Offset: 0x0014E6A9
		public static string GET_STRING_REMOVE_UNIT_WARNING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REMOVE_UNIT_WARNING", false);
			}
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06004361 RID: 17249 RVA: 0x001504B6 File Offset: 0x0014E6B6
		public static string GET_STRING_NO_EXIST_SELECTED_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NO_EXIST_SELECTED_UNIT", false);
			}
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06004362 RID: 17250 RVA: 0x001504C3 File Offset: 0x0014E6C3
		public static string GET_STRING_REMOVE_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REMOVE_UNIT", false);
			}
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06004363 RID: 17251 RVA: 0x001504D0 File Offset: 0x0014E6D0
		public static string GET_STRING_NO_EXIST_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NO_EXIST_UNIT", false);
			}
		}

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06004364 RID: 17252 RVA: 0x001504DD File Offset: 0x0014E6DD
		public static string GET_STRING_REMOVE_UNIT_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REMOVE_UNIT_FAIL", false);
			}
		}

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06004365 RID: 17253 RVA: 0x001504EA File Offset: 0x0014E6EA
		public static string GET_STRING_REMOVE_UNIT_FAIL_LOCKED
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REMOVE_UNIT_FAIL_LOCKED", false);
			}
		}

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06004366 RID: 17254 RVA: 0x001504F7 File Offset: 0x0014E6F7
		public static string GET_STRING_REMOVE_UNIT_FAIL_IN_DECK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REMOVE_UNIT_FAIL_IN_DECK", false);
			}
		}

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x06004367 RID: 17255 RVA: 0x00150504 File Offset: 0x0014E704
		public static string GET_STRING_REMOVE_UNIT_FAIL_MAINUNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REMOVE_UNIT_FAIL_MAINUNIT", false);
			}
		}

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x06004368 RID: 17256 RVA: 0x00150511 File Offset: 0x0014E711
		public static string GET_STRING_REMOVE_UNIT_FAIL_WORLDMAP_LEADER
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REMOVE_UNIT_FAIL_WORLDMAP_LEADER", false);
			}
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x06004369 RID: 17257 RVA: 0x0015051E File Offset: 0x0014E71E
		public static string GET_STRING_REMOVE_UNIT_SELECT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REMOVE_UNIT_SELECT", false);
			}
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x0600436A RID: 17258 RVA: 0x0015052B File Offset: 0x0014E72B
		public static string GET_STRING_CEO
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CEO", false);
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x0600436B RID: 17259 RVA: 0x00150538 File Offset: 0x0014E738
		public static string GET_STRING_NEGOTIATE_OFFER_SELECTION_CANCEL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NEGOTIATE_OFFER_SELECTION_CANCEL", false);
			}
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x0600436C RID: 17260 RVA: 0x00150545 File Offset: 0x0014E745
		public static string GET_STRING_LIFETIME_CONTRACT_DATE_THREE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LIFETIME_CONTRACT_DATE_THREE_PARAM", false);
			}
		}

		// Token: 0x0600436D RID: 17261 RVA: 0x00150554 File Offset: 0x0014E754
		public static void GetNegotiateResult(NEGOTIATE_RESULT result, out string title, out string info)
		{
			title = "";
			info = "";
			if (result == NEGOTIATE_RESULT.SUCCESS)
			{
				title = NKCStringTable.GetString("SI_DP_NEGOTIATE_RESULT_BIG_SUCCESS_TITLE", false);
				info = NKCStringTable.GetString("SI_DP_NEGOTIATE_RESULT_BIG_SUCCESS_INFO", false);
				return;
			}
			if (result != NEGOTIATE_RESULT.COMPLETE)
			{
				return;
			}
			title = NKCStringTable.GetString("SI_DP_NEGOTIATE_RESULT_SUCCESS_TITLE", false);
			info = NKCStringTable.GetString("SI_DP_NEGOTIATE_RESULT_SUCCESS_INFO", false);
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x0600436E RID: 17262 RVA: 0x001505AC File Offset: 0x0014E7AC
		public static string GET_STRING_NEGOTIATE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NEGOTIATE", false);
			}
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x0600436F RID: 17263 RVA: 0x001505B9 File Offset: 0x0014E7B9
		public static string GET_STRING_NEGOTIATE_LEVEL_MAX
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NEGOTIATE_LEVEL_MAX", false);
			}
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06004370 RID: 17264 RVA: 0x001505C6 File Offset: 0x0014E7C6
		public static string GET_STRING_NEGOTIATE_NO_EXIST_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NEGOTIATE_NO_EXIST_UNIT", false);
			}
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06004371 RID: 17265 RVA: 0x001505D3 File Offset: 0x0014E7D3
		public static string GET_STRING_NEGOTIATE_OFFER_UNIT_FIRST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NEGOTIATE_OFFER_UNIT_FIRST", false);
			}
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06004372 RID: 17266 RVA: 0x001505E0 File Offset: 0x0014E7E0
		public static string GET_STRING_ROUND_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_ROUND_ONE_PARAM", false);
			}
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06004373 RID: 17267 RVA: 0x001505ED File Offset: 0x0014E7ED
		public static string GET_STRING_FINAL_ROUND
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FINAL_ROUND", false);
			}
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06004374 RID: 17268 RVA: 0x001505FA File Offset: 0x0014E7FA
		public static string GET_STRING_NEGOTIATE_OFFER_SELECTION_RAISE_ALL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NEGOTIATE_OFFER_SELECTION_RAISE_ALL", false);
			}
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06004375 RID: 17269 RVA: 0x00150607 File Offset: 0x0014E807
		public static string GET_STRING_NEGOTIATE_OFFER_SELECTION_RAISE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NEGOTIATE_OFFER_SELECTION_RAISE", false);
			}
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06004376 RID: 17270 RVA: 0x00150614 File Offset: 0x0014E814
		public static string GET_STRING_NEGOTIATE_OFFER_SELECTION_OK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NEGOTIATE_OFFER_SELECTION_OK", false);
			}
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06004377 RID: 17271 RVA: 0x00150621 File Offset: 0x0014E821
		public static string GET_STRING_NEGOTIATE_OFFER_SELECTION_PASSION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NEGOTIATE_OFFER_SELECTION_PASSION", false);
			}
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06004378 RID: 17272 RVA: 0x0015062E File Offset: 0x0014E82E
		public static string GET_STRING_NOT_ENOUGH_NEGOTIATE_MATERIALS
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NOT_ENOUGH_NEGOTIATE_MATERIALS", false);
			}
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06004379 RID: 17273 RVA: 0x0015063B File Offset: 0x0014E83B
		public static string GET_STRING_NEGOTIATE_RAISE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NEGOTIATE_RAISE_DESC", false);
			}
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x0600437A RID: 17274 RVA: 0x00150648 File Offset: 0x0014E848
		public static string GET_STRING_NEGOTIATE_OK_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NEGOTIATE_OK_DESC", false);
			}
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x0600437B RID: 17275 RVA: 0x00150655 File Offset: 0x0014E855
		public static string GET_STRING_NEGOTIATE_PASSION_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NEGOTIATE_PASSION_DESC", false);
			}
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x0600437C RID: 17276 RVA: 0x00150662 File Offset: 0x0014E862
		public static string GET_NEGOTIATE_OFFER_SELECTION_BONUS
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NEGOTIATE_OFFER_SELECTION_BONUS", false);
			}
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x0600437D RID: 17277 RVA: 0x0015066F File Offset: 0x0014E86F
		public static string GET_STRING_MAX_LEVEL_LOYALTY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MAX_LEVEL_LOYALTY", false);
			}
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x0600437E RID: 17278 RVA: 0x0015067C File Offset: 0x0014E87C
		public static string GET_STRING_NEGOTIATE_OVER_MAX_LEVEL_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_NEGOTIATE_OVER_MAX_LEVEL_ONE_PARAM", false);
			}
		}

		// Token: 0x0600437F RID: 17279 RVA: 0x0015068C File Offset: 0x0014E88C
		public static string GetNegotiateDesc(NEGOTIATE_BOSS_SELECTION selection)
		{
			switch (selection)
			{
			case NEGOTIATE_BOSS_SELECTION.RAISE:
				return string.Format(NKCUtilString.GET_STRING_NEGOTIATE_RAISE_DESC, NKMCommonConst.Negotiation.Bonus_CreditIncreasePercent, NKMCommonConst.Negotiation.Bonus_LoyaltyIncreasePercent, NKMCommonConst.Negotiation.Bonus_ResultSuccessPercent / NKMCommonConst.Negotiation.Normal_ResultSuccessPercent);
			case NEGOTIATE_BOSS_SELECTION.OK:
				return NKCUtilString.GET_STRING_NEGOTIATE_OK_DESC;
			case NEGOTIATE_BOSS_SELECTION.PASSION:
				return string.Format(NKCUtilString.GET_STRING_NEGOTIATE_PASSION_DESC, NKMCommonConst.Negotiation.Passion_CreditDecreasePercent);
			default:
				return string.Empty;
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06004380 RID: 17280 RVA: 0x00150715 File Offset: 0x0014E915
		public static string GET_STRING_LIFETIME
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LIFETIME", false);
			}
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x06004381 RID: 17281 RVA: 0x00150722 File Offset: 0x0014E922
		public static string GET_STRING_LIFETIME_CONTRACT_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LIFETIME_CONTRACT_TITLE", false);
			}
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06004382 RID: 17282 RVA: 0x0015072F File Offset: 0x0014E92F
		public static string GET_STRING_LIFETIME_CONTRACT_TITLE_MECHANIC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LIFETIME_CONTRACT_TITLE_MECHANIC", false);
			}
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06004383 RID: 17283 RVA: 0x0015073C File Offset: 0x0014E93C
		public static string GET_STRING_LIFETIME_CONTRACT_UNIT_SIGN_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LIFETIME_CONTRACT_UNIT_SIGN_TWO_PARAM", false);
			}
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06004384 RID: 17284 RVA: 0x00150749 File Offset: 0x0014E949
		public static string GET_STRING_LIFETIME_CONTRACT_PLAYER_SIGN_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LIFETIME_CONTRACT_PLAYER_SIGN_ONE_PARAM", false);
			}
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06004385 RID: 17285 RVA: 0x00150756 File Offset: 0x0014E956
		public static string GET_STRING_LIFETIME_REWARD_INFO
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LIFETIME_REWARD_INFO", false);
			}
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06004386 RID: 17286 RVA: 0x00150763 File Offset: 0x0014E963
		public static string GET_STRING_LIFETIME_NO_EXIST_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LIFETIME_NO_EXIST_UNIT", false);
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06004387 RID: 17287 RVA: 0x00150770 File Offset: 0x0014E970
		public static string GET_STRING_LIFETIME_REPLAY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LOYALTY_LIFETIME_RECALL", false);
			}
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06004388 RID: 17288 RVA: 0x0015077D File Offset: 0x0014E97D
		public static string GET_STRING_LIFETIME_CONTRACT_POPUP
		{
			get
			{
				return NKCStringTable.GetString("SI_POPUP_UNIT_INFO_LOYALTY_100", false);
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06004389 RID: 17289 RVA: 0x0015078A File Offset: 0x0014E98A
		public static string GET_STRING_LIFETIME_LOYALTY_INFO
		{
			get
			{
				return NKCStringTable.GetString("SI_POPUP_UNIT_INFO_LOYALTY", false);
			}
		}

		// Token: 0x0600438A RID: 17290 RVA: 0x00150798 File Offset: 0x0014E998
		public static string GetLifetimeContractDesc(NKMUnitTempletBase unitTempletBase, string playerName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(NKCStringTable.GetString("SI_DP_LIFETIME_CONTRACT_DESC_UNIT_TITLE", false), unitTempletBase.GetUnitTitle());
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat(NKCStringTable.GetString("SI_DP_LIFETIME_CONTRACT_DESC_UNIT_NAME", false), playerName, unitTempletBase.GetUnitName());
			return stringBuilder.ToString();
		}

		// Token: 0x0600438B RID: 17291 RVA: 0x001507EE File Offset: 0x0014E9EE
		public static string GetNegotiateSpeechBySelection(NEGOTIATE_BOSS_SELECTION selection)
		{
			switch (selection)
			{
			default:
				return NKCStringTable.GetString("SI_DP_NEGOTIATE_SPEECH_BY_SELECTION_RAISE", false);
			case NEGOTIATE_BOSS_SELECTION.OK:
				return NKCStringTable.GetString("SI_DP_NEGOTIATE_SPEECH_BY_SELECTION_OK", false);
			case NEGOTIATE_BOSS_SELECTION.PASSION:
				return NKCStringTable.GetString("SI_DP_NEGOTIATE_SPEECH_BY_SELECTION_PASSION", false);
			}
		}

		// Token: 0x0600438C RID: 17292 RVA: 0x00150825 File Offset: 0x0014EA25
		public static string GetNegotiateResultTalk(NEGOTIATE_RESULT result)
		{
			if (result == NEGOTIATE_RESULT.SUCCESS)
			{
				return NKCStringTable.GetString("SI_DP_NEGOTIATE_RESULT_TALK_BIG_SUCCESS", false);
			}
			if (result != NEGOTIATE_RESULT.COMPLETE)
			{
				return "";
			}
			return NKCStringTable.GetString("SI_DP_NEGOTIATE_RESULT_TALK_SUCCESS", false);
		}

		// Token: 0x0600438D RID: 17293 RVA: 0x00150850 File Offset: 0x0014EA50
		public static string GetBaseMenuName(NKCUIBaseSceneMenu.BaseSceneMenuType type)
		{
			switch (type)
			{
			default:
				return NKCStringTable.GetString("SI_DP_BASE_MENU_NAME_BASE", false);
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Lab:
				return NKCStringTable.GetString("SI_DP_BASE_MENU_NAME_LAB", false);
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Factory:
				return NKCStringTable.GetString("SI_DP_BASE_MENU_NAME_FACTORY", false);
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Hangar:
				return NKCStringTable.GetString("SI_DP_BASE_MENU_NAME_HANGAR", false);
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Personnel:
				return NKCStringTable.GetString("SI_DP_BASE_MENU_NAME_PERSONNEL", false);
			}
		}

		// Token: 0x0600438E RID: 17294 RVA: 0x001508B4 File Offset: 0x0014EAB4
		public static string GetBaseMenuNameEng(NKCUIBaseSceneMenu.BaseSceneMenuType type)
		{
			switch (type)
			{
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Lab:
				return NKCStringTable.GetString("SI_DP_BASE_MENU_NAME_ENG_LAB", false);
			default:
				return NKCStringTable.GetString("SI_DP_BASE_MENU_NAME_ENG_FACTORY", false);
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Hangar:
				return NKCStringTable.GetString("SI_DP_BASE_MENU_NAME_ENG_HANGAR", false);
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Personnel:
				return NKCStringTable.GetString("SI_DP_BASE_MENU_NAME_ENG_PERSONNEL", false);
			}
		}

		// Token: 0x0600438F RID: 17295 RVA: 0x00150908 File Offset: 0x0014EB08
		public static string GetBaseSubMenuDetail(NKCUIBaseSceneMenu.BaseSceneMenuType type)
		{
			switch (type)
			{
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Lab:
				return NKCStringTable.GetString("SI_DP_BASE_SUB_MENU_DETAIL_LAB", false);
			default:
				return NKCStringTable.GetString("SI_DP_BASE_SUB_MENU_DETAIL_FACTORY", false);
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Hangar:
				return NKCStringTable.GetString("SI_DP_BASE_SUB_MENU_DETAIL_HANGAR", false);
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Personnel:
				return NKCStringTable.GetString("SI_DP_BASE_SUB_MENU_DETAIL_PERSONNEL", false);
			}
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06004390 RID: 17296 RVA: 0x0015095C File Offset: 0x0014EB5C
		public static string GET_STRING_TUTORIAL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TUTORIAL", false);
			}
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06004391 RID: 17297 RVA: 0x00150969 File Offset: 0x0014EB69
		public static string GET_STRING_TUTORIAL_GUIDE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TUTORIAL_GUIDE", false);
			}
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06004392 RID: 17298 RVA: 0x00150976 File Offset: 0x0014EB76
		public static string GET_STRING_TUTORIAL_IMAGE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TUTORIAL_IMAGE", false);
			}
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06004393 RID: 17299 RVA: 0x00150983 File Offset: 0x0014EB83
		public static string GET_STRING_TUTORIAL_FIRST
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TUTORIAL_FIRST", false);
			}
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06004394 RID: 17300 RVA: 0x00150990 File Offset: 0x0014EB90
		public static string GET_STRING_TUTORIAL_NEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TUTORIAL_NEXT", false);
			}
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06004395 RID: 17301 RVA: 0x0015099D File Offset: 0x0014EB9D
		public static string GET_STRING_REWARD_FIRST_CLEAR
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REWARD_FIRST_CLEAR", false);
			}
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06004396 RID: 17302 RVA: 0x001509AA File Offset: 0x0014EBAA
		public static string GET_STRING_REWARD_CHANCE_UP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REWARD_CHANCE_UP", false);
			}
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06004397 RID: 17303 RVA: 0x001509B7 File Offset: 0x0014EBB7
		public static string GET_STRING_SLOT_VIEWR_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SLOT_VIEWR_DESC", false);
			}
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06004398 RID: 17304 RVA: 0x001509C4 File Offset: 0x0014EBC4
		public static string GET_STRING_FIRST_GET_SHIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FIRST_GET_SHIP", false);
			}
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06004399 RID: 17305 RVA: 0x001509D1 File Offset: 0x0014EBD1
		public static string GET_STRING_FIRST_GET_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FIRST_GET_UNIT", false);
			}
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x0600439A RID: 17306 RVA: 0x001509DE File Offset: 0x0014EBDE
		public static string GET_STRING_RESULT_CITY_MISSION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_RESULT_CITY_MISSION", false);
			}
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x0600439B RID: 17307 RVA: 0x001509EB File Offset: 0x0014EBEB
		public static string GET_STRING_RESULT_MISSION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_RESULT_MISSION", false);
			}
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x0600439C RID: 17308 RVA: 0x001509F8 File Offset: 0x0014EBF8
		public static string GET_STRING_GET_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GET_UNIT", false);
			}
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x0600439D RID: 17309 RVA: 0x00150A05 File Offset: 0x0014EC05
		public static string GET_STRING_GET_SHIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GET_SHIP", false);
			}
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x0600439E RID: 17310 RVA: 0x00150A12 File Offset: 0x0014EC12
		public static string GET_STRING_RESULT_LIMIT_BREAK_UNIT_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_RESULT_LIMIT_BREAK_UNIT_ONE_PARAM", false);
			}
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x0600439F RID: 17311 RVA: 0x00150A1F File Offset: 0x0014EC1F
		public static string GET_STRING_RESULT_LIMIT_BREAK_UNIT_ONE_PARAM_UNLOCK_HYPER_SKILL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_RESULT_LIMIT_BREAK_UNIT_ONE_PARAM_UNLOCK_HYPER_SKILL", false);
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x060043A0 RID: 17312 RVA: 0x00150A2C File Offset: 0x0014EC2C
		public static string GET_STRING_RESULT_LIMIT_BREAK_UNIT_MAX_LEVEL_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_RESULT_LIMIT_BREAK_UNIT_MAX_LEVEL_TWO_PARAM", false);
			}
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x060043A1 RID: 17313 RVA: 0x00150A39 File Offset: 0x0014EC39
		public static string GET_STRING_RESULT_BONUS_EXP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_RESULT_BONUS_EXP", false);
			}
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x060043A2 RID: 17314 RVA: 0x00150A46 File Offset: 0x0014EC46
		public static string GET_STRING_RESULT_BONUS_RESOURCE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_RESULT_BONUS_RESOURCE", false);
			}
		}

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x060043A3 RID: 17315 RVA: 0x00150A53 File Offset: 0x0014EC53
		public static string GET_EVENT_BUFF_TYPE_RWDBOUNS_CREDIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BUFF_TYPE_RWDBOUNS_CREDIT", false);
			}
		}

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x060043A4 RID: 17316 RVA: 0x00150A60 File Offset: 0x0014EC60
		public static string GET_EVENT_BUFF_TYPE_RWDBOUNS_ETERNIUM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BUFF_TYPE_RWDBOUNS_ETERNIUM", false);
			}
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x060043A5 RID: 17317 RVA: 0x00150A6D File Offset: 0x0014EC6D
		public static string GET_EVENT_BUFF_TYPE_RWDBOUNS_INFORMATION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BUFF_TYPE_RWDBOUNS_INFORMATION", false);
			}
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x060043A6 RID: 17318 RVA: 0x00150A7A File Offset: 0x0014EC7A
		public static string GET_EVENT_BUFF_TYPE_RWDBOUNS_EXP_PLAYER
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BUFF_TYPE_RWDBOUNS_EXP_PLAYER", false);
			}
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x060043A7 RID: 17319 RVA: 0x00150A87 File Offset: 0x0014EC87
		public static string GET_EVENT_BUFF_TYPE_RWDBOUNS_EXP_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BUFF_TYPE_RWDBOUNS_EXP_UNIT", false);
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x060043A8 RID: 17320 RVA: 0x00150A94 File Offset: 0x0014EC94
		public static string GET_EVENT_BUFF_TYPE_WARFARE_ETNM_DISCOUNT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BUFF_TYPE_WARFARE_ETNM_DISCOUNT", false);
			}
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x060043A9 RID: 17321 RVA: 0x00150AA1 File Offset: 0x0014ECA1
		public static string GET_EVENT_BUFF_TYPE_WARFARE_DUNGEON_ETNM_DISCOUNT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BUFF_TYPE_WARFARE_DUNGEON_ETNM_DISCOUNT", false);
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x060043AA RID: 17322 RVA: 0x00150AAE File Offset: 0x0014ECAE
		public static string GET_EVENT_BUFF_TYPE_PVP_POINT_CHARGE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BUFF_TYPE_PVP_POINT_CHARGE", false);
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x060043AB RID: 17323 RVA: 0x00150ABB File Offset: 0x0014ECBB
		public static string GET_EVENT_BUFF_TYPE_CITY_MISSION_TIMEKEEP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BUFF_TYPE_CITY_MISSION_TIMEKEEP", false);
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x060043AC RID: 17324 RVA: 0x00150AC8 File Offset: 0x0014ECC8
		public static string GET_EVENT_BUFF_TYPE_CITY_MISSION_WMMR_S_UP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BUFF_TYPE_CITY_MISSION_WMMR_S_UP", false);
			}
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x060043AD RID: 17325 RVA: 0x00150AD5 File Offset: 0x0014ECD5
		public static string GET_EVENT_BUFF_TYPE_NEGOTIATION_CREDIT_DISCOUNT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BUFF_TYPE_BASE_PERSONNAL_NEGOTIATION_CREDIT_DISCOUNT", false);
			}
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x060043AE RID: 17326 RVA: 0x00150AE2 File Offset: 0x0014ECE2
		public static string GET_EVENT_BUFF_TYPE_FACTORY_CRAFT_CREDIT_DISCOUNT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BUFF_TYPE_BASE_FACTORY_CRAFT_CREDIT_DISCOUNT", false);
			}
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x060043AF RID: 17327 RVA: 0x00150AEF File Offset: 0x0014ECEF
		public static string GET_EVENT_BUFF_TYPE_FACTORY_ENCHANT_TUNING_CREDIT_DISCOUNT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BUFF_TYPE_BASE_FACTORY_ENCHANT_TUNING_CREDIT_DISCOUNT", false);
			}
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x060043B0 RID: 17328 RVA: 0x00150AFC File Offset: 0x0014ECFC
		public static string GET_EVENT_BUFF_SCOPE_MAINSTREAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BUFF_SCOPE_MAINSTREAM", false);
			}
		}

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x060043B1 RID: 17329 RVA: 0x00150B09 File Offset: 0x0014ED09
		public static string GET_EVENT_BUFF_SCOPE_SIDESTORY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BUFF_SCOPE_SIDESTORY", false);
			}
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x060043B2 RID: 17330 RVA: 0x00150B16 File Offset: 0x0014ED16
		public static string GET_EVENT_BUFF_SCOPE_WORLDMAP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BUFF_SCOPE_WORLDMAP", false);
			}
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x060043B3 RID: 17331 RVA: 0x00150B23 File Offset: 0x0014ED23
		public static string GET_EVENT_BUFF_SCOPE_PVP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BUFF_SCOPE_PVP", false);
			}
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x060043B4 RID: 17332 RVA: 0x00150B30 File Offset: 0x0014ED30
		public static string GET_EVENT_BUFF_SCOPE_ALL_WARFARE_DUNGEON
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BUFF_SCOPE_ALL_WARFARE_DUNGEON", false);
			}
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x060043B5 RID: 17333 RVA: 0x00150B3D File Offset: 0x0014ED3D
		public static string GET_EVENT_BUFF_SCOPE_ALL_WARFARE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BUFF_SCOPE_ALL_WARFARE", false);
			}
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x060043B6 RID: 17334 RVA: 0x00150B4A File Offset: 0x0014ED4A
		public static string GET_EVENT_BUFF_SCOPE_ALL_DUNGEON
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BUFF_SCOPE_ALL_DUNGEON", false);
			}
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x060043B7 RID: 17335 RVA: 0x00150B57 File Offset: 0x0014ED57
		public static string GET_EVENT_BUFF_TYPE_PVP_POINT_INVENTORY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BUFF_TYPE_PVP_POINT_INVENTORY", false);
			}
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x060043B8 RID: 17336 RVA: 0x00150B64 File Offset: 0x0014ED64
		public static string GET_EVENT_BUFF_TYPE_PVP_POINT_REWARD
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BUFF_TYPE_PVP_POINT_REWARD", false);
			}
		}

		// Token: 0x060043B9 RID: 17337 RVA: 0x00150B71 File Offset: 0x0014ED71
		public static string GetCompanyBuffTitle(string buffName, bool bAddBuff)
		{
			if (bAddBuff)
			{
				return string.Format(NKCStringTable.GetString("SI_DP_EVENT_BUFF_TITLE_ADD", false), buffName);
			}
			return string.Format(NKCStringTable.GetString("SI_DP_EVENT_BUFF_TITLE_REMOVE", false), buffName);
		}

		// Token: 0x060043BA RID: 17338 RVA: 0x00150B9C File Offset: 0x0014ED9C
		private static string GetEquipPositionString(ITEM_EQUIP_POSITION equipPosition)
		{
			switch (equipPosition)
			{
			case ITEM_EQUIP_POSITION.IEP_WEAPON:
				return NKCStringTable.GetString("SI_DP_EQUIP_POSITION_STRING_IEP_WEAPON", false);
			case ITEM_EQUIP_POSITION.IEP_DEFENCE:
				return NKCStringTable.GetString("SI_DP_EQUIP_POSITION_STRING_IEP_DEFENCE", false);
			case ITEM_EQUIP_POSITION.IEP_ACC:
			case ITEM_EQUIP_POSITION.IEP_ACC2:
				return NKCStringTable.GetString("SI_DP_EQUIP_POSITION_STRING_IEP_ACC", false);
			default:
				return "";
			}
		}

		// Token: 0x060043BB RID: 17339 RVA: 0x00150BEA File Offset: 0x0014EDEA
		private static string GetMiscTypeString(NKM_ITEM_MISC_TYPE miscType)
		{
			if (miscType <= NKM_ITEM_MISC_TYPE.IMT_RANDOMBOX)
			{
				return NKCStringTable.GetString("SI_DP_MISC_TYPE_STRING_IMT_RANDOMBOX", false);
			}
			if (miscType == NKM_ITEM_MISC_TYPE.IMT_RESOURCE)
			{
				return NKCStringTable.GetString("SI_DP_MISC_TYPE_STRING_IMT_RESOURCE", false);
			}
			if (miscType != NKM_ITEM_MISC_TYPE.IMT_INTERIOR)
			{
				return "";
			}
			return NKCStringTable.GetString("SI_DP_MISC_TYPE_STRING_IMT_INTERIOR", false);
		}

		// Token: 0x060043BC RID: 17340 RVA: 0x00150C24 File Offset: 0x0014EE24
		public static string ApplyBuffValueToString(string str, IEnumerable<string> targetBuffTempletStrIDs, int buffLevel, int timeLevel)
		{
			string[] separator = new string[]
			{
				"##"
			};
			string[] array = str.Split(separator, StringSplitOptions.None);
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string buffStrID in targetBuffTempletStrIDs)
			{
				if (num < array.Length)
				{
					break;
				}
				NKMBuffTemplet buffTempletByStrID = NKMBuffManager.GetBuffTempletByStrID(buffStrID);
				stringBuilder.Append(NKCUtilString.ApplyBuffValueToString(array[num], buffTempletByStrID, buffLevel, timeLevel));
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060043BD RID: 17341 RVA: 0x00150CBC File Offset: 0x0014EEBC
		public static string ApplyBuffValueToString(string str, IEnumerable<NKMBuffTemplet> targetBuffTemplets, int buffLevel, int timeLevel)
		{
			string[] separator = new string[]
			{
				"##"
			};
			string[] array = str.Split(separator, StringSplitOptions.None);
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (NKMBuffTemplet targetBuffTemplet in targetBuffTemplets)
			{
				if (num < array.Length)
				{
					break;
				}
				stringBuilder.Append(NKCUtilString.ApplyBuffValueToString(array[num], targetBuffTemplet, buffLevel, timeLevel));
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060043BE RID: 17342 RVA: 0x00150D48 File Offset: 0x0014EF48
		public static string ApplyBuffValueToString(string str, NKMBuffTemplet targetBuffTemplet, int buffLevel, int buffTimeLevel)
		{
			if (targetBuffTemplet == null)
			{
				return str;
			}
			int num = buffLevel - 1;
			int num2 = buffTimeLevel - 1;
			float num3 = (targetBuffTemplet.m_fLifeTime > 0f) ? (targetBuffTemplet.m_fLifeTime + targetBuffTemplet.m_fLifeTimePerLevel * (float)num2) : targetBuffTemplet.m_fLifeTime;
			float num4 = (float)((targetBuffTemplet.m_StatValue1 != 0) ? (targetBuffTemplet.m_StatValue1 + targetBuffTemplet.m_StatAddPerLevel1 * num) : 0);
			float num5 = (float)((targetBuffTemplet.m_StatValue2 != 0) ? (targetBuffTemplet.m_StatValue2 + targetBuffTemplet.m_StatAddPerLevel2 * num) : 0);
			float num6 = (float)((targetBuffTemplet.m_StatValue3 != 0) ? (targetBuffTemplet.m_StatValue3 + targetBuffTemplet.m_StatAddPerLevel3 * num) : 0);
			float num7 = (float)((targetBuffTemplet.m_StatFactor1 != 0) ? (targetBuffTemplet.m_StatFactor1 + targetBuffTemplet.m_StatAddPerLevel1 * num) : 0);
			float num8 = (float)((targetBuffTemplet.m_StatFactor2 != 0) ? (targetBuffTemplet.m_StatFactor2 + targetBuffTemplet.m_StatAddPerLevel2 * num) : 0);
			float num9 = (float)((targetBuffTemplet.m_StatFactor3 != 0) ? (targetBuffTemplet.m_StatFactor3 + targetBuffTemplet.m_StatAddPerLevel3 * num) : 0);
			num4 *= (NKMUnitStatManager.IsPercentStat(targetBuffTemplet.m_StatType1) ? 0.01f : 0.0001f);
			num5 *= (NKMUnitStatManager.IsPercentStat(targetBuffTemplet.m_StatType2) ? 0.01f : 0.0001f);
			num6 *= (NKMUnitStatManager.IsPercentStat(targetBuffTemplet.m_StatType3) ? 0.01f : 0.0001f);
			num4 = Mathf.Abs(num4);
			num5 = Mathf.Abs(num5);
			num6 = Mathf.Abs(num6);
			num7 = Mathf.Abs(num7 * 0.01f);
			num8 = Mathf.Abs(num8 * 0.01f);
			num9 = Mathf.Abs(num9 * 0.01f);
			bool flag = num4 < 0f || num7 < 0f;
			bool flag2 = num5 < 0f || num8 < 0f;
			bool flag3 = num6 < 0f || num9 < 0f;
			string statShortName = NKCUtilString.GetStatShortName(targetBuffTemplet.m_StatType1, flag);
			string statShortName2 = NKCUtilString.GetStatShortName(targetBuffTemplet.m_StatType2, flag2);
			string statShortName3 = NKCUtilString.GetStatShortName(targetBuffTemplet.m_StatType3, flag3);
			float f = NKCUtilString.GetBuffStatPerLevelStringValue((float)targetBuffTemplet.m_StatAddPerLevel1, targetBuffTemplet.m_StatType1, (float)targetBuffTemplet.m_StatValue1, (float)targetBuffTemplet.m_StatFactor1);
			float f2 = NKCUtilString.GetBuffStatPerLevelStringValue((float)targetBuffTemplet.m_StatAddPerLevel2, targetBuffTemplet.m_StatType2, (float)targetBuffTemplet.m_StatValue2, (float)targetBuffTemplet.m_StatFactor2);
			float f3 = NKCUtilString.GetBuffStatPerLevelStringValue((float)targetBuffTemplet.m_StatAddPerLevel3, targetBuffTemplet.m_StatType3, (float)targetBuffTemplet.m_StatValue3, (float)targetBuffTemplet.m_StatFactor3);
			if (flag && NKCUtilString.IsNameReversedIfNegative(targetBuffTemplet.m_StatType1))
			{
				num4 = Mathf.Abs(num4);
				num7 = Mathf.Abs(num7);
				f = Mathf.Abs(f);
			}
			if (flag2 && NKCUtilString.IsNameReversedIfNegative(targetBuffTemplet.m_StatType2))
			{
				num5 = Mathf.Abs(num5);
				num8 = Mathf.Abs(num8);
				f2 = Mathf.Abs(f2);
			}
			if (flag3 && NKCUtilString.IsNameReversedIfNegative(targetBuffTemplet.m_StatType3))
			{
				num6 = Mathf.Abs(num6);
				num9 = Mathf.Abs(num9);
				f3 = Mathf.Abs(f3);
			}
			return str.Replace("{time}", num3.ToString("0.##")).Replace("{value1}", num4.ToString("0.##")).Replace("{value2}", num5.ToString("0.##")).Replace("{value3}", num6.ToString("0.##")).Replace("{factor1}", num7.ToString("0.##")).Replace("{factor2}", num8.ToString("0.##")).Replace("{factor3}", num9.ToString("0.##")).Replace("{stat1}", statShortName).Replace("{stat2}", statShortName2).Replace("{stat3}", statShortName3).Replace("{statperlevel1}", f.ToString("0.##")).Replace("{statperlevel2}", f2.ToString("0.##")).Replace("{statperlevel3}", f3.ToString("0.##")).Replace("{barrierhp}", ((targetBuffTemplet.m_fBarrierHP != 0f) ? (targetBuffTemplet.m_fBarrierHP * 100f + 100f * targetBuffTemplet.m_fBarrierHPPerLevel * (float)num) : 0f).ToString("0.##")).Replace("{barrierhpperlevel}", (targetBuffTemplet.m_fBarrierHPPerLevel * 100f).ToString("0.##")).Replace("{maxoverlapcount}", targetBuffTemplet.m_MaxOverlapCount.ToString("N0"));
		}

		// Token: 0x060043BF RID: 17343 RVA: 0x001511B4 File Offset: 0x0014F3B4
		public static string ApplyBuffValueToString(NKMTacticalCommandTemplet tacticalTemplet, int level)
		{
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder("");
			for (int i = 0; i < tacticalTemplet.m_lstBuffStrID_MyTeam.Count; i++)
			{
				NKMBuffTemplet buffTempletByStrID = NKMBuffManager.GetBuffTempletByStrID(tacticalTemplet.m_lstBuffStrID_MyTeam[i]);
				if (flag)
				{
					stringBuilder.AppendLine();
				}
				stringBuilder.Append(NKCUtilString.ApplyBuffValueToString(tacticalTemplet.GetTCDescMyTeam(i), buffTempletByStrID, level, level));
				flag = true;
			}
			for (int j = 0; j < tacticalTemplet.m_lstBuffStrID_Enemy.Count; j++)
			{
				NKMBuffTemplet buffTempletByStrID = NKMBuffManager.GetBuffTempletByStrID(tacticalTemplet.m_lstBuffStrID_Enemy[j]);
				if (flag && stringBuilder.Length != 0)
				{
					stringBuilder.AppendLine();
				}
				stringBuilder.Append(NKCUtilString.ApplyBuffValueToString(tacticalTemplet.GetTCDescEnemy(j), buffTempletByStrID, level, level));
				flag = true;
			}
			stringBuilder.Replace("{costpump}", tacticalTemplet.m_fCostPump.ToString("0.##"));
			stringBuilder.Replace("{costpumpperlevel}", tacticalTemplet.m_fCostPumpPerLevel.ToString("0.##"));
			stringBuilder.Replace("{costpumpresult}", (tacticalTemplet.m_fCostPump + tacticalTemplet.m_fCostPumpPerLevel * (float)(level - 1)).ToString("0.##"));
			return stringBuilder.ToString();
		}

		// Token: 0x060043C0 RID: 17344 RVA: 0x001512E0 File Offset: 0x0014F4E0
		private static float GetBuffStatPerLevelStringValue(float statPerLevel, NKM_STAT_TYPE statType, float statValue, float statFactor)
		{
			if (statValue != 0f)
			{
				statPerLevel *= (NKMUnitStatManager.IsPercentStat(statType) ? 0.01f : 0.0001f);
				return Mathf.Abs(statPerLevel);
			}
			if (statFactor != 0f)
			{
				return Mathf.Abs(statPerLevel * 0.01f);
			}
			return 0f;
		}

		// Token: 0x060043C1 RID: 17345 RVA: 0x00151330 File Offset: 0x0014F530
		public static string GetUserNickname(string nickname, bool bOpponent)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return nickname;
			}
			if (bOpponent)
			{
				if (gameOptionData.StreamingHideOpponentInfo)
				{
					return "???";
				}
				return NKCStringTable.GetString(nickname, true);
			}
			else
			{
				if (gameOptionData.StreamingHideMyInfo)
				{
					return "???";
				}
				return nickname;
			}
		}

		// Token: 0x060043C2 RID: 17346 RVA: 0x00151378 File Offset: 0x0014F578
		public static string GetUserGuildName(string guildName, bool bOpponent)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return guildName;
			}
			if (bOpponent)
			{
				if (gameOptionData.StreamingHideOpponentInfo)
				{
					return "???";
				}
			}
			else if (gameOptionData.StreamingHideMyInfo)
			{
				return "???";
			}
			return guildName;
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x060043C3 RID: 17347 RVA: 0x001513B5 File Offset: 0x0014F5B5
		public static string GET_STRING_AUTO_RESOURCE_SUPPLY_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_AUTO_RESOURCE_SUPPLY_FAIL", false);
			}
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x060043C4 RID: 17348 RVA: 0x001513C2 File Offset: 0x0014F5C2
		public static string GET_STRING_AUTO_RESOURCE_SUPPLY_FAIL_ETER_FULL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_AUTO_RESOURCE_SUPPLY_FAIL_ETER_FULL", false);
			}
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x060043C5 RID: 17349 RVA: 0x001513CF File Offset: 0x0014F5CF
		public static string GET_STRING_AUTO_RESOURCE_SUPPLY_CREDIT_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_AUTO_RESOURCE_SUPPLY_CREDIT_DESC_01", false);
			}
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x060043C6 RID: 17350 RVA: 0x001513DC File Offset: 0x0014F5DC
		public static string GET_STRING_AUTO_RESOURCE_SUPPLY_ETERNIUM_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_AUTO_RESOURCE_SUPPLY_ETERNIUM_DESC_01", false);
			}
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x060043C7 RID: 17351 RVA: 0x001513E9 File Offset: 0x0014F5E9
		public static string GET_STRING_AUTO_RESOURCE_SUPPLY_MAX_CREDIT
		{
			get
			{
				return NKCStringTable.GetString("SI_LOBBY_RESOURCE_RECHARGE_FUND_MAX_CREDIT", false);
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x060043C8 RID: 17352 RVA: 0x001513F6 File Offset: 0x0014F5F6
		public static string GET_STRING_AUTO_RESOURCE_SUPPLY_MAX_ETERNIUM
		{
			get
			{
				return NKCStringTable.GetString("SI_LOBBY_RESOURCE_RECHARGE_FUND_MAX_ETERNIUM", false);
			}
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x060043C9 RID: 17353 RVA: 0x00151403 File Offset: 0x0014F603
		public static string GET_STRING_TOOLTIP_ETC_NETWORK_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOOLTIP_ETC_NETWORK_TITLE", false);
			}
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x060043CA RID: 17354 RVA: 0x00151410 File Offset: 0x0014F610
		public static string GET_STRING_TOOLTIP_ETC_NETWORK_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TOOLTIP_ETC_NETWORK_DESC", false);
			}
		}

		// Token: 0x060043CB RID: 17355 RVA: 0x00151420 File Offset: 0x0014F620
		public static string GetVoiceCaption(NKMAssetName cNKMAssetName)
		{
			if (cNKMAssetName == null)
			{
				return string.Empty;
			}
			string strID = cNKMAssetName.m_BundleName + "@" + cNKMAssetName.m_AssetName;
			if (NKCDefineManager.DEFINE_SERVICE() && !NKCStringTable.CheckExistString(strID))
			{
				return string.Empty;
			}
			return NKCStringTable.GetString(strID, false);
		}

		// Token: 0x060043CC RID: 17356 RVA: 0x00151469 File Offset: 0x0014F669
		public static string GetStringVoiceCategory(bool lifetime)
		{
			if (lifetime)
			{
				return NKCStringTable.GetString("SI_DP_STRING_VOICE_CATEGORY_LIFETIME", false);
			}
			return NKCStringTable.GetString("SI_DP_STRING_VOICE_CATEGORY_NORMAL", false);
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x060043CD RID: 17357 RVA: 0x00151485 File Offset: 0x0014F685
		public static string GET_STRING_REPEAT_OPERATION_COST_COUNT_UNTIL_NOW
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REPEAT_OPERATION_COST_COUNT_UNTIL_NOW", false);
			}
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x060043CE RID: 17358 RVA: 0x00151492 File Offset: 0x0014F692
		public static string GET_STRING_REPEAT_OPERATION_COST_COUNT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REPEAT_OPERATION_COST_COUNT", false);
			}
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x060043CF RID: 17359 RVA: 0x0015149F File Offset: 0x0014F69F
		public static string GET_STRING_REPEAT_OPERATION_REPEAT_COUNT_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REPEAT_OPERATION_REPEAT_COUNT_ONE_PARAM", false);
			}
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x060043D0 RID: 17360 RVA: 0x001514AC File Offset: 0x0014F6AC
		public static string GET_STRING_REPEAT_OPERATION_RED_COLOR_REPEAT_COUNT_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REPEAT_OPERATION_RED_COLOR_REPEAT_COUNT_ONE_PARAM", false);
			}
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x060043D1 RID: 17361 RVA: 0x001514B9 File Offset: 0x0014F6B9
		public static string GET_STRING_REPEAT_OPERATION_COMPLETE_REPEAT_COUNT_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REPEAT_OPERATION_COMPLETE_REPEAT_COUNT_ONE_PARAM", false);
			}
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x060043D2 RID: 17362 RVA: 0x001514C6 File Offset: 0x0014F6C6
		public static string GET_STRING_REPEAT_OPERATION_REMAIN_REPEAT_COUNT_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REPEAT_OPERATION_REMAIN_REPEAT_COUNT_ONE_PARAM", false);
			}
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x060043D3 RID: 17363 RVA: 0x001514D3 File Offset: 0x0014F6D3
		public static string GET_STRING_REPEAT_OPERATION_IS_ON_GOING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REPEAT_OPERATION_IS_ON_GOING", false);
			}
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x060043D4 RID: 17364 RVA: 0x001514E0 File Offset: 0x0014F6E0
		public static string GET_STRING_REPEAT_OPERATION_IS_TERMINATED
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REPEAT_OPERATION_IS_TERMINATED", false);
			}
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x060043D5 RID: 17365 RVA: 0x001514ED File Offset: 0x0014F6ED
		public static string GET_STRING_REPEAT_OPERATION_RESULT_TOTAL_TIME
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REPEAT_OPERATION_RESULT_TOTAL_TIME", false);
			}
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x060043D6 RID: 17366 RVA: 0x001514FA File Offset: 0x0014F6FA
		public static string GET_STRING_REPEAT_OPERATION_NEED_MORE_RESOURCE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REPEAT_OPERATION_NEED_MORE_RESOURCE", false);
			}
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x060043D7 RID: 17367 RVA: 0x00151507 File Offset: 0x0014F707
		public static string GET_STRING_REPEAT_OPERATION_COST_MORE_REQUIRED
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REPEAT_OPERATION_COST_MORE_REQUIRED", false);
			}
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x060043D8 RID: 17368 RVA: 0x00151514 File Offset: 0x0014F714
		public static string GET_STRING_REPEAT_OPERATION_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_REPEAT_OPERATION_FAIL", false);
			}
		}

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x060043D9 RID: 17369 RVA: 0x00151521 File Offset: 0x0014F721
		public static string GET_STRING_PATCHER_PC_NEW_APP_AVAILABLE_ZLONG
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_PC_NEW_APP_AVAILABLE_ZLONG", false);
			}
		}

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x060043DA RID: 17370 RVA: 0x0015152E File Offset: 0x0014F72E
		public static string GET_STRING_REFRESH_DATA
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_REFRESH_DATA", false);
			}
		}

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x060043DB RID: 17371 RVA: 0x0015153B File Offset: 0x0014F73B
		public static string GET_STRING_PATCHER_NEED_UPDATE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_NEED_UPDATE", false);
			}
		}

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x060043DC RID: 17372 RVA: 0x00151548 File Offset: 0x0014F748
		public static string GET_STRING_PATCHER_CAN_UPDATE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_CAN_UPDATE", false);
			}
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x060043DD RID: 17373 RVA: 0x00151555 File Offset: 0x0014F755
		public static string GET_STRING_PATCHER_MOVE_TO_MARKET
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_MOVE_TO_MARKET", false);
			}
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x060043DE RID: 17374 RVA: 0x00151562 File Offset: 0x0014F762
		public static string GET_STRING_PATCHER_CONTINUE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_CONTINUE", false);
			}
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x060043DF RID: 17375 RVA: 0x0015156F File Offset: 0x0014F76F
		public static string GET_STRING_NOTICE_DOWNLOAD_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_NOTICE_DOWNLOAD_ONE_PARAM", false);
			}
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x060043E0 RID: 17376 RVA: 0x0015157C File Offset: 0x0014F77C
		public static string GET_STRING_NOTICE_ASK_DOWNLOAD_WITH_PROLOGUE_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_ASK_DOWNLOAD_WITH_PROLOGUE_ONE_PARAM", false);
			}
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x060043E1 RID: 17377 RVA: 0x00151589 File Offset: 0x0014F789
		public static string GET_STRING_NOTICE_PLAY_WITHOUT_DOWNLOAD
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_PLAY_WITHOUT_DOWNLOAD", false);
			}
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x060043E2 RID: 17378 RVA: 0x00151596 File Offset: 0x0014F796
		public static string GET_STRING_NOTICE_ASK_DOWNLOAD_IMMEDIATELY_OR_WITH_PROLOGUE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_ASK_DOWNLOAD_IMMEDIATELY_OR_WITH_PROLOGUE", false);
			}
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x060043E3 RID: 17379 RVA: 0x001515A3 File Offset: 0x0014F7A3
		public static string GET_STRING_DECONNECT_INTERNET
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_DECONNECT_INTERNET", false);
			}
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x060043E4 RID: 17380 RVA: 0x001515B0 File Offset: 0x0014F7B0
		public static string GET_STRING_RETRY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_RETRY", false);
			}
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x060043E5 RID: 17381 RVA: 0x001515BD File Offset: 0x0014F7BD
		public static string GET_STRING_FAIL_VERSION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_FAIL_VERSION", false);
			}
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x060043E6 RID: 17382 RVA: 0x001515CA File Offset: 0x0014F7CA
		public static string GET_STRING_FAIL_PATCHDATA
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_FAIL_PATCHDATA", false);
			}
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x060043E7 RID: 17383 RVA: 0x001515D7 File Offset: 0x0014F7D7
		public static string GET_STRING_ERROR_DOWNLOAD_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_ERROR_DOWNLOAD_ONE_PARAM", false);
			}
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x060043E8 RID: 17384 RVA: 0x001515E4 File Offset: 0x0014F7E4
		public static string GET_STRING_PATCHER_CHECKING_VERSION_INFORMATION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_CHECKING_VERSION_INFORMATION", false);
			}
		}

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x060043E9 RID: 17385 RVA: 0x001515F1 File Offset: 0x0014F7F1
		public static string GET_STRING_PATCHER_DOWNLOADING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_DOWNLOADING", false);
			}
		}

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x060043EA RID: 17386 RVA: 0x001515FE File Offset: 0x0014F7FE
		public static string GET_STRING_PATCHER_FINISHING_PATCHPROCESS
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_FINISHING_PATCHPROCESS", false);
			}
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x060043EB RID: 17387 RVA: 0x0015160B File Offset: 0x0014F80B
		public static string GET_STRING_PATCHER_INITIALIZING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_INITIALIZING", false);
			}
		}

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x060043EC RID: 17388 RVA: 0x00151618 File Offset: 0x0014F818
		public static string GET_STRING_PATCHER_ERROR_DOWNLOADING_THREE_PARMA
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_ERROR_DOWNLOADING_THREE_PARMA", false);
			}
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x060043ED RID: 17389 RVA: 0x00151625 File Offset: 0x0014F825
		public static string GET_STRING_PATCHER_CAN_BACKGROUND_DOWNLOAD
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_CAN_BACKGROUND_DOWNLOAD", false);
			}
		}

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x060043EE RID: 17390 RVA: 0x00151632 File Offset: 0x0014F832
		public static string GET_NXPATCHER_DOWNLOAD_COMPLETE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_NXPATCHER_DOWNLOAD_COMPLETE", false);
			}
		}

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x060043EF RID: 17391 RVA: 0x0015163F File Offset: 0x0014F83F
		public static string GET_NXPATCHER_DOWNLOAD_CONTINUE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_NXPATCHER_DOWNLOAD_CONTINUE", false);
			}
		}

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x060043F0 RID: 17392 RVA: 0x0015164C File Offset: 0x0014F84C
		public static string GET_NXPATCHER_DOWNLOAD_PROGRESS
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_NXPATCHER_DOWNLOAD_PROGRESS", false);
			}
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x060043F1 RID: 17393 RVA: 0x00151659 File Offset: 0x0014F859
		public static string GET_STRING_PATCHER_WARNING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_WARNING", false);
			}
		}

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x060043F2 RID: 17394 RVA: 0x00151666 File Offset: 0x0014F866
		public static string GET_STRING_PATCHER_CONFIRM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_CONFIRM", false);
			}
		}

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x060043F3 RID: 17395 RVA: 0x00151673 File Offset: 0x0014F873
		public static string GET_STRING_PATCHER_CANCEL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_CANCEL", false);
			}
		}

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x060043F4 RID: 17396 RVA: 0x00151680 File Offset: 0x0014F880
		public static string GET_STRING_PATCHER_ERROR
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_ERROR", false);
			}
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x060043F5 RID: 17397 RVA: 0x0015168D File Offset: 0x0014F88D
		public static string GET_STRING_PATCHER_NOTICE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PATCHER_NOTICE", false);
			}
		}

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x060043F6 RID: 17398 RVA: 0x0015169A File Offset: 0x0014F89A
		public static string GET_DEV_CONSOLE_CHEAT_EMOTICON_CHEAT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_DEV_CONSOLE_CHEAT_EMOTICON_CHEAT", false);
			}
		}

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x060043F7 RID: 17399 RVA: 0x001516A7 File Offset: 0x0014F8A7
		public static string GET_DEV_CONSOLE_CHEAT_MOOJUCK_MODE_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_DEV_CONSOLE_CHEAT_moojuck_MODE_DESC_01", false);
			}
		}

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x060043F8 RID: 17400 RVA: 0x001516B4 File Offset: 0x0014F8B4
		public static string GET_DEV_CONSOLE_CHEAT_WAREFARE_UNBREAKABLE_MODE_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_DEV_CONSOLE_CHEAT_WAREFARE_UNBREAKABLE_MODE_DESC_01", false);
			}
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x060043F9 RID: 17401 RVA: 0x001516C1 File Offset: 0x0014F8C1
		public static string GET_DEV_CONSOLE_CHEAT_ACCOUNT_RESET_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_DEV_CONSOLE_CHEAT_ACCOUNT_RESET_DESC", false);
			}
		}

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x060043FA RID: 17402 RVA: 0x001516CE File Offset: 0x0014F8CE
		public static string GET_DEV_CONSOLE_CHEAT_MANAGEMENT_RESET_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_DEV_CONSOLE_CHEAT_MANAGEMENT_RESET_DESC", false);
			}
		}

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x060043FB RID: 17403 RVA: 0x001516DB File Offset: 0x0014F8DB
		public static string GET_DEV_CONSOLE_TUTORIAL_NECESSARY_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_DEV_CONSOLE_CHEAT_TUTORIAL_NECESSARY_DESC_01", false);
			}
		}

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x060043FC RID: 17404 RVA: 0x001516E8 File Offset: 0x0014F8E8
		public static string GET_DEV_CONSOLE_TUTORIAL_COMPLETE_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_DEV_CONSOLE_CHEAT_TUTORIAL_COMPLETE_DESC_01", false);
			}
		}

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x060043FD RID: 17405 RVA: 0x001516F5 File Offset: 0x0014F8F5
		public static string GET_DEV_CONSOLE_WORLDMAP_NO_LEADER
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_DEV_CONSOLE_CHEAT_LEADER_ERROR", false);
			}
		}

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x060043FE RID: 17406 RVA: 0x00151702 File Offset: 0x0014F902
		public static string GET_DEV_CONSOLE_WORLDMAP_MISSION_NOT_GOING
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_DEV_CONSOLE_CHEAT_MISSION_ERROR", false);
			}
		}

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x060043FF RID: 17407 RVA: 0x0015170F File Offset: 0x0014F90F
		public static string GET_DEV_CONSOLE_WORLDMAP_MISSION_LEFT_TIME_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_DEV_CONSOLE_CHEAT_MISSION_TIME", false);
			}
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x06004400 RID: 17408 RVA: 0x0015171C File Offset: 0x0014F91C
		public static string GET_DEV_CONSOLE_WORLDMAP_NO_EVENT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_DEV_CONSOLE_CHEAT_EVENT_ERROR", false);
			}
		}

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x06004401 RID: 17409 RVA: 0x00151729 File Offset: 0x0014F929
		public static string GET_DEV_CONSOLE_WORLDMAP_EVENT_LEFT_TIME_TWO_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_DEV_CONSOLE_CHEAT_EVENT_TIME", false);
			}
		}

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x06004402 RID: 17410 RVA: 0x00151736 File Offset: 0x0014F936
		public static string GET_DEV_CONSOLE_WORLDMAP_NO_EXIST_EVENT_ID
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_DEV_CONSOLE_CHEAT_EVENT_NOTFOUND", false);
			}
		}

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x06004403 RID: 17411 RVA: 0x00151743 File Offset: 0x0014F943
		public static string GET_STRING_REPLAY
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_REPLAY", false);
			}
		}

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x06004404 RID: 17412 RVA: 0x00151750 File Offset: 0x0014F950
		public static string GET_STRING_REPLAY_OPTION_LEAVE_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPTION_REPLAY_LEAVE_TITLE", false);
			}
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x06004405 RID: 17413 RVA: 0x0015175D File Offset: 0x0014F95D
		public static string GET_STRING_REPLAY_OPTION_LEAVE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPTION_REPLAY_LEAVE_DESC", false);
			}
		}

		// Token: 0x06004406 RID: 17414 RVA: 0x0015176C File Offset: 0x0014F96C
		public static string GetDeckConditionString(NKMDeckCondition.SingleCondition condition)
		{
			switch (condition.eCondition)
			{
			case NKMDeckCondition.DECK_CONDITION.UNIT_STYLE:
				return string.Format("{0} {1}", NKCUtilString.GetValueListString(condition, (int x) => NKCUtilString.GetUnitStyleName((NKM_UNIT_STYLE_TYPE)x)), NKCUtilString.GetMoreLessString(condition.eMoreLess));
			case NKMDeckCondition.DECK_CONDITION.UNIT_GRADE:
				return string.Format("{0} {1}", NKCUtilString.GetValueListString(condition, new NKCUtilString.ConditionToString(NKCUtilString.GetEventdeckGradeString)), NKCUtilString.GetMoreLessString(condition.eMoreLess));
			case NKMDeckCondition.DECK_CONDITION.UNIT_COST:
				return string.Format(NKCStringTable.GetString("SI_DP_DECKCONDITION_UNIT_COST_TWO_PARAM", false), NKCUtilString.GetValueListString(condition), NKCUtilString.GetMoreLessString(condition.eMoreLess));
			case NKMDeckCondition.DECK_CONDITION.UNIT_ROLE:
				return string.Format("{0} {1}", NKCUtilString.GetValueListString(condition, (int x) => NKCUtilString.GetUnitRoleType((NKM_UNIT_ROLE_TYPE)x, false)), NKCUtilString.GetMoreLessString(condition.eMoreLess));
			case NKMDeckCondition.DECK_CONDITION.UNIT_LEVEL:
				return string.Format(NKCStringTable.GetString("SI_DP_DECKCONDITION_UNIT_LEVEL_TWO_PARAM", false), NKCUtilString.GetValueListString(condition), NKCUtilString.GetMoreLessString(condition.eMoreLess));
			case NKMDeckCondition.DECK_CONDITION.SHIP_STYLE:
				return string.Format("{0} {1}", NKCUtilString.GetValueListString(condition, (int x) => NKCUtilString.GetUnitStyleType((NKM_UNIT_STYLE_TYPE)x)), NKCUtilString.GetMoreLessString(condition.eMoreLess));
			case NKMDeckCondition.DECK_CONDITION.SHIP_LEVEL:
				return string.Format(NKCStringTable.GetString("SI_DP_DECKCONDITION_SHIP_LEVEL_TWO_PARAM", false), NKCUtilString.GetValueListString(condition), NKCUtilString.GetMoreLessString(condition.eMoreLess));
			case NKMDeckCondition.DECK_CONDITION.UNIT_ID_NOT:
			{
				string valueListString = NKCUtilString.GetValueListString(condition, delegate(int x)
				{
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(x);
					if (unitTempletBase == null)
					{
						Debug.LogError(string.Format("UnitID {0} from deckCondition not found!", x));
						return "";
					}
					return unitTempletBase.GetUnitName();
				});
				return string.Format(NKCStringTable.GetString("SI_DP_DECKCONDITION_UNIT_ID_NOT_ONE_PARAM", false), valueListString);
			}
			case NKMDeckCondition.DECK_CONDITION.UNIT_COST_TOTAL:
				return string.Format(NKCStringTable.GetString("SI_DP_DECKCONDITION_UNIT_COST_TOTAL_TWO_PARAM", false), NKCUtilString.GetValueListString(condition), NKCUtilString.GetMoreLessString(condition.eMoreLess));
			case NKMDeckCondition.DECK_CONDITION.AWAKEN_COUNT:
				if (condition.IsProhibited())
				{
					return string.Format(NKCStringTable.GetString("SI_DP_DECKCONDITION_AWAKEN_ONE_PARAM", false), NKCUtilString.GetMoreLessString(NKMDeckCondition.MORE_LESS.NOT));
				}
				return string.Format(NKCStringTable.GetString("SI_DP_DECKCONDITION_AWAKEN_COUNTL_TWO_PARAM", false), NKCUtilString.GetValueListString(condition), NKCUtilString.GetMoreLessString(condition.eMoreLess));
			case NKMDeckCondition.DECK_CONDITION.UNIT_GROUND_COUNT:
				if (condition.IsProhibited())
				{
					return string.Format(NKCStringTable.GetString("SI_DP_DECKCONDITION_UNIT_GROUND_NOT", false), Array.Empty<object>());
				}
				return string.Format(NKCStringTable.GetString("SI_DP_DECKCONDITION_UNIT_GROUND_COUNT_TWO_PARAM", false), NKCUtilString.GetValueListString(condition), NKCUtilString.GetMoreLessString(condition.eMoreLess));
			case NKMDeckCondition.DECK_CONDITION.UNIT_AIR_COUNT:
				if (condition.IsProhibited())
				{
					return string.Format(NKCStringTable.GetString("SI_DP_DECKCONDITION_UNIT_AIR_NOT", false), Array.Empty<object>());
				}
				return string.Format(NKCStringTable.GetString("SI_DP_DECKCONDITION_UNIT_AIR_COUNT_TWO_PARAM", false), NKCUtilString.GetValueListString(condition), NKCUtilString.GetMoreLessString(condition.eMoreLess));
			case NKMDeckCondition.DECK_CONDITION.UNIT_GRADE_COUNT:
				if (condition.IsProhibited())
				{
					return string.Format(NKCStringTable.GetString("SI_DP_DECKCONDITION_UNIT_GRADE_COUNT_NOT", new object[]
					{
						NKCUtilString.GetValueListString(condition, new NKCUtilString.ConditionToString(NKCUtilString.GetEventdeckGradeString))
					}), Array.Empty<object>());
				}
				return string.Format(NKCStringTable.GetString("SI_DP_DECKCONDITION_UNIT_GRADE_COUNT_THREE_PARAM", false), NKCUtilString.GetValueListString(condition, new NKCUtilString.ConditionToString(NKCUtilString.GetEventdeckGradeString)), condition.Value, NKCUtilString.GetMoreLessString(condition.eMoreLess));
			default:
				return "";
			}
		}

		// Token: 0x06004407 RID: 17415 RVA: 0x00151A90 File Offset: 0x0014FC90
		public static string GetGameConditionString(NKMDeckCondition.GameCondition condition)
		{
			NKMDeckCondition.GAME_CONDITION eCondition = condition.eCondition;
			if (eCondition != NKMDeckCondition.GAME_CONDITION.LEVEL_CAP)
			{
				if (eCondition == NKMDeckCondition.GAME_CONDITION.MODIFY_START_COST)
				{
					if (condition.Value > 0)
					{
						return NKCStringTable.GetString("SI_DP_GAMECONDITION_START_COST_TWO_PARAM", new object[]
						{
							condition.Value,
							NKCStringTable.GetString("SI_DP_GAMECONDITION_INCREASE", false)
						});
					}
					if (condition.Value < 0)
					{
						return NKCStringTable.GetString("SI_DP_GAMECONDITION_START_COST_TWO_PARAM", new object[]
						{
							Mathf.Abs(condition.Value),
							NKCStringTable.GetString("SI_DP_GAMECONDITION_DECREASE", false)
						});
					}
				}
				return "";
			}
			return string.Format(NKCStringTable.GetString("SI_DP_DECKCONDITION_LEVEL_CAP_ONE_PARAM", false), condition.Value);
		}

		// Token: 0x06004408 RID: 17416 RVA: 0x00151B44 File Offset: 0x0014FD44
		private static string GetValueListString(NKMDeckCondition.SingleCondition condition, NKCUtilString.ConditionToString converter)
		{
			if (condition.lstValue != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < condition.lstValue.Count; i++)
				{
					if (i != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(converter(condition.lstValue[i]));
				}
				return stringBuilder.ToString();
			}
			return converter(condition.Value);
		}

		// Token: 0x06004409 RID: 17417 RVA: 0x00151BB0 File Offset: 0x0014FDB0
		private static string GetValueListString(NKMDeckCondition.SingleCondition condition)
		{
			return NKCUtilString.GetValueListString(condition, (int x) => x.ToString());
		}

		// Token: 0x0600440A RID: 17418 RVA: 0x00151BD8 File Offset: 0x0014FDD8
		private static string GetEventdeckGradeString(int grade)
		{
			switch ((short)grade)
			{
			default:
				return NKCStringTable.GetString("SI_DP_UNIT_GRADE_STRING_NUG_N_FOR_EVENTDECK", false);
			case 1:
				return NKCStringTable.GetString("SI_DP_UNIT_GRADE_STRING_NUG_R_FOR_EVENTDECK", false);
			case 2:
				return NKCStringTable.GetString("SI_DP_UNIT_GRADE_STRING_NUG_SR_FOR_EVENTDECK", false);
			case 3:
				return NKCStringTable.GetString("SI_DP_UNIT_GRADE_STRING_NUG_SSR_FOR_EVENTDECK", false);
			}
		}

		// Token: 0x0600440B RID: 17419 RVA: 0x00151C30 File Offset: 0x0014FE30
		public static string GetMoreLessString(NKMDeckCondition.MORE_LESS MoreLess)
		{
			switch (MoreLess)
			{
			default:
				return NKCStringTable.GetString("SI_DP_EVENTDECK_DECKCONDITION_EQUAL", false);
			case NKMDeckCondition.MORE_LESS.NOT:
				return NKCStringTable.GetString("SI_DP_EVENTDECK_DECKCONDITION_NOT", false);
			case NKMDeckCondition.MORE_LESS.MORE:
				return NKCStringTable.GetString("SI_DP_EVENTDECK_DECKCONDITION_MORE", false);
			case NKMDeckCondition.MORE_LESS.LESS:
				return NKCStringTable.GetString("SI_DP_EVENTDECK_DECKCONDITION_LESS", false);
			}
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x0600440C RID: 17420 RVA: 0x00151C82 File Offset: 0x0014FE82
		public static string GET_STRING_EVENT_TOTAL_PAY
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENT_S_MEDAL_CASH_INPUT_01", false);
			}
		}

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x0600440D RID: 17421 RVA: 0x00151C8F File Offset: 0x0014FE8F
		public static string GET_STRING_EVENT_TOTAL_PAY_RETURN
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENT_S_MEDAL_CASH_INPUT_02", false);
			}
		}

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x0600440E RID: 17422 RVA: 0x00151C9C File Offset: 0x0014FE9C
		public static string GET_STRING_EVENT_MENU
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MENU_EVENT", false);
			}
		}

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x0600440F RID: 17423 RVA: 0x00151CA9 File Offset: 0x0014FEA9
		public static string GET_STRING_EXCEPTION_EVENT_EXPIRED_POPUP
		{
			get
			{
				return NKCStringTable.GetString("SI_MENU_EXCEPTION_EVENT_EXPIRED_POPUP", false);
			}
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x06004410 RID: 17424 RVA: 0x00151CB6 File Offset: 0x0014FEB6
		public static string GET_STRING_EVENT_BINGO_MILEAGE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BINGO_SELECT_MILLAGE_INFORMATION", false);
			}
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x06004411 RID: 17425 RVA: 0x00151CC3 File Offset: 0x0014FEC3
		public static string GET_STRING_EVENT_BINGO_SPECIAL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BINGO_SELECT_BUTTON_OFF_TO_ON", false);
			}
		}

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x06004412 RID: 17426 RVA: 0x00151CD0 File Offset: 0x0014FED0
		public static string GET_STRING_EVENT_BINGO_SPECIAL_CANCEL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BINGO_SELECT_BUTTON_ON_TO_OFF", false);
			}
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x06004413 RID: 17427 RVA: 0x00151CDD File Offset: 0x0014FEDD
		public static string GET_STRING_EVENT_BINGO_REWARD_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BINGO_REWARD_BINGO_LINE_TITLE", false);
			}
		}

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x06004414 RID: 17428 RVA: 0x00151CEA File Offset: 0x0014FEEA
		public static string GET_STRING_EVENT_BINGO_REWARD_SLOT_PROGRESS
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BINGO_REWARD_GET", false);
			}
		}

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x06004415 RID: 17429 RVA: 0x00151CF7 File Offset: 0x0014FEF7
		public static string GET_STRING_EVENT_BINGO_REWARD_SLOT_COMPLETE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BINGO_REWARD_GET_COMPLET", false);
			}
		}

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x06004416 RID: 17430 RVA: 0x00151D04 File Offset: 0x0014FF04
		public static string GET_STRING_EVENT_BINGO_TRY_CONFIRM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BINGO_GET_QUESTION_NUMBER_NORMAL_POPUP", false);
			}
		}

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x06004417 RID: 17431 RVA: 0x00151D11 File Offset: 0x0014FF11
		public static string GET_STRING_EVENT_BINGO_USE_MILEAGE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BINGO_GET_QUESTION_NUMBER_SELECT_POPUP", false);
			}
		}

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x06004418 RID: 17432 RVA: 0x00151D1E File Offset: 0x0014FF1E
		public static string GET_STRING_EVENT_BINGO_REMAIN_MILEAGE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_BINGO_GET_INFOMATION_HAVE_POINT", false);
			}
		}

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x06004419 RID: 17433 RVA: 0x00151D2B File Offset: 0x0014FF2B
		public static string GET_STRING_EVENT_BINGO_COMPLETE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENT_BINGO_ALL_CLEAR_TOAST_MESSAGE", false);
			}
		}

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x0600441A RID: 17434 RVA: 0x00151D38 File Offset: 0x0014FF38
		public static string GET_STRING_EVENT_RACE_RESULT_TEAM_RED
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENT_RACE_RESULT_TEAM_RED", false);
			}
		}

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x0600441B RID: 17435 RVA: 0x00151D45 File Offset: 0x0014FF45
		public static string GET_STRING_EVENT_RACE_RESULT_TEAM_BLUE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENT_RACE_RESULT_TEAM_BLUE", false);
			}
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x0600441C RID: 17436 RVA: 0x00151D52 File Offset: 0x0014FF52
		public static string GET_STRING_CONSORTIUM_INTRO
		{
			get
			{
				return NKCStringTable.GetString("SI_LOBBY_RIGHT_MENU_3_CONSORTIUM", false);
			}
		}

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x0600441D RID: 17437 RVA: 0x00151D5F File Offset: 0x0014FF5F
		public static string GET_STRING_CONSORTIUM_JOIN_COOLTIME_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_JOIN_COOLTIME_DESC", false);
			}
		}

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x0600441E RID: 17438 RVA: 0x00151D6C File Offset: 0x0014FF6C
		public static string GET_STRING_CONSORTIUM_INTRO_JOIN_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_INTRO_JOIN_TEXT", false);
			}
		}

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x0600441F RID: 17439 RVA: 0x00151D79 File Offset: 0x0014FF79
		public static string GET_STRING_CONSORTIUM_INTRO_FOUNDATION
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_INTRO_FOUNDATION_TEXT", false);
			}
		}

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x06004420 RID: 17440 RVA: 0x00151D86 File Offset: 0x0014FF86
		public static string GET_STRING_CONSORTIUM_CREATE_NAME_SUB_GUIDE_USEFUL
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_CREATE_NAME_SUB_GUIDE_USEFUL_DESC", false);
			}
		}

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x06004421 RID: 17441 RVA: 0x00151D93 File Offset: 0x0014FF93
		public static string GET_STRING_CONSORTIUM_CREATE_NAME_SUB_GUIDE_BADWORD
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_CREATE_NAME_SUB_GUIDE_BADWORD_DESC", false);
			}
		}

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x06004422 RID: 17442 RVA: 0x00151DA0 File Offset: 0x0014FFA0
		public static string GET_STRING_CONSORTIUM_CREATE_NAME_SUB_GUIDE_BASIC_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_CREATE_NAME_SUB_GUIDE_BASIC_DESC", false);
			}
		}

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x06004423 RID: 17443 RVA: 0x00151DAD File Offset: 0x0014FFAD
		public static string GET_STRING_CONSORTIUM_CREATE_NAME_SUB_GUIDE_BASIC_DESC_GLOBAL
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_CREATE_NAME_SUB_GUIDE_BASIC_DESC_GLOBAL", false);
			}
		}

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x06004424 RID: 17444 RVA: 0x00151DBA File Offset: 0x0014FFBA
		public static string GET_STRING_CONSORTIUM_CREATE_CONFIRM_POPUP_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_CREATE_CONFIRM_POPUP_TITLE_DESC", false);
			}
		}

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x06004425 RID: 17445 RVA: 0x00151DC7 File Offset: 0x0014FFC7
		public static string GET_STRING_CONSORTIUM_CREATE_CONFIRM_POPUP_BODY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_CREATE_CONFIRM_POPUP_BODY_DESC", false);
			}
		}

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x06004426 RID: 17446 RVA: 0x00151DD4 File Offset: 0x0014FFD4
		public static string GET_STRING_CONSORTIUM_JOIN_CONFIRM_SITUATION
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_JOIN_CONFIRM_SITUATION_DESC", false);
			}
		}

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x06004427 RID: 17447 RVA: 0x00151DE1 File Offset: 0x0014FFE1
		public static string GET_STRING_CONSORTIUM_ATTENDANCE_REWARD_CONDITION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_ATTENDANCE_REWARD_CONDITION", false);
			}
		}

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x06004428 RID: 17448 RVA: 0x00151DEE File Offset: 0x0014FFEE
		public static string GET_STRING_CONSORTIUM_POPUP_ATTENDANCE_REWARD_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_POPUP_ATTENDANCE_REWARD_TITLE", false);
			}
		}

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06004429 RID: 17449 RVA: 0x00151DFB File Offset: 0x0014FFFB
		public static string GET_STRING_CONSORTIUM_CREATE_JOIN_METHOD_RIGHTOFF_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_CREATE_JOIN_METHOD_RIGHTOFF_DESC", false);
			}
		}

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x0600442A RID: 17450 RVA: 0x00151E08 File Offset: 0x00150008
		public static string GET_STRING_CONSORTIUM_CREATE_JOIN_METHOD_CONFIRM_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_CREATE_JOIN_METHOD_CONFIRM_DESC", false);
			}
		}

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x0600442B RID: 17451 RVA: 0x00151E15 File Offset: 0x00150015
		public static string GET_STRING_CONSORTIUM_CREATE_JOIN_METHOD_BLIND_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_CREATE_JOIN_METHOD_BLIND_DESC", false);
			}
		}

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x0600442C RID: 17452 RVA: 0x00151E22 File Offset: 0x00150022
		public static string GET_STRING_CONSORTIUM_CREATE_JOIN_METHOD_GUIDE_RIGHTOFF_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_CREATE_JOIN_METHOD_GUIDE_RIGHTOFF_DESC", false);
			}
		}

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x0600442D RID: 17453 RVA: 0x00151E2F File Offset: 0x0015002F
		public static string GET_STRING_CONSORTIUM_CREATE_JOIN_METHOD_GUIDE_CONFIRM_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_CREATE_JOIN_METHOD_GUIDE_CONFIRM_DESC", false);
			}
		}

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x0600442E RID: 17454 RVA: 0x00151E3C File Offset: 0x0015003C
		public static string GET_STRING_CONSORTIUM_CREATE_JOIN_METHOD_GUIDE_BLIND_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_CREATE_JOIN_METHOD_GUIDE_BLIND_DESC", false);
			}
		}

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x0600442F RID: 17455 RVA: 0x00151E49 File Offset: 0x00150049
		public static string GET_STRING_CONSORTIUM_POPUP_ATTENDANCE_REWARD_BASIC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_POPUP_ATTENDANCE_REWARD_BASIC", false);
			}
		}

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06004430 RID: 17456 RVA: 0x00151E56 File Offset: 0x00150056
		public static string GET_STRING_CONSORTIUM_JOIN_CONFIRM_JOIN_POPUP_TITLE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_JOIN_CONFIRM_JOIN_POPUP_TITLE_DESC", false);
			}
		}

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06004431 RID: 17457 RVA: 0x00151E63 File Offset: 0x00150063
		public static string GET_STRING_CONSORTIUM_JOIN_RIGHTOFF_JOIN_POPUP_BODY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_JOIN_RIGHTOFF_JOIN_POPUP_BODY_DESC", false);
			}
		}

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06004432 RID: 17458 RVA: 0x00151E70 File Offset: 0x00150070
		public static string GET_CONSORTIUM_JOIN_RIGHTOFF_JOIN_POPUP_APPROVE_BTN_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_JOIN_RIGHTOFF_JOIN_POPUP_APPROVE_BTN_DESC", false);
			}
		}

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06004433 RID: 17459 RVA: 0x00151E7D File Offset: 0x0015007D
		public static string GET_CONSORTIUM_JOIN_CONFIRM_JOIN_POPUP_APPROVE_BTN_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_JOIN_CONFIRM_JOIN_POPUP_APPROVE_BTN_DESC", false);
			}
		}

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06004434 RID: 17460 RVA: 0x00151E8A File Offset: 0x0015008A
		public static string GET_STRING_CONSORTIUM_MEMBER_CONFIRM_JOIN_CONFIRM_POPUP_TITLE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_MEMBER_CONFIRM_JOIN_CONFIRM_POPUP_TITLE_DESC", false);
			}
		}

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06004435 RID: 17461 RVA: 0x00151E97 File Offset: 0x00150097
		public static string GET_STRING_CONSORTIUM_MEMBER_CONFIRM_JOIN_CONFIRM_POPUP_BODY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_MEMBER_CONFIRM_JOIN_CONFIRM_POPUP_BODY_DESC", false);
			}
		}

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06004436 RID: 17462 RVA: 0x00151EA4 File Offset: 0x001500A4
		public static string GET_STRING_CONSORTIUM_MEMBER_CONFIRM_JOIN_REFUSE_POPUP_TITLE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_MEMBER_CONFIRM_JOIN_REFUSE_POPUP_TITLE_DESC", false);
			}
		}

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06004437 RID: 17463 RVA: 0x00151EB1 File Offset: 0x001500B1
		public static string GET_STRING_CONSORTIUM_MEMBER_CONFIRM_JOIN_REFUSE_POPUP_BODY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_MEMBER_CONFIRM_JOIN_REFUSE_POPUP_BODY_DESC", false);
			}
		}

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06004438 RID: 17464 RVA: 0x00151EBE File Offset: 0x001500BE
		public static string GET_CONSORTIUM_JOIN_CONFIRM_JOIN_CANCEL_POPUP_TITLE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_JOIN_CONFIRM_JOIN_CANCEL_POPUP_TITLE_DESC", false);
			}
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06004439 RID: 17465 RVA: 0x00151ECB File Offset: 0x001500CB
		public static string GET_CONSORTIUM_JOIN_CONFIRM_JOIN_CANCEL_POPUP_BODY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_JOIN_CONFIRM_JOIN_CANCEL_POPUP_BODY_DESC", false);
			}
		}

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x0600443A RID: 17466 RVA: 0x00151ED8 File Offset: 0x001500D8
		public static string GET_STRING_CONSORTIUM_JOIN_CONFIRM_JOIN_SUCCESS_POPUP_TITLE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_JOIN_CONFIRM_JOIN_SUCCESS_POPUP_TITLE_DESC", false);
			}
		}

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x0600443B RID: 17467 RVA: 0x00151EE5 File Offset: 0x001500E5
		public static string GET_STRING_CONSORTIUM_JOIN_CONFIRM_JOIN_SUCCESS_POPUP_BODY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_JOIN_CONFIRM_JOIN_SUCCESS_POPUP_BODY_DESC", false);
			}
		}

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x0600443C RID: 17468 RVA: 0x00151EF2 File Offset: 0x001500F2
		public static string GET_STRING_CONSORTIUM_OPTION_DATA_SAVE_CANCEL_POPUP_TITLE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_OPTION_DATA_SAVE_CANCEL_POPUP_TITLE_DESC", false);
			}
		}

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x0600443D RID: 17469 RVA: 0x00151EFF File Offset: 0x001500FF
		public static string GET_STRING_CONSORTIUM_OPTION_DATA_SAVE_CANCEL_POPUP_BODY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_OPTION_DATA_SAVE_CANCEL_POPUP_BODY_DESC", false);
			}
		}

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x0600443E RID: 17470 RVA: 0x00151F0C File Offset: 0x0015010C
		public static string GET_STRING_CONSORTIUM_OPTION_DATA_SAVE_POPUP_TITLE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_OPTION_DATA_SAVE_POPUP_TITLE_DESC", false);
			}
		}

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x0600443F RID: 17471 RVA: 0x00151F19 File Offset: 0x00150119
		public static string GET_STRING_CONSORTIUM_OPTION_DATA_SAVE_POPUP_BODY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_OPTION_DATA_SAVE_POPUP_BODY_DESC", false);
			}
		}

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x06004440 RID: 17472 RVA: 0x00151F26 File Offset: 0x00150126
		public static string GET_STRING_CONSORTIUM_LOBBY_INFORMATION_CHANGE_OVERLAY_TITLE_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_LOBBY_INFORMATION_CHANGE_OVERLAY_TITLE_TEXT", false);
			}
		}

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x06004441 RID: 17473 RVA: 0x00151F33 File Offset: 0x00150133
		public static string GET_STRING_CONSORTIUM_LOBBY_INFORMATION_CHANGE_OVERLAY_BODY_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_LOBBY_INFORMATION_CHANGE_OVERLAY_BODY_TEXT", false);
			}
		}

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06004442 RID: 17474 RVA: 0x00151F40 File Offset: 0x00150140
		public static string GET_STRING_CONSORTIUM_MEMBER_GRADE_UP
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_MEMBER_GRADE_UP", false);
			}
		}

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x06004443 RID: 17475 RVA: 0x00151F4D File Offset: 0x0015014D
		public static string GET_STRING_CONSORTIUM_MEMBER_GRADE_UP_CONFIRM_POPUP_BODY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_MEMBER_GRADE_UP_CONFIRM_POPUP_BODY_DESC", false);
			}
		}

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06004444 RID: 17476 RVA: 0x00151F5A File Offset: 0x0015015A
		public static string GET_STRING_CONSORTIUM_MEMBER_GRADE_DOWN
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_MEMBER_GRADE_DOWN", false);
			}
		}

		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x06004445 RID: 17477 RVA: 0x00151F67 File Offset: 0x00150167
		public static string GET_STRING_CONSORTIUM_MEMBER_GRADE_DOWN_CONFIRM_POPUP_BODY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_MEMBER_GRADE_DOWN_CONFIRM_POPUP_BODY_DESC", false);
			}
		}

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06004446 RID: 17478 RVA: 0x00151F74 File Offset: 0x00150174
		public static string GET_STRING_CONSORTIUM_OPTION_DISMANTLE_POPUP_TITLE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_OPTION_DISMANTLE_POPUP_TITLE_DESC", false);
			}
		}

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06004447 RID: 17479 RVA: 0x00151F81 File Offset: 0x00150181
		public static string GET_STRING_CONSORTIUM_OPTION_DISMANTLE_POPUP_BODY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_OPTION_DISMANTLE_POPUP_BODY_DESC", false);
			}
		}

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06004448 RID: 17480 RVA: 0x00151F8E File Offset: 0x0015018E
		public static string GET_STRING_CONSORTIUM_OPTION_DISMANTLE_POPUP_CONFIRM_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_OPTION_DISMANTLE_POPUP_CONFIRM_DESC", false);
			}
		}

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06004449 RID: 17481 RVA: 0x00151F9B File Offset: 0x0015019B
		public static string GET_STRING_CONSORTIUM_OPTION_DISMANTLE_CANCEL_POPUP_TITLE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_OPTION_DISMANTLE_CANCEL_POPUP_TITLE_DESC", false);
			}
		}

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x0600444A RID: 17482 RVA: 0x00151FA8 File Offset: 0x001501A8
		public static string GET_STRING_CONSORTIUM_OPTION_DISMANTLE_CANCEL_POPUP_BODY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_OPTION_DISMANTLE_CANCEL_POPUP_BODY_DESC", false);
			}
		}

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x0600444B RID: 17483 RVA: 0x00151FB5 File Offset: 0x001501B5
		public static string GET_STRING_CONSORTIUM_OPTION_DISMANTLE_CANCEL_POPUP_CONFIRM_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_OPTION_DISMANTLE_CANCEL_POPUP_CONFIRM_DESC", false);
			}
		}

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x0600444C RID: 17484 RVA: 0x00151FC2 File Offset: 0x001501C2
		public static string GET_STRING_CONSORTIUM_OPTION_DISMANTLE_INFORMATION_BTN_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_OPTION_DISMANTLE_INFORMATION_BTN_TEXT", false);
			}
		}

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x0600444D RID: 17485 RVA: 0x00151FCF File Offset: 0x001501CF
		public static string GET_STRING_CONSORTIUM_OPTION_DISMANTLE_HANDOVER_POPUP_TITLE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_OPTION_DISMANTLE_HANDOVER_POPUP_TITLE_DESC", false);
			}
		}

		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x0600444E RID: 17486 RVA: 0x00151FDC File Offset: 0x001501DC
		public static string GET_STRING_CONSORTIUM_OPTION_DISMANTLE_HANDOVER_POPUP_BODY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_OPTION_DISMANTLE_HANDOVER_POPUP_BODY_DESC", false);
			}
		}

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x0600444F RID: 17487 RVA: 0x00151FE9 File Offset: 0x001501E9
		public static string GET_STRING_CONSORTIUM_OPTION_DISMANTLE_HANDOVER_CONFIRM_POPUP_TITLE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_OPTION_DISMANTLE_HANDOVER_CONFIRM_POPUP_TITLE_DESC", false);
			}
		}

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x06004450 RID: 17488 RVA: 0x00151FF6 File Offset: 0x001501F6
		public static string GET_STRING_CONSORTIUM_OPTION_DISMANTLE_HANDOVER_CONFIRM_POPUP_BODY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_OPTION_DISMANTLE_HANDOVER_CONFIRM_POPUP_BODY_DESC", false);
			}
		}

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x06004451 RID: 17489 RVA: 0x00152003 File Offset: 0x00150203
		public static string GET_STRING_CONSORTIUM_MEMBER_GRADE_HANDOVER_CONFIRM_POPUP_TITLE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_MEMBER_GRADE_HANDOVER_CONFIRM_POPUP_TITLE_DESC", false);
			}
		}

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x06004452 RID: 17490 RVA: 0x00152010 File Offset: 0x00150210
		public static string GET_STRING_CONSORTIUM_MEMBER_GRADE_HANDOVER_CONFIRM_POPUP_BODY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_MEMBER_GRADE_HANDOVER_CONFIRM_POPUP_BODY_DESC", false);
			}
		}

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x06004453 RID: 17491 RVA: 0x0015201D File Offset: 0x0015021D
		public static string GET_STRING_CONSORTIUM_MEMBER_EXIT_CONFIRM_POPUP_TITLE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_MEMBER_EXIT_CONFIRM_POPUP_TITLE_DESC", false);
			}
		}

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x06004454 RID: 17492 RVA: 0x0015202A File Offset: 0x0015022A
		public static string GET_STRING_CONSORTIUM_MEMBER_EXIT_CONFIRM_POPUP_BODY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_MEMBER_EXIT_CONFIRM_POPUP_BODY_DESC", false);
			}
		}

		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x06004455 RID: 17493 RVA: 0x00152037 File Offset: 0x00150237
		public static string GET_STRING_CONSORTIUM_MEMBER_FORCE_EXIT_CONFIRM_POPUP_TITLE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_MEMBER_FORCE_EXIT_CONFIRM_POPUP_TITLE_DESC", false);
			}
		}

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x06004456 RID: 17494 RVA: 0x00152044 File Offset: 0x00150244
		public static string GET_STRING_CONSORTIUM_MEMBER_FORCE_EXIT_CONFIRM_POPUP_BODY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_MEMBER_FORCE_EXIT_CONFIRM_POPUP_BODY_DESC", false);
			}
		}

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x06004457 RID: 17495 RVA: 0x00152051 File Offset: 0x00150251
		public static string GET_STRING_CONSORTIUM_POPUP_INVITE_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_POPUP_INVITE_TITLE", false);
			}
		}

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x06004458 RID: 17496 RVA: 0x0015205E File Offset: 0x0015025E
		public static string GET_STRING_CONSORTIUM_INVITE_SEND_POPUP_BODY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_INVITE_SEND_POPUP_BODY_DESC", false);
			}
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x06004459 RID: 17497 RVA: 0x0015206B File Offset: 0x0015026B
		public static string GET_STRING_CONSORTIUM_INVITE_SEND_SUCCESS_BODY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_INVITE_SEND_SUCCESS_BODY_DESC", false);
			}
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x0600445A RID: 17498 RVA: 0x00152078 File Offset: 0x00150278
		public static string GET_STRING_CONSORTIUM_INVITE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_INVITE", false);
			}
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x0600445B RID: 17499 RVA: 0x00152085 File Offset: 0x00150285
		public static string GET_STRING_CONSORTIUM_JOIN_INVITE_JOIN_AGREE_POPUP_TITLE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_JOIN_INVITE_JOIN_AGREE_POPUP_TITLE_DESC", false);
			}
		}

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x0600445C RID: 17500 RVA: 0x00152092 File Offset: 0x00150292
		public static string GET_STRING_CONSORTIUM_INVITE_JOIN_REJECT_POPUP_TITLE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_INVITE_JOIN_REJECT_POPUP_TITLE_DESC", false);
			}
		}

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x0600445D RID: 17501 RVA: 0x0015209F File Offset: 0x0015029F
		public static string GET_STRING_CONSORTIUM_INVITE_JOIN_REJECT_POPUP_BODY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_INVITE_JOIN_REJECT_POPUP_BODY_DESC", false);
			}
		}

		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x0600445E RID: 17502 RVA: 0x001520AC File Offset: 0x001502AC
		public static string GET_STRING_CONSORTIUM_MEMBER_INTRODUCE_WRITE_POPUP_TITLE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_MEMBER_INTRODUCE_WRITE_POPUP_TITLE_DESC", false);
			}
		}

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x0600445F RID: 17503 RVA: 0x001520B9 File Offset: 0x001502B9
		public static string GET_STRING_CONSORTIUM_MEMBER_INTRODUCE_WRITE_POPUP_BODY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_MEMBER_INTRODUCE_WRITE_POPUP_BODY_DESC", false);
			}
		}

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x06004460 RID: 17504 RVA: 0x001520C6 File Offset: 0x001502C6
		public static string GET_STRING_CONSORTIUM_MEMBER_INTRODUCE_WRITE_POPUP_GUIDE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_MEMBER_INTRODUCE_WRITE_POPUP_GUIDE_DESC", false);
			}
		}

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x06004461 RID: 17505 RVA: 0x001520D3 File Offset: 0x001502D3
		public static string GET_STRING_CONSORTIUM_OVERLAY_MESSAGE_HEAD_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_OVERLAY_MESSAGE_HEAD_DESC", false);
			}
		}

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x06004462 RID: 17506 RVA: 0x001520E0 File Offset: 0x001502E0
		public static string GET_STRING_CONSORTIUM_OVERLAY_MESSAGE_BODY_JOIN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_OVERLAY_MESSAGE_BODY_JOIN", false);
			}
		}

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x06004463 RID: 17507 RVA: 0x001520ED File Offset: 0x001502ED
		public static string GET_STRING_CONSORTIUM_OVERLAY_MESSAGE_BODY_LEVEL_UP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_OVERLAY_MESSAGE_BODY_LEVEL_UP", false);
			}
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x06004464 RID: 17508 RVA: 0x001520FA File Offset: 0x001502FA
		public static string GET_STRING_CONSORTIUM_ATTENDANCE_SUCCESS_TOAST_MESSAGE_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_ATTENDANCE_SUCCESS_TOAST_MESSAGE_TEXT", false);
			}
		}

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x06004465 RID: 17509 RVA: 0x00152107 File Offset: 0x00150307
		public static string GET_STRING_CONSORTIUM_MEMBER_INTRODUCE_WRITE_SUCCESS_TOAST_MESSAGE_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_MEMBER_INTRODUCE_WRITE_SUCCESS_TOAST_MESSAGE_TEXT", false);
			}
		}

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x06004466 RID: 17510 RVA: 0x00152114 File Offset: 0x00150314
		public static string GET_STRING_CONSORTIUM_MEMBER_CHANGE_PERMISSION_TOAST_MESSAGE_TITLE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_MEMBER_CHANGE_PERMISSION_TOAST_MESSAGE_TITLE_DESC", false);
			}
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x06004467 RID: 17511 RVA: 0x00152121 File Offset: 0x00150321
		public static string GET_STRING_CONSORTIUM_MEMBER_GRADE_UP_INFORMATION_POPUP_BODY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_MEMBER_GRADE_UP_INFORMATION_POPUP_BODY_DESC", false);
			}
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x06004468 RID: 17512 RVA: 0x0015212E File Offset: 0x0015032E
		public static string GET_STRING_CONSORTIUM_MEMBER_FRADE_DOWN_INFORMATION_POPUP_BODY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_MEMBER_FRADE_DOWN_INFORMATION_POPUP_BODY_DESC", false);
			}
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x06004469 RID: 17513 RVA: 0x0015213B File Offset: 0x0015033B
		public static string GET_STRING_CONSORTIUM_MEMBER_CHANGE_MASTER_TOAST_MESSAGE_TITLE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_MEMBER_CHANGE_MASTER_TOAST_MESSAGE_TITLE_DESC", false);
			}
		}

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x0600446A RID: 17514 RVA: 0x00152148 File Offset: 0x00150348
		public static string GET_STRING_CONSORTIUM_MEMBER_GRADE_HANDOVER_INFORMATION_POPUP_BODY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_MEMBER_GRADE_HANDOVER_INFORMATION_POPUP_BODY_DESC", false);
			}
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x0600446B RID: 17515 RVA: 0x00152155 File Offset: 0x00150355
		public static string GET_STRING_CONSORTIUM_MEMBER_FORCE_EXIT_TOAST_MESSAGE_TITLE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_MEMBER_FORCE_EXIT_TOAST_MESSAGE_TITLE_DESC", false);
			}
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x0600446C RID: 17516 RVA: 0x00152162 File Offset: 0x00150362
		public static string GET_STRING_CONSORTIUM_MEMBER_FORCE_EXIT_INFORMATION_POPUP_BODY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_MEMBER_FORCE_EXIT_INFORMATION_POPUP_BODY_DESC", false);
			}
		}

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x0600446D RID: 17517 RVA: 0x0015216F File Offset: 0x0015036F
		public static string GET_STRING_CONFORTIUM_FAIL_GUILD_NOT_BELONG_AT_PRESENT_POPUP_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONFORTIUM_FAIL_GUILD_NOT_BELONG_AT_PRESENT_POPUP_TEXT", false);
			}
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x0600446E RID: 17518 RVA: 0x0015217C File Offset: 0x0015037C
		public static string GET_STRING_CONSORTIUM_MEMBER_SORT_LIST_GRADE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_MEMBER_SORT_LIST_GRADE", false);
			}
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x0600446F RID: 17519 RVA: 0x00152189 File Offset: 0x00150389
		public static string GET_STRING_CONSORTIUM_MEMBER_SORT_LIST_SCORE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_MEMBER_SORT_LIST_SCORE", false);
			}
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x06004470 RID: 17520 RVA: 0x00152196 File Offset: 0x00150396
		public static string GET_STRING_CONSORTIUM_MEMBER_SORT_LIST_SCORE_ALL
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_MEMBER_SORT_LIST_SCORE_ALL", false);
			}
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x06004471 RID: 17521 RVA: 0x001521A3 File Offset: 0x001503A3
		public static string GET_STRING_CONSORTIUM_POPUP_DONATION_COMFIRM_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_POPUP_DONATION_COMFIRM_TITLE", false);
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x06004472 RID: 17522 RVA: 0x001521B0 File Offset: 0x001503B0
		public static string GET_STRING_CONSORTIUM_POPUP_DONATION_COMFIRM_BODY
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_POPUP_DONATION_COMFIRM_BODY", false);
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x06004473 RID: 17523 RVA: 0x001521BD File Offset: 0x001503BD
		public static string GET_STRING_CONSORTIUM_DONATION_SUCCESS_TOAST_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_DONATION_SUCCESS_TOAST_TEXT", false);
			}
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x06004474 RID: 17524 RVA: 0x001521CA File Offset: 0x001503CA
		public static string GET_STRING_CONSORTIUM_WELFARE_PERSONAL_CONFIRM_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_WELFARE_PERSONAL_CONFIRM_TITLE", false);
			}
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x06004475 RID: 17525 RVA: 0x001521D7 File Offset: 0x001503D7
		public static string GET_STRING_CONSORTIUM_WELFARE_SUBTAB_PERSONAL_CONFIRM_BODY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_WELFARE_SUBTAB_PERSONAL_CONFIRM_BODY", false);
			}
		}

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x06004476 RID: 17526 RVA: 0x001521E4 File Offset: 0x001503E4
		public static string GET_STRING_CONSORTIUM_WELFARE_GUILD_CONFIRM_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_WELFARE_GUILD_CONFIRM_TITLE", false);
			}
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x06004477 RID: 17527 RVA: 0x001521F1 File Offset: 0x001503F1
		public static string GET_STRING_CONSORTIUM_WELFARE_SUBTAB_GUILD_CONFIRM_BODY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_WELFARE_SUBTAB_GUILD_CONFIRM_BODY", false);
			}
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06004478 RID: 17528 RVA: 0x001521FE File Offset: 0x001503FE
		public static string GET_STRING_CONSORTIUM_WELFARE_BUY_POINT_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_WELFARE_BUY_POINT_TITLE", false);
			}
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06004479 RID: 17529 RVA: 0x0015220B File Offset: 0x0015040B
		public static string GET_STRING_CONSORTIUM_WELFARE_BUY_POINT_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_WELFARE_BUY_POINT_DESC", false);
			}
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x0600447A RID: 17530 RVA: 0x00152218 File Offset: 0x00150418
		public static string GET_STRING_CONSORTIUM_POPUP_MISSION_REFRESH_CONFIRM_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_POPUP_MISSION_REFRESH_CONFIRM_TITLE", false);
			}
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x0600447B RID: 17531 RVA: 0x00152225 File Offset: 0x00150425
		public static string GET_STRING_CONSORTIUM_POPUP_MISSION_REFRESH_CONFIRM_BODY
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_POPUP_MISSION_REFRESH_CONFIRM_BODY", false);
			}
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x0600447C RID: 17532 RVA: 0x00152232 File Offset: 0x00150432
		public static string GET_STRING_CONSORTIUM_POPUP_DONATION_REFRESH_FREE_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_POPUP_DONATION_REFRESH_FREE_TEXT", false);
			}
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x0600447D RID: 17533 RVA: 0x0015223F File Offset: 0x0015043F
		public static string GET_STRING_CONSORTIUM_MISSION_REFRESH_PROGRESS_TITLE_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_MISSION_REFRESH_PROGRESS_TITLE_TEXT", false);
			}
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x0600447E RID: 17534 RVA: 0x0015224C File Offset: 0x0015044C
		public static string GET_STRING_CONSORTIUM_MISSION_REFRESH_PROGRESS_DESC_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_MISSION_REFRESH_PROGRESS_DESC_TEXT", false);
			}
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x0600447F RID: 17535 RVA: 0x00152259 File Offset: 0x00150459
		public static string GET_STRING_CONSORTIUM_RANKING_TOP_INFO_EXP
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_RANKING_TOP_INFO_EXP", false);
			}
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x06004480 RID: 17536 RVA: 0x00152266 File Offset: 0x00150466
		public static string GET_STRING_CONSORTIUM_RANKING_TOP_INFO_DAMAGE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_RANKING_TOP_INFO_DAMAGE", false);
			}
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x06004481 RID: 17537 RVA: 0x00152273 File Offset: 0x00150473
		public static string GET_STRING_CONSORTIUM_LOBBY_MESSAGE_IN_PROGRESS
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_LOBBY_MESSAGE_IN_PROGRESS", false);
			}
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x06004482 RID: 17538 RVA: 0x00152280 File Offset: 0x00150480
		public static string GET_STRING_CONSORTIUM_LOBBY_MESSAGE_RESULT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_LOBBY_MESSAGE_RESULT", false);
			}
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x06004483 RID: 17539 RVA: 0x0015228D File Offset: 0x0015048D
		public static string GET_STRING_CONSORTIUM_LOBBY_MESSAGE_CALCULATE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_LOBBY_MESSAGE_CALCULATE", false);
			}
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x06004484 RID: 17540 RVA: 0x0015229A File Offset: 0x0015049A
		public static string GET_STRING_CONSORTIUM_LOBBY_MESSAGE_NOT_IN_PROGRESS
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_LOBBY_MESSAGE_NOT_IN_PROGRESS", false);
			}
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06004485 RID: 17541 RVA: 0x001522A7 File Offset: 0x001504A7
		public static string GET_STRING_CONSORTIUM_LOBBY_MESSAGE_UNABLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_LOBBY_MESSAGE_UNABLE", false);
			}
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06004486 RID: 17542 RVA: 0x001522B4 File Offset: 0x001504B4
		public static string GET_STRING_CONSORTIUM_SEASON_OPEN_BEFORE_TOAST_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_SEASON_OPEN_BEFORE_TOAST_TEXT", false);
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06004487 RID: 17543 RVA: 0x001522C1 File Offset: 0x001504C1
		public static string GET_STRING_CONSORTIUM_TIME_OUT_ERROR_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_TIME_OUT_ERROR_TEXT", false);
			}
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06004488 RID: 17544 RVA: 0x001522CE File Offset: 0x001504CE
		public static string GET_STRING_CONSORTIUM_SESSION_ATTACK_ING_USER_INFO
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_SESSION_ATTACK_ING_USER_INFO", false);
			}
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06004489 RID: 17545 RVA: 0x001522DB File Offset: 0x001504DB
		public static string GET_STRING_CONSORTIUM_SESSION_PROGRESS_OF_ROUND_INFO
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_SESSION_PROGRESS_OF_ROUND_INFO", false);
			}
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x0600448A RID: 17546 RVA: 0x001522E8 File Offset: 0x001504E8
		public static string GET_STRING_CONSORTIUM_SESSION_PROGRESS_OF_ROUND_TASK_INFO
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_SESSION_PROGRESS_OF_ROUND_TASK_INFO", false);
			}
		}

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x0600448B RID: 17547 RVA: 0x001522F5 File Offset: 0x001504F5
		public static string GET_STRING_CONSORTIUM_COOP_FRONT_COUNT_ARENA
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_COOP_FRONT_COUNT_ARENA", false);
			}
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x0600448C RID: 17548 RVA: 0x00152302 File Offset: 0x00150502
		public static string GET_STRING_CONSORTIUM_COOP_FRONT_COUNT_RAID
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_COOP_FRONT_COUNT_RAID", false);
			}
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x0600448D RID: 17549 RVA: 0x0015230F File Offset: 0x0015050F
		public static string GET_STRING_CONSORTIUM_DUNGEON_DUNGEON_UI_ARENA_INFO
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_DUNGEON_DUNGEON_UI_ARENA_INFO", false);
			}
		}

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x0600448E RID: 17550 RVA: 0x0015231C File Offset: 0x0015051C
		public static string GET_STRING_CONSORTIUM_DUNGEON_ARTIFACT_DUNGEON_CHALLENGE_INFO
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_DUNGEON_ARTIFACT_DUNGEON_CHALLENGE_INFO", false);
			}
		}

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x0600448F RID: 17551 RVA: 0x00152329 File Offset: 0x00150529
		public static string GET_STRING_CONSORTIUM_DUNGEON_PLAY_COUNT_BUY_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_DUNGEON_PLAY_COUNT_BUY_TEXT", false);
			}
		}

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06004490 RID: 17552 RVA: 0x00152336 File Offset: 0x00150536
		public static string GET_STRING_CONSORTIUM_DUNGEON_PLAY_COUNT_BUY_SUCCESS_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_DUNGEON_PLAY_COUNT_BUY_SUCCESS_TEXT", false);
			}
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06004491 RID: 17553 RVA: 0x00152343 File Offset: 0x00150543
		public static string GET_STRING_CONSORTIUM_DUNGEON_ARTIFACT_DUNGEON_POPUP_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_DUNGEON_ARTIFACT_DUNGEON_POPUP_TITLE", false);
			}
		}

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06004492 RID: 17554 RVA: 0x00152350 File Offset: 0x00150550
		public static string GET_STRING_CONSORTIUM_DUNGEON_RAID_UI_LEVEL_INFO
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_DUNGEON_RAID_UI_LEVEL_INFO", false);
			}
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06004493 RID: 17555 RVA: 0x0015235D File Offset: 0x0015055D
		public static string GET_STRING_CONSORTIUM_DUNGEON_RESULT_BOSS_LEVEL_INFO
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_DUNGEON_RESULT_BOSS_LEVEL_INFO", false);
			}
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06004494 RID: 17556 RVA: 0x0015236A File Offset: 0x0015056A
		public static string GET_STRING_POPUP_CONSORTIUM_COOP_SESSION_END_SUB_01_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_POPUP_CONSORTIUM_COOP_SESSION_END_SUB_01_TEXT", false);
			}
		}

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06004495 RID: 17557 RVA: 0x00152377 File Offset: 0x00150577
		public static string GET_STRING_POPUP_CONSORTIUM_COOP_SESSION_END_SUB_02_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_POPUP_CONSORTIUM_COOP_SESSION_END_SUB_02_TEXT", false);
			}
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06004496 RID: 17558 RVA: 0x00152384 File Offset: 0x00150584
		public static string GET_STRING_CONSORTIUM_SESSION_END_INFORMATION_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_SESSION_END_INFORMATION_TEXT", false);
			}
		}

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06004497 RID: 17559 RVA: 0x00152391 File Offset: 0x00150591
		public static string GET_STRING_POPUP_CONSORTIUM_COOP_END_SUB_01_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_POPUP_CONSORTIUM_COOP_END_SUB_01_TEXT", false);
			}
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06004498 RID: 17560 RVA: 0x0015239E File Offset: 0x0015059E
		public static string GET_STRING_POPUP_CONSORTIUM_COOP_END_SUB_02_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_POPUP_CONSORTIUM_COOP_END_SUB_02_TEXT", false);
			}
		}

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06004499 RID: 17561 RVA: 0x001523AB File Offset: 0x001505AB
		public static string GET_STRING_CONSORTIUM_SEASION_END_INFORMATION_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_SEASION_END_INFORMATION_TEXT", false);
			}
		}

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x0600449A RID: 17562 RVA: 0x001523B8 File Offset: 0x001505B8
		public static string GET_STRING_POPUP_CONSORTIUM_COOP_RESULT_TITLE01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_POPUP_CONSORTIUM_COOP_RESULT_TITLE01", false);
			}
		}

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x0600449B RID: 17563 RVA: 0x001523C5 File Offset: 0x001505C5
		public static string GET_STRING_POPUP_CONSORTIUM_COOP_RESULT_TITLE02
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_POPUP_CONSORTIUM_COOP_RESULT_TITLE02", false);
			}
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x0600449C RID: 17564 RVA: 0x001523D2 File Offset: 0x001505D2
		public static string GET_STRING_CONSORTIUM_DUNGEON_RESULT_BOSS_SUMMARY_INFO
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_DUNGEON_RESULT_BOSS_SUMMARY_INFO", false);
			}
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x0600449D RID: 17565 RVA: 0x001523DF File Offset: 0x001505DF
		public static string GET_STRING_CONSORTIUM_DUNGEON_RESULT_BOSS_SUMMARY_INFO_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_DUNGEON_RESULT_BOSS_SUMMARY_INFO_FAIL", false);
			}
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x0600449E RID: 17566 RVA: 0x001523EC File Offset: 0x001505EC
		public static string GET_STRING_CONSORTIUM_DUNGEON_MENU_SEASON_REWARD_KILL_SCORE_STATUS
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_DUNGEON_MENU_SEASON_REWARD_KILL_SCORE_STATUS", false);
			}
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x0600449F RID: 17567 RVA: 0x001523F9 File Offset: 0x001505F9
		public static string GET_STRING_CONSORTIUM_DUNGEON_MENU_SEASON_REWARD_PARTICIPATION_SCORE_STATUS
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_DUNGEON_MENU_SEASON_REWARD_PARTICIPATION_SCORE_STATUS", false);
			}
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x060044A0 RID: 17568 RVA: 0x00152406 File Offset: 0x00150606
		public static string GET_STRING_CONSORTIUM_CHAT_ACCUMELATED_RECEIPT_REPORT_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_CHAT_ACCUMELATED_RECEIPT_REPORT_TEXT", false);
			}
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x060044A1 RID: 17569 RVA: 0x00152413 File Offset: 0x00150613
		public static string GET_STRING_CONSORTIUM_CHAT_REPORT_POPUP_TITLE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_CHAT_REPORT_POPUP_TITLE_DESC", false);
			}
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x060044A2 RID: 17570 RVA: 0x00152420 File Offset: 0x00150620
		public static string GET_STRING_CONSORTIUM_CHAT_REPORT_POPUP_BODY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_CHAT_REPORT_POPUP_TITLE_DESC", false);
			}
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x060044A3 RID: 17571 RVA: 0x0015242D File Offset: 0x0015062D
		public static string GET_STRING_CONSORTIUM_CHAT_REPORT_CONFIRM_POPUP_TITLE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_CHAT_REPORT_CONFIRM_POPUP_TITLE_DESC", false);
			}
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x060044A4 RID: 17572 RVA: 0x0015243A File Offset: 0x0015063A
		public static string GET_STRING_CONSORTIUM_CHAT_REPORT_CONFIRM_POPUP_BODY_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CONSORTIUM_CHAT_REPORT_CONFIRM_POPUP_BODY_DESC", false);
			}
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x060044A5 RID: 17573 RVA: 0x00152447 File Offset: 0x00150647
		public static string GET_STRING_CONSORTIUM_CHAT_INFORMATION_SANCTION_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_CHAT_INFORMATION_SANCTION_DESC", false);
			}
		}

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x060044A6 RID: 17574 RVA: 0x00152454 File Offset: 0x00150654
		public static string GET_STRING_CHAT_FRIEND_COUNT_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CHAT_FRIEND_COUNT_TEXT", false);
			}
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x060044A7 RID: 17575 RVA: 0x00152461 File Offset: 0x00150661
		public static string GET_STRING_CHAT_CONSORTIUM_MEMBER_COUNT_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CHAT_CONSORTIUM_MEMBER_COUNT_TEXT", false);
			}
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x060044A8 RID: 17576 RVA: 0x0015246E File Offset: 0x0015066E
		public static string GET_STRING_CHAT_CONSORTIUM_JOIN_REQ_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CHAT_CONSORTIUM_JOIN_REQ_DESC", false);
			}
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x060044A9 RID: 17577 RVA: 0x0015247B File Offset: 0x0015067B
		public static string GET_STRING_CHAT_BLOCKED
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_CHAT_BLOCKED", false);
			}
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x060044AA RID: 17578 RVA: 0x00152488 File Offset: 0x00150688
		public static string GET_STRING_OPTION_GAME_CHAT_NOTICE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPTION_GAME_CHAT_NOTICE", false);
			}
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x060044AB RID: 17579 RVA: 0x00152495 File Offset: 0x00150695
		public static string GET_SHADOW_PALACE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHADOW_PALACE_TITLE", false);
			}
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x060044AC RID: 17580 RVA: 0x001524A2 File Offset: 0x001506A2
		public static string GET_SHADOW_PALACE_NUMBER
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHADOW_PALACE_NAME", false);
			}
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x060044AD RID: 17581 RVA: 0x001524AF File Offset: 0x001506AF
		public static string GET_SHADOW_PALACE_START_CONFIRM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHADOW_PALACE_ENTER_POPUP_DESC", false);
			}
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x060044AE RID: 17582 RVA: 0x001524BC File Offset: 0x001506BC
		public static string GET_SHADOW_BATTLE_ENEMY_LEVEL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHADOW_PALACE_ENERMY_LEVEL", false);
			}
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x060044AF RID: 17583 RVA: 0x001524C9 File Offset: 0x001506C9
		public static string GET_SHADOW_BATTLE_CLEAR_NUM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHADOW_PALACE_ENERMY_CLEAR_TIME", false);
			}
		}

		// Token: 0x060044B0 RID: 17584 RVA: 0x001524D6 File Offset: 0x001506D6
		public static string GET_SHADOW_PALACE_GIVE_UP(params object[] param)
		{
			return NKCStringTable.GetString("SI_DP_SHADOW_PALACE_SURRENDER_POPUP_DESC", false, param);
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x060044B1 RID: 17585 RVA: 0x001524E4 File Offset: 0x001506E4
		public static string GET_SHADOW_RECORD_POPUP_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHADOW_PALACE_CLEAR_REPORT", false);
			}
		}

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x060044B2 RID: 17586 RVA: 0x001524F1 File Offset: 0x001506F1
		public static string GET_SHADOW_RECORD_POPUP_SLOT_NORMAL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHADOW_PALACE_CLEAR_ROOM_NUM", false);
			}
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x060044B3 RID: 17587 RVA: 0x001524FE File Offset: 0x001506FE
		public static string GET_SHADOW_RECORD_POPUP_SLOT_BOSS
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHADOW_PALACE_CLEAR_ROOM_BOSS", false);
			}
		}

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x060044B4 RID: 17588 RVA: 0x0015250B File Offset: 0x0015070B
		public static string GET_SHADOW_PALACE_REMAIN_TICKET
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHADOW_BUTTON_TICKET_VIEW", false);
			}
		}

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x060044B5 RID: 17589 RVA: 0x00152518 File Offset: 0x00150718
		public static string GET_SHADOW_PALACE_RESULT_GAME_TIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_WARFARE_RESULT_SHADOW_TIP1", false) + NKCStringTable.GetString("SI_DP_WARFARE_RESULT_SHADOW_TIP2", false);
			}
		}

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x060044B6 RID: 17590 RVA: 0x00152535 File Offset: 0x00150735
		public static string GET_STRING_SHADOW_SKIP_ERROR
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_SHADOW_SKIP_ERROR", false);
			}
		}

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x060044B7 RID: 17591 RVA: 0x00152542 File Offset: 0x00150742
		public static string GET_MULTIPLY_REWARD_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MULTIPLY_OPERATION_VALUE_1", false);
			}
		}

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x060044B8 RID: 17592 RVA: 0x0015254F File Offset: 0x0015074F
		public static string GET_MULTIPLY_REWARD_RESULT_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MULTIPLY_OPERATION_VALUE_2", false);
			}
		}

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x060044B9 RID: 17593 RVA: 0x0015255C File Offset: 0x0015075C
		public static string GET_MULTIPLY_OPERATION_MEDAL_COND
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MULTIPLY_OPERATION_MEDAL_COND", false);
			}
		}

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x060044BA RID: 17594 RVA: 0x00152569 File Offset: 0x00150769
		public static string GET_MULTIPLY_REWARD_COUNT_PARAM_02
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MULTIPLY_OPERATION_REWARD_COUNT", false);
			}
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x060044BB RID: 17595 RVA: 0x00152576 File Offset: 0x00150776
		public static string GET_STRING_POPUP_HAMBER_MENU_MISSION_TITLE_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_POPUP_HAMBER_MENU_MISSION_TITLE_DESC_01", false);
			}
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x060044BC RID: 17596 RVA: 0x00152583 File Offset: 0x00150783
		public static string GET_FRIEND_MENTORING_MENTEE_LIMIT_LEVEL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_MENTORING_MENTOR_UNLOCK_LEVEL", false);
			}
		}

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x060044BD RID: 17597 RVA: 0x00152590 File Offset: 0x00150790
		public static string GET_FRIEND_MENTORING_MENTEE_NOT_CLEAR_MENTEE_MESSION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_MENTORING_MENTOR_UNLOCK_MISSION", false);
			}
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x060044BE RID: 17598 RVA: 0x0015259D File Offset: 0x0015079D
		public static string GET_FRIEND_MENTORING_MENTEE_PROCESSING_MISSION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_MENTORING_MENTEE_PROCESSING_MISSION", false);
			}
		}

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x060044BF RID: 17599 RVA: 0x001525AA File Offset: 0x001507AA
		public static string GET_FRIEND_MENTORING_INVITE_REWARD_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_MENTORING_MENTOR_INVITE_REWARD_TITLE", false);
			}
		}

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x060044C0 RID: 17600 RVA: 0x001525B7 File Offset: 0x001507B7
		public static string GET_FRIEND_MENTORING_MENTEE_COUNT_DESC_02
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_MENTORING_MENTEE_COUNT", false);
			}
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x060044C1 RID: 17601 RVA: 0x001525C4 File Offset: 0x001507C4
		public static string GET_FRIEND_MENTORING_MENTEE_MISSION_NOT_COMPLETE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_MENTORING_MENTEE_MISSION_REQ", false);
			}
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x060044C2 RID: 17602 RVA: 0x001525D1 File Offset: 0x001507D1
		public static string GET_FRIEND_MENTORING_CAN_NOT_DELETE_MENTEE_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_MENTORING_CAN_NOT_DELETE_MENTEE", false);
			}
		}

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x060044C3 RID: 17603 RVA: 0x001525DE File Offset: 0x001507DE
		public static string GET_FRIEND_MENTORING_MENTEE_DELETE_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_MENTORING_POPUP_DELETE_MENTEE_TITLE", false);
			}
		}

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x060044C4 RID: 17604 RVA: 0x001525EB File Offset: 0x001507EB
		public static string GET_FRIEND_MENTORING_MENTEE_DELETE_DESC_02
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_MENTORING_POPUP_DELETE_MENTEE", false);
			}
		}

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x060044C5 RID: 17605 RVA: 0x001525F8 File Offset: 0x001507F8
		public static string GET_FRIEND_MENTORING_REGISTER_MENTOR_ACCEPT_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_MENTORING_REGISTER_MENTOR_ACCEPT_TITLE", false);
			}
		}

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x060044C6 RID: 17606 RVA: 0x00152605 File Offset: 0x00150805
		public static string GET_FRIEND_MENTORING_REGISTER_MENTOR_DISACCEPT_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_MENTORING_REGISTER_MENTOR_DISACCEPT_TITLE", false);
			}
		}

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x060044C7 RID: 17607 RVA: 0x00152612 File Offset: 0x00150812
		public static string GET_FRIEND_MENTORING_REGISTER_MENTOR_ACCEPT_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_MENTORING_REGISTER_MENTOR_ACCEPT_DESC_01", false);
			}
		}

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x060044C8 RID: 17608 RVA: 0x0015261F File Offset: 0x0015081F
		public static string GET_FRIEND_MENTORING_REGISTER_MENTOR_DISACCEPT_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_MENTORING_REGISTER_MENTOR_DISACCEPT_DESC_01", false);
			}
		}

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x060044C9 RID: 17609 RVA: 0x0015262C File Offset: 0x0015082C
		public static string GET_FRIEND_MENTORING_LIMIT_COUNT_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_MENTORING_REGISTER_MENTEE_LIMIT_COUNT_DESC_01", false);
			}
		}

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x060044CA RID: 17610 RVA: 0x00152639 File Offset: 0x00150839
		public static string GET_FRIEND_MENTORING_SEASON_END_CHECK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_MENTORING_SEASON_CALCULATE", false);
			}
		}

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x060044CB RID: 17611 RVA: 0x00152646 File Offset: 0x00150846
		public static string GET_FIERCE_ACTIVATE_SEASON_END
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_ACTIVATE_SEASON_END", false);
			}
		}

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x060044CC RID: 17612 RVA: 0x00152653 File Offset: 0x00150853
		public static string GET_FIERCE_ENTER_WAIT_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_ENTER_WAIT_DESC_01", false);
			}
		}

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x060044CD RID: 17613 RVA: 0x00152660 File Offset: 0x00150860
		public static string GET_FIERCE_CAN_NOT_ENTER_FIERCE_BATTLE_SUPPORT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_CAN_NOT_ENTER_FIERCE_BATTLE_SUPPORT", false);
			}
		}

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x060044CE RID: 17614 RVA: 0x0015266D File Offset: 0x0015086D
		public static string GET_FIERCE_CAN_NOT_ACCESS
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_CAN_NOT_ACCESS", false);
			}
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x060044CF RID: 17615 RVA: 0x0015267A File Offset: 0x0015087A
		public static string GET_FIERCE_WAIT_ACTIVATE_DAY_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_WAIT_ACTIVATE_DAY_DESC_01", false);
			}
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x060044D0 RID: 17616 RVA: 0x00152687 File Offset: 0x00150887
		public static string GET_FIERCE_WAIT_ACTIVATE_HOUR_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_WAIT_ACTIVATE_HOUR_DESC_01", false);
			}
		}

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x060044D1 RID: 17617 RVA: 0x00152694 File Offset: 0x00150894
		public static string GET_FIERCE_WAIT_ACTIVATE_MINUTE_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_WAIT_ACTIVATE_MINUTE_DESC_01", false);
			}
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x060044D2 RID: 17618 RVA: 0x001526A1 File Offset: 0x001508A1
		public static string GET_FIERCE_WAIT_ACTIVATE_SECOND_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_WAIT_ACTIVATE_SECOND_DESC_01", false);
			}
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x060044D3 RID: 17619 RVA: 0x001526AE File Offset: 0x001508AE
		public static string GET_FIERCE_WAIT_REWARD_DAY_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_WAIT_REWARD_DAY_DESC_01", false);
			}
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x060044D4 RID: 17620 RVA: 0x001526BB File Offset: 0x001508BB
		public static string GET_FIERCE_WAIT_REWARD_HOUR_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_WAIT_REWARD_HOUR_DESC_01", false);
			}
		}

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x060044D5 RID: 17621 RVA: 0x001526C8 File Offset: 0x001508C8
		public static string GET_FIERCE_WAIT_REWARD_MINUTE_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_WAIT_REWARD_MINUTE_DESC_01", false);
			}
		}

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x060044D6 RID: 17622 RVA: 0x001526D5 File Offset: 0x001508D5
		public static string GET_FIERCE_WAIT_REWARD_SECOND_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_WAIT_REWARD_SECOND_DESC_01", false);
			}
		}

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x060044D7 RID: 17623 RVA: 0x001526E2 File Offset: 0x001508E2
		public static string GET_FIERCE_WAIT_END_DAY_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_WAIT_END_DAY_DESC_01", false);
			}
		}

		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x060044D8 RID: 17624 RVA: 0x001526EF File Offset: 0x001508EF
		public static string GET_FIERCE_WAIT_END_HOUR_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_WAIT_END_HOUR_DESC_01", false);
			}
		}

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x060044D9 RID: 17625 RVA: 0x001526FC File Offset: 0x001508FC
		public static string GET_FIERCE_WAIT_END_MINUTE_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_WAIT_END_MINUTE_DESC_01", false);
			}
		}

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x060044DA RID: 17626 RVA: 0x00152709 File Offset: 0x00150909
		public static string GET_FIERCE_WAIT_END_SECOND_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_WAIT_END_SECOND_DESC_01", false);
			}
		}

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x060044DB RID: 17627 RVA: 0x00152716 File Offset: 0x00150916
		public static string GET_FIERCE_TIME_END_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_TIME_END_DESC_01", false);
			}
		}

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x060044DC RID: 17628 RVA: 0x00152723 File Offset: 0x00150923
		public static string GET_FIERCE_RANK_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_S2_FIERCE_RANK_DESC_01", false);
			}
		}

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x060044DD RID: 17629 RVA: 0x00152730 File Offset: 0x00150930
		public static string GET_FIERCE_RANK_IN_TOP_100_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_BATTLE_RANK_IN_DESC", false);
			}
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x060044DE RID: 17630 RVA: 0x0015273D File Offset: 0x0015093D
		public static string GET_FIERCE_ENTER_LIMIT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_S2_FIERCE_ENTER_LIMIT", false);
			}
		}

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x060044DF RID: 17631 RVA: 0x0015274A File Offset: 0x0015094A
		public static string GET_FIERCE_BATTLE_ENTER_LIMIT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_BATTLE_ENTER_LIMIT", false);
			}
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x060044E0 RID: 17632 RVA: 0x00152757 File Offset: 0x00150957
		public static string GET_FIERCE_BATTLE_RESET_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_BATTLE_RESET_FAIL", false);
			}
		}

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x060044E1 RID: 17633 RVA: 0x00152764 File Offset: 0x00150964
		public static string GET_FIERCE_BATTLE_POINT_REWARD_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_BATTLE_POINT_REWARD_TITLE", false);
			}
		}

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x060044E2 RID: 17634 RVA: 0x00152771 File Offset: 0x00150971
		public static string GET_FIERCE_BATTLE_GIVE_UP_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_BATTLE_GIVE_UP_POPUP", false);
			}
		}

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x060044E3 RID: 17635 RVA: 0x0015277E File Offset: 0x0015097E
		public static string GET_FIERCE_BATTLE_NOT_ACCESS
		{
			get
			{
				return NKCStringTable.GetString("SI_CONTENTS_UNLOCK_FIERCE", false);
			}
		}

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x060044E4 RID: 17636 RVA: 0x0015278B File Offset: 0x0015098B
		public static string GET_FIERCE_BATTLE_ENTER_SEASON_END
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_WAITING", false);
			}
		}

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x060044E5 RID: 17637 RVA: 0x00152798 File Offset: 0x00150998
		public static string GET_FIERCE_BATTLE_END_TEXT_NEW_RECORD
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_BATTLE_END_TEXT_NEW_RECORD", false);
			}
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x060044E6 RID: 17638 RVA: 0x001527A5 File Offset: 0x001509A5
		public static string GET_FIERCE_BATTLE_END_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_BATTLE_END_TEXT", false);
			}
		}

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x060044E7 RID: 17639 RVA: 0x001527B2 File Offset: 0x001509B2
		public static string GET_STRING_FIERCE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE", false);
			}
		}

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x060044E8 RID: 17640 RVA: 0x001527BF File Offset: 0x001509BF
		public static string GET_STRING_FIERCE_POPUP_SELF_PENALTY_BLOCK_TEXT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_S2_FIERCE_NO_PENALTY_TEXT", false);
			}
		}

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x060044E9 RID: 17641 RVA: 0x001527CC File Offset: 0x001509CC
		public static string GET_STRING_FIERCE_POPUP_SELF_PENALTY_NONE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_BATTLE_PENALTY_SELECT_INFO_TEXT", false);
			}
		}

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x060044EA RID: 17642 RVA: 0x001527D9 File Offset: 0x001509D9
		public static string GET_STRING_FIERCE_POPUP_SELF_PENALTY_BUFF
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_BATTLE_PENALTY_SELECT_BUFF", false);
			}
		}

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x060044EB RID: 17643 RVA: 0x001527E6 File Offset: 0x001509E6
		public static string GET_STRING_FIERCE_POPUP_SELF_PENALTY_DEBUFF
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_BATTLE_PENALTY_SELECT_DEBUFF", false);
			}
		}

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x060044EC RID: 17644 RVA: 0x001527F3 File Offset: 0x001509F3
		public static string GET_STRING_FIERCE_PENALTY_POINT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_BATTLE_PENALTY_INFO_TEXT", false);
			}
		}

		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x060044ED RID: 17645 RVA: 0x00152800 File Offset: 0x00150A00
		public static string GET_STRING_FIERCE_PENALTY_SCORE_PLUS
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_BATTLE_PENALTY_SCORE_PLUS", false);
			}
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x060044EE RID: 17646 RVA: 0x0015280D File Offset: 0x00150A0D
		public static string GET_STRING_FIERCE_PENALTY_SCORE_MINUS
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_BATTLE_PENALTY_SCORE_MINUS", false);
			}
		}

		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x060044EF RID: 17647 RVA: 0x0015281A File Offset: 0x00150A1A
		public static string GET_STRING_FIERCE_PENALTY_SCORE_PLUS_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_S2_FIERCE_BATTLE_PENALTY_INFO_PLUS_TEXT", false);
			}
		}

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x060044F0 RID: 17648 RVA: 0x00152827 File Offset: 0x00150A27
		public static string GET_STRING_FIERCE_PENALTY_SCORE_MINUS_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_BATTLE_PENALTY_INFO_MINUS_TEXT", false);
			}
		}

		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x060044F1 RID: 17649 RVA: 0x00152834 File Offset: 0x00150A34
		public static string GET_STRING_FIERCE_PENALTY_TITLE_BUFF
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_BATTLE_PENALTY_SUBTITLE_1", false);
			}
		}

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x060044F2 RID: 17650 RVA: 0x00152841 File Offset: 0x00150A41
		public static string GET_STRING_FIERCE_PENALTY_TITLE_DEBUFF
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_BATTLE_PENALTY_SUBTITLE_2", false);
			}
		}

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x060044F3 RID: 17651 RVA: 0x0015284E File Offset: 0x00150A4E
		public static string GET_STRING_FIERCE_PENALTY_BUFF_POINT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_BATTLE_PENALTY_SELECT_BUFF_POINT", false);
			}
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x060044F4 RID: 17652 RVA: 0x0015285B File Offset: 0x00150A5B
		public static string GET_STRING_FIERCE_PENALTY_DEBUFF_POINT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FIERCE_BATTLE_PENALTY_SELECT_DEBUFF_POINT", false);
			}
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x060044F5 RID: 17653 RVA: 0x00152868 File Offset: 0x00150A68
		public static string GET_STRING_OPERATOR_CONTRACT_STYLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_CONTRACT_STYLE", false);
			}
		}

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x060044F6 RID: 17654 RVA: 0x00152875 File Offset: 0x00150A75
		public static string GET_STRING_OPERATOR_SKILL_TRANSFER
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_SKILL_TRANSPORT", false);
			}
		}

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x060044F7 RID: 17655 RVA: 0x00152882 File Offset: 0x00150A82
		public static string GET_STRING_OPERATOR_INFO_MENU_NAME
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_INFO_MENU_NAME", false);
			}
		}

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x060044F8 RID: 17656 RVA: 0x0015288F File Offset: 0x00150A8F
		public static string GET_STRING_INVEITORY_OPERATOR_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_INVENTORY_OPERATOR_TITLE", false);
			}
		}

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x060044F9 RID: 17657 RVA: 0x0015289C File Offset: 0x00150A9C
		public static string GET_STRING_INVENTORY_OPERATOR_ADD_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_INVENTORY_OPERATOR_ADD_DESC", false);
			}
		}

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x060044FA RID: 17658 RVA: 0x001528A9 File Offset: 0x00150AA9
		public static string GET_STRING_INVENTORY_OPERATOR_ADD_FULL
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_INVENTORY_OPERATOR_ADD_FULL", false);
			}
		}

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x060044FB RID: 17659 RVA: 0x001528B6 File Offset: 0x00150AB6
		public static string GET_STRING_OPERATOR_PASSIVE_SKILL_TRANSFER_BLOCK
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_PASSIVE_SKILL_TRANSFER_BLOCK", false);
			}
		}

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x060044FC RID: 17660 RVA: 0x001528C3 File Offset: 0x00150AC3
		public static string GET_STRING_OPERATOR_SKILL_RESULT_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_SKILL_RESULT_TITLE", false);
			}
		}

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x060044FD RID: 17661 RVA: 0x001528D0 File Offset: 0x00150AD0
		public static string GET_STRING_OPERATOR_SKILL_POPUP
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_SKILL_POPUP", false);
			}
		}

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x060044FE RID: 17662 RVA: 0x001528DD File Offset: 0x00150ADD
		public static string GET_STRING_OPERATOR_MAIN_SKILL_SUCCESS
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_MAIN_SKILL_SUCCESS", false);
			}
		}

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x060044FF RID: 17663 RVA: 0x001528EA File Offset: 0x00150AEA
		public static string GET_STRING_OPERATOR_MAIN_SKILL_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_MAIN_SKILL_FAIL", false);
			}
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06004500 RID: 17664 RVA: 0x001528F7 File Offset: 0x00150AF7
		public static string GET_STRING_OPERATOR_PASSIVE_SKILL_SUCCESS
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_PASSIVE_SKILL_SUCCESS", false);
			}
		}

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06004501 RID: 17665 RVA: 0x00152904 File Offset: 0x00150B04
		public static string GET_STRING_OPERATOR_PASSIVE_SKILL_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_PASSIVE_SKILL_FAIL", false);
			}
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06004502 RID: 17666 RVA: 0x00152911 File Offset: 0x00150B11
		public static string GET_STRING_OPERATOR_PASSIVE_SKILL_IMPLANT_SUCCESS
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_PASSIVE_SKILL_IMPLANT_SUCCESS", false);
			}
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06004503 RID: 17667 RVA: 0x0015291E File Offset: 0x00150B1E
		public static string GET_STRING_OPERATOR_PASSIVE_SKILL_IMPLANT_FAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_PASSIVE_SKILL_IMPLANT_FAIL", false);
			}
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06004504 RID: 17668 RVA: 0x0015292B File Offset: 0x00150B2B
		public static string GET_STRING_OPERATOR_SKILL_INFO_POPUP_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_SKILL_INFO_POPUP_TITLE", false);
			}
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06004505 RID: 17669 RVA: 0x00152938 File Offset: 0x00150B38
		public static string GET_STRING_OPERATOR_TOOLTIP_ACTIVE_SKILL_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_TOOLTIP_ACTIVE_SKILL_TITLE", false);
			}
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06004506 RID: 17670 RVA: 0x00152945 File Offset: 0x00150B45
		public static string GET_STRING_OPERATOR_TOOLTIP_PASSIVE_SKILL_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_TOOLTIP_PASSIVE_SKILL_TITLE", false);
			}
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06004507 RID: 17671 RVA: 0x00152952 File Offset: 0x00150B52
		public static string GET_STRING_NO_EXIST_SELECTED_OPERATOR
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_NO_EXIST_SELECTED_OPERATOR", false);
			}
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06004508 RID: 17672 RVA: 0x0015295F File Offset: 0x00150B5F
		public static string GET_STRING_REMOVE_OPERATOR_WARNING
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_REMOVE_OPERATOR_WARNING", false);
			}
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06004509 RID: 17673 RVA: 0x0015296C File Offset: 0x00150B6C
		public static string GET_STRING_NO_EXIST_OPERATOR
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_NO_EXIST_OPERATOR", false);
			}
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x0600450A RID: 17674 RVA: 0x00152979 File Offset: 0x00150B79
		public static string GET_STRING_OPERATOR_REMOVE_CONFIRM_ONE_PARAM
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_REMOVE_CONFIRM_ONE_PARAM", false);
			}
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x0600450B RID: 17675 RVA: 0x00152986 File Offset: 0x00150B86
		public static string GET_STRING_OPERATOR_IMPLANT_BLOCK_LOCK_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_IMPLANT_BLOCK_LOCK_UNIT", false);
			}
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x0600450C RID: 17676 RVA: 0x00152993 File Offset: 0x00150B93
		public static string GET_STRING_OPERATOR_IMPLANT_BLOCK_NOT_POSSIBLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_IMPLANT_BLOCK_NOT_POSSIBLE", false);
			}
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x0600450D RID: 17677 RVA: 0x001529A0 File Offset: 0x00150BA0
		public static string GET_STRING_OPERATOR_IMPLANT_TRY_NOTHING
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_IMPLANT_TRY_NOTHING", false);
			}
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x0600450E RID: 17678 RVA: 0x001529AD File Offset: 0x00150BAD
		public static string GET_STRING_OPERATOR_NOT_ENOUGH_ITEM
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_NOT_ENOUGH_ITEM", false);
			}
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x0600450F RID: 17679 RVA: 0x001529BA File Offset: 0x00150BBA
		public static string GET_STRING_REMOVE_UNIT_NO_EXIST_OPERATOR
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_REMOVE_UNIT_NO_EXIST_OPERATOR", false);
			}
		}

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06004510 RID: 17680 RVA: 0x001529C7 File Offset: 0x00150BC7
		public static string GET_STRING_OPERATOR_POPUP_CHANGE_STAT_PLUS_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_POPUP_CHANGE_STAT_PLUS_DESC_01", false);
			}
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06004511 RID: 17681 RVA: 0x001529D4 File Offset: 0x00150BD4
		public static string GET_STRING_OPERATOR_POPUP_CHANGE_STAT_MINUS_DESC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_POPUP_CHANGE_STAT_MINUS_DESC_01", false);
			}
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06004512 RID: 17682 RVA: 0x001529E1 File Offset: 0x00150BE1
		public static string GET_STRING_OPERATOR_POPUP_SKILL_ENHACE_REJECT_DESC_MAIN_SKILL_MAX_LEVEL
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_POPUP_SKILL_ENHACE_REJECT_DESC_MAIN_SKILL_MAX_LEVEL", false);
			}
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06004513 RID: 17683 RVA: 0x001529EE File Offset: 0x00150BEE
		public static string GET_STRING_OPERATOR_POPUP_SKILL_ENHACE_REJECT_DESC_MAIN_SKILL_NOT_MATCH
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_POPUP_SKILL_ENHACE_REJECT_DESC_MAIN_SKILL_NOT_MATCH", false);
			}
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06004514 RID: 17684 RVA: 0x001529FB File Offset: 0x00150BFB
		public static string GET_STRING_OPERATOR_POPUP_SKILL_ENHACE_REJECT_DESC_SUB_SKILL_MAX_LEVEL
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_POPUP_SKILL_ENHACE_REJECT_DESC_SUB_SKILL_MAX_LEVEL", false);
			}
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06004515 RID: 17685 RVA: 0x00152A08 File Offset: 0x00150C08
		public static string GET_STRING_OPERATOR_POPUP_SKILL_ENHACE_REJECT_DESC_SUB_SKILL_NOT_MATCH
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_POPUP_SKILL_ENHACE_REJECT_DESC_SUB_SKILL_NOT_MATCH", false);
			}
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06004516 RID: 17686 RVA: 0x00152A15 File Offset: 0x00150C15
		public static string GET_STRING_OPERATOR_POPUP_SKILL_CONFIRM_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_POPUP_SKILL_CONFIRM_DESC", false);
			}
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06004517 RID: 17687 RVA: 0x00152A22 File Offset: 0x00150C22
		public static string GET_STRING_OPERATOR_SKILL_COOL_REDUCE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_INFO_STAT_NAME_SKILL", false);
			}
		}

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06004518 RID: 17688 RVA: 0x00152A2F File Offset: 0x00150C2F
		public static string GET_STRING_OPERATOR_CONFIRM_POPUP_TITLE_TRANSFER
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_SKILL_CONFIRM_POPUP_TITLE", false);
			}
		}

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06004519 RID: 17689 RVA: 0x00152A3C File Offset: 0x00150C3C
		public static string GET_STRING_OPERATOR_CONFIRM_POPUP_TITLE_SELECT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_CONFIRM_POPUP_TITLE_SELECT", false);
			}
		}

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x0600451A RID: 17690 RVA: 0x00152A49 File Offset: 0x00150C49
		public static string GET_STRING_OPERATOR_CONFIRM_POPUP_CONFIRM_WARNING
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_CONFIRM_POPUP_CONFIRM_WARNING", false);
			}
		}

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x0600451B RID: 17691 RVA: 0x00152A56 File Offset: 0x00150C56
		public static string GET_STRING_OPERATOR_CONFIRM_POPUP_CONFIRM_WARNING_UNIT_LEVEL
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_CONFIRM_POPUP_CONFIRM_WARNING_UNIT_LEVEL", false);
			}
		}

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x0600451C RID: 17692 RVA: 0x00152A63 File Offset: 0x00150C63
		public static string GET_STRING_OPERATOR_CONFIRM_POPUP_CONFIRM_WARNING_UNIT_ACTIVE_SKILL_LEVEL
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_CONFIRM_POPUP_CONFIRM_WARNING_ACTIVE_SKILL_LEVEL", false);
			}
		}

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x0600451D RID: 17693 RVA: 0x00152A70 File Offset: 0x00150C70
		public static string GET_STRING_OPERATOR_CONFIRM_POPUP_CONFIRM_WARNING_UNIT_PASSIVE_SKILL_LEVEL
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_OPERATOR_CONFIRM_POPUP_CONFIRM_WARNING_PASSIVE_SKILL_LEVEL", false);
			}
		}

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x0600451E RID: 17694 RVA: 0x00152A7D File Offset: 0x00150C7D
		public static string GET_STRING_EVENTPASS_EVENT_PASS_MENU_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_LOBBY_SUB_BTNS_EVENT_PASS", false);
			}
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x0600451F RID: 17695 RVA: 0x00152A8A File Offset: 0x00150C8A
		public static string GET_STRING_EVENTPASS_END_TIME_REMAIN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_PASS_END_TIME_REMAIN", false);
			}
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06004520 RID: 17696 RVA: 0x00152A97 File Offset: 0x00150C97
		public static string GET_STRING_EVENTPASS_END_TIME_ALMOST_END
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_PASS_END_TIME_ALMOST_END", false);
			}
		}

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x06004521 RID: 17697 RVA: 0x00152AA4 File Offset: 0x00150CA4
		public static string GET_STRING_EVENTPASS_EXP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_PASS_EXP", false);
			}
		}

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x06004522 RID: 17698 RVA: 0x00152AB1 File Offset: 0x00150CB1
		public static string GET_STRING_EVENTPASS_MAX_PASS_LEVEL_FINAL_REWARD
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_PASS_LEVEL_FINAL_REWARD", false);
			}
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06004523 RID: 17699 RVA: 0x00152ABE File Offset: 0x00150CBE
		public static string GET_STRING_EVENTPASS_ELAPSED_WEEK
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_PASS_ELAPSED_WEEK", false);
			}
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06004524 RID: 17700 RVA: 0x00152ACB File Offset: 0x00150CCB
		public static string GET_STRING_EVENTPASS_UPDATE_TIME_LEFT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_PASS_UPDATE_TIME_LEFT", false);
			}
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06004525 RID: 17701 RVA: 0x00152AD8 File Offset: 0x00150CD8
		public static string GET_STRING_EVENTPASS_DAILY_MISSION_ACHIEVE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_PASS_DAILY_MISSION_ACHIEVE", false);
			}
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06004526 RID: 17702 RVA: 0x00152AE5 File Offset: 0x00150CE5
		public static string GET_STRING_EVENTPASS_WEEKLY_MISSION_ACHIEVE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_PASS_WEEKLY_MISSION_ACHIEVE", false);
			}
		}

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06004527 RID: 17703 RVA: 0x00152AF2 File Offset: 0x00150CF2
		public static string GET_STRING_EVENTPASS_PASS_LEVEL_UP_NOTICE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_PASS_LEVEL_UP_NOTICE", false);
			}
		}

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06004528 RID: 17704 RVA: 0x00152AFF File Offset: 0x00150CFF
		public static string GET_STRING_EVENTPASS_PASS_LEVEL_UP_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_PASS_LEVEL_UP_DESC", false);
			}
		}

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06004529 RID: 17705 RVA: 0x00152B0C File Offset: 0x00150D0C
		public static string GET_STRING_EVENTPASS_CORE_PASS_PLUS_PURCHASE_EXP_LOSS
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_PASS_CORE_PASS_PLUS_PURCHASE_EXP_LOSS", false);
			}
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x0600452A RID: 17706 RVA: 0x00152B19 File Offset: 0x00150D19
		public static string GET_STRING_PURCHASE_REFUND_IMPOSSIBLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_PURCHASE_REFUND_IMPOSSIBLE", false);
			}
		}

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x0600452B RID: 17707 RVA: 0x00152B26 File Offset: 0x00150D26
		public static string GET_STRING_EVENTPASS_MISSION_REFRESH
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_PASS_MISSION_REFRESH", false);
			}
		}

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x0600452C RID: 17708 RVA: 0x00152B33 File Offset: 0x00150D33
		public static string GET_STRING_EVENTPASS_MISSION_REFRESH_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_PASS_MISSION_REFRESH_DESC", false);
			}
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x0600452D RID: 17709 RVA: 0x00152B40 File Offset: 0x00150D40
		public static string GET_STRING_EVENTPASS_MISSION_REFRESH_WARNING_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_PASS_MISSION_REFRESH_WARNING_DESC", false);
			}
		}

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x0600452E RID: 17710 RVA: 0x00152B4D File Offset: 0x00150D4D
		public static string GET_STRING_EVENTPASS_MISSION_REFRESH_FREECOUNT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_PASS_MISSION_REFRESH_FREECOUNT", false);
			}
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x0600452F RID: 17711 RVA: 0x00152B5A File Offset: 0x00150D5A
		public static string GET_STRING_EVENTPASS_MISSION_REFRESH_COUNT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_PASS_MISSION_REFRESH_COUNT", false);
			}
		}

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x06004530 RID: 17712 RVA: 0x00152B67 File Offset: 0x00150D67
		public static string GET_STRING_EVENTPASS_MISSION_REFRESH_MAX_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_PASS_MISSION_REFRESH_MAX_DESC", false);
			}
		}

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06004531 RID: 17713 RVA: 0x00152B74 File Offset: 0x00150D74
		public static string GET_STRING_EVENTPASS_MISSION_COMPLETE_DAILY_ALL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_PASS_MISSION_COMPLETE_DAILY_ALL", false);
			}
		}

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06004532 RID: 17714 RVA: 0x00152B81 File Offset: 0x00150D81
		public static string GET_STRING_EVENTPASS_MISSION_COMPLETE_WEEKLY_ALL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_PASS_MISSION_COMPLETE_WEEKLY_ALL", false);
			}
		}

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x06004533 RID: 17715 RVA: 0x00152B8E File Offset: 0x00150D8E
		public static string GET_STRING_EVENTPASS_REWARD_POSSIBLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_PASS_REWARD_POSSIBLE", false);
			}
		}

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x06004534 RID: 17716 RVA: 0x00152B9B File Offset: 0x00150D9B
		public static string GET_STRING_EVENTPASS_END
		{
			get
			{
				return NKCStringTable.GetString("SI_MENU_EXCEPTION_EVENT_EXPIRED_POPUP", false);
			}
		}

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x06004535 RID: 17717 RVA: 0x00152BA8 File Offset: 0x00150DA8
		public static string GET_STRING_EVENTPASS_MISSION_COMPLETE_DAILY_ALL_EX
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENTPASS_MISSION_COMPLETE_DAILY_ALL_EX", false);
			}
		}

		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x06004536 RID: 17718 RVA: 0x00152BB5 File Offset: 0x00150DB5
		public static string GET_STRING_EVENTPASS_MISSION_COMPLETE_WEEKLY_ALL_EX
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENTPASS_MISSION_COMPLETE_WEEKLY_ALL_EX", false);
			}
		}

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x06004537 RID: 17719 RVA: 0x00152BC2 File Offset: 0x00150DC2
		public static string GET_STRING_EVENTPASS_COREPASS_DISCOUNT_RATE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENTPASS_DISCOUNT_PRICE", false);
			}
		}

		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x06004538 RID: 17720 RVA: 0x00152BCF File Offset: 0x00150DCF
		public static string GET_STRING_EVENTPASS_MISSION_UPDATING_DAILY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_PASS_MISSION_UPDATING_DAILY", false);
			}
		}

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06004539 RID: 17721 RVA: 0x00152BDC File Offset: 0x00150DDC
		public static string GET_STRING_EVENTPASS_MISSION_UPDATING_WEEKLY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EVENT_PASS_MISSION_UPDATING_WEEKLY", false);
			}
		}

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x0600453A RID: 17722 RVA: 0x00152BE9 File Offset: 0x00150DE9
		public static string GET_STRING_EVENTPASS_LOBBY_MENU_LEFT_TIME_DAYS_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENTPASS_LOBBY_MENU_LEFT_TIME_DAYS_01", false);
			}
		}

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x0600453B RID: 17723 RVA: 0x00152BF6 File Offset: 0x00150DF6
		public static string GET_STRING_EVENTPASS_LOBBY_MENU_LEFT_TIME_HOUR_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENTPASS_LOBBY_MENU_LEFT_TIME_HOUR_01", false);
			}
		}

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x0600453C RID: 17724 RVA: 0x00152C03 File Offset: 0x00150E03
		public static string GET_STRING_EVENTPASS_LOBBY_MENU_LEFT_TIME_MIN_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENTPASS_LOBBY_MENU_LEFT_TIME_MIN_01", false);
			}
		}

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x0600453D RID: 17725 RVA: 0x00152C10 File Offset: 0x00150E10
		public static string GET_STRING_EVENTPASS_LOBBY_MENU_LEFT_TIME_SEC_01
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENTPASS_LOBBY_MENU_LEFT_TIME_SEC_01", false);
			}
		}

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x0600453E RID: 17726 RVA: 0x00152C1D File Offset: 0x00150E1D
		public static string GET_STRING_EQUIP_PRESET_NAME
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EQUIP_PRESET_NAME", false);
			}
		}

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x0600453F RID: 17727 RVA: 0x00152C2A File Offset: 0x00150E2A
		public static string GET_STRING_EQUIP_PRESET_ADD_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EQUIP_PRESET_ADD", false);
			}
		}

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06004540 RID: 17728 RVA: 0x00152C37 File Offset: 0x00150E37
		public static string GET_STRING_EQUIP_PRESET_ADD_CONTENT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EQUIP_PRESET_ADD_DESC", false);
			}
		}

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x06004541 RID: 17729 RVA: 0x00152C44 File Offset: 0x00150E44
		public static string GET_STRING_EQUIP_PRESET_SAVE_CONTENT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EQUIP_PRESET_SAVE_DESC", false);
			}
		}

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x06004542 RID: 17730 RVA: 0x00152C51 File Offset: 0x00150E51
		public static string GET_STRING_EQUIP_PRESET_NONE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EQUIP_PRESET_NONE", false);
			}
		}

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06004543 RID: 17731 RVA: 0x00152C5E File Offset: 0x00150E5E
		public static string GET_STRING_EQUIP_PRESET_DIFFERENT_TYPE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EQUIP_PRESET_DIFFERENT_TYPE", false);
			}
		}

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x06004544 RID: 17732 RVA: 0x00152C6B File Offset: 0x00150E6B
		public static string GET_STRING_EQUIP_PRESET_APPLY_SLOT_LOCKED
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EQUIP_PRESET_APPLY_SLOT_LOCKED", false);
			}
		}

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x06004545 RID: 17733 RVA: 0x00152C78 File Offset: 0x00150E78
		public static string GET_STRING_EQUIP_PRESET_DIFFERENT_POSITION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EQUIP_PRESET_DIFFERENT_POSITION", false);
			}
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06004546 RID: 17734 RVA: 0x00152C85 File Offset: 0x00150E85
		public static string GET_STRING_EQUIP_PRESET_PRIVATE_EQUIP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EQUIP_PRESET_PRIVATE_EQUIP", false);
			}
		}

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06004547 RID: 17735 RVA: 0x00152C92 File Offset: 0x00150E92
		public static string GET_STRING_EQUIP_PRESET_SLOT_FULL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EQUIP_PRESET_SLOT_FULL", false);
			}
		}

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06004548 RID: 17736 RVA: 0x00152C9F File Offset: 0x00150E9F
		public static string GET_STRING_EQUIP_PRESET_UNIT_EQUIP_EMPTY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EQUIP_PRESET_UNIT_EQUIP_EMPTY", false);
			}
		}

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x06004549 RID: 17737 RVA: 0x00152CAC File Offset: 0x00150EAC
		public static string GET_STRING_EQUIP_PRESET_ORDER_SAVE_WARNING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EQUIP_PRESET_LIST_SAVE_DESC", false);
			}
		}

		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x0600454A RID: 17738 RVA: 0x00152CB9 File Offset: 0x00150EB9
		public static string GET_STRING_OFFICE_ALREADY_ASSIGNED_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OFFICE_ALREADY_ASSIGNED_UNIT", false);
			}
		}

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x0600454B RID: 17739 RVA: 0x00152CC6 File Offset: 0x00150EC6
		public static string GET_STRING_OFFICE_FULL_ASSIGNED
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OFFICE_FULL_ASSIGNED", false);
			}
		}

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x0600454C RID: 17740 RVA: 0x00152CD3 File Offset: 0x00150ED3
		public static string GET_STRING_OFFICE_ASSIGN_COMPLETE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OFFICE_ASSIGN_COMPLETE", false);
			}
		}

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x0600454D RID: 17741 RVA: 0x00152CE0 File Offset: 0x00150EE0
		public static string GET_STRING_OFFICE_ASSIGN_CANCEL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OFFICE_ASSIGN_CANCEL", false);
			}
		}

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x0600454E RID: 17742 RVA: 0x00152CED File Offset: 0x00150EED
		public static string GET_STRING_OFFICE_PURCHASE_SECTION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OFFICE_PURCHASE_SECTION", false);
			}
		}

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x0600454F RID: 17743 RVA: 0x00152CFA File Offset: 0x00150EFA
		public static string GET_STRING_OFFICE_PURCHASE_ROOM
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OFFICE_PURCHASE_ROOM", false);
			}
		}

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x06004550 RID: 17744 RVA: 0x00152D07 File Offset: 0x00150F07
		public static string GET_STRING_OFFICE_ROOM_IN_LOCKED_SECTION
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OFFICE_ROOM_IN_LOCKED_SECTION", false);
			}
		}

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06004551 RID: 17745 RVA: 0x00152D14 File Offset: 0x00150F14
		public static string GET_STRING_OFFICE_OPENED_DORMS_COUNT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OFFICE_OPENED_DORMS_COUNT", false);
			}
		}

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x06004552 RID: 17746 RVA: 0x00152D21 File Offset: 0x00150F21
		public static string GET_STRING_OFFICE_MINIMAP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OFFICE_MINIMAP", false);
			}
		}

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x06004553 RID: 17747 RVA: 0x00152D2E File Offset: 0x00150F2E
		public static string GET_STRING_OFFICE_DORMITORY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OFFICE_DOMITORY", false);
			}
		}

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06004554 RID: 17748 RVA: 0x00152D3B File Offset: 0x00150F3B
		public static string GET_STRING_OFFICE_ROOM_IN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OFFICE_ROOM_IN", false);
			}
		}

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06004555 RID: 17749 RVA: 0x00152D48 File Offset: 0x00150F48
		public static string GET_STRING_OFFICE_DECORATE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OFFICE_DECORATE", false);
			}
		}

		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x06004556 RID: 17750 RVA: 0x00152D55 File Offset: 0x00150F55
		public static string GET_STRING_OFFICE_FRIEND_CANNOT_VISIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OFFICE_FRIEND_CANNOT_VISIT", false);
			}
		}

		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x06004557 RID: 17751 RVA: 0x00152D62 File Offset: 0x00150F62
		public static string GET_STRING_OFFICE_FRIEND_NICKNAME
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OFFICE_FRIEND_NICKNAME", false);
			}
		}

		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x06004558 RID: 17752 RVA: 0x00152D6F File Offset: 0x00150F6F
		public static string GET_STRING_OFFICE_REQUEST_FRIEND
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OFFICE_REQUEST_FRIEND", false);
			}
		}

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x06004559 RID: 17753 RVA: 0x00152D7C File Offset: 0x00150F7C
		public static string GET_STRING_OFFICE_BIZCARD_SENDED_ALL
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_OFFICE_BIZCARD_SENDED_ALL", false);
			}
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x0600455A RID: 17754 RVA: 0x00152D89 File Offset: 0x00150F89
		public static string GET_STRING_TOOLTIP
		{
			get
			{
				return "툴팁";
			}
		}

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x0600455B RID: 17755 RVA: 0x00152D90 File Offset: 0x00150F90
		public static string GET_STRING_POPUP_ITEM_LACK
		{
			get
			{
				return "ItemLack";
			}
		}

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x0600455C RID: 17756 RVA: 0x00152D97 File Offset: 0x00150F97
		public static string GET_STRING_POPUP_ITEM_BOX
		{
			get
			{
				return "ItemBox";
			}
		}

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x0600455D RID: 17757 RVA: 0x00152D9E File Offset: 0x00150F9E
		public static string GET_STRING_POPUP_ITEM_EQUIP_BOX
		{
			get
			{
				return "ItemEquipBox";
			}
		}

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x0600455E RID: 17758 RVA: 0x00152DA5 File Offset: 0x00150FA5
		public static string GET_STRING_UNIT_INFO_DETAIL
		{
			get
			{
				return "UnitInfoDetail";
			}
		}

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x0600455F RID: 17759 RVA: 0x00152DAC File Offset: 0x00150FAC
		public static string GET_STRING_SKIN
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_GET_STRING_SKIN", false);
			}
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x06004560 RID: 17760 RVA: 0x00152DB9 File Offset: 0x00150FB9
		public static string GET_STRING_SKIN_GRADE_VARIATION
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_GET_STRING_SKIN_GRADE_VARIATION", false);
			}
		}

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06004561 RID: 17761 RVA: 0x00152DC6 File Offset: 0x00150FC6
		public static string GET_STRING_POPUP_SKILL_FULL_INFO
		{
			get
			{
				return "SkillInfo";
			}
		}

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06004562 RID: 17762 RVA: 0x00152DCD File Offset: 0x00150FCD
		public static string GET_STRING_MENU_NAME_OPERATION_INTRO
		{
			get
			{
				return "작전인트로";
			}
		}

		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x06004563 RID: 17763 RVA: 0x00152DD4 File Offset: 0x00150FD4
		public static string GET_STRING_WARFARE_RESULT
		{
			get
			{
				return "전역 결과";
			}
		}

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06004564 RID: 17764 RVA: 0x00152DDB File Offset: 0x00150FDB
		public static string GET_STRING_WARFARE_SELECT_SHIP_POPUP
		{
			get
			{
				return "WarfareSelectShipPopup";
			}
		}

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06004565 RID: 17765 RVA: 0x00152DE2 File Offset: 0x00150FE2
		public static string GET_STRING_LOGIN
		{
			get
			{
				return "로그인";
			}
		}

		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06004566 RID: 17766 RVA: 0x00152DE9 File Offset: 0x00150FE9
		public static string GET_STRING_FRIEND_INFO
		{
			get
			{
				return "FriendInfo";
			}
		}

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06004567 RID: 17767 RVA: 0x00152DF0 File Offset: 0x00150FF0
		public static string GET_STRING_MENU_NAME_WORLDMAP_EVENT
		{
			get
			{
				return "탐색현황";
			}
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06004568 RID: 17768 RVA: 0x00152DF7 File Offset: 0x00150FF7
		public static string GET_STRING_NEWS
		{
			get
			{
				return "뉴스";
			}
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06004569 RID: 17769 RVA: 0x00152DFE File Offset: 0x00150FFE
		public static string GET_STRING_CONTRACT_BUNDLE
		{
			get
			{
				return "일괄 채용";
			}
		}

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x0600456A RID: 17770 RVA: 0x00152E05 File Offset: 0x00151005
		public static string GET_STRING_SKILL_TRAIN_POPUP
		{
			get
			{
				return "스킬 훈련 메뉴";
			}
		}

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x0600456B RID: 17771 RVA: 0x00152E0C File Offset: 0x0015100C
		public static string GET_STRING_SLOT_VIEWR
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_GET_STRING_SLOT_VIEWR", false);
			}
		}

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x0600456C RID: 17772 RVA: 0x00152E19 File Offset: 0x00151019
		public static string GET_STRING_RESULT
		{
			get
			{
				return "결과창";
			}
		}

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x0600456D RID: 17773 RVA: 0x00152E20 File Offset: 0x00151020
		public static string GET_STRING_PRIVATE_PVP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_FRIEND_PVP", false);
			}
		}

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x0600456E RID: 17774 RVA: 0x00152E2D File Offset: 0x0015102D
		public static string GET_STRING_PRIVATE_PVP_INVITE_REQ
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_POPUP_NOTICE_FRIENDLY_MATCH_WAIT_ACCEPT", false);
			}
		}

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x0600456F RID: 17775 RVA: 0x00152E3A File Offset: 0x0015103A
		public static string GET_STRING_PRIVATE_PVP_INVITE_NOT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_POPUP_NOTICE_FRIENDLY_MATCH_INVITE_ARRIVE", false);
			}
		}

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06004570 RID: 17776 RVA: 0x00152E47 File Offset: 0x00151047
		public static string GET_STRING_PRIVATE_PVP_AUTO_CANCEL_ID
		{
			get
			{
				return "SI_DP_POPUP_NOTICE_FRIENDLY_MATCH_INVITE_REJECT";
			}
		}

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06004571 RID: 17777 RVA: 0x00152E4E File Offset: 0x0015104E
		public static string GET_STRING_PRIVATE_PVP_READY_CANCEL_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_GAUNTLET_FRIENDLY_BATTLE_CANCEL", false);
			}
		}

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06004572 RID: 17778 RVA: 0x00152E5B File Offset: 0x0015105B
		public static string GET_STRING_PRIVATE_PVP_READY_CANCEL
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_GAUNTLET_FRIENDLY_BATTLE_CANCEL_CHECK", false);
			}
		}

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06004573 RID: 17779 RVA: 0x00152E68 File Offset: 0x00151068
		public static string GET_STRING_PRIVATE_PVP_GUILD_MEMBER_EMPTY
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONSORTIUM_LIST_IS_EMPTY", false);
			}
		}

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06004574 RID: 17780 RVA: 0x00152E75 File Offset: 0x00151075
		public static string GET_STRING_GAUNTLET_OPEN_LEAGUE_MODE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_OPEN_LEAGUE_MODE", false);
			}
		}

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x06004575 RID: 17781 RVA: 0x00152E82 File Offset: 0x00151082
		public static string GET_STRING_GAUNTLET_NOT_OPEN_LEAGUE_MODE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_NOT_OPEN_LEAGUE_MODE", false);
			}
		}

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06004576 RID: 17782 RVA: 0x00152E8F File Offset: 0x0015108F
		public static string GET_STRING_GAUNTLET_LEAGUE_GAME
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_LEAGUEMATCH", false);
			}
		}

		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06004577 RID: 17783 RVA: 0x00152E9C File Offset: 0x0015109C
		public static string GET_STRING_GAUNTLET_LEAGUE_START_REQ_POPUP_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_LEAGUE_START_REQ_POPUP_DESC", false);
			}
		}

		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06004578 RID: 17784 RVA: 0x00152EA9 File Offset: 0x001510A9
		public static string GET_STRING_GAUNTLET_LEAGUE_START_REQ_SHIP_POPUP_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_LEAGUE_START_REQ_SHIP_POPUP_DESC", false);
			}
		}

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06004579 RID: 17785 RVA: 0x00152EB6 File Offset: 0x001510B6
		public static string GET_STRING_GAUNTLET_LEAGUE_START_REQ_UNIT_POPUP_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_LEAGUE_START_REQ_UNIT_POPUP_DESC", false);
			}
		}

		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x0600457A RID: 17786 RVA: 0x00152EC3 File Offset: 0x001510C3
		public static string GET_STRING_TOOLTIP_DEADLINE_BUFF_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_GAUNTLET_LEAGUE_TOOLTIP_DEADLINE_BUFF_TITLE", false);
			}
		}

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x0600457B RID: 17787 RVA: 0x00152ED0 File Offset: 0x001510D0
		public static string GET_STRING_RECALL_FINAL_CHECK_POPUP_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_RECALL_FINAL_CHECK_POPUP_DESC", false);
			}
		}

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x0600457C RID: 17788 RVA: 0x00152EDD File Offset: 0x001510DD
		public static string GET_STRING_RECALL_FINAL_CHECK_POPUP_DATE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_RECALL_FINAL_CHECK_POPUP_DATE", false);
			}
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x0600457D RID: 17789 RVA: 0x00152EEA File Offset: 0x001510EA
		public static string GET_STRING_RECALL_ERROR_ALT_USING_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_RECALL_ERROR_ALT_USING_UNIT", false);
			}
		}

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x0600457E RID: 17790 RVA: 0x00152EF7 File Offset: 0x001510F7
		public static string GET_STRING_RECALL_ERROR_ALT_UNIT_SELECT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_RECALL_ERROR_ALT_UNIT_SELECT", false);
			}
		}

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x0600457F RID: 17791 RVA: 0x00152F04 File Offset: 0x00151104
		public static string GET_STRING_RECALL_COMPLETE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_RECALL_COMPLETE", false);
			}
		}

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x06004580 RID: 17792 RVA: 0x00152F11 File Offset: 0x00151111
		public static string GET_STRING_RECALL_DESC_END_DATE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_RECALL_DESC_END_DATE", false);
			}
		}

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06004581 RID: 17793 RVA: 0x00152F1E File Offset: 0x0015111E
		public static string GET_STRING_KILLCOUNT_SERVER_REWARD_CURRENT_STEP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_KILLCOUNT_SERVER_REWARD_CURRENT_STEP", false);
			}
		}

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06004582 RID: 17794 RVA: 0x00152F2B File Offset: 0x0015112B
		public static string GET_STRING_KILLCOUNT_SERVER_REWARD_STEP
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_KILLCOUNT_SERVER_REWARD_STEP", false);
			}
		}

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06004583 RID: 17795 RVA: 0x00152F38 File Offset: 0x00151138
		public static string GET_STRING_KILLCOUNT_SERVER_REWARD_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_KILLCOUNT_SERVER_REWARD_DESC", false);
			}
		}

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06004584 RID: 17796 RVA: 0x00152F45 File Offset: 0x00151145
		public static string GET_STRING_KILLCOUNT_SERVER_REWARD_GET
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_KILLCOUNT_SERVER_REWARD_GET", false);
			}
		}

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06004585 RID: 17797 RVA: 0x00152F52 File Offset: 0x00151152
		public static string GET_STRING_REARM_EXTRACT_NOT_TARGET_UNIT
		{
			get
			{
				return NKCStringTable.GetString("NEC_FAIL_EXTRACT_UNIT_TARGET_LACK", false);
			}
		}

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x06004586 RID: 17798 RVA: 0x00152F5F File Offset: 0x0015115F
		public static string GET_STRING_REARM_EXTRACT_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_REARM_SHORTCUT_MENU_RECORD", false);
			}
		}

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06004587 RID: 17799 RVA: 0x00152F6C File Offset: 0x0015116C
		public static string GET_STRING_REARM_PROCESS_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_REARM_SHORTCUT_MENU_REARM", false);
			}
		}

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x06004588 RID: 17800 RVA: 0x00152F79 File Offset: 0x00151179
		public static string GET_STRING_REARM_EXTRACT_RESULT_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_REARM_REARM_EXTRACT_RESULT_TITLE", false);
			}
		}

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x06004589 RID: 17801 RVA: 0x00152F86 File Offset: 0x00151186
		public static string GET_STRING_REARM_CONFIRM_POPUP_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_REARM_REARM_CONFIRM_POPUP_TITLE", false);
			}
		}

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x0600458A RID: 17802 RVA: 0x00152F93 File Offset: 0x00151193
		public static string GET_STRING_REARM_CONFIRM_POPUP_FINAL_BOX_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_REARM_REARM_CONFIRM_POPUP_FINAL_BOX_DESC", false);
			}
		}

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x0600458B RID: 17803 RVA: 0x00152FA0 File Offset: 0x001511A0
		public static string GET_STRING_REARM_CONFIRM_POPIP_BOX_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_REARM_REARM_CONFIRM_POPIP_BOX_TITLE", false);
			}
		}

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x0600458C RID: 17804 RVA: 0x00152FAD File Offset: 0x001511AD
		public static string GET_STRING_REARM_EXTRACT_UNIT_LIMIT_UNDER_8
		{
			get
			{
				return NKCStringTable.GetString("NEC_FAIL_UNIT_REMOVE", false);
			}
		}

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x0600458D RID: 17805 RVA: 0x00152FBA File Offset: 0x001511BA
		public static string GET_STRING_REARM_EXTRACT_LACK_TARGET_UNIT_COUNT
		{
			get
			{
				return NKCStringTable.GetString("NEC_FAIL_EXTRACT_UNIT_LACK", false);
			}
		}

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x0600458E RID: 17806 RVA: 0x00152FC7 File Offset: 0x001511C7
		public static string GET_STRING_REARM_PROCESS_UNIT_SELECT_LIST_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_REARM_PROCESS_UNIT_SELECT_LIST_TITLE", false);
			}
		}

		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x0600458F RID: 17807 RVA: 0x00152FD4 File Offset: 0x001511D4
		public static string GET_STRING_REARM_EXTRACT_CONFIRM_POPUP_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_REARM_RECORD_CONFIRM_RESULT_TEXT_V2", false);
			}
		}

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x06004590 RID: 17808 RVA: 0x00152FE1 File Offset: 0x001511E1
		public static string GET_STRING_REARM_EXTRACT_CONFIRM_POPUP_SYNERGY_BONUS
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_REARM_RECORD_CONFIRM_SYNERGY_TEXT", false);
			}
		}

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x06004591 RID: 17809 RVA: 0x00152FEE File Offset: 0x001511EE
		public static string GET_STRING_REARM_EXTRACT_NOT_ACTIVE_SYNERGY_BOUNS
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_REARM_RECORD_CONFIRM_SYNERGY_DISABLE_TEXT", false);
			}
		}

		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x06004592 RID: 17810 RVA: 0x00152FFB File Offset: 0x001511FB
		public static string GET_STRING_REARM_RESULT_POPUP_SKILL_LEVEL_BEFORE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_REARM_RESULT_SKILL_BEFORE_LV_TEXT", false);
			}
		}

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x06004593 RID: 17811 RVA: 0x00153008 File Offset: 0x00151208
		public static string GET_STRING_REARM_RESULT_POPUP_SKILL_LEVEL_AFTER
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_REARM_RESULT_SKILL_AFTER_LV_TEXT", false);
			}
		}

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x06004594 RID: 17812 RVA: 0x00153015 File Offset: 0x00151215
		public static string GET_STRING_REARM_PROCESS_BLOCK_MESSAGE_EMPTY_TARGET_UNIT
		{
			get
			{
				return NKCStringTable.GetString("NEC_FAIL_REARM_UNIT_NOT", false);
			}
		}

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x06004595 RID: 17813 RVA: 0x00153022 File Offset: 0x00151222
		public static string GET_STRING_REARM_PROCESS_BLOCK_MESSAGE_LACK_COND
		{
			get
			{
				return NKCStringTable.GetString("NEC_FAIL_REARM_LACK_ITEM_LV", false);
			}
		}

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x06004596 RID: 17814 RVA: 0x0015302F File Offset: 0x0015122F
		public static string GET_STRING_REARM_PROCESS_BLOCK_MESSAGE_EQUIPED
		{
			get
			{
				return NKCStringTable.GetString("NEC_FAIL_REARM_UNIT_EQUIP", false);
			}
		}

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x06004597 RID: 17815 RVA: 0x0015303C File Offset: 0x0015123C
		public static string GET_STRING_FACTORY_UPGRADE_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FACTORY_UPGRADE_TITLE", false);
			}
		}

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x06004598 RID: 17816 RVA: 0x00153049 File Offset: 0x00151249
		public static string GET_STRING_EQUIP_OPTION_MAIN
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EQUIP_OPTION_MAIN", false);
			}
		}

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x06004599 RID: 17817 RVA: 0x00153056 File Offset: 0x00151256
		public static string GET_STRING_EQUIP_OPTION_1
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FQUIP_OPTION_1", false);
			}
		}

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x0600459A RID: 17818 RVA: 0x00153063 File Offset: 0x00151263
		public static string GET_STRING_EQUIP_OPTION_2
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EQUIP_OPTION_2", false);
			}
		}

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x0600459B RID: 17819 RVA: 0x00153070 File Offset: 0x00151270
		public static string GET_STRING_EQUIP_OPTION_SET
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EQUIP_OPTION_SET", false);
			}
		}

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x0600459C RID: 17820 RVA: 0x0015307D File Offset: 0x0015127D
		public static string GET_STRING_FACTORY_UPGRADE_MAIN_OPTION_TOOLTIP
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FACTORY_UPGRADE_MAIN_OPTION_TOOLTIP", false);
			}
		}

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x0600459D RID: 17821 RVA: 0x0015308A File Offset: 0x0015128A
		public static string GET_STRING_FACTORY_UPGRADE_MAIN_SUB_TOOLTIP
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FACTORY_UPGRADE_MAIN_SUB_TOOLTIP", false);
			}
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x0600459E RID: 17822 RVA: 0x00153097 File Offset: 0x00151297
		public static string GET_STRING_FACTORY_UPGRADE_CONFIRM_POPUP
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FACTORY_UPGRADE_CONFIRM_POPUP", false);
			}
		}

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x0600459F RID: 17823 RVA: 0x001530A4 File Offset: 0x001512A4
		public static string GET_STRING_FACTORY_UPGRADE_COMPLETE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FACTORY_UPGRADE_COMPLETE", false);
			}
		}

		// Token: 0x060045A0 RID: 17824 RVA: 0x001530B4 File Offset: 0x001512B4
		public static string GetEquipUpgradeStateString(NKC_EQUIP_UPGRADE_STATE state)
		{
			switch (state)
			{
			default:
				return "";
			case NKC_EQUIP_UPGRADE_STATE.NEED_ENHANCE:
				return NKCStringTable.GetString("SI_PF_FACTORY_UPGRADE_STATE_ENHANCE", false);
			case NKC_EQUIP_UPGRADE_STATE.NEED_PRECISION:
				return NKCStringTable.GetString("SI_PF_FACTORY_UPGRADE_STATE_TUNING", false);
			case NKC_EQUIP_UPGRADE_STATE.NOT_HAVE:
				return NKCStringTable.GetString("SI_PF_FACTORY_UPGRADE_STATE_NOHAVE", false);
			}
		}

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x060045A1 RID: 17825 RVA: 0x00153104 File Offset: 0x00151304
		public static string GET_STRING_FACTORY_HIDDEN_OPTION_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_FACTORY_HIDDEN_OPTION_TITLE", false);
			}
		}

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x060045A2 RID: 17826 RVA: 0x00153111 File Offset: 0x00151311
		public static string GET_STRING_EQUIP_POTENTIAL_OPEN_REQUIRED
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EQUIP_POTENTIAL_OPEN_REQUIRED", false);
			}
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x060045A3 RID: 17827 RVA: 0x0015311E File Offset: 0x0015131E
		public static string GET_STRING_EQUIP_POTENTIAL_REQUIRED_ENCHANT_LV
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EQUIP_POTENTIAL_REQUIRED_ENCHANT_LV", false);
			}
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x060045A4 RID: 17828 RVA: 0x0015312B File Offset: 0x0015132B
		public static string GET_STRING_EQUIP_POTENTIAL_OPEN_ENABLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EQUIP_POTENTIAL_OPEN_ENABLE", false);
			}
		}

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x060045A5 RID: 17829 RVA: 0x00153138 File Offset: 0x00151338
		public static string GET_STRING_EQUIP_POTENTIAL_CANNOT_OPEN
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_EQUIP_POTENTIAL_CANNOT_OPEN", false);
			}
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x060045A6 RID: 17830 RVA: 0x00153145 File Offset: 0x00151345
		public static string GET_STRING_GREMORY_REQUEST
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENT_GREMORY_BAR_GREMORY_REQUEST", false);
			}
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x060045A7 RID: 17831 RVA: 0x00153152 File Offset: 0x00151352
		public static string GET_STRING_GREMORY_GIVE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENT_GREMORY_BAR_GREMORY_GIVE_DESC", false);
			}
		}

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x060045A8 RID: 17832 RVA: 0x0015315F File Offset: 0x0015135F
		public static string GET_STRING_GREMORY_GIVE_END
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENT_GREMORY_BAR_GREMORY_GIVE_END", false);
			}
		}

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x060045A9 RID: 17833 RVA: 0x0015316C File Offset: 0x0015136C
		public static string GET_STRING_GREMORY_GIVE_CANCEL
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENT_GREMORY_BAR_GREMORY_GIVE_CANCEL_DESC", false);
			}
		}

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x060045AA RID: 17834 RVA: 0x00153179 File Offset: 0x00151379
		public static string GET_STRING_GREMORY_CREATE_RESULT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENT_GREMORY_BAR_REWARD_TEXT", false);
			}
		}

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x060045AB RID: 17835 RVA: 0x00153186 File Offset: 0x00151386
		public static string GET_STRING_GREMORY_BARTENDER_HELLO
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENT_GREMORY_BAR_BARTENDER_MAKING_HELLO", false);
			}
		}

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x060045AC RID: 17836 RVA: 0x00153193 File Offset: 0x00151393
		public static string GET_STRING_GREMORY_MATERIAL_COUNT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENT_GREMORY_BAR_MENU_COUNT_TEXT", false);
			}
		}

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x060045AD RID: 17837 RVA: 0x001531A0 File Offset: 0x001513A0
		public static string GET_STRING_GREMORY_BARTENDER_SHAKE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENT_GREMORY_BAR_BARTENDER_MAKING_SHAKE", false);
			}
		}

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x060045AE RID: 17838 RVA: 0x001531AD File Offset: 0x001513AD
		public static string GET_STRING_GREMORY_BARTENDER_STIR
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENT_GREMORY_BAR_BARTENDER_MAKING_STIR", false);
			}
		}

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x060045AF RID: 17839 RVA: 0x001531BA File Offset: 0x001513BA
		public static string GET_STRING_GREMORY_BARTENDER_REJECT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENT_GREMORY_BAR_BARTENDER_MAKING_REJECT", false);
			}
		}

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x060045B0 RID: 17840 RVA: 0x001531C7 File Offset: 0x001513C7
		public static string GET_STRING_GREMORY_SELECT_COCKTAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENT_GREMORY_BAR_GREMORY_GIVE_SELECT_DESC", false);
			}
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x060045B1 RID: 17841 RVA: 0x001531D4 File Offset: 0x001513D4
		public static string GET_STRING_GREMORY_THAT_IS_WRONG_COCKTAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENT_GREMORY_BAR_GREMORY_GIVE_REJECT_DESC", false);
			}
		}

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x060045B2 RID: 17842 RVA: 0x001531E1 File Offset: 0x001513E1
		public static string GET_STRING_GREMORY_NEED_MORE_COCKTAIL
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENT_GREMORY_BAR_GREMORY_GIVE_REJECT_AMOUNT_DESC", false);
			}
		}

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x060045B3 RID: 17843 RVA: 0x001531EE File Offset: 0x001513EE
		public static string GET_STRING_GREMORY_DAILY_REWARD
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENT_GREMORY_BAR_DAILYREWARD_TEXT", false);
			}
		}

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x060045B4 RID: 17844 RVA: 0x001531FB File Offset: 0x001513FB
		public static string GET_STRING_GREMORY_MOMO_HELLO_1
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENT_GREMORY_BAR_MOMO_HELLO1", false);
			}
		}

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x060045B5 RID: 17845 RVA: 0x00153208 File Offset: 0x00151408
		public static string GET_STRING_GREMORY_MOMO_HELLO_2
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENT_GREMORY_BAR_MOMO_HELLO2", false);
			}
		}

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x060045B6 RID: 17846 RVA: 0x00153215 File Offset: 0x00151415
		public static string GET_STRING_GREMORY_MOMO_IGNORE_MISSION
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENT_GREMORY_BAR_MOMO_IGNORE_MISSION", false);
			}
		}

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x060045B7 RID: 17847 RVA: 0x00153222 File Offset: 0x00151422
		public static string GET_STRING_GREMORY_MOMO_IGNORE_ERRAND
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENT_GREMORY_BAR_MOMO_IGNORE_ERRAND", false);
			}
		}

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x060045B8 RID: 17848 RVA: 0x0015322F File Offset: 0x0015142F
		public static string GET_STRING_GREMORY_MOMO_COMPLETE_ERRAND
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_EVENT_GREMORY_BAR_MOMO_COMPLETE_ERRAND", false);
			}
		}

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x060045B9 RID: 17849 RVA: 0x0015323C File Offset: 0x0015143C
		public static string GET_STRING_JUKEBOX_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MUSIC", false);
			}
		}

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x060045BA RID: 17850 RVA: 0x00153249 File Offset: 0x00151449
		public static string GET_STRING_JUKEBOX_CONTENTS_UNLOCK
		{
			get
			{
				return NKCStringTable.GetString("SI_CONTENTS_UNLOCK_BASE_PERSONNAL", false);
			}
		}

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x060045BB RID: 17851 RVA: 0x00153256 File Offset: 0x00151456
		public static string GET_STRING_JUKEBOX_BLOCK_SLEEP_MODE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_BGM_SLEEP_MODE_NONE", false);
			}
		}

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x060045BC RID: 17852 RVA: 0x00153263 File Offset: 0x00151463
		public static string GET_STRING_JUKEBOX_FINISH_SLEEP_MODE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_BGM_SLEEP_MODE_MUSIC_DONE", false);
			}
		}

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x060045BD RID: 17853 RVA: 0x00153270 File Offset: 0x00151470
		public static string GET_STRING_JUKEBOX_BLOCK_SLOT_MSG
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_BGM_BLIND_MINI_POPUP_TEXT", false);
			}
		}

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x060045BE RID: 17854 RVA: 0x0015327D File Offset: 0x0015147D
		public static string GET_STRING_AD_FAILED_TO_LOAD
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_AD_FAILED_TO_LOAD", false);
			}
		}

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x060045BF RID: 17855 RVA: 0x0015328A File Offset: 0x0015148A
		public static string GET_STRING_AD_FAILED_TO_SHOW
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_AD_FAILED_TO_SHOW", false);
			}
		}

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x060045C0 RID: 17856 RVA: 0x00153297 File Offset: 0x00151497
		public static string GET_STRING_AD_NOT_INITIALIZED
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_AD_NOT_INITIALIZED", false);
			}
		}

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x060045C1 RID: 17857 RVA: 0x001532A4 File Offset: 0x001514A4
		public static string GET_STRING_AD_NOT_READY_STATUS
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_AD_NOT_READY_STATUS", false);
			}
		}

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x060045C2 RID: 17858 RVA: 0x001532B1 File Offset: 0x001514B1
		public static string GET_STRING_TRIM_NOT_INTERVAL_TIME
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_TRIM_DUNGEON_START_POPUP_INTERVAL_TEXT", false);
			}
		}

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x060045C3 RID: 17859 RVA: 0x001532BE File Offset: 0x001514BE
		public static string GET_STRING_TRIM_NOT_ENOUGH_SQUAD
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_TRIM_DUNGEON_START_POPUP_MADE_SQUAD_TEXT", false);
			}
		}

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x060045C4 RID: 17860 RVA: 0x001532CB File Offset: 0x001514CB
		public static string GET_STRING_TRIM_NOT_ENOUGH_POWER
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_TRIM_DUNGEON_START_POPUP_SQUAD_COMBAT_TEXT", false);
			}
		}

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x060045C5 RID: 17861 RVA: 0x001532D8 File Offset: 0x001514D8
		public static string GET_STRING_TRIM_NOT_ENOUGH_TRY_COUNT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_TRIM_DUNGEON_START_POPUP_COUNT_OVER_TEXT", false);
			}
		}

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x060045C6 RID: 17862 RVA: 0x001532E5 File Offset: 0x001514E5
		public static string GET_STRING_TRIM_NOT_ENOUGH_TRY_COUNT_RESTORE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_TRIM_DUNGEON_START_RESTORE_TEXT", false);
			}
		}

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x060045C7 RID: 17863 RVA: 0x001532F2 File Offset: 0x001514F2
		public static string GET_STRING_TRIM_SQUAD_COMBAT_PENALTY
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_TRIM_DUNGEON_SQUAD_COMBAT_PENALTY_TEXT", false);
			}
		}

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x060045C8 RID: 17864 RVA: 0x001532FF File Offset: 0x001514FF
		public static string GET_STRING_TRIM_EXIST_TRIM_COMBAT_DATA
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TRIM_MAIN_PLAYING_TEXT", false);
			}
		}

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x060045C9 RID: 17865 RVA: 0x0015330C File Offset: 0x0015150C
		public static string GET_STRING_TRIM_INTERVAL_END
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TRIM_MAIN_END_INTERVAL_TEXT", false);
			}
		}

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x060045CA RID: 17866 RVA: 0x00153319 File Offset: 0x00151519
		public static string GET_STRING_TRIM_STAGE_INDEX
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TRIM_DUNGEON_STAGE_TEXT", false);
			}
		}

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x060045CB RID: 17867 RVA: 0x00153326 File Offset: 0x00151526
		public static string GET_STRING_TRIM_ENTER_REMAIN_COUNT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_WORLD_MAP_TRIM_BUTTON_COUNT", false);
			}
		}

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x060045CC RID: 17868 RVA: 0x00153333 File Offset: 0x00151533
		public static string GET_STRING_MOUDLE_MERGE_TARGET_EMPTY
		{
			get
			{
				return NKCStringTable.GetString("SI_EVENT_MERGE_TARGET_EMPTY", false);
			}
		}

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x060045CD RID: 17869 RVA: 0x00153340 File Offset: 0x00151540
		public static string GET_STRING_MOUDLE_MERGE_NO_ENOUGH_COUNT
		{
			get
			{
				return NKCStringTable.GetString("SI_EVENT_MERGE_NO_ENOUGH_COUNT", false);
			}
		}

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x060045CE RID: 17870 RVA: 0x0015334D File Offset: 0x0015154D
		public static string GET_STRING_MODULE_MERGE_INPUT_COUNT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MERGE_AMOUNT", false);
			}
		}

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x060045CF RID: 17871 RVA: 0x0015335A File Offset: 0x0015155A
		public static string GET_STRING_MODULE_MERGE_INPUT_UNIT_HAVE_COUNT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_MERGE_HAVE_AMOUNT", false);
			}
		}

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x060045D0 RID: 17872 RVA: 0x00153367 File Offset: 0x00151567
		public static string GET_STRING_MODULE_CONTRACT_MILEAGE_POINT_DESC_02
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT_MILEAGE_POINT_EVENT_CLB_003", false);
			}
		}

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x060045D1 RID: 17873 RVA: 0x00153374 File Offset: 0x00151574
		public static string GET_STRING_TACTIC_UPDATE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_CONTRACT", false);
			}
		}

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x060045D2 RID: 17874 RVA: 0x00153381 File Offset: 0x00151581
		public static string GET_STRING_TACTIC_UPDATE_UNIT_SELECT_LIST_EMPTY_MESSAGE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TACTIC_UPDATE_TEXT_UNIT_NOTICE", false);
			}
		}

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x060045D3 RID: 17875 RVA: 0x0015338E File Offset: 0x0015158E
		public static string GET_STRING_TACTIC_UPDATE_UNIT_SELECT_NONE
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TACTIC_UPDATE_TEXT_UNIT_SELECT", false);
			}
		}

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x060045D4 RID: 17876 RVA: 0x0015339B File Offset: 0x0015159B
		public static string GET_STRING_TACTIC_UPDATE_MAX_UNIT
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TACTIC_UPDATE_TEXT_UNIT_MAX", false);
			}
		}

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x060045D5 RID: 17877 RVA: 0x001533A8 File Offset: 0x001515A8
		public static string GET_STRING_TACTIC_UPDATE_DESC_REARM
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_UNIT_TACTIC_UPDATE_DESC_REARM", false);
			}
		}

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x060045D6 RID: 17878 RVA: 0x001533B5 File Offset: 0x001515B5
		public static string GET_STRING_TACTIC_UPDATE_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_UNIT_TACTIC_UPDATE_DESC", false);
			}
		}

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x060045D7 RID: 17879 RVA: 0x001533C2 File Offset: 0x001515C2
		public static string GET_STRING_TACTIC_UPDATE_UNIT_WARNING
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TACTIC_UPDATE_TEXT_UNIT_WARNING", false);
			}
		}

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x060045D8 RID: 17880 RVA: 0x001533CF File Offset: 0x001515CF
		public static string GET_STRING_TACTIC_UPDATE_REFOUND_POPUP_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_UNIT_TACTIC_UPDATE_TEXT_RETURN", false);
			}
		}

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x060045D9 RID: 17881 RVA: 0x001533DC File Offset: 0x001515DC
		public static string GET_STRING_TACTIC_UPDATE_REFOUND_POPUP_DESC
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_UNIT_TACTIC_UPDATE_DESC_RETURN", false);
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x060045DA RID: 17882 RVA: 0x001533E9 File Offset: 0x001515E9
		public static string GET_STRING_TACTIC_UPDATE_BLOCK_MESSAGE_EQUIPED
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_TACTIC_UPDATE_TEXT_UNIT_EQUIP", false);
			}
		}

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x060045DB RID: 17883 RVA: 0x001533F6 File Offset: 0x001515F6
		public static string GET_STRING_TACTIC_UPDATE_SORT_TITLE
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_UNIT_TACTIC_UPDATE_TEXT_GRADE", false);
			}
		}

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x060045DC RID: 17884 RVA: 0x00153403 File Offset: 0x00151603
		public static string GET_STRING_TACTIC_UPDATE_RETURN_COUNT
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_UNIT_TACTIC_UPDATE_TEXT_RETURN_COUNT", false);
			}
		}

		// Token: 0x0400373F RID: 14143
		private static StringBuilder m_sStringBuilder = new StringBuilder();

		// Token: 0x020013D0 RID: 5072
		public class CompNKMDiveArtifactTemplet : IComparer<NKMDiveArtifactTemplet>
		{
			// Token: 0x0600A6C1 RID: 42689 RVA: 0x00347C68 File Offset: 0x00345E68
			public int Compare(NKMDiveArtifactTemplet x, NKMDiveArtifactTemplet y)
			{
				if (x == null)
				{
					return 1;
				}
				if (y == null)
				{
					return -1;
				}
				if (x.Category < y.Category)
				{
					return 1;
				}
				if (x.Category > y.Category)
				{
					return -1;
				}
				return x.ArtifactID.CompareTo(y.ArtifactID);
			}
		}

		// Token: 0x020013D1 RID: 5073
		public class CompGuildDungeonArtifactTemplet : IComparer<GuildDungeonArtifactTemplet>
		{
			// Token: 0x0600A6C3 RID: 42691 RVA: 0x00347CBC File Offset: 0x00345EBC
			public int Compare(GuildDungeonArtifactTemplet x, GuildDungeonArtifactTemplet y)
			{
				if (x == null)
				{
					return 1;
				}
				if (y == null)
				{
					return -1;
				}
				if (x.GetCategory() < y.GetCategory())
				{
					return 1;
				}
				if (x.GetCategory() > y.GetCategory())
				{
					return -1;
				}
				return x.GetArtifactId().CompareTo(y.GetArtifactId());
			}
		}

		// Token: 0x020013D2 RID: 5074
		// (Invoke) Token: 0x0600A6C6 RID: 42694
		private delegate string ConditionToString(int input);
	}
}
