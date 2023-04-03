using System;
using Cs.Logging;
using Cs.Math;

namespace NKM
{
	// Token: 0x02000431 RID: 1073
	public class NKMMinMaxVec3
	{
		// Token: 0x06001D41 RID: 7489 RVA: 0x000891EF File Offset: 0x000873EF
		public void SetMinMax(float fMinX, float fMaxX, float fMinY, float fMaxY, float fMinZ, float fMaxZ)
		{
			this.m_MinX = fMinX;
			this.m_MaxX = fMaxX;
			this.m_MinY = fMinY;
			this.m_MaxY = fMaxY;
			this.m_MinZ = fMinZ;
			this.m_MaxZ = fMaxZ;
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x00089220 File Offset: 0x00087420
		public void DeepCopyFromSource(NKMMinMaxVec3 source)
		{
			this.m_MinX = source.m_MinX;
			this.m_MaxX = source.m_MaxX;
			this.m_MinY = source.m_MinY;
			this.m_MaxY = source.m_MaxY;
			this.m_MinZ = source.m_MinZ;
			this.m_MaxZ = source.m_MaxZ;
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x00089275 File Offset: 0x00087475
		public NKMMinMaxVec3(float fMinX = 0f, float fMaxX = 0f, float fMinY = 0f, float fMaxY = 0f, float fMinZ = 0f, float fMaxZ = 0f)
		{
			this.m_MinX = fMinX;
			this.m_MaxX = fMaxX;
			this.m_MinY = fMinY;
			this.m_MaxY = fMaxY;
			this.m_MinZ = fMinZ;
			this.m_MaxZ = fMaxZ;
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x000892AA File Offset: 0x000874AA
		public float GetRandomX()
		{
			if (this.m_MinX.IsNearlyEqual(this.m_MaxX, 1E-05f))
			{
				return this.m_MaxX;
			}
			return NKMRandom.Range(this.m_MinX, this.m_MaxX);
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x000892DC File Offset: 0x000874DC
		public float GetRandomY()
		{
			if (this.m_MinY.IsNearlyEqual(this.m_MaxY, 1E-05f))
			{
				return this.m_MaxY;
			}
			return NKMRandom.Range(this.m_MinY, this.m_MaxY);
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x0008930E File Offset: 0x0008750E
		public float GetRandomZ()
		{
			if (this.m_MinZ.IsNearlyEqual(this.m_MaxZ, 1E-05f))
			{
				return this.m_MaxZ;
			}
			return NKMRandom.Range(this.m_MinZ, this.m_MaxZ);
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x00089340 File Offset: 0x00087540
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
				if (cNKMLua.OpenTable(3))
				{
					cNKMLua.GetData(1, ref this.m_MinZ);
					cNKMLua.GetData(2, ref this.m_MaxZ);
					cNKMLua.CloseTable();
				}
				else
				{
					float minZ = this.m_MinZ;
					cNKMLua.GetData(3, ref minZ);
					this.m_MinZ = minZ;
					this.m_MaxZ = minZ;
				}
				cNKMLua.CloseTable();
			}
			if (this.m_MinX > this.m_MaxX)
			{
				Log.Error("m_MinX > m_MaxX Key: " + pKey, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMinMax.cs", 464);
				this.m_MaxX = this.m_MinX;
				return false;
			}
			if (this.m_MinY > this.m_MaxY)
			{
				Log.Error("m_MinY > m_MaxY Key: " + pKey, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMinMax.cs", 470);
				this.m_MaxY = this.m_MinY;
				return false;
			}
			if (this.m_MinZ > this.m_MaxZ)
			{
				Log.Error("m_MinZ > m_MaxZ Key: " + pKey, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMinMax.cs", 476);
				this.m_MaxZ = this.m_MinZ;
				return false;
			}
			return true;
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x000894EC File Offset: 0x000876EC
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
				if (cNKMLua.OpenTable(3))
				{
					cNKMLua.GetData(1, ref this.m_MinZ);
					cNKMLua.GetData(2, ref this.m_MaxZ);
					cNKMLua.CloseTable();
				}
				else
				{
					float minZ = this.m_MinZ;
					cNKMLua.GetData(3, ref minZ);
					this.m_MinZ = minZ;
					this.m_MaxZ = minZ;
				}
				cNKMLua.CloseTable();
			}
			if (this.m_MinX > this.m_MaxX)
			{
				Log.Error("m_MinX > m_MaxX index: " + index.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMinMax.cs", 539);
				this.m_MaxX = this.m_MinX;
				return false;
			}
			if (this.m_MinY > this.m_MaxY)
			{
				Log.Error("m_MinY > m_MaxY index: " + index.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMinMax.cs", 545);
				this.m_MaxY = this.m_MinY;
				return false;
			}
			if (this.m_MinZ > this.m_MaxZ)
			{
				Log.Error("m_MinZ > m_MaxZ index: " + index.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMinMax.cs", 551);
				this.m_MaxZ = this.m_MinZ;
				return false;
			}
			return true;
		}

		// Token: 0x04001C9D RID: 7325
		public float m_MinX;

		// Token: 0x04001C9E RID: 7326
		public float m_MaxX;

		// Token: 0x04001C9F RID: 7327
		public float m_MinY;

		// Token: 0x04001CA0 RID: 7328
		public float m_MaxY;

		// Token: 0x04001CA1 RID: 7329
		public float m_MinZ;

		// Token: 0x04001CA2 RID: 7330
		public float m_MaxZ;
	}
}
