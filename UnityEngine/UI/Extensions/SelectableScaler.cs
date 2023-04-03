using System;
using System.Collections;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000340 RID: 832
	[AddComponentMenu("UI/Extensions/Selectable Scalar")]
	[RequireComponent(typeof(Button))]
	public class SelectableScaler : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
	{
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x0004940F File Offset: 0x0004760F
		public Selectable Target
		{
			get
			{
				if (this.selectable == null)
				{
					this.selectable = base.GetComponent<Selectable>();
				}
				return this.selectable;
			}
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00049431 File Offset: 0x00047631
		private void Awake()
		{
			if (this.target == null)
			{
				this.target = base.transform;
			}
			this.initScale = this.target.localScale;
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0004945E File Offset: 0x0004765E
		private void OnEnable()
		{
			this.target.localScale = this.initScale;
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x00049471 File Offset: 0x00047671
		public void OnPointerDown(PointerEventData eventData)
		{
			if (this.Target != null && !this.Target.interactable)
			{
				return;
			}
			base.StopCoroutine("ScaleOUT");
			base.StartCoroutine("ScaleIN");
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x000494A6 File Offset: 0x000476A6
		public void OnPointerUp(PointerEventData eventData)
		{
			if (this.Target != null && !this.Target.interactable)
			{
				return;
			}
			base.StopCoroutine("ScaleIN");
			base.StartCoroutine("ScaleOUT");
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x000494DB File Offset: 0x000476DB
		private IEnumerator ScaleIN()
		{
			if (this.animCurve.keys.Length != 0)
			{
				this.target.localScale = this.initScale;
				float t = 0f;
				float maxT = this.animCurve.keys[this.animCurve.length - 1].time;
				while (t < maxT)
				{
					t += this.speed * Time.unscaledDeltaTime;
					this.target.localScale = Vector3.one * this.animCurve.Evaluate(t);
					yield return null;
				}
			}
			yield break;
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x000494EA File Offset: 0x000476EA
		private IEnumerator ScaleOUT()
		{
			if (this.animCurve.keys.Length != 0)
			{
				float t = 0f;
				float maxT = this.animCurve.keys[this.animCurve.length - 1].time;
				while (t < maxT)
				{
					t += this.speed * Time.unscaledDeltaTime;
					this.target.localScale = Vector3.one * this.animCurve.Evaluate(maxT - t);
					yield return null;
				}
				base.transform.localScale = this.initScale;
			}
			yield break;
		}

		// Token: 0x04000D83 RID: 3459
		public AnimationCurve animCurve;

		// Token: 0x04000D84 RID: 3460
		[Tooltip("Animation speed multiplier")]
		public float speed = 1f;

		// Token: 0x04000D85 RID: 3461
		private Vector3 initScale;

		// Token: 0x04000D86 RID: 3462
		public Transform target;

		// Token: 0x04000D87 RID: 3463
		private Selectable selectable;
	}
}
