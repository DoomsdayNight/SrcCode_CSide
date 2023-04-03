using System;
using Cs.Logging;

namespace NKM
{
	// Token: 0x0200042F RID: 1071
	public class NKMMinMaxInt
	{
		// Token: 0x06001D35 RID: 7477 RVA: 0x00088D38 File Offset: 0x00086F38
		public void DeepCopyFromSource(NKMMinMaxInt source)
		{
			this.m_Min = source.m_Min;
			this.m_Max = source.m_Max;
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x00088D52 File Offset: 0x00086F52
		public NKMMinMaxInt(int iMin = 0, int iMax = 0)
		{
			this.m_Min = iMin;
			this.m_Max = iMax;
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x00088D68 File Offset: 0x00086F68
		public int GetRandom()
		{
			if (this.m_Min == this.m_Max)
			{
				return this.m_Max;
			}
			return NKMRandom.Range(this.m_Min, this.m_Max + 1);
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x00088D94 File Offset: 0x00086F94
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
				Log.Error("m_Min > m_Max Key: " + pKey, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMinMax.cs", 135);
				this.m_Max = this.m_Min;
				return false;
			}
			return true;
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x00088E24 File Offset: 0x00087024
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
				Log.Error("m_Min > m_Max Index: " + index.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMinMax.cs", 161);
				this.m_Max = this.m_Min;
				return false;
			}
			return true;
		}

		// Token: 0x04001C97 RID: 7319
		public int m_Min;

		// Token: 0x04001C98 RID: 7320
		public int m_Max;
	}
}
