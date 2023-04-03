using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002D2 RID: 722
	public class CUIBezierCurve : MonoBehaviour
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x00032DC4 File Offset: 0x00030FC4
		public Vector3[] ControlPoints
		{
			get
			{
				return this.controlPoints;
			}
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x00032DCC File Offset: 0x00030FCC
		public void Refresh()
		{
			if (this.OnRefresh != null)
			{
				this.OnRefresh();
			}
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00032DE4 File Offset: 0x00030FE4
		public Vector3 GetPoint(float _time)
		{
			float num = 1f - _time;
			return num * num * num * this.controlPoints[0] + 3f * num * num * _time * this.controlPoints[1] + 3f * num * _time * _time * this.controlPoints[2] + _time * _time * _time * this.controlPoints[3];
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x00032E6C File Offset: 0x0003106C
		public Vector3 GetTangent(float _time)
		{
			float num = 1f - _time;
			return 3f * num * num * (this.controlPoints[1] - this.controlPoints[0]) + 6f * num * _time * (this.controlPoints[2] - this.controlPoints[1]) + 3f * _time * _time * (this.controlPoints[3] - this.controlPoints[2]);
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x00032F0C File Offset: 0x0003110C
		public void ReportSet()
		{
			if (this.controlPoints == null)
			{
				this.controlPoints = new Vector3[CUIBezierCurve.CubicBezierCurvePtNum];
				this.controlPoints[0] = new Vector3(0f, 0f, 0f);
				this.controlPoints[1] = new Vector3(0f, 1f, 0f);
				this.controlPoints[2] = new Vector3(1f, 1f, 0f);
				this.controlPoints[3] = new Vector3(1f, 0f, 0f);
			}
			int num = this.controlPoints.Length;
			int cubicBezierCurvePtNum = CUIBezierCurve.CubicBezierCurvePtNum;
		}

		// Token: 0x04000B02 RID: 2818
		public static readonly int CubicBezierCurvePtNum = 4;

		// Token: 0x04000B03 RID: 2819
		[SerializeField]
		protected Vector3[] controlPoints;

		// Token: 0x04000B04 RID: 2820
		public Action OnRefresh;
	}
}
