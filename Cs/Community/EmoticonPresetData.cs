using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FCD RID: 4045
	public sealed class EmoticonPresetData : ISerializable
	{
		// Token: 0x06009A6A RID: 39530 RVA: 0x003316E7 File Offset: 0x0032F8E7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.animationList);
			stream.PutOrGet(ref this.textList);
		}

		// Token: 0x04008DC8 RID: 36296
		public List<int> animationList = new List<int>();

		// Token: 0x04008DC9 RID: 36297
		public List<int> textList = new List<int>();
	}
}
