using System;
using System.Collections.Generic;
using Cs.Core.Util;
using NKC.UI.Component;
using NKM;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.Office
{
	// Token: 0x02000836 RID: 2102
	public abstract class NKCOfficeFloorBase : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
	{
		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x0600537A RID: 21370 RVA: 0x00196FC5 File Offset: 0x001951C5
		public RectTransform Rect
		{
			get
			{
				if (this.m_rect == null)
				{
					this.m_rect = base.GetComponent<RectTransform>();
				}
				return this.m_rect;
			}
		}

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x0600537B RID: 21371 RVA: 0x00196FE7 File Offset: 0x001951E7
		public Image Image
		{
			get
			{
				if (this.m_Image == null)
				{
					this.m_Image = base.GetComponent<Image>();
				}
				return this.m_Image;
			}
		}

		// Token: 0x0600537C RID: 21372 RVA: 0x00197009 File Offset: 0x00195209
		public void Init(BuildingFloor target, Color colEmpty, Color colSelect, Color colOccupied, Color colProhibited, NKCOfficeFloorBase.OnDragEvent onBeginDrag, NKCOfficeFloorBase.OnDragEvent onDrag, NKCOfficeFloorBase.OnDragEvent onEndDrag)
		{
			this.m_eTarget = target;
			NKCOfficeSelectableTile selectableTile = this.m_SelectableTile;
			if (selectableTile != null)
			{
				selectableTile.Init(target, colEmpty, colSelect, colOccupied, colProhibited);
			}
			this.dOnBeginDrag = onBeginDrag;
			this.dOnDrag = onDrag;
			this.dOnEndDrag = onEndDrag;
		}

		// Token: 0x0600537D RID: 21373 RVA: 0x00197044 File Offset: 0x00195244
		public void CleanUp()
		{
			if (this.m_lstDeco != null)
			{
				foreach (NKCOfficeFuniture nkcofficeFuniture in this.m_lstDeco)
				{
					nkcofficeFuniture.CleanUp();
				}
				this.m_lstDeco.Clear();
			}
			this.m_lstDeco = null;
			if (this.m_imgFloor != null)
			{
				this.m_imgFloor.sprite = null;
			}
		}

		// Token: 0x0600537E RID: 21374
		public abstract bool GetFunitureInvert(NKCOfficeFunitureData funitureData);

		// Token: 0x0600537F RID: 21375
		protected abstract bool GetFunitureInvert();

		// Token: 0x06005380 RID: 21376 RVA: 0x001970C8 File Offset: 0x001952C8
		public void SetSize(int x, int y, float tileSize, BuildingFloor target)
		{
			this.m_sizeX = x;
			this.m_sizeY = y;
			this.m_fTileSize = tileSize;
			this.m_eTarget = target;
			this.Rect.SetSize(new Vector2((float)x * tileSize, (float)y * tileSize));
			this.m_SelectableTile.SetSize(x, y, tileSize);
		}

		// Token: 0x06005381 RID: 21377 RVA: 0x00197118 File Offset: 0x00195318
		public Vector3 GetWorldPos(int x, int y)
		{
			Vector3 localPos = this.GetLocalPos(x, y);
			return this.Rect.TransformPoint(localPos);
		}

		// Token: 0x06005382 RID: 21378 RVA: 0x0019713A File Offset: 0x0019533A
		public Vector3 GetWorldPos(OfficeFloorPosition pos)
		{
			return this.GetWorldPos(pos.x, pos.y);
		}

		// Token: 0x06005383 RID: 21379 RVA: 0x00197150 File Offset: 0x00195350
		public Vector3 GetWorldPos(int x, int y, int sizeX, int sizeY)
		{
			Vector3 localPos = this.GetLocalPos(x, y, sizeX, sizeY);
			return this.Rect.TransformPoint(localPos);
		}

		// Token: 0x06005384 RID: 21380 RVA: 0x00197175 File Offset: 0x00195375
		public Vector3 GetLocalPos(ValueTuple<int, int> pos)
		{
			return this.GetLocalPos(pos.Item1, pos.Item2);
		}

		// Token: 0x06005385 RID: 21381 RVA: 0x00197189 File Offset: 0x00195389
		public Vector3 GetLocalPos(OfficeFloorPosition pos)
		{
			return this.GetLocalPos(pos.x, pos.y);
		}

		// Token: 0x06005386 RID: 21382 RVA: 0x001971A0 File Offset: 0x001953A0
		public Vector3 GetLocalPos(int x, int y)
		{
			return new Vector3((float)x * this.m_fTileSize + 0.5f * this.m_fTileSize - this.Rect.pivot.x * this.Rect.GetWidth(), (float)y * this.m_fTileSize + 0.5f * this.m_fTileSize - this.Rect.pivot.y * this.Rect.GetHeight(), 0f);
		}

		// Token: 0x06005387 RID: 21383 RVA: 0x00197220 File Offset: 0x00195420
		public Vector3 GetLocalPos(int x, int y, int sizeX, int sizeY)
		{
			return new Vector3(((float)x + (float)sizeX * 0.5f) * this.m_fTileSize - this.Rect.pivot.x * this.Rect.GetWidth(), ((float)y + (float)sizeY * 0.5f) * this.m_fTileSize - this.Rect.pivot.y * this.Rect.GetHeight(), 0f);
		}

		// Token: 0x06005388 RID: 21384 RVA: 0x00197298 File Offset: 0x00195498
		public OfficeFloorPosition GetCellPosFromScreenPos(Vector2 inputScreenPos, int funitureSizeX, int funitureSizeY)
		{
			Vector2 vector;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.Rect, inputScreenPos, NKCCamera.GetCamera(), out vector);
			OfficeFloorPosition officeFloorPosition;
			officeFloorPosition.x = (int)((vector.x + this.Rect.pivot.x * this.Rect.GetWidth()) / this.m_fTileSize - (float)funitureSizeX * 0.5f);
			officeFloorPosition.y = (int)((vector.y + this.Rect.pivot.y * this.Rect.GetHeight()) / this.m_fTileSize - (float)funitureSizeY * 0.5f);
			officeFloorPosition.x = officeFloorPosition.x.Clamp(0, this.m_sizeX - funitureSizeX);
			officeFloorPosition.y = officeFloorPosition.y.Clamp(0, this.m_sizeY - funitureSizeY);
			return officeFloorPosition;
		}

		// Token: 0x06005389 RID: 21385 RVA: 0x00197368 File Offset: 0x00195568
		public Vector3 GetLocalPosFromScreenPos(Vector2 inputScreenPos)
		{
			Vector2 v;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.Rect, inputScreenPos, NKCCamera.GetCamera(), out v);
			return v;
		}

		// Token: 0x0600538A RID: 21386 RVA: 0x0019738F File Offset: 0x0019558F
		public bool IsContainsScreenPoint(Vector2 inputScreenPos)
		{
			return RectTransformUtility.RectangleContainsScreenPoint(this.Rect, inputScreenPos, NKCCamera.GetCamera());
		}

		// Token: 0x0600538B RID: 21387 RVA: 0x001973A2 File Offset: 0x001955A2
		public Vector3 GetLocalPos(NKCOfficeFunitureData funitureData)
		{
			return this.GetLocalPos(funitureData.PosX, funitureData.PosY, funitureData.SizeX, funitureData.SizeY);
		}

		// Token: 0x0600538C RID: 21388 RVA: 0x001973C2 File Offset: 0x001955C2
		public void ShowSelectionTile(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_SelectableTile, value);
		}

		// Token: 0x0600538D RID: 21389 RVA: 0x001973D0 File Offset: 0x001955D0
		public bool IsInBound(OfficeFloorPosition pos)
		{
			return pos.x >= 0 && pos.y >= 0 && pos.x < this.m_sizeX && pos.y < this.m_sizeY;
		}

		// Token: 0x0600538E RID: 21390 RVA: 0x00197409 File Offset: 0x00195609
		public bool UpdateSelectionTile(NKCOfficeFunitureData selectionData, NKCOfficeRoomData roomData)
		{
			return !(this.m_SelectableTile != null) || this.m_SelectableTile.UpdateSelectionTile(selectionData, roomData);
		}

		// Token: 0x0600538F RID: 21391 RVA: 0x00197428 File Offset: 0x00195628
		public void SetDecoration(NKMOfficeInteriorTemplet templet)
		{
			if (this.m_imgFloor != null)
			{
				this.m_imgFloor.enabled = templet.IsTexture;
			}
			NKCUtil.SetGameobjectActive(this.m_rtDecorationRoot, !templet.IsTexture);
			this.CleanUp();
			if (templet.IsTexture)
			{
				if (this.m_imgFloor == null)
				{
					return;
				}
				NKMAssetName cNKMAssetName = NKMAssetName.ParseBundleName(templet.PrefabName, templet.PrefabName);
				this.m_imgFloor.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>(cNKMAssetName);
				this.m_imgFloor.type = Image.Type.Tiled;
				return;
			}
			else
			{
				if (this.m_rtDecorationRoot == null)
				{
					return;
				}
				if (this.m_lstDeco == null)
				{
					this.m_lstDeco = new List<NKCOfficeFuniture>();
				}
				for (int i = 0; i < this.m_sizeX; i += templet.CellX)
				{
					for (int j = 0; j < this.m_sizeY; j += templet.CellY)
					{
						NKCOfficeFuniture instance = NKCOfficeFuniture.GetInstance(-1L, templet, this.m_fTileSize, this.GetFunitureInvert(), this.m_rtDecorationRoot, false);
						if (instance == null)
						{
							Debug.LogError(string.Format("Decoration {0} not found! id : {1}", templet.PrefabName, templet.m_ItemMiscID));
							return;
						}
						this.m_lstDeco.Add(instance);
						instance.dOnBeginDragFuniture = new NKCOfficeFuniture.OnFunitureDragEvent(this.OnBeginDrag);
						instance.dOnDragFuniture = new NKCOfficeFuniture.OnFunitureDragEvent(this.OnDrag);
						instance.dOnEndDragFuniture = new NKCOfficeFuniture.OnFunitureDragEvent(this.OnEndDrag);
						int num = i + templet.CellX - this.m_sizeX;
						num = Mathf.Max(num, 0);
						int num2 = j + templet.CellY - this.m_sizeY;
						num2 = Mathf.Max(num2, 0);
						if (num > 0 || num2 > 0)
						{
							instance.Resize(templet, num, num2);
							instance.transform.localPosition = this.GetLocalPos(i - num, j - num2, templet.CellX, templet.CellY);
						}
						else
						{
							instance.transform.localPosition = this.GetLocalPos(i, j, templet.CellX, templet.CellY);
						}
					}
				}
				return;
			}
		}

		// Token: 0x06005390 RID: 21392 RVA: 0x0019762B File Offset: 0x0019582B
		public void OnBeginDrag(PointerEventData eventData)
		{
			NKCOfficeFloorBase.OnDragEvent onDragEvent = this.dOnBeginDrag;
			if (onDragEvent == null)
			{
				return;
			}
			onDragEvent(eventData);
		}

		// Token: 0x06005391 RID: 21393 RVA: 0x0019763E File Offset: 0x0019583E
		public void OnDrag(PointerEventData eventData)
		{
			NKCOfficeFloorBase.OnDragEvent onDragEvent = this.dOnDrag;
			if (onDragEvent == null)
			{
				return;
			}
			onDragEvent(eventData);
		}

		// Token: 0x06005392 RID: 21394 RVA: 0x00197651 File Offset: 0x00195851
		public void OnEndDrag(PointerEventData eventData)
		{
			NKCOfficeFloorBase.OnDragEvent onDragEvent = this.dOnEndDrag;
			if (onDragEvent == null)
			{
				return;
			}
			onDragEvent(eventData);
		}

		// Token: 0x040042DB RID: 17115
		public NKCOfficeSelectableTile m_SelectableTile;

		// Token: 0x040042DC RID: 17116
		public Image m_imgFloor;

		// Token: 0x040042DD RID: 17117
		public RectTransform m_rtDecorationRoot;

		// Token: 0x040042DE RID: 17118
		public RectTransform m_rtFunitureRoot;

		// Token: 0x040042DF RID: 17119
		public RectTransform m_rtSelectedFunitureRoot;

		// Token: 0x040042E0 RID: 17120
		protected float m_fTileSize = 100f;

		// Token: 0x040042E1 RID: 17121
		protected int m_sizeX = 1;

		// Token: 0x040042E2 RID: 17122
		protected int m_sizeY = 1;

		// Token: 0x040042E3 RID: 17123
		private BuildingFloor m_eTarget;

		// Token: 0x040042E4 RID: 17124
		protected RectTransform m_rect;

		// Token: 0x040042E5 RID: 17125
		protected Image m_Image;

		// Token: 0x040042E6 RID: 17126
		private List<NKCOfficeFuniture> m_lstDeco;

		// Token: 0x040042E7 RID: 17127
		private NKCOfficeFloorBase.OnDragEvent dOnBeginDrag;

		// Token: 0x040042E8 RID: 17128
		private NKCOfficeFloorBase.OnDragEvent dOnDrag;

		// Token: 0x040042E9 RID: 17129
		private NKCOfficeFloorBase.OnDragEvent dOnEndDrag;

		// Token: 0x020014E0 RID: 5344
		// (Invoke) Token: 0x0600AA1E RID: 43550
		public delegate void OnDragEvent(PointerEventData eventData);
	}
}
