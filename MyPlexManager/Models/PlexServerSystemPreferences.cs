namespace MyPlexManager.Models;

// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false, ElementName = "MediaContainer")]
public partial class PlexServerSystemPreferences : IPlexServerSystemPreferences
{

	private ServerSystemSetting[]? settingField;

	private byte noHistoryField;

	private byte replaceParentField;

	private byte sizeField;

	private string? identifierField;

	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("Setting")]
	public ServerSystemSetting[] Setting
	{
		get
		{
			return this.settingField!;
		}
		set
		{
			this.settingField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public byte noHistory
	{
		get
		{
			return this.noHistoryField;
		}
		set
		{
			this.noHistoryField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public byte replaceParent
	{
		get
		{
			return this.replaceParentField;
		}
		set
		{
			this.replaceParentField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public byte size
	{
		get
		{
			return this.sizeField;
		}
		set
		{
			this.sizeField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string identifier
	{
		get
		{
			return this.identifierField!;
		}
		set
		{
			this.identifierField = value;
		}
	}
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ServerSystemSetting : IServerSystemSetting
{

	private bool secureField;

	private byte defaultField;

	private byte valueField;

	private string? labelField;

	private string? valuesField;

	private string? typeField;

	private string? idField;

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public bool secure
	{
		get
		{
			return this.secureField;
		}
		set
		{
			this.secureField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public byte @default
	{
		get
		{
			return this.defaultField;
		}
		set
		{
			this.defaultField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public byte value
	{
		get
		{
			return this.valueField;
		}
		set
		{
			this.valueField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string label
	{
		get
		{
			return this.labelField!;
		}
		set
		{
			this.labelField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string values
	{
		get
		{
			return this.valuesField!;
		}
		set
		{
			this.valuesField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string type
	{
		get
		{
			return this.typeField!;
		}
		set
		{
			this.typeField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string id
	{
		get
		{
			return this.idField!;
		}
		set
		{
			this.idField = value;
		}
	}
}

