using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x020003DE RID: 990
	public class NKMDungeonEventDeckTemplet : INKMTemplet
	{
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06001A26 RID: 6694 RVA: 0x000701A9 File Offset: 0x0006E3A9
		public int Key
		{
			get
			{
				return this.ID;
			}
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x000701B4 File Offset: 0x0006E3B4
		public NKMDungeonEventDeckTemplet.EventDeckSlot GetUnitSlot(int index)
		{
			if (index < this.m_lstUnitSlot.Count)
			{
				return this.m_lstUnitSlot[index];
			}
			return new NKMDungeonEventDeckTemplet.EventDeckSlot
			{
				m_eType = NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_CLOSED,
				m_ID = 0,
				m_Level = 0,
				m_SkinID = 0
			};
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x00070208 File Offset: 0x0006E408
		public bool IsUnitFitInSlot(NKMDungeonEventDeckTemplet.EventDeckSlot slot, NKMUnitData unitData)
		{
			if (unitData == null)
			{
				return false;
			}
			switch (slot.m_eType)
			{
			default:
				return false;
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE:
				return true;
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FIXED:
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_GUEST:
				return unitData.IsSameBaseUnit(slot.m_ID);
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_COUNTER:
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_SOLDIER:
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_MECHANIC:
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData);
				return unitTempletBase != null && NKMDungeonManager.GetUnitStyleTypeFromEventDeckType(slot.m_eType) == unitTempletBase.m_NKM_UNIT_STYLE_TYPE;
			}
			}
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x00070278 File Offset: 0x0006E478
		public bool IsOperatorFitInSlot(NKMOperator operatorData)
		{
			if (operatorData == null)
			{
				return false;
			}
			switch (this.OperatorSlot.m_eType)
			{
			default:
				return false;
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE:
				return true;
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FIXED:
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_GUEST:
				return operatorData.id == this.OperatorSlot.m_ID;
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_COUNTER:
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_SOLDIER:
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_MECHANIC:
				return false;
			}
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x000702D8 File Offset: 0x0006E4D8
		public static NKMDungeonEventDeckTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonEventDeck.cs", 114))
			{
				return null;
			}
			NKMDungeonEventDeckTemplet nkmdungeonEventDeckTemplet = new NKMDungeonEventDeckTemplet();
			bool flag = true;
			flag &= cNKMLua.GetData("ID", ref nkmdungeonEventDeckTemplet.ID);
			flag &= cNKMLua.GetData("NAME", ref nkmdungeonEventDeckTemplet.NAME);
			cNKMLua.GetData<NKMDungeonEventDeckTemplet.SLOT_TYPE>("SLOT_TYPE_SHIP", ref nkmdungeonEventDeckTemplet.ShipSlot.m_eType);
			cNKMLua.GetData("SLOT_UNIT_ID_SHIP", out nkmdungeonEventDeckTemplet.ShipSlot.m_ID, 0);
			cNKMLua.GetData("SLOT_UNIT_LEVEL_SHIP", out nkmdungeonEventDeckTemplet.ShipSlot.m_Level, 0);
			nkmdungeonEventDeckTemplet.ShipSlot.m_SkinID = 0;
			if (nkmdungeonEventDeckTemplet.ShipSlot.m_eType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_CLOSED)
			{
				Log.Error("EventDeck : Ship slot is closed!", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonEventDeck.cs", 130);
				flag = false;
			}
			cNKMLua.GetData<NKMDungeonEventDeckTemplet.SLOT_TYPE>("SLOT_TYPE_OPERATOR", ref nkmdungeonEventDeckTemplet.OperatorSlot.m_eType);
			cNKMLua.GetData("SLOT_UNIT_ID_OPERATOR", out nkmdungeonEventDeckTemplet.OperatorSlot.m_ID, 0);
			cNKMLua.GetData("SLOT_UNIT_LEVEL_OPERATOR", out nkmdungeonEventDeckTemplet.OperatorSlot.m_Level, 0);
			cNKMLua.GetData("SLOT_UNIT_SKILL_ID_OPERATOR", out nkmdungeonEventDeckTemplet.OperatorSubSkillID, 0);
			for (int i = 0; i < 8; i++)
			{
				string str = (i + 1).ToString();
				NKMDungeonEventDeckTemplet.EventDeckSlot item;
				if (cNKMLua.GetDataEnum<NKMDungeonEventDeckTemplet.SLOT_TYPE>("SLOT_TYPE_UNIT_" + str, out item.m_eType))
				{
					cNKMLua.GetData("SLOT_UNIT_ID_" + str, out item.m_ID, 0);
					cNKMLua.GetData("SLOT_UNIT_LEVEL_" + str, out item.m_Level, 1);
					cNKMLua.GetData("SLOT_UNIT_SKIN_ID_" + str, out item.m_SkinID, 0);
					nkmdungeonEventDeckTemplet.m_lstUnitSlot.Add(item);
				}
			}
			string strCondition;
			cNKMLua.GetData("DECK_CONDITION", out strCondition, "");
			bool flag2 = NKMDeckCondition.ParseDeckCondition(strCondition, out nkmdungeonEventDeckTemplet.m_DeckCondition);
			if (!flag2)
			{
				Log.Error(string.Format("NKMDungeonEventDeckTemplet Condition Parse Fail : {0}", nkmdungeonEventDeckTemplet.ID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonEventDeck.cs", 160);
			}
			if (!flag || !flag2)
			{
				Log.Error(string.Format("NKMDungeonEventDeckTemplet Load Fail - {0}", nkmdungeonEventDeckTemplet.ID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonEventDeck.cs", 166);
				return null;
			}
			nkmdungeonEventDeckTemplet.CheckValidation();
			return nkmdungeonEventDeckTemplet;
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x0007050F File Offset: 0x0006E70F
		public void Join()
		{
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00070511 File Offset: 0x0006E711
		public void Validate()
		{
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x00070513 File Offset: 0x0006E713
		private void CheckValidation()
		{
		}

		// Token: 0x0400131A RID: 4890
		public int ID;

		// Token: 0x0400131B RID: 4891
		public string NAME;

		// Token: 0x0400131C RID: 4892
		public NKMDungeonEventDeckTemplet.EventDeckSlot ShipSlot;

		// Token: 0x0400131D RID: 4893
		public NKMDungeonEventDeckTemplet.EventDeckSlot OperatorSlot;

		// Token: 0x0400131E RID: 4894
		public int OperatorSubSkillID;

		// Token: 0x0400131F RID: 4895
		public List<NKMDungeonEventDeckTemplet.EventDeckSlot> m_lstUnitSlot = new List<NKMDungeonEventDeckTemplet.EventDeckSlot>(8);

		// Token: 0x04001320 RID: 4896
		public NKMDeckCondition m_DeckCondition;

		// Token: 0x020011D3 RID: 4563
		public enum SLOT_TYPE
		{
			// Token: 0x0400936E RID: 37742
			ST_CLOSED,
			// Token: 0x0400936F RID: 37743
			ST_FREE,
			// Token: 0x04009370 RID: 37744
			ST_FIXED,
			// Token: 0x04009371 RID: 37745
			ST_GUEST,
			// Token: 0x04009372 RID: 37746
			ST_NPC,
			// Token: 0x04009373 RID: 37747
			ST_FREE_COUNTER,
			// Token: 0x04009374 RID: 37748
			ST_FREE_SOLDIER,
			// Token: 0x04009375 RID: 37749
			ST_FREE_MECHANIC
		}

		// Token: 0x020011D4 RID: 4564
		public struct EventDeckSlot
		{
			// Token: 0x04009376 RID: 37750
			public NKMDungeonEventDeckTemplet.SLOT_TYPE m_eType;

			// Token: 0x04009377 RID: 37751
			public int m_ID;

			// Token: 0x04009378 RID: 37752
			public int m_Level;

			// Token: 0x04009379 RID: 37753
			public int m_SkinID;
		}
	}
}
