using System;
using UnityEngine;
using UnityEngine.Video;

namespace NKC
{
	// Token: 0x02000768 RID: 1896
	[RequireComponent(typeof(Camera))]
	public class NKCUIComVideoCamera : NKCUIComVideoPlayer
	{
		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06004BA9 RID: 19369 RVA: 0x0016A775 File Offset: 0x00168975
		// (set) Token: 0x06004BAA RID: 19370 RVA: 0x0016A782 File Offset: 0x00168982
		public VideoRenderMode renderMode
		{
			get
			{
				return base.VideoPlayer.renderMode;
			}
			set
			{
				if (value <= VideoRenderMode.CameraNearPlane)
				{
					base.VideoPlayer.renderMode = value;
					return;
				}
				Debug.Log("not allowed VideoRendertype");
			}
		}

		// Token: 0x06004BAB RID: 19371 RVA: 0x0016A79F File Offset: 0x0016899F
		private void Awake()
		{
			base.VideoPlayer.targetCamera = base.GetComponent<Camera>();
			base.VideoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
		}

		// Token: 0x06004BAC RID: 19372 RVA: 0x0016A7BE File Offset: 0x001689BE
		protected override bool CanPlayVideo()
		{
			return (!(base.VideoPlayer.targetCamera != null) || base.VideoPlayer.targetCamera.isActiveAndEnabled) && base.CanPlayVideo();
		}

		// Token: 0x06004BAD RID: 19373 RVA: 0x0016A7ED File Offset: 0x001689ED
		private void OnDestroy()
		{
			this.CleanUp();
		}
	}
}
