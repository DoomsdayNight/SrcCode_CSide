using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001011 RID: 4113
	[PacketId(ClientPacketId.kNKMPacket_USER_PROFILE_CHANGE_FRAME_REQ)]
	public sealed class NKMPacket_USER_PROFILE_CHANGE_FRAME_REQ : ISerializable
	{
		// Token: 0x06009AF2 RID: 39666 RVA: 0x003320F9 File Offset: 0x003302F9
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.selfiFrameId);
		}

		// Token: 0x04008E4F RID: 36431
		public int selfiFrameId;
	}
}
