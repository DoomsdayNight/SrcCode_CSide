using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200094C RID: 2380
	public class NKCUIComTextUnitLevel : Text
	{
		// Token: 0x1700111D RID: 4381
		// (get) Token: 0x06005EE6 RID: 24294 RVA: 0x001D77A8 File Offset: 0x001D59A8
		// (set) Token: 0x06005EE7 RID: 24295 RVA: 0x001D77B0 File Offset: 0x001D59B0
		public bool IsTranscendence { get; private set; }

		// Token: 0x06005EE8 RID: 24296 RVA: 0x001D77B9 File Offset: 0x001D59B9
		protected override void Awake()
		{
			this.Init();
		}

		// Token: 0x06005EE9 RID: 24297 RVA: 0x001D77C1 File Offset: 0x001D59C1
		private void Init()
		{
			if (this.m_bInit)
			{
				return;
			}
			this.originalColor = base.color;
			this.m_bInit = true;
		}

		// Token: 0x06005EEA RID: 24298 RVA: 0x001D77E0 File Offset: 0x001D59E0
		public void SetLevel(NKMUnitData unitData, int buffUnitLevel, params Text[] linkedLabels)
		{
			if (unitData == null)
			{
				this.SetText("0", false, linkedLabels);
				return;
			}
			this.SetText((unitData.m_UnitLevel + buffUnitLevel).ToString(), NKMUnitLimitBreakManager.IsTranscendenceUnit(unitData), linkedLabels);
		}

		// Token: 0x06005EEB RID: 24299 RVA: 0x001D781B File Offset: 0x001D5A1B
		public void SetLevel(NKMOperator operatorData, params Text[] linkedLabels)
		{
			if (operatorData == null)
			{
				this.SetText("0", false, linkedLabels);
				return;
			}
			this.SetText(operatorData.level.ToString(), false, linkedLabels);
		}

		// Token: 0x06005EEC RID: 24300 RVA: 0x001D7841 File Offset: 0x001D5A41
		public void SetLevel(NKMUnitData unitData, int buffUnitLevel, string format, params Text[] linkedLabels)
		{
			if (unitData == null)
			{
				this.SetText(string.Format(format, 0), false, linkedLabels);
				return;
			}
			this.SetText(string.Format(format, unitData.m_UnitLevel), NKMUnitLimitBreakManager.IsTranscendenceUnit(unitData), linkedLabels);
		}

		// Token: 0x06005EED RID: 24301 RVA: 0x001D787B File Offset: 0x001D5A7B
		public void SetText(string _text, NKMUnitData unitData, params Text[] linkedLabels)
		{
			this.SetText(_text, NKMUnitLimitBreakManager.IsTranscendenceUnit(unitData), linkedLabels);
		}

		// Token: 0x06005EEE RID: 24302 RVA: 0x001D788C File Offset: 0x001D5A8C
		public void SetText(string _text, bool bTranscendence, params Text[] linkedLabels)
		{
			if (!this.m_bInit)
			{
				this.Init();
			}
			this.text = _text;
			this.IsTranscendence = bTranscendence;
			if (bTranscendence)
			{
				this.color = NKCUIComTextUnitLevel.transcendenceColor;
				foreach (Text text in linkedLabels)
				{
					if (text != null)
					{
						text.color = NKCUIComTextUnitLevel.transcendenceColor;
					}
				}
				using (List<Text>.Enumerator enumerator = this.m_lstLinkedText.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Text text2 = enumerator.Current;
						if (text2 != null)
						{
							text2.color = NKCUIComTextUnitLevel.transcendenceColor;
						}
					}
					return;
				}
			}
			this.color = this.originalColor;
			foreach (Text text3 in linkedLabels)
			{
				if (text3 != null)
				{
					text3.color = this.originalColor;
				}
			}
			foreach (Text text4 in this.m_lstLinkedText)
			{
				if (text4 != null)
				{
					text4.color = this.originalColor;
				}
			}
		}

		// Token: 0x04004B0E RID: 19214
		[Header("연계된 다른 Text들. 색이 같이 바뀐다")]
		public List<Text> m_lstLinkedText;

		// Token: 0x04004B0F RID: 19215
		private Color originalColor;

		// Token: 0x04004B10 RID: 19216
		private static readonly Color transcendenceColor = new Color(0.772549f, 0.4823529f, 0.9568627f, 1f);

		// Token: 0x04004B12 RID: 19218
		private bool m_bInit;
	}
}
