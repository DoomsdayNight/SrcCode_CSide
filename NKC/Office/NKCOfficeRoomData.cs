using System;
using System.Collections.Generic;
using ClientPacket.Office;
using NKM;
using NKM.Templet.Office;

namespace NKC.Office
{
	// Token: 0x0200083B RID: 2107
	public class NKCOfficeRoomData
	{
		// Token: 0x060053D8 RID: 21464 RVA: 0x00198CF4 File Offset: 0x00196EF4
		public NKCOfficeRoomData(NKMOfficeRoom room, long uid)
		{
			if (room == null)
			{
				this.ID = -1;
				this.Name = "";
				this.m_FloorInteriorID = 0;
				this.m_WallInteriorID = 0;
				this.m_BackgroundID = 0;
				this.m_OwnerUID = 0L;
				return;
			}
			this.ID = room.id;
			this.Name = room.name;
			this.m_dicFuniture = new Dictionary<long, NKCOfficeFunitureData>();
			foreach (NKMOfficeFurniture nkmofficeFurniture in room.furnitures)
			{
				this.m_dicFuniture.Add(nkmofficeFurniture.uid, new NKCOfficeFunitureData(nkmofficeFurniture));
			}
			this.m_lstUnitUID.AddRange(room.unitUids);
			this.m_FloorInteriorID = room.floorInteriorId;
			this.m_WallInteriorID = room.wallInteriorId;
			this.m_BackgroundID = room.backgroundId;
			this.m_OwnerUID = uid;
		}

		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x060053D9 RID: 21465 RVA: 0x00198E08 File Offset: 0x00197008
		public bool IsMyOffice
		{
			get
			{
				return this.m_OwnerUID == 0L;
			}
		}

		// Token: 0x060053DA RID: 21466 RVA: 0x00198E14 File Offset: 0x00197014
		public NKMOfficeRoomTemplet GetTemplet()
		{
			return NKMOfficeRoomTemplet.Find(this.ID);
		}

		// Token: 0x060053DB RID: 21467 RVA: 0x00198E21 File Offset: 0x00197021
		public void AddFuniture(NKCOfficeFunitureData funitureData)
		{
			this.m_dicFuniture.Add(funitureData.uid, funitureData);
		}

		// Token: 0x060053DC RID: 21468 RVA: 0x00198E38 File Offset: 0x00197038
		public NKM_ERROR_CODE CanAddFuniture(NKCOfficeFunitureData funitureData, bool ignoreCount = false)
		{
			if (funitureData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_OFFICE_FAIL;
			}
			if (funitureData.Templet == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_OFFICE_ROOM_INTERIOR_ID_NOT_FOUND;
			}
			if (!this.TypeMatch(funitureData.Templet.Target, funitureData.eTarget))
			{
				return NKM_ERROR_CODE.NEC_FAIL_OFFICE_ROOM_FURNITURE_TYPE_MISMATCH;
			}
			NKMOfficeRoomTemplet nkmofficeRoomTemplet = NKMOfficeRoomTemplet.Find(this.ID);
			if (nkmofficeRoomTemplet == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_OFFICE_ROOM_NOT_FOUND;
			}
			if (NKCScenManager.CurrentUserData().OfficeData.GetFreeInteriorCount(funitureData.itemID) <= 0L)
			{
				return NKM_ERROR_CODE.NEC_FAIL_OFFICE_FURNITURE_NOT_REMAINS;
			}
			ValueTuple<int, int> size = nkmofficeRoomTemplet.GetSize(funitureData.eTarget);
			if (!NKCOfficeManager.FunitureBoundaryCheck(size.Item1, size.Item2, funitureData))
			{
				return NKM_ERROR_CODE.NEC_FAIL_OFFICE_ROOM_FURNITURE_OUT_OF_BOUND;
			}
			if (funitureData.Templet.Target == InteriorTarget.Floor)
			{
				int num = this.GetStuffedFloorCellCount() + funitureData.Templet.CellX * funitureData.Templet.CellY;
				int num2 = size.Item1 * size.Item2;
				if (num > num2 * 9 / 10)
				{
					return NKM_ERROR_CODE.NEC_FAIL_OFFICE_ROOM_FURNITURE_ROOM_FULL;
				}
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = this.CheckFunitureAddOverlap(funitureData);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				return nkm_ERROR_CODE;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x060053DD RID: 21469 RVA: 0x00198F2C File Offset: 0x0019712C
		private int GetStuffedFloorCellCount()
		{
			int num = 0;
			foreach (NKCOfficeFunitureData nkcofficeFunitureData in this.m_dicFuniture.Values)
			{
				if (nkcofficeFunitureData.eTarget == BuildingFloor.Floor)
				{
					num += nkcofficeFunitureData.Templet.CellX * nkcofficeFunitureData.Templet.CellY;
				}
			}
			return num;
		}

		// Token: 0x060053DE RID: 21470 RVA: 0x00198FA4 File Offset: 0x001971A4
		public void MoveFuniture(long uid, BuildingFloor target, int posX, int posY, bool bInvert)
		{
			this.GetFuniture(uid).SetPosition(target, posX, posY, bInvert);
		}

		// Token: 0x060053DF RID: 21471 RVA: 0x00198FB8 File Offset: 0x001971B8
		public NKM_ERROR_CODE CanMoveFuniture(long uid, BuildingFloor target, int posX, int posY, bool bInvert)
		{
			NKCOfficeFunitureData funiture = this.GetFuniture(uid);
			if (funiture == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_OFFICE_ROOM_NOT_FOUND;
			}
			NKCOfficeFunitureData nkcofficeFunitureData = new NKCOfficeFunitureData(funiture);
			nkcofficeFunitureData.SetPosition(target, posX, posY, bInvert);
			nkcofficeFunitureData.bInvert = bInvert;
			if (funiture == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_OFFICE_FAIL;
			}
			if (funiture.Templet == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_OFFICE_ROOM_INTERIOR_ID_NOT_FOUND;
			}
			if (!this.TypeMatch(funiture.Templet.Target, target))
			{
				return NKM_ERROR_CODE.NEC_FAIL_OFFICE_ROOM_FURNITURE_TYPE_MISMATCH;
			}
			NKMOfficeRoomTemplet nkmofficeRoomTemplet = NKMOfficeRoomTemplet.Find(this.ID);
			if (nkmofficeRoomTemplet == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_OFFICE_ROOM_NOT_FOUND;
			}
			ValueTuple<int, int> size = nkmofficeRoomTemplet.GetSize(target);
			if (!NKCOfficeManager.FunitureBoundaryCheck(size.Item1, size.Item2, nkcofficeFunitureData))
			{
				return NKM_ERROR_CODE.NEC_FAIL_OFFICE_ROOM_FURNITURE_OUT_OF_BOUND;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = this.CheckFunitureMoveOverlap(nkcofficeFunitureData);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				return nkm_ERROR_CODE;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x060053E0 RID: 21472 RVA: 0x0019906A File Offset: 0x0019726A
		public void RemoveFuniture(long uid)
		{
			this.m_dicFuniture.Remove(uid);
		}

		// Token: 0x060053E1 RID: 21473 RVA: 0x00199079 File Offset: 0x00197279
		public NKM_ERROR_CODE CanRemoveFurniture(long uid)
		{
			if (!this.m_dicFuniture.ContainsKey(uid))
			{
				return NKM_ERROR_CODE.NEC_FAIL_OFFICE_ROOM_NOT_FOUND;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x060053E2 RID: 21474 RVA: 0x00199090 File Offset: 0x00197290
		private bool TypeMatch(InteriorTarget type, BuildingFloor target)
		{
			if (target == BuildingFloor.Floor)
			{
				return type == InteriorTarget.Floor;
			}
			if (target != BuildingFloor.Tile)
			{
				return target - BuildingFloor.LeftWall <= 1 && type == InteriorTarget.Wall;
			}
			return type == InteriorTarget.Tile;
		}

		// Token: 0x060053E3 RID: 21475 RVA: 0x001990B4 File Offset: 0x001972B4
		public NKCOfficeFunitureData GetFuniture(long uid)
		{
			NKCOfficeFunitureData result;
			if (this.m_dicFuniture.TryGetValue(uid, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060053E4 RID: 21476 RVA: 0x001990D4 File Offset: 0x001972D4
		public NKM_ERROR_CODE CheckFunitureAddOverlap(NKCOfficeFunitureData newFuniture)
		{
			foreach (KeyValuePair<long, NKCOfficeFunitureData> keyValuePair in this.m_dicFuniture)
			{
				if (keyValuePair.Key == newFuniture.uid)
				{
					return NKM_ERROR_CODE.NEC_FAIL_OFFICE_DUPLICATE_FURNITURE_UID;
				}
				NKCOfficeFunitureData value = keyValuePair.Value;
				if (keyValuePair.Value.eTarget == newFuniture.eTarget && NKCOfficeManager.IsFunitureOverlaps(newFuniture, value))
				{
					return NKM_ERROR_CODE.NEC_FAIL_OFFICE_ROOM_FURNITURE_OVERLAP;
				}
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x060053E5 RID: 21477 RVA: 0x00199168 File Offset: 0x00197368
		public NKM_ERROR_CODE CheckFunitureMoveOverlap(NKCOfficeFunitureData funitureMoveData)
		{
			foreach (KeyValuePair<long, NKCOfficeFunitureData> keyValuePair in this.m_dicFuniture)
			{
				if (keyValuePair.Key != funitureMoveData.uid && keyValuePair.Value.eTarget == funitureMoveData.eTarget)
				{
					NKCOfficeFunitureData value = keyValuePair.Value;
					if (NKCOfficeManager.IsFunitureOverlaps(funitureMoveData, value))
					{
						return NKM_ERROR_CODE.NEC_FAIL_OFFICE_ROOM_FURNITURE_OVERLAP;
					}
				}
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x060053E6 RID: 21478 RVA: 0x001991F4 File Offset: 0x001973F4
		public void ClearAllFunitures()
		{
			this.m_dicFuniture.Clear();
		}

		// Token: 0x060053E7 RID: 21479 RVA: 0x00199204 File Offset: 0x00197404
		public NKM_ERROR_CODE SetDecoration(int id, InteriorTarget target)
		{
			NKMOfficeInteriorTemplet nkmofficeInteriorTemplet = NKMItemMiscTemplet.FindInterior(id);
			if (nkmofficeInteriorTemplet.InteriorCategory != InteriorCategory.DECO)
			{
				return NKM_ERROR_CODE.NEC_FAIL_OFFICE_INTERIOR_NOT_DECO;
			}
			if (nkmofficeInteriorTemplet.Target != target)
			{
				return NKM_ERROR_CODE.NEC_FAIL_OFFICE_ROOM_FURNITURE_TYPE_MISMATCH;
			}
			switch (target)
			{
			case InteriorTarget.Floor:
				this.m_FloorInteriorID = id;
				break;
			case InteriorTarget.Tile:
				return NKM_ERROR_CODE.NEC_FAIL_OFFICE_ROOM_FURNITURE_TYPE_MISMATCH;
			case InteriorTarget.Wall:
				this.m_WallInteriorID = id;
				break;
			case InteriorTarget.Background:
				this.m_BackgroundID = id;
				break;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x060053E8 RID: 21480 RVA: 0x00199270 File Offset: 0x00197470
		public NKMOfficePreset MakePresetFromRoom()
		{
			NKMOfficePreset nkmofficePreset = new NKMOfficePreset();
			nkmofficePreset.floorInteriorId = this.m_FloorInteriorID;
			nkmofficePreset.wallInteriorId = this.m_WallInteriorID;
			nkmofficePreset.backgroundId = this.m_BackgroundID;
			nkmofficePreset.furnitures = new List<NKMOfficeFurniture>();
			foreach (NKCOfficeFunitureData nkcofficeFunitureData in this.m_dicFuniture.Values)
			{
				nkmofficePreset.furnitures.Add(nkcofficeFunitureData.ToNKMOfficeFurniture());
			}
			return nkmofficePreset;
		}

		// Token: 0x0400431A RID: 17178
		public int ID;

		// Token: 0x0400431B RID: 17179
		public string Name;

		// Token: 0x0400431C RID: 17180
		public Dictionary<long, NKCOfficeFunitureData> m_dicFuniture = new Dictionary<long, NKCOfficeFunitureData>();

		// Token: 0x0400431D RID: 17181
		public List<long> m_lstUnitUID = new List<long>();

		// Token: 0x0400431E RID: 17182
		public int m_FloorInteriorID;

		// Token: 0x0400431F RID: 17183
		public int m_WallInteriorID;

		// Token: 0x04004320 RID: 17184
		public int m_BackgroundID;

		// Token: 0x04004321 RID: 17185
		public long m_OwnerUID;
	}
}
