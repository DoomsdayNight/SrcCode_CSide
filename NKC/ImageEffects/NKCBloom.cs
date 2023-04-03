using System;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

namespace NKC.ImageEffects
{
	// Token: 0x020008A1 RID: 2209
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	public class NKCBloom : PostEffectsBase
	{
		// Token: 0x060059FC RID: 23036 RVA: 0x001B5768 File Offset: 0x001B3968
		public override bool CheckResources()
		{
			base.CheckSupport(false);
			this.screenBlend = base.CheckShaderAndCreateMaterial(this.screenBlendShader, this.screenBlend);
			this.lensFlareMaterial = base.CheckShaderAndCreateMaterial(this.lensFlareShader, this.lensFlareMaterial);
			this.blurAndFlaresMaterial = base.CheckShaderAndCreateMaterial(this.blurAndFlaresShader, this.blurAndFlaresMaterial);
			this.brightPassFilterMaterial = base.CheckShaderAndCreateMaterial(this.brightPassFilterShader, this.brightPassFilterMaterial);
			if (!this.isSupported)
			{
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x060059FD RID: 23037 RVA: 0x001B57F4 File Offset: 0x001B39F4
		public void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources())
			{
				Graphics.Blit(source, destination);
				return;
			}
			int width = source.width / 4;
			int height = source.height / 4;
			float num = 1f * (float)source.width / (1f * (float)source.height);
			float num2 = 0.001953125f;
			RenderTexture renderTexture = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.Default);
			this.screenBlend.SetVector("_Threshhold", this.bloomThreshold * this.bloomThresholdColor);
			Graphics.Blit(source, renderTexture, this.screenBlend, 1);
			float num3 = this.sepBlurSpread;
			RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.Default);
			this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(0f, num3 * num2, 0f, 0f));
			Graphics.Blit(renderTexture, temporary, this.blurAndFlaresMaterial, 4);
			RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = temporary;
			temporary = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.Default);
			this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num3 / num * num2, 0f, 0f, 0f));
			Graphics.Blit(renderTexture, temporary, this.blurAndFlaresMaterial, 4);
			RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = temporary;
			this.screenBlend.SetFloat("_Intensity", this.bloomIntensity);
			this.screenBlend.SetTexture("_ColorBuffer", source);
			Graphics.Blit(renderTexture, destination, this.screenBlend, 0);
			RenderTexture.ReleaseTemporary(renderTexture);
		}

		// Token: 0x0400459E RID: 17822
		public NKCBloom.TweakMode tweakMode;

		// Token: 0x0400459F RID: 17823
		public NKCBloom.BloomScreenBlendMode screenBlendMode = NKCBloom.BloomScreenBlendMode.Add;

		// Token: 0x040045A0 RID: 17824
		public float sepBlurSpread = 2.5f;

		// Token: 0x040045A1 RID: 17825
		public NKCBloom.BloomQuality quality = NKCBloom.BloomQuality.High;

		// Token: 0x040045A2 RID: 17826
		public float bloomIntensity = 0.5f;

		// Token: 0x040045A3 RID: 17827
		public float bloomThreshold = 0.5f;

		// Token: 0x040045A4 RID: 17828
		public Color bloomThresholdColor = Color.white;

		// Token: 0x040045A5 RID: 17829
		public int bloomBlurIterations = 2;

		// Token: 0x040045A6 RID: 17830
		public int hollywoodFlareBlurIterations = 2;

		// Token: 0x040045A7 RID: 17831
		public float flareRotation;

		// Token: 0x040045A8 RID: 17832
		public NKCBloom.LensFlareStyle lensflareMode = NKCBloom.LensFlareStyle.Anamorphic;

		// Token: 0x040045A9 RID: 17833
		public float hollyStretchWidth = 2.5f;

		// Token: 0x040045AA RID: 17834
		public float lensflareIntensity;

		// Token: 0x040045AB RID: 17835
		public float lensflareThreshold = 0.3f;

		// Token: 0x040045AC RID: 17836
		public float lensFlareSaturation = 0.75f;

		// Token: 0x040045AD RID: 17837
		public Color flareColorA = new Color(0.4f, 0.4f, 0.8f, 0.75f);

		// Token: 0x040045AE RID: 17838
		public Color flareColorB = new Color(0.4f, 0.8f, 0.8f, 0.75f);

		// Token: 0x040045AF RID: 17839
		public Color flareColorC = new Color(0.8f, 0.4f, 0.8f, 0.75f);

		// Token: 0x040045B0 RID: 17840
		public Color flareColorD = new Color(0.8f, 0.4f, 0f, 0.75f);

		// Token: 0x040045B1 RID: 17841
		public Texture2D lensFlareVignetteMask;

		// Token: 0x040045B2 RID: 17842
		public Shader lensFlareShader;

		// Token: 0x040045B3 RID: 17843
		private Material lensFlareMaterial;

		// Token: 0x040045B4 RID: 17844
		public Shader screenBlendShader;

		// Token: 0x040045B5 RID: 17845
		private Material screenBlend;

		// Token: 0x040045B6 RID: 17846
		public Shader blurAndFlaresShader;

		// Token: 0x040045B7 RID: 17847
		private Material blurAndFlaresMaterial;

		// Token: 0x040045B8 RID: 17848
		public Shader brightPassFilterShader;

		// Token: 0x040045B9 RID: 17849
		private Material brightPassFilterMaterial;

		// Token: 0x040045BA RID: 17850
		private Camera m_Camera;

		// Token: 0x0200158F RID: 5519
		public enum LensFlareStyle
		{
			// Token: 0x0400A182 RID: 41346
			Ghosting,
			// Token: 0x0400A183 RID: 41347
			Anamorphic,
			// Token: 0x0400A184 RID: 41348
			Combined
		}

		// Token: 0x02001590 RID: 5520
		public enum TweakMode
		{
			// Token: 0x0400A186 RID: 41350
			Basic,
			// Token: 0x0400A187 RID: 41351
			Complex
		}

		// Token: 0x02001591 RID: 5521
		public enum HDRBloomMode
		{
			// Token: 0x0400A189 RID: 41353
			Auto,
			// Token: 0x0400A18A RID: 41354
			On,
			// Token: 0x0400A18B RID: 41355
			Off
		}

		// Token: 0x02001592 RID: 5522
		public enum BloomScreenBlendMode
		{
			// Token: 0x0400A18D RID: 41357
			Screen,
			// Token: 0x0400A18E RID: 41358
			Add
		}

		// Token: 0x02001593 RID: 5523
		public enum BloomQuality
		{
			// Token: 0x0400A190 RID: 41360
			Cheap,
			// Token: 0x0400A191 RID: 41361
			High
		}
	}
}
