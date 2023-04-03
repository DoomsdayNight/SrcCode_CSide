using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet.Base;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006D9 RID: 1753
	public class NKCTutorialReqTemplet : INKMTemplet
	{
		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06003D3C RID: 15676 RVA: 0x0013AD8A File Offset: 0x00138F8A
		public int Key
		{
			get
			{
				return this.EventID;
			}
		}

		// Token: 0x06003D3D RID: 15677 RVA: 0x0013AD94 File Offset: 0x00138F94
		public static NKCTutorialReqTemplet LoadFromLUA(NKMLua lua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(lua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCTutorialManager.cs", 162))
			{
				return null;
			}
			NKCTutorialReqTemplet nkctutorialReqTemplet = new NKCTutorialReqTemplet();
			bool flag = true & lua.GetData("EventID", ref nkctutorialReqTemplet.EventID) & lua.GetData<TutorialStep>("Step", ref nkctutorialReqTemplet.Step) & lua.GetData<TutorialPoint>("EventPoint", ref nkctutorialReqTemplet.EventPoint);
			string empty = string.Empty;
			TutorialReq key;
			if (lua.GetData<TutorialReq>("Req", out key, TutorialReq.None))
			{
				lua.GetData("ReqValue", ref empty);
				nkctutorialReqTemplet.dicReq.Add(key, empty);
			}
			empty = string.Empty;
			if (lua.GetData<TutorialReq>("Req2", out key, TutorialReq.None))
			{
				lua.GetData("Req2Value", ref empty);
				nkctutorialReqTemplet.dicReq.Add(key, empty);
			}
			if (!flag)
			{
				Debug.LogError("NKCTutorialReqTemplet LoadFromLUA fail");
				return null;
			}
			return nkctutorialReqTemplet;
		}

		// Token: 0x06003D3E RID: 15678 RVA: 0x0013AE63 File Offset: 0x00139063
		public void Join()
		{
		}

		// Token: 0x06003D3F RID: 15679 RVA: 0x0013AE65 File Offset: 0x00139065
		public void Validate()
		{
		}

		// Token: 0x04003692 RID: 13970
		public int EventID;

		// Token: 0x04003693 RID: 13971
		public TutorialStep Step;

		// Token: 0x04003694 RID: 13972
		public TutorialPoint EventPoint;

		// Token: 0x04003695 RID: 13973
		public Dictionary<TutorialReq, string> dicReq = new Dictionary<TutorialReq, string>();
	}
}
