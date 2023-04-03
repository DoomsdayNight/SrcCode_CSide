using System;
using UnityEngine;

namespace NextGenSprites.PropertiesCollections
{
	// Token: 0x0200004B RID: 75
	[AddComponentMenu("NextGenSprites/Properties Collection/Remote/Manager - Host")]
	[HelpURL("http://wiki.next-gen-sprites.com/doku.php?id=scripting:propertiescollection#manager")]
	public class PropertiesCollectionProxyManager : PropertiesCollectionBase
	{
		// Token: 0x0600024D RID: 589 RVA: 0x0000A516 File Offset: 0x00008716
		private void Start()
		{
			this.InitManager();
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000A520 File Offset: 0x00008720
		public void InitManager()
		{
			if (this.PropCollections.Length == 0)
			{
				Debug.LogError("There are no Properties Collections assigned!");
				return;
			}
			for (int i = 0; i < this.PropCollections.Length; i++)
			{
				if (this.PropCollections[i] == null)
				{
					Debug.LogError(string.Format("No Properties Collection assigned at Element {0}!", i));
					return;
				}
			}
			if (this.TargetThis)
			{
				this.SourceObject = base.GetComponent<SpriteRenderer>();
			}
			else if (this.SourceObject == null)
			{
				Debug.LogError("There is no Target Object assigned!");
				return;
			}
			base.InitMaterialCache(base.GetComponent<SpriteRenderer>().sharedMaterial, this._cachedMaterials);
		}

		// Token: 0x040001B5 RID: 437
		public string ReferenceId = "GIVE ME A NAME";

		// Token: 0x040001B6 RID: 438
		public bool TargetThis = true;

		// Token: 0x040001B7 RID: 439
		public SpriteRenderer SourceObject;
	}
}
