using System;
using System.Collections.Generic;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI.Fierce
{
	// Token: 0x02000BB6 RID: 2998
	public class NKCUIFierceBattleSelfPenaltyContent : MonoBehaviour
	{
		// Token: 0x1700161B RID: 5659
		// (get) Token: 0x06008A83 RID: 35459 RVA: 0x002F1C05 File Offset: 0x002EFE05
		public int PenaltyGroupID
		{
			get
			{
				return this.m_iPenaltyGroupID;
			}
		}

		// Token: 0x06008A84 RID: 35460 RVA: 0x002F1C0D File Offset: 0x002EFE0D
		public void Init(RectTransform rtParent)
		{
			base.gameObject.transform.SetParent(rtParent);
		}

		// Token: 0x06008A85 RID: 35461 RVA: 0x002F1C20 File Offset: 0x002EFE20
		public void SetData(List<NKMFiercePenaltyTemplet> lstPenaltys, OnClickPenalty dSlotClick)
		{
			if (lstPenaltys.Count <= 0)
			{
				return;
			}
			int num = 0;
			while (lstPenaltys.Count > num)
			{
				if (num == 0)
				{
					string @string = NKCStringTable.GetString(lstPenaltys[num].PenaltyGroupName, false);
					NKCUtil.SetLabelText(this.m_lbTitle, @string);
					string string2 = NKCStringTable.GetString(lstPenaltys[num].PenaltyGroupDesc, false);
					NKCUtil.SetLabelText(this.m_lbDesc, string2);
					this.m_iPenaltyGroupID = lstPenaltys[num].PenaltyGroupID;
				}
				if (null != this.m_pfbSlot)
				{
					NKCUIFierceBattleSelfPenaltySlot nkcuifierceBattleSelfPenaltySlot = UnityEngine.Object.Instantiate<NKCUIFierceBattleSelfPenaltySlot>(this.m_pfbSlot);
					if (null != nkcuifierceBattleSelfPenaltySlot)
					{
						nkcuifierceBattleSelfPenaltySlot.gameObject.transform.SetParent(this.m_rtParent);
						nkcuifierceBattleSelfPenaltySlot.Init();
						nkcuifierceBattleSelfPenaltySlot.SetData(lstPenaltys[num], new OnClickPenalty(this.OnClickChildSlot));
						this.m_lstChildSlots.Add(nkcuifierceBattleSelfPenaltySlot);
					}
				}
				num++;
			}
			this.m_OnClick = dSlotClick;
		}

		// Token: 0x06008A86 RID: 35462 RVA: 0x002F1D0C File Offset: 0x002EFF0C
		private void OnClickChildSlot(NKMFiercePenaltyTemplet penaltyTempet)
		{
			if (this.m_iPerSelectedPenaltyID == penaltyTempet.Key)
			{
				foreach (NKCUIFierceBattleSelfPenaltySlot nkcuifierceBattleSelfPenaltySlot in this.m_lstChildSlots)
				{
					nkcuifierceBattleSelfPenaltySlot.Select(false);
					nkcuifierceBattleSelfPenaltySlot.Disable(false);
				}
				this.m_iPerSelectedPenaltyID = 0;
			}
			else
			{
				foreach (NKCUIFierceBattleSelfPenaltySlot nkcuifierceBattleSelfPenaltySlot2 in this.m_lstChildSlots)
				{
					if (nkcuifierceBattleSelfPenaltySlot2.TempletData.Key == penaltyTempet.Key)
					{
						nkcuifierceBattleSelfPenaltySlot2.Select(true);
						nkcuifierceBattleSelfPenaltySlot2.Disable(false);
					}
					else
					{
						nkcuifierceBattleSelfPenaltySlot2.Select(false);
						nkcuifierceBattleSelfPenaltySlot2.Disable(true);
					}
				}
				this.m_iPerSelectedPenaltyID = penaltyTempet.Key;
			}
			OnClickPenalty onClick = this.m_OnClick;
			if (onClick == null)
			{
				return;
			}
			onClick(penaltyTempet);
		}

		// Token: 0x06008A87 RID: 35463 RVA: 0x002F1E04 File Offset: 0x002F0004
		public void SelectChildSlot(int PenaltyKey)
		{
			foreach (NKCUIFierceBattleSelfPenaltySlot nkcuifierceBattleSelfPenaltySlot in this.m_lstChildSlots)
			{
				if (nkcuifierceBattleSelfPenaltySlot.TempletData.Key == PenaltyKey)
				{
					nkcuifierceBattleSelfPenaltySlot.Select(true);
					nkcuifierceBattleSelfPenaltySlot.Disable(false);
					this.m_iPerSelectedPenaltyID = PenaltyKey;
				}
				else
				{
					nkcuifierceBattleSelfPenaltySlot.Select(false);
					nkcuifierceBattleSelfPenaltySlot.Disable(true);
				}
			}
		}

		// Token: 0x06008A88 RID: 35464 RVA: 0x002F1E84 File Offset: 0x002F0084
		public void UnCheckChildSlots()
		{
			foreach (NKCUIFierceBattleSelfPenaltySlot nkcuifierceBattleSelfPenaltySlot in this.m_lstChildSlots)
			{
				nkcuifierceBattleSelfPenaltySlot.Select(false);
				nkcuifierceBattleSelfPenaltySlot.Disable(false);
			}
			this.m_iPerSelectedPenaltyID = 0;
		}

		// Token: 0x06008A89 RID: 35465 RVA: 0x002F1EE4 File Offset: 0x002F00E4
		public void Clear()
		{
			for (int i = 0; i < this.m_lstChildSlots.Count; i++)
			{
				UnityEngine.Object.Destroy(this.m_lstChildSlots[i]);
				this.m_lstChildSlots[i] = null;
			}
			this.m_lstChildSlots.Clear();
		}

		// Token: 0x0400774C RID: 30540
		public NKCUIFierceBattleSelfPenaltySlot m_pfbSlot;

		// Token: 0x0400774D RID: 30541
		public NKCComText m_lbTitle;

		// Token: 0x0400774E RID: 30542
		public NKCComText m_lbDesc;

		// Token: 0x0400774F RID: 30543
		public RectTransform m_rtParent;

		// Token: 0x04007750 RID: 30544
		private OnClickPenalty m_OnClick;

		// Token: 0x04007751 RID: 30545
		private List<NKCUIFierceBattleSelfPenaltySlot> m_lstChildSlots = new List<NKCUIFierceBattleSelfPenaltySlot>();

		// Token: 0x04007752 RID: 30546
		private int m_iPerSelectedPenaltyID;

		// Token: 0x04007753 RID: 30547
		private int m_iPenaltyGroupID;
	}
}
