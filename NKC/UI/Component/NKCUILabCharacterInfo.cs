using System;
using NKM;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.Component
{
	// Token: 0x02000C60 RID: 3168
	public class NKCUILabCharacterInfo : MonoBehaviour
	{
		// Token: 0x06009380 RID: 37760 RVA: 0x003258D8 File Offset: 0x00323AD8
		public void Init(NKCUILabCharacterInfo.OnClickChangeCharacterButton _dOnClickChangeCharacterButton)
		{
			this.dOnClickChangeCharacterButton = _dOnClickChangeCharacterButton;
			NKCUICharInfoSummary uicharInfo = this.m_UICharInfo;
			if (uicharInfo != null)
			{
				uicharInfo.SetUnitClassRootActive(false);
			}
			if (this.m_cbtnChangeCharacter != null)
			{
				this.m_cbtnChangeCharacter.PointerClick.RemoveAllListeners();
				this.m_cbtnChangeCharacter.PointerClick.AddListener(new UnityAction(this.OnChangeCharacterButton));
			}
			if (this.m_UICharInfo != null)
			{
				this.m_UICharInfo.Init(true);
			}
		}

		// Token: 0x06009381 RID: 37761 RVA: 0x00325954 File Offset: 0x00323B54
		public void SetData(NKMUnitData unitData)
		{
			this.m_UICharInfo.SetData(unitData);
			Debug.Log("unit LB status : " + NKMUnitLimitBreakManager.GetUnitLimitbreakStatus(unitData).ToString());
		}

		// Token: 0x06009382 RID: 37762 RVA: 0x00325990 File Offset: 0x00323B90
		public void OnChangeCharacterButton()
		{
			if (this.dOnClickChangeCharacterButton != null)
			{
				this.dOnClickChangeCharacterButton();
			}
		}

		// Token: 0x04008070 RID: 32880
		public NKCUICharInfoSummary m_UICharInfo;

		// Token: 0x04008071 RID: 32881
		public NKCUIComButton m_cbtnChangeCharacter;

		// Token: 0x04008072 RID: 32882
		private NKCUILabCharacterInfo.OnClickChangeCharacterButton dOnClickChangeCharacterButton;

		// Token: 0x02001A1C RID: 6684
		// (Invoke) Token: 0x0600BB1A RID: 47898
		public delegate void OnClickChangeCharacterButton();
	}
}
