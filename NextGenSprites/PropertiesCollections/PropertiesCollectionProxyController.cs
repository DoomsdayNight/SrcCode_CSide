using System;
using System.Collections;
using UnityEngine;

namespace NextGenSprites.PropertiesCollections
{
	// Token: 0x0200004A RID: 74
	[AddComponentMenu("NextGenSprites/Properties Collection/Remote/Controller - Receiver")]
	[RequireComponent(typeof(SpriteRenderer))]
	[HelpURL("http://wiki.next-gen-sprites.com/doku.php?id=scripting:propertiescollection#controller")]
	public class PropertiesCollectionProxyController : MonoBehaviour
	{
		// Token: 0x06000247 RID: 583 RVA: 0x0000A28C File Offset: 0x0000848C
		private void Start()
		{
			this._spriteRenderer = base.GetComponent<SpriteRenderer>();
			if (this.CollectionManager == null)
			{
				if (this.FindManagerByReference)
				{
					foreach (PropertiesCollectionProxyManager propertiesCollectionProxyManager in UnityEngine.Object.FindObjectsOfType<PropertiesCollectionProxyManager>())
					{
						if (string.CompareOrdinal(this.ManagerReferenceId, propertiesCollectionProxyManager.ReferenceId) == 0)
						{
							this.CollectionManager = propertiesCollectionProxyManager;
							break;
						}
					}
					if (this.CollectionManager == null)
					{
						Debug.LogError(string.Format("Could not find an Manager with the Id: {0}", this.ManagerReferenceId));
						return;
					}
				}
				else
				{
					Debug.LogError("There is no Manager assigned!");
				}
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000A320 File Offset: 0x00008520
		public void UpdateMaterial(string CollectionName)
		{
			if (string.CompareOrdinal(CollectionName, this._lastId) == 0)
			{
				return;
			}
			if (this.CollectionManager._cachedMaterials.Count < 1)
			{
				this.CollectionManager.InitManager();
			}
			if (this.CollectionManager._cachedMaterials.ContainsKey(CollectionName))
			{
				this._spriteRenderer.material = this.CollectionManager._cachedMaterials[CollectionName];
			}
			this._lastId = CollectionName;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000A390 File Offset: 0x00008590
		public void UpdateMaterialSmooth(string CollectionName, float LerpDuration = 1f)
		{
			if (string.CompareOrdinal(CollectionName, this._lastId) == 0)
			{
				return;
			}
			if (this.CollectionManager._cachedMaterials.Count < 1)
			{
				this.CollectionManager.InitManager();
			}
			if (this.CollectionManager._cachedMaterials.ContainsKey(CollectionName))
			{
				Material material = this._spriteRenderer.material;
				Material target = this.CollectionManager._cachedMaterials[CollectionName];
				base.StartCoroutine(this.SmoothMaterialLerp(material, target, LerpDuration));
			}
			this._lastId = CollectionName;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000A414 File Offset: 0x00008614
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
				if (this.CollectionManager._cachedMaterials.Count < 1)
				{
					this.CollectionManager.InitManager();
				}
				if (this.CollectionManager._cachedMaterials.ContainsKey(text))
				{
					Material material = this._spriteRenderer.material;
					Material target = this.CollectionManager._cachedMaterials[text];
					base.StartCoroutine(this.SmoothMaterialLerp(material, target, duration));
				}
				this._lastId = text;
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000A4DF File Offset: 0x000086DF
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

		// Token: 0x040001B0 RID: 432
		public PropertiesCollectionProxyManager CollectionManager;

		// Token: 0x040001B1 RID: 433
		public bool FindManagerByReference;

		// Token: 0x040001B2 RID: 434
		public string ManagerReferenceId;

		// Token: 0x040001B3 RID: 435
		private SpriteRenderer _spriteRenderer;

		// Token: 0x040001B4 RID: 436
		private string _lastId = "";
	}
}
