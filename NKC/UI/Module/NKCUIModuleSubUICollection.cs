using System;
using System.Collections.Generic;
using ClientPacket.Event;
using NKM;
using NKM.Event;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Module
{
	// Token: 0x02000B23 RID: 2851
	public class NKCUIModuleSubUICollection : NKCUIModuleSubUIBase
	{
		// Token: 0x060081E0 RID: 33248 RVA: 0x002BC3B0 File Offset: 0x002BA5B0
		public override void Init()
		{
		}

		// Token: 0x060081E1 RID: 33249 RVA: 0x002BC3B4 File Offset: 0x002BA5B4
		public override void OnOpen(NKMEventCollectionIndexTemplet templet)
		{
			base.OnOpen(templet);
			if (templet == null)
			{
				return;
			}
			this.m_eventCollectionTemplet = NKMEventCollectionTemplet.Find(templet.EventCollectionGroupId);
			this.m_slotBundleName = templet.EventPrefabId;
			this.m_slotAssetName = templet.EventCollectionSlotPrefabId;
			if (!this.m_bScrollRectInit && this.m_loopScrollFlxRect != null)
			{
				this.m_loopScrollFlxRect.dOnGetObject += this.GetSlot;
				this.m_loopScrollFlxRect.dOnReturnObject += this.ReturnSlot;
				this.m_loopScrollFlxRect.dOnProvideData += this.ProvideData;
				this.m_loopScrollFlxRect.ContentConstraintCount = 1;
				this.m_loopScrollFlxRect.TotalCount = 0;
				this.m_loopScrollFlxRect.PrepareCells(0);
				this.m_bScrollRectInit = true;
			}
			this.Refresh();
		}

		// Token: 0x060081E2 RID: 33250 RVA: 0x002BC484 File Offset: 0x002BA684
		public override void Refresh()
		{
			base.Refresh();
			if (!base.gameObject.activeSelf)
			{
				return;
			}
			if (this.m_eventCollectionTemplet == null)
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKMEventCollectionInfo nkmeventCollectionInfo = (nkmuserData != null) ? nkmuserData.EventCollectionInfo : null;
			int num = 0;
			int num2 = 0;
			Dictionary<int, List<ValueTuple<int, bool>>> dictionary = new Dictionary<int, List<ValueTuple<int, bool>>>();
			foreach (NKMEventCollectionDetailTemplet nkmeventCollectionDetailTemplet in this.m_eventCollectionTemplet.Details)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(nkmeventCollectionDetailTemplet.Key);
				if (unitTempletBase != null)
				{
					if (!dictionary.ContainsKey(nkmeventCollectionDetailTemplet.CollectionGradeGroupId))
					{
						dictionary.Add(nkmeventCollectionDetailTemplet.CollectionGradeGroupId, new List<ValueTuple<int, bool>>());
					}
					bool flag = false;
					if (nkmeventCollectionInfo != null)
					{
						flag = nkmeventCollectionInfo.goodsCollection.Contains(unitTempletBase.m_UnitID);
					}
					dictionary[nkmeventCollectionDetailTemplet.CollectionGradeGroupId].Add(new ValueTuple<int, bool>(unitTempletBase.m_UnitID, flag));
					num++;
					if (flag)
					{
						num2++;
					}
				}
			}
			this.m_unitList.Clear();
			foreach (KeyValuePair<int, List<ValueTuple<int, bool>>> keyValuePair in dictionary)
			{
				this.m_unitList.Add(new ValueTuple<int, List<ValueTuple<int, bool>>>(keyValuePair.Key, keyValuePair.Value));
			}
			this.m_unitList.Sort(delegate(ValueTuple<int, List<ValueTuple<int, bool>>> e1, ValueTuple<int, List<ValueTuple<int, bool>>> e2)
			{
				if (e1.Item1 > e2.Item1)
				{
					return -1;
				}
				if (e1.Item1 < e2.Item1)
				{
					return 1;
				}
				return 0;
			});
			this.m_loopScrollFlxRect.TotalCount = this.m_unitList.Count;
			this.m_loopScrollFlxRect.SetIndexPosition(0);
			float num3 = (float)num2 / (float)num;
			NKCUtil.SetLabelText(this.m_lbAchieveRate, Mathf.FloorToInt(num3 * 100f).ToString());
			NKCUtil.SetLabelText(this.m_lbAchieveCount, string.Format("{0}/{1}", num2, num));
			NKCUtil.SetImageFillAmount(this.m_imgAchieveGauge, num3);
		}

		// Token: 0x060081E3 RID: 33251 RVA: 0x002BC694 File Offset: 0x002BA894
		public override void OnClose()
		{
			base.OnClose();
			this.m_slotBundleName = null;
			this.m_slotAssetName = null;
			this.m_unitList.Clear();
			this.m_eventCollectionTemplet = null;
		}

		// Token: 0x060081E4 RID: 33252 RVA: 0x002BC6BC File Offset: 0x002BA8BC
		private RectTransform GetSlot(int index)
		{
			NKMAssetName nkmassetName = NKMAssetName.ParseBundleName(this.m_slotBundleName, this.m_slotAssetName);
			NKCUIModuleCollectionGroup newInstance = NKCUIModuleCollectionGroup.GetNewInstance(null, nkmassetName.m_BundleName.ToLower(), nkmassetName.m_AssetName);
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x060081E5 RID: 33253 RVA: 0x002BC700 File Offset: 0x002BA900
		private void ReturnSlot(Transform tr)
		{
			NKCUIModuleCollectionGroup component = tr.GetComponent<NKCUIModuleCollectionGroup>();
			tr.SetParent(null);
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x060081E6 RID: 33254 RVA: 0x002BC738 File Offset: 0x002BA938
		private void ProvideData(Transform tr, int index)
		{
			NKCUIModuleCollectionGroup component = tr.GetComponent<NKCUIModuleCollectionGroup>();
			if (component == null)
			{
				return;
			}
			if (index >= this.m_unitList.Count)
			{
				return;
			}
			component.SetData(this.m_unitList[index].Item1, this.m_unitList[index].Item2);
		}

		// Token: 0x04006DF6 RID: 28150
		public LoopScrollFlexibleRect m_loopScrollFlxRect;

		// Token: 0x04006DF7 RID: 28151
		public Text m_lbAchieveRate;

		// Token: 0x04006DF8 RID: 28152
		public Text m_lbAchieveCount;

		// Token: 0x04006DF9 RID: 28153
		public Image m_imgAchieveGauge;

		// Token: 0x04006DFA RID: 28154
		private NKMEventCollectionTemplet m_eventCollectionTemplet;

		// Token: 0x04006DFB RID: 28155
		private List<ValueTuple<int, List<ValueTuple<int, bool>>>> m_unitList = new List<ValueTuple<int, List<ValueTuple<int, bool>>>>();

		// Token: 0x04006DFC RID: 28156
		private string m_slotBundleName;

		// Token: 0x04006DFD RID: 28157
		private string m_slotAssetName;

		// Token: 0x04006DFE RID: 28158
		private bool m_bScrollRectInit;
	}
}
