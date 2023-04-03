using System;
using Cs.Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F70 RID: 3952
	public sealed class NKMBingoTile : ISerializable
	{
		// Token: 0x060099BC RID: 39356 RVA: 0x00330738 File Offset: 0x0032E938
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.eventId);
			stream.PutOrGet(ref this.tileIndex);
		}

		// Token: 0x04008CD2 RID: 36050
		public int eventId;

		// Token: 0x04008CD3 RID: 36051
		public int tileIndex;
	}
}
