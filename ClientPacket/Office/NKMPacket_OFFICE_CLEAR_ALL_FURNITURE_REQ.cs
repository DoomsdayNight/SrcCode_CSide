using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E00 RID: 3584
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_CLEAR_ALL_FURNITURE_REQ)]
	public sealed class NKMPacket_OFFICE_CLEAR_ALL_FURNITURE_REQ : ISerializable
	{
		// Token: 0x060096F4 RID: 38644 RVA: 0x0032C536 File Offset: 0x0032A736
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.roomId);
		}

		// Token: 0x04008909 RID: 35081
		public int roomId;
	}
}
