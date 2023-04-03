using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Service
{
	// Token: 0x02000D53 RID: 3411
	[PacketId(ClientPacketId.kNKMPacket_MESSAGE_NOT)]
	public sealed class NKMPacket_MESSAGE_NOT : ISerializable
	{
		// Token: 0x060095A3 RID: 38307 RVA: 0x0032A5CE File Offset: 0x003287CE
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.message);
		}

		// Token: 0x04008748 RID: 34632
		public List<string> message = new List<string>();
	}
}
