using System;
using UnityEngine;

namespace NKC
{
	// Token: 0x020007C0 RID: 1984
	public class NKCUIFollowCamera : MonoBehaviour
	{
		// Token: 0x06004E9D RID: 20125 RVA: 0x0017B554 File Offset: 0x00179754
		public void Init(RectTransform rt)
		{
			float num = (float)Screen.width / (float)Screen.height;
			if (this.m_fScreenRateActiveValue > num)
			{
				this.m_rtBackground.sizeDelta = rt.sizeDelta + this.m_vecAddValue;
				this.m_rtBackground.localPosition = rt.localPosition;
				NKCCamera.RescaleRectToCameraFrustrum(this.m_rtBackground, NKCCamera.GetCamera(), Vector2.zero, -this.m_fDistanceToCameraZ, NKCCamera.FitMode.FitToWidth, NKCCamera.ScaleMode.Scale);
			}
			Debug.Log("check ScreenRatio : " + num.ToString());
			this.m_bFollowCam = NKCCamera.GetTrackingPos().GetPause();
			this.m_distanceToCamera = new Vector3(0f, 0f, this.m_fDistanceToCameraZ);
		}

		// Token: 0x06004E9E RID: 20126 RVA: 0x0017B604 File Offset: 0x00179804
		private void Update()
		{
			if (this.m_bFollowCam)
			{
				base.transform.position = NKCCamera.GetCamera().transform.position + this.m_distanceToCamera;
			}
		}

		// Token: 0x04003E46 RID: 15942
		[Header("해당 (화면 비율)값 미만인 경우 동작합니다.")]
		public float m_fScreenRateActiveValue = 2f;

		// Token: 0x04003E47 RID: 15943
		[Header("카메라와의 거리 Z축")]
		public float m_fDistanceToCameraZ = 1100f;

		// Token: 0x04003E48 RID: 15944
		[Header("scale이 적용될 타겟")]
		public RectTransform m_rtBackground;

		// Token: 0x04003E49 RID: 15945
		[Header("scale이 적용될 타겟에 추가 사이즈")]
		public Vector2 m_vecAddValue;

		// Token: 0x04003E4A RID: 15946
		private Vector3 m_distanceToCamera;

		// Token: 0x04003E4B RID: 15947
		private bool m_bFollowCam;
	}
}
