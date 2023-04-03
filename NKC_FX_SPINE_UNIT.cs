using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200002C RID: 44
public class NKC_FX_SPINE_UNIT : MonoBehaviour
{
	// Token: 0x06000156 RID: 342 RVA: 0x00006B32 File Offset: 0x00004D32
	private void Start()
	{
		this.isRunning = false;
	}

	// Token: 0x06000157 RID: 343 RVA: 0x00006B3C File Offset: 0x00004D3C
	private void Update()
	{
		float axis = Input.GetAxis("Horizontal");
		if (axis > 0f)
		{
			if (this.Root != null)
			{
				this.Root.localEulerAngles = this.front;
			}
			this.isAstand = false;
			this.ActionRun();
			this.translation = 1f * this.speed * 100f;
			this.translation *= Time.deltaTime;
			if (this.Root != null)
			{
				this.Root.Translate(this.translation, 0f, 0f);
				this.position2D.Set(this.Root.localPosition.x, this.Root.localPosition.y, 0f);
				this.Root.localPosition = this.position2D;
			}
		}
		else if (axis < 0f)
		{
			if (this.Root != null)
			{
				this.Root.localEulerAngles = this.back;
			}
			this.isAstand = false;
			this.ActionRun();
			this.translation = -1f * -this.speed * 100f;
			this.translation *= Time.deltaTime;
			if (this.Root != null)
			{
				this.Root.Translate(this.translation, 0f, 0f);
				this.position2D.Set(this.Root.localPosition.x, this.Root.localPosition.y, 0f);
				this.Root.localPosition = this.position2D;
			}
		}
		else
		{
			this.isRunning = false;
			this.ActionAstand();
		}
		if (Input.anyKey)
		{
			bool keyDown = Input.GetKeyDown(KeyCode.Alpha0);
			bool keyDown2 = Input.GetKeyDown(KeyCode.Alpha1);
			bool keyDown3 = Input.GetKeyDown(KeyCode.Alpha2);
			bool keyDown4 = Input.GetKeyDown(KeyCode.Alpha3);
			bool keyDown5 = Input.GetKeyDown(KeyCode.Alpha4);
			bool keyDown6 = Input.GetKeyDown(KeyCode.Alpha5);
			bool keyDown7 = Input.GetKeyDown(KeyCode.Alpha6);
			bool keyDown8 = Input.GetKeyDown(KeyCode.Alpha7);
			bool keyDown9 = Input.GetKeyDown(KeyCode.Alpha8);
			bool keyDown10 = Input.GetKeyDown(KeyCode.Alpha9);
			bool keyDown11 = Input.GetKeyDown(KeyCode.Q);
			bool keyDown12 = Input.GetKeyDown(KeyCode.W);
			bool keyDown13 = Input.GetKeyDown(KeyCode.E);
			bool keyDown14 = Input.GetKeyDown(KeyCode.R);
			if (keyDown && this.Action_0 != null)
			{
				this.Action_0.onClick.Invoke();
			}
			if (keyDown2 && this.Action_1 != null)
			{
				this.Action_1.onClick.Invoke();
			}
			if (keyDown3 && this.Action_2 != null)
			{
				this.Action_2.onClick.Invoke();
			}
			if (keyDown4 && this.Action_3 != null)
			{
				this.Action_3.onClick.Invoke();
			}
			if (keyDown5 && this.Action_4 != null)
			{
				this.Action_4.onClick.Invoke();
			}
			if (keyDown6 && this.Action_5 != null)
			{
				this.Action_5.onClick.Invoke();
			}
			if (keyDown7 && this.Action_6 != null)
			{
				this.Action_6.onClick.Invoke();
			}
			if (keyDown8 && this.Action_7 != null)
			{
				this.Action_7.onClick.Invoke();
			}
			if (keyDown9 && this.Action_8 != null)
			{
				this.Action_8.onClick.Invoke();
			}
			if (keyDown10 && this.Action_9 != null)
			{
				this.Action_9.onClick.Invoke();
			}
			if (keyDown11 && this.Action_Q != null)
			{
				this.Action_Q.onClick.Invoke();
			}
			if (keyDown12 && this.Action_W != null)
			{
				this.Action_W.onClick.Invoke();
			}
			if (keyDown13 && this.Action_E != null)
			{
				this.Action_E.onClick.Invoke();
			}
			if (keyDown14 && this.Action_R != null)
			{
				this.Action_R.onClick.Invoke();
			}
		}
	}

	// Token: 0x06000158 RID: 344 RVA: 0x00006F5C File Offset: 0x0000515C
	public void SetButton()
	{
		if (this.Spine_Event != null)
		{
			Button[] componentsInChildren = this.Spine_Event.GetComponentsInChildren<Button>();
			this.Action_1 = null;
			this.Action_2 = null;
			this.Action_3 = null;
			this.Action_4 = null;
			this.Action_5 = null;
			this.Action_6 = null;
			this.Action_7 = null;
			this.Action_8 = null;
			this.Action_9 = null;
			this.Action_0 = null;
			this.Action_Q = null;
			this.Action_W = null;
			this.Action_E = null;
			this.Action_R = null;
			int num = 0;
			if (componentsInChildren.Length != 0)
			{
				if (componentsInChildren.Length > num)
				{
					this.Action_1 = componentsInChildren[num];
				}
				num++;
				if (componentsInChildren.Length > num)
				{
					this.Action_2 = componentsInChildren[num];
				}
				num++;
				if (componentsInChildren.Length > num)
				{
					this.Action_3 = componentsInChildren[num];
				}
				num++;
				if (componentsInChildren.Length > num)
				{
					this.Action_4 = componentsInChildren[num];
				}
				num++;
				if (componentsInChildren.Length > num)
				{
					this.Action_5 = componentsInChildren[num];
				}
				num++;
				if (componentsInChildren.Length > num)
				{
					this.Action_6 = componentsInChildren[num];
				}
				num++;
				if (componentsInChildren.Length > num)
				{
					this.Action_7 = componentsInChildren[num];
				}
				num++;
				if (componentsInChildren.Length > num)
				{
					this.Action_8 = componentsInChildren[num];
				}
				num++;
				if (componentsInChildren.Length > num)
				{
					this.Action_9 = componentsInChildren[num];
				}
				num++;
				if (componentsInChildren.Length > num)
				{
					this.Action_0 = componentsInChildren[num];
				}
				num++;
				if (componentsInChildren.Length > num)
				{
					this.Action_Q = componentsInChildren[num];
				}
				num++;
				if (componentsInChildren.Length > num)
				{
					this.Action_W = componentsInChildren[num];
				}
				num++;
				if (componentsInChildren.Length > num)
				{
					this.Action_E = componentsInChildren[num];
				}
				num++;
				if (componentsInChildren.Length > num)
				{
					this.Action_R = componentsInChildren[num];
				}
				num++;
			}
		}
	}

	// Token: 0x06000159 RID: 345 RVA: 0x000070FB File Offset: 0x000052FB
	private void ActionRun()
	{
		if (!this.isRunning && this.Spine_Event != null)
		{
			this.Spine_Event.SetAnimationName(this.RunAnimation);
			this.isRunning = true;
		}
	}

	// Token: 0x0600015A RID: 346 RVA: 0x0000712B File Offset: 0x0000532B
	private void ActionAstand()
	{
		if (!this.isAstand && this.Spine_Event != null)
		{
			this.Spine_Event.SetAnimationName(this.AstandAnimation);
			this.isAstand = true;
		}
	}

	// Token: 0x040000F5 RID: 245
	public NKC_FX_SPINE_EVENT Spine_Event;

	// Token: 0x040000F6 RID: 246
	public Transform Root;

	// Token: 0x040000F7 RID: 247
	public float speed = 1f;

	// Token: 0x040000F8 RID: 248
	public string RunAnimation = "RUN";

	// Token: 0x040000F9 RID: 249
	public string AstandAnimation = "ASTAND";

	// Token: 0x040000FA RID: 250
	[Space]
	[Header("Key Action")]
	public Button Action_1;

	// Token: 0x040000FB RID: 251
	public Button Action_2;

	// Token: 0x040000FC RID: 252
	public Button Action_3;

	// Token: 0x040000FD RID: 253
	public Button Action_4;

	// Token: 0x040000FE RID: 254
	public Button Action_5;

	// Token: 0x040000FF RID: 255
	public Button Action_6;

	// Token: 0x04000100 RID: 256
	public Button Action_7;

	// Token: 0x04000101 RID: 257
	public Button Action_8;

	// Token: 0x04000102 RID: 258
	public Button Action_9;

	// Token: 0x04000103 RID: 259
	public Button Action_0;

	// Token: 0x04000104 RID: 260
	public Button Action_Q;

	// Token: 0x04000105 RID: 261
	public Button Action_W;

	// Token: 0x04000106 RID: 262
	public Button Action_E;

	// Token: 0x04000107 RID: 263
	public Button Action_R;

	// Token: 0x04000108 RID: 264
	private bool isRunning;

	// Token: 0x04000109 RID: 265
	private bool isAstand;

	// Token: 0x0400010A RID: 266
	private float translation;

	// Token: 0x0400010B RID: 267
	private Vector3 position2D = new Vector3(0f, 0f, 0f);

	// Token: 0x0400010C RID: 268
	private Vector3 front = new Vector3(0f, 0f, 0f);

	// Token: 0x0400010D RID: 269
	private Vector3 back = new Vector3(0f, 180f, 0f);
}
