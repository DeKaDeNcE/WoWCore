// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: bgs/low/pb/client/semantic_version.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Bgs.Protocol {

  /// <summary>Holder for reflection information generated from bgs/low/pb/client/semantic_version.proto</summary>
  public static partial class SemanticVersionReflection {

    #region Descriptor
    /// <summary>File descriptor for bgs/low/pb/client/semantic_version.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static SemanticVersionReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CihiZ3MvbG93L3BiL2NsaWVudC9zZW1hbnRpY192ZXJzaW9uLnByb3RvEgxi",
            "Z3MucHJvdG9jb2wibgoPU2VtYW50aWNWZXJzaW9uEhUKDW1ham9yX3ZlcnNp",
            "b24YASABKA0SFQoNbWlub3JfdmVyc2lvbhgCIAEoDRIVCg1wYXRjaF92ZXJz",
            "aW9uGAMgASgNEhYKDnZlcnNpb25fc3RyaW5nGAQgASgJ"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Bgs.Protocol.SemanticVersion), global::Bgs.Protocol.SemanticVersion.Parser, new[]{ "MajorVersion", "MinorVersion", "PatchVersion", "VersionString" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class SemanticVersion : pb::IMessage<SemanticVersion> {
    private static readonly pb::MessageParser<SemanticVersion> _parser = new pb::MessageParser<SemanticVersion>(() => new SemanticVersion());
    private pb::UnknownFieldSet _unknownFields;
    private int _hasBits0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<SemanticVersion> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Bgs.Protocol.SemanticVersionReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SemanticVersion() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SemanticVersion(SemanticVersion other) : this() {
      _hasBits0 = other._hasBits0;
      majorVersion_ = other.majorVersion_;
      minorVersion_ = other.minorVersion_;
      patchVersion_ = other.patchVersion_;
      versionString_ = other.versionString_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SemanticVersion Clone() {
      return new SemanticVersion(this);
    }

    /// <summary>Field number for the "major_version" field.</summary>
    public const int MajorVersionFieldNumber = 1;
    private readonly static uint MajorVersionDefaultValue = 0;

    private uint majorVersion_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint MajorVersion {
      get { if ((_hasBits0 & 1) != 0) { return majorVersion_; } else { return MajorVersionDefaultValue; } }
      set {
        _hasBits0 |= 1;
        majorVersion_ = value;
      }
    }
    /// <summary>Gets whether the "major_version" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool HasMajorVersion {
      get { return (_hasBits0 & 1) != 0; }
    }
    /// <summary>Clears the value of the "major_version" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearMajorVersion() {
      _hasBits0 &= ~1;
    }

    /// <summary>Field number for the "minor_version" field.</summary>
    public const int MinorVersionFieldNumber = 2;
    private readonly static uint MinorVersionDefaultValue = 0;

    private uint minorVersion_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint MinorVersion {
      get { if ((_hasBits0 & 2) != 0) { return minorVersion_; } else { return MinorVersionDefaultValue; } }
      set {
        _hasBits0 |= 2;
        minorVersion_ = value;
      }
    }
    /// <summary>Gets whether the "minor_version" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool HasMinorVersion {
      get { return (_hasBits0 & 2) != 0; }
    }
    /// <summary>Clears the value of the "minor_version" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearMinorVersion() {
      _hasBits0 &= ~2;
    }

    /// <summary>Field number for the "patch_version" field.</summary>
    public const int PatchVersionFieldNumber = 3;
    private readonly static uint PatchVersionDefaultValue = 0;

    private uint patchVersion_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint PatchVersion {
      get { if ((_hasBits0 & 4) != 0) { return patchVersion_; } else { return PatchVersionDefaultValue; } }
      set {
        _hasBits0 |= 4;
        patchVersion_ = value;
      }
    }
    /// <summary>Gets whether the "patch_version" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool HasPatchVersion {
      get { return (_hasBits0 & 4) != 0; }
    }
    /// <summary>Clears the value of the "patch_version" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearPatchVersion() {
      _hasBits0 &= ~4;
    }

    /// <summary>Field number for the "version_string" field.</summary>
    public const int VersionStringFieldNumber = 4;
    private readonly static string VersionStringDefaultValue = "";

    private string versionString_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string VersionString {
      get { return versionString_ ?? VersionStringDefaultValue; }
      set {
        versionString_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }
    /// <summary>Gets whether the "version_string" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool HasVersionString {
      get { return versionString_ != null; }
    }
    /// <summary>Clears the value of the "version_string" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearVersionString() {
      versionString_ = null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as SemanticVersion);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(SemanticVersion other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (MajorVersion != other.MajorVersion) return false;
      if (MinorVersion != other.MinorVersion) return false;
      if (PatchVersion != other.PatchVersion) return false;
      if (VersionString != other.VersionString) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (HasMajorVersion) hash ^= MajorVersion.GetHashCode();
      if (HasMinorVersion) hash ^= MinorVersion.GetHashCode();
      if (HasPatchVersion) hash ^= PatchVersion.GetHashCode();
      if (HasVersionString) hash ^= VersionString.GetHashCode();
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
      if (HasMajorVersion) {
        output.WriteRawTag(8);
        output.WriteUInt32(MajorVersion);
      }
      if (HasMinorVersion) {
        output.WriteRawTag(16);
        output.WriteUInt32(MinorVersion);
      }
      if (HasPatchVersion) {
        output.WriteRawTag(24);
        output.WriteUInt32(PatchVersion);
      }
      if (HasVersionString) {
        output.WriteRawTag(34);
        output.WriteString(VersionString);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (HasMajorVersion) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(MajorVersion);
      }
      if (HasMinorVersion) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(MinorVersion);
      }
      if (HasPatchVersion) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(PatchVersion);
      }
      if (HasVersionString) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(VersionString);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(SemanticVersion other) {
      if (other == null) {
        return;
      }
      if (other.HasMajorVersion) {
        MajorVersion = other.MajorVersion;
      }
      if (other.HasMinorVersion) {
        MinorVersion = other.MinorVersion;
      }
      if (other.HasPatchVersion) {
        PatchVersion = other.PatchVersion;
      }
      if (other.HasVersionString) {
        VersionString = other.VersionString;
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
            MajorVersion = input.ReadUInt32();
            break;
          }
          case 16: {
            MinorVersion = input.ReadUInt32();
            break;
          }
          case 24: {
            PatchVersion = input.ReadUInt32();
            break;
          }
          case 34: {
            VersionString = input.ReadString();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code