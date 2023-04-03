using System;
using System.Collections.Generic;
using Cs.Logging;
using NKC.FX;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200061E RID: 1566
	[DisallowMultipleComponent]
	public class NKCComSpineSkeletonAnimationEvent : MonoBehaviour
	{
		// Token: 0x06003058 RID: 12376 RVA: 0x000EE11F File Offset: 0x000EC31F
		public void SetActiveEvent(bool bActiveEvent)
		{
			this.m_bActiveEvent = bActiveEvent;
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x000EE128 File Offset: 0x000EC328
		private void Awake()
		{
			this.m_SkeletonAnimation = base.GetComponent<SkeletonAnimation>();
			if (this.m_SkeletonAnimation == null)
			{
				this.m_SkeletonGraphic = base.GetComponent<SkeletonGraphic>();
			}
			this.m_dicBone.Clear();
			this.m_setGameObject.Clear();
			this.m_NKM_GLOBAL_EFFECT = GameObject.Find("NKM_GLOBAL_EFFECT");
			if (this.m_EFFECT_ROOT == null)
			{
				this.m_EFFECT_ROOT = GameObject.Find("VFX");
				if (this.m_EFFECT_ROOT == null)
				{
					string str = "VFX 오브젝트가 없습니다. ";
					Transform parent = base.transform.parent;
					Debug.LogWarning(str + ((parent != null) ? parent.name : null));
				}
			}
			else if (this.m_EFFECT_ROOT.name != "VFX")
			{
				Debug.LogWarning("VFX Root 오브젝트가 아닐 수도 있습니다. 현재 : " + this.m_EFFECT_ROOT.name + " ", base.gameObject);
			}
			this.AddEvent(false);
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x000EE218 File Offset: 0x000EC418
		private void Start()
		{
			this.AddEvent(false);
			this.InitFxLifetime();
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x000EE228 File Offset: 0x000EC428
		private void InitFxLifetime()
		{
			if (this.m_EFFECT_ROOT != null)
			{
				this.m_FX_Lifetime = this.m_EFFECT_ROOT.GetComponentsInChildren<NKC_FX_LIFETIME>(true);
				if (this.m_FX_Lifetime != null && this.m_FX_Lifetime.Length != 0)
				{
					for (int i = 0; i < this.m_FX_Lifetime.Length; i++)
					{
						this.m_FX_Lifetime[i].Init = true;
					}
				}
			}
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x000EE288 File Offset: 0x000EC488
		public void AddEvent(bool bForce = false)
		{
			if (!bForce && this.m_bAddEvent)
			{
				return;
			}
			if (this.m_SkeletonAnimation != null && this.m_SkeletonAnimation.SkeletonDataAsset != null)
			{
				this.m_SkeletonDataAssetName = this.m_SkeletonAnimation.SkeletonDataAsset.name;
				if (this.m_SkeletonAnimation.AnimationState != null)
				{
					this.m_SkeletonAnimation.AnimationState.Start -= this.OnStart;
					this.m_SkeletonAnimation.AnimationState.Start += this.OnStart;
					this.m_SkeletonAnimation.UpdateComplete -= this.OnUpdate;
					this.m_SkeletonAnimation.UpdateComplete += this.OnUpdate;
					this.m_SkeletonAnimation.AnimationState.Event -= this.OnSkeletonAnimationEvent;
					this.m_SkeletonAnimation.AnimationState.Event += this.OnSkeletonAnimationEvent;
					this.m_bAddEvent = true;
					return;
				}
			}
			else if (this.m_SkeletonGraphic != null && this.m_SkeletonGraphic.SkeletonDataAsset != null)
			{
				this.m_SkeletonDataAssetName = this.m_SkeletonGraphic.SkeletonDataAsset.name;
				if (this.m_SkeletonGraphic.AnimationState != null)
				{
					this.m_SkeletonGraphic.AnimationState.Event -= this.OnSkeletonAnimationEvent;
					this.m_SkeletonGraphic.AnimationState.Event += this.OnSkeletonAnimationEvent;
					this.m_bAddEvent = true;
				}
			}
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x000EE417 File Offset: 0x000EC617
		private void OnStart(TrackEntry entry)
		{
			if (entry != null)
			{
				this.currentEntry = entry;
				this.current = this.currentEntry.TrackTime;
			}
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x000EE434 File Offset: 0x000EC634
		private void OnUpdate(ISkeletonAnimation s)
		{
			if (this.currentEntry != null)
			{
				this.sinceTime = this.currentEntry.TrackTime;
				this.deltatime = Mathf.Clamp(this.sinceTime - this.current, 0f, Time.maximumDeltaTime);
				this.current = this.sinceTime;
				if (this.m_FX_Lifetime != null && this.m_FX_Lifetime.Length != 0)
				{
					for (int i = 0; i < this.m_FX_Lifetime.Length; i++)
					{
						if (this.m_FX_Lifetime[i].gameObject.activeInHierarchy)
						{
							this.m_FX_Lifetime[i].UpdateLifeTime(this.deltatime * this.currentEntry.TimeScale);
						}
					}
				}
			}
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x000EE4E4 File Offset: 0x000EC6E4
		private void OnDisable()
		{
			HashSet<GameObject>.Enumerator enumerator = this.m_setGameObject.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.activeSelf)
				{
					enumerator.Current.SetActive(false);
				}
			}
			this.m_bAddEvent = false;
		}

		// Token: 0x06003060 RID: 12384 RVA: 0x000EE52C File Offset: 0x000EC72C
		private void OnDestroy()
		{
			if (this.m_SkeletonAnimation != null)
			{
				this.m_SkeletonAnimation.UpdateComplete -= this.OnUpdate;
				if (this.m_SkeletonAnimation.state != null)
				{
					this.m_SkeletonAnimation.state.Start -= this.OnStart;
					this.m_SkeletonAnimation.state.Event -= this.OnSkeletonAnimationEvent;
				}
				this.m_SkeletonAnimation = null;
			}
			if (this.m_SkeletonGraphic != null)
			{
				if (this.m_SkeletonGraphic.AnimationState != null)
				{
					this.m_SkeletonGraphic.AnimationState.Event -= this.OnSkeletonAnimationEvent;
				}
				this.m_SkeletonGraphic = null;
			}
			foreach (KeyValuePair<string, List<GameObject>> keyValuePair in this.m_dicGlobalEffect)
			{
				List<GameObject> value = keyValuePair.Value;
				for (int i = 0; i < value.Count; i++)
				{
					UnityEngine.Object.Destroy(value[i]);
				}
				value.Clear();
			}
			this.m_dicGlobalEffect.Clear();
			this.m_bAddEvent = false;
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x000EE644 File Offset: 0x000EC844
		private void OnSkeletonAnimationEvent(TrackEntry trackEntry, Spine.Event e)
		{
			if (!base.enabled || !this.m_bActiveEvent)
			{
				return;
			}
			if (e.Data.Name == "SE_ACTIVE")
			{
				if (e.String.Length < 2)
				{
					return;
				}
				SPINE_EVENT_OPTION @int = (SPINE_EVENT_OPTION)e.Int;
				string[] array = e.String.Split(this.m_SplitToken);
				if (array.Length > 1)
				{
					this.Event_ACTIVE(@int, e.Data.Name, array[0], array[1]);
					return;
				}
				this.Event_ACTIVE(@int, e.Data.Name, array[0], null);
				return;
			}
			else if (e.Data.Name == "SE_ACTIVE_GLOBAL")
			{
				if (e.String.Length < 2)
				{
					return;
				}
				string[] array2 = e.String.Split(this.m_SplitToken);
				if (array2.Length > 1)
				{
					this.Event_ACTIVE_GLOBAL(e.Data.Name, array2[0], array2[1]);
					return;
				}
				this.Event_ACTIVE_GLOBAL(e.Data.Name, array2[0], null);
				return;
			}
			else
			{
				if (!(e.Data.Name == "SE_SOUND"))
				{
					Log.Warn("Invalid Event Name : [" + e.Data.Name + "]", this, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Components/NKCComSpineSkeletonAnimationEvent.cs", 250);
					return;
				}
				if (e.String.Length < 2)
				{
					return;
				}
				this.Event_SOUND(e.Data.Name, e.String);
				return;
			}
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x000EE7AB File Offset: 0x000EC9AB
		public void ValidateSkeletonAnimationEvent(Spine.Event e)
		{
			this.Awake();
			this.Start();
			Log.Info("Valid Check: " + this.m_SkeletonDataAssetName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Components/NKCComSpineSkeletonAnimationEvent.cs", 259);
			this.OnSkeletonAnimationEvent(null, e);
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x000EE7E0 File Offset: 0x000EC9E0
		private void Event_ACTIVE(SPINE_EVENT_OPTION eSPINE_EVENT_OPTION, string eventName, string objectName, string boneName)
		{
			if (eventName == null)
			{
				Log.Error(string.Concat(new string[]
				{
					"Event_ACTIVE eventName null: ",
					this.m_SkeletonDataAssetName,
					", ",
					eventName,
					", ",
					objectName
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Components/NKCComSpineSkeletonAnimationEvent.cs", 267);
				return;
			}
			if (objectName == null)
			{
				Log.Error(string.Concat(new string[]
				{
					"Event_ACTIVE objectName null: ",
					this.m_SkeletonDataAssetName,
					", ",
					eventName,
					", ",
					objectName
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Components/NKCComSpineSkeletonAnimationEvent.cs", 273);
				return;
			}
			if (this.m_EFFECT_ROOT == null)
			{
				Log.Error(string.Concat(new string[]
				{
					"Event_ACTIVE m_EFFECT_ROOT null: ",
					this.m_SkeletonDataAssetName,
					", ",
					eventName,
					", ",
					objectName
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Components/NKCComSpineSkeletonAnimationEvent.cs", 279);
				return;
			}
			if (this.m_EFFECT_ROOT.transform == null)
			{
				Log.Error(string.Concat(new string[]
				{
					"Event_ACTIVE m_EFFECT_ROOT.transform null: ",
					this.m_SkeletonDataAssetName,
					", ",
					eventName,
					", ",
					objectName
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Components/NKCComSpineSkeletonAnimationEvent.cs", 285);
				return;
			}
			Transform transform = this.m_EFFECT_ROOT.transform.Find(objectName);
			if (transform == null)
			{
				Log.Error(string.Concat(new string[]
				{
					"Event_ACTIVE invalid objectName: ",
					this.m_SkeletonDataAssetName,
					", ",
					eventName,
					", ",
					objectName
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Components/NKCComSpineSkeletonAnimationEvent.cs", 293);
				return;
			}
			GameObject gameObject = transform.gameObject;
			if (gameObject == null)
			{
				Log.Error(string.Concat(new string[]
				{
					"Event_ACTIVE cTargetObject null: ",
					this.m_SkeletonDataAssetName,
					", ",
					eventName,
					", ",
					objectName
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Components/NKCComSpineSkeletonAnimationEvent.cs", 300);
				return;
			}
			if (eSPINE_EVENT_OPTION == SPINE_EVENT_OPTION.SEO_NORMAL && gameObject.activeSelf)
			{
				gameObject.SetActive(false);
			}
			if (boneName != null && boneName.Length > 1)
			{
				Bone bone;
				if (!this.m_dicBone.ContainsKey(boneName))
				{
					if (this.m_SkeletonAnimation != null && this.m_SkeletonAnimation.skeleton != null)
					{
						bone = this.m_SkeletonAnimation.skeleton.FindBone(boneName);
					}
					else
					{
						if (!(this.m_SkeletonGraphic != null) || this.m_SkeletonGraphic.Skeleton == null)
						{
							Log.Error(string.Concat(new string[]
							{
								"Event_ACTIVE skeleton null: ",
								this.m_SkeletonDataAssetName,
								", ",
								eventName,
								", ",
								objectName
							}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Components/NKCComSpineSkeletonAnimationEvent.cs", 321);
							return;
						}
						bone = this.m_SkeletonGraphic.Skeleton.FindBone(boneName);
					}
					if (bone != null)
					{
						this.m_dicBone.Add(boneName, bone);
					}
				}
				else
				{
					bone = this.m_dicBone[boneName];
				}
				if (bone != null)
				{
					Vector3 localPosition = gameObject.transform.localPosition;
					localPosition.x = bone.WorldX;
					localPosition.y = bone.WorldY;
					gameObject.transform.localPosition = localPosition;
				}
				else
				{
					Log.Error(string.Format("Event_ACTIVE cBone null: {0}, {1}, {2}, {3}", new object[]
					{
						this.m_SkeletonDataAssetName,
						eventName,
						objectName,
						bone
					}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Components/NKCComSpineSkeletonAnimationEvent.cs", 344);
				}
			}
			if (!gameObject.activeSelf)
			{
				gameObject.SetActive(true);
			}
			if (!this.m_setGameObject.Contains(gameObject))
			{
				this.m_setGameObject.Add(gameObject);
			}
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x000EEB6C File Offset: 0x000ECD6C
		private void Event_ACTIVE_GLOBAL(string eventName, string objectName, string boneName)
		{
			Transform transform = this.m_EFFECT_ROOT.transform.Find(objectName);
			if (transform == null)
			{
				Log.Error(string.Concat(new string[]
				{
					"Event_ACTIVE invalid objectName: ",
					this.m_EFFECT_ROOT.name,
					", ",
					eventName,
					", ",
					objectName
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Components/NKCComSpineSkeletonAnimationEvent.cs", 358);
				return;
			}
			List<GameObject> list;
			if (!this.m_dicGlobalEffect.ContainsKey(transform.gameObject.name))
			{
				list = new List<GameObject>();
				this.m_dicGlobalEffect.Add(transform.gameObject.name, list);
			}
			list = this.m_dicGlobalEffect[transform.gameObject.name];
			GameObject gameObject = null;
			for (int i = 0; i < list.Count; i++)
			{
				if (!list[i].activeSelf)
				{
					gameObject = list[i];
					break;
				}
			}
			if (gameObject == null)
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(transform.gameObject, this.m_EFFECT_ROOT.transform);
				list.Add(gameObject);
			}
			if (boneName != null && boneName.Length > 1)
			{
				Bone bone = null;
				if (!this.m_dicBone.ContainsKey(boneName))
				{
					if (this.m_SkeletonAnimation != null)
					{
						bone = this.m_SkeletonAnimation.skeleton.FindBone(boneName);
					}
					else if (this.m_SkeletonGraphic != null)
					{
						bone = this.m_SkeletonGraphic.Skeleton.FindBone(boneName);
					}
					if (bone != null)
					{
						this.m_dicBone.Add(boneName, bone);
					}
				}
				else
				{
					bone = this.m_dicBone[boneName];
				}
				if (bone != null)
				{
					Vector3 localPosition = gameObject.transform.localPosition;
					localPosition.x = bone.WorldX;
					localPosition.y = bone.WorldY;
					gameObject.transform.localPosition = localPosition;
				}
				else
				{
					Log.Error(string.Format("Event_ACTIVE cBone null: {0}, {1}, {2}, {3}", new object[]
					{
						this.m_EFFECT_ROOT.name,
						eventName,
						objectName,
						bone
					}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Components/NKCComSpineSkeletonAnimationEvent.cs", 414);
				}
			}
			gameObject.transform.SetParent(this.m_NKM_GLOBAL_EFFECT.transform);
			if (!gameObject.activeSelf)
			{
				gameObject.SetActive(true);
			}
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x000EEDA1 File Offset: 0x000ECFA1
		private void Event_SOUND(string eventName, string objectName)
		{
			NKCSoundManager.PlaySound(objectName, 1f, this.m_EFFECT_ROOT.transform.position.x, 800f, false, 0f, false, 0f);
		}

		// Token: 0x04002FBE RID: 12222
		public GameObject m_EFFECT_ROOT;

		// Token: 0x04002FBF RID: 12223
		private NKC_FX_LIFETIME[] m_FX_Lifetime;

		// Token: 0x04002FC0 RID: 12224
		private bool m_bAddEvent;

		// Token: 0x04002FC1 RID: 12225
		private bool m_bActiveEvent = true;

		// Token: 0x04002FC2 RID: 12226
		private SkeletonAnimation m_SkeletonAnimation;

		// Token: 0x04002FC3 RID: 12227
		private SkeletonGraphic m_SkeletonGraphic;

		// Token: 0x04002FC4 RID: 12228
		private string m_SkeletonDataAssetName = "";

		// Token: 0x04002FC5 RID: 12229
		private char[] m_SplitToken = new char[]
		{
			':'
		};

		// Token: 0x04002FC6 RID: 12230
		private Dictionary<string, Bone> m_dicBone = new Dictionary<string, Bone>();

		// Token: 0x04002FC7 RID: 12231
		private HashSet<GameObject> m_setGameObject = new HashSet<GameObject>();

		// Token: 0x04002FC8 RID: 12232
		private GameObject m_NKM_GLOBAL_EFFECT;

		// Token: 0x04002FC9 RID: 12233
		private Dictionary<string, List<GameObject>> m_dicGlobalEffect = new Dictionary<string, List<GameObject>>();

		// Token: 0x04002FCA RID: 12234
		private float sinceTime;

		// Token: 0x04002FCB RID: 12235
		private float deltatime;

		// Token: 0x04002FCC RID: 12236
		private float current;

		// Token: 0x04002FCD RID: 12237
		private TrackEntry currentEntry;
	}
}
