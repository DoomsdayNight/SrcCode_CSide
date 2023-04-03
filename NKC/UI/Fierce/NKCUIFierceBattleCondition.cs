using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Fierce
{
	// Token: 0x02000BB4 RID: 2996
	public class NKCUIFierceBattleCondition : MonoBehaviour
	{
		// Token: 0x06008A7C RID: 35452 RVA: 0x002F1904 File Offset: 0x002EFB04
		public void SetData(int idx, NKMBattleConditionTemplet battleTemplet, bool bAddTeamUpUnit = true)
		{
			if (battleTemplet == null)
			{
				return;
			}
			string msg = idx.ToString();
			if (idx <= 9)
			{
				msg = string.Format("0{0}", idx);
			}
			NKCUtil.SetLabelText(this.m_lbIdx, msg);
			NKCUtil.SetLabelText(this.m_txtBCDesc, battleTemplet.BattleCondDesc_Translated);
			NKCUtil.SetImageSprite(this.m_ImgBCICon, NKCUtil.GetSpriteBattleConditionICon(battleTemplet), false);
			if (!bAddTeamUpUnit)
			{
				NKCUtil.SetGameobjectActive(this.m_objBCTeamUp, false);
				return;
			}
			List<NKMUnitTempletBase> list = new List<NKMUnitTempletBase>();
			List<string> list2 = new List<string>();
			foreach (string item in battleTemplet.AffectTeamUpID)
			{
				if (!list2.Contains(item))
				{
					list2.Add(item);
				}
			}
			if (list2.Count > 0)
			{
				foreach (string teamUp in list2)
				{
					foreach (NKMUnitTempletBase nkmunitTempletBase in NKMUnitManager.GetListTeamUPUnitTempletBase(teamUp))
					{
						if (nkmunitTempletBase.PickupEnableByTag && nkmunitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL && nkmunitTempletBase.m_UnitID >= this.m_iMinID && nkmunitTempletBase.m_UnitID <= this.m_iMaxID && (nkmunitTempletBase.m_ShipGroupID == 0 || nkmunitTempletBase.m_ShipGroupID == nkmunitTempletBase.m_UnitID) && !list.Contains(nkmunitTempletBase))
						{
							list.Add(nkmunitTempletBase);
						}
					}
				}
			}
			if (list.Count > 0)
			{
				foreach (NKMUnitTempletBase nkmunitTempletBase2 in list)
				{
					NKCUISlot newInstance = NKCUISlot.GetNewInstance(this.m_rtBCTeamUpParent);
					if (null != newInstance)
					{
						newInstance.transform.localPosition = Vector3.zero;
						newInstance.transform.localScale = Vector3.one;
						NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(NKM_REWARD_TYPE.RT_UNIT, nkmunitTempletBase2.m_UnitID, 1, 0);
						NKCUtil.SetGameobjectActive(newInstance.gameObject, true);
						newInstance.SetData(data, true, null);
						this.m_lstTeamUpUnits.Add(newInstance);
					}
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objBCTeamUp, list.Count > 0);
		}

		// Token: 0x06008A7D RID: 35453 RVA: 0x002F1B7C File Offset: 0x002EFD7C
		public void Clear()
		{
			for (int i = 0; i < this.m_lstTeamUpUnits.Count; i++)
			{
				if (!(null == this.m_lstTeamUpUnits[i]))
				{
					UnityEngine.Object.Destroy(this.m_lstTeamUpUnits[i]);
					this.m_lstTeamUpUnits[i] = null;
				}
			}
			this.m_lstTeamUpUnits.Clear();
		}

		// Token: 0x04007744 RID: 30532
		public Text m_lbIdx;

		// Token: 0x04007745 RID: 30533
		public Image m_ImgBCICon;

		// Token: 0x04007746 RID: 30534
		public Text m_txtBCDesc;

		// Token: 0x04007747 RID: 30535
		public GameObject m_objBCTeamUp;

		// Token: 0x04007748 RID: 30536
		public RectTransform m_rtBCTeamUpParent;

		// Token: 0x04007749 RID: 30537
		[Header("unit display id range")]
		public int m_iMinID = 1001;

		// Token: 0x0400774A RID: 30538
		public int m_iMaxID = 9999;

		// Token: 0x0400774B RID: 30539
		private List<NKCUISlot> m_lstTeamUpUnits = new List<NKCUISlot>();
	}
}
