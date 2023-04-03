using System;
using System.Collections.Generic;
using System.Reflection;
using Cs.Logging;
using NKC;

namespace Cs.Protocol
{
	// Token: 0x020010C1 RID: 4289
	public sealed class PacketController
	{
		// Token: 0x17001723 RID: 5923
		// (get) Token: 0x06009CFC RID: 40188 RVA: 0x00337045 File Offset: 0x00335245
		public static PacketController Instance { get; } = new PacketController();

		// Token: 0x17001724 RID: 5924
		// (get) Token: 0x06009CFD RID: 40189 RVA: 0x0033704C File Offset: 0x0033524C
		internal IEnumerable<PacketController.PacketDescription> Descriptions
		{
			get
			{
				return this.packets;
			}
		}

		// Token: 0x06009CFE RID: 40190 RVA: 0x00337054 File Offset: 0x00335254
		public void Initialize()
		{
			if (this.initialized)
			{
				return;
			}
			foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
			{
				PacketIdAttribute packetIdAttribute = Attribute.GetCustomAttribute(type, typeof(PacketIdAttribute)) as PacketIdAttribute;
				if (packetIdAttribute != null)
				{
					ushort packetId = packetIdAttribute.PacketId;
					if (this.packets[(int)packetId] != null)
					{
						PacketController.PacketDescription packetDescription = this.packets[(int)packetId];
						Log.ErrorAndExit(string.Format("packet id duplicated. id:{0} typeA:{1} typeB:{2}", packetId, packetDescription.Type.Name, type.Name), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/COMMON/Cs.Protocol/PacketController.cs", 37);
					}
					this.type2Id.Add(type, packetId);
					this.packets[(int)packetId] = new PacketController.PacketDescription
					{
						Type = type,
						Id = packetId,
						IdStr = packetIdAttribute.PacketIdStr
					};
				}
			}
			this.initialized = true;
		}

		// Token: 0x06009CFF RID: 40191 RVA: 0x00337134 File Offset: 0x00335334
		public ushort GetId(ISerializable target)
		{
			ushort result;
			if (!this.type2Id.TryGetValue(target.GetType(), out result))
			{
				return ushort.MaxValue;
			}
			return result;
		}

		// Token: 0x06009D00 RID: 40192 RVA: 0x00337160 File Offset: 0x00335360
		public ushort GetId(Type type)
		{
			ushort result;
			if (!this.type2Id.TryGetValue(type, out result))
			{
				return ushort.MaxValue;
			}
			return result;
		}

		// Token: 0x06009D01 RID: 40193 RVA: 0x00337184 File Offset: 0x00335384
		public string GetIdStr(ISerializable target)
		{
			PacketController.PacketDescription description = this.GetDescription(target.GetType());
			return ((description != null) ? description.IdStr : null) ?? "[Not a PacketType]";
		}

		// Token: 0x06009D02 RID: 40194 RVA: 0x003371A7 File Offset: 0x003353A7
		public string GetIdStr(ushort id)
		{
			PacketController.PacketDescription packetDescription = this.packets[(int)id];
			return ((packetDescription != null) ? packetDescription.IdStr : null) ?? string.Format("[invalid id:{0}]", id);
		}

		// Token: 0x06009D03 RID: 40195 RVA: 0x003371D1 File Offset: 0x003353D1
		public bool IsPacket(ISerializable target)
		{
			return this.type2Id.ContainsKey(target.GetType());
		}

		// Token: 0x06009D04 RID: 40196 RVA: 0x003371E4 File Offset: 0x003353E4
		public ISerializable Create(ushort id)
		{
			PacketController.PacketDescription packetDescription = this.packets[(int)id];
			if (packetDescription == null)
			{
				Log.Error(string.Format("invalid packet id:{0}", id), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/COMMON/Cs.Protocol/PacketController.cs", 94);
				return null;
			}
			return (ISerializable)NKCPacketObjectPool.OpenObject(packetDescription.Type);
		}

		// Token: 0x06009D05 RID: 40197 RVA: 0x0033722C File Offset: 0x0033542C
		private PacketController.PacketDescription GetDescription(Type type)
		{
			ushort num;
			if (!this.type2Id.TryGetValue(type, out num))
			{
				return null;
			}
			return this.packets[(int)num];
		}

		// Token: 0x04009085 RID: 36997
		private readonly PacketController.PacketDescription[] packets = new PacketController.PacketDescription[65536];

		// Token: 0x04009086 RID: 36998
		private readonly Dictionary<Type, ushort> type2Id = new Dictionary<Type, ushort>();

		// Token: 0x04009087 RID: 36999
		private bool initialized;

		// Token: 0x02001A39 RID: 6713
		internal sealed class PacketDescription
		{
			// Token: 0x17001A00 RID: 6656
			// (get) Token: 0x0600BB6D RID: 47981 RVA: 0x0036F666 File Offset: 0x0036D866
			// (set) Token: 0x0600BB6E RID: 47982 RVA: 0x0036F66E File Offset: 0x0036D86E
			public Type Type { get; set; }

			// Token: 0x17001A01 RID: 6657
			// (get) Token: 0x0600BB6F RID: 47983 RVA: 0x0036F677 File Offset: 0x0036D877
			// (set) Token: 0x0600BB70 RID: 47984 RVA: 0x0036F67F File Offset: 0x0036D87F
			public ushort Id { get; set; }

			// Token: 0x17001A02 RID: 6658
			// (get) Token: 0x0600BB71 RID: 47985 RVA: 0x0036F688 File Offset: 0x0036D888
			// (set) Token: 0x0600BB72 RID: 47986 RVA: 0x0036F690 File Offset: 0x0036D890
			public string IdStr { get; set; }
		}
	}
}
