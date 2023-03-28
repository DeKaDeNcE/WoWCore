// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: bgs/low/pb/client/global_extensions/method_options.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Bgs.Protocol {

  /// <summary>Holder for reflection information generated from bgs/low/pb/client/global_extensions/method_options.proto</summary>
  public static partial class MethodOptionsReflection {

    #region Descriptor
    /// <summary>File descriptor for bgs/low/pb/client/global_extensions/method_options.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static MethodOptionsReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CjhiZ3MvbG93L3BiL2NsaWVudC9nbG9iYWxfZXh0ZW5zaW9ucy9tZXRob2Rf",
            "b3B0aW9ucy5wcm90bxIMYmdzLnByb3RvY29sGiBnb29nbGUvcHJvdG9idWYv",
            "ZGVzY3JpcHRvci5wcm90bxoxYmdzL2xvdy9wYi9jbGllbnQvZ2xvYmFsX2V4",
            "dGVuc2lvbnMvcm91dGluZy5wcm90byLoAgoQQkdTTWV0aG9kT3B0aW9ucxIK",
            "CgJpZBgBIAEoDRJqChdjbGllbnRfaWRlbnRpdHlfcm91dGluZxgCIAEoDjIn",
            "LmJncy5wcm90b2NvbC5DbGllbnRJZGVudGl0eVJvdXRpbmdUeXBlOiBDTElF",
            "TlRfSURFTlRJVFlfUk9VVElOR19ESVNBQkxFRBIVCg1lbmFibGVfZmFub3V0",
            "GAMgASgIEiEKGWxlZ2FjeV9mYW5vdXRfcmVwbGFjZW1lbnQYBCABKAkSEwoL",
            "Zm9yd2FyZF9rZXkYBSABKAkSEgoKaWRlbXBvdGVudBgGIAEoCBImCh5oYW5k",
            "bGVfZGVzdGluYXRpb25fdW5yZWFjaGFibGUYByABKAgSHgoWY3VzdG9tX3Jl",
            "Z2lvbl9yZXNvbHZlchgIIAEoCRIfChdleHBsaWNpdF9yZWdpb25fcm91dGlu",
            "ZxgJIAEoCBIQCghvYnNvbGV0ZRgKIAEoCDpYCg5tZXRob2Rfb3B0aW9ucxIe",
            "Lmdvb2dsZS5wcm90b2J1Zi5NZXRob2RPcHRpb25zGJC/BSABKAsyHi5iZ3Mu",
            "cHJvdG9jb2wuQkdTTWV0aG9kT3B0aW9uc0IiCgxiZ3MucHJvdG9jb2xCEk1l",
            "dGhvZE9wdGlvbnNQcm90bw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Google.Protobuf.DescriptorReflection.Descriptor, global::Bgs.Protocol.RoutingReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pb::Extension[] { MethodOptionsExtensions.MethodOptions_ }, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Bgs.Protocol.BGSMethodOptions), global::Bgs.Protocol.BGSMethodOptions.Parser, new[]{ "Id", "ClientIdentityRouting", "EnableFanout", "LegacyFanoutReplacement", "ForwardKey", "Idempotent", "HandleDestinationUnreachable", "CustomRegionResolver", "ExplicitRegionRouting", "Obsolete" }, null, null, null, null)
          }));
    }
    #endregion

  }
  /// <summary>Holder for extension identifiers generated from the top level of bgs/low/pb/client/global_extensions/method_options.proto</summary>
  public static partial class MethodOptionsExtensions {
    public static readonly pb::Extension<global::Google.Protobuf.MethodOptions, global::Bgs.Protocol.BGSMethodOptions> MethodOptions_ =
      new pb::Extension<global::Google.Protobuf.MethodOptions, global::Bgs.Protocol.BGSMethodOptions>(90000, pb::FieldCodec.ForMessage(720002, global::Bgs.Protocol.BGSMethodOptions.Parser));
  }

  #region Messages
  public sealed partial class BGSMethodOptions : pb::IMessage<BGSMethodOptions> {
    private static readonly pb::MessageParser<BGSMethodOptions> _parser = new pb::MessageParser<BGSMethodOptions>(() => new BGSMethodOptions());
    private pb::UnknownFieldSet _unknownFields;
    private int _hasBits0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<BGSMethodOptions> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Bgs.Protocol.MethodOptionsReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BGSMethodOptions() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BGSMethodOptions(BGSMethodOptions other) : this() {
      _hasBits0 = other._hasBits0;
      id_ = other.id_;
      clientIdentityRouting_ = other.clientIdentityRouting_;
      enableFanout_ = other.enableFanout_;
      legacyFanoutReplacement_ = other.legacyFanoutReplacement_;
      forwardKey_ = other.forwardKey_;
      idempotent_ = other.idempotent_;
      handleDestinationUnreachable_ = other.handleDestinationUnreachable_;
      customRegionResolver_ = other.customRegionResolver_;
      explicitRegionRouting_ = other.explicitRegionRouting_;
      obsolete_ = other.obsolete_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BGSMethodOptions Clone() {
      return new BGSMethodOptions(this);
    }

    /// <summary>Field number for the "id" field.</summary>
    public const int IdFieldNumber = 1;
    private readonly static uint IdDefaultValue = 0;

    private uint id_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint Id {
      get { if ((_hasBits0 & 1) != 0) { return id_; } else { return IdDefaultValue; } }
      set {
        _hasBits0 |= 1;
        id_ = value;
      }
    }
    /// <summary>Gets whether the "id" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool HasId {
      get { return (_hasBits0 & 1) != 0; }
    }
    /// <summary>Clears the value of the "id" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearId() {
      _hasBits0 &= ~1;
    }

    /// <summary>Field number for the "client_identity_routing" field.</summary>
    public const int ClientIdentityRoutingFieldNumber = 2;
    private readonly static global::Bgs.Protocol.ClientIdentityRoutingType ClientIdentityRoutingDefaultValue = global::Bgs.Protocol.ClientIdentityRoutingType.ClientIdentityRoutingDisabled;

    private global::Bgs.Protocol.ClientIdentityRoutingType clientIdentityRouting_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Bgs.Protocol.ClientIdentityRoutingType ClientIdentityRouting {
      get { if ((_hasBits0 & 2) != 0) { return clientIdentityRouting_; } else { return ClientIdentityRoutingDefaultValue; } }
      set {
        _hasBits0 |= 2;
        clientIdentityRouting_ = value;
      }
    }
    /// <summary>Gets whether the "client_identity_routing" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool HasClientIdentityRouting {
      get { return (_hasBits0 & 2) != 0; }
    }
    /// <summary>Clears the value of the "client_identity_routing" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearClientIdentityRouting() {
      _hasBits0 &= ~2;
    }

    /// <summary>Field number for the "enable_fanout" field.</summary>
    public const int EnableFanoutFieldNumber = 3;
    private readonly static bool EnableFanoutDefaultValue = false;

    private bool enableFanout_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool EnableFanout {
      get { if ((_hasBits0 & 4) != 0) { return enableFanout_; } else { return EnableFanoutDefaultValue; } }
      set {
        _hasBits0 |= 4;
        enableFanout_ = value;
      }
    }
    /// <summary>Gets whether the "enable_fanout" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool HasEnableFanout {
      get { return (_hasBits0 & 4) != 0; }
    }
    /// <summary>Clears the value of the "enable_fanout" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearEnableFanout() {
      _hasBits0 &= ~4;
    }

    /// <summary>Field number for the "legacy_fanout_replacement" field.</summary>
    public const int LegacyFanoutReplacementFieldNumber = 4;
    private readonly static string LegacyFanoutReplacementDefaultValue = "";

    private string legacyFanoutReplacement_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string LegacyFanoutReplacement {
      get { return legacyFanoutReplacement_ ?? LegacyFanoutReplacementDefaultValue; }
      set {
        legacyFanoutReplacement_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }
    /// <summary>Gets whether the "legacy_fanout_replacement" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool HasLegacyFanoutReplacement {
      get { return legacyFanoutReplacement_ != null; }
    }
    /// <summary>Clears the value of the "legacy_fanout_replacement" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearLegacyFanoutReplacement() {
      legacyFanoutReplacement_ = null;
    }

    /// <summary>Field number for the "forward_key" field.</summary>
    public const int ForwardKeyFieldNumber = 5;
    private readonly static string ForwardKeyDefaultValue = "";

    private string forwardKey_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string ForwardKey {
      get { return forwardKey_ ?? ForwardKeyDefaultValue; }
      set {
        forwardKey_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }
    /// <summary>Gets whether the "forward_key" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool HasForwardKey {
      get { return forwardKey_ != null; }
    }
    /// <summary>Clears the value of the "forward_key" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearForwardKey() {
      forwardKey_ = null;
    }

    /// <summary>Field number for the "idempotent" field.</summary>
    public const int IdempotentFieldNumber = 6;
    private readonly static bool IdempotentDefaultValue = false;

    private bool idempotent_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Idempotent {
      get { if ((_hasBits0 & 8) != 0) { return idempotent_; } else { return IdempotentDefaultValue; } }
      set {
        _hasBits0 |= 8;
        idempotent_ = value;
      }
    }
    /// <summary>Gets whether the "idempotent" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool HasIdempotent {
      get { return (_hasBits0 & 8) != 0; }
    }
    /// <summary>Clears the value of the "idempotent" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearIdempotent() {
      _hasBits0 &= ~8;
    }

    /// <summary>Field number for the "handle_destination_unreachable" field.</summary>
    public const int HandleDestinationUnreachableFieldNumber = 7;
    private readonly static bool HandleDestinationUnreachableDefaultValue = false;

    private bool handleDestinationUnreachable_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool HandleDestinationUnreachable {
      get { if ((_hasBits0 & 16) != 0) { return handleDestinationUnreachable_; } else { return HandleDestinationUnreachableDefaultValue; } }
      set {
        _hasBits0 |= 16;
        handleDestinationUnreachable_ = value;
      }
    }
    /// <summary>Gets whether the "handle_destination_unreachable" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool HasHandleDestinationUnreachable {
      get { return (_hasBits0 & 16) != 0; }
    }
    /// <summary>Clears the value of the "handle_destination_unreachable" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearHandleDestinationUnreachable() {
      _hasBits0 &= ~16;
    }

    /// <summary>Field number for the "custom_region_resolver" field.</summary>
    public const int CustomRegionResolverFieldNumber = 8;
    private readonly static string CustomRegionResolverDefaultValue = "";

    private string customRegionResolver_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string CustomRegionResolver {
      get { return customRegionResolver_ ?? CustomRegionResolverDefaultValue; }
      set {
        customRegionResolver_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }
    /// <summary>Gets whether the "custom_region_resolver" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool HasCustomRegionResolver {
      get { return customRegionResolver_ != null; }
    }
    /// <summary>Clears the value of the "custom_region_resolver" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearCustomRegionResolver() {
      customRegionResolver_ = null;
    }

    /// <summary>Field number for the "explicit_region_routing" field.</summary>
    public const int ExplicitRegionRoutingFieldNumber = 9;
    private readonly static bool ExplicitRegionRoutingDefaultValue = false;

    private bool explicitRegionRouting_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool ExplicitRegionRouting {
      get { if ((_hasBits0 & 32) != 0) { return explicitRegionRouting_; } else { return ExplicitRegionRoutingDefaultValue; } }
      set {
        _hasBits0 |= 32;
        explicitRegionRouting_ = value;
      }
    }
    /// <summary>Gets whether the "explicit_region_routing" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool HasExplicitRegionRouting {
      get { return (_hasBits0 & 32) != 0; }
    }
    /// <summary>Clears the value of the "explicit_region_routing" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearExplicitRegionRouting() {
      _hasBits0 &= ~32;
    }

    /// <summary>Field number for the "obsolete" field.</summary>
    public const int ObsoleteFieldNumber = 10;
    private readonly static bool ObsoleteDefaultValue = false;

    private bool obsolete_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Obsolete {
      get { if ((_hasBits0 & 64) != 0) { return obsolete_; } else { return ObsoleteDefaultValue; } }
      set {
        _hasBits0 |= 64;
        obsolete_ = value;
      }
    }
    /// <summary>Gets whether the "obsolete" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool HasObsolete {
      get { return (_hasBits0 & 64) != 0; }
    }
    /// <summary>Clears the value of the "obsolete" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearObsolete() {
      _hasBits0 &= ~64;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as BGSMethodOptions);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(BGSMethodOptions other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (ClientIdentityRouting != other.ClientIdentityRouting) return false;
      if (EnableFanout != other.EnableFanout) return false;
      if (LegacyFanoutReplacement != other.LegacyFanoutReplacement) return false;
      if (ForwardKey != other.ForwardKey) return false;
      if (Idempotent != other.Idempotent) return false;
      if (HandleDestinationUnreachable != other.HandleDestinationUnreachable) return false;
      if (CustomRegionResolver != other.CustomRegionResolver) return false;
      if (ExplicitRegionRouting != other.ExplicitRegionRouting) return false;
      if (Obsolete != other.Obsolete) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (HasId) hash ^= Id.GetHashCode();
      if (HasClientIdentityRouting) hash ^= ClientIdentityRouting.GetHashCode();
      if (HasEnableFanout) hash ^= EnableFanout.GetHashCode();
      if (HasLegacyFanoutReplacement) hash ^= LegacyFanoutReplacement.GetHashCode();
      if (HasForwardKey) hash ^= ForwardKey.GetHashCode();
      if (HasIdempotent) hash ^= Idempotent.GetHashCode();
      if (HasHandleDestinationUnreachable) hash ^= HandleDestinationUnreachable.GetHashCode();
      if (HasCustomRegionResolver) hash ^= CustomRegionResolver.GetHashCode();
      if (HasExplicitRegionRouting) hash ^= ExplicitRegionRouting.GetHashCode();
      if (HasObsolete) hash ^= Obsolete.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (HasId) {
        output.WriteRawTag(8);
        output.WriteUInt32(Id);
      }
      if (HasClientIdentityRouting) {
        output.WriteRawTag(16);
        output.WriteEnum((int) ClientIdentityRouting);
      }
      if (HasEnableFanout) {
        output.WriteRawTag(24);
        output.WriteBool(EnableFanout);
      }
      if (HasLegacyFanoutReplacement) {
        output.WriteRawTag(34);
        output.WriteString(LegacyFanoutReplacement);
      }
      if (HasForwardKey) {
        output.WriteRawTag(42);
        output.WriteString(ForwardKey);
      }
      if (HasIdempotent) {
        output.WriteRawTag(48);
        output.WriteBool(Idempotent);
      }
      if (HasHandleDestinationUnreachable) {
        output.WriteRawTag(56);
        output.WriteBool(HandleDestinationUnreachable);
      }
      if (HasCustomRegionResolver) {
        output.WriteRawTag(66);
        output.WriteString(CustomRegionResolver);
      }
      if (HasExplicitRegionRouting) {
        output.WriteRawTag(72);
        output.WriteBool(ExplicitRegionRouting);
      }
      if (HasObsolete) {
        output.WriteRawTag(80);
        output.WriteBool(Obsolete);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (HasId) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Id);
      }
      if (HasClientIdentityRouting) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) ClientIdentityRouting);
      }
      if (HasEnableFanout) {
        size += 1 + 1;
      }
      if (HasLegacyFanoutReplacement) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(LegacyFanoutReplacement);
      }
      if (HasForwardKey) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(ForwardKey);
      }
      if (HasIdempotent) {
        size += 1 + 1;
      }
      if (HasHandleDestinationUnreachable) {
        size += 1 + 1;
      }
      if (HasCustomRegionResolver) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(CustomRegionResolver);
      }
      if (HasExplicitRegionRouting) {
        size += 1 + 1;
      }
      if (HasObsolete) {
        size += 1 + 1;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(BGSMethodOptions other) {
      if (other == null) {
        return;
      }
      if (other.HasId) {
        Id = other.Id;
      }
      if (other.HasClientIdentityRouting) {
        ClientIdentityRouting = other.ClientIdentityRouting;
      }
      if (other.HasEnableFanout) {
        EnableFanout = other.EnableFanout;
      }
      if (other.HasLegacyFanoutReplacement) {
        LegacyFanoutReplacement = other.LegacyFanoutReplacement;
      }
      if (other.HasForwardKey) {
        ForwardKey = other.ForwardKey;
      }
      if (other.HasIdempotent) {
        Idempotent = other.Idempotent;
      }
      if (other.HasHandleDestinationUnreachable) {
        HandleDestinationUnreachable = other.HandleDestinationUnreachable;
      }
      if (other.HasCustomRegionResolver) {
        CustomRegionResolver = other.CustomRegionResolver;
      }
      if (other.HasExplicitRegionRouting) {
        ExplicitRegionRouting = other.ExplicitRegionRouting;
      }
      if (other.HasObsolete) {
        Obsolete = other.Obsolete;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Id = input.ReadUInt32();
            break;
          }
          case 16: {
            ClientIdentityRouting = (global::Bgs.Protocol.ClientIdentityRoutingType) input.ReadEnum();
            break;
          }
          case 24: {
            EnableFanout = input.ReadBool();
            break;
          }
          case 34: {
            LegacyFanoutReplacement = input.ReadString();
            break;
          }
          case 42: {
            ForwardKey = input.ReadString();
            break;
          }
          case 48: {
            Idempotent = input.ReadBool();
            break;
          }
          case 56: {
            HandleDestinationUnreachable = input.ReadBool();
            break;
          }
          case 66: {
            CustomRegionResolver = input.ReadString();
            break;
          }
          case 72: {
            ExplicitRegionRouting = input.ReadBool();
            break;
          }
          case 80: {
            Obsolete = input.ReadBool();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code