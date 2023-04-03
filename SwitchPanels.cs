using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000030 RID: 48
public class SwitchPanels : MonoBehaviour
{
	// Token: 0x0600016A RID: 362 RVA: 0x000074C0 File Offset: 0x000056C0
	private void Awake()
	{
		base.GetComponent<Toggle>().onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleClick));
	}

	// Token: 0x0600016B RID: 363 RVA: 0x000074DE File Offset: 0x000056DE
	public void OnToggleClick(bool isActive)
	{
		this.Menu.SetActive(isActive);
		this.Panel.SetActive(!isActive);
	}

	// Token: 0x04000118 RID: 280
	public GameObject Menu;

	// Token: 0x04000119 RID: 281
	public GameObject Panel;
}
