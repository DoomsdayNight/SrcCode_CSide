using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NKC.Office
{
	// Token: 0x0200082D RID: 2093
	public class NKCOfficeFacility : NKCOfficeBuildingBase
	{
		// Token: 0x06005346 RID: 21318 RVA: 0x00196250 File Offset: 0x00194450
		public static NKCOfficeFacility GetInstance(NKMOfficeRoomTemplet templet)
		{
			if (!templet.IsFacility)
			{
				Debug.LogError("Logic Error! tried open non-facility room as facility");
				return null;
			}
			NKMAssetName nkmassetName = NKMAssetName.ParseBundleName(templet.FacilityPrefab, templet.FacilityPrefab);
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>(nkmassetName, false, null);
			if (((nkcassetInstanceData != null) ? nkcassetInstanceData.m_Instant : null) == null)
			{
				Debug.LogError(string.Format("NKCUIOfficeFacility : {0} not found!", nkmassetName));
				return null;
			}
			NKCOfficeFacility component = nkcassetInstanceData.m_Instant.GetComponent<NKCOfficeFacility>();
			if (component == null)
			{
				Debug.LogError(string.Format("NKCUIOfficeFacility : {0} don't have NKCUIOfficeFacility component!", nkmassetName));
				return null;
			}
			component.Init();
			return component;
		}

		// Token: 0x06005347 RID: 21319 RVA: 0x001962E0 File Offset: 0x001944E0
		public virtual void Init()
		{
			base.Init(null);
			this.dOnSelectFuniture = null;
			NKCOfficeFloor floor = this.m_Floor;
			if (floor != null)
			{
				floor.Init(BuildingFloor.Floor, Color.white, Color.white, Color.white, Color.white, null, null, null);
			}
			NKCOfficeFloor floorTile = this.m_FloorTile;
			if (floorTile != null)
			{
				floorTile.Init(BuildingFloor.Tile, Color.white, Color.white, Color.white, Color.white, null, null, null);
			}
			NKCOfficeWall leftWall = this.m_LeftWall;
			if (leftWall != null)
			{
				leftWall.Init(BuildingFloor.LeftWall, Color.white, Color.white, Color.white, Color.white, null, null, null);
			}
			NKCOfficeWall rightWall = this.m_RightWall;
			if (rightWall != null)
			{
				rightWall.Init(BuildingFloor.RightWall, Color.white, Color.white, Color.white, Color.white, null, null, null);
			}
			this.m_lstFunitures = new List<NKCOfficeFuniture>(base.gameObject.GetComponentsInChildren<NKCOfficeFuniture>(true));
			NKCOfficeFloor floor2 = this.m_Floor;
			if (((floor2 != null) ? floor2.m_rtFunitureRoot : null) != null)
			{
				this.m_lstFloorFunitures = new List<NKCOfficeFuniture>(this.m_Floor.m_rtFunitureRoot.GetComponentsInChildren<NKCOfficeFuniture>(true));
			}
			else
			{
				this.m_lstFloorFunitures = new List<NKCOfficeFuniture>();
			}
			foreach (NKCOfficeFuniture nkcofficeFuniture in this.m_lstFunitures)
			{
				if (!(nkcofficeFuniture == null))
				{
					nkcofficeFuniture.Init();
					nkcofficeFuniture.SetShowTile(false);
					nkcofficeFuniture.dOnBeginDragFuniture = new NKCOfficeFuniture.OnFunitureDragEvent(this.OnBeginDrag);
					nkcofficeFuniture.dOnDragFuniture = new NKCOfficeFuniture.OnFunitureDragEvent(this.OnDrag);
					nkcofficeFuniture.dOnEndDragFuniture = new NKCOfficeFuniture.OnFunitureDragEvent(this.OnEndDrag);
					if (nkcofficeFuniture.m_eFunitureType == InteriorTarget.Wall)
					{
						nkcofficeFuniture.SetFunitureBoxRaycast(false);
					}
				}
			}
			foreach (NKCOfficeCharacterNPC nkcofficeCharacterNPC in this.m_lstNPCCharacters)
			{
				nkcofficeCharacterNPC.Init(this);
				nkcofficeCharacterNPC.StartAI();
			}
			this.SetBackground();
			this.UpdateAlarm();
			this.UpdateFloorMap();
		}

		// Token: 0x06005348 RID: 21320 RVA: 0x001964F4 File Offset: 0x001946F4
		public override void CleanUp()
		{
			base.CleanUp();
		}

		// Token: 0x06005349 RID: 21321 RVA: 0x001964FC File Offset: 0x001946FC
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
			foreach (NKCOfficeFuniture nkcofficeFuniture in this.m_lstFloorFunitures)
			{
				NKCOfficeBuildingBase.FloorRect floorRect = base.CalculateFloorRect(nkcofficeFuniture.m_rtFloor);
				for (int k = floorRect.x; k < floorRect.x + floorRect.sizeX; k++)
				{
					for (int l = floorRect.y; l < floorRect.y + floorRect.sizeY; l++)
					{
						this.m_FloorMap[k, l] = 1L;
					}
				}
			}
		}

		// Token: 0x0600534A RID: 21322 RVA: 0x001965F8 File Offset: 0x001947F8
		public virtual void UpdateAlarm()
		{
		}

		// Token: 0x0600534B RID: 21323 RVA: 0x001965FC File Offset: 0x001947FC
		private void SetBackground()
		{
			if (this.m_objFacilityBackground != null)
			{
				this.m_objBackground = this.m_objFacilityBackground;
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
			base.SetBackgroundSize();
		}

		// Token: 0x040042C6 RID: 17094
		[Header("NPC 캐릭터들")]
		public List<NKCOfficeCharacterNPC> m_lstNPCCharacters;

		// Token: 0x040042C7 RID: 17095
		[Header("배경")]
		public GameObject m_objFacilityBackground;

		// Token: 0x040042C8 RID: 17096
		private List<NKCOfficeFuniture> m_lstFunitures;

		// Token: 0x040042C9 RID: 17097
		private List<NKCOfficeFuniture> m_lstFloorFunitures;
	}
}
