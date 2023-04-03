using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000338 RID: 824
	[RequireComponent(typeof(Text))]
	[AddComponentMenu("UI/Extensions/PPIViewer")]
	public class PPIViewer : MonoBehaviour
	{
		// Token: 0x0600135C RID: 4956 RVA: 0x00048915 File Offset: 0x00046B15
		private void Awake()
		{
			this.label = base.GetComponent<Text>();
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00048924 File Offset: 0x00046B24
		private void Start()
		{
			if (this.label != null)
			{
				this.label.text = "PPI: " + Screen.dpi.ToString();
			}
		}

		// Token: 0x04000D6C RID: 3436
		private Text label;
	}
}
