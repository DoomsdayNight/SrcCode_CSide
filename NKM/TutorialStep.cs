using System;

namespace NKM
{
	// Token: 0x02000481 RID: 1153
	public enum TutorialStep
	{
		// Token: 0x04001FE7 RID: 8167
		None,
		// Token: 0x04001FE8 RID: 8168
		MainstreamGuide = 100,
		// Token: 0x04001FE9 RID: 8169
		NextMission = 105,
		// Token: 0x04001FEA RID: 8170
		WarfarePlay = 110,
		// Token: 0x04001FEB RID: 8171
		WarfarePlaySimple,
		// Token: 0x04001FEC RID: 8172
		Achieventment = 120,
		// Token: 0x04001FED RID: 8173
		AchieventmentTaskPlan = 124,
		// Token: 0x04001FEE RID: 8174
		DeckSetup = 130,
		// Token: 0x04001FEF RID: 8175
		SecondDeckSetup = 140,
		// Token: 0x04001FF0 RID: 8176
		WarfareAssist = 145,
		// Token: 0x04001FF1 RID: 8177
		WarfareAssistSimple,
		// Token: 0x04001FF2 RID: 8178
		DeckSetupUnit = 150,
		// Token: 0x04001FF3 RID: 8179
		CounterCase = 170,
		// Token: 0x04001FF4 RID: 8180
		CounterCaseEnd = 175,
		// Token: 0x04001FF5 RID: 8181
		Contract = 180,
		// Token: 0x04001FF6 RID: 8182
		SecondContract = 190,
		// Token: 0x04001FF7 RID: 8183
		ThirdContract = 195,
		// Token: 0x04001FF8 RID: 8184
		HangerBuild = 200,
		// Token: 0x04001FF9 RID: 8185
		HangerShipyard = 210,
		// Token: 0x04001FFA RID: 8186
		HangerBreakup = 220,
		// Token: 0x04001FFB RID: 8187
		Base = 230,
		// Token: 0x04001FFC RID: 8188
		LabEnhance = 240,
		// Token: 0x04001FFD RID: 8189
		LabSkill = 250,
		// Token: 0x04001FFE RID: 8190
		LabLimitBreak = 260,
		// Token: 0x04001FFF RID: 8191
		TacticUpdate = 266,
		// Token: 0x04002000 RID: 8192
		HRNegotiate = 270,
		// Token: 0x04002001 RID: 8193
		HRDismissal = 280,
		// Token: 0x04002002 RID: 8194
		WorldMap = 290,
		// Token: 0x04002003 RID: 8195
		Gauntlet = 300,
		// Token: 0x04002004 RID: 8196
		GauntletAsync = 305,
		// Token: 0x04002005 RID: 8197
		GauntletLeague = 308,
		// Token: 0x04002006 RID: 8198
		FactoryCraft = 310,
		// Token: 0x04002007 RID: 8199
		FactoryUpgrade = 315,
		// Token: 0x04002008 RID: 8200
		FactoryEnchant = 320,
		// Token: 0x04002009 RID: 8201
		FactoryHiddenOption = 325,
		// Token: 0x0400200A RID: 8202
		FactoryBreakup = 330,
		// Token: 0x0400200B RID: 8203
		Daily = 340,
		// Token: 0x0400200C RID: 8204
		DailyAdd = 345,
		// Token: 0x0400200D RID: 8205
		ThirdDeckSetup = 350,
		// Token: 0x0400200E RID: 8206
		WarfareSupply = 360,
		// Token: 0x0400200F RID: 8207
		WarfareSupplySimple,
		// Token: 0x04002010 RID: 8208
		WarfareSupplySecond = 370,
		// Token: 0x04002011 RID: 8209
		WarfareSupplySecondSimple,
		// Token: 0x04002012 RID: 8210
		TutorialNextStep1 = 380,
		// Token: 0x04002013 RID: 8211
		TutorialNextStep2 = 390,
		// Token: 0x04002014 RID: 8212
		Training1 = 410,
		// Token: 0x04002015 RID: 8213
		Training2 = 420,
		// Token: 0x04002016 RID: 8214
		Training3 = 430,
		// Token: 0x04002017 RID: 8215
		Training4 = 440,
		// Token: 0x04002018 RID: 8216
		Training5 = 450,
		// Token: 0x04002019 RID: 8217
		DeckSetupLobby = 460,
		// Token: 0x0400201A RID: 8218
		WarfareWin = 470,
		// Token: 0x0400201B RID: 8219
		WarfareWinSimple,
		// Token: 0x0400201C RID: 8220
		RechargeFund = 480,
		// Token: 0x0400201D RID: 8221
		FriendSquad = 490,
		// Token: 0x0400201E RID: 8222
		DiveEvent = 500,
		// Token: 0x0400201F RID: 8223
		RaidEvent = 510,
		// Token: 0x04002020 RID: 8224
		RaidEventCheck = 515,
		// Token: 0x04002021 RID: 8225
		FactoryTuning = 520,
		// Token: 0x04002022 RID: 8226
		Shop = 530,
		// Token: 0x04002023 RID: 8227
		LabLimitBreakUnit = 540,
		// Token: 0x04002024 RID: 8228
		ShadowPalace = 550,
		// Token: 0x04002025 RID: 8229
		ShadowBattle = 555,
		// Token: 0x04002026 RID: 8230
		WorldmapBuilding = 560,
		// Token: 0x04002027 RID: 8231
		Consortium1 = 570,
		// Token: 0x04002028 RID: 8232
		Consortium2 = 580,
		// Token: 0x04002029 RID: 8233
		FierceLobby = 600,
		// Token: 0x0400202A RID: 8234
		SupplyGuide = 610,
		// Token: 0x0400202B RID: 8235
		OperatorContract = 620,
		// Token: 0x0400202C RID: 8236
		OperatorSetup = 630,
		// Token: 0x0400202D RID: 8237
		OperatorInfo = 640,
		// Token: 0x0400202E RID: 8238
		OperatorEnhance = 645,
		// Token: 0x0400202F RID: 8239
		OfficeRearm = 650,
		// Token: 0x04002030 RID: 8240
		OfficeExtract = 655,
		// Token: 0x04002031 RID: 8241
		DimensionTrim = 660,
		// Token: 0x04002032 RID: 8242
		DimensionTrimLobby,
		// Token: 0x04002033 RID: 8243
		Challenge = 710,
		// Token: 0x04002034 RID: 8244
		OfficeBase = 720,
		// Token: 0x04002035 RID: 8245
		OfficePersonnal = 730,
		// Token: 0x04002036 RID: 8246
		OfficeTerraBrain = 740,
		// Token: 0x04002037 RID: 8247
		OfficeLab = 750,
		// Token: 0x04002038 RID: 8248
		OfficeFactory = 760,
		// Token: 0x04002039 RID: 8249
		OfficeShipyard = 770,
		// Token: 0x0400203A RID: 8250
		OfficeDormitory = 780,
		// Token: 0x0400203B RID: 8251
		OfficeRoom = 790,
		// Token: 0x0400203C RID: 8252
		OfficeParty = 795,
		// Token: 0x0400203D RID: 8253
		EVENT_001 = 801,
		// Token: 0x0400203E RID: 8254
		EVENT_002,
		// Token: 0x0400203F RID: 8255
		EVENT_003,
		// Token: 0x04002040 RID: 8256
		LobbyInfo = 810,
		// Token: 0x04002041 RID: 8257
		OperationInfo = 820,
		// Token: 0x04002042 RID: 8258
		FriendInfo = 830,
		// Token: 0x04002043 RID: 8259
		NewbieContract = 850,
		// Token: 0x04002044 RID: 8260
		ShipLimitBreak = 860,
		// Token: 0x04002045 RID: 8261
		ShipModule = 865,
		// Token: 0x04002046 RID: 8262
		NicknameChange = 999
	}
}
