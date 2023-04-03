using System;
using System.Collections.Generic;
using NKC.Office;
using NKM;
using NKM.Templet.Base;

namespace NKC.Templet.Office
{
	// Token: 0x02000860 RID: 2144
	public class NKCOfficeUnitInteractionTemplet : INKMTemplet
	{
		// Token: 0x1700103D RID: 4157
		// (get) Token: 0x0600551F RID: 21791 RVA: 0x0019E40C File Offset: 0x0019C60C
		public bool IsSoloAction
		{
			get
			{
				return this.hsTargetGroup == null || this.hsTargetGroup.Count == 0;
			}
		}

		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x06005520 RID: 21792 RVA: 0x0019E426 File Offset: 0x0019C626
		public int Key
		{
			get
			{
				return this.UnitActID;
			}
		}

		// Token: 0x06005521 RID: 21793 RVA: 0x0019E42E File Offset: 0x0019C62E
		public static void LoadFromLua()
		{
			NKMTempletContainer<NKCOfficeUnitInteractionTemplet>.Load("AB_SCRIPT", "LUA_INTERACTION_UNIT_TEMPLET", "INTERACTION_UNIT_TEMPLET", new Func<NKMLua, NKCOfficeUnitInteractionTemplet>(NKCOfficeUnitInteractionTemplet.LoadFromLUA));
			NKCOfficeManager.BuildSkinInteractionGroup();
			NKCOfficeUnitInteractionTemplet.m_sLuaLoaded = true;
		}

		// Token: 0x06005522 RID: 21794 RVA: 0x0019E45C File Offset: 0x0019C65C
		private static NKCOfficeUnitInteractionTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCOfficeUnitInteractionTemplet.cs", 44))
			{
				return null;
			}
			NKCOfficeUnitInteractionTemplet nkcofficeUnitInteractionTemplet = new NKCOfficeUnitInteractionTemplet();
			bool flag = true & cNKMLua.GetData("UnitActID", ref nkcofficeUnitInteractionTemplet.UnitActID) & cNKMLua.GetData("UnitActStrID", ref nkcofficeUnitInteractionTemplet.ActorAni);
			cNKMLua.GetData("UnitActStrID2", ref nkcofficeUnitInteractionTemplet.TargetAni);
			cNKMLua.GetData("AlignUnit", ref nkcofficeUnitInteractionTemplet.AlignUnit);
			cNKMLua.GetData("ActRange", ref nkcofficeUnitInteractionTemplet.ActRange);
			List<string> collection;
			bool flag2 = flag & cNKMLua.GetDataEnum<ActTargetType>("UnitType1", out nkcofficeUnitInteractionTemplet.ActorType) & cNKMLua.GetDataList("UnitID1", out collection);
			nkcofficeUnitInteractionTemplet.hsActorGroup = new HashSet<string>(collection);
			cNKMLua.GetDataEnum<ActTargetType>("UnitType2", out nkcofficeUnitInteractionTemplet.TargetType);
			List<string> collection2;
			if (cNKMLua.GetDataList("UnitID2", out collection2))
			{
				nkcofficeUnitInteractionTemplet.hsTargetGroup = new HashSet<string>(collection2);
			}
			cNKMLua.GetData("InteractionSkinGroup", ref nkcofficeUnitInteractionTemplet.InteractionSkinGroup);
			if (!flag2)
			{
				return null;
			}
			return nkcofficeUnitInteractionTemplet;
		}

		// Token: 0x06005523 RID: 21795 RVA: 0x0019E54B File Offset: 0x0019C74B
		public void Join()
		{
		}

		// Token: 0x06005524 RID: 21796 RVA: 0x0019E54D File Offset: 0x0019C74D
		public void Validate()
		{
		}

		// Token: 0x06005525 RID: 21797 RVA: 0x0019E550 File Offset: 0x0019C750
		public static List<NKCOfficeUnitInteractionTemplet> GetInteractionTempletList(NKCOfficeCharacter character)
		{
			if (character == null)
			{
				return new List<NKCOfficeUnitInteractionTemplet>();
			}
			if (!NKCOfficeUnitInteractionTemplet.m_sLuaLoaded)
			{
				NKCOfficeUnitInteractionTemplet.LoadFromLua();
			}
			List<NKCOfficeUnitInteractionTemplet> list = new List<NKCOfficeUnitInteractionTemplet>();
			foreach (NKCOfficeUnitInteractionTemplet nkcofficeUnitInteractionTemplet in NKMTempletContainer<NKCOfficeUnitInteractionTemplet>.Values)
			{
				if (nkcofficeUnitInteractionTemplet.CheckUnitInteractionCondition(character, false) && NKCAnimationEventManager.CanPlayAnimEvent(character, nkcofficeUnitInteractionTemplet.ActorAni))
				{
					list.Add(nkcofficeUnitInteractionTemplet);
				}
			}
			return list;
		}

		// Token: 0x06005526 RID: 21798 RVA: 0x0019E5D8 File Offset: 0x0019C7D8
		public bool CheckUnitInteractionCondition(NKCOfficeCharacter character, bool bTarget)
		{
			if (character == null)
			{
				return false;
			}
			if (bTarget && this.IsSoloAction)
			{
				return false;
			}
			ActTargetType eActTargetType;
			HashSet<string> hsActTargetGroupID;
			if (bTarget)
			{
				eActTargetType = this.TargetType;
				hsActTargetGroupID = this.hsTargetGroup;
			}
			else
			{
				eActTargetType = this.ActorType;
				hsActTargetGroupID = this.hsActorGroup;
			}
			return NKCOfficeManager.IsActTarget(character, eActTargetType, hsActTargetGroupID);
		}

		// Token: 0x06005527 RID: 21799 RVA: 0x0019E626 File Offset: 0x0019C826
		public static void Drop()
		{
			NKCOfficeUnitInteractionTemplet.m_sLuaLoaded = false;
		}

		// Token: 0x040043FD RID: 17405
		public int UnitActID;

		// Token: 0x040043FE RID: 17406
		public string ActorAni;

		// Token: 0x040043FF RID: 17407
		public string TargetAni;

		// Token: 0x04004400 RID: 17408
		public bool AlignUnit;

		// Token: 0x04004401 RID: 17409
		public float ActRange;

		// Token: 0x04004402 RID: 17410
		public ActTargetType ActorType;

		// Token: 0x04004403 RID: 17411
		public HashSet<string> hsActorGroup;

		// Token: 0x04004404 RID: 17412
		public ActTargetType TargetType;

		// Token: 0x04004405 RID: 17413
		public HashSet<string> hsTargetGroup;

		// Token: 0x04004406 RID: 17414
		public string InteractionSkinGroup;

		// Token: 0x04004407 RID: 17415
		private static bool m_sLuaLoaded;
	}
}
