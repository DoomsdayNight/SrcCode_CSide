using System;
using NKM;
using NKM.Templet.Base;

namespace NKC.Templet
{
	// Token: 0x02000856 RID: 2134
	public class NKCOfficeCharacterTemplet : INKMTemplet
	{
		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x060054C1 RID: 21697 RVA: 0x0019CDE6 File Offset: 0x0019AFE6
		public int Key
		{
			get
			{
				return (int)(this.ID * 10 + this.Type);
			}
		}

		// Token: 0x060054C2 RID: 21698 RVA: 0x0019CDF8 File Offset: 0x0019AFF8
		public static NKCOfficeCharacterTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCOfficeCharacterTemplet.cs", 29))
			{
				return null;
			}
			NKCOfficeCharacterTemplet nkcofficeCharacterTemplet = new NKCOfficeCharacterTemplet();
			bool flag = true & cNKMLua.GetDataEnum<NKCOfficeCharacterTemplet.eType>("Type", out nkcofficeCharacterTemplet.Type) & cNKMLua.GetData("ID", ref nkcofficeCharacterTemplet.ID);
			cNKMLua.GetData("PrefabAsset", ref nkcofficeCharacterTemplet.PrefabAsset);
			cNKMLua.GetData("BTAsset", ref nkcofficeCharacterTemplet.BTAsset);
			cNKMLua.GetData("WalkSpeed", ref nkcofficeCharacterTemplet.WalkSpeed);
			cNKMLua.GetData("RunSpeed", ref nkcofficeCharacterTemplet.RunSpeed);
			cNKMLua.GetData("IgnoreObstacles", ref nkcofficeCharacterTemplet.IgnoreObstacles);
			cNKMLua.GetData("Variables", ref nkcofficeCharacterTemplet.Variables);
			if (!flag)
			{
				return null;
			}
			return nkcofficeCharacterTemplet;
		}

		// Token: 0x060054C3 RID: 21699 RVA: 0x0019CEB2 File Offset: 0x0019B0B2
		public static NKCOfficeCharacterTemplet Find(NKCOfficeCharacterTemplet.eType type, int ID)
		{
			return NKMTempletContainer<NKCOfficeCharacterTemplet>.Find((int)(ID * 10 + type));
		}

		// Token: 0x060054C4 RID: 21700 RVA: 0x0019CEC0 File Offset: 0x0019B0C0
		public static NKCOfficeCharacterTemplet Find(int unitID, int skinID)
		{
			if (skinID != 0)
			{
				NKCOfficeCharacterTemplet nkcofficeCharacterTemplet = NKCOfficeCharacterTemplet.Find(NKCOfficeCharacterTemplet.eType.Skin, skinID);
				if (nkcofficeCharacterTemplet != null)
				{
					return nkcofficeCharacterTemplet;
				}
			}
			return NKCOfficeCharacterTemplet.Find(NKCOfficeCharacterTemplet.eType.Unit, unitID);
		}

		// Token: 0x060054C5 RID: 21701 RVA: 0x0019CEE4 File Offset: 0x0019B0E4
		public static NKCOfficeCharacterTemplet Find(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				return null;
			}
			if (unitData.m_SkinID != 0)
			{
				NKCOfficeCharacterTemplet nkcofficeCharacterTemplet = NKCOfficeCharacterTemplet.Find(NKCOfficeCharacterTemplet.eType.Skin, unitData.m_SkinID);
				if (nkcofficeCharacterTemplet != null)
				{
					return nkcofficeCharacterTemplet;
				}
			}
			return NKCOfficeCharacterTemplet.Find(NKCOfficeCharacterTemplet.eType.Unit, unitData.m_UnitID);
		}

		// Token: 0x060054C6 RID: 21702 RVA: 0x0019CF1C File Offset: 0x0019B11C
		public void Join()
		{
		}

		// Token: 0x060054C7 RID: 21703 RVA: 0x0019CF20 File Offset: 0x0019B120
		public void Validate()
		{
			NKCOfficeCharacterTemplet.eType type = this.Type;
			if (type != NKCOfficeCharacterTemplet.eType.Unit)
			{
				if (type != NKCOfficeCharacterTemplet.eType.Skin)
				{
					return;
				}
				if (NKMSkinManager.GetSkinTemplet(this.ID) == null)
				{
					NKMTempletError.Add(string.Format("[NKCOfficeCharacterTemplet] invalid SkinID:{0}", this.ID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCOfficeCharacterTemplet.cs", 100);
				}
			}
			else if (NKMUnitManager.GetUnitTempletBase(this.ID) == null)
			{
				NKMTempletError.Add(string.Format("[NKCOfficeCharacterTemplet] invalid CharID:{0}", this.ID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCOfficeCharacterTemplet.cs", 93);
				return;
			}
		}

		// Token: 0x040043B2 RID: 17330
		public NKCOfficeCharacterTemplet.eType Type;

		// Token: 0x040043B3 RID: 17331
		public int ID;

		// Token: 0x040043B4 RID: 17332
		public string PrefabAsset;

		// Token: 0x040043B5 RID: 17333
		public string BTAsset;

		// Token: 0x040043B6 RID: 17334
		public float WalkSpeed;

		// Token: 0x040043B7 RID: 17335
		public float RunSpeed;

		// Token: 0x040043B8 RID: 17336
		public bool IgnoreObstacles;

		// Token: 0x040043B9 RID: 17337
		public string Variables;

		// Token: 0x020014F1 RID: 5361
		public enum eType
		{
			// Token: 0x04009F72 RID: 40818
			Unit,
			// Token: 0x04009F73 RID: 40819
			Skin
		}
	}
}
