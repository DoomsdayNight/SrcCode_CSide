using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F63 RID: 3939
	[PacketId(ClientPacketId.kNKMPacket_FIERCE_SEASON_NOT)]
	public sealed class NKMPacket_FIERCE_SEASON_NOT : ISerializable
	{
		// Token: 0x060099A6 RID: 39334 RVA: 0x00330493 File Offset: 0x0032E693
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.fierceId);
		}

		// Token: 0x04008CAC RID: 36012
		public int fierceId;
	}
}
