using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200002D RID: 45
[Serializable]
public class NKC_FX_TIMESCALE : MonoBehaviour
{
	// Token: 0x0600015C RID: 348 RVA: 0x000071DE File Offset: 0x000053DE
	private void Start()
	{
		if (this.TextBoxTimeScale != null && this.TextBoxPaused != null)
		{
			this.TextBoxPaused.enabled = false;
			this.init = true;
		}
	}

	// Token: 0x0600015D RID: 349 RVA: 0x00007210 File Offset: 0x00005410
	public void SetTimeScale(float timeScale)
	{
		if (this.init)
		{
			this.TimeScale = timeScale;
			this.TimeScale = Mathf.Clamp(this.TimeScale, 0f, 3f);
			Time.timeScale = this.TimeScale;
			if (this.TextBoxTimeScale != null)
			{
				this.TextBoxTimeScale.text = Time.timeScale.ToString("N2");
			}
		}
	}

	// Token: 0x0600015E RID: 350 RVA: 0x00007280 File Offset: 0x00005480
	public void IncDecTimeScale(float timeScale)
	{
		if (this.init)
		{
			this.TimeScale += timeScale;
			this.TimeScale = Mathf.Clamp(this.TimeScale, 0f, 3f);
			Time.timeScale = this.TimeScale;
			if (this.TextBoxTimeScale != null)
			{
				this.TextBoxTimeScale.text = Time.timeScale.ToString("N2");
			}
		}
	}

	// Token: 0x0600015F RID: 351 RVA: 0x000072F4 File Offset: 0x000054F4
	private void Update()
	{
		if (this.init)
		{
			if (Input.GetKeyDown(KeyCode.Z))
			{
				this.Toggle();
			}
			else if (Input.GetKeyDown(KeyCode.X))
			{
				this.Step();
			}
			else if (Input.GetKey(KeyCode.C))
			{
				this.Step();
			}
			if (this.IsStepping)
			{
				Time.timeScale = Mathf.Clamp(this.TimeScale, 0f, 3f);
				this.IsStepping = false;
				return;
			}
			Time.timeScale = (this.IsOn ? 0f : Mathf.Clamp(this.TimeScale, 0f, 3f));
		}
	}

	// Token: 0x06000160 RID: 352 RVA: 0x00007390 File Offset: 0x00005590
	private void Toggle()
	{
		this.IsOn = !this.IsOn;
		this.TextBoxPaused.enabled = !this.TextBoxPaused.enabled;
	}

	// Token: 0x06000161 RID: 353 RVA: 0x000073BA File Offset: 0x000055BA
	private void Step()
	{
		this.IsStepping = true;
	}

	// Token: 0x0400010E RID: 270
	[Range(0f, 10f)]
	public float TimeScale = 1f;

	// Token: 0x0400010F RID: 271
	public Text TextBoxTimeScale;

	// Token: 0x04000110 RID: 272
	public Text TextBoxPaused;

	// Token: 0x04000111 RID: 273
	private bool init;

	// Token: 0x04000112 RID: 274
	private bool IsOn;

	// Token: 0x04000113 RID: 275
	private bool IsStepping;
}
