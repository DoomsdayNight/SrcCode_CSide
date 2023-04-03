using System;
using UnityEngine.Events;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002C0 RID: 704
	[RequireComponent(typeof(RectTransform))]
	[DisallowMultipleComponent]
	[AddComponentMenu("UI/Extensions/Re-orderable list")]
	public class ReorderableList : MonoBehaviour
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x0002DD75 File Offset: 0x0002BF75
		public RectTransform Content
		{
			get
			{
				if (this._content == null)
				{
					this._content = this.ContentLayout.GetComponent<RectTransform>();
				}
				return this._content;
			}
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x0002DD9C File Offset: 0x0002BF9C
		private Canvas GetCanvas()
		{
			Transform transform = base.transform;
			Canvas canvas = null;
			int num = 100;
			int num2 = 0;
			while (canvas == null && num2 < num)
			{
				canvas = transform.gameObject.GetComponent<Canvas>();
				if (canvas == null)
				{
					transform = transform.parent;
				}
				num2++;
			}
			return canvas;
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0002DDE7 File Offset: 0x0002BFE7
		public void Refresh()
		{
			this._listContent = this.ContentLayout.gameObject.GetOrAddComponent<ReorderableListContent>();
			this._listContent.Init(this);
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0002DE0C File Offset: 0x0002C00C
		private void Start()
		{
			if (this.ContentLayout == null)
			{
				Debug.LogError("You need to have a child LayoutGroup content set for the list: " + base.name, base.gameObject);
				return;
			}
			if (this.DraggableArea == null)
			{
				this.DraggableArea = base.transform.root.GetComponentInChildren<Canvas>().GetComponent<RectTransform>();
			}
			if (this.IsDropable && !base.GetComponent<Graphic>())
			{
				Debug.LogError("You need to have a Graphic control (such as an Image) for the list [" + base.name + "] to be droppable", base.gameObject);
				return;
			}
			this.Refresh();
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0002DEA8 File Offset: 0x0002C0A8
		public void TestReOrderableListTarget(ReorderableList.ReorderableListEventStruct item)
		{
			Debug.Log("Event Received");
			Debug.Log("Hello World, is my item a clone? [" + item.IsAClone.ToString() + "]");
		}

		// Token: 0x04000A66 RID: 2662
		[Tooltip("Child container with re-orderable items in a layout group")]
		public LayoutGroup ContentLayout;

		// Token: 0x04000A67 RID: 2663
		[Tooltip("Parent area to draw the dragged element on top of containers. Defaults to the root Canvas")]
		public RectTransform DraggableArea;

		// Token: 0x04000A68 RID: 2664
		[Tooltip("Can items be dragged from the container?")]
		public bool IsDraggable = true;

		// Token: 0x04000A69 RID: 2665
		[Tooltip("Should the draggable components be removed or cloned?")]
		public bool CloneDraggedObject;

		// Token: 0x04000A6A RID: 2666
		[Tooltip("Can new draggable items be dropped in to the container?")]
		public bool IsDropable = true;

		// Token: 0x04000A6B RID: 2667
		[Tooltip("Should dropped items displace a current item if the list is full?\n Depending on the dropped items origin list, the displaced item may be added, dropped in space or deleted.")]
		public bool IsDisplacable;

		// Token: 0x04000A6C RID: 2668
		[Tooltip("Should items being dragged over this list have their sizes equalized?")]
		public bool EqualizeSizesOnDrag;

		// Token: 0x04000A6D RID: 2669
		public int maxItems = int.MaxValue;

		// Token: 0x04000A6E RID: 2670
		[Header("UI Re-orderable Events")]
		public ReorderableList.ReorderableListHandler OnElementDropped = new ReorderableList.ReorderableListHandler();

		// Token: 0x04000A6F RID: 2671
		public ReorderableList.ReorderableListHandler OnElementGrabbed = new ReorderableList.ReorderableListHandler();

		// Token: 0x04000A70 RID: 2672
		public ReorderableList.ReorderableListHandler OnElementRemoved = new ReorderableList.ReorderableListHandler();

		// Token: 0x04000A71 RID: 2673
		public ReorderableList.ReorderableListHandler OnElementAdded = new ReorderableList.ReorderableListHandler();

		// Token: 0x04000A72 RID: 2674
		public ReorderableList.ReorderableListHandler OnElementDisplacedFrom = new ReorderableList.ReorderableListHandler();

		// Token: 0x04000A73 RID: 2675
		public ReorderableList.ReorderableListHandler OnElementDisplacedTo = new ReorderableList.ReorderableListHandler();

		// Token: 0x04000A74 RID: 2676
		public ReorderableList.ReorderableListHandler OnElementDisplacedFromReturned = new ReorderableList.ReorderableListHandler();

		// Token: 0x04000A75 RID: 2677
		public ReorderableList.ReorderableListHandler OnElementDisplacedToReturned = new ReorderableList.ReorderableListHandler();

		// Token: 0x04000A76 RID: 2678
		public ReorderableList.ReorderableListHandler OnElementDroppedWithMaxItems = new ReorderableList.ReorderableListHandler();

		// Token: 0x04000A77 RID: 2679
		private RectTransform _content;

		// Token: 0x04000A78 RID: 2680
		private ReorderableListContent _listContent;

		// Token: 0x02001133 RID: 4403
		[Serializable]
		public struct ReorderableListEventStruct
		{
			// Token: 0x06009F40 RID: 40768 RVA: 0x0033C336 File Offset: 0x0033A536
			public void Cancel()
			{
				this.SourceObject.GetComponent<ReorderableListElement>().isValid = false;
			}

			// Token: 0x040091B8 RID: 37304
			public GameObject DroppedObject;

			// Token: 0x040091B9 RID: 37305
			public int FromIndex;

			// Token: 0x040091BA RID: 37306
			public ReorderableList FromList;

			// Token: 0x040091BB RID: 37307
			public bool IsAClone;

			// Token: 0x040091BC RID: 37308
			public GameObject SourceObject;

			// Token: 0x040091BD RID: 37309
			public int ToIndex;

			// Token: 0x040091BE RID: 37310
			public ReorderableList ToList;
		}

		// Token: 0x02001134 RID: 4404
		[Serializable]
		public class ReorderableListHandler : UnityEvent<ReorderableList.ReorderableListEventStruct>
		{
		}
	}
}
