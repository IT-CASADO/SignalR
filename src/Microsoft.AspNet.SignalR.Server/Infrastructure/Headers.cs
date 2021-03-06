﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.AspNet.SignalR.Server.Infrastructure
{
    // Helper methods for creating and consuming CallParameters.Headers and ResultParameters.Headers.
    internal static class Headers
    {
        public static IDictionary<string, string[]> New()
        {
            return new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
        }

        public static IDictionary<string, string[]> New(IDictionary<string, string[]> headers)
        {
            if (headers == null)
            {
                return New();
            }

            return new Dictionary<string, string[]>(headers, StringComparer.OrdinalIgnoreCase);
        }

        public static bool HasHeader(this IDictionary<string, string[]> headers,
            string name)
        {
            string[] values;
            if (!headers.TryGetValue(name, out values) || values == null)
            {
                return false;
            }

            return values.Any(value => !String.IsNullOrWhiteSpace(value));
        }

        public static IDictionary<string, string[]> SetHeader(this IDictionary<string, string[]> headers,
            string name, string value)
        {
            headers[name] = new[] { value };
            return headers;
        }

        public static IDictionary<string, string[]> SetHeader(this IDictionary<string, string[]> headers,
            string name, string[] values)
        {
            headers[name] = values;
            return headers;
        }

        public static IDictionary<string, string[]> AddHeader(this IDictionary<string, string[]> headers,
            string name, string value)
        {
            return AddHeader(headers, name, new[] { value });
        }

        public static IDictionary<string, string[]> AddHeader(this IDictionary<string, string[]> headers,
            string name, string[] value)
        {
            string[] values;
            if (headers.TryGetValue(name, out values))
            {
                headers[name] = values.Concat(value).ToArray();
            }
            else
            {
                headers[name] = value;
            }
            return headers;
        }

        public static string[] GetHeaders(this IDictionary<string, string[]> headers,
            string name)
        {
            string[] value;
            return headers != null && headers.TryGetValue(name, out value) ? value : null;
        }

        public static string GetHeader(this IDictionary<string, string[]> headers,
            string name)
        {
            var values = GetHeaders(headers, name);
            if (values == null)
            {
                return null;
            }

            switch (values.Length)
            {
                case 0:
                    return String.Empty;
                case 1:
                    return values[0];
                default:
                    return String.Join(",", values);
            }
        }
    }
}
