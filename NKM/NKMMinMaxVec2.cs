using System;
using Cs.Logging;
using Cs.Math;

namespace NKM
{
	// Token: 0x02000430 RID: 1072
	public class NKMMinMaxVec2
	{
		// Token: 0x06001D3A RID: 7482 RVA: 0x00088EB7 File Offset: 0x000870B7
		public void SetMinMax(float fMinX, float fMaxX, float fMinY, float fMaxY)
		{
			this.m_MinX = fMinX;
			this.m_MaxX = fMaxX;
			this.m_MinY = fMinY;
			this.m_MaxY = fMaxY;
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x00088ED6 File Offset: 0x000870D6
		public void DeepCopyFromSource(NKMMinMaxVec2 source)
		{
			this.m_MinX = source.m_MinX;
			this.m_MaxX = source.m_MaxX;
			this.m_MinY = source.m_MinY;
			this.m_MaxY = source.m_MaxY;
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x00088F08 File Offset: 0x00087108
		public NKMMinMaxVec2(float fMinX = 0f, float fMaxX = 0f, float fMinY = 0f, float fMaxY = 0f)
		{
			this.m_MinX = fMinX;
			this.m_MaxX = fMaxX;
			this.m_MinY = fMinY;
			this.m_MaxY = fMaxY;
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x00088F2D File Offset: 0x0008712D
		public float GetRandomX()
		{
			if (this.m_MinX.IsNearlyEqual(this.m_MaxX, 1E-05f))
			{
				return this.m_MaxX;
			}
			return NKMRandom.Range(this.m_MinX, this.m_MaxX);
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x00088F5F File Offset: 0x0008715F
		public float GetRandomY()
		{
			if (this.m_MinY.IsNearlyEqual(this.m_MaxY, 1E-05f))
			{
				return this.m_MaxY;
			}
			return NKMRandom.Range(this.m_MinY, this.m_MaxY);
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x00088F94 File Offset: 0x00087194
		public bool LoadFromLua(NKMLua cNKMLua, string pKey)
		{
			if (cNKMLua.OpenTable(pKey))
			{
				if (cNKMLua.OpenTable(1))
				{
					cNKMLua.GetData(1, ref this.m_MinX);
					cNKMLua.GetData(2, ref this.m_MaxX);
					cNKMLua.CloseTable();
				}
				else
				{
					float minX = this.m_MinX;
					cNKMLua.GetData(1, ref minX);
					this.m_MinX = minX;
					this.m_MaxX = minX;
				}
				if (cNKMLua.OpenTable(2))
				{
					cNKMLua.GetData(1, ref this.m_MinY);
					cNKMLua.GetData(2, ref this.m_MaxY);
					cNKMLua.CloseTable();
				}
				else
				{
					float minY = this.m_MinY;
					cNKMLua.GetData(2, ref minY);
					this.m_MinY = minY;
					this.m_MaxY = minY;
				}
				cNKMLua.CloseTable();
			}
			if (this.m_MinX > this.m_MaxX)
			{
				Log.Error("m_MinX > m_MaxX Key: " + pKey, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMinMax.cs", 264);
				this.m_MaxX = this.m_MinX;
				return false;
			}
			if (this.m_MinY > this.m_MaxY)
			{
				Log.Error("m_MinY > m_MaxY Key: " + pKey, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMinMax.cs", 270);
				this.m_MaxY = this.m_MinY;
				return false;
			}
			return true;
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x000890BC File Offset: 0x000872BC
		public bool LoadFromLua(NKMLua cNKMLua, int index)
		{
			if (cNKMLua.OpenTable(index))
			{
				if (cNKMLua.OpenTable(1))
				{
					cNKMLua.GetData(1, ref this.m_MinX);
					cNKMLua.GetData(2, ref this.m_MaxX);
					cNKMLua.CloseTable();
				}
				else
				{
					float minX = this.m_MinX;
					cNKMLua.GetData(1, ref minX);
					this.m_MinX = minX;
					this.m_MaxX = minX;
				}
				if (cNKMLua.OpenTable(2))
				{
					cNKMLua.GetData(1, ref this.m_MinY);
					cNKMLua.GetData(2, ref this.m_MaxY);
					cNKMLua.CloseTable();
				}
				else
				{
					float minY = this.m_MinY;
					cNKMLua.GetData(2, ref minY);
					this.m_MinY = minY;
					this.m_MaxY = minY;
				}
				cNKMLua.CloseTable();
			}
			if (this.m_MinX > this.m_MaxX)
			{
				Log.Error("m_MinX > m_MaxX index: " + index.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMinMax.cs", 319);
				this.m_MaxX = this.m_MinX;
				return false;
			}
			if (this.m_MinY > this.m_MaxY)
			{
				Log.Error("m_MinY > m_MaxY index: " + index.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMinMax.cs", 325);
				this.m_MaxY = this.m_MinY;
				return false;
			}
			return true;
		}

		// Token: 0x04001C99 RID: 7321
		public float m_MinX;

		// Token: 0x04001C9A RID: 7322
		public float m_MaxX;

		// Token: 0x04001C9B RID: 7323
		public float m_MinY;

		// Token: 0x04001C9C RID: 7324
		public float m_MaxY;
	}
}
