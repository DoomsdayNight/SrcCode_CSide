using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F89 RID: 3977
	[PacketId(ClientPacketId.kNKMPacket_EVENT_PASS_LEVEL_UP_REQ)]
	public sealed class NKMPacket_EVENT_PASS_LEVEL_UP_REQ : ISerializable
	{
		// Token: 0x060099EE RID: 39406 RVA: 0x00330B15 File Offset: 0x0032ED15
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.increaseLv);
		}

		// Token: 0x04008D0B RID: 36107
		public int increaseLv = 1;
	}
}
