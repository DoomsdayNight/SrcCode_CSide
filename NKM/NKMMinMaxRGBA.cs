using System;
using Cs.Logging;
using Cs.Math;

namespace NKM
{
	// Token: 0x02000432 RID: 1074
	public class NKMMinMaxRGBA
	{
		// Token: 0x06001D49 RID: 7497 RVA: 0x000896A8 File Offset: 0x000878A8
		public void SetMinMax(float fMinR, float fMaxR, float fMinG, float fMaxG, float fMinB, float fMaxB, float fMinA, float fMaxA)
		{
			this.m_MinR = fMinR;
			this.m_MaxR = fMaxR;
			this.m_MinG = fMinG;
			this.m_MaxG = fMaxG;
			this.m_MinB = fMinB;
			this.m_MaxB = fMaxB;
			this.m_MinA = fMinA;
			this.m_MaxA = fMaxA;
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x000896E8 File Offset: 0x000878E8
		public void DeepCopyFromSource(NKMMinMaxRGBA source)
		{
			this.m_MinR = source.m_MinR;
			this.m_MaxR = source.m_MaxR;
			this.m_MinG = source.m_MinG;
			this.m_MaxG = source.m_MaxG;
			this.m_MinB = source.m_MinB;
			this.m_MaxB = source.m_MaxB;
			this.m_MinA = source.m_MinA;
			this.m_MaxA = source.m_MaxA;
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x00089758 File Offset: 0x00087958
		public NKMMinMaxRGBA(float fMinR = 1f, float fMaxR = 1f, float fMinG = 1f, float fMaxG = 1f, float fMinB = 1f, float fMaxB = 1f, float fMinA = 1f, float fMaxA = 1f)
		{
			this.m_MinR = fMinR;
			this.m_MaxR = fMaxR;
			this.m_MinG = fMinG;
			this.m_MaxG = fMaxG;
			this.m_MinB = fMinB;
			this.m_MaxB = fMaxB;
			this.m_MinA = fMinA;
			this.m_MaxA = fMaxA;
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x000897A8 File Offset: 0x000879A8
		public float GetRandomR()
		{
			if (this.m_MinR.IsNearlyEqual(this.m_MaxR, 1E-05f))
			{
				return this.m_MaxR;
			}
			return NKMRandom.Range(this.m_MinR, this.m_MaxR);
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x000897DA File Offset: 0x000879DA
		public float GetRandomG()
		{
			if (this.m_MinG.IsNearlyEqual(this.m_MaxG, 1E-05f))
			{
				return this.m_MaxG;
			}
			return NKMRandom.Range(this.m_MinG, this.m_MaxG);
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x0008980C File Offset: 0x00087A0C
		public float GetRandomB()
		{
			if (this.m_MinB.IsNearlyEqual(this.m_MaxB, 1E-05f))
			{
				return this.m_MaxB;
			}
			return NKMRandom.Range(this.m_MinB, this.m_MaxB);
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x0008983E File Offset: 0x00087A3E
		public float GetRandomA()
		{
			if (this.m_MinA.IsNearlyEqual(this.m_MaxA, 1E-05f))
			{
				return this.m_MaxA;
			}
			return NKMRandom.Range(this.m_MinA, this.m_MaxA);
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x00089870 File Offset: 0x00087A70
		public bool LoadFromLua(NKMLua cNKMLua, string pKey)
		{
			if (cNKMLua.OpenTable(pKey))
			{
				if (cNKMLua.OpenTable(1))
				{
					cNKMLua.GetData(1, ref this.m_MinR);
					cNKMLua.GetData(2, ref this.m_MaxR);
					cNKMLua.CloseTable();
				}
				else
				{
					float minR = this.m_MinR;
					cNKMLua.GetData(1, ref minR);
					this.m_MinR = minR;
					this.m_MaxR = minR;
				}
				if (cNKMLua.OpenTable(2))
				{
					cNKMLua.GetData(1, ref this.m_MinG);
					cNKMLua.GetData(2, ref this.m_MaxG);
					cNKMLua.CloseTable();
				}
				else
				{
					float minG = this.m_MinG;
					cNKMLua.GetData(2, ref minG);
					this.m_MinG = minG;
					this.m_MaxG = minG;
				}
				if (cNKMLua.OpenTable(3))
				{
					cNKMLua.GetData(1, ref this.m_MinB);
					cNKMLua.GetData(2, ref this.m_MaxB);
					cNKMLua.CloseTable();
				}
				else
				{
					float minB = this.m_MinB;
					cNKMLua.GetData(3, ref minB);
					this.m_MinB = minB;
					this.m_MaxB = minB;
				}
				if (cNKMLua.OpenTable(4))
				{
					cNKMLua.GetData(1, ref this.m_MinA);
					cNKMLua.GetData(2, ref this.m_MaxA);
					cNKMLua.CloseTable();
				}
				else
				{
					float minA = this.m_MinA;
					cNKMLua.GetData(4, ref minA);
					this.m_MinA = minA;
					this.m_MaxA = minA;
				}
				cNKMLua.CloseTable();
			}
			if (this.m_MinR > this.m_MaxR)
			{
				Log.Error("m_MinR > m_MaxR Key: " + pKey, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMinMax.cs", 726);
				this.m_MaxR = this.m_MinR;
				return false;
			}
			if (this.m_MinG > this.m_MaxG)
			{
				Log.Error("m_MinG > m_MaxG Key: " + pKey, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMinMax.cs", 732);
				this.m_MaxG = this.m_MinG;
				return false;
			}
			if (this.m_MinB > this.m_MaxB)
			{
				Log.Error("m_MinB > m_MaxB Key: " + pKey, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMinMax.cs", 738);
				this.m_MaxB = this.m_MinB;
				return false;
			}
			if (this.m_MinA > this.m_MaxA)
			{
				Log.Error("m_MinA > m_MaxA Key: " + pKey, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMinMax.cs", 744);
				this.m_MaxA = this.m_MinA;
				return false;
			}
			return true;
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x00089AA0 File Offset: 0x00087CA0
		public bool LoadFromLua(NKMLua cNKMLua, int index)
		{
			if (cNKMLua.OpenTable(index))
			{
				if (cNKMLua.OpenTable(1))
				{
					cNKMLua.GetData(1, ref this.m_MinR);
					cNKMLua.GetData(2, ref this.m_MaxR);
					cNKMLua.CloseTable();
				}
				else
				{
					float minR = this.m_MinR;
					cNKMLua.GetData(1, ref minR);
					this.m_MinR = minR;
					this.m_MaxR = minR;
				}
				if (cNKMLua.OpenTable(2))
				{
					cNKMLua.GetData(1, ref this.m_MinG);
					cNKMLua.GetData(2, ref this.m_MaxG);
					cNKMLua.CloseTable();
				}
				else
				{
					float minG = this.m_MinG;
					cNKMLua.GetData(2, ref minG);
					this.m_MinG = minG;
					this.m_MaxG = minG;
				}
				if (cNKMLua.OpenTable(3))
				{
					cNKMLua.GetData(1, ref this.m_MinB);
					cNKMLua.GetData(2, ref this.m_MaxB);
					cNKMLua.CloseTable();
				}
				else
				{
					float minB = this.m_MinB;
					cNKMLua.GetData(3, ref minB);
					this.m_MinB = minB;
					this.m_MaxB = minB;
				}
				if (cNKMLua.OpenTable(4))
				{
					cNKMLua.GetData(1, ref this.m_MinA);
					cNKMLua.GetData(2, ref this.m_MaxA);
					cNKMLua.CloseTable();
				}
				else
				{
					float minA = this.m_MinA;
					cNKMLua.GetData(4, ref minA);
					this.m_MinA = minA;
					this.m_MaxA = minA;
				}
				cNKMLua.CloseTable();
			}
			if (this.m_MinR > this.m_MaxR)
			{
				Log.Error("m_MinR > m_MaxR index: " + index.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMinMax.cs", 821);
				this.m_MaxR = this.m_MinR;
				return false;
			}
			if (this.m_MinG > this.m_MaxG)
			{
				Log.Error("m_MinG > m_MaxG index: " + index.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMinMax.cs", 827);
				this.m_MaxG = this.m_MinG;
				return false;
			}
			if (this.m_MinB > this.m_MaxB)
			{
				Log.Error("m_MinB > m_MaxB index: " + index.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMinMax.cs", 833);
				this.m_MaxB = this.m_MinB;
				return false;
			}
			if (this.m_MinA > this.m_MaxA)
			{
				Log.Error("m_MinA > m_MaxA index: " + index.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMinMax.cs", 839);
				this.m_MaxA = this.m_MinA;
				return false;
			}
			return true;
		}

		// Token: 0x04001CA3 RID: 7331
		public float m_MinR;

		// Token: 0x04001CA4 RID: 7332
		public float m_MaxR;

		// Token: 0x04001CA5 RID: 7333
		public float m_MinG;

		// Token: 0x04001CA6 RID: 7334
		public float m_MaxG;

		// Token: 0x04001CA7 RID: 7335
		public float m_MinB;

		// Token: 0x04001CA8 RID: 7336
		public float m_MaxB;

		// Token: 0x04001CA9 RID: 7337
		public float m_MinA;

		// Token: 0x04001CAA RID: 7338
		public float m_MaxA;
	}
}
