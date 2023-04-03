using System;
using System.Collections.Generic;
using NKC.Office;
using NKM;
using NKM.Templet.Base;
using NKM.Templet.Office;
using UnityEngine;

namespace NKC.Templet.Office
{
	// Token: 0x0200085F RID: 2143
	public class NKCOfficeFurnitureInteractionTemplet : INKMTemplet
	{
		// Token: 0x06005514 RID: 21780 RVA: 0x0019E170 File Offset: 0x0019C370
		private static NKCOfficeFurnitureInteractionTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCOfficeFurnitureInteractionTemplet.cs", 32))
			{
				return null;
			}
			NKCOfficeFurnitureInteractionTemplet nkcofficeFurnitureInteractionTemplet = new NKCOfficeFurnitureInteractionTemplet();
			bool flag = true & cNKMLua.GetData("IDX", ref nkcofficeFurnitureInteractionTemplet.IDX) & cNKMLua.GetData("InteractionGroupID", ref nkcofficeFurnitureInteractionTemplet.InteractionGroupID) & cNKMLua.GetDataEnum<NKCOfficeFurnitureInteractionTemplet.ActType>("ActType", out nkcofficeFurnitureInteractionTemplet.eActType) & cNKMLua.GetData("ActProbability", ref nkcofficeFurnitureInteractionTemplet.ActProbability) & cNKMLua.GetDataEnum<ActTargetType>("ActTargetType", out nkcofficeFurnitureInteractionTemplet.eActTargetType);
			List<string> collection;
			if (cNKMLua.GetDataList("ActTargetID", out collection))
			{
				nkcofficeFurnitureInteractionTemplet.m_hsActTargetGroupID = new HashSet<string>(collection);
			}
			cNKMLua.GetData("UnitAni", ref nkcofficeFurnitureInteractionTemplet.UnitAni);
			cNKMLua.GetData("InteriorAni", ref nkcofficeFurnitureInteractionTemplet.InteriorAni);
			if (!flag)
			{
				return null;
			}
			return nkcofficeFurnitureInteractionTemplet;
		}

		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x06005515 RID: 21781 RVA: 0x0019E233 File Offset: 0x0019C433
		public int Key
		{
			get
			{
				return this.IDX;
			}
		}

		// Token: 0x06005516 RID: 21782 RVA: 0x0019E23C File Offset: 0x0019C43C
		public static void LoadFromLua()
		{
			NKMTempletContainer<NKCOfficeFurnitureInteractionTemplet>.Load("AB_SCRIPT", "LUA_INTERACTION_INTERIOR_TEMPLET", "INTERACTION_INTERIOR_TEMPLET", new Func<NKMLua, NKCOfficeFurnitureInteractionTemplet>(NKCOfficeFurnitureInteractionTemplet.LoadFromLUA));
			NKCOfficeFurnitureInteractionTemplet.m_dicGroup = new Dictionary<string, List<NKCOfficeFurnitureInteractionTemplet>>();
			foreach (NKCOfficeFurnitureInteractionTemplet nkcofficeFurnitureInteractionTemplet in NKMTempletContainer<NKCOfficeFurnitureInteractionTemplet>.Values)
			{
				if (!NKCOfficeFurnitureInteractionTemplet.m_dicGroup.ContainsKey(nkcofficeFurnitureInteractionTemplet.InteractionGroupID))
				{
					NKCOfficeFurnitureInteractionTemplet.m_dicGroup[nkcofficeFurnitureInteractionTemplet.InteractionGroupID] = new List<NKCOfficeFurnitureInteractionTemplet>();
				}
				NKCOfficeFurnitureInteractionTemplet.m_dicGroup[nkcofficeFurnitureInteractionTemplet.InteractionGroupID].Add(nkcofficeFurnitureInteractionTemplet);
			}
		}

		// Token: 0x06005517 RID: 21783 RVA: 0x0019E2E8 File Offset: 0x0019C4E8
		public static List<NKCOfficeFurnitureInteractionTemplet> GetInteractionTempletList(NKMOfficeInteriorTemplet InteriorTemplet, NKCOfficeFurnitureInteractionTemplet.ActType actType)
		{
			if (InteriorTemplet == null)
			{
				return null;
			}
			return NKCOfficeFurnitureInteractionTemplet.GetInteractionTempletList(InteriorTemplet.InteractionGroupID, actType);
		}

		// Token: 0x06005518 RID: 21784 RVA: 0x0019E2FC File Offset: 0x0019C4FC
		public static List<NKCOfficeFurnitureInteractionTemplet> GetInteractionTempletList(string group, NKCOfficeFurnitureInteractionTemplet.ActType actType)
		{
			if (NKCOfficeFurnitureInteractionTemplet.m_dicGroup == null)
			{
				NKCOfficeFurnitureInteractionTemplet.LoadFromLua();
			}
			List<NKCOfficeFurnitureInteractionTemplet> list;
			if (NKCOfficeFurnitureInteractionTemplet.m_dicGroup.TryGetValue(group, out list))
			{
				return list.FindAll((NKCOfficeFurnitureInteractionTemplet x) => x.eActType == actType);
			}
			return null;
		}

		// Token: 0x06005519 RID: 21785 RVA: 0x0019E345 File Offset: 0x0019C545
		public static void Drop()
		{
			Dictionary<string, List<NKCOfficeFurnitureInteractionTemplet>> dicGroup = NKCOfficeFurnitureInteractionTemplet.m_dicGroup;
			if (dicGroup != null)
			{
				dicGroup.Clear();
			}
			NKCOfficeFurnitureInteractionTemplet.m_dicGroup = null;
		}

		// Token: 0x0600551A RID: 21786 RVA: 0x0019E35D File Offset: 0x0019C55D
		public void Join()
		{
		}

		// Token: 0x0600551B RID: 21787 RVA: 0x0019E35F File Offset: 0x0019C55F
		public void Validate()
		{
		}

		// Token: 0x0600551C RID: 21788 RVA: 0x0019E361 File Offset: 0x0019C561
		public bool CheckUnitInteractionCondition(NKCOfficeCharacter character)
		{
			return NKCOfficeManager.IsActTarget(character, this.eActTargetType, this.m_hsActTargetGroupID);
		}

		// Token: 0x0600551D RID: 21789 RVA: 0x0019E378 File Offset: 0x0019C578
		public int GetFirstExclusiveActTarget()
		{
			if (this.eActTargetType == ActTargetType.Group)
			{
				return -1;
			}
			if (this.m_hsActTargetGroupID != null)
			{
				foreach (string text in this.m_hsActTargetGroupID)
				{
					int result;
					if (int.TryParse(text, out result))
					{
						return result;
					}
					Debug.Log(string.Format("NKCOfficeInteractionTemplet {0}: Int.Parse failed for id {1}", this.IDX, text));
				}
				return -1;
			}
			return -1;
		}

		// Token: 0x040043F4 RID: 17396
		public int IDX;

		// Token: 0x040043F5 RID: 17397
		public string InteractionGroupID;

		// Token: 0x040043F6 RID: 17398
		public NKCOfficeFurnitureInteractionTemplet.ActType eActType;

		// Token: 0x040043F7 RID: 17399
		public int ActProbability;

		// Token: 0x040043F8 RID: 17400
		public ActTargetType eActTargetType;

		// Token: 0x040043F9 RID: 17401
		public HashSet<string> m_hsActTargetGroupID;

		// Token: 0x040043FA RID: 17402
		public string UnitAni;

		// Token: 0x040043FB RID: 17403
		public string InteriorAni;

		// Token: 0x040043FC RID: 17404
		public static Dictionary<string, List<NKCOfficeFurnitureInteractionTemplet>> m_dicGroup;

		// Token: 0x020014F5 RID: 5365
		public enum ActType
		{
			// Token: 0x04009F7E RID: 40830
			Common,
			// Token: 0x04009F7F RID: 40831
			Reaction
		}
	}
}
