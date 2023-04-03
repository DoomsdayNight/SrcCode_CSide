using System;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002F1 RID: 753
	[ExecuteInEditMode]
	public class SuperellipsePoints : MonoBehaviour
	{
		// Token: 0x06001087 RID: 4231 RVA: 0x00039F50 File Offset: 0x00038150
		private void Start()
		{
			this.RecalculateSuperellipse();
			base.GetComponent<MeshRenderer>().material = this.material;
			this.lastXLim = this.xLimits;
			this.lastYLim = this.yLimits;
			this.lastSuper = this.superness;
			this.lastLoD = this.levelOfDetail;
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00039FA4 File Offset: 0x000381A4
		private void Update()
		{
			if (this.lastXLim != this.xLimits || this.lastYLim != this.yLimits || this.lastSuper != this.superness || this.lastLoD != this.levelOfDetail)
			{
				this.RecalculateSuperellipse();
			}
			this.lastXLim = this.xLimits;
			this.lastYLim = this.yLimits;
			this.lastSuper = this.superness;
			this.lastLoD = this.levelOfDetail;
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0003A020 File Offset: 0x00038220
		private void RecalculateSuperellipse()
		{
			this.pointList.Clear();
			float num = (float)(this.levelOfDetail * 4);
			for (float num2 = 0f; num2 < this.xLimits; num2 += 1f / num)
			{
				float y = this.Superellipse(this.xLimits, this.yLimits, num2, this.superness);
				Vector2 item = new Vector2(num2, y);
				this.pointList.Add(item);
			}
			this.pointList.Add(new Vector2(this.xLimits, 0f));
			this.pointList.Add(Vector2.zero);
			base.GetComponent<MeshCreator>().CreateMesh(this.pointList);
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x0003A0C8 File Offset: 0x000382C8
		private float Superellipse(float a, float b, float x, float n)
		{
			float num = Mathf.Pow(x / a, n);
			return Mathf.Pow(1f - num, 1f / n) * b;
		}

		// Token: 0x04000BA2 RID: 2978
		public float xLimits = 1f;

		// Token: 0x04000BA3 RID: 2979
		public float yLimits = 1f;

		// Token: 0x04000BA4 RID: 2980
		[Range(1f, 96f)]
		public float superness = 4f;

		// Token: 0x04000BA5 RID: 2981
		private float lastXLim;

		// Token: 0x04000BA6 RID: 2982
		private float lastYLim;

		// Token: 0x04000BA7 RID: 2983
		private float lastSuper;

		// Token: 0x04000BA8 RID: 2984
		[Space]
		[Range(1f, 32f)]
		public int levelOfDetail = 4;

		// Token: 0x04000BA9 RID: 2985
		private int lastLoD;

		// Token: 0x04000BAA RID: 2986
		[Space]
		public Material material;

		// Token: 0x04000BAB RID: 2987
		private List<Vector2> pointList = new List<Vector2>();
	}
}
