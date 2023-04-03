using System;
using System.Collections.Generic;
using System.Linq;
using NKM.Contract2;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x02000468 RID: 1128
	public sealed class NKMShipCommandModuleTemplet : INKMTemplet
	{
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06001E99 RID: 7833 RVA: 0x00091362 File Offset: 0x0008F562
		public int Key
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001E9A RID: 7834 RVA: 0x0009136A File Offset: 0x0008F56A
		public bool EnableByTag
		{
			get
			{
				return NKMOpenTagManager.IsOpened(this.openTag);
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001E9B RID: 7835 RVA: 0x00091377 File Offset: 0x0008F577
		public int Slot1Id
		{
			get
			{
				return this.commandModuleSlot1Id;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06001E9C RID: 7836 RVA: 0x0009137F File Offset: 0x0008F57F
		public int Slot2Id
		{
			get
			{
				return this.commandModuleSlot2Id;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06001E9D RID: 7837 RVA: 0x00091387 File Offset: 0x0008F587
		public int LimitBreakGrade
		{
			get
			{
				return this.shipLimitBreakGrade;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06001E9E RID: 7838 RVA: 0x0009138F File Offset: 0x0008F58F
		public IReadOnlyList<MiscItemUnit> ModuleLockItems
		{
			get
			{
				return this.moduleLockItems.ToList<MiscItemUnit>();
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06001E9F RID: 7839 RVA: 0x0009139C File Offset: 0x0008F59C
		public IReadOnlyList<MiscItemUnit> ModuleReqItems
		{
			get
			{
				return this.shipModuleReqItem;
			}
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x000913A4 File Offset: 0x0008F5A4
		public static NKMShipCommandModuleTemplet LoadFromLUA(NKMLua lua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(lua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 537))
			{
				return null;
			}
			NKMShipCommandModuleTemplet nkmshipCommandModuleTemplet = new NKMShipCommandModuleTemplet
			{
				id = lua.GetInt32("ID"),
				openTag = lua.GetString("OpenTag"),
				shipLimitBreakGrade = lua.GetInt32("ShipLimitBreakGrade"),
				commandModuleSlot1Id = lua.GetInt32("CommandModuleSlot1"),
				commandModuleSlot2Id = lua.GetInt32("CommandModuleSlot2")
			};
			lua.GetDataEnum<NKM_UNIT_STYLE_TYPE>("ShipType", out nkmshipCommandModuleTemplet.shipType);
			lua.GetDataEnum<NKM_UNIT_GRADE>("ShipGrade", out nkmshipCommandModuleTemplet.shipGrade);
			for (int i = 0; i < 2; i++)
			{
				bool flag = true;
				int itemId = -1;
				int num = -1;
				if (flag & lua.GetData(string.Format("ModuleSlot{0}_LockItemID", i + 1), ref itemId) & lua.GetData(string.Format("ModuleSlot{0}_LockItemValue", i + 1), ref num))
				{
					nkmshipCommandModuleTemplet.moduleLockItems[i] = new MiscItemUnit(itemId, (long)num);
				}
			}
			for (int j = 0; j < NKMCommonConst.ShipModuleReqItemCount; j++)
			{
				bool flag2 = true;
				int itemId2 = -1;
				int num2 = -1;
				if (flag2 & lua.GetData(string.Format("ModuleReqItemID{0}", j + 1), ref itemId2) & lua.GetData(string.Format("ModuleReqItemValue{0}", j + 1), ref num2))
				{
					nkmshipCommandModuleTemplet.shipModuleReqItem.Add(new MiscItemUnit(itemId2, (long)num2));
				}
			}
			return nkmshipCommandModuleTemplet;
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x00091508 File Offset: 0x0008F708
		public void Join()
		{
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x0009150C File Offset: 0x0008F70C
		public void Validate()
		{
			if (this.shipGrade < NKM_UNIT_GRADE.NUG_N || this.shipGrade >= NKM_UNIT_GRADE.NUG_COUNT)
			{
				NKMTempletError.Add(string.Format("[NKMShipCommandModuleTemplet:{0}] Ÿ�� �Լ� ����� �ùٸ��� ����.  shipGrade:{1}", this.id, this.shipGrade), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 597);
			}
			if (this.shipType < NKM_UNIT_STYLE_TYPE.NUST_SHIP_ASSAULT || this.shipType > NKM_UNIT_STYLE_TYPE.NUST_SHIP_PATROL)
			{
				NKMTempletError.Add(string.Format("[NKMShipCommandModuleTemplet:{0}] Ÿ�� �Լ� Ÿ���� �ùٸ��� ����.  shipType:{1}", this.id, this.shipType), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 602);
			}
			for (int i = 0; i < this.moduleLockItems.Length; i++)
			{
				this.moduleLockItems[i].Join("/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 607);
			}
			this.shipModuleReqItem.ForEach(delegate(MiscItemUnit s)
			{
				s.Join("/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 610);
			});
			if (this.shipLimitBreakGrade <= 0)
			{
				NKMTempletError.Add(string.Format("[NKMShipCommandModuleTemplet:{0}] shipLimitBreakGrade ���� �ùٸ��� ����.  shipLimitBreakGrade:{1}", this.id, this.shipLimitBreakGrade), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 614);
			}
			IReadOnlyList<NKMCommandModulePassiveTemplet> passiveListsByGroupId = NKMShipModuleGroupTemplet.GetPassiveListsByGroupId(this.commandModuleSlot1Id);
			if (passiveListsByGroupId != null && passiveListsByGroupId.Count <= 0)
			{
				NKMTempletError.Add(string.Format("[NKMShipCommandModuleTemplet:{0}] ModuleSlot1Id���� NKMCommandModulePassive�� ����. statGroupId:{1}", this.id, this.commandModuleSlot1Id), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 619);
			}
			IReadOnlyList<NKMCommandModulePassiveTemplet> passiveListsByGroupId2 = NKMShipModuleGroupTemplet.GetPassiveListsByGroupId(this.commandModuleSlot2Id);
			if (passiveListsByGroupId2 != null && passiveListsByGroupId2.Count <= 0)
			{
				NKMTempletError.Add(string.Format("[NKMShipCommandModuleTemplet:{0}] ModuleSlot2Id���� NKMCommandModulePassive�� ����. statGroupId:{1}", this.id, this.commandModuleSlot2Id), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 624);
			}
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x000916C4 File Offset: 0x0008F8C4
		public bool IsTargetTemplet(NKM_UNIT_STYLE_TYPE type, NKM_UNIT_GRADE grade, int limitBreakGrade)
		{
			return this.shipType == type && this.shipGrade == grade && this.LimitBreakGrade == limitBreakGrade;
		}

		// Token: 0x04001F25 RID: 7973
		private const int moduleLockItemCount = 2;

		// Token: 0x04001F26 RID: 7974
		private int id;

		// Token: 0x04001F27 RID: 7975
		private string openTag;

		// Token: 0x04001F28 RID: 7976
		private NKM_UNIT_STYLE_TYPE shipType;

		// Token: 0x04001F29 RID: 7977
		private NKM_UNIT_GRADE shipGrade;

		// Token: 0x04001F2A RID: 7978
		private int shipLimitBreakGrade;

		// Token: 0x04001F2B RID: 7979
		private int commandModuleSlot1Id;

		// Token: 0x04001F2C RID: 7980
		private int commandModuleSlot2Id;

		// Token: 0x04001F2D RID: 7981
		private MiscItemUnit[] moduleLockItems = new MiscItemUnit[2];

		// Token: 0x04001F2E RID: 7982
		private List<MiscItemUnit> shipModuleReqItem = new List<MiscItemUnit>(NKMCommonConst.ShipModuleReqItemCount);
	}
}
