using System;
using UnityEngine;

namespace NKC.UI.Office
{
	// Token: 0x02000AF3 RID: 2803
	public class NKCUIOfficeMinimap : MonoBehaviour
	{
		// Token: 0x06007E5A RID: 32346 RVA: 0x002A6380 File Offset: 0x002A4580
		public void Init()
		{
			Transform background = this.m_background;
			if (background != null)
			{
				background.SetParent(NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIMidCanvas));
			}
			this.CalcCamMoveBoundRect();
			NKCUIOfficeMinimapFacility nkcuiminimapFacility = this.m_NKCUIMinimapFacility;
			nkcuiminimapFacility.m_dOnScrollCamMove = (NKCUIOfficeMinimapFacility.OnScroll)Delegate.Combine(nkcuiminimapFacility.m_dOnScrollCamMove, new NKCUIOfficeMinimapFacility.OnScroll(this.MoveCameraByScroll));
			NKCUIOfficeMinimapRoom nkcuiminimapRoom = this.m_NKCUIMinimapRoom;
			nkcuiminimapRoom.m_dOnScrollCamMove = (NKCUIOfficeMinimapRoom.OnScroll)Delegate.Combine(nkcuiminimapRoom.m_dOnScrollCamMove, new NKCUIOfficeMinimapRoom.OnScroll(this.MoveCameraByScroll));
			this.m_NKCUIMinimapFacility.Init();
			this.m_NKCUIMinimapRoom.Init();
		}

		// Token: 0x06007E5B RID: 32347 RVA: 0x002A6410 File Offset: 0x002A4610
		public void SetAcive(bool value)
		{
			base.gameObject.SetActive(value);
			NKCUtil.SetGameobjectActive(this.m_background.gameObject, value);
			if (value)
			{
				NKCCamera.GetCamera().orthographic = false;
				if (this.m_NKCUIMinimapFacility.gameObject.activeSelf)
				{
					this.m_NKCUIMinimapFacility.UpdateRedDotAll();
				}
				if (this.m_NKCUIMinimapRoom.gameObject.activeSelf)
				{
					this.m_NKCUIMinimapRoom.UpdateRedDotAll();
				}
			}
		}

		// Token: 0x06007E5C RID: 32348 RVA: 0x002A6482 File Offset: 0x002A4682
		public void SetActiveFacility(bool value)
		{
			this.m_NKCUIMinimapFacility.SetActive(value);
			this.m_NKCUIMinimapRoom.SetActive(!value);
		}

		// Token: 0x06007E5D RID: 32349 RVA: 0x002A649F File Offset: 0x002A469F
		public void SetActiveRoom(bool value)
		{
			this.m_NKCUIMinimapFacility.SetActive(!value);
			this.m_NKCUIMinimapRoom.SetActive(value);
		}

		// Token: 0x06007E5E RID: 32350 RVA: 0x002A64BC File Offset: 0x002A46BC
		public void CalcCamMoveBoundRect()
		{
			NKCCamera.RescaleRectToCameraFrustrum(this.m_rtBgImage, NKCCamera.GetCamera(), new Vector2(this.m_fBgScrollRange, 0f), NKCCamera.GetCamera().transform.position.z, NKCCamera.FitMode.FitAuto, NKCCamera.ScaleMode.RectSize);
			this.m_rtCamBoundRect = NKCCamera.GetCameraBoundRect(this.m_rtBgImage, NKCCamera.GetCamera().transform.position.z);
		}

		// Token: 0x06007E5F RID: 32351 RVA: 0x002A6524 File Offset: 0x002A4724
		public void Release()
		{
			this.m_background.SetParent(base.transform);
			NKCUIOfficeMinimapFacility nkcuiminimapFacility = this.m_NKCUIMinimapFacility;
			nkcuiminimapFacility.m_dOnScrollCamMove = (NKCUIOfficeMinimapFacility.OnScroll)Delegate.Remove(nkcuiminimapFacility.m_dOnScrollCamMove, new NKCUIOfficeMinimapFacility.OnScroll(this.MoveCameraByScroll));
			NKCUIOfficeMinimapRoom nkcuiminimapRoom = this.m_NKCUIMinimapRoom;
			nkcuiminimapRoom.m_dOnScrollCamMove = (NKCUIOfficeMinimapRoom.OnScroll)Delegate.Remove(nkcuiminimapRoom.m_dOnScrollCamMove, new NKCUIOfficeMinimapRoom.OnScroll(this.MoveCameraByScroll));
		}

		// Token: 0x06007E60 RID: 32352 RVA: 0x002A6590 File Offset: 0x002A4790
		private void MoveCameraByScroll(Vector2 scrollNormalizedPosition)
		{
			float x = Mathf.Lerp(this.m_rtCamBoundRect.xMin, this.m_rtCamBoundRect.xMax, scrollNormalizedPosition.x);
			Vector3 position = NKCCamera.GetCamera().transform.position;
			position.x = x;
			NKCCamera.SetPos(position, true, true);
		}

		// Token: 0x04006B28 RID: 27432
		public float m_fBgScrollRange;

		// Token: 0x04006B29 RID: 27433
		public Transform m_background;

		// Token: 0x04006B2A RID: 27434
		public RectTransform m_rtBgImage;

		// Token: 0x04006B2B RID: 27435
		public NKCUIOfficeMinimapFacility m_NKCUIMinimapFacility;

		// Token: 0x04006B2C RID: 27436
		public NKCUIOfficeMinimapRoom m_NKCUIMinimapRoom;

		// Token: 0x04006B2D RID: 27437
		private Rect m_rtCamBoundRect;
	}
}
