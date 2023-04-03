using System;
using System.Collections.Generic;
using ClientPacket.Office;
using DG.Tweening;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Office
{
	// Token: 0x02000AF5 RID: 2805
	public class NKCUIOfficeMinimapFacility : MonoBehaviour, IOfficeMinimap
	{
		// Token: 0x06007E73 RID: 32371 RVA: 0x002A65E7 File Offset: 0x002A47E7
		public GameObject GetGameObject()
		{
			return base.gameObject;
		}

		// Token: 0x06007E74 RID: 32372 RVA: 0x002A65EF File Offset: 0x002A47EF
		public ScrollRect GetScrollRect()
		{
			return this.m_scrollRectMapRoom;
		}

		// Token: 0x06007E75 RID: 32373 RVA: 0x002A65F7 File Offset: 0x002A47F7
		public float GetScrollRectContentOriginalWidth()
		{
			return this.m_fOriginalScrollRectContentPreferredWidth;
		}

		// Token: 0x06007E76 RID: 32374 RVA: 0x002A6600 File Offset: 0x002A4800
		public void Init()
		{
			this.m_dicFacilityInfo.Clear();
			if (this.m_facilityInfoArray != null)
			{
				int num = this.m_facilityInfoArray.Length;
				for (int i = 0; i < num; i++)
				{
					if (!this.m_dicFacilityInfo.ContainsKey(this.m_facilityInfoArray[i].key))
					{
						this.m_dicFacilityInfo.Add(this.m_facilityInfoArray[i].key, this.m_facilityInfoArray[i]);
					}
					else
					{
						Debug.LogError("Same facility Info in NKCUIOfficeMinimapFacility Prefab");
					}
				}
			}
			this.m_dicMapTileFacility.Clear();
			ScrollRect scrollRectMapRoom = this.m_scrollRectMapRoom;
			NKCUIComOfficeMapTileFacility[] array;
			if (scrollRectMapRoom == null)
			{
				array = null;
			}
			else
			{
				RectTransform content = scrollRectMapRoom.content;
				array = ((content != null) ? content.GetComponentsInChildren<NKCUIComOfficeMapTileFacility>() : null);
			}
			NKCUIComOfficeMapTileFacility[] array2 = array;
			if (array2 != null)
			{
				int num2 = array2.Length;
				for (int j = 0; j < num2; j++)
				{
					if (NKMOfficeRoomTemplet.Find(array2[j].m_iRoomId) != null)
					{
						if (this.m_dicMapTileFacility.ContainsKey(array2[j].m_iRoomId))
						{
							Debug.LogError(string.Format("Same Room Key Exist in MINIMAP_FACILITY Prefab, RoomId: {0}", array2[j].m_iRoomId));
						}
						else
						{
							this.m_dicMapTileFacility.Add(array2[j].m_iRoomId, array2[j]);
						}
					}
					array2[j].Init();
				}
			}
			if (this.m_scrollRectMapRoom != null)
			{
				LayoutElement component = this.m_scrollRectMapRoom.content.GetComponent<LayoutElement>();
				if (component != null)
				{
					this.m_fOriginalScrollRectContentPreferredWidth = component.preferredWidth;
				}
				this.m_scrollRectMapRoom.onValueChanged.RemoveAllListeners();
				this.m_scrollRectMapRoom.onValueChanged.AddListener(new UnityAction<Vector2>(this.OnScrollRectValueChanged));
			}
			this.UpdateRedDotAll();
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007E77 RID: 32375 RVA: 0x002A67A4 File Offset: 0x002A49A4
		public Transform GetScrollTargetTileTransform(int sectionId)
		{
			List<Transform> list = new List<Transform>();
			foreach (KeyValuePair<int, NKCUIComOfficeMapTileFacility> keyValuePair in this.m_dicMapTileFacility)
			{
				NKMOfficeRoomTemplet nkmofficeRoomTemplet = NKMOfficeRoomTemplet.Find(keyValuePair.Key);
				if (nkmofficeRoomTemplet != null && nkmofficeRoomTemplet.SectionId == sectionId)
				{
					list.Add(keyValuePair.Value.transform);
				}
			}
			if (list.Count <= 0)
			{
				return null;
			}
			list.Sort(delegate(Transform e1, Transform e2)
			{
				if (e1.position.x > e2.position.x)
				{
					return 1;
				}
				if (e1.position.x < e2.position.x)
				{
					return -1;
				}
				return 0;
			});
			return list[0];
		}

		// Token: 0x06007E78 RID: 32376 RVA: 0x002A6858 File Offset: 0x002A4A58
		public RectTransform GetTileRectTransform(int roomId)
		{
			if (this.m_dicMapTileFacility.ContainsKey(roomId))
			{
				return this.m_dicMapTileFacility[roomId].GetComponent<RectTransform>();
			}
			return null;
		}

		// Token: 0x06007E79 RID: 32377 RVA: 0x002A687C File Offset: 0x002A4A7C
		public Transform GetRightEndTileTransform()
		{
			List<Transform> list = new List<Transform>();
			foreach (KeyValuePair<int, NKCUIComOfficeMapTileFacility> keyValuePair in this.m_dicMapTileFacility)
			{
				list.Add(keyValuePair.Value.transform);
			}
			if (list.Count <= 0)
			{
				return null;
			}
			list.Sort(delegate(Transform e1, Transform e2)
			{
				if (e1.position.x < e2.position.x)
				{
					return 1;
				}
				if (e1.position.x > e2.position.x)
				{
					return -1;
				}
				return 0;
			});
			return list[0];
		}

		// Token: 0x06007E7A RID: 32378 RVA: 0x002A6918 File Offset: 0x002A4B18
		public void SetActive(bool value)
		{
			base.gameObject.SetActive(value);
			if (value)
			{
				this.UpdateCameraPosition();
			}
		}

		// Token: 0x06007E7B RID: 32379 RVA: 0x002A6930 File Offset: 0x002A4B30
		public void UpdateRoomStateAll()
		{
			foreach (KeyValuePair<int, NKCUIComOfficeMapTileFacility> keyValuePair in this.m_dicMapTileFacility)
			{
				keyValuePair.Value.UpdateRoomState(this.m_dicFacilityInfo, this.m_LockInfo);
			}
		}

		// Token: 0x06007E7C RID: 32380 RVA: 0x002A6994 File Offset: 0x002A4B94
		public void UpdateRoomState(NKMOfficeRoomTemplet.RoomType roomType)
		{
			foreach (KeyValuePair<int, NKCUIComOfficeMapTileFacility> keyValuePair in this.m_dicMapTileFacility)
			{
				if (keyValuePair.Value.RoomType == roomType)
				{
					keyValuePair.Value.UpdateRoomState(this.m_dicFacilityInfo, this.m_LockInfo);
					break;
				}
			}
		}

		// Token: 0x06007E7D RID: 32381 RVA: 0x002A6A0C File Offset: 0x002A4C0C
		public void UpdateRoomStateInSection(int sectionId)
		{
			NKMOfficeSectionTemplet nkmofficeSectionTemplet = NKMOfficeSectionTemplet.Find(sectionId);
			if (nkmofficeSectionTemplet == null)
			{
				return;
			}
			foreach (KeyValuePair<int, NKMOfficeRoomTemplet> keyValuePair in nkmofficeSectionTemplet.Rooms)
			{
				if (this.m_dicMapTileFacility.ContainsKey(keyValuePair.Key))
				{
					this.m_dicMapTileFacility[keyValuePair.Key].UpdateRoomState(this.m_dicFacilityInfo, this.m_LockInfo);
				}
			}
			NKCUIOfficeMapFront instance = NKCUIOfficeMapFront.GetInstance();
			if (instance == null)
			{
				return;
			}
			NKCUIOfficeUpsideMenu officeUpsideMenu = instance.OfficeUpsideMenu;
			if (officeUpsideMenu == null)
			{
				return;
			}
			officeUpsideMenu.UpdateMinimapRoomInfo();
		}

		// Token: 0x06007E7E RID: 32382 RVA: 0x002A6AB0 File Offset: 0x002A4CB0
		public void UpdateRoomInfo(NKMOfficeRoom officeRoom)
		{
		}

		// Token: 0x06007E7F RID: 32383 RVA: 0x002A6AB2 File Offset: 0x002A4CB2
		public void UpdatePurchasedRoom(NKMOfficeRoom officeRoom)
		{
		}

		// Token: 0x06007E80 RID: 32384 RVA: 0x002A6AB4 File Offset: 0x002A4CB4
		public void LockRoomsInSection(int sectionId)
		{
		}

		// Token: 0x06007E81 RID: 32385 RVA: 0x002A6AB8 File Offset: 0x002A4CB8
		public void ExpandScrollRectRange()
		{
			if (this.m_scrollRectMapRoom == null)
			{
				return;
			}
			Vector3 position = this.GetRightEndTileTransform().position;
			float popupWidth = NKCUIPopupOfficeMemberEdit.Instance.PopupWidth;
			if (popupWidth > 0f)
			{
				float x = ((float)Screen.width - popupWidth) * 0.5f;
				if (NKCUIManager.FrontCanvas != null)
				{
					Vector3 vector = NKCUIManager.FrontCanvas.worldCamera.ScreenToWorldPoint(new Vector3(x, (float)Screen.height * 0.5f, 0f));
					position.x -= vector.x;
				}
			}
			Vector3 position2 = this.m_scrollRectMapRoom.content.position;
			Vector3 position3 = new Vector3(position2.x - position.x, position2.y, position2.z);
			ref Vector3 ptr = NKCUIManager.FrontCanvas.worldCamera.WorldToScreenPoint(position3);
			float num = this.m_scrollRectMapRoom.content.rect.width * NKCUIManager.FrontCanvas.scaleFactor;
			Vector2 pivot = this.m_scrollRectMapRoom.content.pivot;
			float num2 = ptr.x + (1f - this.m_scrollRectMapRoom.content.pivot.x) * num;
			float num3 = ((float)Screen.width - num2) * 2f / NKCUIManager.FrontCanvas.scaleFactor;
			this.m_scrollRectMapRoom.content.GetComponent<LayoutElement>().preferredWidth = this.m_fOriginalScrollRectContentPreferredWidth + num3;
		}

		// Token: 0x06007E82 RID: 32386 RVA: 0x002A6C24 File Offset: 0x002A4E24
		public void RevertScrollRectRange()
		{
			if (this.m_scrollRectMapRoom == null)
			{
				return;
			}
			this.m_scrollRectMapRoom.content.DOKill(false);
			LayoutElement component = this.m_scrollRectMapRoom.content.GetComponent<LayoutElement>();
			if (component != null)
			{
				component.preferredWidth = this.m_fOriginalScrollRectContentPreferredWidth;
			}
		}

		// Token: 0x06007E83 RID: 32387 RVA: 0x002A6C78 File Offset: 0x002A4E78
		public bool IsRedDotOn()
		{
			foreach (KeyValuePair<int, NKCUIComOfficeMapTileFacility> keyValuePair in this.m_dicMapTileFacility)
			{
				if (keyValuePair.Value.IsRedDotOn)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007E84 RID: 32388 RVA: 0x002A6CDC File Offset: 0x002A4EDC
		public void UpdateRedDotAll()
		{
			foreach (KeyValuePair<int, NKCUIComOfficeMapTileFacility> keyValuePair in this.m_dicMapTileFacility)
			{
				keyValuePair.Value.UpdateRedDot();
			}
		}

		// Token: 0x06007E85 RID: 32389 RVA: 0x002A6D34 File Offset: 0x002A4F34
		public void UpdateCameraPosition()
		{
			if (this.m_scrollRectMapRoom.content.sizeDelta.x <= 0f)
			{
				this.m_scrollRectMapRoom.horizontalNormalizedPosition = 0f;
			}
			if (this.m_dOnScrollCamMove != null)
			{
				this.m_dOnScrollCamMove(this.m_scrollRectMapRoom.normalizedPosition);
			}
		}

		// Token: 0x06007E86 RID: 32390 RVA: 0x002A6D8C File Offset: 0x002A4F8C
		private void OnScrollRectValueChanged(Vector2 value)
		{
			if (this.m_scrollRectMapRoom == null)
			{
				return;
			}
			if (this.m_dOnScrollCamMove != null && this.m_scrollRectMapRoom.content.sizeDelta.x > 0f)
			{
				this.m_dOnScrollCamMove(value);
			}
		}

		// Token: 0x06007E87 RID: 32391 RVA: 0x002A6DD8 File Offset: 0x002A4FD8
		private void OnDestroy()
		{
			if (this.m_dicFacilityInfo != null)
			{
				this.m_dicFacilityInfo.Clear();
				this.m_dicFacilityInfo = null;
			}
			if (this.m_dicMapTileFacility != null)
			{
				this.m_dicMapTileFacility.Clear();
				this.m_dicMapTileFacility = null;
			}
		}

		// Token: 0x04006B2E RID: 27438
		public ScrollRect m_scrollRectMapRoom;

		// Token: 0x04006B2F RID: 27439
		[Header("시설 상태 색상 정보")]
		public NKCUIOfficeMinimapFacility.FacilityInfo m_LockInfo;

		// Token: 0x04006B30 RID: 27440
		public NKCUIOfficeMinimapFacility.FacilityInfo[] m_facilityInfoArray;

		// Token: 0x04006B31 RID: 27441
		private Dictionary<string, NKCUIOfficeMinimapFacility.FacilityInfo> m_dicFacilityInfo = new Dictionary<string, NKCUIOfficeMinimapFacility.FacilityInfo>();

		// Token: 0x04006B32 RID: 27442
		private Dictionary<int, NKCUIComOfficeMapTileFacility> m_dicMapTileFacility = new Dictionary<int, NKCUIComOfficeMapTileFacility>();

		// Token: 0x04006B33 RID: 27443
		private float m_fOriginalScrollRectContentPreferredWidth;

		// Token: 0x04006B34 RID: 27444
		public NKCUIOfficeMinimapFacility.OnScroll m_dOnScrollCamMove;

		// Token: 0x02001873 RID: 6259
		[Serializable]
		public struct FacilityInfo
		{
			// Token: 0x0400A8F9 RID: 43257
			public string key;

			// Token: 0x0400A8FA RID: 43258
			public string m_bgColor;

			// Token: 0x0400A8FB RID: 43259
			public string m_glowColor;

			// Token: 0x0400A8FC RID: 43260
			public string m_strokeColor;

			// Token: 0x0400A8FD RID: 43261
			public string m_titleColor;

			// Token: 0x0400A8FE RID: 43262
			public string m_npcColor;

			// Token: 0x0400A8FF RID: 43263
			public string m_strIcon;
		}

		// Token: 0x02001874 RID: 6260
		// (Invoke) Token: 0x0600B5F1 RID: 46577
		public delegate void OnScroll(Vector2 value);
	}
}
