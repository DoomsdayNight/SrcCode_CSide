using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002E9 RID: 745
	[ExecuteInEditMode]
	[AddComponentMenu("UI/Effects/Extensions/SoftMaskScript")]
	public class SoftMaskScript : MonoBehaviour
	{
		// Token: 0x0600105D RID: 4189 RVA: 0x000377A8 File Offset: 0x000359A8
		private void Start()
		{
			if (this.MaskArea == null)
			{
				this.MaskArea = base.GetComponent<RectTransform>();
			}
			Text component = base.GetComponent<Text>();
			if (component != null)
			{
				this.mat = new Material(ShaderLibrary.GetShaderInstance("UI Extensions/SoftMaskShader"));
				component.material = this.mat;
				this.cachedCanvas = component.canvas;
				this.cachedCanvasTransform = this.cachedCanvas.transform;
				if (base.transform.parent.GetComponent<Mask>() == null)
				{
					base.transform.parent.gameObject.AddComponent<Mask>();
				}
				base.transform.parent.GetComponent<Mask>().enabled = false;
				return;
			}
			Graphic component2 = base.GetComponent<Graphic>();
			if (component2 != null)
			{
				this.mat = new Material(ShaderLibrary.GetShaderInstance("UI Extensions/SoftMaskShader"));
				component2.material = this.mat;
				this.cachedCanvas = component2.canvas;
				this.cachedCanvasTransform = this.cachedCanvas.transform;
			}
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x000378B3 File Offset: 0x00035AB3
		private void Update()
		{
			if (this.cachedCanvas != null)
			{
				this.SetMask();
			}
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x000378CC File Offset: 0x00035ACC
		private void SetMask()
		{
			Rect canvasRect = this.GetCanvasRect();
			Vector2 size = canvasRect.size;
			this.maskScale.Set(1f / size.x, 1f / size.y);
			this.maskOffset = -canvasRect.min;
			this.maskOffset.Scale(this.maskScale);
			this.mat.SetTextureOffset("_AlphaMask", this.maskOffset);
			this.mat.SetTextureScale("_AlphaMask", this.maskScale);
			this.mat.SetTexture("_AlphaMask", this.AlphaMask);
			this.mat.SetFloat("_HardBlend", (float)(this.HardBlend ? 1 : 0));
			this.mat.SetInt("_FlipAlphaMask", this.FlipAlphaMask ? 1 : 0);
			this.mat.SetInt("_NoOuterClip", this.DontClipMaskScalingRect ? 1 : 0);
			this.mat.SetFloat("_CutOff", this.CutOff);
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x000379DC File Offset: 0x00035BDC
		public Rect GetCanvasRect()
		{
			if (this.cachedCanvas == null)
			{
				return default(Rect);
			}
			this.MaskArea.GetWorldCorners(this.m_WorldCorners);
			for (int i = 0; i < 4; i++)
			{
				this.m_CanvasCorners[i] = this.cachedCanvasTransform.InverseTransformPoint(this.m_WorldCorners[i]);
			}
			return new Rect(this.m_CanvasCorners[0].x, this.m_CanvasCorners[0].y, this.m_CanvasCorners[2].x - this.m_CanvasCorners[0].x, this.m_CanvasCorners[2].y - this.m_CanvasCorners[0].y);
		}

		// Token: 0x04000B4B RID: 2891
		private Material mat;

		// Token: 0x04000B4C RID: 2892
		private Canvas cachedCanvas;

		// Token: 0x04000B4D RID: 2893
		private Transform cachedCanvasTransform;

		// Token: 0x04000B4E RID: 2894
		private readonly Vector3[] m_WorldCorners = new Vector3[4];

		// Token: 0x04000B4F RID: 2895
		private readonly Vector3[] m_CanvasCorners = new Vector3[4];

		// Token: 0x04000B50 RID: 2896
		[Tooltip("The area that is to be used as the container.")]
		public RectTransform MaskArea;

		// Token: 0x04000B51 RID: 2897
		[Tooltip("Texture to be used to do the soft alpha")]
		public Texture AlphaMask;

		// Token: 0x04000B52 RID: 2898
		[Tooltip("At what point to apply the alpha min range 0-1")]
		[Range(0f, 1f)]
		public float CutOff;

		// Token: 0x04000B53 RID: 2899
		[Tooltip("Implement a hard blend based on the Cutoff")]
		public bool HardBlend;

		// Token: 0x04000B54 RID: 2900
		[Tooltip("Flip the masks alpha value")]
		public bool FlipAlphaMask;

		// Token: 0x04000B55 RID: 2901
		[Tooltip("If a different Mask Scaling Rect is given, and this value is true, the area around the mask will not be clipped")]
		public bool DontClipMaskScalingRect;

		// Token: 0x04000B56 RID: 2902
		private Vector2 maskOffset = Vector2.zero;

		// Token: 0x04000B57 RID: 2903
		private Vector2 maskScale = Vector2.one;
	}
}
