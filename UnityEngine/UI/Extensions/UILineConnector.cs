using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000346 RID: 838
	[AddComponentMenu("UI/Extensions/UI Line Connector")]
	[RequireComponent(typeof(UILineRenderer))]
	[ExecuteInEditMode]
	public class UILineConnector : MonoBehaviour
	{
		// Token: 0x060013B8 RID: 5048 RVA: 0x000498B4 File Offset: 0x00047AB4
		private void Awake()
		{
			this.canvas = base.GetComponentInParent<RectTransform>().GetParentCanvas().GetComponent<RectTransform>();
			this.rt = base.GetComponent<RectTransform>();
			this.lr = base.GetComponent<UILineRenderer>();
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x000498E4 File Offset: 0x00047AE4
		private void Update()
		{
			if (this.transforms == null || this.transforms.Length < 1)
			{
				return;
			}
			if (this.previousPositions != null && this.previousPositions.Length == this.transforms.Length)
			{
				bool flag = false;
				for (int i = 0; i < this.transforms.Length; i++)
				{
					if (!flag && this.previousPositions[i] != this.transforms[i].position)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					return;
				}
			}
			Vector2 pivot = this.rt.pivot;
			Vector2 pivot2 = this.canvas.pivot;
			Vector3[] array = new Vector3[this.transforms.Length];
			Vector3[] array2 = new Vector3[this.transforms.Length];
			Vector2[] array3 = new Vector2[this.transforms.Length];
			for (int j = 0; j < this.transforms.Length; j++)
			{
				array[j] = this.transforms[j].TransformPoint(pivot);
			}
			for (int k = 0; k < this.transforms.Length; k++)
			{
				array2[k] = this.canvas.InverseTransformPoint(array[k]);
			}
			for (int l = 0; l < this.transforms.Length; l++)
			{
				array3[l] = new Vector2(array2[l].x, array2[l].y);
			}
			this.lr.Points = array3;
			this.lr.RelativeSize = false;
			this.lr.drivenExternally = true;
			this.previousPositions = new Vector3[this.transforms.Length];
			for (int m = 0; m < this.transforms.Length; m++)
			{
				this.previousPositions[m] = this.transforms[m].position;
			}
		}

		// Token: 0x04000D94 RID: 3476
		public RectTransform[] transforms;

		// Token: 0x04000D95 RID: 3477
		private Vector3[] previousPositions;

		// Token: 0x04000D96 RID: 3478
		private RectTransform canvas;

		// Token: 0x04000D97 RID: 3479
		private RectTransform rt;

		// Token: 0x04000D98 RID: 3480
		private UILineRenderer lr;
	}
}
