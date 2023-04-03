using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000029 RID: 41
public class NKC_FX_FRAMERATE : MonoBehaviour
{
	// Token: 0x06000143 RID: 323 RVA: 0x00006704 File Offset: 0x00004904
	private void Update()
	{
		this.m_fFPSTime += (Time.deltaTime - this.m_fFPSTime) * 0.1f;
		if (Time.timeScale > 0f)
		{
			this.FPSView(this.m_fFPSTime);
		}
	}

	// Token: 0x06000144 RID: 324 RVA: 0x00006740 File Offset: 0x00004940
	private void FPSView(float _time)
	{
		float num = _time * 1000f;
		float num2 = 1f / _time;
		this.m_FPSText.Remove(0, this.m_FPSText.Length);
		this.m_FPSText.AppendFormat("{0:0.0} ms ({1:0.} fps)", num, num2);
		this.FPS.text = this.m_FPSText.ToString();
	}

	// Token: 0x040000E7 RID: 231
	public Text FPS;

	// Token: 0x040000E8 RID: 232
	private float m_fFPSTime;

	// Token: 0x040000E9 RID: 233
	private StringBuilder m_FPSText = new StringBuilder();
}
