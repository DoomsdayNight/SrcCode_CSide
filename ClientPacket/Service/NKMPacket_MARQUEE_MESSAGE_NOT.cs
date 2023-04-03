using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Service
{
	// Token: 0x02000D52 RID: 3410
	[PacketId(ClientPacketId.kNKMPacket_MARQUEE_MESSAGE_NOT)]
	public sealed class NKMPacket_MARQUEE_MESSAGE_NOT : ISerializable
	{
		// Token: 0x060095A1 RID: 38305 RVA: 0x0032A5AD File Offset: 0x003287AD
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.message);
		}

		// Token: 0x04008747 RID: 34631
		public List<string> message = new List<string>();
	}
}
