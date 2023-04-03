using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002B1 RID: 689
	[RequireComponent(typeof(HorizontalOrVerticalLayoutGroup), typeof(ContentSizeFitter), typeof(ToggleGroup))]
	[AddComponentMenu("UI/Extensions/Accordion/Accordion Group")]
	public class Accordion : MonoBehaviour
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x00029E04 File Offset: 0x00028004
		[HideInInspector]
		public bool ExpandVerticval
		{
			get
			{
				return this.m_expandVertical;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x00029E0C File Offset: 0x0002800C
		// (set) Token: 0x06000E02 RID: 3586 RVA: 0x00029E14 File Offset: 0x00028014
		public Accordion.Transition transition
		{
			get
			{
				return this.m_Transition;
			}
			set
			{
				this.m_Transition = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x00029E1D File Offset: 0x0002801D
		// (set) Token: 0x06000E04 RID: 3588 RVA: 0x00029E25 File Offset: 0x00028025
		public float transitionDuration
		{
			get
			{
				return this.m_TransitionDuration;
			}
			set
			{
				this.m_TransitionDuration = value;
			}
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x00029E2E File Offset: 0x0002802E
		private void Awake()
		{
			this.m_expandVertical = !base.GetComponent<HorizontalLayoutGroup>();
			base.GetComponent<ToggleGroup>();
		}

		// Token: 0x040009B2 RID: 2482
		private bool m_expandVertical = true;

		// Token: 0x040009B3 RID: 2483
		[SerializeField]
		private Accordion.Transition m_Transition;

		// Token: 0x040009B4 RID: 2484
		[SerializeField]
		private float m_TransitionDuration = 0.3f;

		// Token: 0x02001121 RID: 4385
		public enum Transition
		{
			// Token: 0x040091A1 RID: 37281
			Instant,
			// Token: 0x040091A2 RID: 37282
			Tween
		}
	}
}
