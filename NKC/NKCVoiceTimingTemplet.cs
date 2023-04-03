using System;
using NKM;

namespace NKC
{
	// Token: 0x020006F8 RID: 1784
	public class NKCVoiceTimingTemplet
	{
		// Token: 0x060045DF RID: 17887 RVA: 0x00153424 File Offset: 0x00151624
		public bool LoadLUA(NKMLua lua)
		{
			lua.GetData("Index", ref this.Index);
			lua.GetData<VOICE_TYPE>("Type", ref this.VoiceType);
			lua.GetData<VOICE_CONDITION>("Condition", ref this.VoiceCondition);
			lua.GetData("FileName", ref this.FileName);
			lua.GetData("VoiceStartTime", ref this.VoiceStartTime);
			lua.GetData("Unit_ID", ref this.UnitId);
			lua.GetData("Skin_ID", ref this.SkinId);
			return true;
		}

		// Token: 0x04003740 RID: 14144
		public int Index;

		// Token: 0x04003741 RID: 14145
		public int UnitId;

		// Token: 0x04003742 RID: 14146
		public int SkinId;

		// Token: 0x04003743 RID: 14147
		public VOICE_TYPE VoiceType;

		// Token: 0x04003744 RID: 14148
		public VOICE_CONDITION VoiceCondition;

		// Token: 0x04003745 RID: 14149
		public string FileName = "";

		// Token: 0x04003746 RID: 14150
		public float VoiceStartTime;
	}
}
