using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E55 RID: 3669
	public sealed class TrimModeState : ISerializable
	{
		// Token: 0x0600979A RID: 38810 RVA: 0x0032D2E1 File Offset: 0x0032B4E1
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.trimId);
			stream.PutOrGet(ref this.trimLevel);
			stream.PutOrGet(ref this.nextDungeonId);
			stream.PutOrGet<NKMTrimStageData>(ref this.lastClearStage);
			stream.PutOrGet<NKMTrimStageData>(ref this.stageList);
		}

		// Token: 0x040089C4 RID: 35268
		public int trimId;

		// Token: 0x040089C5 RID: 35269
		public int trimLevel;

		// Token: 0x040089C6 RID: 35270
		public int nextDungeonId;

		// Token: 0x040089C7 RID: 35271
		public NKMTrimStageData lastClearStage = new NKMTrimStageData();

		// Token: 0x040089C8 RID: 35272
		public List<NKMTrimStageData> stageList = new List<NKMTrimStageData>();
	}
}
