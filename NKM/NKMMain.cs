using System;

namespace NKM
{
	// Token: 0x02000428 RID: 1064
	public static class NKMMain
	{
		// Token: 0x06001CF9 RID: 7417 RVA: 0x0008708B File Offset: 0x0008528B
		public static NKM_ERROR_CODE IsValidDeck(NKMArmyData cNKMArmyData, NKM_DECK_TYPE selectDeckType, byte selectDeckIndex)
		{
			return NKMMain.IsValidDeck(cNKMArmyData, new NKMDeckIndex(selectDeckType, (int)selectDeckIndex));
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x0008709A File Offset: 0x0008529A
		public static NKM_ERROR_CODE IsValidDeck(NKMArmyData cNKMArmyData, NKM_DECK_TYPE selectDeckType, byte selectDeckIndex, NKM_GAME_TYPE gameType)
		{
			return NKMMain.IsValidDeck(cNKMArmyData, new NKMDeckIndex(selectDeckType, (int)selectDeckIndex), gameType);
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x000870AC File Offset: 0x000852AC
		public static NKM_ERROR_CODE IsValidDeck(NKMArmyData cNKMArmyData, NKMDeckIndex selectDeckIndex)
		{
			NKMDeckData deckData = cNKMArmyData.GetDeckData(selectDeckIndex);
			if (deckData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_DECK_DATA_INVALID;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = deckData.IsValidState();
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				return nkm_ERROR_CODE;
			}
			nkm_ERROR_CODE = NKMMain.IsValidDeckCommon(cNKMArmyData, deckData, selectDeckIndex, NKM_GAME_TYPE.NGT_INVALID);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				return nkm_ERROR_CODE;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x000870E4 File Offset: 0x000852E4
		public static NKM_ERROR_CODE IsValidDeck(NKMArmyData cNKMArmyData, NKMDeckIndex selectDeckIndex, NKM_GAME_TYPE gameType)
		{
			NKMDeckData deckData = cNKMArmyData.GetDeckData(selectDeckIndex);
			if (deckData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_DECK_DATA_INVALID;
			}
			if (!deckData.IsValidGame(gameType))
			{
				return NKM_ERROR_CODE.NEC_FAIL_DECK_INVALID_GAME_TYPE;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = NKMMain.IsValidDeckCommon(cNKMArmyData, deckData, selectDeckIndex, gameType);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				return nkm_ERROR_CODE;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x0008711C File Offset: 0x0008531C
		public static NKM_ERROR_CODE IsValidDeckCommon(NKMArmyData cNKMArmyData, NKMDeckData cNKMDeckData, NKMDeckIndex selectDeckIndex, NKM_GAME_TYPE gameType)
		{
			if (!cNKMArmyData.IsValidDeckIndex(selectDeckIndex))
			{
				return NKM_ERROR_CODE.NEC_FAIL_SELECT_DECK_INDEX_INVALID;
			}
			if (cNKMDeckData.CheckHasDuplicateUnit(cNKMArmyData))
			{
				return NKM_ERROR_CODE.NEC_FAIL_DECK_DUPLICATE_UNIT;
			}
			NKMUnitData shipFromUID = cNKMArmyData.GetShipFromUID(cNKMDeckData.m_ShipUID);
			if (shipFromUID == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_DECK_NO_SHIP;
			}
			if (shipFromUID.IsSeized)
			{
				return NKM_ERROR_CODE.NEC_FAIL_SEIZED_SHIP_IN_DECK;
			}
			int num = 0;
			foreach (long unitUid in cNKMDeckData.m_listDeckUnitUID)
			{
				NKMUnitData unitFromUID = cNKMArmyData.GetUnitFromUID(unitUid);
				if (unitFromUID != null)
				{
					if (unitFromUID.IsSeized)
					{
						return NKM_ERROR_CODE.NEC_FAIL_SEIZED_UNIT_IN_DECK;
					}
					num++;
				}
			}
			if (num == 0)
			{
				return NKM_ERROR_CODE.NEC_FAIL_DECK_NOT_ENOUGH_UNIT_COUNT;
			}
			if (NKMGame.IsPVP(gameType) && num != 8)
			{
				return NKM_ERROR_CODE.NEC_FAIL_DECK_NOT_ENOUGH_UNIT_COUNT;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x000871E0 File Offset: 0x000853E0
		public static float GetAttackCostRateByLimitBreakLevel(short limitBreakLevel)
		{
			if (limitBreakLevel <= 0)
			{
				return 1f;
			}
			if (limitBreakLevel == 1)
			{
				return 1.6f;
			}
			if (limitBreakLevel == 2)
			{
				return 2f;
			}
			return 2.5f;
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x00087208 File Offset: 0x00085408
		public static bool IsBusyDeck(NKMDeckData deckData)
		{
			NKM_DECK_STATE state = deckData.GetState();
			return state - NKM_DECK_STATE.DECK_STATE_WORLDMAP_MISSION <= 2;
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x00087228 File Offset: 0x00085428
		public static string IsTutorialDungeon(int dungeonId)
		{
			switch (dungeonId)
			{
			case 1004:
				return "stage_1";
			case 1005:
				return "stage_2";
			case 1006:
				return "stage_3";
			case 1007:
				return "stage_4";
			default:
				switch (dungeonId)
				{
				case 20001:
					return "stage_5";
				case 20002:
					return "stage_6";
				case 20003:
					return "stage_7";
				case 20004:
					return "stage_8";
				case 20005:
					return "stage_9";
				default:
					return string.Empty;
				}
				break;
			}
		}

		// Token: 0x04001BF1 RID: 7153
		public const int RESET_TIME = 4;

		// Token: 0x04001BF2 RID: 7154
		public const byte DECK_UNIT_COUNT = 8;

		// Token: 0x04001BF3 RID: 7155
		public const byte RAID_DECK_UNIT_COUNT = 16;

		// Token: 0x04001BF4 RID: 7156
		public const byte LEAGUE_PVP_DECK_UNIT_COUNT = 9;

		// Token: 0x04001BF5 RID: 7157
		public const byte LEAGUE_PVP_GLOBAL_BAN_UNIT_COUNT = 2;

		// Token: 0x04001BF6 RID: 7158
		public const int MAXIMUM_CONTRACT_SLOT = 10;

		// Token: 0x04001BF7 RID: 7159
		public const short MAXIMUM_LIMIT_BREAK_COUNT = 3;

		// Token: 0x04001BF8 RID: 7160
		public const short MAXIMUM_TRANSCENDENCE_COUNT = 8;

		// Token: 0x04001BF9 RID: 7161
		public const int CASH_COST_UNLOCK_CONTRACT_SLOT = 300;

		// Token: 0x04001BFA RID: 7162
		public const int CASH_COST_UNLOCK_SHIP_BUILDING_SLOT = 400;

		// Token: 0x04001BFB RID: 7163
		public const int CASH_COST_UNLOCK_ARMY_SLOT = 600;

		// Token: 0x04001BFC RID: 7164
		public const int MAX_SKILL_COUNT_PER_UNIT = 5;

		// Token: 0x04001BFD RID: 7165
		public const int MAX_SKILL_COUNT_PER_SHIP = 3;

		// Token: 0x04001BFE RID: 7166
		public const int MAX_COUNT_UNIT_SKILL_STAT = 5;

		// Token: 0x04001BFF RID: 7167
		public const string TOUCH_UI_RES_NAME = "AB_fx_ui_touch";

		// Token: 0x04001C00 RID: 7168
		public const int IMI_CREDIT = 1;

		// Token: 0x04001C01 RID: 7169
		public const int IMI_ETERNIUM = 2;

		// Token: 0x04001C02 RID: 7170
		public const int IMI_INFORMATION = 3;

		// Token: 0x04001C03 RID: 7171
		public const int IMI_DAILY_TICKET = 4;

		// Token: 0x04001C04 RID: 7172
		public const int IMI_PVP_POINT = 5;

		// Token: 0x04001C05 RID: 7173
		public const int IMI_PVP_CHARGE_POINT = 6;

		// Token: 0x04001C06 RID: 7174
		public const int IMI_CLOTHES_COUPON = 7;

		// Token: 0x04001C07 RID: 7175
		public const int IMI_ITEM_MISC_PARTNERS_BUSINESS_CARD = 8;

		// Token: 0x04001C08 RID: 7176
		public const int IMI_PVP_CHARGE_POINT_FOR_PRACTICE = 9;

		// Token: 0x04001C09 RID: 7177
		public const int IMI_DIVE_POINT = 11;

		// Token: 0x04001C0A RID: 7178
		public const int IMI_PVP_SEASON_POINT = 12;

		// Token: 0x04001C0B RID: 7179
		public const int IMI_ASYNC_PVP_TICKET = 13;

		// Token: 0x04001C0C RID: 7180
		public const int IMI_DAILY_TICKET_A = 15;

		// Token: 0x04001C0D RID: 7181
		public const int IMI_DAILY_TICKET_B = 16;

		// Token: 0x04001C0E RID: 7182
		public const int IMI_DAILY_TICKET_C = 17;

		// Token: 0x04001C0F RID: 7183
		public const int IMI_CONTRIBUTION_MEDAL = 18;

		// Token: 0x04001C10 RID: 7184
		public const int IMI_SHADOW_TICKET = 19;

		// Token: 0x04001C11 RID: 7185
		public const int IMI_WAR_CLOUD_SHADOW_PIECE = 20;

		// Token: 0x04001C12 RID: 7186
		public const int IMI_GUILD_POINT = 21;

		// Token: 0x04001C13 RID: 7187
		public const int IMI_GUILD_WELFARE_POINT = 23;

		// Token: 0x04001C14 RID: 7188
		public const int IMI_GUILD_UNION_POINT = 24;

		// Token: 0x04001C15 RID: 7189
		public const int IMI_FIERCE_POINT = 25;

		// Token: 0x04001C16 RID: 7190
		public const int IMI_CHALLENGE_TICKET = 26;

		// Token: 0x04001C17 RID: 7191
		public const int IMI_QUARTZ = 101;

		// Token: 0x04001C18 RID: 7192
		public const int IMI_PACKAGE_MEDAL = 102;

		// Token: 0x04001C19 RID: 7193
		public const int IMI_REPEAT_POINT = 201;

		// Token: 0x04001C1A RID: 7194
		public const int IMI_ACHIEVE_POINT = 202;

		// Token: 0x04001C1B RID: 7195
		public const int IMI_DAILY_REPEAT_POINT = 203;

		// Token: 0x04001C1C RID: 7196
		public const int IMI_WEEKLY_REPEAT_POINT = 204;

		// Token: 0x04001C1D RID: 7197
		public const int IMI_PVP_CHARGE_POINT_X2 = 301;

		// Token: 0x04001C1E RID: 7198
		public const int IMI_SPECIAL_CONTRACT_POINT = 401;

		// Token: 0x04001C1F RID: 7199
		public const int IMI_USER_EXP = 501;

		// Token: 0x04001C20 RID: 7200
		public const int IMI_UNIT_EXP = 502;

		// Token: 0x04001C21 RID: 7201
		public const int IMI_GUILD_EXP = 503;

		// Token: 0x04001C22 RID: 7202
		public const int IMI_EVENT_PASS_EXP = 504;

		// Token: 0x04001C23 RID: 7203
		public const int IMI_EVENT_POINT_01 = 601;

		// Token: 0x04001C24 RID: 7204
		public const int IMI_2020_VALENTINE_TICKET = 602;

		// Token: 0x04001C25 RID: 7205
		public const int IMI_2020_VALENTINE_CHOCOBALL = 603;

		// Token: 0x04001C26 RID: 7206
		public const int IMI_RUSTY_DOG_TAG = 613;

		// Token: 0x04001C27 RID: 7207
		public const int IMI_2021_HORIZON = 636;

		// Token: 0x04001C28 RID: 7208
		public const int IMI_ITEM_MISC_RANDOM_UNIT = 901;

		// Token: 0x04001C29 RID: 7209
		public const int IMI_ITEM_MISC_RANDOM_WEAPON = 902;

		// Token: 0x04001C2A RID: 7210
		public const int IMI_ITEM_MISC_RANDOM_MISC = 903;

		// Token: 0x04001C2B RID: 7211
		public const int IMI_ITEM_MISC_RANDOM_MOLD = 904;

		// Token: 0x04001C2C RID: 7212
		public const int IMI_UNIT_LEVEL = 910;

		// Token: 0x04001C2D RID: 7213
		public const int IMI_BUILD_POINT = 911;

		// Token: 0x04001C2E RID: 7214
		public const int IMI_LIMITBREAK = 912;

		// Token: 0x04001C2F RID: 7215
		public const int IMI_ITEM_MISC_UNIT_SKILL_TRAING_1 = 1018;

		// Token: 0x04001C30 RID: 7216
		public const int IMI_ITEM_MISC_UNIT_SKILL_TRAING_2 = 1019;

		// Token: 0x04001C31 RID: 7217
		public const int IMI_ITEM_MISC_UNIT_NEGOTIATE_DOC_1 = 1031;

		// Token: 0x04001C32 RID: 7218
		public const int IMI_ITEM_MISC_UNIT_NEGOTIATE_DOC_2 = 1032;

		// Token: 0x04001C33 RID: 7219
		public const int IMI_ITEM_MISC_UNIT_NEGOTIATE_DOC_3 = 1033;

		// Token: 0x04001C34 RID: 7220
		public const int IMI_ITEM_MISC_CONTRACT_DOC_1 = 1001;

		// Token: 0x04001C35 RID: 7221
		public const int IMI_ITEM_MISC_CONTRACT_SPECIAL = 1021;

		// Token: 0x04001C36 RID: 7222
		public const int IMI_ITEM_MISC_UNIT_REMOVE_CARD = 1022;

		// Token: 0x04001C37 RID: 7223
		public const int IMI_ITEM_MISC_SHIP_REMOVE_CARD = 1023;

		// Token: 0x04001C38 RID: 7224
		public const int IMI_ITEM_MISC_CONTRACT_DOC_CLASSIFIED = 1034;

		// Token: 0x04001C39 RID: 7225
		public const int IMI_ITEM_MISC_DIVE_COORD_INITIALIZE = 1041;

		// Token: 0x04001C3A RID: 7226
		public const int IMI_ITEM_MISC_DIVE_COORD_INITIALIZE_FORCE = 1042;

		// Token: 0x04001C3B RID: 7227
		public const int INAPP_PURCHASE_ITEM_ID = 0;

		// Token: 0x04001C3C RID: 7228
		public const int IMI_ITEM_MISC_MAKE_INSTANT = 1012;

		// Token: 0x04001C3D RID: 7229
		public const int IMI_ITEM_MISC_MAKE_INSTANT_COST_CNT = 1;

		// Token: 0x04001C3E RID: 7230
		public const int IMI_ITEM_MISC_TUNING_MATERIAL = 1013;

		// Token: 0x04001C3F RID: 7231
		public const int IMI_ITEM_MISC_SET_MATERIAL = 1035;

		// Token: 0x04001C40 RID: 7232
		public const int MAX_COUNT_NEW_DAILY_TICKET_FREE = 2;

		// Token: 0x04001C41 RID: 7233
		public const int DAILY_TICKET_PRODUCT_ID = 40103;

		// Token: 0x04001C42 RID: 7234
		public const int DAILY_TICKET_A_PRODUCT_ID = 40121;

		// Token: 0x04001C43 RID: 7235
		public const int DAILY_TICKET_B_PRODUCT_ID = 40122;

		// Token: 0x04001C44 RID: 7236
		public const int DAILY_TICKET_C_PRODUCT_ID = 40123;

		// Token: 0x04001C45 RID: 7237
		public const int MAX_FRIEND_ADD_COUNT = 60;

		// Token: 0x04001C46 RID: 7238
		public const int MAX_FRIEND_REQ_COUNT = 50;

		// Token: 0x04001C47 RID: 7239
		public const int MAX_FRIEND_RES_COUNT = 50;

		// Token: 0x04001C48 RID: 7240
		public const int MAX_FRIEND_BAN_COUNT = 30;

		// Token: 0x04001C49 RID: 7241
		public const int MAX_FRIEND_DEL_COUNT_PER_DAY = 10;

		// Token: 0x04001C4A RID: 7242
		public const int MAX_FRIEND_INTRO_LENGTH = 20;

		// Token: 0x04001C4B RID: 7243
		public const int FRIEND_BOX_PRODUCT_ID = 6011;

		// Token: 0x04001C4C RID: 7244
		public const int NKM_COUNTER_CASE_NORMAL_EP_ID = 50;

		// Token: 0x04001C4D RID: 7245
		public const int NKM_COUNTER_CASE_SECRET_EP_ID = 51;

		// Token: 0x04001C4E RID: 7246
		public const int MAX_EPISODE_DIFFICULTY_COUNT = 2;

		// Token: 0x04001C4F RID: 7247
		public const int MAX_EPISODE_COMPLETE_REWARD = 3;

		// Token: 0x04001C50 RID: 7248
		public const int MAX_EPISODE_MISSION_MEDAL_COUNT = 3;

		// Token: 0x04001C51 RID: 7249
		public const int DID_TUTORIAL_1 = 1004;

		// Token: 0x04001C52 RID: 7250
		public const int DID_TUTORIAL_2 = 1005;

		// Token: 0x04001C53 RID: 7251
		public const int DID_TUTORIAL_3 = 1006;

		// Token: 0x04001C54 RID: 7252
		public const int DID_TUTORIAL_4 = 1007;

		// Token: 0x04001C55 RID: 7253
		public const int TUTORIAL_1_STAGE_ID = 11211;

		// Token: 0x04001C56 RID: 7254
		public const int TUTORIAL_2_STAGE_ID = 11212;

		// Token: 0x04001C57 RID: 7255
		public const int TUTORIAL_3_STAGE_ID = 11213;

		// Token: 0x04001C58 RID: 7256
		public const int TUTORIAL_4_STAGE_ID = 11214;

		// Token: 0x04001C59 RID: 7257
		public const int DID_TRAINING_1 = 20001;

		// Token: 0x04001C5A RID: 7258
		public const int DID_TRAINING_2 = 20002;

		// Token: 0x04001C5B RID: 7259
		public const int DID_TRAINING_3 = 20003;

		// Token: 0x04001C5C RID: 7260
		public const int DID_TRAINING_4 = 20004;

		// Token: 0x04001C5D RID: 7261
		public const int DID_TRAINING_5 = 20005;

		// Token: 0x04001C5E RID: 7262
		public const int MISSION_GROWTH01 = 6;

		// Token: 0x04001C5F RID: 7263
		public const int MISSION_GROWTH02 = 7;

		// Token: 0x04001C60 RID: 7264
		public const int MISSION_GROWTH03 = 8;

		// Token: 0x04001C61 RID: 7265
		public const int SHADOW_PALACE_TICKET = 19;

		// Token: 0x04001C62 RID: 7266
		public const int SHADOW_PALACE_TICKET_MAX_COUNT = 3;

		// Token: 0x04001C63 RID: 7267
		public const int SHADOW_PALACE_COIN = 20;

		// Token: 0x04001C64 RID: 7268
		public const int SHADOW_UNLOCK_DUNGEON_ID = 10514;

		// Token: 0x04001C65 RID: 7269
		public static readonly int[] excludeUnitID = new int[]
		{
			21001,
			22001,
			23001,
			24001,
			25001,
			26001,
			21022,
			22022,
			23022,
			24022,
			25022,
			26022
		};
	}
}
