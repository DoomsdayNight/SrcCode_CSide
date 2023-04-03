using System;
using NextGenSprites;
using UnityEngine;

// Token: 0x02000020 RID: 32
[RequireComponent(typeof(SpriteRenderer))]
public class DualMaterialExample : MonoBehaviour
{
	// Token: 0x06000102 RID: 258 RVA: 0x00004BD4 File Offset: 0x00002DD4
	private void Awake()
	{
		this._sRenderer = base.GetComponent<SpriteRenderer>();
		this._dualMat = new DualMaterial(this.FirstMaterial, this.SecondMaterial, this.MaterialName);
		this._sRenderer.material = this._dualMat.FusedMaterial;
	}

	// Token: 0x06000103 RID: 259 RVA: 0x00004C20 File Offset: 0x00002E20
	public void SetMaterialLerp(float target)
	{
		this._dualMat.Lerp = target;
	}

	// Token: 0x04000090 RID: 144
	public string MaterialName = "Awesome Dual Material";

	// Token: 0x04000091 RID: 145
	public Material FirstMaterial;

	// Token: 0x04000092 RID: 146
	public Material SecondMaterial;

	// Token: 0x04000093 RID: 147
	private DualMaterial _dualMat;

	// Token: 0x04000094 RID: 148
	private SpriteRenderer _sRenderer;
}
