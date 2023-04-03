using System;
using UnityEngine;

namespace NextGenSprites.PropertiesCollections
{
	// Token: 0x02000047 RID: 71
	[HelpURL("http://wiki.next-gen-sprites.com/doku.php?id=scripting:propertiescollection")]
	[Serializable]
	public class PropertiesCollection : ScriptableObject
	{
		// Token: 0x040001A6 RID: 422
		public string CollectionName;

		// Token: 0x040001A7 RID: 423
		public PropertiesCollection.TextureTargets[] Textures;

		// Token: 0x040001A8 RID: 424
		public PropertiesCollection.FloatTargets[] Floats;

		// Token: 0x040001A9 RID: 425
		public PropertiesCollection.Vector4Targets[] Vector4s;

		// Token: 0x040001AA RID: 426
		public PropertiesCollection.TintTargets[] Tints;

		// Token: 0x040001AB RID: 427
		public PropertiesCollection.FeatureTargets[] Features;

		// Token: 0x020010F8 RID: 4344
		[Serializable]
		public class TextureTargets
		{
			// Token: 0x0400910E RID: 37134
			public ShaderTexture Target;

			// Token: 0x0400910F RID: 37135
			public Texture Value;
		}

		// Token: 0x020010F9 RID: 4345
		[Serializable]
		public class FloatTargets
		{
			// Token: 0x04009110 RID: 37136
			public ShaderFloat Target;

			// Token: 0x04009111 RID: 37137
			public float Value;
		}

		// Token: 0x020010FA RID: 4346
		[Serializable]
		public class Vector4Targets
		{
			// Token: 0x04009112 RID: 37138
			public ShaderVector4 Target;

			// Token: 0x04009113 RID: 37139
			public Vector4 Value;
		}

		// Token: 0x020010FB RID: 4347
		[Serializable]
		public class TintTargets
		{
			// Token: 0x04009114 RID: 37140
			public ShaderColor Target;

			// Token: 0x04009115 RID: 37141
			public Color Value;
		}

		// Token: 0x020010FC RID: 4348
		[Serializable]
		public class FeatureTargets
		{
			// Token: 0x04009116 RID: 37142
			public ShaderFeatureRuntime Target;

			// Token: 0x04009117 RID: 37143
			public bool Value;
		}
	}
}
