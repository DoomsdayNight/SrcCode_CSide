using System;
using System.Collections.Generic;
using Cs.Logging;
using NKC;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x020003A6 RID: 934
	public sealed class NKMBattleConditionTemplet : INKMTemplet
	{
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06001875 RID: 6261 RVA: 0x00062B5C File Offset: 0x00060D5C
		public int Key
		{
			get
			{
				return this.m_BCondID;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x00062B64 File Offset: 0x00060D64
		public string DebugName
		{
			get
			{
				return string.Format("[{0}]{1}", this.BattleCondID, this.BattleCondStrID);
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06001877 RID: 6263 RVA: 0x00062B81 File Offset: 0x00060D81
		public int BattleCondID
		{
			get
			{
				return this.m_BCondID;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x00062B89 File Offset: 0x00060D89
		public string BattleCondStrID
		{
			get
			{
				return this.m_BCondStrID;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x00062B91 File Offset: 0x00060D91
		public string BattleCondName
		{
			get
			{
				return this.m_BCondName;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600187A RID: 6266 RVA: 0x00062B99 File Offset: 0x00060D99
		public string BattleCondDesc
		{
			get
			{
				return this.m_BCondDescription;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x00062BA1 File Offset: 0x00060DA1
		public NKMBattleConditionTemplet.USE_CONTENT_TYPE UseContentsType
		{
			get
			{
				return this.m_UseContentsType;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600187C RID: 6268 RVA: 0x00062BA9 File Offset: 0x00060DA9
		public string BattleCondInfoIcon
		{
			get
			{
				return this.m_BCondInfoIcon;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600187D RID: 6269 RVA: 0x00062BB1 File Offset: 0x00060DB1
		public string BattleCondWFIcon
		{
			get
			{
				return this.m_BCondWarfareIcon;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x00062BB9 File Offset: 0x00060DB9
		public string BattleCondIngameIcon
		{
			get
			{
				return this.m_BCondIngameIcon;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x00062BC1 File Offset: 0x00060DC1
		public string BattleCondMapStrID
		{
			get
			{
				return this.m_BCondMapStrID;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x00062BC9 File Offset: 0x00060DC9
		public HashSet<string> AffectTeamUpID
		{
			get
			{
				return this.m_hashAffectTeamUpID;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x00062BD1 File Offset: 0x00060DD1
		public bool AffectSHIP
		{
			get
			{
				return this.m_bAffectSHIP;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x00062BD9 File Offset: 0x00060DD9
		public bool AffectCOUNTER
		{
			get
			{
				return this.m_bAffectCOUNTER;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x00062BE1 File Offset: 0x00060DE1
		public bool AffectSOLDIER
		{
			get
			{
				return this.m_bAffectSOLDIER;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x00062BE9 File Offset: 0x00060DE9
		public bool AffectMECHANIC
		{
			get
			{
				return this.m_bAffectMECHANIC;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x00062BF1 File Offset: 0x00060DF1
		public bool AffectCORRUPT
		{
			get
			{
				return this.m_bAffectCORRUPT;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x00062BF9 File Offset: 0x00060DF9
		public NKM_UNIT_ROLE_TYPE AffectUnitRole
		{
			get
			{
				return this.m_AffectUnitRole;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06001887 RID: 6279 RVA: 0x00062C01 File Offset: 0x00060E01
		public bool HitLand
		{
			get
			{
				return this.m_bHitLand;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06001888 RID: 6280 RVA: 0x00062C09 File Offset: 0x00060E09
		public bool HitAir
		{
			get
			{
				return this.m_bHitAir;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06001889 RID: 6281 RVA: 0x00062C11 File Offset: 0x00060E11
		public HashSet<int> hashAffectUnitID
		{
			get
			{
				return this.m_hashAffectUnitID;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x00062C19 File Offset: 0x00060E19
		public HashSet<int> hashIgnoreUnitID
		{
			get
			{
				return this.m_hashIgnoreUnitID;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x00062C21 File Offset: 0x00060E21
		public HashSet<string> AllyBCondUnitStrIDList
		{
			get
			{
				return this.m_listAllyBCondUnitStrID;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600188C RID: 6284 RVA: 0x00062C29 File Offset: 0x00060E29
		public float BoostResource
		{
			get
			{
				return this.m_BoostResource;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x00062C31 File Offset: 0x00060E31
		public HashSet<string> AllyBuffStrIDList
		{
			get
			{
				return this.m_listAllyBuffStrID;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x0600188E RID: 6286 RVA: 0x00062C39 File Offset: 0x00060E39
		public HashSet<string> EnemyBCondUnitStrIDList
		{
			get
			{
				return this.m_listEnemyBCondUnitStrID;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x00062C41 File Offset: 0x00060E41
		public HashSet<string> EnemyBuffStrIDList
		{
			get
			{
				return this.m_listEnemyBuffStrID;
			}
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x00062C4C File Offset: 0x00060E4C
		public static NKMBattleConditionTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			NKMBattleConditionTemplet nkmbattleConditionTemplet = new NKMBattleConditionTemplet();
			bool flag = true;
			flag &= cNKMLua.GetData("m_BCondID", ref nkmbattleConditionTemplet.m_BCondID);
			flag &= cNKMLua.GetData("m_BCondStrID", ref nkmbattleConditionTemplet.m_BCondStrID);
			flag &= cNKMLua.GetData("m_BCondName", ref nkmbattleConditionTemplet.m_BCondName);
			flag &= cNKMLua.GetData("m_BCondDescription", ref nkmbattleConditionTemplet.m_BCondDescription);
			flag &= cNKMLua.GetData<NKMBattleConditionTemplet.USE_CONTENT_TYPE>("m_UseContentsType", ref nkmbattleConditionTemplet.m_UseContentsType);
			cNKMLua.GetData("m_BCondInfoIcon", ref nkmbattleConditionTemplet.m_BCondInfoIcon);
			cNKMLua.GetData("m_BCondWarfareIcon", ref nkmbattleConditionTemplet.m_BCondWarfareIcon);
			cNKMLua.GetData("m_BCondIngameIcon", ref nkmbattleConditionTemplet.m_BCondIngameIcon);
			cNKMLua.GetData("m_BCondMapStrID", ref nkmbattleConditionTemplet.m_BCondMapStrID);
			cNKMLua.GetData("m_hashAffectTeamUpID", nkmbattleConditionTemplet.m_hashAffectTeamUpID);
			flag &= cNKMLua.GetData("m_bAffectSHIP", ref nkmbattleConditionTemplet.m_bAffectSHIP);
			flag &= cNKMLua.GetData("m_bAffectCOUNTER", ref nkmbattleConditionTemplet.m_bAffectCOUNTER);
			flag &= cNKMLua.GetData("m_bAffectSOLDIER", ref nkmbattleConditionTemplet.m_bAffectSOLDIER);
			flag &= cNKMLua.GetData("m_bAffectMECHANIC", ref nkmbattleConditionTemplet.m_bAffectMECHANIC);
			flag &= cNKMLua.GetData("m_bAffectCORRUPT", ref nkmbattleConditionTemplet.m_bAffectCORRUPT);
			flag &= cNKMLua.GetData<NKM_UNIT_ROLE_TYPE>("m_AffectUnitRole", ref nkmbattleConditionTemplet.m_AffectUnitRole);
			flag &= cNKMLua.GetData("m_bHitLand", ref nkmbattleConditionTemplet.m_bHitLand);
			flag &= cNKMLua.GetData("m_bHitAir", ref nkmbattleConditionTemplet.m_bHitAir);
			if (cNKMLua.OpenTable("m_hashAffectUnitID"))
			{
				nkmbattleConditionTemplet.m_hashAffectUnitID.Clear();
				string unitStrID = "";
				int num = 1;
				while (cNKMLua.GetData(num, ref unitStrID))
				{
					int unitID = NKMUnitManager.GetUnitID(unitStrID);
					if (unitID > 0 && !nkmbattleConditionTemplet.m_hashAffectUnitID.Contains(unitID))
					{
						nkmbattleConditionTemplet.m_hashAffectUnitID.Add(unitID);
					}
					num++;
				}
				cNKMLua.CloseTable();
			}
			if (cNKMLua.OpenTable("m_hashIgnoreUnitID"))
			{
				nkmbattleConditionTemplet.m_hashIgnoreUnitID.Clear();
				string unitStrID2 = "";
				int num2 = 1;
				while (cNKMLua.GetData(num2, ref unitStrID2))
				{
					int unitID2 = NKMUnitManager.GetUnitID(unitStrID2);
					if (unitID2 > 0 && !nkmbattleConditionTemplet.m_hashIgnoreUnitID.Contains(unitID2))
					{
						nkmbattleConditionTemplet.m_hashIgnoreUnitID.Add(unitID2);
					}
					num2++;
				}
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_listAllyBCondUnitStrID", nkmbattleConditionTemplet.m_listAllyBCondUnitStrID);
			cNKMLua.GetData("m_BoostResource", ref nkmbattleConditionTemplet.m_BoostResource);
			cNKMLua.GetData("m_listAllyBuffStrID", nkmbattleConditionTemplet.m_listAllyBuffStrID);
			cNKMLua.GetData("m_listEnemyBCondUnitStrID", nkmbattleConditionTemplet.m_listEnemyBCondUnitStrID);
			cNKMLua.GetData("m_listEnemyBuffStrID", nkmbattleConditionTemplet.m_listEnemyBuffStrID);
			if (!flag)
			{
				Log.Error("NKMBattleConditionTemplet LoadFromLUA Fail", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMBattleConditionManager.cs", 148);
				return null;
			}
			return nkmbattleConditionTemplet;
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x00062EFD File Offset: 0x000610FD
		public HashSet<string> GetUnitStrIDList(NKM_TEAM_TYPE teamType)
		{
			if (teamType - NKM_TEAM_TYPE.NTT_A1 <= 1)
			{
				return this.m_listAllyBCondUnitStrID;
			}
			if (teamType - NKM_TEAM_TYPE.NTT_B1 > 1)
			{
				return null;
			}
			return this.m_listEnemyBCondUnitStrID;
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x00062F1C File Offset: 0x0006111C
		public HashSet<string> GetBuffStrIDList(NKM_TEAM_TYPE targetTeamType, NKM_TEAM_TYPE usingTeamType = NKM_TEAM_TYPE.NTT_A1)
		{
			if (!NKMGame.IsATeamStaticFunc(targetTeamType) && !NKMGame.IsBTeamStaticFunc(targetTeamType))
			{
				return null;
			}
			if (NKMGame.IsSameTeamStaticFunc(targetTeamType, usingTeamType))
			{
				return this.m_listAllyBuffStrID;
			}
			return this.m_listEnemyBuffStrID;
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x00062F48 File Offset: 0x00061148
		private void CheckValidation()
		{
			if ((this.m_listAllyBuffStrID.Count > 0 || this.m_listEnemyBuffStrID.Count > 0) && !this.m_bAffectSHIP && ((!this.m_bAffectCOUNTER && !this.m_bAffectSOLDIER && !this.m_bAffectMECHANIC) || (!this.m_bHitLand && !this.m_bHitAir)) && this.m_hashAffectUnitID.Count == 0 && this.m_hashIgnoreUnitID.Count == 0)
			{
				Log.ErrorAndExit(string.Format("[BattleConditionTemplet] 버프를 받을 유닛이 존재하지 않음 m_BCondID : {0}, m_BCondStrID : {1}", this.m_BCondID, this.m_BCondStrID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMBattleConditionManager.cs", 202);
			}
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x00062FE5 File Offset: 0x000611E5
		public void Join()
		{
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x00062FE8 File Offset: 0x000611E8
		public void Validate()
		{
			if (this.m_listAllyBuffStrID != null)
			{
				foreach (string text in this.m_listAllyBuffStrID)
				{
					if (NKMBuffManager.GetBuffTempletByStrID(text) == null)
					{
						Log.ErrorAndExit(string.Format("[NKMBattleConditionTemplet]  m_listAllyBuffStrID is invalid. m_BCondID [{0}], m_BCondStrID [{1}], m_listAllyBuffStrID [{2}]", this.m_BCondID, this.m_BCondStrID, text), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMBattleConditionManager.cs", 218);
					}
				}
			}
			if (this.m_listEnemyBuffStrID != null)
			{
				foreach (string text2 in this.m_listEnemyBuffStrID)
				{
					if (NKMBuffManager.GetBuffTempletByStrID(text2) == null)
					{
						Log.ErrorAndExit(string.Format("[NKMBattleConditionTemplet]  m_listEnemyBuffStrID is invalid. m_BCondID [{0}], m_BCondStrID [{1}], m_listEnemyBuffStrID [{2}]", this.m_BCondID, this.m_BCondStrID, text2), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMBattleConditionManager.cs", 229);
					}
				}
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06001896 RID: 6294 RVA: 0x000630E8 File Offset: 0x000612E8
		public string BattleCondName_Translated
		{
			get
			{
				return NKCStringTable.GetString(this.BattleCondName, false);
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06001897 RID: 6295 RVA: 0x000630F6 File Offset: 0x000612F6
		public string BattleCondDesc_Translated
		{
			get
			{
				return NKCStringTable.GetString(this.BattleCondDesc, false);
			}
		}

		// Token: 0x04001040 RID: 4160
		private int m_BCondID;

		// Token: 0x04001041 RID: 4161
		private string m_BCondStrID = "";

		// Token: 0x04001042 RID: 4162
		private string m_BCondName = "";

		// Token: 0x04001043 RID: 4163
		private string m_BCondDescription = "";

		// Token: 0x04001044 RID: 4164
		private NKMBattleConditionTemplet.USE_CONTENT_TYPE m_UseContentsType;

		// Token: 0x04001045 RID: 4165
		private string m_BCondInfoIcon = "";

		// Token: 0x04001046 RID: 4166
		private string m_BCondWarfareIcon = "";

		// Token: 0x04001047 RID: 4167
		private string m_BCondIngameIcon = "";

		// Token: 0x04001048 RID: 4168
		private string m_BCondMapStrID = "";

		// Token: 0x04001049 RID: 4169
		private readonly HashSet<string> m_hashAffectTeamUpID = new HashSet<string>();

		// Token: 0x0400104A RID: 4170
		private bool m_bAffectSHIP;

		// Token: 0x0400104B RID: 4171
		private bool m_bAffectCOUNTER;

		// Token: 0x0400104C RID: 4172
		private bool m_bAffectSOLDIER;

		// Token: 0x0400104D RID: 4173
		private bool m_bAffectMECHANIC;

		// Token: 0x0400104E RID: 4174
		private bool m_bAffectCORRUPT;

		// Token: 0x0400104F RID: 4175
		private NKM_UNIT_ROLE_TYPE m_AffectUnitRole;

		// Token: 0x04001050 RID: 4176
		private bool m_bHitLand;

		// Token: 0x04001051 RID: 4177
		private bool m_bHitAir;

		// Token: 0x04001052 RID: 4178
		private HashSet<int> m_hashAffectUnitID = new HashSet<int>();

		// Token: 0x04001053 RID: 4179
		private HashSet<int> m_hashIgnoreUnitID = new HashSet<int>();

		// Token: 0x04001054 RID: 4180
		private readonly HashSet<string> m_listAllyBCondUnitStrID = new HashSet<string>();

		// Token: 0x04001055 RID: 4181
		private float m_BoostResource;

		// Token: 0x04001056 RID: 4182
		private readonly HashSet<string> m_listAllyBuffStrID = new HashSet<string>();

		// Token: 0x04001057 RID: 4183
		private readonly HashSet<string> m_listEnemyBCondUnitStrID = new HashSet<string>();

		// Token: 0x04001058 RID: 4184
		private readonly HashSet<string> m_listEnemyBuffStrID = new HashSet<string>();

		// Token: 0x020011AA RID: 4522
		public enum USE_CONTENT_TYPE
		{
			// Token: 0x040092D6 RID: 37590
			UCT_BATTLE_CONDITION,
			// Token: 0x040092D7 RID: 37591
			UCT_DIVE_ARTIFACT,
			// Token: 0x040092D8 RID: 37592
			UCT_GUILD_DUNGEON_ARTIFACT,
			// Token: 0x040092D9 RID: 37593
			UCT_FIERCE_PENALTY
		}
	}
}
