using System;
using NKM;

namespace NKC
{
	// Token: 0x020006DE RID: 1758
	public class NKCVoiceTemplet
	{
		// Token: 0x06003D53 RID: 15699 RVA: 0x0013B624 File Offset: 0x00139824
		public bool LoadLUA(NKMLua lua)
		{
			lua.GetData("Index", ref this.Index);
			lua.GetData<VOICE_TYPE>("Type", ref this.Type);
			lua.GetData<VOICE_CONDITION>("Condition", ref this.Condition);
			lua.GetData("ConditionValue", ref this.ConditionValue);
			lua.GetData("Rate", ref this.Rate);
			lua.GetData("Volume", ref this.Volume);
			lua.GetData("FileName", ref this.FileName);
			lua.GetData("NPC", ref this.Npc);
			lua.GetData("Priority", ref this.Priority);
			return true;
		}

		// Token: 0x040036D0 RID: 14032
		public int Index;

		// Token: 0x040036D1 RID: 14033
		public VOICE_TYPE Type;

		// Token: 0x040036D2 RID: 14034
		public VOICE_CONDITION Condition;

		// Token: 0x040036D3 RID: 14035
		public int ConditionValue;

		// Token: 0x040036D4 RID: 14036
		public int Rate = 100;

		// Token: 0x040036D5 RID: 14037
		public int Volume = 100;

		// Token: 0x040036D6 RID: 14038
		public string FileName = "";

		// Token: 0x040036D7 RID: 14039
		public string Npc = "";

		// Token: 0x040036D8 RID: 14040
		public int Priority = int.MaxValue;
	}
}
