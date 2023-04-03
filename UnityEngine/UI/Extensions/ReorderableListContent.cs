using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002C1 RID: 705
	[DisallowMultipleComponent]
	public class ReorderableListContent : MonoBehaviour
	{
		// Token: 0x06000F02 RID: 3842 RVA: 0x0002DF63 File Offset: 0x0002C163
		private void OnEnable()
		{
			if (this._rect)
			{
				base.StartCoroutine(this.RefreshChildren());
			}
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0002DF7F File Offset: 0x0002C17F
		public void OnTransformChildrenChanged()
		{
			if (base.isActiveAndEnabled)
			{
				base.StartCoroutine(this.RefreshChildren());
			}
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0002DF98 File Offset: 0x0002C198
		public void Init(ReorderableList extList)
		{
			if (this._started)
			{
				base.StopCoroutine(this.RefreshChildren());
			}
			this._extList = extList;
			this._rect = base.GetComponent<RectTransform>();
			this._cachedChildren = new List<Transform>();
			this._cachedListElement = new List<ReorderableListElement>();
			base.StartCoroutine(this.RefreshChildren());
			this._started = true;
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0002DFF6 File Offset: 0x0002C1F6
		private IEnumerator RefreshChildren()
		{
			for (int i = 0; i < this._rect.childCount; i++)
			{
				if (!this._cachedChildren.Contains(this._rect.GetChild(i)))
				{
					this._ele = (this._rect.GetChild(i).gameObject.GetComponent<ReorderableListElement>() ?? this._rect.GetChild(i).gameObject.AddComponent<ReorderableListElement>());
					this._ele.Init(this._extList);
					this._cachedChildren.Add(this._rect.GetChild(i));
					this._cachedListElement.Add(this._ele);
				}
			}
			yield return 0;
			for (int j = this._cachedChildren.Count - 1; j >= 0; j--)
			{
				if (this._cachedChildren[j] == null)
				{
					this._cachedChildren.RemoveAt(j);
					this._cachedListElement.RemoveAt(j);
				}
			}
			yield break;
		}

		// Token: 0x04000A79 RID: 2681
		private List<Transform> _cachedChildren;

		// Token: 0x04000A7A RID: 2682
		private List<ReorderableListElement> _cachedListElement;

		// Token: 0x04000A7B RID: 2683
		private ReorderableListElement _ele;

		// Token: 0x04000A7C RID: 2684
		private ReorderableList _extList;

		// Token: 0x04000A7D RID: 2685
		private RectTransform _rect;

		// Token: 0x04000A7E RID: 2686
		private bool _started;
	}
}
