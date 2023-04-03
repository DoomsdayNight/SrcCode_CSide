using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000339 RID: 825
	[AddComponentMenu("UI/Extensions/Pagination Manager")]
	public class PaginationManager : ToggleGroup
	{
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x00048969 File Offset: 0x00046B69
		public int CurrentPage
		{
			get
			{
				return this.scrollSnap.CurrentPage;
			}
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00048976 File Offset: 0x00046B76
		protected PaginationManager()
		{
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00048980 File Offset: 0x00046B80
		protected override void Start()
		{
			base.Start();
			if (this.scrollSnap == null)
			{
				Debug.LogError("A ScrollSnap script must be attached");
				return;
			}
			if (this.scrollSnap.Pagination)
			{
				this.scrollSnap.Pagination = null;
			}
			this.scrollSnap.OnSelectionPageChangedEvent.AddListener(new UnityAction<int>(this.SetToggleGraphics));
			this.scrollSnap.OnSelectionChangeEndEvent.AddListener(new UnityAction<int>(this.OnPageChangeEnd));
			this.ResetPaginationChildren();
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00048A08 File Offset: 0x00046C08
		public void ResetPaginationChildren()
		{
			this.m_PaginationChildren = base.GetComponentsInChildren<Toggle>().ToList<Toggle>();
			for (int i = 0; i < this.m_PaginationChildren.Count; i++)
			{
				this.m_PaginationChildren[i].onValueChanged.AddListener(new UnityAction<bool>(this.ToggleClick));
				this.m_PaginationChildren[i].group = this;
				this.m_PaginationChildren[i].isOn = false;
			}
			this.SetToggleGraphics(this.CurrentPage);
			if (this.m_PaginationChildren.Count != this.scrollSnap._scroll_rect.content.childCount)
			{
				Debug.LogWarning("Uneven pagination icon to page count");
			}
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00048ABA File Offset: 0x00046CBA
		public void GoToScreen(int pageNo)
		{
			this.scrollSnap.GoToScreen(pageNo, true);
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00048AC9 File Offset: 0x00046CC9
		private void ToggleClick(Toggle target)
		{
			if (!target.isOn)
			{
				this.isAClick = true;
				this.GoToScreen(this.m_PaginationChildren.IndexOf(target));
			}
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00048AEC File Offset: 0x00046CEC
		private void ToggleClick(bool toggle)
		{
			if (toggle)
			{
				for (int i = 0; i < this.m_PaginationChildren.Count; i++)
				{
					if (this.m_PaginationChildren[i].isOn && !this.scrollSnap._suspendEvents)
					{
						this.GoToScreen(i);
						return;
					}
				}
			}
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x00048B3A File Offset: 0x00046D3A
		private void ToggleClick(int target)
		{
			this.isAClick = true;
			this.GoToScreen(target);
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00048B4A File Offset: 0x00046D4A
		private void SetToggleGraphics(int pageNo)
		{
			if (!this.isAClick)
			{
				this.m_PaginationChildren[pageNo].isOn = true;
			}
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x00048B66 File Offset: 0x00046D66
		private void OnPageChangeEnd(int pageNo)
		{
			this.isAClick = false;
		}

		// Token: 0x04000D6D RID: 3437
		private List<Toggle> m_PaginationChildren;

		// Token: 0x04000D6E RID: 3438
		[SerializeField]
		private ScrollSnapBase scrollSnap;

		// Token: 0x04000D6F RID: 3439
		private bool isAClick;
	}
}
