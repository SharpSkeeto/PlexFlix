namespace MyPlexManager.Models;

// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false, ElementName = "MediaContainer")]
public partial class PlexTVAccountDevices : IPlexTVAccountDevices
{

	private PlexTVAccountDevice[]? deviceField;

	private byte sizeField;

	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("Device")]
	public PlexTVAccountDevice[] Device
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
public partial class PlexTVAccountDevice
{

	private PlexTVAccountDeviceConnection[]? connectionField;

	private string? nameField;

	private string? productField;

	private string? productVersionField;

	private string? platformField;

	private string? platformVersionField;

	private string? deviceField;

	private string? clientIdentifierField;

	private uint createdAtField;

	private uint lastSeenAtField;

	private string? providesField;

	private byte ownedField;

	private string? accessTokenField;

	private string? publicAddressField;

	private byte httpsRequiredField;

	private byte syncedField;

	private byte relayField;

	private byte dnsRebindingProtectionField;

	private byte natLoopbackSupportedField;

	private byte publicAddressMatchesField;

	private byte presenceField;

	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("Connection")]
	public PlexTVAccountDeviceConnection[] Connection
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
	public string accessToken
	{
		get
		{
			return this.accessTokenField!;
		}
		set
		{
			this.accessTokenField = value;
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
	public byte httpsRequired
	{
		get
		{
			return this.httpsRequiredField!;
		}
		set
		{
			this.httpsRequiredField = value;
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

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public byte relay
	{
		get
		{
			return this.relayField;
		}
		set
		{
			this.relayField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public byte dnsRebindingProtection
	{
		get
		{
			return this.dnsRebindingProtectionField;
		}
		set
		{
			this.dnsRebindingProtectionField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public byte natLoopbackSupported
	{
		get
		{
			return this.natLoopbackSupportedField;
		}
		set
		{
			this.natLoopbackSupportedField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public byte publicAddressMatches
	{
		get
		{
			return this.publicAddressMatchesField;
		}
		set
		{
			this.publicAddressMatchesField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public byte presence
	{
		get
		{
			return this.presenceField;
		}
		set
		{
			this.presenceField = value;
		}
	}
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class PlexTVAccountDeviceConnection
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

