using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200002F RID: 47
[ExecuteAlways]
public class UITextSync : MonoBehaviour
{
	// Token: 0x06000166 RID: 358 RVA: 0x00007441 File Offset: 0x00005641
	private void Awake()
	{
		if (this.Target == null)
		{
			this.Target = base.gameObject.GetComponentInChildren<Text>();
		}
	}

	// Token: 0x06000167 RID: 359 RVA: 0x00007462 File Offset: 0x00005662
	private void Start()
	{
		if (Application.isPlaying)
		{
			UnityEngine.Object.DestroyImmediate(this);
		}
	}

	// Token: 0x06000168 RID: 360 RVA: 0x00007471 File Offset: 0x00005671
	private void Update()
	{
		if (base.gameObject.name != this.oldName)
		{
			this.oldName = base.gameObject.name;
			this.Target.text = this.oldName;
		}
	}

	// Token: 0x04000116 RID: 278
	public Text Target;

	// Token: 0x04000117 RID: 279
	private string oldName = string.Empty;
}
