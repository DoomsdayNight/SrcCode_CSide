using System;
using NKM;
using NKM.Templet.Base;

namespace NKC.Templet
{
	// Token: 0x02000844 RID: 2116
	public class NKCCollectionVoiceTemplet : INKMTemplet
	{
		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x06005449 RID: 21577 RVA: 0x0019BACE File Offset: 0x00199CCE
		public int Key
		{
			get
			{
				return this.IDX;
			}
		}

		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x0600544A RID: 21578 RVA: 0x0019BAD6 File Offset: 0x00199CD6
		public string ButtonName
		{
			get
			{
				return NKCStringTable.GetString(this.m_VoiceButtonName, false);
			}
		}

		// Token: 0x0600544B RID: 21579 RVA: 0x0019BAE4 File Offset: 0x00199CE4
		public static NKCCollectionVoiceTemplet Find(int idx)
		{
			return NKMTempletContainer<NKCCollectionVoiceTemplet>.Find(idx);
		}

		// Token: 0x0600544C RID: 21580 RVA: 0x0019BAEC File Offset: 0x00199CEC
		public static NKCCollectionVoiceTemplet LoadLua(NKMLua lua)
		{
			NKCCollectionVoiceTemplet nkccollectionVoiceTemplet = new NKCCollectionVoiceTemplet();
			bool flag = true & lua.GetData("IDX", ref nkccollectionVoiceTemplet.IDX);
			lua.GetData<VOICE_TYPE>("m_VoiceType", ref nkccollectionVoiceTemplet.m_VoiceType);
			lua.GetData("m_bVoiceCondLifetime", ref nkccollectionVoiceTemplet.m_bVoiceCondLifetime);
			lua.GetData("m_VoicePostID", ref nkccollectionVoiceTemplet.m_VoicePostID);
			lua.GetData("m_VoiceButtonName", ref nkccollectionVoiceTemplet.m_VoiceButtonName);
			lua.GetData<NKC_VOICE_TYPE>("m_VoiceCategory", ref nkccollectionVoiceTemplet.m_VoiceCategory);
			if (!flag)
			{
				return null;
			}
			return nkccollectionVoiceTemplet;
		}

		// Token: 0x0600544D RID: 21581 RVA: 0x0019BB71 File Offset: 0x00199D71
		public void Join()
		{
		}

		// Token: 0x0600544E RID: 21582 RVA: 0x0019BB73 File Offset: 0x00199D73
		public void Validate()
		{
		}

		// Token: 0x04004344 RID: 17220
		public int IDX;

		// Token: 0x04004345 RID: 17221
		public VOICE_TYPE m_VoiceType;

		// Token: 0x04004346 RID: 17222
		public bool m_bVoiceCondLifetime;

		// Token: 0x04004347 RID: 17223
		public string m_VoicePostID;

		// Token: 0x04004348 RID: 17224
		private string m_VoiceButtonName;

		// Token: 0x04004349 RID: 17225
		public NKC_VOICE_TYPE m_VoiceCategory = NKC_VOICE_TYPE.ETC;
	}
}
