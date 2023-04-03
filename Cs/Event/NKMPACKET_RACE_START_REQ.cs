using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F91 RID: 3985
	[PacketId(ClientPacketId.kNKMPACKET_RACE_START_REQ)]
	public sealed class NKMPACKET_RACE_START_REQ : ISerializable
	{
		// Token: 0x060099FC RID: 39420 RVA: 0x00330C4E File Offset: 0x0032EE4E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.selectLine);
		}

		// Token: 0x04008D1E RID: 36126
		public int selectLine;
	}
}
