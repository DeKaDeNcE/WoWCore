// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: bgs/low/pb/client/voice_types.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Bgs.Protocol {

  /// <summary>Holder for reflection information generated from bgs/low/pb/client/voice_types.proto</summary>
  public static partial class VoiceTypesReflection {

    #region Descriptor
    /// <summary>File descriptor for bgs/low/pb/client/voice_types.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static VoiceTypesReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CiNiZ3MvbG93L3BiL2NsaWVudC92b2ljZV90eXBlcy5wcm90bxIMYmdzLnBy",
            "b3RvY29sIs8BChBWb2ljZUNyZWRlbnRpYWxzEhAKCHZvaWNlX2lkGAEgASgJ",
            "Eg0KBXRva2VuGAIgASgJEgsKA3VybBgDIAEoCRJBCglqb2luX3R5cGUYBCAB",
            "KA4yGy5iZ3MucHJvdG9jb2wuVm9pY2VKb2luVHlwZToRVk9JQ0VfSk9JTl9O",
            "T1JNQUwSSgoLbXV0ZV9yZWFzb24YBSABKA4yHS5iZ3MucHJvdG9jb2wuVm9p",
            "Y2VNdXRlUmVhc29uOhZWT0lDRV9NVVRFX1JFQVNPTl9OT05FKjwKDVZvaWNl",
            "Sm9pblR5cGUSFQoRVk9JQ0VfSk9JTl9OT1JNQUwQABIUChBWT0lDRV9KT0lO",
            "X01VVEVEEAEqowEKD1ZvaWNlTXV0ZVJlYXNvbhIaChZWT0lDRV9NVVRFX1JF",
            "QVNPTl9OT05FEAASMgouVk9JQ0VfTVVURV9SRUFTT05fUEFSRU5UQUxfQ09O",
            "VFJPTF9MSVNURU5fT05MWRABEh8KG1ZPSUNFX01VVEVfUkVBU09OX1JFUVVF",
            "U1RFRBACEh8KG1ZPSUNFX01VVEVfUkVBU09OX1NRVUVMQ0hFRBADKkQKFFZv",
            "aWNlUHJvdmlkZXJWZXJzaW9uEhUKEVZPSUNFX1BST1ZJREVSX1Y0EAASFQoR",
            "Vk9JQ0VfUFJPVklERVJfVjUQAQ=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::Bgs.Protocol.VoiceJoinType), typeof(global::Bgs.Protocol.VoiceMuteReason), typeof(global::Bgs.Protocol.VoiceProviderVersion), }, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Bgs.Protocol.VoiceCredentials), global::Bgs.Protocol.VoiceCredentials.Parser, new[]{ "VoiceId", "Token", "Url", "JoinType", "MuteReason" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Enums
  public enum VoiceJoinType {
    [pbr::OriginalName("VOICE_JOIN_NORMAL")] VoiceJoinNormal = 0,
    [pbr::OriginalName("VOICE_JOIN_MUTED")] VoiceJoinMuted = 1,
  }

  public enum VoiceMuteReason {
    [pbr::OriginalName("VOICE_MUTE_REASON_NONE")] None = 0,
    [pbr::OriginalName("VOICE_MUTE_REASON_PARENTAL_CONTROL_LISTEN_ONLY")] ParentalControlListenOnly = 1,
    [pbr::OriginalName("VOICE_MUTE_REASON_REQUESTED")] Requested = 2,
    [pbr::OriginalName("VOICE_MUTE_REASON_SQUELCHED")] Squelched = 3,
  }

  public enum VoiceProviderVersion {
    [pbr::OriginalName("VOICE_PROVIDER_V4")] VoiceProviderV4 = 0,
    [pbr::OriginalName("VOICE_PROVIDER_V5")] VoiceProviderV5 = 1,
  }

  #endregion

  #region Messages
  public sealed partial class VoiceCredentials : pb::IMessage<VoiceCredentials> {
    private static readonly pb::MessageParser<VoiceCredentials> _parser = new pb::MessageParser<VoiceCredentials>(() => new VoiceCredentials());
    private pb::UnknownFieldSet _unknownFields;
    private int _hasBits0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<VoiceCredentials> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Bgs.Protocol.VoiceTypesReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public VoiceCredentials() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public VoiceCredentials(VoiceCredentials other) : this() {
      _hasBits0 = other._hasBits0;
      voiceId_ = other.voiceId_;
      token_ = other.token_;
      url_ = other.url_;
      joinType_ = other.joinType_;
      muteReason_ = other.muteReason_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public VoiceCredentials Clone() {
      return new VoiceCredentials(this);
    }

    /// <summary>Field number for the "voice_id" field.</summary>
    public const int VoiceIdFieldNumber = 1;
    private readonly static string VoiceIdDefaultValue = "";

    private string voiceId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string VoiceId {
      get { return voiceId_ ?? VoiceIdDefaultValue; }
      set {
        voiceId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }
    /// <summary>Gets whether the "voice_id" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool HasVoiceId {
      get { return voiceId_ != null; }
    }
    /// <summary>Clears the value of the "voice_id" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearVoiceId() {
      voiceId_ = null;
    }

    /// <summary>Field number for the "token" field.</summary>
    public const int TokenFieldNumber = 2;
    private readonly static string TokenDefaultValue = "";

    private string token_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Token {
      get { return token_ ?? TokenDefaultValue; }
      set {
        token_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }
    /// <summary>Gets whether the "token" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool HasToken {
      get { return token_ != null; }
    }
    /// <summary>Clears the value of the "token" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearToken() {
      token_ = null;
    }

    /// <summary>Field number for the "url" field.</summary>
    public const int UrlFieldNumber = 3;
    private readonly static string UrlDefaultValue = "";

    private string url_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Url {
      get { return url_ ?? UrlDefaultValue; }
      set {
        url_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }
    /// <summary>Gets whether the "url" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool HasUrl {
      get { return url_ != null; }
    }
    /// <summary>Clears the value of the "url" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearUrl() {
      url_ = null;
    }

    /// <summary>Field number for the "join_type" field.</summary>
    public const int JoinTypeFieldNumber = 4;
    private readonly static global::Bgs.Protocol.VoiceJoinType JoinTypeDefaultValue = global::Bgs.Protocol.VoiceJoinType.VoiceJoinNormal;

    private global::Bgs.Protocol.VoiceJoinType joinType_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Bgs.Protocol.VoiceJoinType JoinType {
      get { if ((_hasBits0 & 1) != 0) { return joinType_; } else { return JoinTypeDefaultValue; } }
      set {
        _hasBits0 |= 1;
        joinType_ = value;
      }
    }
    /// <summary>Gets whether the "join_type" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool HasJoinType {
      get { return (_hasBits0 & 1) != 0; }
    }
    /// <summary>Clears the value of the "join_type" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearJoinType() {
      _hasBits0 &= ~1;
    }

    /// <summary>Field number for the "mute_reason" field.</summary>
    public const int MuteReasonFieldNumber = 5;
    private readonly static global::Bgs.Protocol.VoiceMuteReason MuteReasonDefaultValue = global::Bgs.Protocol.VoiceMuteReason.None;

    private global::Bgs.Protocol.VoiceMuteReason muteReason_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Bgs.Protocol.VoiceMuteReason MuteReason {
      get { if ((_hasBits0 & 2) != 0) { return muteReason_; } else { return MuteReasonDefaultValue; } }
      set {
        _hasBits0 |= 2;
        muteReason_ = value;
      }
    }
    /// <summary>Gets whether the "mute_reason" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool HasMuteReason {
      get { return (_hasBits0 & 2) != 0; }
    }
    /// <summary>Clears the value of the "mute_reason" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearMuteReason() {
      _hasBits0 &= ~2;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as VoiceCredentials);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(VoiceCredentials other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (VoiceId != other.VoiceId) return false;
      if (Token != other.Token) return false;
      if (Url != other.Url) return false;
      if (JoinType != other.JoinType) return false;
      if (MuteReason != other.MuteReason) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (HasVoiceId) hash ^= VoiceId.GetHashCode();
      if (HasToken) hash ^= Token.GetHashCode();
      if (HasUrl) hash ^= Url.GetHashCode();
      if (HasJoinType) hash ^= JoinType.GetHashCode();
      if (HasMuteReason) hash ^= MuteReason.GetHashCode();
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
      if (HasVoiceId) {
        output.WriteRawTag(10);
        output.WriteString(VoiceId);
      }
      if (HasToken) {
        output.WriteRawTag(18);
        output.WriteString(Token);
      }
      if (HasUrl) {
        output.WriteRawTag(26);
        output.WriteString(Url);
      }
      if (HasJoinType) {
        output.WriteRawTag(32);
        output.WriteEnum((int) JoinType);
      }
      if (HasMuteReason) {
        output.WriteRawTag(40);
        output.WriteEnum((int) MuteReason);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (HasVoiceId) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(VoiceId);
      }
      if (HasToken) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Token);
      }
      if (HasUrl) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Url);
      }
      if (HasJoinType) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) JoinType);
      }
      if (HasMuteReason) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) MuteReason);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(VoiceCredentials other) {
      if (other == null) {
        return;
      }
      if (other.HasVoiceId) {
        VoiceId = other.VoiceId;
      }
      if (other.HasToken) {
        Token = other.Token;
      }
      if (other.HasUrl) {
        Url = other.Url;
      }
      if (other.HasJoinType) {
        JoinType = other.JoinType;
      }
      if (other.HasMuteReason) {
        MuteReason = other.MuteReason;
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
          case 10: {
            VoiceId = input.ReadString();
            break;
          }
          case 18: {
            Token = input.ReadString();
            break;
          }
          case 26: {
            Url = input.ReadString();
            break;
          }
          case 32: {
            JoinType = (global::Bgs.Protocol.VoiceJoinType) input.ReadEnum();
            break;
          }
          case 40: {
            MuteReason = (global::Bgs.Protocol.VoiceMuteReason) input.ReadEnum();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code