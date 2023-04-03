using System;
using System.Collections.Generic;
using ClientPacket.Office;
using NKM;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Office
{
	// Token: 0x02000AF6 RID: 2806
	public class NKCUIOfficeMinimapRoom : MonoBehaviour, IOfficeMinimap
	{
		// Token: 0x06007E89 RID: 32393 RVA: 0x002A6E2C File Offset: 0x002A502C
		public GameObject GetGameObject()
		{
			return base.gameObject;
		}

		// Token: 0x06007E8A RID: 32394 RVA: 0x002A6E34 File Offset: 0x002A5034
		public ScrollRect GetScrollRect()
		{
			return this.m_scrollRectMapRoom;
		}

		// Token: 0x06007E8B RID: 32395 RVA: 0x002A6E3C File Offset: 0x002A503C
		public float GetScrollRectContentOriginalWidth()
		{
			return this.m_fOriginalScrollRectContentPreferredWidth;
		}

		// Token: 0x06007E8C RID: 32396 RVA: 0x002A6E44 File Offset: 0x002A5044
		public void Init()
		{
			this.SortTilesAlongYaxis();
			this.m_dicMapTileRoom.Clear();
			ScrollRect scrollRectMapRoom = this.m_scrollRectMapRoom;
			NKCUIComOfficeMapTileRoom[] array;
			if (scrollRectMapRoom == null)
			{
				array = null;
			}
			else
			{
				RectTransform content = scrollRectMapRoom.content;
				array = ((content != null) ? content.GetComponentsInChildren<NKCUIComOfficeMapTileRoom>() : null);
			}
			NKCUIComOfficeMapTileRoom[] array2 = array;
			if (array2 != null)
			{
				int num = array2.Length;
				for (int i = 0; i < num; i++)
				{
					if (NKMOfficeRoomTemplet.Find(array2[i].m_iRoomId) != null)
					{
						array2[i].Init();
						if (this.m_dicMapTileRoom.ContainsKey(array2[i].m_iRoomId))
						{
							Debug.LogError(string.Format("Same Room Key Exist in MINIMAP_ROOM Prefab, RoomId: {0}", array2[i].m_iRoomId));
						}
						else
						{
							this.m_dicMapTileRoom.Add(array2[i].m_iRoomId, array2[i]);
						}
					}
				}
			}
			if (this.m_scrollRectMapRoom != null)
			{
				LayoutElement component = this.m_scrollRectMapRoom.content.GetComponent<LayoutElement>();
				if (component != null)
				{
					this.m_fOriginalScrollRectContentPreferredWidth = component.preferredWidth;
				}
				this.m_scrollRectMapRoom.content.pivot = new Vector2(0.5f, 0.5f);
				this.m_scrollRectMapRoom.onValueChanged.RemoveAllListeners();
				this.m_scrollRectMapRoom.onValueChanged.AddListener(new UnityAction<Vector2>(this.OnScrollRectValueChanged));
				this.m_eScrollRectExpanded = NKCUIOfficeMinimapRoom.ScrollRectExpanded.Normal;
			}
			this.m_bFirstOpen = true;
			this.UpdateRedDotAll();
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007E8D RID: 32397 RVA: 0x002A6F94 File Offset: 0x002A5194
		public Transform GetScrollTargetTileTransform(int sectionId)
		{
			List<Transform> list = new List<Transform>();
			foreach (KeyValuePair<int, NKCUIComOfficeMapTileRoom> keyValuePair in this.m_dicMapTileRoom)
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

		// Token: 0x06007E8E RID: 32398 RVA: 0x002A7048 File Offset: 0x002A5248
		public RectTransform GetTileRectTransform(int roomId)
		{
			if (this.m_dicMapTileRoom.ContainsKey(roomId))
			{
				return this.m_dicMapTileRoom[roomId].GetComponent<RectTransform>();
			}
			return null;
		}

		// Token: 0x06007E8F RID: 32399 RVA: 0x002A706C File Offset: 0x002A526C
		public Transform GetRightEndTileTransform()
		{
			List<Transform> list = new List<Transform>();
			foreach (KeyValuePair<int, NKCUIComOfficeMapTileRoom> keyValuePair in this.m_dicMapTileRoom)
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

		// Token: 0x06007E90 RID: 32400 RVA: 0x002A7108 File Offset: 0x002A5308
		public void SetActive(bool value)
		{
			base.gameObject.SetActive(value);
			if (value)
			{
				if (this.m_bFirstOpen)
				{
					Vector2 normalizedPosition = this.m_scrollRectMapRoom.normalizedPosition;
					normalizedPosition.x = 0f;
					this.m_scrollRectMapRoom.normalizedPosition = normalizedPosition;
					this.m_bFirstOpen = false;
				}
				this.UpdateCameraPosition();
				this.UpdateRedDotAll();
			}
		}

		// Token: 0x06007E91 RID: 32401 RVA: 0x002A7164 File Offset: 0x002A5364
		public void UpdateRoomStateAll()
		{
			foreach (KeyValuePair<int, NKCUIComOfficeMapTileRoom> keyValuePair in this.m_dicMapTileRoom)
			{
				keyValuePair.Value.UpdateRoomState();
			}
		}

		// Token: 0x06007E92 RID: 32402 RVA: 0x002A71BC File Offset: 0x002A53BC
		public void UpdateRoomState(NKMOfficeRoomTemplet.RoomType roomType)
		{
			foreach (KeyValuePair<int, NKCUIComOfficeMapTileRoom> keyValuePair in this.m_dicMapTileRoom)
			{
				if (keyValuePair.Value.RoomType == roomType)
				{
					keyValuePair.Value.UpdateRoomState();
					break;
				}
			}
		}

		// Token: 0x06007E93 RID: 32403 RVA: 0x002A7228 File Offset: 0x002A5428
		public void UpdateRoomStateInSection(int sectionId)
		{
			NKMOfficeSectionTemplet nkmofficeSectionTemplet = NKMOfficeSectionTemplet.Find(sectionId);
			if (nkmofficeSectionTemplet == null)
			{
				return;
			}
			foreach (KeyValuePair<int, NKMOfficeRoomTemplet> keyValuePair in nkmofficeSectionTemplet.Rooms)
			{
				if (this.m_dicMapTileRoom.ContainsKey(keyValuePair.Key))
				{
					this.m_dicMapTileRoom[keyValuePair.Key].UpdateRoomState();
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

		// Token: 0x06007E94 RID: 32404 RVA: 0x002A72C0 File Offset: 0x002A54C0
		public void UpdateRoomInfo(NKMOfficeRoom officeRoom)
		{
			if (this.m_dicMapTileRoom.ContainsKey(officeRoom.id))
			{
				this.m_dicMapTileRoom[officeRoom.id].UpdateRoomInfo(officeRoom);
			}
		}

		// Token: 0x06007E95 RID: 32405 RVA: 0x002A72EC File Offset: 0x002A54EC
		public void UpdatePurchasedRoom(NKMOfficeRoom officeRoom)
		{
			if (this.m_dicMapTileRoom.ContainsKey(officeRoom.id))
			{
				this.m_dicMapTileRoom[officeRoom.id].UpdateRoomState();
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

		// Token: 0x06007E96 RID: 32406 RVA: 0x002A733C File Offset: 0x002A553C
		public void LockRoomsInSection(int sectionId)
		{
			foreach (KeyValuePair<int, NKCUIComOfficeMapTileRoom> keyValuePair in this.m_dicMapTileRoom)
			{
				if (keyValuePair.Value.m_iSectionId == sectionId)
				{
					keyValuePair.Value.LockRoom();
				}
			}
		}

		// Token: 0x06007E97 RID: 32407 RVA: 0x002A73A4 File Offset: 0x002A55A4
		public void ExpandScrollRectRange()
		{
			if (this.m_scrollRectMapRoom == null || this.m_eScrollRectExpanded == NKCUIOfficeMinimapRoom.ScrollRectExpanded.Expanded)
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
			this.m_scrollRectMapRoom.content.GetComponent<LayoutElement>().preferredWidth += num3;
			this.m_eScrollRectExpanded = NKCUIOfficeMinimapRoom.ScrollRectExpanded.Expanded;
		}

		// Token: 0x06007E98 RID: 32408 RVA: 0x002A7520 File Offset: 0x002A5720
		public void RevertScrollRectRange()
		{
			if (this.m_scrollRectMapRoom == null || this.m_eScrollRectExpanded == NKCUIOfficeMinimapRoom.ScrollRectExpanded.Normal)
			{
				return;
			}
			LayoutElement component = this.m_scrollRectMapRoom.content.GetComponent<LayoutElement>();
			if (component != null)
			{
				component.preferredWidth = this.m_fOriginalScrollRectContentPreferredWidth;
			}
			this.m_eScrollRectExpanded = NKCUIOfficeMinimapRoom.ScrollRectExpanded.Normal;
		}

		// Token: 0x06007E99 RID: 32409 RVA: 0x002A7574 File Offset: 0x002A5774
		public bool IsRedDotOn()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			return nkmuserData != null && NKCAlarmManager.CheckOfficeDormNotify(nkmuserData);
		}

		// Token: 0x06007E9A RID: 32410 RVA: 0x002A7594 File Offset: 0x002A5794
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

		// Token: 0x06007E9B RID: 32411 RVA: 0x002A75EC File Offset: 0x002A57EC
		public void UpdateRoomFxAll()
		{
			foreach (KeyValuePair<int, NKCUIComOfficeMapTileRoom> keyValuePair in this.m_dicMapTileRoom)
			{
				keyValuePair.Value.UpdateFxState();
			}
		}

		// Token: 0x06007E9C RID: 32412 RVA: 0x002A7644 File Offset: 0x002A5844
		public void UpdateRedDotAll()
		{
			foreach (KeyValuePair<int, NKCUIComOfficeMapTileRoom> keyValuePair in this.m_dicMapTileRoom)
			{
				keyValuePair.Value.UpdateRedDot();
			}
		}

		// Token: 0x06007E9D RID: 32413 RVA: 0x002A769C File Offset: 0x002A589C
		private void SortTilesAlongYaxis()
		{
			ScrollRect scrollRectMapRoom = this.m_scrollRectMapRoom;
			if (((scrollRectMapRoom != null) ? scrollRectMapRoom.content : null) != null)
			{
				List<Transform> list = new List<Transform>();
				int childCount = this.m_scrollRectMapRoom.content.childCount;
				for (int i = 0; i < childCount; i++)
				{
					list.Add(this.m_scrollRectMapRoom.content.GetChild(i));
				}
				list.Sort(delegate(Transform e1, Transform e2)
				{
					if (e1.position.y < e2.position.y)
					{
						return 1;
					}
					if (e1.position.y > e2.position.y)
					{
						return -1;
					}
					return 0;
				});
				for (int j = 0; j < childCount; j++)
				{
					list[j].SetSiblingIndex(j);
				}
			}
		}

		// Token: 0x06007E9E RID: 32414 RVA: 0x002A773C File Offset: 0x002A593C
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
			if (!NKCUIPopupOfficeMemberEdit.IsInstanceOpen)
			{
				return;
			}
			if (value.x > 0.5f && this.m_eScrollRectExpanded == NKCUIOfficeMinimapRoom.ScrollRectExpanded.Normal)
			{
				this.ExpandScrollRectRange();
				return;
			}
			if (value.x < 0.5f && this.m_eScrollRectExpanded == NKCUIOfficeMinimapRoom.ScrollRectExpanded.Expanded)
			{
				this.RevertScrollRectRange();
			}
		}

		// Token: 0x06007E9F RID: 32415 RVA: 0x002A77C8 File Offset: 0x002A59C8
		private void OnDestroy()
		{
			if (this.m_dicMapTileRoom != null)
			{
				this.m_dicMapTileRoom.Clear();
				this.m_dicMapTileRoom = null;
			}
		}

		// Token: 0x04006B35 RID: 27445
		public ScrollRect m_scrollRectMapRoom;

		// Token: 0x04006B36 RID: 27446
		private Dictionary<int, NKCUIComOfficeMapTileRoom> m_dicMapTileRoom = new Dictionary<int, NKCUIComOfficeMapTileRoom>();

		// Token: 0x04006B37 RID: 27447
		private NKCUIOfficeMinimapRoom.ScrollRectExpanded m_eScrollRectExpanded;

		// Token: 0x04006B38 RID: 27448
		private float m_fOriginalScrollRectContentPreferredWidth;

		// Token: 0x04006B39 RID: 27449
		private bool m_bFirstOpen;

		// Token: 0x04006B3A RID: 27450
		public NKCUIOfficeMinimapRoom.OnScroll m_dOnScrollCamMove;

		// Token: 0x02001876 RID: 6262
		public enum ScrollRectExpanded
		{
			// Token: 0x0400A904 RID: 43268
			Normal,
			// Token: 0x0400A905 RID: 43269
			Expanded
		}

		// Token: 0x02001877 RID: 6263
		// (Invoke) Token: 0x0600B5F9 RID: 46585
		public delegate void OnScroll(Vector2 value);
	}
}
