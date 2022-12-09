namespace MyPlexManager.Models;
// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false, ElementName = "MediaContainer")]
public partial class PlexTVUserDevices : IPlexTVUserDevices
{

	private PlexTVUserDevice[]? deviceField;

	private string? publicAddressField;

	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("Device")]
	public PlexTVUserDevice[] Device
	{
		get
		{
			return this.deviceField!;
		}
		set
		{
			this.deviceField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string publicAddress
	{
		get
		{
			return this.publicAddressField!;
		}
		set
		{
			this.publicAddressField = value;
		}
	}
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class PlexTVUserDevice
{

	private PlexTVUserDeviceConnection[]? connectionField;

	private string? nameField;

	private string? publicAddressField;

	private string? productField;

	private string? productVersionField;

	private string? platformField;

	private string? platformVersionField;

	private string? deviceField;

	private string? modelField;

	private string? vendorField;

	private string? providesField;

	private string? clientIdentifierField;

	private string? versionField;

	private uint idField;

	private string? tokenField;

	private uint createdAtField;

	private uint lastSeenAtField;

	private string? screenResolutionField;

	private string? screenDensityField;

	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("Connection")]
	public PlexTVUserDeviceConnection[] Connection
	{
		get
		{
			return this.connectionField!;
		}
		set
		{
			this.connectionField = value;
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
	public string publicAddress
	{
		get
		{
			return this.publicAddressField!;
		}
		set
		{
			this.publicAddressField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string product
	{
		get
		{
			return this.productField!;
		}
		set
		{
			this.productField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string productVersion
	{
		get
		{
			return this.productVersionField!;
		}
		set
		{
			this.productVersionField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string platform
	{
		get
		{
			return this.platformField!;
		}
		set
		{
			this.platformField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string platformVersion
	{
		get
		{
			return this.platformVersionField!;
		}
		set
		{
			this.platformVersionField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string device
	{
		get
		{
			return this.deviceField!;
		}
		set
		{
			this.deviceField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string model
	{
		get
		{
			return this.modelField!;
		}
		set
		{
			this.modelField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string vendor
	{
		get
		{
			return this.vendorField!;
		}
		set
		{
			this.vendorField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string provides
	{
		get
		{
			return this.providesField!;
		}
		set
		{
			this.providesField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string clientIdentifier
	{
		get
		{
			return this.clientIdentifierField!;
		}
		set
		{
			this.clientIdentifierField = value;
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
	public string token
	{
		get
		{
			return this.tokenField!;
		}
		set
		{
			this.tokenField = value;
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
	public uint lastSeenAt
	{
		get
		{
			return this.lastSeenAtField;
		}
		set
		{
			this.lastSeenAtField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string screenResolution
	{
		get
		{
			return this.screenResolutionField!;
		}
		set
		{
			this.screenResolutionField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string screenDensity
	{
		get
		{
			return this.screenDensityField!;
		}
		set
		{
			this.screenDensityField = value;
		}
	}
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class PlexTVUserDeviceConnection
{

	private string? protocolField;

	private string? addressField;

	private ushort portField;

	private string? uriField;

	private byte localField;

	private string? relayField;

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string protocol
	{
		get
		{
			return this.protocolField!;
		}
		set
		{
			this.protocolField = value;
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
	public ushort port
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
	public string uri
	{
		get
		{
			return this.uriField!;
		}
		set
		{
			this.uriField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public byte local
	{
		get
		{
			return this.localField;
		}
		set
		{
			this.localField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string relay
	{
		get
		{
			return this.relayField!;
		}
		set
		{
			this.relayField = value;
		}
	}
}

