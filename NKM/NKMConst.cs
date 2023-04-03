using System;
using System.Collections.Generic;
using Cs.GameLog.CountryDescription;
using Cs.Logging;
using NKM.Templet;

namespace NKM
{
	// Token: 0x020003B7 RID: 951
	public static class NKMConst
	{
		// Token: 0x060018FB RID: 6395 RVA: 0x0006651C File Offset: 0x0006471C
		public static void Validate()
		{
			if (NKMItemManager.GetItemMiscTempletByID(1001) == null)
			{
				Log.ErrorAndExit(string.Format("채용계약서 id가 올바르지 않음. id:{0}", 1001), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMConst.cs", 294);
			}
			if (NKMItemManager.GetItemMiscTempletByID(1024) == null)
			{
				Log.ErrorAndExit(string.Format("종신고용서 id가 올바르지 않음. id:{0}", 1024), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMConst.cs", 299);
			}
			if (NKMItemManager.GetItemMiscTempletByID(1002) == null)
			{
				Log.ErrorAndExit(string.Format("긴급채용쿠폰 id가 올바르지 않음. id:{0}", 1002), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMConst.cs", 304);
			}
			if (NKMItemManager.GetItemMiscTempletByID(401) == null)
			{
				Log.ErrorAndExit(string.Format("특별채용권 id가 올바르지 않음. id:{0}", 401), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMConst.cs", 309);
			}
			if (NKMItemManager.GetItemMiscTempletByID(1015) == null)
			{
				Log.ErrorAndExit(string.Format("함선건조권 id가 올바르지 않음. id:{0}", 1015), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMConst.cs", 314);
			}
			if (NKMItemManager.GetItemMiscTempletByID(510) == null)
			{
				Log.ErrorAndExit(string.Format("닉네임변경권 id가 올바르지 않음. id:{0}", 510), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMConst.cs", 319);
			}
			if (NKMItemManager.GetItemMiscTempletByID(13) == null)
			{
				Log.ErrorAndExit(string.Format("전략전 티켓 id가 올바르지 않음. id:{0}", 13), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMConst.cs", 324);
			}
		}

		// Token: 0x04001152 RID: 4434
		public const int MAX_AUTO_SUPPLY_ACCUMULATE_MINUTES = 480;

		// Token: 0x020011B1 RID: 4529
		public static class Deck
		{
			// Token: 0x040092EC RID: 37612
			public const int MaxUnitEnter = 8;
		}

		// Token: 0x020011B2 RID: 4530
		public static class Post
		{
			// Token: 0x040092ED RID: 37613
			public static readonly DateTime MaxExpirationUtcDate = new DateTime(3000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

			// Token: 0x040092EE RID: 37614
			public static readonly DateTime UnlimitedExpirationUtcDate = new DateTime(2100, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
		}

		// Token: 0x020011B3 RID: 4531
		public static class Negotiation
		{
			// Token: 0x040092EF RID: 37615
			public const int MaxMaterialCount = 3;
		}

		// Token: 0x020011B4 RID: 4532
		public static class Profile
		{
			// Token: 0x040092F0 RID: 37616
			public const int MaxEmblemCount = 3;
		}

		// Token: 0x020011B5 RID: 4533
		public static class Equip
		{
			// Token: 0x040092F1 RID: 37617
			public const int MaxPotentialOptionSocketCount = 3;
		}

		// Token: 0x020011B6 RID: 4534
		public static class Warfare
		{
			// Token: 0x040092F2 RID: 37618
			public const int DEFAULT_SUPPLY_COUNT = 2;

			// Token: 0x040092F3 RID: 37619
			public const int MAX_SUPPLY_COUNT = 2;

			// Token: 0x040092F4 RID: 37620
			public const int SERVICE_COST_ITEM_ID = 2;

			// Token: 0x040092F5 RID: 37621
			public const int EXPIRE_TIME = 12;

			// Token: 0x040092F6 RID: 37622
			public const int FriendshipPointItemId = 8;

			// Token: 0x040092F7 RID: 37623
			public const int RECOVERY_COST_MULTIPLE = 2;

			// Token: 0x040092F8 RID: 37624
			public const string MyFriendhipPointMailTitle = "SI_MAIL_ASSIST_ME_TITLE_V2";

			// Token: 0x040092F9 RID: 37625
			public const string MyFriendhipPointMailText = "SI_MAIL_ASSIST_ME_DESC_V2";

			// Token: 0x040092FA RID: 37626
			public const string FriendFriendhipPointMailTitle = "SI_MAIL_ASSIST_OTHER_TITLE_V2";

			// Token: 0x040092FB RID: 37627
			public const string FriendFriendhipPointMailText = "SI_MAIL_ASSIST_OTHER_DESC_V2";

			// Token: 0x040092FC RID: 37628
			public const string MyGuestUsageMailTitle = "SI_MAIL_AST_GUEST_ME_TITLE";

			// Token: 0x040092FD RID: 37629
			public const string MyGuestUsageMailText = "SI_MAIL_AST_GUEST_ME_DESC";

			// Token: 0x040092FE RID: 37630
			public const string GuestUsageMailTitle = "SI_MAIL_AST_GUEST_OTHER_TITLE";

			// Token: 0x040092FF RID: 37631
			public const string GuestUsageMailText = "SI_MAIL_AST_GUEST_OTHER_DESC";
		}

		// Token: 0x020011B7 RID: 4535
		public static class Episode
		{
			// Token: 0x04009300 RID: 37632
			public const int DAILYMISSION_ATTACK = 101;

			// Token: 0x04009301 RID: 37633
			public const int DAILYMISSION_SEARCH = 102;

			// Token: 0x04009302 RID: 37634
			public const int DAILYMISSION_DEFENCE = 103;

			// Token: 0x04009303 RID: 37635
			public const int DAILYMISSION_TACTICAL = 104;

			// Token: 0x04009304 RID: 37636
			public const int MAX_ONE_TIME_REWARD_COUNT = 3;
		}

		// Token: 0x020011B8 RID: 4536
		public static class Raid
		{
			// Token: 0x04009305 RID: 37637
			public const int MAX_RAID_RESULT_COUNT = 99;

			// Token: 0x04009306 RID: 37638
			public const int MAX_RAID_BUFF_LEVEL = 5;
		}

		// Token: 0x020011B9 RID: 4537
		public static class Craft
		{
			// Token: 0x04009307 RID: 37639
			public const int CASH_COST_UNLOCK_SLOT = 300;

			// Token: 0x04009308 RID: 37640
			public const int MAX_CRAFT_START_COUNT = 10;
		}

		// Token: 0x020011BA RID: 4538
		public static class Dungeon
		{
			// Token: 0x04009309 RID: 37641
			public const int DEFAULT_SUPPLY = 2;
		}

		// Token: 0x020011BB RID: 4539
		public static class Worldmap
		{
			// Token: 0x0400930A RID: 37642
			public static readonly List<int> CITY_OPEN_CASH_COST = new List<int>
			{
				0,
				800,
				2400,
				4500,
				8000,
				12500
			};

			// Token: 0x0400930B RID: 37643
			public static readonly List<int> CITY_OPEN_CREDIT_COST = new List<int>
			{
				0,
				100000,
				200000,
				400000,
				800000,
				1600000
			};

			// Token: 0x0400930C RID: 37644
			public static readonly int RaidBuildingId = 21;

			// Token: 0x0400930D RID: 37645
			public static readonly int DiveBuildingId = 22;
		}

		// Token: 0x020011BC RID: 4540
		public static class Unit
		{
			// Token: 0x0400930E RID: 37646
			public const int LoyaltyMax = 10000;

			// Token: 0x0400930F RID: 37647
			public const int PermanentContractDocumentId = 1024;

			// Token: 0x04009310 RID: 37648
			public const int ContractDocumentId = 1001;

			// Token: 0x04009311 RID: 37649
			public const int ContractInstantCouponId = 1002;

			// Token: 0x04009312 RID: 37650
			public const int ContractMilesageItemId = 401;

			// Token: 0x04009313 RID: 37651
			public const int ShipContractDoc = 1015;

			// Token: 0x04009314 RID: 37652
			public const float BonusExpRateOfPermanentContract = 0.2f;

			// Token: 0x04009315 RID: 37653
			public const int MaxLevel = 100;
		}

		// Token: 0x020011BD RID: 4541
		public static class Contract
		{
			// Token: 0x04009316 RID: 37654
			public const int SelectableContractSlotCount = 10;
		}

		// Token: 0x020011BE RID: 4542
		public static class Ship
		{
			// Token: 0x04009317 RID: 37655
			public const int MaxUpgradeLevel = 6;

			// Token: 0x04009318 RID: 37656
			public static readonly List<int> CoffinIds = new List<int>
			{
				21001,
				22001,
				23001,
				24001,
				25001,
				26001
			};
		}

		// Token: 0x020011BF RID: 4543
		public static class OperationPowerFactor
		{
			// Token: 0x0600A06F RID: 41071 RVA: 0x0033E078 File Offset: 0x0033C278
			public static int GetClassValue(NKM_UNIT_ROLE_TYPE unitRoleType)
			{
				switch (unitRoleType)
				{
				case NKM_UNIT_ROLE_TYPE.NURT_STRIKER:
					return 850;
				case NKM_UNIT_ROLE_TYPE.NURT_RANGER:
					return 850;
				case NKM_UNIT_ROLE_TYPE.NURT_DEFENDER:
					return 900;
				case NKM_UNIT_ROLE_TYPE.NURT_SNIPER:
					return 900;
				case NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER:
					return 800;
				case NKM_UNIT_ROLE_TYPE.NURT_SIEGE:
					return 700;
				case NKM_UNIT_ROLE_TYPE.NURT_TOWER:
					return 700;
				default:
					throw new InvalidOperationException("invalid unitRoleType");
				}
			}

			// Token: 0x0600A070 RID: 41072 RVA: 0x0033E0E0 File Offset: 0x0033C2E0
			public static int GetGradeValue(NKM_UNIT_GRADE unitGrade, bool bAwaken, bool bRearm)
			{
				if (bAwaken)
				{
					if (unitGrade == NKM_UNIT_GRADE.NUG_SSR)
					{
						return 10075;
					}
				}
				else if (bRearm)
				{
					if (unitGrade == NKM_UNIT_GRADE.NUG_SR)
					{
						return 9275;
					}
					if (unitGrade == NKM_UNIT_GRADE.NUG_SSR)
					{
						return 9675;
					}
				}
				else
				{
					switch (unitGrade)
					{
					case NKM_UNIT_GRADE.NUG_N:
						return 8600;
					case NKM_UNIT_GRADE.NUG_R:
						return 8850;
					case NKM_UNIT_GRADE.NUG_SR:
						return 9125;
					case NKM_UNIT_GRADE.NUG_SSR:
						return 9500;
					}
				}
				throw new InvalidOperationException("invalid unitGrade");
			}

			// Token: 0x0600A071 RID: 41073 RVA: 0x0033E14C File Offset: 0x0033C34C
			public static int GetTacticValue(NKM_UNIT_GRADE unitGrade, bool bAwaken, bool bRearm)
			{
				if (bAwaken)
				{
					if (unitGrade == NKM_UNIT_GRADE.NUG_SSR)
					{
						return 400;
					}
				}
				else if (bRearm)
				{
					if (unitGrade == NKM_UNIT_GRADE.NUG_SR)
					{
						return 340;
					}
					if (unitGrade == NKM_UNIT_GRADE.NUG_SSR)
					{
						return 380;
					}
				}
				else
				{
					switch (unitGrade)
					{
					case NKM_UNIT_GRADE.NUG_N:
						return 280;
					case NKM_UNIT_GRADE.NUG_R:
						return 300;
					case NKM_UNIT_GRADE.NUG_SR:
						return 320;
					case NKM_UNIT_GRADE.NUG_SSR:
						return 360;
					}
				}
				throw new InvalidOperationException("invalid unitGrade");
			}

			// Token: 0x04009319 RID: 37657
			public const int ClassValueOfSniper = 900;

			// Token: 0x0400931A RID: 37658
			public const int ClassValueOfDefender = 900;

			// Token: 0x0400931B RID: 37659
			public const int ClassValueOfStriker = 850;

			// Token: 0x0400931C RID: 37660
			public const int ClassValueOfRanger = 850;

			// Token: 0x0400931D RID: 37661
			public const int ClassValueOfSuppoter = 800;

			// Token: 0x0400931E RID: 37662
			public const int ClassValueOfTower = 700;

			// Token: 0x0400931F RID: 37663
			public const int ClasValueOfSiege = 700;

			// Token: 0x04009320 RID: 37664
			public const int GradeValueOfAwakenSSR = 10075;

			// Token: 0x04009321 RID: 37665
			public const int GradeValueOfRearmSSR = 9675;

			// Token: 0x04009322 RID: 37666
			public const int GradeValueOfSSR = 9500;

			// Token: 0x04009323 RID: 37667
			public const int GradeValueOfRearmSR = 9275;

			// Token: 0x04009324 RID: 37668
			public const int GradeValueOfSR = 9125;

			// Token: 0x04009325 RID: 37669
			public const int GradeValueOfR = 8850;

			// Token: 0x04009326 RID: 37670
			public const int GradeValueOfN = 8600;

			// Token: 0x04009327 RID: 37671
			public const float AdjustmentFactorOfUnit = 0.6f;

			// Token: 0x04009328 RID: 37672
			public const float AdjustmentFactorOfEquip = 0.5f;

			// Token: 0x04009329 RID: 37673
			public const int TacticValueOfAwakenSSR = 400;

			// Token: 0x0400932A RID: 37674
			public const int TacticValueOfRearmSSR = 380;

			// Token: 0x0400932B RID: 37675
			public const int TacticValueOfSSR = 360;

			// Token: 0x0400932C RID: 37676
			public const int TacticValueOfRearmSR = 340;

			// Token: 0x0400932D RID: 37677
			public const int TacticValueOfSR = 320;

			// Token: 0x0400932E RID: 37678
			public const int TacticValueOfR = 300;

			// Token: 0x0400932F RID: 37679
			public const int TacticValueOfN = 280;
		}

		// Token: 0x020011C0 RID: 4544
		public static class RandomShop
		{
			// Token: 0x04009330 RID: 37680
			public const int MaxSlotCount = 9;

			// Token: 0x04009331 RID: 37681
			public const int RefreshCost = 15;

			// Token: 0x04009332 RID: 37682
			public const int RefreshMaxCountPerDay = 5;
		}

		// Token: 0x020011C1 RID: 4545
		public static class Account
		{
			// Token: 0x04009333 RID: 37683
			public const int NicknameChangeItemId = 510;
		}

		// Token: 0x020011C2 RID: 4546
		public static class ServerString
		{
			// Token: 0x0600A072 RID: 41074 RVA: 0x0033E1B7 File Offset: 0x0033C3B7
			public static string BuildMiscId(int itemId)
			{
				return string.Format("{0}{1}", "<MiscId>", itemId);
			}

			// Token: 0x0600A073 RID: 41075 RVA: 0x0033E1CE File Offset: 0x0033C3CE
			public static string BuildMoldId(int itemId)
			{
				return string.Format("{0}{1}", "<MoldId>", itemId);
			}

			// Token: 0x04009334 RID: 37684
			public const string Seperator = "@@";

			// Token: 0x04009335 RID: 37685
			public const string GuildBanId = "<GuildBanId>";

			// Token: 0x04009336 RID: 37686
			public const string MiscId = "<MiscId>";

			// Token: 0x04009337 RID: 37687
			public const string EquipId = "<EquipId>";

			// Token: 0x04009338 RID: 37688
			public const string MoldId = "<MoldId>";
		}

		// Token: 0x020011C3 RID: 4547
		public static class UserLevelUp
		{
			// Token: 0x04009339 RID: 37689
			public const string UserLevelUpPostTitle = "SI_MAIL_ACCOUNT_LEVEL_UP_REWARD_TITLE";

			// Token: 0x0400933A RID: 37690
			public const string UserLevelUpPostDesc = "SI_MAIL_ACCOUNT_LEVEL_UP_REWARD_DESC";
		}

		// Token: 0x020011C4 RID: 4548
		public static class Buff
		{
			// Token: 0x02001A4C RID: 6732
			public enum BuffType
			{
				// Token: 0x0400AE25 RID: 44581
				[CountryDescription("", CountryCode.KOR)]
				NONE,
				// Token: 0x0400AE26 RID: 44582
				[CountryDescription("전역, 던전 크레딧 보상 증가", CountryCode.KOR)]
				WARFARE_DUNGEON_REWARD_CREDIT,
				// Token: 0x0400AE27 RID: 44583
				[CountryDescription("전역, 던전 유닛 경험치 증가", CountryCode.KOR)]
				WARFARE_DUNGEON_REWARD_EXP_UNIT,
				// Token: 0x0400AE28 RID: 44584
				[CountryDescription("전역, 던전 회사 경험치 증가", CountryCode.KOR)]
				WARFARE_DUNGEON_REWARD_EXP_COMPANY,
				// Token: 0x0400AE29 RID: 44585
				[CountryDescription("전역 입장 이터니움 비용 감소", CountryCode.KOR)]
				WARFARE_ETERNIUM_DISCOUNT,
				// Token: 0x0400AE2A RID: 44586
				[CountryDescription("전역, 던전 입장 이터니움 비용 감소", CountryCode.KOR)]
				WARFARE_DUNGEON_ETERNIUM_DISCOUNT,
				// Token: 0x0400AE2B RID: 44587
				[CountryDescription("랭크전 건틀렛 시간당 포인트 획득량 증가", CountryCode.KOR)]
				PVP_POINT_CHARGE,
				// Token: 0x0400AE2C RID: 44588
				[CountryDescription("모든 건틀렛 포인트 보상 증가", CountryCode.KOR)]
				ALL_PVP_POINT_REWARD,
				// Token: 0x0400AE2D RID: 44589
				[CountryDescription("월드맵 미션 성공률 증가", CountryCode.KOR)]
				WORLDMAP_MISSION_COMPLETE_RATIO_BONUS,
				// Token: 0x0400AE2E RID: 44590
				[CountryDescription("연봉협상 시 크레딧 재화의 소모량 감소", CountryCode.KOR)]
				BASE_PERSONNAL_NEGOTIATION_CREDIT_DISCOUNT,
				// Token: 0x0400AE2F RID: 44591
				[CountryDescription("장비 제작 시 크레딧 재화의 소모량 감소", CountryCode.KOR)]
				BASE_FACTORY_CRAFT_CREDIT_DISCOUNT,
				// Token: 0x0400AE30 RID: 44592
				[CountryDescription("장비 강화, 튜닝 시 크레딧 재화의 소모량 감소", CountryCode.KOR)]
				BASE_FACTORY_ENCHANT_TUNING_CREDIT_DISCOUNT
			}
		}

		// Token: 0x020011C5 RID: 4549
		public static class Background
		{
			// Token: 0x0400933B RID: 37691
			public const int defaultID = 9001;

			// Token: 0x0400933C RID: 37692
			public const string defaultPrefab = "AB_UI_BG_SPRITE_CITY_NIGHT";

			// Token: 0x0400933D RID: 37693
			public const string FollowLobby = "FOLLOW_LOBBY";

			// Token: 0x0400933E RID: 37694
			public const int MaxBackgroundUnitCount = 6;
		}

		// Token: 0x020011C6 RID: 4550
		public static class ShadowPalace
		{
			// Token: 0x0400933F RID: 37695
			public const int dummyUnitID = 999;

			// Token: 0x04009340 RID: 37696
			public const int dummyShipID = 26001;

			// Token: 0x04009341 RID: 37697
			public const string ShadowPlaceTag = "UNLOCK_SHADOW_PALACE_ON";
		}

		// Token: 0x020011C7 RID: 4551
		public static class Operator
		{
			// Token: 0x04009342 RID: 37698
			public const string OperatorTag = "OPERATOR";
		}
	}
}
