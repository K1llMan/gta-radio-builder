﻿using System;

using GtaProperties.UnrealTypes;

using UAssetAPI;
using UAssetAPI.DataAccess;
using UAssetAPI.PropertyTypes;
using UAssetAPI.UnrealTypes;
using UAssetAPI.UnrealTypes.Enums;

namespace GtaProperties.PropertyTypes
{
    /// <summary>
    /// Describes ScalarParameterValue
    /// </summary>
    public class TextureParameterPropertyData : PropertyData<FTextureParameter>
    {
        public TextureParameterPropertyData(FName name) : base(name)
        {

        }

        public TextureParameterPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new("TextureParameterValue");
        public override bool HasCustomStructSerialization => true;
        public override FName PropertyType => CurrentPropertyType;

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            FTextureParameter data = new()
            {
                ParameterInfo = new FMaterialParameterInfo {
                    Name = reader.ReadFName(),
                    Association = (EMaterialParameterAssociation)reader.ReadByte(),
                    Index = reader.ReadInt32(),
                },
                ParameterValue = new FPackageIndex(reader.ReadInt32()),
                ExpressionGUID = new Guid(reader.ReadBytes(16))
            };

            Value = data;
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            writer.Write(Value.ParameterInfo.Name);
            writer.Write((byte)Value.ParameterInfo.Association);
            writer.Write(Value.ParameterInfo.Index);
            writer.Write(Value.ParameterValue.Index);
            writer.Write(Value.ExpressionGUID.ToByteArray());

            return sizeof(short);
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d, UAsset asset)
        {

        }
    }
}