using System;
using Cs.Logging;
using Cs.Math;

namespace NKM
{
	// Token: 0x0200042E RID: 1070
	public class NKMMinMaxFloat
	{
		// Token: 0x06001D2F RID: 7471 RVA: 0x00088BA7 File Offset: 0x00086DA7
		public void SetMinMax(float fMin, float fMax)
		{
			this.m_Min = fMin;
			this.m_Max = fMax;
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x00088BB7 File Offset: 0x00086DB7
		public void DeepCopyFromSource(NKMMinMaxFloat source)
		{
			this.m_Min = source.m_Min;
			this.m_Max = source.m_Max;
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x00088BD1 File Offset: 0x00086DD1
		public NKMMinMaxFloat(float fMin = 0f, float fMax = 0f)
		{
			this.m_Min = fMin;
			this.m_Max = fMax;
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x00088BE7 File Offset: 0x00086DE7
		public float GetRandom()
		{
			if (this.m_Min.IsNearlyEqual(this.m_Max, 1E-05f))
			{
				return this.m_Max;
			}
			return NKMRandom.Range(this.m_Min, this.m_Max);
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x00088C1C File Offset: 0x00086E1C
		public bool LoadFromLua(NKMLua cNKMLua, string pKey)
		{
			if (cNKMLua.OpenTable(pKey))
			{
				cNKMLua.GetData(1, ref this.m_Min);
				cNKMLua.GetData(2, ref this.m_Max);
				cNKMLua.CloseTable();
			}
			else if (cNKMLua.GetData(pKey, ref this.m_Min))
			{
				this.m_Max = this.m_Min;
			}
			if (this.m_Min > this.m_Max)
			{
				Log.Error("m_Min > m_Max Key: " + pKey, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMinMax.cs", 55);
				this.m_Max = this.m_Min;
				return false;
			}
			return true;
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x00088CA8 File Offset: 0x00086EA8
		public bool LoadFromLua(NKMLua cNKMLua, int index)
		{
			if (cNKMLua.OpenTable(index))
			{
				cNKMLua.GetData(1, ref this.m_Min);
				cNKMLua.GetData(2, ref this.m_Max);
				cNKMLua.CloseTable();
			}
			else if (cNKMLua.GetData(index, ref this.m_Min))
			{
				this.m_Max = this.m_Min;
			}
			if (this.m_Min > this.m_Max)
			{
				Log.Error("m_Min > m_Max Index: " + index.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMinMax.cs", 81);
				this.m_Max = this.m_Min;
				return false;
			}
			return true;
		}

		// Token: 0x04001C95 RID: 7317
		public float m_Min;

		// Token: 0x04001C96 RID: 7318
		public float m_Max;
	}
}
