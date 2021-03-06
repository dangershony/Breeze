﻿using System;
using Breeze.Wallet.Helpers;
using NBitcoin;
using Newtonsoft.Json;

namespace Breeze.Wallet.JsonConverters
{
    /// <summary>
    /// Converter used to convert <see cref="Network"/> to and from JSON.
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.JsonConverter" />
    public class NetworkConverter : JsonConverter
    {
        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Network);
        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return WalletHelpers.GetNetwork((string)reader.Value);
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((Network)value).ToString());
        }
    }
}
