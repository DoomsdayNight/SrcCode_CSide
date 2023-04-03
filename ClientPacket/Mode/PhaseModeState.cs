using System;
using Cs.Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E4D RID: 3661
	public sealed class PhaseModeState : ISerializable
	{
		// Token: 0x0600978A RID: 38794 RVA: 0x0032D175 File Offset: 0x0032B375
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.stageId);
			stream.PutOrGet(ref this.phaseIndex);
			stream.PutOrGet(ref this.dungeonId);
			stream.PutOrGet(ref this.totalPlayTime);
		}

		// Token: 0x040089B0 RID: 35248
		public int stageId;

		// Token: 0x040089B1 RID: 35249
		public int phaseIndex;

		// Token: 0x040089B2 RID: 35250
		public int dungeonId;

		// Token: 0x040089B3 RID: 35251
		public float totalPlayTime;
	}
}
