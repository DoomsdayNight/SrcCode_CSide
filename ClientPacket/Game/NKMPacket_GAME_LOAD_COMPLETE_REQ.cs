using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F33 RID: 3891
	[PacketId(ClientPacketId.kNKMPacket_GAME_LOAD_COMPLETE_REQ)]
	public sealed class NKMPacket_GAME_LOAD_COMPLETE_REQ : ISerializable
	{
		// Token: 0x06009946 RID: 39238 RVA: 0x0032FBD5 File Offset: 0x0032DDD5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.isIntrude);
		}

		// Token: 0x04008C2D RID: 35885
		public bool isIntrude;
	}
}
