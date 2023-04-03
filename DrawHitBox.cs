using System;
using UnityEngine;

// Token: 0x02000028 RID: 40
[ExecuteAlways]
[RequireComponent(typeof(LineRenderer))]
public class DrawHitBox : MonoBehaviour
{
	// Token: 0x0600013D RID: 317 RVA: 0x000064E6 File Offset: 0x000046E6
	public void ShowHitBox(bool _toggle)
	{
		this.show = _toggle;
		if (!this.show)
		{
			this.rend = base.GetComponent<LineRenderer>();
			this.rend.enabled = false;
		}
	}

	// Token: 0x0600013E RID: 318 RVA: 0x0000650F File Offset: 0x0000470F
	private void OnEnable()
	{
		this.rend = base.GetComponent<LineRenderer>();
		if (this.show)
		{
			this.rend.enabled = true;
		}
		else
		{
			this.rend.enabled = false;
		}
		if (this.AutoDisable)
		{
			this.HandleDisappear();
		}
	}

	// Token: 0x0600013F RID: 319 RVA: 0x0000654D File Offset: 0x0000474D
	private void HandleDisappear()
	{
		if (Application.isPlaying)
		{
			base.CancelInvoke();
			base.Invoke("Disappear", this.Lifetime);
		}
	}

	// Token: 0x06000140 RID: 320 RVA: 0x0000656D File Offset: 0x0000476D
	private void Disappear()
	{
		this.rend.enabled = false;
	}

	// Token: 0x06000141 RID: 321 RVA: 0x0000657C File Offset: 0x0000477C
	private void OnValidate()
	{
		if (this.rend == null)
		{
			return;
		}
		this.rend.useWorldSpace = false;
		this.rend.widthMultiplier = this.BoxSize.y;
		this.rend.sortingLayerName = "GAME_UI_FRONT";
		this.rend.sortingOrder = 0;
		this.rend.startColor = this.BoxColor;
		this.rend.endColor = this.BoxColor;
		if (this.Centered)
		{
			this.pos.Set(-(this.BoxSize.x * 0.5f), 0f, 0f);
			this.rend.SetPosition(0, this.pos);
			this.pos.Set(this.BoxSize.x * 0.5f, 0f, 0f);
			this.rend.SetPosition(1, this.pos);
			return;
		}
		this.pos.Set(0f, 0f, 0f);
		this.rend.SetPosition(0, this.pos);
		this.pos.Set(this.BoxSize.x, 0f, 0f);
		this.rend.SetPosition(1, this.pos);
	}

	// Token: 0x040000DF RID: 223
	public Color BoxColor;

	// Token: 0x040000E0 RID: 224
	public Vector2 BoxSize;

	// Token: 0x040000E1 RID: 225
	public bool Centered;

	// Token: 0x040000E2 RID: 226
	public bool AutoDisable;

	// Token: 0x040000E3 RID: 227
	[Range(0f, 5f)]
	public float Lifetime = 1f;

	// Token: 0x040000E4 RID: 228
	private bool show = true;

	// Token: 0x040000E5 RID: 229
	private LineRenderer rend;

	// Token: 0x040000E6 RID: 230
	private Vector3 pos = new Vector3(0f, 0f, 0f);
}
