using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace NKC
{
	// Token: 0x0200076A RID: 1898
	[RequireComponent(typeof(VideoPlayer))]
	[RequireComponent(typeof(RawImage))]
	public class NKCUIComVideoTexture : NKCUIComVideoPlayer
	{
		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x06004BCD RID: 19405 RVA: 0x0016AEBE File Offset: 0x001690BE
		private RawImage ImgTarget
		{
			get
			{
				if (this.m_rimgTarget == null)
				{
					this.m_rimgTarget = base.GetComponent<RawImage>();
				}
				return this.m_rimgTarget;
			}
		}

		// Token: 0x06004BCE RID: 19406 RVA: 0x0016AEE0 File Offset: 0x001690E0
		private void Awake()
		{
			base.VideoPlayer.renderMode = VideoRenderMode.RenderTexture;
		}

		// Token: 0x06004BCF RID: 19407 RVA: 0x0016AEEE File Offset: 0x001690EE
		private void OnApplicationQuit()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06004BD0 RID: 19408 RVA: 0x0016AEFC File Offset: 0x001690FC
		private void OnDestroy()
		{
			this.CleanUp();
		}

		// Token: 0x06004BD1 RID: 19409 RVA: 0x0016AF04 File Offset: 0x00169104
		public override void Prepare()
		{
			base.Prepare();
			this.PrepareTexture();
		}

		// Token: 0x06004BD2 RID: 19410 RVA: 0x0016AF14 File Offset: 0x00169114
		protected override void OnStateChange(NKCUIComVideoPlayer.VideoState state)
		{
			base.OnStateChange(state);
			switch (state)
			{
			case NKCUIComVideoPlayer.VideoState.Stop:
				this.ImgTarget.enabled = false;
				NKCUtil.SetGameobjectActive(this.m_objFallback, true);
				NKCUtil.SetGameobjectActive(this.m_objLoading, false);
				return;
			case NKCUIComVideoPlayer.VideoState.PreparingPlay:
				this.ImgTarget.enabled = false;
				NKCUtil.SetGameobjectActive(this.m_objFallback, true);
				NKCUtil.SetGameobjectActive(this.m_objLoading, true);
				return;
			case NKCUIComVideoPlayer.VideoState.Playing:
				this.ImgTarget.enabled = true;
				NKCUtil.SetGameobjectActive(this.m_objFallback, false);
				NKCUtil.SetGameobjectActive(this.m_objLoading, false);
				return;
			default:
				return;
			}
		}

		// Token: 0x06004BD3 RID: 19411 RVA: 0x0016AFAC File Offset: 0x001691AC
		public void PrepareTexture()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData != null && !gameOptionData.UseVideoTexture)
			{
				this.ImgTarget.enabled = false;
				NKCUtil.SetGameobjectActive(this.m_objFallback, true);
				NKCUtil.SetGameobjectActive(this.m_objLoading, false);
				return;
			}
			if (this.m_RenderTexture == null)
			{
				RectTransform component = this.ImgTarget.GetComponent<RectTransform>();
				this.m_RenderTexture = new RenderTexture((int)component.GetWidth(), (int)component.GetHeight(), 0);
				this.m_RenderTexture.hideFlags = HideFlags.HideAndDontSave;
			}
			base.VideoPlayer.targetTexture = this.m_RenderTexture;
			this.ImgTarget.texture = this.m_RenderTexture;
		}

		// Token: 0x06004BD4 RID: 19412 RVA: 0x0016B058 File Offset: 0x00169258
		public override void CleanUp()
		{
			base.CleanUp();
			if (this.m_rimgTarget != null)
			{
				this.m_rimgTarget.texture = null;
			}
			if (this.m_RenderTexture != null)
			{
				this.m_RenderTexture.Release();
				UnityEngine.Object.DestroyImmediate(this.m_RenderTexture);
				this.m_RenderTexture = null;
			}
		}

		// Token: 0x04003A4D RID: 14925
		private RenderTexture m_RenderTexture;

		// Token: 0x04003A4E RID: 14926
		private RawImage m_rimgTarget;

		// Token: 0x04003A4F RID: 14927
		[Header("영상 로딩 중일때 보여줄 오브젝트")]
		public GameObject m_objLoading;

		// Token: 0x04003A50 RID: 14928
		[Header("영상이 재생되지 않는 동안(로딩중/재생실패) 보여줄 오브젝트")]
		public GameObject m_objFallback;
	}
}
