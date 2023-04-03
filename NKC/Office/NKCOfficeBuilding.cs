using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Office;
using NKC.Templet.Office;
using NKM;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NKC.Office
{
	// Token: 0x02000828 RID: 2088
	public class NKCOfficeBuilding : NKCOfficeBuildingBase
	{
		// Token: 0x06005250 RID: 21072 RVA: 0x0019016C File Offset: 0x0018E36C
		public override void Init(NKCOfficeFuniture.OnClickFuniture onSelectFuniture)
		{
			base.Init(onSelectFuniture);
			this.dOnSelectFuniture = onSelectFuniture;
			if (this.ApplyPremultifiledAlpha)
			{
				this.PremultiflyAlpha(ref this.m_colEmpty);
				this.PremultiflyAlpha(ref this.m_colSelect);
				this.PremultiflyAlpha(ref this.m_colOccupied);
				this.PremultiflyAlpha(ref this.m_colProhibited);
			}
			this.m_Floor.Init(BuildingFloor.Floor, this.m_colEmpty, this.m_colSelect, this.m_colOccupied, this.m_colProhibited, new NKCOfficeFloorBase.OnDragEvent(this.OnBeginDrag), new NKCOfficeFloorBase.OnDragEvent(this.OnDrag), new NKCOfficeFloorBase.OnDragEvent(this.OnEndDrag));
			this.m_FloorTile.Init(BuildingFloor.Tile, this.m_colEmpty, this.m_colSelect, this.m_colOccupied, this.m_colProhibited, new NKCOfficeFloorBase.OnDragEvent(this.OnBeginDrag), new NKCOfficeFloorBase.OnDragEvent(this.OnDrag), new NKCOfficeFloorBase.OnDragEvent(this.OnEndDrag));
			this.m_LeftWall.Init(BuildingFloor.LeftWall, this.m_colEmpty, this.m_colSelect, this.m_colOccupied, this.m_colProhibited, new NKCOfficeFloorBase.OnDragEvent(this.OnBeginDrag), new NKCOfficeFloorBase.OnDragEvent(this.OnDrag), new NKCOfficeFloorBase.OnDragEvent(this.OnEndDrag));
			this.m_RightWall.Init(BuildingFloor.RightWall, this.m_colEmpty, this.m_colSelect, this.m_colOccupied, this.m_colProhibited, new NKCOfficeFloorBase.OnDragEvent(this.OnBeginDrag), new NKCOfficeFloorBase.OnDragEvent(this.OnDrag), new NKCOfficeFloorBase.OnDragEvent(this.OnEndDrag));
		}

		// Token: 0x06005251 RID: 21073 RVA: 0x001902ED File Offset: 0x0018E4ED
		private void PremultiflyAlpha(ref Color col)
		{
			col.r *= col.a;
			col.g *= col.a;
			col.b *= col.a;
		}

		// Token: 0x06005252 RID: 21074 RVA: 0x00190320 File Offset: 0x0018E520
		public bool SetRoomData(NKCOfficeRoomData roomData, List<NKMUserProfileData> lstFriends)
		{
			Debug.Log("SetRoomData");
			if (roomData == null)
			{
				return false;
			}
			NKMOfficeRoomTemplet templet = roomData.GetTemplet();
			if (templet == null)
			{
				return false;
			}
			this.CleanupFunitures();
			this.m_currentRoomData = roomData;
			base.SetRoomSize(templet.CellX, templet.CellY, templet.CellZ, this.m_fTileSize);
			this.m_LeftWall.bInvertRequired = false;
			this.m_RightWall.bInvertRequired = true;
			this.SetDecoration(roomData.m_FloorInteriorID, InteriorTarget.Floor);
			this.SetDecoration(roomData.m_WallInteriorID, InteriorTarget.Wall);
			this.SetDecoration(roomData.m_BackgroundID, InteriorTarget.Background);
			this.AddFuniture(roomData.m_dicFuniture.Values);
			base.SortFloorObjects();
			if (roomData != null && !roomData.IsMyOffice)
			{
				base.SetSDCharacters(roomData.m_lstUnitUID, roomData.m_OwnerUID);
			}
			else
			{
				base.SetSDCharacters((roomData != null) ? roomData.m_lstUnitUID : null, lstFriends);
			}
			this.PlayInteractionOnEnter();
			return true;
		}

		// Token: 0x06005253 RID: 21075 RVA: 0x00190404 File Offset: 0x0018E604
		public void SetTempFurniture(NKMOfficePreset preset)
		{
			List<NKCOfficeFunitureData> list = new List<NKCOfficeFunitureData>();
			long num = 0L;
			foreach (NKMOfficeFurniture nkmofficeFuniture in preset.furnitures)
			{
				list.Add(new NKCOfficeFunitureData(nkmofficeFuniture)
				{
					uid = num
				});
				num += 1L;
			}
			Dictionary<int, long> dictionary = NKCOfficeManager.MakeRequiredFurnitureHaveCountDic(this.m_currentRoomData.ID, preset);
			this.CleanupFunitures();
			this.CleanupCharacters(false);
			this.SetDecoration(preset.floorInteriorId, InteriorTarget.Floor);
			this.SetDecoration(preset.wallInteriorId, InteriorTarget.Wall);
			this.SetDecoration(preset.backgroundId, InteriorTarget.Background);
			this.AddFuniture(list);
			base.SortFloorObjects();
			foreach (NKCOfficeFuniture nkcofficeFuniture in this.m_dicFuniture.Values)
			{
				int id = nkcofficeFuniture.Templet.Id;
				long num2;
				if (!dictionary.TryGetValue(id, out num2))
				{
					num2 = 0L;
				}
				if (num2 <= 0L)
				{
					nkcofficeFuniture.SetColor(Color.red);
				}
				dictionary[id] = num2 - 1L;
			}
		}

		// Token: 0x06005254 RID: 21076 RVA: 0x00190548 File Offset: 0x0018E748
		public override void UpdateSDCharacters(List<long> lstUnitUID, List<NKMUserProfileData> lstFriends)
		{
			base.UpdateSDCharacters(lstUnitUID, lstFriends);
			this.m_currentRoomData.m_lstUnitUID = lstUnitUID;
		}

		// Token: 0x06005255 RID: 21077 RVA: 0x0019055E File Offset: 0x0018E75E
		protected override void Update()
		{
			base.Update();
		}

		// Token: 0x06005256 RID: 21078 RVA: 0x00190566 File Offset: 0x0018E766
		public override void CleanUp()
		{
			base.CleanUp();
			this.CleanupFunitures();
			this.CleanupFloors();
		}

		// Token: 0x06005257 RID: 21079 RVA: 0x0019057C File Offset: 0x0018E77C
		protected override void CleanupCharacters(bool bCleanupNPC)
		{
			base.CleanupCharacters(bCleanupNPC);
			if (bCleanupNPC)
			{
				foreach (NKCOfficeCharacterNPC nkcofficeCharacterNPC in this.m_lstNPCCharacters)
				{
					nkcofficeCharacterNPC.Cleanup();
					UnityEngine.Object.Destroy(nkcofficeCharacterNPC.gameObject);
				}
				this.m_lstNPCCharacters.Clear();
			}
		}

		// Token: 0x06005258 RID: 21080 RVA: 0x001905EC File Offset: 0x0018E7EC
		private void CleanupFunitures()
		{
			this.ClearSelection();
			this.ClearAllFuniture();
		}

		// Token: 0x06005259 RID: 21081 RVA: 0x001905FA File Offset: 0x0018E7FA
		private void CleanupFloors()
		{
			this.m_Floor.CleanUp();
			this.m_FloorTile.CleanUp();
			this.m_LeftWall.CleanUp();
			this.m_RightWall.CleanUp();
		}

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x0600525A RID: 21082 RVA: 0x00190628 File Offset: 0x0018E828
		// (set) Token: 0x0600525B RID: 21083 RVA: 0x00190630 File Offset: 0x0018E830
		public NKCOfficeFunitureData m_SelectedFunitureData { get; private set; }

		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x0600525C RID: 21084 RVA: 0x00190639 File Offset: 0x0018E839
		public bool HasSelection
		{
			get
			{
				return this.m_SelectedFunitureData != null && this.m_SelectedFunitureData.Templet != null && this.m_funSelection != null;
			}
		}

		// Token: 0x0600525D RID: 21085 RVA: 0x00190660 File Offset: 0x0018E860
		public void ClearSelection()
		{
			this.m_SelectedFunitureData = null;
			this.m_FunitureDataBeforeMove = null;
			foreach (NKCOfficeFloorBase nkcofficeFloorBase in base.OfficeFloors)
			{
				nkcofficeFloorBase.ShowSelectionTile(false);
			}
			if (this.m_funSelection != null)
			{
				this.m_funSelection.SetColor(Color.white);
				this.m_funSelection.CleanUp();
				this.m_funSelection = null;
			}
			this.SetAllFunitureAlpha(false, -1L);
		}

		// Token: 0x0600525E RID: 21086 RVA: 0x001906F4 File Offset: 0x0018E8F4
		private RectTransform GetTargetSelectionRoot(InteriorTarget type)
		{
			if (type <= InteriorTarget.Tile || type != InteriorTarget.Wall)
			{
				return this.m_Floor.m_rtSelectedFunitureRoot;
			}
			return this.m_LeftWall.m_rtSelectedFunitureRoot;
		}

		// Token: 0x0600525F RID: 21087 RVA: 0x00190715 File Offset: 0x0018E915
		private RectTransform GetTargetSelectionRoot(BuildingFloor target)
		{
			NKCOfficeFloorBase floorBase = this.GetFloorBase(target);
			if (floorBase == null)
			{
				return null;
			}
			return floorBase.m_rtSelectedFunitureRoot;
		}

		// Token: 0x06005260 RID: 21088 RVA: 0x00190729 File Offset: 0x0018E929
		private NKCOfficeFloorBase GetFloorBase(BuildingFloor target)
		{
			if (target <= BuildingFloor.Tile)
			{
				if (target != BuildingFloor.Floor)
				{
					if (target == BuildingFloor.Tile)
					{
						return this.m_FloorTile;
					}
				}
			}
			else
			{
				if (target == BuildingFloor.LeftWall)
				{
					return this.m_LeftWall;
				}
				if (target == BuildingFloor.RightWall)
				{
					return this.m_RightWall;
				}
			}
			return this.m_Floor;
		}

		// Token: 0x06005261 RID: 21089 RVA: 0x00190760 File Offset: 0x0018E960
		public void TouchFurniture(long uid)
		{
			NKCOfficeFuniture nkcofficeFuniture;
			if (this.m_dicFuniture.TryGetValue(uid, out nkcofficeFuniture))
			{
				nkcofficeFuniture.OnTouchReact();
				if (nkcofficeFuniture.Templet != null && nkcofficeFuniture.Templet.HasInteraction)
				{
					List<NKCOfficeCharacter> list = new List<NKCOfficeCharacter>();
					foreach (NKCOfficeCharacter nkcofficeCharacter in this.m_dicCharacter.Values)
					{
						if (!nkcofficeCharacter.HasInteractionTarget())
						{
							Vector3 vector = nkcofficeFuniture.transform.localPosition - nkcofficeCharacter.transform.localPosition;
							if (nkcofficeFuniture.Templet.TargetRange * nkcofficeFuniture.Templet.TargetRange >= vector.sqrMagnitude)
							{
								list.Add(nkcofficeCharacter);
							}
						}
					}
					NKCOfficeManager.TryPlayReactionInteraction(nkcofficeFuniture, list);
				}
			}
		}

		// Token: 0x06005262 RID: 21090 RVA: 0x00190840 File Offset: 0x0018EA40
		public NKM_ERROR_CODE MoveFunitureMode(long uid)
		{
			this.m_FunitureDataBeforeMove = this.m_currentRoomData.GetFuniture(uid);
			if (this.m_FunitureDataBeforeMove == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_OFFICE_ROOM_FURNITURE_NOT_FOUND;
			}
			this.m_SelectedFunitureData = new NKCOfficeFunitureData(this.m_FunitureDataBeforeMove);
			this.GetFloorBase(this.m_SelectedFunitureData.eTarget);
			if (!this.m_dicFuniture.TryGetValue(this.m_SelectedFunitureData.uid, out this.m_funSelection))
			{
				this.ClearSelection();
				return NKM_ERROR_CODE.NEC_FAIL_OFFICE_ROOM_FURNITURE_NOT_FOUND;
			}
			this.m_funSelection.SetShowTile(false);
			this.m_funSelection.dOnBeginDragFuniture = null;
			this.m_funSelection.dOnDragFuniture = new NKCOfficeFuniture.OnFunitureDragEvent(this.OnDragSelectedFuniture);
			this.m_funSelection.dOnEndDragFuniture = null;
			if (this.m_funSelection.IsInteractionOngoing)
			{
				this.m_funSelection.InteractingCharacter.UnregisterInteraction();
			}
			this.SetAllFunitureAlpha(true, uid);
			this.ShowSelectionTile(this.m_SelectedFunitureData.Templet.Target);
			this.UpdateSelectedFuniturePos(true);
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06005263 RID: 21091 RVA: 0x00190938 File Offset: 0x0018EB38
		public void CancelMoveFuniture()
		{
			if (this.m_funSelection != null && this.m_FunitureDataBeforeMove != null)
			{
				this.m_funSelection.SetShowTile(false);
				this.m_funSelection.SetColor(Color.white);
				this.m_funSelection.dOnBeginDragFuniture = new NKCOfficeFuniture.OnFunitureDragEvent(this.OnBeginDrag);
				this.m_funSelection.dOnDragFuniture = new NKCOfficeFuniture.OnFunitureDragEvent(this.OnDragUnSelectedFuniture);
				this.m_funSelection.dOnEndDragFuniture = new NKCOfficeFuniture.OnFunitureDragEvent(this.OnEndDrag);
				this.SetFuniturePosition(this.m_funSelection, this.m_FunitureDataBeforeMove);
			}
			this.m_funSelection = null;
			this.ClearSelection();
		}

		// Token: 0x06005264 RID: 21092 RVA: 0x001909E0 File Offset: 0x0018EBE0
		public NKM_ERROR_CODE AddFunitureMode(int funitureID)
		{
			this.ClearSelection();
			NKMOfficeInteriorTemplet nkmofficeInteriorTemplet = NKMItemMiscTemplet.FindInterior(funitureID);
			if (nkmofficeInteriorTemplet == null)
			{
				Debug.LogError(string.Format("Funiture {0} not found!", funitureID));
				return NKM_ERROR_CODE.NEC_FAIL_OFFICE_ROOM_INTERIOR_ID_NOT_FOUND;
			}
			NKMOfficeRoomTemplet nkmofficeRoomTemplet = NKMOfficeRoomTemplet.Find(this.m_currentRoomData.ID);
			BuildingFloor target;
			int posX;
			int posY;
			switch (nkmofficeInteriorTemplet.Target)
			{
			default:
				target = BuildingFloor.Floor;
				posX = nkmofficeRoomTemplet.FloorX / 3;
				posY = nkmofficeRoomTemplet.FloorY / 3;
				break;
			case InteriorTarget.Tile:
				target = BuildingFloor.Tile;
				posX = nkmofficeRoomTemplet.FloorX / 3;
				posY = nkmofficeRoomTemplet.FloorY / 3;
				break;
			case InteriorTarget.Wall:
				target = BuildingFloor.LeftWall;
				posX = nkmofficeRoomTemplet.LeftWallX - nkmofficeInteriorTemplet.CellX;
				posY = 0;
				break;
			}
			this.m_SelectedFunitureData = new NKCOfficeFunitureData(-1L, funitureID, target, posX, posY, false);
			this.m_funSelection = NKCOfficeFuniture.GetInstance(-1L, nkmofficeInteriorTemplet, this.m_fTileSize, false, this.GetTargetSelectionRoot(target), false);
			if (this.m_funSelection == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_OFFICE_FAIL;
			}
			this.m_funSelection.dOnBeginDragFuniture = null;
			this.m_funSelection.dOnDragFuniture = new NKCOfficeFuniture.OnFunitureDragEvent(this.OnDragSelectedFuniture);
			this.m_funSelection.dOnEndDragFuniture = null;
			this.SetAllFunitureAlpha(true, -1L);
			this.ShowSelectionTile(nkmofficeInteriorTemplet.Target);
			this.UpdateSelectedFuniturePos(true);
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06005265 RID: 21093 RVA: 0x00190B18 File Offset: 0x0018ED18
		private void ShowSelectionTile(InteriorTarget type)
		{
			this.m_Floor.ShowSelectionTile(type == InteriorTarget.Floor);
			this.m_FloorTile.ShowSelectionTile(type == InteriorTarget.Tile);
			this.m_LeftWall.ShowSelectionTile(type == InteriorTarget.Wall);
			this.m_RightWall.ShowSelectionTile(type == InteriorTarget.Wall);
			switch (type)
			{
			case InteriorTarget.Floor:
				this.m_Floor.UpdateSelectionTile(null, this.m_currentRoomData);
				return;
			case InteriorTarget.Tile:
				this.m_FloorTile.UpdateSelectionTile(null, this.m_currentRoomData);
				return;
			case InteriorTarget.Wall:
				this.m_LeftWall.UpdateSelectionTile(null, this.m_currentRoomData);
				this.m_RightWall.UpdateSelectionTile(null, this.m_currentRoomData);
				return;
			default:
				return;
			}
		}

		// Token: 0x06005266 RID: 21094 RVA: 0x00190BC4 File Offset: 0x0018EDC4
		private void UpdateSelectedFuniturePos(bool bForceUpdateColor = false)
		{
			if (!this.HasSelection)
			{
				return;
			}
			NKCOfficeFloorBase floorBase = this.GetFloorBase(this.m_SelectedFunitureData.eTarget);
			if (floorBase == null)
			{
				return;
			}
			this.m_funSelection.transform.SetParent(this.GetTargetSelectionRoot(this.m_SelectedFunitureData.eTarget));
			this.m_funSelection.SetInvert(floorBase.GetFunitureInvert(this.m_SelectedFunitureData), false);
			Vector3 worldPos = floorBase.GetWorldPos(this.m_SelectedFunitureData.PosX, this.m_SelectedFunitureData.PosY, this.m_SelectedFunitureData.SizeX, this.m_SelectedFunitureData.SizeY);
			this.m_funSelection.transform.position = worldPos;
			bool flag = false;
			if (this.m_currentRoomData != null)
			{
				ValueTuple<int, int> size = NKMOfficeRoomTemplet.Find(this.m_currentRoomData.ID).GetSize(this.m_SelectedFunitureData.eTarget);
				flag = NKCOfficeManager.FunitureBoundaryCheck(size.Item1, size.Item2, this.m_SelectedFunitureData);
			}
			bool flag2 = floorBase.UpdateSelectionTile(this.m_SelectedFunitureData, this.m_currentRoomData);
			bool flag3 = !flag || !flag2;
			if (bForceUpdateColor || this.m_SelectionImpossible != flag3)
			{
				if (flag3)
				{
					this.m_funSelection.SetColor(Color.red);
				}
				else
				{
					this.m_funSelection.SetGlow(this.m_colSelectFuniture, this.m_fSelectFunitureLoopTime);
				}
				this.m_SelectionImpossible = flag3;
			}
			this.m_funSelection.InvalidateWorldRect();
		}

		// Token: 0x06005267 RID: 21095 RVA: 0x00190D25 File Offset: 0x0018EF25
		private void OnDragUnSelectedFuniture(PointerEventData eventData)
		{
			this.OnDrag(eventData);
		}

		// Token: 0x06005268 RID: 21096 RVA: 0x00190D30 File Offset: 0x0018EF30
		private void OnDragSelectedFuniture(PointerEventData eventData)
		{
			if (!this.HasSelection)
			{
				return;
			}
			BuildingFloor buildingFloor = this.m_SelectedFunitureData.eTarget;
			bool flag = false;
			if (this.m_SelectedFunitureData.Templet.Target == InteriorTarget.Wall)
			{
				if (buildingFloor == BuildingFloor.LeftWall && this.m_RightWall.IsContainsScreenPoint(eventData.position))
				{
					buildingFloor = BuildingFloor.RightWall;
					flag = true;
				}
				else if (buildingFloor == BuildingFloor.RightWall && this.m_LeftWall.IsContainsScreenPoint(eventData.position))
				{
					buildingFloor = BuildingFloor.LeftWall;
					flag = true;
				}
			}
			NKCOfficeFloorBase floorBase = this.GetFloorBase(buildingFloor);
			if (floorBase == null)
			{
				return;
			}
			OfficeFloorPosition cellPosFromScreenPos = floorBase.GetCellPosFromScreenPos(eventData.position, this.m_SelectedFunitureData.SizeX, this.m_SelectedFunitureData.SizeY);
			this.m_SelectedFunitureData.SetPosition(buildingFloor, cellPosFromScreenPos.x, cellPosFromScreenPos.y);
			this.UpdateSelectedFuniturePos(false);
			if (flag)
			{
				if (buildingFloor == BuildingFloor.RightWall)
				{
					this.m_LeftWall.UpdateSelectionTile(this.m_SelectedFunitureData, this.m_currentRoomData);
					return;
				}
				if (buildingFloor == BuildingFloor.LeftWall)
				{
					this.m_RightWall.UpdateSelectionTile(this.m_SelectedFunitureData, this.m_currentRoomData);
				}
			}
		}

		// Token: 0x06005269 RID: 21097 RVA: 0x00190E38 File Offset: 0x0018F038
		public void InvertSelection()
		{
			this.m_SelectedFunitureData.bInvert = !this.m_SelectedFunitureData.bInvert;
			NKMOfficeRoomTemplet nkmofficeRoomTemplet = NKMOfficeRoomTemplet.Find(this.m_currentRoomData.ID);
			ValueTuple<int, int> size = nkmofficeRoomTemplet.GetSize(this.m_SelectedFunitureData.eTarget);
			if (nkmofficeRoomTemplet != null)
			{
				if (this.m_SelectedFunitureData.PosX + this.m_SelectedFunitureData.SizeX > size.Item1)
				{
					this.m_SelectedFunitureData.SetPosition(size.Item1 - this.m_SelectedFunitureData.SizeX, this.m_SelectedFunitureData.PosY);
				}
				if (this.m_SelectedFunitureData.PosY + this.m_SelectedFunitureData.SizeY > size.Item2)
				{
					this.m_SelectedFunitureData.SetPosition(this.m_SelectedFunitureData.PosX, size.Item2 - this.m_SelectedFunitureData.SizeY);
				}
			}
			this.UpdateSelectedFuniturePos(false);
		}

		// Token: 0x0600526A RID: 21098 RVA: 0x00190F1C File Offset: 0x0018F11C
		public void SetAllFunitureAlpha(bool value, long excludeUID = -1L)
		{
			foreach (KeyValuePair<long, NKCOfficeFuniture> keyValuePair in this.m_dicFuniture)
			{
				if (keyValuePair.Key == excludeUID)
				{
					keyValuePair.Value.SetAlpha(1f);
				}
				else
				{
					keyValuePair.Value.SetAlpha(value ? this.m_fSelectFunitureAlpha : 1f);
				}
			}
		}

		// Token: 0x0600526B RID: 21099 RVA: 0x00190FA4 File Offset: 0x0018F1A4
		public void ClearAllFuniture()
		{
			foreach (object obj in Enum.GetValues(typeof(InteriorTarget)))
			{
				InteriorTarget interiorTarget = (InteriorTarget)obj;
				foreach (KeyValuePair<long, NKCOfficeFuniture> keyValuePair in this.m_dicFuniture)
				{
					keyValuePair.Value.CleanUp();
				}
				this.m_dicFuniture.Clear();
			}
		}

		// Token: 0x0600526C RID: 21100 RVA: 0x00191054 File Offset: 0x0018F254
		public void AddFuniture(IEnumerable<NKCOfficeFunitureData> lstFunitures)
		{
			foreach (NKCOfficeFunitureData funitureData in lstFunitures)
			{
				this.AddFuniture(funitureData, false);
			}
			this.UpdateFloorMap();
		}

		// Token: 0x0600526D RID: 21101 RVA: 0x001910A4 File Offset: 0x0018F2A4
		public void AddFuniture(NKCOfficeFunitureData funitureData, bool bUpdateFloorMap = true)
		{
			if (funitureData == null)
			{
				return;
			}
			NKMOfficeInteriorTemplet templet = funitureData.Templet;
			if (templet == null)
			{
				return;
			}
			NKCOfficeFloorBase floorBase = this.GetFloorBase(funitureData.eTarget);
			if (floorBase == null)
			{
				return;
			}
			NKCOfficeFuniture instance = NKCOfficeFuniture.GetInstance(funitureData.uid, templet, this.m_fTileSize, floorBase.GetFunitureInvert(funitureData), floorBase.m_rtFunitureRoot, false);
			if (instance == null)
			{
				return;
			}
			this.m_dicFuniture.Add(funitureData.uid, instance);
			instance.transform.localPosition = floorBase.GetLocalPos(funitureData);
			instance.dOnClickFuniture = this.dOnSelectFuniture;
			instance.dOnBeginDragFuniture = new NKCOfficeFuniture.OnFunitureDragEvent(this.OnBeginDrag);
			instance.dOnDragFuniture = new NKCOfficeFuniture.OnFunitureDragEvent(this.OnDragUnSelectedFuniture);
			instance.dOnEndDragFuniture = new NKCOfficeFuniture.OnFunitureDragEvent(this.OnEndDrag);
			instance.UpdateInteractionPos(this.m_Floor.m_rtFunitureRoot);
			if (bUpdateFloorMap)
			{
				this.UpdateFloorMap();
			}
		}

		// Token: 0x0600526E RID: 21102 RVA: 0x00191184 File Offset: 0x0018F384
		public void MoveFuniture(NKCOfficeFunitureData funitureData)
		{
			if (funitureData == null)
			{
				Debug.LogError("MoveFuniture : data null.");
				return;
			}
			NKCOfficeFuniture funiture;
			if (!this.m_dicFuniture.TryGetValue(funitureData.uid, out funiture))
			{
				Debug.LogError(string.Format("MoveFuniture : funiture uid {0} not found!!!", funitureData.uid));
				return;
			}
			this.SetFuniturePosition(funiture, funitureData);
			this.UpdateFloorMap();
		}

		// Token: 0x0600526F RID: 21103 RVA: 0x001911E0 File Offset: 0x0018F3E0
		public void RemoveFuniture(long uid)
		{
			NKCOfficeFuniture nkcofficeFuniture;
			if (!this.m_dicFuniture.TryGetValue(uid, out nkcofficeFuniture))
			{
				Debug.LogError(string.Format("RemoveFuniture : funiture uid {0} not found!!!", uid));
				return;
			}
			this.m_dicFuniture.Remove(uid);
			nkcofficeFuniture.CleanUp();
			this.UpdateFloorMap();
		}

		// Token: 0x06005270 RID: 21104 RVA: 0x0019122C File Offset: 0x0018F42C
		public void ClearAllFunitures()
		{
			foreach (NKCOfficeFuniture nkcofficeFuniture in this.m_dicFuniture.Values)
			{
				nkcofficeFuniture.CleanUp();
			}
			this.m_dicFuniture.Clear();
			this.UpdateFloorMap();
		}

		// Token: 0x06005271 RID: 21105 RVA: 0x00191294 File Offset: 0x0018F494
		private void SetFuniturePosition(NKCOfficeFuniture funiture, NKCOfficeFunitureData funitureData)
		{
			NKCOfficeFloorBase floorBase = this.GetFloorBase(funitureData.eTarget);
			funiture.transform.SetParent(floorBase.m_rtFunitureRoot);
			funiture.SetInvert(floorBase.GetFunitureInvert(funitureData), false);
			funiture.transform.localPosition = floorBase.GetLocalPos(funitureData);
			funiture.InvalidateWorldRect();
			funiture.UpdateInteractionPos(this.m_Floor.m_rtFunitureRoot);
		}

		// Token: 0x06005272 RID: 21106 RVA: 0x001912F8 File Offset: 0x0018F4F8
		public void SetDecoration(int id, InteriorTarget target)
		{
			NKMOfficeInteriorTemplet nkmofficeInteriorTemplet = NKMItemMiscTemplet.FindInterior(id);
			if (nkmofficeInteriorTemplet != null)
			{
				this.SetDecoration(nkmofficeInteriorTemplet);
				return;
			}
			switch (target)
			{
			case InteriorTarget.Floor:
				this.SetDecoration(NKMCommonConst.Office.DefaultFloorItem);
				return;
			case InteriorTarget.Tile:
				break;
			case InteriorTarget.Wall:
				this.SetDecoration(NKMCommonConst.Office.DefaultWallItem);
				break;
			case InteriorTarget.Background:
				this.SetDecoration(NKMCommonConst.Office.DefaultBackgroundItem);
				return;
			default:
				return;
			}
		}

		// Token: 0x06005273 RID: 21107 RVA: 0x00191360 File Offset: 0x0018F560
		public void SetDecoration(NKMOfficeInteriorTemplet templet)
		{
			if (templet == null)
			{
				return;
			}
			if (templet.InteriorCategory != InteriorCategory.DECO)
			{
				Debug.LogError("tried SetDecoration with non-deco interior");
				return;
			}
			if (templet.Target == InteriorTarget.Tile)
			{
				Debug.LogError("Tile has no decoration!");
				return;
			}
			switch (templet.Target)
			{
			case InteriorTarget.Floor:
				this.m_Floor.SetDecoration(templet);
				return;
			case InteriorTarget.Tile:
				break;
			case InteriorTarget.Wall:
				this.m_LeftWall.SetDecoration(templet);
				this.m_RightWall.SetDecoration(templet);
				return;
			case InteriorTarget.Background:
				base.SetBackground(templet);
				break;
			default:
				return;
			}
		}

		// Token: 0x06005274 RID: 21108 RVA: 0x001913E4 File Offset: 0x0018F5E4
		protected override void UpdateFloorMap()
		{
			this.m_FloorMap = new long[this.m_SizeX, this.m_SizeY];
			for (int i = 0; i < this.m_SizeX; i++)
			{
				for (int j = 0; j < this.m_SizeY; j++)
				{
					this.m_FloorMap[i, j] = 0L;
				}
			}
			foreach (KeyValuePair<long, NKCOfficeFunitureData> keyValuePair in this.m_currentRoomData.m_dicFuniture)
			{
				NKCOfficeFunitureData value = keyValuePair.Value;
				if (value.eTarget == BuildingFloor.Floor)
				{
					for (int k = value.PosX; k < value.PosX + value.SizeX; k++)
					{
						for (int l = value.PosY; l < value.PosY + value.SizeY; l++)
						{
							this.m_FloorMap[k, l] = value.uid;
						}
					}
				}
			}
		}

		// Token: 0x06005275 RID: 21109 RVA: 0x001914F0 File Offset: 0x0018F6F0
		public void AddNPC(string npcAssetName, string spineAssetName, string BTAssetName, Vector3 localPos)
		{
			NKCOfficeCharacterNPC npcinstance;
			if (string.IsNullOrEmpty(npcAssetName))
			{
				npcinstance = NKCOfficeCharacterNPC.GetNPCInstance();
			}
			else
			{
				npcinstance = NKCOfficeCharacterNPC.GetNPCInstance(NKMAssetName.ParseBundleName(npcAssetName, npcAssetName));
			}
			if (npcinstance == null)
			{
				Debug.LogError("AddNPC Failed! " + npcAssetName);
				return;
			}
			npcinstance.SpineAssetName = spineAssetName;
			npcinstance.BTAssetName = BTAssetName;
			npcinstance.Init(this);
			npcinstance.transform.localPosition = localPos;
			npcinstance.StartAI();
			this.m_lstNPCCharacters.Add(npcinstance);
		}

		// Token: 0x06005276 RID: 21110 RVA: 0x00191568 File Offset: 0x0018F768
		public override void OnCharacterBeginDrag(NKCOfficeCharacter character)
		{
			foreach (NKCOfficeFuniture nkcofficeFuniture in this.m_dicFuniture.Values)
			{
				if (NKCOfficeManager.CanPlayInteraction(character, nkcofficeFuniture))
				{
					nkcofficeFuniture.SetHighlight(true);
				}
				else
				{
					nkcofficeFuniture.SetHighlight(false);
				}
			}
		}

		// Token: 0x06005277 RID: 21111 RVA: 0x001915D4 File Offset: 0x0018F7D4
		public override void OnCharacterEndDrag(NKCOfficeCharacter character)
		{
			if (character == null)
			{
				return;
			}
			foreach (NKCOfficeFuniture nkcofficeFuniture in this.m_dicFuniture.Values)
			{
				nkcofficeFuniture.SetHighlight(false);
			}
			NKCOfficeFuniture furnitureFromPosition = this.GetFurnitureFromPosition(character.transform.localPosition);
			if (furnitureFromPosition != null && NKCOfficeManager.CanPlayInteraction(character, furnitureFromPosition))
			{
				NKCOfficeManager.PlayInteraction(character, furnitureFromPosition);
			}
			if (NKCDefineManager.DEFINE_USE_CHEAT() && NKCUtil.IsUsingSuperUserFunction())
			{
				character.ResetUnitInteractionCooltime();
				foreach (NKCOfficeCharacter nkcofficeCharacter in base.GetCharactersInRange(character.transform.position, 100f))
				{
					if (!(nkcofficeCharacter == character))
					{
						nkcofficeCharacter.ResetUnitInteractionCooltime();
						if (NKCOfficeManager.CanPlayInteraction(character, nkcofficeCharacter, false))
						{
							Debug.Log("DEBUG : Force interaction play");
							NKCOfficeManager.PlayInteraction(character, nkcofficeCharacter, false, false);
						}
					}
				}
			}
		}

		// Token: 0x06005278 RID: 21112 RVA: 0x001916E8 File Offset: 0x0018F8E8
		private NKCOfficeFuniture GetFurnitureFromPosition(Vector3 localPos)
		{
			OfficeFloorPosition officeFloorPosition = base.CalculateFloorPosition(localPos, 1, 1, false);
			if (!this.m_Floor.IsInBound(officeFloorPosition))
			{
				return null;
			}
			long key = base.FloorMap[officeFloorPosition.x, officeFloorPosition.y];
			NKCOfficeFuniture result;
			if (this.m_dicFuniture.TryGetValue(key, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06005279 RID: 21113 RVA: 0x0019173C File Offset: 0x0018F93C
		public override NKCOfficeFuniture FindInteractableInterior(NKCOfficeCharacter character)
		{
			if (character == null)
			{
				return null;
			}
			List<NKCOfficeFuniture> list = new List<NKCOfficeFuniture>();
			Vector3 localPosition = character.transform.localPosition;
			foreach (NKCOfficeFuniture nkcofficeFuniture in this.m_dicFuniture.Values)
			{
				if (nkcofficeFuniture.Templet != null && nkcofficeFuniture.Templet.TargetRange > 0f)
				{
					float num = nkcofficeFuniture.Templet.TargetRange * nkcofficeFuniture.Templet.TargetRange;
					if ((nkcofficeFuniture.transform.localPosition - localPosition).sqrMagnitude <= num && NKCOfficeManager.CanPlayInteraction(character, nkcofficeFuniture))
					{
						list.Add(nkcofficeFuniture);
					}
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			return list[UnityEngine.Random.Range(0, list.Count)];
		}

		// Token: 0x0600527A RID: 21114 RVA: 0x00191828 File Offset: 0x0018FA28
		public override NKCOfficeCharacter FindInteractableCharacter(NKCOfficeCharacter character)
		{
			if (character == null)
			{
				return null;
			}
			List<NKCOfficeCharacter> list = new List<NKCOfficeCharacter>();
			foreach (NKCOfficeCharacter nkcofficeCharacter in this.m_dicCharacter.Values)
			{
				if (!(character == nkcofficeCharacter) && NKCOfficeManager.CanPlayInteraction(character, nkcofficeCharacter, false))
				{
					list.Add(nkcofficeCharacter);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			return list[UnityEngine.Random.Range(0, list.Count)];
		}

		// Token: 0x0600527B RID: 21115 RVA: 0x001918C0 File Offset: 0x0018FAC0
		private void PlayInteractionOnEnter()
		{
			foreach (NKCOfficeFuniture nkcofficeFuniture in this.m_dicFuniture.Values)
			{
				if (!nkcofficeFuniture.HasInteractionTarget() && UnityEngine.Random.Range(0, 100) < NKMCommonConst.Office.OfficeInteraction.RoomEnterActRatePercent)
				{
					foreach (NKCOfficeCharacter nkcofficeCharacter in this.m_dicCharacter.Values)
					{
						if (NKCOfficeManager.CanPlayInteraction(nkcofficeCharacter, nkcofficeFuniture) && NKCOfficeManager.PlayInteraction(nkcofficeCharacter, nkcofficeFuniture))
						{
							nkcofficeCharacter.transform.localPosition = nkcofficeCharacter.GetInteractionPosition();
							break;
						}
					}
				}
			}
			foreach (NKCOfficeCharacter nkcofficeCharacter2 in this.m_dicCharacter.Values)
			{
				if (!nkcofficeCharacter2.HasInteractionTarget() && UnityEngine.Random.Range(0, 100) < NKMCommonConst.Office.OfficeInteraction.RoomEnterActRatePercent)
				{
					foreach (NKCOfficeCharacter nkcofficeCharacter3 in this.m_dicCharacter.Values)
					{
						if (NKCOfficeManager.CanPlayInteraction(nkcofficeCharacter2, nkcofficeCharacter3, true) && NKCOfficeManager.PlayInteraction(nkcofficeCharacter2, nkcofficeCharacter3, true, true))
						{
							nkcofficeCharacter2.transform.localPosition = nkcofficeCharacter2.GetInteractionPosition();
							nkcofficeCharacter3.transform.localPosition = nkcofficeCharacter3.GetInteractionPosition();
						}
					}
				}
			}
			foreach (NKCOfficeCharacter nkcofficeCharacter4 in this.m_dicCharacter.Values)
			{
				if (!nkcofficeCharacter4.HasInteractionTarget() && UnityEngine.Random.Range(0, 100) < NKMCommonConst.Office.OfficeInteraction.RoomEnterActRatePercent && nkcofficeCharacter4.SoloInteractionCache != null && nkcofficeCharacter4.SoloInteractionCache.Count != 0)
				{
					NKCOfficeUnitInteractionTemplet soloTemplet = nkcofficeCharacter4.SoloInteractionCache[UnityEngine.Random.Range(0, nkcofficeCharacter4.SoloInteractionCache.Count)];
					nkcofficeCharacter4.RegisterInteraction(soloTemplet);
				}
			}
		}

		// Token: 0x0400425D RID: 16989
		[Header("가구 선택 모드 빈칸")]
		public Color m_colEmpty;

		// Token: 0x0400425E RID: 16990
		[Header("선택된 가구의 칸")]
		public Color m_colSelect;

		// Token: 0x0400425F RID: 16991
		[Header("다른 가구의 칸")]
		public Color m_colOccupied;

		// Token: 0x04004260 RID: 16992
		[Header("겹친 칸")]
		public Color m_colProhibited;

		// Token: 0x04004261 RID: 16993
		[Header("위 컬러들에 PMA 적용 필요한지")]
		public bool ApplyPremultifiledAlpha = true;

		// Token: 0x04004262 RID: 16994
		[Header("선택 가구")]
		public Color m_colSelectFuniture;

		// Token: 0x04004263 RID: 16995
		public float m_fSelectFunitureLoopTime;

		// Token: 0x04004264 RID: 16996
		public float m_fSelectFunitureAlpha = 0.5f;

		// Token: 0x04004265 RID: 16997
		private NKCOfficeRoomData m_currentRoomData;

		// Token: 0x04004266 RID: 16998
		private List<NKCOfficeCharacterNPC> m_lstNPCCharacters = new List<NKCOfficeCharacterNPC>();

		// Token: 0x04004268 RID: 17000
		private bool m_SelectionImpossible;

		// Token: 0x04004269 RID: 17001
		private NKCOfficeFunitureData m_FunitureDataBeforeMove;

		// Token: 0x0400426A RID: 17002
		private NKCOfficeFuniture m_funSelection;

		// Token: 0x0400426B RID: 17003
		private Dictionary<long, NKCOfficeFuniture> m_dicFuniture = new Dictionary<long, NKCOfficeFuniture>();
	}
}
