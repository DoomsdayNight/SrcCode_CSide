using System;
using Cs.Logging;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000696 RID: 1686
	public class NKCLoginCutSceneTemplet : INKMTempletEx, INKMTemplet
	{
		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x0600376C RID: 14188 RVA: 0x0011D963 File Offset: 0x0011BB63
		public int Key
		{
			get
			{
				return this.m_Key;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x0600376D RID: 14189 RVA: 0x0011D96B File Offset: 0x0011BB6B
		// (set) Token: 0x0600376E RID: 14190 RVA: 0x0011D973 File Offset: 0x0011BB73
		public NKMIntervalTemplet IntervalTemplet { get; private set; } = NKMIntervalTemplet.Invalid;

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x0600376F RID: 14191 RVA: 0x0011D97C File Offset: 0x0011BB7C
		public DateTime StartDateUTC
		{
			get
			{
				return NKMTime.LocalToUTC(this.IntervalTemplet.StartDate, 0);
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06003770 RID: 14192 RVA: 0x0011D98F File Offset: 0x0011BB8F
		public DateTime EndDateUTC
		{
			get
			{
				return NKMTime.LocalToUTC(this.IntervalTemplet.EndDate, 0);
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06003771 RID: 14193 RVA: 0x0011D9A2 File Offset: 0x0011BBA2
		public bool HasDateLimit
		{
			get
			{
				return this.IntervalTemplet.IsValid;
			}
		}

		// Token: 0x06003772 RID: 14194 RVA: 0x0011D9AF File Offset: 0x0011BBAF
		public static NKCLoginCutSceneTemplet Find(int key)
		{
			return NKMTempletContainer<NKCLoginCutSceneTemplet>.Find(key);
		}

		// Token: 0x06003773 RID: 14195 RVA: 0x0011D9B8 File Offset: 0x0011BBB8
		public static NKCLoginCutSceneTemplet LoadFromLUA(NKMLua lua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(lua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCLoginCutSceneManager.cs", 46))
			{
				return null;
			}
			NKCLoginCutSceneTemplet nkcloginCutSceneTemplet = new NKCLoginCutSceneTemplet();
			bool flag = true & lua.GetData("m_Key", ref nkcloginCutSceneTemplet.m_Key) & lua.GetData("m_CutSceneStrID", ref nkcloginCutSceneTemplet.m_CutSceneStrID) & lua.GetData("m_DateStrID", ref nkcloginCutSceneTemplet.dateStrId);
			lua.GetData<EventUnlockCond>("m_CondType", ref nkcloginCutSceneTemplet.m_CondType);
			lua.GetData("m_CondValue", ref nkcloginCutSceneTemplet.m_CondValue);
			if (!flag)
			{
				Debug.LogError("NKCLoginCutSceneTemplet LoadFromLUA fail");
				return null;
			}
			return nkcloginCutSceneTemplet;
		}

		// Token: 0x06003774 RID: 14196 RVA: 0x0011DA46 File Offset: 0x0011BC46
		public void Join()
		{
			if (NKMUtil.IsServer)
			{
				this.JoinIntervalTemplet();
			}
		}

		// Token: 0x06003775 RID: 14197 RVA: 0x0011DA58 File Offset: 0x0011BC58
		public void JoinIntervalTemplet()
		{
			this.IntervalTemplet = NKMIntervalTemplet.Find(this.dateStrId);
			if (this.IntervalTemplet == null)
			{
				this.IntervalTemplet = NKMIntervalTemplet.Unuseable;
				Log.ErrorAndExit("잘못된 interval id :" + this.dateStrId, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCLoginCutSceneManager.cs", 79);
				return;
			}
			if (this.IntervalTemplet.IsRepeatDate)
			{
				Log.ErrorAndExit(string.Format("[LoginCutscene:{0}] 반복 기간설정 사용 불가. id:{1}", this.Key, this.dateStrId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCLoginCutSceneManager.cs", 85);
			}
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x0011DADA File Offset: 0x0011BCDA
		public void PostJoin()
		{
			this.JoinIntervalTemplet();
		}

		// Token: 0x06003777 RID: 14199 RVA: 0x0011DAE2 File Offset: 0x0011BCE2
		public void Validate()
		{
		}

		// Token: 0x0400342A RID: 13354
		private string dateStrId;

		// Token: 0x0400342B RID: 13355
		public int m_Key;

		// Token: 0x0400342C RID: 13356
		public string m_CutSceneStrID;

		// Token: 0x0400342D RID: 13357
		public EventUnlockCond m_CondType;

		// Token: 0x0400342E RID: 13358
		public string m_CondValue;
	}
}
