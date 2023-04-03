using System;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class NKC_FX_ANIMATOR_SET_FLOAT : MonoBehaviour
{
	// Token: 0x06000007 RID: 7 RVA: 0x00002188 File Offset: 0x00000388
	private void Start()
	{
		if (this.Target == null)
		{
			this.Target = base.GetComponent<Animator>();
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x000021A4 File Offset: 0x000003A4
	private void Update()
	{
		if (this.Target != null)
		{
			this.Target.SetFloat(this.Name, this.Value);
		}
	}

	// Token: 0x04000003 RID: 3
	public Animator Target;

	// Token: 0x04000004 RID: 4
	public string Name = string.Empty;

	// Token: 0x04000005 RID: 5
	public float Value;
}
