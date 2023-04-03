using System;
using System.Collections.Generic;
using Cs.Logging;
using NKC.Publisher;
using NKM.Shop;

namespace NKC
{
	// Token: 0x02000616 RID: 1558
	public static class NKCAdjustManager
	{
		// Token: 0x06003018 RID: 12312 RVA: 0x000EB114 File Offset: 0x000E9314
		public static void Init()
		{
			if (!NKCAdjustManager.m_adjustStarted)
			{
				Log.Debug("[Adjust] StartManual - [ADJUST] Symbol not defined", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Adjust/NKCAdjustManager.cs", 69);
				NKCAdjustManager.m_useAdjust = false;
			}
			NKCAdjustManager.RegisterEventCode();
			NKCAdjustManager.RegisterEventID();
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x000EB140 File Offset: 0x000E9340
		public static void RegisterEventCode()
		{
			NKCAdjustManager.m_eventIDToEventCodeList.Clear();
			NKCAdjustManager.m_eventIDToEventCodeList.Add("00_first_purchase", "dxdwev");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("01_appLaunch", "75i8a5");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("02_downLoad_start", "pr6ym3");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("03_downLoad_complete", "5e9eo5");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("04_loading_start", "6tglx6");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("05_loading_complete", "xzedrv");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("06_login_complete", "9x016h");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("07_prologue", "5pdniy");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("08_ep1_1_1_stage_start", "stk6ev");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("09_ep1_1_2_stage_start", "160za9");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("10_ep1_1_3_stage_start", "1ugryd");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("11_ep1_1_4_stage_start", "xtp8jt");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("12_ep1_1_4_cutscene2_start", "qwqpog");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("12_tutorial_hq1_start", "c8hcek");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("13_username_creation", "inbde0");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("14_tutorial_hq2_ceooffice", "1g5joo");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("15_tutorial_gacha_newbie", "2pl5bl");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("16_tutorial_missions", "6j4mk3");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("17_ep1_2_1_missionstart", "sztouq");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("18_ep1_2_1_cutscene1_start", "74wipr");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("19_ep1_2_1_stage_start", "8sslcw");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("20_ep1_2_1_stage_result", "odtmx4");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("21_ep1_2_2_stage_start", "81dn1z");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("23_ep1_2_3_stage_start", "z8itse");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("24_ep1_2_4_stage_start", "3a8kwj");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("25_ep1_2_4_cutscene2_start", "76h47v");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("26_gacha_newbie_special", "dqpo2x");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("27_level_05", "t1o2ko");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("28_level_10", "np8e88");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("29_level_20", "7fcj81");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("30_level_30", "2d0kfd");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("31_level_40", "i4pybd");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("32_level_50", "9jg2wb");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("33_ApprenticeAdmin", "twirdf");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("34_AD_eternium", "r26dsm");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("35_AD_ch_inventory", "96vzgd");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_3401", "2v0qpb");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_3402", "7g9lhn");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_3403", "wdeok5");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_3404", "vwsx8g");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_3405", "jbsngh");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_3406", "hqxphd");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160384", "5s4eu7");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160263", "pyagtv");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160517", "4gfgfj");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160492", "oqn1wo");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160466", "1xr0cx");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160541", "xf9gt1");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160542", "s0g5e1");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160561", "z86u46");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160478", "hg9mur");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160479", "r4tqpv");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160480", "dong3w");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160481", "lrfq3x");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160452", "i84qh4");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160453", "ex0cqd");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160454", "kn9dqw");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160455", "i6hhyp");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160500", "togmnl");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160491", "6ds7q0");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160270", "ncpwpr");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160520", "2qzu6t");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160076", "iuwbh3");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160398", "lp5pke");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160399", "n38rv5");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160400", "rvzu2h");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160519", "x42les");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160518", "760g31");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160271", "l8etw6");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160272", "j8q8mq");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160209", "d9swng");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160210", "qvg55d");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160211", "kd8nbw");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160212", "7jfxtl");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160222", "pofy68");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160449", "5kuzlb");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160407", "1b1w3k");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160408", "w6qyeq");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160409", "fz8vx7");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160364", "mstgj6");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160363", "cu8yzb");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160504", "4su9jf");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160505", "l1t9xt");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160506", "283v0m");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160279", "cn9n97");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160459", "rehrwn");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160460", "pdlado");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160461", "gifppd");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160524", "743msu");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160525", "iv4e29");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160417", "qzp7tu");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160418", "31v747");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160419", "ndzuky");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160442", "hrqreu");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160443", "lnflyo");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160388", "uzs1w7");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5014", "10k48u");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5076", "5438pl");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5081", "njyw2c");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5004", "nkty2g");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5015", "jmu89z");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5011", "cx4w9o");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5005", "zaflnt");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5010", "vkeecl");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5024", "sye9bi");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5042", "u8l0ix");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5043", "q138r8");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5044", "p2wkj7");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5045", "ddlqa7");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5070", "k45bp1");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5051", "qi6iu7");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5053", "b7f2ai");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5012", "laljj7");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5050", "bftqfn");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5063", "mhaf24");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5062", "x5sahy");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160462", "53hmu3");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160463", "mgk3n5");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160387", "ncfskk");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160324", "7sza4i");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160325", "u3xsbn");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160434", "x5l3ug");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160521", "uxk8hc");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160394", "u4onu8");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160395", "vx888e");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160396", "t49sp4");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5047", "j0sfnz");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5079", "tjpmfu");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5046", "tajok0");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5078", "kcgwbq");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5019", "1r8xc7");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5020", "qnc6g5");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5021", "jijl62");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5022", "j2jkjh");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5049", "ktvo10");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160523", "8hmieb");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160522", "v9y7bw");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160482", "7s2gvt");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160531", "v7m65k");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160532", "8ndwvi");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160386", "8xaelf");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160483", "g9qfhb");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160511", "i6wcxi");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160512", "z7zy6z");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160390", "hwfr99");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160526", "uqgavu");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160527", "9b0oic");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160457", "mb3skf");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160566", "d53xek");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160464", "x7r7pb");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160465", "qfqj2x");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160392", "e5bbt9");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5069", "9ysst3");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160582", "29ri6c");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5041", "t9o14d");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5038", "j5fz5j");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5033", "8dks6m");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5034", "5fz06w");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5036", "uige7q");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5039", "v3ze2w");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5040", "5hwk3s");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5037", "hbbasd");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5029", "c4jiu6");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5030", "poow0z");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5031", "8k4j55");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5032", "x0j1vk");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160507", "bacizu");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160508", "1u33z6");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160385", "ub7i3i");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5064", "g9iyop");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5068", "6odw6y");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5065", "dxltti");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5066", "tmt6nr");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5067", "695fgl");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160432", "kseu41");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160433", "npe8m5");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160391", "du8lpt");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160456", "wxl2gd");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5101", "fubpfm");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5099", "2da8tm");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5100", "xhi9lg");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5061", "iyyqth");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5060", "ot8961");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5056", "5f32po");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5057", "256e2q");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5058", "zed1e4");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5059", "3z9qbi");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5109", "u2hr4g");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5108", "lwgnne");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5107", "jl1mj7");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5106", "18k0mr");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160509", "rny9wd");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160510", "3sfx8j");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160393", "l6akke");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160612", "14prph");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160614", "3cupm8");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160610", "hyzq9h");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160613", "ih2zsi");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160611", "mo1esf");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_190912", "ysaerw");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160615", "aq17ki");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160609", "ho0vw1");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160599", "pyxb4p");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160405", "bw0463");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160406", "pwz613");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160389", "c2hz1w");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_160606", "fvmw98");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5087", "fchxzu");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5088", "xr892y");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5085", "ipib46");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5086", "eqssnl");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5072", "uzpoa1");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5074", "zgcixw");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("36_shop_coin_5073", "52w1vk");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("38_counterpass_sp", "2cphzq");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("38_counterpass_spplus", "io8k80");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("38_counterpass_lvup", "ydkno0");
			NKCAdjustManager.m_eventIDToEventCodeList.Add("37_inapp_purchase", "qwrar4");
			Log.Debug(string.Format("[Adjust] RegisterEventCode [{0}]", NKCAdjustManager.m_eventIDToEventCodeList.Count), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Adjust/NKCAdjustManager.cs", 309);
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x000EC288 File Offset: 0x000EA488
		public static void RegisterEventID()
		{
			NKCAdjustManager.m_playCutSceneEvent.Clear();
			NKCAdjustManager.AddCutSceneEvent(7, "07_prologue");
			NKCAdjustManager.AddCutSceneEvent(8, "08_1_ep1_1_stage_end");
			NKCAdjustManager.AddCutSceneEvent(9, "09_ep1_1_2_stage_start");
			NKCAdjustManager.AddCutSceneEvent(11, "10_ep1_1_3_stage_start");
			NKCAdjustManager.AddCutSceneEvent(13, "11_ep1_1_4_stage_start");
			NKCAdjustManager.AddCutSceneEvent(14, "12_ep1_1_4_cutscene2_start");
			NKCAdjustManager.AddCutSceneEvent(17, "17_ep1_2_1_missionstart");
			NKCAdjustManager.AddCutSceneEvent(18, "18_ep1_2_1_cutscene1_start");
			NKCAdjustManager.AddCutSceneEvent(19, "19_ep1_2_1_stage_start");
			NKCAdjustManager.AddCutSceneEvent(20, "20_ep1_2_1_stage_result");
			NKCAdjustManager.AddCutSceneEvent(21, "21_ep1_2_2_stage_start");
			NKCAdjustManager.AddCutSceneEvent(23, "23_ep1_2_3_stage_start");
			NKCAdjustManager.AddCutSceneEvent(24, "24_ep1_2_4_stage_start");
			NKCAdjustManager.AddCutSceneEvent(25, "25_ep1_2_4_cutscene2_start");
			NKCAdjustManager.m_warFareResultEvent.Clear();
			NKCAdjustManager.m_dungeonEnterEvent.Clear();
			NKCAdjustManager.AddDungeonEnter(1004, "08_ep1_1_1_stage_start");
			NKCAdjustManager.m_dungeonClearEvent.Clear();
			NKCAdjustManager.m_playTutorialEvent.Clear();
			NKCAdjustManager.AddTutorialEvent(721, "12_tutorial_hq1_start");
			NKCAdjustManager.AddTutorialEvent(732, "14_tutorial_hq2_ceooffice");
			NKCAdjustManager.AddTutorialEvent(181, "15_tutorial_gacha_newbie");
			NKCAdjustManager.AddTutorialEvent(460, "16_tutorial_missions");
			NKCAdjustManager.AddTutorialEvent(120, "26_gacha_newbie_special");
			NKCAdjustManager.m_cashItemPurchaseEvent.Clear();
			NKCAdjustManager.AddCashItemPurchaseEvent(3401, "36_shop_coin_3401");
			NKCAdjustManager.AddCashItemPurchaseEvent(3402, "36_shop_coin_3402");
			NKCAdjustManager.AddCashItemPurchaseEvent(3403, "36_shop_coin_3403");
			NKCAdjustManager.AddCashItemPurchaseEvent(3404, "36_shop_coin_3404");
			NKCAdjustManager.AddCashItemPurchaseEvent(3405, "36_shop_coin_3405");
			NKCAdjustManager.AddCashItemPurchaseEvent(3406, "36_shop_coin_3406");
			NKCAdjustManager.AddCashItemPurchaseEvent(160384, "36_shop_coin_160384");
			NKCAdjustManager.AddCashItemPurchaseEvent(160263, "36_shop_coin_160263");
			NKCAdjustManager.AddCashItemPurchaseEvent(160517, "36_shop_coin_160517");
			NKCAdjustManager.AddCashItemPurchaseEvent(160492, "36_shop_coin_160492");
			NKCAdjustManager.AddCashItemPurchaseEvent(160466, "36_shop_coin_160466");
			NKCAdjustManager.AddCashItemPurchaseEvent(160541, "36_shop_coin_160541");
			NKCAdjustManager.AddCashItemPurchaseEvent(160542, "36_shop_coin_160542");
			NKCAdjustManager.AddCashItemPurchaseEvent(160561, "36_shop_coin_160561");
			NKCAdjustManager.AddCashItemPurchaseEvent(160478, "36_shop_coin_160478");
			NKCAdjustManager.AddCashItemPurchaseEvent(160479, "36_shop_coin_160479");
			NKCAdjustManager.AddCashItemPurchaseEvent(160480, "36_shop_coin_160480");
			NKCAdjustManager.AddCashItemPurchaseEvent(160481, "36_shop_coin_160481");
			NKCAdjustManager.AddCashItemPurchaseEvent(160452, "36_shop_coin_160452");
			NKCAdjustManager.AddCashItemPurchaseEvent(160453, "36_shop_coin_160453");
			NKCAdjustManager.AddCashItemPurchaseEvent(160454, "36_shop_coin_160454");
			NKCAdjustManager.AddCashItemPurchaseEvent(160455, "36_shop_coin_160455");
			NKCAdjustManager.AddCashItemPurchaseEvent(160500, "36_shop_coin_160500");
			NKCAdjustManager.AddCashItemPurchaseEvent(160491, "36_shop_coin_160491");
			NKCAdjustManager.AddCashItemPurchaseEvent(160270, "36_shop_coin_160270");
			NKCAdjustManager.AddCashItemPurchaseEvent(160520, "36_shop_coin_160520");
			NKCAdjustManager.AddCashItemPurchaseEvent(160076, "36_shop_coin_160076");
			NKCAdjustManager.AddCashItemPurchaseEvent(160398, "36_shop_coin_160398");
			NKCAdjustManager.AddCashItemPurchaseEvent(160399, "36_shop_coin_160399");
			NKCAdjustManager.AddCashItemPurchaseEvent(160400, "36_shop_coin_160400");
			NKCAdjustManager.AddCashItemPurchaseEvent(160519, "36_shop_coin_160519");
			NKCAdjustManager.AddCashItemPurchaseEvent(160518, "36_shop_coin_160518");
			NKCAdjustManager.AddCashItemPurchaseEvent(160271, "36_shop_coin_160271");
			NKCAdjustManager.AddCashItemPurchaseEvent(160272, "36_shop_coin_160272");
			NKCAdjustManager.AddCashItemPurchaseEvent(160209, "36_shop_coin_160209");
			NKCAdjustManager.AddCashItemPurchaseEvent(160210, "36_shop_coin_160210");
			NKCAdjustManager.AddCashItemPurchaseEvent(160211, "36_shop_coin_160211");
			NKCAdjustManager.AddCashItemPurchaseEvent(160212, "36_shop_coin_160212");
			NKCAdjustManager.AddCashItemPurchaseEvent(160222, "36_shop_coin_160222");
			NKCAdjustManager.AddCashItemPurchaseEvent(160449, "36_shop_coin_160449");
			NKCAdjustManager.AddCashItemPurchaseEvent(160407, "36_shop_coin_160407");
			NKCAdjustManager.AddCashItemPurchaseEvent(160408, "36_shop_coin_160408");
			NKCAdjustManager.AddCashItemPurchaseEvent(160409, "36_shop_coin_160409");
			NKCAdjustManager.AddCashItemPurchaseEvent(160364, "36_shop_coin_160364");
			NKCAdjustManager.AddCashItemPurchaseEvent(160363, "36_shop_coin_160363");
			NKCAdjustManager.AddCashItemPurchaseEvent(160504, "36_shop_coin_160504");
			NKCAdjustManager.AddCashItemPurchaseEvent(160505, "36_shop_coin_160505");
			NKCAdjustManager.AddCashItemPurchaseEvent(160506, "36_shop_coin_160506");
			NKCAdjustManager.AddCashItemPurchaseEvent(160279, "36_shop_coin_160279");
			NKCAdjustManager.AddCashItemPurchaseEvent(160459, "36_shop_coin_160459");
			NKCAdjustManager.AddCashItemPurchaseEvent(160460, "36_shop_coin_160460");
			NKCAdjustManager.AddCashItemPurchaseEvent(160461, "36_shop_coin_160461");
			NKCAdjustManager.AddCashItemPurchaseEvent(160524, "36_shop_coin_160524");
			NKCAdjustManager.AddCashItemPurchaseEvent(160525, "36_shop_coin_160525");
			NKCAdjustManager.AddCashItemPurchaseEvent(160417, "36_shop_coin_160417");
			NKCAdjustManager.AddCashItemPurchaseEvent(160418, "36_shop_coin_160418");
			NKCAdjustManager.AddCashItemPurchaseEvent(160419, "36_shop_coin_160419");
			NKCAdjustManager.AddCashItemPurchaseEvent(160442, "36_shop_coin_160442");
			NKCAdjustManager.AddCashItemPurchaseEvent(160443, "36_shop_coin_160443");
			NKCAdjustManager.AddCashItemPurchaseEvent(160388, "36_shop_coin_160388");
			NKCAdjustManager.AddCashItemPurchaseEvent(5014, "36_shop_coin_5014");
			NKCAdjustManager.AddCashItemPurchaseEvent(5076, "36_shop_coin_5076");
			NKCAdjustManager.AddCashItemPurchaseEvent(5081, "36_shop_coin_5081");
			NKCAdjustManager.AddCashItemPurchaseEvent(5004, "36_shop_coin_5004");
			NKCAdjustManager.AddCashItemPurchaseEvent(5015, "36_shop_coin_5015");
			NKCAdjustManager.AddCashItemPurchaseEvent(5011, "36_shop_coin_5011");
			NKCAdjustManager.AddCashItemPurchaseEvent(5005, "36_shop_coin_5005");
			NKCAdjustManager.AddCashItemPurchaseEvent(5010, "36_shop_coin_5010");
			NKCAdjustManager.AddCashItemPurchaseEvent(5024, "36_shop_coin_5024");
			NKCAdjustManager.AddCashItemPurchaseEvent(5042, "36_shop_coin_5042");
			NKCAdjustManager.AddCashItemPurchaseEvent(5043, "36_shop_coin_5043");
			NKCAdjustManager.AddCashItemPurchaseEvent(5044, "36_shop_coin_5044");
			NKCAdjustManager.AddCashItemPurchaseEvent(5045, "36_shop_coin_5045");
			NKCAdjustManager.AddCashItemPurchaseEvent(5070, "36_shop_coin_5070");
			NKCAdjustManager.AddCashItemPurchaseEvent(5051, "36_shop_coin_5051");
			NKCAdjustManager.AddCashItemPurchaseEvent(5053, "36_shop_coin_5053");
			NKCAdjustManager.AddCashItemPurchaseEvent(5012, "36_shop_coin_5012");
			NKCAdjustManager.AddCashItemPurchaseEvent(5050, "36_shop_coin_5050");
			NKCAdjustManager.AddCashItemPurchaseEvent(5063, "36_shop_coin_5063");
			NKCAdjustManager.AddCashItemPurchaseEvent(5062, "36_shop_coin_5062");
			NKCAdjustManager.AddCashItemPurchaseEvent(160462, "36_shop_coin_160462");
			NKCAdjustManager.AddCashItemPurchaseEvent(160463, "36_shop_coin_160463");
			NKCAdjustManager.AddCashItemPurchaseEvent(160387, "36_shop_coin_160387");
			NKCAdjustManager.AddCashItemPurchaseEvent(160324, "36_shop_coin_160324");
			NKCAdjustManager.AddCashItemPurchaseEvent(160325, "36_shop_coin_160325");
			NKCAdjustManager.AddCashItemPurchaseEvent(160434, "36_shop_coin_160434");
			NKCAdjustManager.AddCashItemPurchaseEvent(160521, "36_shop_coin_160521");
			NKCAdjustManager.AddCashItemPurchaseEvent(160394, "36_shop_coin_160394");
			NKCAdjustManager.AddCashItemPurchaseEvent(160395, "36_shop_coin_160395");
			NKCAdjustManager.AddCashItemPurchaseEvent(160396, "36_shop_coin_160396");
			NKCAdjustManager.AddCashItemPurchaseEvent(5047, "36_shop_coin_5047");
			NKCAdjustManager.AddCashItemPurchaseEvent(5079, "36_shop_coin_5079");
			NKCAdjustManager.AddCashItemPurchaseEvent(5046, "36_shop_coin_5046");
			NKCAdjustManager.AddCashItemPurchaseEvent(5078, "36_shop_coin_5078");
			NKCAdjustManager.AddCashItemPurchaseEvent(5019, "36_shop_coin_5019");
			NKCAdjustManager.AddCashItemPurchaseEvent(5020, "36_shop_coin_5020");
			NKCAdjustManager.AddCashItemPurchaseEvent(5021, "36_shop_coin_5021");
			NKCAdjustManager.AddCashItemPurchaseEvent(5022, "36_shop_coin_5022");
			NKCAdjustManager.AddCashItemPurchaseEvent(5049, "36_shop_coin_5049");
			NKCAdjustManager.AddCashItemPurchaseEvent(160523, "36_shop_coin_160523");
			NKCAdjustManager.AddCashItemPurchaseEvent(160522, "36_shop_coin_160522");
			NKCAdjustManager.AddCashItemPurchaseEvent(160482, "36_shop_coin_160482");
			NKCAdjustManager.AddCashItemPurchaseEvent(160531, "36_shop_coin_160531");
			NKCAdjustManager.AddCashItemPurchaseEvent(160532, "36_shop_coin_160532");
			NKCAdjustManager.AddCashItemPurchaseEvent(160386, "36_shop_coin_160386");
			NKCAdjustManager.AddCashItemPurchaseEvent(160483, "36_shop_coin_160483");
			NKCAdjustManager.AddCashItemPurchaseEvent(160511, "36_shop_coin_160511");
			NKCAdjustManager.AddCashItemPurchaseEvent(160512, "36_shop_coin_160512");
			NKCAdjustManager.AddCashItemPurchaseEvent(160390, "36_shop_coin_160390");
			NKCAdjustManager.AddCashItemPurchaseEvent(160526, "36_shop_coin_160526");
			NKCAdjustManager.AddCashItemPurchaseEvent(160527, "36_shop_coin_160527");
			NKCAdjustManager.AddCashItemPurchaseEvent(160457, "36_shop_coin_160457");
			NKCAdjustManager.AddCashItemPurchaseEvent(160566, "36_shop_coin_160566");
			NKCAdjustManager.AddCashItemPurchaseEvent(160464, "36_shop_coin_160464");
			NKCAdjustManager.AddCashItemPurchaseEvent(160465, "36_shop_coin_160465");
			NKCAdjustManager.AddCashItemPurchaseEvent(160392, "36_shop_coin_160392");
			NKCAdjustManager.AddCashItemPurchaseEvent(5069, "36_shop_coin_5069");
			NKCAdjustManager.AddCashItemPurchaseEvent(160582, "36_shop_coin_160582");
			NKCAdjustManager.AddCashItemPurchaseEvent(5041, "36_shop_coin_5041");
			NKCAdjustManager.AddCashItemPurchaseEvent(5038, "36_shop_coin_5038");
			NKCAdjustManager.AddCashItemPurchaseEvent(5033, "36_shop_coin_5033");
			NKCAdjustManager.AddCashItemPurchaseEvent(5034, "36_shop_coin_5034");
			NKCAdjustManager.AddCashItemPurchaseEvent(5036, "36_shop_coin_5036");
			NKCAdjustManager.AddCashItemPurchaseEvent(5039, "36_shop_coin_5039");
			NKCAdjustManager.AddCashItemPurchaseEvent(5040, "36_shop_coin_5040");
			NKCAdjustManager.AddCashItemPurchaseEvent(5037, "36_shop_coin_5037");
			NKCAdjustManager.AddCashItemPurchaseEvent(5029, "36_shop_coin_5029");
			NKCAdjustManager.AddCashItemPurchaseEvent(5030, "36_shop_coin_5030");
			NKCAdjustManager.AddCashItemPurchaseEvent(5031, "36_shop_coin_5031");
			NKCAdjustManager.AddCashItemPurchaseEvent(5032, "36_shop_coin_5032");
			NKCAdjustManager.AddCashItemPurchaseEvent(160507, "36_shop_coin_160507");
			NKCAdjustManager.AddCashItemPurchaseEvent(160508, "36_shop_coin_160508");
			NKCAdjustManager.AddCashItemPurchaseEvent(160385, "36_shop_coin_160385");
			NKCAdjustManager.AddCashItemPurchaseEvent(5064, "36_shop_coin_5064");
			NKCAdjustManager.AddCashItemPurchaseEvent(5068, "36_shop_coin_5068");
			NKCAdjustManager.AddCashItemPurchaseEvent(5065, "36_shop_coin_5065");
			NKCAdjustManager.AddCashItemPurchaseEvent(5066, "36_shop_coin_5066");
			NKCAdjustManager.AddCashItemPurchaseEvent(5067, "36_shop_coin_5067");
			NKCAdjustManager.AddCashItemPurchaseEvent(160432, "36_shop_coin_160432");
			NKCAdjustManager.AddCashItemPurchaseEvent(160433, "36_shop_coin_160433");
			NKCAdjustManager.AddCashItemPurchaseEvent(160391, "36_shop_coin_160391");
			NKCAdjustManager.AddCashItemPurchaseEvent(160456, "36_shop_coin_160456");
			NKCAdjustManager.AddCashItemPurchaseEvent(5101, "36_shop_coin_5101");
			NKCAdjustManager.AddCashItemPurchaseEvent(5099, "36_shop_coin_5099");
			NKCAdjustManager.AddCashItemPurchaseEvent(5100, "36_shop_coin_5100");
			NKCAdjustManager.AddCashItemPurchaseEvent(5061, "36_shop_coin_5061");
			NKCAdjustManager.AddCashItemPurchaseEvent(5060, "36_shop_coin_5060");
			NKCAdjustManager.AddCashItemPurchaseEvent(5056, "36_shop_coin_5056");
			NKCAdjustManager.AddCashItemPurchaseEvent(5057, "36_shop_coin_5057");
			NKCAdjustManager.AddCashItemPurchaseEvent(5058, "36_shop_coin_5058");
			NKCAdjustManager.AddCashItemPurchaseEvent(5059, "36_shop_coin_5059");
			NKCAdjustManager.AddCashItemPurchaseEvent(5109, "36_shop_coin_5109");
			NKCAdjustManager.AddCashItemPurchaseEvent(5108, "36_shop_coin_5108");
			NKCAdjustManager.AddCashItemPurchaseEvent(5107, "36_shop_coin_5107");
			NKCAdjustManager.AddCashItemPurchaseEvent(5106, "36_shop_coin_5106");
			NKCAdjustManager.AddCashItemPurchaseEvent(160509, "36_shop_coin_160509");
			NKCAdjustManager.AddCashItemPurchaseEvent(160510, "36_shop_coin_160510");
			NKCAdjustManager.AddCashItemPurchaseEvent(160393, "36_shop_coin_160393");
			NKCAdjustManager.AddCashItemPurchaseEvent(160612, "36_shop_coin_160612");
			NKCAdjustManager.AddCashItemPurchaseEvent(160614, "36_shop_coin_160614");
			NKCAdjustManager.AddCashItemPurchaseEvent(160610, "36_shop_coin_160610");
			NKCAdjustManager.AddCashItemPurchaseEvent(160613, "36_shop_coin_160613");
			NKCAdjustManager.AddCashItemPurchaseEvent(160611, "36_shop_coin_160611");
			NKCAdjustManager.AddCashItemPurchaseEvent(190912, "36_shop_coin_190912");
			NKCAdjustManager.AddCashItemPurchaseEvent(160615, "36_shop_coin_160615");
			NKCAdjustManager.AddCashItemPurchaseEvent(160609, "36_shop_coin_160609");
			NKCAdjustManager.AddCashItemPurchaseEvent(160599, "36_shop_coin_160599");
			NKCAdjustManager.AddCashItemPurchaseEvent(160405, "36_shop_coin_160405");
			NKCAdjustManager.AddCashItemPurchaseEvent(160406, "36_shop_coin_160406");
			NKCAdjustManager.AddCashItemPurchaseEvent(160389, "36_shop_coin_160389");
			NKCAdjustManager.AddCashItemPurchaseEvent(160606, "36_shop_coin_160606");
			NKCAdjustManager.AddCashItemPurchaseEvent(5087, "36_shop_coin_5087");
			NKCAdjustManager.AddCashItemPurchaseEvent(5088, "36_shop_coin_5088");
			NKCAdjustManager.AddCashItemPurchaseEvent(5085, "36_shop_coin_5085");
			NKCAdjustManager.AddCashItemPurchaseEvent(5086, "36_shop_coin_5086");
			NKCAdjustManager.AddCashItemPurchaseEvent(5072, "36_shop_coin_5072");
			NKCAdjustManager.AddCashItemPurchaseEvent(5074, "36_shop_coin_5074");
			NKCAdjustManager.AddCashItemPurchaseEvent(5073, "36_shop_coin_5073");
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x000ECE3C File Offset: 0x000EB03C
		private static string GetEventCodeByEventID(string eventID)
		{
			string result;
			if (NKCAdjustManager.m_eventIDToEventCodeList.TryGetValue(eventID, out result))
			{
				return result;
			}
			Log.Debug("[Adjust] Can not find Event Code eventID[" + eventID + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Adjust/NKCAdjustManager.cs", 546);
			return "";
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x000ECE7E File Offset: 0x000EB07E
		private static void AddCutSceneEvent(int cutSceneID, string eventID)
		{
			if (!NKCAdjustManager.m_playCutSceneEvent.ContainsKey(cutSceneID))
			{
				NKCAdjustManager.m_playCutSceneEvent.Add(cutSceneID, eventID);
			}
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x000ECE99 File Offset: 0x000EB099
		private static void AddTutorialEvent(int tutorialID, string eventID)
		{
			if (!NKCAdjustManager.m_playTutorialEvent.ContainsKey(tutorialID))
			{
				NKCAdjustManager.m_playTutorialEvent.Add(tutorialID, eventID);
			}
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x000ECEB4 File Offset: 0x000EB0B4
		private static void AddCashItemPurchaseEvent(int productID, string eventID)
		{
			if (!NKCAdjustManager.m_cashItemPurchaseEvent.ContainsKey(productID))
			{
				NKCAdjustManager.m_cashItemPurchaseEvent.Add(productID, eventID);
			}
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x000ECECF File Offset: 0x000EB0CF
		private static void AddDungeonEnter(int dungeonID, string eventID)
		{
			if (!NKCAdjustManager.m_dungeonEnterEvent.ContainsKey(dungeonID))
			{
				NKCAdjustManager.m_dungeonEnterEvent.Add(dungeonID, eventID);
			}
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x000ECEEA File Offset: 0x000EB0EA
		public static void OnCustomEvent(string eventID)
		{
			if (!NKCAdjustManager.m_useAdjust)
			{
				return;
			}
			if (NKCAdjustManager.m_logAdjust)
			{
				Log.Debug("[Adjust] OnCustomEvent EventID[" + eventID + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Adjust/NKCAdjustManager.cs", 592);
			}
			NKCAdjustManager.SendAdjustEvent(eventID);
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x000ECF20 File Offset: 0x000EB120
		public static void OnPlayCutScene(int cutSceneID)
		{
			if (!NKCAdjustManager.m_useAdjust)
			{
				return;
			}
			if (NKCAdjustManager.m_logAdjust)
			{
				Log.Debug(string.Format("[Adjust] OnPlayCutScene CutSceneID[{0}]", cutSceneID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Adjust/NKCAdjustManager.cs", 607);
			}
			string eventID;
			if (NKCAdjustManager.m_playCutSceneEvent.TryGetValue(cutSceneID, out eventID))
			{
				NKCAdjustManager.SendAdjustEvent(eventID);
			}
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x000ECF70 File Offset: 0x000EB170
		public static void OnWarfareResult(int warfareID)
		{
			if (!NKCAdjustManager.m_useAdjust)
			{
				return;
			}
			if (NKCAdjustManager.m_logAdjust)
			{
				Log.Debug(string.Format("[Adjust] OnWarfareResult warfareID[{0}]", warfareID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Adjust/NKCAdjustManager.cs", 625);
			}
			string eventID;
			if (NKCAdjustManager.m_warFareResultEvent.TryGetValue(warfareID, out eventID))
			{
				NKCAdjustManager.SendAdjustEvent(eventID);
			}
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x000ECFC0 File Offset: 0x000EB1C0
		public static void OnPlayTutorial(int tutorialID)
		{
			if (!NKCAdjustManager.m_useAdjust)
			{
				return;
			}
			if (NKCAdjustManager.m_logAdjust)
			{
				Log.Debug(string.Format("[Adjust] OnPlayTutorial eventID[{0}]", tutorialID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Adjust/NKCAdjustManager.cs", 643);
			}
			string eventID;
			if (NKCAdjustManager.m_playTutorialEvent.TryGetValue(tutorialID, out eventID))
			{
				NKCAdjustManager.SendAdjustEvent(eventID);
			}
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x000ED010 File Offset: 0x000EB210
		public static void OnEnterDungeon(int dungeonID)
		{
			if (!NKCAdjustManager.m_useAdjust)
			{
				return;
			}
			if (NKCAdjustManager.m_logAdjust)
			{
				Log.Debug(string.Format("[Adjust] OnEnterDungeon dungeonID[{0}]", dungeonID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Adjust/NKCAdjustManager.cs", 661);
			}
			string eventID;
			if (NKCAdjustManager.m_dungeonEnterEvent.TryGetValue(dungeonID, out eventID))
			{
				NKCAdjustManager.SendAdjustEvent(eventID);
			}
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x000ED060 File Offset: 0x000EB260
		public static void OnClearDungeon(int dungeonID)
		{
			if (!NKCAdjustManager.m_useAdjust)
			{
				return;
			}
			if (NKCAdjustManager.m_logAdjust)
			{
				Log.Debug(string.Format("[Adjust] OnClearDungeon dungeonID[{0}]", dungeonID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Adjust/NKCAdjustManager.cs", 679);
			}
			string eventID;
			if (NKCAdjustManager.m_dungeonClearEvent.TryGetValue(dungeonID, out eventID))
			{
				NKCAdjustManager.SendAdjustEvent(eventID);
			}
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x000ED0B0 File Offset: 0x000EB2B0
		public static void SendAdjustEvent(string eventID)
		{
			string eventCodeByEventID = NKCAdjustManager.GetEventCodeByEventID(eventID);
			if (!string.IsNullOrEmpty(eventCodeByEventID) && NKCAdjustManager.m_logAdjust)
			{
				Log.Debug(string.Concat(new string[]
				{
					"[Adjust] SendAdjustEvent eventID[",
					eventID,
					"] resultCode[",
					eventCodeByEventID,
					"]"
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Adjust/NKCAdjustManager.cs", 695);
			}
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x000ED110 File Offset: 0x000EB310
		public static void OnTrackPurchase(ShopItemTemplet productTemplet)
		{
			if (!NKCAdjustManager.m_useAdjust)
			{
				return;
			}
			if (productTemplet == null)
			{
				return;
			}
			if (NKCAdjustManager.m_logAdjust)
			{
				Log.Debug(string.Format("[Adjust] OnTrackPurchase ProductID[{0}] PriceItemID[{1}]", productTemplet.m_ProductID, productTemplet.m_PriceItemID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Adjust/NKCAdjustManager.cs", 718);
			}
			if (productTemplet.m_PriceItemID == 0)
			{
				double localPrice = decimal.ToDouble(NKCPublisherModule.InAppPurchase.GetLocalPrice(productTemplet.m_MarketID, productTemplet.m_ProductID));
				string priceCurrency = NKCPublisherModule.InAppPurchase.GetPriceCurrency(productTemplet.m_MarketID, productTemplet.m_ProductID);
				NKCAdjustManager.SendAdjustRevenueEvent("37_inapp_purchase", localPrice, priceCurrency);
			}
			string eventID;
			if (NKCAdjustManager.m_cashItemPurchaseEvent.TryGetValue(productTemplet.m_ProductID, out eventID))
			{
				NKCAdjustManager.SendAdjustEvent(eventID);
			}
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x000ED1CC File Offset: 0x000EB3CC
		public static void SendAdjustRevenueEvent(string eventID, double localPrice, string priceCurrency)
		{
			string eventCodeByEventID = NKCAdjustManager.GetEventCodeByEventID(eventID);
			if (!string.IsNullOrEmpty(eventCodeByEventID) && NKCAdjustManager.m_logAdjust)
			{
				Log.Debug(string.Concat(new string[]
				{
					"[Adjust] SendAdjustRevenueEvent eventID[",
					eventID,
					"] resultCode[",
					eventCodeByEventID,
					"]"
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Adjust/NKCAdjustManager.cs", 749);
			}
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x000ED229 File Offset: 0x000EB429
		public static void OnHandlePaidEventAdmob(long paidValue, string currencyCode)
		{
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x000ED22B File Offset: 0x000EB42B
		public static void HandleGooglePlayId(string adId)
		{
			Log.Debug("Google Play Ad ID = " + adId, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Adjust/NKCAdjustManager.cs", 778);
		}

		// Token: 0x04002F95 RID: 12181
		private static bool m_useAdjust = false;

		// Token: 0x04002F96 RID: 12182
		private static bool m_logAdjust = false;

		// Token: 0x04002F97 RID: 12183
		private static bool m_adjustStarted = false;

		// Token: 0x04002F98 RID: 12184
		private static Dictionary<string, string> m_eventIDToEventCodeList = new Dictionary<string, string>();

		// Token: 0x04002F99 RID: 12185
		private static Dictionary<int, string> m_playCutSceneEvent = new Dictionary<int, string>();

		// Token: 0x04002F9A RID: 12186
		private static Dictionary<int, string> m_warFareResultEvent = new Dictionary<int, string>();

		// Token: 0x04002F9B RID: 12187
		private static Dictionary<int, string> m_playTutorialEvent = new Dictionary<int, string>();

		// Token: 0x04002F9C RID: 12188
		private static Dictionary<int, string> m_dungeonEnterEvent = new Dictionary<int, string>();

		// Token: 0x04002F9D RID: 12189
		private static Dictionary<int, string> m_dungeonClearEvent = new Dictionary<int, string>();

		// Token: 0x04002F9E RID: 12190
		private static Dictionary<int, string> m_cashItemPurchaseEvent = new Dictionary<int, string>();
	}
}
