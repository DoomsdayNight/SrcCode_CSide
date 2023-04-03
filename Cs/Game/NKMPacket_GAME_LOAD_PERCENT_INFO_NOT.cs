using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F32 RID: 3890
	[PacketId(ClientPacketId.kNKMPacket_GAME_LOAD_PERCENT_INFO_NOT)]
	public sealed class NKMPacket_GAME_LOAD_PERCENT_INFO_NOT : ISerializable
	{
		// Token: 0x06009944 RID: 39236 RVA: 0x0032FBB3 File Offset: 0x0032DDB3
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.userUID);
			stream.PutOrGet(ref this.percent);
		}

		// Token: 0x04008C2B RID: 35883
		public long userUID;

		// Token: 0x04008C2C RID: 35884
		public int percent;
	}
}
