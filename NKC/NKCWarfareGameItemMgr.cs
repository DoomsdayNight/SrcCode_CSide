using System;
using System.Collections.Generic;
using UnityEngine;

namespace NKC
{
	// Token: 0x020007E0 RID: 2016
	public class NKCWarfareGameItemMgr
	{
		// Token: 0x06004FBE RID: 20414 RVA: 0x00181C94 File Offset: 0x0017FE94
		public NKCWarfareGameItemMgr(Transform parent)
		{
			this.m_Parent = parent;
		}

		// Token: 0x06004FBF RID: 20415 RVA: 0x00181CB0 File Offset: 0x0017FEB0
		public void Set(int index, WARFARE_ITEM_STATE state, Vector3 pos, bool bWithEnemy)
		{
			NKCWarfareGameItem nkcwarfareGameItem = this.getItem(index);
			if (nkcwarfareGameItem == null)
			{
				if (state == WARFARE_ITEM_STATE.None)
				{
					return;
				}
				nkcwarfareGameItem = NKCWarfareGameItem.GetNewInstance(this.m_Parent, index);
				this.m_itemList.Add(nkcwarfareGameItem);
			}
			nkcwarfareGameItem.transform.localPosition = pos;
			nkcwarfareGameItem.Set(state, bWithEnemy);
		}

		// Token: 0x06004FC0 RID: 20416 RVA: 0x00181D00 File Offset: 0x0017FF00
		public void SetPos(int index, bool bWithEnemy)
		{
			NKCWarfareGameItem item = this.getItem(index);
			if (item == null)
			{
				return;
			}
			item.SetPos(bWithEnemy);
		}

		// Token: 0x06004FC1 RID: 20417 RVA: 0x00181D28 File Offset: 0x0017FF28
		public bool IsItem(int index)
		{
			NKCWarfareGameItem item = this.getItem(index);
			return !(item == null) && item.State == WARFARE_ITEM_STATE.Item;
		}

		// Token: 0x06004FC2 RID: 20418 RVA: 0x00181D54 File Offset: 0x0017FF54
		private NKCWarfareGameItem getItem(int index)
		{
			for (int i = 0; i < this.m_itemList.Count; i++)
			{
				if (this.m_itemList[i].Index == index)
				{
					return this.m_itemList[i];
				}
			}
			return null;
		}

		// Token: 0x06004FC3 RID: 20419 RVA: 0x00181D9C File Offset: 0x0017FF9C
		public Vector3 GetWorldPos(int index)
		{
			NKCWarfareGameItem item = this.getItem(index);
			if (item == null)
			{
				return Vector3.zero;
			}
			return item.GetWorldPos();
		}

		// Token: 0x06004FC4 RID: 20420 RVA: 0x00181DC8 File Offset: 0x0017FFC8
		public void Close()
		{
			for (int i = 0; i < this.m_itemList.Count; i++)
			{
				this.m_itemList[i].Close();
			}
			this.m_itemList.Clear();
		}

		// Token: 0x06004FC5 RID: 20421 RVA: 0x00181E08 File Offset: 0x00180008
		public void HideAll()
		{
			for (int i = 0; i < this.m_itemList.Count; i++)
			{
				NKCWarfareGameItem nkcwarfareGameItem = this.m_itemList[i];
				this.m_itemList[i].Set(WARFARE_ITEM_STATE.None, false);
			}
		}

		// Token: 0x04003FCF RID: 16335
		private Transform m_Parent;

		// Token: 0x04003FD0 RID: 16336
		private List<NKCWarfareGameItem> m_itemList = new List<NKCWarfareGameItem>();
	}
}
