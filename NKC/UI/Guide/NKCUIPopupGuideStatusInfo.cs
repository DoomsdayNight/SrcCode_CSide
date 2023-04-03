using System;
using System.Collections.Generic;
using System.Linq;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Guide
{
	// Token: 0x02000C39 RID: 3129
	public class NKCUIPopupGuideStatusInfo : MonoBehaviour, IGuideSubPage
	{
		// Token: 0x06009155 RID: 37205 RVA: 0x00318A00 File Offset: 0x00316C00
		public void SetData()
		{
			if (!this.m_bInit)
			{
				this.Init();
				if (this.m_lstData != null && this.m_lstData.Count > 0)
				{
					this.m_LoopScroll.TotalCount = this.m_lstData.Count;
					this.m_LoopScroll.PrepareCells(0);
					this.m_LoopScroll.SetIndexPosition(0);
					this.m_LoopScroll.RefreshCells(true);
				}
			}
		}

		// Token: 0x06009156 RID: 37206 RVA: 0x00318A6C File Offset: 0x00316C6C
		private void Init()
		{
			this.m_bInit = true;
			if (this.m_LoopScroll != null)
			{
				this.m_LoopScroll.dOnGetObject += this.GetContentObject;
				this.m_LoopScroll.dOnReturnObject += this.ReturnContentObject;
				this.m_LoopScroll.dOnProvideData += this.ProvideContentData;
			}
			switch (this.m_eType)
			{
			case NKCUIPopupGuideStatusInfo.Type.Buff:
				this.m_lstData = (from x in NKMTempletContainer<NKMUnitStatusTemplet>.Values
				where !x.m_bCrowdControl && !x.m_bDebuff
				select x).ToList<NKMUnitStatusTemplet>();
				break;
			case NKCUIPopupGuideStatusInfo.Type.Debuff:
				this.m_lstData = (from x in NKMTempletContainer<NKMUnitStatusTemplet>.Values
				where !x.m_bCrowdControl && x.m_bDebuff
				select x).ToList<NKMUnitStatusTemplet>();
				break;
			case NKCUIPopupGuideStatusInfo.Type.CC:
				this.m_lstData = (from x in NKMTempletContainer<NKMUnitStatusTemplet>.Values
				where x.m_bCrowdControl
				select x).ToList<NKMUnitStatusTemplet>();
				break;
			}
			this.m_lstData.Sort(new Comparison<NKMUnitStatusTemplet>(this.Comparer));
		}

		// Token: 0x06009157 RID: 37207 RVA: 0x00318BA8 File Offset: 0x00316DA8
		private int Comparer(NKMUnitStatusTemplet a, NKMUnitStatusTemplet b)
		{
			return a.m_sortIndex.CompareTo(b.m_sortIndex);
		}

		// Token: 0x06009158 RID: 37208 RVA: 0x00318BBC File Offset: 0x00316DBC
		public void Clear()
		{
			foreach (NKCUIStatInfoSlot nkcuistatInfoSlot in this.m_lstVisibleSlot)
			{
				UnityEngine.Object.Destroy(nkcuistatInfoSlot.gameObject);
			}
			foreach (NKCUIStatInfoSlot nkcuistatInfoSlot2 in this.m_stkSlotPool)
			{
				UnityEngine.Object.Destroy(nkcuistatInfoSlot2.gameObject);
			}
			this.m_lstVisibleSlot.Clear();
			this.m_stkSlotPool.Clear();
		}

		// Token: 0x06009159 RID: 37209 RVA: 0x00318C6C File Offset: 0x00316E6C
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
			NKCUIStatInfoSlot nkcuistatInfoSlot2 = UnityEngine.Object.Instantiate<NKCUIStatInfoSlot>(this.m_pfbSlot);
			nkcuistatInfoSlot2.transform.SetParent(this.m_LoopScroll.content);
			this.m_lstVisibleSlot.Add(nkcuistatInfoSlot2);
			NKCUtil.SetGameobjectActive(nkcuistatInfoSlot2, false);
			return nkcuistatInfoSlot2.GetComponent<RectTransform>();
		}

		// Token: 0x0600915A RID: 37210 RVA: 0x00318D00 File Offset: 0x00316F00
		public void ReturnContentObject(Transform tr)
		{
			NKCUIStatInfoSlot nkcuistatInfoSlot;
			if (tr.TryGetComponent<NKCUIStatInfoSlot>(out nkcuistatInfoSlot))
			{
				NKCUtil.SetGameobjectActive(nkcuistatInfoSlot, false);
				this.m_lstVisibleSlot.Remove(nkcuistatInfoSlot);
				this.m_stkSlotPool.Push(nkcuistatInfoSlot);
				tr.SetParent(base.transform, false);
			}
		}

		// Token: 0x0600915B RID: 37211 RVA: 0x00318D44 File Offset: 0x00316F44
		public void ProvideContentData(Transform tr, int idx)
		{
			NKCUIStatInfoSlot nkcuistatInfoSlot;
			if (!tr.TryGetComponent<NKCUIStatInfoSlot>(out nkcuistatInfoSlot))
			{
				return;
			}
			if (this.m_lstData.Count > idx)
			{
				NKMUnitStatusTemplet nkmunitStatusTemplet = this.m_lstData[idx];
				nkcuistatInfoSlot.SetData(NKCStringTable.GetString(nkmunitStatusTemplet.m_StatusStrID, false), NKCStringTable.GetString(nkmunitStatusTemplet.m_DescStrID, false));
			}
		}

		// Token: 0x04007E82 RID: 32386
		public NKCUIPopupGuideStatusInfo.Type m_eType;

		// Token: 0x04007E83 RID: 32387
		public LoopScrollRect m_LoopScroll;

		// Token: 0x04007E84 RID: 32388
		public NKCUIStatInfoSlot m_pfbSlot;

		// Token: 0x04007E85 RID: 32389
		private bool m_bInit;

		// Token: 0x04007E86 RID: 32390
		private List<NKMUnitStatusTemplet> m_lstData;

		// Token: 0x04007E87 RID: 32391
		private List<NKCUIStatInfoSlot> m_lstVisibleSlot = new List<NKCUIStatInfoSlot>();

		// Token: 0x04007E88 RID: 32392
		private Stack<NKCUIStatInfoSlot> m_stkSlotPool = new Stack<NKCUIStatInfoSlot>();

		// Token: 0x02001A03 RID: 6659
		public enum Type
		{
			// Token: 0x0400AD84 RID: 44420
			Buff,
			// Token: 0x0400AD85 RID: 44421
			Debuff,
			// Token: 0x0400AD86 RID: 44422
			CC
		}
	}
}
