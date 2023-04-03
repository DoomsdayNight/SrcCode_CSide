using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x0200079F RID: 1951
	public class NKCDeckViewGuide : MonoBehaviour
	{
		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06004CAD RID: 19629 RVA: 0x0016F83E File Offset: 0x0016DA3E
		public bool IsOpen
		{
			get
			{
				return base.gameObject.activeSelf;
			}
		}

		// Token: 0x06004CAE RID: 19630 RVA: 0x0016F84B File Offset: 0x0016DA4B
		public void Init(NKCDeckViewGuide.OnWarning onWarning)
		{
			this.dOnWarning = onWarning;
		}

		// Token: 0x06004CAF RID: 19631 RVA: 0x0016F854 File Offset: 0x0016DA54
		public void Active(bool bSet)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, bSet);
		}

		// Token: 0x06004CB0 RID: 19632 RVA: 0x0016F864 File Offset: 0x0016DA64
		public void SetData(NKMArmyData armyData, NKMDeckIndex deckIndex)
		{
			NKMDeckData deckData = armyData.GetDeckData(deckIndex);
			if (deckData == null)
			{
				return;
			}
			Dictionary<NKM_UNIT_ROLE_TYPE, int> dictionary = new Dictionary<NKM_UNIT_ROLE_TYPE, int>();
			Dictionary<NKM_FIND_TARGET_TYPE, int> dictionary2 = new Dictionary<NKM_FIND_TARGET_TYPE, int>();
			for (int i = 0; i < deckData.m_listDeckUnitUID.Count; i++)
			{
				long unitUid = deckData.m_listDeckUnitUID[i];
				NKMUnitData unitFromUID = armyData.GetUnitFromUID(unitUid);
				if (unitFromUID != null)
				{
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitFromUID);
					if (unitTempletBase != null)
					{
						if (dictionary.ContainsKey(unitTempletBase.m_NKM_UNIT_ROLE_TYPE))
						{
							Dictionary<NKM_UNIT_ROLE_TYPE, int> dictionary3 = dictionary;
							NKM_UNIT_ROLE_TYPE nkm_UNIT_ROLE_TYPE = unitTempletBase.m_NKM_UNIT_ROLE_TYPE;
							int num = dictionary3[nkm_UNIT_ROLE_TYPE];
							dictionary3[nkm_UNIT_ROLE_TYPE] = num + 1;
						}
						else
						{
							dictionary.Add(unitTempletBase.m_NKM_UNIT_ROLE_TYPE, 1);
						}
						NKM_FIND_TARGET_TYPE nkm_FIND_TARGET_TYPE = unitTempletBase.m_NKM_FIND_TARGET_TYPE;
						if (unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc != NKM_FIND_TARGET_TYPE.NFTT_INVALID)
						{
							nkm_FIND_TARGET_TYPE = unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc;
						}
						if (dictionary2.ContainsKey(nkm_FIND_TARGET_TYPE))
						{
							Dictionary<NKM_FIND_TARGET_TYPE, int> dictionary4 = dictionary2;
							NKM_FIND_TARGET_TYPE key = nkm_FIND_TARGET_TYPE;
							int num = dictionary4[key];
							dictionary4[key] = num + 1;
						}
						else
						{
							dictionary2.Add(nkm_FIND_TARGET_TYPE, 1);
						}
					}
				}
			}
			bool flag = false;
			flag |= this.SetRole(dictionary);
			flag |= this.SetAtk(dictionary2);
			NKCDeckViewGuide.OnWarning onWarning = this.dOnWarning;
			if (onWarning == null)
			{
				return;
			}
			onWarning(flag);
		}

		// Token: 0x06004CB1 RID: 19633 RVA: 0x0016F988 File Offset: 0x0016DB88
		private bool SetRole(Dictionary<NKM_UNIT_ROLE_TYPE, int> dicRole)
		{
			return false | this.SetRoleText(dicRole, NKM_UNIT_ROLE_TYPE.NURT_STRIKER, this.m_txtStriker) | this.SetRoleText(dicRole, NKM_UNIT_ROLE_TYPE.NURT_RANGER, this.m_txtRanger) | this.SetRoleText(dicRole, NKM_UNIT_ROLE_TYPE.NURT_SNIPER, this.m_txtSniper) | this.SetRoleText(dicRole, NKM_UNIT_ROLE_TYPE.NURT_DEFENDER, this.m_txtDefender) | this.SetRoleText(dicRole, NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER, this.m_txtSupporter) | this.SetRoleText(dicRole, NKM_UNIT_ROLE_TYPE.NURT_TOWER, this.m_txtTower) | this.SetRoleText(dicRole, NKM_UNIT_ROLE_TYPE.NURT_SIEGE, this.m_txtSiege);
		}

		// Token: 0x06004CB2 RID: 19634 RVA: 0x0016FA00 File Offset: 0x0016DC00
		private bool SetRoleText(Dictionary<NKM_UNIT_ROLE_TYPE, int> dicRole, NKM_UNIT_ROLE_TYPE role, Text textUI)
		{
			bool result = false;
			int num = 0;
			string hexRGB = "#FFFFFF";
			if (dicRole.ContainsKey(role))
			{
				num = dicRole[role];
			}
			if (role - NKM_UNIT_ROLE_TYPE.NURT_STRIKER > 3)
			{
				if (role - NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER > 2)
				{
				}
			}
			else
			{
				if (num == 0)
				{
					hexRGB = "#FF2626";
				}
				else if (num == 1)
				{
					hexRGB = "#FF7979";
				}
				result = (num < 2);
			}
			textUI.text = num.ToString();
			textUI.color = NKCUtil.GetColor(hexRGB);
			return result;
		}

		// Token: 0x06004CB3 RID: 19635 RVA: 0x0016FA6C File Offset: 0x0016DC6C
		private bool SetAtk(Dictionary<NKM_FIND_TARGET_TYPE, int> dicAtk)
		{
			bool result = false;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			foreach (KeyValuePair<NKM_FIND_TARGET_TYPE, int> keyValuePair in dicAtk)
			{
				switch (keyValuePair.Key)
				{
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY:
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_AIR_FIRST:
				case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY:
				case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_BOSS_LAST:
					num3 += keyValuePair.Value;
					break;
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_LAND:
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_LAND_RANGER_SUPPORTER_SNIPER_FIRST:
				case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_LAND:
				case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_LAND_BOSS_LAST:
					num += keyValuePair.Value;
					break;
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_AIR:
				case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_AIR:
				case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_AIR_BOSS_LAST:
					num2 += keyValuePair.Value;
					break;
				}
			}
			this.m_txtGround.text = num.ToString();
			this.m_txtAir.text = num2.ToString();
			this.m_txtAll.text = num3.ToString();
			this.m_txtGround.color = NKCUtil.GetColor(this.GetAtkColor(num + num3));
			this.m_txtAir.color = NKCUtil.GetColor(this.GetAtkColor(num2 + num3));
			this.m_txtAll.color = NKCUtil.GetColor(this.GetAtkColor(Mathf.Min(num + num3, num2 + num3)));
			return result;
		}

		// Token: 0x06004CB4 RID: 19636 RVA: 0x0016FBE4 File Offset: 0x0016DDE4
		private string GetAtkColor(int count)
		{
			if (count == 0)
			{
				return "#FF2626";
			}
			if (count <= 2)
			{
				return "#FF7979";
			}
			return "#FFFFFF";
		}

		// Token: 0x04003C63 RID: 15459
		[Header("Class")]
		public Text m_txtStriker;

		// Token: 0x04003C64 RID: 15460
		public Text m_txtRanger;

		// Token: 0x04003C65 RID: 15461
		public Text m_txtSniper;

		// Token: 0x04003C66 RID: 15462
		public Text m_txtDefender;

		// Token: 0x04003C67 RID: 15463
		public Text m_txtSupporter;

		// Token: 0x04003C68 RID: 15464
		public Text m_txtTower;

		// Token: 0x04003C69 RID: 15465
		public Text m_txtSiege;

		// Token: 0x04003C6A RID: 15466
		[Header("ATK Type")]
		public Text m_txtGround;

		// Token: 0x04003C6B RID: 15467
		public Text m_txtAir;

		// Token: 0x04003C6C RID: 15468
		public Text m_txtAll;

		// Token: 0x04003C6D RID: 15469
		private const string TEXT_COLOR_NORMAL = "#FFFFFF";

		// Token: 0x04003C6E RID: 15470
		private const string TEXT_COLOR_WARNING = "#FF2626";

		// Token: 0x04003C6F RID: 15471
		private const string TEXT_COLOR_WARNING2 = "#FF7979";

		// Token: 0x04003C70 RID: 15472
		private NKCDeckViewGuide.OnWarning dOnWarning;

		// Token: 0x0200145F RID: 5215
		// (Invoke) Token: 0x0600A886 RID: 43142
		public delegate void OnWarning(bool bWarning);
	}
}
