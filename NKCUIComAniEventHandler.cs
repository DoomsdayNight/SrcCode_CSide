using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000011 RID: 17
public class NKCUIComAniEventHandler : MonoBehaviour
{
	// Token: 0x060000A0 RID: 160 RVA: 0x00003624 File Offset: 0x00001824
	public void NKCUIComAniEvent()
	{
		if (this.m_NKCUIComAniEvent != null)
		{
			this.m_NKCUIComAniEvent.Invoke();
		}
	}

	// Token: 0x04000041 RID: 65
	public UnityEvent m_NKCUIComAniEvent;

	// Token: 0x020010E0 RID: 4320
	// (Invoke) Token: 0x06009E58 RID: 40536
	private delegate void OnAniEventHandler();
}
