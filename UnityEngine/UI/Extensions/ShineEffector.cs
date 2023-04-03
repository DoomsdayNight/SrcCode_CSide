using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002E8 RID: 744
	[ExecuteInEditMode]
	[RequireComponent(typeof(Image))]
	[AddComponentMenu("UI/Effects/Extensions/Shining Effect")]
	public class ShineEffector : MonoBehaviour
	{
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x0003749B File Offset: 0x0003569B
		// (set) Token: 0x06001057 RID: 4183 RVA: 0x000374A3 File Offset: 0x000356A3
		public float YOffset
		{
			get
			{
				return this.yOffset;
			}
			set
			{
				this.ChangeVal(value);
				this.yOffset = value;
			}
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x000374B4 File Offset: 0x000356B4
		private void OnEnable()
		{
			if (this.effector == null)
			{
				GameObject gameObject = new GameObject("effector");
				this.effectRoot = new GameObject("ShineEffect");
				this.effectRoot.transform.SetParent(base.transform);
				this.effectRoot.AddComponent<Image>().sprite = base.gameObject.GetComponent<Image>().sprite;
				this.effectRoot.GetComponent<Image>().type = base.gameObject.GetComponent<Image>().type;
				this.effectRoot.AddComponent<Mask>().showMaskGraphic = false;
				this.effectRoot.transform.localScale = Vector3.one;
				this.effectRoot.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
				this.effectRoot.GetComponent<RectTransform>().anchorMax = Vector2.one;
				this.effectRoot.GetComponent<RectTransform>().anchorMin = Vector2.zero;
				this.effectRoot.GetComponent<RectTransform>().offsetMax = Vector2.zero;
				this.effectRoot.GetComponent<RectTransform>().offsetMin = Vector2.zero;
				this.effectRoot.transform.SetAsFirstSibling();
				gameObject.AddComponent<RectTransform>();
				gameObject.transform.SetParent(this.effectRoot.transform);
				this.effectorRect = gameObject.GetComponent<RectTransform>();
				this.effectorRect.localScale = Vector3.one;
				this.effectorRect.anchoredPosition3D = Vector3.zero;
				this.effectorRect.gameObject.AddComponent<ShineEffect>();
				this.effectorRect.anchorMax = Vector2.one;
				this.effectorRect.anchorMin = Vector2.zero;
				this.effectorRect.Rotate(0f, 0f, -8f);
				this.effector = gameObject.GetComponent<ShineEffect>();
				this.effectorRect.offsetMax = Vector2.zero;
				this.effectorRect.offsetMin = Vector2.zero;
				this.OnValidate();
			}
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x000376A8 File Offset: 0x000358A8
		private void OnValidate()
		{
			this.effector.Yoffset = this.yOffset;
			this.effector.Width = this.width;
			if (this.yOffset <= -1f || this.yOffset >= 1f)
			{
				this.effectRoot.SetActive(false);
				return;
			}
			if (!this.effectRoot.activeSelf)
			{
				this.effectRoot.SetActive(true);
			}
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00037718 File Offset: 0x00035918
		private void ChangeVal(float value)
		{
			this.effector.Yoffset = value;
			if (value <= -1f || value >= 1f)
			{
				this.effectRoot.SetActive(false);
				return;
			}
			if (!this.effectRoot.activeSelf)
			{
				this.effectRoot.SetActive(true);
			}
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x00037767 File Offset: 0x00035967
		private void OnDestroy()
		{
			if (!Application.isPlaying)
			{
				Object.DestroyImmediate(this.effectRoot);
				return;
			}
			Object.Destroy(this.effectRoot);
		}

		// Token: 0x04000B46 RID: 2886
		public ShineEffect effector;

		// Token: 0x04000B47 RID: 2887
		[SerializeField]
		[HideInInspector]
		private GameObject effectRoot;

		// Token: 0x04000B48 RID: 2888
		[Range(-1f, 1f)]
		public float yOffset = -1f;

		// Token: 0x04000B49 RID: 2889
		[Range(0.1f, 1f)]
		public float width = 0.5f;

		// Token: 0x04000B4A RID: 2890
		private RectTransform effectorRect;
	}
}
