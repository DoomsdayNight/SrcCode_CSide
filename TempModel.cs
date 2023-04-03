using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

// Token: 0x0200000D RID: 13
public class TempModel : INotifyPropertyChanged
{
	// Token: 0x14000002 RID: 2
	// (add) Token: 0x06000083 RID: 131 RVA: 0x00003068 File Offset: 0x00001268
	// (remove) Token: 0x06000084 RID: 132 RVA: 0x000030A0 File Offset: 0x000012A0
	public event PropertyChangedEventHandler PropertyChanged;

	// Token: 0x06000085 RID: 133 RVA: 0x000030D5 File Offset: 0x000012D5
	private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
	{
		this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
	}

	// Token: 0x0400002C RID: 44
	public int _model;
}
