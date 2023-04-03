using System;
using System.Collections.Generic;
using System.Linq;
using NKM.Contract2;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x02000467 RID: 1127
	public sealed class NKMShipLimitBreakTemplet : INKMTemplet
	{
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06001E8A RID: 7818 RVA: 0x00090E9E File Offset: 0x0008F09E
		// (set) Token: 0x06001E8B RID: 7819 RVA: 0x00090EA6 File Offset: 0x0008F0A6
		public NKMUnitTempletBase BaseShipTemplet { get; private set; }

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06001E8C RID: 7820 RVA: 0x00090EAF File Offset: 0x0008F0AF
		public int Key
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06001E8D RID: 7821 RVA: 0x00090EB7 File Offset: 0x0008F0B7
		public bool EnableByTag
		{
			get
			{
				return NKMOpenTagManager.IsOpened(this.openTag);
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06001E8E RID: 7822 RVA: 0x00090EC4 File Offset: 0x0008F0C4
		public int ShipId
		{
			get
			{
				return this.shipId;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x00090ECC File Offset: 0x0008F0CC
		public NKM_UNIT_TYPE UnitType
		{
			get
			{
				return this.unitType;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06001E90 RID: 7824 RVA: 0x00090ED4 File Offset: 0x0008F0D4
		public int ShipLimitBreakMaxLevel
		{
			get
			{
				return this.shipLimitBreakMaxLevel;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06001E91 RID: 7825 RVA: 0x00090EDC File Offset: 0x0008F0DC
		public List<int> ListMaterialShipId
		{
			get
			{
				return this.listMaterialShipId;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06001E92 RID: 7826 RVA: 0x00090EE4 File Offset: 0x0008F0E4
		public int ShipLimitBreakGrade
		{
			get
			{
				return this.shipLimitBreakGrade;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06001E93 RID: 7827 RVA: 0x00090EEC File Offset: 0x0008F0EC
		public List<MiscItemUnit> ShipLimitBreakItems
		{
			get
			{
				return this.shipLimitBreakItems;
			}
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x00090EF4 File Offset: 0x0008F0F4
		public static NKMShipLimitBreakTemplet LoadFromLUA(NKMLua lua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(lua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 398))
			{
				return null;
			}
			NKMShipLimitBreakTemplet nkmshipLimitBreakTemplet = new NKMShipLimitBreakTemplet
			{
				id = lua.GetInt32("ID"),
				openTag = lua.GetString("OpenTag"),
				shipId = lua.GetInt32("ShipID"),
				shipLimitBreakGrade = lua.GetInt32("ShipLimitBreakGrade"),
				shipLimitBreakMaxLevel = lua.GetInt32("ShipLimitBreakMaxLevel")
			};
			lua.GetDataEnum<NKM_UNIT_TYPE>("UnitType", out nkmshipLimitBreakTemplet.unitType);
			for (int i = 0; i < NKMCommonConst.ShipLimitBreakItemCount; i++)
			{
				bool flag = true;
				int itemId = -1;
				int num = -1;
				if (flag & lua.GetData(string.Format("ShipLimitBreakItemID{0}", i + 1), ref itemId) & lua.GetData(string.Format("ShipLimitBreakItemValue{0}", i + 1), ref num))
				{
					nkmshipLimitBreakTemplet.shipLimitBreakItems.Add(new MiscItemUnit(itemId, (long)num));
				}
			}
			if (lua.OpenTable("ListMaterialShipID"))
			{
				nkmshipLimitBreakTemplet.listMaterialShipId = new List<int>();
				int num2 = 1;
				int item = 0;
				while (lua.GetData(num2, ref item))
				{
					nkmshipLimitBreakTemplet.listMaterialShipId.Add(item);
					num2++;
				}
				lua.CloseTable();
			}
			return nkmshipLimitBreakTemplet;
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x0009102C File Offset: 0x0008F22C
		public void Join()
		{
			this.BaseShipTemplet = NKMTempletContainer<NKMUnitTempletBase>.Find(this.shipId);
			if (this.BaseShipTemplet == null)
			{
				NKMTempletError.Add(string.Format("[ShipLimitBreak:{0}] �߸��� �Լ� Id. shipId:{1}", this.shipId, this.shipId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 454);
			}
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x00091084 File Offset: 0x0008F284
		public void Validate()
		{
			if (this.unitType != NKM_UNIT_TYPE.NUT_SHIP)
			{
				NKMTempletError.Add(string.Format("[ShipLimitBreak:{0}] �߸��� ���� Ÿ��. unitType:{1}", this.shipId, this.unitType), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 462);
			}
			if (this.shipLimitBreakGrade < 1)
			{
				NKMTempletError.Add(string.Format("[ShipLimitBreak:{0}] �Լ� �̽� ���� �ܰ谡 ������. shipLimitBreakGrade:{1}", this.shipId, this.shipLimitBreakGrade), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 466);
			}
			if (this.shipLimitBreakMaxLevel < 0)
			{
				NKMTempletError.Add(string.Format("[ShipLimitBreak:{0}] �Լ� �̽� ���� �ִ� ������ ������. shipLimitBreakMaxLevel:{1}", this.shipId, this.shipLimitBreakMaxLevel), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 471);
			}
			if (!this.shipLimitBreakItems.Any<MiscItemUnit>())
			{
				NKMTempletError.Add(string.Format("[ShipLimitBreak:{0}] �Լ� �̽� ���� ��� ������ ����", this.shipId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 476);
			}
			this.shipLimitBreakItems.ForEach(delegate(MiscItemUnit s)
			{
				s.Join("/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 479);
			});
			int[] array = (from e in this.shipLimitBreakItems
			group e by e.ItemId into e
			where e.Count<MiscItemUnit>() > 1
			select e.Key).ToArray<int>();
			if (array.Any<int>())
			{
				string arg = string.Join<int>(", ", array);
				NKMTempletError.Add(string.Format("[ShipLimitBreak:{0}] ��� ������ �� �ߺ��Ǵ� �׸��� ����. duplicateItemId:{1}", this.shipId, arg), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 489);
			}
			foreach (int num in this.listMaterialShipId)
			{
				NKMUnitTempletBase nkmunitTempletBase = NKMTempletContainer<NKMUnitTempletBase>.Find(num);
				if (nkmunitTempletBase == null)
				{
					NKMTempletError.Add(string.Format("[ShipLimitBreak:{0}] �߸��� ��� �Լ� Id. materialShipId:{1}", this.shipId, num), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 497);
				}
				if (nkmunitTempletBase.m_ShipGroupID != this.BaseShipTemplet.m_ShipGroupID)
				{
					NKMTempletError.Add(string.Format("[ShipLimitBreak:{0}] ��� �Լ��� �׷� Id�� �ٸ�. shipId:{1} baseShipGruipId:{2} materialShipId:{3} materialShipGroupID:{4}", new object[]
					{
						this.shipId,
						this.shipId,
						this.BaseShipTemplet.m_ShipGroupID,
						num,
						nkmunitTempletBase.m_ShipGroupID
					}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 502);
				}
			}
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x0009133C File Offset: 0x0008F53C
		public bool ValidateConsumeShipId(int shipId)
		{
			return this.listMaterialShipId.Contains(shipId);
		}

		// Token: 0x04001F1C RID: 7964
		private int id;

		// Token: 0x04001F1D RID: 7965
		private NKM_UNIT_TYPE unitType;

		// Token: 0x04001F1E RID: 7966
		private int shipId;

		// Token: 0x04001F1F RID: 7967
		private int shipLimitBreakGrade;

		// Token: 0x04001F20 RID: 7968
		private int shipLimitBreakMaxLevel;

		// Token: 0x04001F21 RID: 7969
		private List<int> listMaterialShipId;

		// Token: 0x04001F22 RID: 7970
		private List<MiscItemUnit> shipLimitBreakItems = new List<MiscItemUnit>(NKMCommonConst.ShipLimitBreakItemCount);

		// Token: 0x04001F23 RID: 7971
		private string openTag;
	}
}
