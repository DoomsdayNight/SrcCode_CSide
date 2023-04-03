using System;
using UnityEngine;

namespace NKC
{
	// Token: 0x020007DE RID: 2014
	public class NKCWarfareGameItem : MonoBehaviour
	{
		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x06004FB4 RID: 20404 RVA: 0x00181AD5 File Offset: 0x0017FCD5
		public int Index
		{
			get
			{
				return this.m_index;
			}
		}

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x06004FB5 RID: 20405 RVA: 0x00181ADD File Offset: 0x0017FCDD
		public WARFARE_ITEM_STATE State
		{
			get
			{
				return this.m_state;
			}
		}

		// Token: 0x06004FB6 RID: 20406 RVA: 0x00181AE8 File Offset: 0x0017FCE8
		public static NKCWarfareGameItem GetNewInstance(Transform parent, int index)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_WARFARE", "NUM_WARFARE_ITEM", false, null);
			NKCWarfareGameItem component = nkcassetInstanceData.m_Instant.GetComponent<NKCWarfareGameItem>();
			if (component == null)
			{
				Debug.LogError("NKCWarfareGameItem Prefab null!");
				return null;
			}
			component.m_instance = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
				component.transform.localScale = Vector3.one;
			}
			component.init(index);
			return component;
		}

		// Token: 0x06004FB7 RID: 20407 RVA: 0x00181B5C File Offset: 0x0017FD5C
		private void init(int index)
		{
			this.m_index = index;
			this.Set(WARFARE_ITEM_STATE.None, false);
		}

		// Token: 0x06004FB8 RID: 20408 RVA: 0x00181B70 File Offset: 0x0017FD70
		public void Set(WARFARE_ITEM_STATE state, bool bWithEnemy)
		{
			this.m_state = state;
			base.gameObject.SetActive(state > WARFARE_ITEM_STATE.None);
			NKCUtil.SetGameobjectActive(this.m_Ready, state == WARFARE_ITEM_STATE.Question);
			NKCUtil.SetGameobjectActive(this.m_Item, state == WARFARE_ITEM_STATE.Item);
			NKCUtil.SetGameobjectActive(this.m_Recv, state == WARFARE_ITEM_STATE.Recv);
			this.SetPos(bWithEnemy);
		}

		// Token: 0x06004FB9 RID: 20409 RVA: 0x00181BC8 File Offset: 0x0017FDC8
		public void SetPos(bool bWithEnemy)
		{
			GameObject obj = this.GetObj(this.m_state);
			if (obj != null)
			{
				obj.transform.localPosition = (bWithEnemy ? this.m_PosWithEnemy : this.m_PosAlone);
			}
		}

		// Token: 0x06004FBA RID: 20410 RVA: 0x00181C07 File Offset: 0x0017FE07
		private GameObject GetObj(WARFARE_ITEM_STATE state)
		{
			switch (state)
			{
			case WARFARE_ITEM_STATE.Question:
				return this.m_Ready;
			case WARFARE_ITEM_STATE.Item:
				return this.m_Item;
			case WARFARE_ITEM_STATE.Recv:
				return this.m_Recv;
			default:
				return null;
			}
		}

		// Token: 0x06004FBB RID: 20411 RVA: 0x00181C35 File Offset: 0x0017FE35
		public void Close()
		{
			NKCAssetResourceManager.CloseInstance(this.m_instance);
			this.m_instance = null;
		}

		// Token: 0x06004FBC RID: 20412 RVA: 0x00181C49 File Offset: 0x0017FE49
		public Vector3 GetWorldPos()
		{
			return this.m_Item.transform.position;
		}

		// Token: 0x04003FC2 RID: 16322
		public GameObject m_Ready;

		// Token: 0x04003FC3 RID: 16323
		public GameObject m_Item;

		// Token: 0x04003FC4 RID: 16324
		public GameObject m_Recv;

		// Token: 0x04003FC5 RID: 16325
		private WARFARE_ITEM_STATE m_state;

		// Token: 0x04003FC6 RID: 16326
		private int m_index = -1;

		// Token: 0x04003FC7 RID: 16327
		private NKCAssetInstanceData m_instance;

		// Token: 0x04003FC8 RID: 16328
		public readonly Vector3 m_PosAlone = new Vector3(-35f, -50f);

		// Token: 0x04003FC9 RID: 16329
		public readonly Vector3 m_PosWithEnemy = new Vector3(0f, 0f);
	}
}
