using System;
using System.Collections.Generic;
using Cs.Logging;
using NKC;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200047A RID: 1146
	public class NKMTacticalCommandTemplet : INKMTemplet
	{
		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001F27 RID: 7975 RVA: 0x00093F8D File Offset: 0x0009218D
		public int Key
		{
			get
			{
				return (int)this.m_TCID;
			}
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x00093F95 File Offset: 0x00092195
		public bool CheckEnemyTargetBuffExist()
		{
			return this.m_lstBuffStrID_Enemy != null && this.m_lstBuffStrID_Enemy.Count > 0;
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x00093FB2 File Offset: 0x000921B2
		public bool CheckMyTeamTargetBuffExist()
		{
			return this.m_lstBuffStrID_MyTeam != null && this.m_lstBuffStrID_MyTeam.Count > 0;
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x00093FD0 File Offset: 0x000921D0
		public static NKMTacticalCommandTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMTacticalCommand.cs", 245))
			{
				return null;
			}
			NKMTacticalCommandTemplet nkmtacticalCommandTemplet = new NKMTacticalCommandTemplet();
			cNKMLua.GetData("m_TCID", ref nkmtacticalCommandTemplet.m_TCID);
			cNKMLua.GetData("m_TCStrID", ref nkmtacticalCommandTemplet.m_TCStrID);
			cNKMLua.GetData("m_TCName", ref nkmtacticalCommandTemplet.m_TCName);
			if (cNKMLua.OpenTable("m_lstTCDescMyTeam"))
			{
				int num = 1;
				string item = "";
				while (cNKMLua.GetData(num, ref item))
				{
					nkmtacticalCommandTemplet.m_lstTCDescMyTeam.Add(item);
					num++;
				}
				cNKMLua.CloseTable();
			}
			if (cNKMLua.OpenTable("m_lstTCDescEnemy"))
			{
				int num2 = 1;
				string item2 = "";
				while (cNKMLua.GetData(num2, ref item2))
				{
					nkmtacticalCommandTemplet.m_lstTCDescEnemy.Add(item2);
					num2++;
				}
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_TCIconName", ref nkmtacticalCommandTemplet.m_TCIconName);
			cNKMLua.GetData("m_TCEffectName", ref nkmtacticalCommandTemplet.m_TCEffectName);
			cNKMLua.GetData("m_TCEffectSound", ref nkmtacticalCommandTemplet.m_TCEffectSound);
			cNKMLua.GetData("m_fCoolTime", ref nkmtacticalCommandTemplet.m_fCoolTime);
			cNKMLua.GetData<NKM_TACTICAL_COMMAND_TYPE>("m_NKM_TACTICAL_COMMAND_TYPE", ref nkmtacticalCommandTemplet.m_NKM_TACTICAL_COMMAND_TYPE);
			nkmtacticalCommandTemplet.m_listComboType.Clear();
			if (nkmtacticalCommandTemplet.m_NKM_TACTICAL_COMMAND_TYPE == NKM_TACTICAL_COMMAND_TYPE.NTCT_COMBO)
			{
				string comboValue = "";
				int num3 = 1;
				while (cNKMLua.GetData(string.Format("m_ComboValue{0}", num3), ref comboValue))
				{
					NKMTacticalCombo nkmtacticalCombo = new NKMTacticalCombo();
					if (!nkmtacticalCombo.Load(comboValue))
					{
						break;
					}
					nkmtacticalCommandTemplet.m_listComboType.Add(nkmtacticalCombo);
					num3++;
				}
				if (nkmtacticalCommandTemplet.m_listComboType.Count <= 0)
				{
					Log.ErrorAndExit("[NKMTacticalCommandTemplet] ComboValue is not found, m_TCStrID : " + nkmtacticalCommandTemplet.m_TCStrID, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMTacticalCommand.cs", 314);
					return null;
				}
			}
			cNKMLua.GetData("m_fComboResetCoolTime", ref nkmtacticalCommandTemplet.m_fComboResetCoolTime);
			cNKMLua.GetData("m_StartCost", ref nkmtacticalCommandTemplet.m_StartCost);
			cNKMLua.GetData("m_CostAdd", ref nkmtacticalCommandTemplet.m_CostAdd);
			if (cNKMLua.OpenTable("m_lstBuffStrID_MyTeam"))
			{
				int num4 = 1;
				string item3 = "";
				while (cNKMLua.GetData(num4, ref item3))
				{
					nkmtacticalCommandTemplet.m_lstBuffStrID_MyTeam.Add(item3);
					num4++;
				}
				cNKMLua.CloseTable();
			}
			if (cNKMLua.OpenTable("m_lstBuffStrID_Enemy"))
			{
				int num5 = 1;
				string item4 = "";
				while (cNKMLua.GetData(num5, ref item4))
				{
					nkmtacticalCommandTemplet.m_lstBuffStrID_Enemy.Add(item4);
					num5++;
				}
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_fCostPump", ref nkmtacticalCommandTemplet.m_fCostPump);
			cNKMLua.GetData("m_fCostPumpPerLevel", ref nkmtacticalCommandTemplet.m_fCostPumpPerLevel);
			cNKMLua.GetData("m_bTargetBossMyTeam", ref nkmtacticalCommandTemplet.m_bTargetBossMyTeam);
			cNKMLua.GetData("m_bTargetBossEnemy", ref nkmtacticalCommandTemplet.m_bTargetBossEnemy);
			return nkmtacticalCommandTemplet;
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x00094287 File Offset: 0x00092487
		public static NKMTacticalCommandTemplet Find(int key)
		{
			return NKMTempletContainer<NKMTacticalCommandTemplet>.Find(key);
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x0009428F File Offset: 0x0009248F
		public void Join()
		{
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x00094294 File Offset: 0x00092494
		public void Validate()
		{
			if (this.m_lstBuffStrID_Enemy != null)
			{
				foreach (string text in this.m_lstBuffStrID_Enemy)
				{
					if (NKMBuffManager.GetBuffTempletByStrID(text) == null)
					{
						Log.ErrorAndExit(string.Concat(new string[]
						{
							"[NKMTacticalCommandTemplet] m_lstBuffStrID_Enemy is invalid. m_TCStrID [",
							this.m_TCStrID,
							"], m_lstBuffStrID_Enemy [",
							text,
							"]"
						}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMTacticalCommand.cs", 378);
					}
				}
			}
			if (this.m_lstBuffStrID_MyTeam != null)
			{
				foreach (string text2 in this.m_lstBuffStrID_MyTeam)
				{
					if (NKMBuffManager.GetBuffTempletByStrID(text2) == null)
					{
						Log.ErrorAndExit(string.Concat(new string[]
						{
							"[NKMTacticalCommandTemplet] m_lstBuffStrID_Team is invalid. m_TCStrID [",
							this.m_TCStrID,
							"], m_lstBuffStrID_Team [",
							text2,
							"]"
						}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMTacticalCommand.cs", 389);
					}
				}
			}
			if (this.m_lstTCDescMyTeam != null && this.m_lstBuffStrID_MyTeam != null && this.m_lstTCDescMyTeam.Count != this.m_lstBuffStrID_MyTeam.Count)
			{
				Log.ErrorAndExit("[NKMTacticalCommandTemplet] m_lstTCDescMyTeam and m_lstBuffStrID_MyTeam Count different, m_TCStrID = " + this.m_TCStrID, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMTacticalCommand.cs", 398);
			}
			if (this.m_lstTCDescEnemy != null && this.m_lstBuffStrID_Enemy != null && this.m_lstTCDescEnemy.Count != this.m_lstBuffStrID_Enemy.Count)
			{
				Log.ErrorAndExit("[NKMTacticalCommandTemplet] m_lstTCDescEnemy and m_lstBuffStrID_Enemy Count different : , m_TCStrID = " + this.m_TCStrID, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMTacticalCommand.cs", 406);
			}
			if (this.m_listComboType != null)
			{
				for (int i = 0; i < this.m_listComboType.Count; i++)
				{
					NKMTacticalCombo nkmtacticalCombo = this.m_listComboType[i];
					if (nkmtacticalCombo == null)
					{
						Log.ErrorAndExit("[NKMTacticalCommandTemplet] Validate NKMTacticalCombo null found, m_TCStrID = " + this.m_TCStrID, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMTacticalCommand.cs", 417);
					}
					else if (nkmtacticalCombo.m_NKM_TACTICAL_COMBO_TYPE == NKM_TACTICAL_COMBO_TYPE.NTCBT_RESPAWN_COST)
					{
						int valueInt = nkmtacticalCombo.m_ValueInt;
						if (valueInt <= 0 || valueInt > 10)
						{
							Log.ErrorAndExit(string.Format("[NKMTacticalCommandTemplet] NTCBT_RESPAWN_COST is invalid, m_ValueInt is {0}", nkmtacticalCombo.m_ValueInt) + ", m_TCStrID = " + this.m_TCStrID, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMTacticalCommand.cs", 426);
						}
					}
					else if (nkmtacticalCombo.m_NKM_TACTICAL_COMBO_TYPE == NKM_TACTICAL_COMBO_TYPE.NTCBT_UNIT_STR_ID && NKMUnitManager.GetUnitTempletBase(nkmtacticalCombo.m_Value) == null)
					{
						Log.ErrorAndExit("[NKMTacticalCommandTemplet] NTCBT_UNIT_ID is invalid, m_ValueInt is " + nkmtacticalCombo.m_Value + ", m_TCStrID = " + this.m_TCStrID, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMTacticalCommand.cs", 433);
					}
				}
			}
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x0009453C File Offset: 0x0009273C
		public float GetNeedCost(NKMTacticalCommandData cNKMTacticalCommandData)
		{
			if (cNKMTacticalCommandData == null)
			{
				return 0f;
			}
			float num = (float)(this.m_StartCost + this.m_CostAdd * (int)cNKMTacticalCommandData.m_UseCount);
			if (num > 9f)
			{
				num = 9f;
			}
			return num;
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x00094577 File Offset: 0x00092777
		public string GetTCName()
		{
			return NKCStringTable.GetString(this.m_TCName, false);
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x00094585 File Offset: 0x00092785
		public string GetTCDescMyTeam(int index)
		{
			if (this.m_lstTCDescMyTeam == null)
			{
				return "";
			}
			if (index < 0 || index >= this.m_lstTCDescMyTeam.Count)
			{
				return "";
			}
			return NKCStringTable.GetString(this.m_lstTCDescMyTeam[index], false);
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x000945BF File Offset: 0x000927BF
		public string GetTCDescEnemy(int index)
		{
			if (this.m_lstTCDescEnemy == null)
			{
				return "";
			}
			if (index < 0 || index >= this.m_lstTCDescEnemy.Count)
			{
				return "";
			}
			return NKCStringTable.GetString(this.m_lstTCDescEnemy[index], false);
		}

		// Token: 0x04001FAA RID: 8106
		public short m_TCID;

		// Token: 0x04001FAB RID: 8107
		public string m_TCStrID = "";

		// Token: 0x04001FAC RID: 8108
		public string m_TCName;

		// Token: 0x04001FAD RID: 8109
		public List<string> m_lstTCDescMyTeam = new List<string>();

		// Token: 0x04001FAE RID: 8110
		public List<string> m_lstTCDescEnemy = new List<string>();

		// Token: 0x04001FAF RID: 8111
		public string m_TCIconName = "";

		// Token: 0x04001FB0 RID: 8112
		public string m_TCEffectName = "";

		// Token: 0x04001FB1 RID: 8113
		public string m_TCEffectSound = "";

		// Token: 0x04001FB2 RID: 8114
		public float m_fCoolTime;

		// Token: 0x04001FB3 RID: 8115
		public NKM_TACTICAL_COMMAND_TYPE m_NKM_TACTICAL_COMMAND_TYPE;

		// Token: 0x04001FB4 RID: 8116
		public List<NKMTacticalCombo> m_listComboType = new List<NKMTacticalCombo>();

		// Token: 0x04001FB5 RID: 8117
		public float m_fComboResetCoolTime;

		// Token: 0x04001FB6 RID: 8118
		public int m_StartCost;

		// Token: 0x04001FB7 RID: 8119
		public int m_CostAdd;

		// Token: 0x04001FB8 RID: 8120
		public List<string> m_lstBuffStrID_MyTeam = new List<string>();

		// Token: 0x04001FB9 RID: 8121
		public List<string> m_lstBuffStrID_Enemy = new List<string>();

		// Token: 0x04001FBA RID: 8122
		public float m_fCostPump;

		// Token: 0x04001FBB RID: 8123
		public float m_fCostPumpPerLevel;

		// Token: 0x04001FBC RID: 8124
		public bool m_bTargetBossMyTeam;

		// Token: 0x04001FBD RID: 8125
		public bool m_bTargetBossEnemy;
	}
}
