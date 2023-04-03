using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200001A RID: 26
public class WaitForSecondsWithCancel : CustomYieldInstruction
{
	// Token: 0x060000E3 RID: 227 RVA: 0x000043C4 File Offset: 0x000025C4
	public WaitForSecondsWithCancel(float time, WaitForSecondsWithCancel.CancelWait waitCondition, UnityAction onCancel)
	{
		if (waitCondition == null)
		{
			throw new Exception("waitCondition delegate null!");
		}
		this.waitTime = Time.realtimeSinceStartup + time;
		this.dCancelWait = waitCondition;
		this.dOnCancel = onCancel;
	}

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x060000E4 RID: 228 RVA: 0x000043F5 File Offset: 0x000025F5
	public override bool keepWaiting
	{
		get
		{
			if (this.dCancelWait())
			{
				Debug.Log("CancelWait");
				UnityAction unityAction = this.dOnCancel;
				if (unityAction != null)
				{
					unityAction();
				}
				return false;
			}
			return Time.realtimeSinceStartup < this.waitTime;
		}
	}

	// Token: 0x0400006C RID: 108
	private WaitForSecondsWithCancel.CancelWait dCancelWait;

	// Token: 0x0400006D RID: 109
	private float waitTime;

	// Token: 0x0400006E RID: 110
	private UnityAction dOnCancel;

	// Token: 0x020010E5 RID: 4325
	// (Invoke) Token: 0x06009E6C RID: 40556
	public delegate bool CancelWait();
}
