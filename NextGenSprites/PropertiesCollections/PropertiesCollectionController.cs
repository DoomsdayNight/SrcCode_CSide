using System;
using System.Collections;
using UnityEngine;

namespace NextGenSprites.PropertiesCollections
{
	// Token: 0x02000049 RID: 73
	[AddComponentMenu("NextGenSprites/Properties Collection/Controller")]
	[HelpURL("http://wiki.next-gen-sprites.com/doku.php?id=scripting:propertiescollection#managersolo")]
	[RequireComponent(typeof(SpriteRenderer))]
	public class PropertiesCollectionController : PropertiesCollectionBase
	{
		// Token: 0x06000240 RID: 576 RVA: 0x0000A053 File Offset: 0x00008253
		private void Awake()
		{
			this.InitManager();
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000A05C File Offset: 0x0000825C
		private void InitManager()
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
			this._spriteRenderer = base.GetComponent<SpriteRenderer>();
			base.InitMaterialCache(this._spriteRenderer.sharedMaterial, this._cachedMaterials);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000A0DC File Offset: 0x000082DC
		public void UpdateMaterial(string CollectionName)
		{
			if (string.CompareOrdinal(CollectionName, this._lastId) == 0)
			{
				return;
			}
			if (this._cachedMaterials.Count < 1)
			{
				this.InitManager();
			}
			if (this._cachedMaterials.ContainsKey(CollectionName))
			{
				this._spriteRenderer.material = this._cachedMaterials[CollectionName];
			}
			this._lastId = CollectionName;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000A138 File Offset: 0x00008338
		public void UpdateMaterialSmooth(string CollectionName, float LerpDuration = 1f)
		{
			if (string.CompareOrdinal(CollectionName, this._lastId) == 0)
			{
				return;
			}
			if (this._cachedMaterials.Count < 1)
			{
				this.InitManager();
			}
			if (this._cachedMaterials.ContainsKey(CollectionName))
			{
				Material material = this._spriteRenderer.material;
				Material target = this._cachedMaterials[CollectionName];
				base.StartCoroutine(this.SmoothMaterialLerp(material, target, LerpDuration));
			}
			this._lastId = CollectionName;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000A1A8 File Offset: 0x000083A8
		public void UpdateMaterialSmooth(string Arguments)
		{
			string[] array = Arguments.Split(new char[]
			{
				':',
				' '
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length > 1)
			{
				float num;
				float.TryParse(array[1], out num);
				if (num == 0f)
				{
					Debug.LogWarning("Invalid Time argument. Check spelling?");
					return;
				}
				string text = array[0];
				float duration = num;
				if (string.CompareOrdinal(text, this._lastId) == 0)
				{
					return;
				}
				if (this._cachedMaterials.Count < 1)
				{
					this.InitManager();
				}
				if (this._cachedMaterials.ContainsKey(text))
				{
					Material material = this._spriteRenderer.material;
					Material target = this._cachedMaterials[text];
					base.StartCoroutine(this.SmoothMaterialLerp(material, target, duration));
				}
				this._lastId = text;
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000A25F File Offset: 0x0000845F
		private IEnumerator SmoothMaterialLerp(Material origin, Material target, float duration)
		{
			float elapsedTime = 0f;
			while (elapsedTime < duration)
			{
				this._spriteRenderer.material.Lerp(origin, target, elapsedTime / duration);
				elapsedTime += Time.deltaTime;
				yield return null;
			}
			yield break;
		}

		// Token: 0x040001AE RID: 430
		private string _lastId;

		// Token: 0x040001AF RID: 431
		private SpriteRenderer _spriteRenderer;
	}
}
