using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F48 RID: 3912
	[PacketId(ClientPacketId.kNKMPacket_GAME_AUTO_SKILL_CHANGE_REQ)]
	public sealed class NKMPacket_GAME_AUTO_SKILL_CHANGE_REQ : ISerializable
	{
		// Token: 0x06009970 RID: 39280 RVA: 0x00330076 File Offset: 0x0032E276
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_GAME_AUTO_SKILL_TYPE>(ref this.gameAutoSkillType);
		}

		// Token: 0x04008C73 RID: 35955
		public NKM_GAME_AUTO_SKILL_TYPE gameAutoSkillType;
	}
}
