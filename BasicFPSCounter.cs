using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200001F RID: 31
[RequireComponent(typeof(TextMesh))]
public class BasicFPSCounter : MonoBehaviour
{
	// Token: 0x060000FE RID: 254 RVA: 0x00004B6A File Offset: 0x00002D6A
	private void Start()
	{
		this._textMesh = base.GetComponent<TextMesh>();
		this._frameCounter = this.CountFrames();
		base.StartCoroutine(this.Counter());
	}

	// Token: 0x060000FF RID: 255 RVA: 0x00004B91 File Offset: 0x00002D91
	public IEnumerator Counter()
	{
		for (;;)
		{
			base.StartCoroutine(this._frameCounter);
			yield return this._secondPassed;
			base.StopCoroutine(this._frameCounter);
			this._textMesh.text = "FPS: " + this._framesCounted.ToString();
			this._framesCounted = 0;
		}
		yield break;
	}

	// Token: 0x06000100 RID: 256 RVA: 0x00004BA0 File Offset: 0x00002DA0
	private IEnumerator CountFrames()
	{
		for (;;)
		{
			yield return this._frameEnd;
			this._framesCounted++;
		}
		yield break;
	}

	// Token: 0x0400008B RID: 139
	private TextMesh _textMesh;

	// Token: 0x0400008C RID: 140
	private int _framesCounted;

	// Token: 0x0400008D RID: 141
	private IEnumerator _frameCounter;

	// Token: 0x0400008E RID: 142
	private readonly WaitForEndOfFrame _frameEnd = new WaitForEndOfFrame();

	// Token: 0x0400008F RID: 143
	private readonly WaitForSeconds _secondPassed = new WaitForSeconds(1f);
}
