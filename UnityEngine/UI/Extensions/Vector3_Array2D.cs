using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002D5 RID: 725
	[Serializable]
	public struct Vector3_Array2D
	{
		// Token: 0x17000129 RID: 297
		public Vector3 this[int _idx]
		{
			get
			{
				return this.array[_idx];
			}
			set
			{
				this.array[_idx] = value;
			}
		}

		// Token: 0x04000B14 RID: 2836
		[SerializeField]
		public Vector3[] array;
	}
}
