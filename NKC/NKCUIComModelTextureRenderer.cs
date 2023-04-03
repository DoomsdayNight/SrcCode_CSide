using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x0200075A RID: 1882
	public class NKCUIComModelTextureRenderer : MonoBehaviour
	{
		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x06004B27 RID: 19239 RVA: 0x00168318 File Offset: 0x00166518
		private Camera RenderCamera
		{
			get
			{
				if (this.m_RenderCamera == null)
				{
					this.m_RenderCamera = NKCScenManager.GetScenManager().TextureCamera;
				}
				return this.m_RenderCamera;
			}
		}

		// Token: 0x06004B28 RID: 19240 RVA: 0x0016833E File Offset: 0x0016653E
		private void Awake()
		{
			this.m_rawImage.color = new Color(1f, 1f, 1f, 1f);
		}

		// Token: 0x06004B29 RID: 19241 RVA: 0x00168364 File Offset: 0x00166564
		public void PrepareTexture(Material mat)
		{
			if (this.m_Texture == null)
			{
				this.BuildTexture((int)this.m_rtImage.GetWidth(), (int)this.m_rtImage.GetHeight(), mat);
				this.m_RenderBound = new Bounds
				{
					size = new Vector3(this.m_rtImage.GetWidth(), this.m_rtImage.GetHeight(), 1f),
					center = this.m_rtImage.position
				};
			}
		}

		// Token: 0x06004B2A RID: 19242 RVA: 0x001683E8 File Offset: 0x001665E8
		public void PrepareTexture(Material mat, int width, int height)
		{
			if (this.m_Texture == null)
			{
				this.BuildTexture(width, height, mat);
				this.m_RenderBound = new Bounds
				{
					size = new Vector3((float)width, (float)height, 1f),
					center = this.m_rtImage.position
				};
			}
		}

		// Token: 0x06004B2B RID: 19243 RVA: 0x00168444 File Offset: 0x00166644
		private void BuildTexture(int textureResX, int textureResY, Material mat)
		{
			this.CleanUp();
			if (mat != null)
			{
				this.m_rawImage.material = mat;
			}
			this.m_Texture = new RenderTexture(textureResX, textureResY, 0, RenderTextureFormat.ARGB32);
			this.m_Texture.wrapMode = TextureWrapMode.Clamp;
			this.m_Texture.antiAliasing = 1;
			this.m_Texture.filterMode = FilterMode.Bilinear;
			this.m_Texture.anisoLevel = 0;
			this.m_Texture.Create();
			this.m_rawImage.texture = this.m_Texture;
			this.m_rawImage.SetAllDirty();
		}

		// Token: 0x06004B2C RID: 19244 RVA: 0x001684D4 File Offset: 0x001666D4
		public void CleanUp()
		{
			if (this.m_rawImage != null)
			{
				this.m_rawImage.texture = null;
			}
			if (this.m_Texture != null)
			{
				this.m_Texture.Release();
				UnityEngine.Object.DestroyImmediate(this.m_Texture);
				this.m_Texture = null;
			}
		}

		// Token: 0x06004B2D RID: 19245 RVA: 0x00168526 File Offset: 0x00166726
		private void Update()
		{
			this.TextureCapture(this.m_RenderBound, ref this.m_Texture);
		}

		// Token: 0x06004B2E RID: 19246 RVA: 0x0016853C File Offset: 0x0016673C
		public void TextureCapture(Bounds bound, ref RenderTexture Texture)
		{
			if (this.m_Texture == null)
			{
				return;
			}
			if (bound.size.x == 0f || bound.size.y == 0f)
			{
				return;
			}
			this.m_rtImage.GetWorldCorners(this.m_WorldCornerPosArray);
			NKCCamera.FitCameraToWorldRect(this.RenderCamera, this.m_WorldCornerPosArray);
			this.RenderCamera.targetTexture = Texture;
			int layer = this.m_rtImage.gameObject.layer;
			NKCUtil.SetLayer(this.m_rtImage, 31);
			this.RenderCamera.Render();
			NKCUtil.SetLayer(this.m_rtImage, layer);
			this.RenderCamera.targetTexture = null;
		}

		// Token: 0x06004B2F RID: 19247 RVA: 0x001685EF File Offset: 0x001667EF
		public void SetMaterial(Material mat)
		{
			if (this.m_rawImage != null)
			{
				this.m_rawImage.material = mat;
			}
		}

		// Token: 0x06004B30 RID: 19248 RVA: 0x0016860B File Offset: 0x0016680B
		public Material GetMaterial()
		{
			if (this.m_rawImage != null)
			{
				return this.m_rawImage.material;
			}
			return null;
		}

		// Token: 0x06004B31 RID: 19249 RVA: 0x00168628 File Offset: 0x00166828
		public void SetColor(Color color)
		{
			if (this.m_rawImage != null)
			{
				this.m_rawImage.color = color;
			}
		}

		// Token: 0x06004B32 RID: 19250 RVA: 0x00168644 File Offset: 0x00166844
		public Color GetColor()
		{
			if (this.m_rawImage != null)
			{
				return this.m_rawImage.color;
			}
			return Color.white;
		}

		// Token: 0x06004B33 RID: 19251 RVA: 0x00168668 File Offset: 0x00166868
		private void ChangeLayer(Transform target, int layer)
		{
			foreach (object obj in target)
			{
				Transform transform = (Transform)obj;
				transform.gameObject.layer = layer;
				this.ChangeLayer(transform, layer);
			}
		}

		// Token: 0x040039CF RID: 14799
		public RawImage m_rawImage;

		// Token: 0x040039D0 RID: 14800
		public RectTransform m_rtImage;

		// Token: 0x040039D1 RID: 14801
		private Bounds m_RenderBound;

		// Token: 0x040039D2 RID: 14802
		private RenderTexture m_Texture;

		// Token: 0x040039D3 RID: 14803
		public Camera m_RenderCamera;

		// Token: 0x040039D4 RID: 14804
		private Vector3[] m_WorldCornerPosArray = new Vector3[4];
	}
}
