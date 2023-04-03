using System;
using NKC;
using Spine;
using Spine.Unity;
using UnityEngine;

// Token: 0x02000024 RID: 36
[ExecuteAlways]
public class NKC_FX_SPINE_EVENT : MonoBehaviour
{
	// Token: 0x06000118 RID: 280 RVA: 0x00004F48 File Offset: 0x00003148
	private void OnEnable()
	{
		this.Initialize();
	}

	// Token: 0x06000119 RID: 281 RVA: 0x00004F50 File Offset: 0x00003150
	private void Start()
	{
		Shader.WarmupAllShaders();
		if (Application.isPlaying)
		{
			this.CheckEventMode();
			this.InitExternal(false);
		}
	}

	// Token: 0x0600011A RID: 282 RVA: 0x00004F6C File Offset: 0x0000316C
	public void Initialize()
	{
		if (this.m_SpinePrefab != null)
		{
			Transform transform = this.m_SpinePrefab.transform.Find("SPINE_SkeletonAnimation");
			if (transform != null)
			{
				this.skeletonAnimation = transform.GetComponentInChildren<SkeletonAnimation>(true);
				if (this.skeletonAnimation != null)
				{
					this.skeletonDataAsset = this.skeletonAnimation.SkeletonDataAsset;
					this.InitSkeletonAnimation();
					return;
				}
			}
			else
			{
				transform = this.m_SpinePrefab.transform.Find("SPINE_SkeletonGraphic");
				if (transform != null)
				{
					this.skeletonGraphic = transform.GetComponentInChildren<SkeletonGraphic>(true);
					if (this.skeletonGraphic != null)
					{
						this.skeletonDataAsset = this.skeletonGraphic.SkeletonDataAsset;
						this.InitSkeletonGraphic();
						return;
					}
				}
				else
				{
					Debug.LogWarning("Can not found SPINE Object.");
				}
			}
		}
	}

	// Token: 0x0600011B RID: 283 RVA: 0x00005038 File Offset: 0x00003238
	public void InitializeAnimationData()
	{
		if (this.skeletonDataAsset == null)
		{
			return;
		}
		this.animations = this.skeletonDataAsset.GetAnimationStateData().SkeletonData.Animations;
		int count = this.animations.Count;
		this.AnimationDatas = new NKC_FX_SPINE_EVENT.AnimationData[count];
		for (int i = 0; i < count; i++)
		{
			Spine.Animation animation = this.animations.Items[i];
			this.AnimationDatas[i] = new NKC_FX_SPINE_EVENT.AnimationData();
			this.AnimationDatas[i].m_AnimationName = animation.Name;
			this.AnimationDatas[i].m_Duration = animation.Duration;
			this.AnimationDatas[i].m_Loop = false;
			if (animation.Name.Contains("ASTAND") || animation.Name.Contains("RUN") || animation.Name.Contains("IDLE"))
			{
				this.AnimationDatas[i].m_Loop = true;
			}
			this.AnimationDatas[i].m_KeyColor = Color.white;
			this.AnimationDatas[i].m_TimeScale = 1f;
		}
	}

	// Token: 0x0600011C RID: 284 RVA: 0x00005150 File Offset: 0x00003350
	private void InitExternal(bool _active)
	{
		if (this.m_ExternalPrefab)
		{
			for (int i = 0; i < this.m_ExternalPrefab.transform.childCount; i++)
			{
				GameObject gameObject = this.m_ExternalPrefab.transform.GetChild(i).gameObject;
				if (gameObject.name != "SPINE_SkeletonGraphic")
				{
					gameObject.SetActive(_active);
				}
			}
		}
	}

	// Token: 0x0600011D RID: 285 RVA: 0x000051B8 File Offset: 0x000033B8
	private void LoadSpineAnimationEvent()
	{
		if (this.m_SpinePrefab != null)
		{
			this.SkeletonEvent = this.m_SpinePrefab.GetComponentInChildren<NKCComSpineSkeletonAnimationEvent>();
			if (this.SkeletonEvent != null)
			{
				if (this.SkeletonEvent.m_EFFECT_ROOT != null)
				{
					this.VFX = this.SkeletonEvent.m_EFFECT_ROOT.transform;
					return;
				}
				Debug.LogWarning(this.m_SpinePrefab.name + " -> <color=red>EFFECT_ROOT is NULL</color>", this.SkeletonEvent);
				return;
			}
			else
			{
				Debug.LogWarning(this.m_SpinePrefab.name + " -> <color=red>Event Component can not found.</color>", this.SkeletonEvent);
			}
		}
	}

	// Token: 0x0600011E RID: 286 RVA: 0x00005260 File Offset: 0x00003460
	private void CheckEventMode()
	{
		this.LoadSpineAnimationEvent();
		if (this.DebugMode)
		{
			Debug.Log(this.m_SpinePrefab.name + " -> <color=red>DEBUG MODE</color>");
			this.SkeletonEvent.enabled = false;
			return;
		}
		Debug.Log(this.m_SpinePrefab.name + " -> <color=green>GAME MODE</color>");
		this.SkeletonEvent.enabled = true;
	}

	// Token: 0x0600011F RID: 287 RVA: 0x000052C8 File Offset: 0x000034C8
	private void ResetEventsActivation()
	{
		for (int i = 0; i < this.EventList.Length; i++)
		{
			this.EventList[i].activated = false;
		}
	}

	// Token: 0x06000120 RID: 288 RVA: 0x000052F8 File Offset: 0x000034F8
	private void InitSkeletonAnimation()
	{
		this.skeletonAnimation.Awake();
		if (this.EventList != null)
		{
			this.skeletonAnimation.UpdateComplete += delegate(ISkeletonAnimation <p0>)
			{
				if (this.currentTrackEntry != null)
				{
					this.UpdateInternal(this.currentTrackEntry);
				}
			};
			this.skeletonAnimation.AnimationState.Start += delegate(TrackEntry entry)
			{
				if (this.currentTrackEntry != null && this.currentTrackEntry.Animation != null && this.currentTrackEntry.Animation.Name != entry.Animation.Name)
				{
					this.ResetEventsActivation();
				}
				this.currentTrackEntry = entry;
			};
			this.skeletonAnimation.AnimationState.Complete += delegate(TrackEntry entry)
			{
				this.ResetEventsActivation();
			};
		}
		this.init = true;
	}

	// Token: 0x06000121 RID: 289 RVA: 0x00005370 File Offset: 0x00003570
	private void InitSkeletonGraphic()
	{
		this.skeletonGraphic.Initialize(false);
		if (this.EventList != null)
		{
			this.skeletonGraphic.UpdateComplete += delegate(ISkeletonAnimation <p0>)
			{
				if (this.currentTrackEntry != null)
				{
					this.UpdateInternal(this.currentTrackEntry);
				}
			};
			this.skeletonGraphic.AnimationState.Start += delegate(TrackEntry entry)
			{
				if (this.currentTrackEntry != null && this.currentTrackEntry.Animation != null && this.currentTrackEntry.Animation.Name != entry.Animation.Name)
				{
					this.ResetEventsActivation();
				}
				this.currentTrackEntry = entry;
			};
			this.skeletonGraphic.AnimationState.Complete += delegate(TrackEntry entry)
			{
				this.ResetEventsActivation();
			};
		}
		this.init = true;
	}

	// Token: 0x06000122 RID: 290 RVA: 0x000053E8 File Offset: 0x000035E8
	private void UpdateInternal(TrackEntry entry)
	{
		if (this.init && base.enabled)
		{
			this.currentTime = entry.AnimationTime;
			if (0f <= this.currentTime && this.currentTime < entry.Animation.Duration)
			{
				for (int i = 0; i < this.EventList.Length; i++)
				{
					string text = "<color=black><b>";
					string text2 = "<color=yellow><b>";
					if (this.EventList[i].m_Enable && !this.EventList[i].activated && this.EventList[i].m_AnimationName == entry.Animation.Name && this.EventList[i].m_Frame < this.currentTime * 30f)
					{
						this.EventList[i].activated = true;
						if (!this.EventList[i].m_External)
						{
							if (!this.DebugMode)
							{
								break;
							}
							if (this.VFX.Find(this.EventList[i].m_EventName) != null && this.EventList[i].m_EventName != string.Empty)
							{
								GameObject gameObject = this.VFX.Find(this.EventList[i].m_EventName).gameObject;
								if (gameObject.activeInHierarchy)
								{
									gameObject.SetActive(false);
								}
								if (!gameObject.activeInHierarchy)
								{
									gameObject.SetActive(true);
								}
								if (this.DebugMode)
								{
									Debug.Log(string.Concat(new string[]
									{
										"<color=#84FF37><b>",
										entry.Animation.Name,
										"</b></color> : ",
										this.currentTime.ToString("N3"),
										" / ",
										entry.Animation.Duration.ToString("N3"),
										string.Format(", [{0} / {1}], {2}x -> ", this.EventList[i].m_Frame, Mathf.Round(entry.Animation.Duration * 30f), entry.TimeScale),
										text,
										this.EventList[i].m_EventName,
										"</b></color>"
									}));
								}
							}
							else if (this.DebugMode)
							{
								Debug.Log(string.Concat(new string[]
								{
									"<color=#84FF37><b>",
									entry.Animation.Name,
									"</b></color> : ",
									this.currentTime.ToString("N3"),
									" / ",
									entry.Animation.Duration.ToString("N3"),
									string.Format(", [{0} / {1}], {2}x -> ", this.EventList[i].m_Frame, Mathf.Round(entry.Animation.Duration * 30f), entry.TimeScale),
									text,
									this.EventList[i].m_EventName,
									"</b></color><color=red> (Not Found)</color>"
								}));
							}
						}
						else if (this.m_ExternalPrefab.transform.Find(this.EventList[i].m_EventName) != null && this.EventList[i].m_EventName != string.Empty)
						{
							GameObject gameObject2 = this.m_ExternalPrefab.transform.Find(this.EventList[i].m_EventName).gameObject;
							if (gameObject2.activeInHierarchy)
							{
								gameObject2.SetActive(false);
							}
							if (!gameObject2.activeInHierarchy)
							{
								gameObject2.SetActive(true);
							}
							if (this.DebugMode)
							{
								Debug.Log(string.Concat(new string[]
								{
									"<color=#84FF37><b>",
									entry.Animation.Name,
									"</b></color> : ",
									this.currentTime.ToString("N3"),
									" / ",
									entry.Animation.Duration.ToString("N3"),
									string.Format(", [{0} / {1}], {2}x -> ", this.EventList[i].m_Frame, Mathf.Round(entry.Animation.Duration * 30f), entry.TimeScale),
									text2,
									this.EventList[i].m_EventName,
									"</b></color>"
								}));
							}
						}
						else if (this.DebugMode)
						{
							Debug.Log(string.Concat(new string[]
							{
								"<color=#84FF37><b>",
								entry.Animation.Name,
								"</b></color> : ",
								this.currentTime.ToString("N3"),
								" / ",
								entry.Animation.Duration.ToString("N3"),
								string.Format(", [{0} / {1}], {2}x -> ", this.EventList[i].m_Frame, Mathf.Round(entry.Animation.Duration * 30f), entry.TimeScale),
								text2,
								this.EventList[i].m_EventName,
								"</b></color><color=red> (Not Found)</color>"
							}));
						}
					}
				}
			}
		}
	}

	// Token: 0x06000123 RID: 291 RVA: 0x00005954 File Offset: 0x00003B54
	public void SetAnimationName(string _animationName)
	{
		if (this.init && this.AnimationDatas.Length != 0)
		{
			if (this.ContainAnimation(_animationName))
			{
				if (this.currentTrackEntry != null)
				{
					this.ResetEventsActivation();
				}
				this.index = this.GetAnimationIndex(_animationName);
				bool loop = this.AnimationDatas[this.index].m_Loop;
				float timeScale = this.AnimationDatas[this.index].m_TimeScale;
				if (this.skeletonAnimation != null)
				{
					this.skeletonAnimation.AnimationState.SetAnimation(0, _animationName, loop).TimeScale = timeScale;
					return;
				}
				if (this.skeletonGraphic != null)
				{
					this.skeletonGraphic.AnimationState.SetAnimation(0, _animationName, loop).TimeScale = timeScale;
					return;
				}
			}
			else
			{
				Debug.LogWarning("Not found animation name. : " + _animationName);
			}
		}
	}

	// Token: 0x06000124 RID: 292 RVA: 0x00005A28 File Offset: 0x00003C28
	public void AddAnimationName(string _animationName)
	{
		if (this.init && this.AnimationDatas.Length != 0)
		{
			if (this.ContainAnimation(_animationName))
			{
				this.index = this.GetAnimationIndex(_animationName);
				bool loop = this.AnimationDatas[this.index].m_Loop;
				float timeScale = this.AnimationDatas[this.index].m_TimeScale;
				if (this.skeletonAnimation != null)
				{
					this.skeletonAnimation.AnimationState.AddAnimation(0, _animationName, loop, 0f).TimeScale = timeScale;
					return;
				}
				if (this.skeletonGraphic != null)
				{
					this.skeletonGraphic.AnimationState.AddAnimation(0, _animationName, loop, 0f).TimeScale = timeScale;
					return;
				}
			}
			else
			{
				Debug.LogWarning("Not found animation name. : " + _animationName);
			}
		}
	}

	// Token: 0x06000125 RID: 293 RVA: 0x00005AF8 File Offset: 0x00003CF8
	private bool ContainAnimation(string _animationName)
	{
		bool result = false;
		for (int i = 0; i < this.AnimationDatas.Length; i++)
		{
			if (this.AnimationDatas[i].m_AnimationName.Equals(_animationName))
			{
				result = true;
				break;
			}
		}
		return result;
	}

	// Token: 0x06000126 RID: 294 RVA: 0x00005B34 File Offset: 0x00003D34
	private int GetAnimationIndex(string _animationName)
	{
		int result = 0;
		for (int i = 0; i < this.AnimationDatas.Length; i++)
		{
			if (this.AnimationDatas[i].m_AnimationName.Equals(_animationName))
			{
				result = i;
				break;
			}
		}
		return result;
	}

	// Token: 0x040000A2 RID: 162
	public bool DebugMode;

	// Token: 0x040000A3 RID: 163
	public GameObject m_SpinePrefab;

	// Token: 0x040000A4 RID: 164
	public GameObject m_ExternalPrefab;

	// Token: 0x040000A5 RID: 165
	public string PathExport = "D:";

	// Token: 0x040000A6 RID: 166
	public bool AutoKeyName = true;

	// Token: 0x040000A7 RID: 167
	public GameObject m_BaseBtnPrefab;

	// Token: 0x040000A8 RID: 168
	public SkeletonDataAsset skeletonDataAsset;

	// Token: 0x040000A9 RID: 169
	private SkeletonAnimation skeletonAnimation;

	// Token: 0x040000AA RID: 170
	private SkeletonGraphic skeletonGraphic;

	// Token: 0x040000AB RID: 171
	private NKCComSpineSkeletonAnimationEvent SkeletonEvent;

	// Token: 0x040000AC RID: 172
	public NKC_FX_SPINE_EVENT.AnimationData[] AnimationDatas;

	// Token: 0x040000AD RID: 173
	public NKC_FX_SPINE_EVENT.AnimationEvent[] EventList;

	// Token: 0x040000AE RID: 174
	private ExposedList<Spine.Animation> animations;

	// Token: 0x040000AF RID: 175
	private TrackEntry currentTrackEntry;

	// Token: 0x040000B0 RID: 176
	private Transform VFX;

	// Token: 0x040000B1 RID: 177
	private float currentTime;

	// Token: 0x040000B2 RID: 178
	private int index;

	// Token: 0x040000B3 RID: 179
	private bool init;

	// Token: 0x040000B4 RID: 180
	private const int baseFrame = 30;

	// Token: 0x040000B5 RID: 181
	private int childCount;

	// Token: 0x040000B6 RID: 182
	private int newIndex;

	// Token: 0x040000B7 RID: 183
	private int animationIndex;

	// Token: 0x040000B8 RID: 184
	private int eventIndex;

	// Token: 0x040000B9 RID: 185
	private string[] sortedEventName;

	// Token: 0x040000BA RID: 186
	private Transform[] tempTrs;

	// Token: 0x020010ED RID: 4333
	[Serializable]
	public class AnimationData
	{
		// Token: 0x040090E0 RID: 37088
		public string m_AnimationName;

		// Token: 0x040090E1 RID: 37089
		public bool m_Loop;

		// Token: 0x040090E2 RID: 37090
		public Color m_KeyColor;

		// Token: 0x040090E3 RID: 37091
		public float m_TimeScale;

		// Token: 0x040090E4 RID: 37092
		public float m_Duration;
	}

	// Token: 0x020010EE RID: 4334
	[Serializable]
	public class AnimationEvent
	{
		// Token: 0x040090E5 RID: 37093
		public bool m_Enable;

		// Token: 0x040090E6 RID: 37094
		public bool m_External;

		// Token: 0x040090E7 RID: 37095
		[SpineAnimation("", "skeletonDataAsset", true, false)]
		public string m_AnimationName;

		// Token: 0x040090E8 RID: 37096
		public float m_Frame;

		// Token: 0x040090E9 RID: 37097
		public string m_EventName;

		// Token: 0x040090EA RID: 37098
		public int m_EventObjInstanceID;

		// Token: 0x040090EB RID: 37099
		[HideInInspector]
		public bool activated;
	}
}
