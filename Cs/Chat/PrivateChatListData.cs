using System;
using ClientPacket.Common;
using Cs.Protocol;

namespace ClientPacket.Chat
{
	// Token: 0x02001066 RID: 4198
	public sealed class PrivateChatListData : ISerializable
	{
		// Token: 0x06009B89 RID: 39817 RVA: 0x00333326 File Offset: 0x00331526
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMCommonProfile>(ref this.commonProfile);
			stream.PutOrGet<NKMChatMessageData>(ref this.lastMessage);
		}

		// Token: 0x04008F7A RID: 36730
		public NKMCommonProfile commonProfile = new NKMCommonProfile();

		// Token: 0x04008F7B RID: 36731
		public NKMChatMessageData lastMessage = new NKMChatMessageData();
	}
}
