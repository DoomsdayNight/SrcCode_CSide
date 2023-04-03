using System;
using UnityEngine;

// Token: 0x02000017 RID: 23
public class NKCUIMoveText : MonoBehaviour
{
	// Token: 0x060000CC RID: 204 RVA: 0x00003E8F File Offset: 0x0000208F
	private void Start()
	{
		if (this.m_rtText != null)
		{
			this.m_oriPos = this.m_rtText.anchoredPosition;
		}
	}

	// Token: 0x060000CD RID: 205 RVA: 0x00003EB0 File Offset: 0x000020B0
	private void Update()
	{
		if (this.m_rtText == null)
		{
			return;
		}
		RectTransform component = base.GetComponent<RectTransform>();
		if (component == null)
		{
			return;
		}
		float width = component.rect.width;
		float width2 = this.m_rtText.rect.width;
		if (width >= width2)
		{
			this.m_rtText.anchoredPosition = this.m_oriPos;
			return;
		}
		Vector3 vector = this.m_rtText.anchoredPosition;
		vector.x -= this.m_speed;
		if (vector.x < 0f && Mathf.Abs(vector.x) >= width2)
		{
			vector.x = width;
		}
		this.m_rtText.anchoredPosition = vector;
	}

	// Token: 0x0400005B RID: 91
	public RectTransform m_rtText;

	// Token: 0x0400005C RID: 92
	public float m_speed = 1f;

	// Token: 0x0400005D RID: 93
	private Vector2 m_oriPos;
}
