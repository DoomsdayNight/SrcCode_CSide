using System;
using UnityEngine;

// Token: 0x02000022 RID: 34
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/ScreenWater")]
public class ScreenWater : MonoBehaviour
{
	// Token: 0x06000109 RID: 265 RVA: 0x00004CBA File Offset: 0x00002EBA
	protected void Start()
	{
		if (this.shaderRGB == null)
		{
			Debug.Log("Sat shaders are not set up! Disabling saturation effect.");
			base.enabled = false;
			return;
		}
		if (!this.shaderRGB.isSupported)
		{
			base.enabled = false;
		}
	}

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x0600010A RID: 266 RVA: 0x00004CF0 File Offset: 0x00002EF0
	protected Material material
	{
		get
		{
			if (this.m_MaterialRGB == null)
			{
				this.m_MaterialRGB = new Material(this.shaderRGB);
				this.m_MaterialRGB.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_MaterialRGB;
		}
	}

	// Token: 0x0600010B RID: 267 RVA: 0x00004D24 File Offset: 0x00002F24
	protected void OnDisable()
	{
		if (this.m_MaterialRGB)
		{
			UnityEngine.Object.DestroyImmediate(this.m_MaterialRGB);
		}
	}

	// Token: 0x0600010C RID: 268 RVA: 0x00004D40 File Offset: 0x00002F40
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Material material = this.material;
		material.SetTexture("_Splat", this.WaterFlows);
		material.SetTexture("_Flow", this.WaterMask);
		material.SetTexture("_Water", this.WetScreen);
		material.SetFloat("_Speed", this.Speed);
		material.SetFloat("_Intens", this.Intens);
		Graphics.Blit(source, destination, material);
	}

	// Token: 0x04000097 RID: 151
	public Shader shaderRGB;

	// Token: 0x04000098 RID: 152
	private Material m_MaterialRGB;

	// Token: 0x04000099 RID: 153
	public Texture2D WaterFlows;

	// Token: 0x0400009A RID: 154
	public Texture2D WaterMask;

	// Token: 0x0400009B RID: 155
	public Texture2D WetScreen;

	// Token: 0x0400009C RID: 156
	public float Speed = 0.5f;

	// Token: 0x0400009D RID: 157
	public float Intens = 0.5f;
}
