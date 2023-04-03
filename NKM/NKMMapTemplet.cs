using System;
using System.Collections.Generic;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200042B RID: 1067
	public class NKMMapTemplet : INKMTemplet
	{
		// Token: 0x06001D06 RID: 7430 RVA: 0x00087391 File Offset: 0x00085591
		public float GetUnitMinX()
		{
			return this.m_fMinX + 5f;
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x0008739F File Offset: 0x0008559F
		public float GetUnitMaxX()
		{
			return this.m_fMaxX - 5f;
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06001D08 RID: 7432 RVA: 0x000873AD File Offset: 0x000855AD
		public int Key
		{
			get
			{
				return this.m_MapID;
			}
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x000873B8 File Offset: 0x000855B8
		public static NKMMapTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			NKMMapTemplet nkmmapTemplet = new NKMMapTemplet();
			cNKMLua.GetData("m_MapID", ref nkmmapTemplet.m_MapID);
			cNKMLua.GetData("m_MapStrID", ref nkmmapTemplet.m_MapStrID);
			if (!nkmmapTemplet.m_MapAssetName.LoadFromLua(cNKMLua, "m_MapAssetName"))
			{
				nkmmapTemplet.m_MapAssetName.m_BundleName = nkmmapTemplet.m_MapStrID;
				nkmmapTemplet.m_MapAssetName.m_AssetName = nkmmapTemplet.m_MapStrID;
			}
			cNKMLua.GetData("m_bUsePVP", ref nkmmapTemplet.m_bUsePVP);
			cNKMLua.GetData("m_MusicName", ref nkmmapTemplet.m_MusicName);
			nkmmapTemplet.m_PVPMusicName = "BATTLE_NORMAL_01";
			cNKMLua.GetData("m_PVPMusicName", ref nkmmapTemplet.m_PVPMusicName);
			cNKMLua.GetData("m_AmbientName", ref nkmmapTemplet.m_AmbientName);
			cNKMLua.GetData("m_fInitPosX", ref nkmmapTemplet.m_fInitPosX);
			cNKMLua.GetData("m_fCamMinX", ref nkmmapTemplet.m_fCamMinX);
			cNKMLua.GetData("m_fCamMaxX", ref nkmmapTemplet.m_fCamMaxX);
			cNKMLua.GetData("m_fCamMinY", ref nkmmapTemplet.m_fCamMinY);
			cNKMLua.GetData("m_fCamMaxY", ref nkmmapTemplet.m_fCamMaxY);
			cNKMLua.GetData("m_fCamSize", ref nkmmapTemplet.m_fCamSize);
			cNKMLua.GetData("m_fCamSizeMax", ref nkmmapTemplet.m_fCamSizeMax);
			cNKMLua.GetData("m_fMinX", ref nkmmapTemplet.m_fMinX);
			cNKMLua.GetData("m_fMaxX", ref nkmmapTemplet.m_fMaxX);
			cNKMLua.GetData("m_fMinZ", ref nkmmapTemplet.m_fMinZ);
			cNKMLua.GetData("m_fMaxZ", ref nkmmapTemplet.m_fMaxZ);
			if (cNKMLua.OpenTable("m_listBloomPoint"))
			{
				int num = 1;
				while (cNKMLua.OpenTable(num))
				{
					NKMBloomPoint nkmbloomPoint;
					if (nkmmapTemplet.m_listBloomPoint.Count < num)
					{
						nkmbloomPoint = new NKMBloomPoint();
						nkmmapTemplet.m_listBloomPoint.Add(nkmbloomPoint);
					}
					else
					{
						nkmbloomPoint = nkmmapTemplet.m_listBloomPoint[num - 1];
					}
					nkmbloomPoint.LoadFromLUA(cNKMLua);
					num++;
					cNKMLua.CloseTable();
				}
				cNKMLua.CloseTable();
			}
			if (cNKMLua.OpenTable("m_listMapLayer"))
			{
				int num2 = 1;
				while (cNKMLua.OpenTable(num2))
				{
					NKMMapLayer nkmmapLayer;
					if (nkmmapTemplet.m_listMapLayer.Count < num2)
					{
						nkmmapLayer = new NKMMapLayer();
						nkmmapTemplet.m_listMapLayer.Add(nkmmapLayer);
					}
					else
					{
						nkmmapLayer = nkmmapTemplet.m_listMapLayer[num2 - 1];
					}
					nkmmapLayer.LoadFromLUA(cNKMLua);
					num2++;
					cNKMLua.CloseTable();
				}
				cNKMLua.CloseTable();
			}
			return nkmmapTemplet;
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x00087611 File Offset: 0x00085811
		public bool IsValidLand(float fX, float fZ)
		{
			return this.m_fMinX <= fX && this.m_fMaxX >= fX && this.m_fMinZ <= fZ && this.m_fMaxZ >= fZ;
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x0008763C File Offset: 0x0008583C
		public bool IsValidLandX(float fX, bool bTeamA, float fFactor)
		{
			float num = this.m_fMinX;
			float num2 = this.m_fMaxX;
			float num3 = num2 - num;
			if (bTeamA)
			{
				num2 = num + num3 * fFactor;
			}
			else
			{
				num = num2 - num3 * fFactor;
			}
			return num <= fX && num2 >= fX;
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x00087678 File Offset: 0x00085878
		public float GetNearLandX(float fX, bool bTeamA, float fFactor, float minOffset)
		{
			float num = this.m_fMinX;
			float num2 = this.m_fMaxX;
			if (bTeamA)
			{
				num2 = num + (num2 - num) * fFactor;
				num += minOffset;
			}
			else
			{
				num = num2 - (num2 - num) * fFactor;
				num2 -= minOffset;
			}
			if (num > fX)
			{
				return num;
			}
			if (num2 < fX)
			{
				return num2;
			}
			return fX;
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x000876C0 File Offset: 0x000858C0
		public float GetMapRatePos(float fMapRate, bool bTeamA)
		{
			float num = this.m_fMaxX - this.m_fMinX;
			if (bTeamA)
			{
				return this.m_fMinX + num * fMapRate;
			}
			return this.m_fMaxX - num * fMapRate;
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x000876F3 File Offset: 0x000858F3
		public float GetNearLandZ(float fZ)
		{
			if (this.m_fMinZ > fZ)
			{
				return this.m_fMinZ;
			}
			if (this.m_fMaxZ < fZ)
			{
				return this.m_fMaxZ;
			}
			return fZ;
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x00087718 File Offset: 0x00085918
		public float GetValidLandX(bool bTeamA, float fFactor)
		{
			float fMinX = this.m_fMinX;
			float fMaxX = this.m_fMaxX;
			if (bTeamA)
			{
				return fMinX + (fMaxX - fMinX) * fFactor;
			}
			return fMaxX - (fMaxX - fMinX) * fFactor;
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x0008774C File Offset: 0x0008594C
		public float ReversePosX(float fPosX)
		{
			float num = this.m_fMaxX - this.m_fMinX;
			float num2 = this.m_fMinX + num * 0.5f;
			if (fPosX > num2)
			{
				return num2 - (fPosX - num2);
			}
			return num2 + (num2 - fPosX);
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x00087788 File Offset: 0x00085988
		public float GetMapFactor(float fPosX, bool bTeamA)
		{
			float num = fPosX - this.m_fMinX;
			float num2 = this.m_fMaxX - this.m_fMinX;
			if (bTeamA)
			{
				return num / num2;
			}
			return 1f - num / num2;
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x000877BC File Offset: 0x000859BC
		public void Join()
		{
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x000877BE File Offset: 0x000859BE
		public void Validate()
		{
		}

		// Token: 0x04001C6E RID: 7278
		public int m_MapID;

		// Token: 0x04001C6F RID: 7279
		public string m_MapStrID = "";

		// Token: 0x04001C70 RID: 7280
		public NKMAssetName m_MapAssetName = new NKMAssetName();

		// Token: 0x04001C71 RID: 7281
		public bool m_bUsePVP;

		// Token: 0x04001C72 RID: 7282
		public string m_MusicName = "";

		// Token: 0x04001C73 RID: 7283
		public string m_PVPMusicName = "";

		// Token: 0x04001C74 RID: 7284
		public string m_AmbientName = "";

		// Token: 0x04001C75 RID: 7285
		public float m_fInitPosX;

		// Token: 0x04001C76 RID: 7286
		public float m_fCamMinX;

		// Token: 0x04001C77 RID: 7287
		public float m_fCamMaxX;

		// Token: 0x04001C78 RID: 7288
		public float m_fCamMinY;

		// Token: 0x04001C79 RID: 7289
		public float m_fCamMaxY;

		// Token: 0x04001C7A RID: 7290
		public float m_fCamSize = 500f;

		// Token: 0x04001C7B RID: 7291
		public float m_fCamSizeMax = 512f;

		// Token: 0x04001C7C RID: 7292
		public float m_fMinX;

		// Token: 0x04001C7D RID: 7293
		public float m_fMaxX;

		// Token: 0x04001C7E RID: 7294
		public float m_fMinZ;

		// Token: 0x04001C7F RID: 7295
		public float m_fMaxZ;

		// Token: 0x04001C80 RID: 7296
		public List<NKMBloomPoint> m_listBloomPoint = new List<NKMBloomPoint>();

		// Token: 0x04001C81 RID: 7297
		public List<NKMMapLayer> m_listMapLayer = new List<NKMMapLayer>();
	}
}
