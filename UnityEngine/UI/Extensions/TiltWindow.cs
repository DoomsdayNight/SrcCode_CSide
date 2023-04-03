using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002B4 RID: 692
	public class TiltWindow : MonoBehaviour, IDragHandler, IEventSystemHandler
	{
		// Token: 0x06000E39 RID: 3641 RVA: 0x0002A849 File Offset: 0x00028A49
		private void Start()
		{
			this.mTrans = base.transform;
			this.mStart = this.mTrans.localRotation;
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0002A868 File Offset: 0x00028A68
		private void Update()
		{
			Vector3 vector = this.m_screenPos;
			float num = (float)Screen.width * 0.5f;
			float num2 = (float)Screen.height * 0.5f;
			float x = Mathf.Clamp((vector.x - num) / num, -1f, 1f);
			float y = Mathf.Clamp((vector.y - num2) / num2, -1f, 1f);
			this.mRot = Vector2.Lerp(this.mRot, new Vector2(x, y), Time.deltaTime * 5f);
			this.mTrans.localRotation = this.mStart * Quaternion.Euler(-this.mRot.y * this.range.y, this.mRot.x * this.range.x, 0f);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0002A93F File Offset: 0x00028B3F
		public void OnDrag(PointerEventData eventData)
		{
			this.m_screenPos = eventData.position;
		}

		// Token: 0x040009C6 RID: 2502
		public Vector2 range = new Vector2(5f, 3f);

		// Token: 0x040009C7 RID: 2503
		private Transform mTrans;

		// Token: 0x040009C8 RID: 2504
		private Quaternion mStart;

		// Token: 0x040009C9 RID: 2505
		private Vector2 mRot = Vector2.zero;

		// Token: 0x040009CA RID: 2506
		private Vector2 m_screenPos;
	}
}
