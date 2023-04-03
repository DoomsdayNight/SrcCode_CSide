using System;
using System.Collections.Generic;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200062C RID: 1580
	public class NKC2DMotionAfterImage
	{
		// Token: 0x060030D5 RID: 12501 RVA: 0x000F2474 File Offset: 0x000F0674
		public static void Init()
		{
			if (NKC2DMotionAfterImage.DrawMaterial == null)
			{
				NKC2DMotionAfterImage.DrawMaterial = NKCAssetResourceManager.OpenResource<Material>("AB_MATERIAL", "MAT_NKC_AFTERIMAGE", false, null).GetAsset<Material>();
			}
			if (NKC2DMotionAfterImage.s_stkFrame.Count == 0)
			{
				for (int i = 0; i < 20; i++)
				{
					NKC2DMotionAfterImage.s_stkFrame.Push(NKC2DMotionAfterImage.MakeFrame());
				}
			}
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x000F24D1 File Offset: 0x000F06D1
		private static NKC2DMotionAfterImage.MotionAfterImageSingleFrame MakeFrame()
		{
			return new NKC2DMotionAfterImage.MotionAfterImageSingleFrame(NKC2DMotionAfterImage.DrawMaterial);
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x000F24DD File Offset: 0x000F06DD
		private static NKC2DMotionAfterImage.MotionAfterImageSingleFrame GetFrame()
		{
			if (NKC2DMotionAfterImage.s_stkFrame.Count > 0)
			{
				return NKC2DMotionAfterImage.s_stkFrame.Pop();
			}
			return NKC2DMotionAfterImage.MakeFrame();
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x000F24FC File Offset: 0x000F06FC
		private static void ReturnFrame(NKC2DMotionAfterImage.MotionAfterImageSingleFrame frame)
		{
			frame.SetActive(false);
			NKC2DMotionAfterImage.s_stkFrame.Push(frame);
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x000F2510 File Offset: 0x000F0710
		public static void CleanUp()
		{
			foreach (NKC2DMotionAfterImage.MotionAfterImageSingleFrame motionAfterImageSingleFrame in NKC2DMotionAfterImage.s_stkFrame)
			{
				motionAfterImageSingleFrame.Delete();
			}
			NKC2DMotionAfterImage.s_stkFrame.Clear();
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x000F256C File Offset: 0x000F076C
		private NKC2DMotionAfterImage.MotionAfterImageSingleFrame GetOldestFrame()
		{
			if (this.currentFrameIndex >= this.m_lstFrames.Count)
			{
				this.currentFrameIndex = 0;
			}
			NKC2DMotionAfterImage.MotionAfterImageSingleFrame result = this.m_lstFrames[this.currentFrameIndex];
			this.currentFrameIndex++;
			return result;
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x000F25A8 File Offset: 0x000F07A8
		public void AddSingleFrame(Renderer targetRenderer, Color color)
		{
			if (!this.m_bEnable)
			{
				return;
			}
			NKC2DMotionAfterImage.MotionAfterImageSingleFrame motionAfterImageSingleFrame;
			if (this.m_lstFrames.Count < this.maxImageCount)
			{
				motionAfterImageSingleFrame = NKC2DMotionAfterImage.GetFrame();
				this.m_lstFrames.Add(motionAfterImageSingleFrame);
			}
			else
			{
				motionAfterImageSingleFrame = this.GetOldestFrame();
			}
			motionAfterImageSingleFrame.SetFrame(targetRenderer, color);
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x000F25F4 File Offset: 0x000F07F4
		public void Init(int MaxImageCount, Renderer renderTarget)
		{
			this.Clear();
			this.maxImageCount = MaxImageCount;
			this.m_rendererTarget = renderTarget;
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x000F260A File Offset: 0x000F080A
		public void SetColor(Color color)
		{
			this.m_fColor = color;
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x000F2613 File Offset: 0x000F0813
		public void SetColor(float fR, float fG, float fB, float fA)
		{
			this.m_fColor.r = fR;
			this.m_fColor.g = fG;
			this.m_fColor.b = fB;
			this.m_fColor.a = fA;
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x000F2648 File Offset: 0x000F0848
		public void Clear()
		{
			this.m_bEnable = false;
			this.m_bOnProcess = false;
			foreach (NKC2DMotionAfterImage.MotionAfterImageSingleFrame frame in this.m_lstFrames)
			{
				NKC2DMotionAfterImage.ReturnFrame(frame);
			}
			this.m_lstFrames.Clear();
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x000F26B4 File Offset: 0x000F08B4
		public void SetEnable(bool bEnable)
		{
			if (bEnable)
			{
				this.m_bOnProcess = true;
			}
			this.m_bEnable = bEnable;
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x000F26C7 File Offset: 0x000F08C7
		public void StopMotionImage()
		{
			this.SetEnable(false);
			this.Clear();
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x000F26D8 File Offset: 0x000F08D8
		public void Update(float fDeltaTime)
		{
			if (!this.m_bOnProcess)
			{
				return;
			}
			if (!this.m_bEnable)
			{
				if (!this.m_lstFrames.Exists((NKC2DMotionAfterImage.MotionAfterImageSingleFrame x) => x.IsAlive()))
				{
					this.Clear();
					return;
				}
			}
			this.m_fBlurGapTimeNow -= fDeltaTime;
			if (this.m_fBlurGapTimeNow < 0f)
			{
				this.m_fBlurGapTimeNow = this.m_fBlurGapTime;
				this.AddSingleFrame(this.m_rendererTarget, this.m_fColor);
			}
			for (int i = 0; i < this.m_lstFrames.Count; i++)
			{
				NKC2DMotionAfterImage.MotionAfterImageSingleFrame motionAfterImageSingleFrame = this.m_lstFrames[i];
				if (motionAfterImageSingleFrame != null)
				{
					motionAfterImageSingleFrame.Update(fDeltaTime, this.m_fAlphaSpeed);
				}
			}
		}

		// Token: 0x0400303C RID: 12348
		private static Material DrawMaterial;

		// Token: 0x0400303D RID: 12349
		private static Stack<NKC2DMotionAfterImage.MotionAfterImageSingleFrame> s_stkFrame = new Stack<NKC2DMotionAfterImage.MotionAfterImageSingleFrame>();

		// Token: 0x0400303E RID: 12350
		private const int AfterImageLayer = 31;

		// Token: 0x0400303F RID: 12351
		private List<NKC2DMotionAfterImage.MotionAfterImageSingleFrame> m_lstFrames = new List<NKC2DMotionAfterImage.MotionAfterImageSingleFrame>();

		// Token: 0x04003040 RID: 12352
		private bool m_bEnable;

		// Token: 0x04003041 RID: 12353
		private bool m_bOnProcess;

		// Token: 0x04003042 RID: 12354
		private float m_fBlurGapTime = 0.1f;

		// Token: 0x04003043 RID: 12355
		private float m_fBlurGapTimeNow = 0.1f;

		// Token: 0x04003044 RID: 12356
		private float m_fAlphaSpeed = 1.5f;

		// Token: 0x04003045 RID: 12357
		private Color m_fColor;

		// Token: 0x04003046 RID: 12358
		private Renderer m_rendererTarget;

		// Token: 0x04003047 RID: 12359
		private int maxImageCount;

		// Token: 0x04003048 RID: 12360
		private int currentFrameIndex;

		// Token: 0x020012E8 RID: 4840
		private class MotionAfterImageSingleFrame
		{
			// Token: 0x170017E9 RID: 6121
			// (get) Token: 0x0600A4B7 RID: 42167 RVA: 0x0034447A File Offset: 0x0034267A
			public RenderTexture Texture
			{
				get
				{
					return this.m_Texture;
				}
			}

			// Token: 0x0600A4B8 RID: 42168 RVA: 0x00344482 File Offset: 0x00342682
			public bool IsAlive()
			{
				return this.m_fRemainAlpha > 0f || this.m_GameObject.activeSelf;
			}

			// Token: 0x0600A4B9 RID: 42169 RVA: 0x003444A0 File Offset: 0x003426A0
			public MotionAfterImageSingleFrame(Material mat)
			{
				this.m_GameObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
				this.m_GameObject.name = "MotionAfterImage";
				this.m_GameObject.SetActive(false);
				if (NKCScenManager.GetScenManager() != null)
				{
					this.m_GameObject.transform.SetParent(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_GAME_BATTLE_UNIT_MOTION_BLUR().transform, false);
				}
				this.m_Renderer = this.m_GameObject.GetComponent<MeshRenderer>();
				this.m_Renderer.material = mat;
				this.m_Texture = new RenderTexture(256, 256, 0, RenderTextureFormat.ARGB32);
				this.m_Texture.wrapMode = TextureWrapMode.Clamp;
				this.m_Texture.antiAliasing = 1;
				this.m_Texture.filterMode = FilterMode.Bilinear;
				this.m_Texture.anisoLevel = 0;
				this.m_Texture.Create();
				this.m_Renderer.material.mainTexture = this.m_Texture;
			}

			// Token: 0x0600A4BA RID: 42170 RVA: 0x003445B8 File Offset: 0x003427B8
			public void Update(float fDeltaTime, float fAlphaSpeed)
			{
				if (this.m_Texture == null)
				{
					return;
				}
				if (this.m_GameObject == null)
				{
					return;
				}
				if (!this.m_GameObject.activeSelf)
				{
					return;
				}
				if (this.m_fRemainAlpha > 0f)
				{
					this.m_fRemainAlpha -= fAlphaSpeed * fDeltaTime;
					this.m_BlurColor.a = this.m_fRemainAlpha;
					this.SetColor(this.m_BlurColor);
					if (this.m_fRemainAlpha <= 0f)
					{
						this.m_fRemainAlpha = 0f;
						this.SetActive(false);
					}
				}
			}

			// Token: 0x0600A4BB RID: 42171 RVA: 0x0034464C File Offset: 0x0034284C
			public void SetFrame(Renderer targetRenderer, Color color)
			{
				Bounds bounds = targetRenderer.bounds;
				NKCScenManager.GetScenManager().TextureCapture(targetRenderer, bounds, ref this.m_Texture);
				this.m_fRemainAlpha = color.a;
				this.m_BlurColor = color;
				this.SetColor(color);
				this.m_GameObject.transform.localScale = new Vector3(bounds.size.x, bounds.size.y);
				this.m_GameObject.transform.position = bounds.center;
				this.m_Renderer.sortingLayerID = targetRenderer.sortingLayerID;
				this.m_Renderer.sortingOrder = targetRenderer.sortingOrder - 1;
				this.SetActive(true);
			}

			// Token: 0x0600A4BC RID: 42172 RVA: 0x003446FB File Offset: 0x003428FB
			public void SetActive(bool value)
			{
				if (this.m_GameObject != null && this.m_GameObject.activeSelf != value)
				{
					this.m_GameObject.SetActive(value);
				}
			}

			// Token: 0x0600A4BD RID: 42173 RVA: 0x00344725 File Offset: 0x00342925
			public void Delete()
			{
				UnityEngine.Object.Destroy(this.m_GameObject);
				if (this.m_Texture != null)
				{
					this.m_Texture.Release();
					UnityEngine.Object.DestroyImmediate(this.m_Texture);
				}
				this.m_Texture = null;
			}

			// Token: 0x0600A4BE RID: 42174 RVA: 0x0034475D File Offset: 0x0034295D
			public void SetColor(Color color)
			{
				this.m_Renderer.material.color = color;
			}

			// Token: 0x0600A4BF RID: 42175 RVA: 0x00344770 File Offset: 0x00342970
			public void SetColor(float fR, float fG, float fB, float fA)
			{
				Color color = new Color(fR, fG, fB, fA);
				this.m_Renderer.material.color = color;
			}

			// Token: 0x0400974C RID: 38732
			private MeshRenderer m_Renderer;

			// Token: 0x0400974D RID: 38733
			private RenderTexture m_Texture;

			// Token: 0x0400974E RID: 38734
			private GameObject m_GameObject;

			// Token: 0x0400974F RID: 38735
			private float m_fRemainAlpha;

			// Token: 0x04009750 RID: 38736
			private Color m_BlurColor = new Color(1f, 1f, 1f, 1f);

			// Token: 0x04009751 RID: 38737
			private const float zSpacing = 0.1f;
		}
	}
}
