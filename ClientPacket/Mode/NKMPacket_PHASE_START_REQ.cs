using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E4E RID: 3662
	[PacketId(ClientPacketId.kNKMPacket_PHASE_START_REQ)]
	public sealed class NKMPacket_PHASE_START_REQ : ISerializable
	{
		// Token: 0x0600978C RID: 38796 RVA: 0x0032D1AF File Offset: 0x0032B3AF
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.stageId);
			stream.PutOrGet<NKMDeckIndex>(ref this.deckIndex);
			stream.PutOrGet<NKMEventDeckData>(ref this.eventDeckData);
		}

		// Token: 0x040089B4 RID: 35252
		public int stageId;

		// Token: 0x040089B5 RID: 35253
		public NKMDeckIndex deckIndex;

		// Token: 0x040089B6 RID: 35254
		public NKMEventDeckData eventDeckData;
	}
}
