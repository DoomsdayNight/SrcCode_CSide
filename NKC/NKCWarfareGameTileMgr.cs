using System;
using System.Collections.Generic;
using UnityEngine;

namespace NKC
{
	// Token: 0x020007E5 RID: 2021
	public class NKCWarfareGameTileMgr
	{
		// Token: 0x06004FE4 RID: 20452 RVA: 0x001824E1 File Offset: 0x001806E1
		public NKCWarfareGameTileMgr(Transform parent)
		{
			this.m_parent = parent;
		}

		// Token: 0x06004FE5 RID: 20453 RVA: 0x001824FC File Offset: 0x001806FC
		public void Init(NKCWarfareGameTile.onClickPossibleArrivalTile onClickTile)
		{
			if (this.m_tileList.Count > 0)
			{
				return;
			}
			for (int i = 0; i < 70; i++)
			{
				NKCWarfareGameTile newInstance = NKCWarfareGameTile.GetNewInstance(i, this.m_parent, onClickTile);
				this.m_tileList.Add(newInstance);
			}
		}

		// Token: 0x06004FE6 RID: 20454 RVA: 0x00182540 File Offset: 0x00180740
		public void Close()
		{
			for (int i = 0; i < this.m_tileList.Count; i++)
			{
				this.m_tileList[i].Close();
			}
			this.m_tileList.Clear();
		}

		// Token: 0x06004FE7 RID: 20455 RVA: 0x0018257F File Offset: 0x0018077F
		public NKCWarfareGameTile GetTile(int index)
		{
			if (index < 0 || index >= this.m_tileList.Count)
			{
				return null;
			}
			return this.m_tileList[index];
		}

		// Token: 0x06004FE8 RID: 20456 RVA: 0x001825A4 File Offset: 0x001807A4
		public void SetTileActive(int tileCount)
		{
			for (int i = 0; i < this.m_tileList.Count; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_tileList[i], i < tileCount);
			}
		}

		// Token: 0x06004FE9 RID: 20457 RVA: 0x001825DC File Offset: 0x001807DC
		public void SetTileLayer0Type(int index, NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE type)
		{
			NKCWarfareGameTile tile = this.GetTile(index);
			if (tile == null)
			{
				return;
			}
			tile.SetTileLayer0Type(type);
		}

		// Token: 0x06004FEA RID: 20458 RVA: 0x00182604 File Offset: 0x00180804
		public bool IsTileLayer0Type(int index, NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE type)
		{
			NKCWarfareGameTile tile = this.GetTile(index);
			return !(tile == null) && tile.Get_WARFARE_TILE_LAYER_0_TYPE() == type;
		}

		// Token: 0x06004FEB RID: 20459 RVA: 0x00182630 File Offset: 0x00180830
		public bool IsTileLayer2Type(int index, NKCWarfareGameTile.WARFARE_TILE_LAYER_2_TYPE type)
		{
			NKCWarfareGameTile tile = this.GetTile(index);
			return !(tile == null) && tile.Get_WARFARE_TILE_LAYER_2_TYPE() == type;
		}

		// Token: 0x04003FF9 RID: 16377
		private const int DEFAULT_TILE_COUNT = 70;

		// Token: 0x04003FFA RID: 16378
		private Transform m_parent;

		// Token: 0x04003FFB RID: 16379
		private List<NKCWarfareGameTile> m_tileList = new List<NKCWarfareGameTile>();
	}
}
