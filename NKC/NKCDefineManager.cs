using System;

namespace NKC
{
	// Token: 0x02000669 RID: 1641
	public class NKCDefineManager
	{
		// Token: 0x0600339F RID: 13215 RVA: 0x00104418 File Offset: 0x00102618
		public static bool DEFINE_SERVICE()
		{
			return true;
		}

		// Token: 0x060033A0 RID: 13216 RVA: 0x0010441B File Offset: 0x0010261B
		public static bool DEFINE_UNITY_STANDALONE()
		{
			return true;
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x0010441E File Offset: 0x0010261E
		public static bool DEFINE_UNITY_STANDALONE_WIN()
		{
			return true;
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x00104421 File Offset: 0x00102621
		public static bool DEFINE_FULL_BUILD()
		{
			return true;
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x00104424 File Offset: 0x00102624
		public static bool DEFINE_SEMI_FULL_BUILD()
		{
			return true;
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x00104427 File Offset: 0x00102627
		public static bool DEFINE_ANDROID()
		{
			return false;
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x0010442A File Offset: 0x0010262A
		public static bool DEFINE_IOS()
		{
			return false;
		}

		// Token: 0x060033A6 RID: 13222 RVA: 0x0010442D File Offset: 0x0010262D
		public static bool DEFINE_UNITY_DEBUG_LOG()
		{
			return true;
		}

		// Token: 0x060033A7 RID: 13223 RVA: 0x00104430 File Offset: 0x00102630
		public static bool DEFINE_UNITY_EDITOR()
		{
			return false;
		}

		// Token: 0x060033A8 RID: 13224 RVA: 0x00104433 File Offset: 0x00102633
		public static bool DEFINE_USE_CHEAT()
		{
			return false;
		}

		// Token: 0x060033A9 RID: 13225 RVA: 0x00104436 File Offset: 0x00102636
		public static bool DEFINE_USE_TOUCH_DELAY()
		{
			return false;
		}

		// Token: 0x060033AA RID: 13226 RVA: 0x00104439 File Offset: 0x00102639
		public static bool DEFINE_NO_CONSOLE_LOG()
		{
			return false;
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x0010443C File Offset: 0x0010263C
		public static bool DEFINE_ZLONG()
		{
			return false;
		}

		// Token: 0x060033AC RID: 13228 RVA: 0x0010443F File Offset: 0x0010263F
		public static bool DEFINE_CHECKVERSION()
		{
			return false;
		}

		// Token: 0x060033AD RID: 13229 RVA: 0x00104442 File Offset: 0x00102642
		public static bool DEFINE_ZLONG_SEA()
		{
			return false;
		}

		// Token: 0x060033AE RID: 13230 RVA: 0x00104445 File Offset: 0x00102645
		public static bool DEFINE_ZLONG_CHN()
		{
			return false;
		}

		// Token: 0x060033AF RID: 13231 RVA: 0x00104448 File Offset: 0x00102648
		public static bool DEFINE_OBB()
		{
			return false;
		}

		// Token: 0x060033B0 RID: 13232 RVA: 0x0010444B File Offset: 0x0010264B
		public static bool DEFINE_CAN_ONLY_LOAD_MIN_TEMPLET()
		{
			return !NKCDefineManager.DEFINE_SERVICE() || NKCDefineManager.DEFINE_ZLONG() || NKCDefineManager.DEFINE_SB_GB();
		}

		// Token: 0x060033B1 RID: 13233 RVA: 0x00104465 File Offset: 0x00102665
		public static bool DEFINE_NX_PC()
		{
			return false;
		}

		// Token: 0x060033B2 RID: 13234 RVA: 0x00104468 File Offset: 0x00102668
		public static bool DEFINE_NX_PC_TEST()
		{
			return false;
		}

		// Token: 0x060033B3 RID: 13235 RVA: 0x0010446B File Offset: 0x0010266B
		public static bool DEFINE_NX_PC_STAGE()
		{
			return false;
		}

		// Token: 0x060033B4 RID: 13236 RVA: 0x0010446E File Offset: 0x0010266E
		public static bool DEFINE_NX_PC_LIVE()
		{
			return false;
		}

		// Token: 0x060033B5 RID: 13237 RVA: 0x00104471 File Offset: 0x00102671
		public static bool DEFINE_WEBVIEW_TEST()
		{
			return false;
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x00104474 File Offset: 0x00102674
		public static bool DEFINE_DOWNLOAD_CONFIG()
		{
			return false;
		}

		// Token: 0x060033B7 RID: 13239 RVA: 0x00104477 File Offset: 0x00102677
		public static bool DEFINE_USE_CUSTOM_SERVERS()
		{
			return false;
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x0010447A File Offset: 0x0010267A
		public static bool DEFINE_PC_EXTRA_DOWNLOAD_IN_EXE_FOLDER()
		{
			return true;
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x0010447D File Offset: 0x0010267D
		public static bool DEFINE_EXTRA_ASSET()
		{
			return true;
		}

		// Token: 0x060033BA RID: 13242 RVA: 0x00104480 File Offset: 0x00102680
		public static bool DEFINE_PC_FORCE_VERSION_UP()
		{
			return false;
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x00104483 File Offset: 0x00102683
		public static bool DEFINE_SB_GB()
		{
			return false;
		}

		// Token: 0x060033BC RID: 13244 RVA: 0x00104486 File Offset: 0x00102686
		public static bool DEFINE_LB()
		{
			return false;
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x00104489 File Offset: 0x00102689
		public static bool DEFINE_NXTOY()
		{
			return false;
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x0010448C File Offset: 0x0010268C
		public static bool DEFINE_NXTOY_JP()
		{
			return false;
		}

		// Token: 0x060033BF RID: 13247 RVA: 0x0010448F File Offset: 0x0010268F
		public static bool DEFINE_ALLOW_MULTIPC()
		{
			return false;
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x00104492 File Offset: 0x00102692
		public static bool DEFINE_PATCH_SKIP()
		{
			return false;
		}

		// Token: 0x060033C1 RID: 13249 RVA: 0x00104495 File Offset: 0x00102695
		public static bool DEFINE_FBANALYTICS()
		{
			return false;
		}

		// Token: 0x060033C2 RID: 13250 RVA: 0x00104498 File Offset: 0x00102698
		public static bool DEFINE_USE_COMPILED_LUA()
		{
			return true;
		}

		// Token: 0x060033C3 RID: 13251 RVA: 0x0010449B File Offset: 0x0010269B
		public static bool DEFINE_USE_CONVERTED_FILENAME()
		{
			return true;
		}

		// Token: 0x060033C4 RID: 13252 RVA: 0x0010449E File Offset: 0x0010269E
		public static bool DEINFE_USE_CONVERTED_FILENAME_TO_UPPERCASE()
		{
			return false;
		}

		// Token: 0x060033C5 RID: 13253 RVA: 0x001044A1 File Offset: 0x001026A1
		public static bool DEFINE_STEAM()
		{
			return true;
		}

		// Token: 0x060033C6 RID: 13254 RVA: 0x001044A4 File Offset: 0x001026A4
		public static bool DEFINE_JPPC()
		{
			return false;
		}

		// Token: 0x060033C7 RID: 13255 RVA: 0x001044A7 File Offset: 0x001026A7
		public static bool DEFINE_ENCRYPTION_TEST()
		{
			return false;
		}

		// Token: 0x060033C8 RID: 13256 RVA: 0x001044AA File Offset: 0x001026AA
		public static bool DEFINE_SAVE_LOG()
		{
			return true;
		}

		// Token: 0x060033C9 RID: 13257 RVA: 0x001044AD File Offset: 0x001026AD
		public static bool DEFINE_PURE_LOG()
		{
			return false;
		}

		// Token: 0x060033CA RID: 13258 RVA: 0x001044B0 File Offset: 0x001026B0
		public static bool DEFINE_USE_DEV_SCRIPT()
		{
			return false;
		}

		// Token: 0x060033CB RID: 13259 RVA: 0x001044B3 File Offset: 0x001026B3
		public static bool DEFINE_ONESTORE()
		{
			return false;
		}

		// Token: 0x060033CC RID: 13260 RVA: 0x001044B6 File Offset: 0x001026B6
		public static bool DEFINE_CLIENT_KOR()
		{
			return false;
		}

		// Token: 0x060033CD RID: 13261 RVA: 0x001044B9 File Offset: 0x001026B9
		public static bool DEFINE_CLIENT_GBL()
		{
			return false;
		}

		// Token: 0x060033CE RID: 13262 RVA: 0x001044BC File Offset: 0x001026BC
		public static bool DEFINE_CLIENT_CHN()
		{
			return false;
		}

		// Token: 0x060033CF RID: 13263 RVA: 0x001044BF File Offset: 0x001026BF
		public static bool DEFINE_CLIENT_TWN()
		{
			return false;
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x001044C2 File Offset: 0x001026C2
		public static bool DEFINE_CLIENT_SEA()
		{
			return false;
		}

		// Token: 0x060033D1 RID: 13265 RVA: 0x001044C5 File Offset: 0x001026C5
		public static bool DEFINE_CLIENT_JPN()
		{
			return false;
		}

		// Token: 0x060033D2 RID: 13266 RVA: 0x001044C8 File Offset: 0x001026C8
		public static bool DEFINE_SELECT_SERVER()
		{
			return true;
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x001044CB File Offset: 0x001026CB
		public static bool DEFINE_PATCH_OPTIMIZATION()
		{
			return true;
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x001044CE File Offset: 0x001026CE
		public static bool DEFINE_GLOBALQA()
		{
			return false;
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x001044D1 File Offset: 0x001026D1
		public static bool USE_PATCHERCHECKER()
		{
			return true;
		}
	}
}
