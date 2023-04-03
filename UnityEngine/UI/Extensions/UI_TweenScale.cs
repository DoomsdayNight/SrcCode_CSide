using System;
using System.Collections;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x0200034D RID: 845
	[AddComponentMenu("UI/Extensions/UI Tween Scale")]
	public class UI_TweenScale : MonoBehaviour
	{
		// Token: 0x060013FA RID: 5114 RVA: 0x0004B414 File Offset: 0x00049614
		private void Awake()
		{
			this.myTransform = base.GetComponent<Transform>();
			this.initScale = this.myTransform.localScale;
			if (this.playAtAwake)
			{
				this.Play();
			}
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x0004B441 File Offset: 0x00049641
		public void Play()
		{
			base.StartCoroutine("Tween");
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x0004B44F File Offset: 0x0004964F
		private IEnumerator Tween()
		{
			this.myTransform.localScale = this.initScale;
			float t = 0f;
			float maxT = this.animCurve.keys[this.animCurve.length - 1].time;
			while (t < maxT || this.isLoop)
			{
				t += this.speed * Time.deltaTime;
				if (!this.isUniform)
				{
					this.newScale.x = 1f * this.animCurve.Evaluate(t);
					this.newScale.y = 1f * this.animCurveY.Evaluate(t);
					this.myTransform.localScale = this.newScale;
				}
				else
				{
					this.myTransform.localScale = Vector3.one * this.animCurve.Evaluate(t);
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x0004B45E File Offset: 0x0004965E
		public void ResetTween()
		{
			base.StopCoroutine("Tween");
			this.myTransform.localScale = this.initScale;
		}

		// Token: 0x04000DE2 RID: 3554
		public AnimationCurve animCurve;

		// Token: 0x04000DE3 RID: 3555
		[Tooltip("Animation speed multiplier")]
		public float speed = 1f;

		// Token: 0x04000DE4 RID: 3556
		[Tooltip("If true animation will loop, for best effect set animation curve to loop on start and end point")]
		public bool isLoop;

		// Token: 0x04000DE5 RID: 3557
		[Tooltip("If true animation will start automatically, otherwise you need to call Play() method to start the animation")]
		public bool playAtAwake;

		// Token: 0x04000DE6 RID: 3558
		[Space(10f)]
		[Header("Non uniform scale")]
		[Tooltip("If true component will scale by the same amount in X and Y axis, otherwise use animCurve for X scale and animCurveY for Y scale")]
		public bool isUniform = true;

		// Token: 0x04000DE7 RID: 3559
		public AnimationCurve animCurveY;

		// Token: 0x04000DE8 RID: 3560
		private Vector3 initScale;

		// Token: 0x04000DE9 RID: 3561
		private Transform myTransform;

		// Token: 0x04000DEA RID: 3562
		private Vector3 newScale = Vector3.one;
	}
}
