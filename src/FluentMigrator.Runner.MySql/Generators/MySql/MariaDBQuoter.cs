#region License
// Copyright (c) 2024, Fluent Migrator Project
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System.Globalization;

namespace FluentMigrator.Runner.Generators.MySql
{
    public class MariaDBQuoter : MySqlQuoter
    {
        public override string FormatSystemMethods(SystemMethods value)
        {
            switch (value)
            {
                case SystemMethods.NewGuid:
                case SystemMethods.NewSequentialId:
                    return "UUID()";
                case SystemMethods.CurrentDateTime:
                    return "CURRENT_TIMESTAMP(6)";
                case SystemMethods.CurrentUTCDateTime:
                    return "UTC_TIMESTAMP(6)";
                case SystemMethods.CurrentUser:
                    return "CURRENT_USER()";
            }

            return base.FormatSystemMethods(value);
        }

        public override string FormatTimeSpan(System.TimeSpan value)
        {
            return string.Format(CultureInfo.InvariantCulture,
                "{0}{1:00}:{2:00}:{3:00}.{4:000}{0}",
                ValueQuote,
                value.Hours + (value.Days * 24),
                value.Minutes,
                value.Seconds,
                value.Milliseconds);
        }

        public override string FormatDateTime(System.DateTime value)
        {
            return ValueQuote + value.ToString("yyyy-MM-ddTHH:mm:ss.ffffff", CultureInfo.InvariantCulture) + ValueQuote;
        }

        public override string FormatDateTimeOffset(System.DateTimeOffset value)
        {
            return ValueQuote + value.ToString("yyyy-MM-ddTHH:mm:ss.ffffff", CultureInfo.InvariantCulture) + ValueQuote;
        }

#if NET6_0_OR_GREATER
        public override string FormatTimeOnly(System.TimeOnly value)
        {
            return ValueQuote + value.ToString("HH:mm:ss.ffffff", CultureInfo.InvariantCulture) + ValueQuote;
        }
#endif
    }
}
