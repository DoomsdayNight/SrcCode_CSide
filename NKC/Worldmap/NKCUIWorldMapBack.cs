using System;
using System.Collections.Generic;
using ClientPacket.Raid;
using ClientPacket.WorldMap;
using NKM;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NKC.UI.Worldmap
{
	// Token: 0x02000ABE RID: 2750
	public class NKCUIWorldMapBack : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
	{
		// Token: 0x06007ACA RID: 31434 RVA: 0x0028EA0E File Offset: 0x0028CC0E
		public void SetEnableDrag(bool bSet)
		{
			this.m_bEnableDrag = bSet;
		}

		// Token: 0x06007ACB RID: 31435 RVA: 0x0028EA18 File Offset: 0x0028CC18
		public void Init(NKCUIWorldMapCity.OnClickCity onSelectCity, NKCUIWorldMapCityEventPin.OnClickEvent onSelectEvent)
		{
			base.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			foreach (NKCUIWorldMapCity nkcuiworldMapCity in this.m_lstCity)
			{
				NKMWorldMapCityTemplet cityTemplet = NKMWorldMapManager.GetCityTemplet(nkcuiworldMapCity.m_CityID);
				if (cityTemplet == null)
				{
					Debug.LogError(string.Format("CityID {0} does not exist!", nkcuiworldMapCity.m_CityID));
					NKCUtil.SetGameobjectActive(nkcuiworldMapCity, false);
				}
				else
				{
					NKCUtil.SetGameobjectActive(nkcuiworldMapCity, true);
					nkcuiworldMapCity.Init(onSelectCity, onSelectEvent);
					this.m_dicCity.Add(cityTemplet.m_ID, nkcuiworldMapCity);
				}
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007ACC RID: 31436 RVA: 0x0028EAD4 File Offset: 0x0028CCD4
		public void SetData(NKMWorldMapData worldMapData)
		{
			base.gameObject.SetActive(true);
			foreach (KeyValuePair<int, NKCUIWorldMapCity> keyValuePair in this.m_dicCity)
			{
				keyValuePair.Value.SetData(worldMapData.GetCityData(keyValuePair.Key));
			}
			NKCCamera.StopTrackingCamera();
			NKCCamera.GetTrackingPos().SetNowValue(0f, 0f, this.m_fCameraZPos);
		}

		// Token: 0x06007ACD RID: 31437 RVA: 0x0028EB68 File Offset: 0x0028CD68
		public void PlayPinSDAniByCityID(int cityID, NKCASUIUnitIllust.eAnimation eAnim, bool bLoop = false)
		{
			NKCUIWorldMapCity nkcuiworldMapCity = null;
			if (this.m_dicCity.TryGetValue(cityID, out nkcuiworldMapCity) && nkcuiworldMapCity != null)
			{
				nkcuiworldMapCity.PlaySDAnim(eAnim, bLoop);
			}
		}

		// Token: 0x06007ACE RID: 31438 RVA: 0x0028EB98 File Offset: 0x0028CD98
		public void CleanUpEventPinSpineSD(int cityID)
		{
			NKCUIWorldMapCity nkcuiworldMapCity = null;
			if (this.m_dicCity.TryGetValue(cityID, out nkcuiworldMapCity) && nkcuiworldMapCity != null)
			{
				nkcuiworldMapCity.CleanUpEventPinSpineSD();
			}
		}

		// Token: 0x06007ACF RID: 31439 RVA: 0x0028EBC8 File Offset: 0x0028CDC8
		public Vector3 GetPinSDPos(int cityID)
		{
			NKCUIWorldMapCity nkcuiworldMapCity = null;
			if (this.m_dicCity.TryGetValue(cityID, out nkcuiworldMapCity) && nkcuiworldMapCity != null)
			{
				return nkcuiworldMapCity.GetPinSDPos() + base.transform.localPosition;
			}
			return new Vector3(0f, 0f, 0f);
		}

		// Token: 0x06007AD0 RID: 31440 RVA: 0x0028EC1C File Offset: 0x0028CE1C
		public void UpdateCity(int cityID, NKMWorldMapCityData cityData)
		{
			NKCUIWorldMapCity nkcuiworldMapCity;
			if (!this.m_dicCity.TryGetValue(cityID, out nkcuiworldMapCity))
			{
				Debug.LogError("CityUI for city " + cityID.ToString() + " Not Found, maybe city does not exist in CityTemplet!");
				return;
			}
			if (!nkcuiworldMapCity.SetData(cityData))
			{
				Debug.LogError("City Icon SetData Failed!!!");
			}
		}

		// Token: 0x06007AD1 RID: 31441 RVA: 0x0028EC68 File Offset: 0x0028CE68
		public void CityEventSpawned(int cityID)
		{
			NKCUIWorldMapCity nkcuiworldMapCity;
			if (!this.m_dicCity.TryGetValue(cityID, out nkcuiworldMapCity))
			{
				Debug.LogError("CityUI for city " + cityID.ToString() + " Not Found, maybe city does not exist in CityTemplet!");
				return;
			}
		}

		// Token: 0x06007AD2 RID: 31442 RVA: 0x0028ECA4 File Offset: 0x0028CEA4
		public void UpdateCityRaidData(NKMRaidDetailData raidDetailData)
		{
			NKCUIWorldMapCity nkcuiworldMapCity;
			if (this.m_dicCity.TryGetValue(raidDetailData.cityID, out nkcuiworldMapCity))
			{
				nkcuiworldMapCity.UpdateCityRaidData();
			}
		}

		// Token: 0x06007AD3 RID: 31443 RVA: 0x0028ECCC File Offset: 0x0028CECC
		public void OnBeginDrag(PointerEventData data)
		{
		}

		// Token: 0x06007AD4 RID: 31444 RVA: 0x0028ECD0 File Offset: 0x0028CED0
		public void OnDrag(PointerEventData pointData)
		{
			if (!this.m_bEnableDrag)
			{
				return;
			}
			float num = NKCCamera.GetPosNowX(false) - pointData.delta.x * 10f;
			float num2 = NKCCamera.GetPosNowY(false) - pointData.delta.y * 10f;
			num = Mathf.Clamp(num, -this.m_vCameraMoveRange.x, this.m_vCameraMoveRange.x);
			num2 = Mathf.Clamp(num2, -this.m_vCameraMoveRange.y, this.m_vCameraMoveRange.y);
			NKCCamera.TrackingPos(1f, num, num2, -1f);
		}

		// Token: 0x06007AD5 RID: 31445 RVA: 0x0028ED68 File Offset: 0x0028CF68
		private float Rubber(float currentValue, float Limit)
		{
			float num = Mathf.Abs(currentValue);
			return Limit * num / (Limit + num) * Mathf.Sign(currentValue);
		}

		// Token: 0x06007AD6 RID: 31446 RVA: 0x0028ED8A File Offset: 0x0028CF8A
		public void OnEndDrag(PointerEventData data)
		{
		}

		// Token: 0x06007AD7 RID: 31447 RVA: 0x0028ED8C File Offset: 0x0028CF8C
		private void Update()
		{
			if (!this.m_bDragging)
			{
				this.currentCameraPos.x = NKCCamera.GetPosNowX(false);
				this.currentCameraPos.y = NKCCamera.GetPosNowY(false);
			}
		}

		// Token: 0x06007AD8 RID: 31448 RVA: 0x0028EDB8 File Offset: 0x0028CFB8
		public RectTransform GetPinRect(int cityID)
		{
			NKCUIWorldMapCity nkcuiworldMapCity;
			if (this.m_dicCity.TryGetValue(cityID, out nkcuiworldMapCity) && nkcuiworldMapCity != null)
			{
				return nkcuiworldMapCity.GetComponent<RectTransform>();
			}
			return null;
		}

		// Token: 0x0400677D RID: 26493
		public RectTransform m_rtBackground;

		// Token: 0x0400677E RID: 26494
		public Vector2 m_vCameraMoveBound;

		// Token: 0x0400677F RID: 26495
		private Vector3 MainCameraPosition;

		// Token: 0x04006780 RID: 26496
		public float rubberScale = 1f;

		// Token: 0x04006781 RID: 26497
		public float scrollSensitivity = 1f;

		// Token: 0x04006782 RID: 26498
		private Vector2 currentCameraPos;

		// Token: 0x04006783 RID: 26499
		public List<NKCUIWorldMapCity> m_lstCity;

		// Token: 0x04006784 RID: 26500
		private Dictionary<int, NKCUIWorldMapCity> m_dicCity = new Dictionary<int, NKCUIWorldMapCity>();

		// Token: 0x04006785 RID: 26501
		public float m_fCameraZPos = -676f;

		// Token: 0x04006786 RID: 26502
		public Vector2 m_vCameraMoveRange;

		// Token: 0x04006787 RID: 26503
		private bool m_bEnableDrag = true;

		// Token: 0x04006788 RID: 26504
		public Animator m_amtorWorldmapBack;

		// Token: 0x04006789 RID: 26505
		private bool m_bDragging;

		// Token: 0x0400678A RID: 26506
		private Vector2 totalDrag;
	}
}
