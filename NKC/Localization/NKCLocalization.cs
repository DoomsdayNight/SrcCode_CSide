using System;
using System.Collections.Generic;
using Cs.Logging;
using NKC.Publisher;
using NKM;
using UnityEngine;

namespace NKC.Localization
{
	// Token: 0x020008A0 RID: 2208
	public static class NKCLocalization
	{
		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x060059F2 RID: 23026 RVA: 0x001B5044 File Offset: 0x001B3244
		public static bool CensoredVersion
		{
			get
			{
				return NKMContentsVersionManager.HasTag("CENSOR_ASSET");
			}
		}

		// Token: 0x060059F3 RID: 23027 RVA: 0x001B5050 File Offset: 0x001B3250
		public static string GetLangTagByLangCode(string langCode)
		{
			string result;
			if (NKCLocalization.s_dicLangTagByLangCode.TryGetValue(langCode, out result))
			{
				return result;
			}
			return "";
		}

		// Token: 0x060059F4 RID: 23028 RVA: 0x001B5074 File Offset: 0x001B3274
		public static string GetBySystemLanguageCode()
		{
			Log.Debug("GetLangTagByApplicationSystemLanguage [" + Application.systemLanguage.ToString() + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Localization/NKCLocalization.cs", 158);
			SystemLanguage systemLanguage = Application.systemLanguage;
			if (systemLanguage <= SystemLanguage.Japanese)
			{
				switch (systemLanguage)
				{
				case SystemLanguage.Chinese:
					return "zh-hant";
				case SystemLanguage.Czech:
				case SystemLanguage.Dutch:
				case SystemLanguage.German:
					return "de";
				case SystemLanguage.Danish:
				case SystemLanguage.French:
					return "fr";
				case SystemLanguage.English:
					return "en";
				case SystemLanguage.Estonian:
				case SystemLanguage.Faroese:
				case SystemLanguage.Finnish:
					break;
				default:
					if (systemLanguage == SystemLanguage.Japanese)
					{
						return "ja";
					}
					break;
				}
			}
			else
			{
				if (systemLanguage == SystemLanguage.Korean)
				{
					return "ko";
				}
				switch (systemLanguage)
				{
				case SystemLanguage.Thai:
					return "th";
				case SystemLanguage.Vietnamese:
					return "vi";
				case SystemLanguage.ChineseSimplified:
					return "zh-hans";
				}
			}
			return "";
		}

		// Token: 0x060059F5 RID: 23029 RVA: 0x001B5154 File Offset: 0x001B3354
		public static HashSet<NKM_NATIONAL_CODE> GetSelectLanguageSet()
		{
			HashSet<NKM_NATIONAL_CODE> hashSet = new HashSet<NKM_NATIONAL_CODE>();
			foreach (KeyValuePair<string, NKM_NATIONAL_CODE> keyValuePair in NKCLocalization.s_dicLanguageTag)
			{
				if (NKMContentsVersionManager.HasTag(keyValuePair.Key))
				{
					hashSet.Add(keyValuePair.Value);
				}
			}
			if (hashSet.Count > 0)
			{
				return hashSet;
			}
			if (NKCDefineManager.DEFINE_CLIENT_KOR())
			{
				hashSet.Add(NKM_NATIONAL_CODE.NNC_KOREA);
			}
			else if (NKCDefineManager.DEFINE_CLIENT_JPN())
			{
				hashSet.Add(NKM_NATIONAL_CODE.NNC_JAPAN);
			}
			else if (NKCDefineManager.DEFINE_CLIENT_CHN())
			{
				hashSet.Add(NKM_NATIONAL_CODE.NNC_SIMPLIFIED_CHINESE);
			}
			else if (NKCDefineManager.DEFINE_CLIENT_TWN())
			{
				hashSet.Add(NKM_NATIONAL_CODE.NNC_TRADITIONAL_CHINESE);
			}
			else if (NKCDefineManager.DEFINE_CLIENT_SEA())
			{
				hashSet.Add(NKM_NATIONAL_CODE.NNC_VIETNAM);
				hashSet.Add(NKM_NATIONAL_CODE.NNC_THAILAND);
				hashSet.Add(NKM_NATIONAL_CODE.NNC_ENG);
			}
			else if (NKCDefineManager.DEFINE_CLIENT_GBL())
			{
				hashSet.Add(NKM_NATIONAL_CODE.NNC_ENG);
				hashSet.Add(NKM_NATIONAL_CODE.NNC_DEUTSCH);
				hashSet.Add(NKM_NATIONAL_CODE.NNC_FRENCH);
				hashSet.Add(NKM_NATIONAL_CODE.NNC_KOREA);
			}
			if (hashSet.Count == 0)
			{
				hashSet.Add(NKCPublisherModule.Localization.GetDefaultLanguage());
			}
			return hashSet;
		}

		// Token: 0x060059F6 RID: 23030 RVA: 0x001B5274 File Offset: 0x001B3474
		public static bool IsVoiceVariant(string variant)
		{
			return NKCLocalization.s_dicVoiceVariant.ContainsValue(variant);
		}

		// Token: 0x060059F7 RID: 23031 RVA: 0x001B5281 File Offset: 0x001B3481
		public static string GetVariant(NKM_NATIONAL_CODE eCode)
		{
			if (NKCLocalization.s_dicLanguageVariant.ContainsKey(eCode))
			{
				return NKCLocalization.s_dicLanguageVariant[eCode];
			}
			return "";
		}

		// Token: 0x060059F8 RID: 23032 RVA: 0x001B52A1 File Offset: 0x001B34A1
		public static string GetVariant(NKC_VOICE_CODE eCode)
		{
			if (NKCLocalization.s_dicVoiceVariant.ContainsKey(eCode))
			{
				return NKCLocalization.s_dicVoiceVariant[eCode];
			}
			return "";
		}

		// Token: 0x060059F9 RID: 23033 RVA: 0x001B52C4 File Offset: 0x001B34C4
		public static string[] GetVariants(NKM_NATIONAL_CODE currentNationalCode, NKC_VOICE_CODE targetVoiceCode)
		{
			List<string> list = new List<string>();
			if (!NKCDefineManager.DEFINE_SERVICE())
			{
				list.Add("dev");
			}
			if (NKCLocalization.CensoredVersion)
			{
				list.Add("cn");
				list.Add("zlong");
			}
			if (NKMContentsVersionManager.HasCountryTag(CountryTagType.CHN))
			{
				list.Add("zchn");
			}
			if (NKMContentsVersionManager.HasCountryTag(CountryTagType.TWN))
			{
				list.Add("gbtwn");
			}
			if (NKMContentsVersionManager.HasCountryTag(CountryTagType.SEA))
			{
				list.Add("zsea");
			}
			if (NKMContentsVersionManager.HasCountryTag(CountryTagType.JPN))
			{
				list.Add("njpn");
			}
			if (NKMContentsVersionManager.HasCountryTag(CountryTagType.NAEU))
			{
				list.Add("naeu");
			}
			if (NKMContentsVersionManager.HasCountryTag(CountryTagType.GLOBAL))
			{
				list.Add("gbl");
			}
			string variant = NKCLocalization.GetVariant(currentNationalCode);
			if (!string.IsNullOrEmpty(variant))
			{
				list.Add(variant);
			}
			string variant2 = NKCLocalization.GetVariant(targetVoiceCode);
			if (!string.IsNullOrEmpty(variant2))
			{
				list.Add(variant2);
			}
			else
			{
				list.Add("vkor");
			}
			list.Add("asset");
			foreach (string str in list)
			{
				Log.Info("<color=#00ff00> Variant added : [" + str + "]</color>", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Localization/NKCLocalization.cs", 366);
			}
			return list.ToArray();
		}

		// Token: 0x060059FA RID: 23034 RVA: 0x001B5420 File Offset: 0x001B3620
		public static List<string> GetAllVariants()
		{
			List<string> list = new List<string>();
			list.Add("dev");
			list.Add("cn");
			list.Add("zlong");
			list.Add("zchn");
			list.Add("gbtwn");
			list.Add("zsea");
			list.Add("njpn");
			list.Add("naeu");
			list.Add("gbl");
			foreach (object obj in Enum.GetValues(typeof(NKM_NATIONAL_CODE)))
			{
				string variant = NKCLocalization.GetVariant((NKM_NATIONAL_CODE)obj);
				if (!string.IsNullOrEmpty(variant))
				{
					list.Add(variant);
				}
			}
			foreach (KeyValuePair<NKC_VOICE_CODE, string> keyValuePair in NKCLocalization.s_dicVoiceVariant)
			{
				string value = keyValuePair.Value;
				if (!string.IsNullOrEmpty(value))
				{
					list.Add(value);
				}
			}
			list.Add("asset");
			return list;
		}

		// Token: 0x04004575 RID: 17781
		public const string LOCALIZED_PATH_POSTFIX = "_loc";

		// Token: 0x04004576 RID: 17782
		public const string VARIANT_CENSORED = "cn";

		// Token: 0x04004577 RID: 17783
		public const string VARIANT_NATION_CN = "zlong";

		// Token: 0x04004578 RID: 17784
		public const string VARIANT_DEFAULT = "asset";

		// Token: 0x04004579 RID: 17785
		public const string CENSOR_TAG = "CENSOR_ASSET";

		// Token: 0x0400457A RID: 17786
		public const string VARIANT_NATION_CHN = "zchn";

		// Token: 0x0400457B RID: 17787
		public const string VARIANT_NATION_TWN = "gbtwn";

		// Token: 0x0400457C RID: 17788
		public const string VARIANT_NATION_SEA = "zsea";

		// Token: 0x0400457D RID: 17789
		public const string VARIANT_NATION_JPN = "njpn";

		// Token: 0x0400457E RID: 17790
		public const string VARIANT_NATION_NAEU = "naeu";

		// Token: 0x0400457F RID: 17791
		public const string VARIANT_NATION_GLOBAL = "gbl";

		// Token: 0x04004580 RID: 17792
		public const string LANGUAGE_TAG_KOR = "LANGUAGE_KOR";

		// Token: 0x04004581 RID: 17793
		public const string LANGUAGE_TAG_JPN = "LANGUAGE_JPN";

		// Token: 0x04004582 RID: 17794
		public const string LANGUAGE_TAG_ENG = "LANGUAGE_ENG";

		// Token: 0x04004583 RID: 17795
		public const string LANGUAGE_TAG_CENSORED_CHN = "LANGUAGE_CENSORED_CHN";

		// Token: 0x04004584 RID: 17796
		public const string LANGUAGE_TAG_SIMPLIFIED_CHN = "LANGUAGE_SIMPLIFIED_CHN";

		// Token: 0x04004585 RID: 17797
		public const string LANGUAGE_TAG_TRADITIONAL_CHN = "LANGUAGE_TRADITIONAL_CHN";

		// Token: 0x04004586 RID: 17798
		public const string LANGUAGE_TAG_THAILAND = "LANGUAGE_THA";

		// Token: 0x04004587 RID: 17799
		public const string LANGUAGE_TAG_VIETNAM = "LANGUAGE_VTN";

		// Token: 0x04004588 RID: 17800
		public const string LANGUAGE_TAG_DEUTSCH = "LANGUAGE_DEU";

		// Token: 0x04004589 RID: 17801
		public const string LANGUAGE_TAG_FRENCH = "LANGUAGE_FRA";

		// Token: 0x0400458A RID: 17802
		public const string LANGUAGE_CODE_KOR = "ko";

		// Token: 0x0400458B RID: 17803
		public const string LANGUAGE_CODE_JPN = "ja";

		// Token: 0x0400458C RID: 17804
		public const string LANGUAGE_CODE_ENG = "en";

		// Token: 0x0400458D RID: 17805
		public const string LANGUAGE_CODE_SIMPLIFIED_CHN = "zh-hans";

		// Token: 0x0400458E RID: 17806
		public const string LANGUAGE_CODE_TRADITIONAL_CHN = "zh-hant";

		// Token: 0x0400458F RID: 17807
		public const string LANGUAGE_CODE_THAILAND = "th";

		// Token: 0x04004590 RID: 17808
		public const string LANGUAGE_CODE_VIETNAM = "vi";

		// Token: 0x04004591 RID: 17809
		public const string LANGUAGE_CODE_DEUTSCH = "de";

		// Token: 0x04004592 RID: 17810
		public const string LANGUAGE_CODE_FRENCH = "fr";

		// Token: 0x04004593 RID: 17811
		public const string VOICE_VARIANT_KOR = "vkor";

		// Token: 0x04004594 RID: 17812
		public const string VOICE_TAG_KOR = "VOICE_KOR";

		// Token: 0x04004595 RID: 17813
		public const string VOICE_VARIANT_CHN = "vchn";

		// Token: 0x04004596 RID: 17814
		public const string VOICE_TAG_CHN = "VOICE_CHN";

		// Token: 0x04004597 RID: 17815
		public const string VOICE_VARIANT_JPN = "vjpn";

		// Token: 0x04004598 RID: 17816
		public const string VOICE_TAG_JPN = "VOICE_JPN";

		// Token: 0x04004599 RID: 17817
		public static readonly Dictionary<string, NKM_NATIONAL_CODE> s_dicLanguageTag = new Dictionary<string, NKM_NATIONAL_CODE>
		{
			{
				"LANGUAGE_KOR",
				NKM_NATIONAL_CODE.NNC_KOREA
			},
			{
				"LANGUAGE_JPN",
				NKM_NATIONAL_CODE.NNC_JAPAN
			},
			{
				"LANGUAGE_ENG",
				NKM_NATIONAL_CODE.NNC_ENG
			},
			{
				"LANGUAGE_SIMPLIFIED_CHN",
				NKM_NATIONAL_CODE.NNC_SIMPLIFIED_CHINESE
			},
			{
				"LANGUAGE_CENSORED_CHN",
				NKM_NATIONAL_CODE.NNC_CENSORED_CHINESE
			},
			{
				"LANGUAGE_TRADITIONAL_CHN",
				NKM_NATIONAL_CODE.NNC_TRADITIONAL_CHINESE
			},
			{
				"LANGUAGE_THA",
				NKM_NATIONAL_CODE.NNC_THAILAND
			},
			{
				"LANGUAGE_VTN",
				NKM_NATIONAL_CODE.NNC_VIETNAM
			},
			{
				"LANGUAGE_DEU",
				NKM_NATIONAL_CODE.NNC_DEUTSCH
			},
			{
				"LANGUAGE_FRA",
				NKM_NATIONAL_CODE.NNC_FRENCH
			}
		};

		// Token: 0x0400459A RID: 17818
		public static readonly Dictionary<string, string> s_dicLangTagByLangCode = new Dictionary<string, string>
		{
			{
				"ko",
				"LANGUAGE_KOR"
			},
			{
				"ja",
				"LANGUAGE_JPN"
			},
			{
				"en",
				"LANGUAGE_ENG"
			},
			{
				"zh-hans",
				"LANGUAGE_SIMPLIFIED_CHN"
			},
			{
				"zh-hant",
				"LANGUAGE_TRADITIONAL_CHN"
			},
			{
				"th",
				"LANGUAGE_THA"
			},
			{
				"vi",
				"LANGUAGE_VTN"
			},
			{
				"de",
				"LANGUAGE_DEU"
			},
			{
				"fr",
				"LANGUAGE_FRA"
			}
		};

		// Token: 0x0400459B RID: 17819
		public static readonly Dictionary<string, NKC_VOICE_CODE> s_dicVoiceTag = new Dictionary<string, NKC_VOICE_CODE>
		{
			{
				"VOICE_KOR",
				NKC_VOICE_CODE.NVC_KOR
			},
			{
				"VOICE_CHN",
				NKC_VOICE_CODE.NVC_CHN
			},
			{
				"VOICE_JPN",
				NKC_VOICE_CODE.NVC_JPN
			}
		};

		// Token: 0x0400459C RID: 17820
		public static readonly Dictionary<NKM_NATIONAL_CODE, string> s_dicLanguageVariant = new Dictionary<NKM_NATIONAL_CODE, string>
		{
			{
				NKM_NATIONAL_CODE.NNC_KOREA,
				"kor"
			},
			{
				NKM_NATIONAL_CODE.NNC_JAPAN,
				"jpn"
			},
			{
				NKM_NATIONAL_CODE.NNC_ENG,
				"eng"
			},
			{
				NKM_NATIONAL_CODE.NNC_CENSORED_CHINESE,
				"chn"
			},
			{
				NKM_NATIONAL_CODE.NNC_SIMPLIFIED_CHINESE,
				"scn"
			},
			{
				NKM_NATIONAL_CODE.NNC_TRADITIONAL_CHINESE,
				"twn"
			},
			{
				NKM_NATIONAL_CODE.NNC_THAILAND,
				"tha"
			},
			{
				NKM_NATIONAL_CODE.NNC_VIETNAM,
				"vtn"
			},
			{
				NKM_NATIONAL_CODE.NNC_DEUTSCH,
				"deu"
			},
			{
				NKM_NATIONAL_CODE.NNC_FRENCH,
				"fra"
			}
		};

		// Token: 0x0400459D RID: 17821
		public static readonly Dictionary<NKC_VOICE_CODE, string> s_dicVoiceVariant = new Dictionary<NKC_VOICE_CODE, string>
		{
			{
				NKC_VOICE_CODE.NVC_KOR,
				"vkor"
			},
			{
				NKC_VOICE_CODE.NVC_CHN,
				"vchn"
			},
			{
				NKC_VOICE_CODE.NVC_JPN,
				"vjpn"
			}
		};
	}
}
