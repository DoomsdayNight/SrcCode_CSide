using System;
using ClientPacket.Office;
using NKM;
using NKM.Templet.Office;

namespace NKC.Office
{
	// Token: 0x0200083A RID: 2106
	public class NKCOfficeFunitureData
	{
		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x060053CC RID: 21452 RVA: 0x00198AC0 File Offset: 0x00196CC0
		public int PosX
		{
			get
			{
				return this.m_posX;
			}
		}

		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x060053CD RID: 21453 RVA: 0x00198AC8 File Offset: 0x00196CC8
		public int PosY
		{
			get
			{
				return this.m_posY;
			}
		}

		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x060053CE RID: 21454 RVA: 0x00198AD0 File Offset: 0x00196CD0
		public NKMOfficeInteriorTemplet Templet
		{
			get
			{
				if (this._interiorTemplet == null)
				{
					this._interiorTemplet = NKMItemMiscTemplet.FindInterior(this.itemID);
				}
				return this._interiorTemplet;
			}
		}

		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x060053CF RID: 21455 RVA: 0x00198AF1 File Offset: 0x00196CF1
		public int SizeX
		{
			get
			{
				if (this.Templet.Target == InteriorTarget.Wall)
				{
					return this.Templet.CellX;
				}
				if (!this.bInvert)
				{
					return this.Templet.CellX;
				}
				return this.Templet.CellY;
			}
		}

		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x060053D0 RID: 21456 RVA: 0x00198B2C File Offset: 0x00196D2C
		public int SizeY
		{
			get
			{
				if (this.Templet.Target == InteriorTarget.Wall)
				{
					return this.Templet.CellY;
				}
				if (!this.bInvert)
				{
					return this.Templet.CellY;
				}
				return this.Templet.CellX;
			}
		}

		// Token: 0x060053D1 RID: 21457 RVA: 0x00198B67 File Offset: 0x00196D67
		public void SetPosition(int x, int y)
		{
			this.m_posX = x;
			this.m_posY = y;
		}

		// Token: 0x060053D2 RID: 21458 RVA: 0x00198B77 File Offset: 0x00196D77
		public void SetPosition(BuildingFloor target, int x, int y, bool bInvert)
		{
			this.eTarget = target;
			this.m_posX = x;
			this.m_posY = y;
			this.bInvert = bInvert;
		}

		// Token: 0x060053D3 RID: 21459 RVA: 0x00198B96 File Offset: 0x00196D96
		public void SetPosition(BuildingFloor target, int x, int y)
		{
			this.eTarget = target;
			this.m_posX = x;
			this.m_posY = y;
		}

		// Token: 0x060053D4 RID: 21460 RVA: 0x00198BB0 File Offset: 0x00196DB0
		public NKCOfficeFunitureData(NKMOfficeFurniture _NKMOfficeFuniture)
		{
			this.uid = _NKMOfficeFuniture.uid;
			this.itemID = _NKMOfficeFuniture.itemId;
			this.m_posX = _NKMOfficeFuniture.positionX;
			this.m_posY = _NKMOfficeFuniture.positionY;
			this.bInvert = _NKMOfficeFuniture.inverted;
			this.eTarget = (BuildingFloor)_NKMOfficeFuniture.planeType;
		}

		// Token: 0x060053D5 RID: 21461 RVA: 0x00198C0C File Offset: 0x00196E0C
		public NKCOfficeFunitureData(long uid, int itemID, BuildingFloor target, int posX, int posY, bool bInvert = false)
		{
			this.uid = uid;
			this.itemID = itemID;
			this.SetPosition(target, posX, posY, bInvert);
		}

		// Token: 0x060053D6 RID: 21462 RVA: 0x00198C30 File Offset: 0x00196E30
		public NKCOfficeFunitureData(NKCOfficeFunitureData source)
		{
			this.uid = source.uid;
			this.itemID = source.itemID;
			this.m_posX = source.m_posX;
			this.m_posY = source.m_posY;
			this.bInvert = source.bInvert;
			this.eTarget = source.eTarget;
			this._interiorTemplet = source._interiorTemplet;
		}

		// Token: 0x060053D7 RID: 21463 RVA: 0x00198C98 File Offset: 0x00196E98
		public NKMOfficeFurniture ToNKMOfficeFurniture()
		{
			return new NKMOfficeFurniture
			{
				uid = this.uid,
				itemId = this.itemID,
				positionX = this.m_posX,
				positionY = this.m_posY,
				inverted = this.bInvert,
				planeType = (OfficePlaneType)this.eTarget
			};
		}

		// Token: 0x04004313 RID: 17171
		public long uid;

		// Token: 0x04004314 RID: 17172
		public int itemID;

		// Token: 0x04004315 RID: 17173
		public BuildingFloor eTarget;

		// Token: 0x04004316 RID: 17174
		private int m_posX;

		// Token: 0x04004317 RID: 17175
		private int m_posY;

		// Token: 0x04004318 RID: 17176
		public bool bInvert;

		// Token: 0x04004319 RID: 17177
		private NKMOfficeInteriorTemplet _interiorTemplet;
	}
}
