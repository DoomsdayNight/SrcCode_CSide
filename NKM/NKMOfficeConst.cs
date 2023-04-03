using System;
using NKM.Templet.Base;
using NKM.Templet.Office;

namespace NKM
{
	// Token: 0x02000447 RID: 1095
	public sealed class NKMOfficeConst
	{
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06001DC6 RID: 7622 RVA: 0x0008DF96 File Offset: 0x0008C196
		// (set) Token: 0x06001DC7 RID: 7623 RVA: 0x0008DF9E File Offset: 0x0008C19E
		public NKMOfficeInteriorTemplet DefaultBackgroundItem { get; private set; } = NKMOfficeInteriorTemplet.Invalid;

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06001DC8 RID: 7624 RVA: 0x0008DFA7 File Offset: 0x0008C1A7
		// (set) Token: 0x06001DC9 RID: 7625 RVA: 0x0008DFAF File Offset: 0x0008C1AF
		public NKMOfficeInteriorTemplet DefaultWallItem { get; private set; } = NKMOfficeInteriorTemplet.Invalid;

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06001DCA RID: 7626 RVA: 0x0008DFB8 File Offset: 0x0008C1B8
		// (set) Token: 0x06001DCB RID: 7627 RVA: 0x0008DFC0 File Offset: 0x0008C1C0
		public NKMOfficeInteriorTemplet DefaultFloorItem { get; private set; } = NKMOfficeInteriorTemplet.Invalid;

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06001DCC RID: 7628 RVA: 0x0008DFC9 File Offset: 0x0008C1C9
		// (set) Token: 0x06001DCD RID: 7629 RVA: 0x0008DFD1 File Offset: 0x0008C1D1
		public NKMItemMiscTemplet PartyUseItem { get; private set; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06001DCE RID: 7630 RVA: 0x0008DFDA File Offset: 0x0008C1DA
		public int PartyUseItemMaxRefillCount
		{
			get
			{
				return this.partyMaxRefillCount;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06001DCF RID: 7631 RVA: 0x0008DFE2 File Offset: 0x0008C1E2
		public int OfficeNamingLimit
		{
			get
			{
				return this.officeNamingLimit;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06001DD0 RID: 7632 RVA: 0x0008DFEA File Offset: 0x0008C1EA
		public int MaxPostCountPerPage
		{
			get
			{
				return 50;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06001DD1 RID: 7633 RVA: 0x0008DFEE File Offset: 0x0008C1EE
		public NKMOfficeConst.NameCardConst NameCard { get; } = new NKMOfficeConst.NameCardConst();

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06001DD2 RID: 7634 RVA: 0x0008DFF6 File Offset: 0x0008C1F6
		public NKMOfficeConst.OfficePresetConst PresetConst { get; } = new NKMOfficeConst.OfficePresetConst();

		// Token: 0x06001DD3 RID: 7635 RVA: 0x0008E000 File Offset: 0x0008C200
		public void Load(NKMLua lua)
		{
			using (lua.OpenTable("Office", "[OfficeConst] loading Office table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMOfficeConst.cs", 32))
			{
				this.backgroundItemId = lua.GetInt32("OfficeDefaultBackground");
				this.wallItemId = lua.GetInt32("OfficeDefaultWall");
				this.floorItemId = lua.GetInt32("OfficeDefaultFloor");
				this.officeNamingLimit = lua.GetInt32("OfficeNamingLimit");
				this.NameCard.Load(lua);
				this.OfficeInteraction.Load(lua);
				this.PresetConst.Load(lua);
				using (lua.OpenTable("OfficeParty", "[OfficeConst] open subTable 'OfficeParty' failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMOfficeConst.cs", 42))
				{
					this.partyUseResourceId = lua.GetInt32("UseResourceId");
					this.partyMaxRefillCount = lua.GetInt32("MaxCount");
				}
			}
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x0008E0FC File Offset: 0x0008C2FC
		public void Join()
		{
			this.DefaultBackgroundItem = NKMOfficeInteriorTemplet.Find(this.backgroundItemId);
			if (this.DefaultBackgroundItem == null)
			{
				NKMTempletError.Add(string.Format("[Office] 기본 배경 아이템 아이디가 유효하지 않음: {0}", this.backgroundItemId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMOfficeConst.cs", 55);
			}
			this.DefaultWallItem = NKMOfficeInteriorTemplet.Find(this.wallItemId);
			if (this.DefaultWallItem == null)
			{
				NKMTempletError.Add(string.Format("[Office] 기본 벽지 아이템 아이디가 유효하지 않음: {0}", this.wallItemId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMOfficeConst.cs", 61);
			}
			this.DefaultFloorItem = NKMOfficeInteriorTemplet.Find(this.floorItemId);
			if (this.DefaultFloorItem == null)
			{
				NKMTempletError.Add(string.Format("[Office] 기본 바닥 아이템 아이디가 유효하지 않음: {0}", this.floorItemId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMOfficeConst.cs", 67);
			}
			this.NameCard.Join();
			this.PartyUseItem = NKMItemMiscTemplet.Find(this.partyUseResourceId);
			if (this.PartyUseItem == null)
			{
				NKMTempletError.Add(string.Format("[Office] 회식 사용 아이템 아이디가 유효하지 않음: {0}", this.partyUseResourceId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMOfficeConst.cs", 75);
			}
			this.PresetConst.Join();
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x0008E207 File Offset: 0x0008C407
		public void Validate()
		{
			this.NameCard.Validate();
			this.PresetConst.Validate();
		}

		// Token: 0x04001E19 RID: 7705
		private int backgroundItemId;

		// Token: 0x04001E1A RID: 7706
		private int wallItemId;

		// Token: 0x04001E1B RID: 7707
		private int floorItemId;

		// Token: 0x04001E1C RID: 7708
		private int officeNamingLimit;

		// Token: 0x04001E1D RID: 7709
		private int partyUseResourceId;

		// Token: 0x04001E1E RID: 7710
		private int partyMaxRefillCount;

		// Token: 0x04001E25 RID: 7717
		public NKMOfficeConst.OfficeInteractionConst OfficeInteraction = new NKMOfficeConst.OfficeInteractionConst();

		// Token: 0x04001E26 RID: 7718
		public const int NAMECARD_SEND_DAILY_LIMIT = 5;

		// Token: 0x04001E27 RID: 7719
		public const float OFFICE_INTERACTION_CHECK_TIME = 1f;

		// Token: 0x04001E28 RID: 7720
		public const int OFFICE_START_INTERACTION_PROBABILITY = 100;

		// Token: 0x020011F5 RID: 4597
		public sealed class NameCardConst
		{
			// Token: 0x1700179E RID: 6046
			// (get) Token: 0x0600A118 RID: 41240 RVA: 0x0033F341 File Offset: 0x0033D541
			// (set) Token: 0x0600A119 RID: 41241 RVA: 0x0033F349 File Offset: 0x0033D549
			public NKMItemMiscTemplet ItemTemplet { get; private set; }

			// Token: 0x1700179F RID: 6047
			// (get) Token: 0x0600A11A RID: 41242 RVA: 0x0033F352 File Offset: 0x0033D552
			// (set) Token: 0x0600A11B RID: 41243 RVA: 0x0033F35A File Offset: 0x0033D55A
			public int ItemValue { get; private set; }

			// Token: 0x170017A0 RID: 6048
			// (get) Token: 0x0600A11C RID: 41244 RVA: 0x0033F363 File Offset: 0x0033D563
			// (set) Token: 0x0600A11D RID: 41245 RVA: 0x0033F36B File Offset: 0x0033D56B
			public int DailyLimit { get; private set; }

			// Token: 0x0600A11E RID: 41246 RVA: 0x0033F374 File Offset: 0x0033D574
			public void Load(NKMLua lua)
			{
				using (lua.OpenTable("OfficeHostNameCard", "[OfficeConst] loading OfficeHostNameCard table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMOfficeConst.cs", 96))
				{
					this.itemId = lua.GetInt32("ItemId");
					this.ItemValue = lua.GetInt32("ItemValue");
					this.DailyLimit = lua.GetInt32("DayLimit");
				}
			}

			// Token: 0x0600A11F RID: 41247 RVA: 0x0033F3E8 File Offset: 0x0033D5E8
			public void Join()
			{
				this.ItemTemplet = NKMItemMiscTemplet.Find(this.itemId);
				if (this.ItemTemplet == null)
				{
					NKMTempletError.Add(string.Format("[Office] 명함 수령시 교환 아이템 아이디가 유효하지 않음: {0}", this.itemId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMOfficeConst.cs", 109);
				}
			}

			// Token: 0x0600A120 RID: 41248 RVA: 0x0033F424 File Offset: 0x0033D624
			public void Validate()
			{
				if (this.ItemValue <= 0)
				{
					NKMTempletError.Add(string.Format("[Office] 명함 수령시 교환 아이템 개수가 유효하지 않음: {0}", this.ItemValue), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMOfficeConst.cs", 117);
				}
				if (this.DailyLimit <= 0 || this.DailyLimit > 100)
				{
					NKMTempletError.Add(string.Format("[Office] 수령 가능 명함 일일 제한 수량이 유효하지 않음: {0}", this.DailyLimit), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMOfficeConst.cs", 122);
				}
			}

			// Token: 0x040093F7 RID: 37879
			private int itemId;
		}

		// Token: 0x020011F6 RID: 4598
		public sealed class OfficeInteractionConst
		{
			// Token: 0x170017A1 RID: 6049
			// (get) Token: 0x0600A122 RID: 41250 RVA: 0x0033F497 File Offset: 0x0033D697
			// (set) Token: 0x0600A123 RID: 41251 RVA: 0x0033F49F File Offset: 0x0033D69F
			public int ActInteriorRatePercent { get; private set; }

			// Token: 0x170017A2 RID: 6050
			// (get) Token: 0x0600A124 RID: 41252 RVA: 0x0033F4A8 File Offset: 0x0033D6A8
			// (set) Token: 0x0600A125 RID: 41253 RVA: 0x0033F4B0 File Offset: 0x0033D6B0
			public float ActInteriorCoolTime { get; private set; }

			// Token: 0x170017A3 RID: 6051
			// (get) Token: 0x0600A126 RID: 41254 RVA: 0x0033F4B9 File Offset: 0x0033D6B9
			// (set) Token: 0x0600A127 RID: 41255 RVA: 0x0033F4C1 File Offset: 0x0033D6C1
			public int ActUnitRatePercent { get; private set; }

			// Token: 0x170017A4 RID: 6052
			// (get) Token: 0x0600A128 RID: 41256 RVA: 0x0033F4CA File Offset: 0x0033D6CA
			// (set) Token: 0x0600A129 RID: 41257 RVA: 0x0033F4D2 File Offset: 0x0033D6D2
			public float ActUnitCoolTime { get; private set; }

			// Token: 0x170017A5 RID: 6053
			// (get) Token: 0x0600A12A RID: 41258 RVA: 0x0033F4DB File Offset: 0x0033D6DB
			// (set) Token: 0x0600A12B RID: 41259 RVA: 0x0033F4E3 File Offset: 0x0033D6E3
			public int ActSoloRatePercent { get; private set; }

			// Token: 0x170017A6 RID: 6054
			// (get) Token: 0x0600A12C RID: 41260 RVA: 0x0033F4EC File Offset: 0x0033D6EC
			// (set) Token: 0x0600A12D RID: 41261 RVA: 0x0033F4F4 File Offset: 0x0033D6F4
			public float ActSoloCoolTime { get; private set; }

			// Token: 0x170017A7 RID: 6055
			// (get) Token: 0x0600A12E RID: 41262 RVA: 0x0033F4FD File Offset: 0x0033D6FD
			// (set) Token: 0x0600A12F RID: 41263 RVA: 0x0033F505 File Offset: 0x0033D705
			public int RoomEnterActRatePercent { get; private set; }

			// Token: 0x0600A130 RID: 41264 RVA: 0x0033F510 File Offset: 0x0033D710
			public void Load(NKMLua lua)
			{
				using (lua.OpenTable("OfficeInteraction", "[OfficeConst] loading OfficeInteraction table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMOfficeConst.cs", 139))
				{
					this.ActInteriorRatePercent = lua.GetInt32("ActInteriorRatePercent");
					this.ActInteriorCoolTime = lua.GetFloat("ActInteriorCoolTime");
					this.ActUnitRatePercent = lua.GetInt32("ActUnitRatePercent");
					this.ActUnitCoolTime = lua.GetFloat("ActUnitCoolTime");
					this.ActSoloRatePercent = lua.GetInt32("ActSoloUnitRatePercent");
					this.ActSoloCoolTime = lua.GetFloat("ActSoloUnitCoolTime");
					this.RoomEnterActRatePercent = lua.GetInt32("RoomEnterActRatePercent");
				}
			}
		}

		// Token: 0x020011F7 RID: 4599
		public sealed class OfficePresetConst
		{
			// Token: 0x170017A8 RID: 6056
			// (get) Token: 0x0600A132 RID: 41266 RVA: 0x0033F5D4 File Offset: 0x0033D7D4
			public int BaseCount
			{
				get
				{
					return this.baseCount;
				}
			}

			// Token: 0x170017A9 RID: 6057
			// (get) Token: 0x0600A133 RID: 41267 RVA: 0x0033F5DC File Offset: 0x0033D7DC
			public int MaxNameLength
			{
				get
				{
					return 20;
				}
			}

			// Token: 0x170017AA RID: 6058
			// (get) Token: 0x0600A134 RID: 41268 RVA: 0x0033F5E0 File Offset: 0x0033D7E0
			// (set) Token: 0x0600A135 RID: 41269 RVA: 0x0033F5E8 File Offset: 0x0033D7E8
			public NKMItemMiscTemplet ExpandCostItem { get; private set; }

			// Token: 0x170017AB RID: 6059
			// (get) Token: 0x0600A136 RID: 41270 RVA: 0x0033F5F1 File Offset: 0x0033D7F1
			public int MaxCount
			{
				get
				{
					return this.maxCount;
				}
			}

			// Token: 0x170017AC RID: 6060
			// (get) Token: 0x0600A137 RID: 41271 RVA: 0x0033F5F9 File Offset: 0x0033D7F9
			public int ExpandCostValue
			{
				get
				{
					return this.expandCostValue;
				}
			}

			// Token: 0x0600A138 RID: 41272 RVA: 0x0033F604 File Offset: 0x0033D804
			public void Load(NKMLua lua)
			{
				using (lua.OpenTable("OfficeUserPreset", "[OfficeConst] open subTable 'OfficeUserPreset' failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMOfficeConst.cs", 167))
				{
					this.baseCount = lua.GetInt32("FREE_PRESET");
					this.maxCount = lua.GetInt32("MAX_PRESET");
					this.expandCostValue = lua.GetInt32("PRESET_PRICE_QUARTZ");
				}
			}

			// Token: 0x0600A139 RID: 41273 RVA: 0x0033F67C File Offset: 0x0033D87C
			public void Join()
			{
				this.ExpandCostItem = NKMItemMiscTemplet.Find(this.expandCostItemId);
				if (this.ExpandCostItem == null)
				{
					NKMTempletError.Add(string.Format("[Office] 기숙사 확장 아이템 아이디가 유효하지 않음: {0}", this.expandCostItemId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMOfficeConst.cs", 180);
				}
			}

			// Token: 0x0600A13A RID: 41274 RVA: 0x0033F6BB File Offset: 0x0033D8BB
			public void Validate()
			{
				if (this.expandCostValue <= 0)
				{
					NKMTempletError.Add(string.Format("[Office] 기숙사 확장 아이템 개수가 유효하지 않음: {0}", this.expandCostValue), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMOfficeConst.cs", 188);
				}
			}

			// Token: 0x04009402 RID: 37890
			private int baseCount;

			// Token: 0x04009403 RID: 37891
			private int maxCount;

			// Token: 0x04009404 RID: 37892
			private int expandCostValue;

			// Token: 0x04009405 RID: 37893
			private int expandCostItemId = 101;
		}
	}
}
