using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x0200033C RID: 828
	[AddComponentMenu("UI/Extensions/ScrollRectEx")]
	public class ScrollRectEx : ScrollRect
	{
		// Token: 0x06001373 RID: 4979 RVA: 0x00048E50 File Offset: 0x00047050
		private void DoForParents<T>(Action<T> action) where T : IEventSystemHandler
		{
			Transform parent = base.transform.parent;
			while (parent != null)
			{
				foreach (Component component in parent.GetComponents<Component>())
				{
					if (component is T)
					{
						action((T)((object)((IEventSystemHandler)component)));
					}
				}
				parent = parent.parent;
			}
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x00048EB0 File Offset: 0x000470B0
		public override void OnInitializePotentialDrag(PointerEventData eventData)
		{
			this.DoForParents<IInitializePotentialDragHandler>(delegate(IInitializePotentialDragHandler parent)
			{
				parent.OnInitializePotentialDrag(eventData);
			});
			base.OnInitializePotentialDrag(eventData);
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00048EE8 File Offset: 0x000470E8
		public override void OnDrag(PointerEventData eventData)
		{
			if (this.routeToParent)
			{
				this.DoForParents<IDragHandler>(delegate(IDragHandler parent)
				{
					parent.OnDrag(eventData);
				});
				return;
			}
			base.OnDrag(eventData);
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x00048F2C File Offset: 0x0004712C
		public override void OnBeginDrag(PointerEventData eventData)
		{
			if (!base.horizontal && Math.Abs(eventData.delta.x) > Math.Abs(eventData.delta.y))
			{
				this.routeToParent = true;
			}
			else if (!base.vertical && Math.Abs(eventData.delta.x) < Math.Abs(eventData.delta.y))
			{
				this.routeToParent = true;
			}
			else
			{
				this.routeToParent = false;
			}
			if (this.routeToParent)
			{
				this.DoForParents<IBeginDragHandler>(delegate(IBeginDragHandler parent)
				{
					parent.OnBeginDrag(eventData);
				});
				return;
			}
			base.OnBeginDrag(eventData);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x00048FF0 File Offset: 0x000471F0
		public override void OnEndDrag(PointerEventData eventData)
		{
			if (this.routeToParent)
			{
				this.DoForParents<IEndDragHandler>(delegate(IEndDragHandler parent)
				{
					parent.OnEndDrag(eventData);
				});
			}
			else
			{
				base.OnEndDrag(eventData);
			}
			this.routeToParent = false;
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0004903C File Offset: 0x0004723C
		public override void OnScroll(PointerEventData eventData)
		{
			if (!base.horizontal && Math.Abs(eventData.scrollDelta.x) > Math.Abs(eventData.scrollDelta.y))
			{
				this.routeToParent = true;
			}
			else if (!base.vertical && Math.Abs(eventData.scrollDelta.x) < Math.Abs(eventData.scrollDelta.y))
			{
				this.routeToParent = true;
			}
			else
			{
				this.routeToParent = false;
			}
			if (this.routeToParent)
			{
				this.DoForParents<IScrollHandler>(delegate(IScrollHandler parent)
				{
					parent.OnScroll(eventData);
				});
				return;
			}
			base.OnScroll(eventData);
		}

		// Token: 0x04000D78 RID: 3448
		private bool routeToParent;
	}
}
