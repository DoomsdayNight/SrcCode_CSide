using System;
using System.Collections.Generic;
using NKM;

namespace NKC
{
	// Token: 0x02000664 RID: 1636
	public class NKCCutScenTemplet
	{
		// Token: 0x06003367 RID: 13159 RVA: 0x00101F80 File Offset: 0x00100180
		public bool LoadFromLUA(NKMLua cNKMLua, int index)
		{
			cNKMLua.GetData("m_CutScenID", ref this.m_CutScenID);
			cNKMLua.GetData("m_CutScenStrID", ref this.m_CutScenStrID);
			NKCCutTemplet nkccutTemplet = new NKCCutTemplet();
			nkccutTemplet.LoadFromLUA(cNKMLua, this.m_CutScenStrID, index);
			this.m_listCutTemplet.Add(nkccutTemplet);
			return true;
		}

		// Token: 0x06003368 RID: 13160 RVA: 0x00101FD3 File Offset: 0x001001D3
		public void AddCutTemplet(NKCCutScenTemplet cNKCCutScenTemplet)
		{
			if (cNKCCutScenTemplet != null && cNKCCutScenTemplet.m_listCutTemplet.Count > 0)
			{
				this.m_listCutTemplet.Add(cNKCCutScenTemplet.m_listCutTemplet[0]);
			}
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x00102000 File Offset: 0x00100200
		public string GetLastMusicAssetName()
		{
			for (int i = this.m_listCutTemplet.Count - 1; i >= 0; i--)
			{
				NKCCutTemplet nkccutTemplet = this.m_listCutTemplet[i];
				if (nkccutTemplet != null)
				{
					if (nkccutTemplet.m_EndBGMFileName.Length > 0)
					{
						return nkccutTemplet.m_EndBGMFileName;
					}
					if (nkccutTemplet.m_StartBGMFileName.Length > 0)
					{
						return nkccutTemplet.m_StartBGMFileName;
					}
					if (nkccutTemplet.m_Action == NKCCutTemplet.eCutsceneAction.PLAY_MUSIC)
					{
						string actionFirstToken = nkccutTemplet.GetActionFirstToken();
						if (!string.IsNullOrEmpty(actionFirstToken))
						{
							return actionFirstToken;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x04003239 RID: 12857
		public int m_CutScenID;

		// Token: 0x0400323A RID: 12858
		public string m_CutScenStrID = "";

		// Token: 0x0400323B RID: 12859
		public List<NKCCutTemplet> m_listCutTemplet = new List<NKCCutTemplet>();
	}
}
