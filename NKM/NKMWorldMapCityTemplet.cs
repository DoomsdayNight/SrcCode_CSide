using System;
using Cs.Logging;
using NKC;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x02000513 RID: 1299
	public class NKMWorldMapCityTemplet : INKMTemplet
	{
		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06002519 RID: 9497 RVA: 0x000BF98C File Offset: 0x000BDB8C
		public int Key
		{
			get
			{
				return this.m_ID;
			}
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x000BF994 File Offset: 0x000BDB94
		public static NKMWorldMapCityTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			NKMWorldMapCityTemplet nkmworldMapCityTemplet = new NKMWorldMapCityTemplet();
			bool flag = true & cNKMLua.GetData("m_CityID", ref nkmworldMapCityTemplet.m_ID) & cNKMLua.GetData("m_CityStrID", ref nkmworldMapCityTemplet.m_StrID) & cNKMLua.GetData("m_CityName", ref nkmworldMapCityTemplet.m_Name) & cNKMLua.GetData("m_CityNameEng", ref nkmworldMapCityTemplet.m_NameEng) & cNKMLua.GetData<NKMWorldMapCityTemplet.CityType>("m_CityType", ref nkmworldMapCityTemplet.m_CityType) & cNKMLua.GetData("m_MaxLevel", ref nkmworldMapCityTemplet.m_MaxLevel);
			cNKMLua.GetData("m_Title", ref nkmworldMapCityTemplet.m_Title);
			cNKMLua.GetData("m_Description", ref nkmworldMapCityTemplet.m_Description);
			nkmworldMapCityTemplet.CheckValidation();
			if (!flag)
			{
				Log.Error(string.Format("NKMWorldMapCityTemplet Load Fail - {0}", nkmworldMapCityTemplet.m_ID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMWorldMapManager.cs", 102);
				return null;
			}
			return nkmworldMapCityTemplet;
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x000BFA64 File Offset: 0x000BDC64
		private void CheckValidation()
		{
			if (this.m_CityType == NKMWorldMapCityTemplet.CityType.CT_INVALID)
			{
				Log.ErrorAndExit(string.Format("[WorldMapCityTemplet] 도시 타입이 존재하지 않음 m_ID : {0}, m_CityType : {1}", this.m_ID, this.m_CityType), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMWorldMapManager.cs", 113);
			}
			for (int i = 1; i <= this.m_MaxLevel; i++)
			{
				if (NKMWorldMapManager.GetCityExpTable(i) == null)
				{
					Log.ErrorAndExit(string.Format("[WorldMapCityTemplet] 도시 레벨은 {0} 보다 클 수 없음 m_ID : {1}, m_MaxLevel : {2}", i, this.m_ID, this.m_MaxLevel), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMWorldMapManager.cs", 120);
				}
			}
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x000BFAF0 File Offset: 0x000BDCF0
		public void Join()
		{
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x000BFAF2 File Offset: 0x000BDCF2
		public void Validate()
		{
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x000BFAF4 File Offset: 0x000BDCF4
		public string GetName()
		{
			return NKCStringTable.GetString(this.m_Name, false);
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x000BFB02 File Offset: 0x000BDD02
		public string GetNameEng()
		{
			return NKCStringTable.GetString(this.m_NameEng, false);
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x000BFB10 File Offset: 0x000BDD10
		public string GetTitle()
		{
			return NKCStringTable.GetString(this.m_Title, false);
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x000BFB1E File Offset: 0x000BDD1E
		public string GetDesc()
		{
			return NKCStringTable.GetString(this.m_Description, false);
		}

		// Token: 0x0400267C RID: 9852
		public int m_ID = -1;

		// Token: 0x0400267D RID: 9853
		public string m_StrID = "";

		// Token: 0x0400267E RID: 9854
		public string m_Name = "";

		// Token: 0x0400267F RID: 9855
		public string m_NameEng = "";

		// Token: 0x04002680 RID: 9856
		public NKMWorldMapCityTemplet.CityType m_CityType;

		// Token: 0x04002681 RID: 9857
		public int m_MaxLevel = 1;

		// Token: 0x04002682 RID: 9858
		public string m_Title = "";

		// Token: 0x04002683 RID: 9859
		public string m_Description = "";

		// Token: 0x0200123D RID: 4669
		public enum CityType
		{
			// Token: 0x0400954A RID: 38218
			CT_INVALID,
			// Token: 0x0400954B RID: 38219
			CT_MINING,
			// Token: 0x0400954C RID: 38220
			CT_RELAY,
			// Token: 0x0400954D RID: 38221
			CT_DEFENCE
		}
	}
}
