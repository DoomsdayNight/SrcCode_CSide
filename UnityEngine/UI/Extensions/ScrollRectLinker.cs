using System;
using UnityEngine.Events;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x0200033E RID: 830
	[RequireComponent(typeof(ScrollRect))]
	[AddComponentMenu("UI/Extensions/ScrollRectLinker")]
	public class ScrollRectLinker : MonoBehaviour
	{
		// Token: 0x0600137C RID: 4988 RVA: 0x00049134 File Offset: 0x00047334
		private void Awake()
		{
			this.scrollRect = base.GetComponent<ScrollRect>();
			if (this.controllingScrollRect != null)
			{
				this.controllingScrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this.MirrorPos));
			}
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0004916C File Offset: 0x0004736C
		private void MirrorPos(Vector2 scrollPos)
		{
			if (this.clamp)
			{
				this.scrollRect.normalizedPosition = new Vector2(Mathf.Clamp01(scrollPos.x), Mathf.Clamp01(scrollPos.y));
				return;
			}
			this.scrollRect.normalizedPosition = scrollPos;
		}

		// Token: 0x04000D79 RID: 3449
		public bool clamp = true;

		// Token: 0x04000D7A RID: 3450
		[SerializeField]
		private ScrollRect controllingScrollRect;

		// Token: 0x04000D7B RID: 3451
		private ScrollRect scrollRect;
	}
}
