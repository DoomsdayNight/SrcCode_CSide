using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F57 RID: 3927
	[PacketId(ClientPacketId.kNKMPacket_GAME_TACTICAL_COMMAND_REQ)]
	public sealed class NKMPacket_GAME_TACTICAL_COMMAND_REQ : ISerializable
	{
		// Token: 0x0600998E RID: 39310 RVA: 0x003302A2 File Offset: 0x0032E4A2
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.TCID);
		}

		// Token: 0x04008C93 RID: 35987
		public short TCID;
	}
}
