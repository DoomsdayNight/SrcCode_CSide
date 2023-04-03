using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Warfare
{
	// Token: 0x02000CA0 RID: 3232
	[PacketId(ClientPacketId.kNKMPacket_WARFARE_GAME_GIVE_UP_ACK)]
	public sealed class NKMPacket_WARFARE_GAME_GIVE_UP_ACK : ISerializable
	{
		// Token: 0x0600943D RID: 37949 RVA: 0x00328651 File Offset: 0x00326851
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x0400858C RID: 34188
		public NKM_ERROR_CODE errorCode;
	}
}
