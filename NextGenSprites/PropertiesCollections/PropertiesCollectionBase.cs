using System;
using System.Collections.Generic;
using UnityEngine;

namespace NextGenSprites.PropertiesCollections
{
	// Token: 0x02000048 RID: 72
	public class PropertiesCollectionBase : MonoBehaviour
	{
		// Token: 0x0600023D RID: 573 RVA: 0x00009E60 File Offset: 0x00008060
		private void SetProperties(Dictionary<string, PropertiesCollection> PropCollection, string CollectionName, Material Target)
		{
			if (PropCollection.ContainsKey(CollectionName))
			{
				foreach (PropertiesCollection.TextureTargets textureTargets in PropCollection[CollectionName].Textures)
				{
					Target.SetTexture(textureTargets.Target.GetString(), textureTargets.Value);
				}
				foreach (PropertiesCollection.FloatTargets floatTargets in PropCollection[CollectionName].Floats)
				{
					Target.SetFloat(floatTargets.Target.GetString(), floatTargets.Value);
				}
				foreach (PropertiesCollection.TintTargets tintTargets in PropCollection[CollectionName].Tints)
				{
					Target.SetColor(tintTargets.Target.GetString(), tintTargets.Value);
				}
				foreach (PropertiesCollection.FeatureTargets featureTargets in PropCollection[CollectionName].Features)
				{
					if (featureTargets.Value)
					{
						Target.EnableKeyword(featureTargets.Target.GetString());
					}
					else
					{
						Target.DisableKeyword(featureTargets.Target.GetString());
					}
				}
				return;
			}
			Debug.LogError("There is no matching Id on this Collection");
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00009F84 File Offset: 0x00008184
		protected void InitMaterialCache(Material source, Dictionary<string, Material> target)
		{
			Dictionary<string, PropertiesCollection> dictionary = new Dictionary<string, PropertiesCollection>();
			foreach (PropertiesCollection propertiesCollection in this.PropCollections)
			{
				dictionary.Add(propertiesCollection.CollectionName, propertiesCollection);
			}
			foreach (KeyValuePair<string, PropertiesCollection> keyValuePair in dictionary)
			{
				Material material = new Material(source);
				material.name = string.Format("{0} - [{1}]", material.name, keyValuePair.Key);
				this.SetProperties(dictionary, keyValuePair.Key, material);
				target.Add(keyValuePair.Key, material);
			}
		}

		// Token: 0x040001AC RID: 428
		public PropertiesCollection[] PropCollections;

		// Token: 0x040001AD RID: 429
		public Dictionary<string, Material> _cachedMaterials = new Dictionary<string, Material>();
	}
}
