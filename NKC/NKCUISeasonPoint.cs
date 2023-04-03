using System;
using System.Collections.Generic;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000AAA RID: 2730
	public class NKCUISeasonPoint : MonoBehaviour
	{
		// Token: 0x0600796E RID: 31086 RVA: 0x002862D0 File Offset: 0x002844D0
		public void Init(bool bUseFixedDuration)
		{
			this.m_bUseFixedDuration = bUseFixedDuration;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_loop.dOnGetObject += this.GetObject;
			this.m_loop.dOnReturnObject += this.ReturnObject;
			this.m_loop.dOnProvideData += this.ProvideData;
			this.m_loop.PrepareCells(0);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0600796F RID: 31087 RVA: 0x00286350 File Offset: 0x00284550
		private RectTransform GetObject(int idx)
		{
			NKCUISeasonPointSlot nkcuiseasonPointSlot;
			if (this.m_stkSlot.Count > 0)
			{
				nkcuiseasonPointSlot = this.m_stkSlot.Pop();
			}
			else
			{
				nkcuiseasonPointSlot = UnityEngine.Object.Instantiate<NKCUISeasonPointSlot>(this.m_pfbSlot);
			}
			if (nkcuiseasonPointSlot == null)
			{
				return null;
			}
			NKCUtil.SetGameobjectActive(nkcuiseasonPointSlot, true);
			nkcuiseasonPointSlot.transform.SetParent(this.m_trContent);
			return nkcuiseasonPointSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06007970 RID: 31088 RVA: 0x002863B0 File Offset: 0x002845B0
		private void ReturnObject(Transform tr)
		{
			NKCUISeasonPointSlot component = tr.GetComponent<NKCUISeasonPointSlot>();
			if (component == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(component, false);
			component.transform.SetParent(this.m_trObjPool);
			this.m_stkSlot.Push(component);
		}

		// Token: 0x06007971 RID: 31089 RVA: 0x002863F4 File Offset: 0x002845F4
		private void ProvideData(Transform tr, int idx)
		{
			NKCUISeasonPointSlot component = tr.GetComponent<NKCUISeasonPointSlot>();
			if (component == null || this.m_lstSlotData.Count <= idx)
			{
				NKCUtil.SetGameobjectActive(tr, false);
				return;
			}
			float gaugeProgress = 0f;
			bool flag = this.m_lstSlotData.Count == idx + 1;
			if (idx == 0)
			{
				gaugeProgress = (float)this.m_myScore / (float)this.m_lstSlotData[idx + 1].SlotPoint;
				component.SetData(this.m_lstSlotData[idx], this.m_myScore, this.m_receivedPoint, flag, new NKCUISeasonPointSlot.OnClickSlot(this.OnClickSlot));
				component.SetGaugeProgress(gaugeProgress);
				return;
			}
			component.SetData(this.m_lstSlotData[idx], this.m_myScore, this.m_receivedPoint, flag, new NKCUISeasonPointSlot.OnClickSlot(this.OnClickSlot));
			if (!flag)
			{
				if (idx == 0)
				{
					gaugeProgress = (float)this.m_myScore / (float)this.m_lstSlotData[idx].SlotPoint;
				}
				else if (this.m_lstSlotData.Count > idx && this.m_myScore >= this.m_lstSlotData[idx].SlotPoint)
				{
					gaugeProgress = (float)(this.m_myScore - this.m_lstSlotData[idx].SlotPoint) / (float)(this.m_lstSlotData[idx + 1].SlotPoint - this.m_lstSlotData[idx].SlotPoint);
				}
			}
			component.SetGaugeProgress(gaugeProgress);
		}

		// Token: 0x06007972 RID: 31090 RVA: 0x00286554 File Offset: 0x00284754
		public void Open(List<NKCUISeasonPointSlot.SeasonPointSlotData> lstSlotData, string pointName, int myScore, int receivedPoint, NKMIntervalTemplet intervalTemplet, NKCUISeasonPointSlot.OnClickSlot dOnClickSlot)
		{
			if (lstSlotData == null || lstSlotData.Count == 0)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			this.m_myScore = myScore;
			this.m_lstSlotData = lstSlotData;
			this.m_receivedPoint = receivedPoint;
			this.m_dOnClickSlot = dOnClickSlot;
			this.m_endDateUTC = intervalTemplet.GetEndDateUtc();
			NKCUtil.SetLabelText(this.m_lbPointName, pointName);
			NKCUtil.SetLabelText(this.m_lbCurPoint, myScore.ToString("N0"));
			if (!this.m_bUseFixedDuration)
			{
				this.SetRemainTime();
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbSeasonDate, NKCUtilString.GetTimeIntervalString(intervalTemplet.StartDate, intervalTemplet.EndDate, NKMTime.INTERVAL_FROM_UTC, true));
			}
			this.m_loop.TotalCount = this.m_lstSlotData.Count;
			this.m_loop.SetIndexPosition(this.GetCurrentIndex());
		}

		// Token: 0x06007973 RID: 31091 RVA: 0x00286622 File Offset: 0x00284822
		private void SetRemainTime()
		{
			NKCUtil.SetLabelText(this.m_lbSeasonDate, NKCUtilString.GetRemainTimeStringEx(this.m_endDateUTC));
		}

		// Token: 0x06007974 RID: 31092 RVA: 0x0028663C File Offset: 0x0028483C
		private int GetCurrentIndex()
		{
			int num = this.m_lstSlotData.FindIndex(0, (NKCUISeasonPointSlot.SeasonPointSlotData x) => x.SlotPoint > this.m_receivedPoint);
			if (num < 0 && this.m_receivedPoint > 0)
			{
				num = this.m_lstSlotData.Count - 1;
			}
			return num;
		}

		// Token: 0x06007975 RID: 31093 RVA: 0x00286680 File Offset: 0x00284880
		public void Refresh(int myScore, int receivedPoint)
		{
			this.m_myScore = myScore;
			this.m_receivedPoint = receivedPoint;
			NKCUtil.SetLabelText(this.m_lbCurPoint, myScore.ToString("N0"));
			this.m_loop.TotalCount = this.m_lstSlotData.Count;
			this.m_loop.SetIndexPosition(this.GetCurrentIndex());
		}

		// Token: 0x06007976 RID: 31094 RVA: 0x002866D9 File Offset: 0x002848D9
		private void OnClickSlot(NKCUISeasonPointSlot.SeasonPointSlotData seasonSlotData)
		{
			NKCUISeasonPointSlot.OnClickSlot dOnClickSlot = this.m_dOnClickSlot;
			if (dOnClickSlot == null)
			{
				return;
			}
			dOnClickSlot(seasonSlotData);
		}

		// Token: 0x06007977 RID: 31095 RVA: 0x002866EC File Offset: 0x002848EC
		private void Update()
		{
			if (!this.m_bUseFixedDuration)
			{
				this.m_fDeltaTime += Time.deltaTime;
				if (this.m_fDeltaTime > 1f)
				{
					this.m_fDeltaTime -= 1f;
					this.SetRemainTime();
				}
			}
		}

		// Token: 0x0400661E RID: 26142
		public Text m_lbSeasonDate;

		// Token: 0x0400661F RID: 26143
		public Text m_lbPointName;

		// Token: 0x04006620 RID: 26144
		public Text m_lbCurPoint;

		// Token: 0x04006621 RID: 26145
		public Text m_lbDesc;

		// Token: 0x04006622 RID: 26146
		public LoopScrollRect m_loop;

		// Token: 0x04006623 RID: 26147
		public Transform m_trContent;

		// Token: 0x04006624 RID: 26148
		public Transform m_trObjPool;

		// Token: 0x04006625 RID: 26149
		public NKCUISeasonPointSlot m_pfbSlot;

		// Token: 0x04006626 RID: 26150
		private List<NKCUISeasonPointSlot.SeasonPointSlotData> m_lstSlotData = new List<NKCUISeasonPointSlot.SeasonPointSlotData>();

		// Token: 0x04006627 RID: 26151
		private Stack<NKCUISeasonPointSlot> m_stkSlot = new Stack<NKCUISeasonPointSlot>();

		// Token: 0x04006628 RID: 26152
		private int m_myScore;

		// Token: 0x04006629 RID: 26153
		private int m_receivedPoint;

		// Token: 0x0400662A RID: 26154
		private NKCUISeasonPointSlot.OnClickSlot m_dOnClickSlot;

		// Token: 0x0400662B RID: 26155
		private DateTime m_endDateUTC;

		// Token: 0x0400662C RID: 26156
		private bool m_bUseFixedDuration;

		// Token: 0x0400662D RID: 26157
		private float m_fDeltaTime;
	}
}
