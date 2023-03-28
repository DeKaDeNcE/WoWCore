// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: bgs/low/pb/client/ets_types.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Bgs.Protocol {

  /// <summary>Holder for reflection information generated from bgs/low/pb/client/ets_types.proto</summary>
  public static partial class EtsTypesReflection {

    #region Descriptor
    /// <summary>File descriptor for bgs/low/pb/client/ets_types.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static EtsTypesReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CiFiZ3MvbG93L3BiL2NsaWVudC9ldHNfdHlwZXMucHJvdG8SDGJncy5wcm90",
            "b2NvbCIvCgxUaW1lU2VyaWVzSWQSDQoFZXBvY2gYASABKAQSEAoIcG9zaXRp",
            "b24YAiABKAQ="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Bgs.Protocol.TimeSeriesId), global::Bgs.Protocol.TimeSeriesId.Parser, new[]{ "Epoch", "Position" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class TimeSeriesId : pb::IMessage<TimeSeriesId> {
    private static readonly pb::MessageParser<TimeSeriesId> _parser = new pb::MessageParser<TimeSeriesId>(() => new TimeSeriesId());
    private pb::UnknownFieldSet _unknownFields;
    private int _hasBits0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<TimeSeriesId> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Bgs.Protocol.EtsTypesReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TimeSeriesId() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TimeSeriesId(TimeSeriesId other) : this() {
      _hasBits0 = other._hasBits0;
      epoch_ = other.epoch_;
      position_ = other.position_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TimeSeriesId Clone() {
      return new TimeSeriesId(this);
    }

    /// <summary>Field number for the "epoch" field.</summary>
    public const int EpochFieldNumber = 1;
    private readonly static ulong EpochDefaultValue = 0UL;

    private ulong epoch_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ulong Epoch {
      get { if ((_hasBits0 & 1) != 0) { return epoch_; } else { return EpochDefaultValue; } }
      set {
        _hasBits0 |= 1;
        epoch_ = value;
      }
    }
    /// <summary>Gets whether the "epoch" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool HasEpoch {
      get { return (_hasBits0 & 1) != 0; }
    }
    /// <summary>Clears the value of the "epoch" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearEpoch() {
      _hasBits0 &= ~1;
    }

    /// <summary>Field number for the "position" field.</summary>
    public const int PositionFieldNumber = 2;
    private readonly static ulong PositionDefaultValue = 0UL;

    private ulong position_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ulong Position {
      get { if ((_hasBits0 & 2) != 0) { return position_; } else { return PositionDefaultValue; } }
      set {
        _hasBits0 |= 2;
        position_ = value;
      }
    }
    /// <summary>Gets whether the "position" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool HasPosition {
      get { return (_hasBits0 & 2) != 0; }
    }
    /// <summary>Clears the value of the "position" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearPosition() {
      _hasBits0 &= ~2;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as TimeSeriesId);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(TimeSeriesId other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Epoch != other.Epoch) return false;
      if (Position != other.Position) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (HasEpoch) hash ^= Epoch.GetHashCode();
      if (HasPosition) hash ^= Position.GetHashCode();
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
      if (HasEpoch) {
        output.WriteRawTag(8);
        output.WriteUInt64(Epoch);
      }
      if (HasPosition) {
        output.WriteRawTag(16);
        output.WriteUInt64(Position);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (HasEpoch) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(Epoch);
      }
      if (HasPosition) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(Position);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(TimeSeriesId other) {
      if (other == null) {
        return;
      }
      if (other.HasEpoch) {
        Epoch = other.Epoch;
      }
      if (other.HasPosition) {
        Position = other.Position;
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
            Epoch = input.ReadUInt64();
            break;
          }
          case 16: {
            Position = input.ReadUInt64();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code