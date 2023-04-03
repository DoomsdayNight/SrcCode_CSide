using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NKC.InfraTool;

// Token: 0x0200000B RID: 11
public class PatchToolVM : INotifyPropertyChanged
{
	// Token: 0x17000026 RID: 38
	// (get) Token: 0x0600006D RID: 109 RVA: 0x00002DD5 File Offset: 0x00000FD5
	// (set) Token: 0x0600006E RID: 110 RVA: 0x00002DDD File Offset: 0x00000FDD
	public string ConfigServerAddress
	{
		get
		{
			return this._configServerAddress;
		}
		set
		{
			if (this._configServerAddress == value)
			{
				return;
			}
			this._configServerAddress = value;
			this.NotifyPropertyChanged("ConfigServerAddress");
		}
	}

	// Token: 0x17000027 RID: 39
	// (get) Token: 0x0600006F RID: 111 RVA: 0x00002E00 File Offset: 0x00001000
	// (set) Token: 0x06000070 RID: 112 RVA: 0x00002E08 File Offset: 0x00001008
	public string ProtocolVersion
	{
		get
		{
			return this._protocolVersion;
		}
		set
		{
			if (this._protocolVersion == value)
			{
				return;
			}
			this._protocolVersion = value;
			this.NotifyPropertyChanged("ProtocolVersion");
		}
	}

	// Token: 0x17000028 RID: 40
	// (get) Token: 0x06000071 RID: 113 RVA: 0x00002E2B File Offset: 0x0000102B
	// (set) Token: 0x06000072 RID: 114 RVA: 0x00002E33 File Offset: 0x00001033
	public string Status
	{
		get
		{
			return this._status;
		}
		set
		{
			if (this._status == value)
			{
				return;
			}
			this._status = value;
			this.NotifyPropertyChanged("Status");
		}
	}

	// Token: 0x17000029 RID: 41
	// (get) Token: 0x06000073 RID: 115 RVA: 0x00002E56 File Offset: 0x00001056
	// (set) Token: 0x06000074 RID: 116 RVA: 0x00002E5E File Offset: 0x0000105E
	public string Log
	{
		get
		{
			return this._log;
		}
		set
		{
			if (this._log == value)
			{
				return;
			}
			this._log = value;
			this.NotifyPropertyChanged("Log");
		}
	}

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x06000075 RID: 117 RVA: 0x00002E81 File Offset: 0x00001081
	// (set) Token: 0x06000076 RID: 118 RVA: 0x00002E89 File Offset: 0x00001089
	public string Solution
	{
		get
		{
			return this._solution;
		}
		set
		{
			if (this._solution == value)
			{
				return;
			}
			this._solution = value;
			this.NotifyPropertyChanged("Solution");
		}
	}

	// Token: 0x14000001 RID: 1
	// (add) Token: 0x06000077 RID: 119 RVA: 0x00002EAC File Offset: 0x000010AC
	// (remove) Token: 0x06000078 RID: 120 RVA: 0x00002EE4 File Offset: 0x000010E4
	public event PropertyChangedEventHandler PropertyChanged;

	// Token: 0x06000079 RID: 121 RVA: 0x00002F19 File Offset: 0x00001119
	private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
	{
		this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
	}

	// Token: 0x04000025 RID: 37
	public readonly PatchCheckController PatchController = new PatchCheckController();

	// Token: 0x04000026 RID: 38
	private string _configServerAddress;

	// Token: 0x04000027 RID: 39
	private string _protocolVersion;

	// Token: 0x04000028 RID: 40
	private string _status;

	// Token: 0x04000029 RID: 41
	private string _log;

	// Token: 0x0400002A RID: 42
	private string _solution;
}
