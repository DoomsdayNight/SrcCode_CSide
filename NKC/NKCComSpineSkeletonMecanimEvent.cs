using System;
using System.Collections.Generic;
using Cs.Logging;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200061F RID: 1567
	public class NKCComSpineSkeletonMecanimEvent : MonoBehaviour
	{
		// Token: 0x06003067 RID: 12391 RVA: 0x000EEE2F File Offset: 0x000ED02F
		private void Awake()
		{
			this.m_dicEffect.Clear();
			this.ChildAllDisable();
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x000EEE42 File Offset: 0x000ED042
		private void OnDisable()
		{
			this.ChildAllDisable();
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x000EEE4A File Offset: 0x000ED04A
		private void OnDestroy()
		{
			this.m_dicEffect.Clear();
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x000EEE58 File Offset: 0x000ED058
		private void ChildAllDisable()
		{
			if (this.m_EFFECT_ROOT != null)
			{
				for (int i = 0; i < this.m_EFFECT_ROOT.transform.childCount; i++)
				{
					GameObject gameObject = this.m_EFFECT_ROOT.transform.GetChild(i).gameObject;
					if (gameObject.activeSelf)
					{
						gameObject.SetActive(false);
					}
				}
			}
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x000EEEB4 File Offset: 0x000ED0B4
		private void Event_ACTIVE(string objectName)
		{
			if (this.m_EFFECT_ROOT != null)
			{
				if (string.IsNullOrEmpty(objectName))
				{
					Debug.LogWarning("Null objectName");
					return;
				}
				Log.Info("<color=cyan><b>Event_ACTIVE</b></color> : " + base.transform.parent.name + " : " + objectName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Components/NKCComSpineSkeletonMecanimEvent.cs", 48);
				if (!this.m_dicEffect.ContainsKey(objectName))
				{
					Transform transform = this.m_EFFECT_ROOT.transform.Find(objectName);
					if (!(transform != null))
					{
						Debug.LogWarning("Null cTransform");
						return;
					}
					GameObject gameObject = transform.gameObject;
					this.m_dicEffect.Add(objectName, gameObject);
					if (gameObject.activeSelf)
					{
						gameObject.SetActive(false);
					}
					if (!gameObject.activeSelf)
					{
						gameObject.SetActive(true);
						return;
					}
				}
				else
				{
					GameObject gameObject;
					if (!this.m_dicEffect.TryGetValue(objectName, out gameObject))
					{
						Debug.LogWarning("Null cTargetObject");
						return;
					}
					if (gameObject.activeSelf)
					{
						gameObject.SetActive(false);
					}
					if (!gameObject.activeSelf)
					{
						gameObject.SetActive(true);
						return;
					}
				}
			}
			else
			{
				Debug.LogWarning("Null m_EFFECT_ROOT");
			}
		}

		// Token: 0x04002FCE RID: 12238
		public GameObject m_EFFECT_ROOT;

		// Token: 0x04002FCF RID: 12239
		private Dictionary<string, GameObject> m_dicEffect = new Dictionary<string, GameObject>();
	}
}
