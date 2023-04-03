using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Warfare
{
	// Token: 0x02000C99 RID: 3225
	[PacketId(ClientPacketId.kNKMPacket_WARFARE_GAME_MOVE_REQ)]
	public sealed class NKMPacket_WARFARE_GAME_MOVE_REQ : ISerializable
	{
		// Token: 0x0600942F RID: 37935 RVA: 0x00328572 File Offset: 0x00326772
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.tileIndexFrom);
			stream.PutOrGet(ref this.tileIndexTo);
		}

		// Token: 0x04008582 RID: 34178
		public byte tileIndexFrom;

		// Token: 0x04008583 RID: 34179
		public byte tileIndexTo;
	}
}
