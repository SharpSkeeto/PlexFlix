namespace MyPlexManager.Models.XML;
// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false, ElementName = "MediaContainer")]
public partial class PlexTVServerLibraries : IPlexTVServerLibraries
{

	private Server? serverField;

	private string? friendlyNameField;

	private string? identifierField;

	private string? machineIdentifierField;

	private byte sizeField;

	/// <remarks/>
	public Server Server
	{
		get
		{
			return this.serverField!;
		}
		set
		{
			this.serverField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string friendlyName
	{
		get
		{
			return this.friendlyNameField!;
		}
		set
		{
			this.friendlyNameField = value;
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

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string machineIdentifier
	{
		get
		{
			return this.machineIdentifierField!;
		}
		set
		{
			this.machineIdentifierField = value;
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
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class Server
{

	private Library[]? sectionField;

	private string? nameField;

	private string? addressField;

	private byte portField;

	private string? versionField;

	private string? schemeField;

	private string? hostField;

	private string? localAddressesField;

	private string? machineIdentifierField;

	private uint createdAtField;

	private uint updatedAtField;

	private byte ownedField;

	private byte syncedField;

	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("Section")]
	public Library[] Libraries
	{
		get
		{
			return this.sectionField!;
		}
		set
		{
			this.sectionField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string name
	{
		get
		{
			return this.nameField!;
		}
		set
		{
			this.nameField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string address
	{
		get
		{
			return this.addressField!;
		}
		set
		{
			this.addressField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public byte port
	{
		get
		{
			return this.portField;
		}
		set
		{
			this.portField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string version
	{
		get
		{
			return this.versionField!;
		}
		set
		{
			this.versionField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string scheme
	{
		get
		{
			return this.schemeField!;
		}
		set
		{
			this.schemeField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string host
	{
		get
		{
			return this.hostField!;
		}
		set
		{
			this.hostField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string localAddresses
	{
		get
		{
			return this.localAddressesField!;
		}
		set
		{
			this.localAddressesField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string machineIdentifier
	{
		get
		{
			return this.machineIdentifierField!;
		}
		set
		{
			this.machineIdentifierField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public uint createdAt
	{
		get
		{
			return this.createdAtField;
		}
		set
		{
			this.createdAtField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public uint updatedAt
	{
		get
		{
			return this.updatedAtField;
		}
		set
		{
			this.updatedAtField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public byte owned
	{
		get
		{
			return this.ownedField;
		}
		set
		{
			this.ownedField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public byte synced
	{
		get
		{
			return this.syncedField;
		}
		set
		{
			this.syncedField = value;
		}
	}
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class Library
{

	private uint idField;

	private byte keyField;

	private string? typeField;

	private string? titleField;

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public uint id
	{
		get
		{
			return this.idField;
		}
		set
		{
			this.idField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public byte key
	{
		get
		{
			return this.keyField;
		}
		set
		{
			this.keyField = value;
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
	public string title
	{
		get
		{
			return this.titleField!;
		}
		set
		{
			this.titleField = value;
		}
	}
}


