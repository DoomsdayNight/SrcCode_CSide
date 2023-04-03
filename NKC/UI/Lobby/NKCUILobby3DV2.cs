using System;
using System.Collections.Generic;
using ClientPacket.User;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NKC.UI.Lobby
{
	// Token: 0x02000C07 RID: 3079
	public class NKCUILobby3DV2 : MonoBehaviour
	{
		// Token: 0x06008E97 RID: 36503 RVA: 0x00308158 File Offset: 0x00306358
		public void Init()
		{
			this.m_vOriginalPos = this.m_rtMenuRoot.position;
			this.AdjustPositionByScreenRatio();
			for (int i = 0; i < this.m_lstCvLobbyUnit.Count; i++)
			{
				NKCUICharacterView nkcuicharacterView = this.m_lstCvLobbyUnit[i];
				if (nkcuicharacterView != null)
				{
					nkcuicharacterView.Init(new NKCUICharacterView.OnDragEvent(this.OnDrag), null);
				}
			}
			NKCUIPointExchangeLobby pointExchangeLobby = this.m_pointExchangeLobby;
			if (pointExchangeLobby == null)
			{
				return;
			}
			pointExchangeLobby.Init();
		}

		// Token: 0x06008E98 RID: 36504 RVA: 0x003081C6 File Offset: 0x003063C6
		public void AdjustPositionByScreenRatio()
		{
			this.SetRelativePostionByCamera();
			NKCCamera.RescaleRectToCameraFrustrum(this.m_rtBackground, NKCCamera.GetCamera(), new Vector2(200f, 200f), -1000f, NKCCamera.FitMode.FitAuto, NKCCamera.ScaleMode.Scale);
		}

		// Token: 0x06008E99 RID: 36505 RVA: 0x003081F4 File Offset: 0x003063F4
		public void CleanUp()
		{
			for (int i = 0; i < this.m_lstCvLobbyUnit.Count; i++)
			{
				NKCUICharacterView nkcuicharacterView = this.m_lstCvLobbyUnit[i];
				if (nkcuicharacterView != null)
				{
					nkcuicharacterView.CleanUp();
				}
			}
			if (this.m_objBackground != null)
			{
				UnityEngine.Object.Destroy(this.m_objBackground);
			}
		}

		// Token: 0x06008E9A RID: 36506 RVA: 0x00308247 File Offset: 0x00306447
		public void SetData(NKMUserData userData)
		{
			if (userData != null)
			{
				this.SetUnitIllusts(userData);
				this.SetBackground(userData);
			}
			else
			{
				this.SetBackground(null);
			}
			NKCUIPointExchangeLobby pointExchangeLobby = this.m_pointExchangeLobby;
			if (pointExchangeLobby == null)
			{
				return;
			}
			pointExchangeLobby.SetData();
		}

		// Token: 0x06008E9B RID: 36507 RVA: 0x00308274 File Offset: 0x00306474
		private void SetUnitIllusts(NKMUserData userData)
		{
			for (int i = 0; i < this.m_lstCvLobbyUnit.Count; i++)
			{
				NKCUICharacterView nkcuicharacterView = this.m_lstCvLobbyUnit[i];
				if (!(nkcuicharacterView == null))
				{
					NKMBackgroundUnitInfo backgroundUnitInfo = userData.GetBackgroundUnitInfo(i);
					if (backgroundUnitInfo == null)
					{
						nkcuicharacterView.CloseCharacterIllust();
					}
					else
					{
						nkcuicharacterView.SetCharacterIllust(backgroundUnitInfo, false, true);
					}
				}
			}
		}

		// Token: 0x06008E9C RID: 36508 RVA: 0x003082CC File Offset: 0x003064CC
		private void SetBackground(NKMUserData userData)
		{
			NKCBackgroundTemplet nkcbackgroundTemplet = null;
			if (userData != null)
			{
				nkcbackgroundTemplet = NKCBackgroundTemplet.Find(userData.backGroundInfo.backgroundItemId);
			}
			if (nkcbackgroundTemplet == null)
			{
				nkcbackgroundTemplet = NKCBackgroundTemplet.Find(9001);
			}
			if (nkcbackgroundTemplet == null)
			{
				this.m_bUseBGDrag = true;
				this.SetBackground(NKMAssetName.ParseBundleName("AB_UI_BG_SPRITE_CITY_NIGHT", "AB_UI_BG_SPRITE_CITY_NIGHT"), true);
				return;
			}
			this.m_bUseBGDrag = nkcbackgroundTemplet.m_bBackground_CamMove;
			this.SetBackground(NKMAssetName.ParseBundleName(nkcbackgroundTemplet.m_Background_Prefab, nkcbackgroundTemplet.m_Background_Prefab), nkcbackgroundTemplet.m_bBackground_CamMove);
		}

		// Token: 0x06008E9D RID: 36509 RVA: 0x00308348 File Offset: 0x00306548
		private void SetBackground(NKMAssetName assetName, bool bCamMove)
		{
			if (this.m_objBackground != null)
			{
				UnityEngine.Object.Destroy(this.m_objBackground);
			}
			NKCAssetResourceData nkcassetResourceData = NKCAssetResourceManager.OpenResource<GameObject>(assetName, false);
			if (nkcassetResourceData != null && nkcassetResourceData.GetAsset<GameObject>() != null)
			{
				this.m_objBackground = UnityEngine.Object.Instantiate<GameObject>(nkcassetResourceData.GetAsset<GameObject>());
				this.m_objBackground.transform.SetParent(this.m_rtBackgroundRoot);
				Transform transform = this.m_objBackground.transform.Find("Stretch/Background");
				if (transform != null)
				{
					this.m_rtBackground = transform.GetComponent<RectTransform>();
					Vector2 cameraMoveRectSize = bCamMove ? new Vector2(200f, 200f) : Vector2.zero;
					NKCCamera.SetPos(0f, 0f, -1000f, true, false);
					NKCCamera.RescaleRectToCameraFrustrum(this.m_rtBackground, NKCCamera.GetCamera(), cameraMoveRectSize, -1000f, NKCCamera.FitMode.FitAuto, NKCCamera.ScaleMode.Scale);
					EventTrigger eventTrigger = transform.GetComponent<EventTrigger>();
					if (eventTrigger == null)
					{
						eventTrigger = transform.gameObject.AddComponent<EventTrigger>();
					}
					eventTrigger.triggers.Clear();
					EventTrigger.Entry entry = new EventTrigger.Entry();
					entry.eventID = EventTriggerType.Drag;
					entry.callback.AddListener(delegate(BaseEventData eventData)
					{
						PointerEventData cPointerEventData = eventData as PointerEventData;
						this.OnDrag(cPointerEventData);
					});
					eventTrigger.triggers.Add(entry);
					entry = new EventTrigger.Entry();
					entry.eventID = EventTriggerType.PointerClick;
					entry.callback.AddListener(new UnityAction<BaseEventData>(this.OnTouch));
					eventTrigger.triggers.Add(entry);
				}
			}
			if (nkcassetResourceData != null)
			{
				NKCAssetResourceManager.CloseResource(nkcassetResourceData);
			}
		}

		// Token: 0x06008E9E RID: 36510 RVA: 0x003084C4 File Offset: 0x003066C4
		private void OnDrag(PointerEventData cPointerEventData)
		{
			if (!this.m_bUseBGDrag)
			{
				return;
			}
			float num = NKCCamera.GetPosNowX(false) - cPointerEventData.delta.x * 10f;
			float num2 = NKCCamera.GetPosNowY(false) - cPointerEventData.delta.y * 10f;
			num = Mathf.Clamp(num, -50f, 50f);
			num2 = Mathf.Clamp(num2, -50f, 50f);
			NKCCamera.TrackingPos(1f, num, num2, -1f);
		}

		// Token: 0x06008E9F RID: 36511 RVA: 0x0030853F File Offset: 0x0030673F
		public void SetTouchCallback(UnityAction callback)
		{
			this.TouchCallback = callback;
		}

		// Token: 0x06008EA0 RID: 36512 RVA: 0x00308548 File Offset: 0x00306748
		private void OnTouch(BaseEventData cBaseEventData)
		{
			UnityAction touchCallback = this.TouchCallback;
			if (touchCallback == null)
			{
				return;
			}
			touchCallback();
		}

		// Token: 0x06008EA1 RID: 36513 RVA: 0x0030855A File Offset: 0x0030675A
		public bool CameraTracking()
		{
			return this.m_bUseBGDrag;
		}

		// Token: 0x06008EA2 RID: 36514 RVA: 0x00308562 File Offset: 0x00306762
		private void Update()
		{
			this.m_rtMenuRoot.position = NKCCamera.GetCamera().transform.position + this.m_vRelativePosToCamera;
		}

		// Token: 0x06008EA3 RID: 36515 RVA: 0x0030858C File Offset: 0x0030678C
		public void SetRelativePostionByCamera()
		{
			float fieldOfView = NKCCamera.GetCamera().fieldOfView;
			float num = this.m_rtMenuRoot.GetWidth() * this.m_rtMenuRoot.localScale.x;
			float num2 = num * Mathf.Sin(this.m_rtMenuRoot.localEulerAngles.y * 0.017453292f);
			float num3 = 1000f - num2;
			float num4 = Mathf.Tan(fieldOfView * 0.017453292f * 0.5f) * num3 * 2f;
			float num5 = NKCCamera.GetScreenRatio(false) * num4;
			float num6 = num5 / (float)Screen.width;
			float num7 = num * Mathf.Cos(this.m_rtMenuRoot.localEulerAngles.y * 0.017453292f);
			float num8 = num5 * 0.5f - (num7 * (1f - this.m_rtMenuRoot.pivot.x) + this.m_fXGapFromRight * num6);
			if (this.m_bUseSafeRectGap)
			{
				float x = Screen.safeArea.x;
				float width = Screen.safeArea.width;
				float num9 = (float)Screen.width - (x + width);
				num8 -= num9 * num6;
			}
			this.m_vRelativePosToCamera = this.m_vOriginalPos - new Vector3(0f, 0f, -1000f);
			this.m_vRelativePosToCamera.x = num8;
		}

		// Token: 0x04007BAB RID: 31659
		public RectTransform m_rtRoot;

		// Token: 0x04007BAC RID: 31660
		public RectTransform m_rtBackgroundRoot;

		// Token: 0x04007BAD RID: 31661
		public List<NKCUICharacterView> m_lstCvLobbyUnit;

		// Token: 0x04007BAE RID: 31662
		public RectTransform m_rtMenuRoot;

		// Token: 0x04007BAF RID: 31663
		public CanvasGroup m_MenuCanvasGroup;

		// Token: 0x04007BB0 RID: 31664
		public NKCUIPointExchangeLobby m_pointExchangeLobby;

		// Token: 0x04007BB1 RID: 31665
		[Header("������ ����")]
		public bool m_bUseSafeRectGap = true;

		// Token: 0x04007BB2 RID: 31666
		public float m_fXGapFromRight = 50f;

		// Token: 0x04007BB3 RID: 31667
		private UnityAction TouchCallback;

		// Token: 0x04007BB4 RID: 31668
		private GameObject m_objBackground;

		// Token: 0x04007BB5 RID: 31669
		private RectTransform m_rtBackground;

		// Token: 0x04007BB6 RID: 31670
		private bool m_bUseBGDrag;

		// Token: 0x04007BB7 RID: 31671
		private Vector3 m_vOriginalPos;

		// Token: 0x04007BB8 RID: 31672
		private Vector3 m_vRelativePosToCamera;
	}
}
