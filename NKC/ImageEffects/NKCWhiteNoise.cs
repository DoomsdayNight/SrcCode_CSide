using System;
using UnityEngine;

namespace NKC.ImageEffects
{
	// Token: 0x020008A2 RID: 2210
	public class NKCWhiteNoise : MonoBehaviour
	{
		// Token: 0x060059FF RID: 23039 RVA: 0x001B5A5B File Offset: 0x001B3C5B
		public void Init()
		{
			this.SetUpResources();
		}

		// Token: 0x06005A00 RID: 23040 RVA: 0x001B5A64 File Offset: 0x001B3C64
		private void SetUpResources()
		{
			if (this._material != null)
			{
				return;
			}
			this._material = base.GetComponent<Renderer>().material;
			this._material.hideFlags = HideFlags.DontSave;
			this._noiseTexture = new Texture2D(128, 128, TextureFormat.ARGB32, false);
			this._noiseTexture.hideFlags = HideFlags.DontSave;
			this._noiseTexture.wrapMode = TextureWrapMode.Clamp;
			this._noiseTexture.filterMode = FilterMode.Point;
			this._material.SetTexture("_MainTex", this._noiseTexture);
			this._material.SetFloat("_Intensity", this.Intensity);
			this.UpdateNoiseTexture();
		}

		// Token: 0x06005A01 RID: 23041 RVA: 0x001B5B0C File Offset: 0x001B3D0C
		private void UpdateNoiseTexture()
		{
			for (int i = 0; i < this._noiseTexture.height; i++)
			{
				for (int j = 0; j < this._noiseTexture.width; j++)
				{
					if (UnityEngine.Random.value > 0.5f)
					{
						this._noiseTexture.SetPixel(j, i, Color.white);
					}
					else
					{
						this._noiseTexture.SetPixel(j, i, Color.black);
					}
				}
			}
			this._noiseTexture.Apply();
		}

		// Token: 0x06005A02 RID: 23042 RVA: 0x001B5B84 File Offset: 0x001B3D84
		private void Update()
		{
			this.SetUpResources();
			if (UnityEngine.Random.value > Mathf.Lerp(0.9f, 0.5f, this.Intensity))
			{
				this.UpdateNoiseTexture();
			}
			this.VerticalJumpTime += Time.deltaTime * this.VerticalJump * 11.3f;
			float y = Mathf.Clamp01(1f - this.ScanLineJitter * 1.2f);
			float x = 0.002f + Mathf.Pow(this.ScanLineJitter, 3f) * 0.05f;
			this._material.SetVector("_ScanLineJitter", new Vector2(x, y));
			Vector2 v = new Vector2(this.VerticalJump, this.VerticalJumpTime);
			this._material.SetVector("_VerticalJump", v);
			this._material.SetFloat("_HorizontalShake", this.HorizontalShake * 0.2f);
		}

		// Token: 0x040045BB RID: 17851
		[SerializeField]
		[Range(0f, 1f)]
		public float Intensity;

		// Token: 0x040045BC RID: 17852
		[SerializeField]
		[Range(0f, 1f)]
		public float ScanLineJitter;

		// Token: 0x040045BD RID: 17853
		[SerializeField]
		[Range(0f, 1f)]
		public float VerticalJump;

		// Token: 0x040045BE RID: 17854
		[SerializeField]
		[Range(0f, 1f)]
		public float HorizontalShake;

		// Token: 0x040045BF RID: 17855
		private float VerticalJumpTime;

		// Token: 0x040045C0 RID: 17856
		public Shader _shader;

		// Token: 0x040045C1 RID: 17857
		private Material _material;

		// Token: 0x040045C2 RID: 17858
		private Texture2D _noiseTexture;
	}
}
