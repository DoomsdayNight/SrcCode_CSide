using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F31 RID: 3889
	[PacketId(ClientPacketId.kNKMPacket_GAME_LOAD_PERCENT_UPDATE_NOT)]
	public sealed class NKMPacket_GAME_LOAD_PERCENT_UPDATE_NOT : ISerializable
	{
		// Token: 0x06009942 RID: 39234 RVA: 0x0032FB9D File Offset: 0x0032DD9D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.percent);
		}

		// Token: 0x04008C2A RID: 35882
		public int percent;
	}
}
