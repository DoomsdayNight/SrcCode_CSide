using System;
using System.Collections.Generic;
using System.Linq;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Guide
{
	// Token: 0x02000C38 RID: 3128
	public class NKCUIPopupGuideStatInfo : MonoBehaviour, IGuideSubPage
	{
		// Token: 0x0600914E RID: 37198 RVA: 0x00318660 File Offset: 0x00316860
		public void SetData()
		{
			if (!this.m_bInit)
			{
				this.Init();
				if (this.m_lstData != null && this.m_lstData.Count > 0)
				{
					NKCUtil.SetLabelText(this.m_Title, NKCStringTable.GetString(this.m_lstData[0].Stat_Category_Name, false));
					this.m_LoopScroll.TotalCount = this.m_lstData.Count;
					this.m_LoopScroll.PrepareCells(0);
					this.m_LoopScroll.SetIndexPosition(0);
					this.m_LoopScroll.RefreshCells(true);
				}
			}
		}

		// Token: 0x0600914F RID: 37199 RVA: 0x003186F0 File Offset: 0x003168F0
		public void Clear()
		{
			foreach (NKCUIStatInfoSlot nkcuistatInfoSlot in this.m_lstVisibleSlot)
			{
				if (nkcuistatInfoSlot != null)
				{
					nkcuistatInfoSlot.DestoryInstance();
				}
			}
			foreach (NKCUIStatInfoSlot nkcuistatInfoSlot2 in this.m_stkSlotPool)
			{
				if (nkcuistatInfoSlot2 != null)
				{
					nkcuistatInfoSlot2.DestoryInstance();
				}
			}
			this.m_lstVisibleSlot.Clear();
			this.m_stkSlotPool.Clear();
		}

		// Token: 0x06009150 RID: 37200 RVA: 0x003187A4 File Offset: 0x003169A4
		private void Init()
		{
			this.m_bInit = true;
			if (this.m_LoopScroll != null)
			{
				this.m_LoopScroll.dOnGetObject += this.GetContentObject;
				this.m_LoopScroll.dOnReturnObject += this.ReturnContentObject;
				this.m_LoopScroll.dOnProvideData += this.ProvideContentData;
			}
			foreach (List<NKCStatInfoTemplet> list in NKCStatInfoTemplet.Groups.Values)
			{
				if (list.Count > 0 && list[0].Category_Type == this.m_Type)
				{
					this.m_lstData = (from x in list
					where x.m_bShowGuide
					select x).ToList<NKCStatInfoTemplet>();
					break;
				}
			}
		}

		// Token: 0x06009151 RID: 37201 RVA: 0x00318898 File Offset: 0x00316A98
		public RectTransform GetContentObject(int index)
		{
			if (this.m_stkSlotPool.Count > 0)
			{
				NKCUIStatInfoSlot nkcuistatInfoSlot = this.m_stkSlotPool.Pop();
				nkcuistatInfoSlot.transform.SetParent(this.m_LoopScroll.content);
				this.m_lstVisibleSlot.Add(nkcuistatInfoSlot);
				NKCUtil.SetGameobjectActive(nkcuistatInfoSlot, false);
				return nkcuistatInfoSlot.GetComponent<RectTransform>();
			}
			NKCUIStatInfoSlot newInstance = NKCUIStatInfoSlot.GetNewInstance(this.m_LoopScroll.content.transform);
			newInstance.transform.SetParent(this.m_LoopScroll.content);
			this.m_lstVisibleSlot.Add(newInstance);
			NKCUtil.SetGameobjectActive(newInstance, false);
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x06009152 RID: 37202 RVA: 0x00318934 File Offset: 0x00316B34
		public void ReturnContentObject(Transform tr)
		{
			NKCUIStatInfoSlot component = tr.GetComponent<NKCUIStatInfoSlot>();
			if (component != null)
			{
				NKCUtil.SetGameobjectActive(component, false);
				this.m_lstVisibleSlot.Remove(component);
				this.m_stkSlotPool.Push(component);
				tr.SetParent(base.transform, false);
			}
		}

		// Token: 0x06009153 RID: 37203 RVA: 0x00318980 File Offset: 0x00316B80
		public void ProvideContentData(Transform tr, int idx)
		{
			NKCUIStatInfoSlot component = tr.GetComponent<NKCUIStatInfoSlot>();
			if (component == null)
			{
				return;
			}
			if (this.m_lstData.Count > idx)
			{
				component.SetData(NKCStringTable.GetString(this.m_lstData[idx].Stat_Name, false), NKCStringTable.GetString(this.m_lstData[idx].Stat_Desc, false));
			}
		}

		// Token: 0x04007E7B RID: 32379
		public LoopVerticalScrollFlexibleRect m_LoopScroll;

		// Token: 0x04007E7C RID: 32380
		public STAT_TYPE m_Type;

		// Token: 0x04007E7D RID: 32381
		public Text m_Title;

		// Token: 0x04007E7E RID: 32382
		private bool m_bInit;

		// Token: 0x04007E7F RID: 32383
		private List<NKCStatInfoTemplet> m_lstData;

		// Token: 0x04007E80 RID: 32384
		private List<NKCUIStatInfoSlot> m_lstVisibleSlot = new List<NKCUIStatInfoSlot>();

		// Token: 0x04007E81 RID: 32385
		private Stack<NKCUIStatInfoSlot> m_stkSlotPool = new Stack<NKCUIStatInfoSlot>();
	}
}
