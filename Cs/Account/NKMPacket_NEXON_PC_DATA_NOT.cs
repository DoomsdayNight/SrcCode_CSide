using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001082 RID: 4226
	[PacketId(ClientPacketId.kNKMPacket_NEXON_PC_DATA_NOT)]
	public sealed class NKMPacket_NEXON_PC_DATA_NOT : ISerializable
	{
		// Token: 0x06009BC1 RID: 39873 RVA: 0x00333CE5 File Offset: 0x00331EE5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.npacode);
		}

		// Token: 0x04009003 RID: 36867
		public string npacode;
	}
}
