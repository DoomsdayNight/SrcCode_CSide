using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x02000465 RID: 1125
	public sealed class NKMShipBuildTemplet : INKMTemplet
	{
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001E68 RID: 7784 RVA: 0x00090496 File Offset: 0x0008E696
		public int ShipID
		{
			get
			{
				return this.m_ShipID;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06001E69 RID: 7785 RVA: 0x0009049E File Offset: 0x0008E69E
		public string ShipName
		{
			get
			{
				return this.m_ShipName;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06001E6A RID: 7786 RVA: 0x000904A6 File Offset: 0x0008E6A6
		public NKMShipBuildTemplet.ShipUITabType ShipType
		{
			get
			{
				return this.m_ShipType;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06001E6B RID: 7787 RVA: 0x000904AE File Offset: 0x0008E6AE
		public int ShipUpgradeTarget1
		{
			get
			{
				return this.m_ShipUpgradeTarget1;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06001E6C RID: 7788 RVA: 0x000904B6 File Offset: 0x0008E6B6
		public int ShipUpgradeTarget2
		{
			get
			{
				return this.m_ShipUpgradeTarget2;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06001E6D RID: 7789 RVA: 0x000904BE File Offset: 0x0008E6BE
		public int ShipUpgradeCredit
		{
			get
			{
				return this.m_ShipUpgradeCredit;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06001E6E RID: 7790 RVA: 0x000904C6 File Offset: 0x0008E6C6
		public List<UpgradeMaterial> UpgradeMaterialList
		{
			get
			{
				return this.m_UpgradeMaterialList;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06001E6F RID: 7791 RVA: 0x000904CE File Offset: 0x0008E6CE
		public NKMShipBuildTemplet.BuildUnlockType ShipBuildUnlockType
		{
			get
			{
				return this.m_BuildUnlockType;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06001E70 RID: 7792 RVA: 0x000904D6 File Offset: 0x0008E6D6
		public int UnlockValue
		{
			get
			{
				return this.m_UnlockValue;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06001E71 RID: 7793 RVA: 0x000904DE File Offset: 0x0008E6DE
		public List<BuildMaterial> BuildMaterialList
		{
			get
			{
				return this.m_BuildMaterialList;
			}
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x000904E8 File Offset: 0x0008E6E8
		public static NKMShipBuildTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 67))
			{
				return null;
			}
			NKMShipBuildTemplet nkmshipBuildTemplet = new NKMShipBuildTemplet();
			bool flag = true;
			flag &= cNKMLua.GetData("m_ShipID", ref nkmshipBuildTemplet.m_ShipID);
			flag &= cNKMLua.GetData("m_ShipName", ref nkmshipBuildTemplet.m_ShipName);
			flag &= cNKMLua.GetData<NKMShipBuildTemplet.ShipUITabType>("m_ShipType", ref nkmshipBuildTemplet.m_ShipType);
			cNKMLua.GetData("m_ShipUpgradeTarget1", ref nkmshipBuildTemplet.m_ShipUpgradeTarget1);
			cNKMLua.GetData("m_ShipUpgradeTarget2", ref nkmshipBuildTemplet.m_ShipUpgradeTarget2);
			cNKMLua.GetData("m_ShipUpgradeCredit", ref nkmshipBuildTemplet.m_ShipUpgradeCredit);
			for (int i = 0; i < 4; i++)
			{
				int num = 0;
				int num2 = 0;
				string str = (i + 1).ToString("D");
				if (true & cNKMLua.GetData("m_ShipUpgradeMaterial" + str, ref num) & cNKMLua.GetData("m_ShipUpgradeMaterialCount" + str, ref num2))
				{
					if (num != 0 && num2 > 0)
					{
						UpgradeMaterial item;
						item.m_ShipUpgradeMaterial = num;
						item.m_ShipUpgradeMaterialCount = num2;
						nkmshipBuildTemplet.m_UpgradeMaterialList.Add(item);
					}
					else
					{
						Log.Error(string.Format("WARNING : NKMShipBuildTemplet : weird upgrade material data. ID : {0}", nkmshipBuildTemplet.m_ShipID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 104);
					}
				}
			}
			flag &= cNKMLua.GetData<NKMShipBuildTemplet.BuildUnlockType>("m_ShipBuildUnlockType", ref nkmshipBuildTemplet.m_BuildUnlockType);
			flag &= cNKMLua.GetData("m_ShipBuildUnlockValue", ref nkmshipBuildTemplet.m_UnlockValue);
			for (int j = 0; j < 4; j++)
			{
				int num3 = 0;
				int num4 = 0;
				string value = "";
				string str2 = (j + 1).ToString("D");
				if (true & cNKMLua.GetData("m_ShipBuildMaterialType_" + str2, ref value) & cNKMLua.GetData("m_ShipBuildMaterialID_" + str2, ref num3) & cNKMLua.GetData("m_ShipBuildMaterialValue_" + str2, ref num4))
				{
					NKM_REWARD_TYPE nkm_REWARD_TYPE = (NKM_REWARD_TYPE)Enum.Parse(typeof(NKM_REWARD_TYPE), value);
					if (num3 != 0 && num4 > 0)
					{
						BuildMaterial item2 = default(BuildMaterial);
						item2.m_ShipBuildMaterialID = num3;
						item2.m_ShipBuildMaterialCount = num4;
						nkmshipBuildTemplet.m_BuildMaterialList.Add(item2);
					}
					else
					{
						Log.Error(string.Format("WARNING : NKMShipBuildTemplet : weird build material data. ID : {0}", nkmshipBuildTemplet.m_ShipID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 139);
					}
				}
			}
			if (!flag)
			{
				return null;
			}
			return nkmshipBuildTemplet;
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06001E73 RID: 7795 RVA: 0x00090734 File Offset: 0x0008E934
		public int Key
		{
			get
			{
				return this.m_ShipID;
			}
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x0009073C File Offset: 0x0008E93C
		public static NKMShipBuildTemplet Find(int key)
		{
			return NKMTempletContainer<NKMShipBuildTemplet>.Find(key);
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x00090744 File Offset: 0x0008E944
		public void Join()
		{
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x00090748 File Offset: 0x0008E948
		public void Validate()
		{
			if (NKMUnitManager.GetUnitTempletBase(this.m_ShipID) == null)
			{
				NKMTempletError.Add(string.Format("[ShipBuildTemplet] �Լ� ������ �������� ���� m_ShipID : {0}", this.m_ShipID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 159);
			}
			if (this.m_ShipUpgradeTarget1 > 0 && NKMUnitManager.GetUnitTempletBase(this.m_ShipUpgradeTarget1) == null)
			{
				NKMTempletError.Add(string.Format("[ShipBuildTemplet] ���׷��̵� ����� �������� ���� m_ShipID : {0}, m_ShipUpgradeTarget1 : {1}", this.m_ShipID, this.m_ShipUpgradeTarget1), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 166);
			}
			if (this.m_ShipUpgradeTarget2 > 0 && NKMUnitManager.GetUnitTempletBase(this.m_ShipUpgradeTarget2) == null)
			{
				NKMTempletError.Add(string.Format("[ShipBuildTemplet] ���׷��̵� ����� �������� ���� m_ShipID : {0}, m_ShipUpgradeTarget2 : {1}", this.m_ShipID, this.m_ShipUpgradeTarget2), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 174);
			}
			switch (this.m_BuildUnlockType)
			{
			case NKMShipBuildTemplet.BuildUnlockType.BUT_DUNGEON_CLEAR:
				if (NKMDungeonManager.GetDungeonTemplet(this.m_UnlockValue) == null)
				{
					NKMTempletError.Add(string.Format("[ShipBuildTemplet] ���� ���� ����(���� Ŭ����) ����� �������� ���� m_ShipID : {0}, m_BuildUnlockType : {1}, m_UnlockValue : {2}", this.m_ShipID, this.m_BuildUnlockType, this.m_UnlockValue), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 183);
				}
				break;
			case NKMShipBuildTemplet.BuildUnlockType.BUT_WARFARE_CLEAR:
				if (NKMWarfareTemplet.Find(this.m_UnlockValue) == null)
				{
					NKMTempletError.Add(string.Format("[ShipBuildTemplet] ���� ���� ����(���� Ŭ����) ����� �������� ���� m_ShipID : {0}, m_BuildUnlockType : {1}, m_UnlockValue : {2}", this.m_ShipID, this.m_BuildUnlockType, this.m_UnlockValue), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 190);
				}
				break;
			case NKMShipBuildTemplet.BuildUnlockType.BUT_SHIP_GET:
			case NKMShipBuildTemplet.BuildUnlockType.BUT_SHIP_LV100:
				if (NKMUnitManager.GetUnitTempletBase(this.m_UnlockValue) == null)
				{
					NKMTempletError.Add(string.Format("[ShipBuildTemplet] ���� ���� ����(�Լ� ȹ��) ����� �������� ���� m_ShipID : {0}, m_BuildUnlockType : {1}, m_UnlockValue : {2}", this.m_ShipID, this.m_BuildUnlockType, this.m_UnlockValue), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 198);
				}
				break;
			case NKMShipBuildTemplet.BuildUnlockType.BUT_SHADOW_CLEAR:
				if (NKMTempletContainer<NKMShadowPalaceTemplet>.Find(this.m_UnlockValue) == null)
				{
					NKMTempletError.Add(string.Format("[ShipBuildTemplet] ���� ���� ����(�׸��� ���� Ŭ����). �ش� �׸��� ������ �������� ���� m_ShipID : {0}, m_BuildUnlockType : {1}, m_UnlockValue : {2}", this.m_ShipID, this.m_BuildUnlockType, this.m_UnlockValue), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 204);
				}
				break;
			}
			foreach (UpgradeMaterial upgradeMaterial in this.m_UpgradeMaterialList)
			{
				if (NKMItemManager.GetItemMiscTempletByID(upgradeMaterial.m_ShipUpgradeMaterial) == null || upgradeMaterial.m_ShipUpgradeMaterialCount <= 0)
				{
					Log.ErrorAndExit(string.Format("[ShipBuildTemplet] ���׷��̵� ��ᰡ �������� ���� m_ShipID : {0}, m_ShipUpgradeMaterial : {1}, m_ShipUpgradeMaterialCount : {2}", this.m_ShipID, upgradeMaterial.m_ShipUpgradeMaterial, upgradeMaterial.m_ShipUpgradeMaterialCount), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 217);
				}
			}
			foreach (BuildMaterial buildMaterial in this.m_BuildMaterialList)
			{
				if (NKMItemManager.GetItemMiscTempletByID(buildMaterial.m_ShipBuildMaterialID) == null || buildMaterial.m_ShipBuildMaterialCount <= 0)
				{
					Log.ErrorAndExit(string.Format("[ShipBuildTemplet] ���� ��ᰡ �������� ���� m_ShipID : {0}, m_ShipBuildMaterial : {1}, m_ShipBuildMaterialCount : {2}", this.m_ShipID, buildMaterial.m_ShipBuildMaterialID, buildMaterial.m_ShipBuildMaterialCount), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 225);
				}
			}
		}

		// Token: 0x04001F0A RID: 7946
		private int m_ShipID;

		// Token: 0x04001F0B RID: 7947
		private string m_ShipName;

		// Token: 0x04001F0C RID: 7948
		private NKMShipBuildTemplet.ShipUITabType m_ShipType;

		// Token: 0x04001F0D RID: 7949
		private int m_ShipUpgradeTarget1;

		// Token: 0x04001F0E RID: 7950
		private int m_ShipUpgradeTarget2;

		// Token: 0x04001F0F RID: 7951
		private int m_ShipUpgradeCredit;

		// Token: 0x04001F10 RID: 7952
		private List<UpgradeMaterial> m_UpgradeMaterialList = new List<UpgradeMaterial>();

		// Token: 0x04001F11 RID: 7953
		private NKMShipBuildTemplet.BuildUnlockType m_BuildUnlockType;

		// Token: 0x04001F12 RID: 7954
		private int m_UnlockValue;

		// Token: 0x04001F13 RID: 7955
		private List<BuildMaterial> m_BuildMaterialList = new List<BuildMaterial>();

		// Token: 0x020011FC RID: 4604
		public enum BuildUnlockType
		{
			// Token: 0x0400941C RID: 37916
			BUT_UNABLE,
			// Token: 0x0400941D RID: 37917
			BUT_ALWAYS,
			// Token: 0x0400941E RID: 37918
			BUT_PLAYER_LEVEL,
			// Token: 0x0400941F RID: 37919
			BUT_DUNGEON_CLEAR,
			// Token: 0x04009420 RID: 37920
			BUT_WARFARE_CLEAR,
			// Token: 0x04009421 RID: 37921
			BUT_SHIP_GET,
			// Token: 0x04009422 RID: 37922
			BUT_SHIP_LV100,
			// Token: 0x04009423 RID: 37923
			BUT_WORLDMAP_CITY_COUNT,
			// Token: 0x04009424 RID: 37924
			BUT_SHADOW_CLEAR
		}

		// Token: 0x020011FD RID: 4605
		public enum ShipUITabType
		{
			// Token: 0x04009426 RID: 37926
			SHIP_NORMAL,
			// Token: 0x04009427 RID: 37927
			SHIP_EVENT
		}
	}
}
