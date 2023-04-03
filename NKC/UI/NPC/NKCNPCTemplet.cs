using System;
using NKM;

namespace NKC.UI.NPC
{
	// Token: 0x02000BF9 RID: 3065
	public class NKCNPCTemplet
	{
		// Token: 0x06008E14 RID: 36372 RVA: 0x00306848 File Offset: 0x00304A48
		public bool LoadLUA(NKMLua lua)
		{
			bool data = lua.GetData<NPC_ACTION_TYPE>("m_ActionType", ref this.m_ActionType);
			lua.GetData("m_ActionCoolTime", ref this.m_ActionCoolTime);
			lua.GetData("m_AnimationName", ref this.m_AnimationName);
			lua.GetData("m_VoiceFileName", ref this.m_VoiceFileName);
			lua.GetData<NPC_CONDITION>("m_ConditionType", ref this.m_ConditionType);
			lua.GetData("m_ConditionValue", ref this.m_ConditionValue);
			lua.GetData("m_Volume", ref this.m_Volume);
			lua.GetData("m_Text", ref this.m_Text);
			return data;
		}

		// Token: 0x04007B80 RID: 31616
		public NPC_ACTION_TYPE m_ActionType;

		// Token: 0x04007B81 RID: 31617
		public float m_ActionCoolTime;

		// Token: 0x04007B82 RID: 31618
		public string m_AnimationName = "";

		// Token: 0x04007B83 RID: 31619
		public string m_VoiceFileName = "";

		// Token: 0x04007B84 RID: 31620
		public NPC_CONDITION m_ConditionType;

		// Token: 0x04007B85 RID: 31621
		public int m_ConditionValue;

		// Token: 0x04007B86 RID: 31622
		public int m_Volume = 1;

		// Token: 0x04007B87 RID: 31623
		public string m_Text = "";
	}
}
