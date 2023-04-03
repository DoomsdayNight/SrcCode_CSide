using System;
using System.Collections;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Result
{
	// Token: 0x02000BA9 RID: 2985
	public class NKCUIResultSubUIUnitExp : NKCUIResultSubUIBase
	{
		// Token: 0x060089EB RID: 35307 RVA: 0x002EBE10 File Offset: 0x002EA010
		public void SetData(List<NKCUIResultSubUIUnitExp.UnitLevelupUIData> lstUnitLevelupData, int expBonusRate = 0, NKCUIResultSubUIUnitExp.UnitLevelupUIData AdditionalLevelupData = null, bool bIgnoreAutoClose = false)
		{
			if (AdditionalLevelupData == null && (lstUnitLevelupData == null || lstUnitLevelupData.Count == 0))
			{
				base.ProcessRequired = false;
				return;
			}
			base.ProcessRequired = true;
			this.m_bIgnoreAutoClose = bIgnoreAutoClose;
			this.m_lstUnitLevelupData = new List<NKCUIResultSubUIUnitExp.UnitLevelupUIData>(lstUnitLevelupData);
			if (AdditionalLevelupData != null)
			{
				this.m_lstUnitLevelupData.RemoveAll((NKCUIResultSubUIUnitExp.UnitLevelupUIData x) => x.m_UnitData.m_UnitUID == AdditionalLevelupData.m_UnitData.m_UnitUID);
			}
			this.m_AdditionalUnitLevelupData = AdditionalLevelupData;
			if (AdditionalLevelupData != null)
			{
				if (this.m_lstUnitLevelupData.Count > 0)
				{
					NKCUtil.SetGameobjectActive(this.m_AdditionalSlot, true);
					NKCUtil.SetGameobjectActive(this.m_rtSlotsAdditionalRoot, true);
					NKCUtil.SetGameobjectActive(this.m_rtSlotsNormalRoot, true);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_AdditionalSlot, true);
					NKCUtil.SetGameobjectActive(this.m_rtSlotsAdditionalRoot, true);
					NKCUtil.SetGameobjectActive(this.m_rtSlotsNormalRoot, false);
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_AdditionalSlot, false);
				NKCUtil.SetGameobjectActive(this.m_rtSlotsAdditionalRoot, false);
				NKCUtil.SetGameobjectActive(this.m_rtSlotsNormalRoot, true);
			}
			for (int i = 0; i < this.m_lstUnitSlotList.Count; i++)
			{
				NKCUIResultUnitSlot nkcuiresultUnitSlot = this.m_lstUnitSlotList[i];
				if (this.m_lstUnitLevelupData.Count <= i)
				{
					NKCUtil.SetGameobjectActive(nkcuiresultUnitSlot, false);
				}
				else
				{
					NKCUIResultSubUIUnitExp.UnitLevelupUIData unitLevelupUIData = this.m_lstUnitLevelupData[i];
					if (unitLevelupUIData.m_UnitData == null)
					{
						NKCUtil.SetGameobjectActive(nkcuiresultUnitSlot, false);
					}
					else if (NKMUnitManager.GetUnitTempletBase(unitLevelupUIData.m_UnitData.m_UnitID) == null)
					{
						NKCUtil.SetGameobjectActive(nkcuiresultUnitSlot, false);
					}
					else
					{
						NKCUtil.SetGameobjectActive(nkcuiresultUnitSlot, true);
						nkcuiresultUnitSlot.SetData(unitLevelupUIData, this.m_spSlotBGN, this.m_spSlotBGR, this.m_spSlotBGSR, this.m_spSlotBGSSR);
						nkcuiresultUnitSlot.SetLeader(unitLevelupUIData.m_bIsLeader);
					}
				}
			}
			if (AdditionalLevelupData != null)
			{
				NKCUtil.SetGameobjectActive(this.m_AdditionalSlot, true);
				this.m_AdditionalSlot.SetData(AdditionalLevelupData, this.m_spSlotBGN, this.m_spSlotBGR, this.m_spSlotBGSR, this.m_spSlotBGSSR);
				this.m_AdditionalSlot.SetLeader(false);
			}
			NKCUtil.SetGameobjectActive(this.m_lbBonusExp, expBonusRate > 0);
			if (expBonusRate > 0)
			{
				this.m_lbBonusExp.text = string.Format(NKCUtilString.GET_STRING_RESULT_BONUS_EXP, expBonusRate);
			}
			NKCUtil.SetGameobjectActive(this.m_DUNGEON_RESULT_EXP, false);
		}

		// Token: 0x060089EC RID: 35308 RVA: 0x002EC040 File Offset: 0x002EA240
		protected override IEnumerator InnerProcess(bool bAutoSkip)
		{
			yield return null;
			while (this.m_cgLayout.alpha < 1f)
			{
				yield return null;
			}
			int num = 0;
			while (num < this.m_lstUnitLevelupData.Count && this.m_lstUnitSlotList.Count > num)
			{
				NKCUIResultSubUIUnitExp.UnitLevelupUIData unitLevelupUIData = this.m_lstUnitLevelupData[num];
				NKCUIResultUnitSlot nkcuiresultUnitSlot = this.m_lstUnitSlotList[num];
				if (unitLevelupUIData.m_UnitData == null)
				{
					NKCUtil.SetGameobjectActive(nkcuiresultUnitSlot, false);
				}
				else
				{
					nkcuiresultUnitSlot.StartExpProcess(unitLevelupUIData.m_UnitData.m_UnitID, unitLevelupUIData.m_iLevelOld, unitLevelupUIData.m_iExpOld, unitLevelupUIData.m_iLevelNew, unitLevelupUIData.m_iExpNew, unitLevelupUIData.m_iTotalExpGain);
				}
				num++;
			}
			if (this.m_AdditionalUnitLevelupData != null)
			{
				this.m_AdditionalSlot.StartExpProcess(this.m_AdditionalUnitLevelupData.m_UnitData.m_UnitID, this.m_AdditionalUnitLevelupData.m_iLevelOld, this.m_AdditionalUnitLevelupData.m_iExpOld, this.m_AdditionalUnitLevelupData.m_iLevelNew, this.m_AdditionalUnitLevelupData.m_iExpNew, this.m_AdditionalUnitLevelupData.m_iTotalExpGain);
			}
			while (!this.IsProcessFinished())
			{
				yield return null;
			}
			this.FinishProcess();
			yield return null;
			yield break;
		}

		// Token: 0x060089ED RID: 35309 RVA: 0x002EC050 File Offset: 0x002EA250
		public override bool IsProcessFinished()
		{
			bool flag = !this.m_lstUnitSlotList.Exists((NKCUIResultUnitSlot x) => x.gameObject.activeInHierarchy && !x.ProgressFinished);
			bool flag2 = !this.m_AdditionalSlot.gameObject.activeInHierarchy || this.m_AdditionalSlot.ProgressFinished;
			return flag && flag2;
		}

		// Token: 0x060089EE RID: 35310 RVA: 0x002EC0B0 File Offset: 0x002EA2B0
		public override void FinishProcess()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			foreach (NKCUIResultUnitSlot nkcuiresultUnitSlot in this.m_lstUnitSlotList)
			{
				nkcuiresultUnitSlot.Finish();
			}
			this.m_AdditionalSlot.Finish();
			this.m_AdditionalUnitLevelupData = null;
			this.m_lstUnitLevelupData.Clear();
		}

		// Token: 0x0400765E RID: 30302
		public RectTransform m_rtSlotsNormalRoot;

		// Token: 0x0400765F RID: 30303
		public RectTransform m_rtSlotsAdditionalRoot;

		// Token: 0x04007660 RID: 30304
		public List<NKCUIResultUnitSlot> m_lstUnitSlotList;

		// Token: 0x04007661 RID: 30305
		public Text m_lbBonusExp;

		// Token: 0x04007662 RID: 30306
		public NKCUIResultUnitSlot m_AdditionalSlot;

		// Token: 0x04007663 RID: 30307
		public Sprite m_spSlotBGN;

		// Token: 0x04007664 RID: 30308
		public Sprite m_spSlotBGR;

		// Token: 0x04007665 RID: 30309
		public Sprite m_spSlotBGSR;

		// Token: 0x04007666 RID: 30310
		public Sprite m_spSlotBGSSR;

		// Token: 0x04007667 RID: 30311
		public GameObject m_DUNGEON_RESULT_EXP;

		// Token: 0x04007668 RID: 30312
		public CanvasGroup m_cgLayout;

		// Token: 0x04007669 RID: 30313
		private List<NKCUIResultSubUIUnitExp.UnitLevelupUIData> m_lstUnitLevelupData;

		// Token: 0x0400766A RID: 30314
		private NKCUIResultSubUIUnitExp.UnitLevelupUIData m_AdditionalUnitLevelupData;

		// Token: 0x02001970 RID: 6512
		public enum UNIT_LOYALTY
		{
			// Token: 0x0400ABEE RID: 44014
			None,
			// Token: 0x0400ABEF RID: 44015
			Up,
			// Token: 0x0400ABF0 RID: 44016
			Down
		}

		// Token: 0x02001971 RID: 6513
		public class UnitLevelupUIData
		{
			// Token: 0x0400ABF1 RID: 44017
			public NKMUnitData m_UnitData;

			// Token: 0x0400ABF2 RID: 44018
			public int m_iLevelOld;

			// Token: 0x0400ABF3 RID: 44019
			public int m_iExpOld;

			// Token: 0x0400ABF4 RID: 44020
			public int m_iLevelNew;

			// Token: 0x0400ABF5 RID: 44021
			public int m_iExpNew;

			// Token: 0x0400ABF6 RID: 44022
			public int m_iTotalExpGain;

			// Token: 0x0400ABF7 RID: 44023
			public bool m_bIsLeader;

			// Token: 0x0400ABF8 RID: 44024
			public NKCUIResultSubUIUnitExp.UNIT_LOYALTY m_loyalty;
		}
	}
}
