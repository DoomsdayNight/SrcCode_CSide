using System;
using System.Collections.Generic;
using NKC.Office;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Component
{
	// Token: 0x02000C62 RID: 3170
	[RequireComponent(typeof(Image))]
	public class NKCOfficeSelectableTile : MonoBehaviour
	{
		// Token: 0x170016FB RID: 5883
		// (get) Token: 0x0600938B RID: 37771 RVA: 0x00325A96 File Offset: 0x00323C96
		private Image m_image
		{
			get
			{
				if (this._image == null)
				{
					this._image = base.GetComponent<Image>();
				}
				return this._image;
			}
		}

		// Token: 0x170016FC RID: 5884
		// (get) Token: 0x0600938C RID: 37772 RVA: 0x00325AB8 File Offset: 0x00323CB8
		private RectTransform m_imageRectTransform
		{
			get
			{
				if (this._imageRectTransform == null)
				{
					this._imageRectTransform = this.m_image.GetComponent<RectTransform>();
				}
				return this._imageRectTransform;
			}
		}

		// Token: 0x0600938D RID: 37773 RVA: 0x00325ADF File Offset: 0x00323CDF
		public void Init(BuildingFloor target, Color colEmpty, Color colSelect, Color colOccupied, Color colProhibited)
		{
			this.m_eTarget = target;
			this.m_colEmpty = colEmpty;
			this.m_colSelect = colSelect;
			this.m_colOccupied = colOccupied;
			this.m_colProhibited = colProhibited;
		}

		// Token: 0x0600938E RID: 37774 RVA: 0x00325B08 File Offset: 0x00323D08
		public void SetSize(int x, int y, float tileSize)
		{
			this.m_sizeX = x;
			this.m_sizeY = y;
			this.m_tileSize = tileSize;
			this.m_texTile = new Texture2D(x, y, TextureFormat.ARGB32, false);
			this.m_texTile.wrapMode = TextureWrapMode.Clamp;
			this.m_texTile.filterMode = FilterMode.Point;
			this.m_texTile.anisoLevel = 0;
			this.m_image.material = new Material(this.m_mat);
			this.m_image.material.SetFloat("_InvRectSizeX", 1f / ((float)x * tileSize));
			this.m_image.material.SetFloat("_InvRectSizeY", 1f / ((float)y * tileSize));
		}

		// Token: 0x0600938F RID: 37775 RVA: 0x00325BB4 File Offset: 0x00323DB4
		public bool UpdateSelectionTile(NKCOfficeFunitureData selectedFunitureData, NKCOfficeRoomData roomData)
		{
			Color[,] tileState;
			bool result = this.MakeSelectionGrid(selectedFunitureData, roomData, out tileState);
			this.UpdateTileStatus(tileState);
			return result;
		}

		// Token: 0x06009390 RID: 37776 RVA: 0x00325BD4 File Offset: 0x00323DD4
		public void UpdateSelectionTileForAI(long[,] tileState)
		{
			Color[,] array = new Color[tileState.GetLength(0), tileState.GetLength(1)];
			for (int i = 0; i < tileState.GetLength(0); i++)
			{
				for (int j = 0; j < tileState.GetLength(1); j++)
				{
					array[i, j] = ((tileState[i, j] == 0L) ? this.m_colEmpty : this.m_colOccupied);
				}
			}
			this.UpdateTileStatus(array);
		}

		// Token: 0x06009391 RID: 37777 RVA: 0x00325C40 File Offset: 0x00323E40
		private bool MakeSelectionGrid(NKCOfficeFunitureData selectedFunitureData, NKCOfficeRoomData roomData, out Color[,] colorGrid)
		{
			bool result = true;
			NKCOfficeSelectableTile.TileState[,] array = new NKCOfficeSelectableTile.TileState[this.m_sizeX, this.m_sizeY];
			for (int i = 0; i < this.m_sizeX; i++)
			{
				for (int j = 0; j < this.m_sizeY; j++)
				{
					array[i, j] = NKCOfficeSelectableTile.TileState.Empty;
				}
			}
			foreach (KeyValuePair<long, NKCOfficeFunitureData> keyValuePair in roomData.m_dicFuniture)
			{
				if (selectedFunitureData == null || selectedFunitureData.uid != keyValuePair.Key)
				{
					NKCOfficeFunitureData value = keyValuePair.Value;
					if (value.eTarget == this.m_eTarget)
					{
						for (int k = value.PosX; k < value.PosX + value.SizeX; k++)
						{
							for (int l = value.PosY; l < value.PosY + value.SizeY; l++)
							{
								array[k, l] = NKCOfficeSelectableTile.TileState.Occupied;
							}
						}
					}
				}
			}
			if (selectedFunitureData != null && selectedFunitureData.eTarget == this.m_eTarget)
			{
				for (int m = selectedFunitureData.PosX; m < selectedFunitureData.PosX + selectedFunitureData.SizeX; m++)
				{
					if (m >= 0 && m < this.m_sizeX)
					{
						for (int n = selectedFunitureData.PosY; n < selectedFunitureData.PosY + selectedFunitureData.SizeY; n++)
						{
							if (n >= 0 && n < this.m_sizeY)
							{
								NKCOfficeSelectableTile.TileState tileState = array[m, n];
								if (tileState <= NKCOfficeSelectableTile.TileState.Selected || tileState - NKCOfficeSelectableTile.TileState.Occupied > 1)
								{
									array[m, n] = NKCOfficeSelectableTile.TileState.Selected;
								}
								else
								{
									array[m, n] = NKCOfficeSelectableTile.TileState.Prohibited;
									result = false;
								}
							}
						}
					}
				}
			}
			colorGrid = new Color[this.m_sizeX, this.m_sizeY];
			for (int num = 0; num < this.m_sizeX; num++)
			{
				for (int num2 = 0; num2 < this.m_sizeY; num2++)
				{
					colorGrid[num, num2] = this.GetColor(array[num, num2]);
				}
			}
			return result;
		}

		// Token: 0x06009392 RID: 37778 RVA: 0x00325E58 File Offset: 0x00324058
		private Color GetColor(NKCOfficeSelectableTile.TileState state)
		{
			switch (state)
			{
			default:
				return this.m_colEmpty;
			case NKCOfficeSelectableTile.TileState.Selected:
				return this.m_colSelect;
			case NKCOfficeSelectableTile.TileState.Occupied:
				return this.m_colOccupied;
			case NKCOfficeSelectableTile.TileState.Prohibited:
				return this.m_colProhibited;
			}
		}

		// Token: 0x06009393 RID: 37779 RVA: 0x00325E8C File Offset: 0x0032408C
		private void UpdateTileStatus(Color[,] tileState)
		{
			if (this.m_texTile == null)
			{
				return;
			}
			for (int i = 0; i < this.m_sizeX; i++)
			{
				for (int j = 0; j < this.m_sizeY; j++)
				{
					this.m_texTile.SetPixel(i, j, tileState[i, j]);
				}
			}
			this.m_texTile.Apply();
			this.m_image.material.SetTexture("_ColorTex", this.m_texTile);
		}

		// Token: 0x06009394 RID: 37780 RVA: 0x00325F05 File Offset: 0x00324105
		private void UpdateLine()
		{
		}

		// Token: 0x04008076 RID: 32886
		public float m_fBorderWidth = 2f;

		// Token: 0x04008077 RID: 32887
		private Color m_colEmpty;

		// Token: 0x04008078 RID: 32888
		private Color m_colSelect;

		// Token: 0x04008079 RID: 32889
		private Color m_colOccupied;

		// Token: 0x0400807A RID: 32890
		private Color m_colProhibited;

		// Token: 0x0400807B RID: 32891
		[Header("Materials")]
		public Material m_mat;

		// Token: 0x0400807C RID: 32892
		private BuildingFloor m_eTarget;

		// Token: 0x0400807D RID: 32893
		private int m_sizeX;

		// Token: 0x0400807E RID: 32894
		private int m_sizeY;

		// Token: 0x0400807F RID: 32895
		private float m_tileSize;

		// Token: 0x04008080 RID: 32896
		private Image _image;

		// Token: 0x04008081 RID: 32897
		private RectTransform _imageRectTransform;

		// Token: 0x04008082 RID: 32898
		private Texture2D m_texTile;

		// Token: 0x02001A1D RID: 6685
		public enum TileState : short
		{
			// Token: 0x0400ADB9 RID: 44473
			Empty,
			// Token: 0x0400ADBA RID: 44474
			Selected,
			// Token: 0x0400ADBB RID: 44475
			Occupied,
			// Token: 0x0400ADBC RID: 44476
			Prohibited
		}
	}
}
