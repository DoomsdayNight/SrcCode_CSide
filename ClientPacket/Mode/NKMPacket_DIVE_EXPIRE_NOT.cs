using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E3C RID: 3644
	[PacketId(ClientPacketId.kNKMPacket_DIVE_EXPIRE_NOT)]
	public sealed class NKMPacket_DIVE_EXPIRE_NOT : ISerializable
	{
		// Token: 0x06009768 RID: 38760 RVA: 0x0032CE9C File Offset: 0x0032B09C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.stageID);
		}

		// Token: 0x04008989 RID: 35209
		public int stageID;
	}
}
