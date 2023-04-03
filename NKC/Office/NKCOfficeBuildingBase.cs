using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Office;
using NKC.Templet;
using NKC.UI.Component;
using NKM;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NKC.Office
{
	// Token: 0x02000829 RID: 2089
	public abstract class NKCOfficeBuildingBase : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IScrollHandler
	{
		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x0600527D RID: 21117 RVA: 0x00191B60 File Offset: 0x0018FD60
		protected IEnumerable<NKCOfficeFloorBase> OfficeFloors
		{
			get
			{
				if (this.m_Floor != null)
				{
					yield return this.m_Floor;
				}
				if (this.m_FloorTile != null)
				{
					yield return this.m_FloorTile;
				}
				if (this.m_LeftWall != null)
				{
					yield return this.m_LeftWall;
				}
				if (this.m_RightWall != null)
				{
					yield return this.m_RightWall;
				}
				yield break;
			}
		}

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x0600527E RID: 21118 RVA: 0x00191B70 File Offset: 0x0018FD70
		public Transform trActorRoot
		{
			get
			{
				return this.m_Floor.m_rtFunitureRoot;
			}
		}

		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x0600527F RID: 21119 RVA: 0x00191B7D File Offset: 0x0018FD7D
		protected Transform trFloorFunitureRoot
		{
			get
			{
				return this.m_Floor.m_rtFunitureRoot;
			}
		}

		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x06005280 RID: 21120 RVA: 0x00191B8A File Offset: 0x0018FD8A
		public float m_fBGHeight
		{
			get
			{
				return this.m_fBGWidth * 1080f / 1920f;
			}
		}

		// Token: 0x06005281 RID: 21121 RVA: 0x00191B9E File Offset: 0x0018FD9E
		public void SetEnableDrag(bool bSet)
		{
			this.m_bEnableDrag = bSet;
		}

		// Token: 0x06005282 RID: 21122 RVA: 0x00191BA8 File Offset: 0x0018FDA8
		public virtual void Init(NKCOfficeFuniture.OnClickFuniture onSelectFuniture)
		{
			if (this.m_SelectionForAIDebug != null)
			{
				this.m_SelectionForAIDebug.Init(BuildingFloor.Floor, Color.white * 0.4f, Color.white * 0.4f, Color.yellow * 0.4f, Color.red * 0.4f);
				NKCUtil.SetGameobjectActive(this.m_SelectionForAIDebug, false);
			}
		}

		// Token: 0x06005283 RID: 21123 RVA: 0x00191C18 File Offset: 0x0018FE18
		public void SetRoomSize(int x, int y, int wallheight, float tilesize)
		{
			this.m_SizeX = x;
			this.m_SizeY = y;
			this.m_wallHeight = wallheight;
			this.m_fTileSize = tilesize;
			this.m_Floor.SetSize(x, y, tilesize, BuildingFloor.Floor);
			this.m_FloorTile.SetSize(x, y, tilesize, BuildingFloor.Tile);
			this.m_LeftWall.SetSize(x, wallheight, tilesize, BuildingFloor.LeftWall);
			this.m_RightWall.SetSize(y, wallheight, tilesize, BuildingFloor.RightWall);
			if (Application.isPlaying && NKCCamera.GetCamera() != null)
			{
				this.CalculateRoomSize();
			}
		}

		// Token: 0x06005284 RID: 21124 RVA: 0x00191CA0 File Offset: 0x0018FEA0
		protected virtual void Update()
		{
			this.ProcessCameraUpdate();
			this.SortFloorObjects();
			if (NKCDefineManager.DEFINE_UNITY_EDITOR())
			{
				NKCDebugUtil.DebugDrawRect(this.m_rectWorld, Vector2.zero, Vector2.zero, Color.green);
				NKCDebugUtil.DebugDrawRect(this.m_rectCamLimit, Vector2.zero, -this.m_vCameraOverflowRange, Color.yellow);
				NKCDebugUtil.DebugDrawRect(this.m_rectCamMoveRange, Vector2.zero, Vector2.zero, Color.red);
				NKCDebugUtil.DebugDrawRect(this.m_rectCamLimit, Vector2.zero, Vector2.zero, Color.blue);
			}
		}

		// Token: 0x06005285 RID: 21125 RVA: 0x00191D2E File Offset: 0x0018FF2E
		public virtual void CleanUp()
		{
			this.CleanupCharacters(true);
			if (this.m_objBackground != null)
			{
				UnityEngine.Object.Destroy(this.m_objBackground);
				this.m_objBackground = null;
			}
		}

		// Token: 0x06005286 RID: 21126 RVA: 0x00191D58 File Offset: 0x0018FF58
		public long TestAddSDCharacter(string assetName)
		{
			long num = this.sdCount;
			this.sdCount += 1L;
			NKCOfficeCharacter instance = NKCOfficeCharacter.GetInstance(NKMAssetName.ParseBundleName(assetName, assetName));
			instance.Init(this, 0, 0);
			this.m_dicCharacter.Add(num, instance);
			OfficeFloorPosition pos = new OfficeFloorPosition(NKMRandom.Range(0, this.m_SizeX), NKMRandom.Range(0, this.m_SizeY));
			pos = this.FindNearestEmptyCell(pos);
			instance.transform.localPosition = this.m_Floor.GetLocalPos(pos);
			instance.StartAI();
			return num;
		}

		// Token: 0x06005287 RID: 21127 RVA: 0x00191DE3 File Offset: 0x0018FFE3
		public virtual void UpdateSDCharacters(List<long> lstUnitUID, List<NKMUserProfileData> lstFriends)
		{
			this.SetSDCharacters(lstUnitUID, lstFriends);
		}

		// Token: 0x06005288 RID: 21128 RVA: 0x00191DF0 File Offset: 0x0018FFF0
		protected void SetSDCharacters(List<long> lstUnitUID, List<NKMUserProfileData> lstFriends)
		{
			if (lstUnitUID == null)
			{
				lstUnitUID = new List<long>();
			}
			if (lstFriends == null)
			{
				lstFriends = new List<NKMUserProfileData>();
			}
			if (lstUnitUID.Count + lstFriends.Count == 0)
			{
				this.CleanupCharacters(false);
				return;
			}
			List<long> list = new List<long>();
			using (Dictionary<long, NKCOfficeCharacter>.KeyCollection.Enumerator enumerator = this.m_dicCharacter.Keys.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					long uid = enumerator.Current;
					if (!lstUnitUID.Contains(uid) && !lstFriends.Exists((NKMUserProfileData x) => uid == x.commonProfile.userUid))
					{
						list.Add(uid);
					}
				}
			}
			foreach (long unitUID in list)
			{
				this.RemoveSDCharacter(unitUID);
			}
			for (int i = 0; i < lstUnitUID.Count; i++)
			{
				NKMUnitData unitOrTrophyFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitOrTrophyFromUID(lstUnitUID[i]);
				NKCOfficeCharacter nkcofficeCharacter;
				if (this.m_dicCharacter.TryGetValue(lstUnitUID[i], out nkcofficeCharacter))
				{
					nkcofficeCharacter.OnUnitUpdated(unitOrTrophyFromUID);
				}
				else
				{
					this.AddSDCharacter(unitOrTrophyFromUID);
				}
			}
			foreach (NKMUserProfileData nkmuserProfileData in lstFriends)
			{
				NKCOfficeCharacter nkcofficeCharacter2;
				if (!this.m_dicCharacter.TryGetValue(nkmuserProfileData.commonProfile.userUid, out nkcofficeCharacter2))
				{
					NKCOfficeCharacter nkcofficeCharacter3 = this.AddSDCharacter(nkmuserProfileData);
					if (nkcofficeCharacter3 != null)
					{
						nkcofficeCharacter3.m_bCanGrab = false;
						nkcofficeCharacter3.m_bCanTouch = false;
					}
				}
			}
		}

		// Token: 0x06005289 RID: 21129 RVA: 0x00191FBC File Offset: 0x001901BC
		protected void SetSDCharacters(List<long> lstUnitUID, long userUid)
		{
			if (lstUnitUID == null || lstUnitUID.Count == 0)
			{
				this.CleanupCharacters(false);
				return;
			}
			List<long> list = new List<long>();
			foreach (long item in this.m_dicCharacter.Keys)
			{
				if (!lstUnitUID.Contains(item))
				{
					list.Add(item);
				}
			}
			foreach (long unitUID in list)
			{
				this.RemoveSDCharacter(unitUID);
			}
			for (int i = 0; i < lstUnitUID.Count; i++)
			{
				NKMOfficeUnitData friendUnit = NKCScenManager.CurrentUserData().OfficeData.GetFriendUnit(userUid, lstUnitUID[i]);
				if (friendUnit != null && friendUnit.unitId != 0 && !this.m_dicCharacter.ContainsKey(lstUnitUID[i]))
				{
					NKCOfficeCharacter nkcofficeCharacter = this.AddSDCharacter(friendUnit);
					nkcofficeCharacter.m_bCanGrab = false;
					nkcofficeCharacter.m_bCanTouch = false;
				}
			}
		}

		// Token: 0x0600528A RID: 21130 RVA: 0x001920DC File Offset: 0x001902DC
		public NKCOfficeCharacter AddSDCharacter(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				return null;
			}
			NKCOfficeCharacter instance = NKCOfficeCharacter.GetInstance(unitData);
			instance.Init(this, unitData);
			this.m_dicCharacter.Add(unitData.m_UnitUID, instance);
			OfficeFloorPosition pos = new OfficeFloorPosition(NKMRandom.Range(0, this.m_SizeX), NKMRandom.Range(0, this.m_SizeY));
			pos = this.FindNearestEmptyCell(pos);
			instance.transform.localPosition = this.m_Floor.GetLocalPos(pos);
			instance.StartAI();
			return instance;
		}

		// Token: 0x0600528B RID: 21131 RVA: 0x00192154 File Offset: 0x00190354
		public NKCOfficeCharacter AddSDCharacter(NKMOfficeUnitData unitData)
		{
			return this.AddSDCharacter(unitData.unitUid, unitData.unitId, unitData.skinId);
		}

		// Token: 0x0600528C RID: 21132 RVA: 0x00192170 File Offset: 0x00190370
		public NKCOfficeCharacter AddSDCharacter(long unitUID, int unitID, int skinID)
		{
			NKCOfficeCharacter instance = NKCOfficeCharacter.GetInstance(unitID, skinID);
			instance.Init(this, unitID, skinID);
			this.m_dicCharacter.Add(unitUID, instance);
			OfficeFloorPosition pos = new OfficeFloorPosition(NKMRandom.Range(0, this.m_SizeX), NKMRandom.Range(0, this.m_SizeY));
			pos = this.FindNearestEmptyCell(pos);
			instance.transform.localPosition = this.m_Floor.GetLocalPos(pos);
			instance.StartAI();
			return instance;
		}

		// Token: 0x0600528D RID: 21133 RVA: 0x001921E0 File Offset: 0x001903E0
		public NKCOfficeCharacter AddSDCharacter(NKMUserProfileData profileData)
		{
			NKCOfficeCharacter instance = NKCOfficeCharacter.GetInstance(profileData.commonProfile.mainUnitId, profileData.commonProfile.mainUnitSkinId);
			if (instance == null)
			{
				return null;
			}
			instance.Init(this, profileData);
			this.m_dicCharacter.Add(profileData.commonProfile.userUid, instance);
			OfficeFloorPosition pos = new OfficeFloorPosition(NKMRandom.Range(0, this.m_SizeX), NKMRandom.Range(0, this.m_SizeY));
			pos = this.FindNearestEmptyCell(pos);
			instance.transform.localPosition = this.m_Floor.GetLocalPos(pos);
			instance.StartAI();
			return instance;
		}

		// Token: 0x0600528E RID: 21134 RVA: 0x00192278 File Offset: 0x00190478
		private void RemoveSDCharacter(long unitUID)
		{
			NKCOfficeCharacter nkcofficeCharacter;
			if (this.m_dicCharacter.TryGetValue(unitUID, out nkcofficeCharacter))
			{
				nkcofficeCharacter.Cleanup();
				UnityEngine.Object.Destroy(nkcofficeCharacter.gameObject);
				this.m_dicCharacter.Remove(unitUID);
			}
		}

		// Token: 0x0600528F RID: 21135 RVA: 0x001922B4 File Offset: 0x001904B4
		public void OnUnitUpdated(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				return;
			}
			NKCOfficeCharacter nkcofficeCharacter;
			if (this.m_dicCharacter.TryGetValue(unitData.m_UnitUID, out nkcofficeCharacter))
			{
				nkcofficeCharacter.OnUnitUpdated(unitData);
			}
		}

		// Token: 0x06005290 RID: 21136 RVA: 0x001922E4 File Offset: 0x001904E4
		public void OnUnitTakeHeart(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				return;
			}
			NKCOfficeCharacter nkcofficeCharacter;
			if (this.m_dicCharacter.TryGetValue(unitData.m_UnitUID, out nkcofficeCharacter))
			{
				nkcofficeCharacter.OnUnitTakeHeart(unitData);
			}
		}

		// Token: 0x06005291 RID: 21137 RVA: 0x00192314 File Offset: 0x00190514
		public void SetEnableUnitTouch(bool value)
		{
			foreach (NKCOfficeCharacter nkcofficeCharacter in this.m_dicCharacter.Values)
			{
				nkcofficeCharacter.SetEnableTouch(value);
			}
		}

		// Token: 0x06005292 RID: 21138 RVA: 0x0019236C File Offset: 0x0019056C
		public void SetEnableUnitExtraUI(bool value)
		{
			foreach (NKCOfficeCharacter nkcofficeCharacter in this.m_dicCharacter.Values)
			{
				nkcofficeCharacter.SetEnableExtraUI(value);
			}
		}

		// Token: 0x06005293 RID: 21139 RVA: 0x001923C4 File Offset: 0x001905C4
		protected virtual void CleanupCharacters(bool bCleanupNPC)
		{
			foreach (KeyValuePair<long, NKCOfficeCharacter> keyValuePair in this.m_dicCharacter)
			{
				keyValuePair.Value.Cleanup();
				UnityEngine.Object.Destroy(keyValuePair.Value.gameObject);
			}
			this.m_dicCharacter.Clear();
		}

		// Token: 0x06005294 RID: 21140 RVA: 0x00192438 File Offset: 0x00190638
		public virtual void OnBeginDrag(PointerEventData data)
		{
			if (!this.m_bEnableDrag)
			{
				return;
			}
			this.m_vTotalMove = Vector2.zero;
			this.m_bDragging = true;
			this.m_vCamPosBefore.x = NKCCamera.GetPosNowX(false);
			this.m_vCamPosBefore.y = NKCCamera.GetPosNowY(false);
		}

		// Token: 0x06005295 RID: 21141 RVA: 0x00192478 File Offset: 0x00190678
		public virtual void OnDrag(PointerEventData pointData)
		{
			if (!this.m_bEnableDrag)
			{
				return;
			}
			if (!this.m_bDragging)
			{
				return;
			}
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				this.Zoom(pointData.delta.x + pointData.delta.y);
				return;
			}
			if (NKCScenManager.GetScenManager().GetHasPinch())
			{
				this.Zoom(NKCScenManager.GetScenManager().GetPinchDeltaMagnitude() * (float)Screen.height * this.m_fPinchZoomRate);
				return;
			}
			this.MoveCamera(pointData.delta * this.m_fScrollSensibility);
		}

		// Token: 0x06005296 RID: 21142 RVA: 0x00192510 File Offset: 0x00190710
		private void MoveCamera(Vector2 delta)
		{
			float d = NKCCamera.GetCamera().orthographicSize / (float)Screen.height;
			delta *= d;
			this.m_vTotalMove -= delta;
			Vector2 vector = this.m_vCamPosBefore + this.m_vTotalMove;
			Vector2 cameraOverstretch = this.GetCameraOverstretch(vector);
			if (cameraOverstretch.x > 0f)
			{
				vector.x = this.m_rectCamMoveRange.xMax + this.Rubber(cameraOverstretch.x, this.m_vCameraOverflowRange.x);
			}
			else if (cameraOverstretch.x < 0f)
			{
				vector.x = this.m_rectCamMoveRange.xMin + this.Rubber(cameraOverstretch.x, this.m_vCameraOverflowRange.x);
			}
			if (cameraOverstretch.y > 0f)
			{
				vector.y = this.m_rectCamMoveRange.yMax + this.Rubber(cameraOverstretch.y, this.m_vCameraOverflowRange.y);
			}
			else if (cameraOverstretch.y < 0f)
			{
				vector.y = this.m_rectCamMoveRange.yMin + this.Rubber(cameraOverstretch.y, this.m_vCameraOverflowRange.y);
			}
			NKCCamera.SetPos(vector.x, vector.y, -1f, true, true);
		}

		// Token: 0x06005297 RID: 21143 RVA: 0x0019265C File Offset: 0x0019085C
		private float Rubber(float currentValue, float Limit)
		{
			float num = Mathf.Abs(currentValue);
			return Limit * num / (Limit + num) * Mathf.Sign(currentValue);
		}

		// Token: 0x06005298 RID: 21144 RVA: 0x00192680 File Offset: 0x00190880
		private Vector2 GetCameraOverstretch(Vector2 camPos)
		{
			Vector2 zero = Vector2.zero;
			if (camPos.x < this.m_rectCamMoveRange.xMin)
			{
				zero.x = camPos.x - this.m_rectCamMoveRange.xMin;
			}
			else if (camPos.x > this.m_rectCamMoveRange.xMax)
			{
				zero.x = camPos.x - this.m_rectCamMoveRange.xMax;
			}
			if (camPos.y < this.m_rectCamMoveRange.yMin)
			{
				zero.y = camPos.y - this.m_rectCamMoveRange.yMin;
			}
			else if (camPos.y > this.m_rectCamMoveRange.yMax)
			{
				zero.y = camPos.y - this.m_rectCamMoveRange.yMax;
			}
			return zero;
		}

		// Token: 0x06005299 RID: 21145 RVA: 0x00192748 File Offset: 0x00190948
		public virtual void OnEndDrag(PointerEventData data)
		{
			this.m_bDragging = false;
		}

		// Token: 0x0600529A RID: 21146 RVA: 0x00192754 File Offset: 0x00190954
		private void CamReturnStep()
		{
			Vector2 vector;
			vector.x = NKCCamera.GetPosNowX(false);
			vector.y = NKCCamera.GetPosNowY(false);
			if (this.m_rectCamMoveRange.xMax < vector.x)
			{
				vector.x = Mathf.Lerp(vector.x, this.m_rectCamMoveRange.xMax, this.m_fCamReturnRate);
				if (vector.x - this.m_rectCamMoveRange.xMax < 0.001f)
				{
					vector.x = this.m_rectCamMoveRange.xMax;
				}
			}
			else if (this.m_rectCamMoveRange.xMin > vector.x)
			{
				vector.x = Mathf.Lerp(vector.x, this.m_rectCamMoveRange.xMin, this.m_fCamReturnRate);
				if (this.m_rectCamMoveRange.xMin - vector.x < 0.001f)
				{
					vector.x = this.m_rectCamMoveRange.xMin;
				}
			}
			if (this.m_rectCamMoveRange.yMax < vector.y)
			{
				vector.y = Mathf.Lerp(vector.y, this.m_rectCamMoveRange.yMax, this.m_fCamReturnRate);
				if (vector.y - this.m_rectCamMoveRange.yMax < 0.001f)
				{
					vector.y = this.m_rectCamMoveRange.yMax;
				}
			}
			else if (this.m_rectCamMoveRange.yMin > vector.y)
			{
				vector.y = Mathf.Lerp(vector.y, this.m_rectCamMoveRange.yMin, this.m_fCamReturnRate);
				if (this.m_rectCamMoveRange.yMin - vector.y < 0.001f)
				{
					vector.y = this.m_rectCamMoveRange.yMin;
				}
			}
			NKCCamera.SetPos(vector.x, vector.y, -1f, true, true);
		}

		// Token: 0x0600529B RID: 21147 RVA: 0x0019291C File Offset: 0x00190B1C
		protected void ProcessCameraUpdate()
		{
			Vector2 vector = NKCInputManager.GetMoveVector() * this.m_fKeyboardMoveSpeed;
			if (vector != Vector2.zero)
			{
				this.m_vTotalMove = Vector2.zero;
				this.m_vCamPosBefore.x = NKCCamera.GetPosNowX(false);
				this.m_vCamPosBefore.y = NKCCamera.GetPosNowY(false);
				this.MoveCamera(-vector);
			}
			else if (!this.m_bDragging)
			{
				this.CamReturnStep();
			}
			if (NKCInputManager.IsHotkeyPressed(HotkeyEventType.Plus))
			{
				this.Zoom(this.m_fKeyboardMoveSpeed);
				return;
			}
			if (NKCInputManager.IsHotkeyPressed(HotkeyEventType.Minus))
			{
				this.Zoom(-this.m_fKeyboardMoveSpeed);
			}
		}

		// Token: 0x0600529C RID: 21148 RVA: 0x001929BC File Offset: 0x00190BBC
		public virtual void OnScroll(PointerEventData eventData)
		{
			this.Zoom(eventData.scrollDelta.y * this.m_fScrollZoomSensibility);
		}

		// Token: 0x0600529D RID: 21149 RVA: 0x001929D6 File Offset: 0x00190BD6
		public void SetCamera()
		{
			NKCCamera.GetCamera().orthographic = true;
			this.SetDefaultCam();
		}

		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x0600529E RID: 21150 RVA: 0x001929EC File Offset: 0x00190BEC
		private float MaxOrthoSize
		{
			get
			{
				Vector2 vector = new Vector2(this.m_fBGWidth * 0.5f - this.m_vCameraOverflowRange.x - Mathf.Abs(this.m_fCameraOffset), this.m_fBGHeight * 0.5f - this.m_vCameraOverflowRange.y);
				float num = vector.x / vector.y;
				float num2 = (float)Screen.width / (float)Screen.height;
				if (num2 > num)
				{
					return Mathf.Min(vector.x / num2, (float)Screen.height);
				}
				return Mathf.Min(vector.y, (float)Screen.height);
			}
		}

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x0600529F RID: 21151 RVA: 0x00192A81 File Offset: 0x00190C81
		private float MinOrthoSize
		{
			get
			{
				return (float)Screen.height * 0.25f;
			}
		}

		// Token: 0x060052A0 RID: 21152 RVA: 0x00192A90 File Offset: 0x00190C90
		public void Zoom(float zoomDelta)
		{
			NKCCamera.GetCamera().orthographicSize = Mathf.Clamp(NKCCamera.GetCamera().orthographicSize - zoomDelta, this.MinOrthoSize, this.MaxOrthoSize);
			this.CalcuateCamMoveRange();
			Vector2 vector;
			vector.x = NKCCamera.GetPosNowX(false);
			vector.y = NKCCamera.GetPosNowY(false);
			if (vector.x > this.m_rectCamLimit.xMax)
			{
				vector.x = this.m_rectCamLimit.xMax;
			}
			else if (vector.x < this.m_rectCamLimit.xMin)
			{
				vector.x = this.m_rectCamLimit.xMin;
			}
			if (vector.y > this.m_rectCamLimit.yMax)
			{
				vector.y = this.m_rectCamLimit.yMax;
			}
			else if (vector.y < this.m_rectCamLimit.yMin)
			{
				vector.y = this.m_rectCamLimit.yMin;
			}
			NKCCamera.SetPos(vector.x, vector.y, -1f, true, false);
		}

		// Token: 0x060052A1 RID: 21153 RVA: 0x00192B94 File Offset: 0x00190D94
		public void SetCameraOffset(float offset)
		{
			this.m_fCameraOffset = offset;
			this.SetBackgroundSize();
		}

		// Token: 0x060052A2 RID: 21154 RVA: 0x00192BA4 File Offset: 0x00190DA4
		private void SetDefaultCam()
		{
			NKCCamera.GetCamera().orthographicSize = this.MaxOrthoSize;
			this.CalcuateCamMoveRange();
			NKCCamera.SetPos(this.m_rectWorld.center.x - this.m_fCameraOffset, this.m_rectWorld.center.y, -1f, true, false);
		}

		// Token: 0x060052A3 RID: 21155 RVA: 0x00192BFC File Offset: 0x00190DFC
		public void CalculateRoomSize()
		{
			Rect worldRect = this.m_Floor.Rect.GetWorldRect();
			Rect worldRect2 = this.m_LeftWall.Rect.GetWorldRect();
			this.m_rectWorld = Rect.MinMaxRect(worldRect.xMin, worldRect.yMin, worldRect.xMax, worldRect2.yMax);
			this.CalcuateCamMoveRange();
		}

		// Token: 0x060052A4 RID: 21156 RVA: 0x00192C58 File Offset: 0x00190E58
		private void CalcuateCamMoveRange()
		{
			this.m_rectCamLimit = Rect.MinMaxRect(-this.m_fBGWidth * 0.5f - this.m_fCameraOffset, -this.m_fBGHeight * 0.5f, this.m_fBGWidth * 0.5f - this.m_fCameraOffset, this.m_fBGHeight * 0.5f);
			float num = (float)Screen.width * NKCCamera.GetCamera().orthographicSize / (float)Screen.height;
			float orthographicSize = NKCCamera.GetCamera().orthographicSize;
			Vector2 vector = new Vector2(this.m_vCameraOverflowRange.x + num, this.m_vCameraOverflowRange.y + orthographicSize);
			float num2 = -this.m_fBGWidth * 0.5f + vector.x - this.m_fCameraOffset;
			float num3 = -this.m_fBGHeight * 0.5f + vector.y;
			float num4 = this.m_fBGWidth * 0.5f - vector.x - this.m_fCameraOffset;
			float num5 = this.m_fBGHeight * 0.5f - vector.y;
			if (num5 < num3)
			{
				num5 = 0f;
				num3 = 0f;
			}
			if (num4 < num2)
			{
				num4 = -this.m_fCameraOffset;
				num2 = -this.m_fCameraOffset;
			}
			this.m_rectCamMoveRange = Rect.MinMaxRect(num2, num3, num4, num5);
		}

		// Token: 0x060052A5 RID: 21157 RVA: 0x00192D94 File Offset: 0x00190F94
		protected void SetBackground(NKCOfficeRoomData roomData)
		{
			if (roomData == null)
			{
				Debug.LogError("roomData null!");
			}
			NKMOfficeInteriorTemplet background = NKMItemMiscTemplet.FindInterior(roomData.m_BackgroundID);
			this.SetBackground(background);
		}

		// Token: 0x060052A6 RID: 21158 RVA: 0x00192DC4 File Offset: 0x00190FC4
		protected void SetBackground(NKMOfficeInteriorTemplet templet)
		{
			if (templet == null)
			{
				Debug.LogError("templet null!");
				return;
			}
			if (templet.InteriorCategory != InteriorCategory.DECO || templet.Target != InteriorTarget.Background)
			{
				Debug.LogError("Wrong type!");
				return;
			}
			NKMAssetName nkmassetName = NKMAssetName.ParseBundleName(templet.PrefabName, templet.PrefabName);
			this.SetBackground(nkmassetName.m_BundleName, nkmassetName.m_BundleName);
		}

		// Token: 0x060052A7 RID: 21159 RVA: 0x00192E20 File Offset: 0x00191020
		protected void SetBackground(string bundleName, string assetName)
		{
			if (this.m_objBackground != null)
			{
				UnityEngine.Object.Destroy(this.m_objBackground);
				this.m_objBackground = null;
			}
			NKCAssetResourceData nkcassetResourceData = NKCAssetResourceManager.OpenResource<GameObject>(bundleName, assetName, false, null);
			if (nkcassetResourceData != null && nkcassetResourceData.GetAsset<GameObject>() != null)
			{
				this.m_objBackground = UnityEngine.Object.Instantiate<GameObject>(nkcassetResourceData.GetAsset<GameObject>());
				this.m_objBackground.transform.SetParent(this.m_rtBackgroundRoot);
				Transform transform = this.m_objBackground.transform.Find("Stretch/Background");
				if (transform != null)
				{
					this.m_rtBackground = transform.GetComponent<RectTransform>();
					EventTrigger eventTrigger = transform.GetComponent<EventTrigger>();
					if (eventTrigger == null)
					{
						eventTrigger = transform.gameObject.AddComponent<EventTrigger>();
					}
					eventTrigger.triggers.Clear();
					EventTrigger.Entry entry = new EventTrigger.Entry();
					entry.eventID = EventTriggerType.BeginDrag;
					entry.callback.AddListener(delegate(BaseEventData eventData)
					{
						PointerEventData data = eventData as PointerEventData;
						this.OnBeginDrag(data);
					});
					eventTrigger.triggers.Add(entry);
					entry = new EventTrigger.Entry();
					entry.eventID = EventTriggerType.Drag;
					entry.callback.AddListener(delegate(BaseEventData eventData)
					{
						PointerEventData pointData = eventData as PointerEventData;
						this.OnDrag(pointData);
					});
					eventTrigger.triggers.Add(entry);
					entry = new EventTrigger.Entry();
					entry.eventID = EventTriggerType.EndDrag;
					entry.callback.AddListener(delegate(BaseEventData eventData)
					{
						PointerEventData data = eventData as PointerEventData;
						this.OnEndDrag(data);
					});
					eventTrigger.triggers.Add(entry);
					entry = new EventTrigger.Entry();
					entry.eventID = EventTriggerType.Scroll;
					entry.callback.AddListener(delegate(BaseEventData eventData)
					{
						PointerEventData eventData2 = eventData as PointerEventData;
						this.OnScroll(eventData2);
					});
					eventTrigger.triggers.Add(entry);
				}
			}
			if (nkcassetResourceData != null)
			{
				NKCAssetResourceManager.CloseResource(nkcassetResourceData);
			}
			this.SetBackgroundSize();
		}

		// Token: 0x060052A8 RID: 21160 RVA: 0x00192FBC File Offset: 0x001911BC
		protected void SetBackgroundSize()
		{
			if (this.m_rtBackground != null)
			{
				this.m_rtBackground.localScale = Vector3.one;
				this.m_rtBackground.SetSize(new Vector2(this.m_fBGWidth, this.m_fBGHeight));
			}
			if (this.m_objBackground != null)
			{
				this.m_objBackground.transform.localPosition = new Vector3(-this.m_fCameraOffset, 0f, 0f);
				this.m_objBackground.transform.localScale = Vector3.one;
			}
		}

		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x060052A9 RID: 21161 RVA: 0x0019304C File Offset: 0x0019124C
		public long[,] FloorMap
		{
			get
			{
				return this.m_FloorMap;
			}
		}

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x060052AA RID: 21162 RVA: 0x00193054 File Offset: 0x00191254
		public float TileSize
		{
			get
			{
				return this.m_fTileSize;
			}
		}

		// Token: 0x060052AB RID: 21163
		protected abstract void UpdateFloorMap();

		// Token: 0x060052AC RID: 21164 RVA: 0x0019305C File Offset: 0x0019125C
		protected NKCOfficeBuildingBase.FloorRect CalculateFloorRect(RectTransform rect)
		{
			float width = rect.GetWidth();
			float height = rect.GetHeight();
			NKCOfficeBuildingBase.FloorRect floorRect = default(NKCOfficeBuildingBase.FloorRect);
			floorRect.sizeX = Mathf.RoundToInt(width / this.m_fTileSize);
			floorRect.sizeY = Mathf.RoundToInt(height / this.m_fTileSize);
			OfficeFloorPosition officeFloorPosition = this.CalculateFloorPosition(rect.localPosition, floorRect.sizeX, floorRect.sizeY, false);
			floorRect.x = officeFloorPosition.x;
			floorRect.y = officeFloorPosition.y;
			return floorRect;
		}

		// Token: 0x060052AD RID: 21165 RVA: 0x001930DC File Offset: 0x001912DC
		public OfficeFloorPosition CalculateFloorPosition(Vector3 localPos, bool bClamp)
		{
			return this.CalculateFloorPosition(localPos, 1, 1, bClamp);
		}

		// Token: 0x060052AE RID: 21166 RVA: 0x001930E8 File Offset: 0x001912E8
		public OfficeFloorPosition CalculateFloorPosition(Vector3 localPos, int sizeX = 1, int sizeY = 1, bool bClamp = false)
		{
			OfficeFloorPosition officeFloorPosition = default(OfficeFloorPosition);
			officeFloorPosition.x = Mathf.RoundToInt(localPos.x / this.m_fTileSize + this.m_Floor.m_rtFunitureRoot.pivot.x * (float)this.m_SizeX - (float)sizeX * 0.5f);
			officeFloorPosition.y = Mathf.RoundToInt(localPos.y / this.m_fTileSize + this.m_Floor.m_rtFunitureRoot.pivot.y * (float)this.m_SizeY - (float)sizeY * 0.5f);
			if (bClamp)
			{
				officeFloorPosition.x = Mathf.Clamp(officeFloorPosition.x, 0, this.m_SizeX - 1);
				officeFloorPosition.y = Mathf.Clamp(officeFloorPosition.y, 0, this.m_SizeY - 1);
			}
			return officeFloorPosition;
		}

		// Token: 0x060052AF RID: 21167 RVA: 0x001931B8 File Offset: 0x001913B8
		public bool IsPositionUnblocked(OfficeFloorPosition pos)
		{
			return this.m_Floor.IsInBound(pos) && this.FloorMap[pos.x, pos.y] == 0L;
		}

		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x060052B0 RID: 21168 RVA: 0x001931E5 File Offset: 0x001913E5
		// (set) Token: 0x060052B1 RID: 21169 RVA: 0x001931F7 File Offset: 0x001913F7
		public bool AIMapEnable
		{
			get
			{
				return this.m_SelectionForAIDebug.gameObject.activeSelf;
			}
			set
			{
				NKCUtil.SetGameobjectActive(this.m_SelectionForAIDebug, value);
				if (value)
				{
					this.m_SelectionForAIDebug.SetSize(this.m_SizeX, this.m_SizeY, this.m_fTileSize);
					this.m_SelectionForAIDebug.UpdateSelectionTileForAI(this.m_FloorMap);
				}
			}
		}

		// Token: 0x060052B2 RID: 21170 RVA: 0x00193238 File Offset: 0x00191438
		public OfficeFloorPosition FindNearestEmptyCell(OfficeFloorPosition pos)
		{
			if (this.FloorMap == null)
			{
				return pos;
			}
			if (this.FloorMap[pos.x, pos.y] == 0L)
			{
				return pos;
			}
			Queue<OfficeFloorPosition> queue = new Queue<OfficeFloorPosition>();
			HashSet<OfficeFloorPosition> hashSet = new HashSet<OfficeFloorPosition>();
			queue.Enqueue(pos);
			int length = this.FloorMap.GetLength(0);
			int length2 = this.FloorMap.GetLength(1);
			while (queue.Count > 0)
			{
				OfficeFloorPosition officeFloorPosition = queue.Dequeue();
				if (officeFloorPosition.x >= 0 && officeFloorPosition.x < length && officeFloorPosition.y >= 0 && officeFloorPosition.y < length2 && !hashSet.Contains(officeFloorPosition))
				{
					if (this.FloorMap[officeFloorPosition.x, officeFloorPosition.y] == 0L)
					{
						return officeFloorPosition;
					}
					hashSet.Add(officeFloorPosition);
					queue.Enqueue(officeFloorPosition + new OfficeFloorPosition(0, 1));
					queue.Enqueue(officeFloorPosition + new OfficeFloorPosition(1, 0));
					queue.Enqueue(officeFloorPosition + new OfficeFloorPosition(0, -1));
					queue.Enqueue(officeFloorPosition + new OfficeFloorPosition(-1, 0));
				}
			}
			return pos;
		}

		// Token: 0x060052B3 RID: 21171 RVA: 0x00193368 File Offset: 0x00191568
		protected void SortFloorObjects()
		{
			this.BottomPoint = this.BottomMostPoint();
			NKCTopologicalSort<Transform> nkctopologicalSort = new NKCTopologicalSort<Transform>(new NKCTopologicalSort<Transform>.RelationFunction(this.GetRelation));
			this.m_lstTransformBuffer.Clear();
			foreach (object obj in this.trFloorFunitureRoot)
			{
				Transform item = (Transform)obj;
				this.m_lstTransformBuffer.Add(item);
			}
			List<Transform> list = nkctopologicalSort.DoSort(this.m_lstTransformBuffer);
			if (list != null)
			{
				foreach (Transform transform in list)
				{
					transform.SetAsLastSibling();
				}
			}
		}

		// Token: 0x060052B4 RID: 21172 RVA: 0x00193440 File Offset: 0x00191640
		protected ValueTuple<bool, int> GetRelation(Transform a, Transform b)
		{
			return new ValueTuple<bool, int>(!this.ISApart(a, b), this.FullComparer(a, b));
		}

		// Token: 0x060052B5 RID: 21173 RVA: 0x0019345C File Offset: 0x0019165C
		private bool ISApart(Transform a, Transform b)
		{
			NKCOfficeFuniture component = a.GetComponent<NKCOfficeFuniture>();
			NKCOfficeFuniture component2 = b.GetComponent<NKCOfficeFuniture>();
			if (component != null && component2 != null)
			{
				return this.IsFurnitureApart(component, component2);
			}
			if (component != null && component2 == null)
			{
				return this.IsFurniturePointApart(component, b);
			}
			if (component == null && component2 != null)
			{
				return this.IsFurniturePointApart(component2, a);
			}
			NKCOfficeCharacter component3 = a.GetComponent<NKCOfficeCharacter>();
			NKCOfficeCharacter component4 = b.GetComponent<NKCOfficeCharacter>();
			if (component3 != null && component4 != null)
			{
				Rect worldRect = component3.GetWorldRect();
				Rect worldRect2 = component4.GetWorldRect();
				return !worldRect.Overlaps(worldRect2);
			}
			return true;
		}

		// Token: 0x060052B6 RID: 21174 RVA: 0x00193508 File Offset: 0x00191708
		private bool IsFurniturePointApart(NKCOfficeFuniture funA, Transform b)
		{
			NKCOfficeCharacter component = b.GetComponent<NKCOfficeCharacter>();
			if (component != null)
			{
				Rect worldRect = funA.GetWorldRect(false);
				Rect worldRect2 = component.GetWorldRect();
				return !worldRect.Overlaps(worldRect2);
			}
			float num;
			float num2;
			Vector3 vector;
			Vector3 vector2;
			funA.GetWorldInfo(out num, out num2, out vector, out vector2);
			return num > b.position.z || b.position.z > num2 || (vector.x > b.position.x || b.position.x > vector2.x);
		}

		// Token: 0x060052B7 RID: 21175 RVA: 0x00193598 File Offset: 0x00191798
		private bool IsFurnitureApart(NKCOfficeFuniture a, NKCOfficeFuniture b)
		{
			Rect worldRect = a.GetWorldRect(false);
			Rect worldRect2 = b.GetWorldRect(false);
			return !worldRect.Overlaps(worldRect2);
		}

		// Token: 0x060052B8 RID: 21176 RVA: 0x001935C0 File Offset: 0x001917C0
		protected int FurnitureComparer(NKCOfficeFuniture a, NKCOfficeFuniture b)
		{
			ValueTuple<float, float> zminMax = a.GetZMinMax();
			ValueTuple<float, float> zminMax2 = b.GetZMinMax();
			if (zminMax.Item1 > zminMax2.Item2)
			{
				return -1;
			}
			if (zminMax2.Item1 > zminMax.Item2)
			{
				return 1;
			}
			ValueTuple<Vector3, Vector3> horizonalLine = a.GetHorizonalLine(0f);
			ValueTuple<Vector3, Vector3> horizonalLine2 = b.GetHorizonalLine(0f);
			int num = this.CompareFurnitureLines(horizonalLine, horizonalLine2);
			if (num != 0)
			{
				return num;
			}
			float squaredDistanceFromPoint = this.GetSquaredDistanceFromPoint(a, this.BottomPoint);
			return this.GetSquaredDistanceFromPoint(b, this.BottomPoint).CompareTo(squaredDistanceFromPoint);
		}

		// Token: 0x060052B9 RID: 21177 RVA: 0x0019364C File Offset: 0x0019184C
		protected int SimpleComparer(Transform a, Transform b)
		{
			return b.position.z.CompareTo(a.position.z);
		}

		// Token: 0x060052BA RID: 21178 RVA: 0x00193678 File Offset: 0x00191878
		protected int FullComparer(Transform a, Transform b)
		{
			NKCOfficeFuniture component = a.GetComponent<NKCOfficeFuniture>();
			NKCOfficeFuniture component2 = b.GetComponent<NKCOfficeFuniture>();
			if (component != null && component2 != null)
			{
				return this.FurnitureComparer(component, component2);
			}
			if (component != null && component2 == null)
			{
				if (this.GetPointIsBehindFurniture(b.position, component, this.BottomPoint))
				{
					return 1;
				}
				return -1;
			}
			else
			{
				if (!(component == null) || !(component2 != null))
				{
					return b.position.z.CompareTo(a.position.z);
				}
				if (this.GetPointIsBehindFurniture(a.position, component2, this.BottomPoint))
				{
					return -1;
				}
				return 1;
			}
		}

		// Token: 0x060052BB RID: 21179 RVA: 0x00193724 File Offset: 0x00191924
		private int CompareFurnitureLines(ValueTuple<Vector3, Vector3> lineA, ValueTuple<Vector3, Vector3> lineB)
		{
			float num = Mathf.Max(lineA.Item1.x, lineB.Item1.x);
			float num2 = Mathf.Min(lineA.Item2.x, lineB.Item2.x);
			if (num >= num2)
			{
				float value = Mathf.Min(lineA.Item1.y, lineA.Item2.y);
				return Mathf.Min(lineB.Item1.y, lineB.Item2.y).CompareTo(value);
			}
			Vector3 vector = lineA.Item2 - lineA.Item1;
			float num3 = vector.y / vector.x;
			float num4 = lineA.Item1.y - num3 * lineA.Item1.x;
			Vector3 vector2 = lineB.Item2 - lineB.Item1;
			float num5 = vector2.y / vector2.x;
			float num6 = lineB.Item1.y - num5 * lineB.Item1.x;
			float a = num3 * num2 + num4;
			float b = num3 * num + num4;
			float a2 = num5 * num2 + num6;
			float b2 = num5 * num + num6;
			float value2 = Mathf.Min(a, b);
			return Mathf.Min(a2, b2).CompareTo(value2);
		}

		// Token: 0x060052BC RID: 21180 RVA: 0x0019386C File Offset: 0x00191A6C
		private bool GetPointIsBehindFurniture(Vector3 P, NKCOfficeFuniture furniture, Vector3 BasePoint)
		{
			ValueTuple<Vector3, Vector3> horizonalLine = furniture.GetHorizonalLine(0f);
			return this.GetPointIsBehindLine(P, horizonalLine.Item1, horizonalLine.Item2, BasePoint);
		}

		// Token: 0x060052BD RID: 21181 RVA: 0x0019389C File Offset: 0x00191A9C
		private bool GetPointIsBehindLine(Vector3 P, Vector3 L1, Vector3 L2, Vector3 Base)
		{
			Vector3 b = L2 - L1;
			Vector3 vector = P + b;
			float num = this.LineToPointDistanceSquared(L1, L2, Base, Color.yellow);
			Debug.DrawLine(vector, P, Color.cyan);
			return this.LineToPointDistanceSquared(P, vector, Base, Color.green) > num;
		}

		// Token: 0x060052BE RID: 21182 RVA: 0x001938E8 File Offset: 0x00191AE8
		private float LineToPointDistanceSquared(Vector3 P1, Vector3 P2, Vector3 P, Color debugColor)
		{
			Vector3 lhs = P - P1;
			Vector3 rhs = P2 - P1;
			float num = Vector3.Dot(lhs, rhs);
			float sqrMagnitude = rhs.sqrMagnitude;
			if (sqrMagnitude == 0f)
			{
				return (P - P1).sqrMagnitude;
			}
			float d = num / sqrMagnitude;
			Vector3 vector = P1 + d * (P2 - P1);
			Vector3 vector2 = P - vector;
			Debug.DrawLine(P, vector, debugColor);
			return vector2.sqrMagnitude;
		}

		// Token: 0x060052BF RID: 21183 RVA: 0x00193960 File Offset: 0x00191B60
		private float GetSquaredDistanceFromPoint(NKCOfficeFuniture furniture, Vector3 P)
		{
			ValueTuple<Vector3, Vector3> horizonalLine = furniture.GetHorizonalLine(0f);
			return this.SegmentToPointDistanceSquared(horizonalLine.Item1, horizonalLine.Item2, P);
		}

		// Token: 0x060052C0 RID: 21184 RVA: 0x0019398C File Offset: 0x00191B8C
		protected float SegmentToPointDistanceSquared(Vector3 P1, Vector3 P2, Vector3 P)
		{
			Vector3 lhs = P - P1;
			Vector3 rhs = P2 - P1;
			float num = Vector3.Dot(lhs, rhs);
			float sqrMagnitude = rhs.sqrMagnitude;
			if (sqrMagnitude == 0f)
			{
				return (P - P1).sqrMagnitude;
			}
			float num2 = num / sqrMagnitude;
			Vector3 b;
			if (num2 < 0f)
			{
				b = P1;
			}
			else if (num2 > 1f)
			{
				b = P2;
			}
			else
			{
				b = P1 + num2 * (P2 - P1);
			}
			return (P - b).sqrMagnitude;
		}

		// Token: 0x060052C1 RID: 21185 RVA: 0x00193A14 File Offset: 0x00191C14
		protected Vector3 BottomMostPoint()
		{
			this.m_Floor.Rect.GetWorldCorners(this.buffer);
			return this.buffer[0];
		}

		// Token: 0x060052C2 RID: 21186 RVA: 0x00193A38 File Offset: 0x00191C38
		public NKCOfficeCharacter GetCharacter(int unitID)
		{
			foreach (KeyValuePair<long, NKCOfficeCharacter> keyValuePair in this.m_dicCharacter)
			{
				NKCOfficeCharacter value = keyValuePair.Value;
				if (value.UnitID == unitID)
				{
					return value;
				}
			}
			return null;
		}

		// Token: 0x060052C3 RID: 21187 RVA: 0x00193A9C File Offset: 0x00191C9C
		public IEnumerable<NKCOfficeCharacter> GetCharacterEnumerator()
		{
			return this.m_dicCharacter.Values;
		}

		// Token: 0x060052C4 RID: 21188 RVA: 0x00193AA9 File Offset: 0x00191CA9
		public IEnumerable<NKCOfficeCharacter> GetCharactersInRange(Vector3 worldpos, float range)
		{
			float rangeSqr = range * range;
			foreach (NKCOfficeCharacter nkcofficeCharacter in this.m_dicCharacter.Values)
			{
				if ((nkcofficeCharacter.transform.position - worldpos).sqrMagnitude <= rangeSqr)
				{
					yield return nkcofficeCharacter;
				}
			}
			Dictionary<long, NKCOfficeCharacter>.ValueCollection.Enumerator enumerator = default(Dictionary<long, NKCOfficeCharacter>.ValueCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060052C5 RID: 21189 RVA: 0x00193AC7 File Offset: 0x00191CC7
		public virtual void OnCharacterBeginDrag(NKCOfficeCharacter character)
		{
		}

		// Token: 0x060052C6 RID: 21190 RVA: 0x00193AC9 File Offset: 0x00191CC9
		public virtual void OnCharacterEndDrag(NKCOfficeCharacter character)
		{
		}

		// Token: 0x060052C7 RID: 21191 RVA: 0x00193ACB File Offset: 0x00191CCB
		public virtual NKCOfficeFuniture FindInteractableInterior(NKCOfficeCharacter character)
		{
			return null;
		}

		// Token: 0x060052C8 RID: 21192 RVA: 0x00193ACE File Offset: 0x00191CCE
		public virtual NKCOfficeCharacter FindInteractableCharacter(NKCOfficeCharacter character)
		{
			return null;
		}

		// Token: 0x060052C9 RID: 21193 RVA: 0x00193AD4 File Offset: 0x00191CD4
		public bool CalcInteractionPos(NKCOfficeCharacter actor, NKCOfficeCharacter target, out Vector3 actorPos, out Vector3 targetPos)
		{
			Vector3 vector = (actor.transform.localPosition + target.transform.localPosition) * 0.5f;
			Vector3 vector2 = vector + new Vector3(-this.m_fTileSize, this.m_fTileSize, 0f);
			Vector3 vector3 = vector + new Vector3(this.m_fTileSize, -this.m_fTileSize + 1f, 0f);
			foreach (Vector3 localPos in new Vector3[]
			{
				vector,
				vector2,
				vector3
			})
			{
				OfficeFloorPosition pos = this.CalculateFloorPosition(localPos, 1, 1, false);
				if (!this.IsPositionUnblocked(pos))
				{
					actorPos = actor.transform.localPosition;
					targetPos = target.transform.localPosition;
					return false;
				}
			}
			if (actor.transform.position.x <= target.transform.position.x)
			{
				actorPos = vector2;
				targetPos = vector3;
			}
			else
			{
				actorPos = vector3;
				targetPos = vector2;
			}
			return true;
		}

		// Token: 0x060052CA RID: 21194 RVA: 0x00193C08 File Offset: 0x00191E08
		public void OnPartyFinished(NKCOfficePartyTemplet partyTemplet)
		{
			if (partyTemplet == null)
			{
				return;
			}
			foreach (NKCOfficeCharacter nkcofficeCharacter in this.m_dicCharacter.Values)
			{
				List<string> list = new List<string>();
				foreach (string text in partyTemplet.PartyEndAni)
				{
					List<NKCAnimationEventTemplet> lstAnim = NKCAnimationEventManager.Find(text);
					if (NKCAnimationEventManager.CanPlayAnimEvent(nkcofficeCharacter, lstAnim))
					{
						list.Add(text);
					}
				}
				if (list.Count > 0)
				{
					List<NKCAnimationEventTemplet> lstAnimEvent = NKCAnimationEventManager.Find(list[UnityEngine.Random.Range(0, list.Count)]);
					nkcofficeCharacter.StopAllAnimInstances();
					nkcofficeCharacter.EnqueueAnimation(lstAnimEvent);
				}
			}
		}

		// Token: 0x0400426C RID: 17004
		public const float BASE_TILE_SIZE = 100f;

		// Token: 0x0400426D RID: 17005
		public NKCOfficeFloor m_Floor;

		// Token: 0x0400426E RID: 17006
		public NKCOfficeFloor m_FloorTile;

		// Token: 0x0400426F RID: 17007
		public NKCOfficeWall m_LeftWall;

		// Token: 0x04004270 RID: 17008
		public NKCOfficeWall m_RightWall;

		// Token: 0x04004271 RID: 17009
		public RectTransform m_rtBackgroundRoot;

		// Token: 0x04004272 RID: 17010
		protected GameObject m_objBackground;

		// Token: 0x04004273 RID: 17011
		protected RectTransform m_rtBackground;

		// Token: 0x04004274 RID: 17012
		public NKCOfficeSelectableTile m_SelectionForAIDebug;

		// Token: 0x04004275 RID: 17013
		protected Dictionary<long, NKCOfficeCharacter> m_dicCharacter = new Dictionary<long, NKCOfficeCharacter>();

		// Token: 0x04004276 RID: 17014
		public int m_SizeX = 16;

		// Token: 0x04004277 RID: 17015
		public int m_SizeY = 16;

		// Token: 0x04004278 RID: 17016
		public int m_wallHeight = 6;

		// Token: 0x04004279 RID: 17017
		public float m_fTileSize = 100f;

		// Token: 0x0400427A RID: 17018
		[Header("BG 고정 Size. 수정하지 마세요.")]
		public float m_fBGWidth = 5708f;

		// Token: 0x0400427B RID: 17019
		[Header("Camera")]
		public float m_fCameraZPos = -676f;

		// Token: 0x0400427C RID: 17020
		public Vector2 m_vCameraOverflowRange = new Vector2(300f, 200f);

		// Token: 0x0400427D RID: 17021
		private bool m_bEnableDrag = true;

		// Token: 0x0400427E RID: 17022
		protected float m_fCameraOffset;

		// Token: 0x0400427F RID: 17023
		protected Rect m_rectWorld;

		// Token: 0x04004280 RID: 17024
		protected Rect m_rectCamMoveRange;

		// Token: 0x04004281 RID: 17025
		protected Rect m_rectCamLimit;

		// Token: 0x04004282 RID: 17026
		protected NKCOfficeFuniture.OnClickFuniture dOnSelectFuniture;

		// Token: 0x04004283 RID: 17027
		private long sdCount;

		// Token: 0x04004284 RID: 17028
		private Vector2 m_vTotalMove;

		// Token: 0x04004285 RID: 17029
		private Vector2 m_vCamPosBefore;

		// Token: 0x04004286 RID: 17030
		private bool m_bDragging;

		// Token: 0x04004287 RID: 17031
		[Header("스크롤 관련")]
		public float m_fScrollSensibility = 1f;

		// Token: 0x04004288 RID: 17032
		public float m_fCamReturnRate = 0.2f;

		// Token: 0x04004289 RID: 17033
		public float m_fPinchZoomRate = 0.5f;

		// Token: 0x0400428A RID: 17034
		public float m_fKeyboardMoveSpeed = 10f;

		// Token: 0x0400428B RID: 17035
		public float m_fScrollZoomSensibility = 100f;

		// Token: 0x0400428C RID: 17036
		protected long[,] m_FloorMap;

		// Token: 0x0400428D RID: 17037
		private List<Transform> m_lstTransformBuffer = new List<Transform>();

		// Token: 0x0400428E RID: 17038
		private Vector3 BottomPoint;

		// Token: 0x0400428F RID: 17039
		private Vector3[] buffer = new Vector3[4];

		// Token: 0x020014D9 RID: 5337
		protected struct FloorRect
		{
			// Token: 0x0600AA01 RID: 43521 RVA: 0x0034E2C4 File Offset: 0x0034C4C4
			public override string ToString()
			{
				return string.Format("({0},{1}) [{2},{3}]", new object[]
				{
					this.x,
					this.y,
					this.sizeX,
					this.sizeY
				});
			}

			// Token: 0x04009F2D RID: 40749
			public int x;

			// Token: 0x04009F2E RID: 40750
			public int y;

			// Token: 0x04009F2F RID: 40751
			public int sizeX;

			// Token: 0x04009F30 RID: 40752
			public int sizeY;
		}
	}
}
