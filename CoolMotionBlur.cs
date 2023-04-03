using System;
using UnityEngine;

// Token: 0x02000021 RID: 33
[ExecuteInEditMode]
public class CoolMotionBlur : MonoBehaviour
{
	// Token: 0x17000035 RID: 53
	// (get) Token: 0x06000105 RID: 261 RVA: 0x00004C41 File Offset: 0x00002E41
	public Material ScreenMat
	{
		get
		{
			return this.screenMat;
		}
	}

	// Token: 0x06000106 RID: 262 RVA: 0x00004C49 File Offset: 0x00002E49
	private void Start()
	{
		this.screenMat.SetVector("_Center", new Vector4(this.movingCenter.x, this.movingCenter.y, 0f, 0f));
	}

	// Token: 0x06000107 RID: 263 RVA: 0x00004C80 File Offset: 0x00002E80
	private void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		if (this.screenMat != null)
		{
			Graphics.Blit(src, dst, this.screenMat);
		}
	}

	// Token: 0x04000095 RID: 149
	[SerializeField]
	private Material screenMat;

	// Token: 0x04000096 RID: 150
	[SerializeField]
	private Vector2 movingCenter = new Vector2(0.5f, 0.5f);
}
