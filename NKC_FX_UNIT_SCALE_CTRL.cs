using System;
using UnityEngine;

// Token: 0x0200002E RID: 46
public class NKC_FX_UNIT_SCALE_CTRL : MonoBehaviour
{
	// Token: 0x06000163 RID: 355 RVA: 0x000073D6 File Offset: 0x000055D6
	private void Start()
	{
		if (this.unitRoot == null)
		{
			this.unitRoot = GameObject.Find("UNIT").transform;
		}
	}

	// Token: 0x06000164 RID: 356 RVA: 0x000073FB File Offset: 0x000055FB
	public void SetScaleFactor(float _scale)
	{
		if (this.unitRoot != null)
		{
			this.newScale.Set(_scale, _scale, 1f);
			this.unitRoot.localScale = this.newScale;
			return;
		}
		Debug.LogWarning("Null Unit.");
	}

	// Token: 0x04000114 RID: 276
	private Transform unitRoot;

	// Token: 0x04000115 RID: 277
	private Vector3 newScale;
}
