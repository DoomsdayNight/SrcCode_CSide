using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E3A RID: 3642
	[PacketId(ClientPacketId.kNKMPacket_DIVE_AUTO_REQ)]
	public sealed class NKMPacket_DIVE_AUTO_REQ : ISerializable
	{
		// Token: 0x06009764 RID: 38756 RVA: 0x0032CE64 File Offset: 0x0032B064
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.isAuto);
		}

		// Token: 0x04008986 RID: 35206
		public bool isAuto;
	}
}
